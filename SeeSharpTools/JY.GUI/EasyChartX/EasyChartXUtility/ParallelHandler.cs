using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeeSharpTools.JY.GUI.EasyChartXData;

namespace SeeSharpTools.JY.GUI.EasyChartXUtility
{
    internal class ParallelHandler
    {
        const int MinParallelCalcCount = 8000;

        private readonly ParallelOptions _option;
        private readonly object _parallelLock = new object();

        //每个计算块的大小
        private int _blockSize;
        private int _indexOffset;

        private readonly DataEntity _dataEntity;
        private readonly PlotBuffer _buffer;
        private IList<double> _datas;
        private readonly DataCheckParameters _dataCheckParams;

        public ParallelHandler(DataEntity dataEntity, DataCheckParameters dataCheckParams)
        {
            this._dataEntity = dataEntity;
            this._buffer = dataEntity.PlotBuf;
            this._option = new ParallelOptions();
            this._indexOffset = 0;
            this._dataCheckParams = dataCheckParams;
            // 计算限制型配置并行度为内核个数
            _option.MaxDegreeOfParallelism = Environment.ProcessorCount;
//            _option.MaxDegreeOfParallelism = 1;

            this._maxDatas = new double[_option.MaxDegreeOfParallelism];
            this._minDatas = new double[_option.MaxDegreeOfParallelism];
        }

        #region Invalid Data Check

        private Dictionary<int, double> _invalidBuf; 
        public void InvalidDataCheck(IList<double> checkData, Dictionary<int, double> invalidBuf)
        {
            if (_dataCheckParams.IsCheckDisabled() || null == checkData || 0 == checkData.Count)
            {
                return;
            }
            this._datas = checkData;
            this._invalidBuf = invalidBuf;
            _blockSize = GetBlockSize(checkData.Count);
            _indexOffset = 0;
            if (!_dataCheckParams.CheckNaN)
            {
                // 如果不校验Nan则将非法数据替换为Nan
                Parallel.For(0, _option.MaxDegreeOfParallelism, _option, CheckDataExceedRange);
            }
            else
            {
                if (_dataCheckParams.CheckNegtiveOrZero)
                {
                    // 如果校验Nan则将非法数据替换为允许的最大最小值
                    Parallel.For(0, _option.MaxDegreeOfParallelism, _option, CheckInvalidAndNegtiveData);
                }
                else
                {
                    // 如果校验Nan则将非法数据替换为允许的最大最小值
                    Parallel.For(0, _option.MaxDegreeOfParallelism, _option, CheckInvalidData);
                }
            }
            this._datas = null;
            this._invalidBuf = null;
        }

        // 过滤越界数据，将其替换为Nan(空点)
        private void CheckDataExceedRange(int blockIndex)
        {
            int start = blockIndex*_blockSize + _indexOffset;
            int end = start + _blockSize;
            if (start >= _datas.Count)
            {
                return;
            }
            if (end > _datas.Count)
            {
                end = _datas.Count;
            }
            for (int index = start; index < end; index++)
            {
                double originalValue = _datas[index];
                if ((_dataCheckParams.CheckInfinity && double.IsInfinity(originalValue)) ||
                    (_dataCheckParams.CheckNegtiveOrZero && originalValue <= 0))
                {
                    // 如果数据非法则添加到缓存中
                    lock (_parallelLock)
                    {
                        _invalidBuf.Add(index, originalValue);
                    }
                    _datas[index] = double.NaN;
                }
            }
        }

        // 过滤越界数据和Nan数据，分别替换为最大double和最小正double
        private void CheckInvalidAndNegtiveData(int blockIndex)
        {
            int start = blockIndex * _blockSize + _indexOffset;
            int end = start + _blockSize;
            if (start >= _datas.Count)
            {
                return;
            }
            if (end > _datas.Count)
            {
                end = _datas.Count;
            }
            for(int index = start; index < end; index++)
            {
                double originalValue = _datas[index];
                if (double.IsNaN(originalValue))
                {
                    // 如果数据非法则添加到缓存中
                    lock (_parallelLock)
                    {
                        _invalidBuf.Add(index, originalValue);
                    }
                    _datas[index] = Constants.NanDataFakeValue;
                }
                else if (originalValue <= 0 ||
                         (_dataCheckParams.CheckInfinity && double.IsNegativeInfinity(originalValue)))
                {
                    // 如果数据非法则添加到缓存中
                    lock (_parallelLock)
                    {
                        _invalidBuf.Add(index, originalValue);
                    }
                    _datas[index] = Constants.MinPositiveDoubleValue;
                }
                else if (_dataCheckParams.CheckInfinity && double.IsPositiveInfinity(originalValue))
                {
                    // 如果数据非法则添加到缓存中
                    lock (_parallelLock)
                    {
                        _invalidBuf.Add(index, originalValue);
                    }
                    _datas[index] = Constants.MaxPositiveDoubleValue;
                }
            }
        }

        // 过滤越界数据和Nan数据，分别替换为最大double和最小double
        private void CheckInvalidData(int blockIndex)
        {
            int start = blockIndex * _blockSize + _indexOffset;
            int end = start + _blockSize;
            if (start >= _datas.Count)
            {
                return;
            }
            if (end > _datas.Count)
            {
                end = _datas.Count;
            }
            for (int index = start; index < end; index++)
            {
                double originalValue = _datas[index];
                if (double.IsNaN(originalValue))
                {
                    // 如果数据非法则添加到缓存中
                    lock (_parallelLock)
                    {
                        _invalidBuf.Add(index, originalValue);
                    }
                    _datas[index] = Constants.NanDataFakeValue;
                }
                else if (_dataCheckParams.CheckInfinity && double.IsNegativeInfinity(originalValue))
                {
                    // 如果数据非法则添加到缓存中
                    lock (_parallelLock)
                    {
                        _invalidBuf.Add(index, originalValue);
                    }
                    _datas[index] = Constants.MinNegtiveDoubleValue;
                }
                else if (_dataCheckParams.CheckInfinity && double.IsPositiveInfinity(originalValue))
                {
                    // 如果数据非法则添加到缓存中
                    lock (_parallelLock)
                    {
                        _invalidBuf.Add(index, originalValue);
                    }
                    _datas[index] = Constants.MaxPositiveDoubleValue;
                }
            }
        }

        #endregion


        #region Max / Min /Interval计算
        
        private readonly double[] _maxDatas;
        private readonly double[] _minDatas;

        public void GetMaxAndMin(IList<double> datas, out double max, out double min)
        {
            // 点数小于4000时手动计算
            if (datas.Count <= MinParallelCalcCount)
            {
                int startIndex = 0;
                while (startIndex < datas.Count)
                {
                    if (!double.IsNaN(datas[startIndex]))
                    {
                        break;
                    }
                    startIndex++;
                }
                if (startIndex == datas.Count)
                {
                    max = 0;
                    min = 0;
                    return;
                }
                max = datas[startIndex];
                min = datas[startIndex];
                for (int index = startIndex + 1; index < datas.Count; index++)
                {
                    if (double.IsNaN(datas[index]))
                    {
                        continue;
                    }
                    if (datas[index] > max)
                    {
                        max = datas[index];
                    }
                    else if (datas[index] < min)
                    {
                        min = datas[index];
                    }
                }
            }
            else
            {
                _datas = datas;
                _blockSize = GetBlockSize(datas.Count);
                _indexOffset = 0;
                Parallel.For(0, _option.MaxDegreeOfParallelism, FillMaxAndMinToBuf);
                max = _maxDatas.Max();
                min = _minDatas.Min();
            }
            _datas = null;
        }

        private void FillMaxAndMinToBuf(int blockIndex)
        {
            int startIndex = blockIndex * _blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > _datas.Count)
            {
                endIndex = _datas.Count;
            }
            while (startIndex < _datas.Count)
            {
                if (!double.IsNaN(_datas[startIndex]))
                {
                    break;
                }
                startIndex++;
            }
            if (startIndex == _datas.Count)
            {
                _maxDatas[blockIndex] = 0;
                _minDatas[blockIndex] = 0;
                return;
            }
            double maxValue = _datas[startIndex];
            double minValue = _datas[startIndex];
            for (int index = startIndex + 1; index < endIndex; index++)
            {
                double value = _datas[index];
                if (double.IsNaN(value))
                {
                    continue;
                }
                if (value > maxValue)
                {
                    maxValue = value;
                }
                else if (value < minValue)
                {
                    minValue = value;
                }
            }
            _maxDatas[blockIndex] = maxValue;
            _minDatas[blockIndex] = minValue;
        }

        #endregion

        #region Fill Function
        
        #region No Fit

        public void FillNoneFitPlotData(int startIndex, bool isLogView)
        {
            int plotIndex = 0;
            if (XDataInputType.Increment == _dataEntity.DataInfo.XDataInputType)
            {
                if (!isLogView)
                {
                    for (int index = 0; index < _buffer.PlotSize; index++)
                    {
                        int pointIndex = (startIndex + index * _buffer.SparseRatio);
                        _buffer.XPlotBuffer[plotIndex] = _dataEntity.XStart + pointIndex * _dataEntity.XIncrement;
                        for (int i = 0; i < _dataEntity.DataInfo.LineNum; i++)
                        {
                            _buffer.YPlotBuffer[i][plotIndex] = _dataEntity.YData[pointIndex];
                            pointIndex += _dataEntity.DataInfo.Size;
                        }
                        plotIndex++;
                    }
                }
                else
                {
                    double startXValue = _dataEntity.XStart + startIndex * _dataEntity.XIncrement;
                    if (startXValue < Constants.MinPositiveDoubleValue)
                    {
                        startXValue = Constants.MinPositiveDoubleValue;
                    }
                    double axisValue = Math.Log10(startXValue);
                    double axisEndValue = Math.Log10(startXValue + _buffer.SparseRatio*_buffer.PlotSize*_dataEntity.XIncrement);
                    double stepRatio = (axisEndValue - axisValue)/_buffer.PlotSize;
                    for (int index = 0; index < _buffer.PlotSize; index++)
                    {
                        int pointIndex = (int)Math.Round(Math.Pow(10, axisValue));
                        _buffer.XPlotBuffer[plotIndex] = _dataEntity.XStart + pointIndex * _dataEntity.XIncrement;
                        for (int i = 0; i < _dataEntity.DataInfo.LineNum; i++)
                        {
                            _buffer.YPlotBuffer[i][plotIndex] = _dataEntity.YData[pointIndex];
                            pointIndex += _dataEntity.DataInfo.Size;
                        }
                        axisValue += stepRatio;
                    }
                }
            }
            else
            {
                // TODO Array方式入参的对数非拟合算法暂未实现
                for (int index = 0; index < _buffer.PlotSize; index++)
                {
                    int pointIndex = (startIndex + index * _buffer.SparseRatio);
                    _buffer.XPlotBuffer[plotIndex] = (_dataEntity.XData[pointIndex]);
                    for (int i = 0; i < _dataEntity.DataInfo.LineNum; i++)
                    {
                        _buffer.YPlotBuffer[i][plotIndex] = _dataEntity.YData[i * _dataEntity.DataInfo.Size + pointIndex];
                    }
                    plotIndex++;
                }
            }
        }

        #endregion

        #region Range Fit

        private bool _isLogView;
        private double _logBlockSize;

        public void FillRangeFitPlotData(int indexOffset, bool isLogView)
        {
            //将PlotSize的数据分为2*_option.MaxDegreeOfParallelism段，每段最长为_segmentSize
            this._isLogView = isLogView;
            _blockSize = GetBlockSize(this._dataEntity.PlotBuf.PlotSize / 2);
            if (isLogView)
            {
                double startXValue = _dataEntity.XStart + indexOffset*_dataEntity.XIncrement;
                if (startXValue < Constants.MinPositiveDoubleValue)
                {
                    startXValue = Constants.MinPositiveDoubleValue;
                }
                double axisStartValue = Math.Log10(startXValue);
                // 因为按块稀疏点，导致最后一个段不会被计算到，所以需要给PlotSize+1
                double axisEndValue = Math.Log10(startXValue + _buffer.SparseRatio*(_buffer.PlotSize + 1)*_dataEntity.XIncrement);
                // 多加一个常数，减少因为误差导致的点数少计算的问题
                this._logBlockSize = (axisEndValue - axisStartValue)/_option.MaxDegreeOfParallelism + Constants.MinDoubleValue;
            }
            this._indexOffset = indexOffset;
            if (XDataInputType.Increment == this._dataEntity.DataInfo.XDataInputType)
            {
                if (!isLogView)
                {
                    Parallel.For(0, _option.MaxDegreeOfParallelism, _option, FillRangeFitStepXData);
                }
                else
                {
                    Parallel.For(0, _option.MaxDegreeOfParallelism, _option, FillLogRangeFitStepXData);
                }
            }
            else
            {
                Parallel.For(0, _option.MaxDegreeOfParallelism, _option, FillRangeFitArrayXData);
            }
        }

        private void FillRangeFitStepXData(int segmentIndex)
        {
            IList<double> xDataBuf = _buffer.XPlotBuffer;
            IList<IList<double>> yDataBuf = _buffer.YPlotBuffer;

            // 一个拟合对在真是数据中的索引起始位置
            int start = _blockSize*segmentIndex;
            // 一个拟合对在真是数据中的索引结束位置，不包含该位置
            int end = _blockSize*(segmentIndex + 1);
            if (end > this._buffer.PlotSize/2)
            {
                end = this._buffer.PlotSize/2;
            }
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2*dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex*_buffer.SparseRatio + _indexOffset;
                // 真实数据的终止索引位置，不包含该点
                int pointEndIndex = pointStartIndex + 2*_buffer.SparseRatio;
                if (pointEndIndex > _dataEntity.DataInfo.Size)
                {
                    pointEndIndex = _dataEntity.DataInfo.Size;
                }

                xDataBuf[startBufIndex] = _dataEntity.XStart + pointStartIndex*_dataEntity.XIncrement;
                xDataBuf[startBufIndex + 1] = _dataEntity.XStart + (pointStartIndex+_buffer.SparseRatio) * _dataEntity.XIncrement; ;

                int lineIndexOffset = 0;
                for (int lineIndex = 0; lineIndex < _dataEntity.DataInfo.LineNum; lineIndex++)
                {
                    double maxValue = _dataEntity.YData[pointStartIndex + lineIndexOffset];
                    double minValue = _dataEntity.YData[pointStartIndex + lineIndexOffset];
                    int maxIndex = pointStartIndex;
                    int minIndex = pointStartIndex;
                    for (int pointIndex = pointStartIndex + 1; pointIndex < pointEndIndex; pointIndex++)
                    {
                        double value = _dataEntity.YData[pointIndex + lineIndexOffset];
                        if (value > maxValue)
                        {
                            maxIndex = pointIndex;
                            maxValue = value;
                        }
                        else if (value < minValue)
                        {
                            minIndex = pointIndex;
                            minValue = value;
                        }
                    }
                    if (maxIndex > minIndex)
                    {
                        yDataBuf[lineIndex][startBufIndex] = minValue;
                        yDataBuf[lineIndex][startBufIndex + 1] = maxValue;
                    }
                    else
                    {
                        yDataBuf[lineIndex][startBufIndex] = maxValue;
                        yDataBuf[lineIndex][startBufIndex + 1] = minValue;
                    }
                    lineIndexOffset += _dataEntity.DataInfo.Size;
                }
            }
        }

        private void FillLogRangeFitStepXData(int blockIndex)
        {
            // 一个拟合对在真是数据中的索引起始位置
            int start = _blockSize * blockIndex;
            // 一个拟合对在真是数据中的索引结束位置，不包含该位置
            int end = _blockSize * (blockIndex + 1);
            if (end > this._buffer.PlotSize / 2)
            {
                end = this._buffer.PlotSize / 2;
            }

            IList<double> xDataBuf = _buffer.XPlotBuffer;
            IList<IList<double>> yDataBuf = _buffer.YPlotBuffer;

            double axisOffset = Math.Log10(_dataEntity.XStart + this._indexOffset * _dataEntity.XIncrement) + _logBlockSize*blockIndex;
            double startXValue, endXValue;
            int startXIndex, endXIndex;
            endXValue = Math.Round(Math.Pow(10, axisOffset));
            int pairCount = end - start;
            // 两个相邻的数据作为拟合对。
            int pairIndex = 0;
            for (int plotIndex = start; plotIndex < end; plotIndex++)
            {
                // 待写入缓存的索引位置
                int bufIndex = plotIndex*2;
                startXValue = endXValue;
                endXValue = Math.Round(Math.Pow(10, axisOffset + (pairIndex + 1)*_logBlockSize/pairCount));
                double middleXValue = Math.Round(Math.Pow(10, axisOffset + (pairIndex + 0.5)*_logBlockSize/pairCount));
                pairIndex++;
                // 真实数据的起始索引
                startXIndex = (int)Math.Round((startXValue - _dataEntity.XStart) / _dataEntity.XIncrement);
                if (startXIndex < 0)
                {
                    startXIndex = 0;
                }
                // 真实数据的终止索引位置，不包含该点
                endXIndex = (int)Math.Round((int)(endXValue - _dataEntity.XStart) / _dataEntity.XIncrement);
                if (endXIndex > _dataEntity.DataInfo.Size)
                {
                    endXIndex = _dataEntity.DataInfo.Size;
                }
                // 如果两个值相等，说明范围太小，应该用StartXIndex对应的值去填充
                if (startXIndex == endXIndex)
                {
                    endXIndex = startXIndex + 1;
                }
                xDataBuf[bufIndex] = startXValue;
                xDataBuf[bufIndex + 1] = middleXValue;
                int lineIndexOffset = 0;
                for (int lineIndex = 0; lineIndex < _dataEntity.DataInfo.LineNum; lineIndex++)
                {
                    double maxValue = _dataEntity.YData[startXIndex + lineIndexOffset];
                    double minValue = _dataEntity.YData[startXIndex + lineIndexOffset];
                    int maxIndex = startXIndex;
                    int minIndex = startXIndex;
                    for (int pointIndex = startXIndex + 1; pointIndex < endXIndex; pointIndex++)
                    {
                        double value = _dataEntity.YData[pointIndex + lineIndexOffset];
                        if (value > maxValue)
                        {
                            maxIndex = pointIndex;
                            maxValue = value;
                        }
                        else if (value < minValue)
                        {
                            minIndex = pointIndex;
                            minValue = value;
                        }
                    }
                    if (maxIndex > minIndex)
                    {
                        yDataBuf[lineIndex][bufIndex] = minValue;
                        yDataBuf[lineIndex][bufIndex + 1] = maxValue;
                    }
                    else
                    {
                        yDataBuf[lineIndex][bufIndex] = maxValue;
                        yDataBuf[lineIndex][bufIndex + 1] = minValue;
                    }
                    lineIndexOffset += _dataEntity.DataInfo.Size;
                }
            }
            int startBufIndex = 2*start;
            int endBufIndex = 2*end;

            // 如果相邻三个点的值不同，则认为该数据无需执行拟合操作
            if (Math.Abs(xDataBuf[startBufIndex + 1] - xDataBuf[startBufIndex]) < Constants.MinPositiveDoubleValue ||
                Math.Abs(xDataBuf[startBufIndex + 2] - xDataBuf[startBufIndex + 1]) < Constants.MinPositiveDoubleValue)
            {
                // 先拟合X轴的数据，获取X轴最终拟合到的位置，然后再去拟合Y轴的数据
                int fitEndIndex = FitNearSameValuePoint(xDataBuf, startBufIndex, endBufIndex);
                foreach (IList<double> yPlotBuf in yDataBuf)
                {
                    FitNearSameValuePoint(yPlotBuf, startBufIndex, fitEndIndex);
                }
            }
        }

        // 对数展开时X轴最左边经常会出现多个点画一个数据的问题，暂时通过线性拟合去修改这个问题，对这些点做插值操作
        private int FitNearSameValuePoint(IList<double> plotBuf, int startBufIndex, int endBufIndex)
        {
            int startSameValueIndex = startBufIndex;
            double currentValue = plotBuf[startBufIndex];
            bool inSameValueRegion = false;
            int diffValueCount = 0;
            for (int i = startBufIndex + 1; i < endBufIndex; i++)
            {
                if (Math.Abs(plotBuf[i] - currentValue) > Constants.MinPositiveDoubleValue)
                {
                    // 如果有连续两个值不同则说明已经过了需要拟合的阶段，直接返回
                    if (++diffValueCount > 2)
                    {
                        return i;
                    }
                    if (inSameValueRegion)
                    {
                        // 如果重叠点小于等于两个，将不需要拟合
                        if (i - startSameValueIndex <= 2)
                        {
                            return i;
                        }
                        DoLinearFit(plotBuf, startSameValueIndex, i);
                        inSameValueRegion = false;
                        startSameValueIndex = i;
                    }
                    currentValue = plotBuf[i];
                }
                else if (!inSameValueRegion)
                {
                    startSameValueIndex = i - 1;
                    currentValue = plotBuf[i];
                    inSameValueRegion = true;
                    diffValueCount = 0;
                }
            }
            return endBufIndex;
        }

        private void DoLinearFit(IList<double> plotBuf, int startIndex, int endIndex)
        {
            double startValue = plotBuf[startIndex];
            double endValue = plotBuf[endIndex];
            double step = (endValue - startValue)/(endIndex - startIndex);
            double value = startValue;
            for (int i = startIndex + 1; i < endIndex; i++)
            {
                value += step;
                plotBuf[i] = value;
            }
        }

        private void FillRangeFitArrayXData(int segmentIndex)
        {
            IList<double> xDataBuf = _buffer.XPlotBuffer;
            IList<IList<double>> yDataBuf = _buffer.YPlotBuffer;

            // 一个拟合对在真是数据中的索引起始位置
            int start = _blockSize*segmentIndex;
            // 一个拟合对在真是数据中的索引结束位置，不包含该位置
            int end = _blockSize*(segmentIndex + 1);
            if (end > this._buffer.PlotSize/2)
            {
                end = this._buffer.PlotSize/2;
            }
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2*dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex*_buffer.SparseRatio + _indexOffset;
                // 真实数据的终止索引，不包含该点
                int pointEndIndex = pointStartIndex + 2*_buffer.SparseRatio;
                if (pointEndIndex > _dataEntity.DataInfo.Size)
                {
                    pointEndIndex = _dataEntity.DataInfo.Size;
                }

                xDataBuf[startBufIndex] = _dataEntity.XData[pointStartIndex];
                xDataBuf[startBufIndex + 1] = _dataEntity.XData[pointStartIndex + _buffer.SparseRatio];

                int lineIndexOffset = 0;
                for (int lineIndex = 0; lineIndex < _dataEntity.DataInfo.LineNum; lineIndex++)
                {
                    double maxValue = _dataEntity.YData[pointStartIndex + lineIndexOffset];
                    double minValue = _dataEntity.YData[pointStartIndex + lineIndexOffset];
                    int maxIndex = pointStartIndex;
                    int minIndex = pointStartIndex;
                    for (int pointIndex = pointStartIndex + 1; pointIndex < pointEndIndex; pointIndex++)
                    {
                        double value = _dataEntity.YData[pointIndex + lineIndexOffset];
                        if (value > maxValue)
                        {
                            maxIndex = pointIndex;
                            maxValue = value;
                        }
                        else if (value < minValue)
                        {
                            minIndex = pointIndex;
                            minValue = value;
                        }
                    }
                    if (maxIndex > minIndex)
                    {
                        yDataBuf[lineIndex][startBufIndex] = minValue;
                        yDataBuf[lineIndex][startBufIndex + 1] = maxValue;
                    }
                    else
                    {
                        yDataBuf[lineIndex][startBufIndex] = maxValue;
                        yDataBuf[lineIndex][startBufIndex + 1] = minValue;
                    }
                    lineIndexOffset += _dataEntity.DataInfo.Size;
                }
            }
        }

        #endregion

        #endregion

        private int GetBlockSize(int count)
        {
            return (count - 1) / _option.MaxDegreeOfParallelism + 1;
        }

        #region Transpose function

        private double[,] _transposeSrc;
        private double[] _transposeDst;

        public void Transpose(double[,] src, double[] dst)
        {
            int sampleCount = src.GetLength(0);
            _transposeSrc = src;
            _transposeDst = dst;
            if (src.Length <= MinParallelCalcCount)
            {
                _blockSize = sampleCount;
                TransposeSingleBlock(0, null);
            }
            else
            {
                _blockSize = GetBlockSize(sampleCount);
                // 将m行n列的矩阵分解为m/x行n列的矩阵分别转置，x为并行度
                Parallel.For(0, _option.MaxDegreeOfParallelism, TransposeSingleBlock);
            }
            _transposeSrc = null;
            _transposeDst = null;
        }

        private void TransposeSingleBlock(int blockIndex, object state)
        {
            int startRowIndex = blockIndex*_blockSize;
            int endRowIndex = startRowIndex + _blockSize;
            int sampleCount = _transposeSrc.GetLength(0);
            if (endRowIndex > sampleCount)
            {
                endRowIndex = sampleCount;
            }
            int lineCount = _transposeSrc.GetLength(1);
            for (int colIndex = 0; colIndex < lineCount; colIndex++)
            {
                int writeIndex = colIndex * sampleCount + startRowIndex;
                for (int rowIndex = startRowIndex; rowIndex < endRowIndex; rowIndex++)
                {
                    _transposeDst[writeIndex++] = _transposeSrc[rowIndex, colIndex];
                }
            }
        }

        #endregion

    }
}
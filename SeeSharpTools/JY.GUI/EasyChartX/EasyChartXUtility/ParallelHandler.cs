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
            if (datas.Count <= 4000)
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

        public void FillNoneFitPlotData(int startIndex)
        {
            int plotIndex = 0;
            if (XDataInputType.Increment == _dataEntity.DataInfo.XDataInputType)
            {
                for (int index = 0; index < _buffer.PlotSize; index++)
                {
                    int pointIndex = (startIndex + index*_buffer.SparseRatio);
                    _buffer.XPlotBuffer[plotIndex] = _dataEntity.XStart + pointIndex*_dataEntity.XIncrement;
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
                for (int index = 0; index < _buffer.PlotSize; index++)
                {
                    int pointIndex = (startIndex + index*_buffer.SparseRatio);
                    _buffer.XPlotBuffer[plotIndex] = (_dataEntity.XData[pointIndex]);
                    for (int i = 0; i < _dataEntity.DataInfo.LineNum; i++)
                    {
                        _buffer.YPlotBuffer[i][plotIndex] = _dataEntity.YData[i*_dataEntity.DataInfo.Size + pointIndex];
                    }
                    plotIndex++;
                }
            }
        }

        #endregion

        #region Range Fit

        public void FillRangeFitPlotData(int indexOffset)
        {
            //将PlotSize的数据分为2*_option.MaxDegreeOfParallelism段，每段最长为_segmentSize
            _blockSize = GetBlockSize(this._dataEntity.PlotBuf.PlotSize/2);
            this._indexOffset = indexOffset;
            if (XDataInputType.Increment == this._dataEntity.DataInfo.XDataInputType)
            {
                Parallel.For(0, _option.MaxDegreeOfParallelism, _option, FillRangeFitStepXData);
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
    }
}
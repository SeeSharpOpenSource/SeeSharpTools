using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeeSharpTools.JY.GUI.StripChartXData;

namespace SeeSharpTools.JY.GUI.StripChartXUtility
{
    internal class ParallelHandler
    {
        private readonly ParallelOptions _option;
        private readonly object _parallelLock = new object();

        //每个计算块的大小
        private int _blockSize;
        private int _indexOffset;

//        private readonly PlotBuffer _buffer;
        private object _datas;
        private readonly DataCheckParameters _dataCheckParams;

        public ParallelHandler(DataCheckParameters dataCheckParams)
        {
            this._option = new ParallelOptions();
            this._indexOffset = 0;
            this._dataCheckParams = dataCheckParams;
            // 计算限制型配置并行度为内核个数
            _option.MaxDegreeOfParallelism = Environment.ProcessorCount;

            this._maxDatas = new double[_option.MaxDegreeOfParallelism];
            this._minDatas = new double[_option.MaxDegreeOfParallelism];
        }

        #region Invalid Data Check
        // TODO not implemented
        #endregion

        #region Max / Min 计算
        
        private readonly double[] _maxDatas;
        private readonly double[] _minDatas;

        public void GetMaxAndMin<TDataType>(IList<TDataType> datas, out double max, out double min)
        {
            string typeName = typeof(TDataType).FullName;
            if (typeName.Equals(typeof (double).FullName))
            {
                GetDoubleMaxAndMin(datas, datas.Count, out max, out min);
            }
            else if (typeName.Equals(typeof (float).FullName))
            {
                GetFloatMaxAndMin(datas, datas.Count, out max, out min);
            }
            else if (typeName.Equals(typeof(int).FullName))
            {
                GetIntMaxAndMin(datas, datas.Count, out max, out min);
            }
            else if (typeName.Equals(typeof(uint).FullName))
            {
                GetUIntMaxAndMin(datas, datas.Count, out max, out min);
            }
            else if (typeName.Equals(typeof(short).FullName))
            {
                GetShortMaxAndMin(datas, datas.Count, out max, out min);
            }
            else if (typeName.Equals(typeof(ushort).FullName))
            {
                GetUShortMaxAndMin(datas, datas.Count, out max, out min);
            }
            else if (typeName.Equals(typeof(byte).FullName))
            {
                GetByteMaxAndMin(datas, datas.Count, out max, out min);
            }
            else
            {
                max = double.NaN;
                min = double.NaN;
            }
        }

        #region Double Max / Min

        public void GetDoubleMaxAndMin(object datas, int dataCount, out double max, out double min)
        {
            // 点数小于4000时手动计算
            if (dataCount <= 4000)
            {
                IList<double> doubleDatas = datas as IList<double>;
                int startIndex = 0;
                while (startIndex < doubleDatas.Count)
                {
                    if (!double.IsNaN(doubleDatas[startIndex]))
                    {
                        break;
                    }
                    startIndex++;
                }
                if (startIndex == doubleDatas.Count)
                {
                    max = 0;
                    min = 0;
                    return;
                }
                max = doubleDatas[startIndex];
                min = doubleDatas[startIndex];
                for (int index = startIndex + 1; index < doubleDatas.Count; index++)
                {
                    if (double.IsNaN(doubleDatas[index]))
                    {
                        continue;
                    }
                    if (doubleDatas[index] > max)
                    {
                        max = doubleDatas[index];
                    }
                    else if (doubleDatas[index] < min)
                    {
                        min = doubleDatas[index];
                    }
                }
            }
            else
            {
                _datas = datas;
                _blockSize = GetBlockSize(dataCount);
                _indexOffset = 0;
                Parallel.For(0, _option.MaxDegreeOfParallelism, FillDoubleMaxAndMinToBuf);
                max = _maxDatas.Max();
                min = _minDatas.Min();
            }
            _datas = null;
        }

        private void FillDoubleMaxAndMinToBuf(int blockIndex)
        {
            IList<double> doubleDatas = this._datas as IList<double>;
            int startIndex = blockIndex*_blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > doubleDatas.Count)
            {
                endIndex = doubleDatas.Count;
            }
            while (startIndex < doubleDatas.Count)
            {
                if (!double.IsNaN(doubleDatas[startIndex]))
                {
                    break;
                }
                startIndex++;
            }
            if (startIndex == doubleDatas.Count)
            {
                _maxDatas[blockIndex] = 0;
                _minDatas[blockIndex] = 0;
                return;
            }
            double maxValue = doubleDatas[startIndex];
            double minValue = doubleDatas[startIndex];
            for (int index = startIndex + 1; index < endIndex; index++)
            {
                double value = doubleDatas[index];
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

        #region Float Max / Min

        public void GetFloatMaxAndMin(object datas, int dataCount, out double max, out double min)
        {
            // 点数小于4000时手动计算
            if (dataCount <= 4000)
            {
                IList<float> floatDatas = datas as IList<float>;
                int startIndex = 0;
                while (startIndex < floatDatas.Count)
                {
                    if (!float.IsNaN(floatDatas[startIndex]))
                    {
                        break;
                    }
                    startIndex++;
                }
                if (startIndex == floatDatas.Count)
                {
                    max = double.NaN;
                    min = double.NaN;
                    return;
                }
                float tmpMax = floatDatas[startIndex];
                float tmpMin = floatDatas[startIndex];
                for (int index = startIndex + 1; index < floatDatas.Count; index++)
                {
                    if (float.IsNaN(floatDatas[index]))
                    {
                        continue;
                    }
                    if (floatDatas[index] > tmpMax)
                    {
                        tmpMax = floatDatas[index];
                    }
                    else if (floatDatas[index] < tmpMin)
                    {
                        tmpMin = floatDatas[index];
                    }
                }
                max = tmpMax;
                min = tmpMin;
            }
            else
            {
                _datas = datas;
                _blockSize = GetBlockSize(dataCount);
                _indexOffset = 0;
                Parallel.For(0, _option.MaxDegreeOfParallelism, FillFloatMaxAndMinToBuf);
                max = _maxDatas.Max();
                min = _minDatas.Min();
            }
            _datas = null;
        }

        private void FillFloatMaxAndMinToBuf(int blockIndex)
        {
            IList<float> floatDatas = this._datas as IList<float>;
            int startIndex = blockIndex * _blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > floatDatas.Count)
            {
                endIndex = floatDatas.Count;
            }
            while (startIndex < floatDatas.Count)
            {
                if (!float.IsNaN(floatDatas[startIndex]))
                {
                    break;
                }
                startIndex++;
            }
            if (startIndex == floatDatas.Count)
            {
                _maxDatas[blockIndex] = 0;
                _minDatas[blockIndex] = 0;
                return;
            }
            float maxValue = floatDatas[startIndex];
            float minValue = floatDatas[startIndex];
            for (int index = startIndex + 1; index < endIndex; index++)
            {
                float value = floatDatas[index];
                if (float.IsNaN(value))
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

        #region Int Max / Min

        public void GetIntMaxAndMin(object datas, int dataCount, out double max, out double min)
        {
            // 点数小于4000时手动计算
            if (dataCount <= 4000)
            {
                IList<int> intDatas = datas as IList<int>;
                int tmpMax = intDatas[0];
                int tmpMin = intDatas[0];
                foreach (int data in intDatas)
                {
                    if (data > tmpMax)
                    {
                        tmpMax = data;
                    }
                    else if (data < tmpMin)
                    {
                        tmpMin = data;
                    }
                }
                max = tmpMax;
                min = tmpMin;
            }
            else
            {
                _datas = datas;
                _blockSize = GetBlockSize(dataCount);
                _indexOffset = 0;
                Parallel.For(0, _option.MaxDegreeOfParallelism, FillIntMaxAndMinToBuf);
                max = _maxDatas.Max();
                min = _minDatas.Min();
            }
            _datas = null;
        }

        private void FillIntMaxAndMinToBuf(int blockIndex)
        {
            IList<int> intDatas = this._datas as IList<int>;
            int startIndex = blockIndex * _blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > intDatas.Count)
            {
                endIndex = intDatas.Count;
            }
            int maxValue = intDatas[startIndex];
            int minValue = intDatas[startIndex];
            for (int index = startIndex + 1; index < endIndex; index++)
            {
                int value = intDatas[index];
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
        
        #region UInt Max / Min

        public void GetUIntMaxAndMin(object datas, int dataCount, out double max, out double min)
        {
            // 点数小于4000时手动计算
            if (dataCount <= 4000)
            {
                IList<uint> uintDatas = datas as IList<uint>;
                uint tmpMax = uintDatas[0];
                uint tmpMin = uintDatas[0];
                foreach (uint data in uintDatas)
                {
                    if (data > tmpMax)
                    {
                        tmpMax = data;
                    }
                    else if (data < tmpMin)
                    {
                        tmpMin = data;
                    }
                }
                max = tmpMax;
                min = tmpMin;
            }
            else
            {
                _datas = datas;
                _blockSize = GetBlockSize(dataCount);
                _indexOffset = 0;
                Parallel.For(0, _option.MaxDegreeOfParallelism, FillUIntMaxAndMinToBuf);
                max = _maxDatas.Max();
                min = _minDatas.Min();
            }
            _datas = null;
        }

        private void FillUIntMaxAndMinToBuf(int blockIndex)
        {
            IList<uint> uintDatas = this._datas as IList<uint>;
            int startIndex = blockIndex * _blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > uintDatas.Count)
            {
                endIndex = uintDatas.Count;
            }
            uint maxValue = uintDatas[startIndex];
            uint minValue = uintDatas[startIndex];
            for (int index = startIndex + 1; index < endIndex; index++)
            {
                uint value = uintDatas[index];
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

        #region Short Max / Min

        public void GetShortMaxAndMin(object datas, int dataCount, out double max, out double min)
        {
            // 点数小于4000时手动计算
            if (dataCount <= 4000)
            {
                IList<short> shortDatas = datas as IList<short>;
                short tmpMax = shortDatas[0];
                short tmpMin = shortDatas[0];
                foreach (short data in shortDatas)
                {
                    if (data > tmpMax)
                    {
                        tmpMax = data;
                    }
                    else if (data < tmpMin)
                    {
                        tmpMin = data;
                    }
                }
                max = tmpMax;
                min = tmpMin;

            }
            else
            {
                _datas = datas;
                _blockSize = GetBlockSize(dataCount);
                _indexOffset = 0;
                Parallel.For(0, _option.MaxDegreeOfParallelism, FillShortMaxAndMinToBuf);
                max = (short)_maxDatas.Max();
                min = (short)_minDatas.Min();
            }
            _datas = null;
        }

        private void FillShortMaxAndMinToBuf(int blockIndex)
        {
            IList<short> shortDatas = this._datas as IList<short>;
            int startIndex = blockIndex * _blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > shortDatas.Count)
            {
                endIndex = shortDatas.Count;
            }
            short maxValue = shortDatas[startIndex];
            short minValue = shortDatas[startIndex];
            for (int index = startIndex + 1; index < endIndex; index++)
            {
                short value = shortDatas[index];
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

        #region UShort Max / Min

        public void GetUShortMaxAndMin(object datas, int dataCount, out double max, out double min)
        {
            // 点数小于4000时手动计算
            if (dataCount <= 4000)
            {
                IList<ushort> ushortDatas = datas as IList<ushort>;
                ushort tmpMax = ushortDatas[0];
                ushort tmpMin = ushortDatas[0];
                foreach (ushort data in ushortDatas)
                {
                    if (data > tmpMax)
                    {
                        tmpMax = data;
                    }
                    else if (data < tmpMin)
                    {
                        tmpMin = data;
                    }
                }
                max = tmpMax;
                min = tmpMin;
            }
            else
            {
                _datas = datas;
                _blockSize = GetBlockSize(dataCount);
                _indexOffset = 0;
                Parallel.For(0, _option.MaxDegreeOfParallelism, FillUShortMaxAndMinToBuf);
                max = _maxDatas.Max();
                min = _minDatas.Min();
            }
            _datas = null;
        }

        private void FillUShortMaxAndMinToBuf(int blockIndex)
        {
            IList<ushort> shortDatas = this._datas as IList<ushort>;
            int startIndex = blockIndex * _blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > shortDatas.Count)
            {
                endIndex = shortDatas.Count;
            }
            ushort maxValue = shortDatas[startIndex];
            ushort minValue = shortDatas[startIndex];
            for (int index = startIndex + 1; index < endIndex; index++)
            {
                ushort value = shortDatas[index];
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

        #region Byte Max / Min

        public void GetByteMaxAndMin(object datas, int dataCount, out double max, out double min)
        {
            // 点数小于4000时手动计算
            if (dataCount <= 4000)
            {
                IList<byte> byteDatas = datas as IList<byte>;
                byte tmpMax = byteDatas[0];
                byte tmpMin = byteDatas[0];
                foreach (byte data in byteDatas)
                {
                    if (data > tmpMax)
                    {
                        tmpMax = data;
                    }
                    else if (data < tmpMin)
                    {
                        tmpMin = data;
                    }
                }
                max = tmpMax;
                min = tmpMin;
            }
            else
            {
                _datas = datas;
                _blockSize = GetBlockSize(dataCount);
                _indexOffset = 0;
                Parallel.For(0, _option.MaxDegreeOfParallelism, FillByteMaxAndMinToBuf);
                max = _maxDatas.Max();
                min = _minDatas.Min();
            }
            _datas = null;
        }

        private void FillByteMaxAndMinToBuf(int blockIndex)
        {
            IList<byte> byteDatas = this._datas as IList<byte>;
            int startIndex = blockIndex * _blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > byteDatas.Count)
            {
                endIndex = byteDatas.Count;
            }
            byte maxValue = byteDatas[startIndex];
            byte minValue = byteDatas[startIndex];
            for (int index = startIndex + 1; index < endIndex; index++)
            {
                byte value = byteDatas[index];
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

        #endregion

        #region Fill Function

        #region No Fit

        public void FillNoneFitPlotData<TDataType>(int startIndex, int sparseRatio, IList<TDataType> dataBuf, 
            IList<TDataType> plotBuf, int plotCount)
        {
            int plotIndex = startIndex;
            for (int index = 0; index < plotCount; index++)
            {
                plotIndex += _sparseRatio;
                plotBuf[index] = dataBuf[plotIndex];
            }
        }

        #endregion

        #region Range Fit

        private object _dataBuf;
        private object _plotBuf;
        private int _sparseRatio;
        private int _plotCount;

        public void FillRangeFitPlotData<TDataType>(int startIndex, int sparseRatio, IList<TDataType> dataBuf, 
            IList<TDataType> plotBuf, int plotCount)
        {
            //将PlotSize的数据分为2*_option.MaxDegreeOfParallelism段，每段最长为_segmentSize
            _blockSize = GetBlockSize(plotCount/2);
            this._dataBuf = dataBuf;
            this._plotBuf = plotBuf;
            this._sparseRatio = sparseRatio;
            this._indexOffset = startIndex;
            this._plotCount = plotCount;
            Parallel.For(0, _option.MaxDegreeOfParallelism, _option, FillRangeFitData<TDataType>);
            this._dataBuf = null;
            this._plotBuf = null;
        }

        private void FillRangeFitData<TDataType>(int segmentIndex)
        {
            // 一个拟合对在真是数据中的索引起始位置
            int start = _blockSize*segmentIndex;
            // 一个拟合对在真是数据中的索引结束位置，不包含该位置
            int end = _blockSize*(segmentIndex + 1);
            if (end > this._plotCount/2)
            {
                end = this._plotCount/2;
            }
            string typeName = typeof(TDataType).FullName;
            if (typeName.Equals(typeof(double).FullName))
            {
                FillDoubleRangeFitData(start, end);
            }
            else if (typeName.Equals(typeof(float).FullName))
            {
                FillFloatRangeFitData(start, end);
            }
            else if (typeName.Equals(typeof(int).FullName))
            {
                FillIntRangeFitData(start, end);
            }
            else if (typeName.Equals(typeof(uint).FullName))
            {
                FillUIntRangeFitData(start, end);
            }
            else if (typeName.Equals(typeof(short).FullName))
            {
                FillShortRangeFitData(start, end);
            }
            else if (typeName.Equals(typeof(ushort).FullName))
            {
                FillUShortRangeFitData(start, end);
            }
            else if (typeName.Equals(typeof(byte).FullName))
            {
                FillByteRangeFitData(start, end);
            }
        }

        private void FillDoubleRangeFitData(int start, int end)
        {
            IList<double> yDataBuf = _dataBuf as IList<double>;
            IList<double> plotBuf = _plotBuf as IList<double>;
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2*dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex*_sparseRatio + _indexOffset;
                // 真实数据的终止索引位置，不包含该点
                int pointEndIndex = pointStartIndex + 2*_sparseRatio;
                if (pointEndIndex > yDataBuf.Count)
                {
                    pointEndIndex = yDataBuf.Count;
                }

                double maxValue = yDataBuf[pointStartIndex];
                double minValue = yDataBuf[pointStartIndex];
                int maxIndex = pointStartIndex;
                int minIndex = pointStartIndex;
                for (int pointIndex = pointStartIndex + 1; pointIndex < pointEndIndex; pointIndex++)
                {
                    double value = yDataBuf[pointIndex];
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
                    yDataBuf[startBufIndex] = minValue;
                    yDataBuf[startBufIndex + 1] = maxValue;
                }
                else
                {
                    plotBuf[startBufIndex] = maxValue;
                    plotBuf[startBufIndex + 1] = minValue;
                }
            }
        }

        private void FillFloatRangeFitData(int start, int end)
        {
            IList<float> yDataBuf = _dataBuf as IList<float>;
            IList<float> plotBuf = _plotBuf as IList<float>;
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2 * dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex * _sparseRatio + _indexOffset;
                // 真实数据的终止索引位置，不包含该点
                int pointEndIndex = pointStartIndex + 2 * _sparseRatio;
                if (pointEndIndex > yDataBuf.Count)
                {
                    pointEndIndex = yDataBuf.Count;
                }

                float maxValue = yDataBuf[pointStartIndex];
                float minValue = yDataBuf[pointStartIndex];
                int maxIndex = pointStartIndex;
                int minIndex = pointStartIndex;
                for (int pointIndex = pointStartIndex + 1; pointIndex < pointEndIndex; pointIndex++)
                {
                    float value = yDataBuf[pointIndex];
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
                    plotBuf[startBufIndex] = minValue;
                    plotBuf[startBufIndex + 1] = maxValue;
                }
                else
                {
                    plotBuf[startBufIndex] = maxValue;
                    plotBuf[startBufIndex + 1] = minValue;
                }
            }
        }

        private void FillIntRangeFitData(int start, int end)
        {
            IList<int> yDataBuf = _dataBuf as IList<int>;
            IList<int> plotBuf = _plotBuf as IList<int>;
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2 * dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex * _sparseRatio + _indexOffset;
                // 真实数据的终止索引位置，不包含该点
                int pointEndIndex = pointStartIndex + 2 * _sparseRatio;
                if (pointEndIndex > yDataBuf.Count)
                {
                    pointEndIndex = yDataBuf.Count;
                }

                int maxValue = yDataBuf[pointStartIndex];
                int minValue = yDataBuf[pointStartIndex];
                int maxIndex = pointStartIndex;
                int minIndex = pointStartIndex;
                for (int pointIndex = pointStartIndex + 1; pointIndex < pointEndIndex; pointIndex++)
                {
                    int value = yDataBuf[pointIndex];
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
                    plotBuf[startBufIndex] = minValue;
                    plotBuf[startBufIndex + 1] = maxValue;
                }
                else
                {
                    plotBuf[startBufIndex] = maxValue;
                    plotBuf[startBufIndex + 1] = minValue;
                }
            }
        }

        private void FillUIntRangeFitData(int start, int end)
        {
            IList<uint> yDataBuf = _dataBuf as IList<uint>;
            IList<uint> plotBuf = _plotBuf as IList<uint>;
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2 * dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex * _sparseRatio + _indexOffset;
                // 真实数据的终止索引位置，不包含该点
                int pointEndIndex = pointStartIndex + 2 * _sparseRatio;
                if (pointEndIndex > yDataBuf.Count)
                {
                    pointEndIndex = yDataBuf.Count;
                }

                uint maxValue = yDataBuf[pointStartIndex];
                uint minValue = yDataBuf[pointStartIndex];
                int maxIndex = pointStartIndex;
                int minIndex = pointStartIndex;
                for (int pointIndex = pointStartIndex + 1; pointIndex < pointEndIndex; pointIndex++)
                {
                    uint value = yDataBuf[pointIndex];
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
                    plotBuf[startBufIndex] = minValue;
                    plotBuf[startBufIndex + 1] = maxValue;
                }
                else
                {
                    plotBuf[startBufIndex] = maxValue;
                    plotBuf[startBufIndex + 1] = minValue;
                }
            }
        }

        private void FillShortRangeFitData(int start, int end)
        {
            IList<short> yDataBuf = _dataBuf as IList<short>;
            IList<short> plotBuf = _plotBuf as IList<short>;
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2 * dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex * _sparseRatio + _indexOffset;
                // 真实数据的终止索引位置，不包含该点
                int pointEndIndex = pointStartIndex + 2 * _sparseRatio;
                if (pointEndIndex > yDataBuf.Count)
                {
                    pointEndIndex = yDataBuf.Count;
                }

                short maxValue = yDataBuf[pointStartIndex];
                short minValue = yDataBuf[pointStartIndex];
                int maxIndex = pointStartIndex;
                int minIndex = pointStartIndex;
                for (int pointIndex = pointStartIndex + 1; pointIndex < pointEndIndex; pointIndex++)
                {
                    short value = yDataBuf[pointIndex];
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
                    plotBuf[startBufIndex] = minValue;
                    plotBuf[startBufIndex + 1] = maxValue;
                }
                else
                {
                    plotBuf[startBufIndex] = maxValue;
                    plotBuf[startBufIndex + 1] = minValue;
                }
            }
        }
        
        private void FillUShortRangeFitData(int start, int end)
        {
            IList<ushort> yDataBuf = _dataBuf as IList<ushort>;
            IList<ushort> plotBuf = _plotBuf as IList<ushort>;
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2 * dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex * _sparseRatio + _indexOffset;
                // 真实数据的终止索引位置，不包含该点
                int pointEndIndex = pointStartIndex + 2 * _sparseRatio;
                if (pointEndIndex > yDataBuf.Count)
                {
                    pointEndIndex = yDataBuf.Count;
                }

                ushort maxValue = yDataBuf[pointStartIndex];
                ushort minValue = yDataBuf[pointStartIndex];
                int maxIndex = pointStartIndex;
                int minIndex = pointStartIndex;
                for (int pointIndex = pointStartIndex + 1; pointIndex < pointEndIndex; pointIndex++)
                {
                    ushort value = yDataBuf[pointIndex];
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
                    plotBuf[startBufIndex] = minValue;
                    plotBuf[startBufIndex + 1] = maxValue;
                }
                else
                {
                    plotBuf[startBufIndex] = maxValue;
                    plotBuf[startBufIndex + 1] = minValue;
                }
            }
        }
        
        private void FillByteRangeFitData(int start, int end)
        {
            IList<byte> yDataBuf = _dataBuf as IList<byte>;
            IList<byte> plotBuf = _plotBuf as IList<byte>;
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2 * dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex * _sparseRatio + _indexOffset;
                // 真实数据的终止索引位置，不包含该点
                int pointEndIndex = pointStartIndex + 2 * _sparseRatio;
                if (pointEndIndex > yDataBuf.Count)
                {
                    pointEndIndex = yDataBuf.Count;
                }

                byte maxValue = yDataBuf[pointStartIndex];
                byte minValue = yDataBuf[pointStartIndex];
                int maxIndex = pointStartIndex;
                int minIndex = pointStartIndex;
                for (int pointIndex = pointStartIndex + 1; pointIndex < pointEndIndex; pointIndex++)
                {
                    byte value = yDataBuf[pointIndex];
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
                    plotBuf[startBufIndex] = minValue;
                    plotBuf[startBufIndex + 1] = maxValue;
                }
                else
                {
                    plotBuf[startBufIndex] = maxValue;
                    plotBuf[startBufIndex + 1] = minValue;
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
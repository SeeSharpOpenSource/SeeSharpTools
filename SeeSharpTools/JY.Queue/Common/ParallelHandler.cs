using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpTools.JY.ThreadSafeQueue.Common
{
    public class ParallelHandler<TDataType>
    {
        private readonly ParallelOptions _option;
        private const int MinParallelSize = 100;
        private readonly object _parallelLock = new object();

        //每个计算块的大小
        private readonly TDataType[] _dataBuf;
        private int _blockSize;
        private int _indexOffset;

        private IList<double> _datas;

        public ParallelHandler(TDataType[] dataBuf)
        {
            _dataBuf = dataBuf;
            this._option = new ParallelOptions();
            this._indexOffset = 0;
            // 计算限制型配置并行度为内核个数
            _option.MaxDegreeOfParallelism = Environment.ProcessorCount;

            this._maxDatas = new double[_option.MaxDegreeOfParallelism];
            this._minDatas = new double[_option.MaxDegreeOfParallelism];
        }

        #region Enque Operation

        private int _srcIndexOffset;
        private int _enqueCount;
        private IList<TDataType> _srcBuf; 

        public void EnqueData(IList<TDataType> srcBuf, int fillOffset, int srcOffset, int count)
        {
            if (count < MinParallelSize)
            {
                for (int i = 0; i < count; i++)
                {
                    _dataBuf[fillOffset] = srcBuf[srcOffset];
                }
            }
            else
            {
                _blockSize = GetBlockSize(count);
                _indexOffset = fillOffset;
                _srcIndexOffset = srcOffset;
                _enqueCount = count;
                _srcBuf = srcBuf;
                Parallel.For(0, _option.MaxDegreeOfParallelism, _option, EnqueDataToDataBuf);
                _srcBuf = null;
            }
        }

        private void EnqueDataToDataBuf(int blockIndex)
        {
            int startIndex = blockIndex*_blockSize;
            int blockEnqueCount = _blockSize;
            if (startIndex + _blockSize > _enqueCount)
            {
                blockEnqueCount = _enqueCount - startIndex;
            }
            int srcBufIndex = startIndex + _srcIndexOffset;
            int dataBufIndex = startIndex + _indexOffset;
            for (int i = 0; i < blockEnqueCount; i++)
            {
                _dataBuf[dataBufIndex++] = _srcBuf[srcBufIndex++];
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

        private int GetBlockSize(int count)
        {
            return (count - 1) / _option.MaxDegreeOfParallelism + 1;
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpTools.JY.ArrayUtility
{
    internal class ParallelCalculation<TDataType>
    {
        private readonly ParallelOptions _option;
        private readonly object _parallelLock = new object();

        //每个计算块的大小
        private int _blockSize;
        private int _indexOffset;

        private IList<double> _data1;
        private IList<double> _data2;
        private IList<double> _data3;


        public ParallelCalculation()
        {
            this._option = new ParallelOptions();
            this._indexOffset = 0;
            // 计算限制型配置并行度为内核个数
            _option.MaxDegreeOfParallelism = Environment.ProcessorCount;
        }

        #region Max / Min /Interval计算
        
        private double[] _maxDatas;
        private double[] _minDatas;

        public void GetMaxAndMin(IList<double> datas, out double max, out double min)
        {
            this._maxDatas = new double[_option.MaxDegreeOfParallelism];
            this._minDatas = new double[_option.MaxDegreeOfParallelism];
            _data1 = datas;
            _blockSize = GetBlockSize(datas.Count);
            _indexOffset = 0;
            Parallel.For(0, _option.MaxDegreeOfParallelism, FillMaxAndMinToBuf);
            max = _maxDatas.Max();
            min = _minDatas.Min();
            _data1 = null;
        }

        private void FillMaxAndMinToBuf(int blockIndex)
        {
            int startIndex = blockIndex * _blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > _data1.Count)
            {
                endIndex = _data1.Count;
            }
            while (startIndex < _data1.Count)
            {
                if (!double.IsNaN(_data1[startIndex]))
                {
                    break;
                }
                startIndex++;
            }
            if (startIndex == _data1.Count)
            {
                _maxDatas[blockIndex] = 0;
                _minDatas[blockIndex] = 0;
                return;
            }
            double maxValue = _data1[startIndex];
            double minValue = _data1[startIndex];
            for (int index = startIndex + 1; index < endIndex; index++)
            {
                double value = _data1[index];
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

        private TDataType _defaultValue;

        private IList<TDataType> _buf; 

        public void FillData(IList<TDataType> data, TDataType defaultValue)
        {
            this._buf = data;
            this._defaultValue = defaultValue;
            _blockSize = GetBlockSize(data.Count);
            Parallel.For(0, _option.MaxDegreeOfParallelism, _option, FillBufWithDefault);
            this._data1 = null;
        }

        private void FillBufWithDefault(int blockIndex)
        {
            int startIndex = _blockSize*blockIndex;
            int endIndex = startIndex + _blockSize;
            if (endIndex > _buf.Count)
            {
                endIndex = _buf.Count;
            }
            for (int i = startIndex; i < endIndex; i++)
            {
                _buf[i] = _defaultValue;
            }
        }


        #endregion

        private int GetBlockSize(int count)
        {
            return (count - 1) / _option.MaxDegreeOfParallelism + 1;
        }
    }
}
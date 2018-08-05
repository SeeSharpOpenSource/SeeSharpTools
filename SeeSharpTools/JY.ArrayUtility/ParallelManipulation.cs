using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpTools.JY.ArrayUtility
{
    internal class ParallelManipulation<TDataType>
    {
        private readonly ParallelOptions _option;
        private readonly object _parallelLock = new object();

        //每个计算块的大小
        private int _blockSize;
        private int _indexOffset;

        public ParallelManipulation()
        {
            this._option = new ParallelOptions();
            this._indexOffset = 0;
            // 计算限制型配置并行度为内核个数
            _option.MaxDegreeOfParallelism = Environment.ProcessorCount;
        }

        private IList<TDataType> _src1;
        private IList<TDataType> _src2;
        private TDataType[,] _src3;
        private TDataType[,] _src4;
        private IList<TDataType> _dst1;
        private TDataType[,] _dst2;

        public void ConnectTo2Dim(IList<TDataType> src1, IList<TDataType> src2, TDataType[,] dst)
        {
            _blockSize = GetBlockSize(src1.Count);
            this._src1 = src1;
            this._src2 = src2;
            this._dst2 = dst;
            Parallel.For(0, _option.MaxDegreeOfParallelism, _option, Connect1DimTo2Dim);
        }

        private void Connect1DimTo2Dim(int blockIndex)
        {
            int startIndex = _blockSize*blockIndex;
            int endIndex = startIndex + _blockSize;
            if (endIndex > _src1.Count)
            {
                endIndex = _src1.Count;
            }
            for (int i = startIndex; i < endIndex; i++)
            {
                _dst2[i, 0] = _src1[i];
                _dst2[i, 1] = _src2[i];
            }
        }

        public void ArrayConnect(TDataType[,] src1, TDataType[] src2, TDataType[,] dst)
        {
            _blockSize = GetBlockSize(src1.GetLength(0));
            this._src3 = src1;
            this._src2 = src2;
            this._dst2 = dst;
            Parallel.For(0, _option.MaxDegreeOfParallelism, _option, OneDimConnectToTwoDim);
        }

        private void OneDimConnectToTwoDim(int blockIndex)
        {
            int startIndex = _blockSize * blockIndex;
            int endIndex = startIndex + _blockSize;
            if (endIndex > _src1.Count)
            {
                endIndex = _src1.Count;
            }
            int columnSize = _dst2.GetLength(1);
            for (int i = startIndex; i < endIndex; i++)
            {
                
                for (int j = 0; j < columnSize - 1; j++)
                {
                    _dst2[i, j] = _src3[i, j];
                }
                _dst2[i, columnSize - 1] = _src2[i];
            }
        }

        public void Transpose(TDataType[,] src, TDataType[,] dst)
        {
            _blockSize = GetBlockSize(src.GetLength(0));
            this._src3 = src;
            this._dst2 = dst;
            Parallel.For(0, _option.MaxDegreeOfParallelism, _option, TransposeToDst);
        }

        private void TransposeToDst(int blockIndex)
        {
            int startIndex = _blockSize * blockIndex;
            int endIndex = startIndex + _blockSize;
            if (endIndex > _src1.Count)
            {
                endIndex = _src1.Count;
            }
            for (int i = startIndex; i < endIndex; i++)
            {
                for (int j = 0; j < _src3.GetLength(1); j++)
                {
                    _dst2[j, i] = _src3[i, j];
                }
            }
        }

        private int GetBlockSize(int count)
        {
            return (count - 1) / _option.MaxDegreeOfParallelism + 1;
        }

        
    }
}
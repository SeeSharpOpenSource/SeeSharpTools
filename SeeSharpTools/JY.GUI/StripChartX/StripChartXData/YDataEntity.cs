using System;
using System.Collections.Generic;

namespace SeeSharpTools.JY.GUI.StripChartXData
{
    internal class YDataEntity<TDataType> : IDisposable
    {
        private List<OverLapWrapBuffer<TDataType>> _wrapBuffers;
        public int Capacity { get; }
        public int LineCount { get; }

        public YDataEntity(int lineCount, int capacity)
        {
            this.Capacity = capacity;
            this.LineCount = lineCount;
            _wrapBuffers = new List<OverLapWrapBuffer<TDataType>>(lineCount);
            for (int i = 0; i < lineCount; i++)
            {
                _wrapBuffers.Add(new OverLapWrapBuffer<TDataType>(capacity));
            }
        }

        public int Count => _wrapBuffers[0].Count;

        public void AddRange(Array data, int length)
        {
            int sampleCount = length/LineCount;
            int offset = 0;
            foreach (OverLapWrapBuffer<TDataType> wrapBuffer in _wrapBuffers)
            {
                wrapBuffer.Add(data, sampleCount, offset);
                offset += sampleCount;
            }
        }

        public void AddRange(List<TDataType> data, int length)
        {
            AddRange(data.ToArray(), length);
        }

        public TDataType this[int rowIndex, int columnIndex]
        {
            get { return _wrapBuffers[rowIndex][columnIndex]; }
        }

        public void Dispose()
        {
            for (int i = 0; i < LineCount; i++)
            {
                _wrapBuffers[i] = null;
            }
            _wrapBuffers.Clear();
        }
    }
}
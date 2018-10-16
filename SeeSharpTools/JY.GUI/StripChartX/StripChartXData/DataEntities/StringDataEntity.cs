using System;
using System.Collections;
using System.Collections.Generic;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI.StripChartXData.DataEntities
{
    internal class StringDataEntity<TDataType> : DataEntityBase
    {
        private readonly OverLapStrBuffer _xBuffer;
        private readonly List<OverLapWrapBuffer<TDataType>> _yBuffers;

        private readonly PlotBuffer<TDataType> _plotBuffer;

        public StringDataEntity(PlotManager plotManager, DataEntityInfo dataInfo) : base(plotManager, dataInfo)
        {
            _xBuffer = new OverLapStrBuffer(DataInfo.Capacity);
            _yBuffers = new List<OverLapWrapBuffer<TDataType>>(DataInfo.LineCount);
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                _yBuffers.Add(new OverLapWrapBuffer<TDataType>(DataInfo.Capacity));
            }
            _plotBuffer = new PlotBuffer<TDataType>(DataInfo.LineCount, DataInfo.Capacity);
        }

        public override int PlotCount
        {
            get { return _plotBuffer.PlotCount; }
            set { _plotBuffer.PlotCount = value; }
        }

        public override int SamplesInChart => _xBuffer.Count;

        public override void AddPlotData(IList<string> xData, Array lineData)
        {
            int sampleCount = xData.Count;
            _xBuffer.Add(xData, sampleCount);
            int offset = 0;
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                OverLapWrapBuffer<TDataType> yBuffer = _yBuffers[i];
                bool removedDataInsideRange = IsRemovedDataInsideRange(yBuffer, sampleCount, i);
                yBuffer.Add(lineData, sampleCount, offset);
                offset += sampleCount;
                RefreshMaxAndMinValue(yBuffer, i, sampleCount, removedDataInsideRange);
            }
        }

        public override void AddPlotData(DateTime[] startTime, Array lineData)
        {
            throw new NotImplementedException();
        }

        public override void AddPlotData(Array lineData, int sampleCount)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetXPlotBuffer()
        {
            return _plotBuffer.XPlotBuffer;
        }

        public override string GetXValue(int xIndex)
        {
            return _xBuffer[xIndex];
        }

        public override object GetYValue(int xIndex, int seriesIndex)
        {
            return _yBuffers[seriesIndex][xIndex];
        }

        public override void Clear()
        {
            base.Clear();
            _xBuffer.Clear();
            foreach (OverLapWrapBuffer<TDataType> yBuffer in _yBuffers)
            {
                yBuffer.Clear();
            }
        }

        // XPlotBuffer是倒着保存的
        public override IList GetXData()
        {
            int startIndex = _plotBuffer.XPlotBuffer.Count - _plotBuffer.PlotCount;
            return _plotBuffer.XPlotBuffer.GetRange(startIndex, _plotBuffer.PlotCount);
        }

        public override IList GetYData()
        {
            _plotBuffer.YShallowBuffer.Clear();
            foreach (List<TDataType> yBuf in _plotBuffer.YPlotBuffer)
            {
                _plotBuffer.YShallowBuffer.Add(yBuf.GetRange(0, _plotBuffer.PlotCount));
            }
            return _plotBuffer.YShallowBuffer;
        }

        public override bool FillYPlotDatas(int beginXIndex, int endXIndex, bool forceRefresh, int seriesIndex, int newSparseRatio, int plotCount)
        {
            // 如果不需要强制更新，且当前起始位置等于上次起始位置、当前结束位置小于等于上次结束位置、新的SparseRatio等于上次的SpaseRatio时无需更新数据。
            if (!forceRefresh && LastYStartIndex[seriesIndex] == beginXIndex && LastYEndIndex[seriesIndex] >= endXIndex &&
                SparseRatio[seriesIndex] == newSparseRatio)
            {
                return false;
            }
            LastYStartIndex[seriesIndex] = beginXIndex;
            LastYEndIndex[seriesIndex] = endXIndex;
            SparseRatio[seriesIndex] = newSparseRatio;

            if (1 == newSparseRatio)
            {
                ParallelHandler.FillNoneFitPlotData(beginXIndex, SparseRatio[seriesIndex], _yBuffers[seriesIndex],
                            _plotBuffer.YPlotBuffer[seriesIndex], plotCount);
            }
            else
            {
                switch (ParentManager.FitType)
                {
                    case StripChartX.FitType.None:
                        ParallelHandler.FillNoneFitPlotData(beginXIndex, SparseRatio[seriesIndex], _yBuffers[seriesIndex],
                            _plotBuffer.YPlotBuffer[seriesIndex], plotCount);
                        break;
                    case StripChartX.FitType.Range:
                        ParallelHandler.FillRangeFitPlotData<TDataType>(beginXIndex, SparseRatio[seriesIndex], _yBuffers[seriesIndex],
                            _plotBuffer.YPlotBuffer[seriesIndex], plotCount);
                        break;
                }
            }

            return true;
        }
    }
}
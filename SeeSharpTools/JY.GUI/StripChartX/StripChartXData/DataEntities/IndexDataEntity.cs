using System;
using System.Collections;
using System.Collections.Generic;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI.StripChartXData.DataEntities
{
    internal class IndexDataEntity<TDataType> : DataEntityBase
    {
        private int _startIndex = 0;
        private int _nextIndex = 0;

        private readonly PlotBuffer<TDataType> _plotBuffer;

        private readonly List<OverLapWrapBuffer<TDataType>> _yBuffers;

        public IndexDataEntity(PlotManager plotManager, DataEntityInfo dataInfo) : base(plotManager, dataInfo)
        {
            this._startIndex = plotManager.StartIndex;
            this._nextIndex = plotManager.StartIndex;
            
            this._plotBuffer = new PlotBuffer<TDataType>(DataInfo.LineCount, DataInfo.Capacity);

            _yBuffers = new List<OverLapWrapBuffer<TDataType>>(DataInfo.LineCount);
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                _yBuffers.Add(new OverLapWrapBuffer<TDataType>(DataInfo.Capacity));
            }
        }

        public override int PlotCount
        {
            get { return _plotBuffer.PlotCount; }
            set { _plotBuffer.PlotCount = value; }
        }

        public override int SamplesInChart => (_nextIndex - _startIndex);

        public override void AddPlotData(IList<string> xData, Array lineData)
        {
            throw new NotImplementedException();
        }

        public override void AddPlotData(DateTime[] startTime, Array lineData)
        {
            throw new NotImplementedException();
        }

        public override void AddPlotData(Array lineData, int sampleCount)
        {
            RefreshIndexData(sampleCount);
            int offset = 0;
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                _yBuffers[i].Add(lineData, sampleCount, offset);
                offset += sampleCount;
            }
        }

        public override List<int> GetXPlotBuffer()
        {
            return _plotBuffer.XPlotBuffer;
        }

        private void RefreshIndexData(int samplesToPlot)
        {
            _nextIndex += samplesToPlot;
            if (_nextIndex - _startIndex > ParentManager.DisplayPoints)
            {
                _startIndex = _nextIndex - ParentManager.DisplayPoints;
            }
        }

        public override string GetXValue(int xIndex)
        {
            return (_startIndex + xIndex).ToString();
        }

        public override object GetYValue(int xIndex, int seriesIndex)
        {
            return _yBuffers[seriesIndex][xIndex];
        }

        public override IList<TDataType> GetPlotDatas<TDataType>(int startIndex, int endIndex)
        {
            throw new NotImplementedException();
        }

        public override void GetMaxAndMinYValue(int seriesIndex, out double maxYValue, out double minYValue)
        {
            ParallelHandler.GetMaxAndMin(_yBuffers[seriesIndex], out maxYValue, out minYValue);
        }

        public override void GetMaxAndMinYValue(out double maxYValue, out double minYValue)
        {
            maxYValue = double.MinValue;
            minYValue = double.MaxValue;
            double tmpMaxYValue = 0;
            double tmpMinYValue = 0;

            foreach (OverLapWrapBuffer<TDataType> yBuffer in _yBuffers)
            {
                ParallelHandler.GetMaxAndMin(yBuffer, out tmpMaxYValue, out tmpMinYValue);
                if (maxYValue < tmpMaxYValue)
                {
                    maxYValue = tmpMaxYValue;
                }
                if (minYValue > tmpMinYValue)
                {
                    minYValue = tmpMinYValue;
                }
            }
        }

        public override void Clear()
        {
            base.Clear();
            _startIndex = ParentManager.DisplayPoints;
            _nextIndex = ParentManager.DisplayPoints;
            foreach (OverLapWrapBuffer<TDataType> yBuffer in _yBuffers)
            {
                yBuffer.Clear();
            }
        }

        public override IList GetXData()
        {
            return _plotBuffer.XPlotBuffer.GetRange(0, _plotBuffer.PlotCount);
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
            if (!forceRefresh &&　LastYStartIndex[seriesIndex] == beginXIndex && LastYEndIndex[seriesIndex] >= endXIndex && 
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
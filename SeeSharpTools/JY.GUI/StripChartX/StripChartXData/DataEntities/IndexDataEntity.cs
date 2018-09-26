using System;
using System.Collections.Generic;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI.StripChartXData.DataEntities
{
    internal class IndexDataEntity<TDataType> : DataEntityBase
    {
        private int _startIndex = 0;
        private int _nextIndex = 0;

        private readonly List<OverLapWrapBuffer<TDataType>> _yBuffers;

        public IndexDataEntity(PlotManager plotManager, DataEntityInfo dataInfo) : base(plotManager, dataInfo)
        {
            this._startIndex = plotManager.StartIndex;
            this._nextIndex = plotManager.StartIndex;

            _yBuffers = new List<OverLapWrapBuffer<TDataType>>(DataInfo.LineCount);
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                _yBuffers.Add(new OverLapWrapBuffer<TDataType>(DataInfo.Capacity));
            }
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

        private void RefreshIndexData(int samplesToPlot)
        {
            _nextIndex += samplesToPlot;
            if (_nextIndex - _startIndex > ParentManager.DisplayPoints)
            {
                _startIndex = _nextIndex - ParentManager.DisplayPoints;
            }
        }

        public override void GetXYValue(int xIndex, int seriesIndex, ref string xValue, ref string yValue)
        {
            xValue = (_startIndex + xIndex).ToString();
            yValue = _yBuffers[seriesIndex][xIndex].ToString();
        }

        public override IList<TDataType> GetPlotDatas<TDataType>(int startIndex, int endIndex)
        {
            throw new NotImplementedException();
        }

        public override void GetMaxAndMinYValue(int seriesIndex, out double maxYValue, out double minYValue)
        {
            throw new NotImplementedException();
        }

        public override void GetMaxAndMinYValue(out double maxYValue, out double minYValue)
        {
            throw new NotImplementedException();
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
    }
}
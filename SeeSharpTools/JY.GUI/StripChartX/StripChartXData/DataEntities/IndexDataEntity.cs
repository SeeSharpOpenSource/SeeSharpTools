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
            
            this._plotBuffer = new PlotBuffer<TDataType>(DataInfo.LineCount);

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
            return _plotBuffer.XPlotBuffer.GetRange(0, _plotBuffer.PlotSize);
        }

        public override IList GetYData()
        {
            _plotBuffer.YShallowBuffer.Clear();
            foreach (List<TDataType> yBuf in _plotBuffer.YPlotBuffer)
            {
                _plotBuffer.YShallowBuffer.Add(yBuf.GetRange(0, _plotBuffer.PlotSize));
            }
            return _plotBuffer.YShallowBuffer;
        }
    }
}
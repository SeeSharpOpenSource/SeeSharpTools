using System;
using System.Collections.Generic;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI.StripChartXData
{
    internal abstract class DataEntityBase
    {
        public DataEntityInfo DataInfo { get; }

        protected readonly PlotManager ParentManager;

        public StripChartX.FitType FitType { get; set; }

        protected ParallelHandler ParallelHandler;

        public abstract int SamplesInChart { get; }

        public int RealSamplesInChart { get; }

        public readonly List<double[]> _plotBuffers; 

        protected DataEntityBase(PlotManager plotManager, DataEntityInfo dataInfo)
        {
            this.ParentManager = plotManager;
            this.FitType = plotManager.FitType;
            this.DataInfo = dataInfo;
            this._plotBuffers = new List<double[]>(DataInfo.LineCount);
            this.ParallelHandler = new ParallelHandler(this, ParentManager.DataCheckParams);
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                _plotBuffers.Add(new double[Constants.MaxPointsInSingleSeries]);
            }

        }

        public virtual void Initialize(int sampleCount)
        {
        }

        public abstract void AddPlotData(IList<string> xData, Array lineData);

        public abstract void AddPlotData(DateTime[] startTime, Array lineData);

        public abstract void AddPlotData(Array lineData, int sampleCount);

        public abstract void GetXYValue(int xIndex, int seriesIndex, ref string xValue, ref string yValue);

        public abstract IList<TDataType> GetPlotDatas<TDataType>(int startIndex, int endIndex);

        public abstract void GetMaxAndMinYValue(int seriesIndex, out double maxYValue, out double minYValue);

        public abstract void GetMaxAndMinYValue(out double maxYValue, out double minYValue);

//        protected void RefreshSamplesInChart(int plotSamples)
//        {
//            if (SamplesInChart >= ParentManager.DisplayPoints)
//            {
//                return;
//            }
//            SamplesInChart += plotSamples;
//            if (SamplesInChart > ParentManager.DisplayPoints)
//            {
//                SamplesInChart = ParentManager.DisplayPoints;
//            }
//        }

        public virtual void Clear()
        {
//            this.SamplesInChart = 0;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI.StripChartXData
{
    internal abstract class DataEntityBase
    {
        public DataEntityInfo DataInfo { get; }

        // 当前XPlotBuffer中数据的起始和结束值
        private int _lastXStart;
        private int _lastXEnd;
        private int _lastXSparseRatio;

        // 当前YPlotBuffer中各个线条的起始和结束值
        protected int[] LastYStartIndex;
        protected int[] LastYEndIndex;

        // 每个线条当前的稀疏比
        protected int[] SparseRatio;

        protected readonly PlotManager ParentManager;

        public StripChartX.FitType FitType { get; set; }

        public abstract int PlotCount { get; set; }

        protected ParallelHandler ParallelHandler;

        private readonly double[] _maxYValues;
        private readonly double[] _minYValues;

        public abstract int SamplesInChart { get; }

        protected DataEntityBase(PlotManager plotManager, DataEntityInfo dataInfo)
        {
            this.ParentManager = plotManager;
            this.FitType = plotManager.FitType;
            this.DataInfo = dataInfo;
            this.ParallelHandler = new ParallelHandler(ParentManager.DataCheckParams);

            _lastXStart = int.MinValue;
            _lastXEnd = int.MinValue;
            _lastXSparseRatio = int.MinValue;
            LastYStartIndex = new int[DataInfo.LineCount];
            LastYEndIndex = new int[DataInfo.LineCount];
            SparseRatio = new int[DataInfo.LineCount];
            _maxYValues = new double[DataInfo.LineCount];
            _minYValues = new double[DataInfo.LineCount];
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                LastYStartIndex[i] = int.MinValue;
                LastYEndIndex[i] = int.MinValue;
                SparseRatio[i] = int.MaxValue;
                _maxYValues[i] = double.MinValue;
                _minYValues[i] = double.MaxValue;
            }
        }

        public virtual void Initialize(int sampleCount)
        {
        }

        public abstract void AddPlotData(IList<string> xData, Array lineData);

        public abstract void AddPlotData(DateTime[] startTime, Array lineData);

        public abstract void AddPlotData(Array lineData, int sampleCount);

        public abstract List<int> GetXPlotBuffer();

        public abstract string GetXValue(int xIndex);
        public abstract object GetYValue(int xIndex, int seriesIndex);
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
            _lastXStart = int.MinValue;
            _lastXEnd = int.MinValue;
            _lastXSparseRatio = int.MinValue;
            LastYStartIndex = new int[DataInfo.LineCount];
            LastYEndIndex = new int[DataInfo.LineCount];
            SparseRatio = new int[DataInfo.LineCount];
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                LastYStartIndex[i] = int.MinValue;
                LastYEndIndex[i] = int.MinValue;
                SparseRatio[i] = int.MaxValue;
                _maxYValues[i] = double.MinValue;
                _minYValues[i] = double.MaxValue;
            }
        }

        public abstract IList GetXData();

        public abstract IList GetYData();

        public abstract bool FillYPlotDatas(int beginXIndex, int endXIndex, bool forceRefresh, int seriesIndex, int newSparseRatio, int plotCount);

        protected void RefreshMaxAndMinValue<TDataType>(OverLapWrapBuffer<TDataType> datas, int seriesIndex, int samplesAdded, bool removedDataInsideRange)
        {
            int newDataStartIndex = datas.Count - samplesAdded;
            IList<TDataType> newDatas = datas.GetRange(newDataStartIndex, samplesAdded);
            double newDataMax, newDataMin;
            ParallelHandler.GetMaxAndMin(newDatas, out newDataMax, out newDataMin);
            // 如果删除的点没有超过范围，则这些点不会影响最终Y轴的最大最小值
            if (removedDataInsideRange)
            {
                // 如果Y轴新的最大值超过原来最大值，则新的最大值最大；如果Y轴新的最小值小于原来最小值，则新的最小值最小
                if (newDataMax >= _maxYValues[seriesIndex])
                {
                    _maxYValues[seriesIndex] = newDataMax;
                }
                if (newDataMin <= _minYValues[seriesIndex])
                {
                    _minYValues[seriesIndex] = newDataMin;
                }
            }
            // 如果新添加的数据Y值范围覆盖到原来的范围，则新的数据的范围就是最终的Y轴范围
            else if(newDataMax >= _maxYValues[seriesIndex] && newDataMin <= _minYValues[seriesIndex])
            {
                _maxYValues[seriesIndex] = newDataMax;
                _minYValues[seriesIndex] = newDataMin;
            }
            else
            {
                double max, min;
                ParallelHandler.GetMaxAndMin(datas, out max, out min);
                _maxYValues[seriesIndex] = max;
                _minYValues[seriesIndex] = min;
            }
        }

        protected bool IsRemovedDataInsideRange<TDataType>(OverLapWrapBuffer<TDataType> data, int samplesToAdd, int seriesIndex)
        {
            double lastMax = _maxYValues[seriesIndex];
            double lastMin = _minYValues[seriesIndex];
            int samplesToRemove = SamplesInChart + samplesToAdd - DataInfo.Capacity;
            if (samplesToRemove <= 0)
            {
                return true;
            }
            IList<TDataType> removedDatas = data.GetRange(0, samplesToRemove);
            double dataMax, dataMin;
            ParallelHandler.GetMaxAndMin(removedDatas, out dataMax, out dataMin);

            // 是否在区域内的判定规则：新的数值范围最大不能超过原来的范围MinDoubleValue
            bool isDataInsideRange = dataMax - lastMax < Constants.MinDoubleValue &&
                                     lastMin - dataMin < Constants.MinDoubleValue;
            // 是否在区域内的判定规则：新的范围的极值极性必须和原来的极值极性相同
            bool isBoundSamePolarity = !(lastMax <= 0 ^ dataMax <= 0) && !(lastMin >= 0 ^ dataMin >= 0);
            return isDataInsideRange && isBoundSamePolarity;
        }

        public void GetMaxAndMinYValue(int seriesIndex, out double maxYValue, out double minYValue)
        {
            maxYValue = _maxYValues[seriesIndex];
            minYValue = _minYValues[seriesIndex];
        }

        public void GetMaxAndMinYValue(out double maxYValue, out double minYValue)
        {
            maxYValue = double.MinValue;
            minYValue = double.MaxValue;
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                if (maxYValue < _maxYValues[i])
                {
                    maxYValue = _maxYValues[i];
                }
                if (minYValue > _minYValues[i])
                {
                    minYValue = _minYValues[i];
                }
            }
        }

        public bool FillPlotDataInRange(int beginXIndex, int endXIndex, bool forceRefresh, int seriesIndex)
        {
            bool plotParamChanged = false;
            int plotCount = 0;
            int newSparseRatio = GetSparseRatio(beginXIndex, endXIndex, out plotCount);
            if (-1 == seriesIndex)
            {
                // 如果更新所有的线条，则依次更新所有线条的PlotBuffer
                FillXPlotDatas(beginXIndex, endXIndex, newSparseRatio, plotCount);
                for (int i = 0; i < DataInfo.LineCount; i++)
                {
                    plotParamChanged |= FillYPlotDatas(beginXIndex, endXIndex, forceRefresh, i, newSparseRatio, plotCount);
                }
            }
            else
            {
                FillXPlotDatas(beginXIndex, endXIndex, newSparseRatio, plotCount);
                plotParamChanged = FillYPlotDatas(beginXIndex, endXIndex, forceRefresh, seriesIndex, newSparseRatio, plotCount);
            }
            this.PlotCount = plotCount;
            return plotParamChanged;
        }

        // 为了保证效率，X轴的buffer是倒着放的，最后一个元素放在容器最后一个位置，然后依次向前延伸
        private void FillXPlotDatas(int beginXIndex, int endXIndex, int newSparseRatio, int plotCount)
        {
            // 如果当前起始值大于等于上次起始值、当前结束值等于上次结束值、新的SparseRatio等于上次的SpaseRatio时无需更新数据。
            int xStart = beginXIndex - SamplesInChart;
            int xEnd = endXIndex - SamplesInChart;
            if (_lastXStart <= xStart && _lastXEnd == xEnd && _lastXSparseRatio == newSparseRatio)
            {
                return;
            }
            List<int> xPlotBuffer = GetXPlotBuffer();
            int bufSize = xPlotBuffer.Count;
            int xValue = 0;
            int index;
            // 如果X轴终点相同，稀疏比相同，则直接在前面添加
            if (_lastXEnd == xEnd && _lastXSparseRatio == newSparseRatio)
            {
                index = bufSize - ((_lastXEnd - _lastXStart)/newSparseRatio + 1) - 1;
                xValue = _lastXStart;
            }
            else
            {
                // X值终点为-1，全部重新赋值
                xValue = xEnd + newSparseRatio;
                index = bufSize - 1;
            }
            // 如果不等于，最后一次很有可能会出现index < 0，所以对最后一次做特殊处理
            while (xValue > xStart + newSparseRatio)
            {
                xValue -= newSparseRatio;
                xPlotBuffer[index--] = xValue;
            }
            if (index >= 0)
            {
                xValue -= newSparseRatio;
                xPlotBuffer[index] = xValue;
            }

            _lastXStart = xValue;
            _lastXSparseRatio = newSparseRatio;
            _lastXEnd = xEnd;
        }

        private static int GetSparseRatio(int start, int end, out int plotCount)
        {
            int count = end - start + 1;
            int sparseRatio = 1;
            while (count > (Constants.MaxPointsInSingleSeries * sparseRatio))
            {
                sparseRatio *= 2;
            }

            plotCount = (count + sparseRatio - 1) / sparseRatio;
            return sparseRatio;
        }
    }
}
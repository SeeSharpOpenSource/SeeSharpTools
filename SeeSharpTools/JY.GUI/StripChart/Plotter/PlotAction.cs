using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.Plotter;
using SeeSharpTools.JY.GUI.StripChartUtility;

namespace SeeSharpTools.JY.GUI
{
    internal abstract class PlotAction : IDisposable
    {
        protected StripPlotter Plotter;

        protected SeriesCollection PlotSeries;
        protected int MaxSparseRatio;

        public int SamplesInChart { get; protected set; }
        public int SparseRatio { get; protected set; }

        public int ViewStartIndex { get; protected set; }
        public int ViewEndIndex { get; protected set; }

        protected AxisViewAdapter AxisViewAdapter;

        public OverLapStrBuffer XWrapBuf;
        public List<OverLapWrapBuffer<double>> YWrapBufs;

        // 用于显示的X轴数据的缓存、Y轴缓存和待更新的点数
        internal int PlotSize;
        internal List<string> XAxisData;
        internal List<List<double>> YAxisData;
        protected List<List<double>> YShallowAxisData;

        protected delegate void PlotMethod(int sampleSize);

        public int MaxXValue{ get; protected set; }
        public int MinXValue{ get; protected set; }

        private double _maxYValue;
        private double _minYValue;

        protected readonly ParallelHandler Parallel;

        protected PlotAction(StripPlotter plotter, AxisViewAdapter axisViewAdapter)
        {
            this.Plotter = plotter;
            this.PlotSeries = plotter.PlotSeries;
            this.SamplesInChart = 0;
            this.SparseRatio = 1;
            this.AxisViewAdapter = axisViewAdapter;

            this.Parallel = new ParallelHandler(this);

            _maxYValue = double.MinValue;
            _minYValue = double.MaxValue;
            this.YWrapBufs = new List<OverLapWrapBuffer<double>>(Constants.MaxSeriesToDraw);
            
        }

        public virtual void AdaptBufAndPoints()
        {
            MaxSparseRatio = 1;
            while (MaxSparseRatio * Constants.MaxPointsInSingleSeries < Plotter.MaxSampleNum)
            {
                MaxSparseRatio *= 2;
            }

            if (null == XWrapBuf || XWrapBuf.BufSize != Plotter.MaxSampleNum)
            {
                XWrapBuf = new OverLapStrBuffer(Plotter.MaxSampleNum);
                YWrapBufs.Clear();
                for (int i = 0; i < Plotter.LineNum; i++)
                {
                    YWrapBufs.Add(new OverLapWrapBuffer<double>(Plotter.MaxSampleNum));
                }
            }
            else if (YWrapBufs.Count != Plotter.LineNum)
            {
                while (YWrapBufs.Count < Plotter.LineNum)
                {
                    YWrapBufs.Add(new OverLapWrapBuffer<double>(Plotter.MaxSampleNum));
                }
                while (YWrapBufs.Count > Plotter.LineNum)
                {
                    YWrapBufs.RemoveAt(YWrapBufs.Count - 1);
                }
            }
            XWrapBuf.Clear();
            foreach (OverLapWrapBuffer<double> yWrapBuffer in YWrapBufs)
            {
                yWrapBuffer.Clear();
            }
        }

        protected static void FillBufWithDefault<TDataType>(IList<TDataType> buf, int count, TDataType defaultValue)
        {
            if (buf.Count == count)
            {
                return;
            }
            if (buf.Count > count)
            {
                buf.Clear();
            }
            while (buf.Count < count)
            {
                buf.Add(defaultValue);
            }
        }

        private readonly string[] _tmpXBuf = new string[1];
        private readonly double[] _tmpYBuf = new double[1];
        public void Plot(string xData, double yData)
        {
            _tmpYBuf[0] = yData;
            Plot(xData, _tmpYBuf);
        }

        public void Plot(string xData, double[] yData)
        {
            RefreshYAxisRange(1, yData.Max(), yData.Min());
            _tmpXBuf[0] = xData;
            FillDataToWrapBuf(_tmpXBuf, yData, 1);

            AxisViewAdapter.RefreshXAxisRange(1);
            RefreshPlot(1);
        }

        public void Plot(string[] xData, double[,] yData)
        {
            int sampleCount = xData.Length;
            FillDataToWrapBuf(xData, yData, sampleCount);
            RefreshYAxisRange(sampleCount, double.NaN, double.NaN);

            AxisViewAdapter.RefreshXAxisRange(sampleCount);
            RefreshPlot(sampleCount);
        }
        
        public void Plot(string[] xData, double[] yData)
        {
            int sampleCount = xData.Length;
            FillDataToWrapBuf(xData, yData, sampleCount);
            RefreshYAxisRange(sampleCount, double.NaN, double.NaN);

            AxisViewAdapter.RefreshXAxisRange(sampleCount);
            RefreshPlot(sampleCount);
        }

        public void Plot(Array yData, int sampleCount, Action<IList<string>, int> addXDataFunc)
        {
            FillDataToWrapBuf(yData, sampleCount, addXDataFunc);
            RefreshYAxisRange(sampleCount, double.NaN, double.NaN);
            AxisViewAdapter.RefreshXAxisRange(sampleCount);
            RefreshPlot(sampleCount);
        }
       
        public void RefreshPlot(int sampleSize)
        {
            RefreshPlotParams(sampleSize);
            FillAxisData(sampleSize);
            RefreshSeriesData(sampleSize);
//            RefreshPointXValue(sampleSize);
        }

        protected void RefreshSamplesInChart(int sampleSize)
        {
            SamplesInChart += sampleSize;
            if (SamplesInChart > Plotter.MaxSampleNum)
            {
                SamplesInChart = Plotter.MaxSampleNum;
            }
        }

        protected abstract void RefreshSeriesData(int sampleSize);
        protected abstract void FillAxisData(int sampleSize);

        /// <summary>
        /// 更新ViewStart/ViewEnd/SparseRatio/PlotSize
        /// </summary>
        /// <param name="sampleSize"></param>
        protected abstract void RefreshPlotParams(int sampleSize);
        
        // 更新Y轴范围。如果每条线只画一个点则未将数据添加到缓存时计算，如果画多个点则将数据添加到缓存后计算
        public void RefreshYAxisRange(int sampleCount, double maxNewYData, double minNewYData)
        {
            if (!Plotter.AutoYRange)
            {
                return;
            }
            // 外部如果配置maxNewYData或者minNewYData，则需要全部重新计算Y轴范围
            if (sampleCount == 1 &&  !double.IsNaN(maxNewYData) && !double.IsNaN(minNewYData))
            {
                bool isOldDataBeyondRange = false;
                // 需要移出部分点时
                if (SamplesInChart >= Plotter.MaxSampleNum)
                {
                    foreach (OverLapWrapBuffer<double> yWrapBuf in YWrapBufs)
                    {
                        double dataToRemove = yWrapBuf[0];
                        isOldDataBeyondRange |= (_maxYValue - dataToRemove < Constants.MinDoubleValue) ||
                                                (dataToRemove - _minYValue < Constants.MinDoubleValue);
                        if (isOldDataBeyondRange)
                        {
                            break;
                        }
                    }
                }
                bool isNewDataBeyondRange = maxNewYData - _maxYValue > Constants.MinDoubleValue ||
                                       _minYValue - minNewYData > Constants.MinDoubleValue;
                // 待移除数据不是Y轴的边界
                if (isOldDataBeyondRange)
                {
                    double maxY, minY;
                    _maxYValue = YWrapBufs[0][0];
                    _minYValue = YWrapBufs[0][0];
                    foreach (OverLapWrapBuffer<double> yWrapBuf in YWrapBufs)
                    {
                        //将待移除的点移出缓存
                        yWrapBuf.Dequeue();
                        Parallel.GetMaxAndMin(yWrapBuf, out maxY, out minY);
                        if (maxY - _maxYValue > Constants.MinDoubleValue)
                        {
                            _maxYValue = maxY;
                        }
                        if (_minYValue - minY > Constants.MinDoubleValue)
                        {
                            _minYValue = minY;
                        }
                    }
                }
                // 如果新数据也没有超出边界则无需更新Y轴范围
                if (!isNewDataBeyondRange)
                {
                    return;
                }
                if (maxNewYData > _maxYValue)
                {
                    _maxYValue = maxNewYData;
                }
                if (_minYValue > minNewYData)
                {
                    _minYValue = minNewYData;
                }
            }
            else
            {
                double maxY, minY;
                _maxYValue = double.MinValue;
                _minYValue = double.MaxValue;
                foreach (OverLapWrapBuffer<double> yWrapBuf in YWrapBufs)
                {
                    Parallel.GetMaxAndMin(yWrapBuf, out maxY, out minY);
                    if (maxY - _maxYValue > Constants.MinDoubleValue)
                    {
                        _maxYValue = maxY;
                    }
                    if (_minYValue - minY > Constants.MinDoubleValue)
                    {
                        _minYValue = minY;
                    }
                }
            }
            AxisViewAdapter.RefreshYAxisRange(_maxYValue, _minYValue);
        }

        protected void RefreshByDataBind(int sampleSize)
        {
            for (int i = 0; i < YWrapBufs.Count; i++)
            {
                PlotSeries[i].Points.DataBindXY(XWrapBuf, YWrapBufs[i]);
            }
        }

        protected void RefreshByPointOperation(int sampleSize)
        {
            int pointToAdd = PlotSize - PlotSeries[0].Points.Count;
            int pointToMove = SamplesInChart + sampleSize - Plotter.MaxSampleNum;
            
            int pointIndex = XWrapBuf.Count - sampleSize;
            // 将前面的点移动到后面
            while (pointToMove-- > 0)
            {
                for (int i = 0; i < PlotSeries.Count; i++)
                {
                    DataPoint firstPoint = PlotSeries[i].Points[0];
                    PlotSeries[i].Points.RemoveAt(0);
                    firstPoint.SetValueXY(XWrapBuf[pointIndex], YWrapBufs[i][pointIndex]);
                    PlotSeries[i].Points.Add(firstPoint);
                }
                pointIndex++;
            }
            // 新增固定个数的点
            while (pointToAdd-- > 0)
            {
                for (int i = 0; i < PlotSeries.Count; i++)
                {
                    PlotSeries[i].Points.AddXY(XWrapBuf[pointIndex], YWrapBufs[i][pointIndex]);
                }
                pointIndex++;
            }
        }

        // 用来更新每个点的X值，用于缩放计算
        protected void RefreshPointXValue(int sampleSize)
        {
            if (0 == sampleSize && SamplesInChart <= Constants.MaxPointsInSingleSeries)
            {
                return;
            }
            int startIndex;
            if (SamplesInChart > Constants.MaxPointsInSingleSeries)
            {
                startIndex = ViewStartIndex - SamplesInChart;
            }
            else
            {
                startIndex = -1*SamplesInChart;
            }
            for (int pointIndex = 0; pointIndex < PlotSeries[0].Points.Count; pointIndex++)
            {
                for (int lineIndex = 0; lineIndex < PlotSeries.Count; lineIndex++)
                {
                    PlotSeries[lineIndex].Points[pointIndex].XValue = startIndex;
                }
                startIndex += SparseRatio;
            }
        }

        protected void CacheYDataToBuf(double[,] yData, int sparseRatio = 1)
        {
            int sampleSize = yData.GetLength(1);
            if (sampleSize > Plotter.MaxSampleNum)
            {
                sampleSize = Plotter.MaxSampleNum;
            }
            int sampleSizeAfterSparse = GetSampleSizeAfterSparse(sampleSize, sparseRatio); ;
            int firstPlotIndex = sampleSize - 1 - (sampleSizeAfterSparse - 1) * sparseRatio;
            for (int i = 0; i < Plotter.LineNum; i++)
            {
                int plotIndex = firstPlotIndex;
                IList<double> yDataBuf = YAxisData[i];
                while (plotIndex < sampleSize)
                {
                    yDataBuf.Add(yData[i, plotIndex]);
                    plotIndex += sparseRatio;
                }
            }
        }

        protected void CacheYDataToBuf(double[] yData, int sparseRatio = 1)
        {
            int sampleSize = yData.Length/Plotter.LineNum;
            if (sampleSize > Plotter.MaxSampleNum)
            {
                sampleSize = Plotter.MaxSampleNum;
            }
            int sampleSizeAfterSparse = GetSampleSizeAfterSparse(sampleSize, sparseRatio); ;
            int firstPlotIndex = sampleSize - 1 - (sampleSizeAfterSparse - 1) * sparseRatio;
            for (int lineIndex = 0; lineIndex < Plotter.LineNum; lineIndex++)
            {
                IList<double> yDataBuf = YAxisData[lineIndex];
                int plotIndex = firstPlotIndex;
                while (plotIndex < sampleSize)
                {
                    yDataBuf.Add(yData[plotIndex]);
                    plotIndex += sparseRatio;
                }
            }
        }

        protected void ClearAllPoints()
        {
            foreach (Series series in PlotSeries)
            {
                series.Points.Clear();
            }
            SamplesInChart = 0;
        }

        protected int GetSampleSizeAfterSparse(int sampleSize, int sparseRatio, int offset = 0)
        {
            return (sampleSize - offset - 1)/sparseRatio + 1;
        }

        protected void FillDataToWrapBuf(string[] xData, Array yData, int sampleSize)
        {
            XWrapBuf.Add(xData, sampleSize);
            for (int lineIndex = 0; lineIndex < Plotter.LineNum; lineIndex++)
            {
                YWrapBufs[lineIndex].Add(yData, sampleSize, lineIndex * sampleSize);
            }
        }

        protected void FillDataToWrapBuf(Array yData, int sampleSize, Action<IList<string>, int> addXDataFunc)
        {
            addXDataFunc.Invoke(XWrapBuf, sampleSize);
            for (int lineIndex = 0; lineIndex < Plotter.LineNum; lineIndex++)
            {
                YWrapBufs[lineIndex].Add(yData, sampleSize, lineIndex * sampleSize);
            }
        }

        protected void CalcSparseRatioAndPlotSize(int plotDataCount)
        {
            if (plotDataCount >= Plotter.MaxSampleNum)
            {
                SparseRatio =  MaxSparseRatio;
            }
            else
            {
                SparseRatio = 1;
                while (plotDataCount >= Constants.MaxPointsInSingleSeries * SparseRatio)
                {
                    SparseRatio *= 2;
                }
            }
            PlotSize = (plotDataCount + SparseRatio - 1) / SparseRatio;
            //PlotSize在SparseRatio大于0时必须是偶数
            if (SparseRatio > 1)
            {
                PlotSize = PlotSize / 2 * 2;
            }
        }
        
        protected int RefreshPointInChartAndSparseRatio(int sampleSize)
        {
            SamplesInChart += sampleSize;
            if (SamplesInChart >= Plotter.MaxSampleNum)
            {
                SamplesInChart = Plotter.MaxSampleNum;
                SparseRatio = MaxSparseRatio;
                return (SamplesInChart - 1) / SparseRatio + 1;
            }

            int samplesAfterSparse = (SamplesInChart - 1)/SparseRatio + 1;
            while (samplesAfterSparse > Constants.MaxPointsInSingleSeries)
            {
                SparseRatio *= 2;
                samplesAfterSparse = (SamplesInChart - 1) / SparseRatio + 1;
            }
            return samplesAfterSparse;
        }

        public void Dispose()
        {
            XAxisData = null;
            YAxisData?.Clear();

            XWrapBuf = null;
            YWrapBufs?.Clear();
        }

        public void Clear()
        {
            this.SamplesInChart = 0;
            this.SparseRatio = 1;
            _maxYValue = double.MinValue;
            _minYValue = double.MaxValue;
        }
    }
}
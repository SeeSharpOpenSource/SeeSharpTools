using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.StripChartUtility;

namespace SeeSharpTools.JY.GUI
{
    internal class BindStripPlotter : PlotAction
    {
        public BindStripPlotter(StripPlotter plotter, AxisViewAdapter axisViewAdapter) : 
            base(plotter, axisViewAdapter)
        {
            // TODO 
            this.XAxisData = new List<string>(Constants.MaxPointsInSingleSeries);
            FillBufWithDefault(XAxisData, Constants.MaxPointsInSingleSeries, "");
            this.YAxisData = new List<List<double>>(Constants.MaxSeriesToDraw);
            this.YShallowAxisData = new List<List<double>>(Constants.MaxSeriesToDraw);
        }

        // 全局更新数据，主要用于有筛点时的显示
        protected void RefreshDataToChart()
        {
            List<string> xPlotData = GetXPlotData();
            List<List <double>> yPlotData = GetYPlotData();

            int pointsToAdd = PlotSize - PlotSeries[0].Points.Count;
            for (int lineIndex = 0; lineIndex < Plotter.LineNum; lineIndex++)
            {
                if (PlotSeries[lineIndex].Points.Count <= PlotSize && pointsToAdd < Constants.MaxMovePointCount)
                {
                    int leftPointsToAdd = pointsToAdd;
                    while (leftPointsToAdd-- > 0)
                    {
                        PlotSeries[lineIndex].Points.Add(new DataPoint(PlotSeries[lineIndex]));
                    }
                    for (int pointIndex = 0; pointIndex < PlotSize; pointIndex++)
                    {
                        PlotSeries[lineIndex].Points[pointIndex].SetValueXY(xPlotData[pointIndex],
                        yPlotData[lineIndex][pointIndex]);
                    }
                }
                else
                {
                    PlotSeries[lineIndex].Points.DataBindXY(xPlotData, yPlotData[lineIndex]);
                }
            }
        }

        public override void AdaptBufAndPoints()
        {
            base.AdaptBufAndPoints();
            XAxisData = new List<string>(Constants.MaxPointsInSingleSeries);
            FillBufWithDefault(XAxisData, Constants.MaxPointsInSingleSeries, " ");
            if (null != YAxisData && YAxisData.Count != Plotter.LineNum)
            {
                while (YAxisData.Count < Plotter.LineNum)
                {
                    List<double> newYBuf = new List<double>(Constants.MaxPointsInSingleSeries);
                    FillBufWithDefault(newYBuf, Constants.MaxPointsInSingleSeries, 0);
                    YAxisData.Add(newYBuf);
                }
                while (YAxisData.Count > Plotter.LineNum)
                {
                    YAxisData.RemoveAt(YAxisData.Count - 1);
                }
            }
        }

        protected override void RefreshSeriesData(int sampleSize)
        {
            if (SamplesInChart + sampleSize > Constants.MaxPointsInSingleSeries)
            {
                RefreshDataToChart();
            }
            else
            {
                if (sampleSize == 0)
                {
                    return;
                }
                // 如果单次写入点数较少，通过新建Point实现
                if (sampleSize <= Constants.MaxMovePointCount)
                {
                    RefreshByPointOperation(sampleSize);
                }
                else
                {
                    RefreshByDataBind(sampleSize);
                }
            }
            RefreshSamplesInChart(sampleSize);
            RefreshPointXValue(sampleSize);
        }

        private List<string> GetXPlotData()
        {
            return XAxisData.Count != PlotSize ? XAxisData.GetRange(0, PlotSize) : XAxisData;
        }

        private List<List<double>> GetYPlotData()
        {
            if (YAxisData[0].Count == PlotSize)
            {
                return YAxisData;
            }
            YShallowAxisData.Clear();
            for (int i = 0; i < YAxisData.Count; i++)
            {
                YShallowAxisData.Add(YAxisData[i].GetRange(0, PlotSize));
            }
            return YShallowAxisData;
        }

        protected override void FillAxisData(int sampleSize)
        {
            // 如果图内点数未超过筛点个数，则使用移点实现
            if (SamplesInChart + sampleSize <= Constants.MaxPointsInSingleSeries)
            {
                return;
            }
            if (SparseRatio == 1)
            {
                Parallel.FillNoneFitPlotData(ViewStartIndex);
            }
            else
            {
                Parallel.FillRangeFitPlotData(ViewStartIndex);
            }
        }

        protected override void RefreshPlotParams(int sampleSize)
        {
            ViewStartIndex = AxisViewAdapter.ViewStartIndex;
            ViewEndIndex = AxisViewAdapter.ViewEndIndex;
            if (ViewStartIndex < 0 || ViewEndIndex - ViewStartIndex <  Constants.MinPoints)
            {
                ViewStartIndex = 0;
            }
            
            if (ViewEndIndex >=  XWrapBuf.Count || ViewEndIndex - ViewStartIndex < Constants.MinPoints)
            {
                ViewEndIndex = XWrapBuf.Count - 1;
            }
            if (SamplesInChart + sampleSize > Constants.MaxPointsInSingleSeries)
            {
                CalcSparseRatioAndPlotSize(ViewEndIndex - ViewStartIndex + 1);
            }
            else
            {
                SparseRatio = 1;
                PlotSize = SamplesInChart + sampleSize;
            }
        }
    }
}
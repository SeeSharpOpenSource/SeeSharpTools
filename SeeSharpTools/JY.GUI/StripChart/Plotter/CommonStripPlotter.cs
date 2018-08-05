using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.StripChartUtility;

namespace SeeSharpTools.JY.GUI
{
    internal class CommonStripPlotter : PlotAction
    {
        
        public CommonStripPlotter(StripPlotter plotter, AxisViewAdapter axisViewAdapter) : 
            base(plotter, axisViewAdapter)
        {
            XAxisData = null;
            YAxisData = null;
        }
        
        protected void DrawSinglePoint(string xData, double yData, int lineIndex)
        {
            PlotSeries[lineIndex].Points.AddXY(xData, yData);
            if (PlotSeries[lineIndex].Points.Count > Plotter.MaxSampleNum)
            {
                PlotSeries[lineIndex].Points.RemoveAt(0);
            }
        }

        protected override void RefreshSeriesData(int sampleSize)
        {
            // 如果是视图缩放导致的重绘则返回
            if (sampleSize == 0)
            {
                return;
            }
            
            base.RefreshPointXValue(sampleSize);
            // 如果单次写入点数较少，通过新建Point实现
            if (sampleSize <= Constants.MaxMovePointCount)
            {
                RefreshByPointOperation(sampleSize);
            }
            else
            {
                RefreshByDataBind(sampleSize);
            }
            RefreshSamplesInChart(sampleSize);
            RefreshPointXValue(sampleSize);
        }

        protected override void FillAxisData(int sampleSize)
        {
            // ignore 无需筛点时不需要使用AxisData
        }

        protected override void RefreshPlotParams(int sampleSize)
        {
            ViewStartIndex = 0;
            ViewEndIndex = XWrapBuf.Count;
            SparseRatio = 1;
            PlotSize = SamplesInChart + sampleSize;
            if (PlotSize > Plotter.MaxSampleNum)
            {
                PlotSize = Plotter.MaxSampleNum;
            }
        }
    }
}
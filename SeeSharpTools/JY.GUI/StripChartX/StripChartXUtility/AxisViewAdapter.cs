using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI.StripChartXUtility
{
    /// <summary>
    /// 根据样点数，执行坐标轴真实范围和外部看到的范围的适配与转换
    /// </summary>
    internal class AxisViewAdapter
    {
        private readonly PlotManager _plotManager;
        private readonly ChartViewManager _viewManager;

        public AxisViewAdapter(ChartViewManager viewManager, PlotManager plotManager)
        {
            this._viewManager = viewManager;
            this._plotManager = plotManager;
        }
        /// <summary>
        /// 坐标轴值转换为真实索引，如果超过边界值会被修正
        /// </summary>
        public int GetVerifiedIndex(double axisValue)
        {
            int samplesInChart = _plotManager.DataEntity.SamplesInChart;
            int realIndex = (int)Math.Floor(samplesInChart + axisValue);
            if (realIndex < 0)
            {
                realIndex = 0;
            }
            else if (realIndex >= samplesInChart)
            {
                realIndex = samplesInChart - 1;
            }
            return realIndex; ;
        }

        public int GetUnVerifiedIndex(double axisValue)
        {
            return (int)Math.Floor(_plotManager.DataEntity.SamplesInChart + axisValue);
        }

        public bool IsValidSampleIndex(int index)
        {
            return (index >= 0 && index < _plotManager.DataEntity.SamplesInChart);
        }

        /// <summary>
        /// 真实索引转换为坐标轴值
        /// </summary>
        public double GetAxisValue(int realValue)
        {
            return Math.Ceiling((double)(realValue - _plotManager.DataEntity.SamplesInChart));
        }

        //
        public void GetXAxisRange(out double xMin, out double xMax)
        {
            const int maxXValue = -1;
            int samplesInChart = _plotManager.DataEntity.SamplesInChart;
            switch (_viewManager.ScrollType)
            {
                case StripChartX.StripScrollType.Cumulation:
                    // 如果是累积模式，X轴的起始位置就是当前的位置，终止位置时当前位置加1。
                    xMin = -1*samplesInChart;
                    xMax = maxXValue;
                    break;
                case StripChartX.StripScrollType.Scroll:
                    // 如果是滚动模式，X轴的起始位置取决于当前点数是否达到DisplayPoints，终止位置为当前结束位置加1
                    xMin = -1*_plotManager.DisplayPoints;
                    xMax = maxXValue;
                    break;
                default:
                    xMin = -1 * _plotManager.DisplayPoints;
                    xMax = maxXValue;
                    break;
            }
            // 如果点数过少，默认显示两个点
            if (xMax - xMin < 2)
            {
                xMin = -3;
                xMax = maxXValue;
            }
        }

        public int GetXDataStartIndex()
        {
            return -1*_plotManager.DataEntity.SamplesInChart;
        }

        public int GetXDataEndIndex()
        {
            return -1;
        }
    }
}
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
//        private readonly StripChartXAxis _axisX;
//        private readonly StripChartXAxis _axisY;

        private readonly StripChartXCursor _cursorX;
        private readonly StripChartXCursor _cursorY;

        public double MaxYRange
        {
            get {return _axisY.Maximum; }
            set
            {
                if (!double.IsNaN(value))
                {
                    _axisY.Maximum = value;
                }
            }
        } 
        public double MinYRange
        {
            get { return _axisY.Minimum; }
            set
            {
                if (!double.IsNaN(value))
                {
                    _axisY.Minimum = value;
                }
            }
        }

        /// <summary>
        /// 坐标轴值转换为真实索引
        /// </summary>
        public int GetRealIndex(double axisValue)
        {
            return (int)Math.Floor(_samplesInChart + axisValue);
        }

        /// <summary>
        /// 真实索引转换为坐标轴值
        /// </summary>
        public double GetAxisValue(int realValue)
        {
            return Math.Ceiling((double) (realValue - _samplesInChart));
        }

        private int _samplesInChart;

        public void Clear()
        {
            this._samplesInChart = 0;
            this._axisX.Maximum = 1000;
            this._axisX.Minimum = 0;

            this._axisX.ScaleView.ZoomReset(int.MaxValue);
            this._axisX.MajorGrid.Interval = 200;
            this._axisX.Interval = 200;

            this._axisY.Maximum = 3.5;
            this._axisY.Minimum = 0;
            this._axisY.Interval = 0.5;
            this._axisY.ScaleView.ZoomReset(int.MaxValue);
            this._axisY.MajorGrid.Interval = 0.5;
            this._axisY.Interval = 0.5;

            this._axisX.CustomLabels.Clear();
        }
        
        public void RefreshXGrid()
        {
            RefreshGrid(_axisX, Constants.XLabelCount);
        }

        public void RefreshYGrid()
        {
            RefreshGrid(_axisY, Constants.YMajorGridCount);
        }

        
        public void InitAxisParams()
        {
            _samplesInChart = 0;
            _axisX.CustomLabels.Clear();
            for (int i = 0; i < Constants.XLabelCount + 1; i++)
            {
                _axisX.CustomLabels.Add(2*i, 2*i+1, " ");
            }
        }
    }
}
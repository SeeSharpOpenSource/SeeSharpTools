using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.EasyChartXUtility;
using SeeSharpTools.JY.GUI.Plotter;

namespace SeeSharpTools.JY.GUI.StripChartUtility
{
    internal class AxisViewAdapter
    {
        private readonly Axis _axisX;
        private readonly Axis _axisY;

        private readonly Cursor _cursorX;
        private readonly Cursor _cursorY;

        // TODO not good
        private StripPlotter _plotter;

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

        public int ViewStartIndex
        {
            get
            {
                double scaleViewStart = _axisX.ScaleView.ViewMinimum;
                if (double.IsNaN(scaleViewStart) || scaleViewStart < _axisX.Minimum)
                {
                    scaleViewStart = _axisX.Minimum;
                }
                int viewStartIndex = (int) Math.Floor(_samplesInChart + scaleViewStart);
                return viewStartIndex;
            }
        }

        public int ViewEndIndex
        {
            get
            {
                double scaleViewEnd = _axisX.ScaleView.ViewMaximum;
                if (double.IsNaN(scaleViewEnd) || scaleViewEnd > _axisX.Maximum)
                {
                    scaleViewEnd = _axisX.Maximum;
                }
                return (int)Math.Ceiling(_samplesInChart + scaleViewEnd);
            }
        }

        private int _samplesInChart;

        private readonly StripChart _parentChart;

        public AxisViewAdapter(StripChart parentChart, Chart baseChart)
        {
            this._axisX = baseChart.ChartAreas[0].AxisX;
            this._axisY = baseChart.ChartAreas[0].AxisY;

            this._cursorX = baseChart.ChartAreas[0].CursorX;
            this._cursorY = baseChart.ChartAreas[0].CursorY;

            _cursorX.Interval = 1;
            _cursorY.Interval = 1e-3;
            _axisY.ScaleView.SmallScrollSize = double.NaN;
            _axisY.ScaleView.SmallScrollMinSize = 1e-3;
            _axisY.LabelStyle.Format = "0.####";

            this._parentChart = parentChart;

            this._samplesInChart = 0;
        }

        public void SetPlotter(StripPlotter plotter)
        {
            this._plotter = plotter;
        }

        public void RefreshXAxisRange(int sampleCount)
        {
            // 如果点数已经到最大值，并且X轴的左右范围已经匹配则无需更新X轴范围
            if (_samplesInChart >= _parentChart.DisPlayPoints && Math.Abs(_axisX.Maximum) < Constants.MinDoubleValue && 
                Math.Abs(_axisX.Minimum + _samplesInChart) < Constants.MinDoubleValue)
            {
                RefreshXLabels();
                return;
            }
            _samplesInChart += sampleCount;
            if (_samplesInChart >= _parentChart.DisPlayPoints)
            {
                _samplesInChart = _parentChart.DisPlayPoints;
            }
            if (!_plotter.ScrollMode)
            {
                _axisX.Minimum = _samplesInChart > Constants.MinPoints ? -1 * _samplesInChart : -1 * Constants.MinPoints;
                _axisX.Maximum = 0;
                RefreshXGrid();
            }
            else
            {
                _axisX.Minimum = -1*_plotter.MaxSampleNum;
                _axisX.Maximum = 0;
                RefreshXGrid();
            }
            RefreshXLabels();
        }

        public void RefreshYAxisRange(double maxYValue, double minYValue)
        {
            if (!_parentChart.YAutoEnable || double.IsNaN(maxYValue) || double.IsNaN(minYValue))
            {
                return;
            }
            Utility.RoundYRange(ref maxYValue, ref minYValue);
            if (_axisY.Maximum < minYValue)
            {
                _axisY.Maximum = maxYValue;
                _axisY.Minimum = minYValue;
            }
            else
            {
                _axisY.Minimum = minYValue;
                _axisY.Maximum = maxYValue;
            }
            RefreshYGrid();
        }

        

        public void SetCursorFunction(bool xZoomable, bool yZoomable)
        {
            _cursorX.IsUserEnabled = xZoomable;
            _cursorX.IsUserSelectionEnabled = xZoomable;
            _cursorX.LineColor = xZoomable? Color.Red : Color.Transparent;

            _cursorY.IsUserEnabled = yZoomable;
            _cursorY.IsUserSelectionEnabled = yZoomable;
            _cursorY.LineColor = yZoomable ? Color.Red : Color.Transparent;

        }

        public void ZoomReset()
        {
            ResetGrid();
            _axisX.ScaleView.ZoomReset(int.MaxValue);
            _axisY.ScaleView.ZoomReset(int.MaxValue);
            RefreshXLabels();
//            RefreshXAxisRange(0);
//            RefreshXGrid();
//            RefreshYGrid();
        }

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

        public void RefreshXLabels()
        {
            if (double.IsNaN(_axisX.ScaleView.ViewMaximum) || double.IsNaN(_axisX.ScaleView.ViewMinimum) || 
                _axisX.ScaleView.ViewMaximum - _axisX.ScaleView.ViewMinimum < 1)
            {
                return;
            }
            double labelStep = (_axisX.ScaleView.ViewMaximum - _axisX.ScaleView.ViewMinimum)/Constants.XLabelCount;
            double labelRangeSize = labelStep/4;
            double labelPosition = _axisX.ScaleView.ViewMinimum;
            OverLapStrBuffer xLabels = _plotter.PlotAction.XWrapBuf;
            int pointIndex;
            for (int i = 0; i < Constants.XLabelCount; i++)
            {
                _axisX.CustomLabels[i].FromPosition = labelPosition - labelRangeSize;
                _axisX.CustomLabels[i].ToPosition = labelPosition + labelRangeSize;

                pointIndex = (int) Math.Round(labelPosition) + _samplesInChart;
                if (pointIndex >= 0 && pointIndex < xLabels.Count && !string.IsNullOrWhiteSpace(xLabels[pointIndex]))
                {
                    _axisX.CustomLabels[i].Text = xLabels[pointIndex];
                }
                else
                {
                    _axisX.CustomLabels[i].Text = " ";
                }
                labelPosition += labelStep;
            }

            // 最后一个做特殊处理
            labelPosition = 0;
            _axisX.CustomLabels[Constants.XLabelCount].FromPosition = labelPosition - labelRangeSize;
            _axisX.CustomLabels[Constants.XLabelCount].ToPosition = labelPosition + labelRangeSize;
            pointIndex = xLabels.Count - 1;
            if (pointIndex >= 0 && !string.IsNullOrWhiteSpace(xLabels[pointIndex]))
            {
                _axisX.CustomLabels[Constants.XLabelCount].Text = xLabels[pointIndex];
            }
            else
            {
                _axisX.CustomLabels[Constants.XLabelCount].Text = " ";
            }
        }

        public void RefreshXGrid()
        {
            RefreshGrid(_axisX, Constants.XLabelCount);
        }

        public void RefreshYGrid()
        {
            RefreshGrid(_axisY, Constants.YMajorGridCount);
        }

        private void RefreshGrid(Axis axis, int gridCount)
        {
            if (double.IsNaN(axis.ScaleView.ViewMaximum) || double.IsNaN(axis.ScaleView.ViewMinimum))
            {
                return;
            }
//            axis.MajorGrid.IntervalOffset = 0;
//            axis.MinorGrid.IntervalOffset = 0;
            double interval = (axis.ScaleView.ViewMaximum - axis.ScaleView.ViewMinimum) / gridCount;
            axis.MajorGrid.Interval = interval;
            axis.Interval = interval;
            axis.IntervalAutoMode = IntervalAutoMode.VariableCount;
            if (interval < 1)
            {
                int decimalCounts = (int) Math.Ceiling(Math.Log10(1 / interval));
                if (decimalCounts > Constants.MinDecimalOfScientificNotition)
                {
                    axis.LabelStyle.Format = "E2";
                }
                else
                {
                    axis.LabelStyle.Format = $"F{decimalCounts}";
                }
            }
            else
            {
                axis.LabelStyle.Format = "";
            }
        }
        // 每次缩放以前需要先将网格数配置为自动，防止大幅度缩放回退时固定Interval导致的黑屏问题
        private void ResetGrid()
        {
            _axisX.MajorGrid.Interval = (_axisX.Maximum - _axisX.Minimum) / Constants.XLabelCount;
            _axisX.Interval = (_axisX.Maximum - _axisX.Minimum) / Constants.XLabelCount;
            _axisY.MajorGrid.Interval = (_axisY.Maximum - _axisY.Minimum) / Constants.YMajorGridCount; ;
            _axisY.Interval = (_axisY.Maximum - _axisY.Minimum) / Constants.YMajorGridCount;
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
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.EasyChartXUtility;

namespace SeeSharpTools.JY.GUI
{
    internal class AxisViewAdapter
    {
        private readonly Axis _axisX;
        private readonly Axis _axisY;

        private readonly Cursor _cursorX;
        private readonly Cursor _cursorY;

        private bool _xZoomable;
//        private bool _yZoomable;

        public bool XZoomed { get; private set; }
//        private bool _yZoomed;

        private int _scaleSparseRatio = 0;

        // 缩放视图第一个点的真实索引
        private int _dataViewStart;
        // 缩放视图内包含的真实点数
        private int _dataViewPoints;

        // 缩放视图在缩放后第一个点在所有图中点里的索引
        private int _scaleViewStart;
        // 缩放视图中缩放后绘制的点数
        private int _scaleViewPoints;
//        private int _samplesBeforeScaleStart;

        // X轴缩放后的最小值
        private double _sparseAxisMin;
        // X轴缩放后的最大值
        private double _sparseAxisMax;

        const double PlotStartIndex = 1;
        const double XAxisIncreaseStep = 1;

        private readonly StripChart _parentChart;

        public AxisViewAdapter(StripChart parentChart, Chart baseChart)
        {
            this._axisX = baseChart.ChartAreas[0].AxisX;
            this._axisY = baseChart.ChartAreas[0].AxisY;

            this._cursorX = baseChart.ChartAreas[0].CursorX;
            this._cursorY = baseChart.ChartAreas[0].CursorY;

            _cursorX.Interval = 1;
            _cursorY.Interval = 1e-3;

            this._parentChart = parentChart;

            ResetAxisAdaptParas();
        }

        private void ResetAxisAdaptParas()
        {
            //            _scaleStartIndex = 0;
            this.XZoomed = false;
        }

        public void RefreshCursor(bool xZoomable, bool yZoomable)
        {
            _cursorX.IsUserEnabled = xZoomable;
            _cursorX.IsUserSelectionEnabled = xZoomable;
            _cursorX.LineColor = xZoomable? Color.Red : Color.Transparent;

            _cursorY.IsUserEnabled = yZoomable;
            _cursorY.IsUserSelectionEnabled = yZoomable;
            _cursorY.LineColor = yZoomable ? Color.Red : Color.Transparent;

            this._xZoomable = xZoomable;
        }

        public void ZoomReset()
        {
            _axisX.ScaleView.ZoomReset(int.MaxValue);
            _axisY.ScaleView.ZoomReset(int.MaxValue);
            _axisX.Maximum = double.NaN;
            _axisX.Minimum = double.NaN;

            XZoomed = false;
            _dataViewStart = 0;
            _dataViewPoints = 0;
            _scaleSparseRatio = 0;
        }

        public void InitScaleViewParam(int displayPoints, int currentScaleView, int totalPointsInChart)
        {
            double scaleViewMin = _axisX.ScaleView.ViewMinimum;
            double scaleViewMax = _axisX.ScaleView.ViewMaximum;
            if (!_xZoomable || scaleViewMax >= _axisX.Maximum && scaleViewMin <= _axisX.Minimum)
            {
                ZoomReset();
                return;
            }

            // 如果第一次进行缩放则使用外部的sparseRatio初始化sparseRatio
            if (0 == _scaleSparseRatio)
            {
                _scaleSparseRatio = currentScaleView;
            }
            CalcScaleViewInfo(scaleViewMin, scaleViewMax, totalPointsInChart, displayPoints);
            CalcXAxisRange(totalPointsInChart, displayPoints);
            this.XZoomed = true;
        }

        private void CalcScaleViewInfo(double scaleViewMin, double scaleViewMax, int totalPointsInChart, int displayPoints)
        {
            if (displayPoints > StripPlotter.MaxSamplesInChart)
            {
                // 不指定X值时，数据点默认从PlotStartIndex = 1开始
                _dataViewStart += (int)((scaleViewMin - PlotStartIndex) * _scaleSparseRatio / XAxisIncreaseStep);
            }
            else
            {
                _dataViewStart = (int) ((scaleViewMin - PlotStartIndex)/XAxisIncreaseStep);
            }
            
            // 两个范围之间点的个数多一个
            _dataViewPoints = (int)((scaleViewMax - scaleViewMin) / XAxisIncreaseStep + 1) * _scaleSparseRatio;

            if (_dataViewStart < 0)
            {
                _dataViewStart = 0;
            }

            //如果超出当前点数则恢复上次的坐标轴值
            if (_dataViewStart >= totalPointsInChart - 2)
            {
                _dataViewStart -= (int)((scaleViewMin - PlotStartIndex) * _scaleSparseRatio / XAxisIncreaseStep);
            }

            if (_dataViewPoints > totalPointsInChart - _dataViewStart || _dataViewPoints <= 2)
            {
                _dataViewPoints = totalPointsInChart - _dataViewStart;
            }

            RefreshSparseRatio();
            _scaleViewPoints = (_dataViewPoints - 1) / _scaleSparseRatio + 1;
        }

        private void CalcXAxisRange(int totalPointsInChart, int displayPoints)
        {
            _scaleViewStart = (_dataViewStart - 1) / _scaleSparseRatio + 1;
            _sparseAxisMin = -1 * (_scaleViewStart * XAxisIncreaseStep) + PlotStartIndex;
            // 目前最大点数和起始点数之间有(totalPointsInChart - _dataViewStart)个点
            int scaleViewEnd = (totalPointsInChart - _dataViewStart - 1) / _scaleSparseRatio + 1;
            // TODO to confirm
            _sparseAxisMax = scaleViewEnd * XAxisIncreaseStep + PlotStartIndex;
            if (displayPoints > StripPlotter.MaxSamplesInChart)
            {
                _axisX.Minimum = _sparseAxisMin;
                _axisX.Maximum = _sparseAxisMax;
                _axisX.ScaleView.Zoom(PlotStartIndex, _scaleViewPoints + PlotStartIndex - 1);
            }
        }

        private void RefreshSparseRatio()
        {
            if (_scaleViewPoints > StripPlotter.MaxSamplesInChart)
            {
                while (_scaleViewPoints > StripPlotter.MaxSamplesInChart)
                {
                    _scaleSparseRatio *= 2;
                    _scaleViewPoints = (_dataViewPoints - 1) / _scaleSparseRatio + 1;
                }
            }
            else
            {
                while (_scaleViewPoints * 2 <= StripPlotter.MaxSamplesInChart && _scaleSparseRatio/2 >= 1)
                {
                    _scaleSparseRatio /= 2;
                    _scaleViewPoints = (_dataViewPoints - 1) / _scaleSparseRatio + 1;
                }
            }
        }

        public void RefreshNonSparseScaleView(int totalPointsInChart, int sampleCount, int displayPoints)
        {
            if (!XZoomed || totalPointsInChart + sampleCount > displayPoints)
            {
                return;
            }

            int totalPoints = totalPointsInChart + sampleCount;
            if (totalPoints >= displayPoints)
            {
                _dataViewStart += displayPoints - totalPointsInChart;
            }
            else
            {
                _dataViewStart += sampleCount;
            }

            _axisX.Maximum = double.NaN;
            _axisX.Minimum = double.NaN;

            _axisX.ScaleView.Zoom(_dataViewStart * XAxisIncreaseStep, 
                (_dataViewStart + _dataViewPoints - 1) * XAxisIncreaseStep);
        }

        public void RefreshSparseScaleView(int totalPointsInChart, int sampleCount, int displayPoints)
        {
            if (!XZoomed)
            {
                return;
            }

            int totalPoints = totalPointsInChart + sampleCount;
            if (totalPoints >= displayPoints)
            {
                _dataViewStart += displayPoints - totalPointsInChart;
                totalPoints = displayPoints;
            }
            else
            {
                _dataViewStart += sampleCount;
            }

            if (totalPoints <= StripPlotter.MaxSamplesInChart)
            {
                RefreshNonSparseDynamicView(sampleCount);
            }
            else
            {
                RefreshSparseDynamicView(totalPoints, displayPoints);
            }
        }

        private void RefreshNonSparseDynamicView(int sampleCount)
        {
            _sparseAxisMin = -1 * _dataViewStart + PlotStartIndex;
            _axisX.Minimum = _sparseAxisMin;
        }

        private void RefreshSparseDynamicView(int totalPoints, int displayPoints)
        {
            _scaleViewStart = (_dataViewStart - 1)/_scaleSparseRatio + 1;
            _sparseAxisMin = -1*(_scaleViewStart*XAxisIncreaseStep) + PlotStartIndex;
            // 目前最大点数和起始点数之间有(totalPointsInChart - _dataViewStart)个点
            int scaleViewEnd = (totalPoints - _dataViewStart - 1)/_scaleSparseRatio + 1;
            // TODO to confirm
            _sparseAxisMax = scaleViewEnd*XAxisIncreaseStep + PlotStartIndex;
            _axisX.Minimum = _sparseAxisMin;
            _axisX.Maximum = _sparseAxisMax;
        }

        public void GetSparsePlotInfo(out int startIndex, out int plotSize, out int sparseRatio)
        {
            startIndex = _dataViewStart;
            plotSize = _scaleViewPoints;
            sparseRatio = _scaleSparseRatio;
        }

        public void SetYAutoRange(double maxYValue, double minYValue)
        {
            if (!_parentChart.YAutoEnable)
            {
                return;
            }

            if (double.IsNaN(maxYValue) || double.IsNaN(minYValue))
            {
                _axisY.Maximum = double.NaN;
                _axisY.Minimum = double.NaN;
                return;
            }

            double range = (maxYValue - minYValue);
            double expandRange = range * Constants.YAutoExpandRatio;

            if (range <= Constants.MinDoubleValue && maxYValue < Constants.MinDoubleValue)
            {
                maxYValue = 1;
                minYValue = -1;
            }
            else if (range <= Constants.MinDoubleValue)
            {
                maxYValue = maxYValue + Math.Abs(maxYValue);
                minYValue = minYValue - Math.Abs(minYValue);
            }
            else
            {
                // 如果最大值在0和-1 * expandRange之间，配置最大值为0
                if (maxYValue <= Constants.MinDoubleValue && maxYValue >= -1 * expandRange)
                {
                    maxYValue = 0;
                    minYValue -= expandRange;
                }
                // 如果最小值在1 * expandRange和-2 * expandRange之间，配置最小值为0
                else if (minYValue <= expandRange && minYValue >= -1 * Constants.MinDoubleValue)
                {
                    maxYValue += expandRange;
                    minYValue = 0;
                }
                else
                {
                    maxYValue += expandRange;
                    minYValue -= expandRange;
                }
                RoundBounds(range, ref maxYValue, ref minYValue);
            }
            
            _axisY.Maximum = maxYValue;
            _axisY.Minimum = minYValue;
        }

        private void RoundBounds(double range, ref double maxValue, ref double minValue)
        {
            double roundSegment = Math.Pow(10, Math.Floor(Math.Log10(range / 2)));
            maxValue = Math.Ceiling(maxValue / roundSegment) * roundSegment;
            minValue = Math.Floor(minValue / roundSegment) * roundSegment;
        }
    }
}
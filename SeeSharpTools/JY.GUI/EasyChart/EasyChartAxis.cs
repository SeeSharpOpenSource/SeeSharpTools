using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// EasyChart axis class
    /// </summary>
    public class EasyChartAxis
    {
        private EasyChart _baseEasyChart;
        private Axis _baseAxis;

        private double _maxData = double.NaN;
        private double _minData = double.NaN;
//        private bool _isUserChangedView = false;

        #region Constrctor

        /// <summary>
        /// Constructor for design
        /// </summary>
        public EasyChartAxis()
        {
            this._baseEasyChart = null;
            this._baseAxis = null;
            this.Maximum = double.NaN;
            this.Minimum = double.NaN;
            this.ViewMaximum = double.NaN;
            this.ViewMinimum = double.NaN;
            this.InitWithScaleView = false;
        }

        internal void Initialize(EasyChart baseEasyChart, Axis baseAxis)
        {
            this.Name = baseAxis.Name;
            this._baseEasyChart = baseEasyChart;
            this._baseAxis = baseAxis;
            this.Maximum = _specifiedMax;
            this.Minimum = _specifiedMin;
            this.ViewMaximum = _viewMax;
            this.ViewMinimum = _viewMin;
        }

        #endregion

        #region Public property

        /// <summary>
        /// Axis name
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Get the name of axis.")
        ]
        public string Name { get; private set; }

        private bool _autoScale = true;

        

        /// <summary>
        /// Specify whether auto scale enabled
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Set or get whether auto scale enabled.")
        ]
        public bool AutoScale
        {
            get { return _autoScale; }
            set
            {
                if (_autoScale == value)
                {
                    return;
                }
                _autoScale = value;
                RefreshAxisRange();
                _baseEasyChart.OnAxisViewChanged(this, false, false);
            }
        }

        private double _specifiedMax = int.MaxValue;

        /// <summary>
        /// Get or set the maximum value of axis
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Set or get the axis maximum value.")
        ]
        public double Maximum
        {
            get
            {
                return _baseAxis?.Maximum ?? _specifiedMax;
            }
            set
            {
                if (_specifiedMax <= _specifiedMin)
                {
                    return;
                }
                _specifiedMax = value;
                if (null != _baseAxis)
                {
                    RefreshAxisRange();
                    _baseEasyChart.OnAxisViewChanged(this, false, false);
                }
                
            }
        }

        /// <summary>
        /// Get or set the minimum value of axis
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Set or get the axis minimum value.")
        ]
        public double Minimum
        {
            get { return _baseAxis?.Minimum ?? _specifiedMin; }
            set
            {
                if (_specifiedMax <= _specifiedMin)
                {
                    return;
                }
                _specifiedMin = value;
                if (null != _baseAxis)
                {
                    RefreshAxisRange();
                    _baseEasyChart.OnAxisViewChanged(this, false, false);
                }
            }
        }

        private double _specifiedMin = int.MinValue;

        /// <summary>
        /// Get or set the minimum value of axis
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Indicate whether the axis is zoomed.")
        ]
        public bool IsZoomed
        {
            get { return _baseAxis.ScaleView.IsZoomed; }
        }

        private double _viewMax = double.NaN;

        /// <summary>
        /// Get or set the maximum value of scale view of axis
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Specify or get whether axis start with scale view")
        ]
        public bool InitWithScaleView { get; set; }

        /// <summary>
        /// Get or set the maximum value of scale view of axis
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Set or get the axis maximum scale view value.")
        ]
        public double ViewMaximum
        {
            get { return _baseAxis?.ScaleView.ViewMaximum ?? _viewMax; }
            set
            {
                _viewMax = value;
                if (null != _baseAxis)
                {
                    ResetAxisScaleView();
                    _baseEasyChart.OnAxisViewChanged(this, true, false);
                }

            }
        }

        private double _viewMin = double.NaN;

        /// <summary>
        /// Get or set the minimum value of scale view of axis
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Set or get the axis minimum scale view value.")
        ]
        public double ViewMinimum
        {
            get { return _baseAxis?.ScaleView.ViewMinimum ?? _viewMin; }
            set
            {
                _viewMin = value;
                if (null != _baseAxis)
                {
                    ResetAxisScaleView();
                    _baseEasyChart.OnAxisViewChanged(this, true, false);
                }
            }
        }

        // TODO to fix later，暂时封闭该接口
        /// <summary>
        /// Get or set the maximum value of axis
        /// </summary>
//        [
//            Browsable(true),
//            CategoryAttribute("Design"),
//            Description("Set or get the axis maximum value.")
//        ]
//        public bool IsLogarithmic
//        {
//            get { return _baseAxis.IsLogarithmic; }
//            set
//            {
//                if (_baseEasyChart.IsPlotting())
//                {
//                    _baseAxis.IsLogarithmic = value;
//                }
//            }
//        }

        /// <summary>
        /// Axis title 
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Set or get the axis title.")
        ]
        public string Title
        {
            get { return _baseAxis.Title; }
            set { _baseAxis.Title = value; }
        }

        /// <summary>
        /// Axis title orientation
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Set or get the axis title orientation.")
        ]
        public EasyChart.TitleOrientation Orientation
        {
            get { return (EasyChart.TitleOrientation) _baseAxis.TextOrientation; }
            set { _baseAxis.TextOrientation = (TextOrientation) value; }
        }

        /// <summary>
        /// Axis title position
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Set or get the axis title position.")
        ]
        public EasyChart.TitlePosition Position
        {
            get { return (EasyChart.TitlePosition) _baseAxis.TitleAlignment; }
            set { _baseAxis.TitleAlignment = (StringAlignment) value; }
        }

        /// <summary>
        /// Axis title position
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Design"),
            Description("Specify or get if the label is enabled.")
        ]
        public bool LabelEnabled
        {
            get { return _baseAxis.LabelStyle.Enabled; }
            set { _baseAxis.LabelStyle.Enabled = value; }
        }

        /// <summary>
        /// Axis title position
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Specify or get if the label format.")
        ]
        public string LabelFormat
        {
            get { return _baseAxis.LabelStyle.Format; }
            set { _baseAxis.LabelStyle.Format = value; }
        }

        #endregion

        #region Public method

        public void ZoomReset(int resetTimes = int.MaxValue)
        {
            _baseAxis.ScaleView.ZoomReset(resetTimes);
        }

        public bool Zoom(double start, double end)
        {
            if (end <= start || start > Maximum || end < Minimum)
            {
                return false;
            }
            _baseAxis.ScaleView.Zoom(start, end);
            return true;
        }

        #endregion

        #region Internal and private methods

        // Mannually set axis range for auto scale mode
        internal void SetAxisRangeValue(double max, double min)
        {
            if (double.IsNaN(max) || double.IsNaN(min))
            {
                this._maxData = double.NaN;
                this._minData = double.NaN;
            }
            else
            {
                this._maxData = max;
                this._minData = min;
            }
        }

        internal void RefreshAxisRange()
        {
            if (_autoScale || _specifiedMax <= _specifiedMin || (_autoScale && !_baseEasyChart.IsPlotting()))
            {
                SetAxisRange(_maxData, _minData);
            }
            else
            {
                SetAxisRange(_specifiedMax, _specifiedMin);
            }
        }

        private void SetAxisRange(double max, double min)
        {
            if (max > _baseAxis.Minimum || double.IsNaN(_baseAxis.Minimum))
            {
                _baseAxis.Maximum = max;
                _baseAxis.Minimum = min;
            }
            else
            {
                _baseAxis.Minimum = min;
                _baseAxis.Maximum = max;
            }
        }

        internal void ResetAxisScaleView(bool isAxisZoomed = true)
        {
            if (!_baseEasyChart.IsPlotting())
            {
                return;
            }
            if ((!isAxisZoomed && !InitWithScaleView && !IsZoomed) || 
                (isAxisZoomed && IsZoomed))
            {
                return;
            }
            if (!isAxisZoomed || !InitWithScaleView)
            {
                ZoomReset();
            }
            else
            {
                _baseAxis.ScaleView.Zoom(_viewMin, _viewMax);
                _baseEasyChart.RefreshPlotDatas(this);
            }
        }

//        private bool IsConfiguredNoZoom()
//        {
//            const double minimumPrecision = 1E-40;
//            return (double.IsNaN(_viewMax) && double.IsNaN(_viewMin)) || _viewMax > Maximum || _viewMin < Minimum || 
//                (Maximum - _viewMax < minimumPrecision && _viewMin - Minimum < minimumPrecision);
//        }

        #endregion
    }
}
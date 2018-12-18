using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.EasyChartXUtility;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// EasyChartX axis class
    /// </summary>
    public class EasyChartXAxis
    {
        private EasyChartX _parentChart;
        private EasyChartXPlotArea _parentPlotArea;
        private Axis _baseAxis;

        private double _maxData;
        private double _minData;

        /// <summary>
        /// 当前坐标轴是否为对数坐标，且处于缩放状态。该状态为true时坐标轴范围使用对数值，false时使用真实值
        /// </summary>
        internal bool IsLogScaleView { get; set; }

        #region Constructor

        /// <summary>
        /// Constructor for design
        /// </summary>
        public EasyChartXAxis()
        {
            this._parentChart = null;
            this._baseAxis = null;
            this._autoScale = true;
            this.AutoZoomReset = false;
            this.InitWithScaleView = false;
            this.IsLogScaleView = false;
        }

        internal void Initialize(EasyChartX baseEasyChart, EasyChartXPlotArea basePlotArea, Axis baseAxis)
        {
            this.Name = baseAxis.Name;
            this._parentChart = baseEasyChart;
            this._parentPlotArea = basePlotArea;
            this._baseAxis = baseAxis;
            
            this._viewMax = baseAxis.ScaleView.ViewMaximum;
            this._viewMin = baseAxis.ScaleView.ViewMinimum;

            if (IsXAxis())
            {
                this._maxData = Constants.DefaultXMax;
                this._minData = IsLogarithmic ? Constants.DefaultMinLogarithmic : Constants.DefaultXMin;
                this._specifiedMax = _maxData;
                this._specifiedMin = _minData;
                this.ViewMaximum = Constants.DefaultXMax;
                this.ViewMinimum = _minData;
                this._majorGridCount = -1;
            }
            else
            {
                this._maxData = Constants.DefaultYMax;
                this._minData = IsLogarithmic ? Constants.DefaultMinLogarithmic :Constants.DefaultYMin;
                this._specifiedMax = _maxData;
                this._specifiedMin = _minData;
                this.ViewMaximum = _maxData;
                this.ViewMinimum = _minData;
                this._majorGridCount = Constants.DefaultYMajorGridCount;
            }
            RefreshAxisRange();
            if (IsYAxis())
            {
                RefreshYMajorGridInterval();
            }
            SetAxisLabelStyle();
            // 设置主网格默认为虚线
            this.MajorGridType = GridStyle.Dash;
            this.MinorGridType = GridStyle.DashDot;
        }

        // TODO 为了避免初始化时范围为double.nan的问题，后续优化
        internal void InitAxisViewRange(double max, double min, double viewMax, double viewMin)
        {
            this._specifiedMax = max;
            this._specifiedMin = min;
            this._viewMax = viewMax;
            this._viewMin = viewMin;
            // 为了避免在未绘图时重新绘图因为Interval==0导致的异常
            if (IsXAxis())
            {
//                _baseAxis.Interval = Constants.ClearXInterval;
            }
            else if (IsYAxis())
            {
                _baseAxis.Interval = Constants.ClearYInterval;
            }
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
        
        private bool _autoScale;
        /// <summary>
        /// Specify whether auto scale enabled
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get whether auto scale enabled.")
        ]
        public bool AutoScale
        {
            get
            {
                bool isMaxAndMinValid = (!double.IsNaN(_specifiedMax) && !double.IsNaN(_specifiedMin) && 
                    (_specifiedMax - _specifiedMin > Constants.MinLegalInterval));
                return _autoScale || !isMaxAndMinValid;
            }
            set
            {
                if (_autoScale == value)
                {
                    return;
                }
                _autoScale = value;
                if (!_parentChart.IsPlotting())
                {
                    return;
                }
                _parentChart.RefreshAxesRange(_parentPlotArea);
                _parentChart.RefreshScaleViewAndSendEvent(_parentPlotArea.ChartArea, _baseAxis, false);
            }
        }

        private double _specifiedMax;

        /// <summary>
        /// Get or set the maximum value of axis
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get the axis maximum value.")
        ]
        public double Maximum
        {
            get { return _parentChart.IsPlotting() ? _baseAxis.Maximum : _specifiedMax; }
            set
            {
                if (double.IsNaN(value) || (IsLogarithmic && value <= 0))
                {
                    return;
                }
                _specifiedMax = value;
                if (value - _specifiedMin < Constants.MinLegalInterval || null == _baseAxis)
                {
                    return;
                }
                RefreshUserConfigAxisRange();
            }
        }

        /// <summary>
        /// Get or set the minimum value of axis
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get the axis minimum value.")
        ]
        public double Minimum
        {
            get { return _parentChart.IsPlotting() ? _baseAxis.Minimum : _specifiedMin; }
            set
            {
                if (double.IsNaN(value) || (IsLogarithmic && value <= 0))
                {
                    return;
                }
                _specifiedMin = value;
                if (_specifiedMax - value < Constants.MinLegalInterval || null == _baseAxis)
                {
                    return;
                }
                RefreshUserConfigAxisRange();
            }
        }

        private void RefreshUserConfigAxisRange()
        {
            // 如果正在绘图，则根据用户的配置和当前的数据更新坐标轴范围
            if (_parentChart.IsPlotting())
            {
                _parentChart.RefreshAxesRange(_parentPlotArea);
                _parentChart.RefreshScaleViewAndSendEvent(_parentPlotArea.ChartArea, this._baseAxis, false);
            }
            else
            {
                // 如果用户配置值非法，则自动计算
                if (_specifiedMax <= _specifiedMin)
                {
                    SetAxisRange(_maxData, _minData);
                }
                else
                {
                    SetAxisRange(_specifiedMax, _specifiedMin);
                }
                if (IsYAxis())
                {
                    RefreshYMajorGridInterval();
                }
            }
        }

        private double _specifiedMin;

        /// <summary>
        /// Get or set the minimum value of axis
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Indicate whether the axis is zoomed.")
        ]
        public bool IsZoomed
        {
            get { return _baseAxis.ScaleView.IsZoomed; }
        }

        private double _viewMax = double.NaN;

        /// <summary>
        /// Specify whether auto scale enabled
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Specify or get whether zoom reset when plot new data.")
        ]
        public bool AutoZoomReset { get; set; }

        /// <summary>
        /// Specify whether auto scale enabled
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Specify or get whether enable scale view when plot.")
        ]
        public bool InitWithScaleView { get; set; }

        /// <summary>
        /// Get or set the maximum value of scale view of axis
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get the axis maximum scale view value.")
        ]
        public double ViewMaximum
        {
            get { return _parentChart.IsPlotting() ? _baseAxis.ScaleView.ViewMaximum : _viewMax; }
            set
            {
                if (_viewMin >= value || double.IsNaN(value))
                {
                    return;
                }
                _viewMax = value;
                if (null != _baseAxis && _parentChart.IsPlotting())
                {
                    ResetAxisScaleView();
                    _parentChart.OnAxisViewChanged(this, true, false);
                }
            }
        }

        private double _viewMin = double.NaN;

        /// <summary>
        /// Get or set the minimum value of scale view of axis
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get the axis minimum scale view value.")
        ]
        public double ViewMinimum
        {
            get { return _parentChart.IsPlotting() ? _baseAxis.ScaleView.ViewMinimum : _viewMin; }
            set
            {
                if (_viewMax <= value || double.IsNaN(value))
                {
                    return;
                }
                _viewMin = value;
                if (null != _baseAxis && _parentChart.IsPlotting())
                {
                    ResetAxisScaleView();
                    _parentChart.OnAxisViewChanged(this, true, false);
                }
            }
        }

        /// <summary>
        /// Get or set the maximum value of axis
        /// </summary>
        [
            // TODO 暂时封闭IsLogarithmic配置
            Browsable(false),
            Category("Design"),
            Description("Set or get the axis maximum value."),
            // TODO 暂时封闭IsLogarithmic的代码支持
            EditorBrowsable(EditorBrowsableState.Never)
        ]
        public bool IsLogarithmic
        {
            get { return _baseAxis?.IsLogarithmic ?? false; }
            set
            {
                // TODO 暂时修改该接口，只支持Y轴的对数显示
                if (null == _baseAxis || value == _baseAxis.IsLogarithmic)
                {
                    return;
                }
                if (!_parentChart.IsPlotting() && Minimum <= 0)
                {
                    if (Maximum <= Constants.DefaultMinLogarithmic)
                    {
                        Maximum = IsXAxis() ? Constants.DefaultXMax : Constants.DefaultYMax;
                    }
                    Minimum = Constants.DefaultMinLogarithmic;
                }
                if (value && !_parentChart.ExistLogAxis())
                {
                    _parentChart.RefreshLogAxisChangingEvents(true);
                }
                else if (!value && _parentChart.ExistLogAxis())
                {
                    _parentChart.RefreshLogAxisChangingEvents(false);
                }
                _baseAxis.IsLogarithmic = value;
            }
        }

        /// <summary>
        /// Axis color
        /// 是否使能Major Grid
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Specify the color of axis.")
        ]

        public Color Color
        {
            get { return _baseAxis.LineColor; }
            set
            {
                _baseAxis.LineColor = value;
            }
        }

        /// <summary>
        /// Axis title 
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
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
            Category("Design"),
            Description("Set or get the axis title orientation.")
        ]
        public AxisTextOrientation TitleOrientation
        {
            get { return (AxisTextOrientation)_baseAxis.TextOrientation; }
            set { _baseAxis.TextOrientation = (TextOrientation)value; }
        }

        /// <summary>
        /// Axis title position
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get the axis title position.")
        ]
        public AxisTextPosition TitlePosition
        {
            get { return (AxisTextPosition)_baseAxis.TitleAlignment; }
            set { _baseAxis.TitleAlignment = (StringAlignment)value; }
        }

        /// <summary>
        /// Axis title position
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Specify or get if the label is enabled.")
        ]
        public bool LabelEnabled
        {
            get { return _baseAxis.LabelStyle.Enabled; }
            set { _baseAxis.LabelStyle.Enabled = value; }
        }

        private string _labelFormat;
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
            get { return _labelFormat; }
            set
            {
                _labelFormat = value;
                _baseAxis.LabelStyle.Format = value;
            }
        }

        /// <summary>
        /// Axis label angle
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Specify or get the axis label angle.")
        ]
        public int LabelAngle
        {
            get { return _baseAxis.LabelStyle.Angle; }
            set { _baseAxis.LabelStyle.Angle = value; }
        }

        /// <summary>
        /// Is major grid enable
        /// 是否使能Major Grid
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Indicate whether minor grid lines are shown.")
        ]

        public bool MajorGridEnabled
        {
            get { return _baseAxis.MajorGrid.Enabled; }
            set
            {
                _baseAxis.MajorGrid.Enabled = value;
            }
        }

        /// <summary>
        /// major grid color
        /// 是否使能Major Grid
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Specify the color of major grids.")
        ]

        public Color MajorGridColor
        {
            get { return _baseAxis.MajorGrid.LineColor; }
            set
            {
                _baseAxis.MajorGrid.LineColor = value;
            }
        }

        /// <summary>
        /// minor grid line type
        /// MinorGrid线条类型
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Specify the line type of major grids.")
        ]
        public GridStyle MajorGridType
        {
            get { return (GridStyle)_baseAxis.MajorGrid.LineDashStyle; }
            set
            {
                _baseAxis.MajorGrid.LineDashStyle = (ChartDashStyle)value;
            }
        }

        /// <summary>
        /// Is minor grid enable
        /// 是否使能MinorGrid
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Indicate whether minor grid lines are shown.")
        ]

        public bool MinorGridEnabled
        {
            get { return _baseAxis.MinorGrid.Enabled; }
            set
            {
                _baseAxis.MinorGrid.Enabled = value;
            }
        }

        /// <summary>
        /// minor grid color
        /// 是否使能MinorGrid
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Specify the color of minor grids.")
        ]

        public Color MinorGridColor
        {
            get { return _baseAxis.MinorGrid.LineColor; }
            set
            {
                _baseAxis.MinorGrid.LineColor = value;
            }
        }

        /// <summary>
        /// minor grid line type
        /// MinorGrid线条类型
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Specify the color of minor grids.")
        ]
        public GridStyle MinorGridType
        {
            get { return (GridStyle)_baseAxis.MinorGrid.LineDashStyle; }
            set
            {
                _baseAxis.MinorGrid.LineDashStyle = (ChartDashStyle)value;
            }
        }

        /// <summary>
        /// The percentage of Major grid tick line width to the chart width or height
        /// 主网格在坐标轴另一侧的长度占整个图表的长或宽的百分比
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Specify the color of minor grids.")
        ]
        public float TickWidth
        {
            get { return _baseAxis?.MajorTickMark.Size ?? 0; }
            set
            {
                if (_baseAxis?.MajorTickMark != null)
                {
                    _baseAxis.MajorTickMark.Size = value;
                }
            }
        }

        private int _majorGridCount;
        public int MajorGridCount
        {
            get { return _majorGridCount;}
            set
            {
                // 值不对或X轴，该值不生效
                if (value < 2 || value == _majorGridCount || IsXAxis(PlotAxis.Primary, PlotAxis.Secondary))
                {
                    return;
                }
                // 主Y轴使用用户配置，副Y轴使用主坐标轴的配置
                _majorGridCount = value;
                RefreshYMajorGridInterval();
                RefreshLabels();
            }
        }

        #endregion

        #region Public method

        /// <summary>
        /// Zoom reset definite steps
        /// </summary>
        /// <param name="resetTimes">Reset times. Default value: Cancel scale view.</param>
        public void ZoomReset(int resetTimes = int.MaxValue)
        {
            if (!IsZoomed)
            {
                return;
            }
            // 如果是Y轴取消缩放，则先更新Interval，避免Interval过小导致的Label重叠显示
            if (IsYAxis())
            {
                RefreshYMajorGridInterval(true);
            }
            if (IsLogarithmic)
            {
                this.IsLogScaleView = false;
                RefreshAxisRange();
            }
            _baseAxis.ScaleView.ZoomReset(resetTimes);
            if (_parentChart.IsPlotting())
            {
                _parentChart.RefreshScaleViewAndSendEvent(_parentPlotArea.ChartArea, _baseAxis, false);
            }
        }

        // 取消缩放视图而不触发事件
        internal void CancelScaleView()
        {
            if (_baseAxis.ScaleView.IsZoomed)
            {
                _baseAxis.ScaleView.ZoomReset(int.MaxValue);
            }
        }

        /// <summary>
        /// Zoom scale view to specified range.
        /// </summary>
        /// <param name="start">Scale view start</param>
        /// <param name="end">Scale view end</param>
        /// <returns></returns>
        public bool Zoom(double start, double end)
        {
            if (end <= start || start > Maximum || end < Minimum)
            {
                return false;
            }
            _baseAxis.ScaleView.Zoom(start, end);
            if (_parentChart.IsPlotting())
            {
                _parentChart.RefreshScaleViewAndSendEvent(_parentPlotArea.ChartArea, _baseAxis, false);
            }
            return true;
        }

        #endregion

        #region Internal and private methods

        // Mannually set axis range for auto scale mode
        /// <summary>
        /// 主坐标轴在自动范围时配置当前绘图的范围
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        internal void SetXAxisRangeValue(double max, double min)
        {
            if (double.IsNaN(max) || double.IsNaN(min))
            {
                this._maxData = Constants.DefaultXMax;
                this._minData = Constants.DefaultXMin;
            }
            else
            {
                this._maxData = max;
                this._minData = min;
            }
        }

        /// <summary>
        /// 主坐标轴在自动范围时配置当前绘图的范围
        /// </summary>
        /// <param name="maxYValue"></param>
        /// <param name="minYValue"></param>
        internal void SetYAxisRangeValue(double maxYValue, double minYValue)
        {
            if (!_autoScale)
            {
                return;
            }
            Utility.RoundYRange(ref maxYValue, ref minYValue, _majorGridCount, IsLogarithmic);
            _maxData = maxYValue;
            _minData = minYValue;
        }

        // 设置副坐标轴真实的范围
        internal void SetSlaveAxisSpecifiedRange(double maxValue, double minValue)
        {
            _specifiedMax = maxValue;
            _specifiedMin = minValue;
        }

        // 副坐标轴使用，用于修改配置最大最小值
        internal void SetSlaveAxisRange(double maxValue, double minValue)
        {
            SetAxisRange(maxValue, minValue);
            CancelScaleView();
        }

        // 副坐标轴使用，用于获取用户配置的最大最小值
        internal void GetSpecifiedRange(out double maxValue, out double minValue)
        {
            maxValue = this._specifiedMax;
            minValue = this._specifiedMin;
        }

        internal void RefreshAxisRange()
        {
            if (_autoScale || _specifiedMax <= _specifiedMin)
            {
                SetAxisRange(_maxData, _minData);
                // 更新_specifiedMax和_specifiedMin，如果取消AutoScale即可使用当前的最大最小值
                _specifiedMax = _maxData;
                _specifiedMin = _minData;
            }
            else
            {
                SetAxisRange(_specifiedMax, _specifiedMin);
            }
        }

        private void SetAxisRange(double max, double min)
        {
            // 坐标轴为对数显示且处于缩放时，坐标轴的大小值需要使用对数计算
            if (IsLogScaleView)
            {
                max = Math.Log10(max);
                min = Math.Log10(min);
            }
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

        internal void Clear()
        {
            if (null == _baseAxis)
            {
                return;
            }
            CancelScaleView();
        }

        internal void ResetAxisScaleView()
        {
            if (!InitWithScaleView || IsConfiguredNoZoom())
            {
                CancelScaleView();
            }
            else
            {
                _baseAxis.ScaleView.Zoom(_viewMin, _viewMax);
                //                BaseEasyChart.RefreshPlotDatas(this);
            }
        }

        public void RefreshLabels()
        {
            _baseAxis.CustomLabels.Clear();
        }

        // TODO 新增属性需要在这里添加代码，后期可以考虑通过接口注解实现
        internal void ApplyConfig(EasyChartXAxis template)
        {
            this.Title = template.Title;
            this.TitleOrientation = template.TitleOrientation;
            this.TitlePosition = template.TitlePosition;

            this.AutoScale = template.AutoScale;
            this.AutoZoomReset = template.AutoZoomReset;
            this.InitWithScaleView = template.InitWithScaleView;

            this.IsLogarithmic = template.IsLogarithmic;
            this.LabelEnabled = template.LabelEnabled;
            this.LabelFormat = template.LabelFormat;
            this.LabelAngle = template.LabelAngle;

            this.MajorGridEnabled = template.MajorGridEnabled;
            this.MajorGridColor = template.MajorGridColor;
            this.MajorGridType = template.MajorGridType;

            this.MinorGridEnabled = template.MinorGridEnabled;
            this.MinorGridColor = template.MinorGridColor;
            this.MinorGridType = template.MinorGridType;

            this.TickWidth = template.TickWidth;

            // 需要判断赋值顺序
            double max, min;
            template.GetSpecifiedRange(out max, out min);
            this._specifiedMax = max;
            this._specifiedMin = min;
            this.RefreshAxisRange();

            if (IsYAxis())
            {
                RefreshYMajorGridInterval();
            }

            template.GetViewRange(out max, out min);
            this._viewMax = max;
            this._viewMin = min;

            this.Color = template.Color;

            this.TickWidth = template.TickWidth;
            this.MajorGridCount = template.MajorGridCount;
        }

        internal void GetViewRange(out double viewMax, out double viewMin)
        {
            viewMax = _viewMax;
            viewMin = _viewMin;
        }

        internal void SetAxisLabelStyle()
        {
            _baseAxis.LabelStyle.ForeColor = _parentChart.ForeColor;
            _baseAxis.LabelStyle.Font = _parentChart.Font;
            _baseAxis.TitleForeColor = _parentChart.ForeColor;
            //            _baseAxis.TitleFont = _baseEasyChart.Font;
        }

        private bool IsConfiguredNoZoom()
        {
            return (double.IsNaN(_viewMax) && double.IsNaN(_viewMin)) || _viewMax > Maximum || _viewMin < Minimum ||
                (Maximum - _viewMax < Constants.MinDoubleValue && _viewMin - Minimum < Constants.MinDoubleValue);
        }

        private void SetLabelFormat(double interval)
        {
            //已配置LabelFormat或最小Interval大于等于1则无需配置
            string format = "";
            if (!string.IsNullOrEmpty(_labelFormat))
            {
                format = _labelFormat;
            }
            else if (!IsLogarithmic)
            {
                double maxPointNum = Math.Ceiling(Math.Abs(Math.Log10(interval))) + 1;
                if (maxPointNum > Constants.MinDecimalOfScientificNotition)
                {
                    format = "E2";
                }
                else if (interval >= 10 || interval <= 0)
                {
                    format = "F0";
                }
                else
                {
                    StringBuilder labelFormat = new StringBuilder("0.");
                    for (int i = 0; i < maxPointNum; i++)
                    {
                        labelFormat.Append("#");
                    }
                    format = labelFormat.ToString();
                }
            }
            else
            {
                format = "E2";
            }
            if (!format.Equals(_baseAxis.LabelStyle.Format))
            {
                _baseAxis.LabelStyle.Format = format;
            }
        }

        public void RefreshXLabelFormat()
        {
            double viewMax = _baseAxis.ScaleView.ViewMaximum;
            double viewMin = _baseAxis.ScaleView.ViewMinimum;
            if (double.IsNaN(viewMax) || double.IsNaN(viewMin))
            {
                viewMax = _baseAxis.Maximum;
                viewMin = _baseAxis.Minimum;
            }
            if (!IsLogarithmic)
            {
                SetLabelFormat((viewMax - viewMin) / Constants.MaxXGridCount);
            }
            else
            {
                SetLabelFormat(viewMin);
            }
        }

        internal void RefreshYMajorGridInterval(bool resetOperation = false)
        {
            double viewMax = _baseAxis.ScaleView.ViewMaximum;
            double viewMin = _baseAxis.ScaleView.ViewMinimum;
            if (double.IsNaN(viewMax) || double.IsNaN(viewMin) || resetOperation)
            {
                viewMax = _baseAxis.Maximum;
                viewMin = _baseAxis.Minimum;
            }
            if (!IsLogarithmic)
            {
                double interval = (viewMax - viewMin) / _majorGridCount;
                //            _baseAxis.IntervalAutoMode = IntervalAutoMode.VariableCount;
                _baseAxis.Interval = interval;
                SetLabelFormat(interval);
            }
            else
            {
                _baseAxis.Interval = (Math.Log10(viewMax) - Math.Log10(viewMin)) / _majorGridCount;
                SetLabelFormat(viewMin);
            }
//            _baseAxis.CustomLabels.Clear();
        }

        private bool IsXAxis(params PlotAxis[] axis)
        {
            if (null == _baseAxis)
            {
                return false;
            }
            return IsAxis(Constants.PrimaryXAxisName, axis);
        }

        private bool IsYAxis(params PlotAxis[] axis)
        {
            if (null == _baseAxis)
            {
                return false;
            }
            return IsAxis(Constants.PrimaryYAxisName, axis);
        }

        private bool IsAxis(string primaryAxisName, params PlotAxis[] axis)
        {
            if (null == _baseAxis)
            {
                return false;
            }
            if (null == axis || 1 != axis.Length)
            {
                return _baseAxis.Name.Contains(primaryAxisName);
            }

            if (PlotAxis.Primary == axis[0])
            {
                return _baseAxis.Name.Equals(primaryAxisName);
            }
            else
            {
                return _baseAxis.Name.Contains(primaryAxisName) &&
                       !_baseAxis.Name.Equals(primaryAxisName);
            }
        }

        #endregion

        #region Enumeration Declaration

        /// <summary>
        /// Axis title display orientation
        /// 坐标轴名称方向
        /// </summary>
        public enum AxisTextOrientation
        {
            /// <summary>
            /// Auto
            /// </summary>
            Auto = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Auto,

            /// <summary>
            /// Horizental
            /// </summary>
            Horizontal = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal,

            /// <summary>
            /// Rotate 270 degrees
            /// </summary>
            Rotated270 = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270,

            /// <summary>
            /// Rotate 90 degrees
            /// </summary>
            Rotated90 = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated90,

            /// <summary>
            /// Stacked
            /// </summary>
            Stacked = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Stacked
        }

        /// <summary>
        /// Axis title align postion
        /// 坐标轴名称对齐位置
        /// </summary>
        public enum AxisTextPosition
        {
            /// <summary>
            /// Near the base point
            /// </summary>
            Near = StringAlignment.Near,

            /// <summary>
            /// In the midlle of Axis
            /// </summary>
            Center = StringAlignment.Center,

            /// <summary>
            /// In the opposite side of base point
            /// </summary>
            Far = StringAlignment.Far
        }

        /// <summary>
        /// Plot axis for series
        /// 线条显示的坐标轴
        /// </summary>
        public enum PlotAxis
        {
            /// <summary>
            /// Primary axis
            /// 主坐标轴
            /// </summary>
            Primary = AxisType.Primary,

            /// <summary>
            /// 副坐标轴
            /// </summary>
            Secondary = AxisType.Secondary
        }


        /// <summary>
        /// Grid line Style
        /// </summary>
        public enum GridStyle
        {
            /// <summary>
            /// Solid line
            /// </summary>
            Solid = ChartDashStyle.Solid,

            /// <summary>
            /// Dash line
            /// </summary>
            Dash = ChartDashStyle.Dash,

            /// <summary>
            /// Dash And Dot
            /// </summary>
            DashDot = ChartDashStyle.DashDot,

            /// <summary>
            /// Dash, Dot Dot And so on.
            /// </summary>
            DashDotDot = ChartDashStyle.DashDotDot
        }

        #endregion


    }
}
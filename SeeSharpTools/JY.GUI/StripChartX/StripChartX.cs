using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using SeeSharpTools.JY.GUI.Common.i18n;
using SeeSharpTools.JY.GUI.StripChartXData;
using SeeSharpTools.JY.GUI.StripChartXEditor;
using SeeSharpTools.JY.GUI.StripTabCursorUtility;
using SeeSharpTools.JY.GUI.StripChartXUtility;
using Control = System.Windows.Forms.Control;
using UserControl = System.Windows.Forms.UserControl;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// Chart to plot waveform(s).
    /// </summary>
    [Designer(typeof(StripChartXDesigner))]
    [ToolboxBitmap(typeof(StripChartX), "StripChartX.StripChartX.bmp")]
    [DefaultEvent("AxisViewChanged")]
    public partial class StripChartX : UserControl
    {
        #region Private Fields
        // 保存选择游标对齐到哪条线的menuItem的列表
        private readonly List<ToolStripMenuItem> _cursorSeriesMenuItems = new List<ToolStripMenuItem>(Constants.DefaultMaxSeriesCount);
        // 保存选择配置是否使能线条的menuItem的列表
        private readonly List<ToolStripMenuItem> _enableSeriesMenuItems = new List<ToolStripMenuItem>(Constants.DefaultMaxSeriesCount);
        // 图表视图管理类
        private readonly ChartViewManager _chartViewManager;
        // 绘制线条的管理类
        private readonly PlotManager _plotManager;
        // 当前选择的线条
        private StripChartXSeries _hitSeries;
        // 当前激活的绘图区
        private StripChartXPlotArea _hitPlotArea;
        // 游标管理窗体的实例
        private StripTabCursorInfoForm _tabCursorForm;
        // 国际化管理类
        private readonly I18nEntity i18n = I18nEntity.GetInstance("GUI");
        // 视图和数据适配类，不太好，没有找到更好的处理方式，暂时放在这
        internal readonly AxisViewAdapter ViewAdapter;

        #endregion   //Private Fields

        #region Public Properties

        /// <summary>
        /// Set the backColor of chartArea.
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Set the BackColor of ChartArea.")      
        ]
        public Color ChartAreaBackColor
        {
            get
            {
                return _chartViewManager.MainPlotArea.BackColor;
            }

            set
            {
                _chartViewManager.MainPlotArea.BackColor = value;
                if (_chartViewManager.UseMainAreaConfig)
                {
                    _chartViewManager.ApplyMainPlotAreaToAll();
                }
            }
        }

        /// <summary>
        /// Get or specify whether plot in split view.
        /// </summary>
        [
            Browsable(true),
            Category("Behavior"),
            Description("Get or specify whether plot in split view.")
        ]
        public bool SplitView
        {
            get
            {
                return _chartViewManager?.IsSplitView ?? false;
            }

            set
            {
                _chartViewManager.IsSplitView = value;
            }
        }
        
        /// <summary>
        /// Get plot areas in split view.
        /// </summary>
        [
            Browsable(false),
            Category("Behavior"),
            EditorBrowsable(EditorBrowsableState.Always),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Description("Get the split plot areas.")
        ]
        public StripChartXPlotAreaCollection SplitPlotArea => _chartViewManager.SplitPlotAreas;

        /// <summary>
        /// Set the  backColor of legend
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Set the BackColor of LegendBackColor.")
        ]
        public Color LegendBackColor
        {
            get
            {
                return _chart.Legends[0].BackColor;
            }

            set
            {
                _chart.Legends[0].BackColor = value;
            }
        }

        /// <summary>
        /// Get or set the font of Legend.
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Get or set the font of Legend.")
        ]
        public Font LegendFont
        {
            get
            {
                return _chart.Legends[0].Font;
            }

            set
            {
                _chart.Legends[0].Font = value;
            }
        }

        /// <summary>
        /// Get or set the fore color of Legend.
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Get or set the fore color of Legend.")
        ]
        public Color LegendForeColor
        {
            get
            {
                return _chart.Legends[0].ForeColor;
            }

            set
            {
                _chart.Legends[0].ForeColor = value;
            }
        }

        /// <summary>
        /// Specify or get whether legend visible.
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Specify whether legend is visible.")
        ]
        public bool LegendVisible
        {
            get { return _chart.Legends[0].Enabled; }
            set
            {
                _chart.Legends[0].Enabled = value;
            }
        }

        /// <summary>
        /// Set the type of gradient style
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Set the  style of BackGradientStyle.")
        ]
        public ChartGradientStyle GradientStyle
        {
            get
            {
                return (ChartGradientStyle)_chart.BackGradientStyle;
            }
            set
            {
                _chart.BackGradientStyle = (GradientStyle) value;
            }
        }

        /// <summary>
        /// Set the default series count or get the series count in current plot.
        /// </summary>
        [
            Browsable(true),
            Category("Data"),
            Description("Set the default series count.")
        ]
        public int SeriesCount
        {
            get
            {
                return _plotManager.SeriesCount;
            }

            set
            {
                if (value > 0 && !IsPlotting())
                {
                    _plotManager.SeriesCount = value;
                    _chartViewManager.AdaptView();
                    Clear();
                }
            }
        }

        /// <summary>
        /// X Axis
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            Description("Set or get the primary X axis attributes.")
        ]
        public StripChartXAxis AxisX
        {
            get { return _chartViewManager.MainPlotArea.AxisX; }
            set { _chartViewManager.MainPlotArea.AxisX = value; }
        }

        /// <summary>
        /// Y Axis
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            Description("Set or get the primary Y axis attributes."),
        ]
        public StripChartXAxis AxisY
        {
            get { return _chartViewManager.MainPlotArea.AxisY; }
            set { _chartViewManager.MainPlotArea.AxisY = value; }
        }

        /// <summary>
        /// X2 Axis
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            Description("Set or get the secondary X axis attributes.")
        ]
        public StripChartXAxis AxisX2
        {
            get { return _chartViewManager.MainPlotArea.AxisX2; }
            set { _chartViewManager.MainPlotArea.AxisX2 = value; }
        }

        /// <summary>
        /// Y2 Axis
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            Description("Set or get the secondary Y axis attributes."),
        ]
        public StripChartXAxis AxisY2
        {
            get { return _chartViewManager.MainPlotArea.AxisY2; }
            set { _chartViewManager.MainPlotArea.AxisY2 = value; }
        }

        private StripChartXAxis[] _axes = new StripChartXAxis[4];
        /// <summary>
        /// Get or set the axis attributes.
        /// </summary>
        [
            Browsable(true),
            Bindable(true),
            Editor(typeof (ArrayEditor), typeof (UITypeEditor)),
            Category("Design"),
            Description("Specify or get the attribute of X and Y Axis."),
            EditorBrowsable(EditorBrowsableState.Never),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        ]
        public StripChartXAxis[] Axes
        {
            get { return _axes; }
            set
            {
                AxisX = value[0];
                AxisY = value[1];
                AxisX2 = value[2];
                AxisY2 = value[3];
            }
        }

        /// <summary>
        /// X Cursor
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            Description("Set or get the X cursor attributes.")
        ]
        public StripChartXCursor XCursor
        {
            get { return _chartViewManager.MainPlotArea.XCursor; }
            set { _chartViewManager.MainPlotArea.XCursor = value; }
        }

        /// <summary>
        /// Y Cursor
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            Description("Set or get the Y cursor attributes.")
        ]
        public StripChartXCursor YCursor
        {
            get { return _chartViewManager.MainPlotArea.YCursor; }
            set { _chartViewManager.MainPlotArea.YCursor = value; }
        }

        private StripChartXCursor[] _cursors = new StripChartXCursor[2];
        /// <summary>
        /// Get or set the cursors attributes.
        /// </summary>
        [
            Browsable(true),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            Editor(typeof (ArrayEditor), typeof (UITypeEditor)),
            Category("Design"),
            Description("Specify or get the attribute of X and Y Axis."),
            EditorBrowsable(EditorBrowsableState.Never),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
        ]
        public StripChartXCursor[] Cursors
        {
            get { return _cursors; }
            set
            {
                XCursor = value[0]; 
                YCursor = value[1]; 
            }
        }

        /// <summary>
        /// Get or set the series attributes.
        /// </summary>
        [
            Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Editor(typeof (StripChartXLineSeriesEditor), typeof (UITypeEditor)),
            Category("Design"),
            Description("Specify or get the attribute of all series."),
            EditorBrowsable(EditorBrowsableState.Never)
        ]
        public StripChartXLineSeries LineSeries => _plotManager.LineSeries;

        /// <summary>
        /// Get or set the series attributes.
        /// </summary>
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Editor(typeof (StripChartXLineSeriesEditor), typeof (UITypeEditor)),
            Category("Design"),
            Description("Specify or get the attribute of all series."),
            EditorBrowsable(EditorBrowsableState.Always)
        ]
        public StripChartXSeriesCollection Series => _plotManager.Series;

        /// <summary>
        /// Tabcursor container. This property is just used for design time.
        /// </summary>
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Editor(typeof(StripTabCursorCollectionEditor), typeof(UITypeEditor)),
            Category("Data"),
            Description("Tabcursor container. This property is just used for design time."),
            EditorBrowsable(EditorBrowsableState.Never)
        ]
        public StripTabCursorDesignTimeCollection TabCursorContainer { get; }

        /// <summary>
        /// TabCursor collection of StripChartX.
        /// </summary>
        [
            Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Editor(typeof(StripTabCursorCollectionEditor), typeof(UITypeEditor)),
            Category("Data"),
            Description("TabCursor collection of StripChartX."),
            EditorBrowsable(EditorBrowsableState.Always)
        ]
        public StripTabCursorCollection TabCursors { get; }

        /// <summary>
        /// Split view layout configure
        /// </summary>
        [
            Browsable(true),
            Category("Misc"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Editor(typeof(PropertyClonableClassEditor), typeof(UITypeEditor)),
            Description("Split view layout configure."),
            EditorBrowsable(EditorBrowsableState.Always)
        ]
        public MiscellaneousConfiguration Miscellaneous { get; internal set; }

        /// <summary>
        /// Select stripchartX scroll type
        /// 选择StripChartX滚动类型
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Select the scroll type of stripchartX"),
        ]
        public StripScrollType ScrollType
        {
            get { return _chartViewManager.ScrollType; }
            set
            {
                if (IsPlotting())
                {
                    throw new InvalidOperationException(i18n.GetFStr("Runtime.NotSetInRunTime", "ScrollType"));
                }
                _chartViewManager.ScrollType = value;
            }
        }

        /// <summary>
        /// Maximum point count to show in single line. The points at the most front will be overlapped when the point count exceed this number.
        /// 单条线最多显示的点数，超过该点数后数据开始滚动并覆盖最前面的点。
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Maximum point count to show in single line. The points at the most front will be overlapped when the point count exceed this number.")
        ]
        public int DisplayPoints
        {
            get { return _plotManager.DisplayPoints; }
            set
            {
                if (value < Constants.MinDisplayPoints || value > Constants.MaxDisplayPoints)
                {
                    throw new ArgumentOutOfRangeException(i18n.GetFStr("ParamCheck.InvalidRange", "DisplayPoints", Constants.MinDisplayPoints, 
                        Constants.MaxDisplayPoints));
                }
                if (IsPlotting())
                {
                    throw new InvalidOperationException(i18n.GetFStr("Runtime.NotSetInRunTime", "DisplayPoints"));
                }
                _plotManager.DisplayPoints = value;
            }
        }

        /// <summary>
        /// Specify the x axis label type
        /// 配置X轴显示的类型
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Data"),
            Description("Specify X axis data type")
        ]
        public XAxisDataType XDataType
        {
            get { return _plotManager.XDataType; }
            set
            {
                if (IsPlotting())
                {
                    throw new InvalidOperationException(i18n.GetFStr("Runtime.NotSetInRunTime", "XDataType"));
                }
                _plotManager.XDataType = value;
            }
        }
        
        /// <summary>
        /// Time stamp format
        /// 时间戳格式
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Data"),
            DefaultValueAttribute(Constants.DefaultTimeStampFormat),
            Description("Specify the time stamp format. Only available when XAxisDataType is TimeStamp.")
        ]
        public string TimeStampFormat
        {
            get { return _plotManager.TimeStampFormat; }
            set { _plotManager.TimeStampFormat = value; }
        }

        /// <summary>
        /// Get or set the next time stamp value
        /// 获取或配置下一个绘图时的其实时间戳
        /// </summary>
        [
            Browsable(false),
            CategoryAttribute("Data"),
            Description("Get or set the next time stamp value.")
        ]
        public DateTime NextTimeStamp
        {
            get { return _plotManager.NextTimeStamp; }
            set { _plotManager.NextTimeStamp = value; }
        }

        /// <summary>
        /// Get or set the time interval between two samples
        /// 获取或配置相邻两个样点之间的时间间隔
        /// </summary>
        [
            Browsable(false),
            CategoryAttribute("Data"),
            Description("Get or set the time interval between two samples.")
        ]
        public TimeSpan TimeInterval
        {
            get { return _plotManager.TimeInterval; }
            set
            {
                if (IsPlotting())
                {
                    throw new InvalidOperationException(i18n.GetFStr("Runtime.NotSetInRunTime", "TimeInterval"));
                }
                _plotManager.TimeInterval = value;
            }
        }

        /// <summary>
        /// Start value of X axis index. Only available when XDataType is Index.
        /// X轴索引起始值，仅在XDataType为Index时可用。
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Data"),
            Description("Specify the start index of x axis. Only available when XDataType is Index.")
        ]
        public int StartIndex
        {
            get { return _plotManager.StartIndex; }
            set
            {
                if (IsPlotting())
                {
                    throw new InvalidOperationException(i18n.GetFStr("Runtime.NotSetInRunTime", "StartIndex"));
                }
                _plotManager.StartIndex = value; 
            }
        }

        /// <summary>
        /// Get or specify the scroll direction of StripChartX.
        /// 获取或配置StripChartX的滚动方向
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Get or specify the scroll direction of StripChartX.")
        ]
        public ScrollDirection Direction
        {
            get { return _chartViewManager.Direction; }
            set { _chartViewManager.Direction = value; }
        }

        #endregion

        #region User event handler and event call

        /// <summary>
        /// StripChartX axis view changing event delegate
        /// </summary>
        public delegate void ViewEvent(object sender, StripChartXViewEventArgs e);

        /// <summary>
        /// StripChartX cursor changed event delegate
        /// </summary>
        public delegate void CursorEvent(object sender, StripChartXCursorEventArgs e);

        /// <summary>
        /// StripChartX tabcursor changed event delegate
        /// </summary>
        public delegate void TabCursorEvent(object sender, StripTabCursorEventArgs e);

        /// <summary>
        /// StripChartX plot event delegate
        /// </summary>
        public delegate void PlotEvent(object sender, StripChartXPlotEventArgs e);

        /// <summary>
        /// Axis view changed event. Raised when scale view changed by mouse or user.
        /// </summary>
        [Description("Raised when StripChartX axis view changed.")]
        public event ViewEvent AxisViewChanged;

        internal void OnAxisViewChanged(StripChartXAxis axis, bool isScaleViewChanged, bool isRaiseByMouseEvent)
        {
            if (null == AxisViewChanged)
            {
                return;
            }
            StripChartXViewEventArgs eventArgs = new StripChartXViewEventArgs();
            eventArgs.Axis = axis;
            eventArgs.IsScaleViewChanged = isScaleViewChanged;
            eventArgs.IsRaisedByMouseEvent = isRaiseByMouseEvent;
            eventArgs.ParentChart = this;
            AxisViewChanged(this, eventArgs);
        }

        /// <summary>
        /// Cursor position changed event. Raised when cursor position changed by mouse or user.
        /// </summary>
        [Description("Raised when cursor position changed by mouse or user.")]
        public event CursorEvent CursorPositionChanged;

        internal void OnCursorPositionChanged(StripChartXCursor cursor, bool raiseByMouseEvent, int seriesIndex = -1)
        {
            if (null == CursorPositionChanged)
            {
                return;
            }
            StripChartXCursorEventArgs eventArgs = new StripChartXCursorEventArgs();
            eventArgs.Cursor = cursor;
            eventArgs.IsRaisedByMouseEvent = raiseByMouseEvent;
            eventArgs.ParentChart = this;
            eventArgs.ParentChartArea = _hitPlotArea;
            eventArgs.SeriesIndex = seriesIndex;
            CursorPositionChanged(cursor, eventArgs);
        }

        /// <summary>
        /// tab cursor operation event. Raised when tab cursor operation happened..
        /// </summary>
        [Description("Raised when tab cursor operation happened.")]
        public event TabCursorEvent TabCursorChanged;

        internal void OnTabCursorChanged(StripTabCursor cursor, StripTabCursorOperation operation, StripChartXPlotArea plotArea)
        {
            if (null == TabCursorChanged)
            {
                return;
            }
            StripTabCursorEventArgs eventArgs = new StripTabCursorEventArgs();
            eventArgs.Cursor = cursor;
            eventArgs.Operation = operation;
            eventArgs.ParentChartArea = plotArea;
            eventArgs.ParentChart = this;
            TabCursorChanged(this, eventArgs);
        }

        /// <summary>
        /// Event raised before plot data.
        /// </summary>
        [Description("Event raised before plot data.")]
        public event PlotEvent BeforePlot;

        internal void OnBeforePlot(bool isClearOperation)
        {
            BeforePlot?.Invoke(this, new StripChartXPlotEventArgs() {ParentChart = this, IsClear = isClearOperation});
        }

        /// <summary>
        /// Event raised after plot data.
        /// </summary>
        [Description("Event raised after plot data.")]
        public event PlotEvent AfterPlot;

        internal void OnAfterPlot(bool isClearOperation)
        {
            AfterPlot?.Invoke(this, new StripChartXPlotEventArgs() { ParentChart = this, IsClear = isClearOperation });
        }
        #endregion

        #region Constructor

        public StripChartX()
        {
            InitializeComponent();
            // 设计器中自动配置了Name会导致在设计时获取控件名称失败
            this.Name = "";
            // StripChartX中最核心的两个功能类：
            // _chartViewManager：管理chart的所有视图更新
            // _plotManager：管理线条的数据特性
            _plotManager = new PlotManager(this, _chart.Series);
            _chartViewManager = new ChartViewManager(this, _chart, _plotManager);
            ViewAdapter = new AxisViewAdapter(_chartViewManager, _plotManager);
            _tabCursorForm = null;
            TabCursors = new StripTabCursorCollection(this, _chart, _chartViewManager.MainPlotArea);
            this.TabCursorContainer = new StripTabCursorDesignTimeCollection(TabCursors);


            _plotManager.AdaptSeriesCount();
            //更新

            this.Miscellaneous = new MiscellaneousConfiguration(this, _chartViewManager, _plotManager);

            AdaptPlotSeriesAndChartView();
            Clear();
            // 初始化坐标轴和游标
            _axes[0] = AxisX;
            _axes[1] = AxisY;
            _axes[2] = AxisX2;
            _axes[3] = AxisY2;
            _cursors[0] = XCursor;
            _cursors[1] = YCursor;
            // 将StripChartX的事件绑定到chart上，保证用户添加的事件会被响应
            AddChartEvents();
            // 绘图结束后执行视图更新(主要执行分区视图的部分处理)
            _chart.PostPaint += ChartViewOnPostPaint;
            // 将控件本身的背景色和Chart的背景色绑定
            this.BackColor = _chart.BackColor;
            this.BackColorChanged += (sender, args) => { _chart.BackColor = this.BackColor; };
            // 字体和字体颜色与坐标轴的字体和字体颜色绑定
            this.ForeColorChanged += (sender, args) => { _chartViewManager.SetAxisLabelStyle();};
            this.FontChanged += (sender, args) => { _chartViewManager.SetAxisLabelStyle();};
        }
        
        private void AddChartEvents()
        {
            // 触发用户鼠标点击事件
            this._chart.Click += (sender, args) => OnClick(args);
            this._chart.DoubleClick += (sender, args) => OnDoubleClick(args);
            this._chart.MouseClick += (sender, args) => OnMouseClick(args);
            this._chart.MouseDoubleClick += (sender, args) => OnMouseDoubleClick(args);
            this._chart.MouseUp += (sender, args) => OnMouseUp(args);
            this._chart.MouseDown += (sender, args) => OnMouseDown(args);
            this._chart.MouseMove += (sender, args) => OnMouseMove(args);
            this._chart.MouseEnter += (sender, args) => OnMouseEnter(args);
            this._chart.MouseLeave += (sender, args) => OnMouseLeave(args);
            this._chart.MouseHover += (sender, args) => OnMouseHover(args);
            this._chart.DragDrop += (sender, args) => OnDragDrop(args);
            this._chart.DragEnter += (sender, args) => OnDragEnter(args);
            this._chart.DragLeave += (sender, args) => OnDragLeave(args);
            this._chart.DragOver += (sender, args) => OnDragOver(args);
            this._chart.KeyDown += (sender, args) => OnKeyDown(args);
            this._chart.KeyUp += (sender, args) => OnKeyUp(args);
            this._chart.KeyPress += (sender, args) => OnKeyPress(args);
            this._chart.Enter += (sender, args) => OnEnter(args);
            this._chart.Leave += (sender, args) => OnLeave(args);
        }

        #endregion  // Constructor

        #region Public Methods

        #region Array interface

        #region MultiSample interface

        /// <summary>
        /// 绘制多条曲线的多个点，String模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabels">X轴待显示内容</param>
        public void Plot<TDataType>(TDataType[,] lineData, string[] xLabels)
        {
            CheckXData(XAxisDataType.String);
            int seriesCount = lineData.GetLength(0);
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, xLabels.Length);
            }
            CheckYData(seriesCount, typeof (TDataType));
            _plotManager.DataEntity.AddPlotData(xLabels, lineData);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制多条曲线的多个点，String模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabels">X轴待显示内容</param>
        public void Plot<TDataType>(TDataType[] lineData, string[] xLabels)
        {
            CheckXData(XAxisDataType.String);
            int seriesCount = 1;
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, xLabels.Length);
            }
            CheckYData(seriesCount, typeof (TDataType));
            _plotManager.DataEntity.AddPlotData(xLabels, lineData);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制多条曲线的多个点，TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabels">X轴待显示内容</param>
        public void Plot<TDataType>(TDataType[,] lineData, DateTime[] xLabels)
        {
            CheckXData(XAxisDataType.TimeStamp);
            int seriesCount = lineData.GetLength(0);
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, xLabels.Length);
            }
            CheckYData(seriesCount, typeof (TDataType));
            _plotManager.DataEntity.AddPlotData(xLabels, lineData);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制多条曲线的多个点，TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabels">X轴待显示内容</param>
        public void Plot<TDataType>(TDataType[] lineData, DateTime[] xLabels)
        {
            CheckXData(XAxisDataType.TimeStamp);
            int seriesCount = lineData.Length/xLabels.Length;
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, xLabels.Length);
            }
            CheckYData(seriesCount, typeof (TDataType));
            _plotManager.DataEntity.AddPlotData(xLabels, lineData);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制多条曲线的多个点，Index/TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        public void Plot<TDataType>(TDataType[,] lineData)
        {
            CheckXData(XAxisDataType.Index, XAxisDataType.TimeStamp);
            int seriesCount = lineData.GetLength(0);
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, lineData.GetLength(1));
            }
            CheckYData(seriesCount, typeof (TDataType));
            _plotManager.DataEntity.AddPlotData(lineData, lineData.GetLength(1));
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制一条曲线的多个点，Index/TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        public void Plot<TDataType>(TDataType[] lineData)
        {
            CheckXData(XAxisDataType.Index, XAxisDataType.TimeStamp);
            int seriesCount = 1;
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, lineData.Length);
            }
            CheckYData(seriesCount, typeof (TDataType));
            _plotManager.DataEntity.AddPlotData(lineData, lineData.Length);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        #endregion

        #region Single Sample Interface

        /// <summary>
        /// 绘制多条曲线的一个样点，String模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabel">X轴待显示内容</param>
        public void PlotSingle<TDataType>(TDataType[] lineData, string xLabel)
        {
            CheckXData(XAxisDataType.String);
            int seriesCount = lineData.Length;
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, 1);
            }
            CheckYData(seriesCount, typeof (TDataType));
            _plotManager.DataEntity.AddPlotData(new string[] {xLabel}, lineData);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制多条曲线的一个样点，TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabel">X轴待显示内容</param>
        public void PlotSingle<TDataType>(TDataType[] lineData, DateTime xLabel)
        {
            CheckXData(XAxisDataType.TimeStamp);
            int seriesCount = lineData.Length;
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, 1);
            }
            CheckYData(seriesCount, typeof (TDataType));
            _plotManager.DataEntity.AddPlotData(new DateTime[] {xLabel}, lineData);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制多条曲线的一个样点，Index/TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        public void PlotSingle<TDataType>(TDataType[] lineData)
        {
            CheckXData(XAxisDataType.Index, XAxisDataType.TimeStamp);
            int seriesCount = lineData.Length;
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, 1);
            }
            CheckYData(seriesCount, typeof (TDataType));
            _plotManager.DataEntity.AddPlotData(lineData, 1);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制一条曲线的一个样点，String模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabel">X轴待显示内容</param>
        public void PlotSingle<TDataType>(TDataType lineData, string xLabel)
        {
            CheckXData(XAxisDataType.String);
            int seriesCount = 1;
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, 1);
            }
            CheckYData(seriesCount, typeof(TDataType));
            _plotManager.DataEntity.AddPlotData(new string[] { xLabel }, new TDataType[] { lineData });
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制一条曲线的一个样点，TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabel">X轴待显示内容</param>
        public void PlotSingle<TDataType>(TDataType lineData, DateTime xLabel)
        {
            CheckXData(XAxisDataType.TimeStamp);
            int seriesCount = 1;
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, 1);
            }
            CheckYData(seriesCount, typeof(TDataType));
            _plotManager.DataEntity.AddPlotData(new DateTime[] { xLabel }, new TDataType[] { lineData });
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// 绘制一条曲线的一个样点，Index/TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        public void PlotSingle<TDataType>(TDataType lineData)
        {
            CheckXData(XAxisDataType.Index, XAxisDataType.TimeStamp);
            int seriesCount = 1;
            if (!_plotManager.IsPlotting)
            {
                InitPlotManagerAndViewManager<TDataType>(seriesCount, 1);
            }
            CheckYData(seriesCount, typeof(TDataType));
            _plotManager.DataEntity.AddPlotData(new TDataType[] { lineData }, 1);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        #endregion


        private void CheckXData(params XAxisDataType[] validXDataTypes)
        {
            if (!validXDataTypes.Contains(_plotManager.XDataType))
            {
                throw new InvalidOperationException(i18n.GetFStr("ParamCheck.InvalidCall", "XDataType", _plotManager.XDataType));
            }
        }

        private void CheckYData(int seriesCount, Type dataType)
        {
            if (_plotManager.IsPlotting && seriesCount != _plotManager.SeriesCount)
            {
                throw new ArgumentException(i18n.GetFStr("RunTime.NoFixInRunTime", "SeriesCount"));
            }
            if (null != _plotManager.DataType && !ReferenceEquals(dataType, _plotManager.DataType))
            {
                throw new ArgumentException(i18n.GetStr("RunTime.YDataTypeNotSame"));
            }
        }

        private void InitPlotManagerAndViewManager<TDataType>(int newSeriesCount, int sampleCount)
        {
            Type dataType = typeof (TDataType);
            if (!Constants.ValidDataType.Contains(dataType))
            {
                throw new ArgumentException(i18n.GetFStr("RunTime.InvalidDataType", dataType.Name));
            }
            int lastSeriesCount = _plotManager.SeriesCount;
            AdaptPlotSeriesAndChartView(newSeriesCount != lastSeriesCount);
            _plotManager.InitializeDataEntity<TDataType>(newSeriesCount, sampleCount);
        }

        #endregion

        /// <summary>
        /// Clear chart line points
        /// </summary>
        public void Clear()
        {
//            BindPlotSeriesAndChartView();
            OnBeforePlot(true);
            _plotManager.Clear();
            _chartViewManager.Clear();
            ValueDisplayToolTip.RemoveAll();
            OnAfterPlot(true);
        }

        /// <summary>
        /// Save chart view to png file
        /// </summary>
        /// <para name="filePath">Png file path</para>
        public void SaveAsImage(string filePath = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                var saveFileDialogPic = new SaveFileDialog();
                saveFileDialogPic.Filter = "Png图片|*.Png";
                if (saveFileDialogPic.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                filePath = saveFileDialogPic.FileName;
            }
            _chart.SaveImage(filePath, ChartImageFormat.Png);
        }

        /// <summary>
        /// Save chart data to csv file
        /// </summary>
        /// <param name="filePath">The path of the save file. A file selection form will shown if file path is null</param>
        public void SaveAsCsv(string filePath = null)
        {
            if (!IsPlotting())
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                var saveFileDialogPic = new SaveFileDialog();
                saveFileDialogPic.Filter = "CSV表格|*.csv";
                if (saveFileDialogPic.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                filePath = saveFileDialogPic.FileName;
            }
            _plotManager.SaveAsCsv(filePath);
        }

        /// <summary>
        /// Zoom reset all axis
        /// </summary>
        /// <param name="resetTime"></param>
        public void ZoomReset(int resetTime = int.MaxValue)
        {
            foreach (StripChartXAxis axis in _chartViewManager.MainPlotArea.Axes)
            {
                axis.ZoomReset(resetTime);
            }
            foreach (StripChartXPlotArea plotArea in _chartViewManager.SplitPlotAreas)
            {
                foreach (StripChartXAxis axis in plotArea.Axes)
                {
                    axis.ZoomReset(resetTime);
                }
            }
        }

        public bool IsPlotting()
        {
            return _plotManager.IsPlotting;
        }

        #endregion

        #region Event Handler
        
        private void Chart_AxisViewChanged(object sender, ViewEventArgs viewEventArgs)
        {
            if (null == viewEventArgs || null == viewEventArgs.ChartArea)
            {
                return;
            }
            ChartArea chartArea = viewEventArgs.ChartArea;
            Axis axis = viewEventArgs.Axis;
            RefreshScaleViewAndSendEvent(chartArea, axis, true);
        }

        // TODO 暂时只同步Y轴，X轴后期再看
        internal void RefreshScaleViewAndSendEvent(ChartArea chartArea, Axis axis, bool raisedByMouse)
        {
            StripChartXAxis changedAxis = null;
            // X轴的缩放需要执行数据更新
            if (chartArea.AxisX.Name.Equals(axis.Name) || chartArea.AxisX2.Name.Equals(axis.Name))
            {
                // 非分区视图，且是主绘图区的缩放事件
                if (ReferenceEquals(_chartViewManager.MainPlotArea.ChartArea, chartArea) && !_chartViewManager.IsSplitView)
                {
                    changedAxis = _chartViewManager.MainPlotArea.AxisX;
                    changedAxis.RefreshGridsAndLabels();
                    _plotManager.PlotDataInRange(axis.ScaleView.ViewMinimum, axis.ScaleView.ViewMaximum, false);
                }
                // 分区视图，副绘图区的缩放事件
                else if (_chartViewManager.IsSplitView)
                {
                    int seriesIndex = _chartViewManager.SplitPlotAreas.FindIndexByBaseChartArea(chartArea);
                    if (seriesIndex >= 0 && seriesIndex < _chartViewManager.SplitPlotAreas.Count)
                    {
                        changedAxis = _chartViewManager.SplitPlotAreas[seriesIndex].AxisX;
                        changedAxis.RefreshGridsAndLabels();
                        _plotManager.PlotDataInRange(axis.ScaleView.ViewMinimum, axis.ScaleView.ViewMaximum, seriesIndex, false);
                        
                    }
                }
            }
            // Y轴的缩放需要同步副坐标轴、更新网格间隔
            else
            {
                if (ReferenceEquals(_chartViewManager.MainPlotArea.ChartArea, chartArea))
                {
                    changedAxis = _chartViewManager.MainPlotArea.AxisY;
                    _chartViewManager.MainPlotArea.YAxisSync.SyncAxis();
                    _chartViewManager.MainPlotArea.AxisY.RefreshGridsAndLabels();
                    _chartViewManager.MainPlotArea.AxisY2.RefreshGridsAndLabels();
                }
                else
                {
                    int seriesIndex = _chartViewManager.SplitPlotAreas.FindIndexByBaseChartArea(chartArea);
                    if (seriesIndex >= 0 && seriesIndex < _chartViewManager.SplitPlotAreas.Count)
                    {
                        changedAxis = _chartViewManager.SplitPlotAreas[seriesIndex].AxisY;
                        _chartViewManager.SplitPlotAreas[seriesIndex].YAxisSync.SyncAxis();
                        // 分区视图更新后需要手动刷新Y轴的label
                        _chartViewManager.SplitPlotAreas[seriesIndex].AxisY.RefreshGridsAndLabels();
                        _chartViewManager.SplitPlotAreas[seriesIndex].AxisY2.RefreshGridsAndLabels();
                        // 分区模式下，视图更新需要手动刷新Label
                        changedAxis.ClearLabels();
                    }
                }
            }
            OnAxisViewChanged(changedAxis, true, true);
        }

        private void _chart_MouseDown(object sender, MouseEventArgs eventArgs)
        {
            HitTestResult result = null;
            try
            {
                result = _chart.HitTest(eventArgs.X, eventArgs.Y);
            }
            catch (Exception)
            {
                return;
            }
            this.ValueDisplayToolTip.Hide(this._chart);
            _hitSeries = null;
            _hitPlotArea = null;
            if (result.ChartElementType == ChartElementType.LegendItem)
            {
                LegendItem legendItem = (LegendItem)result.Object;
                _hitSeries = _plotManager.Series.First(item => item.Name.Equals(legendItem.SeriesName));
                if (null == _hitSeries)
                {
                    return;
                }
                ShowSeriesViewConfigMenu(eventArgs.Location);
            }
            else
            {
                //在chartarea区域显示当前缩放需要显示的光标
                _hitPlotArea = _chartViewManager.GetHitPlotArea(result);
                if (null == _hitPlotArea)
                {
                    return;
                }
                //这是用来显示XYValue的代码函数
                switch (eventArgs.Button)
                {
                    case MouseButtons.Left:
                        if (IsPlotting() && IsCursorMode(_hitPlotArea))
                        {
                            ShowCursorDataValue();
                        }
                        break;
                    case MouseButtons.Right:
                        ShowContextMenu(eventArgs);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ShowSeriesViewConfigMenu(Point location)
        {
            toolStripTextBox_seriesName.TextBox.Text = _hitSeries.Name;
            curveColorToolStripMenuItem.ForeColor = _hitSeries.Color;
            // 更新线宽的选单
            string seriesPropertyValue = _hitSeries.Width.ToString();
            foreach (object dropDownItem in lineWidthToolStripMenuItem.DropDownItems)
            {
                ToolStripMenuItem menuItems = dropDownItem as ToolStripMenuItem;
                menuItems.Checked = seriesPropertyValue.Equals(menuItems.Tag);
            }
            // 更新线型的选单
            seriesPropertyValue = _hitSeries.Type.ToString();
            foreach (object dropDownItem in interpolationToolStripMenuItem.DropDownItems)
            {
                ToolStripMenuItem menuItems = dropDownItem as ToolStripMenuItem;
                menuItems.Checked = seriesPropertyValue.Equals(menuItems.Tag);
            }
            // 更新点型的选单
            seriesPropertyValue = _hitSeries.Marker.ToString();
            foreach (object dropDownItem in ponintStyleToolStripMenuItem.DropDownItems)
            {
                ToolStripMenuItem menuItems = dropDownItem as ToolStripMenuItem;
                menuItems.Checked = seriesPropertyValue.Equals(menuItems.Tag);
            }
            ChartSeriesMenu.Show(_chart, location);
        }

        private void ShowCursorDataValue()
        {
            int seriesIndex = GetCursorSeriesIndex();
            string dispText = MoveCursorAndShowValue(_hitPlotArea, seriesIndex);
            if (string.IsNullOrEmpty(dispText))
            {
                return;
            }
            Point clickPoint = this._chart.PointToClient(Control.MousePosition); //鼠标相对于窗体左上角的坐标
            clickPoint = new Point(clickPoint.X + 10, clickPoint.Y + 10);
            this.ValueDisplayToolTip.Show(dispText, this._chart, clickPoint);
            this.OnCursorPositionChanged(_hitPlotArea.XCursor, true, seriesIndex);
        }

        private string MoveCursorAndShowValue(StripChartXPlotArea hitPlotArea, int seriesIndex)
        {
            StripChartXCursor xCursor = hitPlotArea.XCursor;
            StripChartXCursor yCursor = hitPlotArea.YCursor;
            string dispText = string.Empty;
            if (seriesIndex < 0)
            {
                return dispText;
            }
            double xNearValue = xCursor.Value;
//            double yValue = yCursor.Value;
            if (hitPlotArea.AxisX.IsLogarithmic)
            {
                xNearValue = Math.Pow(10, xNearValue);
            }
//            if (hitPlotArea.AxisY.IsLogarithmic)
//            {
//                yValue = Math.Pow(10, yValue);
//            }
            int xIndex = ViewAdapter.GetVerifiedIndex(xNearValue);
            string xValue = _plotManager.DataEntity.GetXValue(xIndex);
            double yValue = (double)_plotManager.DataEntity.GetYValue(xIndex, seriesIndex);

            StripChartXAxis xAxis, yAxis;
            xAxis = StripChartXAxis.PlotAxis.Primary == Series[seriesIndex].XPlotAxis
                ? hitPlotArea.AxisX
                : hitPlotArea.AxisX2;

            yAxis = StripChartXAxis.PlotAxis.Primary == Series[seriesIndex].YPlotAxis
                ? hitPlotArea.AxisY
                : hitPlotArea.AxisY2;

            xCursor.Value = !xAxis.IsLogarithmic ? xNearValue : Math.Log10(xNearValue);
            yCursor.Value = !yAxis.IsLogarithmic ? yValue : Math.Log10(yValue);
            return string.Format(Constants.DataValueFormat, xValue, yValue, Environment.NewLine);
        }

        private void ShowContextMenu(MouseEventArgs eventArgs)
        {
            //更新菜单项勾选状态
            RefreshContextMenuItems();
            //更新使能Series菜单项
            RefreshEnableSeriesMenuItems();
            //更新游标Series菜单项
            RefreshCursorSeriesMenuItems();
            //更新动态游标菜单项
            RefreshDynamicCursorMenuItems();
            //显示菜单栏
            ChartFunctionMenu.Show(_chart, eventArgs.Location);
            
        }
        
        private void RefreshContextMenuItems()
        {
            // 配置未绘图情况下部分菜单项不使能
            bool isPlotting = IsPlotting();
            ToolStripMenuItem_xAxisZoom.Enabled = isPlotting;
            ToolStripMenuItem_yAxisZoom.Enabled = isPlotting;
            ToolStripMenuItem_windowZoom.Enabled =isPlotting;
            ToolStripMenuItem_zoomReset.Enabled = isPlotting;
            ToolStripMenuItem_showValue.Enabled = isPlotting;
            ToolStripMenuItem_saveAsPicture.Enabled = isPlotting;
            ToolStripMenuItem_saveAsCsv.Enabled = isPlotting;
            ToolStripMenuItem_showSeriesParent.Enabled = isPlotting;
            ToolStripMenuItem_cursorSeriesParent.Enabled = isPlotting;
            tabCursorToolStripMenuItem.Enabled = isPlotting;
            // 根据当前的用户配置更新各个菜单项的勾选情况
            ToolStripMenuItem_xAxisZoom.Checked = StripChartXCursor.CursorMode.Zoom == _hitPlotArea.XCursor.Mode &&
                                                  StripChartXCursor.CursorMode.Zoom != _hitPlotArea.YCursor.Mode;
            ToolStripMenuItem_yAxisZoom.Checked = StripChartXCursor.CursorMode.Zoom != _hitPlotArea.XCursor.Mode &&
                                                  StripChartXCursor.CursorMode.Zoom == _hitPlotArea.YCursor.Mode;
            ToolStripMenuItem_windowZoom.Checked = StripChartXCursor.CursorMode.Zoom == _hitPlotArea.XCursor.Mode &&
                                                  StripChartXCursor.CursorMode.Zoom == _hitPlotArea.YCursor.Mode;
            ToolStripMenuItem_showValue.Checked = IsCursorMode(_hitPlotArea);
            toolStripMenuItem_splitView.Checked = _chartViewManager.IsSplitView;
            ToolStripMenuItem_legendVisible.Checked = LegendVisible;
            ToolStripMenuItem_yAxisAutoScale.Checked = _hitPlotArea.AxisY.AutoScale;
            
            // 分区视图隐藏CursorSeries
//            ToolStripMenuItem_showSeriesParent.Visible = !_chartViewManager.IsSplitView;
            ToolStripMenuItem_cursorSeriesParent.Visible = (!_chartViewManager.IsSplitView && IsCursorMode(_hitPlotArea));
//            toolStripSeparator_range.Visible = !_chartViewManager.IsSplitView;
        }

        private void RefreshEnableSeriesMenuItems()
        {
            ToolStripItemCollection seriesEnableMenuItems = ToolStripMenuItem_showSeriesParent.DropDownItems;
            if (_enableSeriesMenuItems.Count != _plotManager.PlotSeries.Count)
            {
                while (_enableSeriesMenuItems.Count > _plotManager.PlotSeries.Count)
                {
                    int removeIndex = _enableSeriesMenuItems.Count - 1;
                    seriesEnableMenuItems.RemoveAt(removeIndex);
                    _enableSeriesMenuItems.RemoveAt(removeIndex);
                }

                while (_enableSeriesMenuItems.Count < _plotManager.PlotSeries.Count)
                {
                    ToolStripMenuItem seriesMenuItem = new ToolStripMenuItem();
                    seriesMenuItem.Click += RefreshSeriesEnableStatus;
                    seriesEnableMenuItems.Add(seriesMenuItem);
                    _enableSeriesMenuItems.Add(seriesMenuItem);
                }
            }
            // 只有在非分区模式下用户才可以选择是否显示某个Series
            bool seriesMenuEnabled = !_chartViewManager.IsSplitView;
            for (int seriesIndex = 0; seriesIndex < _plotManager.SeriesCount; seriesIndex++)
            {
                Series plotSeries = _plotManager.PlotSeries[seriesIndex];
                _enableSeriesMenuItems[seriesIndex].Text = plotSeries.Name;
                _enableSeriesMenuItems[seriesIndex].Visible = _hitPlotArea.Name.Equals(plotSeries.ChartArea);
                _enableSeriesMenuItems[seriesIndex].Checked = plotSeries.Enabled;
                _enableSeriesMenuItems[seriesIndex].Enabled = seriesMenuEnabled;
            }
        }

        private void RefreshCursorSeriesMenuItems()
        {
            if (!IsCursorMode(_hitPlotArea))
            {
                ToolStripMenuItem_cursorSeriesParent.Visible = false;
                return;
            }
            ToolStripMenuItem_cursorSeriesParent.Visible = IsCursorMode(_hitPlotArea);
            ToolStripItemCollection cursorSeriesItems = ToolStripMenuItem_cursorSeriesParent.DropDownItems;
            if (_cursorSeriesMenuItems.Count != _plotManager.PlotSeries.Count)
            {
                while (_cursorSeriesMenuItems.Count > _plotManager.PlotSeries.Count)
                {
                    int removeIndex = _enableSeriesMenuItems.Count - 1;
                    cursorSeriesItems.RemoveAt(removeIndex);
                    _cursorSeriesMenuItems.RemoveAt(removeIndex);
                }

                while (_cursorSeriesMenuItems.Count < _plotManager.PlotSeries.Count)
                {
                    ToolStripMenuItem cursorMenuItem = new ToolStripMenuItem();
                    cursorMenuItem.Click += DeselectOthersAndUpdateCursorBinding;
                    cursorSeriesItems.Add(cursorMenuItem);
                    _cursorSeriesMenuItems.Add(cursorMenuItem);
                }
            }
            for (int seriesIndex = 0; seriesIndex < _plotManager.SeriesCount; seriesIndex++)
            {
                Series plotSeries = _plotManager.PlotSeries[seriesIndex];
                _cursorSeriesMenuItems[seriesIndex].Text = plotSeries.Name;
                _cursorSeriesMenuItems[seriesIndex].Visible = _hitPlotArea.Name.Equals(plotSeries.ChartArea) && plotSeries.Enabled;

                // TODO Visible不可用，使用Enabled代替判断
                _cursorSeriesMenuItems[seriesIndex].Enabled = _hitPlotArea.Name.Equals(plotSeries.ChartArea) && plotSeries.Enabled;
            }
            int checkedCount = _cursorSeriesMenuItems.Count(item => item.Enabled && item.Checked);
            ToolStripMenuItem firstAvailableItem = _cursorSeriesMenuItems.Find(item => item.Enabled);
            if (checkedCount > 1)
            {
                DeselectOthersAndUpdateCursorBinding(firstAvailableItem, null);
            }
            else if (checkedCount <= 0)
            {
                // 如果没有选中的Cursor，则使能第一个可用的
                if (null != firstAvailableItem)
                {
                    firstAvailableItem.Checked = true;
                }
            }
            _hitPlotArea?.BindCursorToAxis();
        }

        private void RefreshDynamicCursorMenuItems()
        {
            // TODO 分区视图暂不支持动态游标
            tabCursorToolStripMenuItem.Visible = !_chartViewManager.IsSplitView;
            toolStripSeparator_series.Visible = !_chartViewManager.IsSplitView;
        }

        private void RefreshSeriesEnableStatus(object sender, EventArgs e)
        {
            ToolStripMenuItem seriesMenuItem = sender as ToolStripMenuItem;
            if (null == seriesMenuItem)
            {
                return;
            }
            seriesMenuItem.Checked = !seriesMenuItem.Checked;
            Series plotSeries = _plotManager.PlotSeries.FindByName(seriesMenuItem.Text);
            if (null == plotSeries)
            {
                return;
            }
            plotSeries.Enabled = seriesMenuItem.Checked;
            RefreshCursorSeriesMenuItems();
        }

        private int GetCursorSeriesIndex()
        {
            int selectSeriesIndex = 0;
            if (!_chartViewManager.IsSplitView)
            {
                int selectIndex = _cursorSeriesMenuItems.FindIndex(seriesItem => seriesItem.Checked && seriesItem.Enabled);
                Series selectSeries = null;
                if (selectIndex >= 0)
                {
                    selectSeries = _plotManager.PlotSeries.FindByName(_cursorSeriesMenuItems[selectIndex].Text);
                }
                else
                {
                    selectSeries = _plotManager.PlotSeries.FirstOrDefault(item => item.Enabled);
                }
                if (null == selectSeries)
                {
                    return -1;
                }
                selectSeriesIndex = _chart.Series.IndexOf(selectSeries);
            }
            else
            {
                selectSeriesIndex = _chartViewManager.SplitPlotAreas.IndexOf(_hitPlotArea);
                if (selectSeriesIndex < 0 || !_plotManager.PlotSeries[selectSeriesIndex].Enabled)
                {
                    selectSeriesIndex = -1;
                }
            }
            return selectSeriesIndex;
        }

        private void tabCursorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _tabCursorForm?.Close();
            _tabCursorForm = new StripTabCursorInfoForm(this);
            _tabCursorForm.Show(this);
        }

        #region rightClickMenuEvnet
        /// <summary>
        /// 当点击保存图片按钮的事件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAsImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 点击保存到CSV中，所处理的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAsCsv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// 事件内容，是否显示LegendItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void legendVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_legendVisible.Checked = !ToolStripMenuItem_legendVisible.Checked;
            this.LegendVisible = ToolStripMenuItem_legendVisible.Checked;
        }

        private void toolStripMenuItem_splitView_Click(object sender, EventArgs e)
        {
            toolStripMenuItem_splitView.Checked = !toolStripMenuItem_splitView.Checked;
            _chartViewManager.IsSplitView = toolStripMenuItem_splitView.Checked;
        }

        /// <summary>
        /// 取消Unchecke按钮
        /// </summary>
        private void UncheckedZoom_ToolStripMenuItem()
        {
            ToolStripMenuItem_xAxisZoom.Checked = false;
            ToolStripMenuItem_yAxisZoom.Checked = false;
            ToolStripMenuItem_windowZoom.Checked = false;
            ToolStripMenuItem_showValue.Checked = false;
        }
        /// <summary>
        /// 私有方法，主要用清楚ValueTips Show的各种方法
        /// </summary>
        private void Show_XYValueClear()
        {
            //XYValue Show的清楚方法
            ValueDisplayToolTip.RemoveAll();
            if (null == _hitPlotArea)
            {
                return;
            }
            _hitPlotArea.XCursor.Mode = StripChartXCursor.CursorMode.Disabled;
            _hitPlotArea.YCursor.Mode = StripChartXCursor.CursorMode.Disabled;
        }
        /// <summary>
        /// 点击ToolStripMenu  ZoomX状态的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zoom_XAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == _hitPlotArea)
            {
                return;
            }
            if (ToolStripMenuItem_xAxisZoom.Checked)
            {
                ToolStripMenuItem_xAxisZoom.Checked = false;
                _hitPlotArea.XCursor.Mode = StripChartXCursor.CursorMode.Disabled;
                _hitPlotArea.YCursor.Mode = StripChartXCursor.CursorMode.Disabled;
            }
            else
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValueClear();
                ToolStripMenuItem_xAxisZoom.Checked = true;
                //                SetZoomable(true, false);
                _hitPlotArea.XCursor.Mode = StripChartXCursor.CursorMode.Zoom;
                _hitPlotArea.YCursor.Mode = StripChartXCursor.CursorMode.Disabled;
            }
//            RefreshCursorSeriesMenuItems();
        }

        /// <summary>
        /// 点击ToolStripMenu  ZoomY状态的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zoom_YAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == _hitPlotArea)
            {
                return;
            }

            if (ToolStripMenuItem_yAxisZoom.Checked)
            {
                ToolStripMenuItem_yAxisZoom.Checked = false;
                //                SetZoomable(false, false);
                _hitPlotArea.XCursor.Mode = StripChartXCursor.CursorMode.Disabled;
                _hitPlotArea.YCursor.Mode = StripChartXCursor.CursorMode.Disabled;
            }
            else
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValueClear();
                ToolStripMenuItem_yAxisZoom.Checked = true;
                //                SetZoomable(false, true);
                _hitPlotArea.XCursor.Mode = StripChartXCursor.CursorMode.Disabled;
                _hitPlotArea.YCursor.Mode = StripChartXCursor.CursorMode.Zoom;
            }
//            RefreshCursorSeriesMenuItems();
        }
        /// <summary>
        /// 点击ToolStripMenu  ZoomWindows状态的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zoom_WindowtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == _hitPlotArea)
            {
                return;
            }

            if (ToolStripMenuItem_windowZoom.Checked == true)
            {
                ToolStripMenuItem_windowZoom.Checked = false;
                _hitPlotArea.XCursor.Mode = StripChartXCursor.CursorMode.Disabled;
                _hitPlotArea.XCursor.Mode = StripChartXCursor.CursorMode.Disabled;
            }
            else
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValueClear();
                ToolStripMenuItem_windowZoom.Checked = true;
                _hitPlotArea.XCursor.Mode = StripChartXCursor.CursorMode.Zoom;
                _hitPlotArea.YCursor.Mode = StripChartXCursor.CursorMode.Zoom;
            }
//            RefreshCursorSeriesMenuItems();
        }

        /// <summary>
        /// 点击ToolStripMenu ShowXYValue的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_XYValuetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_showValue.Checked = !ToolStripMenuItem_showValue.Checked;
            if (ToolStripMenuItem_showValue.Checked)
            {
                UncheckedZoom_ToolStripMenuItem();
                ToolStripMenuItem_showValue.Checked = true;
                _hitPlotArea.XCursor.Mode = StripChartXCursor.CursorMode.Cursor;
                _hitPlotArea.YCursor.Mode = StripChartXCursor.CursorMode.Cursor;
            }
            else
            {
                Show_XYValueClear();
            }
            RefreshCursorSeriesMenuItems();
        }

        /// <summary>
        /// 点击ToolStripMenu  ZoomReset状态的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zoom_ResettoolStripMenuItem_Click(object sender, EventArgs e)
        {
            _hitPlotArea?.AxisX.ZoomReset();
            _hitPlotArea?.AxisY.ZoomReset();
            // 暂时屏蔽，会影响正常轴的缩放
//            _hitPlotArea?.AxisX2.ZoomReset();
            _hitPlotArea?.AxisY2.ZoomReset();
        }

        /// <summary>
        /// 是否使能Y轴的Auto Scale的功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YAutoScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == _hitPlotArea)
            {
                return;
            }
            ToolStripMenuItem_yAxisAutoScale.Checked = !ToolStripMenuItem_yAxisAutoScale.Checked;
            _hitPlotArea.AxisY.AutoScale = ToolStripMenuItem_yAxisAutoScale.Checked;
            _hitPlotArea.AxisY2.AutoScale = ToolStripMenuItem_yAxisAutoScale.Checked;
        }

        #endregion

        #region leftClickMenuEvnet

        private void DeselectOthersAndUpdateCursorBinding(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in _cursorSeriesMenuItems)
            {
                item.Checked = false;
            }
            ((ToolStripMenuItem) sender).Checked = true;
            _hitPlotArea?.BindCursorToAxis();
        }

        /// <summary>
        /// 选择线条颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSeriesLineColor(object sender, EventArgs e)
        {
            ColorDialog loColorForm = new ColorDialog();
            if (loColorForm.ShowDialog() == DialogResult.OK)
            {
                Color chooseColor = loColorForm.Color;
                _hitSeries.Color = chooseColor; //更换曲线的颜色
            }
        }
        /// <summary>
        /// 选择线宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSeriesLineWidth(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (null == menuItem || null == _hitSeries)
            {
                return;
            }
            StripChartXSeries.LineWidth lineWidth;
            if (null != menuItem.Tag && Enum.TryParse(menuItem.Tag.ToString(), out lineWidth))
            {
                _hitSeries.Width = lineWidth;
            }
        }
        /// <summary>
        /// 选择曲线类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSeriesLineType(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (null == menuItem || null == _hitSeries)
            {
                return;
            }
            StripChartXSeries.LineType interpolation;
            if (null != menuItem.Tag && Enum.TryParse(menuItem.Tag.ToString(), out interpolation))
            {
                _hitSeries.Type = interpolation;
            }
        }

        private void SetSeriesMarkerType(object sender, EventArgs e)
        {
            // TODO to Check
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (null == menuItem || null == _hitSeries)
            {
                return;
            }
            StripChartXSeries.MarkerType markerType;
            if (null != menuItem.Tag && Enum.TryParse(menuItem.Tag.ToString(), out markerType))
            {
                _hitSeries.Marker = markerType;
            }
        }

        private void setYAxisRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form yAxisRangeForm = new StripChartXRangeYConfigForm(_hitPlotArea);
            Point clickPoint = this._chart.PointToClient(Control.MousePosition);//鼠标相对于窗体左上角的坐标
            clickPoint = new Point(clickPoint.X, clickPoint.Y);
            yAxisRangeForm.StartPosition = FormStartPosition.Manual;
            yAxisRangeForm.Location = clickPoint;
            yAxisRangeForm.Show();
        }

        #region Callback function for other module

        internal void RefreshAxesRange(StripChartXPlotArea plotArea)
        {
            _chartViewManager.RefreshAxesRange(plotArea);
        }

        internal Series GetCursorSeries(StripChartXPlotArea plotArea)
        {
            int index = ReferenceEquals(_chartViewManager.MainPlotArea, plotArea)
                ? GetCursorSeriesIndex()
                : _chartViewManager.SplitPlotAreas.IndexOf(plotArea);
            return index >= 0 ? _plotManager.PlotSeries[index] : null;
        }

        internal void ShowDynamicValue(string showInfo, Point position, bool isShow)
        {
            const int tabValueTipXOffset = 5;
            if (isShow)
            {
                position.X += tabValueTipXOffset;
                toolTip_tabCursorValue.Show(showInfo, this, position);
            }
            else
            {
                toolTip_tabCursorValue.Hide(this);
            }
        }

        #endregion


        #endregion

        #endregion // Event Handler

        #region Chart Utility

        /// <summary>
        /// 根据待绘图线数匹配Series和视图区
        /// </summary>
        /// <param name="refreshBinding">是否强制更新绑定</param>
        internal void AdaptPlotSeriesAndChartView(bool refreshBinding = true)
        {
            _plotManager.AdaptSeriesCount();
            if (refreshBinding)
            {
                _chartViewManager.AdaptView();
                if (!_chartViewManager.IsSplitView)
                {
                    foreach (Series plotSeries in _plotManager.PlotSeries)
                    {
                        plotSeries.ChartArea = _chartViewManager.MainPlotArea.Name;
                    }
                }
                else
                {
                    for (int i = 0; i < _plotManager.SeriesCount; i++)
                    {
                        _plotManager.PlotSeries[i].ChartArea = _chartViewManager.SplitPlotAreas[i].Name;
                    }
                }
            }
        }

        /// <summary>
        /// 根据坐标轴的缩放范围在Series上绘点
        /// </summary>
        internal void PlotDataInRange()
        {
            OnBeforePlot(false);
            // 如果不是分区视图则统一绘制，如果是分区视图则分别绘制每条线
            if (!_chartViewManager.IsSplitView)
            {
                double beginX = _chartViewManager.MainPlotArea.AxisX.ViewMinimum;
                double endX = _chartViewManager.MainPlotArea.AxisX.ViewMaximum;
                _plotManager.PlotDataInRange(beginX, endX, true);
            }
            else
            {
                for (int i = 0; i < _plotManager.SeriesCount; i++)
                {
                    double beginX = _chartViewManager.SplitPlotAreas[i].AxisX.ViewMinimum;
                    double endX = _chartViewManager.SplitPlotAreas[i].AxisX.ViewMaximum;
                    _plotManager.PlotDataInRange(beginX, endX, i, true);
                }
            }
            OnAfterPlot(false);
        }

        internal string GetXLabelValue(double axisValue)
        {
            const string emptyStr = " ";
            if (null == ViewAdapter || null == _plotManager.DataEntity)
            {
                return emptyStr;
            }
            int realIndex = ViewAdapter.GetUnVerifiedIndex(axisValue);
            if (realIndex < 0 || realIndex >= _plotManager.DataEntity.SamplesInChart)
            {
                return emptyStr;
            }
            return _plotManager.DataEntity.GetXValue(realIndex);
        }

        internal void GetNearestPoint(double xRawValue, out double yValue, int seriesIndex)
        {
            int index = ViewAdapter.GetUnVerifiedIndex(xRawValue);
            if (index < 0 || index >= _plotManager.DataEntity.SamplesInChart)
            {
                yValue = double.NaN;
            }
            else
            {
                yValue = (double)_plotManager.DataEntity.GetYValue(index, seriesIndex);
            }
        }

        private bool IsCursorMode(StripChartXPlotArea plotArea)
        {
            if (null == plotArea)
            {
                return false;
            }
            return StripChartXCursor.CursorMode.Cursor == _hitPlotArea.XCursor.Mode &&
                   StripChartXCursor.CursorMode.Cursor == _hitPlotArea.YCursor.Mode;
        }

        #endregion

        #region Paint Function

        private bool _isRefreshPaint = false;
        private float _lastLegendPos = -1;
        private bool _lastSplitView = false;
        private bool _lastLegendVisible = false;
        private int _lastWidth = 0;

        private void ChartViewOnPostPaint(object sender, ChartPaintEventArgs eventArgs)
        {
            // 如果不是分区视图、如果是手动强制刷新UI元素、如果图例相关参数未变化、如果视图管理器未完成视图匹配则不更新视图
            if (!_chartViewManager.IsSplitView || _isRefreshPaint || !IsLegendParamChanged() || 
                _chartViewManager.SplitPlotAreas.Count != _plotManager.SeriesCount)
            {
                return;
            }
            // 绘图时先绘制ChartArea再绘制Legend最后绘制Chart
            // 如果使能Legend,绘制完Legend后强制更新绘图区并刷新整个绘图
            // 如果不使能Legend，绘制完Chart后更新绘图区并强制更新绘图区
            if ((LegendVisible && ReferenceEquals(eventArgs.ChartElement.GetType(), typeof(Legend))) || 
                (!LegendVisible && ReferenceEquals(eventArgs.ChartElement.GetType(), typeof(Chart))))
            {
                _chartViewManager.ArrangeSplitPlotAreas();
                RefreshChart();
                _lastLegendPos = _chart.Legends[0].Position.X;
                _lastSplitView = _chartViewManager.IsSplitView;
                _lastWidth = this.Width;
                _lastLegendVisible = LegendVisible;
            }
        }

        private bool IsLegendParamChanged()
        {
            return Math.Abs(_chart.Legends[0].Position.X - _lastLegendPos) > Constants.MinDoubleValue ||
                _lastSplitView != _chartViewManager.IsSplitView || _lastLegendVisible != LegendVisible ||
                _lastWidth != this.Width;
        }

        private void RefreshChart()
        {
            _isRefreshPaint = true;
            _chart.Refresh();
            _isRefreshPaint = false;
        }


        #endregion

        #region Enumeration Declaration

        /// <summary>
        /// Gradient Style of StripChartX
        /// </summary>
        public enum ChartGradientStyle
        {
            /// <summary>
            /// Without gradient style
            /// </summary>
            None = System.Windows.Forms.DataVisualization.Charting.GradientStyle.None,
            /// <summary>
            /// Center
            /// </summary>
            Center = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center,
            /// <summary>
            /// Vertical center
            /// </summary>
            VerticalCenter = System.Windows.Forms.DataVisualization.Charting.GradientStyle.VerticalCenter,
            /// <summary>
            /// Horizental center
            /// </summary>
            HorizontalCenter = System.Windows.Forms.DataVisualization.Charting.GradientStyle.HorizontalCenter,
            /// <summary>
            /// Left to right
            /// </summary>
            LeftRight = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight,
            /// <summary>
            /// Top to bottom
            /// </summary>
            TopBottom = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom,
            /// <summary>
            /// Diagnal left to right
            /// </summary>
            DiagonalLeft = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft,
            /// <summary>
            /// Diagonal right to left
            /// </summary>
            DiagonalRight = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight,
        }

        /// <summary>
        /// Fitting algorithm type when point sparse used 
        /// </summary>
        public enum FitType
        {
            /// <summary>
            /// No fitting
            /// </summary>
            None,

            /// <summary>
            /// Range fitting
            /// </summary>
            Range
        }

        /// <summary>
        /// StripChart scroll type
        /// </summary>
        public enum StripScrollType
        {
            /// <summary>
            /// 累积点数，到达最大点数时滚动
            /// </summary>
            Cumulation,

            /// <summary>
            /// 滚动模式
            /// </summary>
            Scroll,
        }

        /// <summary>
        /// X axis data type
        /// X轴数据类型
        /// </summary>
        public enum XAxisDataType
        {
            /// <summary>
            /// 数字索引
            /// </summary>
            Index,

            /// <summary>
            /// 时间戳
            /// </summary>
            TimeStamp,

            /// <summary>
            /// 字符串(用户自定义)
            /// </summary>
            String
        }

        /// <summary>
        /// Enum Display Mode
        /// 显示方向
        /// </summary>
        public enum ScrollDirection
        {
            /// <summary>
            /// From left to right
            /// 从左到右
            /// </summary>
            LeftToRight,

            /// <summary>
            /// From right to left
            /// 从右到左
            /// </summary>
            RightToLeft
        };

        #endregion
        
    }
}

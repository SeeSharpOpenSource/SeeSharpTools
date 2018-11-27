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
using SeeSharpTools.JY.GUI.EasyChartXData;
using SeeSharpTools.JY.GUI.EasyChartXEditor;
using SeeSharpTools.JY.GUI.TabCursorUtility;
using SeeSharpTools.JY.GUI.EasyChartXUtility;
using Control = System.Windows.Forms.Control;
using UserControl = System.Windows.Forms.UserControl;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// Chart to plot waveform(s).
    /// </summary>
    [Designer(typeof(EasyChartXDesigner))]
    [ToolboxBitmap(typeof(EasyChartX), "EasyChartX.EasyChartX.bmp")]
    [DefaultEvent("AxisViewChanged")]
    public partial class EasyChartX : UserControl
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
        private EasyChartXSeries _hitSeries;
        // 当前激活的绘图区
        private EasyChartXPlotArea _hitPlotArea;
        // 游标管理窗体的实例
        private TabCursorInfoForm _tabCursorForm;

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
        /// Get or specify whether enable cumulative.
        /// </summary>
        [
            Browsable(false),
            Category("Behavior"),
            Description("Get or specify whether use cumulative plot.")
        ]
        public bool Cumulitive
        {
            get
            {
                return _plotManager.CumulativePlot;
            }
            set
            {
                _plotManager.CumulativePlot = value;
            }
        }

        /// <summary>
        /// Get or specify whether check NaN data.
        /// </summary>
        [
            Obsolete,
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Category("Data"),
            Description("Get or specify whether check NaN data.")
        ]
        public bool CheckNaN
        {
            get
            {
                return _plotManager.DataCheckParams.CheckNaN;
            }

            set
            {
                _plotManager.DataCheckParams.CheckNaN = value;
            }
        }

        /// <summary>
        /// Get or specify whether check negtive or zero data.
        /// </summary>
        [
            Obsolete,
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Category("Data"),
            Description("Get or specify whether check negtive or zero data.")
        ]
        public bool CheckNegtiveOrZero
        {
            get
            {
                return _plotManager.DataCheckParams.CheckNegtiveOrZero;
            }

            set
            {
                _plotManager.DataCheckParams.CheckNegtiveOrZero = value;
            }
        }

        /// <summary>
        /// Get or specify whether check infinity data.
        /// </summary>
        [
            Obsolete,
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Category("Data"),
            Description("Get or specify whether check infinity data.")
        ]
        public bool CheckInfinity
        {
            get
            {
                return _plotManager.DataCheckParams.CheckInfinity;
            }

            set
            {
                _plotManager.DataCheckParams.CheckInfinity = value;
            }
        }

        /// <summary>
        /// Specify or get the fitting algorithm type when point sparse enabled.
        /// </summary>
        [
            Obsolete,
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Category("Data"),
            Description("Specify or get the fitting algorithm type when point sparse enabled."),
            EditorBrowsable(EditorBrowsableState.Never),
        ]
        public FitType Fitting
        {
            get { return _plotManager.FitType; }
            set { _plotManager.FitType = value; }
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
        public EasyChartXPlotAreaCollection SplitPlotArea => _chartViewManager.SplitPlotAreas;

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

        //停止该接口，后续如果需要直接通过AxisY去自行配置
        /// <summary>
        /// Specify whether auto Y axis range enabled
        /// </summary>
        [
            Browsable(false),
            Category("Behavior"),
            Description("Y axis auto scale")
        ]
        internal bool YAutoEnable
        {
            get
            {
                return _chartViewManager.MainPlotArea.AxisY.AutoScale;
            }

            set
            {
                _chartViewManager.MainPlotArea.AxisY.AutoScale = value;
                if (_chartViewManager.UseMainAreaConfig)
                {
                    _chartViewManager.ApplyMainPlotAreaToAll();
                }
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
        public EasyChartXAxis AxisX
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
        public EasyChartXAxis AxisY
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
        public EasyChartXAxis AxisX2
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
        public EasyChartXAxis AxisY2
        {
            get { return _chartViewManager.MainPlotArea.AxisY2; }
            set { _chartViewManager.MainPlotArea.AxisY2 = value; }
        }

        private EasyChartXAxis[] _axes = new EasyChartXAxis[4];
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
        public EasyChartXAxis[] Axes
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
        public EasyChartXCursor XCursor
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
        public EasyChartXCursor YCursor
        {
            get { return _chartViewManager.MainPlotArea.YCursor; }
            set { _chartViewManager.MainPlotArea.YCursor = value; }
        }

        private EasyChartXCursor[] _cursors = new EasyChartXCursor[2];
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
        public EasyChartXCursor[] Cursors
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
            Editor(typeof (EasyChartXLineSeriesEditor), typeof (UITypeEditor)),
            Category("Design"),
            Description("Specify or get the attribute of all series."),
            EditorBrowsable(EditorBrowsableState.Never)
        ]
        public EasyChartXLineSeries LineSeries => _plotManager.LineSeries;

        /// <summary>
        /// Get or set the series attributes.
        /// </summary>
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Editor(typeof (EasyChartXLineSeriesEditor), typeof (UITypeEditor)),
            Category("Design"),
            Description("Specify or get the attribute of all series."),
            EditorBrowsable(EditorBrowsableState.Always)
        ]
        public EasyChartXSeriesCollection Series => _plotManager.Series;

        /// <summary>
        /// Tabcursor container. This property is just used for design time.
        /// </summary>
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Editor(typeof(TabCursorCollectionEditor), typeof(UITypeEditor)),
            Category("Data"),
            Description("Tabcursor container. This property is just used for design time."),
            EditorBrowsable(EditorBrowsableState.Never)
        ]
        public TabCursorDesignTimeCollection TabCursorContainer { get; }

        /// <summary>
        /// TabCursor collection of EasyChartX.
        /// </summary>
        [
            Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Editor(typeof(TabCursorCollectionEditor), typeof(UITypeEditor)),
            Category("Data"),
            Description("TabCursor collection of EasyChartX."),
            EditorBrowsable(EditorBrowsableState.Always)
        ]
        public TabCursorCollection TabCursors { get; }

        /// <summary>
        /// Split view layout configure
        /// </summary>
        [
            Browsable(true),
            Category("Misc"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Editor(typeof(PropertyClonableClassEditor), typeof(UITypeEditor)),
            Description("Split view layout configure."),
            EditorBrowsable(EditorBrowsableState.Never)
        ]
        public MiscellaneousConfiguration Miscellaneous { get; internal set; }

        #endregion

        #region User event handler and event call

        /// <summary>
        /// EasyChartX axis view changing event delegate
        /// </summary>
        public delegate void ViewEvents(object sender, EasyChartXViewEventArgs e);

        /// <summary>
        /// EasyChartX cursor changed event delegate
        /// </summary>
        public delegate void CursorEvents(object sender, EasyChartXCursorEventArgs e);

        /// <summary>
        /// EasyChartX tabcursor changed event delegate
        /// </summary>
        public delegate void TabCursorEvent(object sender, TabCursorEventArgs e);

        /// <summary>
        /// EasyChartX plot event delegate
        /// </summary>
        public delegate void PlotEvent(object sender, EasyChartXPlotEventArgs e);

        /// <summary>
        /// Axis view changed event. Raised when scale view changed by mouse or user.
        /// </summary>
        [Description("Raised when EasyChart axis view changed.")]
        public event ViewEvents AxisViewChanged;

        internal void OnAxisViewChanged(EasyChartXAxis axis, bool isScaleViewChanged, bool isRaiseByMouseEvent)
        {
            if (null == AxisViewChanged)
            {
                return;
            }
            EasyChartXViewEventArgs eventArgs = new EasyChartXViewEventArgs();
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
        public event CursorEvents CursorPositionChanged;

        internal void OnCursorPositionChanged(EasyChartXCursor cursor, bool raiseByMouseEvent, int seriesIndex = -1)
        {
            if (null == CursorPositionChanged)
            {
                return;
            }
            EasyChartXCursorEventArgs eventArgs = new EasyChartXCursorEventArgs();
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

        internal void OnTabCursorChanged(TabCursor cursor, TabCursorOperation operation, EasyChartXPlotArea plotArea)
        {
            if (null == TabCursorChanged)
            {
                return;
            }
            TabCursorEventArgs eventArgs = new TabCursorEventArgs();
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
            BeforePlot?.Invoke(this, new EasyChartXPlotEventArgs() {ParentChart = this, IsClear = isClearOperation});
        }

        /// <summary>
        /// Event raised after plot data.
        /// </summary>
        [Description("Event raised after plot data.")]
        public event PlotEvent AfterPlot;

        internal void OnAfterPlot(bool isClearOperation)
        {
            AfterPlot?.Invoke(this, new EasyChartXPlotEventArgs() { ParentChart = this, IsClear = isClearOperation });
        }
        #endregion

        #region Constructor

        public EasyChartX()
        {
            InitializeComponent();
            // 设计器中自动配置了Name会导致在设计时获取控件名称失败
            this.Name = "";
            // EasyChartX中最核心的两个功能类：
            // _chartViewManager：管理chart的所有视图更新
            // _plotManager：管理线条的数据特性
            _plotManager = new PlotManager(this, _chart.Series);
            _chartViewManager = new ChartViewManager(this, _chart, _plotManager);

            _tabCursorForm = null;
            TabCursors = new TabCursorCollection(this, _chart, _chartViewManager.MainPlotArea);
            this.TabCursorContainer = new TabCursorDesignTimeCollection(TabCursors);


            _plotManager.AdaptSeriesCount();
            //更新
            _plotManager.AdaptPlotDatasCount(_plotManager.SeriesCount);

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
            // 将EasyChartX的事件绑定到chart上，保证用户添加的事件会被响应
            AddEasyChartXEvents();
            // 绘图结束后执行视图更新(主要执行分区视图的部分处理)
            _chart.PostPaint += ChartViewOnPostPaint;
            // 将控件本身的背景色和Chart的背景色绑定
            this.BackColor = _chart.BackColor;
            this.BackColorChanged += (sender, args) => { _chart.BackColor = this.BackColor; };
            // 字体和字体颜色与坐标轴的字体和字体颜色绑定
            this.ForeColorChanged += (sender, args) => { _chartViewManager.SetAxisLabelStyle();};
            this.FontChanged += (sender, args) => { _chartViewManager.SetAxisLabelStyle();};
        }
        
        private void AddEasyChartXEvents()
        {
            // 触发用户鼠标点击事件
            this._chart.Click += (sender, args) => OnClick(args);
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

        #region Double Array interface

        /// <summary>
        /// Plot single waveform y on chart, x will be generated using xStart and xIncrement.
        /// </summary>
        /// <param name="yData"> waveform to plot</param>
        /// <param name="xStart"> offset value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="xIncrement">increment value for generating x sequence using "offset + (Increment * i)"</param>
        public void Plot(double[] yData, double xStart = 0, double xIncrement = 1)
        {
            // Plot的流程
            // 1. 将绘图数据保存到plotmanager的PlotDatas的DataEntity结构中
            // 2. 根据绘图数据的线数匹配视图
            // 3. 根据待绘制的数据更新坐标轴和游标部分与数据相关的配置
            // 4. 根据坐标轴当前的缩放范围进行绘图
            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xStart, xIncrement, yData, yData.Length, yData.Length);

            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);

            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// Plot multiple waveforms on chart, x will be generated using xStart and xIncrement.
        /// </summary>
        /// <param name="yData"> waveforms to plot, each line in y[,] represents a single waveform</param>
        /// <param name="xStart">offset value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="xIncrement">increment value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="rowDirection">Specify whether plot data by rows.</param>
        public void Plot(double[,] yData, double xStart = 0, double xIncrement = 1, bool rowDirection = true)
        {
            // Plot的流程
            // 1. 将绘图数据保存到plotmanager的PlotDatas的DataEntity结构中
            // 2. 根据绘图数据的线数匹配视图
            // 3. 根据待绘制的数据更新坐标轴和游标部分与数据相关的配置
            // 4. 根据坐标轴当前的缩放范围进行绘图
            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xStart, xIncrement, yData, rowDirection);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// Plot x[] and y[] pair on chart.
        /// </summary>
        /// <param name="xData"> x sequence to plot</param>
        /// <param name="yData"> y sequence to plot</param>
        public void Plot(double[] xData, double[] yData)
        {
            // Plot的流程
            // 1. 将绘图数据保存到plotmanager的PlotDatas的DataEntity结构中
            // 2. 根据绘图数据的线数匹配视图
            // 3. 根据待绘制的数据更新坐标轴和游标部分与数据相关的配置
            // 4. 根据坐标轴当前的缩放范围进行绘图
            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xData, yData, yData.Length, yData.Length);

            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// Plot MutiDimension x and y data on chart.
        /// </summary>
        /// <param name="xData"> x sequences to plot</param>
        /// <param name="yData"> y sequences to plot</param>
        public void Plot(double[][] xData, double[][] yData)
        {
            // Plot的流程
            // 1. 将绘图数据保存到plotmanager的PlotDatas的DataEntity结构中
            // 2. 根据绘图数据的线数匹配视图
            // 3. 根据待绘制的数据更新坐标轴和游标部分与数据相关的配置
            // 4. 根据坐标轴当前的缩放范围进行绘图
            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xData, yData);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }
        /*
                /// <summary>
                /// Plot single waveform y on chart, x will be generated using xStart and xIncrement.
                /// </summary>
                /// <param name="yData"> waveform to plot</param>
                /// <param name="startTime"></param>
                /// <param name="sampleRate"></param>
                public void Plot(double[] yData, DateTime startTime, double sampleRate)
                {
                    int lastSeriesCount = _plotManager.SeriesCount;
                    _plotManager.AddPlotData(startTime, sampleRate, yData, yData.Length, yData.Length);

                    AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);

                    _chartViewManager.RefreshAxesAndCursors();
                    PlotDataInRange();
                }

                /// <summary>
                /// Plot multiple waveforms on chart, x will be generated using xStart and xIncrement.
                /// </summary>
                /// <param name="yData"> waveforms to plot, each line in y[,] represents a single waveform</param>
                /// <param name="startTime"></param>
                /// <param name="sampleRate"></param>
                public void Plot(double[,] yData, DateTime startTime, double sampleRate)
                {
                    int lastSeriesCount = _plotManager.SeriesCount;
                    _plotManager.AddPlotData(startTime, sampleRate, yData.Cast<double>(), yData.GetLength(1), yData.Length);
                    AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
                    _chartViewManager.RefreshAxesAndCursors();
                    PlotDataInRange();
                }
        */
        #endregion

        #region Double IList interface

        /// <summary>
        /// Plot one or more line data with xIncrement x
        /// </summary>
        /// <param name="yData">Y datas to plot</param>
        /// <param name="xStart">offset value for generating x sequence using "offset + (xIncrement * i)"</param>
        /// <param name="xIncrement">xIncrement value for generating x sequence using "offset + (xIncrement * i)"</param>
        /// <param name="xSize">X data size, when xSize smaller than 1 means only one line in yData</param>
        public void Plot(IList<double> yData, double xStart = 0, double xIncrement = 1, int xSize = 0)
        {
            int lastSeriesCount = _plotManager.SeriesCount;
            if (xSize <= 0)
            {
                xSize = yData.Count;
            }
            _plotManager.AddPlotData(xStart, xIncrement, yData, xSize, yData.Count);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// X-Y plot. Plot List x Data and list y data pair on chart. The series count is y.Count/x.Count.
        /// </summary>
        /// <param name="xData"> x sequence to plot</param>
        /// <param name="yData"> y sequence to plot</param>
        public void Plot(IList<double> xData, IList<double> yData)
        {
            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xData, yData, xData.Count, yData.Count);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// Plot MutiDimension x and y data on chart.
        /// </summary>
        /// <param name="xData"> x sequences to plot</param>
        /// <param name="yData"> y sequences to plot</param>
        public void Plot(IList<IList<double>> xData, IList<IList<double>> yData)
        {
            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xData, yData);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        #endregion

        #region Template interface

        // 当数据类型不是double时的类型转换器
        private DataConvertor _convertor;

        /// <summary>
        /// Plot single waveform y on chart, x will be generated using xStart and xIncrement. Supported data type: float/int/uint/short/ushort.
        /// </summary>
        /// <param name="yData"> waveform to plot. Supported data type: float/int/uint/short/ushort.</param>
        /// <param name="xStart"> offset value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="xIncrement">increment value for generating x sequence using "offset + (Increment * i)"</param>
        public void Plot<TDataType>(TDataType[] yData, double xStart = 0, double xIncrement = 1)
        {
            if (null == _convertor)
            {
                _convertor = new DataConvertor();
            }
            if (!_convertor.IsValidType(typeof(TDataType)))
            {
                return;
            }
            double[] convertedYData = _convertor.Convert(yData, yData.Length);

            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xStart, xIncrement, convertedYData, yData.Length, yData.Length);

            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);

            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// Plot multiple waveforms on chart, x will be generated using xStart and xIncrement. Supported data type: float/int/uint/short/ushort.
        /// </summary>
        /// <param name="yData"> waveforms to plot, each line in y[,] represents a single waveform. Supported data type: float/int/uint/short/ushort.</param>
        /// <param name="xStart">offset value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="xIncrement">increment value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="rowDirection">Specify whether plot data by rows.</param>
        public void Plot<TDataType>(TDataType[,] yData, double xStart = 0, double xIncrement = 1, bool rowDirection = true)
        {
            if (null == _convertor)
            {
                _convertor = new DataConvertor();
            }
            if (!_convertor.IsValidType(typeof(TDataType)))
            {
                return;
            }
            double[,] convertedYData = _convertor.Convert(yData, yData.GetLength(0), yData.GetLength(1));

            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xStart, xIncrement, convertedYData, rowDirection);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// Plot x[] and y[] pair on chart. Supported data type: float/int/uint/short/ushort.
        /// </summary>
        /// <param name="xData"> x sequence to plot, Supported data type: float/int/uint/short/ushort.</param>
        /// <param name="yData"> y sequence to plot, Supported data type: float/int/uint/short/ushort.</param>
        public void Plot<TDataType1, TDataType2>(TDataType1[] xData, TDataType2[] yData)
        {
            if (null == _convertor)
            {
                _convertor = new DataConvertor();
            }
            if (!_convertor.IsValidType(typeof(TDataType1)) || !_convertor.IsValidType(typeof(TDataType2)))
            {
                return;
            }
            double[] convertedXData = _convertor.Convert(xData, xData.Length);
            double[] convertedYData = _convertor.Convert(yData, yData.Length);

            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(convertedXData, convertedYData, yData.Length, yData.Length);

            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// Plot MutiDimension x and y data on chart. Supported data type: float/int/uint/short/ushort.
        /// </summary>
        /// <param name="xData"> x sequences to plot. Supported data type: float/int/uint/short/ushort.</param>
        /// <param name="yData"> y sequences to plot. Supported data type: float/int/uint/short/ushort.</param>
        public void Plot<TDataType1, TDataType2>(TDataType1[][] xData, TDataType2[][] yData)
        {
            if (null == _convertor)
            {
                _convertor = new DataConvertor();
            }
            if (!_convertor.IsValidType(typeof(TDataType1)) || !_convertor.IsValidType(typeof(TDataType2)))
            {
                return;
            }
            double[][] convertedXData = _convertor.Convert(xData);
            double[][] convertedYData = _convertor.Convert(yData);

            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(convertedXData, convertedYData);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        #endregion

        // TODO IList模板类接口暂时屏蔽
        #region Template IList interface
/*

        /// <summary>
        /// Plot one or more line data with xIncrement x
        /// </summary>
        /// <param name="yData">Y datas to plot</param>
        /// <param name="xStart">offset value for generating x sequence using "offset + (xIncrement * i)"</param>
        /// <param name="xIncrement">xIncrement value for generating x sequence using "offset + (xIncrement * i)"</param>
        /// <param name="xSize">X data size, when xSize smaller than 1 means only one line in yData</param>
        public void Plot<TDataType>(IList<TDataType> yData, double xStart = 0, double xIncrement = 1, int xSize = 0)
        {
            int lastSeriesCount = _plotManager.SeriesCount;
            if (xSize <= 0)
            {
                xSize = yData.Count;
            }
            _plotManager.AddPlotData(xStart, xIncrement, yData, xSize, yData.Count);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// Plot x[] and y[] pair on chart.
        /// </summary>
        /// <param name="xData"> x sequence to plot</param>
        /// <param name="yData"> y sequence to plot</param>
        public void Plot<TDataType>(IList<TDataType> xData, IList<TDataType> yData)
        {
            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xData, yData, yData.Count, yData.Count);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }

        /// <summary>
        /// Plot MutiDimension x and y data on chart.
        /// </summary>
        /// <param name="xData"> x sequences to plot</param>
        /// <param name="yData"> y sequences to plot</param>
        public void Plot<TDataType>(IList<IList<TDataType>> xData, IList<IList<TDataType>> yData)
        {
            int lastSeriesCount = _plotManager.SeriesCount;
            _plotManager.AddPlotData(xData, yData);
            AdaptPlotSeriesAndChartView(_plotManager.SeriesCount != lastSeriesCount);
            _chartViewManager.RefreshAxesAndCursors();
            PlotDataInRange();
        }
*/

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
            EasyChartXValueDisplayToolTip.RemoveAll();
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
            foreach (EasyChartXAxis axis in _chartViewManager.MainPlotArea.Axes)
            {
                axis.ZoomReset(resetTime);
            }
            foreach (EasyChartXPlotArea plotArea in _chartViewManager.SplitPlotAreas)
            {
                foreach (EasyChartXAxis axis in plotArea.Axes)
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
            EasyChartXAxis changedAxis = null;
            // X轴的缩放需要执行数据更新
            if (chartArea.AxisX.Name.Equals(axis.Name) || chartArea.AxisX2.Name.Equals(axis.Name))
            {
                // 非分区视图，且是主绘图区的缩放事件
                if (ReferenceEquals(_chartViewManager.MainPlotArea.ChartArea, chartArea) && !_chartViewManager.IsSplitView)
                {
                    changedAxis = _chartViewManager.MainPlotArea.AxisX;
                    changedAxis.RefreshXLabelFormat();
                    _plotManager.PlotDataInRange(axis.ScaleView.ViewMinimum, axis.ScaleView.ViewMaximum, false);
                }
                // 分区视图，副绘图区的缩放事件
                else if (_chartViewManager.IsSplitView)
                {
                    int seriesIndex = _chartViewManager.SplitPlotAreas.FindIndexByBaseChartArea(chartArea);
                    if (seriesIndex >= 0 && seriesIndex < _chartViewManager.SplitPlotAreas.Count)
                    {
                        changedAxis = _chartViewManager.SplitPlotAreas[seriesIndex].AxisX;
                        changedAxis.RefreshXLabelFormat();
                        _plotManager.PlotDataInRange(axis.ScaleView.ViewMinimum, axis.ScaleView.ViewMaximum, seriesIndex, false);
                        // 分区模式下，视图更新需要手动刷新Label
                        changedAxis.RefreshLabels();
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
                    _chartViewManager.MainPlotArea.AxisY.RefreshYMajorGridInterval();
                    _chartViewManager.MainPlotArea.AxisY2.RefreshYMajorGridInterval();
                }
                else
                {
                    int seriesIndex = _chartViewManager.SplitPlotAreas.FindIndexByBaseChartArea(chartArea);
                    if (seriesIndex >= 0 && seriesIndex < _chartViewManager.SplitPlotAreas.Count)
                    {
                        changedAxis = _chartViewManager.SplitPlotAreas[seriesIndex].AxisY;
                        _chartViewManager.SplitPlotAreas[seriesIndex].YAxisSync.SyncAxis();
                        // 分区视图更新后需要手动刷新Y轴的label
                        _chartViewManager.SplitPlotAreas[seriesIndex].AxisY.RefreshLabels();
                        _chartViewManager.SplitPlotAreas[seriesIndex].AxisY2.RefreshLabels();

                        _chartViewManager.SplitPlotAreas[seriesIndex].AxisY.RefreshYMajorGridInterval();
                        _chartViewManager.SplitPlotAreas[seriesIndex].AxisY2.RefreshYMajorGridInterval();
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
            this.EasyChartXValueDisplayToolTip.Hide(this._chart);
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
            EasyChartXSeriesMenu.Show(_chart, location);
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
            this.EasyChartXValueDisplayToolTip.Show(dispText, this._chart, clickPoint);
            this.OnCursorPositionChanged(_hitPlotArea.XCursor, true, seriesIndex);
        }

        private string MoveCursorAndShowValue(EasyChartXPlotArea hitPlotArea, int seriesIndex)
        {
            EasyChartXCursor xCursor = hitPlotArea.XCursor;
            EasyChartXCursor yCursor = hitPlotArea.YCursor;
            string dispText = string.Empty;
            int lineIndex;
            DataEntity cursorData = _plotManager.GetDataEntityBySeriesIndex(seriesIndex, out lineIndex);
            if (seriesIndex < 0 || null == cursorData)
            {
                return dispText;
            }
            double xValue = xCursor.Value;
            double yValue = yCursor.Value;
            if (hitPlotArea.AxisX.IsLogarithmic)
            {
                xValue = Math.Pow(10, xValue);
            }
            if (hitPlotArea.AxisY.IsLogarithmic)
            {
                yValue = Math.Pow(10, yValue);
            }
            int pointIndex = cursorData.FindeNearestIndex(ref xValue, ref yValue, lineIndex);
            if (pointIndex < 0)
            {
                return dispText;
            }

            EasyChartXAxis xAxis, yAxis;
            xAxis = EasyChartXAxis.PlotAxis.Primary == Series[seriesIndex].XPlotAxis
                ? hitPlotArea.AxisX
                : hitPlotArea.AxisX2;

            yAxis = EasyChartXAxis.PlotAxis.Primary == Series[seriesIndex].YPlotAxis
                ? hitPlotArea.AxisY
                : hitPlotArea.AxisY2;

            xCursor.Value = !xAxis.IsLogarithmic ? xValue : Math.Log10(xValue);
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
            EasyChartXFunctionMenu.Show(_chart, eventArgs.Location);
            
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
            ToolStripMenuItem_xAxisZoom.Checked = EasyChartXCursor.CursorMode.Zoom == _hitPlotArea.XCursor.Mode &&
                                                  EasyChartXCursor.CursorMode.Zoom != _hitPlotArea.YCursor.Mode;
            ToolStripMenuItem_yAxisZoom.Checked = EasyChartXCursor.CursorMode.Zoom != _hitPlotArea.XCursor.Mode &&
                                                  EasyChartXCursor.CursorMode.Zoom == _hitPlotArea.YCursor.Mode;
            ToolStripMenuItem_windowZoom.Checked = EasyChartXCursor.CursorMode.Zoom == _hitPlotArea.XCursor.Mode &&
                                                  EasyChartXCursor.CursorMode.Zoom == _hitPlotArea.YCursor.Mode;
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
            _tabCursorForm = new TabCursorInfoForm(this);
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
            EasyChartXValueDisplayToolTip.RemoveAll();
            if (null == _hitPlotArea)
            {
                return;
            }
            _hitPlotArea.XCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
            _hitPlotArea.YCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
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
                _hitPlotArea.XCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
                _hitPlotArea.YCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
            }
            else
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValueClear();
                ToolStripMenuItem_xAxisZoom.Checked = true;
                //                SetZoomable(true, false);
                _hitPlotArea.XCursor.Mode = EasyChartXCursor.CursorMode.Zoom;
                _hitPlotArea.YCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
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
                _hitPlotArea.XCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
                _hitPlotArea.YCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
            }
            else
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValueClear();
                ToolStripMenuItem_yAxisZoom.Checked = true;
                //                SetZoomable(false, true);
                _hitPlotArea.XCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
                _hitPlotArea.YCursor.Mode = EasyChartXCursor.CursorMode.Zoom;
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
                _hitPlotArea.XCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
                _hitPlotArea.XCursor.Mode = EasyChartXCursor.CursorMode.Disabled;
            }
            else
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValueClear();
                ToolStripMenuItem_windowZoom.Checked = true;
                _hitPlotArea.XCursor.Mode = EasyChartXCursor.CursorMode.Zoom;
                _hitPlotArea.YCursor.Mode = EasyChartXCursor.CursorMode.Zoom;
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
                _hitPlotArea.XCursor.Mode = EasyChartXCursor.CursorMode.Cursor;
                _hitPlotArea.YCursor.Mode = EasyChartXCursor.CursorMode.Cursor;
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
            EasyChartXSeries.LineWidth lineWidth;
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
            EasyChartXSeries.LineType interpolation;
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
            EasyChartXSeries.MarkerType markerType;
            if (null != menuItem.Tag && Enum.TryParse(menuItem.Tag.ToString(), out markerType))
            {
                _hitSeries.Marker = markerType;
            }
        }

        private void setYAxisRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form yAxisRangeForm = new EasyChartXRangeYConfigForm(_hitPlotArea);
            Point clickPoint = this._chart.PointToClient(Control.MousePosition);//鼠标相对于窗体左上角的坐标
            clickPoint = new Point(clickPoint.X, clickPoint.Y);
            yAxisRangeForm.StartPosition = FormStartPosition.Manual;
            yAxisRangeForm.Location = clickPoint;
            yAxisRangeForm.Show();
        }

        #region Callback function for other module

        internal void RefreshAxesRange(EasyChartXPlotArea plotArea)
        {
            _chartViewManager.RefreshAxesRange(plotArea);
        }

        internal Series GetCursorSeries(EasyChartXPlotArea plotArea)
        {
            int index = ReferenceEquals(_chartViewManager.MainPlotArea, plotArea)
                ? GetCursorSeriesIndex()
                : _chartViewManager.SplitPlotAreas.IndexOf(plotArea);
            return index >= 0 ? _plotManager.PlotSeries[index] : null;
        }

        internal void ShowDynamicValue(string showInfo, Point position, bool isShow)
        {
            if (isShow)
            {
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

        private bool IsCursorMode(EasyChartXPlotArea plotArea)
        {
            if (null == plotArea)
            {
                return false;
            }
            return EasyChartXCursor.CursorMode.Cursor == _hitPlotArea.XCursor.Mode &&
                   EasyChartXCursor.CursorMode.Cursor == _hitPlotArea.YCursor.Mode;
        }

        internal int GetNearestPoint(ref double xValue, out double yValue, int seriesIndex)
        {
            yValue = double.NaN;
            int lineIndex;
            DataEntity cursorData = _plotManager.GetDataEntityBySeriesIndex(seriesIndex, out lineIndex);
            return cursorData.FindeNearestIndex(ref xValue, ref yValue, lineIndex);
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
        /// Gradient Style of EasyChart
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
        #endregion
    }
}

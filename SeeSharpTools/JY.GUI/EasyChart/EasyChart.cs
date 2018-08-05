using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using SeeSharpTools.JY.GUI.EasyChartComponents;
using SeeSharpTools.JY.GUI.EasyChartEditor;
using SeeSharpTools.JY.GUI.EasyChartEvents;
using Control = System.Windows.Forms.Control;
using SeriesCollection = System.Windows.Forms.DataVisualization.Charting.SeriesCollection;
using UserControl = System.Windows.Forms.UserControl;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// Chart to plot waveform(s).
    /// </summary>
    [Designer(typeof(EasyChartDesigner))]
    [ToolboxBitmap(typeof(EasyChart), "EasyChart.EasyChart.bmp")]
    [DefaultEvent("AxisViewChanged")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public partial class EasyChart : UserControl
    {
        #region Private Fields

        // Maximum number of Series to draw on Chart
        /// <summary>
        ///  chart constants
        /// </summary>
        internal const int maxSeriesToDraw = 32;

        /// <summary>
        /// Color and names for series
        /// </summary>
        private Color[] _palette = new Color[8] { Color.Red, Color.Blue, Color.DeepPink, Color.Navy, Color.DarkGreen, Color.OrangeRed, Color.DarkCyan, Color.Black };
        private Color[] _seriesColors = new Color[maxSeriesToDraw];
        private string[] _seriesNames = new string[maxSeriesToDraw];

        /// <summary>
        /// X/Y axis property
        /// </summary>
        private bool _fixAxisX = true;
        
        private bool _xIsLogarithmic = false;
        private bool _yIsLogarithmic = false;
        /// <summary>
        /// Legend property
        /// </summary>
        private bool _legendVisibe = true;
        // Keep number of series currently on chart.
        private int _numberOfActiveSeries = 1;
        /// <summary>
        /// X Axis range max
        /// </summary>
        private double _xMax = double.MaxValue;
        /// <summary>
        /// X Axis range min
        /// </summary>
        private double _xMin = double.MinValue;
        //当前Plot数据的类型
        private PlotMode _plotMode = PlotMode.NaN;
        List<double[]> _yof1D = new List<double[]>();
        List<double[,]> _yof2D = new List<double[,]>();
        List<double[]> _xof1DXY = new List<double[]>();
        List<double[]> _yof1DXY = new List<double[]>();
        List<double[][]> _xof2DXY = new List<double[][]>();
        List<double[][]> _yof2DXY = new List<double[][]>();
        private double _xStart;
        private double _xIncrement;
        private int _xDatalength = 0;
        //First time Run mark of Plot1Dy,Plot2Dy,Plot1Dxy,Plot2Dxy
        bool[] _fistTimeRun = new bool[4] { true, true, true, true };
        
//        //CSV Templaete[]
//        private double[] x1DCSVdata; //X轴数据CSV临时缓存
//        private double[][] x2DCSVdata; //X轴数据CSV临时缓存
//        //CSV Templaete[]  City修改
//        private double[] y1DCSVdata; //CSV临时数据类型
//        // double[,] y2DCSVdata;
//        private List<double[]> y2DCSVdata = new List<double[]>();
        
        private List<DataEntity> _plotDatas = new List<DataEntity>(maxSeriesToDraw);
        
        private bool zoomCSVFlag = false;

        //配置每条线的属性内容
        private int currentClickCurve;//单击选择内容（0~31）
        //Y轴的最大值最小值的配置
        private int _xStartIndex = int.MinValue;
        private int _xEndIndex = int.MaxValue;

        //ShowXYValue的Series配置菜单项
        private readonly List<ToolStripMenuItem> _cursorSeriesMenuItems = new List<ToolStripMenuItem>(maxSeriesToDraw);

        #endregion   //Private Fields

        #region Public Properties
        /// <summary>
        /// Set the BackColor of EasyChart.
        /// </summary>
        [
            Browsable(false),
            CategoryAttribute("Appearance"),
            EditorBrowsable(EditorBrowsableState.Never),
            Description("Set the BackColor of EasyChart.")
        ]
        public override Color BackColor
        {
            get
            {
                return _chart.BackColor;
            }

            set
            {
                _chart.BackColor = value;
            }
        }
        public Color EasyChartBackColor
        {
            get
            {
                return _chart.BackColor;
            }

            set
            {
                _chart.BackColor = value;
            }
        }
        /// <summary>
        /// Set the  BackColor of ChartAreaBackColor
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Set the BackColor of ChartArea.")      
        ]
        public Color ChartAreaBackColor
        {
            get
            {
                return _chart.ChartAreas[0].BackColor;
            }

            set
            {
                _chart.ChartAreas[0].BackColor = value;
            }

        }
        /// <summary>
        /// Set the  BackColor of LegendBackColor
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
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

        // Reserved property to fix DataVisualization reference error
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            EditorBrowsable(EditorBrowsableState.Never)
        ]
        public object BackGradientStyle
        {
            get
            {
                return null;
            }
            set
            {
                if (value is GradientStyle)
                {
                    GradientStyle = (EasyChartGradientStyle) value;
                }
            }
        }

        /// <summary>
        /// Set the  style of GradientStyle
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Set the  style of BackGradientStyle.")
        ]
        public EasyChartGradientStyle GradientStyle
        {
            get
            {
                return (EasyChartGradientStyle)_chart.BackGradientStyle;
            }
            set
            {
                _chart.BackGradientStyle = (GradientStyle) value;
            }
        }

        /// <summary>
        /// 修改Name名称
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Set the  name of series."),
            EditorBrowsable(EditorBrowsableState.Always)
        ]
        public string[] SeriesNames
        {
            get
            {
                return _seriesNames;
            }

            set
            {
                // _seriesNames 
                string[] tempNames = value;
                for (int i = 0; i < tempNames.Length; i++)
                {
                    _seriesNames[i] = tempNames[i];
                }

                for (int i = 0; i < maxSeriesToDraw - tempNames.Length; i++)
                {
                    _seriesNames[i + tempNames.Length] = "Series" + (i + 1 + tempNames.Length);
                }

                SetSeriesNames();
            }
        }

        /// <summary>
        /// Fix Axis X range.
        /// </summary>
        [
            Browsable(true),
            Description("Fix Axis X range."),
            DefaultValue(true)
        ]
        public bool FixAxisX
        {
            get
            {
                return _fixAxisX;
            }
            set
            {
                _fixAxisX = value;
            }
        }

        /// <summary>
        /// Color palette, the colors in palette will be applied to series in sequence.
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify a series of colors. Colors in palette will be set to each series in sequence."),
            EditorBrowsable(EditorBrowsableState.Always)
        ]
        public Color[] Palette
        {
            get { return _palette; }
            set
            {
                if (value.Length > 0)
                {
                    _palette = value;
                    SetSeriesColor();
                }
                else
                {
                    MessageBox.Show("Palette can NOT be empty.");
                }
            }
        }

        private void SetSeriesColor()
        {
            // Create colors for series according to palette and set to chart
            for (int i = 0; i < _chart.Series.Count; i++)
            {
                _chart.Series[i].Color = _palette[i%_palette.Length];
            }
        }

        /// <summary>
        /// Set X axis logarithmic.
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Data"),
            Description("Specify X axis is linear or logarithmic scale.")
        ]
        public bool XAxisLogarithmic
        {
            get { return _xIsLogarithmic; }
            set
            {
                _xIsLogarithmic = value;
                SetAxisMappingType();
            }
        }

        /// <summary>
        /// Set Y axis logarithmic.
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Data"),
            Description("Specify Y axis is linear or logarithmic scale.")
        ]
        public bool YAxisLogarithmic
        {
            get { return _yIsLogarithmic; }
            set
            {
                _yIsLogarithmic = value;
                SetAxisMappingType();
            }
        }

        /// <summary>
        /// Set Legend visible.
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify whether legend is visible.")
        ]
        public bool LegendVisible
        {
            get { return _legendVisibe; }
            set
            {
                _legendVisibe = value;
                SetLegendVisible();
            }
        }
        /// <summary>
        /// set double.NaN is auto scale
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Data"),
            Description("Y axis maximum value")
        ]
        public double AxisYMax
        {
            get { return _axisY.Maximum; }

            set { _axisY.Maximum = value; }
        }
        /// <summary>
        /// set double.NaN is auto scale
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Data"),
            Description("Y axis minimum value")
        ]
        public double AxisYMin
        {
            get { return _axisY.Minimum; }

            set { _axisY.Minimum = value; }
        }

        /// <summary>
        /// Specify whether auto Y axis range enabled
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Y axis auto scale")
        ]
        public bool YAutoEnable
        {
            get
            {
                return _axisY.AutoScale;
            }

            set
            {
                _axisY.AutoScale = value;
            }
        }

        /// <summary>
        /// Is major grid enable
        /// 是否使能Major Grid
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Indicate whether minor grid lines are shown.")
        ]

        public bool MajorGridEnabled
        {
            get { return _chart.ChartAreas[0].AxisX.MajorGrid.Enabled && _chart.ChartAreas[0].AxisY.MajorGrid.Enabled; }
            set
            {
                _chart.ChartAreas[0].AxisX.MajorGrid.Enabled = value;
                _chart.ChartAreas[0].AxisY.MajorGrid.Enabled = value;
            }
        }

        /// <summary>
        /// major grid color
        /// 是否使能Major Grid
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the color of major grids.")
        ]

        public Color MajorGridColor
        {
            get { return _chart.ChartAreas[0].AxisX.MajorGrid.LineColor; }
            set
            {
                _chart.ChartAreas[0].AxisX.MajorGrid.LineColor = value;
                _chart.ChartAreas[0].AxisY.MajorGrid.LineColor = value;
            }
        }

        /// <summary>
        /// Is minor grid enable
        /// 是否使能MinorGrid
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Indicate whether minor grid lines are shown.")
        ]

        public bool MinorGridEnabled
        {
            get { return _chart.ChartAreas[0].AxisX.MinorGrid.Enabled && _chart.ChartAreas[0].AxisY.MinorGrid.Enabled; }
            set
            {
                _chart.ChartAreas[0].AxisX.MinorGrid.Enabled = value;
                _chart.ChartAreas[0].AxisY.MinorGrid.Enabled = value;
            }
        }

        /// <summary>
        /// minor grid color
        /// 是否使能MinorGrid
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the color of minor grids.")
        ]

        public Color MinorGridColor
        {
            get { return _chart.ChartAreas[0].AxisX.MinorGrid.LineColor; }
            set
            {
                _chart.ChartAreas[0].AxisX.MinorGrid.LineColor = value;
                _chart.ChartAreas[0].AxisY.MinorGrid.LineColor = value;
            }
        }

        /// <summary>
        /// minor grid line type
        /// MinorGrid线条类型
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the color of minor grids.")
        ]
        public GridStyle MinorGridType
        {
            get { return (GridStyle) _chart.ChartAreas[0].AxisX.MinorGrid.LineDashStyle; }
            set
            {
                _chart.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = (ChartDashStyle)value;
                _chart.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = (ChartDashStyle)value;
            }
        }

        /// <summary>
        /// X axis title
        /// X轴名称
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the title of X axis.")
        ]
        public string XAxisTitle
        {
            get { return _chart.ChartAreas[0].AxisX.Title; }
            set { _chart.ChartAreas[0].AxisX.Title = value; }
        }

        /// <summary>
        /// Y axis title
        /// Y轴名称
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the title of Y axis.")
        ]
        public string YAxisTitle
        {
            get { return _chart.ChartAreas[0].AxisY.Title; }
            set { _chart.ChartAreas[0].AxisY.Title = value; }
        }

        /// <summary>
        /// X axis title position
        /// X轴标题位置
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the position of X axis title.")
        ]
        public TitlePosition XTitlePosition
        {
            get { return (TitlePosition) _chart.ChartAreas[0].AxisX.TitleAlignment; }
            set { _chart.ChartAreas[0].AxisX.TitleAlignment = (StringAlignment) value; }
        }

        /// <summary>
        /// Y axis title position
        /// Y轴名称位置
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the position of Y axis title.")
        ]
        public TitlePosition YTitlePosition
        {
            get { return (TitlePosition) _chart.ChartAreas[0].AxisY.TitleAlignment; }
            set { _chart.ChartAreas[0].AxisY.TitleAlignment = (StringAlignment) value; }
        }

        /// <summary>
        /// X axis title orientation
        /// X轴标题方向
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the orientation of X axis title.")
        ]
        public TitleOrientation XTitleOrientation
        {
            get { return (TitleOrientation)_chart.ChartAreas[0].AxisX.TextOrientation; }
            set { _chart.ChartAreas[0].AxisX.TextOrientation = (TextOrientation)value; }
        }

        /// <summary>
        /// Y axis title orientation
        /// Y轴名称方向
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the orientation of Y axis title.")
        ]
        public TitleOrientation YTitleOrientation
        {
            get { return (TitleOrientation)_chart.ChartAreas[0].AxisY.TextOrientation; }
            set { _chart.ChartAreas[0].AxisY.TextOrientation = (TextOrientation)value; }
        }

        private EasyChartAxis _axisX;
        /// <summary>
        /// X Axis
        /// </summary>
        [
            Browsable(false),
            BindableAttribute(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            CategoryAttribute("Design"),
            PersistenceMode(PersistenceMode.InnerProperty),
            Description("Set or get the X axis attributes.")
        ]
        public EasyChartAxis AxisX
        {
            get { return _axisX; }
            set
            {
                _axisX = value;
                _axisX.Initialize(this, _chart.ChartAreas[0].AxisX);
                _axes[0] = _axisX;
            }
        }

        private EasyChartAxis _axisY;
        /// <summary>
        /// Y Axis
        /// </summary>
        [
            Browsable(false),
            BindableAttribute(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            CategoryAttribute("Design"),
            PersistenceMode(PersistenceMode.InnerProperty),
            Description("Set or get the Y axis attributes."),
        ]
        public EasyChartAxis AxisY
        {
            get { return _axisY;}
            set { _axisY = value; }
        }

        private EasyChartAxis[] _axes = new EasyChartAxis[2];
        [
            Browsable(true),
            BindableAttribute(true),
            Editor(typeof (ArrayEditor), typeof (UITypeEditor)),
            CategoryAttribute("Design"),
            Description("Specify or get the attribute of X and Y Axis."),
            EditorBrowsable(EditorBrowsableState.Never),
            DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden),
        ]
        public EasyChartAxis[] Axes
        {
            get { return _axes; }
            set
            {
                AxisX = value[0];
                AxisY = value[1];
            }
        }

        private EasyChartCursor _xCursor;
        /// <summary>
        /// X Cursor
        /// </summary>
        [
            Browsable(false),
            BindableAttribute(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            CategoryAttribute("Design"),
            Description("Set or get the X axis attributes.")
        ]
        public EasyChartCursor XCursor
        {
            get { return _xCursor; }
            set { _xCursor = value; }
        }

        private EasyChartCursor _yCursor;
        /// <summary>
        /// Y Cursor
        /// </summary>
        [
            Browsable(false),
            BindableAttribute(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            CategoryAttribute("Design"),
            Description("Set or get the Y axis attributes.")
        ]
        public EasyChartCursor YCursor
        {
            get { return _yCursor; }
            set { _yCursor = value; }
        }

        private EasyChartCursor[] _cursors = new EasyChartCursor[2];

        [
            Browsable(true),
            BindableAttribute(true),
            Editor(typeof (ArrayEditor), typeof (UITypeEditor)),
            CategoryAttribute("Design"),
            Description("Specify or get the attribute of X and Y Axis."),
            EditorBrowsable(EditorBrowsableState.Never),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public EasyChartCursor[] Cursors
        {
            get { return _cursors;}
            set
            {
                XCursor = value[0];
                YCursor = value[1];
            }
        }
        
        [
            Browsable(true),
//            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            PersistenceMode(PersistenceMode.InnerProperty),
            Editor(typeof(EasyChartSeriesEditor), typeof(UITypeEditor)),
            Category("Design"),
            Description("Specify or get the attribute of X and Y Axis."),
//            EditorBrowsable(EditorBrowsableState.Never)
        ]

//        public EasyChartSeriesEditorCollection LineSeries { get; set; }
        public EasyChartSeriesCollection LineSeries { get; set; }

        #endregion

        #region User event handler and event call

        public delegate void ViewEvents(object sender, EasyChartViewEventArgs e);
        public delegate void CursorEvents(object sender, EasyChartCursorEventArgs e);
        [Description("Raised when EasyChart axis view changed.")]
        public event ViewEvents AxisViewChanged;

        internal void OnAxisViewChanged(EasyChartAxis axis, bool isScaleViewChanged, bool isRaiseByMouseEvent)
        {
            if (null == AxisViewChanged)
            {
                return;
            }
            EasyChartViewEventArgs eventArgs = new EasyChartViewEventArgs();
            eventArgs.Axis = axis;
            eventArgs.IsScaleViewChanged = isScaleViewChanged;
            eventArgs.IsRaisedByMouseEvent = isRaiseByMouseEvent;
            eventArgs.ParentChart = this;
            AxisViewChanged(axis, eventArgs);
        }

        public event CursorEvents CursorPositionChanged;

        internal void OnCursorPositionChanged(EasyChartCursor cursor, bool raiseByMouseEvent)
        {
            if (null == CursorPositionChanged)
            {
                return;
            }
            EasyChartCursorEventArgs eventArgs = new EasyChartCursorEventArgs();
            eventArgs.Cursor = cursor;
            eventArgs.IsRaisedByMouseEvent = raiseByMouseEvent;
            eventArgs.ParentChart = this;
            CursorPositionChanged(cursor, eventArgs);
        }

        #endregion

        #region Constructor
        //double[] 
        /// <summary>
        /// Set default style of chart.
        /// </summary>
        public EasyChart()
        {
            InitializeComponent();
            // 设计器中自动配置了Name会导致在设计时获取控件名称失败
            this.Name = "";
            // Initialize series classes
            const string seriesNameFormat = "Series{0}";
            LineSeries = new EasyChartSeriesCollection(_chart.Series);

            // Initialize axis classes
            ChartArea chartArea = _chart.ChartAreas[0];
            _axisX = new EasyChartAxis();
            _axisX.Initialize(this, chartArea.AxisX);
            _axisY = new EasyChartAxis();
            _axisY.Initialize(this, chartArea.AxisY);
            _axes[0] = _axisX;
            _axes[1] = _axisY;

            // Initialize cursor classes
            _xCursor = new EasyChartCursor(this, chartArea.CursorX, chartArea.AxisX, "X cursor");
            _xCursor.Mode = EasyChartCursor.CursorMode.Zoom;
            _yCursor = new EasyChartCursor(this, chartArea.CursorY, chartArea.AxisY, "Y cursor");
            _yCursor.Mode = EasyChartCursor.CursorMode.Disabled;
            _cursors[0] = _xCursor;
            _cursors[1] = _yCursor;

            // Plot default one cycle sine wave, the sine wave has DC offset so that all values are positive.
            Clear();

            // Create default names for series and set to chart
            for (int i = 0; i < maxSeriesToDraw; i++) { _seriesNames[i] = "Series" + (i + 1); }
            SetSeriesNames();
            SetSeriesColor();

            //在窗体缩放时时判断X轴缩放还是Y轴缩放
            chartArea.AxisX.Tag = "AxisX";
            chartArea.AxisY.Tag = "AxisY";
            chartArea.CursorY.Interval = 1e-3;

            AddEasyChartEvents();
        }

        private void AddEasyChartEvents()
        {
            // 触发用户鼠标点击事件
            this._chart.Click += (sender, args) => OnClick(args);
            this._chart.MouseClick += (sender, args) => OnMouseClick(args);
            this._chart.MouseUp += (sender, args) => OnMouseUp(args);
            this._chart.MouseDown += (sender, args) => OnMouseDown(args);
        }

        #endregion  // Constructor

        #region Public Methods
        /// <summary>
        /// Plot single waveform y on chart, x will be generated using xStart and xIncrement.
        /// </summary>
        /// <param name="y"> waveform to plot</param>
        /// <param name="xStart"> offset value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="xIncrement">increment value for generating x sequence using "offset + (Increment * i)"</param>
        public void Plot(double[] y, double xStart = 0, double xIncrement = 1)
        {
            bool firstTimeRun = (PlotMode.NaN == _plotMode);
            bool isXZoomed = (PlotMode.NaN == _plotMode || _axisX.IsZoomed);
            bool isYZoomed = (PlotMode.NaN == _plotMode || _axisY.IsZoomed);
            _plotMode = PlotMode.Yof1D;
            _yof1D.Clear();
            _yof1D.Add(y);
            _tranBuf = null;
            _xStart = xStart;
            _xIncrement = xIncrement;
            if (_xDatalength != y.Length||!FixAxisX)
            {
                ZoomReset();
                isXZoomed = false;
            }

            _xDatalength = y.Length;

            if (_fistTimeRun[0])
            {
                double[] x, zoomedY, selectedY;      // for x sequence
                int i, startIndex;
                double zoomRate;
                //第一次需要全部复制，CSV数据进行指针赋值
                _xStartIndex = 0;
                _xEndIndex = y.Length - 1;
                if (y.Length < 4000)
                {
                    // Generate x sequence according to xStart and xIncrement 
                    x = new double[y.Length];
                    for (i = 0; i < x.Length; i++) { x[i] = xStart + xIncrement * i; }

                    // Plot x,y at Series[0]
                    BindXYData(y, x, 0);
                }
                else
                {   //Zoom y sequence if y.length >= 4000.
                    zoomRate = 1;
                    //
                    if (((xStart <= _xMin) && (_xMax <= (xStart + (y.Length - 1) * xIncrement)))
                        || (((xStart + (y.Length - 1) * xIncrement) <= _xMin) && (_xMax <= xStart)))
                    {
                        startIndex = 0;
                        selectedY = SelectYInRange(y);
                        zoomedY = ZoomResults(selectedY, ref zoomRate);

                        // Generate x sequence according to xStart and xIncrement 
                        x = new double[zoomedY.Length];
                        for (i = 0; i < x.Length; i++) { x[i] = xStart + xIncrement * startIndex + xIncrement * zoomRate * i; }
                    }
                    //
                    else
                    {
                        zoomedY = ZoomResults(y, ref zoomRate);

                        // Generate x sequence according to xStart and xIncrement 
                        x = new double[zoomedY.Length];
                        for (i = 0; i < x.Length; i++) { x[i] = xStart + xIncrement * zoomRate * i; }
                    }

                    // Plot x,y at Series[0]
                    BindXYData(zoomedY, x, 0);
                    // AxisX set zoom min value

                }

                // Remove rest Series[] if any
                for (i = 1; i < _numberOfActiveSeries; i++) { _chart.Series.RemoveAt(1); }

                // Set minimum of X axis to 0 if xStart is 0 and logarithmic is set to false.
                // Manually set this because in this case chart will defaulty display "-1" as X axis start due to autoscale.
                if ((x[0] == 0) && (xIncrement > 0) && (!_xIsLogarithmic))
                { _chart.ChartAreas[0].AxisX.Minimum = 0; }

                // Update _numberOfActiveSeries variable
                _numberOfActiveSeries = 1;

                _fistTimeRun[0] = false;
            }
            else
            {
                PlotInternal(y, xStart, xIncrement);
            }
            _chart.ChartAreas[0].CursorX.Interval = xIncrement;

            _plotDatas.Clear();
            _plotDatas.Add(new DataEntity(xStart, xIncrement, y.Length, y, y.Length));
            RefreshAxesAndCursor(isXZoomed, isYZoomed, firstTimeRun);
            AdaptLineSeries();
        }

        private double[] _tranBuf = null;

        /// <summary>
        /// Plot multiple waveforms on chart, x will be generated using xStart and xIncrement.
        /// </summary>
        /// <param name="y"> waveforms to plot, each line in y[,] represents a single waveform</param>
        /// <param name="xStart">offset value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="xIncrement">increment value for generating x sequence using "offset + (Increment * i)"</param>
        public void Plot(double[,] y, double xStart = 0, double xIncrement = 1)
        {
            bool firstTimeRun = (PlotMode.NaN == _plotMode);
            bool isXZoomed = (PlotMode.NaN == _plotMode || _axisX.IsZoomed);
            bool isYZoomed = (PlotMode.NaN == _plotMode || _axisY.IsZoomed);
            _plotMode = PlotMode.Yof2D;
            _yof2D.Clear();
            _yof2D.Add(y);
            _xStart = xStart;
            _xIncrement = xIncrement;
            if (_xDatalength != y.GetLength(1) || !FixAxisX)
            {
                ZoomReset();
                isXZoomed = false;
            }
            _xDatalength = y.GetLength(1);

            if (_fistTimeRun[1])
            {
                double[] x;                    // for x sequence
                double[] waveform, zoomedWaveform;      // for getting each line in Y[,]
                int numberOfWaveforms, i, j, startIndex;
                double zoomRate;

                //CSV文档临时写入
                _xStartIndex = 0;
                _xEndIndex = y.GetLength(1) - 1;
                // if number of waveform in y[,] is greater than maxSeriesToDraw, only draw "maxSeriesToDraw" waveforms.
                numberOfWaveforms = Math.Min(y.GetLength(0), maxSeriesToDraw);

                // Add Series[] for each waveform if not added before 
                for (i = _numberOfActiveSeries; i < numberOfWaveforms; i++)
                {
                    // new added Series[i] uses name from predefiend name list.
                    _chart.Series.Add(_seriesNames[i]);
                    //  new added Series[i] uses the same ChartType as Series[0].
                    _chart.Series[i].ChartType = _chart.Series[0].ChartType;
                }
                SetSeriesColor();

                //If row number of waveform in y[,] is less than 4000, display without zoom.
                if (y.GetLength(1) < 4000)
                {
                    // Generate x sequence according to xStart and xIncrement 
                    x = new double[y.GetLength(1)];
                    for (i = 0; i < x.Length; i++) { x[i] = xStart + xIncrement * i; }

                    // plot Y[,] by lines
                    waveform = new double[y.GetLength(1)];
                    for (i = 0; i < numberOfWaveforms; i++)
                    {
                        for (j = 0; j < y.GetLength(1); j++) { waveform[j] = y[i, j]; }
                        BindXYData(waveform, x, i);
                    }
                }
                else // If row number of waveform in y[,] is >=4000, zoom and display.
                {
                    // plot Y[,] by lines
                    zoomRate = 1;
                    startIndex = 0;
                    waveform = new double[y.GetLength(1)];
                    Buffer.BlockCopy(y, 0, waveform, 0, waveform.Length * sizeof(double));
                    zoomedWaveform = ZoomResults(waveform, ref zoomRate);

                    // Generate x sequence according to xStart and xIncrement 
                    x = new double[zoomedWaveform.Length];
                    for (i = 0; i < x.Length; i++) { x[i] = xStart + xIncrement * zoomRate * (i + startIndex); }
                    BindXYData(zoomedWaveform, x, 0);

                    for (i = 1; i < numberOfWaveforms; i++)
                    {
                        Buffer.BlockCopy(y, waveform.Length * sizeof(double) * i, waveform, 0, waveform.Length * sizeof(double));
                        zoomedWaveform = ZoomResults(waveform, ref zoomRate);
                        BindXYData(zoomedWaveform, x, i);
                    }
                }

                // Remove rest Series[] if any
                for (i = numberOfWaveforms; i < _numberOfActiveSeries; i++)
                { _chart.Series.RemoveAt(numberOfWaveforms); }

                // Set minimum of X axis to 0 if xStart is 0 and logarithmic is set to false.
                // Manually set this because in this case chart will defaulty display "-1" as X axis start due to autoscale.
                if ((xStart == 0) && (xIncrement > 0) && (!_xIsLogarithmic))
                { _chart.ChartAreas[0].AxisX.Minimum = 0; }

                // Update _numberOfActiveSeries variable
                _numberOfActiveSeries = numberOfWaveforms;
                _fistTimeRun[1] = false;
            }
            else
            {
                PlotInternal(y, xStart, xIncrement);
            }
            _chart.ChartAreas[0].CursorX.Interval = xIncrement;

            _plotDatas.Clear();
            if (null == _tranBuf || _tranBuf.Length != y.Length)
            {
                _tranBuf = new double[y.Length];
            }
            Buffer.BlockCopy(y, 0, _tranBuf, 0, y.Length * sizeof(double));
            _plotDatas.Add(new DataEntity(xStart, xIncrement, y.GetLength(1), _tranBuf, y.Length));
            RefreshAxesAndCursor(isXZoomed, isYZoomed, firstTimeRun);
            AdaptLineSeries();
        }

        /// <summary>
        /// Plot x[] and y[] pair on chart.
        /// </summary>
        /// <param name="x"> x sequence to plot</param>
        /// <param name="y"> y sequence to plot</param>
        public void Plot(double[] x, double[] y)
        {
            bool firstTimeRun = (PlotMode.NaN == _plotMode);
            bool isXZoomed = (PlotMode.NaN == _plotMode || _axisX.IsZoomed);
            bool isYZoomed = (PlotMode.NaN == _plotMode || _axisY.IsZoomed);
            _plotMode = PlotMode.XYof1D;
            _xof1DXY.Clear();
            _yof1DXY.Clear();
            _xof1DXY.Add(x);
            _yof1DXY.Add(y);
            _tranBuf = null;
            if (_xDatalength != y.Length || !FixAxisX)
            {
                ZoomReset();
                isXZoomed = false;
            }
            _xDatalength = y.Length;

            if (_fistTimeRun[2])
            {
                double[] zoomedX, zoomedY;

                if (x.Length < 4000 && y.Length < 4000)
                {
                    // Plot x,y at Series[0]
                    BindXYData(y, x, 0);
                }
                else
                {
                    zoomedX = ZoomResultsWithoutCaculate(x);
                    zoomedY = ZoomResultsWithoutCaculate(y);
                    BindXYData(zoomedY, zoomedX, 0);
                }

                // Remove rest Series[] if any
                for (int i = 1; i < _numberOfActiveSeries; i++) { _chart.Series.RemoveAt(1); }

                // Update _numberOfActiveSeries variable
                _numberOfActiveSeries = 1;

                _fistTimeRun[2] = false;
            }
            else
            {
                PlotInternal(x, y);
            }

            _plotDatas.Clear();
            _plotDatas.Add(new DataEntity(x, y, y.Length));

            RefreshAxesAndCursor(isXZoomed, isYZoomed, firstTimeRun);
            AdaptLineSeries();
        }


        /// <summary>
        /// Plot x[][] and y[][] on chart.
        /// </summary>
        /// <param name="x"> x sequences to plot</param>
        /// <param name="y"> y sequences to plot</param>
        public void Plot(double[][] x, double[][] y)
        {
            bool firstTimeRun = (PlotMode.NaN == _plotMode);
            bool isXZoomed = (PlotMode.NaN == _plotMode || _axisX.IsZoomed);
            bool isYZoomed = (PlotMode.NaN == _plotMode || _axisY.IsZoomed);
            _plotMode = PlotMode.XYof2D;
            _xof2DXY.Clear();
            _yof2DXY.Clear();
            _tranBuf = null;
            _xof2DXY.Add(x);
            _yof2DXY.Add(y);

            //            x2DCSVdata = new double[x.Length][];
            //            y2DCSVdata.Clear();
            //            for (int k = 0; k < y.Length; k++)
            //            {
            //                tempXCSV = new double[x[k].Length];
            //                Buffer.BlockCopy(x[k], 0, tempXCSV, 0, tempXCSV.Length * sizeof(double));
            //                x2DCSVdata[k] = tempXCSV;
            //
            //                tempYCSV = new double[y[k].Length];
            //                Buffer.BlockCopy(y[k], 0, tempYCSV, 0, tempYCSV.Length * sizeof(double));
            //                y2DCSVdata.Add(tempYCSV);
            //            }

            if (_xDatalength != y[0].Length || !FixAxisX)
            {
                ZoomReset();
                isXZoomed = false;
            }
            _xDatalength = y[0].Length;

            if (_fistTimeRun[3])
            {
                double[] lineX, lineY, zoomedLineX, zoomedLineY;      // for getting each line in x[][] and y[][]
                int numberOfWaveforms, i;

                // if number of waveform in y[][] is greater than maxSeriesToDraw, only draw "maxSeriesToDraw" waveforms.
                numberOfWaveforms = Math.Min(y.GetLength(0), maxSeriesToDraw);

                // Add Series[] for each waveform if not added before 
                for (i = _numberOfActiveSeries; i < numberOfWaveforms; i++)
                {
                    // new added Series[i] uses name from predefiend name list.
                    _chart.Series.Add(_seriesNames[i]);
                    //  new added Series[i] uses the same ChartType as Series[0].
                    _chart.Series[i].ChartType = _chart.Series[0].ChartType;
                }
                SetSeriesColor();

                // plot by lines
                for (i = 0; i < numberOfWaveforms; i++)
                {
                    lineX = x[i];
                    lineY = y[i];
                    if (lineX.Length < 4000 && lineY.Length < 4000)
                    {
                        BindXYData(lineY, lineX, i);
                    }
                    else
                    {
                        zoomedLineX = ZoomResultsWithoutCaculate(lineX);
                        zoomedLineY = ZoomResultsWithoutCaculate(lineY);
                        BindXYData(zoomedLineY, zoomedLineX, i);
                    }

                }

                // Remove rest Series[] if any
                for (i = numberOfWaveforms; i < _numberOfActiveSeries; i++)
                { _chart.Series.RemoveAt(numberOfWaveforms); }

                // Update _numberOfActiveSeries variable
                _numberOfActiveSeries = numberOfWaveforms;

                _fistTimeRun[3] = false;
            }
            else
            {
                PlotInternal(x, y);
            }

            _plotDatas.Clear();
            _plotDatas.AddRange(DataEntity.GetMultiDataEntity(x, y));
            
            RefreshAxesAndCursor(isXZoomed, isYZoomed, firstTimeRun);
            AdaptLineSeries();
        }        
        /// <summary>
        /// Clear chart line points
        /// </summary>
        public void Clear()
        {
            // verify scale plot parameters
            for (int i = 0; i < _fistTimeRun.Length; i++)
            {
                _fistTimeRun[i] = true;
            }

            for (int i = 0; i < _chart.Series.Count; i++)
            {
                _chart.Series[i].Points.Clear();
            }
            _plotMode = PlotMode.NaN;
            _xCursor.RefreshCursor(EasyChartCursor.CursorMode.Disabled);
            _yCursor.RefreshCursor(EasyChartCursor.CursorMode.Disabled);
            PlotDefaultView();
            tt_xyValTips.RemoveAll();
//            Rectangle paintArea = new Rectangle(0, 0, _chart.Width, _chart.Height);
//            _chart.Invalidate(paintArea, true);
        }

        private void PlotDefaultView()
        {
            const int defaultViewSize = 1000;
            const int startOffset = 100;
            const int endOffset = 900;
            const int cycle = endOffset - startOffset;
            double[] y = new double[defaultViewSize];
            for (int i = 0; i < startOffset; i++)
            {
                y[i] = double.NaN;
            }
            for (int i = endOffset; i < defaultViewSize; i++)
            {
                y[i] = double.NaN;
            }
            for (int i = 0; i < endOffset - startOffset; i++)
            {
                y[startOffset + i] = Math.Sin(2*Math.PI*i/cycle) + 2;
            }

            
            if (LineSeries.Count >= 1)
            {
                ((EasyChartSeries)LineSeries[0]).AdaptBaseSeries(_chart.Series[0]);
            }
            else
            {
                _chart.Series[0].Color = _palette[0];
                _chart.Series[0].Name = _seriesNames[0];
            }

            _chart.Series[0].Points.DataBindY(y);

            ChartArea chartArea = _chart.ChartAreas[0];
            chartArea.AxisX.Maximum = double.NaN;
            chartArea.AxisX.Minimum = double.NaN;
            chartArea.AxisX.IsLogarithmic = false;

            chartArea.AxisY.Maximum = double.NaN;
            chartArea.AxisY.Minimum = double.NaN;
            chartArea.AxisY.IsLogarithmic = false;

            _axisX.ZoomReset();
            _axisY.ZoomReset();
            
        }
        #endregion

        #region Private Methods

        #region PlotInternal
        /// <summary>
        /// Plot single waveform y on chart, x will be generated using xStart and xIncrement.Do not record.
        /// </summary>
        /// <param name="y"> waveform to plot</param>
        /// <param name="xStart"> offset value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="xIncrement">increment value for generating x sequence using "offset + (Increment * i)"</param>
        private void PlotInternal(double[] y, double xStart = 0, double xIncrement = 1)
        {
            double[] x, zoomedY, selectedY, zoomedYWithRange;      // for x sequence
            int i;
            double zoomRate;
//            SetAxisXRange();
            zoomRate = 1;
            selectedY = SelectYInRange(y);

            if (y.Length < 4000)
            {
                // Generate x sequence according to xStart and xIncrement 
                x = new double[selectedY.Length];
                for (i = 0; i < x.Length; i++) { x[i] = xStart + xIncrement * (i+_xStartIndex); }

                // Plot x,y at Series[0]
                BindXYData(selectedY, x, 0);
            }
            else
            {   
                zoomedY = ZoomResults(selectedY, ref zoomRate);
                //缩放后，判断点数是否超过4000，CSV数据进行指针赋值

                //这一部是为了可以缩放回去
                zoomedYWithRange = new double[zoomedY.Length];
                Buffer.BlockCopy(zoomedY, 0, zoomedYWithRange, 0, zoomedY.Length * sizeof(double));

                // Generate x sequence according to xStart and xIncrement 
                x = new double[zoomedYWithRange.Length];
                for (i = 0; i < x.Length; i++) { x[i] = xStart + xIncrement * _xStartIndex + xIncrement * zoomRate * i; }

                // Plot x,y at Series[0]
                BindXYData(zoomedYWithRange, x, 0);

            }

            // Remove rest Series[] if any
            for (i = 1; i < _numberOfActiveSeries; i++) { _chart.Series.RemoveAt(1); }

            // Set minimum of X axis to 0 if xStart is 0 and logarithmic is set to false.
            // Manually set this because in this case chart will defaulty display "-1" as X axis start due to autoscale.
            if ((x[0] == 0) && (xIncrement > 0) && (!_xIsLogarithmic))
            { _chart.ChartAreas[0].AxisX.Minimum = 0; }

            // Update _numberOfActiveSeries variable
            _numberOfActiveSeries = 1;

            // Refresh colors of Series
        }

        /// <summary>
        /// Plot multiple waveforms on chart, x will be generated using xStart and xIncrement.Do not record.
        /// </summary>
        /// <param name="y"> waveforms to plot, each line in y[,] represents a single waveform</param>
        /// <param name="xStart">offset value for generating x sequence using "offset + (Increment * i)"</param>
        /// <param name="xIncrement">increment value for generating x sequence using "offset + (Increment * i)"</param>
        private void PlotInternal(double[,] y, double xStart = 0, double xIncrement = 1)
        {
            double[] x;                    // for x sequence
            double[] waveform, selectedWaveform, zoomedWaveform;      // for getting each line in Y[,]
            int numberOfWaveforms, i, j;
            double zoomRate;

//            SetAxisXRange();
            // if number of waveform in y[,] is greater than maxSeriesToDraw, only draw "maxSeriesToDraw" waveforms.
            numberOfWaveforms = Math.Min(y.GetLength(0), maxSeriesToDraw);

            // Add Series[] for each waveform if not added before 
            for (i = _numberOfActiveSeries; i < numberOfWaveforms; i++)
            {
                // new added Series[i] uses name from predefiend name list.
                _chart.Series.Add(_seriesNames[i]);
                //  new added Series[i] uses the same ChartType as Series[0].
                _chart.Series[i].ChartType = _chart.Series[0].ChartType;
            }
            SetSeriesColor();

            //If row number of waveform in y[,] is less than 4000, display without zoom.
            //CSV文档临时写入

            // plot Y[,] by lines
            zoomRate = 1;
            waveform = new double[y.GetLength(1)];
            Buffer.BlockCopy(y, 0, waveform, 0, waveform.Length * sizeof(double));
            selectedWaveform = SelectYInRange(waveform);
            //CSV文档临时写入

            if (y.GetLength(1) < 4000)
            {
                // Generate x sequence according to xStart and xIncrement 
                x = new double[selectedWaveform.Length];
                for (i = 0; i < x.Length; i++) { x[i] = xStart + xIncrement * (i + _xStartIndex); }

                BindXYData(selectedWaveform, x, 0);

                for (i = 1; i < numberOfWaveforms; i++)
                {
                    Buffer.BlockCopy(y, waveform.Length * sizeof(double) * i, waveform, 0, waveform.Length * sizeof(double));
                    selectedWaveform = SelectYInRange(waveform);
                    //CSV文档临时写入
                    BindXYData(selectedWaveform, x, i);
                }
            }
            else // If row number of waveform in y[,] is >=4000, zoom and display.
            {

                zoomedWaveform = ZoomResults(selectedWaveform, ref zoomRate);

                // Generate x sequence according to xStart and xIncrement 
                x = new double[zoomedWaveform.Length];
                for (i = 0; i < x.Length; i++) { x[i] = xStart + xIncrement * _xStartIndex + xIncrement * zoomRate * i; }

                BindXYData(zoomedWaveform, x, 0);

                for (i = 1; i < numberOfWaveforms; i++)
                {
                    Buffer.BlockCopy(y, waveform.Length * sizeof(double) * i, waveform, 0, waveform.Length * sizeof(double));
                    selectedWaveform = SelectYInRange(waveform);
                    //CSV文档临时写入
                    zoomedWaveform = ZoomResults(selectedWaveform, ref zoomRate);
                    BindXYData(zoomedWaveform, x, i);
                }
            }

            // Remove rest Series[] if any
            for (i = numberOfWaveforms; i < _numberOfActiveSeries; i++)
            { _chart.Series.RemoveAt(numberOfWaveforms); }

            // Set minimum of X axis to 0 if xStart is 0 and logarithmic is set to false.
            // Manually set this because in this case chart will defaulty display "-1" as X axis start due to autoscale.
            if ((xStart == 0) && (xIncrement > 0) && (!_xIsLogarithmic))
            { _chart.ChartAreas[0].AxisX.Minimum = 0; }

            // Update _numberOfActiveSeries variable
            _numberOfActiveSeries = numberOfWaveforms;
        }

        /// <summary>
        /// Plot x[] and y[] pair on chart.Do not record.
        /// </summary>
        /// <param name="x"> x sequence to plot</param>
        /// <param name="y"> y sequence to plot</param>
        private void PlotInternal(double[] x, double[] y)
        {
            double[] zoomedX, zoomedY, zoomedXWithRange, zoomedYWithRange;
            double[][] selectedXY, XMaxAndMinWithY;

            if (x.Length < 4000 && y.Length < 4000)
            {
                // Plot x,y at Series[0]
                BindXYData(y, x, 0);
            }
            else
            {
                selectedXY = SelectXYInRange(x, y);
                XMaxAndMinWithY = GetXMaxAndMinWithY(x, y);
                if (selectedXY != null)
                {
                    zoomedX = ZoomResultsWithoutCaculate(selectedXY[0]);
                    zoomedY = ZoomResultsWithoutCaculate(selectedXY[1]);
                    zoomedXWithRange = new double[zoomedX.Length + 2];
                    zoomedYWithRange = new double[zoomedY.Length + 2];
                    Buffer.BlockCopy(zoomedX, 0, zoomedXWithRange, sizeof(double), zoomedX.Length * sizeof(double));
                    Buffer.BlockCopy(zoomedY, 0, zoomedYWithRange, sizeof(double), zoomedY.Length * sizeof(double));
                    zoomedXWithRange[0] = XMaxAndMinWithY[0][0];
                    zoomedXWithRange[zoomedXWithRange.Length - 1] = XMaxAndMinWithY[0][1];
                    zoomedYWithRange[0] = XMaxAndMinWithY[1][0];
                    zoomedYWithRange[zoomedYWithRange.Length - 1] = XMaxAndMinWithY[1][1];
                    BindXYData(zoomedYWithRange, zoomedXWithRange, 0);
                }
                else
                {
                    zoomedX = ZoomResultsWithoutCaculate(x);
                    zoomedY = ZoomResultsWithoutCaculate(y);
                    BindXYData(zoomedY, zoomedX, 0);
                }
            }

            // Remove rest Series[] if any
            for (int i = 1; i < _numberOfActiveSeries; i++) { _chart.Series.RemoveAt(1); }

            // Update _numberOfActiveSeries variable
            _numberOfActiveSeries = 1;
        }


        /// <summary>
        /// Plot x[][] and y[][] on chart.Do not record.
        /// </summary>
        /// <param name="x"> x sequences to plot</param>
        /// <param name="y"> y sequences to plot</param>
        private void PlotInternal(double[][] x, double[][] y)
        {
            double[] lineX, lineY, zoomedLineX, zoomedLineY, zoomedXWithRange, zoomedYWithRange;      // for getting each line in x[][] and y[][]
            int numberOfWaveforms, i;
            double[][] selectedXY, XMaxAndMinWithY;

            // if number of waveform in y[][] is greater than maxSeriesToDraw, only draw "maxSeriesToDraw" waveforms.
            numberOfWaveforms = Math.Min(y.GetLength(0), maxSeriesToDraw);

            // Add Series[] for each waveform if not added before 
            for (i = _numberOfActiveSeries; i < numberOfWaveforms; i++)
            {
                // new added Series[i] uses name from predefiend name list.
                _chart.Series.Add(_seriesNames[i]);
                //  new added Series[i] uses the same ChartType as Series[0].
                _chart.Series[i].ChartType = _chart.Series[0].ChartType;
            }
            SetSeriesColor();

            // plot by lines
            for (i = 0; i < numberOfWaveforms; i++)
            {
                lineX = x[i];
                lineY = y[i];
                if (lineX.Length < 4000 && lineY.Length < 4000)
                {
                    BindXYData(lineY, lineX, i);
                }
                else
                {
                    selectedXY = SelectXYInRange(x[i], y[i]);
                    XMaxAndMinWithY = GetXMaxAndMinWithY(x[i], y[i]);
                    if (selectedXY != null)
                    {
                        zoomedLineX = ZoomResultsWithoutCaculate(selectedXY[0]);
                        zoomedLineY = ZoomResultsWithoutCaculate(selectedXY[1]);
                        zoomedXWithRange = new double[zoomedLineX.Length + 2];
                        zoomedYWithRange = new double[zoomedLineY.Length + 2];
                        Buffer.BlockCopy(zoomedLineX, 0, zoomedXWithRange, sizeof(double), zoomedLineX.Length * sizeof(double));
                        Buffer.BlockCopy(zoomedLineY, 0, zoomedYWithRange, sizeof(double), zoomedLineY.Length * sizeof(double));
                        zoomedXWithRange[0] = XMaxAndMinWithY[0][0];
                        zoomedXWithRange[zoomedXWithRange.Length - 1] = XMaxAndMinWithY[0][1];
                        zoomedYWithRange[0] = XMaxAndMinWithY[1][0];
                        zoomedYWithRange[zoomedYWithRange.Length - 1] = XMaxAndMinWithY[1][1];
                        BindXYData(zoomedYWithRange, zoomedXWithRange, i);
                    }
                    else
                    {
                        zoomedLineX = ZoomResultsWithoutCaculate(x[i]);
                        zoomedLineY = ZoomResultsWithoutCaculate(y[i]);
                        BindXYData(zoomedLineY, zoomedLineX, i);
                    }
                }
            }

            // Remove rest Series[] if any
            for (i = numberOfWaveforms; i < _numberOfActiveSeries; i++)
            { _chart.Series.RemoveAt(numberOfWaveforms); }

            // Update _numberOfActiveSeries variable
            _numberOfActiveSeries = numberOfWaveforms;
        }

        #endregion

        #region Chart Utility
        /// <summary>
        /// Bind x/y data to Series[indexOfSeries] of chart
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="indexOfSeries"></param>
        private void BindXYData(double[] y, double[] x, int indexOfSeries)
        {
            // TODO 后期修改判断条件使用XAxis和YAxis
            if (_xIsLogarithmic && x.Any(xValue => xValue <= 0))
            {
                MessageBox.Show("Some iterms in X data are less than 0.", "Plot Error");
                return;
            }
            if (_yIsLogarithmic && y.Any(yValue => (yValue <= 0)))
            {
                MessageBox.Show("Some iterms in Y data are less than 0.", "Plot Error");
                return;
            }
            _chart.Series[indexOfSeries].Points.DataBindXY(x, y);
            // TODO 后期修改到RefreshAxesAndCursor方法里
            SetAxisMappingType();
        }

        /// <summary>
        /// Set names of series according to property "_seriesNames"
        /// </summary>
        private void SetSeriesNames()
        {
            for (int i = 0; i < _chart.Series.Count; i++) { _chart.Series[i].Name = _seriesNames[i]; }
        }
        /// <summary>
        /// Set if X/Y axis Logarithmic.
        /// </summary>
        private void SetAxisMappingType()
        {
            if (IsPlotting())
            {
                _chart.ChartAreas[0].AxisX.IsLogarithmic = _xIsLogarithmic;
                _chart.ChartAreas[0].AxisY.IsLogarithmic = _yIsLogarithmic;
            }
        }

        /// <summary>
        /// Set if legend visible
        /// </summary>
        private void SetLegendVisible()
        {
            _chart.Legends[0].Enabled = _legendVisibe;
        }


        /// <summary>
        /// Set if X/Y axis zoomable
        /// </summary>
        private void SetZoomable(bool _xZoomable, bool _yZoomable)
        {
            //X轴
            _chart.ChartAreas[0].CursorX.IsUserEnabled = _xZoomable;
            _chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = _xZoomable;     //用户可选择
            _chart.ChartAreas[0].AxisX.ScaleView.Zoomable = _xZoomable;           //是否可以缩放
            if (_xZoomable)
            { _chart.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Solid; }
            else
            { _chart.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.NotSet; }

            //Y轴
            _chart.ChartAreas[0].CursorY.IsUserEnabled = _yZoomable;
            _chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = _yZoomable;     //用户可选择
            _chart.ChartAreas[0].AxisY.ScaleView.Zoomable = _yZoomable;           //是否可以缩放
            if (_yZoomable)
            { _chart.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Solid; }
            else
            { _chart.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.NotSet; }
        }

        private int GetIndexFromChannelName(string channelName)
        {
            for (int i = 0; i < _seriesNames.Length; i++)
            {
                if (_seriesNames[i] == channelName)
                    return i;
            }
            //如果不存在则返回-1
            return -1;
        }
        #endregion

        #region Zoom Utility
        /// <summary>
        /// Zoom points
        /// </summary>
        /// <param name="y"> waveform to plot</param>
        /// <param name="zoomRate"> zoomRate of y</param>
        /// <returns>return caculated results</returns>
        private double[] ZoomResults(double[] y, ref double zoomRate)
        {
            double[] zoomedY;
            int displayNumberOfPoints;
            double zoomRateForPass;
            zoomRate = 1;
            if (y.Length < 4000)
            {
                return y;
            }
            displayNumberOfPoints = y.Length;
            while (displayNumberOfPoints >= 4000)
            {
                displayNumberOfPoints /= 2;
                zoomRate *= 2;
            }
            if (displayNumberOfPoints % 2 == 1) { displayNumberOfPoints += 1; }
            zoomedY = new double[displayNumberOfPoints];
            zoomRateForPass = zoomRate;
            Parallel.For(0, displayNumberOfPoints / 2, i =>
            {
                int Maxindex, Minindex, k;
                double Max, Min;
                Maxindex = (int)Math.Ceiling(zoomRateForPass * i * 2);
                Minindex = Maxindex;
                Max = y[Maxindex];
                Min = y[Minindex];
                if (i < displayNumberOfPoints / 2 - 1)
                {
                    k = (int)Math.Ceiling(zoomRateForPass * (i + 1) * 2);
                }
                else
                {
                    k = y.Length - 1;
                }
                for (int j = Maxindex; j < k; j++)
                {
                    if (y[j] > Max)
                    {
                        Max = y[j];
                        Maxindex = j;
                    }
                    if (y[j] < Min)
                    {
                        Min = y[j];
                        Minindex = j;
                    }
                }
                if (Maxindex <= Minindex)
                {
                    zoomedY[i * 2] = Max;
                    zoomedY[i * 2 + 1] = Min;
                }
                else
                {
                    zoomedY[i * 2] = Min;
                    zoomedY[i * 2 + 1] = Max;
                }
            });
            return zoomedY;
        }

        private double[] ZoomResultsWithoutCaculate(double[] y)
        {
            double[] zoomedY;
            int displayNumberOfPoints;
            int zoomRate = 1;
            if (y.Length < 4000)
            {
                return y;
            }
            displayNumberOfPoints = y.Length;
            while (displayNumberOfPoints >= 4000)
            {
                displayNumberOfPoints /= 2;
                zoomRate *= 2;
            }
            zoomedY = new double[displayNumberOfPoints];
            Parallel.For(0, zoomedY.Length, i =>
            {
                zoomedY[i] = y[i * zoomRate];
            });
            return zoomedY;
        }

        /// <summary>
        /// Select Y in range set by user.
        /// </summary>
        /// <param name="y">waveform to plot</param>
        /// <returns></returns>
        private double[] SelectYInRange(double[] y)
        {
            double[] selectedY;
            if (_xEndIndex - _xStartIndex < 2)
            {
                selectedY = new double[(int) ((_axisX.ViewMaximum - _axisX.ViewMinimum)/_xIncrement) + 1];
                for (int i = 0; i < selectedY.Length; i++)
                {
                    selectedY[i] = double.NaN;
                }
            }
            else
            {
                selectedY = new double[_xEndIndex - _xStartIndex + 1];
                Buffer.BlockCopy(y, _xStartIndex * sizeof(double), selectedY, 0, selectedY.Length * sizeof(double));
            }
            return selectedY;
        }
        private void RefreshAxisXIndexRange(double xStart, double xIncrement,int xLength, ref int xStartIndex,ref int xEndIndex)
        {
            xStartIndex = (int)((_xMin - xStart) / xIncrement);
            if (xStartIndex < 0) xStartIndex = 0;
            xEndIndex = (int)((_xMax - xStart) / xIncrement);
            if (xEndIndex > xLength - 1||xEndIndex < 0) xEndIndex = xLength - 1;
        }

        private void SetAxisXRange()
        {
            lock (this)
            {
                if (_chart.ChartAreas[0].AxisX.Minimum != _xStart ||
                _chart.ChartAreas[0].AxisX.Maximum != _xStart + _xIncrement * (_xDatalength))
                {
                    _chart.ChartAreas[0].AxisX.Minimum = _xStart;
                    _chart.ChartAreas[0].AxisX.Maximum = _xStart + _xIncrement * (_xDatalength);
                    _chart.ChartAreas[0].AxisX.ScaleView.Zoom(_xStart + _xStartIndex * _xIncrement+1, _xStart + _xEndIndex * _xIncrement-1);
                }
            }
        }

        private void RefreshPlot()
        {
            switch (_plotMode)
            {
                case PlotMode.Yof1D:
                    if (_yof1D.Count > 0)
                    {
                        PlotInternal(_yof1D[0], _xStart, _xIncrement);
                    }
                    break;
                case PlotMode.Yof2D:
                    if (_yof2D.Count > 0)
                    {
                        PlotInternal(_yof2D[0], _xStart, _xIncrement);
                    }
                    break;
                case PlotMode.XYof1D:
                    if (_xof1DXY.Count > 0 && _yof1DXY.Count > 0)
                    {
                        PlotInternal(_xof1DXY[0], _yof1DXY[0]);
                    }
                    break;
                case PlotMode.XYof2D:
                    if (_xof2DXY.Count > 0 && _yof2DXY.Count > 0)
                    {
                        PlotInternal(_xof2DXY[0], _yof2DXY[0]);
                    }
                    break;
                case PlotMode.NaN:
                    break;
            }
        }

        private double[][] GetXMaxAndMinWithY(double[] x, double[] y)
        {
            double XMax, XMin, YMax, YMin;
            double[][] XYMaxMin = new double[][] { new double[2], new double[2] };
            XMax = XMin = x[0];
            YMax = YMin = y[0];
            for (int i = 1; i < x.Length; i++)
            {
                if (x[i] > XMax)
                {
                    XMax = x[i];
                    YMax = y[i];
                }
                if (x[i] < XMin)
                {
                    XMin = x[i];
                    YMin = y[i];
                }
            }
            XYMaxMin[0][0] = XMin;
            XYMaxMin[0][1] = XMax;
            XYMaxMin[1][0] = YMin;
            XYMaxMin[1][1] = YMax;
            return XYMaxMin;
        }

        /// <summary>
        /// GetYAxis Maximum and Minimum
        /// </summary>
        /// <returns></returns>
        internal double[] GetYAxisRange()
        {
            double[] YAxisRange = new double[2];
            YAxisRange[0] = _chart.ChartAreas[0].AxisY.Maximum;
            YAxisRange[1] = _chart.ChartAreas[0].AxisY.Minimum;
            return YAxisRange;
        }

        private double[][] SelectXYInRange(double[] x, double[] y)
        {
            List<double> xSelected = new List<double>();
            List<double> ySelected = new List<double>();
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] <= (_xMax + 1) && x[i] >= (_xMin - 1) && i < y.Length)
                {
                    xSelected.Add(x[i]);
                    ySelected.Add(y[i]);
                }
            }
            if (xSelected.Count > 0)
            {
                double[][] selectedXY = new double[2][];
                selectedXY[0] = xSelected.ToArray();
                selectedXY[1] = ySelected.ToArray();
                return selectedXY;
            }
            return null;
        }

        // In case of unexpected error, keep original code
        private void ZoomReset()
        {
//            _chart.ChartAreas[0].AxisX.ScaleView.ZoomReset(int.MaxValue);
//            _chart.ChartAreas[0].AxisY.ScaleView.ZoomReset(int.MaxValue);
//            _chart.ChartAreas[0].AxisX.Minimum = double.NaN;
//            _chart.ChartAreas[0].AxisX.Maximum = double.NaN;
            _xMin = double.MinValue;
            _xMax = double.MaxValue;
            _fistTimeRun = new bool[]{ true,true,true,true };
        }
        #endregion

        #endregion

        #region Event Handler
        /// <summary>
        /// 这个主要是X轴缩放，Y轴缩放时的自动调用的方法，坐标轴值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _chart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            EasyChartAxis changedAxis = null;
            if (e.Axis.Tag.ToString() == "AxisX")
            {
                _xMin = e.Axis.ScaleView.ViewMinimum;
                _xMax = e.Axis.ScaleView.ViewMaximum;
                RefreshAxisXIndexRange(_xStart, _xIncrement, _xDatalength, ref _xStartIndex, ref _xEndIndex);
                RefreshPlot();
                _axisX.RefreshAxisRange();
                _axisY.RefreshAxisRange();
                changedAxis = _axisX;
            }
            else
            {
                changedAxis = _axisY;
            }
            OnAxisViewChanged(changedAxis, true, true);
        }

        private void _chart_MouseDown(object sender, MouseEventArgs e)
        {
            if (PlotMode.NaN == _plotMode)
            {
                return;
            }
            //这是用来显示XYValue的代码函数
            if (e.Button == MouseButtons.Left)
            {
                if (EasyChartCursor.CursorMode.Cursor == _xCursor.Mode && EasyChartCursor.CursorMode.Cursor == _yCursor.Mode)
                {
//                    Point pointToClient = _chart.PointToClient(Point.Empty);
//                    _chart.PointToScreen();
//                    _chart.ChartAreas[0]

                    HitTestResult result = _chart.HitTest(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y);
                    this.tt_xyValTips.Hide(this._chart);
                    //在chartarea区域显示当前缩放需要显示的光标
                    if ((result.ChartElementType != ChartElementType.PlottingArea &&
                         result.ChartElementType != ChartElementType.Gridlines &&
                         result.ChartElementType != ChartElementType.StripLines &&
                         result.ChartElementType != ChartElementType.DataPoint &&
                         result.ChartElementType != ChartElementType.Axis))  
                    {
                        return;
                    }
                    string dispText = MoveCursorAndGetXYValueShowInfo();
                    if (null == dispText || dispText.Equals(string.Empty))
                    {
                        return;
                    }
                    Point clickPoint = this._chart.PointToClient(Control.MousePosition);//鼠标相对于窗体左上角的坐标
                    clickPoint = new Point(clickPoint.X + 10, clickPoint.Y + 10);
                    this.tt_xyValTips.Show(dispText, this._chart, clickPoint);
                    this.OnCursorPositionChanged(_xCursor, true);
                }
            }
            //这是用来显示右击菜单的代码函数
            if (e.Button == MouseButtons.Right)
            {

                //Update series
                for (int x = 0; x < contextMenuRightClick.Items.Count; x++)
                {
                    if (contextMenuRightClick.Items[x].Tag != null)
                    {
                        if (contextMenuRightClick.Items[x].Tag.ToString() == "Series")
                        {
                            contextMenuRightClick.Items.RemoveAt(x);
                            x--;
                        }
                    }
                }
                foreach (Series ptrSeries in _chart.Series)
                {
                    ToolStripItem ptrItem = contextMenuRightClick.Items.Add(ptrSeries.Name);
                    ToolStripMenuItem ptrMenuItem = (ToolStripMenuItem)ptrItem;
                    ptrMenuItem.Checked = ptrSeries.Enabled;
                    ptrItem.Tag = "Series";
                }

                //更新菜单项勾选状态
                RefreshContextMenuItems();
                //获取节点区域的右下角坐标值
                Point pos = new Point(e.Location.X, e.Location.Y);
                contextMenuRightClick.Show(_chart, pos);
            }
        }

        private void RefreshContextMenuItems()
        {
            Zoom_XAxisToolStripMenuItem.Checked = EasyChartCursor.CursorMode.Zoom == _xCursor.Mode &&
                                                  EasyChartCursor.CursorMode.Zoom != _yCursor.Mode;
            Zoom_YAxisToolStripMenuItem.Checked = EasyChartCursor.CursorMode.Zoom != _xCursor.Mode &&
                                                  EasyChartCursor.CursorMode.Zoom == _yCursor.Mode;
            Zoom_WindowtoolStripMenuItem.Checked = EasyChartCursor.CursorMode.Zoom == _xCursor.Mode &&
                                                  EasyChartCursor.CursorMode.Zoom == _yCursor.Mode;
            Show_XYValuetoolStripMenuItem.Checked = EasyChartCursor.CursorMode.Cursor == _xCursor.Mode &&
                                                  EasyChartCursor.CursorMode.Cursor == _yCursor.Mode;
            legendVisibleToolStripMenuItem.Checked = _legendVisibe;
            YAutoScaleToolStripMenuItem.Checked = _axisY.AutoScale;
        }

        private string MoveCursorAndGetXYValueShowInfo()
        {
            string dispText = string.Empty;
            int seriesIndex = GetCursorSeriesIndex();
            if (EasyChartCursor.CursorMode.Cursor != _xCursor.Mode || seriesIndex < 0)
            {
                return dispText;
            }
            double xValue = _xCursor.Value;
            double yValue = _yCursor.Value;
            int pointIndex;
            if (_xIsLogarithmic)
            {
                xValue = Math.Pow(10, xValue);
            }
            if (_yIsLogarithmic)
            {
                yValue = Math.Pow(10, yValue);
            }
            if (_plotDatas.Count > 1)
            {
                pointIndex = _plotDatas[seriesIndex].FindeNearestIndex(ref xValue, ref yValue, 0);
            }
            else
            {
                pointIndex = _plotDatas[0].FindeNearestIndex(ref xValue, ref yValue, seriesIndex);
            }
            if (pointIndex < 0)
            {
                return dispText;
            }
            _xCursor.Value = xValue;
            _yCursor.Value = yValue;
            const string dispTextFormat = "X Val: {0}{2}Y Val: {1}";
            return string.Format(dispTextFormat, xValue, yValue, Environment.NewLine);
        }

        private int GetCursorSeriesIndex()
        {
            int selectIndex = _cursorSeriesMenuItems.FindIndex(seriesItem => seriesItem.Checked);
            if (selectIndex < 0)
            {
                return -1;
            }
            Series selectSeries = _chart.Series.FindByName(_cursorSeriesMenuItems[selectIndex].Text);
            if (null == selectSeries)
            {
                return -1;
            }
            return _chart.Series.IndexOf(selectSeries);
        }


        private double FindNearestValue(double[] yValues, double yCursorValue)
        {
            if (null == yValues || 0 == yValues.Length)
            {
                return yCursorValue;
            }
            double nearestYValue = yValues[0];
            for (int i = 1; i < yValues.Length - 0; i++)
            {
                if (Math.Abs(nearestYValue - yCursorValue) > Math.Abs(yValues[i] - yCursorValue))
                {
                    nearestYValue = yValues[i];
                }
            }
            return nearestYValue;
        }

        private void _chart_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult result = null;
            try
            {
                result = _chart.HitTest(e.X, e.Y);
            }
            catch
            {
                return;
            }
            if (null == result)
            {
                return;
            }
            if (result.ChartElementType == ChartElementType.LegendItem)
            {
                this.Cursor = System.Windows.Forms.Cursors.Hand;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        /// <summary>
        /// 选择条件下，展现出Chart XY坐标值
        /// 修改City Shao
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _chart_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = _chart.HitTest(e.X, e.Y);
            if (result.ChartElementType == ChartElementType.LegendItem)
            {
                LegendItem legendItem = (LegendItem)result.Object;  //取得当前的LegendItem
                int idx = GetIndexFromChannelName(legendItem.SeriesName);
                if (idx != -1)
                {
                    this.currentClickCurve = idx;//记住当前选择的曲线
                    switch (_chart.Series[this.currentClickCurve].BorderWidth)//cvProperty[idx].LineWidth)
                    {
                        case 1:
                            this.LinethinToolStripMenuItem.Checked = true;
                            this.LinemiddleToolStripMenuItem.Checked = false;
                            this.LinethickToolStripMenuItem.Checked = false;
                            break;
                        case 2:
                            this.LinethinToolStripMenuItem.Checked = false;
                            this.LinemiddleToolStripMenuItem.Checked = true;
                            this.LinethickToolStripMenuItem.Checked = false;
                            break;
                        case 3:
                            this.LinethinToolStripMenuItem.Checked = false;
                            this.LinemiddleToolStripMenuItem.Checked = false;
                            this.LinethickToolStripMenuItem.Checked = true;
                            break;
                        default:
                            this.LinethinToolStripMenuItem.Checked = false;
                            this.LinemiddleToolStripMenuItem.Checked = false;
                            this.LinethickToolStripMenuItem.Checked = false;
                            break;
                    }
                    switch (_chart.Series[this.currentClickCurve].ChartType)
                    {
                        case SeriesChartType.Point:
                        case SeriesChartType.FastPoint:
                            this.InterpointToolStripMenuItem.Checked = true;
                            this.InterFastlineToolStripMenuItem.Checked = false;
                            this.InterstepLineToolStripMenuItem.Checked = false;
                            this.InterLineToolStripMenuItem.Checked = false;
                            break;
                        case SeriesChartType.Line:
                            this.InterpointToolStripMenuItem.Checked = false;
                            this.InterFastlineToolStripMenuItem.Checked = false;
                            this.InterstepLineToolStripMenuItem.Checked = false;
                            this.InterLineToolStripMenuItem.Checked = true;
                            break;
                        case SeriesChartType.FastLine:
                            this.InterpointToolStripMenuItem.Checked = false;
                            this.InterFastlineToolStripMenuItem.Checked = true;
                            this.InterstepLineToolStripMenuItem.Checked = false;
                            this.InterLineToolStripMenuItem.Checked = false;
                            break;
                        case SeriesChartType.StepLine:
                            this.InterpointToolStripMenuItem.Checked = false;
                            this.InterFastlineToolStripMenuItem.Checked = false;
                            this.InterstepLineToolStripMenuItem.Checked = true;
                            this.InterLineToolStripMenuItem.Checked = false;
                            break;
                        default:
                            this.InterpointToolStripMenuItem.Checked = false;
                            this.InterFastlineToolStripMenuItem.Checked = false;
                            this.InterstepLineToolStripMenuItem.Checked = false;
                            this.InterLineToolStripMenuItem.Checked = false;
                            break;
                    }
                    this.PointStylenoneToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.None);
                    this.PointStylesquareToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.Square);
                    this.PointStylecircleToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.Circle);
                    this.PointStylediamodToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.Diamond);
                    this.PointStyletriangleToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.Triangle);
                    this.PointStylecrossToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.Cross);
                    this.PointStylestart4ToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.Star4);
                    this.PointStylestart5ToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.Star5);
                    this.PointStylestart6ToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.Star6);
                    this.PointStylestart10ToolStripMenuItem.Checked = (_chart.Series[idx].MarkerStyle == MarkerStyle.Star10);
                }

                Point pos = new Point(e.Location.X, e.Location.Y);
                contextMenuLeftClick.Show(_chart, pos);

            }
        }
        #region rightClickMenuEvnet
        /// <summary>
        /// 当点击保存图片按钮的事件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsImage();
        }

        /// <summary>
        /// Save chart view to png file
        /// </summary>
        public void SaveAsImage()
        {
            var saveFileDialogPic = new SaveFileDialog();
            saveFileDialogPic.Filter = "Png图片|*.Png";
            if (saveFileDialogPic.ShowDialog() == DialogResult.OK)
            {
                SaveAsImage(saveFileDialogPic.FileName);
            }
        }

        /// <summary>
        /// Save chart view to png file
        /// </summary>
        /// <para name="filePath">Png file path</para>
        public void SaveAsImage(string filePath)
        {
            try
            {
                _chart.SaveImage(filePath, ChartImageFormat.Png);
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
            SaveAsCsv();
        }

        /// <summary>
        /// Save chart data to csv file
        /// </summary>
        public void SaveAsCsv()
        {
            if (PlotMode.NaN == _plotMode)
            {
                return;
            }
            var saveFileDialogPic = new SaveFileDialog();
            saveFileDialogPic.Filter = "CSV表格|*.csv";
            if (saveFileDialogPic.ShowDialog() == DialogResult.OK)
            {
                SaveAsCsv(saveFileDialogPic.FileName);
            }
        }

        /// <summary>
        /// Save chart data to csv file
        /// </summary>
        /// <param name="filePath">Csv file path</param>
        public void SaveAsCsv(string filePath)
        {
            
            if (PlotMode.NaN == _plotMode || _plotDatas.Count <= 0)
            {
                return;
            }
            FileStream stream = null;
            StreamWriter writer = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Create,FileAccess.Write);
                writer = new StreamWriter(stream, Encoding.UTF8);
                if (1 == _plotDatas.Count)
                {
                    SaveOneDimXDataToCsv(writer);
                }
                else
                {
                    SaveMultiDimXDataToCsv(writer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ReleaseResource(writer);
                ReleaseResource(stream);
            }
        }

        private static void ReleaseResource(IDisposable resource)
        {
            try
            {
                resource?.Dispose();
            }
            catch (Exception)
            {
                // igore
            }
        }

        private const string XAxisCsvLabel = "T";
        private const char CsvDelim = ',';
        private void SaveOneDimXDataToCsv(StreamWriter writer)
        {
            StringBuilder lineData = new StringBuilder();
            lineData.Append(XAxisCsvLabel).Append(CsvDelim);
            //写出列名称
            for (int i = 0; i < _chart.Series.Count; i++)
            {
                lineData.Append(_seriesNames[i]).Append(CsvDelim);
            }
            if (lineData.Length > 0)
            {
                lineData.Remove(lineData.Length - 1, 1);
            }
            writer.WriteLine(lineData);
            lineData.Clear();
            //写出各行数据
            for (int i = 0; i < _plotDatas[0].Size; i++)
            {
                lineData.Append(_plotDatas[0].GetXData(i)).Append(CsvDelim);
                for (int j = 0; j < _plotDatas[0].LineNum; j++)
                {
                    lineData.Append(_plotDatas[0].GetYData(j, i)).Append(CsvDelim);
                }
                lineData.Remove(lineData.Length - 1, 1);
                writer.WriteLine(lineData);
                lineData.Clear();
            }
        }

        private void SaveMultiDimXDataToCsv(StreamWriter writer)
        {
            StringBuilder lineData = new StringBuilder();
            
            //写出列名称
            for (int i = 0; i < _chart.Series.Count; i++)
            {
                lineData.Append(XAxisCsvLabel).Append(CsvDelim);
                lineData.Append(_seriesNames[i]).Append(CsvDelim);
            }
            if (lineData.Length > 0)
            {
                lineData.Remove(lineData.Length - 1, 1);
            }
            writer.WriteLine(lineData);
            lineData.Clear();
            //写出各行数据
            int maxDataSize = GetMaxDataSize();
            for (int i = 0; i < maxDataSize; i++)
            {
                
                for (int j = 0; j < _plotDatas.Count; j++)
                {
                    if (_plotDatas[j].Size > i)
                    {
                        lineData.Append(_plotDatas[0].GetXData(i)).Append(CsvDelim);
                        lineData.Append(_plotDatas[0].GetYData(0, i)).Append(CsvDelim);
                    }
                    else
                    {
                        lineData.Append(CsvDelim).Append(CsvDelim);
                    }
                    
                }
                lineData.Remove(lineData.Length - 1, 1);
                writer.WriteLine(lineData);
                lineData.Clear();
            }
        }

        /// <summary>
        /// 事件内容，是否显示LegendItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void legendVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legendVisibleToolStripMenuItem.Checked == false)
            {
                legendVisibleToolStripMenuItem.Checked = true;
                this.LegendVisible = true;
            }
            else
            {
                legendVisibleToolStripMenuItem.Checked = false;
                this.LegendVisible = false;
            }

        }
        /// <summary>
        /// 取消Unchecke按钮
        /// </summary>
        private void UncheckedZoom_ToolStripMenuItem()
        {
            Zoom_XAxisToolStripMenuItem.Checked = false;
            Zoom_YAxisToolStripMenuItem.Checked = false;
            Zoom_WindowtoolStripMenuItem.Checked = false;
            Show_XYValuetoolStripMenuItem.Checked = false;
        }
        /// <summary>
        /// 私有方法，主要用清楚ValueTips Show的各种方法
        /// </summary>
        private void Show_XYValueClear()
        {
            //XYValue Show的清楚方法
            tt_xyValTips.RemoveAll();
            //            _chart.ChartAreas[0].CursorY.IsUserEnabled = false;
            //            _chart.ChartAreas[0].CursorX.IsUserEnabled = false;
            //            _chart.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.NotSet;
            //            _chart.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.NotSet;
            _xCursor.Mode = EasyChartCursor.CursorMode.Disabled;
            _yCursor.Mode = EasyChartCursor.CursorMode.Disabled;
        }
        /// <summary>
        /// 点击ToolStripMenu  ZoomX状态的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zoom_XAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Zoom_XAxisToolStripMenuItem.Checked)
            {
                Zoom_XAxisToolStripMenuItem.Checked = false;
                //                SetZoomable(false, false);
                _xCursor.Mode = EasyChartCursor.CursorMode.Disabled;
            }
            else
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValueClear();
                Zoom_XAxisToolStripMenuItem.Checked = true;
//                SetZoomable(true, false);
                _xCursor.Mode = EasyChartCursor.CursorMode.Zoom;
                _yCursor.Mode = EasyChartCursor.CursorMode.Disabled;
            }
            RefreshCursorSeriesMenuItems();
        }

        /// <summary>
        /// 点击ToolStripMenu  ZoomY状态的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zoom_YAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Zoom_YAxisToolStripMenuItem.Checked == true)
            {
                Zoom_YAxisToolStripMenuItem.Checked = false;
                //                SetZoomable(false, false);
                _yCursor.Mode = EasyChartCursor.CursorMode.Disabled;
            }
            else
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValueClear();
                Zoom_YAxisToolStripMenuItem.Checked = true;
                //                SetZoomable(false, true);
                _xCursor.Mode = EasyChartCursor.CursorMode.Disabled;
                _yCursor.Mode = EasyChartCursor.CursorMode.Zoom;
            }
            RefreshCursorSeriesMenuItems();
        }
        /// <summary>
        /// 点击ToolStripMenu  ZoomWindows状态的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zoom_WindowtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Zoom_WindowtoolStripMenuItem.Checked == true)
            {
                Zoom_WindowtoolStripMenuItem.Checked = false;
                //                SetZoomable(false, false);
                _xCursor.Mode = EasyChartCursor.CursorMode.Disabled;
                _yCursor.Mode = EasyChartCursor.CursorMode.Disabled;
            }
            else
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValueClear();
                Zoom_WindowtoolStripMenuItem.Checked = true;
                //                SetZoomable(true, true);
                _xCursor.Mode = EasyChartCursor.CursorMode.Zoom;
                _yCursor.Mode = EasyChartCursor.CursorMode.Zoom;
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
            ZoomReset();
            RefreshAxisXIndexRange(_xStart, _xIncrement, _xDatalength, ref _xStartIndex, ref _xEndIndex);
            RefreshPlot();
            _axisX.ZoomReset();
            _axisY.ZoomReset();
            _axisX.RefreshAxisRange();
        }

        /// <summary>
        /// 点击ToolStripMenu ShowXYValue的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_XYValuetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show_XYValuetoolStripMenuItem.Checked = !Show_XYValuetoolStripMenuItem.Checked;
            if (Show_XYValuetoolStripMenuItem.Checked == true)
            {
                UncheckedZoom_ToolStripMenuItem();
                Show_XYValuetoolStripMenuItem.Checked = true;
//                SetZoomable(false, false);

                //                _chart.ChartAreas[0].CursorY.IsUserEnabled = false;
                //                _chart.ChartAreas[0].CursorX.IsUserEnabled = true;
                //                _chart.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Solid;
                //                _chart.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Solid;
                _xCursor.Mode = EasyChartCursor.CursorMode.Cursor;
                _yCursor.Mode = EasyChartCursor.CursorMode.Cursor;
            }
            else
            {
                Show_XYValueClear();
            }
            RefreshCursorSeriesMenuItems();
        }

        /// <summary>
        /// 是否使能Y轴的Auto Scale的功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YAutoScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _axisY.AutoScale = !_axisY.AutoScale;
            YAutoScaleToolStripMenuItem.Checked = _axisY.AutoScale;
        }


        #endregion

        #region leftClickMenuEvnet

        /// <summary>
        /// 选择显示某个通道的事件操作，由于无法判断有哪些通道所以用一个总的事件继续进行判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuRightClick_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag == null) return;
            if (e.ClickedItem.Tag.ToString() != "Series") return;
            if (e.ClickedItem.Tag.ToString() == "Series")
            {
                _chart.Series[e.ClickedItem.Text].Enabled = !((ToolStripMenuItem)e.ClickedItem).Checked;
            }
            //刷新是否仍需要继续缩放
            _chart.ChartAreas["ChartArea1"].RecalculateAxesScale();
            RefreshCursorSeriesItems();
        }

        

        private void RefreshCursorSeriesItems()
        {
            List<Series> checkedSeries = GetCheckedSeries();
            //更新菜单项显示
            ToolStripItemCollection cursorSeriesItems = cursorSeriesRootMenuItem.DropDownItems;
            bool itemChecked = false;   //是否有菜单项被选择的标识位
            for (int i = 0; i < checkedSeries.Count; i++)
            {
                if (_cursorSeriesMenuItems.Count <= i)
                {
                    ToolStripMenuItem seriesItem = new ToolStripMenuItem(checkedSeries[i].Name);
                    seriesItem.Click += new EventHandler(this.SelectSelfAndDeselectOthers);
                    _cursorSeriesMenuItems.Add(seriesItem);
                    cursorSeriesItems.Add(seriesItem);
                }
                else if (!checkedSeries[i].Name.Equals(_cursorSeriesMenuItems[i].Text))
                {
                    _cursorSeriesMenuItems[i].Text = checkedSeries[i].Name;
                }
                itemChecked |= _cursorSeriesMenuItems[i].Checked;
            }
            //如果没有任何菜单被选择则默认选择第一个
            if (!itemChecked && _cursorSeriesMenuItems.Count > 0)
            {
                _cursorSeriesMenuItems[0].Checked = true;
            }
            //移除多余的菜单项
            int itermsToRemove = _cursorSeriesMenuItems.Count - checkedSeries.Count;
            for (int i = 0; i < itermsToRemove; i++)
            {
                _cursorSeriesMenuItems.RemoveAt(_cursorSeriesMenuItems.Count - 1);
                cursorSeriesItems.RemoveAt(cursorSeriesItems.Count - 1);
            }
        }

        private List<Series> GetCheckedSeries()
        {
            SeriesCollection series = _chart.Series;
            List<Series> checkedSeries = new List<Series>(series.Count);
            //获取已经显示的Series
            foreach (Series singleSerie in series)
            {
                if (singleSerie.Enabled)
                {
                    checkedSeries.Add(singleSerie);
                }
            }
            return checkedSeries;
        }

        private void SelectSelfAndDeselectOthers(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in _cursorSeriesMenuItems)
            {
                item.Checked = false;
            }
            ((ToolStripMenuItem) sender).Checked = true;
        }

        private void curveColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog loColorForm = new ColorDialog();

            if (loColorForm.ShowDialog() == DialogResult.OK)
            {
                Color chooseColor = loColorForm.Color;
                _chart.Series[this.currentClickCurve].Color = chooseColor; //更换曲线的颜色
                ((EasyChartSeries)LineSeries[currentClickCurve]).Color = chooseColor;      //保存更改的颜色           
            }
        }
        /// <summary>
        /// 选择线宽Thin的处理内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinethinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((EasyChartSeries)LineSeries[currentClickCurve]).Width = EasyChartSeries.LineWidth.Thin; //细线的选择
        }
        /// <summary>
        /// 选择线宽Middle的处理内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinemiddleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((EasyChartSeries)LineSeries[currentClickCurve]).Width = EasyChartSeries.LineWidth.Middle; //细线的选择
        }
        /// <summary>
        /// 选择线宽Thick的处理内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinethickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((EasyChartSeries)LineSeries[currentClickCurve]).Width = EasyChartSeries.LineWidth.Thick; //细线的选择
        }
        /// <summary>
        /// 选择Point类型曲线的菜单单击事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Point;
        }

        private void InterFastlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.FastLine;
        }

        private void InterstepLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.StepLine;
        }

        private void InterLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
        }

        private void PointStylenoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO to confirm why
            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.None;
            ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.FastLine;
        }

        private void PointStylesquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EasyChartSeries.Interpolation.FastLine == ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle)
            {
                ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
            }

            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.Square;

        }

        private void PointStylecircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EasyChartSeries.Interpolation.FastLine == ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle)
            {
                ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
            }
            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.Circle;

        }

        private void PointStylediamodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EasyChartSeries.Interpolation.FastLine == ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle)
            {
                ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
            }

            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.Diamond;

        }

        private void PointStyletriangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EasyChartSeries.Interpolation.FastLine == ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle)
            {
                ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
            }
            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.Triangle;

        }

        private void setYAxisRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form setYAxisRange = new Set_YAxis_Range(this);
            Point clickPoint = this._chart.PointToClient(Control.MousePosition);//鼠标相对于窗体左上角的坐标
            clickPoint = new Point(clickPoint.X, clickPoint.Y);
            setYAxisRange.StartPosition = FormStartPosition.Manual;
            setYAxisRange.Location = clickPoint;
            setYAxisRange.Show();
        }

        private void PointStylestart4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EasyChartSeries.Interpolation.FastLine == ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle)
            {
                ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
            }
            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.Star4;
        }


        private void PointStylestart5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EasyChartSeries.Interpolation.FastLine == ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle)
            {
                ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
            }
            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.Star5;
        }

        private void PointStylestart6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EasyChartSeries.Interpolation.FastLine == ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle)
            {
                ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
            }
            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.Star6;
        }

        private void PointStylestart10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EasyChartSeries.Interpolation.FastLine == ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle)
            {
                ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
            }
            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.Star10;
        }

        private void PointStylecrossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EasyChartSeries.Interpolation.FastLine == ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle)
            {
                ((EasyChartSeries)LineSeries[currentClickCurve]).InterpolationStyle = EasyChartSeries.Interpolation.Line;
            }
            ((EasyChartSeries)LineSeries[currentClickCurve]).MarkerType = EasyChartSeries.PointStyle.Cross;
        }

        internal bool IsPlotting()
        {
            return PlotMode.NaN != _plotMode;
        }

        private void AdaptLineSeries()
        {
            SeriesCollection baseSeries = _chart.Series;
            for (int i = 0; i < baseSeries.Count; i++)
            {
                if (i >= LineSeries.Count)
                {
                    EasyChartSeries series = new EasyChartSeries(baseSeries[i].Name);
                    series.Color = Palette[i%Palette.Length];
                    LineSeries.AddInternal(series);
                }
                ((EasyChartSeries)LineSeries[i]).AdaptBaseSeries(baseSeries[i]);
            }
            for (int i = baseSeries.Count; i < LineSeries.Count; i++)
            {
                ((EasyChartSeries)LineSeries[i]).AdaptBaseSeries(null);
            }
            
        }

        private void RefreshAxesAndCursor(bool isXZoomed, bool isYZoomed, bool firstTimeRun)
        {
            RefreshAxis(isXZoomed, isYZoomed, firstTimeRun);
            RefreshCursor();
            RefreshCursorSeriesMenuItems();
        }

        private void RefreshAxis(bool isXZoomed, bool isYZoomed, bool firstTimeRun = false)
        {
            _axisX.SetAxisRangeValue(GetMaxXData(), GetMinXData());
            _axisX.RefreshAxisRange();
            _axisY.RefreshAxisRange();
            if (!firstTimeRun &&　isXZoomed && _fixAxisX)
            {
                _xMin = _axisX.ViewMinimum;
                _xMax = _axisX.ViewMaximum;
                RefreshAxisXIndexRange(_xStart, _xIncrement, _xDatalength, ref _xStartIndex, ref _xEndIndex);
                RefreshPlot();
            }
            else
            {
                _axisX.ResetAxisScaleView(isXZoomed);
            }
            if (firstTimeRun || !isYZoomed)
            {
                _axisY.ResetAxisScaleView(isYZoomed);
            }
            OnAxisViewChanged(_axisX, false, false);
        }

        private void RefreshCursor()
        {
            _xCursor.RefreshCursor();
            _yCursor.RefreshCursor();
            // TODO to modify later
            _xCursor.SetInterval(GetMinXInterval());
            _yCursor.SetInterval(1e-3);
        }

        internal void RefreshPlotDatas(EasyChartAxis axis)
        {
            if (AxisX.Name.Equals(axis.Name))
            {
                _xMin = _axisX.ViewMinimum;
                _xMax = _axisX.ViewMaximum;
                RefreshAxisXIndexRange(_xStart, _xIncrement, _xDatalength, ref _xStartIndex, ref _xEndIndex);
                RefreshPlot();
            }
        }

        private void RefreshCursorSeriesMenuItems()
        {
            // 如果不使能 ShowXYValue 则删除Cursor的Series选择，如果使能则添加
            if (Show_XYValuetoolStripMenuItem.Checked && PlotMode.NaN != _plotMode)
            {
                toolStripSeparator_CursorSelect.Visible = true;
                cursorSeriesRootMenuItem.Visible = true;
                RefreshCursorSeriesItems();
            }
            else
            {
                toolStripSeparator_CursorSelect.Visible = false;
                cursorSeriesRootMenuItem.Visible = false;
            }
        }

        private double GetMinXData()
        {
            double minData = double.MaxValue;
            foreach (DataEntity dataEntity in _plotDatas)
            {
                if (dataEntity.MinXValue < minData)
                {
                    minData = dataEntity.MinXValue;
                }
            }
            return minData;
        }

        const double MinDoubleValue = 1E-40;
        private double GetMaxXData()
        {
            double maxData = double.MinValue;
            DataEntity maxEntity = null;
            foreach (DataEntity dataEntity in _plotDatas)
            {
                if (dataEntity.MaxXValue > maxData)
                {
                    maxData = dataEntity.MaxXValue;
                    maxEntity = dataEntity;
                }
            }
            // TODO fix later.为了避免0-999这种情况做的特殊处理
            if (null != maxEntity && XDataInputType.Increment == maxEntity.XDataInputType)
            {
                maxData += maxEntity.XIncrement;
            }
            return maxData;
        }

        private double GetMinXInterval()
        {
            double minInterval = double.MaxValue;
            foreach (DataEntity dataEntity in _plotDatas)
            {
                if (dataEntity.XMinInterval < minInterval)
                {
                    minInterval = dataEntity.XMinInterval;
                }
            }
            return minInterval;
        }

        private int GetMaxDataSize()
        {
            int maxData = int.MinValue;
            foreach (DataEntity dataEntity in _plotDatas)
            {
                if (dataEntity.Size > maxData)
                {
                    maxData = dataEntity.Size;
                }
            }
            return maxData;
        }



        // TODO for debug
        internal SeriesCollection GetSeries()
        {
            return _chart.Series;
        }

        #endregion

        #endregion // Event Handler

        #region Enumeration Declaration

        /// <summary>
        /// Gradient Style of EasyChart
        /// </summary>
        public enum EasyChartGradientStyle
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

        internal enum PlotMode
        {
            /// <summary>
            /// Have not run plot yet
            /// </summary>
            NaN,
            /// <summary>
            /// 1D y plot
            /// </summary>
            Yof1D,
            /// <summary>
            /// 2D y plot
            /// </summary>
            Yof2D,
            /// <summary>
            /// 1D x and y plot
            /// </summary>
            XYof1D,
            /// <summary>
            /// 2D x and y plot
            /// </summary>
            XYof2D
        }

        /// <summary>
        /// Axis title display orientation
        /// 坐标轴名称方向
        /// </summary>
        public enum TitleOrientation
        {
            /// <summary>
            /// Auto
            /// </summary>
            Auto = TextOrientation.Auto,

            /// <summary>
            /// Horizental
            /// </summary>
            Horizontal = TextOrientation.Horizontal,

            /// <summary>
            /// Rotate 270 degrees
            /// </summary>
            Rotated270 = TextOrientation.Rotated270,

            /// <summary>
            /// Rotate 90 degrees
            /// </summary>
            Rotated90 = TextOrientation.Rotated90,

            /// <summary>
            /// Stacked
            /// </summary>
            Stacked = TextOrientation.Stacked
        }

        /// <summary>
        /// Axis title align postion
        /// 坐标轴名称对齐位置
        /// </summary>
        public enum TitlePosition
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
        #endregion
    }
}

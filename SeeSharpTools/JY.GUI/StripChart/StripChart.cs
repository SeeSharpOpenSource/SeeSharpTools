using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.StripChartUtility;

namespace SeeSharpTools.JY.GUI
{

    /// <summary>
    /// Chart to plot StripWave
    /// </summary>
    [Designer(typeof(StripChartDesigner))]
    [ToolboxBitmap(typeof(StripChart),"StripChart.StripChart.bmp")]
    public partial class StripChart : UserControl, IDisposable
    {

        #region Private Fields
        /// <summary>
        /// chart constants
        /// </summary>
        //默认显示的最大点数
        private const int DefaultLineNum = 1;
        private const string DefaultTimeStampFormat = "HH:mm:ss";

        /// <summary>
        /// Color and names for series
        /// </summary>
        private Color[] _palette = new Color[] {Color.Red};
        private int[] _lineWidth = new int[] {1};
        private string[] _seriesNames = new string[] { "Series1" };
        private int _lineNum = 1;

        private bool _isPlotStart = false;
        private const string ErrorMsgCaption = "参数错误";

        /// <summary>
        /// Legend property
        /// 标签显示
        /// </summary>
        private bool _legendVisibe = true;

        /// <summary>
        /// X/Y axis property
        /// X/Y 轴属性
        /// </summary>
        private bool _yIsLogarithmic = true;

        private double _lastYMax = 3.5;
        private double _lastYMin = 0;


        private readonly AxisViewAdapter _axisViewAdapter;

//        /// <summary>
//        /// X Axis range max
//        /// X 轴最大范围
//        /// </summary>
//        private double _yMax = 1000000000;
//        /// <summary>
//        /// X Axis range min
//        /// X 轴最小范围
//        /// </summary>
//        private double _yMin = -1000000000;

        /// <summary>
        /// Enum Display Mode
        /// 显示方向
        /// </summary>
        public enum DisplayDirection
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
        /// Private Fields
        /// 私有变量
        /// </summary>
        private DisplayDirection _displaydirection = DisplayDirection.RightToLeft;
        private XAxisDataType _xAxisType = XAxisDataType.Index;
        private string _timeStampFormat = DefaultTimeStampFormat;
        private int _xAxisStartIndex = 0;

        private List<ToolStripMenuItem> _seriesMenuItems = new List<ToolStripMenuItem>();
        private List<ToolStripMenuItem> _zoomMenuItems = new List<ToolStripMenuItem>();

        private long xAxisIndex = 0;

        private StripPlotter plotter;
        //First time Run mark of Plot1Dy,Plot2Dy,Plot1Dxy,Plot2Dxy

        #endregion

        #region Public Properties

        /// <summary>
        /// Set the color of chart.
        /// 设置图表背景色
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the color of strip chart.")
        ]
        public Color ChartBackColor
        {
            get { return chart.BackColor; }
            set
            {
                this.BackColor = value;
                chart.BackColor = value;
            }
        }

        /// <summary>
        /// Set the color of chart area.
        /// 设置图标绘图区背景色
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the color of strip chart area.")
        ]
        public Color ChartAreaBackColor
        {
            get { return chart.ChartAreas[0].BackColor; }
            set
            {
                chart.ChartAreas[0].BackColor = value;
            }
        }
        
        /// <summary>
        /// Is major grid enable
        /// 是否使能Major Grid
        /// </summary>
        [
            Browsable(false),
            CategoryAttribute("Appearance"),
            Description("Indicate whether enable scroll when sample count less than 'DisplayPoints'."),
            EditorBrowsable(EditorBrowsableState.Never)
        ]
        public bool ScrollMode
        {
            get { return plotter.ScrollMode; }
            set
            {
                plotter.ScrollMode = value;
            }
        }

        /// <summary>
        /// Select stripchart scroll type
        /// 选择StripChart滚动类型
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Select the scroll mode for strip chart"),
        ]
        public StripScrollType ScrollType {
            get { return plotter.ScrollMode ? StripScrollType.Scroll : StripScrollType.Cumulation; }
            set
            {
                switch (value)
                {
                    case StripScrollType.Cumulation:
                        plotter.ScrollMode = false;
                        break;
                    case StripScrollType.Scroll:
                        plotter.ScrollMode = true;
                        break;
                    default:
                        break;
                }
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
            get { return chart.ChartAreas[0].AxisX.MajorGrid.Enabled && chart.ChartAreas[0].AxisY.MajorGrid.Enabled; }
            set
            {
                chart.ChartAreas[0].AxisX.MajorGrid.Enabled = value;
                chart.ChartAreas[0].AxisY.MajorGrid.Enabled = value;
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
            get { return chart.ChartAreas[0].AxisX.MinorGrid.Enabled && chart.ChartAreas[0].AxisY.MinorGrid.Enabled; }
            set
            {
                chart.ChartAreas[0].AxisX.MinorGrid.Enabled = value;
                chart.ChartAreas[0].AxisY.MinorGrid.Enabled = value;
            }
        }

        /// <summary>
        /// Set Legend visible.
        /// 设置标签是否可见
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
            get { return chart.ChartAreas[0].AxisX.Title; }
            set { chart.ChartAreas[0].AxisX.Title = value; }
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
            get { return chart.ChartAreas[0].AxisY.Title; }
            set { chart.ChartAreas[0].AxisY.Title = value; }
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
            get { return (TitlePosition)chart.ChartAreas[0].AxisX.TitleAlignment; }
            set { chart.ChartAreas[0].AxisX.TitleAlignment = (StringAlignment)value; }
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
            get { return (TitlePosition)chart.ChartAreas[0].AxisY.TitleAlignment; }
            set { chart.ChartAreas[0].AxisY.TitleAlignment = (StringAlignment)value; }
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
            get { return (TitleOrientation)chart.ChartAreas[0].AxisX.TextOrientation; }
            set { chart.ChartAreas[0].AxisX.TextOrientation = (TextOrientation)value; }
        }

        /// <summary>
        /// Y axis title orientation
        /// Y轴名称方向
        /// </summary>
        /// /// <summary>
        /// X axis title orientation
        /// X轴标题方向
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify the orientation of Y axis title.")
        ]
        public TitleOrientation YTitleOrientation
        {
            get { return (TitleOrientation)chart.ChartAreas[0].AxisY.TextOrientation; }
            set { chart.ChartAreas[0].AxisY.TextOrientation = (TextOrientation)value; }
        }

        /// <summary>
        /// Whether enable X axis label
        /// 是否显示X轴的数值标签
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Appearance"),
            Description("Specify whether enable X axis label.")
        ]
        public bool XLabelEnabled
        {
            get { return chart.ChartAreas[0].AxisX.LabelStyle.Enabled; }
            set { chart.ChartAreas[0].AxisX.LabelStyle.Enabled = value; }
        }

        /// <summary>
        /// The line count to show in the chart.
        /// 定义显示的线数
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Series property"),
            Description("The line count display in chart.")
        ]

        public int LineNum
        {
            get { return _lineNum; }
            set
            {
                if (_isPlotStart)
                {
                    return;
                }
                AdaptSeriesNum(value);
                if (_lineNum == value)
                {
                    return;
                }
                if (_lineNum > Constants.MaxSeriesToDraw)
                {
                    value = Constants.MaxSeriesToDraw;
                }
                _lineNum = value;
                SetSeriesColors();
                SetSeriesNames();
                SetSeriesLineWidth();
                RefreshSeriesMenuItems();
                SetSeriesInitialvalue();
            }
        }

        /// <summary>
        /// Color palette, the colors in palette will be applied to series in sequence.
        /// 颜色画板，用于线条的颜色标识
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Series property"),
            Description("Specify a series of colors. Colors in palette will be set to each series in sequence.")
        ]

        public Color[] Palette
        {
            get { return _palette; }
            set
            {
                _palette = value; 
                SetSeriesColors();
            }
        }

        /// <summary>
        /// Line width of each lines
        /// 颜色画板，用于线条的颜色标识
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Series property"),
            Description("Specify the line width. Line widths in array will be set to each series in sequence.")
        ]

        public int[] LineWidth
        {
            get { return _lineWidth; }
            set
            {
                _lineWidth = value;
                SetSeriesLineWidth();
            }
        }

        /// <summary>
        /// 线条的名称
        /// Series名称
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Series property"),
            Description("Specify series names. Series names in palette will be set to each series in sequence.")
        ]
        public string[] SeriesNames
        {
            get { return _seriesNames; }
            set
            {
                _seriesNames = value; 
                SetSeriesNames();
            }
        }

        /// <summary>
        /// Set Y axis logarithmic.
        /// Y轴是否对数显示
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Specify Y axis is linear or logarithmic scale.")
        ]
        public bool YAxisLogarithmic
        {
            get { return chart.ChartAreas[0].AxisY.IsLogarithmic; }
            set
            {
                if (_isPlotStart)
                {
                    return;
                }
                _yIsLogarithmic = value;
                chart.ChartAreas[0].AxisY.IsLogarithmic = _yIsLogarithmic;
            }
        }

        private bool _yAutoEnabled;
        /// <summary>
        /// set double.NaN is auto scale
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Specify if Y axis range will be set in auto mode.")
        ]
        public bool YAutoEnable
        {
            get
            {
                return _yAutoEnabled;
            }
            set
            {
                if (value == _yAutoEnabled)
                {
                    return;
                }
                _yAutoEnabled = value;
                if (_yAutoEnabled)
                {
                    _lastYMax = chart.ChartAreas[0].AxisY.Maximum;
                    _lastYMin = chart.ChartAreas[0].AxisY.Minimum;
                    plotter.PlotAction?.RefreshYAxisRange(0, double.NaN, double.NaN);
                }
                else
                {
                    chart.ChartAreas[0].AxisY.Maximum = _lastYMax;
                    chart.ChartAreas[0].AxisY.Minimum = _lastYMin;
                }
                _axisViewAdapter.RefreshYGrid();
            }
        }

        /// <summary>
        /// set double.NaN is auto scale
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Specify the maximum value of Y Axis. Available when YAutoEnable is true.")
        ]
        public double AxisYMax
        {
            get
            {
                return chart.ChartAreas[0].AxisY.Maximum;
            }

            set
            {
                if (YAutoEnable || double.IsNaN(value) || value <= AxisYMin)
                {
                    return;
                }
                chart.ChartAreas[0].AxisY.Maximum = value;
                _lastYMax = value;
                _axisViewAdapter.RefreshYGrid();
            }
        }
        /// <summary>
        /// set double.NaN is auto scale
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Specify the minimum value of Y Axis. Available when YAutoEnable is true.")
        ]
        public double AxisYMin
        {
            get
            {
                return chart.ChartAreas[0].AxisY.Minimum;
            }

            set
            {
                if (YAutoEnable || double.IsNaN(value) || value >= AxisYMax)
                {
                    return;
                }
                chart.ChartAreas[0].AxisY.Minimum = value;
                _lastYMin = value;
                _axisViewAdapter.RefreshYGrid();
            }
        }

        /// <summary>
        /// Display Direction.
        /// 显示方向
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Specify display direction.")
        ]
        public DisplayDirection Displaydirection
        {
            get { return _displaydirection; }

            set
            {
                _displaydirection = value;
                chart.ChartAreas[0].AxisX.IsReversed = (_displaydirection == DisplayDirection.LeftToRight);
            }
        }

        /// <summary>
        /// Maximum point count to show in single line
        /// 单条线最多显示的点数
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Specify the max number of points in each line to show.")
        ]
        public int DisPlayPoints
        {
            get { return plotter.MaxSampleNum; }
            set
            {
                if (_isPlotStart)
                {
                    return;
                }
                plotter.MaxSampleNum = (value <= 0) ? Constants.MaxPointsInSingleSeries : value;
            }
        }

        /// <summary>
        /// Specify the x axis label type
        /// 配置X轴显示的类型
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Specify X axis data type")
        ]
        public XAxisDataType XAxisTypes
        {
            get { return _xAxisType; }
            set { _xAxisType = value; }
        }

        /// <summary>
        /// Time stamp format
        /// 时间戳格式
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            DefaultValueAttribute(DefaultTimeStampFormat),
            Description("Specify the time stamp format. Only available when XAxisDataType is TimeStamp.")
        ]
        public string TimeStampFormat
        {
            get { return _timeStampFormat; }
            set
            {
                _timeStampFormat = value;
            }
        }

        [Browsable(false)]
        public DateTime NextTimeStamp { get; set; }

        public TimeSpan TimeInterval { get; set; }

        /// <summary>
        /// Start value of X axis index
        /// X轴索引起始值
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Behavior"),
            Description("Specify the start index of x axis. Only available when XAxisDataType is Index.")
        ]
        public int XAxisStartIndex
        {
            get { return _xAxisStartIndex; }

            set
            {
                _xAxisStartIndex = value;
                xAxisIndex = value;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// 构造函数
        /// </summary>
        public StripChart()
        {
            Control.CheckForIllegalCrossThreadCalls = true;
            InitializeComponent();
            // 设计器中自动配置了Name会导致在设计时获取控件名称失败
            this.Name = "";
            //            chart.ChartAreas[0].AxisX.LabelStyle.Format = DefaultTimeStampFormat;
            //            chart.ChartAreas[0].AxisX.IsMarginVisible = false;

            _yAutoEnabled = true;

            // Set default legend visible;
            _legendVisibe = true;
            SetLegendVisible();

            ResetDrawingParams();
            this._axisViewAdapter = new AxisViewAdapter(this, this.chart);
            this.plotter = new StripPlotter(chart, _axisViewAdapter);
            this._axisViewAdapter.SetPlotter(this.plotter);
            SetSeriesInitialvalue();

            _zoomMenuItems.Add(ToolStripMenuItem_zoomX);
            _zoomMenuItems.Add(ToolStripMenuItem_zoomY);
            _zoomMenuItems.Add(ToolStripMenuItem_zoomWindow);
            _zoomMenuItems.Add(ToolStripMenuItem_showValue);

            NextTimeStamp = DateTime.MinValue;
            TimeInterval = TimeSpan.Zero;

            this.Clear();
        }
        
        private void ResetDrawingParams()
        {
            xAxisIndex = _xAxisStartIndex;
            _isPlotStart = false;
        }

        #endregion

        #region Methods

        #region 单点绘制方法
        /// <summary>
        /// 绘制多条曲线的一组点，TimeStamp和Index模式可用
        /// </summary>
        /// <param name="lineDatas">待显示数据</param>
        public void PlotSingle(double[] lineDatas)
        {
            CheckXAxisType(XAxisDataType.TimeStamp, XAxisDataType.Index);
            PlotXYData(lineDatas, GetXAxisData());
        }

        /// <summary>
        /// 绘制单条曲线的一个点，TimeStamp和Index模式可用
        /// </summary>
        /// <param name="lineDatas">待显示数据</param>
        public void PlotSingle(double lineDatas)
        {
            CheckXAxisType(XAxisDataType.TimeStamp, XAxisDataType.Index);
            PlotXYData(new double[] { lineDatas }, GetXAxisData());
        }

        /// <summary>
        /// 绘制多条曲线的一组点，TimeStamp模式可用
        /// </summary>
        /// <param name="lineDatas">待显示数据</param>
        /// <param name="time">时间信息</param>
        public void PlotSingle(double[] lineDatas, DateTime time)
        {
            CheckXAxisType(XAxisDataType.TimeStamp);
            PlotXYData(lineDatas, time.ToString(_timeStampFormat));
        }

        /// <summary>
        /// 绘制单条曲线的一个点，TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="time">时间信息</param>
        public void PlotSingle(double lineData, DateTime time)
        {
            CheckXAxisType(XAxisDataType.TimeStamp);
            PlotXYData(new[] { lineData }, time.ToString(_timeStampFormat));
        }

        /// <summary>
        /// 绘制多条曲线的一组点，String模式可用
        /// </summary>
        /// <param name="lineDatas">待显示数据</param>
        /// <param name="xLabel">X轴待显示内容</param>
        public void PlotSingle(double[] lineDatas, string xLabel)
        {
            CheckXAxisType(XAxisDataType.String);
            PlotXYData(lineDatas, xLabel);
        }

        /// <summary>
        /// 绘制一条曲线的一个点，String模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabel">X轴待显示内容</param>
        public void PlotSingle(double lineData, string xLabel)
        {
            CheckXAxisType(XAxisDataType.String);
            PlotXYData(new double[] { lineData }, xLabel);
        }
        #endregion

        #region 多点绘制

        /// <summary>
        /// 绘制多条曲线的多个点，String模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabels">X轴待显示内容</param>
        public void Plot(double[,] lineData, string[] xLabels)
        {
            CheckXAxisType(XAxisDataType.String);
            PlotXYData(lineData, xLabels);
        }

        /// <summary>
        /// 绘制一条曲线的多个点，String模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabels">X轴待显示内容</param>
        public void Plot(double[] lineData, string[] xLabels)
        {
            CheckXAxisType(XAxisDataType.String);
            PlotXYData(lineData, xLabels);
        }

        /// <summary>
        /// 绘制多条曲线的多个点，TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabels">X轴待显示内容</param>
        public void Plot(double[,] lineData, DateTime[] xLabels)
        {
            CheckXAxisType(XAxisDataType.TimeStamp);
            PlotXYData(lineData, ConvertDateTimeToString(xLabels));
        }

        /// <summary>
        /// 绘制一条曲线的多个点，TimeStamp模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        /// <param name="xLabels">X轴待显示内容</param>
        public void Plot(double[] lineData, DateTime[] xLabels)
        {
            CheckXAxisType(XAxisDataType.TimeStamp);
            PlotXYData(lineData, ConvertDateTimeToString(xLabels));
        }

        /// <summary>
        /// 绘制多条曲线的多个点，Index模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        public void Plot(double[,] lineData)
        {
            CheckXAxisType(XAxisDataType.Index, XAxisDataType.TimeStamp);
            PlotYData(lineData, lineData.GetLength(0), lineData.GetLength(1));
        }

        /// <summary>
        /// 绘制多条曲线的多个点，Index模式可用
        /// </summary>
        /// <param name="lineData">待显示数据</param>
        public void Plot(double[] lineData)
        {
            CheckXAxisType(XAxisDataType.Index, XAxisDataType.TimeStamp);
            // 如果用户配置的线条个数和输入数组的长度一致，则认为是n个通道的1个样点，否则认为是1个通道的n个样点
            int lineNum = _lineNum == lineData.Length ? _lineNum : 1;
            PlotYData(lineData, lineNum, lineData.Length/lineNum);
        }

        #endregion

        public void Clear()
        {
            plotter.Clear();
            ResetDrawingParams();
            RefreshCursorStatus();
            _axisViewAdapter.Clear();
            SetSeriesInitialvalue();
            RefreshSeriesMenuItems();
        }

        private string[] ConvertDateTimeToString(DateTime[] dateTimes)
        {
            string[] timeStrs = new string[dateTimes.Length];
            for (int i = 0; i < dateTimes.Length; i++)
            {
                timeStrs[i] = dateTimes[i].ToString(_timeStampFormat);
            }
            return timeStrs;
        }

        private string[] CreateXAxisIndexStrs(int pointSize)
        {
            string[] indexStrs = new string[pointSize];
            for (int i = 0; i < pointSize; i++)
            {
                indexStrs[i] = xAxisIndex++.ToString();
            }
            return indexStrs;
        }

        private void PlotXYData(double[] lineDatas, string xLabel)
        {
            InitPlotter(lineDatas.Length);
            CheckLineNum(lineDatas.Length);
//            CheckXYDim(lineDatas.Length, xLabel.Length);
            plotter.PlotAction.Plot(xLabel, lineDatas);
        }

        private void PlotXYData(double[,] lineDatas, string[] xLabels)
        {
            InitPlotter(lineDatas.GetLength(0));
            CheckLineNum(lineDatas.GetLength(0));
            CheckXYDim(lineDatas.GetLength(1), xLabels.Length);
            plotter.PlotAction.Plot(xLabels, lineDatas);
        }

        private void PlotXYData(double[] lineDatas, string[] xLabels)
        {
            InitPlotter(1);
            CheckLineNum(1);
            CheckXYDim(lineDatas.Length, xLabels.Length);
            plotter.PlotAction.Plot(xLabels, lineDatas);
        }

        private void PlotYData(Array yData, int lineNum, int sampleCount)
        {
            InitPlotter(lineNum);
            CheckLineNum(lineNum);
            Action<IList<string>, int> fillXDataFunc = null;
            if (XAxisDataType.TimeStamp == _xAxisType)
            {
                fillXDataFunc = FillTimeStampToBuffer;
            }
            else if (XAxisDataType.Index == _xAxisType)
            {
                fillXDataFunc = FillIndexToBuffer;
            }
            plotter.PlotAction.Plot(yData, sampleCount, fillXDataFunc);
        }

        private void InitPlotter(int dataDim)
        {
            // 如果原绘图已经结束则开始绘图准备
            if (PlotStatus.Idle == plotter.PlotStatus)
            {
                _isPlotStart = true;
                LineNum = dataDim;
                // 关闭点位置和索引号的关联
                foreach (Series series in chart.Series)
                {
                    series.IsXValueIndexed = false;
                }
                _axisViewAdapter.InitAxisParams();
                RefreshCursorStatus();
                plotter.StartPlot();
            }
        }

        private void CheckLineNum(int dataDim)
        {
            if (_lineNum != dataDim)
            {
                throw new InvalidOperationException($"Current data line count {dataDim} is not the same as configured value {_lineNum}.");
            }
        }

        private void CheckXYDim(int yDataDim, int xDataDim)
        {
            if (yDataDim != xDataDim)
            {
                throw new InvalidOperationException($"Sample count of y data{yDataDim} is not the same as the sample count of x data {xDataDim}.");
            }
        }
        /// <summary>
        /// 设置线条颜色属性
        /// Set color of series according to property "_seriesColors"
        /// </summary>
        private void SetSeriesColors()
        {
            int setSeriesCount = (_palette.Length >= _lineNum) ? _lineNum : _palette.Length;
            for (int i = 0; i < setSeriesCount; i++) { chart.Series[i].Color = _palette[i]; }
        }

        /// <summary>
        /// 设置线宽属性
        /// </summary>
        private void SetSeriesLineWidth()
        {
            const int maxWidth = 10;
            const int minWidth = 1;
            int setSeriesCount = (_lineWidth.Length >= _lineNum) ? _lineNum : _lineWidth.Length;
            for (int i = 0; i < setSeriesCount; i++)
            {
                if (_lineWidth[i] > maxWidth)
                {
                    _lineWidth[i] = maxWidth;
                }
                else if (_lineWidth[i] < minWidth)
                {
                    _lineWidth[i] = minWidth;
                }
                chart.Series[i].BorderWidth = _lineWidth[i];
            }
        }

        /// <summary>
        /// 设置线条名字属性
        /// Set names of series according to property "_seriesNames"
        /// </summary>
        private void SetSeriesNames()
        {
            int setSeriesCount = (_seriesNames.Length >= _lineNum) ? _lineNum : _seriesNames.Length;
            for (int i = 0; i < setSeriesCount; i++) { chart.Series[i].Name = _seriesNames[i]; }
            int index = setSeriesCount;
            while (index < chart.Series.Count)
            {
                chart.Series[index].Name = $"Series{index + 1}";
                index++;
            }
        }

        /// <summary>
        /// 设置线条初始值
        /// Set initial value of series 
        /// </summary>
        private void SetSeriesInitialvalue()
        {
            const int defaultXRange = 1001;
            // 保证数据不可见
            const double defaultYValue = 1000000;
            chart.ChartAreas[0].AxisX.Maximum = defaultXRange;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            double[] defaultView = new double[defaultXRange];
            string[] xData = new string[defaultXRange];
            for (int i = 0; i < defaultXRange; i++)
            {
                defaultView[i] = defaultYValue;
                xData[i] = " ";
            }
            for (int i = 0; i < _lineNum; i++)
            {
                chart.Series[i].Points.DataBindXY(xData, defaultView);
            }
        }

        /// <summary>
        /// 设置标签是否显示
        /// Set if legend visible
        /// </summary>
        private void SetLegendVisible()
        {
            chart.Legends[0].Enabled = _legendVisibe;
        }

        private void CheckXAxisType(params XAxisDataType[] availableTypes)
        {
            if (!availableTypes.Contains(_xAxisType))
            {
                throw new InvalidOperationException($"Mehtod cannot be called when XAxisType is {_xAxisType}.");
            }
        }

        private string GetXAxisData()
        {
            string xAxisData = "";
            switch (_xAxisType)
            {
                case XAxisDataType.Index:
                    xAxisData = xAxisIndex++.ToString();
                    break;
                case XAxisDataType.TimeStamp:
                    DateTime nextTime = DateTime.MinValue == NextTimeStamp ? DateTime.Now : NextTimeStamp;
                    xAxisData = nextTime.ToString(_timeStampFormat);
                    break;
                case XAxisDataType.String:
                    break;
            }
            return xAxisData;
        }

        /// <summary>
        /// 释放函数
        /// </summary>
        protected new void Dispose()
        {
            plotter.Dispose();
            base.Dispose();
        }

        /// <summary>
        /// 获取坐标值
        /// Get X/Y axis Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tipControl"></param>
        private void chart_GetToolTipText(object sender, ToolTipEventArgs tipControl)
        {
            if (tipControl.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int pointIndex = tipControl.HitTestResult.PointIndex - 1;
                DataPoint dp = tipControl.HitTestResult.Series.Points[pointIndex];
                //分别显示x轴和y轴的数值，其中{1:F2},表示显示的是float类型，精确到小数点后2位。  
                tipControl.Text = $"X轴:{dp.AxisLabel}{Environment.NewLine}数值:{dp.YValues[0]} ";
            }
        }

        private void AdaptSeriesNum(int value)
        {
            if (chart.Series.Count < value)
            {
                AddSeries(value);
            }
            else if (chart.Series.Count > value)
            {
                ReleaseSeries(value);
            }
        }

        private void AddSeries(int value)
        {
            while (chart.Series.Count != value)
            {
                Series newSeries = new Series();
                newSeries.ChartType = SeriesChartType.FastLine;
                chart.Series.Add(newSeries);
            }
        }

        private void ReleaseSeries(int value)
        {
            while (chart.Series.Count != value)
            {
                chart.Series.RemoveAt(chart.Series.Count - 1);
            }
        }

        private void FillTimeStampToBuffer(IList<string> buffer, int sampleSize)
        {
            DateTime timestamp = NextTimeStamp;
            if (NextTimeStamp == DateTime.MinValue)
            {
                timestamp = DateTime.Now;
            }
            for (int i = 0; i < sampleSize; i++)
            {
                buffer.Add(timestamp.ToString(_timeStampFormat));
                timestamp += TimeInterval;
            }
            if (DateTime.MinValue != NextTimeStamp)
            {
                NextTimeStamp = timestamp;
            }
        }

        private void FillIndexToBuffer(IList<string> buffer, int sampleSize)
        {
            long index = xAxisIndex;
            for (int i = 0; i < sampleSize; i++)
            {
                buffer.Add(index++.ToString());
            }
            xAxisIndex = index;
        }

        #endregion

        #region Events

        private void legendVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            legendVisibleToolStripMenuItem.Checked = !legendVisibleToolStripMenuItem.Checked;
            LegendVisible = legendVisibleToolStripMenuItem.Checked;
        }

        private void YAutoScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YAutoScaleToolStripMenuItem.Checked = !YAutoScaleToolStripMenuItem.Checked;
            YAutoEnable = YAutoScaleToolStripMenuItem.Checked;
            setYAxisRangeToolStripMenuItem.Visible = !YAutoEnable;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void StripChart_Load(object sender, EventArgs e)
        {
            InitRightClickMenuItems();
            RefreshSeriesMenuItems();
        }

        private void InitRightClickMenuItems()
        {
            legendVisibleToolStripMenuItem.Checked = LegendVisible;
            YAutoScaleToolStripMenuItem.Checked = YAutoEnable;
            setYAxisRangeToolStripMenuItem.Visible = !YAutoEnable;
        }


        private void RefreshSeriesMenuItems()
        {
//            if (!isPlotStart)
//            {
//                return;
//            }
            SeriesCollection plotSeries = chart.Series;
            for (int i = 0; i < plotSeries.Count; i++)
            {
                if (_seriesMenuItems.Count <= i)
                {
                    ToolStripMenuItem seriesItem = new ToolStripMenuItem(plotSeries[i].Name);
                    seriesItem.Click += SeriesMenuItemClick_Click;
//                    seriesItem.Checked = plotSeries[i].Enabled;
                    _seriesMenuItems.Add(seriesItem);
                    contextMenuRightClick.Items.Add(seriesItem);
                }
                else
                {
                    if (!_seriesMenuItems[i].Text.Equals(plotSeries[i].Name))
                    {
                        _seriesMenuItems[i].Text = plotSeries[i].Name;
                    }
//                    _seriesMenuItems[i].Checked = plotSeries[i].Enabled;
                }
                _seriesMenuItems[i].Checked = plotSeries[i].Enabled;
            }
            // Remove extra items
            while (plotSeries.Count < _seriesMenuItems.Count)
            {
                ToolStripMenuItem lastSeriesItem = _seriesMenuItems[_seriesMenuItems.Count - 1];
                _seriesMenuItems.RemoveAt(_seriesMenuItems.Count - 1);
                contextMenuRightClick.Items.Remove(lastSeriesItem);
            }
        }

        private void SeriesMenuItemClick_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem source = sender as ToolStripMenuItem;
            if (null == source)
            {
                return;
            }
            int index = _seriesMenuItems.FindIndex(item => item.Text == source.Text);
            if (index < 0 || index >= chart.Series.Count)
            {
                return;
            }
            source.Checked = !source.Checked;
            chart.Series[index].Enabled = source.Checked;
        }

        private void setYAxisRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Set_StripChart_YAxis_Range setForm = new Set_StripChart_YAxis_Range(this);
            Point clickPoint = this.chart.PointToClient(MousePosition); //鼠标相对于窗体左上角的坐标
            clickPoint = new Point(clickPoint.X, clickPoint.Y);
            setForm.StartPosition = FormStartPosition.Manual;
            setForm.Location = clickPoint;
            setForm.Show();
        }

        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isPlotStart && MouseButtons.Right == e.Button)
            {
                //获取节点区域的右下角坐标值
                Point pos = new Point(e.Location.X, e.Location.Y);
                contextMenuRightClick.Show(chart, pos);
            }
        }

        private void ZoomMenuItemAction(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            bool isChecked = !item.Checked;
            foreach (ToolStripMenuItem menuItem in _zoomMenuItems)
            {
                menuItem.Checked = false;
            }
            item.Checked = isChecked;
            _axisViewAdapter.SetCursorFunction(ToolStripMenuItem_zoomX.Checked || ToolStripMenuItem_zoomWindow.Checked,
                ToolStripMenuItem_zoomY.Checked || ToolStripMenuItem_zoomWindow.Checked);
        }

        private void chart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (null == e.Axis || null == e.ChartArea)
            {
                return;
            }
            // 如果是Y轴的缩放
            if (ReferenceEquals(e.Axis, e.ChartArea.AxisY))
            {
                _axisViewAdapter.RefreshYGrid();
            }
            else if (ReferenceEquals(e.Axis, e.ChartArea.AxisX))
            {
                _axisViewAdapter.RefreshXGrid();
                _axisViewAdapter.RefreshXLabels();
                plotter.PlotAction.RefreshPlot(0);
            }
        }

        private void toolStripMenuItem_zoomReset_Click(object sender, EventArgs e)
        {
            _axisViewAdapter.ZoomReset();
            plotter.PlotAction?.RefreshPlot(0);
        }

        private void RefreshCursorStatus()
        {
            if (!_isPlotStart)
            {
                chart.ChartAreas[0].CursorX.IsUserEnabled = false;
                chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chart.ChartAreas[0].CursorY.IsUserEnabled = false;
                chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
                return;
            }
            chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = ToolStripMenuItem_zoomX.Checked ||
                                                                 ToolStripMenuItem_zoomWindow.Checked;
            chart.ChartAreas[0].CursorX.IsUserEnabled = chart.ChartAreas[0].CursorX.IsUserSelectionEnabled ||
                                                        ToolStripMenuItem_showValue.Checked;

            chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = ToolStripMenuItem_zoomY.Checked ||
                                                                 ToolStripMenuItem_zoomWindow.Checked;
            chart.ChartAreas[0].CursorY.IsUserEnabled = chart.ChartAreas[0].CursorY.IsUserSelectionEnabled ||
                                                        ToolStripMenuItem_showValue.Checked;
        }

        #endregion


        #region Enumerations

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

        #endregion
        
    }

}

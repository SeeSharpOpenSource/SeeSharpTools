using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
//    [TypeConverter(typeof(ExpandableObjectConverter))]
//    [Serializable]
    public class DigitalChartSeries
    {
        public DigitalChartSeries()
        {
            _name = "";
            _baseSeries = null;
//            _name = "Series";
            _color = Color.Red;
            _lineWidth = LineWidth.Thin;
            _type = LineType.FastLine;
            _marker = MarkerType.None;

            _xPlotAxis = DigitalChartAxis.PlotAxis.Primary;
            _yPlotAxis = DigitalChartAxis.PlotAxis.Primary;
        }

        private Series _baseSeries;
        private DigitalChartSeriesCollection _seriesCollection = null;

        internal void SetSeriesCollecton(DigitalChartSeriesCollection seriesCollection)
        {
            this._seriesCollection = seriesCollection;
        }

        #region Properties

        private string _name;

        [
            Browsable(true),
            NotifyParentProperty(true),
            Category("Data"),
            Description("Get or set series name.")
        ]
        public string Name
        {
            get { return _name; }
            set
            {
                if (null == value ||
                    (null != _seriesCollection && _seriesCollection.Any(item => item.Name.Equals(value))))
                {
                    return;
                }
                _name = value;
                if (null != _baseSeries)
                {
                    _baseSeries.Name = _name;
                }
            }
        }

        private Color _color;

        [
            Browsable(true),
            NotifyParentProperty(true),
            Category("Apperance"),
            Description("Get or set the color of series.")
        ]
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (null != _baseSeries)
                {
                    _baseSeries.Color = _color;
                }
            }
        }

        [
            Browsable(false),
            Category("Behavior"),
            Description("Get or set the visibility of series.")
        ]
        public bool Visible
        {
            get { return _baseSeries?.Enabled ?? false; }
            set
            {
                if (null != _baseSeries)
                {
                    _baseSeries.Enabled = value;
                }
            }
        }

        private LineWidth _lineWidth;

        [
            Browsable(true),
            NotifyParentProperty(true),
            Category("Apperance"),
            Description("Get or set the color of series.")
        ]
        public LineWidth Width
        {
            get { return _lineWidth; }
            set
            {
                _lineWidth = value;
                if (null != _baseSeries)
                {
                    _baseSeries.BorderWidth = (int) _lineWidth;
                    _baseSeries.MarkerSize = 3*_baseSeries.BorderWidth + 2;
                }
            }
        }


        private DigitalChartAxis.PlotAxis _xPlotAxis;

        [
            Browsable(true),
            Category("Behavior"),
            Description("Specify which X axis to show series.")
        ]
        public DigitalChartAxis.PlotAxis XPlotAxis
        {
            get { return _xPlotAxis; }
            set
            {
                // TODO 暂时封闭该接口的配置
                return;
                if (_xPlotAxis == value)
                {
                    return;
                }
                _xPlotAxis = value;
                _seriesCollection?.RefreshPlotAxis(this);
            }
        }

        private DigitalChartAxis.PlotAxis _yPlotAxis;

        [
            Browsable(true),
            Category("Behavior"),
            Description("Specify which Y axis to show series.")
        ]
        public DigitalChartAxis.PlotAxis YPlotAxis
        {
            get { return _yPlotAxis; }
            set
            {
                if (_yPlotAxis == value)
                {
                    return;
                }
                _yPlotAxis = value;
                _seriesCollection?.RefreshPlotAxis(this);
            }
        }

        private LineType _type;

        [
            Browsable(true),
            NotifyParentProperty(true),
            Category("Apperance"),
            Description("Specify the line type of series.")
        ]
        public LineType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                if (null != _baseSeries)
                {
                    // 如果marker不为none，则自动将线型修改为Line
                    _baseSeries.ChartType = (MarkerType.None == _marker) ? (SeriesChartType)_type : SeriesChartType.Line;
                }
            }
        }

        private MarkerType _marker;

        [
            NotifyParentProperty(true),
            Category("Apperance"),
            Description("Specify the marker type of series.")
        ]
        public MarkerType Marker
        {
            get { return _marker; }
            set
            {
                _marker = value;
                if (null != _baseSeries)
                {
                    _baseSeries.MarkerStyle = (MarkerStyle) _marker;
                    // 如果marker不为none，则自动将线型修改为Line
                    _baseSeries.ChartType = (MarkerType.None == _marker) ? (SeriesChartType)_type : SeriesChartType.Line;
                }
            }
        }

        #endregion

        // 绑定EasyChartXSeries和真实的Series，根据用户配置修改对应真实线条的配置
        internal void AdaptBaseSeries(Series baseSeries)
        {
            if (null == baseSeries)
            {
                this._baseSeries = null;
                return;
            }
            this._baseSeries = baseSeries;
            baseSeries.Name = this._name;
            baseSeries.Color = this._color;
            baseSeries.BorderWidth = (int) this._lineWidth;
            baseSeries.ChartType = (SeriesChartType) this._type;
            baseSeries.MarkerStyle = (MarkerStyle) this._marker;
            _seriesCollection.RefreshPlotAxis(this);
        }

        // 取消配置的Series和EasyChartXSeries的绑定
        internal void DetachBaseSeries()
        {
            this._baseSeries = null;
        }
        
        // 更新线条显示的坐标轴，如果在分区视图，则始终使用主坐标轴显示。
        // 坐标轴更新在新建线条/分区视图切换/用户修改时需要更新
        // 需要获取isSplitView，所以必须委托SeriesCollection去执行，后期考虑再优化
        internal void RefreshPlotAxis(bool isSplitView)
        {
            if (null == _baseSeries) return;
            if (isSplitView)
            {
                _baseSeries.XAxisType = AxisType.Primary;
                _baseSeries.YAxisType = AxisType.Primary;
            }
            else
            {
                _baseSeries.XAxisType = (AxisType)_xPlotAxis;
                _baseSeries.YAxisType = (AxisType)_yPlotAxis;
            }
        }

        public void Dispose()
        {
            // ignore
        }

        //        public void GetObjectData(SerializationInfo info, StreamingContext context)
        //        {
        //            // TODO to fix
        //            info.AddValue("Name", Name);
        ////            info.AddValue("Color", Color);
        ////            info.AddValue("Width", Width);
        ////            info.AddValue("InterpolationStyle", InterpolationStyle);
        ////            info.AddValue("MarkerType", MarkerType);
        //        }


        #region Enumeration

        /// <summary>
        /// 线宽
        /// </summary>
        public enum LineWidth
        {
            /// <summary>
            /// 细线宽
            /// </summary>
            Thin = 1,

            /// <summary>
            /// 中等线宽
            /// </summary>
            Middle = 2,

            /// <summary>
            /// 粗线宽
            /// </summary>
            Thick = 3
        }

        /// <summary>
        /// 线型
        /// </summary>
        public enum LineType
        {
            /// <summary>
            /// 点状线
            /// </summary>
            Point = SeriesChartType.Point,

            /// <summary>
            /// 快速扫描线
            /// </summary>
            FastLine = SeriesChartType.FastLine,

            /// <summary>
            /// 直线
            /// </summary>
            Line = SeriesChartType.Line,

            /// <summary>
            /// 阶梯线
            /// </summary>
            StepLine = SeriesChartType.StepLine
        }

        public enum MarkerType
        {
            /// <summary>
            /// 无标记
            /// </summary>
            None = MarkerStyle.None,

            /// <summary>
            /// 正方形标记
            /// </summary>
            Square = MarkerStyle.Square,

            /// <summary>
            /// 圆形标记
            /// </summary>
            Circle = MarkerStyle.Circle,

            /// <summary>
            /// 菱形标记
            /// </summary>
            Diamond = MarkerStyle.Diamond,

            /// <summary>
            /// 三角形标记
            /// </summary>
            Triangle = MarkerStyle.Triangle,

            /// <summary>
            /// 交叉线
            /// </summary>
            Cross = MarkerStyle.Cross,

            /// <summary>
            /// 四角星标记
            /// </summary>
            Star4 = MarkerStyle.Star4,

            /// <summary>
            /// 五角星标记
            /// </summary>
            Star5 = MarkerStyle.Star5,

            /// <summary>
            /// 六角星标记
            /// </summary>
            Star6 = MarkerStyle.Star6,

            /// <summary>
            /// 十角星标记
            /// </summary>
            Star10 = MarkerStyle.Star10,

        }

        #endregion

    }
}
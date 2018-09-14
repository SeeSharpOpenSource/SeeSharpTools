using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI
{
    public class StripChartXPlotArea
    {
        internal ChartArea ChartArea { get; }

        private readonly StripChartX _parentChart;
//        private readonly AxisSynchronizer _xAxisSync;
        internal AxisSynchronizer YAxisSync { get; }

        internal StripChartXPlotArea(StripChartX parentChart, ChartArea chartArea)
        {
            this._parentChart = parentChart;
            this.ChartArea = chartArea;
            this.ChartArea.Position.Auto = false;
            this._enabled = chartArea.Visible;

            this._axisX = new StripChartXAxis();
            this._axisX2 = new StripChartXAxis();
            this._axisY = new StripChartXAxis();
            this._axisY2 = new StripChartXAxis();
            _axisX.Initialize(parentChart, this, chartArea.AxisX);
            _axisX.InitAxisViewRange(Constants.DefaultXMax, Constants.DefaultXMin, Constants.DefaultXMax, Constants.DefaultXMin);

            _axisX2.Initialize(parentChart, this, chartArea.AxisX2);
            _axisX2.InitAxisViewRange(Constants.DefaultXMax, Constants.DefaultXMin, Constants.DefaultXMax, Constants.DefaultXMin);

            _axisY.Initialize(parentChart, this, chartArea.AxisY);
            _axisY.InitAxisViewRange(Constants.DefaultYMax, Constants.DefaultYMin, Constants.DefaultYMax, Constants.DefaultYMin);

            _axisY2.Initialize(parentChart, this, chartArea.AxisY2);
            _axisY2.InitAxisViewRange(Constants.DefaultYMax, Constants.DefaultYMin, Constants.DefaultYMax, Constants.DefaultYMin);

            _axes[0] = _axisX;
            _axes[1] = _axisX2;
            _axes[2] = _axisY;
            _axes[3] = _axisY2;

            // Initialize cursor classes
            _xCursor = new StripChartXCursor(_parentChart, this, chartArea.CursorX, chartArea.AxisX, "X cursor");
            _xCursor.Mode = StripChartXCursor.CursorMode.Zoom;
            _yCursor = new StripChartXCursor(_parentChart, this, chartArea.CursorY, chartArea.AxisY, "Y cursor");
            _yCursor.Mode = StripChartXCursor.CursorMode.Disabled;
            _cursors[0] = _xCursor;
            _cursors[1] = _yCursor;

//            _xAxisSync = new AxisSynchronizer(_axisX, _axisX2);
            YAxisSync = new AxisSynchronizer(_axisY, _axisY2);
        }

        private bool _enabled;

        /// <summary>
        /// Plot area name
        /// </summary>
        public string Name => ChartArea.Name;

        internal bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (null != ChartArea)
                {
                    ChartArea.Visible = value;
                }
                _enabled = value;
            }
        }

        [
            Browsable(true),
            Category("Appearance"),
            Description("Set the BackColor of ChartArea.")
        ]
        public Color BackColor
        {
            get
            {
                return ChartArea.BackColor;
            }

            set
            {
                ChartArea.BackColor = value;
            }

        }

        /// <summary>
        /// Set the  BackColor of LegendBackColor
        /// </summary>
        [
            Browsable(true),
            Category("Appearance"),
            Description("Set the BackColor of LegendBackColor.")
        ]
        internal float XPosition
        {
            get { return ChartArea.Position.X; }
            set { ChartArea.Position.X = value; }
        }

        internal float YPosition
        {
            get { return ChartArea.Position.Y; }
            set { ChartArea.Position.Y = value; }
        }

        internal float Width
        {
            get { return ChartArea.Position.Width; }
            set { ChartArea.Position.Width = value; }
        }

        internal float Height
        {
            get { return ChartArea.Position.Height; }
            set { ChartArea.Position.Height = value; }
        }

        private StripChartXAxis[] _axes = new StripChartXAxis[4];
        [
            Browsable(true),
            Bindable(true),
            Editor(typeof(CollectionEditor), typeof(UITypeEditor)),
            Category("Design"),
            Description("Specify or get the attribute of X and Y Axis."),
            EditorBrowsable(EditorBrowsableState.Never),
            PersistenceMode(PersistenceMode.InnerProperty),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        ]
        public StripChartXAxis[] Axes
        {
            get { return _axes; }
            set
            {
                //                if (null == value || value.Length < 2)
                //                {
                //                    return;
                //                }
                _axisX = value[0];
                _axisX2 = value[1];
                _axisY = value[2];
                _axisY2 = value[3];
            }
        }

        private StripChartXAxis _axisX;

        /// <summary>
        /// X Axis
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            PersistenceMode(PersistenceMode.InnerProperty),
            Description("Set or get the X axis attributes.")
        ]
        public StripChartXAxis AxisX
        {
            get { return _axisX; }
            set { _axisX = value; }
        }

        private StripChartXAxis _axisX2;
        /// <summary>
        /// X2 Axis
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            PersistenceMode(PersistenceMode.InnerProperty),
            Description("Set or get the X axis attributes.")
        ]
        public StripChartXAxis AxisX2
        {
            get { return _axisX2; }
            set { _axisX2 = value; }
        }

        private StripChartXAxis _axisY;

        /// <summary>
        /// Y Axis
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            PersistenceMode(PersistenceMode.InnerProperty),
            Description("Set or get the X axis attributes.")
        ]
        public StripChartXAxis AxisY
        {
            get { return _axisY; }
            set { _axisY = value; }
        }

        private StripChartXAxis _axisY2;
        /// <summary>
        /// Y2 Axis
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Design"),
            PersistenceMode(PersistenceMode.InnerProperty),
            Description("Set or get the X axis attributes.")
        ]
        public StripChartXAxis AxisY2
        {
            get { return _axisY2; }
            set { _axisY2 = value; }
        }

        private StripChartXCursor _xCursor;
        /// <summary>
        /// X Cursor
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            Category("Design"),
            Description("Set or get the X axis attributes.")
        ]
        public StripChartXCursor XCursor
        {
            get { return _xCursor; }
            set { _xCursor = value; }
        }

        private StripChartXCursor _yCursor;
        /// <summary>
        /// Y Cursor
        /// </summary>
        [
            Browsable(false),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            Category("Design"),
            Description("Set or get the Y axis attributes.")
        ]
        public StripChartXCursor YCursor
        {
            get { return _yCursor; }
            set { _yCursor = value; }
        }

        private StripChartXCursor[] _cursors = new StripChartXCursor[2];

        [
            Browsable(true),
            Bindable(BindableSupport.Yes, BindingDirection.TwoWay),
            Editor(typeof(System.ComponentModel.Design.CollectionEditor), typeof(UITypeEditor)),
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

        public StripChartXPlotArea()
        {
            // ignore
        }
        public void Clear()
        {
            _axisX.Clear();
            _axisX2.Clear();
            _axisY.Clear();
            _axisY2.Clear();

            _xCursor.RefreshCursor();
            _yCursor.RefreshCursor();
        }

        // TODO to check if axis view event triggered in Reset
        internal void AdaptPrimaryAxes(double maxXValue, double minXValue, double maxYValue, double minYValue)
        {
            double lastMaxX = AxisX.Maximum;
            double lastMinX = AxisX.Minimum;
            double lastMaxY = AxisY.Maximum;
            double lastMinY = AxisY.Minimum;

            _axisX.SetXAxisRangeValue(maxXValue, minXValue);
            _axisX.RefreshAxisRange();

            _axisY.SetYAxisRangeValue(maxYValue, minYValue);
            _axisY.RefreshAxisRange();
            
            CancelScaleViewIfRangeNotFit(lastMaxX, lastMinX, lastMaxY, lastMinY);

            if (_axisX.AutoZoomReset)
            {
                _axisX.CancelScaleView();
            }
            else if (!_axisX.IsZoomed)
            {
                _axisX.ResetAxisScaleView();
            }

            if (_axisY.AutoZoomReset)
            {
                _axisY.CancelScaleView();
            }
            else if (!_axisY.IsZoomed)
            {
                _axisY.ResetAxisScaleView();
            }

            _axisX.RefreshXGridsAndLabels();
            _axisY.RefreshYGridsAndLabels();
        }

        // TODO 暂时只开发Y轴和Y2轴的同步，X轴的后期再加
        internal void AdaptSecondaryAxes(double maxXValue, double minXValue, double maxYValue, double minYValue)
        {
            YAxisSync.InitSyncParam(maxYValue, minYValue);
            YAxisSync.SyncAxis();

//            _axisX2.RefreshXLabelFormat();
            _axisY2.RefreshYGridsAndLabels();
        }

        internal void AdaptCursors(double xMinInterval, double yMinInternval)
        {
            _xCursor.RefreshCursor();
            _yCursor.RefreshCursor();

            _xCursor.SetInterval(xMinInterval);
            _yCursor.SetInterval(yMinInternval);

            //如果是小数则根据interval限制最多显示的小数点位数
//            _axisX.SetLabelFormat(xMinInterval);
//            _axisY.SetLabelFormat(yMinInternval);

//            _axisX2.SetLabelFormat(xMinInterval);
//            _axisY2.SetLabelFormat(yMinInternval);
        }

        // TODO 新增属性后需要同步
        internal void ApplyConfig(StripChartXPlotArea template)
        {
            this.BackColor = template.BackColor;
            this.AxisX.ApplyConfig(template.AxisX);
            this.AxisY.ApplyConfig(template.AxisY);
            this.AxisX2.ApplyConfig(template.AxisX2);
            this.AxisY2.ApplyConfig(template.AxisY2);

            this.XCursor.ApplyConfig(template.XCursor);
            this.YCursor.ApplyConfig(template.YCursor);
        }

        // 根据当前选择的线，将cursor绑定到对应坐标轴上
        internal void BindCursorToAxis()
        {
            if (StripChartXCursor.CursorMode.Cursor != _xCursor.Mode || StripChartXCursor.CursorMode.Cursor != _yCursor.Mode)
            {
                _xCursor?.BindToAxis(AxisType.Primary);
                _yCursor?.BindToAxis(AxisType.Primary);
            }
            else
            {
                Series cursorSeries = _parentChart.GetCursorSeries(this);
                if (null == cursorSeries)
                {
                    return;
                }
                _xCursor.BindToAxis(cursorSeries.XAxisType);
                _yCursor.BindToAxis(cursorSeries.YAxisType);
            }
        }

        // 如果上一次的视图和当前XY轴范围不匹配则取消缩放视图
        private void CancelScaleViewIfRangeNotFit(double lastMaxX, double lastMinX, double lastMaxY, double lastMinY)
        {
            if (!AxisX.IsZoomed && !AxisY.IsZoomed)
            {
                return;
            }

            double currentMaxX = AxisX.Maximum;
            double currentMinX = AxisX.Minimum;
            double currentMaxY = AxisY.Maximum;
            double currentMinY = AxisY.Minimum;

            //            double viewMaxX = plotArea.AxisX.ViewMaximum;
            //            double viewMinX = plotArea.AxisX.ViewMinimum;
            //            double viewMaxY = plotArea.AxisY.ViewMaximum;
            //            double viewMinY = plotArea.AxisY.ViewMinimum;
            // 如果没有重合区域则取消缩放
            if (double.IsNaN(lastMaxX) || double.IsNaN(lastMinX) || double.IsNaN(lastMaxY) || double.IsNaN(lastMinY) ||
                double.IsNaN(currentMaxX) || double.IsNaN(currentMinX) || double.IsNaN(currentMaxY) || double.IsNaN(currentMinY) ||
                currentMaxX <= lastMinX || currentMinX >= lastMaxX || currentMaxY <= lastMinY || currentMinY >= lastMaxY)
            {
                AxisX.CancelScaleView();
                AxisX2.CancelScaleView();
                AxisY.CancelScaleView();
                AxisY2.CancelScaleView();
            }
            double currentXRange = currentMaxX - currentMinX;
            double currentYRange = currentMaxY - currentMinY;
            double lastXRange = lastMaxX - lastMinX;
            double lastYRange = lastMaxY - lastMinY;
            double minXRange = currentXRange > lastXRange ? lastXRange : currentXRange;
            double minYRange = currentYRange > lastYRange ? lastYRange : currentYRange;
            // 如果X轴范围差比例超过最大维持系数则取消XY轴的缩放
            if (Math.Abs(currentXRange - lastXRange) / minXRange > Constants.MaxDiffToKeepXScaleview)
            {
                AxisX.CancelScaleView();
                AxisX2.CancelScaleView();
                AxisY.CancelScaleView();
                AxisY2.CancelScaleView();
            }
            // 如果Y轴范围差比例超过最大维持系数则取消Y轴的缩放
            else if (Math.Abs(currentYRange - lastYRange) / minYRange > Constants.MaxDiffToKeepYScaleview)
            {
                AxisY.CancelScaleView();
                AxisY2.CancelScaleView();
            }
        }
    }
}
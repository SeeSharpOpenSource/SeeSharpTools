using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
//    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EasyChartXCursor
    {
        private readonly EasyChartX _parentChart;
        private readonly EasyChartXPlotArea _parentPlotArea;
        private readonly Cursor _baseCursor;
        private readonly Axis _baseAxis;
        //        private bool _isUserChangedView = false;

        #region Constrctor

        internal EasyChartXCursor(EasyChartX parentChart, EasyChartXPlotArea parentPlotArea, Cursor baseCursor, Axis baseAxis, string cursorName)
        {
            this._parentChart = parentChart;
            this._parentPlotArea = parentPlotArea;
            this._baseCursor = baseCursor;
            this._baseAxis = baseAxis;
            this.Name = cursorName;
        }

        #endregion

        #region Public property

        /// <summary>
        /// Axis name
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Get the name of cursor."),
            NotifyParentProperty(true)
        ]
        public string Name { get; }

        private CursorMode _mode = CursorMode.Disabled;
        /// <summary>
        /// Specify whether cursor enabled
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get whether cursor is enabled."),
//            NotifyParentProperty(true)
        ]
        public CursorMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                RefreshCursor();
                _parentPlotArea.BindCursorToAxis();
            }
        }

        private Color _color = Color.DeepSkyBlue;
        /// <summary>
        /// Specify whether cursor zoom function enabled
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get whether cursor zoom function is enabled."),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            NotifyParentProperty(true)
        ]
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                RefreshCursor();
            }
        }

        /// <summary>
        /// Specify whether cursor zoom function enabled
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get whether cursor zoom function is enabled."),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            NotifyParentProperty(true)
        ]
        public Color SelectionColor
        {
            get { return _baseCursor.SelectionColor; }
            set
            {
                _baseCursor.SelectionColor = value;
            }
        }

        private bool _autoInterval = true;
        /// <summary>
        /// Specify whether cursor zoom function enabled
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get whether cursor zoom function is enabled.")
        ]
        public bool AutoInterval
        {
            get { return _autoInterval; }
            set
            {
                _autoInterval = value;
                if (!_autoInterval)
                {
                    _baseCursor.Interval = _interval;
                }
            }
        }

        /// <summary>
        /// Specify whether cursor zoom function enabled
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get whether cursor zoom function is enabled.")
        ]
        public double Value
        {
            get { return CursorMode.Cursor == Mode ? _baseCursor.Position : double.NaN; }
            set
            {
                if (CursorMode.Cursor == Mode)
                {
                    _baseCursor.Position = value;
                }
                _parentChart.OnCursorPositionChanged(this, false);
            }
        }

        private double _interval = 1e-3;

        /// <summary>
        /// Get or set the maximum value of axis
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get the axis maximum value.")
        ]
        public double Interval
        {
            get { return _parentChart.IsPlotting() ? _baseCursor.Interval : _interval; }
            set
            {
                _interval = value;
                if (!_autoInterval)
                {
                    _baseCursor.Interval = _interval;
                    _baseAxis.ScaleView.SmallScrollSize = double.NaN;
                    _baseAxis.ScaleView.SmallScrollMinSize = _interval;
                }
            }
        }
        

        internal void RefreshCursor()
        {
            if (!_parentChart.IsPlotting())
            {
                _baseCursor.LineColor = Color.Transparent;
                _baseCursor.IsUserEnabled = false;
                _baseCursor.IsUserSelectionEnabled = false;
//                _baseAxis.ScaleView.Zoomable = false;
                return;
            }
            switch (_mode)
            {
                case CursorMode.Disabled:
                    _baseCursor.LineColor = Color.Transparent;
                    _baseCursor.IsUserEnabled = false;
                    _baseCursor.IsUserSelectionEnabled = false;
//                    _baseAxis.ScaleView.Zoomable = false;
                    break;
                    case CursorMode.Cursor:
                    _baseCursor.LineColor = _color;
                    _baseCursor.IsUserEnabled = true;
                    _baseCursor.IsUserSelectionEnabled = false;
//                    _baseAxis.ScaleView.Zoomable = false;
                    break;
                case CursorMode.Zoom:
                    _baseCursor.LineColor = _color;
                    _baseCursor.IsUserEnabled = true;
                    _baseCursor.IsUserSelectionEnabled = true;
//                    _baseAxis.ScaleView.Zoomable = true;
                    break;
                default:
                    break;
            }
        }

        internal void BindToAxis(AxisType axisType)
        {
            _baseCursor.AxisType = axisType;
        }

        // TODO 新增属性时需要更新该方法
        public void ApplyConfig(EasyChartXCursor template)
        {
            this.AutoInterval = template.AutoInterval;
            this.Color = template.Color;
            this.Interval = template.Interval;
            this.Mode = template.Mode;
            this.SelectionColor = template.SelectionColor;
        }

        /// <summary>
        /// Designed for auto interval setting
        /// </summary>
        /// <param name="interval"></param>
        internal void SetInterval(double interval)
        {
            if (_autoInterval)
            {
                _baseCursor.Interval = interval;
                _baseAxis.ScaleView.SmallScrollSize = interval;
                _baseAxis.ScaleView.SmallScrollMinSize = interval;
            }
            else
            {
                _baseCursor.Interval = _interval;
                _baseAxis.ScaleView.SmallScrollSize = _interval;
                _baseAxis.ScaleView.SmallScrollMinSize = _interval;
            }
        }
//
//        internal void SetVisible(bool visible)
//        {
//            _baseCursor.LineColor = visible ? _color : Color.Transparent;
//        }
        
        public enum CursorMode
        {
            /// <summary>
            /// Disabled
            /// </summary>
            Disabled,
            /// <summary>
            /// Cursor mode
            /// </summary>
            Cursor,

            /// <summary>
            /// Zoom mode
            /// </summary>
            Zoom
        }

        #endregion
    }
}
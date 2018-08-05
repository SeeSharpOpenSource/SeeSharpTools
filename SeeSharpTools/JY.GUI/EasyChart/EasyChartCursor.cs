using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
//    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EasyChartCursor
    {
        private readonly EasyChart _baseEasyChart;
        private readonly Cursor _baseCursor;
        private readonly Axis _baseAxis;
        //        private bool _isUserChangedView = false;

        #region Constrctor

        internal EasyChartCursor(EasyChart baseEasyChart, Cursor baseCursor, Axis baseAxis, string cursorName)
        {
            this._baseEasyChart = baseEasyChart;
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
            }
        }

        private Color _color = Color.Red;
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
                _baseEasyChart.OnCursorPositionChanged(this, false);
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
            get { return _baseCursor.Interval; }
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
            RefreshCursor(_mode);
        }

        internal void RefreshCursor(CursorMode mode)
        {
            if (!_baseEasyChart.IsPlotting())
            {
                _baseCursor.LineColor = Color.Transparent;
                _baseCursor.IsUserEnabled = false;
                _baseCursor.IsUserSelectionEnabled = false;
//                _baseAxis.ScaleView.Zoomable = false;
                return;
            }
            switch (mode)
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

        /// <summary>
        /// Designed for auto interval setting
        /// </summary>
        /// <param name="interval"></param>
        internal void SetInterval(double interval)
        {
            if (_autoInterval)
            {
                _baseCursor.Interval = interval;
                // TODO 为了保证缩放后拖动，Y轴和X轴太多的小数点显示，暂时封闭
//                _baseAxis.ScaleView.SmallScrollSize = double.NaN;
//                _baseAxis.ScaleView.SmallScrollMinSize = double.NaN;
            }
        }
        
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
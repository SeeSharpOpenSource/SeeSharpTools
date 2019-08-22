using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using SeeSharpTools.JY.GUI.TabCursorUtility;
using SeeSharpTools.JY.GUI.EasyChartXUtility;

namespace SeeSharpTools.JY.GUI
{
    //DynamicCursor类是public权限，为了避免在ToolBox显示DynamicCursor的控件，将DynamicCursor的控件功能迁移到内部类DynamicCursorControl中实现
    /// <summary>
    /// Class of data cursor
    /// </summary>
    public class TabCursor
    {
//        private EasyChartX _parentChart;
        private TabCursorCollection _collection;
        internal TabCursorControl Control { get; }

        /// <summary>
        /// Create a tab cursor instance
        /// </summary>
        public TabCursor()
        {
            this.Control = new TabCursorControl();
            this._name = "";
            this._value = 0;
            this.Color = Color.Red;
            this._enabled = true;

            this._seriesIndex = -1;
        }

        internal void Initialize(TabCursorCollection collection)
        {
            this._collection = collection;
            this.Control.RefreshAndShowView = new Action(() =>
            {
                _collection.RefreshCursorValue(this);
                _collection.ShowCursorValue(this, true);
            });
            this.Control.MouseEnter += (sender, args) => { _collection.ShowCursorValue(this, true);};
            this.Control.MouseLeave += (sender, args) => { _collection.ShowCursorValue(this, false);};
        }

        const int MaxNameLength = 50;
        private string _name;
        /// <summary>
        /// Set or get cursor name
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get the name of cursor.")
        ]
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || _name.Equals(value) || value.Length > MaxNameLength || 
                    (null != _collection && _collection.Any(cursor => cursor.Name.Equals(value))))
                {
                    return;
                }
                _name = value;
            }
        }

        private double _value;
        /// <summary>
        /// Set or get cursor value
        /// </summary>
        [
            Browsable(true),
            Category("Data"),
            Description("Set or get the X value of cursor.")
        ]
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (Math.Abs(_value - value) < Constants.MinDoubleValue)
                {
                    return;
                }
                _value = value;
                _collection?.RefreshCursorPosition(this);
            }
        }

        /// <summary>
        /// Set or get the Y value of cursor.
        /// </summary>
        [
            Browsable(true),
            Category("Data"),
            Description("Set or get the Y value of cursor.")
        ]
        public double YValue
        {
            get
            {
                double xAfterAlign = _value;
                double yValue = _collection.GetYValue(ref xAfterAlign, _seriesIndex);
                // 更新对齐后的X值
                Value = xAfterAlign;
                return yValue;
            }
            
        }

        private int _seriesIndex;
        /// <summary>
        /// Specify the index of series which the tabcursor will be attached to.
        /// </summary>
        [
            Browsable(true),
            Category("Behavior"),
            Description("Specify the index of series which the tabcursor will be attached to.")
        ]
        public int SeriesIndex {
            get { return _seriesIndex; }
            set
            {
                _seriesIndex = value >= 0 ? value : -1;
            }
        }

        /// <summary>
        /// Get the formated value string
        /// </summary>
        internal string ValueString => string.IsNullOrEmpty(_collection?.CursorValueFormat)
            ? _value.ToString()
            : _value.ToString(_collection.CursorValueFormat);

        /// <summary>
        /// Specify or get cursor color
        /// </summary>
        [
            Browsable(true),
            Category("Apperance"),
            Description("Specify or get the color of cursor.")
        ]
        public Color Color
        {
            get { return Control.CursorColor; }
            set { Control.CursorColor = value; }
        }

        private bool _enabled;

        /// <summary>
        /// Specify or get whether cursor enabled.
        /// </summary>
        [
            Browsable(true),
            Category("Design"),
            Description("Set or get the name of cursor.")
        ]
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                _collection?.RefreshCursorPosition(this);
                _collection?.AttachOrDetachPaintEvent();
            }
        }

        public void ShowValue()
        {
            _collection.ShowCursorValue(this, true);
        }

        public void HideValue()
        {
            _collection.ShowCursorValue(this, false);
        }
    }

    
}
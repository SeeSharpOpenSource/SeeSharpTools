using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System;

/// <summary>
/// 修改日期：2017.06.24
/// 作者： 简仪科技
/// 软件版本： SeeSharpTool v1.2.1
/// 描述：   1.添加控件 （1D switch control array)
///          2.新增快速配置视窗
///          3.新增事件
///          4.新增单一数值变更
/// </summary>

namespace SeeSharpTools.JY.GUI
{    
    [Designer(typeof(LEDArrayDesigner))]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(LEDArray), "LEDArray.LEDArray.bmp")]
    public partial class LEDArray : UserControl
    {
        #region Private Data
        List<LED> _controls;
        uint _dimension;
        LED _model;
        public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);

        #endregion

        #region Constructor
        public LEDArray()
        {
            InitializeComponent();

            _controls = new List<LED>();
            _model = new LED();

            Dimension = 1;

            ControlWidth = 30;
            ControlHeight = 30;
            Direction = true;

            flpanel.AutoScroll = true;
            flpanel.FlowDirection = FlowDirection.TopDown;
            flpanel.WrapContents = false;

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get/Set the Dimension of the Array
        /// </summary>
        public uint Dimension
        {
            get { return _dimension; }
            set
            {
                _dimension = value;
                _controls.Clear();
                for (int i = 0; i < _dimension; i++)
                {
                    _controls.Add(new LED());
                }
                UpdateControls();
                ApplyTemplate();
            }
        }

        /// <summary>
        /// Get/Set the Array direction to display
        /// </summary>
        public bool Direction
        {
            get { return flpanel.FlowDirection == FlowDirection.TopDown; }
            set
            {
                if (value)
                {
                    flpanel.FlowDirection = FlowDirection.TopDown;
                    this.Width = ControlWidth + 25;
                    this.Height = ControlHeight * 2 + 15;
                }
                else
                {
                    flpanel.FlowDirection = FlowDirection.LeftToRight;
                    this.Width = ControlWidth * 2 + 10;
                    this.Height = ControlHeight + 25;

                }
            }
        }

        /// <summary>
        /// Get/Set the Widthof the controls and update to all
        /// </summary>
        public int ControlWidth
        {
            get
            {
                return _model.Width;
            }
            set
            {
                _model.Width = value;
                _controls.ForEach(x => x.Size = new Size(value, _model.Height));
                if (Direction)
                {
                    this.Width = _model.Width + 25;
                }
            }
        }

        /// <summary>
        /// Get/Set the Height of the controls and update to all
        /// </summary>
        public int ControlHeight
        {
            get { return _model.Height; }
            set
            {
                _model.Height = value;
                _controls.ForEach(x => x.Size = new Size(_model.Width, value));
                if (!Direction)
                {
                    this.Height = _model.Height + 25;
                }
            }
        }

        /// <summary>
        /// Get/Set all data
        /// </summary>
        public bool[] Value
        {
            get { return GetValues(); }
            set { SetValues(value); }
        }
        #region Template object properties
        //private LED Model
        //{
        //    get { return _model; }
        //    set
        //    {
        //        _model = value;
        //        ApplyTemplate();
        //    }
        //}

        /// <summary>
        /// Style of the control
        /// </summary>
        public LED.LedStyle LEDStyle
        {
            get { return _model.Style; }
            set
            {
                _model.Style = value;
                ApplyTemplate();
            }
        }

        /// <summary>
        /// OFFColor of the control
        /// </summary>
        public Color LEDOnColor
        {
            get { return _model.OnColor; }
            set
            {
                _model.OnColor = value;
                ApplyTemplate();
            }
        }
        /// <summary>
        /// ONColor of the control
        /// </summary>
        public Color LEDOffColor
        {
            get { return _model.OffColor; }
            set
            {
                _model.OffColor = value;
                ApplyTemplate();
            }
        }

        #endregion
        #endregion

        #region Public Methods


        /// <summary>
        /// Clear the Array
        /// </summary>
        public void Clear()
        {
            _controls.Clear();
        }

        /// <summary>
        /// Set single value using index and value
        /// </summary>
        public void SetSingleValue(int index, bool value)
        {
            if (index <= _controls.Count)
            {
                if (_controls[index].Value != value)
                {
                    _controls[index].Value = value;
                    ValueChangedEventArgs arg = new ValueChangedEventArgs(index, value);
                    SendEvent(arg);

                }
            }
        }


        #endregion

        #region Private Methods
        private void UpdateControls()
        {
            flpanel.Controls.Clear();
            flpanel.Controls.AddRange(_controls.ToArray());
        }

        /// <summary>
        /// Apply the Template properties to the Control
        /// </summary>
        private void ApplyTemplate()
        {
            foreach (LED x in flpanel.Controls)
            {
                x.Style = _model.Style;
                x.OnColor = _model.OnColor;
                x.OffColor = _model.OffColor;
                x.Width = _model.Width;
                x.Height = _model.Height;
            }

        }
        private void SetValues(bool[] values)
        {
            if (values.Length >= Dimension)
            {
                for (int i = 0; i < Dimension; i++)
                {
                    if (_controls.ElementAt(i).Value != values[i])
                    {
                        _controls.ElementAt(i).Value = values[i];
                        ValueChangedEventArgs arg = new ValueChangedEventArgs(i,values[i]);
                        SendEvent(arg);


                    }


                }
            }

            else
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (_controls.ElementAt(i).Value != values[i])
                    {
                        _controls.ElementAt(i).Value = values[i];
                        ValueChangedEventArgs arg = new ValueChangedEventArgs(i,values[i]);
                        SendEvent(arg);

                    }


                }
            }
        }

        private bool[] GetValues()
        {
            bool[] outputData = new bool[_controls.Count];
            for (int i = 0; i < _controls.Count; i++)
            {
                outputData[i] = _controls.ElementAt(i).Value;
            }
            return outputData;
        }
        private void SendEvent(ValueChangedEventArgs arg)
        {
            if (ControlValueChanged != null)
            {
                ControlValueChanged(this, arg);
            }
        }

        #endregion

        #region Event Handler
        /// <summary>
        /// Single value changed event
        /// </summary>
        public event ValueChangedEventHandler ControlValueChanged;

        public class ValueChangedEventArgs : EventArgs
        {
            private bool value;
            private int index;

            /// <summary>
            /// Send back three paramters (index, value)
            /// </summary>
            public ValueChangedEventArgs(int index, bool value)
            {
                this.value = value;
                this.index = index;
            }
            /// <summary>
            /// Current row data of the selected cell
            /// </summary>
            public bool Data
            {
                get { return value; }
            }
            /// <summary>
            /// Current row index of the selected cell
            /// </summary>
            public int Index
            {
                get { return index; }
            }

        }

        #endregion



    }
}

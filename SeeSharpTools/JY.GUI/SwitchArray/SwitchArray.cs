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
    [Designer(typeof(SwitchArrayDesigner))]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(SwitchArray), "SwitchArray.SwitchArray.bmp")]
    public partial class SwitchArray : UserControl
    {
        #region Private Data
        List<IndustrySwitch> _controls;
        uint _dimension;
        IndustrySwitch _model;
        public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);

        #endregion

        #region Constructor
        public SwitchArray()
        {
            InitializeComponent();
            // 设计器中自动配置了Name会导致在设计时获取控件名称失败
            this.Name = "";
            _controls = new List<IndustrySwitch>();
            _model = new IndustrySwitch();
            Dimension = 1;
            
            ControlWidth = 50;
            ControlHeight = 80;
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
                    _controls.Add(new IndustrySwitch());
                }
                foreach (IndustrySwitch item in _controls)
                {
                    item.ValueChanged += Item_ValueChanged1;
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
        /// Get/Set the Width of the controls and update to all
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
        /// Get/Set the values
        /// </summary>
        public bool[] Value
        {
            get { return GetValues(); }
            set { SetValues(value); }
        }


        #region Template object properties

        /// <summary>
        /// Control Style
        /// </summary>
        public IndustrySwitch.SwitchStyles Style
        {
            get { return _model.Style; }
            set
            {
                _model.Style = value;
                ApplyTemplate();
            }
        }

        #endregion
        #endregion

        #region Public Methods


        /// <summary>
        /// Clear the List
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
                _controls[index].Value = value;
                ValueChangedEventArgs arg = new ValueChangedEventArgs(index, value);
                SendEvent(arg);
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
            foreach (IndustrySwitch item in flpanel.Controls)
            {
                item.Style = _model.Style;
                item.Width = _model.Width;
                item.Height = _model.Height;
            }
        }

        private void SetValues(bool[] values)
        {
            if (values.Length >= Dimension)
            {
                for (int i = 0; i < Dimension; i++)
                {
                    if (_controls.ElementAt(i).Value!= values[i])
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
            /// Send back three paramters (index,value)
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
            public int SelectedIndex
            {
                get { return index; }
            }

        }

        private void Item_ValueChanged1(object sender, EventArgs e)
        {
            int index=_controls.IndexOf((IndustrySwitch)sender);
            ValueChangedEventArgs arg = new ValueChangedEventArgs(index,Value[index]);
            SendEvent(arg);
        }


        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// 视图控制单个控件的信息类。如果需要新增属性需要修改PropertyNames、PropertyDefaultValue、FromStrToValue方法、ApplyConfig方法
    /// </summary>
    internal class ViewControlElement
    {
        private readonly Control _control;
        public string Name { get; set; }
        public static string[] PropertyNames = {"Enabled", "Visible"};
        public static object[] DefaultValue = {true, true};
        private readonly Dictionary<string, object[]> _values;

        public bool IsEmpty
        {
            get
            {
                bool isEmpty = true;
                foreach (object[] values in _values.Values.Where(values => null != values))
                {
                    isEmpty = values.All(value => null == value);
                    if (!isEmpty)
                    {
                        break;
                    }
                }
                return isEmpty;
            }
        }
        // Constructor for design time
        public ViewControlElement(Control control)
        {
            this._control = control;
            this.Name = control.Name;
            const int defaultEnumCount = 8;
            this._values = new Dictionary<string, object[]>(defaultEnumCount);
        }

        const char ControlDelim = ':';
        const char StateDelim = ';';
        const char StateToPropertyDelim = '*';
        const char PropertyDelim = ',';
        const char PropertyAndValueDelim = '=';
        public ViewControlElement(string controlInfo, Form parentForm)
        {
            const int defaultEnumCount = 8;
            this._values = new Dictionary<string, object[]>(defaultEnumCount);
            string[] infos = controlInfo.Split(ControlDelim);
            Name = infos[0];
            _control = RecursiveFindControl(parentForm.Controls);
            foreach (string singleStateInfo in infos[1].Split(StateDelim))
            {
                string[] stateNameToInfo = singleStateInfo.Split(StateToPropertyDelim);
                foreach (string singlePropertyToValue in stateNameToInfo[1].Split(PropertyDelim))
                {
                    string[] propertyToValue = singlePropertyToValue.Split(PropertyAndValueDelim);
                    object value = FromStrToValue(propertyToValue[0], propertyToValue[1]);
                    SetValue(stateNameToInfo[0], propertyToValue[0], value);
                }
            }
        }

        // Constructor for runtime
        internal ViewControlElement(string controlInfo, IList<string> states)
        {
            const int defaultEnumCount = 8;
            this._values = new Dictionary<string, object[]>(defaultEnumCount);
            AdaptStates(states);
            string[] infos = controlInfo.Split(ControlDelim);
            Name = infos[0];
            _control = null;
            foreach (string singleStateInfo in infos[1].Split(StateDelim))
            {
                string[] stateNameToInfo = singleStateInfo.Split(StateToPropertyDelim);
                foreach (string singlePropertyToValue in stateNameToInfo[1].Split(PropertyDelim))
                {
                    string[] propertyToValue = singlePropertyToValue.Split(PropertyAndValueDelim);
                    SetValue(stateNameToInfo[0], propertyToValue[0], propertyToValue[1]);
                }
            }
        }

        private Control RecursiveFindControl(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.Name.Equals(Name))
                {
                    return control;
                }
                Control findResultInSubControls = RecursiveFindControl(control.Controls);
                if (null != findResultInSubControls)
                {
                    return findResultInSubControls;
                }
            }
            return null;
        }

        private object FromStrToValue(string property, string valueStr)
        {
            if (string.IsNullOrEmpty(valueStr))
            {
                return null;
            }
            object value = null;
            for (int i = 0; i < PropertyNames.Length; i++)
            {
                if (PropertyNames[i].Equals(property))
                {
                    switch (i)
                    {
                        case 0:
                        case 1:
                            value =  true.ToString().Equals(valueStr);
                            break;
                        default:
                            break;
                    }
                    // OTHERS to add 
                }
            }
            return value;
        }

        public override string ToString()
        {
            StringBuilder dstStr = new StringBuilder(200);
            dstStr.Append(Name).Append(ControlDelim);
            foreach (string state in _values.Keys)
            {
                if (null == _values[state])
                {
                    continue;
                }
                dstStr.Append(state).Append(StateToPropertyDelim);
                for (int i = 0; i < _values[state].Length; i++)
                {
                    object value = _values[state][i] ?? "";
                    dstStr.Append(PropertyNames[i]).Append(PropertyAndValueDelim).Append(value);
                    dstStr.Append(PropertyDelim);
                }
                dstStr.Remove(dstStr.Length - 1, 1);
                dstStr.Append(StateDelim);
            }
            dstStr.Remove(dstStr.Length - 1, 1);
            return dstStr.ToString();
        }

        public void AdaptStates(IList<string> states)
        {
            _values.Clear();
            foreach (string state in states)
            {
                _values.Add(state, null);
            }
        }

        public object[] GetValue(string state)
        {
            if (!_values.ContainsKey(state) || null == _values[state])
            {
                return null;
            }
            return _values[state];
        }

        public void SetValue(string state, string propertyName, object value)
        {
            if (!_values.ContainsKey(state) || null == _values[state])
            {
                if (_values.ContainsKey(state))
                {
                    _values[state] = new object[PropertyNames.Length];
                }
                else
                {
                    _values.Add(state, new object[PropertyNames.Length]);
                }
                for (int i = 0; i < PropertyNames.Length; i++)
                {
                    _values[state][i] = null;
                }
            }
            _values[state][GetPropertyIndex(propertyName)] = 
                (null != value) ? FromStrToValue(propertyName, value.ToString()) : null;
        }

        public void RenameState(string originalState, string newState)
        {
            if (_values.ContainsKey(originalState))
            {
                object[] stateValue = _values[originalState];
                _values.Remove(originalState);
                _values.Add(newState, stateValue);
            }
        }

        public void RemoveState(string state)
        {
            if (_values.ContainsKey(state))
            {
                _values.Remove(state);
            }
        }

        public void ApplyConfig(string state)
        {
            if (!_values.ContainsKey(state) || null == _control)
            {
                return;
            }
            object[] values = _values[state];
            for (int propertyIndex = 0; propertyIndex < PropertyNames.Length; propertyIndex++)
            {
                if (null == values[propertyIndex])
                {
                    continue;
                }
                // TODO 反射会影响效率，暂时使用写死的
//                Type controlType = _control.GetType();
//
//                PropertyInfo propertyInfo = controlType.GetProperty(PropertyNames[i],
//                    BindingFlags.Instance | BindingFlags.Public);
//                propertyInfo?.SetValue(_control, values[i], null);
                switch (propertyIndex)
                {
                    case 0:
                        _control.Enabled = (bool) values[propertyIndex];
                        break;
                    case 1:
                        _control.Visible = (bool) values[propertyIndex];
                        break;
                    default:
                        break;
                }
            }
        }

        public ViewControlElement Clone()
        {
            ViewControlElement cloneObj = new ViewControlElement(_control);
            foreach (string key in _values.Keys)
            {
                if (null == _values[key])
                {
                    continue;
                }
                for (int i = 0; i < PropertyNames.Length; i++)
                {
                    cloneObj.SetValue(key, PropertyNames[i], _values[key][i]);   
                }
            }
            return null;
        }

        private int GetPropertyIndex(string propertyName)
        {
            for (int index = 0; index < PropertyNames.Length; index++)
            {
                if (PropertyNames[index].Equals(propertyName))
                {
                    return index;
                }
            }
            return -1;
        }
    }
}
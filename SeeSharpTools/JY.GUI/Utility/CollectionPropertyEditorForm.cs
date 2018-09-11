using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    public partial class CollectionPropertyEditorForm<TDataType> : Form
    {
        private const string ItemNameKey = "Name";
        private IList<TDataType> _configInst;
        private List<Dictionary<string, object>> _originalPropertyData;
        private Action _addItemMethod;

        public CollectionPropertyEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open the editor form of property
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="value">PropertyValue</param>
        /// <param name="addItemMethod"></param>
        /// <param name="editable"></param>
        /// <returns>Value after configure</returns>
        public static object EditValue<TDataType>(string propertyName, IList<TDataType> value, Action addItemMethod, bool editable)
        {
            CollectionPropertyEditorForm<TDataType> form = new CollectionPropertyEditorForm<TDataType>();
            form.BindData(propertyName, value, addItemMethod);
            form.SetCollectionEditable(editable);
            form.ShowDialog();
            return form.GetValue();
        }

        private void SetCollectionEditable(bool editable)
        {
            button_add.Enabled = editable;
            button_delete.Enabled = editable;
        }

        internal void BindData(string name, IList<TDataType> value, Action addItemMethod)
        {
            this._addItemMethod = addItemMethod;

            this._configInst = value;
            this.Text = name + " Editor";
            _originalPropertyData = new List<Dictionary<string, object>>(10);
            foreach (TDataType data in value)
            {
                Dictionary<string, object> cloneData = CloneProperties(data);
                listBox_members.Items.Add(cloneData[ItemNameKey]);
                _originalPropertyData.Add(cloneData);
            }
            if (0 != value.Count)
            {
                listBox_members.SelectedIndex = 0;
            }
        }

        private object GetValue()
        {
            return propertyGrid_object.SelectedObject;
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            while (_configInst.Count > _originalPropertyData.Count)
            {
                _configInst.RemoveAt(_configInst.Count - 1);
            }

            while (_configInst.Count < _originalPropertyData.Count)
            {
                _addItemMethod.Invoke();
            }
            for (int i = 0; i < _configInst.Count; i++)
            {
                ApplyPropertiesData(_configInst[i], _originalPropertyData[i]);
            }
            this.Dispose();
        }

        /// <summary>
        /// Shallow copy class properties value to dictionary.
        /// </summary>
        private Dictionary<string, object> CloneProperties(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Dictionary<string, object> propertiesData = new Dictionary<string, object>(properties.Length);
            foreach (PropertyInfo property in properties)
            {
                // 仅备份可读可写的参数
                if (!property.CanWrite || !property.CanRead) continue;
                object value = property.GetValue(obj, null);
                // 对属性只做浅拷贝
                propertiesData.Add(property.Name, value);
            }
            return propertiesData;
        }

        /// <summary>
        /// Apply properties value to class instance from dictionary backup data.
        /// </summary>
        private void ApplyPropertiesData(object obj, Dictionary<string, object> propertiesData)
        {
            Type type = obj.GetType();
            foreach (string propertyName in propertiesData.Keys)
            {
                PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                // 对属性只做浅拷贝
                property.SetValue(obj, propertiesData[propertyName], null);
            }
        }

        private void listBox_members_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox_members.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < _configInst.Count)
            {
                propertyGrid_object.SelectedObject = _configInst[selectedIndex];
            }
            else
            {
                propertyGrid_object.SelectedObject = null;
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            _addItemMethod.Invoke();
            // 添加不成功是更新所有的列表显示
            PropertyInfo nameProperty = typeof(TDataType).GetProperty(ItemNameKey, BindingFlags.Public | BindingFlags.Instance);
            if (listBox_members.Items.Count + 1 != _configInst.Count)
            {
                listBox_members.Items.Clear();
                foreach (TDataType data in _configInst)
                {
                    object nameValue = nameProperty.GetValue(data, null);
                    listBox_members.Items.Add(nameValue);
                }
                listBox_members.SelectedIndex = 0;
            }
            else
            {
                int newItemIndex = _configInst.Count - 1;
                listBox_members.Items.Add(nameProperty.GetValue(_configInst[newItemIndex], null));
                listBox_members.SelectedIndex = newItemIndex;
            }
            
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            int removeItemIndex = listBox_members.SelectedIndex;
            if (removeItemIndex < 0)
            {
                return;
            }
            _configInst.RemoveAt(removeItemIndex);
            listBox_members.Items.RemoveAt(removeItemIndex);
            // 如果不是最后一个，则删除后选择的索引不变，否则选择上一个
            int selectIndex = (removeItemIndex < _configInst.Count) ? removeItemIndex : removeItemIndex - 1;
            if (0 > selectIndex)
            {
                propertyGrid_object.SelectedObject = null;
            }
            else
            {
                listBox_members.SelectedIndex = selectIndex;
            }
        }

        private void propertyGrid_object_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (ItemNameKey.Equals(e.ChangedItem.Label))
            {
                int itemIndex = listBox_members.SelectedIndex;
                listBox_members.Items[itemIndex] = e.ChangedItem.Value;
            }
        }

        private string GetCollectionGenericType(object propertyValue)
        {
            const string ilistFullName = "System.Collections.Generic.IList";
            Type ilistType = null;
            foreach (Type typeInterface in propertyValue.GetType().GetInterfaces())
            {
                if (typeInterface.FullName.Contains(ilistFullName))
                {
                    ilistType = typeInterface;
                }
            }
            if (!ilistType.IsGenericType)
            {
                throw new InvalidCastException(
                    "The collection should implement the interface: System.Collections.Generic.IList");
            }
            return ilistType.GetGenericArguments()[0].Name;
        }
    }
}

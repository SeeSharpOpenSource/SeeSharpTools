using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    public partial class PropertiesEditorForm : Form
    {
        private PropertyClonableClass _configInst;
        private Dictionary<string, object> _originalPropertyData;

        public PropertiesEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open the editor form of property
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="value">PropertyValue</param>
        /// <returns>Value after configure</returns>
        public static object EditValue(string propertyName, PropertyClonableClass value)
        {
            PropertiesEditorForm form = new PropertiesEditorForm();
            form.BindData(propertyName, value);
            form.ShowDialog();
            return form.GetValue();
        }
        
        internal void BindData(string name, PropertyClonableClass value)
        {
            this._configInst = value;
            this.Text = name + " Editor";
            label_title.Text = name + " Properties:";
            _originalPropertyData = _configInst.CloneProperties();
            propertyGrid_object.SelectedObject = _configInst;
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
            _configInst.AppllyPropertiesData(_originalPropertyData);
            this.Dispose();
        }
    }
}

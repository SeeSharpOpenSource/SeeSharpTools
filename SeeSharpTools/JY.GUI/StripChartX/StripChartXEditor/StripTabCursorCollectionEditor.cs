using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SeeSharpTools.JY.GUI.StripChartXEditor
{
    /// <summary>
    /// The editor of class type property. This property type should be the subclass of PropertyClonableClass
    /// </summary>
    public class StripTabCursorCollectionEditor : UITypeEditor
    {
        // Indicates whether the UITypeEditor provides a form-based (modal) dialog, 
        // drop down dialog, or no UI outside of the properties window.
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //打开属性编辑器修改数据
            Control control = context.Instance as Control;
            PropertyDescriptor descriptor = context.PropertyDescriptor;
            StripTabCursorCollection tabCursor = descriptor.GetValue(context.Instance) as StripTabCursorCollection;
            // 强制变更，以将变更写入文件
            PropertyDescriptor backColorProperty = TypeDescriptor.GetProperties(control)["BackColor"];
            backColorProperty.SetValue(control, control.BackColor);
            
            return CollectionPropertyEditorForm<StripTabCursor>.EditValue(descriptor.Name, tabCursor, () => { tabCursor.Add(new StripTabCursor());  });
        }
    }
}
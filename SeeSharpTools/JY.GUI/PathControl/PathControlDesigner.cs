using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace SeeSharpTools.JY.GUI
{
    class PathControlDesigner:ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new FilePathActionList(this.Component));
                    //注意要在这边新建下面所以生成的类
                }
                return actionLists;
            }
        }

    }
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class FilePathActionList : DesignerActionList
    {
        private PathControl colUserControl;
        //这一行主要是要根据你修饰的控件来选择

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public FilePathActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as PathControl;

            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        // Properties that are targets of DesignerActionPropertyItem entries.
        //一下部分就主要是来修饰你要在快速设计视窗中要改变什么样的属性了，也是就所开放出来的属性
        public PathMode BrowseMode
        {
            get { return colUserControl.BrowseMode; }
            set { GetPropertyByName("BrowseMode").SetValue(colUserControl, value); }
        }
        public string Extension
        {
            get { return colUserControl.ExtFileType; }
            set { GetPropertyByName("ExtFileType").SetValue(colUserControl, value); }
        }




        //以下这一部分的代码是可以写可以不写的地方，这部分主要是来排序，拍出在快速设计视窗中的显示的属性的顺序来用的。
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("FilePath selector"));
            items.Add(new DesignerActionPropertyItem("BrowseMode", "Browse Mode", "Appearance", "browse mode for the path control (file/folder)"));
            items.Add(new DesignerActionPropertyItem("Extension", "Extension File Name", "Appearance", "Extension file name for the selection"));

            return items;
        }

    }

}

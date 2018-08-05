using System;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;


namespace SeeSharpTools.JY.GUI
{
    class SwitchArrayDesigner: ControlDesigner
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
                    actionLists.Add(new SwitchArrayActionList(this.Component));
                    //注意要在这边新建下面所以生成的类
                }
                return actionLists;
            }
        }

    }
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class SwitchArrayActionList : DesignerActionList
    {
        private SwitchArray colUserControl;
        //这一行主要是要根据你修饰的控件来选择

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public SwitchArrayActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as SwitchArray;

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
        public uint Dimension
        {
            get { return colUserControl.Dimension; }
            set { GetPropertyByName("Dimension").SetValue(colUserControl, value); }                
        }

        public bool Direction
        {
            get { return colUserControl.Direction; }
            set { GetPropertyByName("Direction").SetValue(colUserControl, value); }
        }

        public int ControlWidth
        {
            get { return colUserControl.ControlWidth; }
            set { GetPropertyByName("ControlWidth").SetValue(colUserControl, value); }
        }

        public int ControlHeight
        {
            get { return colUserControl.ControlHeight; }
            set { GetPropertyByName("ControlHeight").SetValue(colUserControl, value); }
        }

        public IndustrySwitch.SwitchStyles Style
        {
            get
            {
                return colUserControl.Style;
            }
            set
            {
                GetPropertyByName("Style").SetValue(colUserControl, value);
            }
        }




        //以下这一部分的代码是可以写可以不写的地方，这部分主要是来排序，拍出在快速设计视窗中的显示的属性的顺序来用的。
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Switch Array (1D)"));
            items.Add(new DesignerActionPropertyItem("Dimension", "Dimention Of the Array", "Appearance", "Get/Set the rank of the Array"));
            items.Add(new DesignerActionPropertyItem("Direction", "Horizontal/Vertical display(true for Vertical)", "Appearance", "Order of the Array"));
            items.Add(new DesignerActionPropertyItem("ControlWidth", "Width the width of the Swich", "Appearance", "Width of the switch(apply to all)"));
            items.Add(new DesignerActionPropertyItem("ControlHeight", "Height of the Swich", "Appearance", "Height of the switch(apply to all)"));

            items.Add(new DesignerActionHeaderItem("Template Style"));
            items.Add(new DesignerActionPropertyItem("Style", "Style of the Switch", "Template Style", "Style of the Industry Switch"));



            return items;
        }

    }

}

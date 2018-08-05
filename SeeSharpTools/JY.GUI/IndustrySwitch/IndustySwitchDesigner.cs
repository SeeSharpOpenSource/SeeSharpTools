using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;

namespace SeeSharpTools.JY.GUI
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    class IndustySwitchDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new IndustryActionList(this.Component));
                }
                return actionLists;
            }
        }

    }

    internal class IndustryActionList : System.ComponentModel.Design.DesignerActionList
    {
        private IndustrySwitch colUserControl;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public IndustryActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as IndustrySwitch;

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
        public bool Value
        {
            get
            {
                return colUserControl.Value;
            }
            set
            {
                GetPropertyByName("Value").SetValue(colUserControl, value);

            }
        }
        /// <summary>
        /// Set the interaction of industryswitch
        /// </summary>
        public IndustrySwitch.InteractionStyle Interacton
        {
            get
            {
                return colUserControl.Interacton;
            }

            set
            {
                GetPropertyByName("Interacton").SetValue(colUserControl, value);
            }
        }
        /// <summary>
        /// 打开颜色
        /// </summary>
        public Color OnColor
        {
            get
            {
                return colUserControl.OnColor;
            }

            set
            {
                GetPropertyByName("OnColor").SetValue(colUserControl, value);
                colUserControl.Refresh();
            }
        }
        /// <summary>
        /// 关闭颜色
        /// </summary>
        public Color OffColor
        {
            get
            {
                return colUserControl.OffColor;
            }

            set
            {
                GetPropertyByName("OffColor").SetValue(colUserControl, value);
                colUserControl.Refresh();
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            //   items.Add(new DesignerActionHeaderItem("Appearance"));
            items.Add(new DesignerActionHeaderItem("Value"));
            items.Add(new DesignerActionPropertyItem("OnColor",
                                 "On Color", "Appearance",
                                 "Selects the on color of switch."));
            items.Add(new DesignerActionPropertyItem("OffColor",
                                 "Off Color", "Appearance",
                                 "Selects the off color of switch."));
            items.Add(new DesignerActionPropertyItem("Style",
                                 "switch Style", "Appearance",
                                 "Selects the of style of switch."));
            items.Add(new DesignerActionPropertyItem("Value",
                                 "Value", "Appearance",
                                 "Set the of value of switch."));
            items.Add(new DesignerActionHeaderItem("Behavior"));
            items.Add(new DesignerActionPropertyItem("Interacton",
                     "Interacton", "Interacton",
                     "Set the Interacton of switch."));

            return items;
        }
    }
}

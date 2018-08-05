using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace SeeSharpTools.JY.GUI
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class LedDesigner: ControlDesigner
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
                    actionLists.Add(new LedActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
    internal class LedActionList : System.ComponentModel.Design.DesignerActionList
    {
        private LED colUserControl;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public LedActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as LED;

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
        public Color OffColor
        {
            get
            {
                return colUserControl.OffColor;
            }
            set
            {
                GetPropertyByName("OffColor").SetValue(colUserControl, value);

            }
        }
        public LED.LedStyle Style
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

        public Color OnColor
        {
            get
            {
                return colUserControl.OnColor;
            }
            set
            {
                GetPropertyByName("OnColor").SetValue(colUserControl, value);
            }
        }

        public Color BlinkColor
        {
            get
            {
                return colUserControl.BlinkColor;
            }
            set
            {
                GetPropertyByName("BlinkColor").SetValue(colUserControl, value);
            }
        }

        public int BlinkInterval
        {
            get
            {
                return colUserControl.BlinkInterval;
            }
            set
            {
                GetPropertyByName("BlinkInterval").SetValue(colUserControl, value);
            }
        }

        public bool BlinkOn
        {
            get
            {
                return colUserControl.BlinkOn;
            }
            set
            {
                GetPropertyByName("BlinkOn").SetValue(colUserControl, value);
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
        
        public LED.InteractionStyle Interacton
        {
            get
            {
                return colUserControl.Interacton ;
            }
            set
            {
                GetPropertyByName("Interacton").SetValue(colUserControl, value);
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
                                 "Selects the on color of Led."));
            items.Add(new DesignerActionPropertyItem("OffColor",
                                 "Off Color", "Appearance",
                                 "Selects the off color of Led."));
            items.Add(new DesignerActionPropertyItem("Style",
                                 "Led Style", "Appearance",
                                 "Selects the of style of Led."));
            items.Add(new DesignerActionPropertyItem("Value",
                                 "Value", "Appearance",
                                 "Set the of value of Led."));
            items.Add(new DesignerActionHeaderItem("Blink"));
            items.Add(new DesignerActionPropertyItem("BlinkColor",
                                 "Blink Color", "Blink",
                                 "Set the of blink color of Led."));
            items.Add(new DesignerActionPropertyItem("BlinkInterval",
                                 "Interval of Blink(ms)", "Blink",
                                 "Set the interval of Led."));
            items.Add(new DesignerActionPropertyItem("BlinkOn",
                                 "BlinkOn", "Blink",
                                 "Set the blink of Led."));
            items.Add(new DesignerActionHeaderItem("Behavior"));
            items.Add(new DesignerActionPropertyItem("Interacton",
                     "Interacton", "Interacton",
                     "Set the Interacton of Led."));

            return items;
        }

    }
}

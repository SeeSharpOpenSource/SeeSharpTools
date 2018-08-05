using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace SeeSharpTools.JY.GUI
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class SevenSegmentDesigner : ControlDesigner
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
                    actionLists.Add(new SevenSegmentActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    public class SevenSegmentActionList : System.ComponentModel.Design.DesignerActionList
    {
        private SevenSegment colUserControl;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public SevenSegmentActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as SevenSegment;

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
        public Color BackgroundColor
        {
            get
            {
                return colUserControl.BackgroundColor;
            }
            set
            {
                GetPropertyByName("BackgroundColor").SetValue(colUserControl, value);

            }
        }


        public Color DarkColor
        {
            get
            {
                return colUserControl.DarkColor;
            }
            set
            {
                GetPropertyByName("DarkColor").SetValue(colUserControl, value);
            }
        }

        public Color LightColor
        {
            get
            {
                return colUserControl.LightColor ;
            }
            set
            {
                GetPropertyByName("LightColor").SetValue(colUserControl, value);
            }
        }
        public bool DecimalShow
        {
            get
            {
                return colUserControl.IsDecimalShow;
            }
            set
            {
                GetPropertyByName("IsDecimalShow").SetValue(colUserControl, value);
            }
        }
        public int NumberOfChars
        {
            get
            {
                return colUserControl.NumberOfChars;
            }
            set
            {
                GetPropertyByName("NumberOfChars").SetValue(colUserControl, value);
            }
        }
        public string Value
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

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();
            items.Add(new DesignerActionPropertyItem("BackgroundColor",
                                 "Back Ground Color", "Appearance",
                                 "Selects the on color of Background."));
            items.Add(new DesignerActionPropertyItem("DarkColor",
                                 "Dark Color", "Appearance",
                                 "Selects the Dark color of Sevensegment."));
            items.Add(new DesignerActionPropertyItem("LightColor",
                                 "Light Color", "Appearance",
                                 "Selects the light color of Sevensegment."));
            items.Add(new DesignerActionPropertyItem("DecimalShow",
                                 "Decimal Show", "Appearance",
                                 "Set whether show the Decimal."));
            items.Add(new DesignerActionPropertyItem("NumberOfChars",
                                 "Number Of Chars", "Appearance",
                                 "Set the number of Chars."));
            items.Add(new DesignerActionPropertyItem("Value",
                                 "Value", "Appearance",
                                 "Set the value of Sevensegment."));
            //items.Add(new DesignerActionPropertyItem("BlinkOn",
            //                     "BlinkOn", "Blink",
            //                     "Set the of blink of Led."));

            return items;
        }
    }
}

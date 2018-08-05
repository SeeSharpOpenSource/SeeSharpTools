using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace SeeSharpTools.JY.GUI
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class ButtonSwitchDesigner: ControlDesigner
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
                    actionLists.Add(new ButtonSwitchActionList(this.Component));
                }
                return actionLists;
            }
        }

    }

    public class ButtonSwitchActionList : System.ComponentModel.Design.DesignerActionList
    {
        private ButtonSwitch colUserControl;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public ButtonSwitchActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as ButtonSwitch;

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
        public ButtonSwitch.ButtonSwitchStyle Style
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
    }
}

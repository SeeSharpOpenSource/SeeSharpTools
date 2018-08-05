using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class ViewControllerDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(
                        new ViewControllerActionList(this.Component, this.ParentComponent));
                }
                return actionLists;
            }
        }
    }
    internal class ViewControllerActionList : System.ComponentModel.Design.DesignerActionList
    {
        private readonly ViewController _colUserControl;
        private Form _parentControl;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public ViewControllerActionList(IComponent component, IComponent parentComponent)
            : base(component)
        {
            this._colUserControl = component as ViewController;
            this._parentControl = _colUserControl.ParentForm;
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        private void OpenProperty()
        {
            var viewControllerDialog = new ViewControllerDialog(_colUserControl, _parentControl);
            viewControllerDialog.ShowDialog();

            GetPropertyByName("StateNames").SetValue(_colUserControl, _colUserControl.StateNames);
        }


        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        private PropertyDescriptor GetPropertyByName(string propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(_colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }


        // Properties that are targets of DesignerActionPropertyItem entries.
        public string[] StateNames
        {
            get
            {
                return _colUserControl.StateNames;
            }
            set
            {
                GetPropertyByName("StateNames").SetValue(_colUserControl, value);

            }
        }


        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionPropertyItem("StateNames",
                                 "StateNames", "Appearance",
                                 "Set the names of series."));
            items.Add(new DesignerActionMethodItem(this, "OpenProperty", "property"));
            return items;
        }

    }
}

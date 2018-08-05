using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace SeeSharpTools.JY.GUI
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class TankDesigner : ControlDesigner
    {
        #region Fields

        private readonly DesignerVerbCollection designerVerbs = new DesignerVerbCollection();

        private IDesignerHost designerHost;

        private ISelectionService selectionService;

        private DesignerActionUIService designerActionUISvc = null;

        private Tank colUserControl;

        public override SelectionRules SelectionRules
        {
            get
            {
                return Control.Dock == DockStyle.Fill ? SelectionRules.Visible : base.SelectionRules;
            }
        }
        public override DesignerVerbCollection Verbs
        {
            get
            {
                return designerVerbs;
            }
        }


        public IDesignerHost DesignerHost
        {
            get
            {
                return designerHost ?? (designerHost = (IDesignerHost)(GetService(typeof(IDesignerHost))));
            }
        }

        public ISelectionService SelectionService
        {
            get
            {
                return selectionService ?? (selectionService = (ISelectionService)(GetService(typeof(ISelectionService))));
            }
        }


        #endregion

        #region Constructor

        public TankDesigner()
        {
            var verb1 = new DesignerVerb("property", OpenProperty);
            designerVerbs.AddRange(new[] { verb1 });
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;

        }

        #endregion

        #region Private Methods

        private void OpenProperty(Object sender, EventArgs e)
        {
            this.colUserControl = base.Component as Tank;
            var parentControl = (Tank)Control;
            var oldTabs = parentControl.Controls;
            var propertyForm = new TankProperty((Tank)Control);
            propertyForm.ShowDialog();
            //每一次都要改变
            //只改变BackColor进行Designer.cs的强制更新
            GetPropertyByName("BackColor").SetValue(colUserControl, parentControl.BackColor);

        }

        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }
        #endregion

    }

    ///// <summary>
    ///// Processes events of changing the ChartControl and ChartArea settings.
    ///// </summary>
    ///// <param name="sender">The event's sender.</param>
    ///// <param name="e">The event's parameters.</param>
    //private void componentChangeService_ComponentChanged(object sender, ComponentChangedEventArgs e)
    //{
    //    if (this.Control == null)
    //        return;

    //    bool isNecessary = (e.Component == this.Control);


    //    if (!isNecessary)
    //        return;

    //    if (e.Member != null && this.Control != null)
    //        this.Control.Invalidate();
    //}




    #region Overrides

    #endregion

}


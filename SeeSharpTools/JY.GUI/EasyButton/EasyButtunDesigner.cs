using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SeeSharpTools.JY.GUI;
using System.ComponentModel;

namespace SeeSharpTools.JY.GUI
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
   internal class EasyButtonDesigner : ParentControlDesigner
    {
        #region Fields

        private readonly DesignerVerbCollection designerVerbs = new DesignerVerbCollection();

        private IDesignerHost designerHost;

        private ISelectionService selectionService;

        private DesignerActionUIService designerActionUISvc = null;

        private EasyButton colUserControl;

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

        public EasyButtonDesigner()
        {
            var verb1 = new DesignerVerb("property", OpenProperty);
            designerVerbs.AddRange(new[] { verb1 });
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;

        }

        #endregion

        #region Private Methods

        private void OpenProperty(Object sender, EventArgs e)
        {
            this.colUserControl = base.Component as EasyButton;
            var parentControl = (EasyButton)Control;
            var oldTabs = parentControl.Controls;
            var propertyForm = new EasyButtonProperty(parentControl);
            propertyForm.Show();
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

        #region Overrides

        #endregion

    }
}

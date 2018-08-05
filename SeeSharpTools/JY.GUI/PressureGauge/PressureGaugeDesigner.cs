using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace SeeSharpTools.JY.GUI
{
    internal class PressureGaugeDesigner: ControlDesigner
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
                    actionLists.Add(new PressureGaugeActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    public class PressureGaugeActionList : System.ComponentModel.Design.DesignerActionList
    {
        private PressureGauge colUserControl;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public PressureGaugeActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as PressureGauge;

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
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName("BackColor").SetValue(colUserControl, value);

            }
        }


        public double RangeMax
        {
            get
            {
                return colUserControl.Max;
            }
            set
            {
                GetPropertyByName("Max").SetValue(colUserControl, value);
            }
        }


        public double RangeMin
        {
            get
            {
                return colUserControl.Min;
            }
            set
            {
                GetPropertyByName("Min").SetValue(colUserControl, value);
            }
        }
        public float Glossiness
        {
            get
            {
                return colUserControl.Glossiness;
            }
            set
            {
                if (value <= 100 & value >= 0)
                    GetPropertyByName("Glossiness").SetValue(colUserControl, value);
            }
        }
        public double Value
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
    }
}

using System;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;
using ILNumerics.Drawing;
using SeeSharpTools.JY.Graph3D;


namespace SeeSharpTools.JY.Graph3D
{
    internal class SGDesigner:ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists==null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new SGActionList(this.Component));
                    //注意要在这边新建下面所以生成的类
                }
                return actionLists;
            }
        }
    }
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class SGActionList : DesignerActionList
    {
        private SurfaceGraph colUserControl;
        //这一行主要是要根据你修饰的控件来选择

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public SGActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as SurfaceGraph; 

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
        public bool CubeGridVisible
        {
            get { return colUserControl.CubeGridVisible; }
            set { GetPropertyByName("CubeGridVisible").SetValue(colUserControl, value); }
        }

        public bool AxesLineVisible
        {
            get { return colUserControl.AxesLineVisible; }
            set { GetPropertyByName("CubeGridVisible").SetValue(colUserControl, value); }        
        }

        public string XAxisTitle
        {
            get { return colUserControl.XAxisTitle; }
            set { GetPropertyByName("XAxisTitle").SetValue(colUserControl, value); }

        }

        public string YAxisTitle
        {
            get { return colUserControl.YAxisTitle; }
            set { GetPropertyByName("YAxisTitle").SetValue(colUserControl, value); }
        }

        public string ZAxisTitle
        {
            get { return colUserControl.ZAxisTitle; }
            set { GetPropertyByName("ZAxisTitle").SetValue(colUserControl, value); }
        }

        public Color CubeColor
        {
            get { return colUserControl.CubeColor; }
            set { GetPropertyByName("CubeColor").SetValue(colUserControl, value); }
        }

        public Color BackGroundColor
        {
            get { return colUserControl.BackColor; }
            set { GetPropertyByName("BackColor").SetValue(colUserControl, value); }
        }

        public bool WireframeVisible
        {
            get { return colUserControl.WireframeVisible; }
            set { GetPropertyByName("WireframeVisible").SetValue(colUserControl, value); }
        }

        public bool VisibleColorBar
        {
            get { return colUserControl.VisibleColorBar; }
            set { GetPropertyByName("VisibleColorBar").SetValue(colUserControl, value); }
        }

        public int HeightofColorBar
        {
            get { return colUserControl.HeightofColorBar; }
            set { GetPropertyByName("HeightofColorBar").SetValue(colUserControl, value); }
        }

        public int WidthofColorBar
        {
            get { return colUserControl.WidthofColorBar; }
            set { GetPropertyByName("WidthofColorBar").SetValue(colUserControl, value); }
        }

        public Point Position
        {
            get { return colUserControl.Position; }
            set { GetPropertyByName("Position").SetValue(colUserControl, value); }
        }

        public BorderStyle BorderStyleOfColorBar
        {
            get { return colUserControl.BorderStyleOfColorBar; }
            set { GetPropertyByName("BorderStyleOfColorBar").SetValue(colUserControl, value); }
        }

        public Color BackColorOfColorBar
        {
            get { return colUserControl.BackColorOfColorBar; }
            set { GetPropertyByName("BackColorOfColorBar").SetValue(colUserControl, value); }
        }

        public Colormaps ColormapType
        {
            get { return colUserControl.ColormapType; }
            set { GetPropertyByName("ColormapType").SetValue(colUserControl, value); }
        }

        public int DigitsOfColorbar
        {
            get { return colUserControl.DigitsOfColorbar; }
            set { GetPropertyByName("DigitsOfColorbar").SetValue(colUserControl, value); }
        }

        public bool isColorbarHorizontal
        {
            get { return colUserControl.isColorbarHorizontal; }
            set { GetPropertyByName("OrientationOfColorbar").SetValue(colUserControl, value); }
        }


        //以下这一部分的代码是可以写可以不写的地方，这部分主要是来排序，拍出在快速设计视窗中的显示的属性的顺序来用的。
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.


            items.Add(new DesignerActionHeaderItem("Graph"));
            items.Add(new DesignerActionPropertyItem("CubeGridVisible", "Show Grid of cube", "Graph", "Show grid of plotting cubic area(Only apply on 3D mode"));
            items.Add(new DesignerActionPropertyItem("AxesLineVisible", "Show Axis Line", "Graph", "Show axis line"));
            items.Add(new DesignerActionPropertyItem("WireframeVisible", "Show grid on the graph", "Graph", "Show grid on the graph"));
            items.Add(new DesignerActionPropertyItem("XAxisTitle", "X Axis Title", "Graph", "X axis title"));
            items.Add(new DesignerActionPropertyItem("YAxisTitle", "Y Axis Title", "Graph", "Y axis title"));
            items.Add(new DesignerActionPropertyItem("ZAxisTitle", "Z Axis Title", "Graph", "Z axis title"));
            items.Add(new DesignerActionPropertyItem("CubeColor", "Color of Cube", "Graph", "Color of the plotting cubic area"));
            items.Add(new DesignerActionPropertyItem("BackGroundColor", "Color of the background", "Graph", "Color of the background"));
            items.Add(new DesignerActionPropertyItem("ColormapType", "Color Type", "Graph", "Color Type"));


            items.Add(new DesignerActionHeaderItem("Colorbar"));
            items.Add(new DesignerActionPropertyItem("VisibleColorBar", "Show colorbar", "Color Bar", "Show colorbar"));
            items.Add(new DesignerActionPropertyItem("HeightofColorBar", "Height of colorbar", "Color Bar", "Height of colorba"));
            items.Add(new DesignerActionPropertyItem("WidthofColorBar", "Width of colorbar", "Color Bar", "Width of colorbar"));
            items.Add(new DesignerActionPropertyItem("Position", "Position of colorbar", "Color Bar", "Position of colorbar"));
            items.Add(new DesignerActionPropertyItem("BorderStyleOfColorBar", "Border style of colorbar", "Color Bar", "Border style of colorbar"));
            items.Add(new DesignerActionPropertyItem("BackColorOfColorBar", "Background of colorbar", "Color Bar", "Background of colorbar"));
            items.Add(new DesignerActionPropertyItem("DigitsOfColorbar", "Digits of coloorbar", "Color Bar", "Digits of coloorbar"));
            items.Add(new DesignerActionPropertyItem("isColorbarHorizontal", "isColorbarHorizontal", "Color Bar", "Horizontal(true)/Verical orientation of colorbar"));




            return items;
        }

    }

}

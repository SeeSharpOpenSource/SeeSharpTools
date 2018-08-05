using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using ILNumerics;
using ILNumerics.BuiltInFunctions;
using ILNumerics.Drawing.Controls;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Graphs;
using ILNumerics.Algorithms;
using ILNumerics.Drawing.Misc;




/// <summary>
/// Surface Graph Control
/// Reference Library   : ILNumerics.Net (ver 1.4)
///                       ILNumerics.Drawing (ver 0.9)
///             Author  : H.Kutschbach
///             Date    : 2010
///             Email   : support@ilnumerics.net
///             License : LGPL License
/// This is control is for free. You can use for any commercial or non-commercial purposes following LGPL license.
/// [Please do no remove this header when using this control in your application.]
/// 修改者     ：林家纬
/// 修改日期   ：2017.06.20
/// </summary>

namespace SeeSharpTools.JY.Graph3D
{
    public enum stdWfm
    {
        Sinc =0,
        Waterfall=1
    }
    [ToolboxBitmap(typeof(SurfaceGraph), "SurfaceGraph.SurfaceGraph.png")]
    [Designer(typeof(SGDesigner))]
    public partial class SurfaceGraph : UserControl
    {
        #region Private Data
        //Main panel for the 3D graph
        private ILPanel _panel;
        //Surface graph control
        private ILSurfaceGraph _sgr;

        //Colormap for the graph
        private ILColormap _cm;
        //Colorbar for the graph
        private ILColorBar _cb;
        //PlotData
        private ILArray<double> _displayData;

        private bool _flagOfRuntime = false;

        /// <summary>
        /// cube grid for surface graph
        /// </summary>
        private bool _cubeGridVisible;

        /// <summary>
        /// axes line for surface graph
        /// </summary>
        private bool _axesLineVisible;

        /// <summary>
        /// Color for background and Cube
        /// </summary>
        private Color _backColor;
        private Color _cubeColor;

        /// <summary>
        /// Title for X/Y/Z axes
        /// </summary>
        private string _xAxisText;
        private string _yAxisText;
        private string _zAxisText;

        /// <summary>
        /// Wireframe for surface graph
        /// </summary>
        private bool _wireframeVisible;

        /// <summary>
        /// Properties of colorbar
        /// </summary>
        private bool _colorbarVisible;
        private int _heightOfColorbar;
        private int _widthOfColorbar;
        private Point _position;
        private Color _backColorOfColorbar;
        private BorderStyle _borderStyleOfColorbar;
        private Colormaps _colormapType;
        private int _colorbarDigits;
        private bool isHorizontal;

        private ViewPoint _cameraView;
        private InteractiveModes _mouseMode;

        #endregion

        #region constructor
        /// <summary>
        /// Initialize the graph components
        /// </summary>
        public SurfaceGraph()
        {
            InitializeComponent();

            //Create the panel on the GUI
            _panel = ILPanel.Create();
            //Create the color map for the graph
            _cm = new ILColormap(_colormapType);
            //Create the colorbar for the graph
            _cb = new ILColorBar(_cm);

            //Add the panel and colorbar on the GUI
            tableLayoutPanel.Controls.Add(_panel, 0, 0);
            _panel.Dock = DockStyle.Fill;
            _panel.Controls.Add(_cb);

            _cameraView = new ViewPoint();

            //plot the waveform with built-in data
            Plot(stdWfm.Waterfall);

            this.CubeGridVisible = false;
            this.AxesLineVisible = true;
            this.CubeColor = Color.White;
            this.BackGroundColor = Color.White;
            this.WireframeVisible = false;
            this.XAxisTitle = "X";
            this.YAxisTitle = "Y";
            this.ZAxisTitle = "Z";
            this.ColormapType = Colormaps.Hsv;
            this.isColorbarHorizontal = true;
            this.DigitsOfColorbar = 5;
            
            this.BackColorOfColorBar = Color.Transparent;
            this.Position = new Point(5, 5);
            this.WidthofColorBar = 120;
            this.HeightofColorBar = 45;
            checkBox_showCB.Checked = true;
            _cameraView.phi = (float)45.0;
            _cameraView.rho = (float)89.9;
            _cameraView.distance = (float)362;

            InteractiveMode = InteractiveModes.None;


            ColorBarUpdate();
            UpdatePointOfView();

        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Determine if the graph should be update depending on the run-time status
        /// </summary>
        /// <param name="flag"></param>
        public void UpdateFlag(bool flag)
        {
            _flagOfRuntime = flag;
        }

        [DefaultValue(false)]
        [Category("SurfaceGraph Appearance")]
        [Description("Show the grid line of the cube")]
        /// <summary>
        /// Show the grid line of the cube 
        /// </summary>
        public bool CubeGridVisible
        {
            get { return _cubeGridVisible; }
            set
            {
                _cubeGridVisible = value;
                _panel.Axes.GridVisible = _cubeGridVisible;
                UpdateGraph();
            }
        }

        [DefaultValue(true)]
        [Category("SurfaceGraph Appearance")]
        [Description("Show the Axes Line of the surface graph")]
        /// <summary>
        /// Show the Axes Line of the surface graph
        /// </summary>
        /// <param name="axes"></param>
        public bool AxesLineVisible
        {
            get { return _axesLineVisible; }
            set
            {
                _axesLineVisible = value;
                _panel.Axes.LinesVisible = _axesLineVisible;
                UpdateGraph();
            }
        }

        [DefaultValue("X")]
        [Category("SurfaceGraph Appearance")]
        [Description("Label text of X axis")]
        /// <summary>
        /// Label text of X axis
        /// </summary>
        /// <param name="title"></param>
        public string XAxisTitle
        {
            get { return _xAxisText; }
            set
            {
                _xAxisText = value;
                _panel.Axes.XAxis.Label.Text = _xAxisText;
                UpdateGraph();
            }

        }

        [DefaultValue("Y")]
        [Category("SurfaceGraph Appearance")]
        [Description("Label text of Y axis")]
        /// <summary>
        /// Label text of Y axis
        /// </summary>
        /// <param name="title"></param>
        public string YAxisTitle
        {
            get { return _yAxisText; }
            set
            {
                _yAxisText = value;
                _panel.Axes.YAxis.Label.Text = _yAxisText;
                UpdateGraph();
            }
        }

        [DefaultValue("Z")]
        [Category("SurfaceGraph Appearance")]
        [Description("Label text of Z axis")]
        /// <summary>
        /// Label text of Z axis
        /// </summary>
        /// <param name="title"></param>
        public string ZAxisTitle
        {
            get { return _zAxisText; }
            set
            {
                _zAxisText = value;
                _panel.Axes.ZAxis.Label.Text = _zAxisText;
                UpdateGraph();
            }
        }

        [DefaultValue(typeof(Color), "White")]
        [Category("SurfaceGraph Appearance")]
        [Description("Color of the Cube")]
        /// <summary>
        /// Color of the Cube
        /// </summary>
        /// <param name="ARGB"></param>
        public Color CubeColor
        {
            get { return _cubeColor; }
            set
            {
                _cubeColor = value;
                _panel.BackColorCube = _cubeColor;
                UpdateGraph();
            }
        }

        [DefaultValue(typeof(Color), "White")]
        [Category("SurfaceGraph Appearance")]
        [Description("Color of the background")]
        /// <summary>
        /// Color of the background
        /// </summary>
        /// <param name="ARGB"></param>
        public Color BackGroundColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                _panel.BackColor = _backColor;
                UpdateGraph();
            }

        }

        [DefaultValue(true)]
        [Category("SurfaceGraph Appearance")]
        [Description("Enable/Disble the wireframe of the surface graph")]
        /// <summary>
        /// Enable/Disble the wireframe of the surface graph
        /// </summary>
        /// <param name="ARGB"></param>
        public bool WireframeVisible
        {
            get { return _wireframeVisible; }
            set
            {
                _wireframeVisible = value;
                _sgr.Wireframe.Visible = _wireframeVisible;
                UpdateGraph();
            }
        }

        [DefaultValue(true)]
        [Category("ColorBar Appearance")]
        [Description("Enable/Disable the colorbar")]
        /// <summary>
        /// Enable/Disable the colorbar
        /// </summary>
        public bool VisibleColorBar
        {
            get { return _colorbarVisible; }
            set
            {
                checkBox_showCB.Checked = value;
            }
        }


        [DefaultValue(45)]
        [Category("ColorBar Appearance")]
        [Description("Height of the colorbar")]
        /// <summary>
        /// Height of the colorbar   
        /// </summary>
        public int HeightofColorBar
        {
            get { return _heightOfColorbar; }
            set
            {
                _heightOfColorbar = value;
                _cb.Height = _heightOfColorbar;
            }
        }


        [DefaultValue(120)]
        [Category("ColorBar Appearance")]
        [Description("Width of the colorbar")]
        /// <summary>
        /// Width of the colorbar   
        /// </summary>
        public int WidthofColorBar
        {
            get { return _widthOfColorbar; }
            set
            {
                _widthOfColorbar = value;
                _cb.Width = _widthOfColorbar;

            }
        }


        [DefaultValue(typeof(Point), "(5,5)")]
        [Category("ColorBar Appearance")]
        [Description("Position of colorbar")]
        /// <summary>
        /// Position of colorbar  
        /// </summary>
        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _cb.Location = _position;
            }
        }


        [DefaultValue(BorderStyle.FixedSingle)]
        [Category("ColorBar Appearance")]
        [Description("BorderStyle of the colorbar")]
        /// <summary>
        /// BorderStyle of the colorbar   
        /// </summary>
        public BorderStyle BorderStyleOfColorBar
        {
            get { return _borderStyleOfColorbar; }
            set
            {
                _borderStyleOfColorbar = value;
                _cb.BorderStyle = _borderStyleOfColorbar;
            }
        }


        [DefaultValue(typeof(Color), "White")]
        [Category("ColorBar Appearance")]
        [Description("BackColor of the colorbar")]
        /// <summary>
        /// BackColor of the colorbar   
        /// </summary>
        public Color BackColorOfColorBar
        {
            get { return _backColorOfColorbar; }
            set
            {
                _backColorOfColorbar = value;
                _cb.BackColor = _backColorOfColorbar;
                //ColorBarUpdate();
            }
        }

        [DefaultValue(typeof(Colormaps), "Hsv")]
        [Category("ColorBar Appearance")]
        [Description("Type of Colormap")]
        /// <summary>
        /// Type of Colormap  
        /// </summary>
        public Colormaps ColormapType
        {
            get { return _colormapType; }
            set
            {
                _colormapType = value;
                ColorBarUpdate();
                UpdateGraph();
            }
        }

        [DefaultValue(5)]
        [Category("ColorBar Appearance")]
        [Description("Diaplay Digits of colorbar")]
        /// <summary>
        /// Diaplay Digits of colorbar
        /// </summary>
        public int DigitsOfColorbar
        {
            get { return _colorbarDigits; }
            set
            {
                _colorbarDigits = value;
                _cb.Precision = _colorbarDigits;
                ColorBarUpdate();
            }
        }

        [DefaultValue(true)]
        [Category("ColorBar Appearance")]
        [Description("Orientation of Colorbar(true is Horizontal)")]
        /// <summary>
        /// Orientation of Colorbar
        /// </summary>
        public bool isColorbarHorizontal
        {
            get { return isHorizontal; }
            set
            {
                isHorizontal = value;
                ColorBarUpdate();
            }
        }

        public ViewPoint PerspectiveView
        {
            get
            {
                return _cameraView;
            }

            set
            {
                _cameraView = value;

            }
        }

        public bool Is2DMode
        {
            get { return checkBox_2DMode.Checked; }
            set { checkBox_2DMode.Checked = value; }
        }
        public InteractiveModes InteractiveMode
        {
            get { return _mouseMode; }
            set
            {
                _mouseMode = value;
                _panel.InteractiveMode = _mouseMode;
            }
        }
        public bool ShowXAxis
        {
            set
            {
                _panel.Axes.XAxis.Visible = value;
                _panel.Invalidate();
            }
        }

        public bool ShowYAxis
        {
            set
            {
                _panel.Axes.YAxis.Visible = value;
                _panel.Invalidate();
            }
        }

        public bool ShowZAxis
        {
            set
            {
                _panel.Axes.ZAxis.Visible = value;
                _panel.Invalidate();
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Plot with assigned double data (2 dimension)
        /// </summary>
        /// <param name="z"></param>
        public void Plot(double[,] z)
        {
            ///Colormap and part of the colorbar function could only be modified in the static state.
            ///This will ensure the best efficiency in run-time status.

            _panel.Graphs.Clear();

            _displayData = z;
            _sgr = _panel.Graphs.AddSurfGraph(_displayData);
            //_sgr.Opacity = 0.8f;
            //_sgr.Shading = ShadingStyles.Interpolate;
            //ApplyProperties();
            //AutoScaleColorRange();
            UpdateGraph();
            
            
        }

        /// <summary>
        /// Plot with built-in waveform in the library
        /// </summary>
        /// <param name="builtInWfmIndex"></param>
        public void Plot(stdWfm wfmType)
        {
            ///Colormap and part of the colorbar function could only be modified in the static state.
            ///This will ensure the best efficiency in run-time status.

            switch (wfmType)
            {
                case stdWfm.Sinc:
                    _displayData = ILSpecialData.sinc(128, 128) + 1;
                    break;
                case stdWfm.Waterfall:
                    _displayData = ILSpecialData.waterfall(128, 128) + 1;
                    break;
                default:
                    _displayData = ILSpecialData.sinc(128, 128) + 1;
                    break;
            }
            _panel.Graphs.Clear();
            _sgr = _panel.Graphs.AddSurfGraph(_displayData);
            _sgr.Opacity = 0.8f;
            _sgr.Shading = ShadingStyles.Interpolate;
            ApplyProperties();
            AutoScaleColorRange();
            UpdateGraph();

        }

        /// <summary>
        /// Clear the waveform 
        /// </summary>
        public void Clear()
        {
            _panel.Graphs.Clear();
        }

        /// <summary>
        /// Update the surface Graph
        /// </summary>
        private void UpdateGraph()
        {
            if (_flagOfRuntime == false)
            {
                _panel.Invalidate();
            }

        }

        /// <summary>
        /// Update the colorbar of the surface graph
        /// </summary>
        private void ColorBarUpdate()
        {
            _panel.Controls.Remove(_cb);
            _cm = new ILColormap(_colormapType);
            _cm.Data = ILMath.tosingle(_displayData);
            _panel.Colormap = _cm;
            _cb = new ILColorBar(_cm);
            _cb.Orientation = isHorizontal?TextOrientation.Horizontal:TextOrientation.Vertical;
            _cb.Precision = _colorbarDigits;
            _cb.BorderStyle = _borderStyleOfColorbar;
            _cb.Visible = _colorbarVisible;
            _cb.BackColor = _backColor;
            _cb.Width = _widthOfColorbar;
            _cb.Height = _heightOfColorbar;
            _cb.Location = _position;
            _panel.Controls.Add(_cb);
            AutoScaleColorRange();
        }

        /// <summary>
        /// Autoscale the range of the colorbar
        /// </summary>
        private void AutoScaleColorRange()
        {
            _cb.Update((float)_displayData.MinValue, (float)_displayData.MaxValue);
        }

        /// <summary>
        /// Change the Phi angle of perspective view
        /// </summary>
        private void hScrollBar_Phi_Scroll(object sender, ScrollEventArgs e)
        {
            _cameraView.phi = (float)(hScrollBar_Phi.Value / 100.0);
            UpdatePointOfView();
        }

        /// <summary>
        /// Change the Rho angle of perspective view
        /// </summary>
        private void vScrollBar_Rho_Scroll(object sender, ScrollEventArgs e)
        {
            _cameraView.rho = (float)(vScrollBar_Rho.Value / 100.0);
            UpdatePointOfView();
        }

        /// <summary>
        /// Distance of the perspective View
        /// </summary>
        private void numericUpDown_distance_ValueChanged(object sender, System.EventArgs e)
        {
            _cameraView.distance = (float)numericUpDown_distance.Value;
            UpdatePointOfView();
        }

        /// <summary>
        /// Rho angle of the perspective View
        /// </summary>
        private void numericUpDown_rho_ValueChanged(object sender, System.EventArgs e)
        {
            _cameraView.rho = (float)numericUpDown_rho.Value;
            UpdatePointOfView();
        }

        /// <summary>
        /// Phi angle of the perspective View
        /// </summary>
        private void numericUpDown_phi_ValueChanged(object sender, System.EventArgs e)
        {
            _cameraView.phi = (float)numericUpDown_phi.Value;
            UpdatePointOfView();
        }

        /// <summary>
        /// Update the perspective view parameters and replot
        /// </summary>
        private void UpdatePointOfView()
        {
            _panel.DefaultView.SetDeg(_cameraView.phi, _cameraView.rho, _cameraView.distance);
            hScrollBar_Phi.Value = (int)(_cameraView.phi * 100.0);
            vScrollBar_Rho.Value = (int)(_cameraView.rho * 100.0);
            numericUpDown_phi.Value = (decimal)_cameraView.phi;
            numericUpDown_rho.Value = (decimal)_cameraView.rho;
            numericUpDown_distance.Value = (decimal)_cameraView.distance;
            _panel.ResetView();
            _panel.Refresh();
        }

        #endregion

        /// <summary>
        /// Show Colorbar button
        /// </summary>
        private void checkBox_showCB_CheckedChanged(object sender, System.EventArgs e)
        {
            _colorbarVisible = checkBox_showCB.Checked;
            if (_colorbarVisible)
            {
                _cb.Show();
            }
            else
            {
                _cb.Hide();
            }
        }

        /// <summary>
        /// Switch the 2D/3D graph mode
        /// </summary>
        private void checkBox_2DMode_CheckedChanged(object sender, System.EventArgs e)
        {

            if (checkBox_2DMode.Checked)
            {
                _cameraView.phi = _cameraView.rho = 0;
                hScrollBar_Phi.Enabled = false;
                vScrollBar_Rho.Enabled = false;
                numericUpDown_phi.Enabled = false;
                numericUpDown_rho.Enabled = false;
                numericUpDown_distance.Enabled = false;
            }

            else
            {                
                _cameraView.phi = (float)45;
                _cameraView.rho = (float)89.9;
                hScrollBar_Phi.Enabled = true;
                vScrollBar_Rho.Enabled = true;
                numericUpDown_phi.Enabled = true;
                numericUpDown_rho.Enabled = true;
                numericUpDown_distance.Enabled = true;
            }
            UpdatePointOfView();
            UpdateGraph();
        }

        /// <summary>
        /// Apply Parameters when replot the graph
        /// </summary>
        private void ApplyProperties()
        {
            _panel.Axes.GridVisible = _cubeGridVisible;
            _panel.Axes.LinesVisible = _axesLineVisible;
            _panel.Axes.XAxis.Label.Text = _xAxisText;
            _panel.Axes.YAxis.Label.Text = _yAxisText;
            _panel.Axes.ZAxis.Label.Text = _zAxisText;
            _panel.BackColorCube = _cubeColor;
            _panel.BackColor = _backColor;
            _sgr.Wireframe.Visible = _wireframeVisible;
            _panel.Refresh();
        }

    }



}

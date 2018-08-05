using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;


namespace SeeSharpTools.JY.GUI
{

    [Designer(typeof(ThermometerDesigner))]
 
    [ToolboxBitmap(typeof(Slide), "Thermometer.Thermometer.bmp")]
    [DefaultProperty("Maximum")]
    [DefaultEvent("ValueChanged")]
    public partial class Thermometer : UserControl
    {
        #region Private Members

        // Instance fields
        private double _value = 0;
        private double _minimum = 0;
        private double _maximum = 100;

        private int _largeChange = 2;
        private double _smallChange = 1;


        private Color _borderColor = SystemColors.ActiveBorder;


        private int _indentWidth = 10;
        private int _indentHeight = 10;

        private int _tickHeight = 2;
        
        private Color _tickColor = Color.Black;
        private TickStyle _tickStyle = TickStyle.BottomRight;
        private TickStyle _textTickStyle = TickStyle.BottomRight;

        private int _trackLineHeight = 3;
        private Color _trackLineColor = SystemColors.Control;

        public RectangleF _trackerRect = RectangleF.Empty;

        private bool _autoSize = true;

        private bool leftButtonDown = false;
        private float mouseStartPos = -1;
        //修改by 邵天宇
        private int decimals = 3;
        //  private SlidesTrackerStyle styles = SlidesTrackerStyle.rectangle;
        private int ballSize = 15;
        private Size _trackerSize = new Size(15,15);
        private int _interval = 10;
        //tickFrequency = (max -min )/10
        private double _tickFrequency = 10;
        private Color textColor = Color.Black;

        /// <summary>
        /// Occurs when either a mouse or keyboard action moves the slider.
        /// </summary>
        //    public event EventHandler Scroll;

        #endregion

        #region Public Contruction

        /// <summary>
        /// Constructor method of <see cref="Slide"/> class
        /// </summary>

        public Thermometer()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);

            Font = new Font("Verdana", 8.25F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            ForeColor = Color.FromArgb(123, 125, 123);
            BackColor = Color.Transparent;

            _tickColor = Color.FromArgb(148, 146, 148);
            _tickHeight = 4;

            _trackerSize = new Size(15,15);
            _indentWidth = 6;
            _indentHeight = 6;

            _trackLineColor = Color.FromArgb(90, 93, 90);
            _trackLineHeight = 5;

            _borderColor = SystemColors.ActiveBorder;

            _autoSize = true;
            this.Height = FitSize.Height;

            this.ForeColor = Color.Red;
        }

        #endregion

        #region Public Properties

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this._autoSize)
            {
                // Calculate the Position for children controls
              
                    this.Width = FitSize.Width;
              
                //=================================================
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether the height or width of the track bar is being automatically sized.
        /// </summary>
        /// <remarks>You can set the AutoSize property to true to cause the track bar to adjust either its height or width, depending on orientation, to ensure that the control uses only the required amount of space.</remarks>
        /// <value>true if the track bar is being automatically sized; otherwise, false. The default is true.</value>
        [Category("Behavior")]
        [Description("Gets or sets the height of track line.")]
        [DefaultValue(true)]
        private bool AutoSize
        {
            get { return _autoSize; }

            set
            {
                if (_autoSize != value)
                {
                    _autoSize = value;
                    if (_autoSize == true)
                        this.Size = FitSize;
                }
            }
        }
        /// <summary>
        /// Gets or sets the height of track line.
        /// </summary>
        /// <value>The default value is 4.</value>
        [Category("Appearance")]
        [Description("Gets or sets the height of track line.")]
        [DefaultValue(4)]
        public int LineWidth
        {
            get { return _trackLineHeight; }

            set
            {
                if (_trackLineHeight != value)
                {
                    _trackLineHeight = value;
                    if (_trackLineHeight < 1)
                        _trackLineHeight = 1;

                    if (_trackLineHeight > _trackerSize.Height)
                        _trackLineHeight = _trackerSize.Height;

                    this.Invalidate();
                }

            }
        }

        /// <summary>
        /// Gets or sets the tick's <see cref="Color"/> of the control.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets the tick's color of the control.")]
        public Color TickColor
        {
            get { return _tickColor; }

            set
            {
                if (_tickColor != value)
                {
                    _tickColor = value;
                    this.Invalidate();
                }

            }
        }
        /// <summary>
        /// Gets or sets the height of tick.
        /// </summary>
        /// <value>The height of tick in pixels. The default value is 2.</value>
        [Category("Appearance")]
        [Description("Gets or sets the height of tick.")]
        [DefaultValue(6)]
        public int TickWidth
        {
            get { return _tickHeight; }

            set
            {
                if (_tickHeight != value)
                {
                    _tickHeight = value;

                    if (_tickHeight < 1)
                        _tickHeight = 1;

                    if (_autoSize == true)
                        this.Size = FitSize;

                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Gets or sets the tracker's size. 
        /// The tracker's width must be greater or equal to tracker's height.
        /// </summary>
        /// <value>The <see cref="Size"/> object that represents the height and width of the tracker in pixels.</value>
        [Category("Appearance")]
        [Description("Gets or sets the tracker's size.")]
        public int BallSize
        {
            get { return ballSize; }

            set
            {
                ballSize = value;
                _trackerSize.Width = ballSize;
                _trackerSize.Height = ballSize;

                if (_autoSize == true)
                    this.Size = FitSize;

                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text tick style of the trackbar.
        /// There are 4 styles for selection: None, TopLeft, BottomRight, Both. 
        /// </summary>
        /// <remarks>You can use the <see cref="Control.Font"/>, <see cref="Control.ForeColor"/>
        /// properties to customize the tick text.</remarks>
        /// <value>One of the <see cref="TickStyle"/> values. The default is <b>BottomRight</b>.</value>
        [Category("Appearance")]
        [Description("Gets or sets the text tick style.")]
        [DefaultValue(TickStyle.BottomRight)]
        public TickStyle TextStyle
        {
            get { return _textTickStyle; }

            set
            {
                if (_textTickStyle != value)
                {
                    _textTickStyle = value;

                    if (_autoSize == true)
                        this.Size = FitSize;

                    this.Invalidate();
                }

            }
        }

        /// <summary>
        /// Gets or sets the tick style of the trackbar.
        /// There are 4 styles for selection: None, TopLeft, BottomRight, Both. 
        /// </summary>
        /// <remarks>You can use the <see cref="TickColor"/>, <see cref="TickFrequency"/>, 
        /// <see cref="TickWidth"/> properties to customize the trackbar's ticks.</remarks>
        /// <value>One of the <see cref="TickStyle"/> values. The default is <b>BottomRight</b>.</value>
        [Category("Appearance")]
        [Description("Gets or sets the tick style.")]
        [DefaultValue(TickStyle.BottomRight)]
        public TickStyle TickStyle
        {
            get { return _tickStyle; }

            set
            {
                if (_tickStyle != value)
                {
                    _tickStyle = value;

                    if (_autoSize == true)
                        this.Size = FitSize;

                    this.Invalidate();
                }

            }
        }


        /// <summary>
        /// Gets or sets a numeric value that represents the current position of the slider on the track bar.
        /// </summary>
        /// <remarks>The Value property contains the number that represents the current position of the slider on the track bar.</remarks>
        /// <value>A numeric value that is within the <see cref="Min"/> and <see cref="Max"/> range. 
        /// The default value is 0.</value>
        [Description("The current value for the MACTrackBar, in the range specified by the Minimum and Maximum properties.")]
        [Category("Behavior")]
        public double Value
        {
            get
            {
                return _value;

            }
            set
            {
                if (_value != value)
                {
                    if (value < _minimum)
                        _value = _minimum;
                    else
                        if (value > _maximum)
                        _value = _maximum;
                    else
                        _value = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the lower limit of the range this <see cref="Slide"/> is working with.
        /// </summary>
        /// <remarks>You can use the <see cref="SetRange"/> method to set both the <see cref="Max"/> and <see cref="Min"/> properties at the same time.</remarks>
        /// <value>The minimum value for the <see cref="Slide"/>. The default value is 0.</value>
        [Description("The lower bound of the range this MACTrackBar is working with.")]
        [Category("Behavior")]
        public double Min
        {
            get
            {
                return _minimum;
            }
            set
            {
                _minimum = value;

                if (_minimum > _maximum)
                    _maximum = _minimum;
                if (_minimum > _value)
                    _value = _minimum;

                this.TickFrequency = (_maximum - _minimum) / _interval;
                if (_autoSize == true)
                    this.Size = FitSize;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the upper limit of the range this <see cref="Slide"/> is working with.
        /// </summary>
        /// <remarks>You can use the <see cref="SetRange"/> method to set both the <see cref="Max"/> and <see cref="Min"/> properties at the same time.</remarks>
        /// <value>The maximum value for the <see cref="Slide"/>. The default value is 10.</value>
        [Description("The uppper bound of the range this MACTrackBar is working with.")]
        [Category("Behavior")]
        public double Max
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value;

                if (_maximum < _value)
                    _value = _maximum;
                if (_maximum < _minimum)
                    _minimum = _maximum;

                this.TickFrequency = (_maximum - _minimum) / _interval;

                if (_autoSize == true)
                    this.Size = FitSize;
                this.Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the interval of the control.
        /// </summary>
        /// <value>A <see cref="Color"/> object that represents the border color of the control.</value>
        [Category("Appearance")]
        [Description("Gets or sets the interval of the control.")]
        public int NumberOfDivisions
        {
            get
            {
                return _interval;
            }

            set
            {
                if (value >= 1 && value <= 25)
                {
                    _interval = value;
                    TickFrequency = (Max - Min) / _interval;
                    this.Invalidate();
                }

            }
        }
        /// <summary>
        /// Gets or sets the Decimals of the control.
        /// </summary>
        /// <value>A <see cref="Color"/> object that represents the border color of the control.</value>
        [Category("Appearance")]
        [Description("Gets or sets the Decimals of the control.")]
        public int TextDecimals
        {
            get
            {
                return decimals;
            }

            set
            {
                if (value >= 0 && value <= 10)
                {
                    decimals = value;
                    this.Invalidate();
                }
            }
        }


        #endregion

        #region Private Properties

        /// <summary>
        /// Gets or sets the width of indent (or Padding-Y).
        /// </summary>
        /// <value>The width of indent in pixels. The default value is 6.</value>
        [Category("Appearance")]
        [Description("Gets or sets the width of indent.")]
        [DefaultValue(6)]
        private int IndentWidth
        {
            get { return _indentWidth; }

            set
            {
                if (_indentWidth != value)
                {
                    _indentWidth = value;
                    if (_indentWidth < 0)
                        _indentWidth = 0;

                    if (_autoSize == true)
                        this.Size = FitSize;

                    this.Invalidate();
                }

            }
        }

        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the <see cref="Value"/> property when the slider is moved a small distance.
        /// </summary>
        /// <remarks>
        /// When the user presses one of the arrow keys, the <see cref="Value"/> property changes according to the value set in the SmallChange property.
        /// You might consider setting the <see cref="SmallChange"/> value to a percentage of the <see cref="Control.Height"/> (for a vertically oriented track bar) or 
        /// <see cref="Control.Width"/> (for a horizontally oriented track bar) values. This keeps the distance your track bar moves proportionate to its size.
        /// </remarks>
        /// <value>A numeric value. The default value is 1.</value>
        [Category("Behavior")]
        [Description("Gets or sets a value to be added to or subtracted from the Value property when the slider is moved a small distance.")]
        [DefaultValue(1)]
        private double SmallChange
        {
            get { return _smallChange; }

            set
            {
                _smallChange = value;
                //if (_smallChange < 1)
                //    _smallChange = 1;
            }
        }

        /// <summary>
        /// Gets the Size of area need for drawing.
        /// </summary>
        [Description("Gets the Size of area need for drawing.")]
        [Browsable(false)]
        private Size FitSize
        {
            get
            {
                Size fitSize;
                float textAreaSize;

                // Create a Graphics object for the Control.
                Graphics g = this.CreateGraphics();

                Rectangle workingRect = Rectangle.Inflate(this.ClientRectangle, -_indentWidth, -_indentHeight);
                float currentUsedPos = 0;


                currentUsedPos = _indentWidth;
                //==========================================================================

                // Get Width of Text Area
                textAreaSize = g.MeasureString(_maximum.ToString(), this.Font).Width;

                if (_textTickStyle == TickStyle.TopLeft || _textTickStyle == TickStyle.Both)
                    currentUsedPos += textAreaSize;

                if (_tickStyle == TickStyle.TopLeft || _tickStyle == TickStyle.Both)
                    currentUsedPos += _tickHeight + 1;

                currentUsedPos += _trackerSize.Height;

                if (_tickStyle == TickStyle.BottomRight || _tickStyle == TickStyle.Both)
                {
                    currentUsedPos += 1;
                    currentUsedPos += _tickHeight;
                }

                if (_textTickStyle == TickStyle.BottomRight || _textTickStyle == TickStyle.Both)
                    currentUsedPos += textAreaSize;

                currentUsedPos += _indentWidth;

                fitSize = new Size((int)currentUsedPos, this.ClientRectangle.Height);

                // Clean up the Graphics object.
                g.Dispose();

                return fitSize;
            }
        }


        /// <summary>
        /// Gets the rectangle containing the tracker.
        /// </summary>
        [Description("Gets the rectangle containing the tracker.")]
        private RectangleF TrackerRect
        {
            get
            {
                RectangleF trackerRect;
                float textAreaSize;

                // Create a Graphics object for the Control.
                Graphics g = this.CreateGraphics();

                Rectangle workingRect = Rectangle.Inflate(this.ClientRectangle, -_indentWidth, -_indentHeight);
                double currentUsedPos = 0;



                currentUsedPos = _indentWidth;
                //==========================================================================

                // Get Width of Text Area
                textAreaSize = g.MeasureString(_maximum.ToString(), this.Font).Width;

                if (_textTickStyle == TickStyle.TopLeft || _textTickStyle == TickStyle.Both)
                    currentUsedPos += textAreaSize;

                if (_tickStyle == TickStyle.TopLeft || _tickStyle == TickStyle.Both)
                    currentUsedPos += _tickHeight + 1;

                //==========================================================================
                // Caculate the Tracker's rectangle
                //==========================================================================
                double currentTrackerPos;
                if (_maximum == _minimum)
                    currentTrackerPos = workingRect.Top;
                else
                    currentTrackerPos = (workingRect.Height - _trackerSize.Width) * (_value - _minimum) / (_maximum - _minimum);

                trackerRect = new RectangleF((float)currentUsedPos, (float)(workingRect.Bottom - currentTrackerPos - _trackerSize.Width), _trackerSize.Height, _trackerSize.Width);// Remember this for drawing the Tracker later
                trackerRect.Inflate(-1, 0);
                
                // Clean up the Graphics object.
                g.Dispose();

                return trackerRect;
            }
        }

        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the <see cref="Value"/> property when the slider is moved a large distance.
        /// </summary>
        /// <remarks>
        /// When the user presses the PAGE UP or PAGE DOWN key or clicks the track bar on either side of the slider, the <see cref="Value"/> 
        /// property changes according to the value set in the <see cref="LargeChange"/> property. 
        /// You might consider setting the <see cref="LargeChange"/> value to a percentage of the <see cref="Control.Height"/> (for a vertically oriented track bar) or 
        /// <see cref="Control.Width"/> (for a horizontally oriented track bar) values. This keeps the distance your track bar moves proportionate to its size.
        /// </remarks>
        /// <value>A numeric value. The default value is 2.</value>
        [Category("Behavior")]
        [Description("Gets or sets a value to be added to or subtracted from the Value property when the slider is moved a large distance.")]
        [DefaultValue(2)]
        private int LargeChange
        {
            get { return _largeChange; }

            set
            {
                _largeChange = value;
                if (_largeChange < 1)
                    _largeChange = 1;
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies the delta between ticks drawn on the control.
        /// </summary>
        /// <remarks>
        /// For a <see cref="Slide"/> with a large range of values between the <see cref="Min"/> and the 
        /// <see cref="Max"/>, it might be impractical to draw all the ticks for values on the control. 
        /// For example, if you have a control with a range of 100, passing in a value of 
        /// five here causes the control to draw 20 ticks. In this case, each tick 
        /// represents five units in the range of values.
        /// </remarks>
        /// <value>The numeric value representing the delta between ticks. The default is 1.</value>
        [Category("Appearance")]
        [Description("Gets or sets a value that specifies the delta between ticks drawn on the control.")]
        [DefaultValue(1)]
        private double TickFrequency
        {
            get { return _tickFrequency; }

            set
            {
                if (_tickFrequency != value)
                {
                    _tickFrequency = value;
                    //if (_tickFrequency < 1)
                    //    _tickFrequency = 1;
                    this.Invalidate();
                }

            }
        }

        /// <summary>
        /// Gets or sets the height of indent (or Padding-Y).
        /// </summary>
        /// <value>The height of indent in pixels. The default value is 6.</value>
        [Category("Appearance")]
        [Description("Gets or sets the height of indent.")]
        [DefaultValue(2)]
        private int IndentHeight
        {
            get { return _indentHeight; }

            set
            {
                if (_indentHeight != value)
                {
                    _indentHeight = value;
                    if (_indentHeight < 0)
                        _indentHeight = 0;

                    if (_autoSize == true)
                        this.Size = FitSize;

                    this.Invalidate();
                }

            }
        }


        /// <summary>
        /// Gets or sets the border color of the control.
        /// </summary>
        /// <value>A <see cref="Color"/> object that represents the border color of the control.</value>
        [Category("Appearance")]
        [Description("Gets or sets the border color of the control.")]
        private Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (value != _borderColor)
                {
                    _borderColor = value;
                    Invalidate();
                }
            }
        }
        [Description("Set the color of Text.")]
        [Category("Appearance")]
        public Color TextColor
        {
            get
            {
                return textColor;
            }

            set
            {
                textColor = value;
                this.Invalidate();
            }
        }
        [Description("Set the color of Line.")]
        [Category("Appearance")]
        public Color LineColor
        {
            get
            {
                return _trackLineColor;
            }

            set
            {
                _trackLineColor = value;
                this.Invalidate();
            }
        }
        #endregion
        #region Public Methods




        /// <summary>
        /// Call the Increment() method to increase the value displayed by an integer you specify 
        /// </summary>
        /// <param name="value"></param>
        private void Increment(double value)
        {
            if (_value < _maximum)
            {
                _value += value;
                if (_value > _maximum)
                    _value = _maximum;
            }
            else
                _value = _maximum;
            this.Invalidate();
        }

        /// <summary>
        /// Call the Decrement() method to decrease the value displayed by an integer you specify 
        /// </summary>
        /// <param name="value"> The value to decrement</param>
        private void Decrement(double value)
        {
            if (_value > _minimum)
            {
                _value -= value;
                if (_value < _minimum)
                    _value = _minimum;
            }
            else
                _value = _minimum;
            this.Invalidate();
        }

        /// <summary>
        /// Sets the minimum and maximum values for a TrackBar.
        /// </summary>
        /// <param name="minValue">The lower limit of the range of the track bar.</param>
        /// <param name="maxValue">The upper limit of the range of the track bar.</param>
        private void SetRange(int minValue, int maxValue)
        {
            _minimum = minValue;

            if (_minimum > _value)
                _value = _minimum;

            _maximum = maxValue;

            if (_maximum < _value)
                _value = _maximum;
            if (_maximum < _minimum)
                _minimum = _maximum;

            this.Invalidate();
        }

        /// <summary>
        /// Reset the appearance properties.
        /// </summary>
        private void ResetAppearance()
        {
            Font = new Font("Verdana", 8.25F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            ForeColor = Color.FromArgb(123, 125, 123);
            BackColor = Color.Transparent;

            _tickColor = Color.FromArgb(148, 146, 148);
            _tickHeight = 4;

            ForeColor = Color.Red;
            _trackerSize = new Size(16, 16);
            //_trackerRect.Size = _trackerSize;

            _indentWidth = 6;
            _indentHeight = 6;

            _trackLineColor = Color.FromArgb(90, 93, 90);
            _trackLineHeight = 3;

            _borderColor = SystemColors.ActiveBorder;

            //==========================================================================

            if (_autoSize == true)
                this.Size = FitSize;
            Invalidate();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// The OnCreateControl method is called when the control is first created.
        /// </summary>
        protected override void OnCreateControl()
        {
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnLostFocus">Control.OnLostFocus</see>.
        /// </summary>
        protected override void OnLostFocus(EventArgs e)
        {
            this.Invalidate();
            base.OnLostFocus(e);
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnGotFocus">Control.OnGotFocus</see>.
        /// </summary>
        protected override void OnGotFocus(EventArgs e)
        {
            this.Invalidate();
            base.OnGotFocus(e);
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnClick">Control.OnClick</see>.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            this.Focus();
            this.Invalidate();
            base.OnClick(e);
        }

        /// <summary>
        /// This member overrides <see cref="Control.ProcessCmdKey">Control.ProcessCmdKey</see>.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool blResult = true;

            /// <summary>
            /// Specified WM_KEYDOWN enumeration value.
            /// </summary>
            const int WM_KEYDOWN = 0x0100;

            /// <summary>
            /// Specified WM_SYSKEYDOWN enumeration value.
            /// </summary>
            const int WM_SYSKEYDOWN = 0x0104;


            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Left:
                    case Keys.Down:
                        this.Decrement(_smallChange);
                        break;
                    case Keys.Right:
                    case Keys.Up:
                        this.Increment(_smallChange);
                        break;

                    case Keys.PageUp:
                        this.Increment(_largeChange);
                        break;
                    case Keys.PageDown:
                        this.Decrement(_largeChange);
                        break;

                    case Keys.Home:
                        Value = _maximum;
                        break;
                    case Keys.End:
                        Value = _minimum;
                        break;

                    default:
                        blResult = base.ProcessCmdKey(ref msg, keyData);
                        break;
                }
            }

            return blResult;
        }

        /// <summary>
        /// Dispose of instance resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)

        {

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Painting Methods

        /// <summary>
        /// This member overrides <see cref="Control.OnPaint">Control.OnPaint</see>.
        /// </summary>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Brush brush;
            RectangleF rectTemp, drawRect, drawFillRect;
            float textAreaSize;
            //如果有问题 则更改这里，显示不够长的话
            Rectangle workingRect = Rectangle.Inflate(this.ClientRectangle, -_indentWidth - 10, -_indentHeight);
            float currentUsedPos = 0;

            //==========================================================================
            // Draw the background of the ProgressBar control.
            //==========================================================================
            brush = new SolidBrush(this.BackColor);
            rectTemp = this.ClientRectangle;
            e.Graphics.FillRectangle(brush, rectTemp);
            brush.Dispose();
            //==========================================================================

            //==========================================================================

             //_orientation == Orientation.Vertical
            
                currentUsedPos = _indentWidth;
                //==========================================================================

                // Get Width of Text Area
                textAreaSize = e.Graphics.MeasureString(_maximum.ToString(), this.Font).Width;

                if (_textTickStyle == TickStyle.TopLeft || _textTickStyle == TickStyle.Both)
                {
                    //==========================================================================
                    // Draw the 1st Text Line.
                    //==========================================================================
                    // Get Height of Text Area
                    drawRect = new RectangleF(currentUsedPos, workingRect.Top, textAreaSize, workingRect.Height);
                    drawRect.Inflate(0, -_trackerSize.Width / 2);
                    currentUsedPos += textAreaSize;

                    DrawTickTextLine(e.Graphics, drawRect, _tickFrequency, _minimum, _maximum, this.ForeColor, this.Font, Orientation.Vertical);
                    //==========================================================================
                }

                if (_tickStyle == TickStyle.TopLeft || _tickStyle == TickStyle.Both)
                {
                    //==========================================================================
                    // Draw the 1st Tick Line.
                    //==========================================================================
                    drawRect = new RectangleF(currentUsedPos, workingRect.Top, _tickHeight, workingRect.Height);
                    drawRect.Inflate(0, -_trackerSize.Width / 2);
                    currentUsedPos += _tickHeight + 1;

                    DrawTickLine(e.Graphics, drawRect, _tickFrequency, _minimum, _maximum, _tickColor, Orientation.Vertical);
                    //==========================================================================
                }

                //==========================================================================
                // Caculate the Tracker's rectangle
                //==========================================================================
                double currentTrackerPos;
                currentTrackerPos = 0;

                _trackerRect = new RectangleF(currentUsedPos, (float)(workingRect.Bottom - currentTrackerPos - _trackerSize.Width), _trackerSize.Height, _trackerSize.Width);// Remember this for drawing the Tracker later
                                                                                                                                                                             //_trackerRect.Inflate(-1,0);

                rectTemp = _trackerRect;//Testing

            //==========================================================================
            // Draw the Track Line
            //==========================================================================


                drawRect = new RectangleF(currentUsedPos + _trackerSize.Height / 2 - _trackLineHeight / 2, workingRect.Top, _trackLineHeight, workingRect.Height);      
                
                DrawTrackLine(e.Graphics, drawRect);
                drawRect = new RectangleF(currentUsedPos, workingRect.Top, _tickHeight, workingRect.Height);
                drawRect.Inflate(0, -_trackerSize.Width / 2);

                double currentValuePos = drawRect.Height * (Value - _minimum) / (_maximum - _minimum);
            //    drawFillRect = new RectangleF(currentUsedPos + _trackerSize.Height / 2 - _trackLineHeight / 2, drawRect.Bottom -(float) currentValuePos, _trackLineHeight, (float)( currentValuePos - _trackerSize.Width / 2));
                drawFillRect = new RectangleF(currentUsedPos + _trackerSize.Height / 2 - _trackLineHeight / 2, drawRect.Bottom - (float)currentValuePos, _trackLineHeight, (float)(currentValuePos - _trackerSize.Width / 4));

                JYDrawStyleHelper.DrawAquaPillSingleLayer(e.Graphics, drawFillRect,ForeColor, Orientation.Vertical);
                currentUsedPos += _trackerSize.Height;
                //==========================================================================

                if (_tickStyle == TickStyle.BottomRight || _tickStyle == TickStyle.Both)
                {
                    //==========================================================================
                    // Draw the 2st Tick Line.
                    //==========================================================================
                    currentUsedPos += 1;
                    drawRect = new RectangleF(currentUsedPos, workingRect.Top, _tickHeight, workingRect.Height);
                    drawRect.Inflate(0, -_trackerSize.Width / 2);
                    currentUsedPos += _tickHeight;

                    DrawTickLine(e.Graphics, drawRect, _tickFrequency, _minimum, _maximum, _tickColor, Orientation.Vertical);
                    //==========================================================================
                }

                if (_textTickStyle == TickStyle.BottomRight || _textTickStyle == TickStyle.Both)
                {
                    //==========================================================================
                    // Draw the 2st Text Line.
                    //==========================================================================
                    // Get Height of Text Area
                    drawRect = new RectangleF(currentUsedPos, workingRect.Top, textAreaSize, workingRect.Height);
                    drawRect.Inflate(0, -_trackerSize.Width / 2);
                    currentUsedPos += textAreaSize;

                    DrawTickTextLine(e.Graphics, drawRect, _tickFrequency, _minimum, _maximum, this.ForeColor, this.Font, Orientation.Vertical);
                    //==========================================================================
                }
            

            //==========================================================================
            // Draw the Tracker
            //==========================================================================
            DrawTracker(e.Graphics, _trackerRect);
            //==========================================================================

            // Draws a focus rectangle
            //if(this.Focused && this.BackColor != Color.Transparent)
            if (this.Focused)
                ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(this.ClientRectangle, -2, -2));
            //==========================================================================
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="drawRect"></param>
        private void DrawTrackLine(Graphics g, RectangleF drawRect)
        {
            JYDrawStyleHelper.DrawAquaPillSingleLayer(g, drawRect, _trackLineColor,  Orientation.Vertical);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="trackerRect"></param>
        private void DrawTracker(Graphics g, RectangleF trackerRect)
        {
            JYDrawStyleHelper.DrawAquaPill(g, trackerRect, ForeColor, Orientation.Vertical);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="drawRect"></param>
        /// <param name="tickFrequency"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="foreColor"></param>
        /// <param name="font"></param>
        /// <param name="orientation"></param>
        private void DrawTickTextLine(Graphics g, RectangleF drawRect, double tickFrequency, double minimum, double maximum, Color foreColor, Font font, Orientation orientation)
        {

            //Check input value
            if (maximum == minimum)
                return;

            //Caculate tick number
            int tickCount = (int)((maximum - minimum) / tickFrequency);
            if ((maximum - minimum) % tickFrequency == 0)
                tickCount -= 1;

            //Prepare for drawing Text
            //===============================================================
            StringFormat stringFormat;
            stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.NoWrap;
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            stringFormat.HotkeyPrefix = HotkeyPrefix.Show;

            Brush brush = new SolidBrush(textColor);
            string text;
            double tickFrequencySize;
            //===============================================================

            // Calculate tick's setting
            tickFrequencySize = drawRect.Height * tickFrequency / (maximum - minimum);
            //===============================================================

            // Draw each tick text
            for (int i = 0; i <= tickCount; i++)
            {
                text = Math.Round(_minimum + tickFrequency * i, decimals).ToString();
                g.DrawString(text, font, brush, drawRect.Left + drawRect.Width / 2, (float)(drawRect.Bottom - tickFrequencySize * i), stringFormat);
            }
            // Draw last tick text at Maximum
            text = Math.Round(_maximum, decimals).ToString();
            g.DrawString(text, font, brush, drawRect.Left + drawRect.Width / 2, drawRect.Top, stringFormat);
            //===============================================================
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="drawRect"></param>
        /// <param name="tickFrequency"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="tickColor"></param>
        /// <param name="orientation"></param>
        private void DrawTickLine(Graphics g, RectangleF drawRect, double tickFrequency, double minimum, double maximum, Color tickColor, Orientation orientation)
        {
            //Check input value
            if (maximum == minimum)
                return;

            //Create the Pen for drawing Ticks
            Pen pen = new Pen(tickColor, 1);
            double tickFrequencySize;

            //Caculate tick number
            int tickCount = (int)((maximum - minimum) / tickFrequency);
            if ((maximum - minimum) % tickFrequency == 0)
                tickCount -= 1;


            // Calculate tick's setting
            tickFrequencySize = drawRect.Height * tickFrequency / (maximum - minimum);
            //===============================================================

            // Draw each tick
            for (int i = 0; i <= tickCount; i++)
            {
                g.DrawLine(pen, drawRect.Left, (float)(drawRect.Bottom - tickFrequencySize * i), drawRect.Right, (float)(drawRect.Bottom - tickFrequencySize * i));
            }
            // Draw last tick at Maximum
            g.DrawLine(pen, drawRect.Left, drawRect.Top, drawRect.Right, drawRect.Top);
            //===============================================================

        }


        #endregion

        #region Private Methods


        #endregion
    }
}

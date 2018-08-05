using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace SeeSharpTools.JY.GUI
{
    [Obsolete]
    [ToolboxBitmap(typeof(LEDBright), "IndustryLedBright.IndustryLedBright.PNG")]
    [ToolboxItem(false)]
    [Designer(typeof(LedBrightDesigner))]
    public partial class LEDBright : UserControl
    {
        #region Enum


        /// <summary>
        /// LED类型
        /// </summary>
        public enum LedBrghtStyle
        {
            Circular = 0,
            Rectangular,
        }

        /// <summary>
        /// Interaction Style
        /// </summary>
        public enum InteractionStyle
        {
            SwitchWhenPressed,
            SwitchUntilReleased,
            SwitchWhenReleased,
            Indicator
        }
        #endregion

        #region Fields
        private Color _color;
        private bool _value = false;

        //内部变量
        private bool _blink = false;
        //外部变量进行内容更新
        private bool _blinkOn = false;
        //闪烁所需时间间隔
        private int _blinkInterval = 1000;
        //闪烁时候的颜色
        private Color _blinkColor;

        private LedBrghtStyle _styles = LedBrghtStyle.Circular;
        private Color _reflectionColor = Color.FromArgb(180, 255, 255, 255);
        private Color[] _surroundColor = new Color[] { Color.FromArgb(0, 255, 255, 255) };
        private Timer _timer = new Timer();
        private InteractionStyle _interactionStyle;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the color of the LED light
        /// </summary>
        [DefaultValue(typeof(Color), "153, 255, 54")]
        [Category("Appearance")]
        [Description("Gets or Sets the color of the LED light.")]
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                this.DarkColor = ControlPaint.Dark(_color);
                this.DarkDarkColor = ControlPaint.DarkDark(_color);
                this.Invalidate();  // Redraw the control
            }
        }

        /// <summary>
        /// Dark shade of the LED color used for gradient
        /// </summary>
        [Category("Appearance")]
        public Color DarkColor { get; protected set; }

        /// <summary>
        /// Very dark shade of the LED color used for gradient
        /// </summary>
        [Category("Appearance")]
        public Color DarkDarkColor { get; protected set; }


        /// <summary>
        /// Gets or Sets whether the light is turned on
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or Sets whether the light is turned on.")]
        public bool Value
        {
            get { return _value; }
            set { _value = value; this.Invalidate(); }
        }
        [Category("Appearance")]
        [Description("Gets or Sets the style of the LED light.")]
        public LedBrghtStyle Style
        {
            get
            {
                return _styles;
            }

            set
            {
                _styles = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 闪烁标志位,The Flag of Blinking
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or Sets  whether the light is blinked on.")]
        public bool BlinkOn
        {
            get
            {
                return _blinkOn;
            }
            set
            {
                _blinkOn = value;
                //当BlinkOn为真时，打开闪烁功能。
                if (_blinkOn == true)
                {
                    _timer.Enabled = true;
                }
                else
                {
                    _timer.Enabled = false;
                    _blink = false;
                }
                this.Invalidate();
            }
        }

        /// <summary>
        /// Interval of Blinking(ms),100~5000
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or Sets the Interval when light is blinking.")]
        public int BlinkInterval
        {
            get
            {
                return _blinkInterval;
            }

            set
            {
                if (value <= 5000 & value >= 100)
                {
                    _blinkInterval = value;
                    _timer.Interval = _blinkInterval;
                }

            }
        }

        /// <summary>
        /// 闪烁是的颜色变化
        /// </summary>        
        [Category("Appearance")]
        [Description("Gets or Sets Color of the light is blinking.")]
        public Color BlinkColor
        {
            get
            {
                return _blinkColor;
            }

            set
            {
                _blinkColor = value;
                this.BlinkDarkColor = ControlPaint.Dark(_blinkColor);
                this.BlinkDarkDarkColor = ControlPaint.DarkDark(_blinkColor);
                this.Invalidate();  // Redraw the control
            }
        }

        /// <summary>
        /// Dark shade of the LED Blinkcolor used for gradient
        /// </summary>
        [Category("Appearance")]
        public Color BlinkDarkColor { get; protected set; }

        /// <summary>
        /// Very dark shade of the LED Blinkcolor used for gradient
        /// </summary>
        [Category("Appearance")]
        public Color BlinkDarkDarkColor { get; protected set; }
     
        
        /// <summary>
        /// Set the interaction of led
        /// </summary>
        [Category("Behavior")]
        public InteractionStyle Interacton
        {
            get
            {
                return _interactionStyle;
            }

            set
            {
                _interactionStyle = value;
                if (_interactionStyle == InteractionStyle.Indicator)
                    this.Cursor = Cursors.Arrow;
                else
                    this.Cursor = Cursors.Hand;
            }
        }
        #endregion

        #region Construction / Deconstruction
        public LEDBright()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.Color = System.Drawing.Color.Lime;
            this.BlinkColor = System.Drawing.Color.Lime;
            this.Size = new Size(60, 60);
            this.Value = false;
            _interactionStyle = InteractionStyle.Indicator;
            //设置与初始相等
            _timer.Interval = _blinkInterval;
            _timer.Tick += delegate (object o, EventArgs args)
            {
                LEDBlink();
            };
        }


        #endregion

        #region Private Methods
        /// <summary>
        /// Renders the control to an image
        /// </summary>
        private void drawControl(Graphics g, bool on, bool IsBlink)
        {
            Color lightColor;
            Color darkColor;
            if (IsBlink == true)
            {
                lightColor = (on) ? this.BlinkColor : Color.FromArgb(150, this.BlinkDarkColor);
                darkColor = (on) ? this.BlinkDarkColor : this.BlinkDarkDarkColor;
            }
            else
            {
                lightColor = (on) ? this.Color : Color.FromArgb(150, this.DarkColor);
                darkColor = (on) ? this.DarkColor : this.DarkDarkColor;
            }
            // Is the bulb on or off


            // Calculate the dimensions of the bulb
            int width = this.Width - (this.Padding.Left + this.Padding.Right);
            int height = this.Height - (this.Padding.Top + this.Padding.Bottom);
            // Diameter is the lesser of width and height
            int diameter = Math.Min(width, height);
            // Subtract 1 pixel so ellipse doesn't get cut off
            diameter = Math.Max(diameter - 1, 1);

            if (_styles == LedBrghtStyle.Circular)
            {
                // Draw the background ellipse
                var rectangle = new Rectangle(this.Padding.Left, this.Padding.Top, diameter, diameter);
                g.FillEllipse(new SolidBrush(darkColor), rectangle);

                // Draw the glow gradient
                var path = new GraphicsPath();
                path.AddEllipse(rectangle);
                var pathBrush = new PathGradientBrush(path);
                pathBrush.CenterColor = lightColor;
                pathBrush.SurroundColors = new Color[] { Color.FromArgb(0, lightColor) };
                g.FillEllipse(pathBrush, rectangle);

                // Draw the white reflection gradient
                var offset = Convert.ToInt32(diameter * .15F);
                var diameter1 = Convert.ToInt32(rectangle.Width * .8F);
                var whiteRect = new Rectangle(rectangle.X - offset, rectangle.Y - offset, diameter1, diameter1);
                var path1 = new GraphicsPath();
                path1.AddEllipse(whiteRect);
                var pathBrush1 = new PathGradientBrush(path);
                pathBrush1.CenterColor = _reflectionColor;
                pathBrush1.SurroundColors = _surroundColor;
                g.FillEllipse(pathBrush1, whiteRect);

                // Draw the border
                g.SetClip(this.ClientRectangle);
                if (this.Value) g.DrawEllipse(new Pen(Color.FromArgb(85, Color.Black), 1F), rectangle);
            }
            if (_styles == LedBrghtStyle.Rectangular)
            {
                // Draw the background ellipse
                var rectangle = new Rectangle(this.Padding.Left, this.Padding.Top, diameter, diameter);
                g.FillRectangle(new SolidBrush(darkColor), rectangle);

                // Draw the glow gradient
                var path = new GraphicsPath();
                path.AddRectangle(rectangle);
                var pathBrush = new PathGradientBrush(path);
                pathBrush.CenterColor = lightColor;
                pathBrush.SurroundColors = new Color[] { Color.FromArgb(0, lightColor) };
                g.FillRectangle(pathBrush, rectangle);
            }


        }

        private void LEDBlink()
        {
            _blink = !_blink;
            this.Invalidate();
        }

        /// <summary>
        /// Handles the Paint event for this UserControl
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create an offscreen graphics object for double buffering
            Bitmap offScreenBmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            using (System.Drawing.Graphics g = Graphics.FromImage(offScreenBmp))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                //当属性值为On的时候
                if (BlinkOn == true)
                {
                    //按照_blink的状态进行控件画图
                    drawControl(g, _blink, true);
                }
                else
                {
                    // Draw the control
                    drawControl(g, this.Value, false);
                }

                // Draw the image to the screen
                e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {

            switch (_interactionStyle)
            {
                case InteractionStyle.SwitchWhenPressed:
                    this.Value = !this.Value;
                    break;
                case InteractionStyle.SwitchUntilReleased:
                    this.Value = !this.Value;
                    break;
                case InteractionStyle.SwitchWhenReleased:
                    break;
                case InteractionStyle.Indicator:
                    break;
                default:
                    break;
            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            switch (_interactionStyle)
            {
                case InteractionStyle.SwitchWhenPressed:
                    break;
                case InteractionStyle.SwitchUntilReleased:
                    this.Value = !this.Value;
                    break;
                case InteractionStyle.SwitchWhenReleased:
                    this.Value = !this.Value;
                    break;
                case InteractionStyle.Indicator:
                    break;
                default:
                    break;
            }

        }

        #endregion

        #region Public Methods
        #endregion

        #region Threading

        #endregion

    }
}

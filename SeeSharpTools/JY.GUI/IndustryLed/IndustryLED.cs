using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// 工业LED灯控件，具有圆形和矩形控件
    /// </summary>
    [ToolboxBitmap(typeof(LED), "IndustryLed.IndustryLED.bmp")]
    [Designer(typeof(LedDesigner))]
    public partial class LED : UserControl
    {
        #region Enum
        /// <summary>
        /// LED类型
        /// </summary>
        public enum LedStyle
        {
            Circular = 0,
            Rectangular,
            Circular3D,
            Rectangular3D,
            CircularBright,
            RectangularBright
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
        private Color _oncolor;
        private Color _offcolor;
        //内部变量
        private bool _blink = false;
        //外部变量进行内容更新
        private bool _blinkOn = false;
        //闪烁所需时间间隔
        private int _blinkInterval = 1000;
        //闪烁时候的颜色
        private Color _blinkColor;

        private static readonly Color surroundColor = Color.FromArgb(40, Color.Black);


        private bool _value = true;
        private Timer _timer = new Timer();
        private LedStyle _styles;
        LinearGradientBrush _brOn;
        LinearGradientBrush _brOff;

        InteractionStyle _interactionStyle = InteractionStyle.Indicator;
        
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the color of the LED light turn on
        /// </summary>
        [DefaultValue(typeof(Color), "153, 255, 54")]
        [Category("Appearance")]
        [Description("Gets or Sets the color of the LED light turn on.")]
        public Color OnColor
        {
            get { return _oncolor; }
            set
            {
                _oncolor = value;
                _onDarkColor = ControlPaint.Dark(_oncolor);
           //     _onDarkDarkColor = ControlPaint.DarkDark(_oncolor);
                this.Invalidate();  // Redraw the control
            }
        }
        /// <summary>
        /// Gets or Sets the color of the LED light turn off
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or Sets the color of the LED light turn off.")]
        public Color OffColor
        {
            get
            {
                return _offcolor;
            }

            set
            {
                _offcolor = value;
                _offDarkColor = ControlPaint.Dark(_offcolor);
            //    _offDarkDarkColor = ControlPaint.DarkDark(_offcolor);
                this.Invalidate();  // Redraw the control
            }
        }

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

        /// <summary>
        /// Gets or Sets styles of the light is turned on
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or Sets the style of the LED light.")]
        public LedStyle Style
        {
            get
            {
                return _styles;
            }

            set
            {
                _styles = value;
                this.Invalidate();
                //this.Refresh();
            }
        }
        /// <summary>
        /// Gets or Sets styles of the light is blinking
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
                _blinkDarkColor = ControlPaint.Dark(_blinkColor);
            //    _blinkarkDarkColor = ControlPaint.Dark(_blinkDarkColor);
                this.Invalidate();
            }
        }
        /// <summary>
        /// Gets or Sets state of the light blink
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
        /// <summary>
        /// 工业LED灯的内容
        /// </summary>
        public LED()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            _interactionStyle = InteractionStyle.Indicator;
            this.Cursor = Cursors.Arrow;
            this.Size = new Size(60, 60);
            this.Value = false;

            this.OnColor = Color.Lime;
            this.OffColor = Color.Gray;
            this.BlinkColor = Color.Lime;


        _timer.Interval = _blinkInterval;
            _timer.Tick += delegate (object o, EventArgs args)
                {
                    LEDBlink();
                };
        }


        #endregion

        #region Private Methods
        private void LEDBlink()
        {
            _blink = !_blink;
            this.Invalidate();
        }

        /// <summary>
        /// Handles the Paint event for this UserControl
        /// 重新绘画LED,每次刷新时候调用
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create an offscreen graphics object for double buffering
            Bitmap offScreenBmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            using (System.Drawing.Graphics g = Graphics.FromImage(offScreenBmp))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                // Draw the control
                switch (_styles)
                {
                    case LedStyle.Circular:
                        if (BlinkOn == true)
                        {
                            drawControl(g, _blink, BlinkOn);
                        }
                        else
                        {
                            drawControl(g, this.Value, BlinkOn);
                        }
                        // Draw the image to the screen
                        e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
                        break;
                    case LedStyle.Rectangular:
                        if (BlinkOn == true)
                        {
                            drawControlRectangular(g, _blink, BlinkOn);
                        }
                        else
                        {
                            drawControlRectangular(g, this.Value, BlinkOn);
                        }
                        // Draw the image to the screen
                        e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
                        break;
                    case LedStyle.Circular3D:
                        if (BlinkOn == true)
                        {
                            drawControl3D(g, _blink, BlinkOn);
                        }
                        else
                        {
                            drawControl3D(g, this.Value, BlinkOn);
                        }
                        // Draw the image to the screen
                        e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
                        break;
                    case LedStyle.Rectangular3D:
                        if (BlinkOn == true)
                        {
                            drawControlRect3D(g, _blink, BlinkOn);
                        }
                        else
                        {
                            drawControlRect3D(g, this.Value, BlinkOn);
                        }
                        // Draw the image to the screen
                        e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
                        break;
                    case LedStyle.CircularBright:
                    case LedStyle.RectangularBright:
                        if (BlinkOn == true)
                        {
                            drawLedBrightControl(g, _blink, BlinkOn);
                        }
                        else
                        {
                            drawLedBrightControl(g, this.Value, BlinkOn);
                        }
                        // Draw the image to the screen
                        e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
                        break;
                    default:
                        break;
                }

            }
        }

        /// <summary>
        /// Renders the control to an image
        /// </summary>
        private void drawControl(Graphics g, bool on, bool IsBlink)
        {
            Color cDarkOff;
            Color cDarkOn ;
            // Calculate the dimensions of the bulb
            int width = this.Width - (this.Padding.Left + this.Padding.Right);
            int height = this.Height - (this.Padding.Top + this.Padding.Bottom);
            int diameter = Math.Min(width, height);
            diameter = Math.Max(diameter - 1, 1);
            // Draw the background ellipse
            var rectangle = new Rectangle(this.Padding.Left, this.Padding.Top, width - 1, height - 1);

            cDarkOff = StepColor(this.OffColor, 20);

            if (IsBlink == true)
            {
                cDarkOn = StepColor(this.BlinkColor, 60);
                _brOff = new LinearGradientBrush(rectangle, this.OffColor, cDarkOff, 45);
                _brOn = new LinearGradientBrush(rectangle, this.BlinkColor, cDarkOn, 45);
            }
            else
            {
                cDarkOn = StepColor(this.OnColor, 60);
                _brOff = new LinearGradientBrush(rectangle, this.OffColor, cDarkOff, 45);
                _brOn = new LinearGradientBrush(rectangle, this.OnColor, cDarkOn, 45);
            }
            if (on == true)
                g.FillEllipse(_brOn, rectangle);
            else
                g.FillEllipse(_brOff, rectangle);

        }

        /// <summary>
        /// Renders the control to an image
        /// </summary>
        private void drawControlRectangular(Graphics g, bool on,bool IsBlink)
        {
            Color cDarkOff;
            Color cDarkOn;
            // Calculate the dimensions of the bulb
            int width = this.Width - (this.Padding.Left + this.Padding.Right);
            int height = this.Height - (this.Padding.Top + this.Padding.Bottom);
            int diameter = Math.Min(width, height);
            diameter = Math.Max(diameter - 1, 1);
            var rectangle = new Rectangle(this.Padding.Left, this.Padding.Top, width - 1, height - 1);

            cDarkOff = StepColor(this.OffColor, 20);

            if (IsBlink == true)
            {
                cDarkOn = StepColor(this.BlinkColor, 60);
                _brOff = new LinearGradientBrush(rectangle, this.OffColor, cDarkOff, 45);
                _brOn = new LinearGradientBrush(rectangle, this.BlinkColor, cDarkOn, 45);
            }
            else
            {
                cDarkOn = StepColor(this.OnColor, 60);
                _brOff = new LinearGradientBrush(rectangle, this.OffColor, cDarkOff, 45);
                _brOn = new LinearGradientBrush(rectangle, this.OnColor, cDarkOn, 45);
            }


            // Draw the background ellipse


            if (on == true)
                g.FillRectangle(_brOn, rectangle);
            else
                g.FillRectangle(_brOff, rectangle);
        }

        #region ControlRound3D
        private static GraphicsPath smethod_5(Rectangle rectangle_0)
        {
            Rectangle rectangle = new Rectangle(rectangle_0.X, rectangle_0.Y, rectangle_0.Width - 1, rectangle_0.Height - 1);
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(rectangle);
            return graphicsPath;
        }

        private void drawControl3D(Graphics g, bool on, bool IsBlink)
        {
            Color cDarkOff;
            Color cDarkOn;

            Point p = new Point(this.Width - this.Width / 8, this.Height - this.Height / 8);
            Point p2 = new Point(this.Width / 4, this.Height / 4);
            Color[] surroundColors = new Color[]
            {
                                       surroundColor
            };
            using (GraphicsPath graphicsPath = smethod_5(new Rectangle(new Point(this.Padding.Left, this.Padding.Top), new Size(this.Width, this.Height))))
            {
                using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
                {
                    using (GraphicsPath graphicsPath2 = new GraphicsPath())
                    {                        
                        graphicsPath2.AddEllipse(new Rectangle(this.Padding.Left + this.Width / 12, this.Padding.Top + this.Height / 12, this.Width - 2 * this.Width / 12, this.Height - 2 * this.Height / 12));
                        graphicsPath.AddPath(graphicsPath2, true);
                        pathGradientBrush.CenterColor = Color.White;
                        pathGradientBrush.SurroundColors = surroundColors;
                        pathGradientBrush.CenterPoint = p;
                        g.FillPath(pathGradientBrush, graphicsPath);

                        cDarkOff = _offcolor;
                        //判断当前颜色是什么？如果打开Blink则是blinkColor
                        //
                        if (IsBlink == true)
                        {
                            cDarkOn = _blinkColor;
            
                        }
                        else
                        {
                            cDarkOn = _oncolor;
                        }
                        //判断blink On或者 开始按钮
                        if (on == true)
                        {
                            Draw3DCircle(g, graphicsPath2, cDarkOn, p2);

                        }
                        else
                        {
                            Draw3DCircle(g, graphicsPath2, _offcolor, p2);

                        }
                    }
                }
            }
        }

        private void Draw3DCircle(Graphics graphics, GraphicsPath circlePath, Color circleColor, PointF lightLocation)
        {
            HSLColor hSLColor = new HSLColor(circleColor);
            Color shadedColor = hSLColor.GetShadedColor();
            using (SolidBrush solidBrush = new SolidBrush(circleColor))
            {
                using (PathGradientBrush pathGradientBrush = new PathGradientBrush(circlePath))
                {
                    pathGradientBrush.CenterColor = Color.FromArgb(200, Color.White); ;
                    pathGradientBrush.SurroundColors = new Color[]
                    {
                        shadedColor
                    };
                    pathGradientBrush.CenterPoint = lightLocation;
                    graphics.FillPath(solidBrush, circlePath);
                    graphics.FillPath(pathGradientBrush, circlePath);
                }
            }
        }
        #endregion

        #region ControlRect3D             
        private void drawControlRect3D(Graphics g, bool on, bool IsBlink)
        {
            Color cDarkOff;
            Color cDarkOn;

            Rectangle bounds = new Rectangle(new Point(this.Padding.Left, this.Padding.Top), new Size(this.Width, this.Height));
            Rectangle rectangle_ = new Rectangle(bounds.X - bounds.Width / 2, bounds.Y - bounds.Width / 2, bounds.Width - 1 + bounds.Width, bounds.Height - 1 + bounds.Width);
            Point p = new Point(bounds.Right - bounds.Width / 12, bounds.Bottom - bounds.Height / 12);
            Color[] surroundColors = new Color[]
            {
                   surroundColor
            };
            using (GraphicsPath graphicsPath = CreateLedPath(bounds))
            {
                using (GraphicsPath graphicsPath2 = CreateBezelPath(bounds))
                {
                    using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath2))
                    {
                        graphicsPath2.AddPath(graphicsPath, true);
                        pathGradientBrush.CenterColor = Color.White;
                        pathGradientBrush.SurroundColors = surroundColors;
                        pathGradientBrush.CenterPoint = p;
                        g.FillPath(pathGradientBrush, graphicsPath2);

                        cDarkOff = _offcolor;
                        //判断当前颜色是什么？如果打开Blink则是blinkColor
                        if (IsBlink == true)
                        {
                            cDarkOn = _blinkColor;

                        }
                        else
                        {
                            cDarkOn = _oncolor;
                        }
                        if (on == true)
                        {
                            DrawLinearGradient(g, graphicsPath, cDarkOn, new Point(bounds.Left, bounds.Top), new Point(bounds.Right, bounds.Bottom));

                        }
                        else
                        {
                            DrawLinearGradient(g, graphicsPath, cDarkOff, new Point(bounds.Left, bounds.Top), new Point(bounds.Right, bounds.Bottom));
                        }
                    }
                }
            }
        }

        public static GraphicsPath CreateLedPath(Rectangle bounds)
        {
            Rectangle rectangle = new Rectangle(bounds.X + 1 + bounds.Width / 20, bounds.Y + 1 + bounds.Height / 20, bounds.Width - 2 - 2 * bounds.Width / 20, bounds.Height - 2 - 2 * bounds.Height / 20);
            Size size = new Size(bounds.Width / 8, bounds.Height / 8);
            return RoundedRect(rectangle, size);
        }

        public static GraphicsPath CreateBezelPath(Rectangle bounds)
        {
            Rectangle rectangle = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2);

            Size size = new Size(bounds.Width / 8, bounds.Height / 8);
            return RoundedRect(rectangle, size);
        }

        public static GraphicsPath RoundedRect(Rectangle rect, Size size)
        {
            if (size.Width < 1)
            {
                size.Width = 1;
            }
            if (size.Height < 1)
            {
                size.Height = 1;
            }
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddLine(rect.Left + size.Width / 2, rect.Top, rect.Right - size.Width / 2, rect.Top);
            graphicsPath.AddArc(rect.Right - size.Width, rect.Top, size.Width, size.Height, 270f, 90f);
            graphicsPath.AddLine(rect.Right, rect.Top + size.Height / 2, rect.Right, rect.Bottom - size.Height / 2);
            graphicsPath.AddArc(rect.Right - size.Width, rect.Bottom - size.Height, size.Width, size.Height, 0f, 90f);
            graphicsPath.AddLine(rect.Right - size.Width / 2, rect.Bottom, rect.Left + size.Width / 2, rect.Bottom);
            graphicsPath.AddArc(rect.Left, rect.Bottom - size.Height, size.Width, size.Height, 90f, 90f);
            graphicsPath.AddLine(rect.Left, rect.Bottom - size.Height / 2, rect.Left, rect.Top + size.Height / 2);
            graphicsPath.AddArc(rect.Left, rect.Top, size.Width, size.Height, 180f, 90f);
            return graphicsPath;
        }

        public static void DrawLinearGradient(Graphics graphics, GraphicsPath path, Color color, Point point1, Point point2)
        {
            HSLColor hSLColor = new HSLColor(color);
            Color shadedColor = hSLColor.GetShadedColor();
            using (SolidBrush solidBrush = new SolidBrush(color))
            {
                using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(point1, point2, Color.White, shadedColor))
                {
                    graphics.FillPath(solidBrush, path);
                    graphics.FillPath(linearGradientBrush, path);
                }
            }
        }
        #endregion


        #region ControlLedBright
        /// <summary>
        /// Renders the control to an image
        /// </summary>
        /// 
        private Color _blinkDarkColor;
        //private Color _blinkarkDarkColor;
        private Color _onDarkColor;
        private Color _offDarkColor;
        //private Color _onDarkDarkColor;
        //private Color _offDarkDarkColor;
        private Color _reflectionColor = Color.FromArgb(180, 255, 255, 255);
        private Color[] _surroundColor = new Color[] { Color.FromArgb(0, 255, 255, 255) };
        private void drawLedBrightControl(Graphics g, bool on, bool IsBlink)
        {
            Color lightColor;
            Color darkColor;
            if (IsBlink == true)
            {
                lightColor = (on) ? this.BlinkColor : _offcolor;
                darkColor = (on) ? _blinkDarkColor : _offDarkColor;
            }
            else
            {
                // lightColor = (on) ? _oncolor : Color.FromArgb(150, _offDarkColor);
                lightColor = (on) ? _oncolor :_offcolor;
                darkColor = (on) ? _onDarkColor : _offDarkColor;
            }
            // Is the bulb on or off


            // Calculate the dimensions of the bulb
            int width = this.Width - (this.Padding.Left + this.Padding.Right);
            int height = this.Height - (this.Padding.Top + this.Padding.Bottom);
            // Diameter is the lesser of width and height
            int diameter = Math.Min(width, height);
            // Subtract 1 pixel so ellipse doesn't get cut off
            diameter = Math.Max(diameter - 1, 1);

            if (_styles ==  LedStyle.CircularBright)
            {
                // Draw the background ellipse
                var rectangle = new Rectangle(this.Padding.Left + 1, this.Padding.Top + 1, this.Width - 2, this.Height - 2);
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
                var diameter1 = Convert.ToInt32(diameter * .8F);
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
            if (_styles ==  LedStyle.RectangularBright)
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

        #endregion

        private Color StepColor(Color clr, int alpha)
        {
            if (alpha == 100)
                return clr;

            byte a = clr.A;
            byte r = clr.R;
            byte g = clr.G;
            byte b = clr.B;
            float bg = 0;

            int _alpha = Math.Min(alpha, 200);
            _alpha = Math.Max(alpha, 0);
            double ialpha = ((double)(_alpha - 100.0)) / 100.0;

            if (ialpha > 100)
            {
                // blend with white
                bg = 255.0F;
                ialpha = 1.0F - ialpha;  // 0 = transparent fg; 1 = opaque fg
            }
            else
            {
                // blend with black
                bg = 0.0F;
                ialpha = 1.0F + ialpha;  // 0 = transparent fg; 1 = opaque fg
            }

            r = (byte)(BlendColour(r, bg, ialpha));
            g = (byte)(BlendColour(g, bg, ialpha));
            b = (byte)(BlendColour(b, bg, ialpha));

            return Color.FromArgb(a, r, g, b);
        }

        private double BlendColour(double fg, double bg, double alpha)
        {
            double result = bg + (alpha * (fg - bg));
            if (result < 0.0)
                result = 0.0;
            if (result > 255)
                result = 255;
            return result;
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

        #region Threading

        #endregion
    }
}

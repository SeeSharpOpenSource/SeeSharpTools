using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// A termometer control
    /// </summary>
    [ToolboxBitmap(typeof(PressureGauge), "PressureGauge.PressureGauge.bmp")]
    [Designer(typeof(PressureGaugeDesigner))]
    public class PressureGauge : UserControl
    {
        #region -- Members --
        private double max;
        private double min;
        private int startAngle = startAngleDefault;


        private RectangleF shadowRect, backgroundRect, numberRect, bar1Rect, bar2Rect, bar3Rect;
        private RectangleF arrow1Rect, arrow2Rect;
        private double numberSpacing = defaultSpacing;
        private Color backColor = Color.Peru;
        private int borderWidth;
        private Color colorArrow;
        private Color barColor = Color.Black;
        private bool clockWise = true;
        private int decimals;
        private int barsBetweenNumbers = defaultBarsBetweenNumbers;
        private Brush textureBrush;
        private int numberofDevision =10;
        private float glossinessAlpha = 60;


        private String textUnit;
        private String textDescription = "";
        private double value;

        private float interval = defaultInterval;
        //Constants
        private const int defaultInterval = 10;


        private const String textUnitDefault = "";
        private const String textDescriptionDefault = "";
        //Constants
        private const int maxDefault = 100;
        private const int minDefault = 0;
        private const int startAngleDefault = 225;
        private const int numberofDevisionDefault = 10;

        private const int defaultWidth = 215;
        private const int defaultHeight = 215;
        private const int defaultFontSize = 11;
        private const int defaultMax = 100;
        private const int defaultMin = 0;
        private const int defaultLightingAngle = 90;
        private const int defaultBorderWidth = 6;
        //保留小数点后位数
        private const int defaultDecimals = 3;
        private const double defaultSpacing = 27;
        private const int barOuterMargin = 12;
        private const int barInnerMargin = 4;
        private const int defaultBarHeight = 5;
        private const int defaultBarWidth = 2;
        private const int defaultBarsBetweenNumbers = 5;
        private const int defaultInnerShadowWidth = 2;
        private const int defaultOuterShadowWidth = 2;
        private const int numberMargin = barOuterMargin + barInnerMargin + defaultBorderWidth + defaultBarHeight;

        #endregion

        #region -- Properties --

        /// <summary>
        /// Gets or sets the starting angle (degrees)
        /// </summary>
        /// <value>The starting angle</value>
        [Browsable(true)]
        [Description("Gets or sets the layout start (degrees).")]
        [Category("Layout")]
        [DefaultValue(startAngleDefault)]
        private int StartAngle
        {
            get { return startAngle; }
            set
            {
                if (value > 360)
                    value = 360;
                startAngle = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        /// <value>The max value</value>
        [Browsable(true)]
        [Description("Gets or sets the max value.")]
        [Category("Layout")]
        [DefaultValue(maxDefault)]
        public double Max
        {
            get { return max; }
            set
            {
                max = (max < min) ? min : value;

                if (max <= Value)
                {
                    Value = max;
                }

                Interval =(float)(max - min) / numberofDevision;
                NumberSpacing = (270 /(double) numberofDevision);


                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        /// <value>The min.</value>
        [Browsable(true)]
        [Description("Gets or sets the min value.")]
        [Category("Layout")]
        [DefaultValue(minDefault)]
        public double Min
        {
            get { return min; }
            set
            {
                min = (min > max) ? max : value;

                if (min >= Value)
                {
                    Value = min;
                }

                Interval = (float)(max - min) / numberofDevision;
                NumberSpacing = (float)(270 / (double)numberofDevision);

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the decimals used for the numbers
        /// </summary>
        /// <value>The decimals.</value>
        [Browsable(true)]
        [Description("Gets or sets the decimals.")]
        [Category("Appearance")]
        [DefaultValue(defaultDecimals)]
        private int Decimals
        {
            get { return decimals; }
            set { decimals = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the space between numbers in degrees.
        /// </summary>
        /// <value>The number spacing.</value>
        [Browsable(true)]
        [DefaultValue(defaultSpacing)]
        [Description("Gets or sets the space between numbers in degrees.")]
        [Category("Layout")]
        [Localizable(true)]
        private double NumberSpacing
        {
            get { return numberSpacing; }
            set
            {
                if (numberSpacing <= 0)
                    Debug.Assert(false, "Number interval is less than 0");
                else
                {
                    numberSpacing = value; Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the background color for the control, this property is not relevant for this control.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor"></see> property.</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [Browsable(true)]
        [Description("Set the background color of the control.")]
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Peru")]
        public new Color BackColor
        {
            get { return backColor; }
            set { backColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        [Browsable(true)]
        [Description("Gets or sets the width of the border.")]
        [Category("Appearance")]
        [DefaultValue(defaultBorderWidth)]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                if ((value < 0) || value > 10)
                {
                    Debug.Assert(false, "Value must be between 0 and 10");
                    value = defaultBorderWidth;
                }
                borderWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the arrow.
        /// </summary>
        /// <value>The color of the arrow.</value>
        [Browsable(true)]
        [Description("Gets or sets the color of the arrow.")]
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Black")]
        [Localizable(true)]
        private Color ArrowColor
        {
            get { return colorArrow; }
            set { colorArrow = value; Invalidate(); }
        }

        /// <summary>
        /// Set to true if the layout should be clockwise.
        /// </summary>
        /// <value>true if Clockwise</value>
        [Browsable(true)]
        [Description("Set to true if the layout should be clockwise.")]
        [Category("Layout")]
        [DefaultValue(true)]
        [Localizable(true)]
        private bool ClockWise
        {
            get { return clockWise; }
            set { clockWise = value; Invalidate(); }
        }

        /// <summary>
        /// Number of bars between the numbers.
        /// </summary>
        /// <value>true if Clockwise</value>
        [Browsable(true)]
        [Description("Number of bars between the numbers.1~10")]
        [Category("Layout")]
        [DefaultValue(defaultBarsBetweenNumbers)]
        public int NumberOfSubDivisons
        {
            get { return barsBetweenNumbers; }
            set
            {
                if (value > 1 && value <=10)
                    barsBetweenNumbers = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Number of bars between the numbers.
        /// </summary>
        /// <value>true if Clockwise</value>
        [Browsable(true)]
        [Description("Number of Devision.1~25")]
        [Category("Layout")]
        [DefaultValue(numberofDevisionDefault)]
        public int NumberofDevisions
        {
            get
            {
                return numberofDevision;
            }

            set
            {
                if (value >= 1 && value <= 25)
                {
                    numberofDevision = value;

                    if (numberofDevision <= 0)
                    {
                        numberofDevision = numberofDevisionDefault;
                    }


                    Interval =(float) (max - min) / numberofDevision;
                    NumberSpacing = (270 / (double)numberofDevision);

                   
                }
                Invalidate();



            }
        }

        /// <summary>
        /// Glossiness strength. Range: 0-100
        /// </summary>
        [Browsable(true)]
        [Category("Layout")]
        [Description("Glossiness strength. Range: 0-100")]
        public float Glossiness
        {
            get
            {
                return (glossinessAlpha * 100) / 220;
            }
            set
            {
                float val = value;
                if (val > 100)
                    value = 100;
                if (val < 0)
                    value = 0;
                glossinessAlpha = (value * 220) / 100;
                this.Refresh();
            }
        }

        /// <summary>
        /// The intervals between Min and Max.
        /// </summary>
        /// <value>The min.</value>
        [Browsable(true)]
        [Description("The intervals between Min and Max.")]
        [Category("Layout")]
        [DefaultValue(defaultInterval)]
        private float Interval
        {
            get { return interval; }
            set
            {
                interval = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description</value>
        [Browsable(true)]
        [Description("Gets or sets the description.")]
        [Category("Appearance")]
        [DefaultValue(textDescriptionDefault)]
        [Localizable(true)]
        public string DescriptionText
        {
            get { return textDescription; }
            set { textDescription = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the text unit.
        /// </summary>
        /// <value>The text unit.</value>
        [Browsable(true)]
        [Description("Gets or sets the description.")]
        [Category("Appearance")]
        [DefaultValue(textUnitDefault)]
        [Localizable(true)]
        public string UnitText
        {
            get { return textUnit; }
            set { textUnit = value; Invalidate(); }
        }





        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Browsable(true)]
        [Description("Gets or sets the value.")]
        [Category("Layout")]
        [DefaultValue(0)]
        public double Value
        {
            get { return value; }
            set
            {
                //数值范围应该在最大最小值之间
                if (value > max)
                {
                    this.value = max;
                }
                if (value < min)
                {
                    this.value = min;
                }

                this.value = value;

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control. Not relevant for this control
        /// </summary>
        /// <value></value>
        /// <returns>The text associated with this control.</returns>
        [Browsable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }


        #endregion

        #region -- Constructor --
        /// <summary>
        /// Initializes a new instance of the <see cref="PressureGauge"/> class.
        /// </summary>
        /// <remarks></remarks>
        public PressureGauge()
        {
            //Styles
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.ContainerControl, false);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);


            InitializeComponent();
            Max = defaultMax;
            Min = defaultMin;
            ClockWise = true;
            NumberOfSubDivisons = defaultBarsBetweenNumbers;
            ArrowColor = Color.Black;
            BorderWidth = defaultBorderWidth;
            Decimals = defaultDecimals;
            numberSpacing = defaultSpacing;
            Width = defaultWidth;
            Height = defaultHeight;
            UnitText = "°C";
            //   this.Size = new System.Drawing.Size(150, 150);
            this.Size = new Size(215, 215);
            this.Font = new System.Drawing.Font("Calibri", 8F);
            this.ForeColor = System.Drawing.Color.Black;
            this.BorderWidth = 2;
            Font = new Font("Calibri", defaultFontSize, GraphicsUnit.Point);
            CalcRectangles();
            Resize += new EventHandler(Termometer_Resize);
            base.BackColor = Color.Transparent;

            this.BackColor = Color.Silver;
            // 
            //后续代码补充，为防止超出界限
            this.StartAngle = 225;
            this.numberSpacing = 27;
            numberofDevision = numberofDevisionDefault;
            textureBrush = new TextureBrush(SeeSharpTools.JY.GUI.Properties.Resources.Reflection);
            this.Glossiness = 45;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        #endregion

        #region -- EventHandlers --

        private void Termometer_Resize(object sender, EventArgs e)
        {
            CalcRectangles();
        }

        #endregion

        #region -- Protected Overrides --

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Set smoothingmode to AntiAlias
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // Shadow
            PaintShadow(e.Graphics);
            // Background
            PaintBackground(e.Graphics);
            // Border
            PaintBorder(e.Graphics);
            // Inner shadow
            PaintInnerShadow(e.Graphics);
            // Bars
            PaintBars(e.Graphics);
            // Numbers
            PaintNumbers(e.Graphics);
            // Paint the text(s)
            PaintText(e.Graphics);
            // Paint the Arrows
            PaintArrows(e.Graphics);
            // Reflex
            PaintReflex(e.Graphics);
            // Reset smoothingmode
            e.Graphics.SmoothingMode = SmoothingMode.Default;
        }

        #endregion

        #region -- Protected Methods --

        #region PaintShadow
        /// <summary>
        /// Paints the outer shadow.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintShadow(Graphics g)
        {
            if (shadowRect.IsEmpty) return; //break if nothing to draw
            using (Pen p = new Pen(Color.FromArgb(60, Color.Black), defaultOuterShadowWidth))
            {
                g.DrawEllipse(p, shadowRect);
            }
        }
        #endregion

        #region PaintBackground
        /// <summary>
        /// Paints the background.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintBackground(Graphics g)
        {
            if (backgroundRect.IsEmpty) return; //break if nothing to draw
            using (Brush b = new LinearGradientBrush(backgroundRect,
                Color.FromArgb(240, 240, 240), backColor, defaultLightingAngle)) //From gray to BackColor
            {
                g.FillEllipse(b, backgroundRect);
            }
        }
        #endregion

        #region PaintBorder
        /// <summary>
        /// Paints the border.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintBorder(Graphics g)
        {
            // First draw a image to reflect
            RectangleF r = backgroundRect;
            r.Inflate(-BorderWidth / 2, -BorderWidth / 2);
            if (r.IsEmpty) return; //break if nothing to draw
            using (Pen texturePen = new Pen(textureBrush, BorderWidth))
            {
                g.DrawEllipse(texturePen, r);
            }

            // Gradient overlay
            using (Brush b = new LinearGradientBrush(backgroundRect, Color.White,
                Color.FromArgb(200, Color.White), defaultLightingAngle))
            {
                using (Pen p = new Pen(b, BorderWidth))
                {
                    g.DrawArc(p, r, defaultLightingAngle - 90, -180); // Upper half of ellipse
                }
            }
        }
        #endregion

        #region PaintInnerShadow
        /// <summary>
        /// Paints the inner shadow.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintInnerShadow(Graphics g)
        {
            RectangleF r = backgroundRect;
            // Adjust for pen and border width
            r.Inflate(-(BorderWidth + defaultInnerShadowWidth / 2),
                -(BorderWidth + defaultInnerShadowWidth / 2));
            if (r.IsEmpty) return; // Break if nothing to draw
            Brush b = new LinearGradientBrush(backgroundRect,
                Color.FromArgb(60, Color.Black),
                Color.FromArgb(30, Color.White), defaultLightingAngle);
            using (Pen p = new Pen(b, defaultInnerShadowWidth))
            {
                g.DrawEllipse(p, r);
            }
            b.Dispose();
        }
        #endregion

        #region PaintNumbers
        /// <summary>
        /// Paints the numbers.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintNumbers(Graphics g)
        {
            double tmpAngle = StartAngle;
            for (double d = Min; d <= Max+ Interval/2/(max-min); d += Interval)
            {
                String text = Math.Round(d, Decimals).ToString();
                PointF p = CalcTextPosition(tmpAngle, MeasureText(g, text, Font, (int)numberRect.Width));
                if (ClockWise)
                    tmpAngle -= numberSpacing;
                else
                    tmpAngle += numberSpacing;
                using (Brush b = new SolidBrush(ForeColor))
                {
                    g.DrawString(text, Font, b, p);
                }
            }
        }
        #endregion

        #region PaintBars
        /// <summary>
        /// Paints the bars.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintBars(Graphics g)
        {
            double tmpAngle = StartAngle;
            for (double d = Min; d < Max+ Interval / 2/(max - min); d += Interval)
            {
                PaintBar(g, bar2Rect, bar3Rect, tmpAngle, defaultBarWidth, barColor);
                if (ClockWise)
                    tmpAngle -= numberSpacing;
                else
                    tmpAngle += numberSpacing;
            }
            if (ClockWise)
            {
                //原始代码 d<= StartAngle，为防止由于位数不统一造成的少计算，特别多了用<进行弥补
                for (double d = tmpAngle + numberSpacing; d < StartAngle+ (float)numberSpacing / (NumberOfSubDivisons + 1); d += (float)numberSpacing / NumberOfSubDivisons)
                    PaintBar(g, bar1Rect, bar2Rect, d, .5f, barColor);
            }
            else
            {
                for (double d = StartAngle; d <= tmpAngle - numberSpacing; d += (float)numberSpacing / NumberOfSubDivisons)
                    PaintBar(g, bar1Rect, bar2Rect, d, .5f, barColor);
            }
        }
        #endregion

        #region PaintArrows
        /// <summary>
        /// Paints the arrows.
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintArrows(Graphics g)
        {
            //If the Max value is displayed using the red arrow
            //Arrow
            DrawArrow(g, Value, ArrowColor);
        }
        #endregion

        #region PaintText
        /// <summary>
        /// Paint the text properties TextUnit and TextDescription
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintText(Graphics g)
        {
            PointF center = new PointF(numberRect.Width / 2 + numberRect.X, numberRect.Height / 2 + numberRect.Y);
            if (UnitText.Length > 0)
            {
                using (Font font = new Font(Font.FontFamily, Font.Size + 4, FontStyle.Bold))
                {
                    SizeF size = MeasureText(g, UnitText, font, (int)numberRect.Width);
                    PointF p = new PointF(center.X - size.Width / 2, center.Y + numberRect.Height / 8);
                    g.DrawString(UnitText, font, new SolidBrush(ForeColor), p);
                }
            }
            if (DescriptionText.Length > 0)
            {
                SizeF size = MeasureText(g, DescriptionText, Font, (int)numberRect.Width);
                PointF p = new PointF(center.X - size.Width / 2, center.Y + numberRect.Height / 3);
                g.DrawString(DescriptionText, Font, new SolidBrush(ForeColor), p);
            }
        }

        #endregion

        #region PaintReflex
        /// <summary>
        /// Paint the reflex on top
        /// </summary>
        /// <param name="g">The graphics object</param>
        protected virtual void PaintReflex(Graphics g)
        {
            if (backgroundRect.IsEmpty) return; //break if nothing to draw
            using (Brush b = new LinearGradientBrush(backgroundRect, Color.Transparent, Color.FromArgb((int)glossinessAlpha, Color.White), defaultLightingAngle))
            {
                GraphicsPath path = new GraphicsPath();
                RectangleF r = backgroundRect;
                r.Inflate(-borderWidth, -borderWidth);
                if (r.IsEmpty) return; //break if noting to draw
                path.AddArc(r, 0, -180);
                r.Height /= 2;
                r.Offset(0, r.Height);
                r.Height /= 8;
                path.AddArc(r, 180, -180);
                g.FillPath(b, path);
                path.Dispose();
            }
        }
        #endregion

        #endregion Protected Methods End

        #region -- Private Methods --

        #region DrawArrow
        /// <summary>
        /// Draws the arrow from 3 points
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="v">The value between Min and Max</param>
        /// <param name="c">The arrow color</param>
        private void DrawArrow(Graphics g, double v, Color c)
        {
            PointF p1, p2, p3;
            //Make v relative to Min
            v -= Min;
            double angleValue = (v / Interval) * numberSpacing;
            if (ClockWise)
            {
                p1 = PointInEllipse(arrow1Rect, StartAngle - angleValue);
                p2 = PointInEllipse(arrow2Rect, StartAngle - angleValue - 170);
                p3 = PointInEllipse(arrow2Rect, StartAngle - angleValue - 190);
            }
            else
            {
                p1 = PointInEllipse(arrow1Rect, StartAngle + angleValue);
                p2 = PointInEllipse(arrow2Rect, StartAngle + angleValue - 170);
                p3 = PointInEllipse(arrow2Rect, StartAngle + angleValue - 190);
            }
            GraphicsPath path = new GraphicsPath();
            path.AddLine(p1, p2);
            path.AddLine(p2, p3);
            //Fill the arrow
            using (Brush b = new SolidBrush(c))
            {
                g.FillPath(b, path);
            }
            path.Dispose();
        }
        #endregion

        #region PaintBar
        /// <summary>
        /// Paint a single bar
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="outerRect">The outer rectangle</param>
        /// <param name="innerRect">The inner rectangle</param>
        /// <param name="a">The angle from the </param>
        /// <param name="width">The width of the pen</param>
        /// <param name="c">The color of the bar</param>
        private static void PaintBar(Graphics g, RectangleF outerRect, RectangleF innerRect, double a, float width, Color c)
        {
            using (Pen pen = new Pen(c, width))
            {
                PointF p1 = PointInEllipse(innerRect, a);
                PointF p2 = PointInEllipse(outerRect, a);
                g.DrawLine(pen, p1, p2);
            }
        }
        #endregion

        #region MeasureText
        /// <summary>
        /// Measures the text size
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="text">The text to size up</param>
        /// <param name="f">The font</param>
        /// <param name="maxWidth">Max width of the text</param>
        /// <returns>The size of the text</returns>
        private static SizeF MeasureText(Graphics g, string text, Font f, int maxWidth)
        {
            //Get the size of the text
            StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
            sf.FormatFlags |= StringFormatFlags.NoWrap | StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox;
            sf.Trimming = StringTrimming.None;
            SizeF size = g.MeasureString(text, f, maxWidth, sf);
            return size;
        }
        #endregion

        #region CalcTextPosition
        /// <summary>
        /// Calcs the position af the text based on the angle in the ellipse
        /// </summary>
        /// <param name="a">The angle</param>
        /// <param name="size">The size of the text to place</param>
        /// <returns>Calculated position as PointF</returns>
        private PointF CalcTextPosition(double a, SizeF size)
        {
            PointF p = PointInEllipse(numberRect, a);
            p.X -= (float)((size.Width / 2) * (1 + Math.Cos(Convert.ToRadians(a))));
            p.Y -= (float)((size.Height / 2) * (1 - Math.Sin(Convert.ToRadians(a))));
            return p;
        }
        #endregion

        #region PointInEllipse
        /// <summary>
        /// Return a point in an ellipse.
        /// </summary>
        /// <param name="rect">The rectectangle around the ellipse</param>
        /// <param name="angle">The angle.</param>
        /// <returns>PointF in the specified ellipse</returns>
        private static PointF PointInEllipse(RectangleF rect, double angle)
        {
            double r1 = rect.Width / 2;
            double r2 = rect.Height / 2;
            double x = (float)(r1 * Math.Cos(Convert.ToRadians(angle))) + r1 + rect.X;
            double y = -(float)(r2 * Math.Sin(Convert.ToRadians(angle))) + r2 + rect.Y;
            return new PointF((float)x, (float)y);
        }
        #endregion

        #region CalcRectangles
        /// <summary>
        /// Calc most rectangles used in the design
        /// Called on the Resize event.
        /// </summary>
        private void CalcRectangles()
        {
            //ShadowRectangle
            shadowRect = ClientRectangle;
            shadowRect.Inflate(-1, -1);
            //Reducing width and height of shadow to avoid clipping
            shadowRect.Width -= 1;
            shadowRect.Height -= 1;
            //Background Rectangle
            backgroundRect = shadowRect;
            backgroundRect.Inflate(.5f, .5f);
            backgroundRect.Offset(-1, -1);
            numberRect = backgroundRect;
            numberRect.Inflate(-(numberMargin + Font.Size), -(numberMargin + Font.Size));
            //The rectangle for the bars
            bar1Rect = backgroundRect;
            bar1Rect.Inflate(-(borderWidth + barOuterMargin), -(borderWidth + barOuterMargin));
            bar2Rect = numberRect;
            bar2Rect.Inflate(barInnerMargin + defaultBarHeight, barInnerMargin + defaultBarHeight);
            bar3Rect = numberRect;
            bar3Rect.Inflate(barInnerMargin, barInnerMargin);
            //Arrow Rectangles
            arrow1Rect = numberRect;
            int infl = barInnerMargin + defaultBarHeight * 2;
            arrow1Rect.Inflate(infl, infl);
            arrow2Rect = numberRect;
            arrow2Rect.Inflate(-numberRect.Width / 6, -numberRect.Width / 6);
        }
        #endregion

        #endregion

        #region * Internal Class Convert *
        /// <summary>
        /// Sealed class Convert
        /// </summary>
        internal static class Convert
        {
            /// <summary>
            /// Convert degrees to radians.
            /// </summary>
            /// <returns>Radians</returns>
            public static double ToRadians(double degrees)
            {
                double radians = (Math.PI / 180) * degrees;
                return (radians);
            }

            /// <summary>
            /// Convert radians to degrees
            /// </summary>
            /// <returns>Degrees</returns>
            public static double ToDegrees(double radians)
            {
                double degrees = (radians * 180) / Math.PI;
                return degrees;
            }
        }
        #endregion
    }
}
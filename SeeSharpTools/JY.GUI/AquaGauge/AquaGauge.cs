using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// Aqua Gauge Control - A Windows User Control.
    /// Author  : Ambalavanar Thirugnanam
    /// Date    : 24th August 2007
    /// email   : ambalavanar.thiru@gmail.com
    /// This is control is for free. You can use for any commercial or non-commercial purposes.
    /// [Please do no remove this header when using this control in your application.]
    /// 修改者：邵天宇，改动相对较大
    /// </summary>
    [ToolboxBitmap(typeof(AquaGauge), "AquaGauge.Industry1AquaGauge.PNG")]
    [Designer(typeof(AquaGaugeDesigner))]
    public partial class AquaGauge : UserControl
    {
        #region Private Attributes

        private double minValue;
        private double maxValue;
        private float threshold;
        private double currentValue;
        private float recommendedValue;
        private int numberOfDivisions;
        private int numberOfSubDivisions;
        private string dialText;
        private Color dialColor = Color.Lavender;
        private float glossinessAlpha = 176;
        private int oldWidth, oldHeight;
        int x, y, width, height;
        float fromAngle = 135F;
        float toAngle = 405F;
        private bool enableTransparentBackground;
        private bool requiresRedraw;
        private Image backgroundImg;
        private Rectangle rectImg;
        #endregion

        public AquaGauge()
        {
            InitializeComponent();

            // 设计器中自动配置了Name会导致在设计时获取控件名称失败
            this.Name = "";

            x = 5;
            y = 5;
            width = this.Width - 10;
            height = this.Height - 10;
            this.numberOfDivisions = 10;
            this.numberOfSubDivisions = 5;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.BackColor = Color.Transparent;
            this.Resize += new EventHandler(AquaGauge_Resize);
            this.requiresRedraw = true;
            this.Size = new Size(215,215);
            this.Max = 100;
            this.BackColor = Color.Silver;
        }



        #region Public Properties
        /// <summary>
        /// Mininum value on the scale
        /// </summary>
        [DefaultValue(0)]
        [Description("Mininum value on the scale")]
        [Category("Layout")]
        public double Min
        {
            get { return minValue; }
            set
            {
                if (value < maxValue)
                {
                    minValue = value;
                    if (currentValue < minValue)
                        currentValue = minValue;
                    requiresRedraw = true;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Maximum value on the scale
        /// </summary>
        [DefaultValue(100)]
        [Description("Maximum value on the scale")]
        [Category("Layout")]
        public double Max
        {
            get { return maxValue; }
            set
            {
                if (value > minValue)
                {
                    maxValue = value;
                    if (currentValue > maxValue)
                        currentValue = maxValue;
                    requiresRedraw = true;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the Threshold area from the Recommended Value. (0-100%)
        /// </summary>
        [DefaultValue(25)]
        [Description("Gets or Sets the Threshold area from the Recommended Value. (0-100%)")]
        private float ThresholdPercent
        {
            get { return threshold; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    threshold = value;
                    requiresRedraw = true;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Threshold value from which green area will be marked.
        /// </summary>
        [DefaultValue(25)]
        [Description("Threshold value from which green area will be marked.")]
        private float RecommendedValue
        {
            get { return recommendedValue; }
            set
            {
                if (value > minValue && value < maxValue)
                {
                    recommendedValue = value;
                    requiresRedraw = true;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Value where the pointer will point to.
        /// </summary>
        [DefaultValue(0)]
        [Description("Value where the pointer will point to.")]
        [Category("Layout")]
        public double Value
        {
            get { return currentValue; }
            set
            {
                if (value >= minValue && value <= maxValue)
                {
                    currentValue = value;
                    requiresRedraw = true;
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// Background color of the dial
        /// </summary>
        [Description("Background color of the dial")]
        public new Color BackColor
        {
            get { return dialColor; }
            set
            {
                dialColor = value;
                requiresRedraw = true;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Glossiness strength. Range: 0-100
        /// </summary>
        [DefaultValue(72)]
        [Description("Glossiness strength. Range: 0-100")]
        [Category("Appearance")]
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
        /// Get or Sets the number of Divisions in the dial scale.
        /// </summary>
        [DefaultValue(10)]
        [Description("Get or Sets the number of Divisions in the dial scale.")]
        [Category("Appearance")]
        public int NumberOfDivisions
        {
            get { return this.numberOfDivisions; }
            set
            {
                if (value >= 1 && value <= 25)
                {
                    this.numberOfDivisions = value;
                    requiresRedraw = true;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the number of Sub Divisions in the scale per Division.
        /// </summary>
        [DefaultValue(3)]
        [Description("Gets or Sets the number of Sub Divisions in the scale per Division.")]
        [Category("Appearance")]
        public int NumberOfSubDivisions
        {
            get { return this.numberOfSubDivisions; }
            set
            {
                if (value > 0 && value <= 10)
                {
                    this.numberOfSubDivisions = value;
                    requiresRedraw = true;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the Text to be displayed in the dial
        /// </summary>
        [Description("Gets or Sets the Text to be displayed in the dial")]
        [Category("Appearance")]
        public string DescriptionText
        {
            get { return this.dialText; }
            set
            {
                this.dialText = value;
                requiresRedraw = true;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Enables or Disables Transparent Background color.
        /// Note: Enabling this will reduce the performance and may make the control flicker.
        /// </summary>
        [DefaultValue(false)]
        [Description("Enables or Disables Transparent Background color. Note: Enabling this will reduce the performance and may make the control flicker.")]
        private bool EnableTransparentBackground
        {
            get { return this.enableTransparentBackground; }
            set
            {
                this.enableTransparentBackground = value;
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, !enableTransparentBackground);
                requiresRedraw = true;
                this.Refresh();
            }
        }
        #endregion

        #region Overriden Control methods
        /// <summary>
        /// Draws the pointer.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            width = this.Width - x * 2;
            height = this.Height - y * 2;
            DrawPointer(e.Graphics, ((width) / 2) + x, ((height) / 2) + y);
        }

        /// <summary>
        /// Draws the dial background.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!enableTransparentBackground)
            {
                base.OnPaintBackground(e);
            }

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(0, 0, Width, Height));
            if (backgroundImg == null || requiresRedraw)
            {
                backgroundImg = new Bitmap(this.Width, this.Height);
                Graphics g = Graphics.FromImage(backgroundImg);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                width = this.Width - x * 2;
                height = this.Height - y * 2;
                rectImg = new Rectangle(x, y, width, height);

                //Draw background color
                Brush backGroundBrush = new SolidBrush(Color.FromArgb(120, dialColor));
                if (enableTransparentBackground && this.Parent != null)
                {
                    float gg = width / 60;
                    //g.FillEllipse(new SolidBrush(this.Parent.BackColor), -gg, -gg, this.Width+gg*2, this.Height+gg*2);
                }
                g.FillEllipse(backGroundBrush, x, y, width, height);

                //Draw Rim
                SolidBrush outlineBrush = new SolidBrush(Color.FromArgb(100, Color.SlateGray));
                Pen outline = new Pen(outlineBrush, (float)(width * .03));
                g.DrawEllipse(outline, rectImg);
                Pen darkRim = new Pen(Color.SlateGray);
                g.DrawEllipse(darkRim, x, y, width, height);

                //Draw Callibration
                DrawCalibration(g, rectImg, ((width) / 2) + x, ((height) / 2) + y);

                //Draw Colored Rim
                Pen colorPen = new Pen(Color.FromArgb(190, Color.Gainsboro), this.Width / 40);
                Pen blackPen = new Pen(Color.FromArgb(250, Color.Black), this.Width / 200);
                int gap = (int)(this.Width * 0.03F);
                Rectangle rectg = new Rectangle(rectImg.X + gap, rectImg.Y + gap, rectImg.Width - gap * 2, rectImg.Height - gap * 2);
                g.DrawArc(colorPen, rectg, 135, 270);

                //Draw Threshold
                colorPen = new Pen(Color.FromArgb(200, Color.LawnGreen), this.Width / 50);
                rectg = new Rectangle(rectImg.X + gap, rectImg.Y + gap, rectImg.Width - gap * 2, rectImg.Height - gap * 2);
                float val =(float) (Max - Min);
                val =(float) (100 * (this.recommendedValue - Min)) / val;
                val = ((toAngle - fromAngle) * val) / 100;
                val += fromAngle;
                float stAngle = val - ((270 * threshold) / 200);
                if (stAngle <= 135) stAngle = 135;
                float sweepAngle = ((270 * threshold) / 100);
                if (stAngle + sweepAngle > 405) sweepAngle = 405 - stAngle;
                g.DrawArc(colorPen, rectg, stAngle, sweepAngle);

                //Draw Digital Value
                RectangleF digiRect = new RectangleF((float)this.Width / 2F - (float)this.width / 5F, (float)this.height / 1.2F, (float)this.width / 2.5F, (float)this.Height / 9F);
                RectangleF digiFRect = new RectangleF(this.Width / 2 - this.width / 7, (int)(this.height / 1.18), this.width / 4, this.Height / 12);
                g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Gray)), digiRect);
                DisplayNumber(g, (float)this.currentValue, digiFRect);

                SizeF textSize = g.MeasureString(this.dialText, this.Font);
                RectangleF digiFRectText = new RectangleF(this.Width / 2 - textSize.Width / 2, (int)(this.height / 1.5), textSize.Width, textSize.Height);
                g.DrawString(dialText, this.Font, new SolidBrush(this.ForeColor), digiFRectText);
                requiresRedraw = false;
            }
            e.Graphics.DrawImage(backgroundImg, rectImg);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Draws the Pointer.
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        private void DrawPointer(Graphics gr, int cx, int cy)
        {
            float radius = this.Width / 2 - (this.Width * .12F);
            float val = (float)(Max - Min);

            Image img = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            val = (float)(100 * (this.currentValue - Min)) / val;
            val = ((toAngle - fromAngle) * val) / 100;
            val += fromAngle;

            float angle = GetRadian(val);
            float gradientAngle = angle;

            PointF[] pts = new PointF[5];

            pts[0].X = (float)(cx + radius * Math.Cos(angle));
            pts[0].Y = (float)(cy + radius * Math.Sin(angle));

            pts[4].X = (float)(cx + radius * Math.Cos(angle - 0.02));
            pts[4].Y = (float)(cy + radius * Math.Sin(angle - 0.02));

            angle = GetRadian((val + 20));
            pts[1].X = (float)(cx + (this.Width * .09F) * Math.Cos(angle));
            pts[1].Y = (float)(cy + (this.Width * .09F) * Math.Sin(angle));

            pts[2].X = cx;
            pts[2].Y = cy;

            angle = GetRadian((val - 20));
            pts[3].X = (float)(cx + (this.Width * .09F) * Math.Cos(angle));
            pts[3].Y = (float)(cy + (this.Width * .09F) * Math.Sin(angle));

            Brush pointer = new SolidBrush(Color.Black);
            g.FillPolygon(pointer, pts);

            PointF[] shinePts = new PointF[3];
            angle = GetRadian(val);
            shinePts[0].X = (float)(cx + radius * Math.Cos(angle));
            shinePts[0].Y = (float)(cy + radius * Math.Sin(angle));

            angle = GetRadian(val + 20);
            shinePts[1].X = (float)(cx + (this.Width * .09F) * Math.Cos(angle));
            shinePts[1].Y = (float)(cy + (this.Width * .09F) * Math.Sin(angle));

            shinePts[2].X = cx;
            shinePts[2].Y = cy;

            LinearGradientBrush gpointer = new LinearGradientBrush(shinePts[0], shinePts[2], Color.SlateGray, Color.Black);
            g.FillPolygon(gpointer, shinePts);

            Rectangle rect = new Rectangle(x, y, width, height);
            DrawCenterPoint(g, rect, ((width) / 2) + x, ((height) / 2) + y);

            DrawGloss(g);

            gr.DrawImage(img, 0, 0);
        }

        /// <summary>
        /// Draws the glossiness.
        /// </summary>
        /// <param name="g"></param>
        private void DrawGloss(Graphics g)
        {
            RectangleF glossRect = new RectangleF(
               x + (float)(width * 0.10),
               y + (float)(height * 0.07),
               (float)(width * 0.80),
               (float)(height * 0.7));
            LinearGradientBrush gradientBrush =
                new LinearGradientBrush(glossRect,
                Color.FromArgb((int)glossinessAlpha, Color.White),
                Color.Transparent,
                LinearGradientMode.Vertical);
            g.FillEllipse(gradientBrush, glossRect);

            //TODO: Gradient from bottom
            glossRect = new RectangleF(
               x + (float)(width * 0.25),
               y + (float)(height * 0.77),
               (float)(width * 0.50),
               (float)(height * 0.2));
            int gloss = (int)(glossinessAlpha / 3);
            gradientBrush =
                new LinearGradientBrush(glossRect,
                Color.Transparent, Color.FromArgb(gloss, this.BackColor),
                LinearGradientMode.Vertical);
            g.FillEllipse(gradientBrush, glossRect);
        }

        /// <summary>
        /// Draws the center point.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        private void DrawCenterPoint(Graphics g, Rectangle rect, int cX, int cY)
        {
            float shift = Width / 5;
            RectangleF rectangle = new RectangleF(cX - (shift / 2), cY - (shift / 2), shift, shift);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.FromArgb(100, this.dialColor), LinearGradientMode.Vertical);
            g.FillEllipse(brush, rectangle);

            shift = Width / 7;
            rectangle = new RectangleF(cX - (shift / 2), cY - (shift / 2), shift, shift);
            brush = new LinearGradientBrush(rect, Color.SlateGray, Color.Black, LinearGradientMode.ForwardDiagonal);
            g.FillEllipse(brush, rectangle);
        }

        /// <summary>
        /// Draws the Ruler
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        private void DrawCalibration(Graphics g, Rectangle rect, int cX, int cY)
        {
            int noOfParts = this.numberOfDivisions + 1;
            int noOfIntermediates = this.numberOfSubDivisions;
            float currentAngle = GetRadian(fromAngle);
            int gap = (int)(this.Width * 0.01F);
            float shift = this.Width / 25;
            Rectangle rectangle = new Rectangle(rect.Left + gap, rect.Top + gap, rect.Width - gap, rect.Height - gap);

            float x, y, x1, y1, tx, ty, radius;
            radius = rectangle.Width / 2 - gap * 5;
            float totalAngle = toAngle - fromAngle;
            float incr = GetRadian(((totalAngle) / ((noOfParts - 1) * (noOfIntermediates + 1))));

            Pen thickPen = new Pen(Color.Black, Width / 50);
            Pen thinPen = new Pen(Color.Black, Width / 100);
            float rulerValue =(float) Min;
            for (int i = 0; i <= noOfParts; i++)
            {
                //Draw Thick Line
                x = (float)(cX + radius * Math.Cos(currentAngle));
                y = (float)(cY + radius * Math.Sin(currentAngle));
                x1 = (float)(cX + (radius - Width / 20) * Math.Cos(currentAngle));
                y1 = (float)(cY + (radius - Width / 20) * Math.Sin(currentAngle));
                g.DrawLine(thickPen, x, y, x1, y1);

                //Draw Strings
                StringFormat format = new StringFormat();
                tx = (float)(cX + (radius - Width / 10) * Math.Cos(currentAngle));
                ty = (float)(cY - shift + (radius - Width / 10) * Math.Sin(currentAngle));
                Brush stringPen = new SolidBrush(this.ForeColor);
                StringFormat strFormat = new StringFormat(StringFormatFlags.NoClip);
                strFormat.Alignment = StringAlignment.Center;
                Font f = new Font(this.Font.FontFamily, (float)(this.Width / 23), this.Font.Style);
                g.DrawString(rulerValue.ToString() + "", f, stringPen, new PointF(tx, ty), strFormat);
                rulerValue += (float)((Max - Min) / (noOfParts - 1));
                rulerValue = (float)Math.Round(rulerValue, 2);

                //currentAngle += incr;
                if (i == noOfParts - 1)
                    break;
                for (int j = 0; j <= noOfIntermediates; j++)
                {
                    //Draw thin lines 
                    currentAngle += incr;
                    x = (float)(cX + radius * Math.Cos(currentAngle));
                    y = (float)(cY + radius * Math.Sin(currentAngle));
                    x1 = (float)(cX + (radius - Width / 50) * Math.Cos(currentAngle));
                    y1 = (float)(cY + (radius - Width / 50) * Math.Sin(currentAngle));
                    g.DrawLine(thinPen, x, y, x1, y1);
                }
            }
        }

        /// <summary>
        /// Converts the given degree to radian.
        /// </summary>
        /// <param name="theta"></param>
        /// <returns></returns>
        private float GetRadian(float theta)
        {
            return theta * (float)Math.PI / 180F;
        }

        /// <summary>
        /// Displays the given number in the 7-Segement format.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="number"></param>
        /// <param name="drect"></param>
        private void DisplayNumber(Graphics g, float number, RectangleF drect)
        {
            try
            {
                string num = number.ToString("000.00");
                num.PadLeft(3, '0');
                float shift = 0;
                if (number < 0)
                {
                    shift -= width / 17;
                }
                bool drawDPS = false;
                char[] chars = num.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    if (i < chars.Length - 1 && chars[i + 1] == '.')
                        drawDPS = true;
                    else
                        drawDPS = false;
                    if (c != '.')
                    {
                        if (c == '-')
                        {
                            DrawDigit(g, -1, new PointF(drect.X + shift, drect.Y), drawDPS, drect.Height);
                        }
                        else
                        {
                            DrawDigit(g, Int16.Parse(c.ToString()), new PointF(drect.X + shift, drect.Y), drawDPS, drect.Height);
                        }
                        shift += 15 * this.width / 250;
                    }
                    else
                    {
                        shift += 2 * this.width / 250;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Draws a digit in 7-Segement format.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="number"></param>
        /// <param name="position"></param>
        /// <param name="dp"></param>
        /// <param name="height"></param>
        private void DrawDigit(Graphics g, int number, PointF position, bool dp, float height)
        {
            float width;
            width = 10F * height / 13;

            Pen outline = new Pen(Color.FromArgb(40, this.dialColor));
            Pen fillPen = new Pen(Color.Black);

            #region Form Polygon Points
            //Segment A
            PointF[] segmentA = new PointF[5];
            segmentA[0] = segmentA[4] = new PointF(position.X + GetX(2.8F, width), position.Y + GetY(1F, height));
            segmentA[1] = new PointF(position.X + GetX(10, width), position.Y + GetY(1F, height));
            segmentA[2] = new PointF(position.X + GetX(8.8F, width), position.Y + GetY(2F, height));
            segmentA[3] = new PointF(position.X + GetX(3.8F, width), position.Y + GetY(2F, height));

            //Segment B
            PointF[] segmentB = new PointF[5];
            segmentB[0] = segmentB[4] = new PointF(position.X + GetX(10, width), position.Y + GetY(1.4F, height));
            segmentB[1] = new PointF(position.X + GetX(9.3F, width), position.Y + GetY(6.8F, height));
            segmentB[2] = new PointF(position.X + GetX(8.4F, width), position.Y + GetY(6.4F, height));
            segmentB[3] = new PointF(position.X + GetX(9F, width), position.Y + GetY(2.2F, height));

            //Segment C
            PointF[] segmentC = new PointF[5];
            segmentC[0] = segmentC[4] = new PointF(position.X + GetX(9.2F, width), position.Y + GetY(7.2F, height));
            segmentC[1] = new PointF(position.X + GetX(8.7F, width), position.Y + GetY(12.7F, height));
            segmentC[2] = new PointF(position.X + GetX(7.6F, width), position.Y + GetY(11.9F, height));
            segmentC[3] = new PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.7F, height));

            //Segment D
            PointF[] segmentD = new PointF[5];
            segmentD[0] = segmentD[4] = new PointF(position.X + GetX(7.4F, width), position.Y + GetY(12.1F, height));
            segmentD[1] = new PointF(position.X + GetX(8.4F, width), position.Y + GetY(13F, height));
            segmentD[2] = new PointF(position.X + GetX(1.3F, width), position.Y + GetY(13F, height));
            segmentD[3] = new PointF(position.X + GetX(2.2F, width), position.Y + GetY(12.1F, height));

            //Segment E
            PointF[] segmentE = new PointF[5];
            segmentE[0] = segmentE[4] = new PointF(position.X + GetX(2.2F, width), position.Y + GetY(11.8F, height));
            segmentE[1] = new PointF(position.X + GetX(1F, width), position.Y + GetY(12.7F, height));
            segmentE[2] = new PointF(position.X + GetX(1.7F, width), position.Y + GetY(7.2F, height));
            segmentE[3] = new PointF(position.X + GetX(2.8F, width), position.Y + GetY(7.7F, height));

            //Segment F
            PointF[] segmentF = new PointF[5];
            segmentF[0] = segmentF[4] = new PointF(position.X + GetX(3F, width), position.Y + GetY(6.4F, height));
            segmentF[1] = new PointF(position.X + GetX(1.8F, width), position.Y + GetY(6.8F, height));
            segmentF[2] = new PointF(position.X + GetX(2.6F, width), position.Y + GetY(1.3F, height));
            segmentF[3] = new PointF(position.X + GetX(3.6F, width), position.Y + GetY(2.2F, height));

            //Segment G
            PointF[] segmentG = new PointF[7];
            segmentG[0] = segmentG[6] = new PointF(position.X + GetX(2F, width), position.Y + GetY(7F, height));
            segmentG[1] = new PointF(position.X + GetX(3.1F, width), position.Y + GetY(6.5F, height));
            segmentG[2] = new PointF(position.X + GetX(8.3F, width), position.Y + GetY(6.5F, height));
            segmentG[3] = new PointF(position.X + GetX(9F, width), position.Y + GetY(7F, height));
            segmentG[4] = new PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.5F, height));
            segmentG[5] = new PointF(position.X + GetX(2.9F, width), position.Y + GetY(7.5F, height));

            //Segment DP
            #endregion

            #region Draw Segments Outline
            g.FillPolygon(outline.Brush, segmentA);
            g.FillPolygon(outline.Brush, segmentB);
            g.FillPolygon(outline.Brush, segmentC);
            g.FillPolygon(outline.Brush, segmentD);
            g.FillPolygon(outline.Brush, segmentE);
            g.FillPolygon(outline.Brush, segmentF);
            g.FillPolygon(outline.Brush, segmentG);
            #endregion

            #region Fill Segments
            //Fill SegmentA
            if (IsNumberAvailable(number, 0, 2, 3, 5, 6, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentA);
            }

            //Fill SegmentB
            if (IsNumberAvailable(number, 0, 1, 2, 3, 4, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentB);
            }

            //Fill SegmentC
            if (IsNumberAvailable(number, 0, 1, 3, 4, 5, 6, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentC);
            }

            //Fill SegmentD
            if (IsNumberAvailable(number, 0, 2, 3, 5, 6, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentD);
            }

            //Fill SegmentE
            if (IsNumberAvailable(number, 0, 2, 6, 8))
            {
                g.FillPolygon(fillPen.Brush, segmentE);
            }

            //Fill SegmentF
            if (IsNumberAvailable(number, 0, 4, 5, 6, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentF);
            }

            //Fill SegmentG
            if (IsNumberAvailable(number, 2, 3, 4, 5, 6, 8, 9, -1))
            {
                g.FillPolygon(fillPen.Brush, segmentG);
            }
            #endregion

            //Draw decimal point
            if (dp)
            {
                g.FillEllipse(fillPen.Brush, new RectangleF(
                    position.X + GetX(10F, width),
                    position.Y + GetY(12F, height),
                    width / 7,
                    width / 7));
            }
        }

        /// <summary>
        /// Gets Relative X for the given width to draw digit
        /// </summary>
        /// <param name="x"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private float GetX(float x, float width)
        {
            return x * width / 12;
        }

        /// <summary>
        /// Gets relative Y for the given height to draw digit
        /// </summary>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private float GetY(float y, float height)
        {
            return y * height / 15;
        }

        /// <summary>
        /// Returns true if a given number is available in the given list.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="listOfNumbers"></param>
        /// <returns></returns>
        private bool IsNumberAvailable(int number, params int[] listOfNumbers)
        {
            if (listOfNumbers.Length > 0)
            {
                foreach (int i in listOfNumbers)
                {
                    if (i == number)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Restricts the size to make sure the height and width are always same.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AquaGauge_Resize(object sender, EventArgs e)
        {
            if (this.Width < 136)
            {
                this.Width = 136;
            }
            if (oldWidth != this.Width)
            {
                this.Height = this.Width;
                oldHeight = this.Width;
            }
            if (oldHeight != this.Height)
            {
                this.Width = this.Height;
                oldWidth = this.Width;
            }
            requiresRedraw = true;
            this.Invalidate();
        }
        #endregion
    }
}

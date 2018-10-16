using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// A basic vertical/horizontal ProgressBar
    /// </summary>
    [Designer(typeof(TankDesigner))]
    [ToolboxBitmap(typeof(Tank), "Tank.Tank.bmp")]
    public partial class Tank : UserControl
    {
        #region Enum
        public enum TankStyles
        {
            Solid,
            Dashed
        }

        /// <summary>
        /// Determines how the text on the progressbar is shown.
        /// <para>None - No text is drawn</para>
        /// <para>Percentage - The percentage done is shown</para>
        /// <para>Text - Draws the text assosciated with the control</para>
        /// <para>Value - The current Value is shown</para>
        /// <para>ValueOverMaximum - The current Value shown with the Maximum value</para>
        /// </summary>
        public enum TextStyleType
        {
            None,
            Text,
            Value
        };

        #endregion


        #region Fields
        private double minimum = 0;
        private double maximum = 100;
        private double currentValue = 0;
        private Orientation orientation = Orientation.Vertical;
        private Color barColor = Color.DodgerBlue;
        private Color borderColor = Color.Black;
        private int borderThickness = 2;
        private TextStyleType textStyle = TextStyleType.Value;
        private TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis;
        private Color textColor = Color.Black;
        private CompositingMode compositingMode = CompositingMode.SourceOver;
        private CompositingQuality compositingQuality = CompositingQuality.HighQuality;
        private InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic;
        private PixelOffsetMode pixelOffsetMode = PixelOffsetMode.HighQuality;
        private SmoothingMode smoothingMode = SmoothingMode.HighQuality;
        private bool hasErrors = false;
        private string errorLog = null;
        private TankStyles styles = TankStyles.Solid;
        private float dashedWith;
        private bool isbright = true;
        private int _dashedInterval = 10;

        private string text = "Tank";
        #endregion


        #region Properties
        /// <summary>
        /// The maximum value.
        /// </summary>
        [Description("The maximum value."),
        DefaultValue(100)]
        [Category("Behavior")]
        public double Maximum
        {
            get { return maximum; }
            set
            {
                if (value >0&&value>minimum)
                {
                    maximum = value;
                }
                this.Refresh();
            }
        }

        /// <summary>
        /// The minimum value.
        /// </summary>
        [Description("The minimum value."),
        DefaultValue(0)]
        public double Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The current value.
        /// <para>Note:</para>
        /// <para>If the value is less than the Minimum, the value is set to the Minimum</para>
        /// <para>If the value is greater than the Maximum, the value is set to the Maximum</para>
        /// </summary>
        [Description("The current value."),
        DefaultValue(25)]
        [Category("Behavior")]
        public double Value
        {
            get { return currentValue; }
            set
            {
                if (value >= minimum && value <= maximum)
                {
                    currentValue = value;
                }
                else if (value > maximum)
                {
                    currentValue = maximum;
                }
                else if (value < minimum)
                {
                    currentValue = minimum;
                }

                this.Invalidate();

            }
        }

        /// <summary>
        /// The border color.
        /// <para>If set to Transparent, no border is drawn.</para>
        /// </summary>
        [Description("The border color."),
        DefaultValue(typeof(Color), "Black")]
        [Category("Appearance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The border thickness.
        /// <para>If set to 0, no border is drawn.</para>
        /// </summary>
        [Description("The border thickness"),
        DefaultValue(2)]
        [Category("Appearance")]
        public int BorderWidth
        {
            get { return borderThickness; }
            set
            {
                borderThickness = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The ProgressBar oritentation.
        /// </summary>
        [Description("The Tank oritentation."),
        DefaultValue(typeof(Orientation), "Vertical")]
        [Category("Appearance")]
        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                orientation = value;
                this.Invalidate();
            }
        }

        private bool _reversed;
        /// <summary>
        /// Specify whether reverse the direction of tank.
        /// </summary>
        [Description("Specify whether reverse the direction of tank.")]
        [Category("Appearance")]
        public bool Reversed
        {
            get { return _reversed; }
            set
            {
                if (value == _reversed)
                {
                    return;
                }
                _reversed = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The color of the text drawn on the ProgressBar.
        /// <para>If set to transparent, no text is drawn.</para>
        /// </summary>
        [Description("The color of the text drawn on the Control."),
        DefaultValue(typeof(Color), "Black")]
        [Category("Appearance")]
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The way the text on the ProgressBar is drawn.
        /// </summary>
        [Description("The way the text on the ProgressBar is drawn."),
        DefaultValue(typeof(TextStyleType), "Value")]
        [Category("Appearance")]
        public TextStyleType TextStyle
        {
            get { return textStyle; }
            set
            {
                textStyle = value;
                this.Invalidate();
            }
        }
        [Browsable(true)]
        [Description("Set the styles of the control.")]
        [Category("Appearance")]
        public TankStyles Style
        {
            get
            {
                return styles;
            }

            set
            {
                styles = value;
                this.Refresh();
            }
        }
        [Browsable(true)]
        [Description("Set the whether use the bright color of the control.")]
        [Category("Appearance")]
        public bool IsBright
        {
            get
            {
                return isbright;
            }

            set
            {
                isbright = value;
                this.Invalidate();
            }
        }
        [Category("Appearance")]
        [Browsable(true)]
        public override string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// If any errors occur, this will contain the errors information. 
        /// HasErrors will be set to true if any errors have occured.
        /// </summary>
        [Description("If any errors occur, this will contain the errors information. " +
            "HasErrors will be set to true if any errors have occured")]
        private string ErrorLog
        {
            get { return errorLog; }
        }

        /// <summary>
        /// If any errors have occured, this will be set to true.
        /// </summary>
        [Description("If any errors have occured, this will be set to true")]
        private bool HasErrors
        {
            get { return hasErrors; }
        }


        #endregion



        #region Construction / Deconstruction
        /// <summary>
        /// BasicProgressBar initialization
        /// </summary>
        public Tank()
        {

            this.SetStyle
                (
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true
                );

            this.ForeColor = barColor;
            this.BackColor = Color.DarkGray;
            this.Size = new Size(70, 150);
            this.Font = new Font("Consolas", 10.25f);
            this._reversed = false;
            OnForeColorChanged(EventArgs.Empty);

        }


        #endregion


        #region Public Methods
        #endregion



        #region Private Methods
        /// <summary>
        /// Clears any errors and sets HasErrors to false.
        /// </summary>
        private void ClearErrors()
        {
            errorLog = "";
            hasErrors = false;
        }

        /// <summary>
        /// Draws the progress bar.
        /// </summary>
        /// <param name="pe">PaintEventArgs</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(e);

            try
            {
                //Don't bother drawing if there's no surface area to work with
                if (this.Width <= 0 || this.Height <= 0)
                {
                    return;
                }

                pe.Graphics.CompositingMode = compositingMode;
                pe.Graphics.CompositingQuality = compositingQuality;
                pe.Graphics.InterpolationMode = interpolationMode;
                pe.Graphics.PixelOffsetMode = pixelOffsetMode;
                pe.Graphics.SmoothingMode = smoothingMode;

                //Draw bar
            //    if (currentValue != 0)
                {
                    var darkColor = ControlPaint.Dark(barColor);


                    using (SolidBrush brush = new SolidBrush(barColor))
                    {

                        if (orientation == Orientation.Vertical)
                        {
                            float scaledHeight = (float) ((this.Height/(maximum - minimum))*(currentValue - minimum));
                            float yPos = this._reversed ? 0 : this.Height - scaledHeight;
                            //    

                            if (isbright == true)
                            {
                                var brush1 = new LinearGradientBrush(new Point(0, 0), new Point(this.Width / 2, 0), darkColor, barColor);
                                pe.Graphics.FillRectangle(brush1, 0, yPos, this.Width / 2, scaledHeight);
                                var brush2 = new LinearGradientBrush(new Point(this.Width / 2 - 1, 0), new Point(this.Width, 0), barColor, darkColor);
                                pe.Graphics.FillRectangle(brush2, this.Width / 2 - 1, yPos, this.Width, scaledHeight);
                            }
                            else
                            {
                                pe.Graphics.FillRectangle(brush, 0, yPos, this.Width, scaledHeight);
                            }


                            //如果选择Dolid模式，同时应该画线
                            if (styles == TankStyles.Dashed)
                            {
                                int sepWidth = this.Height / 10;
                                int sepCount = (int)((((double)this.Height / (maximum-minimum)) * (currentValue-minimum)) / sepWidth);
                                Color sepColor = ControlPaint.LightLight(barColor);
                                // Draw each separator line
                                for (int i = 1; i <= sepCount; i++)
                                {
                                    pe.Graphics.DrawLine(new Pen(sepColor, 1),
                                        0, this.Height-sepWidth * i, this.Width, Height - sepWidth * i);
                                }
                            }
                        }
                        else
                        {
                            float scaledWidth = (float)((this.Width / (maximum - minimum)) * (currentValue - minimum));
                            float xPos = this._reversed ? this.Width - scaledWidth : 0;
                            if (isbright == true)
                            {
                                var brush1 = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height / 2), darkColor, barColor);
                                pe.Graphics.FillRectangle(brush1, xPos, 0, scaledWidth, this.Height / 2);
                                var brush2 = new LinearGradientBrush(new Point(0, this.Height / 2 - 1), new Point(0, this.Height), barColor, darkColor);
                                pe.Graphics.FillRectangle(brush2, xPos, Height / 2 - 1, scaledWidth, this.Height);
                            }
                            else
                            {
                                pe.Graphics.FillRectangle(brush, xPos, 0, scaledWidth, this.Height);
                            }

 

                            //如果选择Dolid模式，同时应该画线
                            if (styles == TankStyles.Dashed)
                            {
                                int sepWidth = this.Width / _dashedInterval;
                                int sepCount = (int)((((double)this.Width / (maximum - minimum)) * (currentValue - minimum)) / sepWidth);
                                Color sepColor = ControlPaint.LightLight(barColor);
                                // Draw each separator line
                                for (int i = 1; i <= sepCount; i++)
                                {
                                    pe.Graphics.DrawLine(new Pen(sepColor, 1),
                                        sepWidth * i, 0, sepWidth * i, this.Height);
                                }
                            }
                        }
                    }



                }

                //Draw text
                if (textStyle != TextStyleType.None)
                {
                    if (textColor != Color.Transparent)
                    {
                        using (Font font = new Font(this.Font.Name, this.Font.SizeInPoints, this.Font.Style, GraphicsUnit.Pixel))
                        {
                            string txt = null;

                            if (textStyle == TextStyleType.Value)
                            {
                                txt = currentValue.ToString();
                            }
                            //else if (textStyle == TextStyleType.ValueOverMaximum)
                            //{
                            //    txt = String.Format("{0}/{1}", currentValue, maximum);
                            //}
                            //else if (textStyle == TextStyleType.Percentage && maximum != 0)
                            //{
                            //    double p = Convert.ToDouble((100d / maximum) * Value);
                            //    txt = String.Format("{0}%", p);
                            //}
                            else if (textStyle == TextStyleType.Text && !String.IsNullOrWhiteSpace(this.Text))
                            {
                                txt = this.Text;
                            }
                            
                            if (txt != null)
                            {
                                TextRenderer.DrawText(pe.Graphics, txt, font, new Rectangle(0, 0, this.Width, this.Height), textColor, flags);
                            }

                        }
                    }
                }

                //Draw border
                if (borderThickness > 0)
                {
                    if (borderColor != Color.Transparent)
                    {
                        using (Pen pen = new Pen(borderColor, borderThickness))
                        {
                            pe.Graphics.DrawRectangle(pen, 0, 0, this.Width, this.Height);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                errorLog += "Error in OnPaint event\n " + 
                    "Message: " + ex.Message + "\n" + 
                    "Type: " + ex.GetType().ToString() + "\n";
                
                hasErrors = true;
            }

        }



        /// <summary>
        /// Overrides the OnForeColorChanged event to set the current bar color to the ForeColor.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnForeColorChanged(EventArgs e)
        {
            barColor = ForeColor;
            base.OnForeColorChanged(e);
        }


        #endregion



        #region Threading

        #endregion


        #region Smoothing properties

        /// <summary>
        /// The ProgressBar's Graphic's CompositingMode.
        /// </summary>
        [Description("The ProgressBar's Graphic's CompositingMode."),
        DefaultValue(typeof(CompositingMode), "SourceOver")]
        private CompositingMode CompositingMode
        {
            get { return compositingMode; }
            set 
            { 
                compositingMode = value; 
                this.Invalidate(); 
            }
        }
        /// <summary>
        /// The ProgressBar's Graphic's CompositingQuality.
        /// </summary>
        [Description("The ProgressBar's Graphic's CompositingQuality."),
        DefaultValue(typeof(CompositingQuality), "HighQuality")]
        private CompositingQuality CompositingQuality
        {
            get { return compositingQuality; }
            set
            {
                if (value != CompositingQuality.Invalid)
                {
                    compositingQuality = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// The ProgressBar's Graphic's InterpolationMode.
        /// </summary>
        [Description("The ProgressBar's Graphic's InterpolationMode."),
        DefaultValue(typeof(InterpolationMode), "HighQualityBicubic")]
        private InterpolationMode InterpolationMode
        {
            get { return interpolationMode; }
            set
            {
                if (value != InterpolationMode.Invalid)
                {
                    interpolationMode = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// The ProgressBar's Graphic's PixelOffsetMode.
        /// </summary>
        [Description("The ProgressBar's Graphic's PixelOffsetMode."),
        DefaultValue(typeof(PixelOffsetMode), "HighQuality")]
        private PixelOffsetMode PixelOffsetMode
        {
            get { return pixelOffsetMode; }
            set
            {
                if (value != PixelOffsetMode.Invalid)
                {
                    pixelOffsetMode = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// The ProgressBar's Graphic's SmoothingMode.
        /// </summary>
        [Description("The ProgressBar's Graphic's SmoothingMode."),
        DefaultValue(typeof(SmoothingMode), "HighQuality")]
        private SmoothingMode SmoothingMode
        {
            get { return smoothingMode; }
            set
            {
                if (value != SmoothingMode.Invalid)
                {
                    smoothingMode = value; 
                    this.Invalidate();
                }
            }
        }
        #endregion
    }



    #region ValueChangedEventArgs : EventArgs

    /// <summary>
    /// Event arguments for the ValueChanged ProgressBar event
    /// </summary>
    public class ValueChangedEventArgs : EventArgs
    {
        /// <summary>
        /// ValueChangedEventArgs
        /// </summary>
        /// <param name="currentValue">The current value of the ProgressBar</param>
        public ValueChangedEventArgs(int currentValue)
        {
            this.Value = currentValue;
        }

        /// <summary>
        /// The current value of ProgressBar
        /// </summary>
        public int Value { get; set; }
    }

    #endregion

}

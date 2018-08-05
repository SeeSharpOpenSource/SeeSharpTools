using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SeeSharpTools.JY.GUI
{
    [ToolboxBitmap(typeof(SevenSegmentSingle), "Segment.SevenSegment.bmp")]
    [Designer(typeof(SevenSegmentDesigner))]
    public partial class SevenSegment : UserControl
    {
        #region Enum
        #endregion


        #region Fields
        /// <summary>
        /// Array of segment controls that are currently children of this control.
        /// </summary>
        private SevenSegmentSingle[] segments = null;

        private int elementWidth = 10;
        private float italicFactor = 0.0F;
        private Color colorBackground = Color.Black;
        private Color colorDark = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        private Color colorLight = Color.Red;
        private bool showDot = true;
        private Padding elementPadding;
        private string theValue = null;

        #endregion
        #region Properties
        /// <summary>
        /// Background color of the LED array.
        /// </summary>
        [Description("Background color of the LED array.")]
        [Category("Appearance")]
        public  Color BackgroundColor { get { return colorBackground; } set { colorBackground = value; UpdateSegments(); } }


        [Browsable(false)]
        public override Color BackColor
        {
            set { } 
        }


        /// <summary>
        /// Color of inactive LED segments.
        /// </summary>
        [Description("Color of inactive LED segments.")]
        [Category("Appearance")]
        public Color DarkColor { get { return colorDark; } set { colorDark = value; UpdateSegments(); } }

        /// <summary>
        /// Color of active LED segments.
        /// </summary>
        [Description("Color of active LED segments.")]
        [Category("Appearance")]
        public Color LightColor { get { return colorLight; } set { colorLight = value; UpdateSegments(); } }

        /// <summary>
        /// Width of LED segments.
        /// </summary>
        private int ElementWidth { get { return elementWidth; } set { elementWidth = value; UpdateSegments(); } }

        /// <summary>
        /// Shear coefficient for italicizing the displays. Try a value like -0.1.
        /// </summary>
        [Description("Shear coefficient for italicizing the displays. Try a value like -0.1.")]
        [Category("Appearance")]
        public float ItalicFactor { get { return italicFactor; } set { italicFactor = value; UpdateSegments(); } }

        /// <summary>
        /// Specifies if the decimal point LED is displayed.
        /// </summary>
        [Description("Specifies if the decimal point LED is displayed.")]
        [Category("Appearance")]
        public bool IsDecimalShow { get { return showDot; } set { showDot = value; UpdateSegments(); } }

        /// <summary>
        /// The value to be displayed on the LED array. This can contain numbers,
        /// certain letters, and decimal points.
        /// </summary>
        [Description("The value to be displayed on the LED array. This can contain numbers,certain letters, and decimal points.")]
        [Category("Appearance")]
        public string Value
        {
            get { return theValue; }
            set
            {
                theValue = value;
                for (int i = 0; i < segments.Length; i++) { segments[i].CustomPattern = 0; segments[i].DecimalOn = false; }
                if (theValue != null)
                {
                    int segmentIndex = 0;
                    for (int i = theValue.Length - 1; i >= 0; i--)
                    {
                        if (segmentIndex >= segments.Length) break;
                        if (theValue[i] == '.') segments[segmentIndex].DecimalOn = true;
                        else segments[segmentIndex++].Value = theValue[i].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Number of seven-segment elements in this array.
        /// </summary>
        [Description("Number of seven-segment elements in this array.")]
        [Category("Appearance")]
        public int NumberOfChars { get { return segments.Length; } set { if ((value > 0) && (value <= 100)) RecreateSegments(value); } }

        /// <summary>
        /// Padding that applies to each seven-segment element in the array.
        /// Tweak these numbers to get the perfect appearance for the array of your size.
        /// </summary>
        private Padding ElementPadding { get { return elementPadding; } set { elementPadding = value; UpdateSegments(); } }


        #endregion
        #region Construction / Deconstruction
        public SevenSegment()
        {
            InitializeComponent();
            this.SuspendLayout();
            this.Size = new System.Drawing.Size(200,100);
            this.Resize += new System.EventHandler(this.SevenSegmentArray_Resize);
            this.ResumeLayout(false);
            this.italicFactor = (float)-0.08;
            this.TabStop = false;
         //   this.BackColor = Color.Black;
            elementPadding = new Padding(4, 4, 4, 4);
            RecreateSegments(4);
        }


        #endregion
        
        #region Public Methods
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Change the number of elements in our LED array. This destroys
        /// the previous elements, and creates new ones in their place, applying
        /// all the current options to the new ones.
        /// </summary>
        /// <param name="count">Number of elements to create.</param>
        private void RecreateSegments(int count)
        {
            if (segments != null)
                for (int i = 0; i < segments.Length; i++) { segments[i].Parent = null; segments[i].Dispose(); }

            if (count <= 0) return;
            segments = new SevenSegmentSingle[count];

            for (int i = 0; i < count; i++)
            {
                segments[i] = new SevenSegmentSingle();
                segments[i].Parent = this;
                segments[i].Top = 0;
                segments[i].Height = this.Height;
                segments[i].Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                segments[i].Visible = true;
               // segments[i].BackColor = this.BackColor;
            }

            ResizeSegments();
            UpdateSegments();
            this.Value = theValue;
        }

        /// <summary>
        /// Align the elements of the array to fit neatly within the
        /// width of the parent control.
        /// </summary>
        private void ResizeSegments()
        {
            int segWidth = this.Width / segments.Length;
            for (int i = 0; i < segments.Length; i++)
            {
                segments[i].Left = this.Width * (segments.Length - 1 - i) / segments.Length;
                segments[i].Width = segWidth;
            }
        }

        /// <summary>
        /// Update the properties of each element with the properties
        /// we have stored.
        /// </summary>
        private void UpdateSegments()
        {
            for (int i = 0; i < segments.Length; i++)
            {
                segments[i].ColorBackground = colorBackground;;
                segments[i].ColorDark = colorDark;
                segments[i].ColorLight = colorLight;
                segments[i].ElementWidth = elementWidth;
                segments[i].ItalicFactor = italicFactor;
                segments[i].DecimalShow = showDot;
                segments[i].Padding = elementPadding;
            }
        }

        private void SevenSegmentArray_Resize(object sender, EventArgs e) { ResizeSegments(); }

        protected override void OnPaintBackground(PaintEventArgs e) { e.Graphics.Clear(colorBackground); }


        #endregion


        /// <summary>
        /// 单个七位段码器的类
        /// </summary>
        private class SevenSegmentSingle : UserControl
        {
            #region Enum
            /// <summary>
            /// These are the various bit patterns that represent the characters
            /// that can be displayed in the seven segments. Bits 0 through 6
            /// correspond to each of the LEDs, from top to bottom!
            /// </summary>
            public enum ValuePattern
            {
                None = 0x0, Zero = 0x77, One = 0x24, Two = 0x5D, Three = 0x6D,
                Four = 0x2E, Five = 0x6B, Six = 0x7B, Seven = 0x25,
                Eight = 0x7F, Nine = 0x6F, A = 0x3F, B = 0x7A, C = 0x53,
                D = 0x7C, E = 0x5B, F = 0x1B, G = 0x73, H = 0x3E,
                J = 0x74, L = 0x52, N = 0x38, O = 0x78, P = 0x1F, Q = 0x2F, R = 0x18,
                T = 0x5A, U = 0x76, Y = 0x6E,
                Dash = 0x8, Equals = 0x48
            }

            #endregion


            #region Fields
            private Point[][] segPoints;

            private int gridHeight = 80;
            private int gridWidth = 48;
            private int elementWidth = 10;
            private float italicFactor = 0.0F;
            private Color colorBackground ;
            private Color colorDark ;
            private Color colorLight;
            private string theValue = null;
            private int customPattern = 0;
            private bool showDot = true, dotOn = false;
            #endregion


            #region Properties
            /// <summary>
            /// Background color of the 7-segment display.
            /// </summary>
            public Color ColorBackground { get { return colorBackground; } set { colorBackground = value; Invalidate(); } }
            /// <summary>
            /// Color of inactive LED segments.
            /// </summary>
            public Color ColorDark { get { return colorDark; } set { colorDark = value; Invalidate(); } }
            /// <summary>
            /// Color of active LED segments.
            /// </summary>
            public Color ColorLight { get { return colorLight; } set { colorLight = value; Invalidate(); } }

            /// <summary>
            /// Width of LED segments.
            /// </summary>
            public int ElementWidth { get { return elementWidth; } set { elementWidth = value; RecalculatePoints(); Invalidate(); } }
            /// <summary>
            /// Shear coefficient for italicizing the displays. Try a value like -0.1.
            /// </summary>
            public float ItalicFactor { get { return italicFactor; } set { italicFactor = value; Invalidate(); } }

            /// <summary>
            /// Character to be displayed on the seven segments. Supported characters
            /// are digits and most letters.
            /// </summary>
            public string Value
            {
                get { return theValue; }
                set
                {
                    customPattern = 0;
                    if (value != null)
                    {
                        //is it an integer?
                        bool success = false;
                        try
                        {
                            int tempValue = Convert.ToInt32(value);
                            if (tempValue > 9) tempValue = 9; if (tempValue < 0) tempValue = 0;
                            switch (tempValue)
                            {
                                case 0: customPattern = (int)ValuePattern.Zero; break;
                                case 1: customPattern = (int)ValuePattern.One; break;
                                case 2: customPattern = (int)ValuePattern.Two; break;
                                case 3: customPattern = (int)ValuePattern.Three; break;
                                case 4: customPattern = (int)ValuePattern.Four; break;
                                case 5: customPattern = (int)ValuePattern.Five; break;
                                case 6: customPattern = (int)ValuePattern.Six; break;
                                case 7: customPattern = (int)ValuePattern.Seven; break;
                                case 8: customPattern = (int)ValuePattern.Eight; break;
                                case 9: customPattern = (int)ValuePattern.Nine; break;
                            }
                            success = true;
                        }
                        catch { }
                        if (!success)
                        {
                            try
                            {
                                //is it a letter?
                                string tempString = Convert.ToString(value);
                                switch (tempString.ToLower()[0])
                                {
                                    case 'a': customPattern = (int)ValuePattern.A; break;
                                    case 'b': customPattern = (int)ValuePattern.B; break;
                                    case 'c': customPattern = (int)ValuePattern.C; break;
                                    case 'd': customPattern = (int)ValuePattern.D; break;
                                    case 'e': customPattern = (int)ValuePattern.E; break;
                                    case 'f': customPattern = (int)ValuePattern.F; break;
                                    case 'g': customPattern = (int)ValuePattern.G; break;
                                    case 'h': customPattern = (int)ValuePattern.H; break;
                                    case 'j': customPattern = (int)ValuePattern.J; break;
                                    case 'l': customPattern = (int)ValuePattern.L; break;
                                    case 'n': customPattern = (int)ValuePattern.N; break;
                                    case 'o': customPattern = (int)ValuePattern.O; break;
                                    case 'p': customPattern = (int)ValuePattern.P; break;
                                    case 'q': customPattern = (int)ValuePattern.Q; break;
                                    case 'r': customPattern = (int)ValuePattern.R; break;
                                    case 't': customPattern = (int)ValuePattern.T; break;
                                    case 'u': customPattern = (int)ValuePattern.U; break;
                                    case 'y': customPattern = (int)ValuePattern.Y; break;
                                    case '-': customPattern = (int)ValuePattern.Dash; break;
                                    case '=': customPattern = (int)ValuePattern.Equals; break;
                                }
                            }
                            catch { }
                        }
                    }
                    theValue = value; Invalidate();
                }
            }
            /// <summary>
            /// Set a custom bit pattern to be displayed on the seven segments. This is an
            /// integer value where bits 0 through 6 correspond to each respective LED
            /// segment.
            /// </summary>
            public int CustomPattern { get { return customPattern; } set { customPattern = value; Invalidate(); } }
            /// <summary>
            /// Specifies if the decimal point LED is displayed.
            /// </summary>
            public bool DecimalShow { get { return showDot; } set { showDot = value; Invalidate(); } }
            /// <summary>
            /// Specifies if the decimal point LED is active.
            /// </summary>
            public bool DecimalOn { get { return dotOn; } set { dotOn = value; Invalidate(); } }
            #endregion



            #region Construction / Deconstruction
            public SevenSegmentSingle()
            {
                this.SuspendLayout();
                this.Size = new System.Drawing.Size(32, 64);
                this.Paint += new System.Windows.Forms.PaintEventHandler(this.SevenSegment_Paint);
                this.Resize += new System.EventHandler(this.SevenSegment_Resize);
                this.ResumeLayout(false);
                this.italicFactor = (float)-0.08;
                this.TabStop = false;
                this.Padding = new Padding(4, 4, 4, 4);
                this.DoubleBuffered = true;

                segPoints = new Point[7][];
                for (int i = 0; i < 7; i++) segPoints[i] = new Point[6];

                RecalculatePoints();
            }


            #endregion


            #region Private Methods
            /// <summary>
            /// Recalculate the points that represent the polygons of the
            /// seven segments, whether we're just initializing or
            /// changing the segment width.
            /// </summary>
            private void RecalculatePoints()
            {
                int halfHeight = gridHeight / 2, halfWidth = elementWidth / 2;

                int p = 0;
                segPoints[p][0].X = elementWidth + 1; segPoints[p][0].Y = 0;
                segPoints[p][1].X = gridWidth - elementWidth - 1; segPoints[p][1].Y = 0;
                segPoints[p][2].X = gridWidth - halfWidth - 1; segPoints[p][2].Y = halfWidth;
                segPoints[p][3].X = gridWidth - elementWidth - 1; segPoints[p][3].Y = elementWidth;
                segPoints[p][4].X = elementWidth + 1; segPoints[p][4].Y = elementWidth;
                segPoints[p][5].X = halfWidth + 1; segPoints[p][5].Y = halfWidth;

                p++;
                segPoints[p][0].X = 0; segPoints[p][0].Y = elementWidth + 1;
                segPoints[p][1].X = halfWidth; segPoints[p][1].Y = halfWidth + 1;
                segPoints[p][2].X = elementWidth; segPoints[p][2].Y = elementWidth + 1;
                segPoints[p][3].X = elementWidth; segPoints[p][3].Y = halfHeight - halfWidth - 1;
                segPoints[p][4].X = 4; segPoints[p][4].Y = halfHeight - 1;
                segPoints[p][5].X = 0; segPoints[p][5].Y = halfHeight - 1;

                p++;
                segPoints[p][0].X = gridWidth - elementWidth; segPoints[p][0].Y = elementWidth + 1;
                segPoints[p][1].X = gridWidth - halfWidth; segPoints[p][1].Y = halfWidth + 1;
                segPoints[p][2].X = gridWidth; segPoints[p][2].Y = elementWidth + 1;
                segPoints[p][3].X = gridWidth; segPoints[p][3].Y = halfHeight - 1;
                segPoints[p][4].X = gridWidth - 4; segPoints[p][4].Y = halfHeight - 1;
                segPoints[p][5].X = gridWidth - elementWidth; segPoints[p][5].Y = halfHeight - halfWidth - 1;

                p++;
                segPoints[p][0].X = elementWidth + 1; segPoints[p][0].Y = halfHeight - halfWidth;
                segPoints[p][1].X = gridWidth - elementWidth - 1; segPoints[p][1].Y = halfHeight - halfWidth;
                segPoints[p][2].X = gridWidth - 5; segPoints[p][2].Y = halfHeight;
                segPoints[p][3].X = gridWidth - elementWidth - 1; segPoints[p][3].Y = halfHeight + halfWidth;
                segPoints[p][4].X = elementWidth + 1; segPoints[p][4].Y = halfHeight + halfWidth;
                segPoints[p][5].X = 5; segPoints[p][5].Y = halfHeight;

                p++;
                segPoints[p][0].X = 0; segPoints[p][0].Y = halfHeight + 1;
                segPoints[p][1].X = 4; segPoints[p][1].Y = halfHeight + 1;
                segPoints[p][2].X = elementWidth; segPoints[p][2].Y = halfHeight + halfWidth + 1;
                segPoints[p][3].X = elementWidth; segPoints[p][3].Y = gridHeight - elementWidth - 1;
                segPoints[p][4].X = halfWidth; segPoints[p][4].Y = gridHeight - halfWidth - 1;
                segPoints[p][5].X = 0; segPoints[p][5].Y = gridHeight - elementWidth - 1;

                p++;
                segPoints[p][0].X = gridWidth - elementWidth; segPoints[p][0].Y = halfHeight + halfWidth + 1;
                segPoints[p][1].X = gridWidth - 4; segPoints[p][1].Y = halfHeight + 1;
                segPoints[p][2].X = gridWidth; segPoints[p][2].Y = halfHeight + 1;
                segPoints[p][3].X = gridWidth; segPoints[p][3].Y = gridHeight - elementWidth - 1;
                segPoints[p][4].X = gridWidth - halfWidth; segPoints[p][4].Y = gridHeight - halfWidth - 1;
                segPoints[p][5].X = gridWidth - elementWidth; segPoints[p][5].Y = gridHeight - elementWidth - 1;

                p++;
                segPoints[p][0].X = elementWidth + 1; segPoints[p][0].Y = gridHeight - elementWidth;
                segPoints[p][1].X = gridWidth - elementWidth - 1; segPoints[p][1].Y = gridHeight - elementWidth;
                segPoints[p][2].X = gridWidth - halfWidth - 1; segPoints[p][2].Y = gridHeight - halfWidth;
                segPoints[p][3].X = gridWidth - elementWidth - 1; segPoints[p][3].Y = gridHeight;
                segPoints[p][4].X = elementWidth + 1; segPoints[p][4].Y = gridHeight;
                segPoints[p][5].X = halfWidth + 1; segPoints[p][5].Y = gridHeight - halfWidth;
            }

            private void SevenSegment_Resize(object sender, EventArgs e) { this.Invalidate(); }

            protected override void OnPaddingChanged(EventArgs e) { base.OnPaddingChanged(e); this.Invalidate(); }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                //base.OnPaintBackground(e);
                e.Graphics.Clear(colorBackground);
            }

            private void SevenSegment_Paint(object sender, PaintEventArgs e)
            {
                int useValue = customPattern;

                Brush brushLight = new SolidBrush(colorLight);
                Brush brushDark = new SolidBrush(colorDark);

                // Define transformation for our container...
                RectangleF srcRect = new RectangleF(0.0F, 0.0F, gridWidth, gridHeight);
                RectangleF destRect = new RectangleF(Padding.Left, Padding.Top, this.Width - Padding.Left - Padding.Right, this.Height - Padding.Top - Padding.Bottom);

                // Begin graphics container that remaps coordinates for our convenience
                GraphicsContainer containerState = e.Graphics.BeginContainer(destRect, srcRect, GraphicsUnit.Pixel);

                Matrix trans = new Matrix();
                trans.Shear(italicFactor, 0.0F);
                e.Graphics.Transform = trans;

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;

                // Draw elements based on whether the corresponding bit is high
                e.Graphics.FillPolygon((useValue & 0x1) == 0x1 ? brushLight : brushDark, segPoints[0]);
                e.Graphics.FillPolygon((useValue & 0x2) == 0x2 ? brushLight : brushDark, segPoints[1]);
                e.Graphics.FillPolygon((useValue & 0x4) == 0x4 ? brushLight : brushDark, segPoints[2]);
                e.Graphics.FillPolygon((useValue & 0x8) == 0x8 ? brushLight : brushDark, segPoints[3]);
                e.Graphics.FillPolygon((useValue & 0x10) == 0x10 ? brushLight : brushDark, segPoints[4]);
                e.Graphics.FillPolygon((useValue & 0x20) == 0x20 ? brushLight : brushDark, segPoints[5]);
                e.Graphics.FillPolygon((useValue & 0x40) == 0x40 ? brushLight : brushDark, segPoints[6]);

                if (showDot)
                    e.Graphics.FillEllipse(dotOn ? brushLight : brushDark, gridWidth - 1, gridHeight - elementWidth + 1, elementWidth, elementWidth);

                e.Graphics.EndContainer(containerState);
            }
            #endregion

        }
    }
}

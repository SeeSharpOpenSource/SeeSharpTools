using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Text;


namespace SeeSharpTools.JY.GUI
{

    // A delegate type for hooking up ValueChanged notifications. 
    public delegate void ValueChangedEventHandler(object Sender);
    public delegate void ValueChangingEventHandler(object Sender);
    /// <summary>
    /// Summary description for KnobControl.
    /// </summary>
    /// 
    [DefaultEvent("ValueChanged")]
    [Designer(typeof(KnobDesigner))]
    [ToolboxBitmap(typeof(KnobControl), "KnobControl.KnobControl.bmp")]
    public class KnobControl : UserControl
	{

        #region Enum
        #endregion

        #region Fields
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private double MinDoubleValue = 1E-100;
        private double _minimum = 0;
        private double _maximum = 100;
        private double _scaleChange = 10;

        private bool _showValueText = true;
        private bool _isFocused = false;
        private int tickWidth = 5;
        //  private TickTestDisplay_ValueType _tickTextDisplayType = TickTestDisplayType.Outer_Knob;

        private int oldWidth, oldHeight;
        private double _value = 0;
        private bool isKnobRotating = false;
        private Rectangle rKnob;
        private Point pKnob;
        private Rectangle rScale;
        private Pen DottedPen;

        Brush bKnob;
        Brush bKnobPoint;
        //-------------------------------------------------------
        // declare Off screen image and Offscreen graphics       
        //-------------------------------------------------------
        private Image OffScreenImage;
        private Graphics gOffScreen;
        //Changes by Shao Tianyu
        private int decimals =3;
        private int valuedecimals = 3;
        private int numberofDevision = 10;
        private int sizeofInsetCircle = 12;
        private Color knobColor = System.Drawing.SystemColors.Control;
        private double _lastValue = double.NaN;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        /// <value>The min.</value>
        [Browsable(true)]
        [Description("Gets or sets the min value.")]
        [Category("Layout")]
        [DefaultValue(0)]
        public double Min 
		{
			get{return _minimum;}
			set{
                _minimum = (_minimum > _maximum) ? _maximum : value;

                if (_minimum >= Value)
                {
                    Value = _minimum;
                }
                
                _scaleChange = (_maximum - _minimum) / numberofDevision;
                this.Refresh();
            }
        }
        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        /// <value>The max value</value>
        [Browsable(true)]
        [Description("Gets or sets the max value.")]
        [Category("Layout")]
        [DefaultValue(100)]
        public double Max 
		{
			get{return _maximum;}
			set{
                _maximum = (_maximum < _minimum) ? _minimum : value;

                if (_maximum <= Value)
                {
                    Value = _maximum;
                }
                _maximum = value;
                _scaleChange = (_maximum - _minimum) / numberofDevision;
                this.Refresh();
            }
		}
        /// <summary>
        /// Current Value of knob control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or sets the value.")]
        [Category("Layout")]
        [DefaultValue(0)]
        public double Value
        {
            get
            {
                _value = Math.Round(_value, valuedecimals);
                return _value;
            }
            set
            {

                if (Math.Abs(_value - value) < MinDoubleValue)
                {
                    return;
                }
                if (value < _minimum)
                {
                    _value = _minimum;
                }
                else if (value > _maximum)
                {
                    _value = _maximum;
                }
                else
                {
                    _value = value;
                }

                OnValueChanged(_value);
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Number of Devision.1~25")]
        [Category("Layout")]
        [DefaultValue(10)]
        public int NumberOfDivisions
        {
            get
            {
                return numberofDevision;
            }

            set
            {
                if (value <= 25 && value >= 1)
                {
                    numberofDevision = value;
                    _scaleChange = (_maximum - _minimum) / numberofDevision;
                    this.Refresh();
                }
            }
        }
        [Browsable(true)]
        [Description("Gets or sets the whether show the value text.")]
        [Category("Appearance")]
        public bool IsTextShow
        {
            get
            {
                return _showValueText;
            }

            set
            {
                _showValueText = value;
                this.Refresh();
            }
        }
        [Browsable(true)]
        [Description("Gets or sets the tick width.")]
        [Category("Appearance")]
        public int TickWidth
        {
            get
            {
                return tickWidth;
            }

            set
            {
                if (value <= 25 && value >= 0)
                {
                    tickWidth = value;
                    this.Refresh();
                }

               
            }
        }
        [Browsable(true)]
        [Description("Gets or sets the color of knob.")]
        [Category("Appearance")]
        public Color KnobColor
        {
            get
            {
                return knobColor;
            }

            set
            {
                knobColor = value;
                bKnob = new System.Drawing.Drawing2D.LinearGradientBrush(
                  rKnob, getLightColor(knobColor, 55), getDarkColor(knobColor, 55), LinearGradientMode.ForwardDiagonal);
                this.Refresh();
            }
        }
        [Browsable(true)]
        [Description("Gets or sets the decimals of text.")]
        [Category("Appearance")]
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
        [Browsable(true)]
        [Description("Gets or sets the decimals of value.")]
        [Category("Appearance")]
        public int Valuedecimals
        {
            get
            {
                return valuedecimals;
            }

            set
            {
                if (value >= 0 && value <= 10)
                {
                    valuedecimals = value;
                    this.Invalidate();
                }
            }
        }
        #endregion

        #region Construction / Deconstruction
        public KnobControl()
        {

            this.Resize += new EventHandler(Knob_Resize);
            // This call is required by the Windows.Forms Form Designer.
            DottedPen = new Pen(getDarkColor(this.BackColor, 40));
            DottedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            DottedPen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            InitializeComponent();
            setDimensions();


            // TODO: Add any initialization after the InitForm call

        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
			// 
			// KnobControl
			// 
			this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Font = new System.Drawing.Font("ËÎÌו", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Resize += new System.EventHandler(this.KnobControl_Resize);

		}


        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        //-------------------------------------------------------
        // Invoke the ValueChanged event; called  when value     
        // is changed                                            
        //-------------------------------------------------------
        protected virtual void OnValueChanged(object sender)
        {
            if (ValueChanged != null)
                ValueChanged(sender);
        }

        protected virtual void OnValueChanging(object sender)
        {
            ValueChanging?.Invoke(sender);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
     //       Color foreColor = System.Drawing.Color.Black;
            Brush brush = new SolidBrush(ForeColor);
            string text;
            StringFormat stringFormat;
            stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.NoWrap;
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            stringFormat.HotkeyPrefix = HotkeyPrefix.Show;

            Font font = this.Font;
            Graphics g = e.Graphics;
            // Set background color of Image...            
            gOffScreen.Clear(this.BackColor);
            // Fill knob Background to give knob effect....
            gOffScreen.FillEllipse(bKnob, rKnob);
            // Set antialias effect on                     
            gOffScreen.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // Draw border of knob                         
            gOffScreen.DrawEllipse(new Pen(this.BackColor), rKnob);

            //if control is focused 
            if (this._isFocused)
            {
                gOffScreen.DrawEllipse(DottedPen, rKnob);
            }

            // get current position of pointer             
            Point Arrow = this.getKnobPosition();

            // Draw pointer arrow that shows knob position 
            DrawInsetCircle(ref gOffScreen, new Rectangle(Arrow.X - 3, Arrow.Y - 3, sizeofInsetCircle, sizeofInsetCircle), new Pen( this.BackColor));

            //---------------------------------------------
            // darw small and large scale             _scaleChange/ (_Maximum - _Minimum)     
            //---------------------------------------------
            if (this._showValueText)
            {
             //   int legth = 10;
                for (double i = Min; i <= Max+ _scaleChange / (_maximum - _minimum); i += this._scaleChange)
                {
                    gOffScreen.DrawLine(new Pen(this.ForeColor), getMarkerPoint(0, i-Min), getMarkerPoint(0+ tickWidth, i-Min));
                    text = Math.Round(i, decimals).ToString();
                    // gOffScreen.DrawString(text, font, brush, getMarkerPoint((text.Length * 3 + tickWidth + 9), i - Min).X, getMarkerPoint((text.Length * 3 + tickWidth + 9), i - Min).Y, stringFormat);
                    gOffScreen.DrawString(text, font, brush, getMarkerPoint((text.Length * 3 + tickWidth), i - Min).X, getMarkerPoint((text.Length * 3 + tickWidth), i - Min).Y, stringFormat);
                }

            }


            // Drawimage on screen                    
            g.DrawImage(OffScreenImage, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Empty To avoid Flickring due do background Drawing.
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this._lastValue = _value;
            if (isPointinRectangle(new Point(e.X, e.Y), rKnob))
            {
                // Start Rotation of knob         
                this.isKnobRotating = true;
            }

        }

        //----------------------------------------------------------
        // we need to override IsInputKey method to allow user to   
        // use up, down, right and bottom keys other wise using this
        // keys will change focus from current object to another    
        // object on the form                                       
        //----------------------------------------------------------
        protected override bool IsInputKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                    return true;
            }
            return base.IsInputKey(key);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Stop rotation                   
            this.isKnobRotating = false;
            //            if (isPointinRectangle(new Point(e.X, e.Y), rKnob))
            //            {
            //                
            //            }
            // get value                   
            if (Math.Abs(_lastValue - _value) > MinDoubleValue)
            {
                OnValueChanged(_value);
            }
            this.Cursor = Cursors.Default;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //--------------------------------------
            //  Following Handles Knob Rotating     
            //--------------------------------------
            if (this.isKnobRotating == true)
            {
                this.Cursor = Cursors.Hand;
                Point p = new Point(e.X, e.Y);
                double posVal = this.getValueFromPosition(p);
                SetValueWithChangingEvent(posVal);
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            this._isFocused = true;
            this.Refresh();
            base.OnEnter(new EventArgs());
        }

        protected override void OnLeave(EventArgs e)
        {
            this._isFocused = false;
            this.Refresh();
            base.OnLeave(new EventArgs());
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {

            //--------------------------------------------------------
            // Handles knob rotation with up,down,left and right keys 
            //--------------------------------------------------------
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
            {
                if (_value < Max) Value = _value + 1;
                this.Refresh();
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
            {
                if (_value > Min) Value = _value - 1;
                this.Refresh();
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void setDimensions()
        {
			// get smaller from height and width
			int size = this.Width ;
			if (this.Width > this.Height)
			{
				size = this.Height;
			}
			// allow 10% gap on all side to determine size of knob    
			this.rKnob = new Rectangle((int)(size*0.25),(int)(size*0.25),(int)(size*0.50),(int)(size*0.50));
			
			this.rScale = new Rectangle(2,2,size-2,size-2);

			this.pKnob = new Point(rKnob.X + rKnob.Width/2, rKnob.Y + rKnob.Height/2);
			// create offscreen image                                 
			this.OffScreenImage = new Bitmap(this.Width,this.Height);
			// create offscreen graphics                              
			this.gOffScreen = Graphics.FromImage(OffScreenImage);	

			// create LinearGradientBrush for creating knob            
			bKnob = new System.Drawing.Drawing2D.LinearGradientBrush(
			rKnob, getLightColor(knobColor,55), getDarkColor(knobColor, 55),LinearGradientMode.ForwardDiagonal);
			// create LinearGradientBrush for knobPoint                
			bKnobPoint = new System.Drawing.Drawing2D.LinearGradientBrush(
				rKnob, getLightColor(this.BackColor,55), getDarkColor(this.BackColor,55),LinearGradientMode.ForwardDiagonal);
        }

        private void KnobControl_Resize(object sender, System.EventArgs e)
        {
			setDimensions();
			Refresh();
        }

        /// <summary>
        /// gets knob position that is to be drawn on control.
        /// </summary>
        /// <returns>Point that describes current knob position</returns>
        private Point getKnobPosition()
		{
			double degree = 270* (this.Value-Min)/(this.Max-this.Min);
			degree = (degree +135)*Math.PI /180;

			Point Pos = new Point(0,0);
			Pos.X = (int)(Math.Cos(degree)*(rKnob.Width/2-10)  + rKnob.X + rKnob.Width/2);
			Pos.Y = (int)(Math.Sin(degree)*(rKnob.Width/2-10)  + rKnob.Y + rKnob.Height/2);
			return Pos;
        }

        /// <summary>
        /// gets marker point required to draw scale marker.
        /// </summary>
        /// <param name="length">distance from center</param>
        /// <param name="Value">value that is to be marked</param>
        /// <returns>Point that describes marker position</returns>
        private Point getMarkerPoint(int length,double Value)
		{
			double degree = 270* Value/(this.Max-this.Min);
			degree = (degree +135)*Math.PI /180;

			Point Pos = new Point(0,0);

            Pos.X = (int)(Math.Cos(degree) * (rKnob.Width / 2 + length) + rKnob.X + rKnob.Width / 2);
            Pos.Y = (int)(Math.Sin(degree) * (rKnob.Width / 2 + length) + rKnob.Y + rKnob.Height / 2);

            return Pos;
        }

        /// <summary>
        /// converts geomatrical position in to value..
        /// </summary>
        /// <param name="p">Point that is to be converted</param>
        /// <returns>Value derived from position</returns>
        private double getValueFromPosition(Point p)
        {
            double degree = 0.0;
            double v = 0;
            if (p.X <= pKnob.X)
            {
                degree = (double)(pKnob.Y - p.Y) / (double)(pKnob.X - p.X);
                degree = Math.Atan(degree);
                degree = (degree) * (180 / Math.PI) + 45;
                v = (double)(degree * (this.Max - this.Min) / 270+Min);

            }
            else if (p.X > pKnob.X)
            {
                degree = (double)(p.Y - pKnob.Y) / (double)(p.X - pKnob.X);
                degree = Math.Atan(degree);
                degree = 225 + (degree) * (180 / Math.PI);
                v = (double)(degree * (this.Max - this.Min) / 270+Min);

            }
            if (v > Max) v = Max;
            if (v < Min) v = Min;
            return v;

        }

        private void SetValueWithChangingEvent(double value)
        {
            if (Math.Abs(_value - value) < MinDoubleValue) return;
            if (value < _minimum)
            {
                _value = _minimum;
            }
            else if (value > _maximum)
            {
                _value = _maximum;
            }
            else
            {
                _value = value;
            }
            OnValueChanging(_value);
            this.Invalidate();
        }

        private void Knob_Resize(object sender, EventArgs e)
        {
            if (this.Width < 80)
            {
                this.Width = 80;
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
         //    requiresRedraw = true;
            this.Invalidate();
        }
        #endregion

        #region Threading
        private  Color getDarkColor(Color c, byte d)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            if (c.R > d) r = (byte)(c.R - d);
            if (c.G > d) g = (byte)(c.G - d);
            if (c.B > d) b = (byte)(c.B - d);

            Color c1 = Color.FromArgb(r, g, b);
            return c1;
        }
        private  Color getLightColor(Color c, byte d)
        {
            byte r = 255;
            byte g = 255;
            byte b = 255;

            if (c.R + d < 255) r = (byte)(c.R + d);
            if (c.G + d < 255) g = (byte)(c.G + d);
            if (c.B + d < 255) b = (byte)(c.B + d);

            Color c2 = Color.FromArgb(r, g, b);
            return c2;
        }

        /// <summary>
        /// Method which checks is particular point is in rectangle
        /// </summary>
        /// <param name="p">Point to be Chaecked</param>
        /// <param name="r">Rectangle</param>
        /// <returns>true is Point is in rectangle, else false</returns>
        private bool isPointinRectangle(Point p, Rectangle r)
        {
            bool flag = false;
            if (p.X > r.X && p.X < r.X + r.Width && p.Y > r.Y && p.Y < r.Y + r.Height)
            {
                flag = true;
            }
            return flag;

        }
        private void DrawInsetCircle(ref Graphics g, Rectangle r, Pen p)
        {
            Pen p1 = new Pen(getDarkColor(p.Color, 50));
            Pen p2 = new Pen(getLightColor(p.Color, 50));
            for (int i = 0; i < p.Width; i++)
            {
                Rectangle r1 = new Rectangle(r.X + i, r.Y + i, r.Width - i * 2, r.Height - i * 2);
                g.DrawArc(p2, r1, -45, 180);
                g.DrawArc(p1, r1, 135, 180);
            }
        }
        #endregion

        /// <summary>
        /// Event raised when value is changed by code or after a rotation by mouse.
        /// </summary>
        [Description("Event raised when value is changed by code or after a rotation by mouse.")]
        public event ValueChangedEventHandler ValueChanged;
        /// <summary>
        /// Event raised during the time when knobcontrol is being rotated by mouse
        /// </summary>
        [Description("Event raised during the time when knobcontrol is being rotated by mouse.")]
        public event ValueChangingEventHandler ValueChanging;
		#region Component Designer generated code
		#endregion
	}

}

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace SeeSharpTools.JY.GUI
{
    //  [Designer(typeof(GaugeLinearDesigner))]
    [DefaultEvent("ValueChanged")]
    public partial class GaugeLinear : UserControl
    {

        /// <summary>
        /// Represents the method that will handle a change in value.
        /// </summary>
        public delegate void ValueChangedHandler(object sender, double value);

        /// <summary>
        /// 枚举：方向
        /// </summary>
        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        //public enum ScaleType
        //{
        //    Linear,
        //    Log10
        //}

        /// <summary>
        /// 枚举：SideDirection
        /// </summary>
        public enum SideDirection
        {
            LeftToRight,
            RightToLeft
        }

        #region Fields
        /// <summary>
        /// SideDirection变量
        /// </summary>
        private SideDirection m_Direction;
        /// <summary>
        /// 保存大Tick List
        /// </summary>
        List<TickMajor> m_ListTickMajor = new List<TickMajor>();
        /// <summary>
        /// 保存小Tick List
        /// </summary>
        List<TickMinor> m_ListTickMinor = new List<TickMinor>();
        /// <summary>
        /// 主Tick的长度
        /// </summary>
        private int m_TickMajorLength;
        /// <summary>
        /// 从Tick的长度
        /// </summary>
        private int m_TickMinorLength;
        /// <summary>
        /// Tick的颜色
        /// </summary>
        private Color m_TickColor;
        /// <summary>
        /// Color of Text
        /// </summary>
        private Color m_TextColor;
        /// <summary>
        /// 文本与边界间隔距离，程序内可以调
        /// </summary>
        private int m_TextOverlapPixels;
        /// <summary>
        /// 
        /// </summary>
        private int m_MaxTickStackingDepth;
        /// <summary>
        /// 区间间隔像素
        /// </summary>
        private int m_PixelSpanTotal;
        /// <summary>
        /// Text与杠杆间隔
        /// </summary>
        private int m_LabelSpace;
        /// <summary>
        /// 边界间隔
        /// </summary>
        private int m_Margin;
        /// <summary>
        /// 滑块大小
        /// </summary>
       // private int m_trackerSize.Width;
       //  private int m_PointerHeight;
        /// <summary>
        /// Tick Text最大高度
        /// </summary>
        private int m_MaxTickTextHeight;
        /// <summary>
        /// Tick Text最大宽度
        /// </summary>
        private int m_MaxTickTextWidth;
        /// <summary>
        /// 最小值设定
        /// </summary>
        private double m_Minimum =0;
        /// <summary>
        /// 最大值设定
        /// </summary>
        private double m_Maximum =100;
        /// <summary>
        /// 当前Value
        /// </summary>
        private double m_Value;
        /// <summary>
        /// 方向设定
        /// </summary>
        private Orientation m_Orientation;
        /// <summary>
        /// 主Tick步进
        /// </summary>
        private double m_MajorStepSize;
        /// <summary>
        /// 主Tick数量
        /// </summary>
        private int m_MajorCount;
        /// <summary>
        /// 最大Tick数量
        /// </summary>
        private int m_MaxTicks;
        /// <summary>
        /// PixelsLow标记
        /// </summary>
        private int m_PixelsLow;
        /// <summary>
        /// ClipLow标记
        /// </summary>
        private int m_ClipLow;
        /// <summary>
        /// PixelsHigh标记
        /// </summary>
        private int m_PixelsHigh;
        /// <summary>
        /// ClipHigh标记
        /// </summary>
        private int m_ClipHigh;

        private bool m_IsMouseDown;

        private double m_MouseDownValue;
        private int m_decimals;
        private Color m_trackerColor;
        private Size m_trackerSize;
        private Color m_trackLineColor;
        /// <summary>
        /// Occurs when the property Value has been changed.
        /// </summary>
        public event ValueChangedHandler ValueChanged;
        #endregion

        #region Properties


        [Category("JYTek")]
        public Orientation Orientation
        {
            get
            {
                return m_Orientation;
            }
            set
            {
                if (value != m_Orientation)
                {
                    Size tempSize;
                    tempSize = m_trackerSize;
                    m_trackerSize = new Size(tempSize.Height, tempSize.Width);
                    m_Orientation = value;
                    if (m_Orientation == Orientation.Horizontal)
                    {
                        if (this.Width < this.Height)
                        {
                            int temp = this.Width;
                            this.Width = this.Height;
                            this.Height = temp;
                        }
                    }
                    else //Vertical 
                    {
                        if (this.Width > this.Height)
                        {
                            int temp = this.Width;
                            this.Width = this.Height;
                            this.Height = temp;
                        }
                    }
                    this.Invalidate();
                }
            }
        }


        [Category("JYTek")]
        public SideDirection Sidedirection
        {
            get
            {
                return this.m_Direction;
            }
            set
            {
                if (this.Sidedirection != value)
                {
                    this.m_Direction = value;
                    this.Invalidate();
                }
            }
        }

        [Category("JYTek")]
        public Color LineColor
        {
            get { return m_trackLineColor; }
            set
            {
                if (value != m_trackLineColor)
                {
                    m_trackLineColor = value;
                    Invalidate();
                }
            }
        }

        [Category("JYTek")]
        public double Value
        {
            get
            {
                return m_Value;
            }

            set
            {
              //  m_Value = value;
                if (m_Value != value)
                {
                    if (value < m_Minimum)
                        m_Value = m_Minimum;
                    else
                        if (value > m_Maximum)
                        m_Value = m_Maximum;
                    else
                        m_Value = value;

                    OnValueChanged(m_Value);
                }
                this.Invalidate();
            }
        }
        [Category("JYTek")]
        public int TickMajorLength
        {
            get
            {
                return m_TickMajorLength;
            }

            set
            {
                m_TickMajorLength = value;
                this.Invalidate();
            }
        }
        [Category("JYTek")]
        public int TickMinorLength
        {
            get
            {
                return m_TickMinorLength;
            }

            set
            {
                m_TickMinorLength = value;
                this.Invalidate();
            }
        }
        [Category("JYTek")]
        public Color TickColor
        {
            get
            {
                return m_TickColor;
            }

            set
            {
                m_TickColor = value;
                this.Invalidate();
            }
        }
        [Category("JYTek")]
        public double Minimum
        {
            get
            {
                return m_Minimum;
            }

            set
            {
                if (m_Minimum > m_Maximum)
                    m_Maximum = m_Minimum;
                if (m_Minimum > m_Value)
                    m_Value = m_Minimum;

                m_Minimum = value;
                this.Invalidate();
            }
        }


        [Category("JYTek")]
        public Color TrackerColor
        {
            get
            {
                return m_trackerColor;
            }
            set
            {
                if (m_trackerColor != value)
                {
                    m_trackerColor = value;
                    this.Invalidate();
                }
            }
        }



        [Category("JYTek")]
        public double Maximum
        {
            get
            {
                return m_Maximum;
            }

            set
            {
                if (m_Maximum < m_Value)
                    m_Value = m_Maximum;
                if (m_Maximum < m_Minimum)
                    m_Minimum = m_Maximum;

                m_Maximum = value;
              //  this.Invalidate();
            }
        }
        [Category("JYTek")]
        public Color TextColor
        {
            get
            {
                return m_TextColor;
            }

            set
            {
                m_TextColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the Decimals of the control.
        /// </summary>
        /// <value>A <see cref="Color"/> object that represents the border color of the control.</value>
        [Category("JYTek")]
        [Description("Gets or sets the Decimals of the control.")]
        public int TextDecimals
        {
            get
            {
                return m_decimals;
            }

            set
            {
                if (value >= 0 && value <= 10)
                {
                    m_decimals = value;
                    this.Invalidate();
                }
            }
        }

        [Category("JYTek")]
        [Description("Gets or sets the tracker's size.")]
        public Size TrackerSize
        {
            get { return m_trackerSize; }

            set
            {
                if (m_trackerSize != value)
                {
                    m_trackerSize = value;
                    this.Invalidate();
                }

            }
        }
        #endregion
        public GaugeLinear()
        {
            InitializeComponent();
            // 设计器中自动配置了Name会导致在设计时获取控件名称失败
            this.Name = "";
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);


            m_TickMajorLength = 10;

            m_TickMinorLength = 3;


            m_trackerSize = new Size(20,10);
           // m_PointerHeight = 20;

            m_TickColor = Color.Black;

            m_TextColor = Color.Black;

            m_LabelSpace = 2;

            m_Margin = 2;

            m_trackLineColor = Color.White;

            m_trackerColor = SystemColors.Control;

            m_IsMouseDown = false;

            m_Orientation = Orientation.Vertical;
            this.Cursor = Cursors.Arrow;
            this.Size = new Size(100, 200);

            base.MouseDown += new MouseEventHandler(OnMouseDownSlider);
            base.MouseUp += new MouseEventHandler(OnMouseUpSlider);
            base.MouseMove += new MouseEventHandler(OnMouseMoveSlider);

        }


        private void OnMouseMoveSlider(object sender, MouseEventArgs e)
        {
            if (!m_IsMouseDown)
            {
                return;
            }
            if(m_Orientation == Orientation.Vertical)
                this.Value = ValueClamped(PixelsToValue(e.Y));
            else
                this.Value = ValueClamped(PixelsToValue(e.X));
        }

        private void OnMouseUpSlider(object sender, MouseEventArgs e)
        {

            if (m_IsMouseDown)
            {
                m_IsMouseDown = false;
            }
            m_IsMouseDown = false;
        }

        private void OnMouseDownSlider(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            m_IsMouseDown = true;
            this.m_MouseDownValue = this.Value;
            this.OnMouseMoveSlider(sender, e);
        }



        private void DrawTicks(PaintEventArgs p)
        {
            m_ListTickMajor.Clear();
            m_ListTickMinor.Clear();

            int GetMaxTicks;
            if (m_Orientation == Orientation.Vertical)
            {
                m_PixelSpanTotal = this.Height;
                //Calulate the MaxTicks
                SizeF sizeF = p.Graphics.MeasureString("0.0", this.Font, 0, StringFormat.GenericTypographic);
                var TextSize = new Size((int)Math.Ceiling((double)sizeF.Width) + 1, (int)Math.Ceiling((double)sizeF.Height) + 1);
                var LabelMaxHeight = TextSize.Height * 2;

                GetMaxTicks = (int)(m_PixelSpanTotal / LabelMaxHeight);
            }
            else
            {
                m_PixelSpanTotal = this.Width;
                //Calulate the MaxTicks
                SizeF sizeF = p.Graphics.MeasureString("0.000", this.Font, 0, StringFormat.GenericTypographic);
                var TextSize = new Size((int)Math.Ceiling((double)sizeF.Width) + 1, (int)Math.Ceiling((double)sizeF.Height) + 1);
                var LabelMaxWidth = TextSize.Width * 2;

                GetMaxTicks = (int)(m_PixelSpanTotal / LabelMaxWidth);
            }

            if (GetMaxTicks < 1)
            {
                GetMaxTicks = 1;
            }
            this.m_MaxTicks = GetMaxTicks;
                        
            int num = (int)(Math.Log10(m_Maximum-m_Minimum) - 1.0 - Math.Log10((double)GetMaxTicks));
            while (true)
            {
                double num2 = Math.Pow(10.0, (double)num);
                if (LabelsFit((m_Maximum - m_Minimum), num2 * 1.0))
                {
                    break;
                }
                if (LabelsFit((m_Maximum - m_Minimum), num2 * 2.0))
                {
                    break;
                }
                if (LabelsFit((m_Maximum - m_Minimum), num2 * 5.0))
                {
                    break;
                }
                num++;
            }

            int MinorCount = 4;
            double MinorStepSize =(double)  (m_MajorStepSize / (double)(MinorCount + 1));


            double startValue = this.GetStartValue();
            double stopValue = this.GetStopValue();

            while (true)
            {
                bool flag = InRangeDelta(startValue, m_Minimum, m_Maximum);
                if (flag)
                {
                    SizeF sizeF = p.Graphics.MeasureString("0.0", this.Font, 0, StringFormat.GenericTypographic);

                    m_ListTickMajor.Add(new TickMajor()
                    {
                        LocationValue =   Math.Round(startValue, m_decimals),
                        Length = m_TickMajorLength,
                        TextSize = new Size((int)Math.Ceiling((double)sizeF.Width) + 1, (int)Math.Ceiling((double)sizeF.Height) + 1)
                });
                }
                if (startValue >= stopValue)
                {
                    break;
                }
                for (int i = 0; i < MinorCount; i++)
                {
                    startValue += MinorStepSize;
                    flag = InRangeDelta(startValue, m_Minimum, m_Maximum);
                    if (flag)
                    {
                        m_ListTickMinor.Add(new TickMinor()
                        {
                            LocationValue = startValue,
                            Length = m_TickMinorLength,
                            
                        });
                    }
                }
                startValue += MinorStepSize;
            }

            foreach (var item in m_ListTickMajor)
            {
                SizeF tempSize = p.Graphics.MeasureString(item.LocationValue.ToString("F" + m_decimals.ToString()), this.Font);
                item.TextSize = (new Size((int)Math.Ceiling((double)tempSize.Width) + 1, (int)Math.Ceiling((double)tempSize.Height) + 1));
            }

            //每一次都要讲最大值清空
            m_MaxTickTextWidth = 0;
            m_MaxTickTextHeight = 0;
            m_MaxTickStackingDepth = 0;
            //为帮助

            foreach (var item in m_ListTickMajor)
            {
                int tempStacking;
                if (m_Orientation == Orientation.Vertical)
                    tempStacking = item.TextSize.Height;
                else
                    tempStacking = item.TextSize.Width;
                int tempHigh = item.TextSize.Height;
                int tempWidth = item.TextSize.Width;
                if (m_MaxTickStackingDepth<tempStacking)
                {
                    m_MaxTickStackingDepth = tempStacking;
                }
                if (m_MaxTickTextHeight < tempHigh)
                {
                    m_MaxTickTextHeight = tempHigh;
                }
                if (m_MaxTickTextWidth < tempWidth)
                {
                    m_MaxTickTextWidth = tempWidth;
                }
            }       

        }

        private void DrawgaugeLinear(PaintEventArgs p)
        {
            m_TextOverlapPixels = (int)Math.Ceiling((double)m_MaxTickStackingDepth / 2.0);


            if (m_Orientation == Orientation.Vertical)
            {
                m_PixelsHigh = this.Size.Height - 1;
            }
            else
            {
                m_PixelsHigh = this.Size.Width - 1;
            }
            
            m_PixelsLow = 0;

            m_ClipLow = m_PixelsLow;
            m_ClipHigh = m_PixelsHigh;
            //上下边界进行初始化确认
            if (this.m_PixelsLow < this.m_ClipLow + m_TextOverlapPixels)
            {
                this.m_PixelsLow = this.m_ClipLow + m_TextOverlapPixels;
            }
            if (this.m_PixelsHigh > this.m_ClipHigh - m_TextOverlapPixels)
            {
                this.m_PixelsHigh = this.m_ClipHigh - m_TextOverlapPixels;
            }

            if (m_Orientation == Orientation.Vertical)
            {
                if (m_Direction == SideDirection.LeftToRight)
                {
      
                    Pen TickPen = new Pen(m_TickColor, 1);

                    var r4 = new Rectangle(this.Width-m_Margin - 4 - (m_trackerSize.Width - 8), m_PixelsLow, m_trackerSize.Width - 8, m_PixelsHigh - m_PixelsLow);
                    p.Graphics.FillRectangle(new SolidBrush(m_trackLineColor), r4);
                    BorderSimple.Draw(p, r4, BorderStyleSimple.Sunken, Color.Gray);
                    //p.Graphics.DrawLine(TickPen, m_Margin + m_TickMajorLength+m_PointerSize, m_PixelsHigh, m_Margin + m_TickMajorLength+ m_PointerSize, m_PixelsLow);

                    //Draw Pointer
                    var r3 = new Rectangle(this.Width - m_Margin- m_trackerSize.Width, m_ClipHigh - ValueToPixels(m_Value) - m_trackerSize.Height/2, m_trackerSize.Width, m_trackerSize.Height);
                    var points = GetPointerPoints(r3, Direction.Right);
                    p.Graphics.FillRectangle(new SolidBrush(m_trackerColor), r3);
                    BorderSimple.Draw(p, r3, BorderStyleSimple.Raised, m_trackerColor);  



                    foreach (var item in m_ListTickMajor)
                    {
                        p.Graphics.DrawLine(TickPen, this.Width - m_Margin - m_TickMajorLength - m_trackerSize.Width, ValueToPixels(item.LocationValue), this.Width - m_Margin - m_trackerSize.Width, ValueToPixels(item.LocationValue));

                      //  var r = new Rectangle(this.Width - m_Margin -m_trackerSize.Width - m_TickMajorLength - m_LabelSpace - m_MaxTickTextWidth, m_ClipHigh - ValueToPixels(item.LocationValue) - m_MaxTickTextHeight / 2, m_MaxTickTextWidth, m_MaxTickTextHeight);
                        var r = new Rectangle(this.Width - m_Margin - m_trackerSize.Width - m_TickMajorLength - m_LabelSpace - m_MaxTickTextWidth, m_ClipHigh - ValueToPixels(item.LocationValue) - m_MaxTickTextHeight / 2, m_MaxTickTextWidth, m_MaxTickTextHeight);

                        p.Graphics.DrawString( Math.Round(item.LocationValue, m_decimals).ToString("F"+m_decimals.ToString()), this.Font, new SolidBrush(m_TextColor), r);
                      //  p.Graphics.DrawString("0.000", this.Font, new SolidBrush(m_TextColor), r);
                    }


                    foreach (var item in m_ListTickMinor)
                    {
                        p.Graphics.DrawLine(TickPen, this.Width  - m_trackerSize.Width - m_Margin, ValueToPixels(item.LocationValue), this.Width  - m_trackerSize.Width - m_Margin - m_TickMinorLength, ValueToPixels(item.LocationValue));
                    }
                }
                else
                {
                    ////画边框线
                    Pen TickPen = new Pen(m_TickColor, 1);
                    var r4 = new Rectangle(m_Margin + 4, m_PixelsLow, m_trackerSize.Width - 8, m_PixelsHigh - m_PixelsLow);
                    p.Graphics.FillRectangle(new SolidBrush(m_trackLineColor), r4);
                    BorderSimple.Draw(p, r4, BorderStyleSimple.Sunken, Color.Gray);
                    //p.Graphics.DrawLine(TickPen, m_Margin + m_TickMajorLength+m_PointerSize, m_PixelsHigh, m_Margin + m_TickMajorLength+ m_PointerSize, m_PixelsLow);


                    //Draw Pointer
                    var r3 = new Rectangle(m_Margin, m_ClipHigh - ValueToPixels(m_Value) - m_trackerSize.Height / 2, m_trackerSize.Width, m_trackerSize.Height);
                    var points = GetPointerPoints(r3, Direction.Right);
                    p.Graphics.FillRectangle(new SolidBrush(m_trackerColor), r3);
                    BorderSimple.Draw(p, r3, BorderStyleSimple.Raised, m_trackerColor);


                    foreach (var item in m_ListTickMajor)
                    {
                        p.Graphics.DrawLine(TickPen, m_Margin + m_trackerSize.Width, ValueToPixels(item.LocationValue), m_Margin + m_trackerSize.Width + item.Length, ValueToPixels(item.LocationValue));
                        //确认画Label数值的位置
                        var r = new Rectangle(m_Margin + m_TickMajorLength + m_trackerSize.Width + m_LabelSpace, m_ClipHigh - ValueToPixels(item.LocationValue) - m_MaxTickTextHeight / 2, m_MaxTickTextWidth, m_MaxTickTextHeight);
                        p.Graphics.DrawString(Math.Round(item.LocationValue, m_decimals).ToString("F" + m_decimals.ToString()), this.Font, new SolidBrush(m_TextColor), r);
                    }
                    foreach (var item in m_ListTickMinor)
                    {
                        p.Graphics.DrawLine(TickPen, m_Margin + m_trackerSize.Width, ValueToPixels(item.LocationValue), 2 + m_TickMinorLength + m_trackerSize.Width, ValueToPixels(item.LocationValue));
                    }

                }
            }
            //水平状态下的内容
            else  
            {
                if (m_Direction == SideDirection.LeftToRight)
                {
                    var r4 = new Rectangle(m_PixelsLow, m_Margin+4, m_PixelsHigh - m_PixelsLow, m_trackerSize.Height - 8);
                    p.Graphics.FillRectangle(new SolidBrush(m_trackLineColor), r4);
                    BorderSimple.Draw(p, r4, BorderStyleSimple.Sunken, Color.Gray);

                    //Draw Pointer
                    var r3 = new Rectangle(ValueToPixels(m_Value) - m_trackerSize.Width / 2, m_Margin , m_trackerSize.Width, m_trackerSize.Height);
                    var points = GetPointerPoints(r3, Direction.Right);
                    p.Graphics.FillRectangle(new SolidBrush(m_trackerColor), r3);
                    BorderSimple.Draw(p, r3, BorderStyleSimple.Raised, m_trackerColor);



                    Pen TickPen = new Pen(m_TickColor, 1);

                    //p.Graphics.DrawLine(TickPen, m_PixelsHigh, m_Margin + m_TickMajorLength + m_trackerSize.Height, m_PixelsLow, m_Margin + m_TickMajorLength + m_trackerSize.Height);


                    foreach (var item in m_ListTickMajor)
                    {
                        p.Graphics.DrawLine(TickPen, ValueToPixels(item.LocationValue), m_Margin +m_trackerSize.Height, ValueToPixels(item.LocationValue), m_Margin +m_trackerSize.Height + m_TickMajorLength);

                        var r = new Rectangle(ValueToPixels(item.LocationValue) - item.TextSize.Width / 4, m_Margin+ m_trackerSize.Height + m_TickMajorLength + m_LabelSpace , item.TextSize.Width, item.TextSize.Height);

                        p.Graphics.DrawString(Math.Round(item.LocationValue, m_decimals).ToString("F" + m_decimals.ToString()), this.Font, new SolidBrush(m_TextColor), r);
                    }


                    foreach (var item in m_ListTickMinor)
                    {
                        p.Graphics.DrawLine(TickPen, ValueToPixels(item.LocationValue), m_trackerSize.Height + m_TickMajorLength + m_Margin, ValueToPixels(item.LocationValue), m_trackerSize.Height + m_TickMajorLength + m_Margin - m_TickMinorLength);
                    }
                }
                else
                {
                    var r4 = new Rectangle(m_PixelsLow,this.Height-(m_Margin + m_trackerSize.Height - 4 ), m_PixelsHigh - m_PixelsLow, m_trackerSize.Height - 8);
                    p.Graphics.FillRectangle(new SolidBrush(m_trackLineColor), r4);
                    BorderSimple.Draw(p, r4, BorderStyleSimple.Sunken, Color.Gray);

                    //Draw Pointer
                    var r3 = new Rectangle(ValueToPixels(m_Value) - m_trackerSize.Width / 2, this.Height - m_Margin - m_trackerSize.Height , m_trackerSize.Width, m_trackerSize.Height);
                    p.Graphics.FillRectangle(new SolidBrush(m_trackerColor), r3);
                    BorderSimple.Draw(p, r3, BorderStyleSimple.Raised, m_trackerColor);



                    Pen TickPen = new Pen(m_TickColor, 1);

                 //   p.Graphics.DrawLine(TickPen, m_PixelsHigh,this.Height- m_Margin - m_TickMajorLength - m_trackerSize.Width, m_PixelsLow, this.Height - m_Margin - m_TickMajorLength - m_trackerSize.Width);


                    foreach (var item in m_ListTickMajor)
                    {
                        p.Graphics.DrawLine(TickPen, ValueToPixels(item.LocationValue),this.Height - m_Margin - m_trackerSize.Height, ValueToPixels(item.LocationValue),this.Height- m_Margin - m_trackerSize.Height - m_TickMajorLength);

                        var r = new Rectangle(ValueToPixels(item.LocationValue) - item.TextSize.Width / 4,this.Height - m_Margin*2 - m_TickMajorLength-m_trackerSize.Height - m_LabelSpace - m_MaxTickTextHeight/2, item.TextSize.Width, item.TextSize.Height);

                        p.Graphics.DrawString(Math.Round(item.LocationValue, m_decimals).ToString("F" + m_decimals.ToString()), this.Font, new SolidBrush(m_TextColor), r);
                    }


                    foreach (var item in m_ListTickMinor)
                    {
                        p.Graphics.DrawLine(TickPen, ValueToPixels(item.LocationValue),this.Height - m_TickMajorLength - m_trackerSize.Height - m_Margin, ValueToPixels(item.LocationValue),this.Height- m_TickMajorLength - m_trackerSize.Height - m_Margin + m_TickMinorLength);
                    }
                }
            }



        }

        private static Point[] GetPointerPoints(Rectangle r, Direction direction)
        {
            if (direction == Direction.Up)
            {
                return new Point[]
                {
                    new Point(r.Left, r.Bottom),
                    new Point(r.Right, r.Bottom),
                    new Point(r.Right, r.Top + r.Width / 2),
                    new Point((r.Left + r.Right) / 2, r.Top),
                    new Point(r.Left, r.Top + r.Width / 2)
                };
            }
            if (direction == Direction.Down)
            {
                return new Point[]
                {
                    new Point(r.Left, r.Top),
                    new Point(r.Right, r.Top),
                    new Point(r.Right, r.Bottom - r.Width / 2),
                    new Point((r.Left + r.Right) / 2, r.Bottom),
                    new Point(r.Left, r.Bottom - r.Width / 2)
                };
            }
            if (direction == Direction.Left)
            {
                return new Point[]
                {
                    new Point(r.Right, r.Top),
                    new Point(r.Right, r.Bottom),
                    new Point(r.Left + r.Height / 2, r.Bottom),
                    new Point(r.Left, (r.Top + r.Bottom) / 2),
                    new Point(r.Left + r.Height / 2, r.Top)
                };
            }
            return new Point[]
            {
                new Point(r.Left, r.Top),
                new Point(r.Left, r.Bottom),
                new Point(r.Right - r.Height / 2, r.Bottom),
                new Point(r.Right, (r.Top + r.Bottom) / 2),
                new Point(r.Right - r.Height / 2, r.Top)
            };
        }


        public int ValueToPixels(double value)
        {
            double num2;
            {
                double num = (value - m_Minimum) / (m_Maximum - m_Minimum);

                num2 = (double)this.m_PixelsLow + num * (double)(m_PixelsHigh-m_PixelsLow);
                
            }
            if (num2 > 32768.0)
            {
                return 32768;
            }
            if (num2 < -32768.0)
            {
                return -32768;
            }
            return (int)Math.Round(num2);
        }


        private Point GetTickPoint(double Value)
        {
            int num = 0;

            int num2 = this.ValueToPixels(Value);
            int num3 = 3;

            return new Point(num3, num2);
        }


        public static bool InRangeDelta(double value, double min, double max)
        {
            return value >= min - Math.Abs(min * 1E-14) && value <= max + Math.Abs(max * 1E-14);
        }

        private double GetStopValue()
        {
            return m_Maximum + m_MajorStepSize;
        }

        private double GetStartValue()
        {
            double num;
            {
                num = 0;
            }
            if (num < m_Minimum)
            {
                double num2 = Math.Round((m_Minimum - num) / m_Minimum);
                num += num2 * m_MajorStepSize;
            }
            else if (num > m_Minimum)
            {
                double num2 = Math.Round((num - m_Minimum) / m_MajorStepSize);
                num -= (num2 + 1.0) * m_MajorStepSize;
            }
            if (num >= m_Minimum)
            {
                num -= m_Minimum;
            }
            return num;
        }


        public bool LabelsFit(double span, double increment)
        {
            double num = span / increment;
            if (num > 1000.0)
            {
                return false;
            }
            this.m_MajorStepSize = increment;
            this.m_MajorCount = (int)num;
            return ( this.m_MajorCount <= this.m_MaxTicks);
        }
        /// <summary>
        /// 重新绘制
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            DrawTicks(e);
            DrawgaugeLinear(e);
            return;
        }

        public double ValueClamped(double value)
        {
            if (value <= m_Minimum)
            {
                return m_Minimum;
            }
            if (value >= m_Maximum)
            {
                return m_Maximum;
            }
            return value;
        }

        public double PixelsToValue(double value)
        {

            double num;

            num = (double)(m_PixelsHigh - value) / (double)(m_PixelsHigh - m_PixelsLow);
            //           }
            if (m_Orientation == Orientation.Vertical)
            {
                return num * (m_Maximum - m_Minimum) + m_Minimum;
            }
            else
            {
                return m_Maximum- num * (m_Maximum - m_Minimum) ;
            }

            //}
        }

        /// <summary>
        /// Raises the ValueChanged event.
        /// </summary>
        /// <param name="value">The new value</param>
        private void OnValueChanged(double value)
        {
            // Any attached event handlers?
            if (ValueChanged != null)
                ValueChanged(this, value);

        }

    }


   
}

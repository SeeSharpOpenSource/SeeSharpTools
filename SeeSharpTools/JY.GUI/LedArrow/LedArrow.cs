using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System;

namespace SeeSharpTools.JY.GUI
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(LedArrow), "LedArrow.LedArrow.bmp")]
    [Designer(typeof(LedArrowDesigner))]
    public partial class LedArrow : UserControl
    {
        public enum LedArrowStyle
        {
            SingleHead,
            DualHead
        }

        public enum DirectionQ
        {
            Right,
            Down,
            Left,
            Up
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

        InteractionStyle _interactionStyle = InteractionStyle.Indicator;

        private DirectionQ _direction;
        private bool _value = true;
        private Timer _timer = new Timer();
        private LedArrowStyle _styles;
        //InteractionStyle _interactionStyle = InteractionStyle.Indicator;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the color of the LED light turn on
        /// </summary>
        [Category("JYTek")]
        [Description("Gets or Sets the color of the LED light turn on.")]
        public Color OnColor
        {
            get { return _oncolor; }
            set
            {
                _oncolor = value;
                this.Invalidate();  // Redraw the control
            }
        }
        /// <summary>
        /// Gets or Sets the color of the LED light turn off
        /// </summary>
        [Category("JYTek")]
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
                this.Invalidate();  // Redraw the control
            }
        }

        /// <summary>
        /// Gets or Sets whether the light is turned on
        /// </summary>
        [Category("JYTek")]
        [Description("Gets or Sets whether the light is turned on.")]
        public bool Value
        {
            get { return _value; }
            set { _value = value; this.Invalidate(); }
        }

        /// <summary>
        /// Gets or Sets styles of the light is turned on
        /// </summary>
        [Category("JYTek")]
        [Description("Gets or Sets the style of the LED light.")]
        public LedArrowStyle Style
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
        [Category("JYTek")]
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
                this.Invalidate();
            }
        }
        /// <summary>
        /// Gets or Sets state of the light blink
        /// </summary>
        [Category("JYTek")]
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
        [Category("JYTek")]
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
        /// rotate the Shape
        /// </summary>
        [Category("JYTek")]
        [Description("Gets or Sets the Rotation of control.")]
        public DirectionQ Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                DirectionQ _prerotation = _direction;
                _direction = value;
                if (NeedsSizeSwap(_prerotation))
                {
                    this.Size = new Size(base.Height, base.Width);
                }                
                this.Invalidate();
            }
        }

        /// <summary>
        /// Set the interaction of led
        /// </summary>
        [Category("JYTek")]
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
        public LedArrow()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);



            this.Cursor = Cursors.Arrow;
            this.Size = new Size(60, 30);
            this.Value = false;

            this.OnColor = Color.Lime;
            this.OffColor = Color.DarkGreen;
            this.BlinkColor = Color.Lime;
            this.Style = LedArrowStyle.SingleHead;
            _direction = DirectionQ.Right;
            this.Resize += new EventHandler(LedArrow_Resize);

            _timer.Interval = _blinkInterval;
            _timer.Tick += delegate (object o, EventArgs args)
            {
                LedArrowBlink();
            };
            
        }

        private void LedArrow_Resize(object sender, EventArgs e)
        {
            if (_direction == DirectionQ.Right || _direction == DirectionQ.Left)
            {
                if (_styles == LedArrowStyle.SingleHead)
                {
                    if (this.Height - this.Width >= 5)
                    {
                        this.Height = this.Width + 5;
                    }
                }
                else
                {
                    if (this.Height * 2 - this.Width >= 5)
                    {
                        this.Height = (this.Width - 5) / 2;
                    }
                }
            }
            else
            {
                if (_styles == LedArrowStyle.SingleHead)
                {
                    if (this.Width - this.Height >= 5)
                    {
                        this.Width = this.Height + 5;
                    }
                }
                else
                {
                    if (this.Width * 2 - this.Height >= 5)
                    {
                        this.Width = (this.Height - 5) / 2;
                    }
                }
            }
        }

        private void LedArrowBlink()
        {
            _blink = !_blink;
            this.Invalidate();
        }

        private void DrawSingleHead(PaintEventArgs p, Brush brush)
        {
            int num ;
            
            //iColors.FaceColorLight = SystemColors.ControlLightLight;
            //iColors.FaceColorDark = SystemColors.ControlDarkDark;
            var r = new Rectangle(new Point(0, 0), this.Size);
            Point[] array = new Point[7];
            Color[] array2 = new Color[7];
            switch (_direction)
            {
                case DirectionQ.Right:
                    num = this.Height - 1;
                    array[0] = new Point(r.Left, r.Top + num / 3);
                    array[1] = new Point(r.Right - num, r.Top + num / 3);
                    array[2] = new Point(r.Right - num, r.Top);
                    array[3] = new Point(r.Right, r.Top + num / 2);
                    array[4] = new Point(r.Right - num, r.Top + num);
                    array[5] = new Point(r.Right - num, r.Top + num * 2 / 3);
                    array[6] = new Point(r.Left, r.Top + num * 2 / 3); 
                    break;
                case DirectionQ.Down:
                    num = this.Width - 1;
                    array[0] = new Point(r.Left + num / 3, r.Top);
                    array[1] = new Point(r.Left + num / 3, r.Bottom - num);
                    array[2] = new Point(r.Left, r.Bottom - num);
                    array[3] = new Point(r.Right/ 2, r.Bottom);
                    array[4] = new Point(r.Right, r.Bottom - num);
                    array[5] = new Point(r.Right - num / 3, r.Bottom - num);
                    array[6] = new Point(r.Right - num / 3, r.Top);
                    break;
                case DirectionQ.Left:
                    num = this.Height - 1;
                    array[0] = new Point(r.Right, r.Top + num / 3);
                    array[1] = new Point(r.Left + num, r.Top + num / 3);
                    array[2] = new Point(r.Left + num, r.Top);
                    array[3] = new Point(r.Left, r.Top + num / 2);
                    array[4] = new Point(r.Left + num, r.Top + num);
                    array[5] = new Point(r.Left + num, r.Top + num * 2 / 3);
                    array[6] = new Point(r.Right, r.Top + num * 2 / 3);
                    break;
                case DirectionQ.Up:
                    num = this.Width - 1;
                    array[0] = new Point(r.Left + num / 3, r.Bottom);
                    array[1] = new Point(r.Left + num / 3, r.Top + num);
                    array[2] = new Point(r.Left, r.Top + num);
                    array[3] = new Point(r.Left + num / 2, r.Top);
                    array[4] = new Point(r.Right, r.Top + num);
                    array[5] = new Point(r.Right - num / 3, r.Top + num);
                    array[6] = new Point(r.Right - num / 3, r.Bottom);
                    break;
                default:
                    break;
            }

            
            p.Graphics.FillPolygon(brush, array);
        }


        private void DrawDualHead(PaintEventArgs p, Brush brush)
        {
            int num;
            var r = new Rectangle(new Point(0, 0), this.Size);
            Point[] array = new Point[10];
            switch (_direction)
            {
                case DirectionQ.Right:
                case DirectionQ.Left:
                    num = this.Height - 1;
                    array[0] = new Point(r.Left, r.Top + num / 2);
                    array[1] = new Point(r.Left + num, r.Top);
                    array[2] = new Point(r.Left + num, r.Top + num / 3);
                    array[3] = new Point(r.Right - num, r.Top + num / 3);
                    array[4] = new Point(r.Right - num, r.Top);
                    array[5] = new Point(r.Right, r.Top + num / 2);
                    array[6] = new Point(r.Right - num, r.Bottom);
                    array[7] = new Point(r.Right - num, r.Top + num * 2 / 3);
                    array[8] = new Point(r.Left + num, r.Top + num * 2 / 3);
                    array[9] = new Point(r.Left + num, r.Bottom);
                    break;
                case DirectionQ.Down:
                case DirectionQ.Up:
                    num = this.Width - 1;
                    array[0] = new Point(r.Left+num/2, r.Top );
                    array[1] = new Point(r.Left, r.Top +num);
                    array[2] = new Point(r.Left + num/3, r.Top + num);
                    array[3] = new Point(r.Left + num/3, r.Bottom - num);
                    array[4] = new Point(r.Left , r.Bottom-num);
                    array[5] = new Point(r.Left+num/2, r.Bottom);
                    array[6] = new Point(r.Right, r.Bottom-num);
                    array[7] = new Point(r.Right - num/3, r.Bottom - num);
                    array[8] = new Point(r.Right - num/3, r.Top + num );
                    array[9] = new Point(r.Right , r.Top+num);
                    break;
                default:
                    break;
            }

            p.Graphics.FillPolygon(brush, array);
        }
        /// <summary>
        /// 重新绘制
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Brush brush;
            //如果闪烁开始
            if (_blinkOn)
            {
                if (_blink)
                {
                    brush = new SolidBrush(_blinkColor);
                }
                else
                {
                    brush = new SolidBrush(_offcolor);
                }
            }
            //闪烁关闭
            else
            {
                if (_value)
                {
                    brush = new SolidBrush(_oncolor);
                }
                else
                {
                     brush = new SolidBrush(_offcolor);
                }
            }
            //选择形状，单向还是双向
            if (_styles == LedArrowStyle.SingleHead)
            {
                this.DrawSingleHead(e, brush);
            }
            else
            {

                 DrawDualHead(e, brush);
            }


            return;
        }
        /// <summary>
        /// 判断一下是否需要Size的反转
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool NeedsSizeSwap(DirectionQ value)
        {
            if (value == this._direction)
            {
                return false;
            }
            bool flag = this._direction == DirectionQ.Right || this._direction == DirectionQ.Left;
            bool flag2 = value == DirectionQ.Right || value == DirectionQ.Left;
            return flag != flag2;
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
    }
}

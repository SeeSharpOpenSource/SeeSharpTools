using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{

    [DefaultEvent("ValueChanged")]
    [ToolboxBitmap(typeof(IndustrySwitch), "IndustrySwitch.IndustrySwitch.bmp")]
    [Designer(typeof(IndustySwitchDesigner))]
    public partial class IndustrySwitch : UserControl
    {
        /// <summary>
        /// In
        /// </summary>
        public IndustrySwitch()
        {
            InitializeComponent();
            this.Name = "";
            //设置Style支持透明背景色并且双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
            //鼠标移动到控件时变成手型
            this.Cursor = Cursors.Hand;

            _oncolor = Color.Silver;
            _offcolor = Color.Silver;
        }
        private Color _oncolor;
        private Color _offcolor;

        private static readonly Color RockerShadingColor = Color.FromArgb(60, Color.Black);
        private static readonly Color RockerLightColor = Color.FromArgb(60, Color.White);
        private static readonly Color PushbuttonShadingDark = Color.FromArgb(60, Color.Black);
        private static readonly Color PushbuttonShadingLight = Color.FromArgb(60, Color.White);
        private static readonly Color SlideShadingDark = Color.FromArgb(60, Color.Black);
        private static readonly Color SlideShadingLight = Color.FromArgb(60, Color.White);

        private static readonly Color SwitchShadingDark = Color.FromArgb(50, Color.Black);

        private static readonly Color SwitchShadingLight = Color.FromArgb(200, Color.White);

        private static readonly Color SwitchOutline = Color.FromArgb(40, Color.Black);
        bool isCheck = false;
        InteractionStyle _interactionStyle = InteractionStyle.SwitchWhenPressed;
        /// <summary>
        /// 是否选中
        /// </summary>
        [Category("Behavior")]
        public bool Value
        {
            set {
                if (isCheck != value)
                {
                    isCheck = value;

                    if (ValueChanged != null)
                    {
                        //TODO
                        ValueChanged(this, null);
                    }
                    this.Invalidate();
                }
            }

            get { return isCheck; }
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
        /// <summary>
        /// Instrudsry Switch Styles
        /// </summary>
        public enum SwitchStyles
        {
            /// <summary>
            /// 垂直工业开关
            /// </summary>
            Vertical,
            /// <summary>
            /// 水平工业开关
            /// </summary>
            Horizontal,

            HorizontalFlatButton,

            VerticalFlatButton,

            VerticalSlider,

            HorizontalSlider,

            PushButton



        };
        SwitchStyles checkStyle = SwitchStyles.Vertical;

        /// <summary>
        /// 样式
        /// </summary>
        [Category("Appearance")]
        public SwitchStyles Style
        {
            set { checkStyle = value;
                this.Invalidate(); }
            get { return checkStyle; }
        }
        /// <summary>
        /// Set the interaction of industryswitch
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
        /// <summary>
        /// 打开颜色
        /// </summary>
        [Category("Appearance")]
        public Color OnColor
        {
            get
            {
                return _oncolor;
            }

            set
            {
                _oncolor = value;
            }
        }
        /// <summary>
        /// 关闭颜色
        /// </summary>
        [Category("Appearance")]
        public Color OffColor
        {
            get
            {
                return _offcolor;
            }

            set
            {
                _offcolor = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            switch (checkStyle)
            {
                case SwitchStyles.Vertical:
                    VerticalToggleDraw(e.Graphics, isCheck);
                    break;
                case SwitchStyles.Horizontal:
                    HorizontalToggle(e.Graphics, isCheck);
                    break;
                case SwitchStyles.VerticalSlider:
                    VerticalSlideDraw(e.Graphics, isCheck);
                    break;
                case SwitchStyles.HorizontalSlider:
                    HorizontalSlideDraw(e.Graphics, isCheck);
                    break;
                case SwitchStyles.PushButton:
                    DrawPushButton(e.Graphics, isCheck);
                    break;
                case SwitchStyles.VerticalFlatButton:
                    DrawVerticalRocker(e.Graphics, isCheck);
                    break;
                case SwitchStyles.HorizontalFlatButton:
                    DrawHorizontalRocker(e.Graphics, isCheck);
                    break;
                default:
                    break;
            }
        }
        #region RockerStyleDraw
        public void DrawVerticalRocker(Graphics graphics,bool IsChecked)
        {
            Color CurrentColor;
            int rotation = 180;
            bool mirror = false;
            if (!IsChecked)
            {
                CurrentColor = _offcolor;
                mirror = true;
                rotation = 0;
            }
            else
            {
                CurrentColor = _oncolor;
            }
            DrawRocker(graphics, new Rectangle(new Point(0,0), new Size(this.Width, this.Height)), CurrentColor, rotation, mirror);
        }
        public void DrawHorizontalRocker(Graphics graphics, bool IsChecked)
        {
            Color CurrentColor;
            int rotation = 90;
            bool mirror = true;
            if (!IsChecked)
            {
                CurrentColor = _offcolor;
                mirror = false;
                rotation = 270;
            }
            else
            {
                CurrentColor = _oncolor;
            }
            DrawRocker(graphics, new Rectangle(new Point(0, 0), new Size(this.Width, this.Height)), CurrentColor, rotation, mirror);
        }


        internal static void DrawRocker(Graphics graphics_0, Rectangle bounds, Color color, int rotation, bool mirror)
        {
            GraphicsPath graphicsPath = CreateRockerTop(bounds, rotation, mirror);
            GraphicsPath graphicsPath2 = CreateRockerSide(bounds, rotation, mirror);
            GraphicsPath graphicsPath3 = CreateRockerFront(bounds, rotation, mirror);
            GraphicsPath graphicsPath4 = CreateRockerBottom(bounds, rotation, mirror);
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                PinTopLeftCorner(bounds, graphicsPath, graphicsPath2, graphicsPath3, graphicsPath4);
                graphicsPath4 = AlignRockerPaths(graphicsPath3, graphicsPath4, graphicsPath2, true);
            }
            else
            {
                PinTopLeftCorner(bounds, graphicsPath, graphicsPath2, graphicsPath3, graphicsPath4);
                graphicsPath4 = AlignRockerPaths(graphicsPath3, graphicsPath4, graphicsPath2, false);
            }
            RectangleF bounds2 = graphicsPath.GetBounds();
            RectangleF bounds3 = graphicsPath2.GetBounds();
            RectangleF bounds4 = graphicsPath3.GetBounds();
            RectangleF bounds5 = graphicsPath4.GetBounds();
            Color color2 = Color.FromArgb(60, Color.White); ;
            if (rotation > 45 && rotation < 225)
            {
                color2 = Color.FromArgb(60, Color.Black);
            }
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new PointF(bounds2.Left, bounds2.Top), new PointF(bounds2.Right, bounds2.Bottom), color2, RockerShadingColor))
            {
                using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new PointF(bounds3.Left, bounds3.Top), new PointF(bounds3.Right, bounds3.Bottom), RockerLightColor, RockerLightColor))
                {
                    using (LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(new PointF(bounds4.Left, bounds4.Top), new PointF(bounds4.Right, bounds4.Bottom), RockerLightColor, RockerShadingColor))
                    {
                        using (LinearGradientBrush linearGradientBrush4 = new LinearGradientBrush(new PointF(bounds5.Left, bounds5.Top), new PointF(bounds5.Right, bounds5.Bottom), RockerLightColor, RockerShadingColor))
                        {
                            using (SolidBrush solidBrush = new SolidBrush(color))
                            {
                                using (Pen pen = new Pen(Color.FromArgb(60, Color.Black), 1f))
                                {
                                    graphics_0.FillPath(solidBrush, graphicsPath);
                                    graphics_0.FillPath(solidBrush, graphicsPath3);
                                    graphics_0.FillPath(solidBrush, graphicsPath4);
                                    graphics_0.FillPath(linearGradientBrush, graphicsPath);
                                    graphics_0.FillPath(linearGradientBrush3, graphicsPath3);
                                    graphics_0.FillPath(linearGradientBrush4, graphicsPath4);
                                    graphics_0.DrawPath(pen, graphicsPath);
                                    graphics_0.DrawPath(pen, graphicsPath3);
                                    graphics_0.DrawPath(pen, graphicsPath4);
                                    graphics_0.FillPath(solidBrush, graphicsPath2);
                                    graphics_0.FillPath(linearGradientBrush2, graphicsPath2);
                                    graphics_0.DrawPath(pen, graphicsPath2);
                                }
                            }
                        }
                    }
                }
            }
            graphicsPath.Dispose();
            graphicsPath = null;
            graphicsPath2.Dispose();
            graphicsPath2 = null;
            graphicsPath3.Dispose();
            graphicsPath3 = null;
            graphicsPath4.Dispose();
            graphicsPath4 = null;
        }
        internal static GraphicsPath AlignRockerPaths(GraphicsPath front, GraphicsPath bottom, GraphicsPath side, bool isHorizontalRocker)
        {
            PointF[] array = new PointF[5];
            if (isHorizontalRocker)
            {
                MoveSidePathOnePixelDown(front, side);
                array[0] = front.PathPoints[3];
                array[1] = new PointF(front.PathPoints[2].X, side.PathPoints[4].Y);
                array[2] = side.PathPoints[3];
                array[3] = new PointF(side.PathPoints[3].X, front.PathPoints[3].Y);
            }
            else
            {
                array[0] = front.PathPoints[3];
                array[1] = new PointF(side.PathPoints[4].X, front.PathPoints[2].Y);
                array[2] = side.PathPoints[3];
                array[3] = new PointF(front.PathPoints[3].X, side.PathPoints[3].Y);
            }
            array[4] = array[0];
            return new GraphicsPath(array, bottom.PathTypes);
        }
        internal static void MoveSidePathOnePixelDown(GraphicsPath front, GraphicsPath side)
        {
            if (Point.Round(side.PathPoints[1]).Y != Point.Round(front.PathPoints[2]).Y)
            {
                Matrix matrix = new Matrix();
                matrix.Translate(0f, -1f);
                side.Transform(matrix);
            }
        }

        internal static GraphicsPath CreateRockerTop(Rectangle bounds, int rotation, bool mirror)
        {
            Rectangle rectangle = CreateRockerTopRectangle(bounds, rotation);
            int num = bounds.Width;
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                num = bounds.Height;
            }
            return CreatePathFromRegion(new Point[]
            {
                new Point(rectangle.Left + num / 6, rectangle.Top),
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Right - num / 6, rectangle.Bottom),
                new Point(rectangle.Left, rectangle.Bottom)
            }, bounds, rotation, mirror, false);
        }

        internal static Rectangle CreateRockerTopRectangle(Rectangle bounds, int rotation)
        {
            Rectangle rectangle = default(Rectangle);
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                rectangle = new Rectangle(bounds.Left + bounds.Width / 2 - bounds.Height / 2, bounds.Top + bounds.Height / 2 - bounds.Width / 2 + 1, bounds.Height - 2, bounds.Width / 10);
            }
            else
            {
                rectangle = new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2, bounds.Height / 10);
            }
            //  rectangle = SwitchStyle.CheckRectangleSize(rectangle);
            return rectangle;
        }

        internal static GraphicsPath CreatePathFromRegion(Point[] points, Rectangle bounds, int rotation, bool mirror, bool horizontal)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            for (int i = 0; i < points.Length - 1; i++)
            {
                graphicsPath.AddLine(points[i], points[i + 1]);
            }
            graphicsPath.AddLine(points[points.Length - 1], points[0]);
            if (mirror)
            {
                Matrix matrix = new Matrix(-1f, 0f, 0f, 1f, (float)bounds.Width, 0f);
                graphicsPath.Transform(matrix);
            }
            if (mirror || rotation != 0)
            {
                Matrix matrix2 = default(Matrix);
                if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
                {
                    if (horizontal)
                    {
                        matrix2 = new Matrix(1f, 0f, 0f, 1f, (float)bounds.Left, (float)(-(float)bounds.Top - 1));
                    }
                    else
                    {
                        matrix2 = new Matrix(1f, 0f, 0f, 1f, (float)bounds.Left, (float)(bounds.Top - 1));
                    }
                }
                else
                {
                    matrix2 = new Matrix(1f, 0f, 0f, 1f, (float)(2 * bounds.Left), 0f);
                }
                matrix2.RotateAt((float)rotation, new Point(bounds.Width / 2, bounds.Height / 2 + bounds.Top));
                graphicsPath.Transform(matrix2);
            }
            return graphicsPath;
        }

        internal static GraphicsPath CreateRockerSide(Rectangle bounds, int rotation, bool mirror)
        {
            Rectangle rectangle = CreateRockerSideRectangle(bounds, rotation);
            int num = bounds.Width;
            int num2 = bounds.Height;
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                num = bounds.Height;
                num2 = bounds.Width;
            }
            return CreatePathFromRegion(new Point[]
            {
                new Point(rectangle.Left, rectangle.Top + num2 / 10),
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Right, rectangle.Bottom - num2 / 24),
                new Point(rectangle.Right - num / 24, rectangle.Bottom),
                new Point(rectangle.Right - num / 24, rectangle.Bottom - num2 / 2)
            }, bounds, rotation, mirror, false);
        }

        internal static Rectangle CreateRockerSideRectangle(Rectangle bounds, int rotation)
        {
            Rectangle rectangle = default(Rectangle);
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                rectangle = new Rectangle(bounds.Left + bounds.Width / 2 + bounds.Height / 2 - bounds.Height / 6 - 2, bounds.Top + bounds.Height / 2 - bounds.Width / 2 + 1, bounds.Height / 6, bounds.Width - 2);
            }
            else
            {
                rectangle = new Rectangle(bounds.Right - bounds.Width / 6 - 1, bounds.Top + 1, bounds.Width / 6, bounds.Height - 2);
            }
            //  rectangle = SwitchStyle.CheckRectangleSize(rectangle);
            return rectangle;
        }

        internal static GraphicsPath CreateRockerFront(Rectangle bounds, int rotation, bool mirror)
        {
            Rectangle rectangle = CreateRockerFrontRectangle(bounds, rotation);
            int num = bounds.Width;
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                num = bounds.Height;
            }
            return CreatePathFromRegion(new Point[]
            {
                new Point(rectangle.Left, rectangle.Top + 1),
                new Point(rectangle.Right - num / 6, rectangle.Top + 1),
                new Point(rectangle.Right, rectangle.Bottom),
                new Point(rectangle.Left + num / 6, rectangle.Bottom)
            }, bounds, rotation, mirror, false);
        }

        internal static Rectangle CreateRockerFrontRectangle(Rectangle bounds, int rotation)
        {
            Rectangle rectangle = default(Rectangle);
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                rectangle = new Rectangle(bounds.Left + bounds.Width / 2 - bounds.Height / 2, bounds.Top + bounds.Height / 2 - bounds.Width / 2 + bounds.Width / 10, bounds.Height - 2, bounds.Width / 2 - bounds.Width / 10);
            }
            else
            {
                rectangle = new Rectangle(bounds.Left + 1, bounds.Top + bounds.Height / 10, bounds.Width - 2, bounds.Height / 2 - bounds.Height / 10);
            }
            //  rectangle = SwitchStyle.CheckRectangleSize(rectangle);
            return rectangle;
        }

        internal static GraphicsPath CreateRockerBottom(Rectangle bounds, int rotation, bool mirror)
        {
            Rectangle rectangle = CreateRockerBottomRectangle(bounds, rotation);
            return CreatePathFromRegion(new Point[]
            {
                new Point(rectangle.Left, rectangle.Top),
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Right, rectangle.Bottom),
                new Point(rectangle.Left, rectangle.Bottom)
            }, bounds, rotation, mirror, false);
        }

        internal static Rectangle CreateRockerBottomRectangle(Rectangle bounds, int rotation)
        {
            Rectangle rectangle = default(Rectangle);
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                rectangle = new Rectangle(bounds.Left + bounds.Width / 2 - bounds.Height / 2 + bounds.Height / 6, bounds.Bottom - bounds.Height / 2 - 1, bounds.Height - 2 - bounds.Height / 6 - bounds.Height / 24, bounds.Width / 2 - 1);
            }
            else
            {
                rectangle = new Rectangle(bounds.Left + bounds.Width / 6 + 1, bounds.Bottom - bounds.Height / 2 - 1, bounds.Width - 2 - bounds.Width / 6 - bounds.Width / 24, bounds.Height / 2 - 1);
            }
            return rectangle;
        }

        internal static void PinTopLeftCorner(Rectangle bounds, GraphicsPath aPath, GraphicsPath bPath, GraphicsPath cPath, GraphicsPath dPath)
        {
            RectangleF a = Rectangle.Empty;
            Matrix matrix = new Matrix();
            a = RectangleF.Union(aPath.GetBounds(), aPath.GetBounds());
            a = RectangleF.Union(a, bPath.GetBounds());
            a = RectangleF.Union(a, cPath.GetBounds());
            a = RectangleF.Union(a, dPath.GetBounds());
            int num = bounds.X - (int)Math.Round(a.X);
            int num2 = bounds.Y - (int)Math.Round(a.Y);
            matrix.Translate((float)(num + 1), (float)(num2 + 1));
            aPath.Transform(matrix);
            bPath.Transform(matrix);
            cPath.Transform(matrix);
            dPath.Transform(matrix);
        }
        #endregion
        
        #region PushButtonDraw
        public void DrawPushButton(Graphics graphics, bool IsChecked)
        {
            Color CurrentColor;
            int offset = 6;
            if (IsChecked)
            {
                offset = 20;
                CurrentColor = _oncolor;
            }
            else
            {
                CurrentColor = _offcolor;
            }
            Rectangle Bounds = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
            GraphicsPath graphicsPath = CreatePushButtonEnd(Bounds, offset);
            GraphicsPath graphicsPath2 = CreatePushButtonBase(Bounds);
            GraphicsPath graphicsPath3 = CreatePushButtonShaft(Bounds);
            RectangleF bounds = graphicsPath.GetBounds();
            RectangleF bounds2 = graphicsPath2.GetBounds();
            RectangleF bounds3 = graphicsPath3.GetBounds();
            using (GraphicsPath graphicsPath4 = CreatePathFromRegion(new Point[]
            {
                    new Point((int)bounds3.Left, (int)bounds3.Top + (int)bounds3.Height / 2),
                    new Point((int)bounds3.Right, (int)bounds3.Top + (int)bounds3.Height / 2),
                    new Point((int)bounds.Right, (int)bounds.Top + (int)bounds.Height / 2),
                    new Point((int)bounds.Left, (int)bounds.Top + (int)bounds.Height / 2)
            }, Bounds, 0, false, false))
            {
                using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new PointF(bounds.Left, bounds.Top), new PointF(bounds.Right, bounds.Bottom), PushbuttonShadingLight, PushbuttonShadingDark))
                {
                    using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new PointF(bounds3.Left, bounds3.Top), new PointF(bounds3.Right, bounds3.Top), PushbuttonShadingLight, PushbuttonShadingDark))
                    {
                        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath2))
                        {
                            using (SolidBrush solidBrush = new SolidBrush(CurrentColor))
                            {
                                using (Pen pen = new Pen(RockerShadingColor, 1f))
                                {
                                    Color[] surroundColors = new Color[]
                                    {
                                            Color.FromArgb(40, Color.Black)
                                };
                                    pathGradientBrush.CenterColor = Color.White;
                                    pathGradientBrush.SurroundColors = surroundColors;
                                    pathGradientBrush.CenterPoint = new Point((int)bounds2.Right - (int)bounds2.Width / 4, (int)bounds2.Bottom - (int)bounds2.Height / 4);
                                    graphics.FillPath(pathGradientBrush, graphicsPath2);
                                    graphics.FillPath(solidBrush, graphicsPath3);
                                    graphics.FillPath(linearGradientBrush2, graphicsPath3);
                                    graphics.DrawPath(pen, graphicsPath3);
                                    graphics.FillPath(solidBrush, graphicsPath4);
                                    graphics.FillPath(linearGradientBrush2, graphicsPath4);
                                    graphics.DrawLine(pen, bounds3.Left, bounds3.Top + bounds3.Height / 2f, bounds.Left, bounds.Top + bounds.Height / 2f);
                                    graphics.DrawLine(pen, bounds3.Right, bounds3.Top + bounds3.Height / 2f, bounds.Right, bounds.Top + bounds.Height / 2f);
                                    graphics.FillPath(solidBrush, graphicsPath);
                                    graphics.FillPath(linearGradientBrush, graphicsPath);
                                    graphics.DrawPath(pen, graphicsPath);
                                }
                            }
                        }
                    }
                }
            }
            graphicsPath.Dispose();
            graphicsPath = null;
            graphicsPath3.Dispose();
            graphicsPath3 = null;
            graphicsPath2.Dispose();
            graphicsPath2 = null;
        }

        internal static GraphicsPath CreatePushButtonEnd(Rectangle bounds, int offset)
        {
            Rectangle rectangle = new Rectangle(bounds.Left + bounds.Width / 12, bounds.Top + bounds.Height / 12 + bounds.Height / offset, bounds.Width - 2 * bounds.Width / 12, bounds.Height - bounds.Height / 12 - 2 - bounds.Height / 6);
            //  rectangle = SwitchStyle.CheckRectangleSize(rectangle);
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(rectangle);
            return graphicsPath;
        }

        internal static GraphicsPath CreatePushButtonBase(Rectangle bounds)
        {
            Rectangle rectangle = new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2, bounds.Height - 2 - bounds.Height / 6);

            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(rectangle);
            return graphicsPath;
        }
        internal static GraphicsPath CreatePushButtonShaft(Rectangle bounds)
        {
            Rectangle rectangle = new Rectangle(bounds.Left + bounds.Width / 12, bounds.Top + bounds.Height / 12, bounds.Width - 2 * bounds.Width / 12, bounds.Height - bounds.Height / 12 - 2 - bounds.Height / 6);
            //    rectangle = SwitchStyle.CheckRectangleSize(rectangle);
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(rectangle);
            return graphicsPath;
        }
        #endregion

        #region SliderDraw

        public void VerticalSlideDraw(Graphics graphics, bool IsChecked)
        {
            Color CurrentColor;
            int rotation = 270;
            bool mirror = true;
            int offset = 0;
            Rectangle bounds = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
            if (!IsChecked)
            {
                offset = bounds.Height / 2;
                CurrentColor = _offcolor;
            }
            else
            {
                CurrentColor = _oncolor;
            }
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.Left, bounds.Top), new Point(bounds.Left, bounds.Top + (int)Math.Max((double)(Math.Max(bounds.Width, bounds.Height) / 40), 2.0)), SlideShadingLight, SlideShadingDark);
            DrawSlide(graphics, bounds, linearGradientBrush, offset, rotation, mirror, CurrentColor);
            linearGradientBrush.Dispose();
        }

        public void HorizontalSlideDraw(Graphics graphics, bool IsChecked)
        {
            Color CurrentColor;
            int rotation = 0;
            bool mirror = false;
            int offset = 0;
            Rectangle bounds = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
            if (!IsChecked)
            {
                offset = bounds.Width / 2;
                CurrentColor = _offcolor;
            }
            else
            {
                CurrentColor= _oncolor;
            }
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.Left, bounds.Top), new Point(bounds.Left, bounds.Top + (int)Math.Max((double)(Math.Max(bounds.Width, bounds.Height) / 40), 2.0)), SlideShadingLight, SlideShadingDark);
            DrawSlide(graphics, bounds, linearGradientBrush, offset, rotation, mirror, CurrentColor);
            linearGradientBrush.Dispose();
        }

        internal static void DrawSlide(Graphics graphics, Rectangle bou, LinearGradientBrush stemBrush, int offset, int rotation, bool mirror,Color drawcolor)
        {
            GraphicsPath graphicsPath = CreateSlideBase(bou, rotation, mirror);
            GraphicsPath graphicsPath2 = CreateSlideStem(bou, offset, rotation, mirror);
            GraphicsPath graphicsPath3 = CreateSlideStemTop(bou, offset, rotation, mirror);
            GraphicsPath graphicsPath4 = CreateSlideStemLeft(bou, offset, rotation, mirror);
            PinTopLeftCorner(bou, graphicsPath, graphicsPath2, graphicsPath3, graphicsPath4);
            RectangleF bounds = graphicsPath.GetBounds();
            RectangleF bounds2 = graphicsPath4.GetBounds();
            RectangleF bounds3 = graphicsPath3.GetBounds();
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point((int)bounds.Right, (int)bounds.Bottom), new Point((int)bounds.Left, (int)bounds.Top), SlideShadingDark, SlideShadingLight))
            {
                using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Point((int)bounds2.Left, (int)bounds2.Top), new Point((int)bounds2.Left, (int)bounds2.Bottom), SlideShadingLight, SlideShadingDark))
                {
                    using (LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(new Point((int)bounds3.Left, (int)bounds3.Top), new Point((int)bounds3.Right, (int)bounds3.Top), SlideShadingLight, SlideShadingDark))
                    {
                        using (Pen pen = new Pen(Color.White, 1f))
                        {
                            using (Pen pen2 = new Pen(Color.Black, 1f))
                            {
                                using (Pen pen3 = new Pen(SlideShadingLight, 1f))
                                {
                                    using (Pen pen4 = new Pen(SlideShadingDark, 1f))
                                    {
                                        using (SolidBrush solidBrush = new SolidBrush(drawcolor))
                                        {
                                            using (Pen pen5 = new Pen(Color.Black, 1f))
                                            {
                                                stemBrush.WrapMode = WrapMode.TileFlipX;
                                                graphics.FillPath(solidBrush, graphicsPath);
                                                graphics.FillPath(linearGradientBrush, graphicsPath);
                                                graphics.DrawLine(pen, bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom);
                                                graphics.DrawLine(pen, bounds.Right, bounds.Bottom, bounds.Right, bounds.Top);
                                                graphics.DrawLine(pen2, bounds.Right, bounds.Top, bounds.Left, bounds.Top);
                                                graphics.DrawLine(pen2, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom);
                                                graphics.DrawLine(pen3, bounds.Left + 1f, bounds.Bottom - 1f, bounds.Right - 1f, bounds.Bottom - 1f);
                                                graphics.DrawLine(pen3, bounds.Right - 1f, bounds.Bottom - 1f, bounds.Right - 1f, bounds.Top + 1f);
                                                graphics.DrawLine(pen4, bounds.Right - 2f, bounds.Top + 1f, bounds.Left + 1f, bounds.Top + 1f);
                                                graphics.DrawLine(pen4, bounds.Left + 1f, bounds.Top + 1f, bounds.Left + 1f, bounds.Bottom - 2f);
                                                graphics.FillPath(solidBrush, graphicsPath2);
                                                graphics.FillPath(solidBrush, graphicsPath4);
                                                graphics.FillPath(solidBrush, graphicsPath3);
                                                graphics.FillPath(stemBrush, graphicsPath2);
                                                graphics.FillPath(linearGradientBrush3, graphicsPath3);
                                                graphics.FillPath(linearGradientBrush2, graphicsPath4);
                                                graphics.DrawPath(pen5, graphicsPath2);
                                                graphics.DrawPath(pen5, graphicsPath4);
                                                graphics.DrawPath(pen5, graphicsPath3);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        internal static GraphicsPath CreateSlideStemLeft(Rectangle bounds, int offset, int rotation, bool mirror)
        {
            Rectangle rectangle_ = default(Rectangle);
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                rectangle_ = new Rectangle(bounds.Left + bounds.Width / 2 - bounds.Height / 2 + offset + 2, bounds.Top + bounds.Height / 2 - bounds.Width / 2 + 2, Math.Min(bounds.Height, bounds.Width) / 12, bounds.Width - 4);
            }
            else
            {
                rectangle_ = new Rectangle(bounds.Left + 2 + offset, bounds.Top + 2, Math.Min(bounds.Height, bounds.Width) / 12, bounds.Height - 4);
            }
            //  rectangle_ = SwitchStyle.CheckRectangleSize(rectangle_);
            return CreatePathFromRegion(new Point[]
            {
                new Point(rectangle_.Left, rectangle_.Top),
                new Point(rectangle_.Right, rectangle_.Top + Math.Min(bounds.Height, bounds.Width) / 12),
                new Point(rectangle_.Right, rectangle_.Bottom),
                new Point(rectangle_.Left, rectangle_.Bottom - Math.Min(bounds.Height, bounds.Width) / 12)
            }, bounds, rotation, mirror, true);
        }

        internal static GraphicsPath CreateSlideStemTop(Rectangle bounds, int offset, int rotation, bool mirror)
        {
            Rectangle rectangle_ = default(Rectangle);
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                rectangle_ = new Rectangle(bounds.Left + bounds.Width / 2 - bounds.Height / 2 + offset + 2, bounds.Top + bounds.Height / 2 - bounds.Width / 2 + 2, bounds.Height - 4 - bounds.Height / 2, Math.Min(bounds.Height, bounds.Width) / 12);
            }
            else
            {
                rectangle_ = new Rectangle(bounds.Left + 2 + offset, bounds.Top + 2, bounds.Width - 4 - bounds.Width / 2, Math.Min(bounds.Height, bounds.Width) / 12);
            }
            //  rectangle_ = CheckRectangleSize(rectangle_);
            return CreatePathFromRegion(new Point[]
            {
                new Point(rectangle_.Left, rectangle_.Top),
                new Point(rectangle_.Right - Math.Min(bounds.Height, bounds.Width) / 12, rectangle_.Top),
                new Point(rectangle_.Right, rectangle_.Bottom),
                new Point(rectangle_.Left + Math.Min(bounds.Height, bounds.Width) / 12, rectangle_.Bottom)
            }, bounds, rotation, mirror, true);
        }
        internal static GraphicsPath CreateSlideBase(Rectangle bounds, int rotation, bool mirror)
        {
            Rectangle rectangle_ = default(Rectangle);
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                rectangle_ = new Rectangle(bounds.Left + bounds.Width / 2 - bounds.Height / 2 + 1, bounds.Top + bounds.Height / 2 - bounds.Width / 2 + 1, bounds.Height - 2 - Math.Min(bounds.Height, bounds.Width) / 12, bounds.Width - 2 - Math.Min(bounds.Height, bounds.Width) / 12);
            }
            else
            {
                rectangle_ = new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2 - Math.Min(bounds.Height, bounds.Width) / 12, bounds.Height - 2 - Math.Min(bounds.Height, bounds.Width) / 12);
            }
            //   rectangle_ = CheckRectangleSize(rectangle_);
            return CreatePathFromRegion(new Point[]
            {
                new Point(rectangle_.Left, rectangle_.Top),
                new Point(rectangle_.Right, rectangle_.Top),
                new Point(rectangle_.Right, rectangle_.Bottom),
                new Point(rectangle_.Left, rectangle_.Bottom)
            }, bounds, rotation, mirror, true);
        }

        internal static GraphicsPath CreateSlideStem(Rectangle bounds, int offset, int rotation, bool mirror)
        {
            Rectangle rectangle_ = default(Rectangle);
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                rectangle_ = new Rectangle(bounds.Left + 2 + bounds.Width / 2 - bounds.Height / 2 + offset + Math.Min(bounds.Height, bounds.Width) / 12, bounds.Top + 2 + bounds.Height / 2 - bounds.Width / 2 + Math.Min(bounds.Height, bounds.Width) / 12, bounds.Height / 2 - 4 - Math.Min(bounds.Height, bounds.Width) / 12, bounds.Width - 4 - Math.Min(bounds.Height, bounds.Width) / 12);
            }
            else
            {
                rectangle_ = new Rectangle(bounds.Left + 2 + offset + Math.Min(bounds.Height, bounds.Width) / 12, bounds.Top + 2 + Math.Min(bounds.Height, bounds.Width) / 12, bounds.Width / 2 - 4 - Math.Min(bounds.Height, bounds.Width) / 12, bounds.Height - 4 - Math.Min(bounds.Height, bounds.Width) / 12);
            }
            // rectangle_ = CheckRectangleSize(rectangle_);
            return CreatePathFromRegion(new Point[]
            {
                new Point(rectangle_.Left, rectangle_.Top),
                new Point(rectangle_.Right, rectangle_.Top),
                new Point(rectangle_.Right, rectangle_.Bottom),
                new Point(rectangle_.Left, rectangle_.Bottom)
            }, bounds, rotation, mirror, true);
        }

        #endregion

        #region ToggleStyleDraw
        public void VerticalToggleDraw(Graphics graphics, bool IsChecked)
        {
            Color CurrentColor;
            int rotation = 0;
            if (!IsChecked)
            {
                rotation = 180;
                CurrentColor = _offcolor;
            }
            else
            {
                CurrentColor = _oncolor;
            }
            DrawSwitch(graphics, new Rectangle(new Point(0, 0), new Size(this.Width, this.Height)), CurrentColor, rotation);
        }

        public void HorizontalToggle(Graphics graphics, bool IsChecked)
        {
            Color CurrentColor;
            int rotation = 270;
            if (!IsChecked)
            {
                rotation = 90;
                CurrentColor = _offcolor;
            }
            else
            {
                CurrentColor = _oncolor;
            }
            DrawSwitch(graphics, new Rectangle(new Point(0, 0), new Size(this.Width, this.Height)), CurrentColor, rotation);
        }

        internal static void DrawSwitch(Graphics graphics_0, Rectangle bounds, Color color, int rotation)
        {
            GraphicsPath graphicsPath = CreateToggleBase(bounds, rotation);
            Point p = new Point((int)graphicsPath.GetBounds().X, (int)graphicsPath.GetBounds().Y);
            Color[] surroundColors = new Color[]
            {
                SwitchShadingDark
            };
            GraphicsPath graphicsPath2 = CreateToggleStem(bounds, rotation);
            GraphicsPath graphicsPath3 = CreateToggleEnd(bounds, rotation);
            RectangleF bounds2 = graphicsPath2.GetBounds();
            RectangleF bounds3 = graphicsPath3.GetBounds();
            Color color2 = SwitchShadingLight;
            if (rotation > 45 && rotation < 225)
            {
                color2 = SwitchShadingDark;
            }
            using (SolidBrush solidBrush = new SolidBrush(color))
            {
                using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
                {
                    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new PointF(bounds2.Left, bounds2.Top), new PointF(bounds2.Right, bounds2.Bottom), SwitchShadingLight, SwitchShadingDark))
                    {
                        using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new PointF(bounds3.Left, bounds3.Top), new PointF(bounds3.Right, bounds3.Bottom), color2, SwitchShadingDark))
                        {
                            using (Pen pen = new Pen(SwitchOutline, 1f))
                            {
                                pathGradientBrush.CenterColor = SwitchShadingLight;
                                pathGradientBrush.CenterPoint = p;
                                pathGradientBrush.SurroundColors = surroundColors;
                                graphics_0.FillPath(solidBrush, graphicsPath);
                                graphics_0.FillPath(pathGradientBrush, graphicsPath);
                                graphics_0.DrawPath(pen, graphicsPath);
                                graphics_0.FillPath(solidBrush, graphicsPath2);
                                graphics_0.FillPath(solidBrush, graphicsPath3);
                                graphics_0.FillPath(linearGradientBrush, graphicsPath2);
                                graphics_0.FillPath(linearGradientBrush2, graphicsPath3);
                                graphics_0.DrawPath(pen, graphicsPath2);
                                graphics_0.DrawPath(pen, graphicsPath3);
                            }
                        }
                    }
                }
            }
        }
        internal static GraphicsPath CreateToggleStem(Rectangle bounds, int rotation)
        {
            Rectangle rectangle_ = Rectangle.Empty;
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                int x = bounds.Left + bounds.Width / 2 - bounds.Height / 2;
                int num = bounds.Top + bounds.Height / 2 - bounds.Width / 2 + bounds.Width / 12;
                int width = bounds.Height - 2;
                int height = bounds.Width / 2 - bounds.Width / 12 - 1;
                if (rotation == 90)
                {
                    rectangle_ = new Rectangle(x, num + 1, width, height);
                }
                else
                {
                    rectangle_ = new Rectangle(x, num, width, height);
                }
            }
            else
            {
                int x = bounds.Left + 1;
                int num = bounds.Top + 1 + bounds.Height / 12;
                int width = bounds.Width - 2;
                int height = bounds.Height / 2 - bounds.Height / 12 - 1;
                rectangle_ = new Rectangle(x, num, width, height);
            }
            //  rectangle_ = SwitchStyle.CheckRectangleSize(rectangle_);
            Point point = new Point(rectangle_.Left, rectangle_.Top);
            Point point2 = new Point(rectangle_.Right, rectangle_.Top);
            Point point3 = default(Point);
            Point point4 = default(Point);
            if (bounds.Width < bounds.Height)
            {
                point3 = new Point(rectangle_.Right - rectangle_.Width / 2 + 4 * bounds.Width / 20, rectangle_.Bottom);
                point4 = new Point(rectangle_.Left + rectangle_.Width / 2 + 1 - 4 * bounds.Width / 20, rectangle_.Bottom);
            }
            else
            {
                point3 = new Point(rectangle_.Right - rectangle_.Width / 2 + 4 * bounds.Height / 20, rectangle_.Bottom);
                point4 = new Point(rectangle_.Left + rectangle_.Width / 2 + 1 - 4 * bounds.Height / 20, rectangle_.Bottom);
            }
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddLine(point, point2);
            graphicsPath.AddLine(point2, point3);
            graphicsPath.AddLine(point3, point4);
            graphicsPath.AddLine(point4, point);
            Matrix matrix = new Matrix(1f, 0f, 0f, 1f, 0f, 0f);
            matrix.RotateAt((float)rotation, new Point(bounds.Width / 2 + bounds.Left, bounds.Height / 2 + bounds.Top));
            graphicsPath.Transform(matrix);
            return graphicsPath;
        }

        internal static GraphicsPath CreateToggleEnd(Rectangle bounds, int rotation)
        {
            Rectangle rectangle_ = Rectangle.Empty;
            if (rotation > 45 && rotation < 315 && (rotation < 135 || rotation > 225))
            {
                int x = bounds.Left + bounds.Width / 2 - bounds.Height / 2;
                int num = bounds.Top + bounds.Height / 2 - bounds.Width / 2;
                int width = bounds.Height - 2;
                int height = bounds.Width / 12;
                if (rotation == 90)
                {
                    rectangle_ = new Rectangle(x, num + 1, width, height);
                }
                else
                {
                    rectangle_ = new Rectangle(x, num, width, height);
                }
            }
            else
            {
                int x = bounds.Left + 1;
                int num = bounds.Top + 1;
                int width = bounds.Width - 2;
                int height = bounds.Height / 12;
                rectangle_ = new Rectangle(x, num, width, height);
            }
            // rectangle_ = SwitchStyle.CheckRectangleSize(rectangle_);
            Point point = new Point(rectangle_.Left + rectangle_.Width / 10, rectangle_.Top);
            Point point2 = new Point(rectangle_.Right - rectangle_.Width / 10, rectangle_.Top);
            Point point3 = new Point(rectangle_.Right, rectangle_.Bottom);
            Point point4 = new Point(rectangle_.Left, rectangle_.Bottom);
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddLine(point, point2);
            graphicsPath.AddLine(point2, point3);
            graphicsPath.AddLine(point3, point4);
            graphicsPath.AddLine(point4, point);
            Matrix matrix = new Matrix(1f, 0f, 0f, 1f, 0f, 0f);
            matrix.RotateAt((float)rotation, new Point(bounds.Width / 2 + bounds.Left, bounds.Height / 2 + bounds.Top));
            graphicsPath.Transform(matrix);
            return graphicsPath;
        }


        internal static GraphicsPath CreateToggleBase(Rectangle bounds, int rotation)
        {
            int x = default(int);
            int y = default(int);
            int num = default(int);
            if (bounds.Width < bounds.Height)
            {
                x = bounds.Left + bounds.Width / 10;
                y = bounds.Top + bounds.Height / 2 - 8 * bounds.Width / 20;
                num = 8 * bounds.Width / 10;
            }
            else
            {
                x = bounds.Left + bounds.Width / 2 - 8 * bounds.Height / 20;
                y = bounds.Top + bounds.Height / 10;
                num = 8 * bounds.Height / 10;
            }
            Rectangle rectangle = new Rectangle(x, y, num, num);

            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(rectangle);
            return graphicsPath;
        }
        #endregion
        /// <summary>
        /// 事件
        /// </summary>
        public event EventHandler ValueChanged;

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

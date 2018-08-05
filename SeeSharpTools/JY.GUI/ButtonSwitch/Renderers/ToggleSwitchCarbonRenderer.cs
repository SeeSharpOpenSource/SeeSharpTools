using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ToggleSwitch;

namespace SeeSharpTools.JY.GUI
{
    public class ToggleSwitchCarbonRenderer : ToggleSwitchRendererBase, IDisposable
    {
        #region Constructor

        private GraphicsPath _innerControlPath = null;

        public ToggleSwitchCarbonRenderer()
        {
            OuterBorderColor = Color.FromArgb(255, 106, 106, 106);
            InnerBorderColor1 = Color.FromArgb(255, 141, 151, 158);
            InnerBorderColor2 = Color.FromArgb(255, 138, 139, 140);
            LeftSideBackColor1 = Color.FromArgb(255, 61, 99, 119);
            LeftSideBackColor2 = Color.FromArgb(255, 108, 148, 179);
            RightSideBackColor1 = Color.FromArgb(255, 72, 72, 72);
            RightSideBackColor2 = Color.FromArgb(255, 85, 85, 85);
            ButtonNormalBorderColor1 = Color.FromArgb(255, 165, 167, 169);
            ButtonNormalBorderColor2 = Color.FromArgb(255, 106, 106, 106);
            ButtonNormalSurfaceColor1 = Color.FromArgb(255, 235, 235, 235);
            ButtonNormalSurfaceColor2 = Color.FromArgb(255, 178, 180, 182);
            ButtonHoverBorderColor1 = Color.FromArgb(255, 166, 168, 169);
            ButtonHoverBorderColor2 = Color.FromArgb(255, 169, 171, 173);
            ButtonHoverSurfaceColor1 = Color.FromArgb(255, 223, 224, 224);
            ButtonHoverSurfaceColor2 = Color.FromArgb(255, 169, 171, 173);
            ButtonPressedBorderColor1 = Color.FromArgb(255, 159, 159, 162);
            ButtonPressedBorderColor2 = Color.FromArgb(255, 106, 106, 106);
            ButtonPressedSurfaceColor1 = Color.FromArgb(255, 176, 177, 177);
            ButtonPressedSurfaceColor2 = Color.FromArgb(255, 133, 135, 136);
            ButtonShadowColor1 = Color.FromArgb(50, 0, 0, 0);
            ButtonShadowColor2 = Color.FromArgb(0, 0, 0, 0);

            ButtonShadowWidth = 7;
            CornerRadius = 6;
        }

        public void Dispose()
        {
            if (_innerControlPath != null)
                _innerControlPath.Dispose();
        }

        #endregion Constructor

        #region Public Properties

        public Color OuterBorderColor { get; set; }
        public Color InnerBorderColor1 { get; set; }
        public Color InnerBorderColor2 { get; set; }
        public Color LeftSideBackColor1 { get; set; }
        public Color LeftSideBackColor2 { get; set; }
        public Color RightSideBackColor1 { get; set; }
        public Color RightSideBackColor2 { get; set; }
        public Color ButtonNormalBorderColor1 { get; set; }
        public Color ButtonNormalBorderColor2 { get; set; }
        public Color ButtonNormalSurfaceColor1 { get; set; }
        public Color ButtonNormalSurfaceColor2 { get; set; }
        public Color ButtonHoverBorderColor1 { get; set; }
        public Color ButtonHoverBorderColor2 { get; set; }
        public Color ButtonHoverSurfaceColor1 { get; set; }
        public Color ButtonHoverSurfaceColor2 { get; set; }
        public Color ButtonPressedBorderColor1 { get; set; }
        public Color ButtonPressedBorderColor2 { get; set; }
        public Color ButtonPressedSurfaceColor1 { get; set; }
        public Color ButtonPressedSurfaceColor2 { get; set; }
        public Color ButtonShadowColor1 { get; set; }
        public Color ButtonShadowColor2 { get; set; }
        
        public int ButtonShadowWidth { get; set; }
        public int CornerRadius { get; set; }

        #endregion Public Properties

        #region Render Method Implementations

        public override void RenderBorder(Graphics g, Rectangle borderRectangle)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            //Draw outer border
            using (GraphicsPath outerBorderPath = GetRoundedRectanglePath(borderRectangle, CornerRadius))
            {
                g.SetClip(outerBorderPath);

                Color outerBorderColor = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? OuterBorderColor.ToGrayScale() : OuterBorderColor;

                using (Brush outerBorderBrush = new SolidBrush(outerBorderColor))
                {
                    g.FillPath(outerBorderBrush, outerBorderPath);
                }

                g.ResetClip();    
            }

            //Draw inner border
            Rectangle innerborderRectangle = new Rectangle(borderRectangle.X + 1, borderRectangle.Y + 1, borderRectangle.Width - 2, borderRectangle.Height - 2);

            using (GraphicsPath innerBorderPath = GetRoundedRectanglePath(innerborderRectangle, CornerRadius))
            {
                g.SetClip(innerBorderPath);

                Color borderColor1 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? InnerBorderColor1.ToGrayScale() : InnerBorderColor1;
                Color borderColor2 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? InnerBorderColor2.ToGrayScale() : InnerBorderColor2;

                using (Brush borderBrush = new LinearGradientBrush(borderRectangle, borderColor1, borderColor2, LinearGradientMode.Vertical))
                {
                    g.FillPath(borderBrush, innerBorderPath);
                }

                g.ResetClip();    
            }

            Rectangle backgroundRectangle = new Rectangle(borderRectangle.X + 2, borderRectangle.Y + 2, borderRectangle.Width - 4, borderRectangle.Height - 4);
            _innerControlPath = GetRoundedRectanglePath(backgroundRectangle, CornerRadius);
        }

        public override void RenderLeftToggleField(Graphics g, Rectangle leftRectangle, int totalToggleFieldWidth)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            int buttonWidth = GetButtonWidth();

            //Draw inner background
            int gradientRectWidth = leftRectangle.Width + buttonWidth / 2;
            Rectangle gradientRectangle = new Rectangle(leftRectangle.X, leftRectangle.Y, gradientRectWidth, leftRectangle.Height);

            Color leftSideBackColor1 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? LeftSideBackColor1.ToGrayScale() : LeftSideBackColor1;
            Color leftSideBackColor2 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? LeftSideBackColor2.ToGrayScale() : LeftSideBackColor2;

            if (_innerControlPath != null)
            {
                g.SetClip(_innerControlPath);
                g.IntersectClip(gradientRectangle);
            }
            else
            {
                g.SetClip(gradientRectangle);
            }

            using (Brush backgroundBrush = new LinearGradientBrush(gradientRectangle, leftSideBackColor1, leftSideBackColor2, LinearGradientMode.Vertical))
            {
                g.FillRectangle(backgroundBrush, gradientRectangle);
            }

            g.ResetClip();

            Rectangle leftShadowRectangle = new Rectangle();
            leftShadowRectangle.X = leftRectangle.X + leftRectangle.Width - ButtonShadowWidth;
            leftShadowRectangle.Y = leftRectangle.Y;
            leftShadowRectangle.Width = ButtonShadowWidth + CornerRadius;
            leftShadowRectangle.Height = leftRectangle.Height;

            if (_innerControlPath != null)
            {
                g.SetClip(_innerControlPath);
                g.IntersectClip(leftShadowRectangle);
            }
            else
            {
                g.SetClip(leftShadowRectangle);
            }

            using (Brush buttonShadowBrush = new LinearGradientBrush(leftShadowRectangle, ButtonShadowColor2, ButtonShadowColor1, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(buttonShadowBrush, leftShadowRectangle);
            }

            g.ResetClip();

            //Draw image or text
            if (ToggleSwitch.OnSideImage != null || !string.IsNullOrEmpty(ToggleSwitch.OnText))
            {
                RectangleF fullRectangle = new RectangleF(leftRectangle.X + 1 - (totalToggleFieldWidth - leftRectangle.Width), 1, totalToggleFieldWidth - 1, ToggleSwitch.Height - 2);

                if (_innerControlPath != null)
                {
                    g.SetClip(_innerControlPath);
                    g.IntersectClip(fullRectangle);
                }
                else
                {
                    g.SetClip(fullRectangle);
                }

                if (ToggleSwitch.OnSideImage != null)
                {
                    Size imageSize = ToggleSwitch.OnSideImage.Size;
                    Rectangle imageRectangle;

                    int imageXPos = (int)fullRectangle.X;

                    if (ToggleSwitch.OnSideScaleImageToFit)
                    {
                        Size canvasSize = new Size((int)fullRectangle.Width, (int)fullRectangle.Height);
                        Size resizedImageSize = ImageHelper.RescaleImageToFit(imageSize, canvasSize);

                        if (ToggleSwitch.OnSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Center)
                        {
                            imageXPos = (int)((float)fullRectangle.X + (((float)fullRectangle.Width - (float)resizedImageSize.Width) / 2));
                        }
                        else if (ToggleSwitch.OnSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Near)
                        {
                            imageXPos = (int)((float)fullRectangle.X + (float)fullRectangle.Width - (float)resizedImageSize.Width);
                        }

                        imageRectangle = new Rectangle(imageXPos, (int)((float)fullRectangle.Y + (((float)fullRectangle.Height - (float)resizedImageSize.Height) / 2)), resizedImageSize.Width, resizedImageSize.Height);

                        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                            g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle, 0, 0, ToggleSwitch.OnSideImage.Width, ToggleSwitch.OnSideImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                        else
                            g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle);
                    }
                    else
                    {
                        if (ToggleSwitch.OnSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Center)
                        {
                            imageXPos = (int)((float)fullRectangle.X + (((float)fullRectangle.Width - (float)imageSize.Width) / 2));
                        }
                        else if (ToggleSwitch.OnSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Near)
                        {
                            imageXPos = (int)((float)fullRectangle.X + (float)fullRectangle.Width - (float)imageSize.Width);
                        }

                        imageRectangle = new Rectangle(imageXPos, (int)((float)fullRectangle.Y + (((float)fullRectangle.Height - (float)imageSize.Height) / 2)), imageSize.Width, imageSize.Height);

                        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                            g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle, 0, 0, ToggleSwitch.OnSideImage.Width, ToggleSwitch.OnSideImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                        else
                            g.DrawImageUnscaled(ToggleSwitch.OnSideImage, imageRectangle);
                    }
                }
                else if (!string.IsNullOrEmpty(ToggleSwitch.OnText))
                {
                    SizeF textSize = g.MeasureString(ToggleSwitch.OnText, ToggleSwitch.OnFont);

                    float textXPos = fullRectangle.X;

                    if (ToggleSwitch.OnSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Center)
                    {
                        textXPos = (float)fullRectangle.X + (((float)fullRectangle.Width - (float)textSize.Width) / 2);
                    }
                    else if (ToggleSwitch.OnSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Near)
                    {
                        textXPos = (float)fullRectangle.X + (float)fullRectangle.Width - (float)textSize.Width;
                    }

                    RectangleF textRectangle = new RectangleF(textXPos, (float)fullRectangle.Y + (((float)fullRectangle.Height - (float)textSize.Height) / 2), textSize.Width, textSize.Height);

                    Color textForeColor = ToggleSwitch.OnForeColor;

                    if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                        textForeColor = textForeColor.ToGrayScale();

                    using (Brush textBrush = new SolidBrush(textForeColor))
                    {
                        g.DrawString(ToggleSwitch.OnText, ToggleSwitch.OnFont, textBrush, textRectangle);
                    }
                }

                g.ResetClip();
            }
        }

        public override void RenderRightToggleField(Graphics g, Rectangle rightRectangle, int totalToggleFieldWidth)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            int buttonWidth = GetButtonWidth();

            //Draw inner background
            int gradientRectWidth = rightRectangle.Width + buttonWidth / 2;
            Rectangle gradientRectangle = new Rectangle(rightRectangle.X - buttonWidth / 2, rightRectangle.Y, gradientRectWidth, rightRectangle.Height);

            Color rightSideBackColor1 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? RightSideBackColor1.ToGrayScale() : RightSideBackColor1;
            Color rightSideBackColor2 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? RightSideBackColor2.ToGrayScale() : RightSideBackColor2;

            if (_innerControlPath != null)
            {
                g.SetClip(_innerControlPath);
                g.IntersectClip(gradientRectangle);
            }
            else
            {
                g.SetClip(gradientRectangle);
            }

            using (Brush backgroundBrush = new LinearGradientBrush(gradientRectangle, rightSideBackColor1, rightSideBackColor2, LinearGradientMode.Vertical))
            {
                g.FillRectangle(backgroundBrush, gradientRectangle);
            }

            g.ResetClip();

            Rectangle rightShadowRectangle = new Rectangle();
            rightShadowRectangle.X = rightRectangle.X - CornerRadius;
            rightShadowRectangle.Y = rightRectangle.Y;
            rightShadowRectangle.Width = ButtonShadowWidth + CornerRadius;
            rightShadowRectangle.Height = rightRectangle.Height;

            if (_innerControlPath != null)
            {
                g.SetClip(_innerControlPath);
                g.IntersectClip(rightShadowRectangle);
            }
            else
            {
                g.SetClip(rightShadowRectangle);
            }

            using (Brush buttonShadowBrush = new LinearGradientBrush(rightShadowRectangle, ButtonShadowColor1, ButtonShadowColor2, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(buttonShadowBrush, rightShadowRectangle);
            }

            g.ResetClip();

            //Draw image or text
            if (ToggleSwitch.OffSideImage != null || !string.IsNullOrEmpty(ToggleSwitch.OffText))
            {
                RectangleF fullRectangle = new RectangleF(rightRectangle.X, 1, totalToggleFieldWidth - 1, ToggleSwitch.Height - 2);

                if (_innerControlPath != null)
                {
                    g.SetClip(_innerControlPath);
                    g.IntersectClip(fullRectangle);
                }
                else
                {
                    g.SetClip(fullRectangle);
                }

                if (ToggleSwitch.OffSideImage != null)
                {
                    Size imageSize = ToggleSwitch.OffSideImage.Size;
                    Rectangle imageRectangle;

                    int imageXPos = (int)fullRectangle.X;

                    if (ToggleSwitch.OffSideScaleImageToFit)
                    {
                        Size canvasSize = new Size((int)fullRectangle.Width, (int)fullRectangle.Height);
                        Size resizedImageSize = ImageHelper.RescaleImageToFit(imageSize, canvasSize);

                        if (ToggleSwitch.OffSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Center)
                        {
                            imageXPos = (int)((float)fullRectangle.X + (((float)fullRectangle.Width - (float)resizedImageSize.Width) / 2));
                        }
                        else if (ToggleSwitch.OffSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Far)
                        {
                            imageXPos = (int)((float)fullRectangle.X + (float)fullRectangle.Width - (float)resizedImageSize.Width);
                        }

                        imageRectangle = new Rectangle(imageXPos, (int)((float)fullRectangle.Y + (((float)fullRectangle.Height - (float)resizedImageSize.Height) / 2)), resizedImageSize.Width, resizedImageSize.Height);

                        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                            g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle, 0, 0, ToggleSwitch.OnSideImage.Width, ToggleSwitch.OnSideImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                        else
                            g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle);
                    }
                    else
                    {
                        if (ToggleSwitch.OffSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Center)
                        {
                            imageXPos = (int)((float)fullRectangle.X + (((float)fullRectangle.Width - (float)imageSize.Width) / 2));
                        }
                        else if (ToggleSwitch.OffSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Far)
                        {
                            imageXPos = (int)((float)fullRectangle.X + (float)fullRectangle.Width - (float)imageSize.Width);
                        }

                        imageRectangle = new Rectangle(imageXPos, (int)((float)fullRectangle.Y + (((float)fullRectangle.Height - (float)imageSize.Height) / 2)), imageSize.Width, imageSize.Height);

                        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                            g.DrawImage(ToggleSwitch.OnSideImage, imageRectangle, 0, 0, ToggleSwitch.OnSideImage.Width, ToggleSwitch.OnSideImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                        else
                            g.DrawImageUnscaled(ToggleSwitch.OffSideImage, imageRectangle);
                    }
                }
                else if (!string.IsNullOrEmpty(ToggleSwitch.OffText))
                {
                    SizeF textSize = g.MeasureString(ToggleSwitch.OffText, ToggleSwitch.OffFont);

                    float textXPos = fullRectangle.X;

                    if (ToggleSwitch.OffSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Center)
                    {
                        textXPos = (float)fullRectangle.X + (((float)fullRectangle.Width - (float)textSize.Width) / 2);
                    }
                    else if (ToggleSwitch.OffSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Far)
                    {
                        textXPos = (float)fullRectangle.X + (float)fullRectangle.Width - (float)textSize.Width;
                    }

                    RectangleF textRectangle = new RectangleF(textXPos, (float)fullRectangle.Y + (((float)fullRectangle.Height - (float)textSize.Height) / 2), textSize.Width, textSize.Height);

                    Color textForeColor = ToggleSwitch.OffForeColor;

                    if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                        textForeColor = textForeColor.ToGrayScale();

                    using (Brush textBrush = new SolidBrush(textForeColor))
                    {
                        g.DrawString(ToggleSwitch.OffText, ToggleSwitch.OffFont, textBrush, textRectangle);
                    }
                }

                g.ResetClip();
            }
        }

        public override void RenderButton(Graphics g, Rectangle buttonRectangle)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            buttonRectangle.Inflate(-1, -1);
            
            using (GraphicsPath buttonPath = GetRoundedRectanglePath(buttonRectangle, CornerRadius))
            {
                g.SetClip(buttonPath);

                //Draw button surface
                Color buttonSurfaceColor1 = ButtonNormalSurfaceColor1;
                Color buttonSurfaceColor2 = ButtonNormalSurfaceColor2;

                if (ToggleSwitch.IsButtonPressed)
                {
                    buttonSurfaceColor1 = ButtonPressedSurfaceColor1;
                    buttonSurfaceColor2 = ButtonPressedSurfaceColor2;
                }
                else if (ToggleSwitch.IsButtonHovered)
                {
                    buttonSurfaceColor1 = ButtonHoverSurfaceColor1;
                    buttonSurfaceColor2 = ButtonHoverSurfaceColor2;
                }

                if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                {
                    buttonSurfaceColor1 = buttonSurfaceColor1.ToGrayScale();
                    buttonSurfaceColor2 = buttonSurfaceColor2.ToGrayScale();
                }

                using (Brush buttonSurfaceBrush = new LinearGradientBrush(buttonRectangle, buttonSurfaceColor1, buttonSurfaceColor2, LinearGradientMode.Vertical))
                {
                    g.FillPath(buttonSurfaceBrush, buttonPath);
                }

                //Draw button border
                Color buttonBorderColor1 = ButtonNormalBorderColor1;
                Color buttonBorderColor2 = ButtonNormalBorderColor2;

                if (ToggleSwitch.IsButtonPressed)
                {
                    buttonBorderColor1 = ButtonPressedBorderColor1;
                    buttonBorderColor2 = ButtonPressedBorderColor2;
                }
                else if (ToggleSwitch.IsButtonHovered)
                {
                    buttonBorderColor1 = ButtonHoverBorderColor1;
                    buttonBorderColor2 = ButtonHoverBorderColor2;
                }

                if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                {
                    buttonBorderColor1 = buttonBorderColor1.ToGrayScale();
                    buttonBorderColor2 = buttonBorderColor2.ToGrayScale();
                }

                using (Brush buttonBorderBrush = new LinearGradientBrush(buttonRectangle, buttonBorderColor1, buttonBorderColor2, LinearGradientMode.Vertical))
                {
                    using (Pen buttonBorderPen = new Pen(buttonBorderBrush))
                    {
                        g.DrawPath(buttonBorderPen, buttonPath);
                    }
                }

                g.ResetClip();

                //Draw button image
                Image buttonImage = ToggleSwitch.ButtonImage ?? (ToggleSwitch.Value ? ToggleSwitch.OnButtonImage : ToggleSwitch.OffButtonImage);

                if (buttonImage != null)
                {
                    g.SetClip(buttonPath);

                    ButtonSwitch.ToggleSwitchButtonAlignment alignment = ToggleSwitch.ButtonImage != null ? ToggleSwitch.ButtonAlignment : (ToggleSwitch.Value ? ToggleSwitch.OnButtonAlignment : ToggleSwitch.OffButtonAlignment);

                    Size imageSize = buttonImage.Size;

                    Rectangle imageRectangle;

                    int imageXPos = buttonRectangle.X;

                    bool scaleImage = ToggleSwitch.ButtonImage != null ? ToggleSwitch.ButtonScaleImageToFit : (ToggleSwitch.Value ? ToggleSwitch.OnButtonScaleImageToFit : ToggleSwitch.OffButtonScaleImageToFit);

                    if (scaleImage)
                    {
                        Size canvasSize = buttonRectangle.Size;
                        Size resizedImageSize = ImageHelper.RescaleImageToFit(imageSize, canvasSize);

                        if (alignment == ButtonSwitch.ToggleSwitchButtonAlignment.Center)
                        {
                            imageXPos = (int)((float)buttonRectangle.X + (((float)buttonRectangle.Width - (float)resizedImageSize.Width) / 2));
                        }
                        else if (alignment == ButtonSwitch.ToggleSwitchButtonAlignment.Right)
                        {
                            imageXPos = (int)((float)buttonRectangle.X + (float)buttonRectangle.Width - (float)resizedImageSize.Width);
                        }

                        imageRectangle = new Rectangle(imageXPos, (int)((float)buttonRectangle.Y + (((float)buttonRectangle.Height - (float)resizedImageSize.Height) / 2)), resizedImageSize.Width, resizedImageSize.Height);

                        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                            g.DrawImage(buttonImage, imageRectangle, 0, 0, buttonImage.Width, buttonImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                        else
                            g.DrawImage(buttonImage, imageRectangle);
                    }
                    else
                    {
                        if (alignment == ButtonSwitch.ToggleSwitchButtonAlignment.Center)
                        {
                            imageXPos = (int)((float)buttonRectangle.X + (((float)buttonRectangle.Width - (float)imageSize.Width) / 2));
                        }
                        else if (alignment == ButtonSwitch.ToggleSwitchButtonAlignment.Right)
                        {
                            imageXPos = (int)((float)buttonRectangle.X + (float)buttonRectangle.Width - (float)imageSize.Width);
                        }

                        imageRectangle = new Rectangle(imageXPos, (int)((float)buttonRectangle.Y + (((float)buttonRectangle.Height - (float)imageSize.Height) / 2)), imageSize.Width, imageSize.Height);

                        if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                            g.DrawImage(buttonImage, imageRectangle, 0, 0, buttonImage.Width, buttonImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
                        else
                            g.DrawImageUnscaled(buttonImage, imageRectangle);
                    }

                    g.ResetClip();
                }
            }
        }

        #endregion Render Method Implementations

        #region Helper Method Implementations

        public GraphicsPath GetRoundedRectanglePath(Rectangle rectangle, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            int diameter = 2*radius;

            if (diameter > ToggleSwitch.Height)
                diameter = ToggleSwitch.Height;

            if (diameter > ToggleSwitch.Width)
                diameter = ToggleSwitch.Width;

            gp.AddArc(rectangle.X, rectangle.Y, diameter, diameter, 180, 90);
            gp.AddArc(rectangle.X + rectangle.Width - diameter, rectangle.Y, diameter, diameter, 270, 90);
            gp.AddArc(rectangle.X + rectangle.Width - diameter, rectangle.Y + rectangle.Height - diameter, diameter, diameter, 0, 90);
            gp.AddArc(rectangle.X, rectangle.Y + rectangle.Height - diameter, diameter, diameter, 90, 90);
            gp.CloseFigure();

            return gp;
        }

        public override int GetButtonWidth()
        {
            float buttonWidth = 1.61f*(ToggleSwitch.Height - 2);
            return (int) buttonWidth;
        }

        public override Rectangle GetButtonRectangle()
        {
            int buttonWidth = GetButtonWidth();
            return GetButtonRectangle(buttonWidth);
        }

        public override Rectangle GetButtonRectangle(int buttonWidth)
        {
            Rectangle buttonRect = new Rectangle(ToggleSwitch.ButtonValue, 0, buttonWidth, ToggleSwitch.Height);
            return buttonRect;
        }

        #endregion Helper Method Implementations
    }
}

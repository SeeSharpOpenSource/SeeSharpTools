using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ToggleSwitch;

namespace SeeSharpTools.JY.GUI
{
    public class ToggleSwitchIOS5Renderer : ToggleSwitchRendererBase
    {
        #region Constructor

        public ToggleSwitchIOS5Renderer()
        {
            BorderColor = Color.FromArgb(255, 202, 202, 202);
            LeftSideUpperColor1 = Color.FromArgb(255, 48, 115, 189);
            LeftSideUpperColor2 = Color.FromArgb(255, 17, 123, 220);
            LeftSideLowerColor1 = Color.FromArgb(255, 65, 143, 218);
            LeftSideLowerColor2 = Color.FromArgb(255, 130, 190, 243);
            LeftSideUpperBorderColor = Color.FromArgb(150, 123, 157, 196);
            LeftSideLowerBorderColor = Color.FromArgb(150, 174, 208, 241);
            RightSideUpperColor1 = Color.FromArgb(255, 191, 191, 191);
            RightSideUpperColor2 = Color.FromArgb(255, 229, 229, 229);
            RightSideLowerColor1 = Color.FromArgb(255, 232, 232, 232);
            RightSideLowerColor2 = Color.FromArgb(255, 251, 251, 251);
            RightSideUpperBorderColor = Color.FromArgb(150, 175, 175, 175);
            RightSideLowerBorderColor = Color.FromArgb(150, 229, 230, 233);
            ButtonShadowColor = Color.Transparent;
            ButtonNormalOuterBorderColor = Color.FromArgb(255, 149, 172, 194);
            ButtonNormalInnerBorderColor = Color.FromArgb(255, 235, 235, 235);
            ButtonNormalSurfaceColor1 = Color.FromArgb(255, 216, 215, 216);
            ButtonNormalSurfaceColor2 = Color.FromArgb(255, 251, 250, 251);
            ButtonHoverOuterBorderColor = Color.FromArgb(255, 141, 163, 184);
            ButtonHoverInnerBorderColor = Color.FromArgb(255, 223, 223, 223);
            ButtonHoverSurfaceColor1 = Color.FromArgb(255, 205, 204, 205);
            ButtonHoverSurfaceColor2 = Color.FromArgb(255, 239, 238, 239);
            ButtonPressedOuterBorderColor = Color.FromArgb(255, 111, 128, 145);
            ButtonPressedInnerBorderColor = Color.FromArgb(255, 176, 176, 176);
            ButtonPressedSurfaceColor1 = Color.FromArgb(255, 162, 161, 162);
            ButtonPressedSurfaceColor2 = Color.FromArgb(255, 187, 187, 187);
        }

        #endregion Constructor

        #region Public Properties

        public Color BorderColor { get; set; }
        public Color LeftSideUpperColor1 { get; set; }
        public Color LeftSideUpperColor2 { get; set; }
        public Color LeftSideLowerColor1 { get; set; }
        public Color LeftSideLowerColor2 { get; set; }
        public Color LeftSideUpperBorderColor { get; set; }
        public Color LeftSideLowerBorderColor { get; set; }
        public Color RightSideUpperColor1 { get; set; }
        public Color RightSideUpperColor2 { get; set; }
        public Color RightSideLowerColor1 { get; set; }
        public Color RightSideLowerColor2 { get; set; }
        public Color RightSideUpperBorderColor { get; set; }
        public Color RightSideLowerBorderColor { get; set; }
        public Color ButtonShadowColor { get; set; }
        public Color ButtonNormalOuterBorderColor { get; set; }
        public Color ButtonNormalInnerBorderColor { get; set; }
        public Color ButtonNormalSurfaceColor1 { get; set; }
        public Color ButtonNormalSurfaceColor2 { get; set; }
        public Color ButtonHoverOuterBorderColor { get; set; }
        public Color ButtonHoverInnerBorderColor { get; set; }
        public Color ButtonHoverSurfaceColor1 { get; set; }
        public Color ButtonHoverSurfaceColor2 { get; set; }
        public Color ButtonPressedOuterBorderColor { get; set; }
        public Color ButtonPressedInnerBorderColor { get; set; }
        public Color ButtonPressedSurfaceColor1 { get; set; }
        public Color ButtonPressedSurfaceColor2 { get; set; }

        #endregion Public Properties

        #region Render Method Implementations

        public override void RenderBorder(Graphics g, Rectangle borderRectangle)
        {
            //Draw this one AFTER the button is drawn in the RenderButton method
        }

        public override void RenderLeftToggleField(Graphics g, Rectangle leftRectangle, int totalToggleFieldWidth)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int buttonWidth = GetButtonWidth();

            //Draw upper gradient field
            int gradientRectWidth = leftRectangle.Width + buttonWidth / 2;
            int upperGradientRectHeight = (int)((double)0.8*(double)(leftRectangle.Height - 2));

            Rectangle controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);
            GraphicsPath controlClipPath = GetControlClipPath(controlRectangle);

            Rectangle upperGradientRectangle = new Rectangle(leftRectangle.X, leftRectangle.Y + 1, gradientRectWidth, upperGradientRectHeight - 1);

            g.SetClip(controlClipPath);
            g.IntersectClip(upperGradientRectangle);

            using (GraphicsPath upperGradientPath = new GraphicsPath())
            {
                upperGradientPath.AddArc(upperGradientRectangle.X, upperGradientRectangle.Y, ToggleSwitch.Height, ToggleSwitch.Height, 135, 135);
                upperGradientPath.AddLine(upperGradientRectangle.X, upperGradientRectangle.Y, upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y);
                upperGradientPath.AddLine(upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y, upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y + upperGradientRectangle.Height);
                upperGradientPath.AddLine(upperGradientRectangle.X, upperGradientRectangle.Y + upperGradientRectangle.Height, upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y + upperGradientRectangle.Height);

                Color upperColor1 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? LeftSideUpperColor1.ToGrayScale() : LeftSideUpperColor1;
                Color upperColor2 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? LeftSideUpperColor2.ToGrayScale() : LeftSideUpperColor2;

                using (Brush upperGradientBrush = new LinearGradientBrush(upperGradientRectangle, upperColor1, upperColor2, LinearGradientMode.Vertical))
                {
                    g.FillPath(upperGradientBrush, upperGradientPath);
                }
            }

            g.ResetClip();

            //Draw lower gradient field
            int lowerGradientRectHeight = (int)Math.Ceiling((double)0.5 * (double)(leftRectangle.Height - 2));

            Rectangle lowerGradientRectangle = new Rectangle(leftRectangle.X, leftRectangle.Y + (leftRectangle.Height / 2), gradientRectWidth, lowerGradientRectHeight);

            g.SetClip(controlClipPath);
            g.IntersectClip(lowerGradientRectangle);

            using (GraphicsPath lowerGradientPath = new GraphicsPath())
            {
                lowerGradientPath.AddArc(1, lowerGradientRectangle.Y, (int) (0.75*(ToggleSwitch.Height - 1)), ToggleSwitch.Height - 1, 215, 45); //Arc from side to top
                lowerGradientPath.AddLine(lowerGradientRectangle.X + buttonWidth/2, lowerGradientRectangle.Y, lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y);
                lowerGradientPath.AddLine(lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y, lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y + lowerGradientRectangle.Height);
                lowerGradientPath.AddLine(lowerGradientRectangle.X + buttonWidth/4, lowerGradientRectangle.Y + lowerGradientRectangle.Height, lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y + lowerGradientRectangle.Height);
                lowerGradientPath.AddArc(1, 1, ToggleSwitch.Height - 1, ToggleSwitch.Height - 1, 90, 70); //Arc from side to bottom

                Color lowerColor1 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? LeftSideLowerColor1.ToGrayScale() : LeftSideLowerColor1;
                Color lowerColor2 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? LeftSideLowerColor2.ToGrayScale() : LeftSideLowerColor2;

                using (Brush lowerGradientBrush = new LinearGradientBrush(lowerGradientRectangle, lowerColor1, lowerColor2, LinearGradientMode.Vertical))
                {
                    g.FillPath(lowerGradientBrush, lowerGradientPath);
                }
            }

            g.ResetClip();

            controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);
            controlClipPath = GetControlClipPath(controlRectangle);

            g.SetClip(controlClipPath);

            //Draw upper inside border
            Color upperBordercolor = LeftSideUpperBorderColor;

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                upperBordercolor = upperBordercolor.ToGrayScale();

            using (Pen upperBorderPen = new Pen(upperBordercolor))
            {
                g.DrawLine(upperBorderPen, leftRectangle.X, leftRectangle.Y + 1, leftRectangle.X + leftRectangle.Width + (buttonWidth / 2), leftRectangle.Y + 1);
            }

            //Draw lower inside border
            Color lowerBordercolor = LeftSideLowerBorderColor;

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                lowerBordercolor = lowerBordercolor.ToGrayScale();

            using (Pen lowerBorderPen = new Pen(lowerBordercolor))
            {
                g.DrawLine(lowerBorderPen, leftRectangle.X, leftRectangle.Y + leftRectangle.Height - 1, leftRectangle.X + leftRectangle.Width + (buttonWidth / 2), leftRectangle.Y + leftRectangle.Height - 1);
            }

            //Draw image or text
            if (ToggleSwitch.OnSideImage != null || !string.IsNullOrEmpty(ToggleSwitch.OnText))
            {
                RectangleF fullRectangle = new RectangleF(leftRectangle.X + 2 - (totalToggleFieldWidth - leftRectangle.Width), 2, totalToggleFieldWidth - 2, ToggleSwitch.Height - 4);

                g.IntersectClip(fullRectangle);

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
            }

            g.ResetClip();
        }

        public override void RenderRightToggleField(Graphics g, Rectangle rightRectangle, int totalToggleFieldWidth)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle buttonRectangle = GetButtonRectangle();

            Rectangle controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);
            GraphicsPath controlClipPath = GetControlClipPath(controlRectangle);

            //Draw upper gradient field
            int gradientRectWidth = rightRectangle.Width + buttonRectangle.Width / 2;
            int upperGradientRectHeight = (int)((double)0.8 * (double)(rightRectangle.Height - 2));
            
            Rectangle upperGradientRectangle = new Rectangle(rightRectangle.X - buttonRectangle.Width / 2, rightRectangle.Y + 1, gradientRectWidth - 1, upperGradientRectHeight - 1);

            g.SetClip(controlClipPath);
            g.IntersectClip(upperGradientRectangle);

            using (GraphicsPath upperGradientPath = new GraphicsPath())
            {
                upperGradientPath.AddLine(upperGradientRectangle.X, upperGradientRectangle.Y, upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y);
                upperGradientPath.AddArc(upperGradientRectangle.X + upperGradientRectangle.Width - ToggleSwitch.Height + 1, upperGradientRectangle.Y - 1, ToggleSwitch.Height, ToggleSwitch.Height, 270, 115);
                upperGradientPath.AddLine(upperGradientRectangle.X + upperGradientRectangle.Width, upperGradientRectangle.Y + upperGradientRectangle.Height, upperGradientRectangle.X, upperGradientRectangle.Y + upperGradientRectangle.Height);
                upperGradientPath.AddLine(upperGradientRectangle.X, upperGradientRectangle.Y + upperGradientRectangle.Height, upperGradientRectangle.X, upperGradientRectangle.Y);

                Color upperColor1 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? RightSideUpperColor1.ToGrayScale() : RightSideUpperColor1;
                Color upperColor2 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? RightSideUpperColor2.ToGrayScale() : RightSideUpperColor2;

                using (Brush upperGradientBrush = new LinearGradientBrush(upperGradientRectangle, upperColor1, upperColor2, LinearGradientMode.Vertical))
                {
                    g.FillPath(upperGradientBrush, upperGradientPath);
                }
            }

            g.ResetClip();

            //Draw lower gradient field
            int lowerGradientRectHeight = (int)Math.Ceiling((double)0.5 * (double)(rightRectangle.Height - 2));
            
            Rectangle lowerGradientRectangle = new Rectangle(rightRectangle.X - buttonRectangle.Width / 2, rightRectangle.Y + (rightRectangle.Height / 2), gradientRectWidth - 1, lowerGradientRectHeight);

            g.SetClip(controlClipPath);
            g.IntersectClip(lowerGradientRectangle);

            using (GraphicsPath lowerGradientPath = new GraphicsPath())
            {
                lowerGradientPath.AddLine(lowerGradientRectangle.X, lowerGradientRectangle.Y, lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y);
                lowerGradientPath.AddArc(lowerGradientRectangle.X + lowerGradientRectangle.Width - (int)(0.75 * (ToggleSwitch.Height - 1)), lowerGradientRectangle.Y, (int)(0.75 * (ToggleSwitch.Height - 1)), ToggleSwitch.Height - 1, 270, 45);  //Arc from top to side
                lowerGradientPath.AddArc(ToggleSwitch.Width - ToggleSwitch.Height, 0, ToggleSwitch.Height, ToggleSwitch.Height, 20, 70); //Arc from side to bottom
                lowerGradientPath.AddLine(lowerGradientRectangle.X + lowerGradientRectangle.Width, lowerGradientRectangle.Y + lowerGradientRectangle.Height, lowerGradientRectangle.X, lowerGradientRectangle.Y + lowerGradientRectangle.Height);
                lowerGradientPath.AddLine(lowerGradientRectangle.X, lowerGradientRectangle.Y + lowerGradientRectangle.Height, lowerGradientRectangle.X, lowerGradientRectangle.Y);

                Color lowerColor1 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? RightSideLowerColor1.ToGrayScale() : RightSideLowerColor1;
                Color lowerColor2 = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? RightSideLowerColor2.ToGrayScale() : RightSideLowerColor2;

                using (Brush lowerGradientBrush = new LinearGradientBrush(lowerGradientRectangle, lowerColor1, lowerColor2, LinearGradientMode.Vertical))
                {
                    g.FillPath(lowerGradientBrush, lowerGradientPath);
                }
            }

            g.ResetClip();

            controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);
            controlClipPath = GetControlClipPath(controlRectangle);

            g.SetClip(controlClipPath);

            //Draw upper inside border
            Color upperBordercolor = RightSideUpperBorderColor;

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                upperBordercolor = upperBordercolor.ToGrayScale();

            using (Pen upperBorderPen = new Pen(upperBordercolor))
            {
                g.DrawLine(upperBorderPen, rightRectangle.X - (buttonRectangle.Width / 2), rightRectangle.Y + 1, rightRectangle.X + rightRectangle.Width, rightRectangle.Y + 1);
            }

            //Draw lower inside border
            Color lowerBordercolor = RightSideLowerBorderColor;

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                lowerBordercolor = lowerBordercolor.ToGrayScale();

            using (Pen lowerBorderPen = new Pen(lowerBordercolor))
            {
                g.DrawLine(lowerBorderPen, rightRectangle.X - (buttonRectangle.Width / 2), rightRectangle.Y + rightRectangle.Height - 1, rightRectangle.X + rightRectangle.Width, rightRectangle.Y + rightRectangle.Height - 1);
            }

            //Draw image or text
            if (ToggleSwitch.OffSideImage != null || !string.IsNullOrEmpty(ToggleSwitch.OffText))
            {
                RectangleF fullRectangle = new RectangleF(rightRectangle.X, 2, totalToggleFieldWidth - 2, ToggleSwitch.Height - 4);

                g.IntersectClip(fullRectangle);

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
            }

            g.ResetClip();
        }

        public override void RenderButton(Graphics g, Rectangle buttonRectangle)
        {
            if (ToggleSwitch.IsButtonOnLeftSide)
                buttonRectangle.X += 1;
            else if (ToggleSwitch.IsButtonOnRightSide)
                buttonRectangle.X -= 1;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //Draw button shadow
            buttonRectangle.Inflate(1, 1);

            Rectangle shadowClipRectangle = new Rectangle(buttonRectangle.Location, buttonRectangle.Size);
            shadowClipRectangle.Inflate(0, -1);

            if (ToggleSwitch.IsButtonOnLeftSide)
            {
                shadowClipRectangle.X += shadowClipRectangle.Width / 2;
                shadowClipRectangle.Width = shadowClipRectangle.Width / 2;
            }
            else if (ToggleSwitch.IsButtonOnRightSide)
            {
                shadowClipRectangle.Width = shadowClipRectangle.Width / 2;
            }

            g.SetClip(shadowClipRectangle);

            Color buttonShadowColor = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? ButtonShadowColor.ToGrayScale() : ButtonShadowColor;

            using (Pen buttonShadowPen = new Pen(buttonShadowColor))
            {
                g.DrawEllipse(buttonShadowPen, buttonRectangle);
            }

            g.ResetClip();

            buttonRectangle.Inflate(-1, -1);

            //Draw outer button border
            Color buttonOuterBorderColor = ButtonNormalOuterBorderColor;

            if (ToggleSwitch.IsButtonPressed)
                buttonOuterBorderColor = ButtonPressedOuterBorderColor;
            else if (ToggleSwitch.IsButtonHovered)
                buttonOuterBorderColor = ButtonHoverOuterBorderColor;

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                buttonOuterBorderColor = buttonOuterBorderColor.ToGrayScale();

            using (Brush outerBorderBrush = new SolidBrush(buttonOuterBorderColor))
            {
                g.FillEllipse(outerBorderBrush, buttonRectangle);
            }

            //Draw inner button border
            buttonRectangle.Inflate(-1, -1);

            Color buttonInnerBorderColor = ButtonNormalInnerBorderColor;

            if (ToggleSwitch.IsButtonPressed)
                buttonInnerBorderColor = ButtonPressedInnerBorderColor;
            else if (ToggleSwitch.IsButtonHovered)
                buttonInnerBorderColor = ButtonHoverInnerBorderColor;

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                buttonInnerBorderColor = buttonInnerBorderColor.ToGrayScale();

            using (Brush innerBorderBrush = new SolidBrush(buttonInnerBorderColor))
            {
                g.FillEllipse(innerBorderBrush, buttonRectangle);
            }

            //Draw button surface
            buttonRectangle.Inflate(-1, -1);

            Color buttonUpperSurfaceColor = ButtonNormalSurfaceColor1;

            if (ToggleSwitch.IsButtonPressed)
                buttonUpperSurfaceColor = ButtonPressedSurfaceColor1;
            else if (ToggleSwitch.IsButtonHovered)
                buttonUpperSurfaceColor = ButtonHoverSurfaceColor1;

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                buttonUpperSurfaceColor = buttonUpperSurfaceColor.ToGrayScale();

            Color buttonLowerSurfaceColor = ButtonNormalSurfaceColor2;

            if (ToggleSwitch.IsButtonPressed)
                buttonLowerSurfaceColor = ButtonPressedSurfaceColor2;
            else if (ToggleSwitch.IsButtonHovered)
                buttonLowerSurfaceColor = ButtonHoverSurfaceColor2;

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                buttonLowerSurfaceColor = buttonLowerSurfaceColor.ToGrayScale();

            using (Brush buttonSurfaceBrush = new LinearGradientBrush(buttonRectangle, buttonUpperSurfaceColor, buttonLowerSurfaceColor, LinearGradientMode.Vertical))
            {
                g.FillEllipse(buttonSurfaceBrush, buttonRectangle);
            }

            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighQuality;

            //Draw outer control border
            Rectangle controlRectangle = new Rectangle(0, 0, ToggleSwitch.Width, ToggleSwitch.Height);

            using (GraphicsPath borderPath = GetControlClipPath(controlRectangle))
            {
                Color controlBorderColor = (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled) ? BorderColor.ToGrayScale() : BorderColor;

                using (Pen borderPen = new Pen(controlBorderColor))
                {
                    g.DrawPath(borderPen, borderPath);
                }
            }

            g.ResetClip();

            //Draw button image
            Image buttonImage = ToggleSwitch.ButtonImage ?? (ToggleSwitch.Value ? ToggleSwitch.OnButtonImage : ToggleSwitch.OffButtonImage);

            if (buttonImage != null)
            {
                g.SetClip(GetButtonClipPath());

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
                        g.DrawImage(buttonImage, imageRectangle, 0,0, buttonImage.Width, buttonImage.Height, GraphicsUnit.Pixel, ImageHelper.GetGrayscaleAttributes());
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

        #endregion Render Method Implementations

        #region Helper Method Implementations

        public GraphicsPath GetControlClipPath(Rectangle controlRectangle)
        {
            GraphicsPath borderPath = new GraphicsPath();
            borderPath.AddArc(controlRectangle.X, controlRectangle.Y, controlRectangle.Height, controlRectangle.Height, 90, 180);
            borderPath.AddArc(controlRectangle.Width - controlRectangle.Height, controlRectangle.Y, controlRectangle.Height, controlRectangle.Height, 270, 180);
            borderPath.CloseFigure();

            return borderPath;
        }

        public GraphicsPath GetButtonClipPath()
        {
            Rectangle buttonRectangle = GetButtonRectangle();

            GraphicsPath buttonPath = new GraphicsPath();

            buttonPath.AddArc(buttonRectangle.X, buttonRectangle.Y, buttonRectangle.Height, buttonRectangle.Height, 0, 360);

            return buttonPath;
        }

        public override int GetButtonWidth()
        {
            return ToggleSwitch.Height - 2;
        }

        public override Rectangle GetButtonRectangle()
        {
            int buttonWidth = GetButtonWidth();
            return GetButtonRectangle(buttonWidth);
        }

        public override Rectangle GetButtonRectangle(int buttonWidth)
        {
            Rectangle buttonRect = new Rectangle(ToggleSwitch.ButtonValue, 1, buttonWidth, buttonWidth);
            return buttonRect;
        }
        
        #endregion Helper Method Implementations
    }
}

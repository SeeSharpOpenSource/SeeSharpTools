using System.Drawing;
using ToggleSwitch;

namespace SeeSharpTools.JY.GUI
{
    public class ToggleSwitchMetroRenderer : ToggleSwitchRendererBase
    {
        #region Constructor

        public ToggleSwitchMetroRenderer()
        {
            BackColor = Color.White;
            LeftSideColor = Color.FromArgb(255, 23, 153, 0);
            LeftSideColorHovered = Color.FromArgb(255, 36, 182, 9);
            LeftSideColorPressed = Color.FromArgb(255, 121, 245, 100);
            RightSideColor = Color.FromArgb(255, 166, 166, 166);
            RightSideColorHovered = Color.FromArgb(255, 181, 181, 181);
            RightSideColorPressed = Color.FromArgb(255, 189, 189, 189);
            BorderColor = Color.FromArgb(255, 166, 166, 166);
            ButtonColor = Color.Black;
            ButtonColorHovered = Color.Black;
            ButtonColorPressed = Color.Black;
        }

        #endregion Constructor

        #region Public Properties

        public Color BackColor { get; set; }
        public Color LeftSideColor { get; set; }
        public Color LeftSideColorHovered { get; set; }
        public Color LeftSideColorPressed { get; set; }
        public Color RightSideColor { get; set; }
        public Color RightSideColorHovered { get; set; }
        public Color RightSideColorPressed { get; set; }
        public Color BorderColor { get; set; }
        public Color ButtonColor { get; set; }
        public Color ButtonColorHovered { get; set; }
        public Color ButtonColorPressed { get; set; }

        #endregion Public Properties

        #region Render Method Implementations

        public override void RenderBorder(Graphics g, Rectangle borderRectangle)
        {
            Color borderColor = !ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled ? BorderColor.ToGrayScale() : BorderColor;

            g.SetClip(borderRectangle);

            using (Pen borderPen = new Pen(borderColor))
            {
                g.DrawRectangle(borderPen, borderRectangle.X, borderRectangle.Y, borderRectangle.Width - 1, borderRectangle.Height - 1);
            }

            g.ResetClip();
        }

        public override void RenderLeftToggleField(Graphics g, Rectangle leftRectangle, int totalToggleFieldWidth)
        {
            Rectangle adjustedLeftRect = new Rectangle(leftRectangle.X + 2, 2, leftRectangle.Width - 2, leftRectangle.Height - 4);

            if (adjustedLeftRect.Width > 0)
            {
                Color leftColor = LeftSideColor;

                if (ToggleSwitch.IsLeftSidePressed)
                    leftColor = LeftSideColorPressed;
                else if (ToggleSwitch.IsLeftSideHovered)
                    leftColor = LeftSideColorHovered;

                if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                    leftColor = leftColor.ToGrayScale();

                g.SetClip(adjustedLeftRect);

                using (Brush leftBrush = new SolidBrush(leftColor))
                {
                    g.FillRectangle(leftBrush, adjustedLeftRect);
                }

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
                            textXPos = (float) fullRectangle.X + (((float) fullRectangle.Width - (float) textSize.Width)/2);
                        }
                        else if (ToggleSwitch.OnSideAlignment == ButtonSwitch.ToggleSwitchAlignment.Near)
                        {
                            textXPos = (float) fullRectangle.X + (float)fullRectangle.Width - (float) textSize.Width;
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
        }

        public override void RenderRightToggleField(Graphics g, Rectangle rightRectangle, int totalToggleFieldWidth)
        {
            Rectangle adjustedRightRect = new Rectangle(rightRectangle.X, 2, rightRectangle.Width - 2, rightRectangle.Height - 4);

            if (adjustedRightRect.Width > 0)
            {
                Color rightColor = RightSideColor;

                if (ToggleSwitch.IsRightSidePressed)
                    rightColor = RightSideColorPressed;
                else if (ToggleSwitch.IsRightSideHovered)
                    rightColor = RightSideColorHovered;

                if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                    rightColor = rightColor.ToGrayScale();

                g.SetClip(adjustedRightRect);

                using (Brush rightBrush = new SolidBrush(rightColor))
                {
                    g.FillRectangle(rightBrush, adjustedRightRect);
                }

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
            }
        }

        public override void RenderButton(Graphics g, Rectangle buttonRectangle)
        {
            Color buttonColor = ButtonColor;

            if (ToggleSwitch.IsButtonPressed)
            {
                buttonColor = ButtonColorPressed;
            }
            else if (ToggleSwitch.IsButtonHovered)
            {
                buttonColor = ButtonColorHovered;
            }

            if (!ToggleSwitch.Enabled && ToggleSwitch.GrayWhenDisabled)
                buttonColor = buttonColor.ToGrayScale();

            g.SetClip(buttonRectangle);

            using (Brush backBrush = new SolidBrush(buttonColor))
            {
                g.FillRectangle(backBrush, buttonRectangle);
            }

            g.ResetClip();
        }

        #endregion Render Method Implementations

        #region Helper Method Implementations

        public override int GetButtonWidth()
        {
            return (int)((double)ToggleSwitch.Height / 3 * 2);
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
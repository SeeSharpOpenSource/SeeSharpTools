using System;
using System.Drawing;

namespace SeeSharpTools.JY.GUI
{
    internal class HSLColor
    {
        private Color _privateColor;

        private float _hue;

        private float _luminance;

        private float _saturation;

        public float Luminance
        {
            get
            {
                return this._luminance;
            }
        }

        public float Hue
        {
            get
            {
                return this._hue;
            }
        }

        public float Saturation
        {
            get
            {
                return this._saturation;
            }
        }

        public HSLColor(Color color)
        {
            this._privateColor = color;
            HSLColor.smethod_0(color, out this._hue, out this._saturation, out this._luminance);
        }

        private static void smethod_0(Color color_1, out float float_3, out float float_4, out float float_5)
        {
            float num = (float)color_1.R / 255f;
            float num2 = (float)color_1.G / 255f;
            float num3 = (float)color_1.B / 255f;
            float num4 = Math.Max(num, Math.Max(num2, num3));
            float num5 = Math.Min(num, Math.Min(num2, num3));
            float_5 = (num4 + num5) / 2f;
            if (num4 == num5)
            {
                float_4 = 0f;
                float_3 = 0f;
                return;
            }
            if ((double)float_5 < 0.5)
            {
                float_4 = (num4 - num5) / (num4 + num5);
            }
            else
            {
                float_4 = (num4 - num5) / (2f - num4 - num5);
            }
            float num6 = num4 - num5;
            if (num == num4)
            {
                float_3 = (num2 - num3) / num6;
            }
            else if (num2 == num4)
            {
                float_3 = 2f + (num3 - num) / num6;
            }
            else
            {
                float_3 = 4f + (num - num2) / num6;
            }
            float_3 *= 60f;
            if (float_3 < 0f)
            {
                float_3 += 360f;
            }
        }

        private static float smethod_1(float float_3, float float_4, float float_5)
        {
            if (float_5 < 0f)
            {
                float_5 += 1f;
            }
            if (float_5 > 1f)
            {
                float_5 -= 1f;
            }
            float result = default(float);
            if (6f * float_5 < 1f)
            {
                result = float_3 + (float_4 - float_3) * 6f * float_5;
            }
            else if (2f * float_5 < 1f)
            {
                result = float_4;
            }
            else if (3f * float_5 < 2f)
            {
                result = float_3 + (float_4 - float_3) * (0.6666667f - float_5) * 6f;
            }
            else
            {
                result = float_3;
            }
            return result;
        }

        public static Color ToRGB(byte alpha, float float_3, float saturation, float luminance)
        {
            float num = default(float);
            float num2 = default(float);
            float num3 = default(float);
            if (saturation == 0f)
            {
                num = luminance;
                num2 = luminance;
                num3 = luminance;
            }
            else
            {
                float num4 = default(float);
                if ((double)luminance < 0.5)
                {
                    num4 = luminance * (1f + saturation);
                }
                else
                {
                    num4 = luminance + saturation - luminance * saturation;
                }
                float float_4 = 2f * luminance - num4;
                float_3 /= 360f;
                num3 = HSLColor.smethod_1(float_4, num4, float_3 + 0.333333343f);
                num2 = HSLColor.smethod_1(float_4, num4, float_3);
                num = HSLColor.smethod_1(float_4, num4, float_3 - 0.333333343f);
            }
            int red = (int)(num3 * 255f);
            int green = (int)(num2 * 255f);
            int blue = (int)(num * 255f);
            return Color.FromArgb((int)alpha, red, green, blue);
        }

        public Color GetComplimentColor(float complimentFactor)
        {
            float luminance = default(float);
            if (this.Luminance - complimentFactor >= 0f)
            {
                luminance = this.Luminance - complimentFactor;
            }
            else if (this.Luminance + complimentFactor <= 1f)
            {
                luminance = this.Luminance + complimentFactor;
            }
            else
            {
                luminance = 1f - Math.Abs(this.Luminance - complimentFactor);
            }
            return HSLColor.ToRGB(this._privateColor.A, this.Hue, this.Saturation, luminance);
        }

        public Color GetDarkColor(float complimentFactor)
        {
            float num = this.Luminance - complimentFactor;
            if (num < 0f)
            {
                num = 0f;
            }
            return HSLColor.ToRGB(this._privateColor.A, this.Hue, this.Saturation, num);
        }

        public Color GetLighterColor(float complimentFactor)
        {
            float num = this.Luminance + complimentFactor;
            if (num > 1f)
            {
                num = 1f;
            }
            return HSLColor.ToRGB(this._privateColor.A, this.Hue, this.Saturation, num);
        }

        public Color GetShadedColor()
        {
            if ((double)this.Luminance < 0.9)
            {
                return this.GetComplimentColor(this.Luminance * 0.1f);
            }
            if ((double)this.Luminance < 0.95)
            {
                return this.GetComplimentColor((1f - this.Luminance) / 10f);
            }
            return this.GetComplimentColor(0.03f);
        }
    }
}

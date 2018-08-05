using System;
using System.Drawing;

namespace SeeSharpTools.JY.GUI
{
    public class iColors
    {
        private static Color m_FaceColorLight;

        private static Color m_FaceColorDark;

        private static Color m_FaceColorFlat;

        public static Color FaceColorLight
        {
            get
            {
                return iColors.m_FaceColorLight;
            }
            set
            {
                iColors.m_FaceColorLight = value;
            }
        }

        public static Color FaceColorDark
        {
            get
            {
                return iColors.m_FaceColorDark;
            }
            set
            {
                iColors.m_FaceColorDark = value;
            }
        }

        public static Color FaceColorFlat
        {
            get
            {
                return iColors.m_FaceColorFlat;
            }
            set
            {
                iColors.m_FaceColorFlat = value;
            }
        }

        private static Color Lighten(Color color, float multiplier)
        {
            int a = (int)color.A;
            int num = (int)((float)color.R + (float)(255 - color.R) * multiplier);
            int num2 = (int)((float)color.G + (float)(255 - color.G) * multiplier);
            int num3 = (int)((float)color.B + (float)(255 - color.B) * multiplier);
            if (num > 255)
            {
                num = 255;
            }
            if (num2 > 255)
            {
                num2 = 255;
            }
            if (num3 > 255)
            {
                num3 = 255;
            }
            if (num < 0)
            {
                num = 0;
            }
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            return Color.FromArgb(a, num, num2, num3);
        }

        private static Color Darken(Color color, float multiplier)
        {
            int a = (int)color.A;
            int num = (int)((float)color.R - (float)color.R * multiplier);
            int num2 = (int)((float)color.G - (float)color.G * multiplier);
            int num3 = (int)((float)color.B - (float)color.B * multiplier);
            if (num > 255)
            {
                num = 255;
            }
            if (num2 > 255)
            {
                num2 = 255;
            }
            if (num3 > 255)
            {
                num3 = 255;
            }
            if (num < 0)
            {
                num = 0;
            }
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            return Color.FromArgb(a, num, num2, num3);
        }

        private static Color ColorOffset(Color color, int value)
        {
            int a = (int)color.A;
            int num = (int)color.R + value;
            int num2 = (int)color.G + value;
            int num3 = (int)color.B + value;
            if (num > 255)
            {
                num = 255;
            }
            if (num2 > 255)
            {
                num2 = 255;
            }
            if (num3 > 255)
            {
                num3 = 255;
            }
            if (num < 0)
            {
                num = 0;
            }
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            return Color.FromArgb(a, num, num2, num3);
        }

        public static Color Lighten1(Color color)
        {
            return iColors.Lighten(color, 0.15f);
        }

        public static Color Lighten2(Color color)
        {
            return iColors.Lighten(color, 0.25f);
        }

        public static Color Lighten3(Color color)
        {
            return iColors.Lighten(color, 0.5f);
        }

        public static Color Lighten4(Color color)
        {
            return iColors.Lighten(color, 0.75f);
        }

        public static Color Darken1(Color color)
        {
            return iColors.Darken(color, 0.15f);
        }

        public static Color Darken2(Color color)
        {
            return iColors.Darken(color, 0.25f);
        }

        public static Color Darken3(Color color)
        {
            return iColors.Darken(color, 0.5f);
        }

        public static Color Darken4(Color color)
        {
            return iColors.Darken(color, 0.75f);
        }

        public static Color ToCornerHighlight(Color color)
        {
            return iColors.Lighten(color, 0.6f);
        }

        public static Color ToCornerShadow(Color color)
        {
            return iColors.Darken(color, 0.05f);
        }

        public static Color ToBright(Color color)
        {
            return iColors.ColorOffset(color, 128);
        }

        public static Color ToDim(Color color)
        {
            return iColors.ColorOffset(color, -128);
        }

        public static Color ToOffColor(Color color)
        {
            return iColors.ToDim(color);
        }

        //public static Color ToFaceColor(FaceReference side, RotationQuad rotation, bool invert)
        //{
        //    if (side == FaceReference.Flat)
        //    {
        //        return iColors.FaceColorFlat;
        //    }
        //    if (invert)
        //    {
        //        if (side == FaceReference.Left)
        //        {
        //            side = FaceReference.Right;
        //        }
        //        else if (side == FaceReference.Top)
        //        {
        //            side = FaceReference.Bottom;
        //        }
        //        else if (side == FaceReference.Right)
        //        {
        //            side = FaceReference.Left;
        //        }
        //        else if (side == FaceReference.Bottom)
        //        {
        //            side = FaceReference.Top;
        //        }
        //    }
        //    if (rotation == RotationQuad.X000)
        //    {
        //        if (side == FaceReference.Left)
        //        {
        //            return iColors.FaceColorLight;
        //        }
        //        if (side == FaceReference.Top)
        //        {
        //            return iColors.FaceColorLight;
        //        }
        //        if (side == FaceReference.Right)
        //        {
        //            return iColors.FaceColorDark;
        //        }
        //        if (side == FaceReference.Bottom)
        //        {
        //            return iColors.FaceColorDark;
        //        }
        //    }
        //    else if (rotation == RotationQuad.X090)
        //    {
        //        if (side == FaceReference.Left)
        //        {
        //            return iColors.FaceColorLight;
        //        }
        //        if (side == FaceReference.Top)
        //        {
        //            return iColors.FaceColorDark;
        //        }
        //        if (side == FaceReference.Right)
        //        {
        //            return iColors.FaceColorDark;
        //        }
        //        if (side == FaceReference.Bottom)
        //        {
        //            return iColors.FaceColorLight;
        //        }
        //    }
        //    else if (rotation == RotationQuad.X180)
        //    {
        //        if (side == FaceReference.Left)
        //        {
        //            return iColors.FaceColorDark;
        //        }
        //        if (side == FaceReference.Top)
        //        {
        //            return iColors.FaceColorDark;
        //        }
        //        if (side == FaceReference.Right)
        //        {
        //            return iColors.FaceColorLight;
        //        }
        //        if (side == FaceReference.Bottom)
        //        {
        //            return iColors.FaceColorLight;
        //        }
        //    }
        //    else
        //    {
        //        if (side == FaceReference.Left)
        //        {
        //            return iColors.FaceColorDark;
        //        }
        //        if (side == FaceReference.Top)
        //        {
        //            return iColors.FaceColorLight;
        //        }
        //        if (side == FaceReference.Right)
        //        {
        //            return iColors.FaceColorLight;
        //        }
        //        if (side == FaceReference.Bottom)
        //        {
        //            return iColors.FaceColorDark;
        //        }
        //    }
        //    return Color.Black;
        //}
    }
}

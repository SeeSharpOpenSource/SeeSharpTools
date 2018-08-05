using System.Drawing;

namespace SeeSharpTools.JY.GUI
{
    public static class GraphicsExtensionMethods
    {
        public static Color ToGrayScale(this Color originalColor)
        {
            if (originalColor.Equals(Color.Transparent))
                return originalColor;

            int grayScale = (int)((originalColor.R * .299) + (originalColor.G * .587) + (originalColor.B * .114));
            return Color.FromArgb(grayScale, grayScale, grayScale);
        }
    }
}

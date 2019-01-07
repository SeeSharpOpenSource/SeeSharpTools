using Microsoft.Office.Interop.Word;
using System.Drawing;

namespace SeeSharpTools.JY.Report
{
    /// <summary>
    /// Excel文字外观配置
    /// </summary>
    public class ExcelFont
    {
        public ExcelFont()
        {
            FontName = "";
            FontSize = 10;
            IsBold = false;
            IsItalic = false;
            IsUnderlined = false;
            IsStrikedThrough = false;
            FontColor = Color.Black;
        }

        /// <summary>
        /// 字型
        /// </summary>
        public string FontName
        { get; set; }

        /// <summary>
        /// 字型大小
        /// </summary>
        public int FontSize
        { get; set; }

        /// <summary>
        /// 粗体
        /// </summary>
        public bool IsBold
        { get; set; }

        /// <summary>
        /// 斜体
        /// </summary>
        public bool IsItalic
        { get; set; }

        /// <summary>
        /// 底线
        /// </summary>
        public bool IsUnderlined
        { get; set; }

        /// <summary>
        /// 删除线
        /// </summary>
        public bool IsStrikedThrough
        { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color FontColor
        { get; set; }
    }

    public class ExcelChartStyle
    {
        public ExcelChartStyle()
        {
            ChartStyle = OfficeChartStyle.xlLine;
            FirstColumnAsXAxis = true;
            ChartWidth = 320;
            ChartHeight = 180;
        }

        public OfficeChartStyle ChartStyle
        { get; set; }

        public bool FirstColumnAsXAxis
        { get; set; }

        public int ChartWidth
        { get; set; }

        public int ChartHeight
        { get; set; }
    }

    /// <summary>
    /// Word文字外观配置
    /// </summary>
    public class WordFont
    {
        private string _fontName = "";
        private int _fontSize = 12;
        private int _bold = 0;
        private int _italic = 0;
        private WdUnderline _underline = WdUnderline.wdUnderlineNone;
        private int _strikeThrough = 0;
        private WdColor _fontColor = WdColor.wdColorBlack;

        public WordFont()
        {
            FontName = "";
            FontSize = 12;
            IsBold = 0;
            IsItalic = 0;
            IsUnderlined = 0;
            IsStrikedThrough = 0;
            FontColor = WdColor.wdColorBlack;
        }

        /// <summary>
        /// 字型
        /// </summary>
        public string FontName
        { get; set; }

        /// <summary>
        /// 字型大小
        /// </summary>
        public int FontSize
        { get; set; }

        /// <summary>
        /// 粗体
        /// </summary>
        public int IsBold
        { get; set; }

        /// <summary>
        /// 斜体
        /// </summary>
        public int IsItalic
        { get; set; }

        /// <summary>
        /// 底线
        /// </summary>
        public WdUnderline IsUnderlined
        { get; set; }

        /// <summary>
        /// 删除线
        /// </summary>
        public int IsStrikedThrough
        { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public WdColor FontColor
        { get; set; }
    }

    public class WordChartStyle
    {
        public WordChartStyle()
        {
            ChartStyle = OfficeChartStyle.xlXYScatterLines;
            FirstColumnAsXAxis = true;
            ChartWidth = 320;
            ChartHeight = 180;
        }

        public OfficeChartStyle ChartStyle
        { get; set; }

        public bool FirstColumnAsXAxis
        { get; set; }

        public int ChartWidth
        { get; set; }

        public int ChartHeight
        { get; set; }
    }
}
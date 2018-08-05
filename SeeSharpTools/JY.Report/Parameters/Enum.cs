using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace SeeSharpTools.JY.Report
{
    #region Excel

    /// <summary>
    /// 一维数组写入方向
    /// </summary>
    public enum WriteArrayDirection
    {
        /// <summary>
        /// 垂直
        /// </summary>
        Vertical,

        /// <summary>
        /// 水平
        /// </summary>
        Horizontal,
    }

    /// <summary>
    /// Excel存档格式
    /// </summary>
    public enum ExcelSaveFormat
    {
        /// <summary>
        /// HTML格式
        /// </summary>
        html = Excel.XlFileFormat.xlHtml,

        /// <summary>
        /// CSV格式
        /// </summary>
        csv = Excel.XlFileFormat.xlCSV,

        /// <summary>
        /// TXT格式
        /// </summary>
        text = Excel.XlFileFormat.xlHtml,

        /// <summary>
        /// XML格式
        /// </summary>
        xml = Excel.XlFileFormat.xlXMLSpreadsheet,

        /// <summary>
        /// XLSX格式(Excel 2007后)
        /// </summary>
        xlsx = Excel.XlFileFormat.xlExcel12,

        /// <summary>
        /// XLS格式(Excel1997~2003)
        /// </summary>
        xls = Excel.XlFileFormat.xlExcel8,
    }

    #endregion Excel

    #region Word

    /// <summary>
    /// 插入点 （套用Word)
    /// </summary>
    public enum InsertionPoint
    {
        /// <summary>
        /// 文档最后
        /// </summary>
        EndOfDocument,

        /// <summary>
        /// 文档开头
        /// </summary>
        BeginningOfDoument,
    }

    /// <summary>
    /// Word表格样式
    /// </summary>
    public enum WordTableStyle
    {
        /// <summary>
        /// LightShading
        /// </summary>
        LightShading = Word.WdBuiltinStyle.wdStyleTableLightShading,

        /// <summary>
        /// LightList
        /// </summary>
        LightList = Word.WdBuiltinStyle.wdStyleTableLightList,

        /// <summary>
        /// LightGrid
        /// </summary>
        LightGrid = Word.WdBuiltinStyle.wdStyleTableLightGrid,

        /// <summary>
        /// MediumShading1
        /// </summary>
        MediumShading1 = Word.WdBuiltinStyle.wdStyleTableMediumShading1,

        /// <summary>
        /// MediumShading2
        /// </summary>
        MediumShading2 = Word.WdBuiltinStyle.wdStyleTableMediumShading2,

        /// <summary>
        /// MediumList1
        /// </summary>
        MediumList1 = Word.WdBuiltinStyle.wdStyleTableMediumList1,

        /// <summary>
        /// MediumList2
        /// </summary>
        MediumList2 = Word.WdBuiltinStyle.wdStyleTableMediumList2,

        /// <summary>
        /// MediumGrid1
        /// </summary>
        MediumGrid1 = Word.WdBuiltinStyle.wdStyleTableMediumGrid1,

        /// <summary>
        /// MediumGrid2
        /// </summary>
        MediumGrid2 = Word.WdBuiltinStyle.wdStyleTableMediumGrid2,

        /// <summary>
        /// MediumGrid3
        /// </summary>
        MediumGrid3 = Word.WdBuiltinStyle.wdStyleTableMediumGrid3,

        /// <summary>
        /// DarkList
        /// </summary>
        DarkList = Word.WdBuiltinStyle.wdStyleTableDarkList,

        /// <summary>
        /// ColorfulShading
        /// </summary>
        ColorfulShading = Word.WdBuiltinStyle.wdStyleTableColorfulShading,

        /// <summary>
        /// ColorfulList
        /// </summary>
        ColorfulList = Word.WdBuiltinStyle.wdStyleTableColorfulList,

        /// <summary>
        /// ColorfulGrid
        /// </summary>
        ColorfulGrid = Word.WdBuiltinStyle.wdStyleTableColorfulGrid,

        /// <summary>
        /// LightShadingAccent1
        /// </summary>
        LightShadingAccent1 = Word.WdBuiltinStyle.wdStyleTableLightShadingAccent1,

        /// <summary>
        /// LightListAccent1
        /// </summary>
        LightListAccent1 = Word.WdBuiltinStyle.wdStyleTableLightListAccent1,

        /// <summary>
        /// LightGridAccent1
        /// </summary>
        LightGridAccent1 = Word.WdBuiltinStyle.wdStyleTableLightGridAccent1,

        /// <summary>
        /// MediumShading1Accent1
        /// </summary>
        MediumShading1Accent1 = Word.WdBuiltinStyle.wdStyleTableMediumShading1Accent1,

        /// <summary>
        /// MediumShading2Accent1
        /// </summary>
        MediumShading2Accent1 = Word.WdBuiltinStyle.wdStyleTableMediumShading2Accent1,

        /// <summary>
        /// MediumList1Accent1
        /// </summary>
        MediumList1Accent1 = Word.WdBuiltinStyle.wdStyleTableMediumList1Accent1,
    }

    /// <summary>
    /// Word存档格式
    /// </summary>
    public enum WordSaveFormat
    {
        /// <summary>
        /// docx格式(Excel2007后)
        /// </summary>
        docx = Word.WdSaveFormat.wdFormatDocumentDefault,

        /// <summary>
        /// doc格式(Word1997~2003)
        /// </summary>
        doc = Word.WdSaveFormat.wdFormatDocument97,

        /// <summary>
        /// PDF格式
        /// </summary>
        pdf = Word.WdSaveFormat.wdFormatPDF,
    }

    #endregion Word

    #region Chart

    public enum OfficeChartStyle
    {
        xl3DArea = Excel.XlChartType.xl3DArea,
        xl3DAreaStacked = Excel.XlChartType.xl3DAreaStacked,
        xl3DAreaStacked100 = Excel.XlChartType.xl3DAreaStacked100,
        xl3DBarClustered = Excel.XlChartType.xl3DBarClustered,
        xl3DBarStacked = Excel.XlChartType.xl3DBarStacked,
        xl3DBarStacked100 = Excel.XlChartType.xl3DBarStacked100,
        xl3DColumn = Excel.XlChartType.xl3DColumn,
        xl3DColumnClustered = Excel.XlChartType.xl3DColumnClustered,
        xl3DColumnStacked = Excel.XlChartType.xl3DColumnStacked,
        xl3DColumnStacked100 = Excel.XlChartType.xl3DColumnStacked100,
        xl3DLine = Excel.XlChartType.xl3DLine,
        xl3DPie = Excel.XlChartType.xl3DPie,
        xl3DPieExploded = Excel.XlChartType.xl3DPieExploded,
        xlArea = Excel.XlChartType.xlArea,
        xlAreaStacked = Excel.XlChartType.xlAreaStacked,
        xlAreaStacked100 = Excel.XlChartType.xlAreaStacked100,
        xlBarClustered = Excel.XlChartType.xlBarClustered,
        xlBarOfPie = Excel.XlChartType.xlBarOfPie,
        xlBarStacked = Excel.XlChartType.xlBarStacked,
        xlBarStacked100 = Excel.XlChartType.xlBarStacked100,
        xlBubble = Excel.XlChartType.xlBubble,
        xlBubble3DEffect = Excel.XlChartType.xlBubble3DEffect,
        xlColumnClustered = Excel.XlChartType.xlColumnClustered,
        xlColumnStacked = Excel.XlChartType.xlColumnStacked,
        xlColumnStacked100 = Excel.XlChartType.xlColumnStacked100,
        xlConeBarClustered = Excel.XlChartType.xlConeBarClustered,
        xlConeBarStacked = Excel.XlChartType.xlConeBarStacked,
        xlConeBarStacked100 = Excel.XlChartType.xlConeBarStacked100,
        xlConeCol = Excel.XlChartType.xlConeCol,
        xlConeColClustered = Excel.XlChartType.xlConeColClustered,
        xlConeColStacked = Excel.XlChartType.xlConeColStacked,
        xlConeColStacked100 = Excel.XlChartType.xlConeColStacked100,
        xlCylinderBarClustered = Excel.XlChartType.xlCylinderBarClustered,
        xlCylinderBarStacked = Excel.XlChartType.xlCylinderBarStacked,
        xlCylinderBarStacked100 = Excel.XlChartType.xlCylinderBarStacked100,
        xlCylinderCol = Excel.XlChartType.xlCylinderCol,
        xlCylinderColClustered = Excel.XlChartType.xlCylinderColClustered,
        xlCylinderColStacked = Excel.XlChartType.xlCylinderColStacked,
        xlCylinderColStacked100 = Excel.XlChartType.xlCylinderColStacked100,
        xlDoughnut = Excel.XlChartType.xlDoughnut,
        xlDoughnutExploded = Excel.XlChartType.xlDoughnutExploded,
        xlLine = Excel.XlChartType.xlLine,
        xlLineMarkers = Excel.XlChartType.xlLineMarkers,
        xlLineMarkersStacked = Excel.XlChartType.xlLineMarkersStacked,
        xlLineMarkersStacked100 = Excel.XlChartType.xlLineMarkersStacked100,
        xlLineStacked = Excel.XlChartType.xlLineStacked,
        xlLineStacked100 = Excel.XlChartType.xlLineStacked100,
        xlPie = Excel.XlChartType.xlPie,
        xlPieExploded = Excel.XlChartType.xlPieExploded,
        xlPieOfPie = Excel.XlChartType.xlPieOfPie,
        xlPyramidBarClustered = Excel.XlChartType.xlPyramidBarClustered,
        xlPyramidBarStacked = Excel.XlChartType.xlPyramidBarStacked,
        xlPyramidBarStacked100 = Excel.XlChartType.xlPyramidBarStacked100,
        xlPyramidCol = Excel.XlChartType.xlPyramidCol,
        xlPyramidColClustered = Excel.XlChartType.xlPyramidColClustered,
        xlPyramidColStacked = Excel.XlChartType.xlPyramidColStacked,
        xlPyramidColStacked100 = Excel.XlChartType.xlPyramidColStacked100,
        xlRadar = Excel.XlChartType.xlRadar,
        xlRadarFilled = Excel.XlChartType.xlRadarFilled,
        xlRadarMarkers = Excel.XlChartType.xlRadarMarkers,
        xlStockHLC = Excel.XlChartType.xlStockHLC,
        xlStockOHLC = Excel.XlChartType.xlStockOHLC,
        xlStockVHLC = Excel.XlChartType.xlStockVHLC,
        xlStockVOHLC = Excel.XlChartType.xlStockVOHLC,
        xlSurface = Excel.XlChartType.xlSurface,
        xlSurfaceTopView = Excel.XlChartType.xlSurfaceTopView,
        xlSurfaceTopViewWireframe = Excel.XlChartType.xlSurfaceTopViewWireframe,
        xlSurfaceWireframe = Excel.XlChartType.xlSurfaceWireframe,
        xlXYScatter = Excel.XlChartType.xlXYScatter,
        xlXYScatterLines = Excel.XlChartType.xlXYScatterLines,
        xlXYScatterLinesNoMarkers = Excel.XlChartType.xlXYScatterLinesNoMarkers,
        xlXYScatterSmooth = Excel.XlChartType.xlXYScatterSmooth,
        xlXYScatterSmoothNoMarkers = Excel.XlChartType.xlXYScatterSmoothNoMarkers,
    }

    #endregion Chart
}
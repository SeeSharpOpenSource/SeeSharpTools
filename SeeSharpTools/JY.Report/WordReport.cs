using Microsoft.Office.Core;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace SeeSharpTools.JY.Report
{
    /// <summary>
    /// Word文档类库
    /// </summary>
    public class WordReport
    {
        #region 成员变量

        private object _missing = Missing.Value;
        private Word._Application _engine = null;
        private dynamic _wordDoc = null;
        private WordFont _defaultFont;
        private Word.Tables _wordTables;
        private dynamic _wordShapes;
        private double _version = 0.0;
        private WordChartStyle _chartStyle;

        #endregion 成员变量

        #region 构造函式

        /// <summary>
        /// 构造函式，开启新Word
        /// </summary>
        public WordReport()
        {
            try
            {
                _defaultFont = new WordFont();
                _engine = new Word.Application();
                _wordDoc = _engine.Documents.Add(_missing, _missing, _missing, _missing);
                _wordTables = _wordDoc.Tables;
                _wordShapes = _wordDoc.InlineShapes;
                _version = DetermineVersion();
                _chartStyle = new WordChartStyle();

            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 构造函式，使用模板
        /// </summary>
        /// <param name="template"></param>
        public WordReport(string template)
        {
            try
            {
                _defaultFont = new WordFont();
                _engine = new Word.Application();
                _wordDoc = _engine.Documents.Open(template, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing);
                _wordTables = _wordDoc.Tables;
                _wordShapes = _wordDoc.InlineShapes;
                _version = DetermineVersion();
                _chartStyle = new WordChartStyle();

            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        #endregion 构造函式

        #region 公共属性

        /// <summary>
        /// 默认的Word文字外观属性
        /// </summary>
        public WordFont DefaultFont
        {
            get { return _defaultFont; }
            set { _defaultFont = value; }
        }

        #endregion 公共属性

        #region 公共方法

        #region 操作

        /// <summary>
        /// 关闭Word程序
        /// </summary>
        public void Close(bool saveChanges=false)
        {
            try
            {
                bool result = _engine.IsObjectValid[_engine];
                if (result)
                {
                    _wordDoc.Close(saveChanges, _missing, _missing);
                    _engine.Quit();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// 另存新档
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="format"></param>
        public void SaveAs(string filePath, WordSaveFormat format = WordSaveFormat.docx)
        {
            try
            {
                if (_version > 12.0)
                {
                    //"SaveAs2" only support office 2010 and above version
                    _wordDoc.SaveAs2(filePath, format, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing);
                }
                else
                {
                    _wordDoc.SaveAs(filePath, format, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing, _missing);
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 显示Word应用程序
        /// </summary>
        public void Show()
        {
            try
            {
                _engine.Visible = true;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 隐藏Word应用程序
        /// </summary>
        public void Hide()
        {
            try
            {
                _engine.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        #endregion 操作

        #region 写入

        /// <summary>
        /// 在指定书签位置上插入文字
        /// </summary>
        /// <param name="bookmark">书签</param>
        /// <param name="text">文字</param>
        /// <param name="font">文字外观</param>
        public void WriteTextToDoc(string bookmark, string text, WordFont font = null)
        {
            try
            {
                Word.Range r = GetRange(bookmark);
                r.Text = text;
                if (font == null)
                {
                    ApplyFont(r, _defaultFont);
                }
                else
                {
                    ApplyFont(r, font);
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在指定位置上插入文字
        /// </summary>
        /// <param name="type">插入类型</param>
        /// <param name="text">文字</param>
        /// <param name="font">文字外观</param>
        public void WriteTextToDoc(InsertionPoint type, string text, WordFont font = null)
        {
            try
            {
                Word.Range r = GetRange(type);
                r.Text = text;
                if (font == null)
                {
                    ApplyFont(r, _defaultFont);
                }
                else
                {
                    ApplyFont(r, font);
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在指定书签位置上插入表格
        /// </summary>
        /// <param name="bookmark">书签</param>
        /// <param name="value">二维字符串数组</param>
        /// <param name="wordStyle">文字外观</param>
        public void WriteTableToDoc<T>(string bookmark, T[,] value, WordTableStyle wordStyle = WordTableStyle.LightGrid)
        {
            try
            {
                int rowCount = value.GetLength(0);
                int colCount = value.GetLength(1);
                Word.Range r = GetRange(bookmark);
                Word.Table tab = _wordTables.Add(r, rowCount, colCount);
                tab.set_Style(wordStyle);
                tab.Select();
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        tab.Cell(i + 1, j + 1).Select();
                        _engine.Selection.TypeText(value[i, j].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在指定位置上插入表格
        /// </summary>
        /// <param name="type">插入类型</param>
        /// <param name="value">二维字符串数组</param>
        /// <param name="wordStyle">文字外观</param>
        public void WriteTableToDoc<T>(InsertionPoint type, T[,] value, WordTableStyle wordStyle = WordTableStyle.LightGrid)
        {
            try
            {
                int rowCount = value.GetLength(0);
                int colCount = value.GetLength(1);
                Word.Range r = GetRange(type);
                Word.Table tab = _wordTables.Add(r, rowCount, colCount);
                tab.set_Style(wordStyle);
                tab.Select();
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        tab.Cell(i + 1, j + 1).Select();
                        _engine.Selection.TypeText(value[i, j].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在指定书签位置上使用输入的二维数组插入图表
        /// </summary>
        /// <param name="bookmark">书签</param>
        /// <param name="value">二维数组</param>
        /// <param name="chartType">图表类型</param>
        /// <param name="rowHeader">行标题（null表示显示index值）</param>
        /// <param name="colHeader">列标题 (null表示显示Column1, Column2值)</param>
        public void InsertGraph(string bookmark, double[,] value, WordChartStyle chartType = null, string[] rowHeader = null, string[] colHeader = null)
        {
            try
            {
                Word.Range r = GetRange(bookmark);
                dynamic oShape;
                WordChartStyle style = chartType == null ? _chartStyle : chartType;
                if (_version > 14.0)
                {
                    //“AddChart2" only support Office 2013 and above version
                    oShape = _wordShapes.AddChart2(-1, (XlChartType)style.ChartStyle, r);
                }
                else
                {
                    oShape = _wordShapes.AddChart((XlChartType)style.ChartStyle, r);
                }

                //Demonstrate use of late bound oChart and oChartApp objects to
                //manipulate the chart object with MSGraph.
                Word.Chart oChart = oShape.Chart;

                //object oChartApp = oChart.Application;

                ArrayDataToGraphData(value, oChart, rowHeader, colHeader, style.FirstColumnAsXAxis);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在指定位置上使用输入的二维数组插入图表
        /// </summary>
        /// <param name="type">插入的方式 (文件开始或文件结束位置）</param>
        /// <param name="value">二维数组</param>
        /// <param name="chartType">图表类型</param>
        /// <param name="rowHeader">行标题（null表示显示index值）</param>
        /// <param name="colHeader">列标题 (null表示显示Column1, Column2值)</param>
        public void InsertGraph(InsertionPoint type, double[,] value, WordChartStyle chartType = null, string[] rowHeader = null, string[] colHeader = null)
        {
            try
            {
                Word.Range r = GetRange(type);
                dynamic oShape;
                WordChartStyle style = chartType == null ? _chartStyle : chartType;
                if (_version > 12.0)
                {
                    oShape = _wordShapes.AddChart2(-1, (XlChartType)style.ChartStyle, r);
                }
                else
                {
                    oShape = _wordShapes.AddChart((XlChartType)style.ChartStyle, r);
                }

                //Demonstrate use of late bound oChart and oChartApp objects to
                //manipulate the chart object with MSGraph.
                Word.Chart oChart = oShape.Chart;

                //object oChartApp = oChart.Application;

                ArrayDataToGraphData(value, oChart, rowHeader, colHeader, style.FirstColumnAsXAxis);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在指定书签位置上插入图片
        /// </summary>
        /// <param name="bookmark">书签</param>
        /// <param name="picturePath">图片档案路径</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        public void InsertPicture(string bookmark, string picturePath, float width, float height)
        {
            try
            {
                Word.Range range = GetRange(bookmark);
                _wordShapes.AddPicture(picturePath, _missing, _missing, range);
                _wordDoc.Application.ActiveDocument.InlineShapes[1].Width = width;
                _wordDoc.Application.ActiveDocument.InlineShapes[1].Height = height;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在指定位置上插入图片
        /// </summary>
        /// <param name="type">插入类型</param>
        /// <param name="picturePath">图片档案路径</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        public void InsertPicture(InsertionPoint type, string picturePath, float width, float height)
        {
            try
            {
                Word.Range range = GetRange(type);
                _wordShapes.AddPicture(picturePath, _missing, _missing, range);
                _wordDoc.Application.ActiveDocument.InlineShapes[1].Width = width;
                _wordDoc.Application.ActiveDocument.InlineShapes[1].Height = height;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        #endregion 写入

        #region Macro

        public void ExecuteMacroFromScript(string macroName, string macoScript, params object[] parameter)
        {
            try
            {
                var vbModule = _wordDoc.VBProject.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule);
                vbModule.CodeModule.AddFromString(macoScript);
                int size = parameter.Length;
                object[] para = new object[30];
                for (int j = 0; j < 30; j++)
                {
                    if (j < size)
                    {
                        para[j] = parameter[j];
                    }
                    else
                    {
                        para[j + size] = _missing;
                    }
                }
                _engine.Run(macroName, para[0], para[1], para[2], para[3], para[4], para[5], para[6], para[7], para[8], para[9], para[10], para[11], para[12], para[13], para[14], para[15], para[16], para[17], para[18], para[19], para[20], para[21], para[22], para[23], para[24], para[25], para[26], para[27], para[28], para[29]);

                _wordDoc.VBProject.VBComponents.Remove(vbModule);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void ExecuteMacroFromFile(string macroName, string macroPath, params object[] parameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(macroPath) && File.Exists(macroPath))
                {
                    var vbModule = _wordDoc.VBProject.VBComponents.Import(macroPath);
                    int size = parameter.Length;
                    object[] para = new object[30];
                    for (int j = 0; j < 30; j++)
                    {
                        if (j < size)
                        {
                            para[j] = parameter[j];
                        }
                        else
                        {
                            para[j + size] = _missing;
                        }
                    }
                    _engine.Run(macroName, para[0], para[1], para[2], para[3], para[4], para[5], para[6], para[7], para[8], para[9], para[10], para[11], para[12], para[13], para[14], para[15], para[16], para[17], para[18], para[19], para[20], para[21], para[22], para[23], para[24], para[25], para[26], para[27], para[28], para[29]);
                    _wordDoc.VBProject.VBComponents.Remove(vbModule);
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        #endregion Macro


        #endregion 公共方法

        #region 私有方法

        private void ArrayDataToGraphData(double[,] value, Word.Chart oChart, string[] rowHeader = null, string[] colHeader = null, bool firstColumnAsXAxis = true)
        {
            string data;
            string rc;
            string rHeader;
            string cHeader;

            Excel.Worksheet series = ((Excel.Workbook)oChart.ChartData.Workbook).Worksheets[1];
            Excel.ListObject list = series.ListObjects[1];
            string colIdx = firstColumnAsXAxis ? "B" : "A";

            list.Resize(series.Cells.get_Range(colIdx + "1", ExcelReport.IntToLetter(value.GetLength(1) + 1) + (value.GetLength(0) + 1).ToString()));

            for (int i = 0; i < value.GetLength(0); i++)
            {
                rHeader = "A" + (i + 2).ToString();
                if (rowHeader != null)
                {
                    series.Cells.get_Range(rHeader, _missing).FormulaR1C1 = rowHeader[i];
                }
                else
                {
                    series.Cells.get_Range(rHeader, _missing).FormulaR1C1 = (i).ToString();
                }

                for (int j = 0; j < value.GetLength(1); j++)
                {
                    data = value[i, j].ToString();
                    rc = ExcelReport.IntToLetter(j + 2) + (i + 2).ToString();
                    series.Cells.get_Range(rc, _missing).FormulaR1C1 = data;

                    cHeader = ExcelReport.IntToLetter(j + 2) + "1";
                    if (i == 0)
                    {
                        if (colHeader != null)
                        {
                            series.Cells.get_Range(cHeader, _missing).FormulaR1C1 = colHeader[j];
                        }
                        else
                        {
                            series.Cells.get_Range(cHeader, _missing).FormulaR1C1 = "Column" + (j).ToString();
                        }
                    }
                }
            }

            series.Activate();
            ((Excel.Workbook)oChart.ChartData.Workbook).Close();
            Thread.Sleep(5);
        }

        private Word.Range GetRange(string bookmark)
        {
            Word.Range r = null;
            if (_wordDoc.Bookmarks.Exists(bookmark))
            {
                foreach (Word.Bookmark item in _wordDoc.Bookmarks)
                {
                    if (item.Name == bookmark)
                    {
                        r = item.Range;
                        break;
                    }
                }
                return r;
            }
            else
            {
                return null;
            }
        }

        private Word.Range GetRange(InsertionPoint type)
        {
            switch (type)
            {
                case InsertionPoint.EndOfDocument:
                    int end = _wordDoc.Range().End;
                    return _wordDoc.Range(end - 1, end);

                case InsertionPoint.BeginningOfDoument:
                    return _wordDoc.Range(0, 1);

                default:
                    return null;
            }
        }

        /// <summary>
        /// 写入指定范围资料的文字外观
        /// </summary>
        /// <param name="range">范围</param>
        /// <param name="font">文字外观</param>
        private void ApplyFont(Word.Range range, WordFont font)
        {
            try
            {
                var f = range.Font;
                f.Name = font.FontName;
                f.Size = font.FontSize;
                f.Bold = font.IsBold;
                f.Italic = font.IsItalic;
                f.Underline = font.IsUnderlined;
                f.StrikeThrough = font.IsStrikedThrough;
                f.Color = font.FontColor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static double DetermineVersion()
        {
            //CreateObject Not Present In C#
            dynamic objEApp = Activator.CreateInstance(Type.GetTypeFromProgID("Word.Application")); //Excel object

            double version = double.Parse(objEApp.Version);

            objEApp.Quit();
            //quit

            objEApp = null;
            return version;
            /*
             * Office 97   -  7.0
             * Office 98   -  8.0
             * Office 2000 -  9.0
             * Office XP   - 10.0
             * Office 2003 - 11.0
             * Office 2007 - 12.0
             * Office 2010 - 14.0 (sic!)
             * Office 2013 - 15.0
             * Office 2016 - 16.0
             */
        }

        private void ErrorParser(Exception ex)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo hresultFieldInfo = typeof(Exception).GetField("_HResult", flags);
            int errcode = (int)hresultFieldInfo.GetValue(ex);
            string description = ErrorHandler.ErrorParsing(errcode);
            description = description != null ? description : ex.Message;
            throw new Exception(string.Format("操作错误，错误代码 ={0}\r\n描述={1} ", errcode, description));
        }

        #endregion 私有方法
    }
}
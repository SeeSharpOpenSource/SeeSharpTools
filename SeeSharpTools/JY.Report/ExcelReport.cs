using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SeeSharpTools.JY.Report
{
    /// <summary>
    /// <para>Excel report operation .</para>
    /// </summary>
    public class ExcelReport
    {
        #region 成员变量

        private object _missing = Missing.Value;
        private Excel._Application _engine;
        private Excel._Workbook _workBook;
        private Excel._Worksheet _workSheet;
        private Excel.Shapes _shape;
        private Excel.Sheets _workSheets;
        private ExcelFont _defaultFont;
        private ExcelChartStyle _chartStyle;

        #endregion 成员变量

        #region 构造函数

        /// <summary>
        /// <param>构造函数，将一个已有Excel工作簿作为模板</param>
        /// </summary>
        /// <param name="templateFilePath"></param>
        public ExcelReport(string templateFilePath)
        {
            try
            {
                //创建一个Application对象并使其可见
                _engine = new Excel.Application();
                _engine.Visible = false;

                if (!File.Exists(templateFilePath))
                {
                    //新建一个WorkBook
                    _workBook = _engine.Workbooks.Add(Type.Missing);
                }
                else
                {
                    //打开模板文件，得到WorkBook对象
                    _workBook = _engine.Workbooks.Add(templateFilePath);
                }
                //得到WorkSheet对象
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;
                _shape = _workSheet.Shapes;
                _workSheets = (Excel.Sheets)_workBook.Sheets;
                _defaultFont = new ExcelFont();
                _chartStyle = new ExcelChartStyle();
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 构造函数，创建Excel工作簿
        /// </summary>
        public ExcelReport()
        {
            try
            {
                //创建一个Application对象并使其可见
                _engine = new Excel.Application();
                _engine.Visible = false;

                //新建一个WorkBook
                _workBook = _engine.Workbooks.Add(Type.Missing);
                //得到WorkSheet对象
                _workSheet = _workBook.ActiveSheet;
                _workSheets = (Excel.Sheets)_workBook.Sheets;

                _shape = _workSheet.Shapes;
                _defaultFont = new ExcelFont();
                _chartStyle = new ExcelChartStyle();
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        #endregion 构造函数

        #region 公共属性

        /// <summary>
        /// 默认配置的文字外观设定
        /// </summary>
        public ExcelFont DefaultFont
        {
            get { return _defaultFont; }
            set { _defaultFont = value; }
        }

        /// <summary>
        /// 返回当前Worksheet工作表的行长度，使用ChangeCurrentWorkSheet方法切换工作表
        /// </summary>
        public int RowCount
        {
            get
            {
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;
                return _workSheet.UsedRange.Rows.Count;

            }
        }

        /// <summary>
        /// 返回当前Worksheet工作表的列长度，使用ChangeCurrentWorkSheet方法切换工作表
        /// </summary>
        public int ColumnCount
        {
            get
            {
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;
                return _workSheet.UsedRange.Columns.Count;

            }

        }
        #endregion 公共属性

        #region 公共方法

        #region 基本操作

        #region WorkSheet操作

        /// <summary>
        /// 改变选择的工作表worksheet
        /// </summary>
        /// <param name="sheetIndex">工作表索引值</param>
        public void ChangeCurrentWorkSheet(int sheetIndex)
        {
            try
            {
                //若指定工作表索引超出范围，则不改变当前工作表
                if (sheetIndex < 1)
                    return;

                if (sheetIndex > this._workBook.Sheets.Count)
                    return;

                this._workSheet = (Excel.Worksheet)this._workBook.Sheets[sheetIndex];
                _workSheet.Select();
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void ChangeCurrentWorkSheet(string sheetName)
        {
            try
            {
                int index = -1;
                for (int i = 0; i < _workSheets.Count; i++)
                {
                    if (_workSheets[i + 1].Name == sheetName)
                    {
                        index = i;
                        break;
                    }
                }

                if (index >= 0)
                {
                    _workSheet = _workBook.Sheets[index + 1];
                    _workSheet.Select();
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void CreateWorksheet(string name)
        {
            try
            {
                _workSheet = _workSheets.Add(_missing, _missing, _missing, _missing);
                _workSheet.Name = name;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void ChangeWorksheetTitle(string sheetName, string title)
        {
            try
            {
                int index = -1;
                _workSheets = _workBook.Sheets;
                for (int i = 0; i < _workSheets.Count; i++)
                {
                    if (_workSheets[i + 1].Name == sheetName)
                    {
                        index = i;
                        break;
                    }
                }

                if (index >= 0)
                {
                    _workSheets[index + 1].Name = title;
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void ChangeWorksheetTitle(int sheetIndex, string title)
        {
            try
            {
                if (sheetIndex < 1)
                    return;

                if (sheetIndex > this._workBook.Sheets.Count)
                    return;

                if (sheetIndex >= 0)
                {
                    _workSheets[sheetIndex + 1].Name = title;
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void DeleteWorkSheet(string sheetName)
        {
            try
            {
                int index = -1;
                _workSheets = _workBook.Sheets;

                for (int i = 0; i < _workSheets.Count; i++)
                {
                    if (_workSheets[i + 1].Name == sheetName)
                    {
                        index = i;
                        break;
                    }
                }

                if (index >= 0)
                {
                    _workSheets[index + 1].Delete();
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void DeleteWorkSheet(int sheetIndex)
        {
            try
            {
                if (sheetIndex < 1)
                    return;

                if (sheetIndex > this._workBook.Sheets.Count)
                    return;

                if (sheetIndex >= 0)
                {
                    _workSheets[sheetIndex + 1].Delete();
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        #endregion WorkSheet操作

        /// <summary>
        /// 隐藏Excel窗体
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

        /// <summary>
        /// 显示Excel窗体
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
        /// 将Excel文件另存为指定格式
        /// </summary>
        /// <param name="fileName">存档路径</param>
        /// <param name="format">支持档案格式：HTML，CSV，TEXT，EXCEL，XML</param>
        public void SaveAs(string fileName, ExcelSaveFormat format)
        {
            try
            {
                _workBook.SaveAs(fileName, _missing, _missing, _missing, _missing, _missing, Excel.XlSaveAsAccessMode.xlNoChange, _missing, _missing, _missing, _missing);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 关闭excel所有进程
        /// </summary>
        public void Close(bool saveChanges = false, string path = "")
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    _workBook.Close(saveChanges, null, null);
                }
                else
                {
                    _workBook.Close(saveChanges, path, null);
                }

                _engine.Quit();
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void Close()
        {
            try
            {
                _workBook.Close(_missing, _missing, _missing);
                _engine.Quit();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// 将Exccel的Row,Col栏位数字转换成字符串(如 1,1变成A1)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public string RCToString(int row, int col)
        {
            try
            {
                if (col == 0)
                {
                    return string.Format("{0}:{1}", row, row);
                }
                else
                {
                    return IntToLetter(col) + row.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        /// <summary>
        /// 将Exccel的栏位字符串转换成Row,Col数字(如A1变成1,1)
        /// </summary>
        /// <param name="cell">栏位字符串</param>
        /// <returns></returns>
        public int[] StringToRC(string cell)
        {
            try
            {
                int[] rc = new int[2] { 0, 0 };
                char[] chars = cell.ToCharArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    if (char.IsLetter(c))
                    {
                        c = char.ToUpper(c);
                        rc[1] = rc[1] * 26 + Convert.ToInt32(c) - 64;
                    }
                    else
                    {
                        rc[0] = rc[0] * 10 + int.Parse(chars[i].ToString());
                    }
                }
                return rc;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        /// <summary>
        /// 将Excel列的字母索引值转换成整数索引值
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public static int LetterToInt(string letter)
        {
            int n = 0;
            char[] chars = letter.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if (char.IsLetter(c))
                {
                    c = char.ToUpper(c);
                    n = n * 26 + Convert.ToInt32(c) - 64;
                }
                else
                {
                    break;
                }
            }
            return n;
        }

        /// <summary>
        /// 将Excel列的整数索引值转换为字符索引值
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string IntToLetter(int n)
        {
            List<char> list = new List<char>();
            int result;
            int residue;
            int dummy = n;

            do
            {
                result = dummy / 26;
                residue = dummy % 26;
                if (residue == 0)

                {
                    result--;
                    residue = 26;
                }
                list.Add(Convert.ToChar(residue + 64));
                dummy = result;
            } while (result != 0);
            list.Reverse();
            char[] chars = list.ToArray();
            return new string(list.ToArray());
        }

        #endregion 基本操作

        #region 写入方法

        /// <summary>
        /// 写入文字到目前工作表中的指定栏位(透过栏位标签)，并可配置文字外观
        /// </summary>
        /// <param name="cellName">excel中已经配置好的栏位标签</param>
        /// <param name="value">写入文字值</param>
        /// <param name="font">文字外观配置</param>
        public void WriteTextToReport<T>(string cellName, T value, ExcelFont font = null)
        {
            try
            {
                //get the range of the cell
                Excel.Range range = GetRange(cellName);
                //update the value
                range.Value2 = value.ToString();
                //update the font
                ExcelFont f = font == null ? _defaultFont : font;
                ApplyFont(range, f);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 写入文字到目前工作表中的指定栏位(行、列数字)，并可配置文字外观（例如行1列1)
        /// </summary>
        /// <param name="row">excel当前的栏位行数字</param>
        /// <param name="col">excel当前的栏位列数字</param>
        /// <param name="value">写入文字值</param>
        /// <param name="font">文字外观配置</param>
        public void WriteTextToReport<T>(int row, int col, T value, ExcelFont font = null)
        {
            try
            {
                //get the range of the cell
                Excel.Range range = GetRange(row, col);
                //update the value
                range.Value2 = value.ToString();
                //update the font
                ExcelFont f = font == null ? _defaultFont : font;
                ApplyFont(range, f);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 写入表格到目前工作表中的指定栏位(透过栏位标签)，并可选择是否移除标题栏位
        /// </summary>
        /// <param name="cellName">excel中已经配置好的栏位标签</param>
        /// <param name="table">写入的表格</param>
        /// <param name="ignoreHeader">true：保留行列的栏位标题</param>
        public void WriteTableToReport(string cellName, System.Data.DataTable table, bool ignoreHeader = false)
        {
            try
            {
                int sizeRow;
                int sizeCol;
                string[,] arr = DataTableToStringArray(table, out sizeRow, out sizeCol, ignoreHeader);

                //get the range of the cell and update the table to it
                GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                //this process make sure the data is saved as the number format instead of string
                GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 写入表格到目前工作表中的指定栏位(行、列数字,例如行1列1)。并可选择是否移除标题栏位
        /// </summary>
        /// <param name="row">excel当前的栏位行数字</param>
        /// <param name="col">excel当前的栏位列数字</param>
        /// <param name="table">写入的表格</param>
        /// <param name="ignoreHeader">true：保留行列的栏位标题</param>
        public void WriteTableToReport(int row, int col, System.Data.DataTable table, bool ignoreHeader = false)
        {
            try
            {
                int sizeRow;
                int sizeCol;
                string[,] arr = DataTableToStringArray(table, out sizeRow, out sizeCol, ignoreHeader);

                //get the range of the cell and update the table to it
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                //this process make sure the data is saved as the number format instead of string
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 写入List到目前工作表中的指定栏位(透过栏位标签)，并可选择是否移除标题栏位。List中可设置自定义类以及数字
        /// </summary>
        /// <param name="cellName">excel中已经配置好的栏位标签</param>
        /// <param name="listData">写入的清单</param>
        /// <param name="ignoreHeader">true：保留行列的栏位标题</param>
        public void WriteListToReport<T>(string cellName, IList<T> listData, bool ignoreHeader = false)
        {
            try
            {
                int sizeRow;
                int sizeCol;
                DataTable dt = ListToDatTable(listData);
                string[,] arr = DataTableToStringArray(dt, out sizeRow, out sizeCol, ignoreHeader);

                //get the range of the cell and update the table to it
                GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                //this process make sure the data is saved as the number format instead of string
                GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 写入List到目前工作表中的指定栏位(行、列数字,例如行1列1)。并可选择是否移除标题栏位。List中可设置自定义类以及数字
        /// </summary>
        /// <param name="row">excel当前的栏位行数字</param>
        /// <param name="col">excel当前的栏位列数字</param>
        /// <param name="listData">写入的清单</param>
        /// <param name="ignoreHeader">true：保留行列的栏位标题</param>
        public void WriteListToReport<T>(int row, int col, IList<T> listData, bool ignoreHeader = false)
        {
            try
            {
                int sizeRow;
                int sizeCol;
                DataTable dt = ListToDatTable(listData);
                string[,] arr = DataTableToStringArray(dt, out sizeRow, out sizeCol, ignoreHeader);

                //get the range of the cell and update the table to it
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                //this process make sure the data is saved as the number format instead of string
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 指定资料来源（A1的格式），在当前工作表的指定栏位(透过栏位标签）插入图表
        /// </summary>
        /// <param name="cellName">excel中已经配置好的栏位标签</param>
        /// <param name="dataRangeStart">资料来源的起始栏位(格式为A1)</param>
        /// <param name="dataRangeEnd">资料来源的结束栏位(格式为A1)</param>
        public void InsertGraph(string cellName, string dataRangeStart, string dataRangeEnd, ExcelChartStyle chartStyle = null)
        {
            try
            {
                Excel.Range r;
                ExcelChartStyle style = chartStyle == null ? _chartStyle : chartStyle;

                //get the range, and insert an chart object
                r = GetRange(cellName);
                Excel.ChartObjects xlCharts = (Excel.ChartObjects)_workSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add((double)r.Left, (double)r.Top, style.ChartWidth, style.ChartHeight);

                //Apply the style of the chart
                Excel._Chart chartPage = myChart.Chart;
                ApplyChartStyle(chartPage, dataRangeStart, dataRangeEnd, style);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 指定资料来源（A1的格式），在当前工作表的指定栏位(行、列数字）插入图表,（例如行1列1)
        /// </summary>
        /// <param name="row">excel当前的栏位行数字</param>
        /// <param name="col">excel当前的栏位列数字</param>
        /// <param name="dataRangeStart">资料来源的起始栏位(格式为A1)</param>
        /// <param name="dataRangeEnd">资料来源的结束栏位(格式为A1)</param>
        public void InsertGraph(int row, int col, string dataRangeStart, string dataRangeEnd, ExcelChartStyle chartStyle = null)
        {
            try
            {
                Excel.Range r;
                ExcelChartStyle style = chartStyle == null ? _chartStyle : chartStyle;

                //get the range, and insert an chart object
                r = GetRange(row, col);
                Excel.ChartObjects xlCharts = (Excel.ChartObjects)_workSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add((double)r.Left, (double)r.Top, style.ChartWidth, style.ChartHeight);

                //Apply the style of the chart
                Excel._Chart chartPage = myChart.Chart;
                ApplyChartStyle(chartPage, dataRangeStart, dataRangeEnd, style);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在当前工作表的指定栏位中写入一维数组，并可指定写入方向
        /// </summary>
        /// <param name="cellName">excel中已经配置好的栏位标签</param>
        /// <param name="data">写入的一维数组</param>
        /// <param name="direction">写入方向</param>
        public void WriteArrayToReport<T>(string cellName, T[] data, WriteArrayDirection direction = WriteArrayDirection.Horizontal)
        {
            try
            {
                int sizeRow;
                int sizeCol;
                string[,] arr = DataArrayToStringArray(data, out sizeRow, out sizeCol, direction);

                GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在当前工作表的指定栏位中写入一维数组，并可指定写入方向
        /// </summary>
        /// <param name="row">excel当前的栏位行数字</param>
        /// <param name="col">excel当前的栏位列数字</param>
        /// <param name="data">写入的一维数组</param>
        /// <param name="direction">写入方向</param>
        public void WriteArrayToReport<T>(int row, int col, T[] data, WriteArrayDirection direction = WriteArrayDirection.Horizontal)
        {
            try
            {
                int sizeRow;
                int sizeCol;
                string[,] arr = DataArrayToStringArray(data, out sizeRow, out sizeCol, direction);

                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在当前工作表的指定栏位中写入二维数组
        /// </summary>
        /// <param name="cellName">excel中已经配置好的栏位标签</param>
        /// <param name="data">写入的二维数组</param>
        public void WriteArrayToReport<T>(string cellName, T[,] data)
        {
            try
            {
                int sizeRow;
                int sizeCol;
                string[,] arr = DataArrayToStringArray(data, out sizeRow, out sizeCol);

                GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(cellName).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        /// <summary>
        /// 在当前工作表的指定栏位中写入二维数组
        /// </summary>
        /// <param name="row">excel当前的栏位行数字</param>
        /// <param name="col">excel当前的栏位列数字</param>
        /// <param name="data">写入的二维数组</param>
        public void WriteArrayToReport<T>(int row, int col, T[,] data)
        {
            try
            {
                int sizeRow;
                int sizeCol;
                string[,] arr = DataArrayToStringArray(data, out sizeRow, out sizeCol);

                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void AppendRow<T>(T[] data, WriteArrayDirection direction = WriteArrayDirection.Horizontal)
        {
            try
            {
                Excel.Range r = _workSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                int row = r.Row * r.Column == 1 ? r.Row : r.Row + 1;
                int col = 1;
                int sizeRow;
                int sizeCol;
                string[,] arr = DataArrayToStringArray(data, out sizeRow, out sizeCol, direction);

                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void AppendRow<T>(T[,] data)
        {
            try
            {
                Excel.Range r = _workSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                int row = r.Row * r.Column == 1 ? r.Row : r.Row + 1;
                int col = 1;
                int sizeRow;
                int sizeCol;
                string[,] arr = DataArrayToStringArray(data, out sizeRow, out sizeCol);

                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void AppendColumn<T>(T[] data, WriteArrayDirection direction = WriteArrayDirection.Horizontal)
        {
            try
            {
                Excel.Range r = _workSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                int row = 1;
                int col = r.Row * r.Column == 1 ? r.Column : r.Column + 1;
                int sizeRow;
                int sizeCol;
                string[,] arr = DataArrayToStringArray(data, out sizeRow, out sizeCol, direction);

                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        public void AppendColumn<T>(T[,] data)
        {
            try
            {
                int dummy = _workSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Column;
                int row = 1;
                int col = dummy == 1 ? dummy : dummy + 1;
                int sizeRow;
                int sizeCol;
                string[,] arr = DataArrayToStringArray(data, out sizeRow, out sizeCol);

                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = arr;
                GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2 = GetRange(row, col).Range["A1", RCToString(sizeRow, sizeCol)].Value2;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return;
            }
        }

        #endregion 写入方法

        #region 读取方法

        /// <summary>
        ///返回指定单元格的内容
        /// </summary>
        /// <param name="iRow">定位的行</param>
        /// <param name="iCol">定位的列</param>
        public string ReadSingleCell(int iRow, int iCol)
        {
            try
            {
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;

                Excel.Range sRange = GetRange(iRow, iCol);
                string returnText = (string)sRange.Text;
                Marshal.ReleaseComObject(sRange);
                sRange = null;
                return returnText;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        /// <summary>
        /// 得到指定单元格的内容
        /// </summary>
        /// <param name="cellName">指定的单元格比如 A1,A2</param>
        public string ReadSingleCell(string cellName)
        {
            try
            {
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;

                string returnValue;
                Excel.Range sRange = GetRange(cellName);
                returnValue = sRange.Cells.Text;
                Marshal.ReleaseComObject(sRange);
                sRange = null;
                return returnValue;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        /// <summary>
        /// 读取一个连续区域的Cell的值(矩形区域，包含一行或一列,或多行，多列)，返回一个一维字符串数组。
        /// </summary>
        /// <param name="startCell">StartCell是要写入区域的左上角单元格</param>
        /// <param name="endCell">EndCell是要写入区域的右下角单元格</param>
        public string[] ReadConsecutiveCells(string startCell, string endCell)
        {
            try
            {
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;

                string[] cellsValue = null;
                Excel.Range sRange = GetRange(StringToRC(startCell)[0], StringToRC(startCell)[1], StringToRC(endCell)[0], StringToRC(endCell)[1]);
                cellsValue = new string[sRange.Count];
                int rowStartIndex = _workSheet.get_Range(startCell, endCell).Row;  //起始行号
                int columnStartIndex = _workSheet.get_Range(startCell, endCell).Column; //起始列号
                int rowNum = sRange.Rows.Count;     //行数目
                int columnNum = sRange.Columns.Count;    //列数目
                object[,] datavaluerange = new object[rowNum, columnNum];
                datavaluerange = sRange.Cells.Value;
                int index = 0;
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < columnNum; j++)
                    {
                        //读到空值null和读到空串""分别处理
                        cellsValue[index] = datavaluerange[i + 1, j + 1] == null ? string.Empty : datavaluerange[i + 1, j + 1].ToString();
                        index++;
                    }
                }
                return (cellsValue);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        /// <summary>
        /// 读取一个连续区域的Cell的值(矩形区域，包含一行或一列,或多行，多列)，返回一个一维字符串数组。
        /// </summary>
        /// <param name="startRow">定位开始Range的Cell的行</param>
        /// <param name="startCol">定位开始Range的Cell的列</param>
        /// <param name="endRow">定位结束Range的Cell的行</param>
        /// <param name="endCol">定位结束Range的Cell的列</param>
        public string[] ReadConsecutiveCells(int startRow, int startCol, int endRow, int endCol)
        {
            try
            {
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;

                string[] cellsValue = null;
                Excel.Range sRange = GetRange(startRow, startCol, endRow, endCol);
                cellsValue = new string[sRange.Count];
                int rowNum = sRange.Rows.Count;     //行数目
                int columnNum = sRange.Columns.Count;    //列数目
                object[,] datavaluerange = new object[rowNum, columnNum];
                datavaluerange = sRange.Cells.Value;
                int index = 0;
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < columnNum; j++)
                    {
                        //读到空值null和读到空串""分别处理
                        cellsValue[index] = datavaluerange[i + 1, j + 1] == null ? string.Empty : datavaluerange[i + 1, j + 1].ToString();
                        index++;
                    }
                }
                return (cellsValue);
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        /// <summary>
        /// 读取一个连续区域的Cell的值(矩形区域，包含一行或一列,或多行，多列)，返回一个二维字符串数组。
        /// </summary>
        /// <param name="startCell">startCell是要写入区域的左上角单元格</param>
        /// <param name="endCell">endCell是要写入区域的右下角单元格</param>
        public string[,] ReadRegionCells(string startCell, string endCell)
        {
            try
            {
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;
                //整体代码计算内容，完成Excel等的报表生成
                Excel.Range sRange = GetRange(StringToRC(startCell)[0], StringToRC(startCell)[1], StringToRC(endCell)[0], StringToRC(endCell)[1]);
                int rowStartIndex = _workSheet.get_Range(startCell, endCell).Row;  //起始行号
                int columnStartIndex = _workSheet.get_Range(startCell, endCell).Column; //起始列号
                int rowNum = sRange.Rows.Count;     //行数目
                int columnNum = sRange.Columns.Count;    //列数目
                object[,] datavaluerange = new object[rowNum, columnNum];
                string[,] cellsValue = new string[rowNum, columnNum];
                datavaluerange = sRange.Cells.Value;
                for (int i = rowStartIndex; i < rowStartIndex + rowNum; i++)
                {
                    for (int j = columnStartIndex; j < columnNum + columnStartIndex; j++)
                    {
                        cellsValue[i - rowStartIndex, j - columnStartIndex] = datavaluerange[i - rowStartIndex + 1, j + 1 - columnStartIndex] == null ? string.Empty : datavaluerange[i - rowStartIndex + 1, j + 1 - columnStartIndex].ToString();
                    }
                }
                return cellsValue;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        /// <summary>
        /// 读取一个连续区域的Cell的值(矩形区域，包含一行或一列,或多行，多列)，返回一个二维字符串数组。
        /// </summary>
        /// <param name="startRow">定位开始Range的Cell的行</param>
        /// <param name="startCol">定位开始Range的Cell的列</param>
        /// <param name="endRow">定位结束Range的Cell的行</param>
        /// <param name="endCol">定位结束Range的Cell的列</param>
        public string[,] ReadRegionCells(int startRow, int startCol, int endRow, int endCol)
        {
            try
            {
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;

                Excel.Range sRange = GetRange(startRow, startCol, endRow, endCol);
                int rowNum = sRange.Rows.Count;     //行数目
                int columnNum = sRange.Columns.Count;    //列数目
                string[,] cellsValue = new string[rowNum, columnNum];
                object[,] datavaluerange = new object[rowNum, columnNum];
                datavaluerange = sRange.Cells.Value;
                for (int i = startRow; i < startRow + rowNum; i++)
                {
                    for (int j = startCol; j < columnNum + startCol; j++)
                    {
                        cellsValue[i - startRow, j - startCol] = datavaluerange[i + 1 - startRow, j + 1 - startCol] == null ? string.Empty : datavaluerange[i + 1 - startRow, j + 1 - startCol].ToString();
                    }
                }
                return cellsValue;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        /// <summary>
        /// 读取当前活动工作簿的所有数据
        /// </summary>
        public DataTable ReadCurrentSheet()
        {
            try
            {
                _workSheet = (Excel.Worksheet)_workBook.ActiveSheet;
                int rows = _workSheet.UsedRange.Rows.Count;//Range[startrange.ToUpper()].CurrentRegion.Rows.Count;
                int columns = _workSheet.UsedRange.Columns.Count;//Range[startrange.ToUpper()].CurrentRegion.Columns.Count;

                //UsedRange.Row和UsedRange.Column属性会包含曾经使用过元素的范围，有时候不够准确
                //int rows = workSheet.UsedRange.Rows.Count;
                //int columns = workSheet.UsedRange.Columns.Count;
                string[,] cellsValue = ReadRegionCells(1, 1, rows, columns);

                System.Data.DataTable dt = new System.Data.DataTable();

                for (int i = 0; i < columns; i++)
                    dt.Columns.Add(IntToLetter(i), typeof(string));
                for (int i = 0; i < cellsValue.GetLength(0); i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < cellsValue.GetLength(1); j++)
                        dr[j] = cellsValue[i, j];
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        public DataTable ReadSheet(string sheetName)
        {
            try
            {
                ChangeCurrentWorkSheet(sheetName);
                int rows = _workSheet.UsedRange.Rows.Count;//Range[startrange.ToUpper()].CurrentRegion.Rows.Count;
                int columns = _workSheet.UsedRange.Columns.Count;//Range[startrange.ToUpper()].CurrentRegion.Columns.Count;

                //UsedRange.Row和UsedRange.Column属性会包含曾经使用过元素的范围，有时候不够准确
                //int rows = workSheet.UsedRange.Rows.Count;
                //int columns = workSheet.UsedRange.Columns.Count;
                string[,] cellsValue = ReadRegionCells(1, 1, rows, columns);

                System.Data.DataTable dt = new System.Data.DataTable();

                for (int i = 0; i < columns; i++)
                    dt.Columns.Add(IntToLetter(i), typeof(string));
                for (int i = 0; i < cellsValue.GetLength(0); i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < cellsValue.GetLength(1); j++)
                        dr[j] = cellsValue[i, j];
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        public DataTable ReadSheet(int sheetIndex)
        {
            try
            {
                ChangeCurrentWorkSheet(sheetIndex);
                int rows = _workSheet.UsedRange.Rows.Count;//Range[startrange.ToUpper()].CurrentRegion.Rows.Count;
                int columns = _workSheet.UsedRange.Columns.Count;//Range[startrange.ToUpper()].CurrentRegion.Columns.Count;

                //UsedRange.Row和UsedRange.Column属性会包含曾经使用过元素的范围，有时候不够准确
                //int rows = workSheet.UsedRange.Rows.Count;
                //int columns = workSheet.UsedRange.Columns.Count;
                string[,] cellsValue = ReadRegionCells(1, 1, rows, columns);

                System.Data.DataTable dt = new System.Data.DataTable();

                for (int i = 0; i < columns; i++)
                    dt.Columns.Add(IntToLetter(i), typeof(string));
                for (int i = 0; i < cellsValue.GetLength(0); i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < cellsValue.GetLength(1); j++)
                        dr[j] = cellsValue[i, j];
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }


        /// <summary>
        /// 读取一个Excel文件的多个workSheet，返回一个DataSet实例
        /// </summary>
        public DataSet ReadAllSheets()
        {
            try
            {
                _workSheets = _workBook.Sheets;
                DataSet ds = new DataSet();
                for (int k = 1; k <= _workSheets.Count; k++)
                {
                    ChangeCurrentWorkSheet(k);
                    _workSheet = (Excel.Worksheet)_workSheets[k];
                    //查找列数
                    int rowCount = _workSheet.UsedRange.Rows.Count;
                    //查找行数
                    int columnCount = _workSheet.UsedRange.Columns.Count;

                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.TableName = _workSheet.Name;
                    string[,] cellsValue = ReadRegionCells(1, 1, rowCount, columnCount);
                    for (int i = 0; i < columnCount; i++)
                        dt.Columns.Add(IntToLetter(i), typeof(string));
                    for (int i = 0; i < cellsValue.GetLength(0); i++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < cellsValue.GetLength(1); j++)
                            dr[j] = cellsValue[i, j];
                        dt.Rows.Add(dr);
                    }

                    ds.Tables.Add(dt);
                }
                return ds;
            }
            catch (Exception ex)
            {
                ErrorParser(ex);
                return null;
            }
        }

        #endregion 读取方法

        #region Macro

        public void ExecuteMacroFromScript(string macroName, string macoScript, params object[] parameter)
        {
            try
            {
                var vbModule = _workBook.VBProject.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule);
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

                _workBook.VBProject.VBComponents.Remove(vbModule);
            }
            catch (Exception ex)
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
                FieldInfo hresultFieldInfo = typeof(Exception).GetField("_HResult", flags);
                if ((int)hresultFieldInfo.GetValue(ex) == -2146827284)
                {
                    MessageBox.Show("未开启Excel对于VBA的信任配置，Macro操作无效，错误代码-2146827284 ");
                    return;
                }
                else
                {
                    ErrorParser(ex);
                    return;
                }
            }
        }

        public void ExecuteMacroFromFile(string macroName, string macroPath, params object[] parameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(macroPath) && File.Exists(macroPath))
                {
                    var vbModule = _workBook.VBProject.VBComponents.Import(macroPath);
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
                    _workBook.VBProject.VBComponents.Remove(vbModule);
                }
            }
            catch (Exception ex)
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
                FieldInfo hresultFieldInfo = typeof(Exception).GetField("_HResult", flags);

                if ((int)hresultFieldInfo.GetValue(ex) == -2146827284)
                {
                    MessageBox.Show("未开启Excel对于VBA的信任配置，Macro操作无效，错误代码-2146827284 ");
                    return;
                }
                else
                {
                    ErrorParser(ex);
                    return;
                }
            }
        }

        #endregion Macro

        #endregion 公共方法

        #region 私有方法

        /// <summary>
        /// 获取当前工作表的指定范围
        /// </summary>
        /// <param name="name">栏位字符串或标签(如为空，则返回目前使用范围)</param>
        /// <returns></returns>
        private Excel.Range GetRange(string cellLabel)
        {
            //仿照 LabVIEW 流程
            if (!string.IsNullOrEmpty(cellLabel))
            {
                return _workSheet.Range[cellLabel];
            }
            else
            {
                return _workSheet.UsedRange;
            }
        }

        /// <summary>
        /// 获取当前工作表的指定范围
        /// </summary>
        /// <param name="startRowIdx">起始行索引(从1开始）</param>
        /// <param name="startColIdx">起始列索引(从1开始）</param>
        /// <returns></returns>
        private Excel.Range GetRange(int startRowIdx, int startColIdx)
        {
            //仿照 LabVIEW 流程
            if (startColIdx + startRowIdx == -2 | startColIdx == 0 | startRowIdx == 0)
            {
                return _workSheet.UsedRange;
            }
            else
            {
                return _workSheet.Range[RCToString(startRowIdx, startColIdx)];
            }
        }

        /// <summary>
        /// 获取当前工作表的指定范围
        /// </summary>
        /// <param name="name">栏位字符串或标签(如为空，则返回目前使用范围)</param>
        /// <returns></returns>
        private Excel.Range GetRange(string startCellName, string endCellName)
        {
            if (!string.IsNullOrEmpty(startCellName) & !string.IsNullOrEmpty(endCellName))
            {
                return _workSheet.Range[startCellName, endCellName];
            }
            else
            {
                return _workSheet.UsedRange;
            }
        }

        /// <summary>
        /// 获取当前工作表的指定范围
        /// </summary>
        /// <param name="startRowIdx">起始行索引(从1开始）</param>
        /// <param name="startColIdx">起始列索引(从1开始）</param>
        /// <param name="endRowIdx">结束行索引(从1开始）</param>
        /// <param name="endColIdx">结束列索引(从1开始）</param>
        /// <returns></returns>
        private Excel.Range GetRange(int startRowIdx, int startColIdx, int endRowIdx, int endColIdx)
        {
            //仿照 LabVIEW 流程
            if (startColIdx + startRowIdx + endColIdx + endRowIdx == -4 | startColIdx == 0 | startRowIdx == 0 | endColIdx == 0 | endRowIdx == 0)
            {
                return _workSheet.UsedRange;
            }
            else
            {
                if (endRowIdx + endColIdx == -2)
                {
                    if (startColIdx > 0 & startRowIdx > 0)
                    {
                        return _workSheet.Range[RCToString(startRowIdx, startColIdx)];
                    }
                    else
                    {
                        if (startColIdx != -1)
                        {
                            return (Excel.Range)_workSheet.UsedRange.Rows.Item[startRowIdx];
                        }
                        else
                        {
                            return (Excel.Range)_workSheet.UsedRange.Columns.Item[startColIdx];
                        }
                    }
                }
                else
                {
                    return _workSheet.Range[RCToString(startRowIdx, startColIdx), RCToString(endRowIdx, endColIdx)];
                }
            }
        }

        private string[,] DataTableToStringArray(DataTable table, out int sizeRow, out int sizeCol, bool ignoreHeader = false)
        {
            string[,] arr;
            if (ignoreHeader)
            {
                //elimanate the header of the DataTable
                sizeCol = table.Columns.Count;
                sizeRow = table.Rows.Count;

                arr = new string[sizeRow, sizeCol];

                for (int j = 0; j < sizeRow; j++)
                {
                    for (int k = 0; k < sizeCol; k++)
                    {
                        arr[j, k] = table.Rows[j][k].ToString();
                    }
                }
            }
            else
            {
                //add header of the datatable to the first row and first column
                sizeCol = table.Columns.Count + 1;
                sizeRow = table.Rows.Count + 1;

                arr = new string[sizeRow, sizeCol];

                for (int i = 0; i < sizeRow - 1; i++)
                {
                    arr[i + 1, 0] = i.ToString();

                    for (int j = 0; j < sizeCol - 1; j++)
                    {
                        if (i == 0)
                        {
                            arr[0, j + 1] = table.Columns[j].ColumnName.ToString();
                        }
                        arr[i + 1, j + 1] = table.Rows[i][j].ToString();
                    }
                }
            }
            return arr;
        }

        private string[,] DataArrayToStringArray<T>(T[] data, out int sizeRow, out int sizeCol, WriteArrayDirection direction = WriteArrayDirection.Horizontal)
        {
            sizeRow = sizeCol = 1;
            switch (direction)
            {
                case WriteArrayDirection.Vertical:
                    sizeRow = data.Length;
                    break;

                case WriteArrayDirection.Horizontal:
                    sizeCol = data.Length;
                    break;

                default:
                    break;
            }

            string[,] arr = new string[sizeRow, sizeCol];

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (direction == WriteArrayDirection.Horizontal)
                    {
                        arr[i, j] = data[j].ToString();
                    }
                    else if (direction == WriteArrayDirection.Vertical)
                    {
                        arr[i, j] = data[i].ToString();
                    }
                }
            }

            return arr;
        }

        private string[,] DataArrayToStringArray<T>(T[,] data, out int sizeRow, out int sizeCol)
        {
            string[,] arr;
            sizeRow = data.GetLength(0);
            sizeCol = data.GetLength(1);

            arr = new string[sizeRow, sizeCol];

            for (int j = 0; j < sizeRow; j++)
            {
                for (int k = 0; k < sizeCol; k++)
                {
                    arr[j, k] = data[j, k].ToString();
                }
            }
            return arr;
        }

        /// <summary>
        /// 写入指定范围资料的文字外观
        /// </summary>
        /// <param name="range">目前worksheet的制定范围</param>
        /// <param name="font">文字外观配置</param>
        private void ApplyFont(Excel.Range range, ExcelFont font)
        {
            try
            {
                var f = range.Characters.Font;
                f.Name = font.FontName;
                f.Size = font.FontSize;
                f.Bold = font.IsBold;
                f.Italic = font.IsItalic;
                f.Underline = font.IsUnderlined;
                f.Strikethrough = font.IsStrikedThrough;
                f.Color = font.FontColor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ApplyChartStyle(Excel._Chart chartPage, string dataRangeStart, string dataRangeEnd, ExcelChartStyle style)
        {
            Excel.Range chartRange;
            if (style.FirstColumnAsXAxis)
            {
                int startRowIdx = StringToRC(dataRangeStart)[0];
                int xColumnIndex = StringToRC(dataRangeStart)[1];
                int startColIdx = xColumnIndex + 1;
                int endRowIdx = StringToRC(dataRangeEnd)[0];
                int endColIdx = StringToRC(dataRangeEnd)[1];
                chartRange = GetRange(startRowIdx, startColIdx , endRowIdx, endColIdx);
                chartPage.SetSourceData(chartRange, Excel.XlRowCol.xlColumns);
                for (int i = 0; i < chartPage.SeriesCollection().Count; i++)
                {
                    chartPage.SeriesCollection(i + 1).XValues = GetRange(startRowIdx, xColumnIndex, endRowIdx, xColumnIndex);
                }
            }
            else
            {
                chartRange = GetRange(StringToRC(dataRangeStart)[0], StringToRC(dataRangeStart)[1], StringToRC(dataRangeEnd)[0], StringToRC(dataRangeEnd)[1]);
                chartPage.SetSourceData(chartRange, Excel.XlRowCol.xlColumns);
            }
            chartPage.ChartType = (Excel.XlChartType)style.ChartStyle;
        }

        /// <summary>
        /// 结束EXE进程
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        private void KillSpecialExcel()
        {
            _workBook.Close(null, null, null);
            _engine.Quit();
            if (_engine != null)
            {
                int lpdwProcessId;
                GetWindowThreadProcessId(new IntPtr(_engine.Hwnd), out lpdwProcessId);

                System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
            }
        }

        private void KillSpecialExcel(bool saveChanges, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                _workBook.Close(saveChanges, null, null);
            }
            else
            {
                _workBook.Close(saveChanges, path, null);
            }

            _engine.Quit();
            if (_engine != null)
            {
                int lpdwProcessId;
                GetWindowThreadProcessId(new IntPtr(_engine.Hwnd), out lpdwProcessId);

                System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
            }
        }

        private void ErrorParser(Exception ex)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo hresultFieldInfo = typeof(Exception).GetField("_HResult", flags);

            int errcode = (int)hresultFieldInfo.GetValue(ex);
            string description = ErrorHandler.ErrorParsing(errcode);

            throw new Exception(string.Format("操作错误，错误代码 ={0}\r\n描述={1} ", errcode, description));
        }

        private DataTable ListToDatTable<T>(IList<T> listData)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();


            if (typeof(T).IsClass&&typeof(T).Name!="String")
            {
                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                foreach (T item in listData)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }
                return table;
            }
            else
            {
                table.Columns.Add("Value");
                foreach (T item in listData)
                {                                       
                    table.Rows.Add(item.ToString());
                }
                return table;

            }

        }
        #endregion 私有方法
    }
}
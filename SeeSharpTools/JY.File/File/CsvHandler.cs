using System;
using System.Collections;
using System.IO;
using System.Text;
using SeeSharpTools.JY.File.Common;
using SeeSharpTools.JY.File.Common.i18n;

namespace SeeSharpTools.JY.File
{
    /// <summary>
    /// Csv文件数据读写
    /// Csv file write and Read.
    /// </summary>
    public class CsvHandler
    {
        #region 私有变量
        private const string Delims = ",";

        private const string FileExtName = "csv";

        private static I18nEntity i18n = I18nEntity.GetInstance(I18nLocalWrapper.Name);

        // 禁止类被实例化
        private CsvHandler()
        {
            throw new NotSupportedException("Class should not be instantiationed.");
        }
        #endregion

        #region 公共方法

        #region 读取接口


        /// <summary>
        /// 在csv文件中设置起始索引值，读取二维数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">要读取的起始行索引</param>
        /// <param name="startColumn">要读取的起始列索引</param>
        /// <param name="rowSize">要读取的行数，若为-1则读取全部行</param>
        /// <param name="columnSize">要读取的列数,若为-1读取全部列</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static T[,] Read<T>(long startRow=0, long startColumn=0, long rowSize = -1, long columnSize = -1, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return Read<T>(filePath, startRow, startColumn, rowSize, columnSize, encoding);
        }

        /// <summary>
        /// 在csv文件中设置起始索引值，读取二维数组，需配置文件路径。
        /// </summary>
        /// <param name="startRow">要读取的起始行索引</param>
        /// <param name="filePath">csv文件路径和名称</param>
        /// <param name="startColumn">要读取的起始列索引</param>
        /// <param name="rowSize">要读取的行数，若为-1则读取全部行</param>
        /// <param name="columnSize">要读取的列数,若为-1则读取全部列</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static T[,] Read<T>(string filePath, long startRow = 0, long startColumn = 0, long rowSize = -1, long columnSize = -1, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<T>(filePath, startRow, startColumn, rowSize, columnSize, encoding);
        }

        /// <summary>
        /// 设置起始行和读取的列索引， 在csv文件中读取数据，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">要读取的起始行索引</param>
        /// <param name="columns">要读取的起始列索引</param>
        /// <param name="rowSize">要读取的行数，若为-1则读取全部行</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static T[,] Read<T>(long startRow, long[] columns, long rowSize = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return Read<T>(filePath, startRow, columns, rowSize, encoding);
        }

        /// <summary>
        /// 设置起始行和读取的列索引， 在csv文件中读取数据，需配置文件路径。
        /// </summary>
        /// <param name="startRow">要读取的起始行索引</param>
        /// <param name="filePath">csv文件路径和名称</param>
        /// <param name="columns">要读取的起始列索引集合</param>
        /// <param name="rowSize">要读取的行数，若为-1则读取全部行</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static T[,] Read<T>(string filePath, long startRow, long[] columns, long rowSize = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            uint[] columnCollection = new uint[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                columnCollection[i] = (uint) columns[i];
            }
            return StreamReadData<T>(filePath, (uint)startRow, columnCollection, (uint)rowSize, encoding);
        }


        /// <summary>
        /// 在csv文件中设置要读取的行列索引值数组，读取二维数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="rowCollection">要读取的行的索引集合</param>
        /// <param name="columnCollection">要读取的列的索引集合</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static T[,] Read<T>(long[] rowCollection, long[] columnCollection, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return Read<T>(filePath, rowCollection, columnCollection, encoding);
        }

        /// <summary>
        ///  在csv文件中设置要读取的行列索引值数组，读取二维数组，需配置文件路径。
        /// </summary>
        /// <param name="filePath">csv文件路径和名称</param>
        /// <param name="rowCollection">要读取的行的索引集合</param>
        /// <param name="columnCollection">要读取的列的索引集合</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static T[,] Read<T>(string filePath, long[] rowCollection, long[] columnCollection, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<T>(filePath, rowCollection, columnCollection, encoding);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="majorOrder">读取数值的方向</param>
        /// <param name="index">读取的行/列索引值</param>
        /// <param name="startIndex">要读取的行/列的起始索引位置</param>
        /// <param name="size">读取的数组长度，若为-1则读取全部</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static T[] Read<T>(MajorOrder majorOrder ,long index, long startIndex=0, long size=-1,  Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return Read<T>(filePath, majorOrder, index, startIndex, size,encoding);
        }
        /// <summary>
        /// 在csv文件中设置要读取的行列索引值，读取一维数组，需配置文件路径。
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="filePath">csv文件路径和名称</param>
        /// <param name="majorOrder">读取数值的方向</param>
        /// <param name="index">读取的行/列索引值</param>
        /// <param name="startPosition">要读取的行/列的起始索引位置</param>
        /// <param name="size">读取的数组长度，若为-1则读取全部</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static T[] Read<T>(string filePath, MajorOrder majorOrder , long index,long startPosition=0, long size=-1,  Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<T>(filePath, index, startPosition, size, MajorOrder.Column == majorOrder, encoding);
        }

        #region Obsolete
        /// <summary>
        /// 在 csv文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static string[,] ReadData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static string[,] ReadData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维string数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static string[,] ReadData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<string>(filePath, startRow, startColumn, rowCount, encoding);

            //            string[] fileDatas = FileUtil.ReadFileDataAsString(filePath);
            //            return FileUtil.GetStrData(fileDatas, Delims);
        }
        /// <summary>
        /// 在 csv文件中读取数据为二维string数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static string[,] ReadData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<string>(filePath, startRow, columns, rowCount, encoding);

            //            string[] fileDatas = FileUtil.ReadFileDataAsString(filePath);
            //            return FileUtil.GetStrData(fileDatas, Delims);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维double数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static double[,] ReadDoubleData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadDoubleData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维double数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static double[,] ReadDoubleData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadDoubleData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维double数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static double[,] ReadDoubleData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<double>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维double数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static double[,] ReadDoubleData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<double>(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维int数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static float[,] ReadFloatData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadFloatData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维float数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static float[,] ReadFloatData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadFloatData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维float数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static float[,] ReadFloatData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<float>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维float数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static float[,] ReadFloatData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<float>(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维int数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static int[,] ReadIntData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadIntData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维int数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static int[,] ReadIntData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadIntData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维int数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static int[,] ReadIntData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<int>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维int数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static int[,] ReadIntData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<int>(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维uint数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static uint[,] ReadUIntData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUIntData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维uint数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static uint[,] ReadUIntData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUIntData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维uint数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static uint[,] ReadUIntData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<uint>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维uint数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static uint[,] ReadUIntData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<uint>(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维short数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static short[,] ReadShortData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadShortData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维short数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static short[,] ReadShortData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadShortData(filePath, startRow, columns, rowCount, encoding);
        }


        /// <summary>
        /// 在 csv文件中读取数据为二维short数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static short[,] ReadShortData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<short>(filePath, startRow, startColumn, rowCount, encoding);
        }


        /// <summary>
        /// 在 csv文件中读取数据为二维short数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static short[,] ReadShortData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<short>(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维ushort数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static ushort[,] ReadUShortData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUShortData(filePath, startRow, startColumn, rowCount, encoding);
        }


        /// <summary>
        /// 在 csv文件中读取数据为二维ushort数组，通过弹窗选择文件路径。
        /// </summary>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static ushort[,] ReadUShortData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUShortData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维ushort数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="startColumn">读取的起始列索引号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static ushort[,] ReadUShortData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<ushort>(filePath, startRow, startColumn, rowCount, encoding);
        }


        /// <summary>
        /// 在 csv文件中读取数据为二维ushort数组
        /// </summary>
        /// <param name="filePath">待读取文件的完整路径</param>
        /// <param name="startRow">读取的起始行索引号，从0开始</param>
        /// <param name="columns">读取的列号，从0开始</param>
        /// <param name="rowCount">读取的行数，等于0时读取所有行</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        /// <returns></returns>
        [Obsolete]
        public static ushort[,] ReadUShortData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<ushort>(filePath, startRow, columns, rowCount, encoding);
        }

        #endregion


        #endregion
        
        #region 写入接口



        /// <summary>
        /// 在csv文件中写入数据，通过弹窗选择文件路径,需指定数据类型
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">待写入文件的一维数组</param>
        /// <param name="writeMode">文件已存在时的写入模式</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        public static void WriteData<T>(T[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入数据，通过弹窗选择文件路径,需指定数据类型
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="filePath">待写入文件的完整路径和名称</param>
        /// <param name="data">待写入文件的一维数组</param>
        /// <param name="writeMode">文件已存在时的写入模式</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        public static void WriteData<T>(string filePath, T[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.Length, 1, writeMode, encoding);
        }


        /// <summary>
        /// 在csv文件中写入数据，通过弹窗选择文件路径,需指定数据类型
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">待写入文件的二维数组</param>
        /// <param name="writeMode">文件已存在时的写入模式</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        public static void WriteData<T>(T[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }


        /// <summary>
        /// 在csv文件中写入数据，通过弹窗选择文件路径,需指定数据类型
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="filePath">待写入文件的完整路径和名称</param>
        /// <param name="data">待写入文件的二维数组</param>
        /// <param name="writeMode">文件已存在时的写入模式</param>
        /// <param name="encoding">文件的编码格式。encoding为null时使用系统默认的编码格式</param>
        public static void WriteData<T>(string filePath, T[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.GetLength(0), data.GetLength(1), writeMode, encoding);
        }


        #endregion

        #endregion

        #region 私有方法


        private static TDataType[] StreamReadData<TDataType>(string filePath, long index, long startIndex, long size,bool majorOrder, Encoding encoding)
        {

            StreamReader reader = null;
            try
            {
                if (majorOrder == false)
                {
                    FileUtil.InitReadStream(ref reader, filePath, encoding);//获取文件中所有数据在reader里
                    TDataType[] data = FileUtil.StreamReadFromStrFile<TDataType>(reader, index, startIndex, size, majorOrder, Delims);
                    return data;

                }
                else
                {
                    //如果行数为-1，则读取全部，即文件的总行数-起始行
                    if (size == -1)
                    {
                        size = FileUtil.GetFileLineNum(filePath);
                    }
                    FileUtil.InitReadStream(ref reader, filePath, encoding);//获取文件中所有数据在reader里
                    TDataType[] data = FileUtil.StreamReadFromStrFile<TDataType>(reader, index, startIndex, size, majorOrder, Delims);
                    return data;

                }

            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (ApplicationException ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError,
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
            }
        }

        private static TDataType[,] StreamReadData<TDataType>(string filePath, long startRow, long startColumn, Encoding encoding)
        {
            StreamReader reader = null;
            try
            {

                //如果行数为0，则读取全部，即文件的总行数-起始行
                long lineCount = FileUtil.GetFileLineNum(filePath) - startRow;
                FileUtil.InitReadStream(ref reader, filePath, encoding);//获取文件中所有数据在reader里
                return FileUtil.StreamReadFromStrFile<TDataType>(reader, startRow, startColumn, lineCount, 0, Delims);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (ApplicationException ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError,
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
            }
        }

        private static TDataType[,] StreamReadData<TDataType>(string filePath, long[] rowCollection, long[] columnCollection, Encoding encoding)
        {
            StreamReader reader = null;
            uint rowCount = (uint)rowCollection.Length;//行数
            uint columnCount = (uint)rowCollection.Length;//列数
            try
            {
                if (0 == rowCount)
                {
                    //如果行数为0，则读取全部，即文件的总行数-起始行
                    rowCount = FileUtil.GetFileLineNum(filePath);
                    for (int i = 0; i < rowCount; i++)
                    {
                        rowCollection[i] = i;
                    }
                }

                FileUtil.InitReadStream(ref reader, filePath, encoding);//获取文件中所有数据在reader里
                return FileUtil.StreamReadFromStrFile<TDataType>(reader, rowCollection, columnCollection, Delims);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (ApplicationException ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError,
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
            }
        }

        private static TDataType[,] StreamReadData<TDataType>(string filePath, long startRow, long startColumn, long rowCount, long columnCount, Encoding encoding)
        {
            StreamReader reader = null;
            try
            {

                if (-1== rowCount)
                {
                    //如果行数为0，则读取全部，即文件的总行数-起始行
                    rowCount = FileUtil.GetFileLineNum(filePath) - startRow;
                }
                FileUtil.InitReadStream(ref reader, filePath, encoding);//获取文件中所有数据在reader里
                return FileUtil.StreamReadFromStrFile<TDataType>(reader, startRow, startColumn, rowCount, columnCount, Delims);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (ApplicationException ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError,
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
            }
        }

        private static void StreamWriteData(string filePath, IEnumerator enumerator,
            int rowCount, int colCount, WriteMode writeMode, Encoding encoding)
        {
            StreamWriter writer = null;
            try
            {
                FileUtil.InitWriteStream(ref writer, filePath, writeMode, encoding);
                FileUtil.StreamWriteStrToFile(writer, enumerator, rowCount,
                    colCount, Delims);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (ApplicationException ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError,
                    i18n.GetFStr("Runtime.WriteFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(writer);
            }
        }

        
        private static TDataType[,] StreamReadData<TDataType>(string filePath, uint startRow, uint startColumn, uint lineCount, Encoding encoding)
        {
            StreamReader reader = null;
            try
            {
                if (0 == lineCount)
                {
                    lineCount = FileUtil.GetFileLineNum(filePath) - startRow;
                }
                FileUtil.InitReadStream(ref reader, filePath, encoding);
                return FileUtil.StreamReadFromStrFile<TDataType>(reader, lineCount, Delims, startRow, startColumn);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (ApplicationException ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError,
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
            }
        }

        private static TDataType[,] StreamReadData<TDataType>(string filePath, uint startRow, uint[] columns, uint lineCount, Encoding encoding)
        {
            StreamReader reader = null;
            try
            {
                if (0 == lineCount)
                {
                    lineCount = FileUtil.GetFileLineNum(filePath) - startRow;
                }
                FileUtil.InitReadStream(ref reader, filePath, encoding);
                return FileUtil.StreamReadFromStrFile<TDataType>(reader, lineCount, Delims, startRow, columns);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (ApplicationException ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError,
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
            }
        }
        #endregion
    }
}
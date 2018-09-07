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
        /// 在csv文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional string array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional string array</para>
        ///         <para>Chinese Simplified:读取后的二维string数组</para>
        ///     </returns>
        /// </summary>
        public static string[,] ReadData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional string array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional string array</para>
        ///         <para>Chinese Simplified:读取后的二维string数组</para>
        ///     </returns>
        /// </summary>
        public static string[,] ReadData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维string数组。
        /// Read data from csv file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional string array</para>
        ///         <para>Chinese Simplified:读取后的二维string数组</para>
        ///     </returns>
        /// </summary>
        public static string[,] ReadData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<string>(filePath, startRow, startColumn, rowCount, encoding);

//            string[] fileDatas = FileUtil.ReadFileDataAsString(filePath);
//            return FileUtil.GetStrData(fileDatas, Delims);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维string数组。
        /// Read data from csv file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional string array</para>
        ///         <para>Chinese Simplified:读取后的二维string数组</para>
        ///     </returns>
        /// </summary>
        public static string[,] ReadData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<string>(filePath, startRow, columns, rowCount, encoding);

            //            string[] fileDatas = FileUtil.ReadFileDataAsString(filePath);
            //            return FileUtil.GetStrData(fileDatas, Delims);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维double数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional double array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static double[,] ReadDoubleData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadDoubleData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维double数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional double array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static double[,] ReadDoubleData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadDoubleData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维double数组。
        /// Read data from csv file as two dimensional double array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static double[,] ReadDoubleData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<double>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维double数组。
        /// Read data from csv file as two dimensional double array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static double[,] ReadDoubleData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<double>(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维float数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional float array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns>
        ///         <para>The read two dimensional float array</para>
        ///         <para>Chinese Simplified:读取后的二维float数组</para>
        ///     </returns>
        /// </summary>
        public static float[,] ReadFloatData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadFloatData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维float数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional float array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional float array</para>
        ///         <para>Chinese Simplified:读取后的二维float数组</para>
        ///     </returns>
        /// </summary>
        public static float[,] ReadFloatData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadFloatData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维float数组。
        /// Read data from csv file as two dimensional float array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional float array</para>
        ///         <para>Chinese Simplified:读取后的二维float数组</para>
        ///     </returns>
        /// </summary>
        public static float[,] ReadFloatData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<float>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维float数组。
        /// Read data from csv file as two dimensional float array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional float array</para>
        ///         <para>Chinese Simplified:读取后的二维float数组</para>
        ///     </returns>
        /// </summary>
        public static float[,] ReadFloatData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<float>(filePath, startRow, columns, rowCount, encoding);
        }


        /// <summary>
        /// 在csv文件中读取数据为二维int数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional int array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional int array</para>
        ///         <para>Chinese Simplified:读取后的二维int数组</para>
        ///     </returns>
        /// </summary>
        public static int[,] ReadIntData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadIntData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维int数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional int array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional int array</para>
        ///         <para>Chinese Simplified:读取后的二维int数组</para>
        ///     </returns>
        /// </summary>
        public static int[,] ReadIntData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadIntData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维int数组。
        /// Read data from csv file as two dimensional int array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional int array</para>
        ///         <para>Chinese Simplified:读取后的二维int数组</para>
        ///     </returns>
        /// </summary>
        public static int[,] ReadIntData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<int>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维int数组。
        /// Read data from csv file as two dimensional int array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional int array</para>
        ///         <para>Chinese Simplified:读取后的二维int数组</para>
        ///     </returns>
        /// </summary>
        public static int[,] ReadIntData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<int>(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维uint数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional uint array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional uint array</para>
        ///         <para>Chinese Simplified:读取后的二维uint数组</para>
        ///     </returns>
        /// </summary>
        public static uint[,] ReadUIntData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUIntData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维uint数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional uint array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional uint array</para>
        ///         <para>Chinese Simplified:读取后的二维uint数组</para>
        ///     </returns>
        /// </summary>
        public static uint[,] ReadUIntData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUIntData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维uint数组。
        /// Read data from csv file as two dimensional uint array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional uint array</para>
        ///         <para>Chinese Simplified:读取后的二维uint数组</para>
        ///     </returns>
        /// </summary>
        public static uint[,] ReadUIntData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<uint>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维uint数组。
        /// Read data from csv file as two dimensional uint array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional uint array</para>
        ///         <para>Chinese Simplified:读取后的二维uint数组</para>
        ///     </returns>
        /// </summary>
        public static uint[,] ReadUIntData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<uint>(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维short数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional short array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional short array</para>
        ///         <para>Chinese Simplified:读取后的二维short数组</para>
        ///     </returns>
        /// </summary>
        public static short[,] ReadShortData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadShortData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维short数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional short array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional short array</para>
        ///         <para>Chinese Simplified:读取后的二维short数组</para>
        ///     </returns>
        /// </summary>
        public static short[,] ReadShortData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadShortData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维short数组。
        /// Read data from csv file as two dimensional short array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional short array</para>
        ///         <para>Chinese Simplified:读取后的二维short数组</para>
        ///     </returns>
        /// </summary>
        public static short[,] ReadShortData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<short>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维short数组。
        /// Read data from csv file as two dimensional short array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional short array</para>
        ///         <para>Chinese Simplified:读取后的二维short数组</para>
        ///     </returns>
        /// </summary>
        public static short[,] ReadShortData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<short>(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维ushort数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional ushort array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional ushort array</para>
        ///         <para>Chinese Simplified:读取后的二维ushort数组</para>
        ///     </returns>
        /// </summary>
        public static ushort[,] ReadUShortData(uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUShortData(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维ushort数组，通过弹窗选择文件路径。
        /// Read data from csv file as two dimensional ushort array. File path can be choosen from the GUI.
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional ushort array</para>
        ///         <para>Chinese Simplified:读取后的二维ushort数组</para>
        ///     </returns>
        /// </summary>
        public static ushort[,] ReadUShortData(uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUShortData(filePath, startRow, columns, rowCount, encoding);
        }

        /// <summary>
        /// 在csv文件中读取数据为二维ushort数组。
        /// Read data from csv file as two dimensional ushort array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="startColumn">
        ///         <para>The start column index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始列索引号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional ushort array</para>
        ///         <para>Chinese Simplified:读取后的二维ushort数组</para>
        ///     </returns>
        /// </summary>
        public static ushort[,] ReadUShortData(string filePath, uint startRow = 0, uint startColumn = 0, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<ushort>(filePath, startRow, startColumn, rowCount, encoding);
        }

        /// <summary>
        /// 在 csv文件中读取数据为二维ushort数组。
        /// Read data from csv file as two dimensional ushort array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="startRow">
        ///         <para>The start row index to read, Start from 0.</para>
        ///         <para>Chinese Simplified:读取的起始行索引号，从0开始。</para>
        ///     </param>
        ///     <param name="columns">
        ///         <para>The columns to read, start from 0.</para>
        ///         <para>Chinese Simplified:读取的列号，从0开始。</para>
        ///     </param>
        ///     <param name="rowCount">
        ///         <para>The row count to read. Read all rows when rowCount equals 0.</para>
        ///         <para>Chinese Simplified:读取的行数，等于0时读取所有行。</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional ushort array</para>
        ///         <para>Chinese Simplified:读取后的二维ushort数组</para>
        ///     </returns>
        /// </summary>
        public static ushort[,] ReadUShortData(string filePath, uint startRow, uint[] columns, uint rowCount = 0, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<ushort>(filePath, startRow, columns, rowCount, encoding);
        }

        #endregion

        #region 写入接口

        /// <summary>
        /// 在csv文件中写入string类型数据，通过弹窗选择文件路径
        /// Write one dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入string类型数据
        /// Write one dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, string[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            //一维数组需要被转换为1列n行的形式
            StreamWriteData(filePath, data.GetEnumerator(), data.Length, 1, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入string类型数据，通过弹窗选择文件路径
        /// Write two dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入string类型数据
        /// Write two dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, string[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.GetLength(0), data.GetLength(1), writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入double类型数据，通过弹窗选择文件路径
        /// Write one dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(double[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入double类型数据
        /// Write one dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, double[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.Length, 1, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入double类型数据，通过弹窗选择文件路径
        /// Write two dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(double[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入float类型数据
        /// Write two dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, float[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.GetLength(0), data.GetLength(1), writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入float类型数据，通过弹窗选择文件路径
        /// Write one dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(float[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入float类型数据
        /// Write one dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, float[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.Length, 1, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入float类型数据，通过弹窗选择文件路径
        /// Write two dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(float[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入double类型数据
        /// Write two dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, double[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.GetLength(0), data.GetLength(1), writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入double类型数据，通过弹窗选择文件路径
        /// Write one dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(int[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入int类型数据，通过弹窗选择文件路径
        /// Write one dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, int[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.Length, 1, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入int类型数据，通过弹窗选择文件路径
        /// Write two dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(int[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入int类型数据
        /// Write two dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, int[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.GetLength(0), data.GetLength(1), writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入uint类型数据，通过弹窗选择文件路径
        /// Write one dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(uint[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入uint类型数据
        /// Write one dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, uint[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.Length, 1, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入uint类型数据，通过弹窗选择文件路径
        /// Write two dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(uint[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入uint类型数据
        /// Write two dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, uint[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.GetLength(0), data.GetLength(1), writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入short类型数据，通过弹窗选择文件路径
        /// Write one dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(short[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入short类型数据
        /// Write one dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, short[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.Length, 1, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入short类型数据，通过弹窗选择文件路径
        /// Write two dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(short[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入short类型数据
        /// Write two dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, short[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.GetLength(0), data.GetLength(1), writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入ushort类型数据，通过弹窗选择文件路径
        /// Write one dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(ushort[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入ushort类型数据
        /// Write one dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, ushort[] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.Length, 1, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入ushort类型数据，通过弹窗选择文件路径
        /// Write two dimensional data to csv file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(ushort[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode, encoding);
        }

        /// <summary>
        /// 在csv文件中写入ushort类型数据
        /// Write two dimensional data to csv file.
        ///     <param name="filePath">
        ///         <para>The full path of the file to write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径。</para>
        ///     </param>
        ///     <param name="data">
        ///         <para>One dimension datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        ///     <param name="encoding">
        ///         <para>The encoding of file. Use default encoding when encoding is null.</para>
        ///         <para>Chinese Simplified:文件的编码格式。encoding为null时使用系统默认的编码格式。</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string filePath, ushort[,] data, WriteMode writeMode = WriteMode.OverLap, Encoding encoding = null)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            StreamWriteData(filePath, data.GetEnumerator(), data.GetLength(0), data.GetLength(1), writeMode, encoding);
        }

        #endregion

        #endregion

        #region 私有方法

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
        #endregion
    }
}
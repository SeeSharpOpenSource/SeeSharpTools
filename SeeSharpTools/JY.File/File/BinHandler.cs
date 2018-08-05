using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using SeeSharpTools.JY.File.Common;
using SeeSharpTools.JY.File.Common.i18n;

namespace SeeSharpTools.JY.File
{
    /// <summary>
    /// Bin文件读写类
    /// </summary>
    public class BinHandler
    {
        private static I18nEntity i18n = I18nEntity.GetInstance(I18nLocalWrapper.Name);

        #region 静态读模块
        /// <summary>
        /// 在bin文件中读取数据为二维double数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional double array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional string array</para>
        ///         <para>Chinese Simplified:读取后的二维string数组</para>
        ///     </returns>
        /// </summary>
        public static string[,] ReadData(int colNum)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadData(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组。
        /// Read data from binary file as two dimensional double array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="colNum">
        ///         <para>The column count of read data.</para>
        ///         <para>Chinese Simplified:读出数组的列数。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional string array</para>
        ///         <para>Chinese Simplified:读取后的二维string数组</para>
        ///     </returns>
        /// </summary>
        public static string[,] ReadData(string filePath, int colNum)
        {

            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadStr(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static double[,] ReadDoubleData(int colNum)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadDoubleData(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="colNum">
        ///         <para>The column count of read data.</para>
        ///         <para>Chinese Simplified:读出数组的列数。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static double[,] ReadDoubleData(string filePath, int colNum)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<double>(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static int[,] ReadIntData(int colNum)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadIntData(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="colNum">
        ///         <para>The column count of read data.</para>
        ///         <para>Chinese Simplified:读出数组的列数。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static int[,] ReadIntData(string filePath, int colNum)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<int>(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static uint[,] ReadUIntData(int colNum)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUIntData(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="colNum">
        ///         <para>The column count of read data.</para>
        ///         <para>Chinese Simplified:读出数组的列数。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static uint[,] ReadUIntData(string filePath, int colNum)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<uint>(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static short[,] ReadShortData(int colNum)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadShortData(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="colNum">
        ///         <para>The column count of read data.</para>
        ///         <para>Chinese Simplified:读出数组的列数。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static short[,] ReadShortData(string filePath, int colNum)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<short>(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static ushort[,] ReadUShortData(int colNum)
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUShortData(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <param name="colNum">
        ///         <para>The column count of read data.</para>
        ///         <para>Chinese Simplified:读出数组的列数。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static ushort[,] ReadUShortData(string filePath, int colNum)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<ushort>(filePath, colNum);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional double array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional string array</para>
        ///         <para>Chinese Simplified:读取后的二维string数组</para>
        ///     </returns>
        /// </summary>
        public static string[] ReadData()
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadData(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组。
        /// Read data from binary file as two dimensional double array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional string array</para>
        ///         <para>Chinese Simplified:读取后的二维string数组</para>
        ///     </returns>
        /// </summary>
        public static string[] ReadData(string filePath)
        {

            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadStr(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static double[] ReadDoubleData()
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadDoubleData(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static double[] ReadDoubleData(string filePath)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<double>(filePath);
        }


        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static int[] ReadIntData()
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadIntData(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static int[] ReadIntData(string filePath)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<int>(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static uint[] ReadUIntData()
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUIntData(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static uint[] ReadUIntData(string filePath)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<uint>(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static short[] ReadShortData()
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadShortData(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static short[] ReadShortData(string filePath)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<short>(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维double数组文件中读取数据为二维string数组，通过弹窗选择文件路径。
        /// Read data from binary file as two dimensional string array. File path can be choosen from the GUI.
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static ushort[] ReadUShortData()
        {
            string filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            return ReadUShortData(filePath);
        }

        /// <summary>
        /// 在bin文件中读取数据为二维string数组。
        /// Read data from binary file as two dimensional string array.
        ///     <param name="filePath">
        ///         <para>The full path of the file to read.</para>
        ///         <para>Chinese Simplified:待读取文件的完整路径。</para>
        ///     </param>
        ///     <returns >
        ///         <para>The read two dimensional double array</para>
        ///         <para>Chinese Simplified:读取后的二维double数组</para>
        ///     </returns>
        /// </summary>
        public static ushort[] ReadUShortData(string filePath)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            return StreamReadData<ushort>(filePath);
        }

        private static TDataType[,] StreamReadData<TDataType>(string filePath, int colCount)
        {
            FileStream stream = null;
            BinaryReader reader = null;
            try
            {
                long byteSize = FileUtil.InitBinReadStream(ref stream, ref reader, filePath);
                return FileUtil.StreamReadFromBinFile<TDataType>(reader, byteSize, colCount, 0);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError, 
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
                FileUtil.ReleaseResource(stream);
            }
        }

        private static TDataType[] StreamReadData<TDataType>(string filePath)
        {
            FileStream stream = null;
            BinaryReader reader = null;
            try
            {
                long byteSize = FileUtil.InitBinReadStream(ref stream, ref reader, filePath);
                return FileUtil.StreamReadFromBinFile<TDataType>(reader, byteSize, 0);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError, 
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
                FileUtil.ReleaseResource(stream);
            }
        }

        private static string[,] StreamReadStr(string filePath, int colNum)
        {
            FileStream stream = null;
            BinaryReader reader = null;
            try
            {
                FileUtil.InitBinReadStream(ref stream, ref reader, filePath);
                return FileUtil.StreamReadStrFromBinFile(reader, colNum);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError, 
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
                FileUtil.ReleaseResource(stream);
            }
        }

        private static string[] StreamReadStr(string filePath)
        {
            FileStream stream = null;
            BinaryReader reader = null;
            try
            {
                FileUtil.InitBinReadStream(ref stream, ref reader, filePath);
                return FileUtil.StreamReadStrFromBinFile(reader);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError, 
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(reader);
                FileUtil.ReleaseResource(stream);
            }
        }
        #endregion

        #region 静态写模块
        #region 私有变量

        private const string FileExtName = "bin";
        // 行分隔符
        private static readonly string NewLineDelim = Environment.NewLine;

        private static readonly Encoding Encode = Encoding.UTF8;
        // 字符串分隔符
        private const string StrDelims = "#X#";

        #endregion

        #region 公共方法



        /// <summary>
        /// 在bin文件中写入string类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入string类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, string[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            //一维数组需要被转换为1列n行的形式
            string strData = FileUtil.BuildStringData(data.GetEnumerator(), data.Length, 1, StrDelims);
            FileUtil.WriteStrToFile(filePath, strData, Encode);
        }


        /// <summary>
        /// 在bin文件中写入string类型二维数据，通过弹窗选择文件路径
        /// Write two dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(string[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入string类型二维数据
        /// Write two dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, string[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            WriteStrDataToFile(filePath, data);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(double[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, double[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            StreamWriteData(filePath, data, data.Length * sizeof(double), writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据，通过弹窗选择文件路径
        /// Write two dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(double[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据
        /// Write two dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, double[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            StreamWriteData(filePath, data, data.Length * sizeof(double), writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(int[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, int[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            StreamWriteData(filePath, data, data.Length * sizeof(int), writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据，通过弹窗选择文件路径
        /// Write two dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(int[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据
        /// Write two dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, int[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            StreamWriteData(filePath, data, data.Length * sizeof(int), writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(uint[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, uint[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);

            StreamWriteData(filePath, data, data.Length * sizeof(uint), writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据，通过弹窗选择文件路径
        /// Write two dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(uint[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据
        /// Write two dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, uint[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            StreamWriteData(filePath, data, data.Length * sizeof(uint), writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(short[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, short[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            StreamWriteData(filePath, data, data.Length * sizeof(short), writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据，通过弹窗选择文件路径
        /// Write two dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(short[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据
        /// Write two dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, short[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            StreamWriteData(filePath, data, data.Length * sizeof(short), writeMode);
            //            byte[] dataBuf = FileUtil.BuildByteData(data);
            //            WriteBinFile(filePath, dataBuf);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>One dimensional datas to write.</para>
        ///         <para>Chinese Simplified:待写入文件的一维数组</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(ushort[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型一维数据，通过弹窗选择文件路径
        /// Write one dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, ushort[] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);

            StreamWriteData(filePath, data, data.Length * sizeof(ushort), writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据，通过弹窗选择文件路径
        /// Write two dimensional data to binary file, file path can be choosen from the pop up GUI.
        ///     <param name="data">
        ///         <para>Two dimensional datas to be write.</para>
        ///         <para>Chinese Simplified:待写入文件的完整路径</para>
        ///     </param>
        ///     <param name="writeMode">
        ///         <para>Write Mode when file exist.</para>
        ///         <para>Chinese Simplified:文件已存在时的写入模式</para>
        ///     </param>
        /// </summary>
        public static void WriteData(ushort[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            string filePath = FileUtil.GetSaveFilePathFromDialog(FileExtName);
            WriteData(filePath, data, writeMode);
        }

        /// <summary>
        /// 在bin文件中写入double类型二维数据
        /// Write two dimensional data to binary file.
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
        /// </summary>
        public static void WriteData(string filePath, ushort[,] data, WriteMode writeMode = WriteMode.OverLap)
        {
            FileUtil.CheckFilePath(filePath, FileExtName);
            FileUtil.CheckDataSize(data);
            StreamWriteData(filePath, data, data.Length * sizeof(ushort), writeMode);
        }

        #endregion

        #region 私有方法

        private static void WriteStrDataToFile(string filePath, string[,] data)
        {
            string strData = FileUtil.BuildStringData(data.GetEnumerator(), data.GetLength(0), data.GetLength(1), StrDelims);
            FileUtil.WriteStrToFile(filePath, strData, Encode);
        }

        private static void StreamWriteData(string filePath, Array data, long byteSize, WriteMode writeMode)
        {
            FileStream stream = null;
            BinaryWriter writer = null;

            try
            {
                FileUtil.InitBinWriteStream(ref stream, ref writer, filePath, writeMode);
                FileUtil.StreamwriteDataToFile(writer, data, byteSize, 0);
            }
            catch (SeeSharpFileException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError, 
                    i18n.GetFStr("Runtime.WriteFail", ex.Message), ex);
            }
            finally
            {
                FileUtil.ReleaseResource(writer);
                FileUtil.ReleaseResource(stream);
            }
        }

        #endregion
        #endregion

        #region 可实例化模块

        private FileStream stream = null;
        private BinaryReader reader = null;
        private readonly string filePath;
        private Type dataType;
        private readonly int typeSize;

        /// <summary>
        /// 流式读取的构造方法
        /// </summary>
        /// <param name="filePath">读取文件的路径</param>
        /// <param name="colNum">读取文件的列数</param>
        public BinHandler(string filePath, int colNum)
        {
            this.filePath = filePath;
            this._colNum = colNum;
            InitStreamAndReader();
        }

        /// <summary>
        /// 流式读取的构造方法
        /// </summary>
        /// <param name="filePath">读取文件的路径</param>
        public BinHandler(string filePath)
        {
            this.filePath = filePath;
            this._colNum = 1;
            InitStreamAndReader();
        }

        /// <summary>
        /// 流式读取的构造方法
        /// </summary>
        public BinHandler()
        {
            this.filePath = FileUtil.GetOpenFilePathFromDialog(FileExtName);
            this._readOver = false;
            InitStreamAndReader();
        }

        private void InitStreamAndReader()
        {
            try
            {
                FileUtil.InitBinReadStream(ref stream, ref reader, filePath);
            }
            catch (Exception ex)
            {
                FileUtil.ReleaseResource(reader);
                FileUtil.ReleaseResource(stream);
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError, 
                    i18n.GetFStr("Runtime.OpenfileFail", ex.Message), ex);
            }
        }

        private int _colNum = 1;
        private bool _readOver = false;
        /// <summary>
        /// 流式读取结束
        /// </summary>
        public bool ReadOver { get { return _readOver; } }

        /// <summary>
        /// 停止流式操作
        /// </summary>
        public void StopStreamRead()
        {
            _readOver = true;
            FileUtil.ReleaseResource(reader);
            FileUtil.ReleaseResource(stream);
        }

        private int byteOffset = 0;

        /// <summary>
        /// 流式读取数据
        /// </summary>
        /// <param name="sampleCount">读取的样点数</param>
        /// <returns>包含数据的数组</returns>
        public TDataType[,] StreamRead<TDataType>(int sampleCount)
        {
            InitDataType(typeof(TDataType));
            return StreamReadFileData<TDataType>(null, ref sampleCount);
        }

        private void InitDataType(Type inputType)
        {
            if (null == dataType)
            {
                dataType = inputType;
            }
            else if (!ReferenceEquals(dataType, inputType))
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.ParamCheckError, i18n.GetFStr("ParamCheck.DataTypeDiff", dataType.Name));
            }
        }

        /// <summary>
        /// 流式读取数据
        /// </summary>
        /// <param name="dataBuf">数据缓存</param>
        /// <returns>读取到的数组行数</returns>
        /// <exception cref="SeeSharpFileException">Exception</exception>
        public int StreamRead<TDataType>(TDataType[,] dataBuf)
        {
            if (null == dataBuf)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.ParamCheckError, i18n.GetStr("ParamCheck.NullBuf"));
            }
            InitDataType(typeof(TDataType));
            int readSize = dataBuf.GetLength(0);
            StreamReadFileData<TDataType>(dataBuf, ref readSize);
            return readSize;
        }

        private TDataType[,] StreamReadFileData<TDataType>(Array dataBuf, ref int readSize)
        {
            if (_readOver)
            {
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError, i18n.GetStr("Runtime.ReadOver"));
            }
            try
            {
                int readBytes = readSize * _colNum * Marshal.SizeOf(dataType);
                //如果文件中剩余数据小于readSize，则更新readSize的值为文件剩余的样点数
                if (readBytes + byteOffset > stream.Length)
                {
                    readBytes = (int)(stream.Length - byteOffset);
                    readSize = readBytes / (_colNum * sizeof(double));
                }
                if (readBytes <= 0)
                {
                    _readOver = true;
                    readSize = 0;
                    return null;
                }
                byteOffset += readBytes;
                if (null == dataBuf)
                {
                    dataBuf = new double[readSize, _colNum];
                }
                //                reader.BaseStream.Position = byteOffset;
                byte[] tmpBuf = reader.ReadBytes(readBytes);
                Buffer.BlockCopy(tmpBuf, 0, dataBuf, 0, readBytes);
                //如果读取结束后文件已经读取完成，则自动停止流式读取。
                if (byteOffset >= stream.Length - 1)
                {
                    StopStreamRead();
                }
                return (TDataType[,])dataBuf;
            }
            catch (Exception ex)
            {
                StopStreamRead();
                throw new SeeSharpFileException(SeeSharpFileErrorCode.RuntimeError, 
                    i18n.GetFStr("Runtime.ReadFail", ex.Message), ex);
            }
        }
        #endregion
    }
}
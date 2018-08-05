using System;
using System.Runtime.InteropServices;
using System.Text;
using IniParser;
using IniParser.Model;
using SeeSharpTools.JY.File;

namespace SeeSharpTools.JY.File
{
    /// <summary>
    /// Ini文件读写改操作处理类
    /// </summary>
    public static class IniHandler
    {
        #region Methods for whole data operation

        private static FileIniDataParser _initParser;

        private static FileIniDataParser GetIniFileParser()
        {
            if (null == _initParser)
            {
                _initParser = new FileIniDataParser();
            }
            return _initParser;
        }

        /// <summary>
        /// 读取Ini文件到IniData类中
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码格式，默认为UTF8</param>
        /// <returns></returns>
        public static IniData ReadIniFile(string filePath, Encoding encoding = null)
        {
            if (null == encoding)
            {
                encoding = Encoding.UTF8;
            }
            FileIniDataParser parser = GetIniFileParser();
            return parser.ReadFile(filePath, encoding);
        }

        /// <summary>
        /// 将Ini数据写入文件中
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="iniData">待写入的Ini数据</param>
        /// <param name="encoding">编码格式，默认为UTF8</param>
        /// <returns></returns>
        public static void WriteIniFile(string filePath, IniData iniData, Encoding encoding = null)
        {
            if (null == encoding)
            {
                encoding = Encoding.UTF8;
            }
            FileIniDataParser parser = GetIniFileParser();
            parser.WriteFile(filePath, iniData, encoding);
        }

        /// <summary>
        /// 将合并ini数据合并覆盖到源ini数据中
        /// </summary>
        /// <param name="srcIniData">源ini数据</param>
        /// <param name="mergeIniData">待合并的ini数据</param>
        /// <returns></returns>
        public static IniData MergeIniData(IniData srcIniData, IniData mergeIniData)
        {
            srcIniData.Merge(mergeIniData);
            return srcIniData;
        }

        /// <summary>
        /// 将ini数据合并覆盖到已有文件中
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="mergeIniData">待合并的ini数据</param>
        /// <param name="encoding">编码格式，默认为UTF8</param>
        /// <returns></returns>
        public static void MergeToFile(string filePath, IniData mergeIniData, Encoding encoding = null)
        {
            if (null == encoding)
            {
                encoding = Encoding.UTF8;
            }
            FileIniDataParser parser = GetIniFileParser();
            IniData srcIniData = parser.ReadFile(filePath, encoding);
            srcIniData.Merge(mergeIniData);
            parser.WriteFile(filePath, srcIniData, encoding);
        }

        /// <summary>
        /// Free file parser resources
        /// </summary>
        public static void Close()
        {
            _initParser = null;
        }

        #endregion

        #region Methods for partial data read/write

        /// <summary>
        /// 读取指定ini档案内的section或key或value
        /// </summary>
        /// <param name="section">section名称，如果为null则返回所有section的名称</param>
        /// <param name="key">键的名称，如果为null则返回section下所有的键名</param>
        /// <param name="path">文件路径，不能为null</param>
        /// <param name="length">缓存大小，如果数据较多请配置较长的buffer</param>
        /// <returns>读取的结果，如果section/key/path都不为空，则只保存key对应的值</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string[] Read(string section, string key, string path, int length = 10000)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Cannot find file, please select the right path");

            string[] result;
            if (string.IsNullOrEmpty(section))
            {
                result = GetAllSectionNames(length, path);
            }
            else
            {
                if (string.IsNullOrEmpty(key))
                {
                    result = GetAllItemKeys(section, length, path);
                }
                else
                {
                    result = new string[1];
                    result[0] = ReadValue(section, key, length, path);
                }
            }
            return result;
        }
        
        /// <summary>
        /// 在ini文件中新增或者修改一个键值对
        /// </summary>
        /// <param name="section">section的名称，不能为空</param>
        /// <param name="key">key的名称，不能为空</param>
        /// <param name="iValue">key对应的值，不能为空</param>
        /// <param name="path">文件路径，不能为空</param>
        public static void Write(string section, string key, string iValue, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Cannot find file, please select the right path");
            if (string.IsNullOrEmpty(section))
                throw new ArgumentException("Please assign the section");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Please assign the key");
            if (string.IsNullOrEmpty(iValue))
                throw new ArgumentException("Please assign the value");
            WritePrivateProfileString(section, key, iValue, path);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// 读取指定key的值
        /// </summary>
        private static string ReadValue(string section, string key, int length, string path)
        {
            StringBuilder temp = new StringBuilder(length);
            int i = GetPrivateProfileString(section, key, "", temp, length, path);
            if (i == 0)
                throw new ArgumentException("Cannot find values, please select the right section and key");
            return temp.ToString();
        }
        /// <summary>
        /// 读取指定ini档案内的所有section名称
        /// </summary>
        private static string[] GetAllSectionNames(int length, string path)
        {
            byte[] temp = new byte[length];
            string[] result;
            string tempStr;

            int size = GetPrivateProfileSectionNames(temp, length, path);
            if (size == 0)
                throw new ArgumentException("Cannot find any section values, please check the section setting in the ini file");
            else
            {
                tempStr = ByteToStr(temp, Encoding.UTF8);
                result = (tempStr.Remove(size - 1)).Split('\0');
            }

            return result;
        }
        /// <summary>
        /// 读取指定section的所有key名称
        /// </summary>
        private static string[] GetAllItemKeys(string section, int length, string path)
        {
            byte[] temp = new byte[length];
            string[] result;
            string tempStr;

            int bytesReturned = GetPrivateProfileStringA(section, null, "", temp, length, path);
            if (bytesReturned == 0)
                throw new ArgumentException("Cannot find any key values, please check the key setting in the ini file");

            else
            {
                tempStr = ByteToStr(temp, Encoding.UTF8);
                result = (tempStr.Remove(bytesReturned - 1)).Split('\0');
            }

            return result;
        }
        /// <summary>
        /// 将Byte[]转换成string
        /// </summary>
        private static string ByteToStr(byte[] bt, Encoding encoding)
        {
            return encoding.GetString(bt);
        }

        #endregion

        #region Imported functions (kernel32.dll)
        /// <summary>
        /// (kernel32函式引用)
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string segName, string keyName, string sDefault, StringBuilder buffer, int nSize, string fileName);

        /// <summary>
        /// (kernel32函式引用)
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileStringA(string segName, string keyName, string sDefault, byte[] buffer, int iLen, string fileName);

        /// <summary>
        /// (kernel32函式引用)
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string segName, StringBuilder buffer, int nSize, string fileName);

        /// <summary>
        /// (kernel32函式引用)
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileSection(string segName, string sValue, string fileName);

        /// <summary>
        /// (kernel32函式引用)
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(string segName, string keyName, string sValue, string fileName);

        /// <summary>
        /// (kernel32函式引用)
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSectionNames(byte[] segNames, int iLen, string fileName);

        #endregion
    }
}
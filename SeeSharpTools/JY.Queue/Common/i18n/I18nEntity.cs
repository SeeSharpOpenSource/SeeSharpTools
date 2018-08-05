using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SeeSharpTools.JY.ThreadSafeQueue.Common.i18n
{
    /// <summary>
    /// 国际化实体类，支持语言zh-CN和en_US
    /// </summary>
    internal class I18nEntity : IDisposable
    {
        private static ConcurrentDictionary<string, I18nEntity> _targetNameToInst = 
            new ConcurrentDictionary<string, I18nEntity>();
        private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        private static string _languageType = "";
        private static Encoding _encode;

        private readonly string _targetName;
        private readonly string _resourceName;
        private Dictionary<string, int> _labelKeyToStartIndex = new Dictionary<string, int>();
        private Dictionary<string, int> _labelKeyToStrSize = new Dictionary<string, int>();

        #region 对外接口

        /// <summary>
        /// 获取I18n处理类的实例。该方法的实现在I18nLocalWrapper类中。
        /// </summary>
        /// <param name="targetObj">国际化目标类的实例</param>
        /// <returns>I18n处理类实例</returns>
        public static I18nEntity GetInstance(object targetObj)
        {
            return I18nLocalWrapper.GetInstance(targetObj);
        }

        /// <summary>
        /// 获取I18n处理类的实例
        /// </summary>
        /// <param name="targetName">国际化目标名称</param>
        /// <returns>I18n处理类实例</returns>
        public static I18nEntity GetInstance(string targetName)
        {
            lock (_languageType)
            {
                if (null == _encode)
                {
                    _languageType = I18nLocalWrapper.GetLanguageType();
                    _encode = I18nLocalWrapper.GetFileEncoding();
                }
                if (!_targetNameToInst.ContainsKey(targetName))
                {
                    _targetNameToInst[targetName] = new I18nEntity(targetName);
                }
            }
            return _targetNameToInst[targetName];
        }

        /// <summary>
        /// 释放目标名称的实例。该方法的实现在I18nLocalWrapper类中。
        /// </summary>
        /// <param name="targetObj">国际化目标类</param>
        public static void RemoveInstance(object targetObj)
        {
            I18nLocalWrapper.RemoveInstance(targetObj);
        }

        /// <summary>
        /// 释放目标名称的实例
        /// </summary>
        /// <param name="targetName">国际化目标名称</param>
        public static void RemoveInstance(string targetName)
        {
            I18nEntity outEntity;
            _targetNameToInst.TryRemove(targetName, out outEntity);
        }

        private const string ERRCODE = "ERRCODE";
        /// <summary>
        /// 根据异常码获取API的异常信息
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static string GetErrCodeMsg(int errorCode)
        {
            if (!_targetNameToInst.ContainsKey(ERRCODE))
            {
                _targetNameToInst[ERRCODE] = GetInstance(ERRCODE);
            }
            return _targetNameToInst[ERRCODE].GetStr(errorCode.ToString());
        }

        /// <summary>
        /// 获取使用占位符的国际化信息
        /// </summary>
        /// <param name="labelKey">信息标签</param>
        /// <param name="paramArray">占位符参数</param>
        /// <returns>国际化后的信息</returns>
        public string GetFStr(string labelKey, params object[] paramArray)
        {
            string labelFormat = GetStr(labelKey);
            try
            {
                labelFormat = string.Format(labelFormat, paramArray);
            }
            catch (Exception ex)
            {
                // ignore
//                JYLog.Print("Format exception in {0}", ex.StackTrace);
            }
            return labelFormat;
        }

        /// <summary>
        /// 获取国际化信息
        /// </summary>
        /// <param name="labelKey">信息标签</param>
        /// <returns>国际化后的信息</returns>
        public string GetStr(string labelKey)
        {
            return _labelKeyToStartIndex.ContainsKey(labelKey) ? _getStrFromFile(labelKey) : labelKey;
        }

        #endregion

        private const string I18nFileFormat = "{0}.Resources.locale.i18n_{1}_{2}.properties";
        private I18nEntity(string targetName)
        {
            _targetName = targetName;
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                _resourceName = string.Format(I18nFileFormat, I18nLocalWrapper.DefaultNameSpace, targetName, _languageType);
                _initLabelKeyToValuePosMapping(ref stream, ref reader);
            }
            catch (Exception ex)
            {
                //ignore
                //JYLog.Print("Read i18n resource {0} failed: {1}.", _resourceName, ex.Message);
            }
            finally
            {
                ReleaseResource(reader);
                ReleaseResource(stream);
            }
        }

        private void _initLabelKeyToValuePosMapping(ref Stream stream, ref StreamReader reader)
        {
            stream = Assembly.GetManifestResourceStream(_resourceName);
            reader = new StreamReader(stream);
            string lineStr, labelKey;
            // TODO 起始byte是第4个，To confirm why
            int lastByteIndex = 3;
            int delimSize = _encode.GetByteCount("=");
            //换行符长度
            int lineBreakSize = _encode.GetByteCount(System.Environment.NewLine);
            while (null != (lineStr = reader.ReadLine()))
            {
                int lineSize = _encode.GetByteCount(lineStr);
                if (lineStr.StartsWith(@"//") || !lineStr.Contains("="))
                {
                    lastByteIndex += lineSize + lineBreakSize;
                    continue;
                }
                int delimPos = lineStr.IndexOf("=");
                labelKey = lineStr.Substring(0, delimPos);
                int prefixSize = _encode.GetByteCount(labelKey) + delimSize;
                _labelKeyToStartIndex[labelKey] = prefixSize + lastByteIndex;
                _labelKeyToStrSize[labelKey] = lineSize - prefixSize;
                lastByteIndex += lineSize + lineBreakSize;
            }
        }

        private string _getStrFromFile(string labelKey)
        {
            Stream stream = null;
            try
            {
                return _getLabelValueFromFile(ref stream, labelKey);
            }
            catch (Exception ex)
            {
//                JYLog.Print("Read label from resource {0} failed: {1}.", _resourceName, ex.Message);
                return labelKey;
            }
            finally
            {
                ReleaseResource(stream);
            }
        }

        private string _getLabelValueFromFile(ref Stream fileStream, string labelKey)
        {
            lock (_resourceName)
            {
                int startIndex = _labelKeyToStartIndex[labelKey];
                int strSize = _labelKeyToStrSize[labelKey];
                // label value not exist
                if (startIndex < 0 || strSize <= 0)
                {
                    return labelKey;
                }
                fileStream = Assembly.GetManifestResourceStream(_resourceName);
                fileStream.Seek(startIndex, SeekOrigin.Begin);
                byte[] labelValue = new byte[strSize];
                fileStream.Read(labelValue, 0, strSize);
                return _encode.GetString(labelValue);
            }
        }

        private static void ReleaseResource(IDisposable resource)
        {
            if (null == resource)
            {
                return;
            }
            try
            {
                resource.Dispose();
            }
            catch (Exception ex)
            {
                // ignore
//                JYLog.Print("Release {0} failed: {1}", resource.GetType().Name, ex.Message);
            }
        }

        public void Dispose()
        {
            RemoveInstance(_targetName);
        }
    }
}
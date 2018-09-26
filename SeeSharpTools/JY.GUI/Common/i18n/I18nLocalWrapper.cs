using System.Text;

namespace SeeSharpTools.JY.GUI.Common.i18n
{
    /// <summary>
    /// 本地化i18n定制的实现类。实现使用object类型获取和释放目标国际化类实例的方法和自定义组件，保证I18nEntity类的通用性
    /// </summary>
    internal class I18nLocalWrapper
    {

        internal const string Name = "GUI";
        
        internal const string DefaultNameSpace = "SeeSharpTools.JY.GUI";

        private static string _getTargetName(object taskObj)
        {
            return Name;
        }
        /// <summary>
        /// 获取本地化i18n定制的实现类
        /// </summary>
        /// <param name="targetObj"></param>
        /// <returns>目标类型I18n实例</returns>
        public static I18nEntity GetInstance(object targetObj)
        {
            return I18nEntity.GetInstance(_getTargetName(targetObj));
        }

        /// <summary>
        /// 释放本地化i18n定制的实现类
        /// </summary>
        /// <param name="targetObj">国际化目标类</param>
        /// <returns></returns>
        public static void RemoveInstance(object targetObj)
        {
            I18nEntity.RemoveInstance(_getTargetName(targetObj));
        }

        /// <summary>
        /// 获取语言类型标签
        /// </summary>
        /// <returns>语言类型标签</returns>
        public static string GetLanguageType()
        {
            string languageType;
            switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name)
            {
                case "en-US":
                    languageType = "EN";
                    break;
                case "zh-CN":
                    languageType = "CN";
                    break;
                default:
                    languageType = "EN";
                    break;
            }
            return languageType;
        }

        /// <summary>
        /// 获取编码类型
        /// </summary>
        /// <returns>语言类型标签</returns>
        public static Encoding GetFileEncoding()
        {
            return Encoding.UTF8;
        }
        
    }
}
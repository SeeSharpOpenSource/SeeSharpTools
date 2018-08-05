using System;
/// <summary>
/// JY.Sensors
/// Author：JYTEK.Inc
/// Release Time ：2018.05.30
/// Version：SeeSharp Tools  1.4.1
/// This source code is developed by JYTEK or modified from the open source community such as GitHub.
/// JYTEK follows GNU GPL license.This means you may use this code free of charge, you can use this code
/// for commercial purposes, you can also change the code.However, if you change the source code and
/// make an improved version of this code, you also agree to follow GNU GPL license and share your
/// changed source code on JYTEK website.
///
/// Chinese Simplified:
/// JY.Sensors
/// 作者：简仪科技
/// 修改日期：2018.05.30
/// 版本：SeeSharp Tools  1.4.1
/// 简仪科技通过自主开发及利用来源于GitHub等开源社区的资源编制了这一代码，简仪科技遵从GNU GPL开源授权方式。
/// 您可以免费使用这一程序；您可以在您的商业代码中使用此代码；您也可以对此源程序修改。如果您修改了此源程序，
/// 您同意也遵循GNU GPL授权方式在简仪科技的网站上发布您修改过的源程序。
/// </summary>
namespace SeeSharpTools.JY.Sensors
{
    /// <summary>
    /// 自定义转换函式
    /// </summary>
    public class CustomScaling
    {
        /// <summary>
        /// 按照指定的公式转换,单位自定义
        /// </summary>
        /// <param name="voltValues"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static double[] Convert(double[] voltValues, Func<double, double> function)
        {
            double[] result = new double[voltValues.Length];
            for (int i = 0; i < voltValues.Length; i++)
            {
                result[i] = function.Invoke(voltValues[i]);
            }
            return result;
        }

        /// <summary>
        /// 按照指定的公式转换,单位自定义
        /// </summary>
        /// <param name="voltValues"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static double Convert(double voltValues, Func<double, double> function)
        {
            return function.Invoke(voltValues);
        }
    }
}
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
    /// 线性位移传感器,公式 R*最大位移量/最大阻值范围
    /// </summary>
    public class DisplacementSensor
    {
        #region Static

        /// <summary>
        /// 电组数组(Ohm)转换成位移量数组(Unit)
        /// </summary>
        /// <param name="rawValues">电组(Ohm）</param>
        /// <param name="maxDisplacement">最大位移量(Unit)</param>
        /// <param name="maxResistance">最大电阻值(Ohm)</param>
        /// <param name="minResistance">最小电阻值(Ohm)</param>
        /// <returns></returns>
        public static double[] Convert(double[] rawValues, double maxDisplacement, double maxResistance, double minResistance = 0)
        {
            return Array.ConvertAll(rawValues, new Converter<double, double>(x => x / (maxResistance - minResistance) * maxDisplacement));
        }

        /// <summary>
        /// 电组(Ohm)转换成位移量(Unit)
        /// </summary>
        /// <param name="rawValues">电组(Ohm）</param>
        /// <param name="maxDisplacement">最大位移量(Unit)</param>
        /// <param name="maxResistance">最大电阻值(Ohm)</param>
        /// <param name="minResistance">最小电阻值(Ohm)</param>
        /// <returns></returns>
        public static double Convert(double rawValue, double maxDisplacement, double maxResistance, double minResistance = 0)
        {
            return rawValue / (maxResistance - minResistance) * maxDisplacement;
        }

        #endregion Static
    }
}
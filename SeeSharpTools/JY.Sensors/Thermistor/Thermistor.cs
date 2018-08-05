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
    /// 热敏电阻, 转换公式:β parameter equation, 1.0 / (1.0 / T0 + Math.Log(V / R0) / beta) - 273.15), T0=25, R=摄氏25度时的电阻(Ohm)，beta自定义
    /// </summary>
    public class Thermistor
    {
        #region Static

        private static double _t0 = 25.0 + 273.15;

        /// <summary>
        /// 电压数组(V)转换成温度数组(摄氏)
        /// </summary>
        /// <param name="rawValues">电压(V)</param>
        /// <param name="r0">摄氏25度时的电阻(Ohm)</param>
        /// <param name="beta">beta参数</param>
        /// <returns></returns>
        public static double[] Convert(double[] rawValues, double r0, double beta)
        {
            return Array.ConvertAll(rawValues, new Converter<double, double>(x => 1.0 / (1.0 / _t0 + Math.Log(x / r0) / beta) - 273.15));
        }

        /// <summary>
        /// 电压(V)转换成温度(摄氏)
        /// </summary>
        /// <param name="rawValues">电压(V)</param>
        /// <param name="r0">摄氏25度时的电阻(Ohm)</param>
        /// <param name="beta">beta参数</param>
        /// <returns></returns>
        public static double Convert(double rawValue, double r0, double beta)
        {
            return 1.0 / (1.0 / _t0 + Math.Log(rawValue / r0) / beta) - 273.15;
        }

        #endregion Static
    }
}
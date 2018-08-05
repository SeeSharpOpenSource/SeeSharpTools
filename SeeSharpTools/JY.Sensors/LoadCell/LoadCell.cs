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
    /// 荷重传感器,计算公式 V*最大荷重/(灵敏度*激励电压), 单位要一致
    /// </summary>
    public class LoadCell
    {
        #region Static

        /// <summary>
        /// 电压数组转换成荷重数组(单位和maxLoad最大荷重单位一样)
        /// </summary>
        /// <param name="rawValues">电压(V)</param>
        /// <param name="sensitivity">灵敏度(mV/V)</param>
        /// <param name="maxload">最大荷重(Unit)</param>
        /// <param name="ExcitationValtage">激励电压(V),默认2.5V</param>
        /// <returns></returns>
        public static double[] Convert(double[] rawValues, double sensitivity, double maxload, double excitationValtage = 2.5)
        {
            //单位是mV
            double fullRange = sensitivity * excitationValtage;
            return Array.ConvertAll(rawValues, new Converter<double, double>(x => ((x * 1000)) * maxload / fullRange));
        }

        /// <summary>
        /// 电压转换成荷重(单位和maxLoad最大荷重单位一样)
        /// </summary>
        /// <param name="rawValues">电压(V)</param>
        /// <param name="sensitivity">灵敏度(mV/V)</param>
        /// <param name="maxload">最大荷重(Unit)</param>
        /// <param name="ExcitationValtage">激励电压(V),默认2.5V</param>
        /// <returns></returns>
        public static double Convert(double rawValue, double sensitivity, double maxload, double excitationValtage = 2.5)
        {
            //单位是mV
            double fullRange = sensitivity * excitationValtage;
            return rawValue * 1000 * maxload / fullRange;
        }

        #endregion Static
    }
}
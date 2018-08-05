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
    /// PT-100 公式: T=(-A+Sqrt(A^2-40*B*(0.1-V)))/2B , where A=0.0039083 and B=-0.0000005775
    /// </summary>
    public class RTD
    {
        #region Static

        #region Private StaticFields

        //Senser used in this program is PT-100 Resistance Temperature Detector.
        //a and b are constants in formula used to express the relationship between temperature and measured voltage .
        //The relationship references from  https://en.wikipedia.org/wiki/Resistance_thermometer.
        private const double coeff_a = 3.9083E-3;

        private const double coeff_b = -5.775E-7;

        #endregion Private StaticFields

        #region Static Methods

        /// <summary>
        /// Convert Voltage Array of RTD into Temperature Array
        /// </summary>
        /// <param name="temperature">Temperature Array Measured(the default unit is ℃) </param>
        /// <param name="voltValues">Voltage Array of RTD(the default unit is V) </param>
        /// <returns></returns>
        public static double[] Convert(double[] voltValues)
        {
            return Array.ConvertAll(voltValues, new Converter<double, double>(x => (-1 * coeff_a + Math.Sqrt(Math.Pow(coeff_a, 2) - 40 * coeff_b * (0.1 - x))) / (2 * coeff_b)));
        }

        /// <summary>
        /// Convert Voltage Value of RTD into Temperature Value
        /// </summary>
        /// <param name="temperature">Temperature Value Measured(the default unit is ℃)</param>
        /// <param name="voltValues">Voltage Value of RTD(the default unit is V)</param>
        /// <returns></returns>
        public static double Convert(double voltValues)
        {
            return (-1 * coeff_a + Math.Sqrt(Math.Pow(coeff_a, 2) - 40 * coeff_b * (0.1 - voltValues))) / (2 * coeff_b);
        }

        #endregion Static Methods

        #endregion Static
    }
}
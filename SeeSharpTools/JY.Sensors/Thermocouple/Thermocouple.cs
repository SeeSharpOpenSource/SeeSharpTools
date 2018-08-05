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
    /// 热电偶传感器，支持类型B,E,J,K,N,R,S,T. 计算公式参考NIST
    /// </summary>
    public class Thermocouple
    {
        #region Static

        /// <summary>
        /// 电压数组(V)转换成温度数组(摄氏)
        /// </summary>
        /// <param name="type">热电偶类型，支持B,E,J,K,N,R,S,T</param>
        /// <param name="voltValues">电压值(V)</param>
        /// <param name="cjcValue">冷点补偿温度(摄氏),默认25度</param>
        /// <returns></returns>
        public static double[] Convert(ThermocoupleType type, double[] voltValues, bool enableCJC=true,double cjcValue = 25.0)
        {
            switch (type)
            {
                case ThermocoupleType.TypeB:
                    return TC_TypeB.VoltToTemperature(voltValues, enableCJC,cjcValue);

                case ThermocoupleType.TypeE:
                    return TC_TypeE.VoltToTemperature(voltValues, enableCJC, cjcValue);

                case ThermocoupleType.TypeJ:
                    return TC_TypeJ.VoltToTemperature(voltValues, enableCJC, cjcValue);

                case ThermocoupleType.TypeK:
                    return TC_TypeK.VoltToTemperature(voltValues, enableCJC, cjcValue);

                case ThermocoupleType.TypeN:
                    return TC_TypeN.VoltToTemperature(voltValues, enableCJC, cjcValue);

                case ThermocoupleType.TypeR:
                    return TC_TypeR.VoltToTemperature(voltValues, enableCJC, cjcValue);

                case ThermocoupleType.TypeS:
                    return TC_TypeS.VoltToTemperature(voltValues, enableCJC, cjcValue);

                case ThermocoupleType.TypeT:
                    return TC_TypeT.VoltToTemperature(voltValues, enableCJC, cjcValue);

                default:
                    return TC_TypeB.VoltToTemperature(voltValues, enableCJC, cjcValue);
            }
        }

        /// <summary>
        /// 电压(V)转换成温度(摄氏)
        /// </summary>
        /// <param name="type">热电偶类型，支持B,E,J,K,N,R,S,T</param>
        /// <param name="voltValue">电压值(V)</param>
        /// <param name="cjcValue">冷点补偿温度(摄氏),默认25度</param>
        /// <returns></returns>
        public static double Convert(ThermocoupleType type, double voltValue, bool enableCJC=true, double cjcValue = 25.0)
        {
            switch (type)
            {
                case ThermocoupleType.TypeB:
                    return TC_TypeB.VoltToTemperature(voltValue, enableCJC, cjcValue);

                case ThermocoupleType.TypeE:
                    return TC_TypeE.VoltToTemperature(voltValue, enableCJC, cjcValue);

                case ThermocoupleType.TypeJ:
                    return TC_TypeJ.VoltToTemperature(voltValue, enableCJC, cjcValue);

                case ThermocoupleType.TypeK:
                    return TC_TypeK.VoltToTemperature(voltValue, enableCJC, cjcValue);

                case ThermocoupleType.TypeN:
                    return TC_TypeN.VoltToTemperature(voltValue, enableCJC, cjcValue);

                case ThermocoupleType.TypeR:
                    return TC_TypeR.VoltToTemperature(voltValue, enableCJC, cjcValue);

                case ThermocoupleType.TypeS:
                    return TC_TypeS.VoltToTemperature(voltValue, enableCJC, cjcValue);

                case ThermocoupleType.TypeT:
                    return TC_TypeT.VoltToTemperature(voltValue, enableCJC, cjcValue);

                default:
                    return TC_TypeB.VoltToTemperature(voltValue, enableCJC, cjcValue);
            }
        }

        #endregion Static
    }

    public struct ThermocoupleParameter
    {
        public double Vmax { get; set; }
        public double Vmin { get; set; }
        public double Tmax { get; set; }
        public double Tmin { get; set; }
    }

    public enum ThermocoupleType
    {
        /// <summary>
        /// B-Type热电偶，Vmin = 0.291, Vmax = 13.280, Tmin = 250.0, Tmax = 1820.0 单位mV和摄氏温度
        /// </summary>
        TypeB,

        /// <summary>
        /// E-Type热电偶，Vmin = -9.835, Vmax = 76.373, Tmin = -270.0, Tmax = 1000.0 单位mV和摄氏温度
        /// </summary>
        TypeE,

        /// <summary>
        /// J-Type热电偶，Vmin = -8.095, Vmax = 69.553, Tmin = -210.0, Tmax = 1200.0  单位mV和摄氏温度
        /// </summary>
        TypeJ,

        /// <summary>
        /// K-Type热电偶， Vmin = -6.404, Vmax = 69.553, Tmin = -250.0, Tmax = 1200.0 单位mV和摄氏温度
        /// </summary>
        TypeK,

        /// <summary>
        /// N-Type热电偶，Vmin = -4.313, Vmax = 47.513, Tmin = -250.0, Tmax = 1300.0 单位mV和摄氏温度
        /// </summary>
        TypeN,

        /// <summary>
        /// R-Type热电偶，Vmin = -0.226, Vmax = 21.101, Tmin = -50.0, Tmax = 1768.0 单位mV和摄氏温度
        /// </summary>
        TypeR,

        /// <summary>
        /// S-Type热电偶， Vmin = -0.236, Vmax = 18.693, Tmin = -50.0, Tmax = 1768.0 单位mV和摄氏温度
        /// </summary>
        TypeS,

        /// <summary>
        /// T-Type热电偶， Vmin = -6.18, Vmax = 20.872, Tmin = -250.0, Tmax = 400.0  单位mV和摄氏温度
        /// </summary>
        TypeT,
    }
}
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
    internal class TC_TypeJ
    {
        private static ThermocoupleParameter _param = new ThermocoupleParameter() { Vmin = -8.095, Vmax = 69.553, Tmin = -210.0, Tmax = 1200.0 };

        public ThermocoupleParameter Parameter { get { return _param; } }

        public static double[] VoltToTemperature(double[] volt, bool enableCJC, double cjcTemperature)
        {
            //输入电压单位是V,计算是使用的是mV
            double volt_cal = 0;
            double cjcVolt = enableCJC ? CJCTemperatureToVolt(cjcTemperature) : 0;

            double[] outputTemperature = new double[volt.Length];
            for (int i = 0; i < outputTemperature.Length; i++)
            {
                volt_cal = volt[i] * 1000.0 + cjcVolt;
                outputTemperature[i] = SinglePointCalculate(volt_cal);
            }
            return outputTemperature;
        }

        public static double VoltToTemperature(double volt, bool enableCJC, double cjcTemperature)
        {
            //输入电压单位是V,计算是使用的是mV

            double volt_cal = enableCJC ? volt * 1000.0 + CJCTemperatureToVolt(cjcTemperature) : volt * 1000.0;

            return SinglePointCalculate(volt_cal);
        }

        private static double SinglePointCalculate(double volt_cal)
        {
            double t0, v0, p1, p2, p3, p4, q1, q2, q3;
            if (volt_cal < -8.095)
            {
                return _param.Tmin;
            }
            else if (volt_cal >= -8.095 && volt_cal < 0.000)
            {
                t0 = -6.4936529E+01;
                v0 = -3.1169773E+00;
                p1 = 2.2133797E+01;
                p2 = 2.0476437E+00;
                p3 = -4.6867532E-01;
                p4 = -3.6673992E-02;
                q1 = 1.1746348E-01;
                q2 = -2.0903413E-02;
                q3 = -2.1823704E-03;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 0.000 && volt_cal < 21.840)
            {
                t0 = 2.5066947E+02;
                v0 = 1.3592329E+01;
                p1 = 1.8014787E+01;
                p2 = -6.5218881E-02;
                p3 = -1.2179108E-02;
                p4 = 2.0061707E-04;
                q1 = -3.9494552E-03;
                q2 = -7.3728206E-04;
                q3 = 1.6679731E-05;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 21.840 && volt_cal < 45.494)
            {
                t0 = 6.4950262E+02;
                v0 = 3.6040848E+01;
                p1 = 1.6593395E+01;
                p2 = 7.3009590E-01;
                p3 = 2.4157343E-02;
                p4 = 1.2787077E-03;
                q1 = 4.9172861E-02;
                q2 = 1.6813810E-03;
                q3 = 7.6067922E-05;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal > 45.494 && volt_cal <= 57.953)
            {
                t0 = 9.2510550E+02;
                v0 = 5.3433832E+01;
                p1 = 1.6243326E+01;
                p2 = 9.2793267E-01;
                p3 = 6.4644193E-03;
                p4 = 2.0464414E-03;
                q1 = 5.2541788E-02;
                q2 = 1.3682959E-04;
                q3 = 1.3454746E-04;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal > 57.953 && volt_cal <= 69.553)
            {
                t0 = 1.0511294E+03;
                v0 = 6.0956091E+01;
                p1 = 1.7156001E+01;
                p2 = -2.5931041E+00;
                p3 = -5.8339803E-02;
                p4 = 1.9954137E-02;
                q1 = -1.5305581E-01;
                q2 = -2.9523967E-03;
                q3 = 1.1340164E-03;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else
            {
                return _param.Tmax;
            }
        }

        private static double CJCTemperatureToVolt(double temperature)
        {
            double t0, v0, p1, p2, p3, p4, q1, q2;

            t0 = 2.5000000E+01;
            v0 = 1.2773432E+00;
            p1 = 5.1744084E-02;
            p2 = -5.4138663E-05;
            p3 = -2.2895769E-06;
            p4 = -7.7947143E-10;
            q1 = -1.5173342E-03;
            q2 = -4.2314514E-05;

            return v0 + (temperature - t0) * (p1 + (temperature - t0) * (p2 + (temperature - t0) * (p3 + p4 * (temperature - t0)))) / (1.0 + (temperature - t0) * (q1 + q2 * (temperature - t0)));
        }
    }
}
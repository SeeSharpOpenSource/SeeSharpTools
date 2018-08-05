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
    internal class TC_TypeS
    {
        private static ThermocoupleParameter _param = new ThermocoupleParameter() { Vmin = -0.236, Vmax = 18.693, Tmin = -50.0, Tmax = 1768.0 };

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
            if (volt_cal < -0.236)
            {
                return _param.Tmin;
            }
            else if (volt_cal >= -0.236 && volt_cal < 1.441)
            {
                t0 = 1.3792630E+02;
                v0 = 9.3395024E-01;
                p1 = 1.2761836E+02;
                p2 = 1.1089050E+02;
                p3 = 1.9898457E+01;
                p4 = 9.6152996E-02;
                q1 = 9.6545918E-01;
                q2 = 2.0813850E-01;
                q3 = 0.0000000E+00;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 1.441 && volt_cal < 6.913)
            {
                t0 = 4.7673468E+02;
                v0 = 4.0037367E+00;
                p1 = 1.0174512E+02;
                p2 = -8.9306371E+00;
                p3 = -4.2942435E+00;
                p4 = 2.0453847E-01;
                q1 = -7.1227776E-02;
                q2 = -4.4618306E-02;
                q3 = 1.6822887E-03;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 6.913 && volt_cal < 12.856)
            {
                t0 = 9.7946589E+02;
                v0 = 9.3508283E+00;
                p1 = 8.7126730E+01;
                p2 = -2.3139202E+00;
                p3 = -3.2682118E-02;
                p4 = 4.6090022E-03;
                q1 = -1.4299790E-02;
                q2 = -1.2289882E-03;
                q3 = 0.0000000E+00;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 12.856 && volt_cal < 18.693)
            {
                t0 = 1.6010461E+03;
                v0 = 1.6789315E+01;
                p1 = 8.4315871E+01;
                p2 = -1.0185043E+01;
                p3 = -4.6283954E+00;
                p4 = -1.0158749E+00;
                q1 = -1.2877783E-01;
                q2 = -5.5802216E-02;
                q3 = -1.2146518E-02;
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
            v0 = 1.4269163E-01;
            p1 = 5.9829057E-03;
            p2 = 4.5292259E-06;
            p3 = -1.3380281E-06;
            p4 = -2.3742577E-09;
            q1 = -1.0650446E-03;
            q2 = -2.2042420E-04;

            return v0 + (temperature - t0) * (p1 + (temperature - t0) * (p2 + (temperature - t0) * (p3 + p4 * (temperature - t0)))) / (1.0 + (temperature - t0) * (q1 + q2 * (temperature - t0)));
        }
    }
}
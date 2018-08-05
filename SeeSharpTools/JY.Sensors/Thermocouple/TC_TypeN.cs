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
    internal class TC_TypeN
    {
        private static ThermocoupleParameter _param = new ThermocoupleParameter() { Vmin = -4.313, Vmax = 47.513, Tmin = -250.0, Tmax = 1300.0 };

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
            if (volt_cal < -4.313)
            {
                return _param.Tmin;
            }
            else if (volt_cal >= -4.313 && volt_cal < -0.000)
            {
                t0 = -5.9610511E+01;
                v0 = -1.5000000E+00;
                p1 = 4.2021322E+01;
                p2 = 4.7244037E+00;
                p3 = -6.1153213E+00;
                p4 = -9.9980337E-01;
                q1 = 1.6385664E-01;
                q2 = -1.4994026E-01;
                q3 = -3.0810372E-02;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= -0.000 && volt_cal < 20.613)
            {
                t0 = 3.1534505E+02;
                v0 = 9.8870997E+00;
                p1 = 2.7988676E+01;
                p2 = 1.5417343E+00;
                p3 = -1.4689457E-01;
                p4 = -6.8322712E-03;
                q1 = 6.2600036E-02;
                q2 = -5.1489572E-03;
                q3 = -2.8835863E-04;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 20.613 && volt_cal < 47.513)
            {
                t0 = 1.0340172E+03;
                v0 = 3.7565475E+01;
                p1 = 2.6029492E+01;
                p2 = -6.0783095E-01;
                p3 = -9.7742562E-03;
                p4 = -3.3148813E-06;
                q1 = -2.5351881E-02;
                q2 = -3.8746827E-04;
                q3 = 1.7088177E-06;
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

            t0 = 7.0000000E+00;
            v0 = 1.8210024E-01;
            p1 = 2.6228256E-02;
            p2 = -1.5485539E-04;
            p3 = 2.1366031E-06;
            p4 = 9.2047105E-10;
            q1 = -6.4070932E-03;
            q2 = 8.2161781E-05;

            return v0 + (temperature - t0) * (p1 + (temperature - t0) * (p2 + (temperature - t0) * (p3 + p4 * (temperature - t0)))) / (1.0 + (temperature - t0) * (q1 + q2 * (temperature - t0)));
        }
    }
}
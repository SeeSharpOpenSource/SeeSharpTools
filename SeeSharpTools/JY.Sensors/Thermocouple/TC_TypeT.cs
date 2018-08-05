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
    internal class TC_TypeT
    {
        private static ThermocoupleParameter _param = new ThermocoupleParameter() { Vmin = -6.18, Vmax = 20.872, Tmin = -250.0, Tmax = 400.0 };

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
            if (volt_cal < -6.18)
            {
                return _param.Tmin;
            }
            else if (volt_cal >= -6.18 && volt_cal < -4.648)
            {
                t0 = -1.9243000E+02;
                v0 = -5.4798963E+00;
                p1 = 5.9572141E+01;
                p2 = 1.9675733E+00;
                p3 = -7.8176011E+01;
                p4 = -1.0963280E+01;
                q1 = 2.7498092E-01;
                q2 = -1.3768944E+00;
                q3 = -4.5209805E-01;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= -4.648 && volt_cal < 0.000)
            {
                t0 = -6.0000000E+01;
                v0 = -2.1528350E+00;
                p1 = 3.0449332E+01;
                p2 = -1.2946560E+00;
                p3 = -3.0500735E+00;
                p4 = -1.9226856E-01;
                q1 = 6.9877863E-03;
                q2 = -1.0596207E-01;
                q3 = -1.0774995E-02;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 0.000 && volt_cal < 9.288)
            {
                t0 = 1.3500000E+02;
                v0 = 5.9588600E+00;
                p1 = 2.0325591E+01;
                p2 = 3.3013079E+00;
                p3 = 1.2638462E-01;
                p4 = -8.2883695E-04;
                q1 = 1.7595577E-01;
                q2 = 7.9740521E-03;
                q3 = 0.0000000E+00;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 9.288 && volt_cal < 20.872)
            {
                t0 = 3.0000000E+02;
                v0 = 1.4861780E+01;
                p1 = 1.7214707E+01;
                p2 = -9.3862713E-01;
                p3 = -7.3509066E-02;
                p4 = 2.9576140E-04;
                q1 = -4.8095795E-02;
                q2 = -4.7352054E-03;
                q3 = 0.0000000E+00;
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
            v0 = 9.9198279E-01;
            p1 = 4.0716564E-02;
            p2 = 7.1170297E-04;
            p3 = 6.8782631E-07;
            p4 = 4.3295061E-11;
            q1 = 1.6458102E-02;
            q2 = 0.0000000E+00;

            return v0 + (temperature - t0) * (p1 + (temperature - t0) * (p2 + (temperature - t0) * (p3 + p4 * (temperature - t0)))) / (1.0 + (temperature - t0) * (q1 + q2 * (temperature - t0)));
        }
    }
}
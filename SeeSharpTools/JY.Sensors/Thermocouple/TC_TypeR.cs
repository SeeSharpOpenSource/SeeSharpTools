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
    internal class TC_TypeR
    {
        private static ThermocoupleParameter _param = new ThermocoupleParameter() { Vmin = -0.226, Vmax = 21.101, Tmin = -50.0, Tmax = 1768.0 };

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
            if (volt_cal < -0.226)
            {
                return _param.Tmin;
            }
            else if (volt_cal >= -0.226 && volt_cal < 1.469)
            {
                t0 = 1.3054315E+02;
                v0 = 8.8333090E-01;
                p1 = 1.2557377E+02;
                p2 = 1.3900275E+02;
                p3 = 3.3035469E+01;
                p4 = -8.5195924E-01;
                q1 = 1.2232896E+00;
                q2 = 3.5603023E-01;
                q3 = 0.0000000E+00;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 1.469 && volt_cal < 7.461)
            {
                t0 = 5.4188181E+02;
                v0 = 4.9312886E+00;
                p1 = 9.0208190E+01;
                p2 = 6.1762254E+00;
                p3 = -1.2279323E+00;
                p4 = 1.4873153E-02;
                q1 = 8.7670455E-02;
                q2 = -1.2906694E-02;
                q3 = 0.0000000E+00;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 7.461 && volt_cal < 14.277)
            {
                t0 = 1.0382132E+03;
                v0 = 1.1014763E+01;
                p1 = 7.4669343E+01;
                p2 = 3.4090711E+00;
                p3 = -1.4511205E-01;
                p4 = 6.3077387E-03;
                q1 = 5.6880253E-02;
                q2 = -2.0512736E-03;
                q3 = 0.0000000E+00;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 14.277 && volt_cal < 21.101)
            {
                t0 = 1.5676133E+03;
                v0 = 1.8397910E+01;
                p1 = 7.1646299E+01;
                p2 = -1.0866763E+00;
                p3 = -2.0968371E+00;
                p4 = -7.6741168E-01;
                q1 = -1.9712341E-02;
                q2 = -2.9903595E-02;
                q3 = -1.0766878E-02;
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
            v0 = 1.4067016E-01;
            p1 = 5.9330356E-03;
            p2 = 2.7736904E-05;
            p3 = -1.0819644E-06;
            p4 = -2.3098349E-09;
            q1 = 2.6146871E-03;
            q2 = -1.8621487E-04;

            return v0 + (temperature - t0) * (p1 + (temperature - t0) * (p2 + (temperature - t0) * (p3 + p4 * (temperature - t0)))) / (1.0 + (temperature - t0) * (q1 + q2 * (temperature - t0)));
        }
    }
}
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
    internal class TC_TypeB
    {
        private static ThermocoupleParameter _param = new ThermocoupleParameter() { Vmin = 0.291, Vmax = 13.280, Tmin = 250.0, Tmax = 1820.0 };

        public ThermocoupleParameter Parameter { get { return _param; } }

        public static double[] VoltToTemperature(double[] volt, bool enableCJC,double cjcTemperature)
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

        public static double VoltToTemperature(double volt, bool enableCJC,double cjcTemperature)
        {
            //输入电压单位是V,计算是使用的是mV

            double volt_cal = enableCJC ? volt * 1000.0 +  CJCTemperatureToVolt(cjcTemperature) : volt * 1000.0;

            return SinglePointCalculate(volt_cal);
        }

        private static double SinglePointCalculate(double volt_cal)
        {
            double t0, v0, p1, p2, p3, p4, q1, q2, q3;

            if (volt_cal < 0.291)
            {
                return _param.Tmin;
            }
            else if (volt_cal >= 0.291 && volt_cal < 2.431)
            {
                t0 = 5.0000000E+02;
                v0 = 1.2417900E+00;
                p1 = 1.9858097E+02;
                p2 = 2.4284248E+01;
                p3 = -9.7271640E+01;
                p4 = -1.5701178E+01;
                q1 = 3.1009445E-01;
                q2 = -5.0880251E-01;
                q3 = -1.6163342E-01;
                return t0 + (volt_cal - v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 2.431 && volt_cal < 13.820)
            {
                t0 = 1.2461474E+03;
                v0 = 7.2701221E+00;
                p1 = 9.4321033E+01;
                p2 = 7.3899296E+00;
                p3 = -1.5880987E-01;
                p4 = 1.2681877E-02;
                q1 = 1.0113834E-01;
                q2 = -1.6145962E-03;
                q3 = -4.1086314E-06;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 * (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else
            {
                return _param.Tmax;
            }
        }

        private static double CJCTemperatureToVolt(double temperature)
        {
            double t0, v0, p1, p2, p3, p4, q1, q2;

            t0 = 4.2000000E+01;
            v0 = 3.3933898E-04;
            p1 = 2.1196684E-04;
            p2 = 3.3801250E-06;
            p3 = -1.4793289E-07;
            p4 = -3.3571424E-09;
            q1 = -1.0920410E-02;
            q2 = -4.9782932E-04;

            return v0 + (temperature - t0) * (p1 + (temperature - t0) * (p2 + (temperature - t0) * (p3 + p4 * (temperature - t0)))) / (1.0 + (temperature - t0) * (q1 + q2 * (temperature - t0)));
        }
    }
}
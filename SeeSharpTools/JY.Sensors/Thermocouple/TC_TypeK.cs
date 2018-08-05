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
    internal class TC_TypeK
    {
        private static ThermocoupleParameter _param = new ThermocoupleParameter() { Vmin = -6.404, Vmax = 69.553, Tmin = -250.0, Tmax = 1200.0 };

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
            if (volt_cal < -6.404)
            {
                return _param.Tmin;
            }
            else if (volt_cal >= -6.404 && volt_cal < -3.554)
            {
                t0 = -1.2147164E+02;
                v0 = -4.1790858E+00;
                p1 = 3.6069513E+01;
                p2 = 3.0722076E+01;
                p3 = 7.7913860E+00;
                p4 = 5.2593991E-01;
                q1 = 9.3939547E-01;
                q2 = 2.7791285E-01;
                q3 = 2.5163349E-02;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= -3.554 && volt_cal < 4.096)
            {
                t0 = -8.7935962E+00;
                v0 = -3.4489914E-01;
                p1 = 2.5678719E+01;
                p2 = -4.9887904E-01;
                p3 = -4.4705222E-01;
                p4 = -4.4869203E-02;
                q1 = 2.3893439E-04;
                q2 = -2.0397750E-02;
                q3 = -1.8424107E-03;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 4.096 && volt_cal < 16.397)
            {
                t0 = 3.1018976E+02;
                v0 = 1.2631386E+01;
                p1 = 2.4061949E+01;
                p2 = 4.0158622E+00;
                p3 = 2.6853917E-01;
                p4 = -9.7188544E-03;
                q1 = 1.6995872E-01;
                q2 = 1.1413069E-02;
                q3 = -3.9275155E-04;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal > 16.397 && volt_cal <= 33.275)
            {
                t0 = 6.0572562E+02;
                v0 = 2.5148718E+01;
                p1 = 2.3539401E+01;
                p2 = 4.6547228E-02;
                p3 = 1.3444400E-02;
                p4 = 5.9236853E-04;
                q1 = 8.3445513E-04;
                q2 = 4.6121445E-04;
                q3 = 2.5488122E-05;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal > 33.275 && volt_cal <= 69.553)
            {
                t0 = 1.0184705E+03;
                v0 = 4.1993851E+01;
                p1 = 2.5783239E+01;
                p2 = -1.8363403E+00;
                p3 = 5.6176662E-02;
                p4 = 1.8532400E-04;
                q1 = -7.4803355E-02;
                q2 = 2.3841860E-03;
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
            v0 = 1.0003453E+00;
            p1 = 4.0514854E-02;
            p2 = -3.8789638E-05;
            p3 = -2.8608478E-06;
            p4 = -9.5367041E-10;
            q1 = -1.3948675E-03;
            q2 = -6.7976627E-05;

            return v0 + (temperature - t0) * (p1 + (temperature - t0) * (p2 + (temperature - t0) * (p3 + p4 * (temperature - t0)))) / (1.0 + (temperature - t0) * (q1 + q2 * (temperature - t0)));
        }
    }
}
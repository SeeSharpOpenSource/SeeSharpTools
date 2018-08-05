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
    internal class TC_TypeE
    {
        private static ThermocoupleParameter _param = new ThermocoupleParameter() { Vmin = -9.835, Vmax = 76.373, Tmin = -270.0, Tmax = 1000.0 };

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

            if (volt_cal < -9.835)
            {
                return _param.Tmin;
            }
            else if (volt_cal >= -9.835 && volt_cal < -5.237)
            {
                t0 = -1.1721668E+02;
                v0 = -5.9901698E+00;
                p1 = 2.3647275E+01;
                p2 = 1.2807377E+01;
                p3 = 2.0665069E+00;
                p4 = 8.6513472E-02;
                q1 = 5.8995860E-01;
                q2 = 1.0960713E-01;
                q3 = 6.1769588E-03;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= -5.237 && volt_cal < 0.591)
            {
                t0 = -5.0000000E+01;
                v0 = -2.7871777E+00;
                p1 = 1.9022736E+01;
                p2 = -1.7042725E+00;
                p3 = -3.5195189E-01;
                p4 = 4.7766102E-03;
                q1 = -6.5379760E-02;
                q2 = -2.1732833E-02;
                q3 = 0.0000000E+00;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal >= 0.591 && volt_cal < 24.964)
            {
                t0 = 2.5014600E+02;
                v0 = 1.7191713E+01;
                p1 = 1.3115522E+01;
                p2 = 1.1780364E+00;
                p3 = 3.6422433E-02;
                p4 = 3.9584261E-04;
                q1 = 9.3112756E-02;
                q2 = 2.9804232E-03;
                q3 = 3.3263032E-05;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal > 24.964 && volt_cal <= 53.112)
            {
                t0 = 6.0139890E+02;
                v0 = 4.5206167E+01;
                p1 = 1.2399357E+01;
                p2 = 4.3399963E-01;
                p3 = 9.1967085E-03;
                p4 = 1.6901585E-04;
                q1 = 3.4424680E-02;
                q2 = 6.9741215E-04;
                q3 = 1.2946992E-05;
                return t0 + (volt_cal + -v0) * (p1 + (volt_cal - v0) * (p2 + (volt_cal - v0) * (p3 + p4 * (volt_cal - v0)))) / (1.0 + (volt_cal - v0) * (q1 + (volt_cal - v0) * (q2 + q3 * (volt_cal - v0))));
            }
            else if (volt_cal > 53.112 && volt_cal <= 76.373)
            {
                t0 = 8.0435911E+02;
                v0 = 6.1359178E+01;
                p1 = 1.2759508E+01;
                p2 = -1.1116072E+00;
                p3 = 3.5332536E-02;
                p4 = 3.3080380E-05;
                q1 = -8.8196889E-02;
                q2 = 2.8497415E-03;
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
            v0 = 1.4950582E+00;
            p1 = 6.0958443E-02;
            p2 = -2.7351789E-04;
            p3 = -1.9130146E-05;
            p4 = -1.3948840E-08;
            q1 = -5.2382378E-03;
            q2 = -3.0970168E-04;

            return v0 + (temperature - t0) * (p1 + (temperature - t0) * (p2 + (temperature - t0) * (p3 + p4 * (temperature - t0)))) / (1.0 + (temperature - t0) * (q1 + q2 * (temperature - t0)));
        }
    }
}
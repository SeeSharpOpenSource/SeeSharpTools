using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpTools.JY.Sensors
{
    /// <summary>
    /// RTD类型
    /// </summary>
    public enum RTDType
    {
        /// <summary>
        /// 当温度为0℃时阻值为100Ω，100℃时约为138.5Ω；温度每变化1℃，其阻值变化约0.38Ω。
        /// </summary>
        PT100,
        /// <summary>
        /// 当温度为0℃时阻值为1000Ω，100℃时约为1385.005Ω；温度每变化1℃，其阻值变化约3.8Ω。
        /// </summary>
        PT1000
    }

    internal interface IRTDValueConvertible
    {
        double ConvertTemperatureToResistance(double temperature);

        double ConvertResistanceToTemperature(double resistance);
    }

    /// <summary>
    /// 用于TCR(即温度系数)为3851的RTD(PT100/PT1000)的电阻值与温度值的相互转换。
    /// 该类中所用到的常数和公式来源于铂金RTD标准DIN/IEC 60751:2008(Edition 2.0)。
    /// </summary>
    internal static class RTD3851ValueConvertor
    {
         static RTD3851ValueConvertor()
         {
            //根据公式动态生成TRTable，TRTable用于根据电阻反求温度
            TRTable = new int[MaxTemperature - MinTemperature + 1];
            for (int i = 0; i < TRTable.Length; i++)
            {
                TRTable[i] = (int)Math.Round((ConvertTemperatureToResistance(MinTemperature + i) * 100));
            }
         }

        #region -- Constant --

        /// <summary>
        /// Temperature Coefficient.
        /// </summary>
        public const int TCR = 3851;

        /// <summary>
        /// Callendar-Van Dusen fanctor A, ohm*degree^-1.
        /// </summary>
        public const double A = 3.9083e-3;

        /// <summary>
        /// Callendar-Van Dusen fanctor B, ohm*degree^-2.
        /// </summary>
        public const double B = -5.775e-7;

        /// <summary>
        /// Callendar-Van Dusen fanctor C, ohm*degree^-3.
        /// </summary>
        public const double C = -4.183e-12;

        /// <summary>
        /// RTD3851的最小可测温度值
        /// </summary>
        public const int MinTemperature = -200;

        /// <summary>
        /// RTD3851的最大可测温度值
        /// </summary>
        public const int MaxTemperature = 850;

        /// <summary>
        /// PT100/PT1000温度电阻分度表，该表由静态方法ConvertTemperatureToResistance动态生成。
        /// 数组的索引值为温度值，从-200℃开始，每次增1℃，最终到850℃。
        /// 数组的成员值为归一化的电阻值，应用于PT100时需除以100，应用于PT1000时需除以10。
        /// </summary>
        public static int[] TRTable;


        #endregion

        /// <summary>
        /// 根据温度值算出电阻值。
        /// </summary>
        /// <param name="temperature">温度值</param>
        /// <param name="rtdType">RTD类型(PT100/PT1000),该参数决定R0为100还是1000</param>
        /// <returns></returns>
        public static double ConvertTemperatureToResistance(double temperature, RTDType rtdType = RTDType.PT100)
        {
            double r0; //0℃时RTD的阻值
            switch(rtdType)
            {
                case RTDType.PT100: r0 = 100; break;
                case RTDType.PT1000: r0 = 1000; break;
                default: r0 = 100; break;
            }

            if (temperature <= 0 && temperature >= MinTemperature)
            {
                return r0 * (1 + A * temperature + B * Math.Pow(temperature, 2) + C * (temperature - r0) * Math.Pow(temperature, 3));
            }
            else if (temperature > 0 && temperature <= MaxTemperature)
            {
                return r0 * (1 + A * temperature + B * Math.Pow(temperature, 2));
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 根据电阻值算出温度值。
        /// </summary>
        /// <param name="resistance">电阻值</param>
        /// <param name="rtdType">RTD类型(PT100/PT1000),该参数决定R0为100还是1000</param>
        /// <returns></returns>
        public static double ConvertResistanceToTemperature(double resistance, RTDType rtdType = RTDType.PT100)
        {
            double normalizeFactor = 1; //使用RTTable前需对传入的resistance进行归一化，具体描述见RTTable的注释
            switch (rtdType)
            {
                case RTDType.PT100: normalizeFactor = 100; break;
                case RTDType.PT1000: normalizeFactor = 10; break;
                default: normalizeFactor = 100; break;
            }

            //将电阻归一化到TRTable描述的R值上
            int resistanceNormalized = (int)(resistance * normalizeFactor);

            return Interpolation.LinearInterpolation1D(TRTable, 1, MinTemperature, resistanceNormalized);
        }

    }

}


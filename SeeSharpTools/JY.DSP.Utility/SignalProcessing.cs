using System;
using System.Collections.Generic;
using MathNet.Numerics;

namespace SeeSharpTools.JY.DSP.Utility
{
    /// <summary>
    /// 信号处理类库，包含峰值检测、过零点检测以及自定义上下限范围检测
    /// </summary>
    public static class SignalProcessing
    {
        /// <summary>
        /// 列举出超过阈值/低于阈值的集合
        /// </summary>
        /// <param name="data">输入数组</param>
        /// <param name="threshold">阈值</param>
        /// <param name="isAboveLevel">T:侦测波峰；F:侦测波谷</param>
        /// <returns></returns>
        public static Dictionary<int, double> CheckThreshold(double[] data, double threshold, bool isAboveLevel = true)
        {
            #region 检查入参
            if (data.Length < 1 || data == null)
            {
                throw new Exception("输入资料不能为空");
            }
            #endregion

            try
            {                
                //创建字典存放位置以及峰值
                Dictionary<int, double> dict = new Dictionary<int, double>();

                for (int i = 1; i < data.Length - 1; i++)
                {
                    if (isAboveLevel)
                    {
                        //侦测波峰
                        if (data[i] > data[i - 1] && data[i] > data[i + 1] && data[i] >= threshold)
                        {
                            dict.Add(i, data[i]);
                        }
                    }
                    else
                    {
                        //侦测波谷
                        if (data[i] < data[i - 1] && data[i] < data[i + 1] && data[i] <= threshold)
                        {
                            dict.Add(i, data[i]);
                        }
                    }
                }

                return dict;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 过零点检测，列举出过零点的值（和下一点作比较）
        /// </summary>
        /// <param name="data">输入数组</param>
        /// <param name="isAbove">T:大于零点；F:小于零点</param>
        /// <returns></returns>
        public static Dictionary<int, double> CheckCrossZeroPoints(double[] data, bool isAbove = true)
        {
            #region 检查入参
            if (data.Length < 1 || data == null)
            {
                throw new Exception("输入资料不能为空");
            }
            #endregion

            try
            {
                //创建字典存放位置以及值
                Dictionary<int, double> dict = new Dictionary<int, double>();

                for (int i = 0; i < data.Length-1 ; i++)
                {
                    if (isAbove)
                    {
                        //侦测爬升过零点
                        if (data[i] < data[i + 1] && data[i] * data[ i + 1]<0)
                        {
                            dict.Add(i, data[i]);
                        }
                    }
                    else
                    {
                        //侦测下降过零点
                        if (data[i] > data[i + 1] && data[i] * data[i + 1] < 0)
                        {
                            dict.Add(i, data[i]);
                        }
                    }
                }

                return dict;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 自定义上下限范围检测(三个数组长度需要一样，大于上限返回1,小于下限返回-1,在范围内返回0）
        /// </summary>
        /// <param name="data">输入数组</param>
        /// <param name="highLimit">上限数组</param>
        /// <param name="lowLimit">下限数组</param>
        /// <returns></returns>
        public static List<int> CheckInRange(double[]data, double[] highLimit, double[] lowLimit)
        {
            #region 检查入参

            if (data.Length!=highLimit.Length||data.Length!=lowLimit.Length)
            {
                throw new Exception("数组长度不同");
            }

            #endregion

            List<int> dict = new List<int>();

            for (int i = 0; i < data.Length; i++)
            {

                if (data[i] > highLimit[i])
                {
                    dict.Add(1);
                }
                else if (data[i] < lowLimit[i])
                {
                    dict.Add(-1);
                }
                else
                {
                    dict.Add(0);
                }

            }
            return dict;
        }

        /// <summary>
        /// 内插值计算，指定xValues以及yValues，经过大小排序后返回指定xPoint资料的内插值。默认使用线性内插
        /// </summary>
        /// <param name="xValues">数据点</param>
        /// <param name="yValues">数据值</param>
        /// <param name="xPoint">指定的数据点</param>
        /// <param name="type">内插类型</param>
        /// <returns></returns>
        public static double Interpolate(double[] xValues, double[] yValues, double xPoint, IntepolationType type= IntepolationType.Linear)
        {
            switch (type)
            {                
                case IntepolationType.Linear:
                    return MathNet.Numerics.Interpolate.Linear(xValues, yValues).Interpolate(xPoint);
                case IntepolationType.CubicSpline:
                    return MathNet.Numerics.Interpolate.CubicSpline(xValues, yValues).Interpolate(xPoint);
                case IntepolationType.LogLinear:
                    return MathNet.Numerics.Interpolate.LogLinear(xValues, yValues).Interpolate(xPoint);
                case IntepolationType.Polynomial:
                    return MathNet.Numerics.Interpolate.Polynomial(xValues, yValues).Interpolate(xPoint);
                case IntepolationType.Step:
                    return MathNet.Numerics.Interpolate.Step(xValues, yValues).Interpolate(xPoint);
                default:
                    return MathNet.Numerics.Interpolate.Linear(xValues, yValues).Interpolate(xPoint);
            }
        }

    }
    /// <summary>
    /// 内插类型
    /// </summary>
    public enum IntepolationType
    {
        /// <summary>
        /// Linear
        /// </summary>
        Linear,
        /// <summary>
        /// CubicSpline
        /// </summary>
        CubicSpline,
        /// <summary>
        /// LogLinear
        /// </summary>
        LogLinear,
        /// <summary>
        /// Polynomial
        /// </summary>
        Polynomial,
        /// <summary>
        /// Step
        /// </summary>
        Step
    }
}

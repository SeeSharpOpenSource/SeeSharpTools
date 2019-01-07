using System;

namespace SeeSharpTools.JY.Mathematics
{
    /// <summary>
    /// 曲线拟合
    /// </summary>
    public class Fitting
    {
        /// <summary>
        /// 指数型拟合 (y=a*exp(b*x))
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <returns>返回拟合的函式</returns>
        public static Func<double, double> ExponentialFittingFunc(double[] x, double[] y)
        {
            return Engine.Base.ExponentialFittingFunc(x, y);
        }

        /// <summary>
        /// 指数型拟合 (y=a*exp(b*x))
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <returns>返回拟合的系数tuple(a,b)</returns>
        public static Tuple<double, double> ExponentialFitting(double[] x, double[] y)
        {
            return Engine.Base.ExponentialFitting(x, y);
        }

        /// <summary>
        /// 线性拟合 (y=a+b*x)
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <returns>返回拟合的函式</returns>
        public static Func<double, double> LinearFittingFunc(double[] x, double[] y)
        {
            return Engine.Base.LinearFittingFunc(x, y);
        }

        /// <summary>
        /// 线性拟合 (y=a+b*x)
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <returns>返回拟合的系数Tuple(a,b)</returns>
        public static Tuple<double, double> LinearFitting(double[] x, double[] y)
        {
            return Engine.Base.LinearFitting(x, y);
        }

        /// <summary>
        /// 多项式拟合 (y=a0+a1*x+a2*x^2+.....)
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <param name="order">多项式项次</param>
        /// <returns>返回拟合的含式</returns>
        public static Func<double, double> PolynomialFittingFunc(double[] x, double[] y, int order)
        {
            return Engine.Base.PolynomialFittingFunc(x, y, order);
        }

        /// <summary>
        /// 多项式拟合 (y=a0+a1*x+a2*x^2+.....)
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <param name="order">多项式项次</param>
        /// <returns>返回拟合的系数(a0,a1,a2,a3.....)</returns>
        public static double[] PolynomialFitting(double[] x, double[] y, int order)
        {
            return Engine.Base.PolynomialFitting(x, y, order);
        }
    }
}
namespace SeeSharpTools.JY.Mathematics
{
    /// <summary>
    /// 内插
    /// </summary>
    public class Interpolation
    {
        /// <summary>
        /// CubicSpline内插
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <param name="xValue">指定的x值</param>
        /// <returns>返回对应的内插值</returns>
        public static double Interpolate_CubicSpline(double[] x, double[] y, double xValue)
        {
            return Engine.Base.Interpolate_CubicSpline(x, y, xValue);
        }

        /// <summary>
        /// Linear内插
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <param name="xValue">指定的x值</param>
        /// <returns>返回对应的内插值</returns>
        public static double Interpolate_Linear(double[] x, double[] y, double xValue)
        {
            return Engine.Base.Interpolate_Linear(x, y, xValue);
        }

        /// <summary>
        /// LogLinear内插
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <param name="xValue">指定的x值</param>
        /// <returns>返回对应的内插值</returns>
        public static double Interpolate_LogLinear(double[] x, double[] y, double xValue)
        {
            return Engine.Base.Interpolate_LogLinear(x, y, xValue);
        }

        /// <summary>
        /// Polynomia内插
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <param name="xValue">指定的x值</param>
        /// <returns>返回对应的内插值</returns>
        public static double Interpolate_Polynomial(double[] x, double[] y, double xValue)
        {
            return Engine.Base.Interpolate_Polynomial(x, y, xValue);
        }

        /// <summary>
        /// Step内插
        /// </summary>
        /// <param name="x">x数组</param>
        /// <param name="y">y数组</param>
        /// <param name="xValue">指定的x值</param>
        /// <returns>返回对应的内插值</returns>
        public static double Interpolate_Step(double[] x, double[] y, double xValue)
        {
            return Engine.Base.Interpolate_Step(x, y, xValue);
        }
    }
}
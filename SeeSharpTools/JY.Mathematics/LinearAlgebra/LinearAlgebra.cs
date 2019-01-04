namespace SeeSharpTools.JY.Mathematics
{
    /// <summary>
    /// 线性代数
    /// </summary>
    public class LinearAlgebra
    {
        /// <summary>
        /// 乘积
        /// </summary>
        /// <param name="a">矢量a</param>
        /// <param name="b">矢量b</param>
        /// <returns>乘积值</returns>
        public static double Dot(double[] a, double[] b)
        {
            return Engine.Base.Dot(a, b);
        }

        /// <summary>
        /// 乘积
        /// </summary>
        /// <param name="a">矢量a</param>
        /// <param name="b">矢量b</param>
        /// <returns>乘积值</returns>
        public static float Dot(float[] a, float[] b)
        {
            return Engine.Base.Dot(a, b);
        }

        /// <summary>
        /// 范数
        /// </summary>
        /// <param name="a">矢量a</param>
        /// <param name="b">矢量b</param>
        /// <returns>范数值</returns>
        public static double Norm(double[] a, double p)
        {
            return Engine.Base.Norm(a, p);
        }

        /// <summary>
        /// 范数
        /// </summary>
        /// <param name="a">矢量a</param>
        /// <param name="b">矢量b</param>
        /// <returns>范数值</returns>
        public static double Norm(float[] a, double p)
        {
            return Engine.Base.Norm(a, p);
        }
    }
}
namespace SeeSharpTools.JY.Mathematics
{
    /// <summary>
    /// 统计分析类库
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// 直方图
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="binSize">直方条数目</param>
        /// <param name="min">分类最小值（若min=max，自动适配范围)</param>
        /// <param name="max">分类最大值（若min=max，自动适配范围)</param>
        /// <returns>返回直方图数组</returns>
        public static int[] Histogram(double[] data, int binSize, double min = 0, double max = 0)
        {
            return Engine.Base.Histogram(data, binSize, min, max);
        }

        /// <summary>
        /// Kurtosis
        /// </summary>
        /// <param name="src">数组</param>
        /// <returns>返回值</returns>
        public static double Kurtosis(double[] src)
        {
            return Engine.Base.Kurtosis(src);
        }

        /// <summary>
        /// Mean
        /// </summary>
        /// <param name="src">数组</param>
        /// <returns>返回值</returns>
        public static double Mean(double[] src)
        {
            return Engine.Base.Mean(src);
        }

        /// <summary>
        /// Median
        /// </summary>
        /// <param name="src">数组</param>
        /// <returns>返回值</returns>
        public static double Median(double[] src)
        {
            return Engine.Base.Median(src);
        }

        /// <summary>
        /// Percentile
        /// </summary>
        /// <param name="data">数组</param>
        /// <param name="place">百分比的位置，单位：%</param>
        /// <returns>返回值</returns>
        public static double Percentile(double[] data, int place)
        {
            return Engine.Base.Percentile(data, place);
        }

        /// <summary>
        /// RMS
        /// </summary>
        /// <param name="src">数组</param>
        /// <returns>返回值</returns>
        public static double RMS(double[] src)
        {
            return Engine.Base.RMS(src);
        }

        /// <summary>
        /// Skewness
        /// </summary>
        /// <param name="src">数组</param>
        /// <returns>返回值</returns>
        public static double Skewness(double[] src)
        {
            return Engine.Base.Skewness(src);
        }

        /// <summary>
        /// StandardDeviationn
        /// </summary>
        /// <param name="src">数组</param>
        /// <returns>返回值</returns>
        public static double StandardDeviation(double[] src)
        {
            return Engine.Base.StandardDeviation(src);
        }

        /// <summary>
        /// Variance
        /// </summary>
        /// <param name="src">数组</param>
        /// <returns>返回值</returns>
        public static double Variance(double[] src)
        {
            return Engine.Base.Variance(src);
        }
    }
}
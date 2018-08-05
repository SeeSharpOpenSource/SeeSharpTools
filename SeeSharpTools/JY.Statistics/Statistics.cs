using MathNet.Numerics.Statistics;
using System;
using System.Threading.Tasks;

namespace SeeSharpTools.JY.Statistics
{
    /// <summary>
    /// Statistical functions based on MathNet and Intel IPP, to use Intel IPP platform, please place intel IPP dll in the same folder of SeeSharpTools.JY.Statistics.dll. \NativeDLLs\ia32 for x86 platform and \NativeDLLs\intel64 for x64 platform.
    /// </summary>
    public class Statistics
    {
        #region Static Functions

        /// <summary>
        /// Find the Maximum value from the double array (supported platforms: MathNet, Intel IPP)
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>maximum value</returns>
        public static double Maximum(double[] data)
        {
            try
            {
                switch (Engine.Provider)
                {
                    case ProviderEngine.MathNet:
                        return ArrayStatistics.Maximum(data);

                    case ProviderEngine.IntelIPP:
                        return IPP.Statistics.FindMaxValue(data);

                    default:
                        return ArrayStatistics.Maximum(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Find the Minimum value from the double array
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns></returns>
        public static double Minimum(double[] data)
        {
            try
            {
                switch (Engine.Provider)
                {
                    case ProviderEngine.MathNet:
                        return ArrayStatistics.Minimum(data);

                    case ProviderEngine.IntelIPP:
                        return IPP.Statistics.FindMinValue(data);

                    default:
                        return ArrayStatistics.Minimum(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Find the Mean value from the double array (supported platforms: MathNet, Intel IPP)
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>minimum value</returns>
        public static double Mean(double[] data)
        {
            try
            {
                switch (Engine.Provider)
                {
                    case ProviderEngine.MathNet:
                        return ArrayStatistics.Mean(data);

                    case ProviderEngine.IntelIPP:
                        return IPP.Statistics.Mean(data);

                    default:
                        return ArrayStatistics.Mean(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Find the RMS value from the double array (supported platforms: MathNet)
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>RMS value</returns>
        public static double RMS(double[] data)
        {
            try
            {
                //IPP does not support RMS function yet
                switch (Engine.Provider)
                {
                    case ProviderEngine.MathNet:
                        return ArrayStatistics.RootMeanSquare(data);

                    default:
                        return ArrayStatistics.RootMeanSquare(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// FInd the standard deviation from the double array (supported platforms: MathNet, Intel IPP)
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>standard deviation value</returns>
        public static double StandardDeviation(double[] data)
        {
            try
            {
                switch (Engine.Provider)
                {
                    case ProviderEngine.MathNet:
                        return ArrayStatistics.StandardDeviation(data);

                    case ProviderEngine.IntelIPP:
                        return IPP.Statistics.StdDev(data);

                    default:
                        return ArrayStatistics.StandardDeviation(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Find the variance from the double array (supported platforms: MathNet)
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>variance value</returns>
        public static double Variance(double[] data)
        {
            try
            {
                //IPP does not support Variance yet
                switch (Engine.Provider)
                {
                    case ProviderEngine.MathNet:
                        return ArrayStatistics.Variance(data);

                    case ProviderEngine.IntelIPP:
                        var stddev = IPP.Statistics.StdDev(data);
                        return Math.Pow(stddev, 2);

                    default:
                        return ArrayStatistics.Variance(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Find the skewness from the double array (supported platforms: MathNet)
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>skewness value</returns>
        public static double Skewness(double[] data)
        {
            try
            {
                //IPP does not support Skewness yet
                switch (Engine.Provider)
                {
                    case ProviderEngine.MathNet:
                        var stat = new DescriptiveStatistics(data);
                        return stat.Skewness;

                    default:
                        var stats = new DescriptiveStatistics(data);
                        return stats.Skewness;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Find the Kurtosis from the double array (supported platforms: MathNet)
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>kurtosis value</returns>
        public static double Kurtosis(double[] data)
        {
            try
            {
                //IPP does not support Kurtosis yet
                switch (Engine.Provider)
                {
                    case ProviderEngine.MathNet:
                        var stat = new DescriptiveStatistics(data);
                        return stat.Kurtosis;

                    default:
                        var stats = new DescriptiveStatistics(data);
                        return stats.Kurtosis;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the histogram based on the given double array ((supported platforms: MathNet)
        /// </summary>
        /// <param name="data">input data</param>
        /// <param name="binSize">number of the bin</param>
        /// <param name="min">minimum value of the range, autorange if max<min</param>
        /// <param name="max">maximum value of the range, autorange if max<min</param>
        /// <returns>histogram result</returns>
        public static int[] Histogram(double[] data, int binSize, double min = 0, double max = 0)
        {
            try
            {
                Histogram hgram;
                int[] stats = new int[binSize];
                if (max <= min)
                {
                    hgram = new Histogram(data, binSize);
                }
                else
                {
                    hgram = new Histogram(data, binSize, min, max);
                }
                Parallel.For(0, binSize, new Action<int>(i => { stats[i] = (int)hgram[i].Count; }));
                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Static Functions
    }
}
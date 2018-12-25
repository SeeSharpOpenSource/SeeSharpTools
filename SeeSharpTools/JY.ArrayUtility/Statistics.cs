using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using MathNet.Numerics.Statistics;

namespace SeeSharpTools.JY.ArrayUtility
{
    /// <summary>
    /// 
    /// </summary>
    public static class Statistics
    {
        #region Mean value related

        /// <summary>
        /// Calculate the mean value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The mean value of array data.</returns>
        /// <exception cref="NotSupportedException">Supported data type: double/float/int.</exception>
        public static double Mean<TDataType>(TDataType[] data)
        {
            double mean = double.NaN;
            if (ReferenceEquals(typeof(TDataType), typeof(double)))
            {
                double[] doubleData = data as double[];
                mean = ArrayStatistics.Mean(doubleData);
            }
            else if (ReferenceEquals(typeof(TDataType), typeof(float)))
            {
                float[] floatData = data as float[];
                mean = ArrayStatistics.Mean(floatData);
            }
            else if (ReferenceEquals(typeof(TDataType), typeof(int)))
            {
                int[] intData = data as int[];
                mean = ArrayStatistics.Mean(intData);
            }
            else
            {
                throw new NotSupportedException("Unsupported data type.");
            }
            return mean;
        }


        /// <summary>
        /// Calculate the mean value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The mean value of array data.</returns>
        public static double Mean<TDataType>(TDataType[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return Mean(GetDirectionData(data, index, majorOrder));
        }

        /// <summary>
        /// Calculate the harmonic mean value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The harmonic mean value of array data.</returns>
        /// <exception cref="NotSupportedException">Supported data type: double/float/int.</exception>
        public static double HarmonicMean<TDataType>(TDataType[] data)
        {
            double harmonicMean = double.NaN;
            if (ReferenceEquals(typeof (TDataType), typeof (double)))
            {
                double[] doubleData = data as double[];
                harmonicMean = ArrayStatistics.HarmonicMean(doubleData);
            }
            else if (ReferenceEquals(typeof (TDataType), typeof (float)))
            {
                float[] floatData = data as float[];
                harmonicMean = ArrayStatistics.HarmonicMean(floatData);
            }
            else if (ReferenceEquals(typeof (TDataType), typeof (int)))
            {
                int[] intData = data as int[];
                harmonicMean = ArrayStatistics.HarmonicMean(intData);
            }
            else
            {
                throw new NotSupportedException("Unsupported data type.");
            }
            return harmonicMean;
        }

        /// <summary>
        /// Calculate the harmonic mean value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The harmonic mean value of array data.</returns>
        public static double HarmonicMean<TDataType>(TDataType[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return HarmonicMean(GetDirectionData(data, index, majorOrder));
        }

        /// <summary>
        /// Calculate the root mean sqaure value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The root mean square value of array data.</returns>
        /// <exception cref="NotSupportedException">Supported data type: double/float/int.</exception>
        public static double RootMeanSqaure<TDataType>(TDataType[] data)
        {
            double rootMeanSqure = double.NaN;
            if (ReferenceEquals(typeof(TDataType), typeof(double)))
            {
                double[] doubleData = data as double[];
                rootMeanSqure = ArrayStatistics.RootMeanSquare(doubleData);
            }
            else if (ReferenceEquals(typeof(TDataType), typeof(float)))
            {
                float[] floatData = data as float[];
                rootMeanSqure = ArrayStatistics.RootMeanSquare(floatData);
            }
            else if (ReferenceEquals(typeof(TDataType), typeof(int)))
            {
                int[] intData = data as int[];
                rootMeanSqure = ArrayStatistics.RootMeanSquare(intData);
            }
            else
            {
                throw new NotSupportedException("Unsupported data type.");
            }
            return rootMeanSqure;
        }

        /// <summary>
        /// Calculate the root mean square value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The root mean value of array data.</returns>
        public static double RootMeanSqaure<TDataType>(TDataType[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return RootMeanSqaure(GetDirectionData(data, index, majorOrder));
        }


        /// <summary>
        /// Calculate the harmonic mean value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The harmonic mean value of array data.</returns>
        /// <exception cref="NotSupportedException">Supported data type: double/float/int.</exception>
        public static double GeometricMean<TDataType>(TDataType[] data)
        {
            double harmonicMean = double.NaN;
            if (ReferenceEquals(typeof(TDataType), typeof(double)))
            {
                double[] doubleData = data as double[];
                harmonicMean = ArrayStatistics.GeometricMean(doubleData);
            }
            else if (ReferenceEquals(typeof(TDataType), typeof(float)))
            {
                float[] floatData = data as float[];
                harmonicMean = ArrayStatistics.GeometricMean(floatData);
            }
            else if (ReferenceEquals(typeof(TDataType), typeof(int)))
            {
                int[] intData = data as int[];
                harmonicMean = ArrayStatistics.GeometricMean(intData);
            }
            else
            {
                throw new NotSupportedException("Unsupported data type.");
            }
            return harmonicMean;
        }

        /// <summary>
        /// Calculate the harmonic mean value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The harmonic mean value of array data.</returns>
        public static double GeometricMean<TDataType>(TDataType[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return GeometricMean(GetDirectionData(data, index, majorOrder));
        }

        #endregion

        #region Percentile related

        /// <summary>
        /// Get minimum/lower-quartile/median/upper-quartile/maximum value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <returns>{minimum, lower-quartile, median, upper-quartile, maximum}</returns>
        public static double[] FiveSummaryPercentile(double[] data)
        {
            return ArrayStatistics.FiveNumberSummaryInplace(data);
        }

        /// <summary>
        /// Get minimum/lower-quartile/median/upper-quartile/maximum value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <returns>{minimum, lower-quartile, median, upper-quartile, maximum}</returns>
        public static float[] FiveSummaryPercentile(float[] data)
        {
            return ArrayStatistics.FiveNumberSummaryInplace(data);
        }


        /// <summary>
        /// Get minimum/lower-quartile/median/upper-quartile/maximum value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <returns>{minimum, lower-quartile, median, upper-quartile, maximum}</returns>
        public static double[] FiveSummaryPercentile(double[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return FiveSummaryPercentile(GetDirectionData(data, index, majorOrder));
        }

        /// <summary>
        /// Get minimum/lower-quartile/median/upper-quartile/maximum value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <returns>{minimum, lower-quartile, median, upper-quartile, maximum}</returns>
        public static float[] FiveSummaryPercentile(float[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return FiveSummaryPercentile(GetDirectionData(data, index, majorOrder));
        }

        /// <summary>
        /// Get the percentile value of array data in specific place.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="place">The place to calculate percentile. Invalid value from 0 to 100.</param>
        /// <returns>The percentile value of array data in specific place.</returns>
        public static double Percentile(double[] data, int place)
        {
            return ArrayStatistics.PercentileInplace(data, place);
        }

        /// <summary>
        /// Get the percentile value of array data in specific place.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="place">The place to calculate percentile. Invalid value from 0 to 100.</param>
        /// <returns>The percentile value of array data in specific place.</returns>
        public static float Percentile(float[] data, int place)
        {
            return ArrayStatistics.PercentileInplace(data, place);
        }

        /// <summary>
        /// Get the percentile value of array data in specific place.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="place">The place to calculate percentile. Invalid value from 0 to 100.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <returns>The percentile value of array data in specific place.</returns>
        public static double Percentile(double[,] data, int place, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return Percentile(GetDirectionData(data, index, majorOrder), place);
        }

        /// <summary>
        /// Get the percentile value of array data in specific place.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="place">The place to calculate percentile. Invalid value from 0 to 100.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <returns>The percentile value of array data in specific place.</returns>
        public static float Percentile(float[,] data, int place, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return Percentile(GetDirectionData(data, index, majorOrder), place);
        }

        /// <summary>
        /// Get the median value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <returns>The median value of array data.</returns>
        public static double Median(double[] data)
        {
            return ArrayStatistics.MedianInplace(data);
        }

        /// <summary>
        /// Get the median value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <returns>The median value of array data.</returns>
        public static float Median(float[] data)
        {
            return ArrayStatistics.MedianInplace(data);
        }


        /// <summary>
        /// Get the median value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <returns>The median value of array data.</returns>
        public static double Median(double[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return Median(GetDirectionData(data, index, majorOrder));
        }

        /// <summary>
        /// Get the median value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <returns>The median value of array data.</returns>
        public static float Median(float[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return Median(GetDirectionData(data, index, majorOrder));
        }

        #endregion

        #region Statistics

        /// <summary>
        /// Estimates the empirical cumulative distribution function (CDF) at x from the provided samples. Supported data type: double/float.
        /// </summary>
        /// <param name="data">The data sample sequence.</param>
        /// <param name="x">The value where to estimate the CDF at.</param>
        /// <typeparam name="TDataType">Supported data type: double/float.</typeparam>
        /// <returns>The empirical cumulative distribution function (CDF) at x.</returns>
        /// <exception cref="NotSupportedException">Supported data type: double/float.</exception>
        public static double EmpiricalCDF<TDataType>(TDataType[] data, double x)
        {
            double cdf = double.NaN;
            if (ReferenceEquals(typeof (TDataType), typeof (double)))
            {
                double[] doubleData = data as double[];
                cdf = doubleData.EmpiricalCDF(x);
            }
            else if (ReferenceEquals(typeof (TDataType), typeof (float)))
            {
                float[] floatData = data as float[];
                cdf = floatData.EmpiricalCDF((float) x);
            }
            else
            {
                throw new NotSupportedException("Unsupported data type.");
            }
            return cdf;
        }

        /// <summary>
        /// Estimates the empirical cumulative distribution function (CDF) at x from the provided samples. Supported data type: double/float.
        /// </summary>
        /// <param name="data">The data sample sequence.</param>
        /// <param name="x">The value where to estimate the CDF at.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <typeparam name="TDataType"></typeparam>
        /// <returns>The empirical cumulative distribution function (CDF) at x.</returns>
        public static double EmpiricalCDF<TDataType>(TDataType[,] data, double x, int index,
            MajorOrder majorOrder = MajorOrder.Column)
        {
            return EmpiricalCDF(GetDirectionData(data, index, majorOrder), x);
        }

        /// <summary>
        /// Get the kurtosis value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <returns>The kurtosis value of array data.</returns>
        public static double Kurtosis(IEnumerable<double> data)
        {
            return data.Kurtosis();
        }

        /// <summary>
        /// Get the kurtosis value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <returns>The kurtosis value of array data.</returns>
        public static double Kurtosis(double[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return GetDirectionData(data, index, majorOrder).Kurtosis();
        }

        /// <summary>
        /// Get the kurtosis value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <returns>The kurtosis value of array data.</returns>
        public static double Skewness(IEnumerable<double> data)
        {
            return data.Skewness();
        }

        /// <summary>
        /// Get the skewness value of array data.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <returns>The skewness value of array data.</returns>
        public static double Skewness(double[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return GetDirectionData(data, index, majorOrder).Skewness();
        }

        #endregion

        #region Covariance

        /// <summary>
        /// Calculate the standard deviation value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The standard deviation value of array data in specific place.</returns>
        /// <exception cref="NotSupportedException">Supported data type: double/float/int.</exception>
        public static double StandardDeviation<TDataType>(TDataType[] data)
        {
            double standardDeviation = double.NaN;
            if (ReferenceEquals(typeof(TDataType), typeof(double)))
            {
                double[] doubleData = data as double[];
                standardDeviation = ArrayStatistics.StandardDeviation(doubleData);
            }
            else if (ReferenceEquals(typeof(TDataType), typeof(float)))
            {
                float[] floatData = data as float[];
                standardDeviation = ArrayStatistics.StandardDeviation(floatData);
            }
            else if (ReferenceEquals(typeof(TDataType), typeof(int)))
            {
                int[] intData = data as int[];
                standardDeviation = ArrayStatistics.StandardDeviation(intData);
            }
            else
            {
                throw new NotSupportedException("Unsupported data type.");
            }
            return standardDeviation;
        }


        /// <summary>
        /// Calculate the standard deviation value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The standard deviation value of array data in specific place.</returns>
        public static double StandardDeviation<TDataType>(TDataType[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return StandardDeviation(GetDirectionData(data, index, majorOrder));
        }

        /// <summary>
        /// Calculate the variance value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The variance value of array data in specific place.</returns>
        /// <exception cref="NotSupportedException">Supported data type: double/float/int.</exception>
        public static double Variance<TDataType>(TDataType[] data)
        {
            double variance = double.NaN;
            if (ReferenceEquals(typeof (TDataType), typeof (double)))
            {
                double[] doubleData = data as double[];
                variance = ArrayStatistics.Variance(doubleData);
            }
            else if (ReferenceEquals(typeof (TDataType), typeof (float)))
            {
                float[] floatData = data as float[];
                variance = ArrayStatistics.Variance(floatData);
            }
            else if (ReferenceEquals(typeof (TDataType), typeof (int)))
            {
                int[] intData = data as int[];
                variance = ArrayStatistics.Variance(intData);
            }
            else
            {
                throw new NotSupportedException("Unsupported data type.");
            }
            return variance;
        }


        /// <summary>
        /// Calculate the variance value of array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data">The array data to calculate statistic value.</param>
        /// <param name="index">The column or row index of matrix to calculate statistic value.</param>
        /// <param name="majorOrder">Galculate matrix statistic value by column of row.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The variance value of array data in specific place.</returns>
        public static double Variance<TDataType>(TDataType[,] data, int index, MajorOrder majorOrder = MajorOrder.Column)
        {
            return Variance(GetDirectionData(data, index, majorOrder));
        }


        /// <summary>
        /// Calculate the covariance value of two array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data1">The first array data to calculate Covariance value.</param>
        /// <param name="data2">The second array data to calculate Covariance value.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The covariance value of two array data in specific place.</returns>
        /// <exception cref="NotSupportedException">Supported data type: double/float/int.</exception>
        public static double Covariance<TDataType>(TDataType[] data1, TDataType[] data2)
        {
            double covariance = double.NaN;
            if (ReferenceEquals(typeof (TDataType), typeof (double)))
            {
                double[] doubleData1 = data1 as double[];
                double[] doubleData2 = data2 as double[];
                covariance = ArrayStatistics.Covariance(doubleData1, doubleData2);
            }
            else if (ReferenceEquals(typeof (TDataType), typeof (float)))
            {
                float[] floatData1 = data1 as float[];
                float[] floatData2 = data2 as float[];
                covariance = ArrayStatistics.Covariance(floatData1, floatData2);
            }
            else if (ReferenceEquals(typeof (TDataType), typeof (int)))
            {
                int[] intData1 = data1 as int[];
                int[] intData2 = data2 as int[];
                covariance = ArrayStatistics.Covariance(intData1, intData2);
            }
            else
            {
                throw new NotSupportedException("Unsupported data type.");
            }
            return covariance;
        }


        /// <summary>
        /// Calculate the covariance value of two array data. Supported data type: double/float/int.
        /// </summary>
        /// <param name="data1">The first array data to calculate Covariance value.</param>
        /// <param name="data2">The second array data to calculate Covariance value.</param>
        /// <param name="index1">The column or row index of first matrix to calculate covariance value.</param>
        /// <param name="index2">The column or row index of second matrix to calculate covariance value.</param>
        /// <param name="direction1">Galculate first matrix covariance by column of row.</param>
        /// <param name="direction2">Galculate second matrix covariance by column of row.</param>
        /// <typeparam name="TDataType">Supported data type: double/float/int.</typeparam>
        /// <returns>The covariance value of two array data in specific place.</returns>
        public static double Covariance<TDataType>(TDataType[,] data1, TDataType[,] data2, int index1, int index2, 
            MajorOrder direction1 = MajorOrder.Column, MajorOrder direction2 = MajorOrder.Column)
        {
            return Covariance(GetDirectionData(data1, index1, direction1), GetDirectionData(data2, index2, direction2));
        }

        #endregion

        private static TDataType[] GetDirectionData<TDataType>(TDataType[,] data, int index, MajorOrder majorOrder)
        {
            TDataType[] directionData = null;
            switch (majorOrder)
            {
                case MajorOrder.Column:
                    directionData = new TDataType[data.GetLength(0)];
                    for (int i = 0; i < data.GetLength(0); i++)
                    {
                        directionData[index] = data[i, index];
                    }
                    break;
                case MajorOrder.Row:
                    directionData = new TDataType[data.GetLength(1)];
                    int rowSize = data.GetLength(1)*Marshal.SizeOf(typeof(TDataType));
                    Buffer.BlockCopy(data, index*rowSize, directionData, 0, rowSize);
                    break;
            }
            return directionData;
        }

    }
}
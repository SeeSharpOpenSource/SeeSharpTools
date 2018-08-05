using System;

namespace SeeSharpTools.JY.Statistics.IPP
{
    unsafe internal class Statistics
    {
        public static double Sum(double[] src)
        {
            double[] sum = new double[1];
            fixed (double* pSrc = src, pSum = sum)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsSum_64f(pSrc, src.Length, pSum);
                }
                else
                {
                    ippNativeX86.ippsSum_64f(pSrc, src.Length, pSum);
                }
            };
            return sum[0];
        }

        public static double FindMaxValue(double[] src)
        {
            double[] maxValue = new double[1];
            int[] maxIndex = new int[1];
            fixed (double* pSrc = src, pMax = maxValue)
            {
                fixed (int* pMaxIdx = maxIndex)
                {
                    if (Environment.Is64BitProcess)
                    {
                        ippNativeX64.ippsMaxIndx_64f(pSrc, src.Length, pMax, pMaxIdx);
                    }
                    else
                    {
                        ippNativeX86.ippsMaxIndx_64f(pSrc, src.Length, pMax, pMaxIdx);
                    }
                };
            };
            return maxValue[0];
        }

        public static int FindMaxIndex(double[] src)
        {
            double[] maxValue = new double[1];
            int[] maxIndex = new int[1];
            fixed (double* pSrc = src, pMax = maxValue)
            {
                fixed (int* pMaxIdx = maxIndex)
                {
                    if (Environment.Is64BitProcess)
                    {
                        ippNativeX64.ippsMaxIndx_64f(pSrc, src.Length, pMax, pMaxIdx);
                    }
                    else
                    {
                        ippNativeX86.ippsMaxIndx_64f(pSrc, src.Length, pMax, pMaxIdx);
                    }
                };
            };
            return maxIndex[0];
        }

        public static double FindMinValue(double[] src)
        {
            double[] minValue = new double[1];
            int[] minIndex = new int[1];
            fixed (double* pSrc = src, pMin = minValue)
            {
                fixed (int* pMinIdx = minIndex)
                {
                    if (Environment.Is64BitProcess)
                    {
                        ippNativeX64.ippsMinIndx_64f(pSrc, src.Length, pMin, pMinIdx);
                    }
                    else
                    {
                        ippNativeX86.ippsMinIndx_64f(pSrc, src.Length, pMin, pMinIdx);
                    }
                }
            };
            return minValue[0];
        }

        public static int FindMinIndex(double[] src)
        {
            double[] minValue = new double[1];
            int[] minIndex = new int[1];
            fixed (double* pSrc = src, pMin = minValue)
            {
                fixed (int* pMinIdx = minIndex)
                {
                    if (Environment.Is64BitProcess)
                    {
                        ippNativeX64.ippsMinIndx_64f(pSrc, src.Length, pMin, pMinIdx);
                    }
                    else
                    {
                        ippNativeX86.ippsMinIndx_64f(pSrc, src.Length, pMin, pMinIdx);
                    }
                };
            };
            return minIndex[0];
        }

        public static void FindMaxMin(double[] src, ref double max, ref int indexMax, ref double min, ref int indexMin)
        {
            double[] minValue = new double[1];
            int[] minIndex = new int[1];
            double[] maxValue = new double[1];
            int[] maxIndex = new int[1];

            fixed (double* pSrc = src, pMin = minValue, pMax = maxValue)
            {
                fixed (int* pMinIdx = minIndex, pMaxIdx = maxIndex)
                {
                    if (Environment.Is64BitProcess)
                    {
                        ippNativeX64.ippsMinMaxIndx_64f(pSrc, src.Length, pMin, pMinIdx, pMax, pMaxIdx);
                    }
                    else
                    {
                        ippNativeX86.ippsMinMaxIndx_64f(pSrc, src.Length, pMin, pMinIdx, pMax, pMaxIdx);
                    }
                };
            };
            indexMax = maxIndex[0];
            indexMin = minIndex[0];
            max = maxValue[0];
            min = minValue[0];
        }

        public static double Mean(double[] src)
        {
            double[] mean = new double[1];
            fixed (double* pSrc = src, pMean = mean)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsMean_64f(pSrc, src.Length, pMean);
                }
                else
                {
                    ippNativeX86.ippsMean_64f(pSrc, src.Length, pMean);
                }
            };
            return mean[0];
        }

        public static double StdDev(double[] src)
        {
            double[] stdDev = new double[1];
            fixed (double* pSrc = src, pStdDev = stdDev)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsStdDev_64f(pSrc, src.Length, pStdDev);
                }
                else
                {
                    ippNativeX86.ippsStdDev_64f(pSrc, src.Length, pStdDev);
                }
            };
            return stdDev[0];
        }

        public static void MeanStdDev(double[] src, ref double stdDev, ref double mean)
        {
            double[] stdDevValue = new double[1];
            double[] meanValue = new double[1];

            fixed (double* pSrc = src, pStdDev = stdDevValue, pMean = meanValue)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsMeanStdDev_64f(pSrc, src.Length, pMean, pStdDev);
                }
                else
                {
                    ippNativeX86.ippsMeanStdDev_64f(pSrc, src.Length, pMean, pStdDev);
                }
            };
            stdDev = stdDevValue[0];
            mean = meanValue[0];
        }

        public static double Dot(double[] src1, double[] src2)
        {
            double[] dotValue = new double[1];
            fixed (double* pSrc1 = src1, pSrc2 = src2, pDp = dotValue)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsDotProd_64f(pSrc1, pSrc2, src1.Length, pDp);
                }
                else
                {
                    ippNativeX86.ippsDotProd_64f(pSrc1, pSrc2, src1.Length, pDp);
                }
            };
            return dotValue[0];
        }

        public static int[] CountInRange(int[] src, int rangeHigh, int rangeLow)
        {
            int[] result = new int[] { };
            fixed (int* pSrc = src, pCounts = result)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsCountInRange_32s(pSrc, src.Length, pCounts, rangeLow, rangeHigh);
                }
                else
                {
                    ippNativeX64.ippsCountInRange_32s(pSrc, src.Length, pCounts, rangeLow, rangeHigh);
                }
            };
            return result;
        }
    }
}
using System;

namespace SeeSharpTools.JY.Statistics.IPP
{
    unsafe internal class Power
    {
        public static void Inverse(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsInv_64f_A53(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsInv_64f_A53(pSrc, pDst, src.Length);
                }
            };
        }

        public static void Sqrt(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsSqrt_64f_A53(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsSqrt_64f_A53(pSrc, pDst, src.Length);
                }
            };
        }

        public static void Powx(double[] src, ref double[] dest, double value)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsPowx_64f_A53(pSrc, value, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsPowx_64f_A53(pSrc, value, pDst, src.Length);
                }
            };
        }
    }
}
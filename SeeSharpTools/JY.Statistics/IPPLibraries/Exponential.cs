using System;

namespace SeeSharpTools.JY.Statistics.IPP
{
    unsafe internal class Exponential
    {
        public static void Exp(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsExp_64f_A53(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsExp_64f_A53(pSrc, pDst, src.Length);
                }
            };
        }

        public static void Ln(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsLn_64f_A53(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsLn_64f_A53(pSrc, pDst, src.Length);
                }
            };
        }

        public static void Log10(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsLog10_64f_A53(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsLog10_64f_A53(pSrc, pDst, src.Length);
                }
            };
        }
    }
}
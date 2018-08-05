using System;

namespace SeeSharpTools.JY.Statistics.IPP
{
    unsafe internal class ArrayOperation
    {
        public static void Initialize(ref double[] dest, double value)
        {
            fixed (double* pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsSet_64f(value, pDst, dest.Length);
                }
                else
                {
                    ippNativeX86.ippsSet_64f(value, pDst, dest.Length);
                }
            };
        }

        public static void Copy(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsCopy_64f(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsCopy_64f(pSrc, pDst, src.Length);
                }
            };
        }

        public static void ReplaceNaN(double[] src, double value)
        {
            fixed (double* pSrcDst = src)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsReplaceNAN_64f_I(pSrcDst, src.Length, value);
                }
                else
                {
                    ippNativeX86.ippsReplaceNAN_64f_I(pSrcDst, src.Length, value);
                }
            };
        }
    }
}
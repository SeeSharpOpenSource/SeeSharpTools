using System;

namespace SeeSharpTools.JY.Statistics.IPP
{
    unsafe internal class Rounding
    {
        public static void Floor(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsFloor_64f(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsFloor_64f(pSrc, pDst, src.Length);
                }
            };
        }

        public static void Fraction(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsFrac_64f(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsFrac_64f(pSrc, pDst, src.Length);
                }
            };
        }

        public static void Ceiling(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsCeil_64f(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsCeil_64f(pSrc, pDst, src.Length);
                }
            };
        }

        public static void Truncate(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsTrunc_64f(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsTrunc_64f(pSrc, pDst, src.Length);
                }
            };
        }

        public static void Round(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsRound_64f(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsRound_64f(pSrc, pDst, src.Length);
                }
            };
        }

        public static void NearByInt(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsNearbyInt_64f(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsNearbyInt_64f(pSrc, pDst, src.Length);
                }
            };
        }
    }
}
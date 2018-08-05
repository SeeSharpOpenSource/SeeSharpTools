using System;

namespace SeeSharpTools.JY.Statistics.IPP
{
    unsafe internal class Arithmetics
    {
        public static void AddScalar(double[] src, ref double[] dest, double value)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsAddC_64f(pSrc, value, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsAddC_64f(pSrc, value, pDst, src.Length);
                }
            };
        }

        public static void AddScalar_InPlace(ref double[] srcDest, double value)
        {
            fixed (double* pSrcDst = srcDest)

                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsAddC_64f_I(value, pSrcDst, srcDest.Length);
                }
                else
                {
                    ippNativeX86.ippsAddC_64f_I(value, pSrcDst, srcDest.Length);
                }
        }

        public static void Add(double[] src1, double[] src2, ref double[] dest)
        {
            fixed (double* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsAdd_64f(pSrc1, pSrc2, pDst, src1.Length);
                }
                else
                {
                    ippNativeX86.ippsAdd_64f(pSrc1, pSrc2, pDst, src1.Length);
                }
            };
        }

        public static void Add_InPlace(double[] src1, ref double[] src2Dest)
        {
            fixed (double* pSrc1 = src1, pSrcDst = src2Dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsAdd_64f_I(pSrc1, pSrcDst, src2Dest.Length);
                }
                else
                {
                    ippNativeX86.ippsAdd_64f_I(pSrc1, pSrcDst, src2Dest.Length);
                }
            };
        }

        public static void MultiplyScalar(double[] src, ref double[] dest, double value)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsMulC_64f(pSrc, value, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsMulC_64f(pSrc, value, pDst, src.Length);
                }
            };
        }

        public static void MultiplyScalar_InPlace(ref double[] srcDest, double value)
        {
            fixed (double* pSrcDst = srcDest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsMulC_64f_I(value, pSrcDst, srcDest.Length);
                }
                else
                {
                    ippNativeX86.ippsMulC_64f_I(value, pSrcDst, srcDest.Length);
                }
            };
        }

        public static void Multiply(double[] src1, double[] src2, ref double[] dest)
        {
            fixed (double* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsMul_64f(pSrc1, pSrc2, pDst, src1.Length);
                }
                else
                {
                    ippNativeX86.ippsMul_64f(pSrc1, pSrc2, pDst, src1.Length);
                }
            };
        }

        public static void Multiply_InPlace(double[] src1, ref double[] src2Dest)
        {
            fixed (double* pSrc1 = src1, pSrcDst = src2Dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsMul_64f_I(pSrc1, pSrcDst, src2Dest.Length);
                }
                else
                {
                    ippNativeX86.ippsMul_64f_I(pSrc1, pSrcDst, src2Dest.Length);
                }
            };
        }

        public static void SubScalar(double[] src, ref double[] dest, double value)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsSubC_64f(pSrc, value, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsSubC_64f(pSrc, value, pDst, src.Length);
                }
            };
        }

        public static void SubScalar_InPlace(ref double[] srcDest, double value)
        {
            fixed (double* pSrcDst = srcDest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsSubC_64f_I(value, pSrcDst, srcDest.Length);
                }
                else
                {
                    ippNativeX86.ippsSubC_64f_I(value, pSrcDst, srcDest.Length);
                }
            };
        }

        public static void Sub(double[] src1, double[] src2, ref double[] dest)
        {
            fixed (double* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsSub_64f(pSrc1, pSrc2, pDst, src1.Length);
                }
                else
                {
                    ippNativeX86.ippsSub_64f(pSrc1, pSrc2, pDst, src1.Length);
                }
            };
        }

        public static void Sub_InPlace(double[] src1, ref double[] src2Dest)
        {
            fixed (double* pSrc1 = src1, pSrcDst = src2Dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsSub_64f_I(pSrc1, pSrcDst, src2Dest.Length);
                }
                else
                {
                    ippNativeX86.ippsSub_64f_I(pSrc1, pSrcDst, src2Dest.Length);
                }
            };
        }

        public static void DivideScalar(double[] src, ref double[] dest, double value)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsDivC_64f(pSrc, value, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsDivC_64f(pSrc, value, pDst, src.Length);
                }
            };
        }

        public static void DivideScalar_InPlace(ref double[] srcDest, double value)
        {
            fixed (double* pSrcDst = srcDest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsDivC_64f_I(value, pSrcDst, srcDest.Length);
                }
                else
                {
                    ippNativeX86.ippsDivC_64f_I(value, pSrcDst, srcDest.Length);
                }
            };
        }

        public static void Divide(double[] src1, double[] src2, ref double[] dest)
        {
            fixed (double* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsDiv_64f(pSrc1, pSrc2, pDst, src1.Length);
                }
                else
                {
                    ippNativeX86.ippsDiv_64f(pSrc1, pSrc2, pDst, src1.Length);
                }
            };
        }

        public static void Divide_InPlace(double[] src1, ref double[] src2Dest)
        {
            fixed (double* pSrc1 = src1, pSrcDst = src2Dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsDiv_64f_I(pSrc1, pSrcDst, src2Dest.Length);
                }
                else
                {
                    ippNativeX86.ippsDiv_64f_I(pSrc1, pSrcDst, src2Dest.Length);
                }
            };
        }

        public static void Abs(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsAbs_64f(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsAbs_64f(pSrc, pDst, src.Length);
                }
            };
        }

        public static void Sqr(double[] src, ref double[] dest)
        {
            fixed (double* pSrc = src, pDst = dest)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsSqr_64f(pSrc, pDst, src.Length);
                }
                else
                {
                    ippNativeX86.ippsSqr_64f(pSrc, pDst, src.Length);
                }
            };
        }
    }
}
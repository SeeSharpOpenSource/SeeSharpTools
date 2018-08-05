using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    [SuppressUnmanagedCodeSecurity]
    internal static class VMLNative
    {
        /** CBLAS native LAPACKE_zgesv declaration */

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdMul(int n, double[] a, double[] b, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdMul(int n, double[] a, IntPtr b, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsMul(int n, float[] a, float[] b, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzMul(int n, Complex[] a, Complex[] b, Complex[] y);

        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcMul(int n, Complex32[] a, Complex32[] b, Complex32[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzAbs(int n, Complex[] a, double[] r);

        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcAbs(int n, Complex32[] a, float[] r);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAbs(int n, double[] a, double[] r);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsAbs(int n, float[] a, float[] r);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzAdd(int n, Complex[] a, Complex[] b, Complex[] y);

        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcAdd(int n, Complex32[] a, Complex32[] b, Complex32[] r);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAdd(int n, double[] a, double[] b, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsAdd(int n, float[] a, float[] b, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdLog10(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsLog10(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzLog10(int n, Complex[] a, Complex[] y);

        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcLog10(int n, Complex32[] a, Complex32[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSqr(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsSqr(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzSqr(int n, Complex[] a, Complex[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSqrt(int n, double[] a, double[] y);

        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcSqr(int n, Complex32[] a, Complex32[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSin(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdCos(int n, double[] a, double[] y);
    }
}

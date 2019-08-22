using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    /** CBLAS native declarations */

    [SuppressUnmanagedCodeSecurity]
    internal static class CBLASNative
    {
        /** CBLAS native LAPACKE_zgesv declaration */

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dcopy(int n, double[] X, int incX, double[] Y, int incY);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dscal(int n, double alpha, double[] X, int incX);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dscal(int n, double alpha, IntPtr X, int incX);
    }
}

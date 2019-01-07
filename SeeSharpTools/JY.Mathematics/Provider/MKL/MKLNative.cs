using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    #region BLAS

    [SuppressUnmanagedCodeSecurity]
    internal class CBLASNative
    {
        /** CBLAS native LAPACKE_zgesv declaration */

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dcopy(int n, double[] X, int incX, double[] Y, int incY);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dscal(int n, double alpha, double[] X, int incX);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern double cblas_ddoti(int nz, double[] x, int[] indx, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern float cblas_sdoti(int nz, float[] x, int[] indx, float[] y);
    }

    #endregion BLAS

    #region VML

    [SuppressUnmanagedCodeSecurity]
    internal class VMLNative
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

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsSqrt(int n, float[] a, float[] y);

        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcSqr(int n, Complex32[] a, Complex32[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSin(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsSin(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdCos(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsCos(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAcos(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsAcos(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAsin(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsAsin(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAtan(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsAtan(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdTan(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsTan(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdExp(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsExp(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdLn(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsLn(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdPowx(int n, double[] a, double b, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsPowx(int n, float[] a, float b, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSub(int n, double[] a, double[] b, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsSub(int n, float[] a, float[] b, float[] y);
    }

    #endregion VML

    #region VSL

    //Sumary Statistics

    [SuppressUnmanagedCodeSecurity]
    internal class VSLNative
    {
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public unsafe static extern int vsldSSNewTask(ref int task, int* p, int* n, int* xstorage, double* x, int w = 0, int indices = 0);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsldSSEditTask(int task, VSLSS_EditTaskParam parameter, double[] par_addr);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public unsafe static extern int vsldSSEditMoments(int task, double[] mean, double[] r2m, double[] r3m, double[] r4m, double[] c2m, double[] c3m, double[] c4m);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public unsafe static extern int vsldSSEditCovCor(int task, ref double mean, ref double cov, VSLSS_MATRIX_STORAGE cov_storage, ref double cor, VSLSS_MATRIX_STORAGE cor_storage);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public unsafe static extern int vsldSSEditCP(int task, ref double mean, ref double sum, double[] cp, VSLSS_MATRIX_STORAGE cp_storage);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsldSSCompute(int task, VSLSS_ComputeRoutine estimates, VSLSS_Method method);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vslSSDeleteTask(ref int task);

        public enum VSLSS_Method
        {
            VSL_SS_METHOD_FAST = 0x00000001,
            VSL_SS_METHOD_1PASS = 0x00000002,
            VSL_SS_METHOD_FAST_USER_MEAN = 0x00000100,
            VSL_SS_METHOD_CP_TO_COVCOR = 0x00000200,
            VSL_SS_METHOD_SUM_TO_MOM = 0x00000400,
        }

        public enum VSLSS_Storage
        {
            VSL_SS_MATRIX_STORAGE_ROWS = 0x00010000,
            VSL_SS_MATRIX_STORAGE_COLS = 0x00020000,
        }

        public enum VSLSS_EditTaskParam
        {
            VSL_SS_ED_DIMEN = 1,
            VSL_SS_ED_OBSERV_N = 2,
            VSL_SS_ED_OBSERV = 3,
            VSL_SS_ED_OBSERV_STORAGE = 4,
            VSL_SS_ED_INDC = 5,
            VSL_SS_ED_WEIGHTS = 6,
            VSL_SS_ED_MEAN = 7,
            VSL_SS_ED_2R_MOM = 8,
            VSL_SS_ED_3R_MOM = 9,
            VSL_SS_ED_4R_MOM = 10,
            VSL_SS_ED_2C_MOM = 11,
            VSL_SS_ED_3C_MOM = 12,
            VSL_SS_ED_4C_MOM = 13,
            VSL_SS_ED_SUM = 67,
            VSL_SS_ED_2R_SUM = 68,
            VSL_SS_ED_3R_SUM = 69,
            VSL_SS_ED_4R_SUM = 70,
            VSL_SS_ED_2C_SUM = 71,
            VSL_SS_ED_3C_SUM = 72,
            VSL_SS_ED_4C_SUM = 73,
            VSL_SS_ED_KURTOSIS = 14,
            VSL_SS_ED_SKEWNESS = 15,
            VSL_SS_ED_MIN = 16,
            VSL_SS_ED_MAX = 17,
            VSL_SS_ED_VARIATION = 18,
            VSL_SS_ED_COV = 19,
            VSL_SS_ED_COV_STORAGE = 20,
            VSL_SS_ED_COR = 21,
            VSL_SS_ED_COR_STORAGE = 22,
            VSL_SS_ED_CP = 74,
            VSL_SS_ED_CP_STORAGE = 75,
            VSL_SS_ED_ACCUM_WEIGHT = 23,
            VSL_SS_ED_QUANT_ORDER_N = 24,
            VSL_SS_ED_QUANT_ORDER = 25,
            VSL_SS_ED_QUANT_QUANTILES = 26,
            VSL_SS_ED_ORDER_STATS = 27,
            VSL_SS_ED_GROUP_INDC = 28,
            VSL_SS_ED_POOLED_COV_STORAGE = 29,
            VSL_SS_ED_POOLED_MEAN = 30,
            VSL_SS_ED_POOLED_COV = 31,
            VSL_SS_ED_GROUP_COV_INDC = 32,
            VSL_SS_ED_REQ_GROUP_INDC = 32,
            VSL_SS_ED_GROUP_MEAN = 33,
            VSL_SS_ED_GROUP_COV_STORAGE = 34,
            VSL_SS_ED_GROUP_COV = 35,
            VSL_SS_ED_ROBUST_COV_STORAGE = 36,
            VSL_SS_ED_ROBUST_COV_PARAMS_N = 37,
            VSL_SS_ED_ROBUST_COV_PARAMS = 38,
            VSL_SS_ED_ROBUST_MEAN = 39,
            VSL_SS_ED_ROBUST_COV = 40,
            VSL_SS_ED_OUTLIERS_PARAMS_N = 41,
            VSL_SS_ED_OUTLIERS_PARAMS = 42,
            VSL_SS_ED_OUTLIERS_WEIGHT = 43,
            VSL_SS_ED_ORDER_STATS_STORAGE = 44,
            VSL_SS_ED_PARTIAL_COV_IDX = 45,
            VSL_SS_ED_PARTIAL_COV = 46,
            VSL_SS_ED_PARTIAL_COV_STORAGE = 47,
            VSL_SS_ED_PARTIAL_COR = 48,
            VSL_SS_ED_PARTIAL_COR_STORAGE = 49,
            VSL_SS_ED_MI_PARAMS_N = 50,
            VSL_SS_ED_MI_PARAMS = 51,
            VSL_SS_ED_MI_INIT_ESTIMATES_N = 52,
            VSL_SS_ED_MI_INIT_ESTIMATES = 53,
            VSL_SS_ED_MI_SIMUL_VALS_N = 54,
            VSL_SS_ED_MI_SIMUL_VALS = 55,
            VSL_SS_ED_MI_ESTIMATES_N = 56,
            VSL_SS_ED_MI_ESTIMATES = 57,
            VSL_SS_ED_MI_PRIOR_N = 58,
            VSL_SS_ED_MI_PRIOR = 59,
            VSL_SS_ED_PARAMTR_COR = 60,
            VSL_SS_ED_PARAMTR_COR_STORAGE = 61,
            VSL_SS_ED_STREAM_QUANT_PARAMS_N = 62,
            VSL_SS_ED_STREAM_QUANT_PARAMS = 63,
            VSL_SS_ED_STREAM_QUANT_ORDER_N = 64,
            VSL_SS_ED_STREAM_QUANT_ORDER = 65,
            VSL_SS_ED_STREAM_QUANT_QUANTILES = 66,
            VSL_SS_ED_MDAD = 76,
            VSL_SS_ED_MNAD = 77,
            VSL_SS_ED_SORTED_OBSERV = 78,
            VSL_SS_ED_SORTED_OBSERV_STORAGE = 79
        }

        public enum VSLSS_ComputeRoutine : ulong
        {
            VSL_SS_MEAN = 0x0000000000000001,
            VSL_SS_2R_MOM = 0x0000000000000002,
            VSL_SS_3R_MOM = 0x0000000000000004,
            VSL_SS_4R_MOM = 0x0000000000000008,
            VSL_SS_2C_MOM = 0x0000000000000010,
            VSL_SS_3C_MOM = 0x0000000000000020,
            VSL_SS_4C_MOM = 0x0000000000000040,
            VSL_SS_SUM = 0x0000000002000000,
            VSL_SS_2R_SUM = 0x0000000004000000,
            VSL_SS_3R_SUM = 0x0000000008000000,
            VSL_SS_4R_SUM = 0x0000000010000000,
            VSL_SS_2C_SUM = 0x0000000020000000,
            VSL_SS_3C_SUM = 0x0000000040000000,
            VSL_SS_4C_SUM = 0x0000000080000000,
            VSL_SS_KURTOSIS = 0x0000000000000080,
            VSL_SS_SKEWNESS = 0x0000000000000100,
            VSL_SS_VARIATION = 0x0000000000000200,
            VSL_SS_MIN = 0x0000000000000400,
            VSL_SS_MAX = 0x0000000000000800,
            VSL_SS_COV = 0x0000000000001000,
            VSL_SS_COR = 0x0000000000002000,
            VSL_SS_CP = 0x0000000100000000,
            VSL_SS_POOLED_COV = 0x0000000000004000,
            VSL_SS_GROUP_COV = 0x0000000000008000,
            VSL_SS_POOLED_MEAN = 0x0000000800000000,
            VSL_SS_GROUP_MEAN = 0x0000001000000000,
            VSL_SS_QUANTS = 0x0000000000010000,
            VSL_SS_ORDER_STATS = 0x0000000000020000,
            VSL_SS_SORTED_OBSERV = 0x0000008000000000,
            VSL_SS_ROBUST_COV = 0x0000000000040000,
            VSL_SS_OUTLIERS = 0x0000000000080000,
            VSL_SS_PARTIAL_COV = 0x0000000000100000,
            VSL_SS_PARTIAL_COR = 0x0000000000200000,
            VSL_SS_MISSING_VALS = 0x0000000000400000,
            VSL_SS_PARAMTR_COR = 0x0000000000800000,
            VSL_SS_STREAM_QUANTS = 0x0000000001000000,
            VSL_SS_MDAD = 0x0000000200000000,
            VSL_SS_MNAD = 0x0000000400000000
        }

        public enum VSLSS_MATRIX_STORAGE
        {
            VSL_SS_MATRIX_STORAGE_FULL = 0x00000000,
            VSL_SS_MATRIX_STORAGE_L_PACKED = 0x00000001,
            VSL_SS_MATRIX_STORAGE_U_PACKED = 0x00000002,
        }
    }

    #endregion VSL
}
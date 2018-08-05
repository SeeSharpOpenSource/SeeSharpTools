using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    internal static class DFTI
    {
        /** Constants for DFTI, file "mkl_dfti.h" */
        /** DFTI configuration parameters */
        public static int PRECISION = 3;
        public static int FORWARD_DOMAIN = 0;
        public static int DIMENSION = 1;
        public static int LENGTHS = 2;
        public static int NUMBER_OF_TRANSFORMS = 7;
        public static int FORWARD_SCALE = 4;
        public static int BACKWARD_SCALE = 5;
        public static int PLACEMENT = 11;
        public static int COMPLEX_STORAGE = 8;
        public static int REAL_STORAGE = 9;
        public static int CONJUGATE_EVEN_STORAGE = 10;
        public static int DESCRIPTOR_NAME = 20;
        public static int PACKED_FORMAT = 21;
        public static int NUMBER_OF_USER_THREADS = 26;
        public static int INPUT_DISTANCE = 14;
        public static int OUTPUT_DISTANCE = 15;
        public static int INPUT_STRIDES = 12;
        public static int OUTPUT_STRIDES = 13;
        public static int ORDERING = 18;
        public static int TRANSPOSE = 19;
        public static int COMMIT_STATUS = 22;
        public static int VERSION = 23;
        /** DFTI configuration values */
        public static int SINGLE = 35;
        public static int DOUBLE = 36;
        public static int COMPLEX = 32;
        public static int REAL = 33;
        public static int INPLACE = 43;
        public static int NOT_INPLACE = 44;
        public static int COMPLEX_COMPLEX = 39;
        public static int REAL_REAL = 42;
        public static int COMPLEX_REAL = 40;
        public static int REAL_COMPLEX = 41;
        public static int COMMITTED = 30;
        public static int UNCOMMITTED = 31;
        public static int ORDERED = 48;
        public static int BACKWARD_SCRAMBLED = 49;
        public static int NONE = 53;
        public static int CCS_FORMAT = 54;
        public static int PACK_FORMAT = 55;
        public static int PERM_FORMAT = 56;
        public static int CCE_FORMAT = 57;
        public static int VERSION_LENGTH = 198;
        public static int MAX_NAME_LENGTH = 10;
        public static int MAX_MESSAGE_LENGTH = 40;
        /** DFTI predefined error classes */
        public static int NO_ERROR = 0;
        public static int MEMORY_ERROR = 1;
        public static int INVALID_CONFIGURATION = 2;
        public static int INCONSISTENT_CONFIGURATION = 3;
        public static int NUMBER_OF_THREADS_ERROR = 8;
        public static int MULTITHREADED_ERROR = 4;
        public static int BAD_DESCRIPTOR = 5;
        public static int UNIMPLEMENTED = 6;
        public static int MKL_INTERNAL_ERROR = 7;
        public static int LENGTH_EXCEEDS_INT32 = 9;

        /// <summary>
        /// DFTI DftiCreateDescriptor wrapper
        /// </summary>
        /// <param name="desc">return desc handle</param>
        /// <param name="precision">precision</param>
        /// <param name="domain">domain</param>
        /// <param name="dimention">dimention</param>
        /// <param name="length">length</param>
        /// <returns></returns>
        public static int DftiCreateDescriptor(ref IntPtr desc,
            int precision, int domain, int dimention, int length)
        {
            return DFTINative.DftiCreateDescriptor(ref desc,
                precision, domain, dimention, length);
        }

        /// <summary>
        /// DFTI DftiFreeDescriptor wrapper
        /// </summary>
        /// <param name="desc">desc handle</param>
        /// <returns></returns>
        public static int DftiFreeDescriptor(ref IntPtr desc)
        {
            return DFTINative.DftiFreeDescriptor(ref desc);
        }

        /// <summary>
        /// DFTI DftiSetValue wrapper
        /// </summary>
        /// <param name="desc">desc handle</param>
        /// <param name="configParam">config_param</param>
        /// <param name="configVal">config_val</param>
        /// <returns></returns>
        public static int DftiSetValue(IntPtr desc,
            int configParam, int configVal)
        {
            return DFTINative.DftiSetValue(desc,
                configParam, configVal);
        }

        /// <summary>
        /// DFTI DftiSetValue wrapper
        /// </summary>
        /// <param name="desc">desc handle</param>
        /// <param name="configParam">config_param</param>
        /// <param name="configVal">config_val</param>
        /// <returns></returns>
        public static int DftiSetValue(IntPtr desc,
            int configParam, double configVal)
        {
            return DFTINative.DftiSetValue(desc,
                configParam, configVal);
        }

        /// <summary>
        /// DFTI DftiGetValue wrapper 
        /// </summary>
        /// <param name="desc">desc handle</param>
        /// <param name="configParam">config_param</param>
        /// <param name="configVal">config_val</param>
        /// <returns></returns>
        public static int DftiGetValue(IntPtr desc,
            int configParam, ref double configVal)
        {
            return DFTINative.DftiGetValue(desc,
                configParam, ref configVal);
        }

        /// <summary>
        /// DFTI DftiCommitDescriptor wrapper
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static int DftiCommitDescriptor(IntPtr desc)
        {
            return DFTINative.DftiCommitDescriptor(desc);
        }

        /// <summary>
        /// DFTI DftiComputeForward wrapper
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="x_in"></param>
        /// <param name="x_out"></param>
        /// <returns></returns>
        public static int DftiComputeForward(IntPtr desc,
            [In] double[] x_in, [Out] double[] x_out)
        {
            return DFTINative.DftiComputeForward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeForward wrapper
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="x_in"></param>
        /// <param name="x_out"></param>
        /// <returns></returns>
        public static int DftiComputeForward(IntPtr desc,
            [In] IntPtr x_in, [Out] IntPtr x_out)
        {
            return DFTINative.DftiComputeForward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeForward wrapper
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="x_in"></param>
        /// <param name="x_out"></param>
        /// <returns></returns>
        public static int DftiComputeForward(IntPtr desc,
            [In] double[] x_in, [Out] IntPtr x_out)
        {
            return DFTINative.DftiComputeForward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeForward wrapper
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="x_in"></param>
        /// <param name="x_out"></param>
        /// <returns></returns>
        public static int DftiComputeForward(IntPtr desc,
            [In] IntPtr x_in, [Out] double[] x_out)
        {
            return DFTINative.DftiComputeForward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeBackward wrapper
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="x_in"></param>
        /// <param name="x_out"></param>
        /// <returns></returns>
        public static int DftiComputeBackward(IntPtr desc,
            [In] double[] x_in, [Out] double[] x_out)
        {
            return DFTINative.DftiComputeBackward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeBackward wrapper
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="x_in"></param>
        /// <param name="x_out"></param>
        /// <returns></returns>
        public static int DftiComputeBackward(IntPtr desc,
            [In] IntPtr x_in, [Out] IntPtr x_out)
        {
            return DFTINative.DftiComputeBackward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeBackward wrapper
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="x_in"></param>
        /// <param name="x_out"></param>
        /// <returns></returns>
        public static int DftiComputeBackward(IntPtr desc,
            [In] double[] x_in, [Out] IntPtr x_out)
        {
            return DFTINative.DftiComputeBackward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeBackward wrapper
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="x_in"></param>
        /// <param name="x_out"></param>
        /// <returns></returns>
        public static int DftiComputeBackward(IntPtr desc,
            [In] IntPtr x_in, [Out] double[] x_out)
        {
            return DFTINative.DftiComputeBackward(desc, x_in, x_out);
        }
    }
    
    /// <summary>
    /// DFTI native declarations
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class DFTINative
    {
        /** DFTI native DftiCreateDescriptor declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiCreateDescriptor(ref IntPtr desc,
            int precision, int domain, int dimention, int length);
        /** DFTI native DftiCommitDescriptor declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiCommitDescriptor(IntPtr desc);
        /** DFTI native DftiFreeDescriptor declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiFreeDescriptor(ref IntPtr desc);
        /** DFTI native DftiSetValue declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiSetValue(IntPtr desc,
            int config_param, int config_val);
        /** DFTI native DftiSetValue declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiSetValue(IntPtr desc,
            int config_param, double config_val);
        /** DFTI native DftiGetValue declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiGetValue(IntPtr desc,
            int config_param, ref double config_val);
        /** DFTI native DftiComputeForward declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeForward(IntPtr desc,
            [In] double[] x_in, [Out] double[] x_out);

        /** DFTI native DftiComputeForward declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeForward(IntPtr desc,
            [In] IntPtr x_in, [Out] IntPtr x_out);

        /** DFTI native DftiComputeForward declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeForward(IntPtr desc,
            [In] double[] x_in, [Out] IntPtr x_out);

        /** DFTI native DftiComputeForward declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeForward(IntPtr desc,
            [In] IntPtr x_in, [Out] double[] x_out);

        /** DFTI native DftiComputeBackward declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeBackward(IntPtr desc,
            [In] double[] x_in, [Out] double[] x_out);

        /** DFTI native DftiComputeBackward declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeBackward(IntPtr desc,
            [In] IntPtr x_in, [Out] IntPtr x_out);

        /** DFTI native DftiComputeBackward declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeBackward(IntPtr desc,
            [In] double[] x_in, [Out] IntPtr x_out);

        /** DFTI native DftiComputeBackward declaration */
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl,
             ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeBackward(IntPtr desc,
            [In] IntPtr x_in, [Out] double[] x_out);
    }
}

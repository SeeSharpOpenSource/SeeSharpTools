using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    public static class Dfti
    {
        private const string mklCore = @"C:\Program Files (x86)\IntelSWTools\compilers_and_libraries_2017.1.143\windows\redist\ia32_win\mkl\mkl_rt.dll";

        //[SuppressUnmanagedCodeSecurity]
        //[DllImport(mklCore, CallingConvention = CallingConvention.StdCall, EntryPoint = "DftiCreateDescriptor_d_1d")]
        //public static extern int DftiCreateDescriptor(ref IntPtr descHandle, DftiConfigValue precision,
        //                                               DftiConfigValue domain, int dimension, int sizes);
        //[DllImport("mkl.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int DftiCreateDescriptor(ref IntPtr desc,
        //    int precision, int domain, int dimention, int length);

        [DllImport(mklCore, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int DftiCreateDescriptor(ref IntPtr desc,
            int precision, int domain, int dimention, int length);
    }

    /// <summary>
    /// Values of the descriptor configuration parameters
    /// </summary>
    public enum DftiConfigValue:int
    {
        /// <summary>
        /// DFTI_COMMIT_STATUS
        /// </summary>
        DFTI_COMMITTED = 30,

        /// <summary>
        /// DFTI_COMMIT_STATUS
        /// </summary>
        DFTI_UNCOMMITTED = 31,

        /// <summary>
        /// DFTI_FORWARD_DOMAIN
        /// </summary>
        DFTI_COMPLEX = 32,
        /// <summary>
        /// DFTI_FORWARD_DOMAIN
        /// </summary>
        DFTI_REAL = 33,

        /* DFTI_CONJUGATE_EVEN = 34,   ## NOT IMPLEMENTED */

        /// <summary>
        /// DFTI_PRECISION
        /// </summary>
        DFTI_SINGLE = 35,

        /// <summary>
        /// DFTI_PRECISION
        /// </summary>
        DFTI_DOUBLE = 36,

        /* DFTI_FORWARD_SIGN */
        /* DFTI_NEGATIVE = 37,         ## NOT IMPLEMENTED */
        /* DFTI_POSITIVE = 38,         ## NOT IMPLEMENTED */

        /// <summary>
        /// DFTI_COMPLEX_STORAGE and DFTI_CONJUGATE_EVEN_STORAGE
        /// </summary>
        DFTI_COMPLEX_COMPLEX = 39,

        /// <summary>
        /// DFTI_COMPLEX_STORAGE and DFTI_CONJUGATE_EVEN_STORAGE
        /// </summary>
        DFTI_COMPLEX_REAL = 40,

        /// <summary>
        /// DFTI_REAL_STORAGE
        /// </summary>
        DFTI_REAL_COMPLEX = 41,

        /// <summary>
        /// DFTI_REAL_STORAGE
        /// </summary>
        DFTI_REAL_REAL = 42,

        /// <summary>
        /// DFTI_PLACEMENT
        /// </summary>
        DFTI_INPLACE = 43,          /* Result overwrites input */

        /// <summary>
        /// DFTI_PLACEMENT
        /// </summary>
        DFTI_NOT_INPLACE = 44,      /* Have another place for result */

        /* DFTI_INITIALIZATION_EFFORT */
        /* DFTI_LOW = 45,              ## NOT IMPLEMENTED */
        /* DFTI_MEDIUM = 46,           ## NOT IMPLEMENTED */
        /* DFTI_HIGH = 47,             ## NOT IMPLEMENTED */

        /// <summary>
        /// DFTI_ORDERING
        /// </summary>
        DFTI_ORDERED = 48,

        /// <summary>
        /// DFTI_ORDERING
        /// </summary>
        DFTI_BACKWARD_SCRAMBLED = 49,
        /* DFTI_FORWARD_SCRAMBLED = 50, ## NOT IMPLEMENTED */

        /// <summary>
        /// Allow/avoid certain usages
        /// </summary>
        DFTI_ALLOW = 51,            /* Allow transposition or workspace */

        /// <summary>
        /// Allow/avoid certain usages
        /// </summary>
        DFTI_AVOID = 52,

        /// <summary>
        /// Allow/avoid certain usages
        /// </summary>
        DFTI_NONE = 53,

        /// <summary>
        /// DFTI_PACKED_FORMAT (for storing congugate-even finite sequence in real array)
        /// </summary>
        DFTI_CCS_FORMAT = 54,       /* Complex conjugate-symmetric */
        DFTI_PACK_FORMAT = 55,      /* Pack format for real DFT */
        DFTI_PERM_FORMAT = 56,      /* Perm format for real DFT */
        DFTI_CCE_FORMAT = 57        /* Complex conjugate-even */
    };

    /* Descriptor configuration parameters [default values in brackets] */
    public enum DftiConfigPARAM:int
    {
        /// <summary>
        /// Domain for forward transform. No default value
        /// </summary>
        DFTI_FORWARD_DOMAIN = 0,

        /// <summary>
        /// Dimensionality, or rank. No default value
        /// </summary>
        DFTI_DIMENSION = 1,

        /// <summary>
        /// Length(s) of transform. No default value
        /// </summary>
        DFTI_LENGTHS = 2,

        /* Floating point precision. No default value */
        DFTI_PRECISION = 3,

        /// <summary>
        /// Length(s) of transform. No default value
        /// </summary>
        DFTI_FORWARD_SCALE = 4,

        /// <summary>
        /// Scale factor for backward transform [1.0]
        /// </summary>
        DFTI_BACKWARD_SCALE = 5,

        /* Exponent sign for forward transform [DFTI_NEGATIVE]  */
        /* DFTI_FORWARD_SIGN = 6, ## NOT IMPLEMENTED */

        /// <summary>
        /// Number of data sets to be transformed [1]
        /// </summary>
        DFTI_NUMBER_OF_TRANSFORMS = 7,

        /// <summary>
        /// Storage of finite complex-valued sequences in complex domain [DFTI_COMPLEX_COMPLEX]        
        /// </summary>
        DFTI_COMPLEX_STORAGE = 8,

        /// <summary>
        /// Storage of finite real-valued sequences in real domain [DFTI_REAL_REAL]
        /// </summary>
        DFTI_REAL_STORAGE = 9,

        /// <summary>
        ///  Storage of finite complex-valued sequences in conjugate-even domain [DFTI_COMPLEX_REAL]        
        /// </summary>
        DFTI_CONJUGATE_EVEN_STORAGE = 10,

        /// <summary>
        /// Placement of result [DFTI_INPLACE]
        /// </summary>
        DFTI_PLACEMENT = 11,

        /// <summary>
        ///  Generalized strides for input data layout [tigth, row-major for C]
        /// </summary>
        DFTI_INPUT_STRIDES = 12,

        /// <summary>
        /// Generalized strides for output data layout [tight, row-major for C]
        /// </summary>
        DFTI_OUTPUT_STRIDES = 13,

        /// <summary>
        /// Distance between first input elements for multiple transforms [0]
        /// </summary>
        DFTI_INPUT_DISTANCE = 14,

        /// <summary>
        ///  Distance between first output elements for multiple transforms [0]
        /// </summary>
        DFTI_OUTPUT_DISTANCE = 15,

        /* Effort spent in initialization [DFTI_MEDIUM] */
        /* DFTI_INITIALIZATION_EFFORT = 16, ## NOT IMPLEMENTED */

        /// <summary>
        /// Use of workspace during computation [DFTI_ALLOW]
        /// </summary>
        DFTI_WORKSPACE = 17,

        /// <summary>
        /// Ordering of the result [DFTI_ORDERED]
        /// </summary>
        DFTI_ORDERING = 18,

        /// <summary>
        /// Possible transposition of result [DFTI_NONE] 
        /// </summary>
        DFTI_TRANSPOSE = 19,

        /// <summary>
        /// User-settable descriptor name [""] 
        /// </summary>
        DFTI_DESCRIPTOR_NAME = 20, /* DEPRECATED */

        /// <summary>
        /// Packing format for DFTI_COMPLEX_REAL storage of finite conjugate-even sequences [DFTI_CCS_FORMAT]
        /// </summary>
        DFTI_PACKED_FORMAT = 21,

        /// <summary>
        /// Commit status of the descriptor - R/O parameter
        /// </summary>
        DFTI_COMMIT_STATUS = 22,

        /// <summary>
        /// Version string for this DFTI implementation - R/O parameter
        /// </summary>
        DFTI_VERSION = 23,

        /* Ordering of the forward transform - R/O parameter */
        /* DFTI_FORWARD_ORDERING  = 24, ## NOT IMPLEMENTED */

        /* Ordering of the backward transform - R/O parameter */
        /* DFTI_BACKWARD_ORDERING = 25, ## NOT IMPLEMENTED */

        /// <summary>
        /// Number of user threads that share the descriptor [1]
        /// </summary>
        DFTI_NUMBER_OF_USER_THREADS = 26,

        /// <summary>
        /// Limit the number of threads used by this descriptor [0 = don't care]
        /// </summary>
        DFTI_THREAD_LIMIT = 27,

        /// <summary>
        /// Possible input data destruction [DFTI_AVOID = prevent input data]
        /// </summary>
        DFTI_DESTROY_INPUT = 28
    };
}

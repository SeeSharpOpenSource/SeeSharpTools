using System.Runtime.InteropServices;

namespace SeeSharpTools.JY.Statistics.IPP
{
    unsafe internal class ippNativeX64
    {
        private const string dllFolder = @".\NativeDLLs\intel64\";
        private const string vectorMathematics = "ippvm.dll";
        private const string signalProcessing = "ipps.dll";

        #region Vector Initializing

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSet_64f(double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsCopy_64f(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsReplaceNAN_64f_I(double* pSrcDst, int len, double value);

        #endregion Vector Initializing

        #region Signal Generation

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsTone_64f(double* pDst, int len, double magn, double rFreq, double* pPhase, IppHintAlgorithm hint);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsTriangle_64f(double* pDst, int len, double magn, double rFreq, double asym, double* pPhase);

        #endregion Signal Generation

        #region Arithmetics

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAddC_64f(double* pSrc, double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAddC_64f_I(double val, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAdd_64f(double* pSrc1, double* pSrc2, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAdd_64f_I(double* pSrc, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMulC_64f(double* pSrc, double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMulC_64f_I(double val, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMul_64f(double* pSrc1, double* pSrc2, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMul_64f_I(double* pSrc, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSubC_64f(double* pSrc, double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSubC_64f_I(double val, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSub_64f(double* pSrc1, double* pSrc2, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSub_64f_I(double* pSrc, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDivC_64f(double* pSrc, double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDivC_64f_I(double val, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDiv_64f(double* pSrc1, double* pSrc2, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDiv_64f_I(double* pSrc, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAbs_64f(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSqr_64f(double* pSrc, double* pDst, int len);

        #endregion Arithmetics

        #region Power and Root

        [DllImport(dllFolder + "ippvm.dll")]
        public static extern IppStatus ippsInv_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + "ippvm.dll")]
        public static extern IppStatus ippsSqrt_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + "ippvm.dll")]
        public static extern IppStatus ippsPowx_64f_A53(double* pSrc1, double ConstValue, double* pDst, int len);

        #endregion Power and Root

        #region Exponential and Logarithmic

        [DllImport(dllFolder + "ippvm.dll")]
        public static extern IppStatus ippsExp_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + "ippvm.dll")]
        public static extern IppStatus ippsLn_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + "ippvm.dll")]
        public static extern IppStatus ippsLog10_64f_A53(double* pSrc, double* pDst, int len);

        #endregion Exponential and Logarithmic

        #region Statistics

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSum_64f(double* pSrc, int len, double* pSum);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMaxIndx_64f(double* pSrc, int len, double* pMax, int* pIndx);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMinIndx_64f(double* pSrc, int len, double* pMin, int* pIndx);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMinMaxIndx_64f(double* pSrc, int len, double* pMin, int* pMinIndx, double* pMax, int* pMaxIndx);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMean_64f(double* pSrc, int len, double* pMean);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsStdDev_64f(double* pSrc, int len, double* pStdDev);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMeanStdDev_64f(double* pSrc, int len, double* pMean, double* pStdDev);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDotProd_64f(double* pSrc1, double* pSrc2, int len, double* pDp);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsCountInRange_32s(int* pSrc, int len, int* pCounts, int lowerBound, int upperBound);

        #endregion Statistics

        #region Rounding

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsFloor_64f(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsFrac_64f(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsCeil_64f(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsTrunc_64f(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsRound_64f(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsNearbyInt_64f(double* pSrc, double* pDst, int len);

        #endregion Rounding

        #region FastFourierTransform

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippFree(byte[] ptr);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsFFTGetSize_R_64f(int order, int flag, IppHintAlgorithm hint, int* pSpecSize, int* pSpecBufferSize, int* pBufferSize);

        /*
         * order        | FFT order. The input signal length is N= 2order.
         * flag         | Specifies the result normalization method. The values for the flag argument are described in Flag and Hint Arguments.
         * hint         | This parameter is deprecated. Set the value to ippAlgHintNone.
         * pSpecSize    | Pointer to the FFT specification structure size value.
         * pSpecBufferSize Pointer to the buffer size value for FFT initialization function.
         * pBufferSize  | Pointer to the size value of the FFT external work buffer.
        */

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsFFTInit_R_64f(IppsFFTSpec_R_64f** ppFFTSpec, int order, int flag, IppHintAlgorithm hint, byte* pSpec, byte* pSpecBuffer);

        /*
         * order        | FFT order. The input signal length is N= 2order.
         * flag         | Specifies the result normalization method. The values for the flagargument are described in the section Flag and Hint Arguments.hint This parameter is deprecated. Set the value to ippAlgHintNone.
         * ppFFTSpec    | Double pointer to the FFT specification structure to be created.
         * pSpec        | Pointer to the area for the FFT specification structure.
         * pSpecBuffer  | Pointer to the work buffer.
         */

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsFFTFwd_RToPack_64f(double* pSrc, double* pDst, IppsFFTSpec_R_64f* pFFTSpec, byte* pBuffer);

        /*
         * pFFTSpec | Pointer to the FFT specification structure.
         * pSrc     | Pointer to the input array.
         * pDst     | Pointer to the output array containing packed complex values.
         * pSrcDst  | Pointer to the input and output arrays for the in-place operation.
         * pBuffer  | Pointer to the external work buffer.
        */

        #endregion FastFourierTransform
    }
}
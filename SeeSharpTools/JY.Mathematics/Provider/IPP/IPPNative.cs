using System.Runtime.InteropServices;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    unsafe internal class IPPNative
    {
        private const string dllFolder = @".\intelIPP\";
        private const string vectorMathematics = "ippvm.dll";
        private const string signalProcessing = "ipps.dll";

        #region Vector Initializing

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSet_64f(double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSet_32f(float val, float* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSet_16s(short val, short* pDst, int len);

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

        #region Conversion Function

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsFlip_64f_I(double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsFlip_32f_I(float* pSrcDst, int len);

        #endregion Conversion Function

        #region Arithmetics

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAddC_64f(double* pSrc, double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAddC_32f(float* pSrc, float val, float* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAddC_64f_I(double val, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAddC_32f_I(float val, float* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAddC_16s_I(short val, short* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAdd_64f(double* pSrc1, double* pSrc2, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAdd_32f(float* pSrc1, float* pSrc2, float* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAdd_16s(short* pSrc1, short* pSrc2, short* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAdd_64f_I(double* pSrc, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAdd_32f_I(float* pSrc, float* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAdd_16s_I(short* pSrc, short* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMulC_64f(double* pSrc, double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMulC_32f(float* pSrc, float val, float* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMulC_64f_I(double val, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMulC_32f_I(float val, float* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMulC_16s_I(short val, short* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMul_64f(double* pSrc1, double* pSrc2, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMul_32f(float* pSrc1, float* pSrc2, float* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMul_16s(short* pSrc1, short* pSrc2, short* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMul_64f_I(double* pSrc, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMul_32f_I(float* pSrc, float* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMul_16s_I(short* pSrc, short* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSubC_64f(double* pSrc, double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSubC_32f(float* pSrc, float val, float* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSubC_64f_I(double val, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSubC_32f_I(float val, float* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSubC_16s_I(short val, short* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSub_64f(double* pSrc1, double* pSrc2, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSub_32f(float* pSrc1, float* pSrc2, float* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSub_16s(short* pSrc1, short* pSrc2, short* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSub_64f_I(double* pSrc, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSub_32f_I(float* pSrc, float* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSub_16s_I(short* pSrc, short* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDivC_64f(double* pSrc, double val, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDivC_64f_I(double val, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDiv_64f(double* pSrc1, double* pSrc2, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDiv_32f(float* pSrc1, float* pSrc2, float* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDiv_64f_I(double* pSrc, double* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDiv_32f_I(float* pSrc, float* pSrcDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAbs_64f(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsAbs_32f(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsSqr_64f(double* pSrc, double* pDst, int len);

        #endregion Arithmetics

        #region Power and Root

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsInv_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsInv_32f_A24(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsSqrt_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsSqrt_32f_A24(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsPowx_64f_A53(double* pSrc1, double ConstValue, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsPowx_32f_A24(float* pSrc1, float ConstValue, float* pDst, int len);

        #endregion Power and Root

        #region Exponential and Logarithmic

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsExp_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsExp_32f_A24(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsLn_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsLn_32f_A24(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsLog10_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsLog10_32f_A24(float* pSrc, float* pDst, int len);

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
        public static extern IppStatus ippsMinMaxIndx_32f(float* pSrc, int len, float* pMin, int* pMinIndx, float* pMax, int* pMaxIndx);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMinMaxIndx_16s(short* pSrc, int len, short* pMin, int* pMinIndx, short* pMax, int* pMaxIndx);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMean_64f(double* pSrc, int len, double* pMean);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsStdDev_64f(double* pSrc, int len, double* pStdDev);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsMeanStdDev_64f(double* pSrc, int len, double* pMean, double* pStdDev);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDotProd_64f(double* pSrc1, double* pSrc2, int len, double* pDp);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsDotProd_32f(float* pSrc1, float* pSrc2, int len, float* pDp);

        [DllImport(dllFolder + signalProcessing)]
        public static extern IppStatus ippsCountInRange_32s(int* pSrc, int len, int* pCounts, int lowerBound, int upperBound);

        #endregion Statistics

        #region Triag functions

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsCos_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsCos_32f_A24(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsSin_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsSin_32f_A24(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsTan_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsTan_32f_A24(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsAsin_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsAsin_32f_A24(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsAcos_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsAcos_32f_A24(float* pSrc, float* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsAtan_64f_A53(double* pSrc, double* pDst, int len);

        [DllImport(dllFolder + vectorMathematics)]
        public static extern IppStatus ippsAtan_32f_A24(float* pSrc, float* pDst, int len);

        #endregion Triag functions

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

    public enum IppStatus
    {
        ippStsNoErr = 0,
    }

    public enum IppHintAlgorithm
    {
        ippAlgHintNone = 0,
        ippAlgHintFast = 1,
        ippAlgHintAccurate = 2,
    }

    public enum IppRoundMode
    {
        ippRndZero,
        ippRndNear,
        ippRndFinancial
    }

    public enum IppFlag
    {
        IPP_FFT_DIV_FWD_BY_N,
        IPP_FFT_DIV_INV_BY_N,
        IPP_FFT_DIV_BY_SQRTN,
        IPP_FFT_NODIV_BY_ANY,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IppsFFTSpec_R_64f { };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Ipp64fc
    {
        private double re;
        private double im;

        private Ipp64fc(double re, double im)
        {
            this.re = re;
            this.im = im;
        }
    };
}
using System.Runtime.InteropServices;

namespace SeeSharpTools.JY.Statistics.IPP
{
    internal enum IppStatus
    {
        ippStsNoErr = 0,
    }

    internal enum IppHintAlgorithm
    {
        ippAlgHintNone = 0,
        ippAlgHintFast = 1,
        ippAlgHintAccurate = 2,
    }

    internal enum IppRoundMode
    {
        ippRndZero,
        ippRndNear,
        ippRndFinancial
    }

    internal enum IppFlag
    {
        IPP_FFT_DIV_FWD_BY_N,
        IPP_FFT_DIV_INV_BY_N,
        IPP_FFT_DIV_BY_SQRTN,
        IPP_FFT_NODIV_BY_ANY,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct IppsFFTSpec_R_64f { };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct Ipp64fc
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
using System;

namespace SeeSharpTools.JY.Statistics.IPP
{
    unsafe internal class Generation
    {
        public static double ToneGeneration(ref double[] dest, double magnitude, double freq)
        {
            double[] phase = new double[1];
            IppHintAlgorithm hint = IppHintAlgorithm.ippAlgHintFast;
            fixed (double* pDst = dest, pPhase = phase)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsTone_64f(pDst, dest.Length, magnitude, freq, pPhase, hint);
                }
                else
                {
                    ippNativeX86.ippsTone_64f(pDst, dest.Length, magnitude, freq, pPhase, hint);
                }
            };

            return phase[0];
        }

        public static double TriangleGeneration(ref double[] dest, double magnitude, double freq, double asym = 0)
        {
            double[] phase = new double[1];
            fixed (double* pDst = dest, pPhase = phase)
            {
                if (Environment.Is64BitProcess)
                {
                    ippNativeX64.ippsTriangle_64f(pDst, dest.Length, magnitude, freq, asym, pPhase);
                }
                else
                {
                    ippNativeX86.ippsTriangle_64f(pDst, dest.Length, magnitude, freq, asym, pPhase);
                }
            };
            return phase[0];
        }
    }
}
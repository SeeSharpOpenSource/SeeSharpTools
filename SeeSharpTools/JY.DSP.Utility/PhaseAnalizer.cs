using System;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.IntegralTransforms;
using SeeSharpTools.JY.DSP.Fundamental;

namespace SeeSharpTools.JY.DSP.Utility
{
    /// <summary>
    /// Phase measurement class
    /// </summary>
    public static class Phase
    {
        #region 公有方法
        /// <summary>
        /// Calculate the phase shift between two input waveform arrays, return value will be between -180° and 180°.
        /// </summary>
        /// <param name="signal1">waveform array1</param>
        /// <param name="signal2">waveform array2</param>
        /// <returns></returns>
        public static double CalPhaseShift(double[] signal1, double[] signal2)
        {
            var signal1Hilbert = MathDotNetHilbert(signal1);
            var signal2Hilbert = MathDotNetHilbert(signal2);
            int dataLength = signal1.Length > signal2.Length ? signal2.Length : signal1.Length;
            double phaseShiftSum = 0;
            for (int i = 0; i < dataLength; i++)
            {
                phaseShiftSum += (signal1Hilbert[i] / signal2Hilbert[i]).Phase;
            }
            return phaseShiftSum * 180 / (Math.PI * dataLength);
        }

        /// <summary>
        /// Calculate the phase shift between two input waveform arrays, return value will be between -180° and 180°.
        /// </summary>
        /// <param name="signal1">waveform array1</param>
        /// <param name="signal2">waveform array2</param>
        /// <returns>phase shift in unit of degree</returns>
        public static double CalPhaseShift(float[] signal1, float[] signal2)
        {
            var signal1Hilbert = MathDotNetHilbert(signal1);
            var signal2Hilbert = MathDotNetHilbert(signal2);
            int dataLength = signal1.Length > signal2.Length ? signal2.Length : signal1.Length;
            double phaseShiftSum = 0;
            for (int i = 0; i < dataLength; i++)
            {
                phaseShiftSum += (signal1Hilbert[i] / signal2Hilbert[i]).Phase;
            }
            return phaseShiftSum * 180 / (Math.PI * dataLength);
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// Hilbert transform by Math.Net
        /// </summary>
        /// <param name="xreal"></param>
        /// <returns></returns>
        private static Complex[] MathDotNetHilbert(double[] xreal)
        {
            var x = (from sample in xreal select new Complex(sample, 0)).ToArray();
            Fourier.Forward(x, FourierOptions.Default);
            var h = new double[x.Length];
            var fftLengthIsOdd = (x.Length | 1) == 1;
            if (fftLengthIsOdd)
            {
                h[0] = 1;
                for (var i = 1; i < xreal.Length / 2; i++) h[i] = 2;
            }
            else
            {
                h[0] = 1;
                h[(xreal.Length / 2)] = 1;
                for (var i = 1; i < xreal.Length / 2; i++) h[i] = 2;
            }
            for (var i = 0; i < x.Length; i++) x[i] *= h[i];
            Fourier.Inverse(x, FourierOptions.Default);

            return x;
        }

        /// <summary>
        /// Hilbert transform by Math.Net
        /// </summary>
        /// <param name="xreal"></param>
        /// <returns></returns>
        private static Complex[] MathDotNetHilbert(float[] xreal)
        {
            var x = (from sample in xreal select new Complex(sample, 0)).ToArray();
            Fourier.Forward(x, FourierOptions.Default);
            var h = new double[x.Length];
            var fftLengthIsOdd = (x.Length | 1) == 1;
            if (fftLengthIsOdd)
            {
                h[0] = 1;
                for (var i = 1; i < xreal.Length / 2; i++) h[i] = 2;
            }
            else
            {
                h[0] = 1;
                h[(xreal.Length / 2)] = 1;
                for (var i = 1; i < xreal.Length / 2; i++) h[i] = 2;
            }
            for (var i = 0; i < x.Length; i++) x[i] *= h[i];
            Fourier.Inverse(x, FourierOptions.Default);

            return x;
        }
        #endregion
    }
}

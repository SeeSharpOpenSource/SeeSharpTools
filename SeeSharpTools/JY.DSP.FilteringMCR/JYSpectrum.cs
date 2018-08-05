/*************************************************************
 * Matlab Code:
 * 
 * function [Y,df]= Spectrum(X,N,Fs,Unit)
 * y = fft(X,N);
 * mag = 2 * abs(y/N);
 * mag(1) = mag(1)/2;
 * if Unit == 0 % Unit:V
 * else
 *     if Unit == 1 % Unit:Vrms
 *         mag = mag/sqrt(2);
 *     else
 *         if Unit == 2 % Unit:DBV
 *         mag = 20 * log(mag); %spectrum unit : dBV
 *         end
 *     end
 * end
 * df = Fs/length(y);
 * datalength = length(mag)/2;
 * Y = mag(1:datalength);
 * end
 * 
**************************************************************/
using System;
using DSPMatlab;
using MathWorks.MATLAB.NET.Arrays;

namespace SeeSharpTools.JY.DSP.FilteringMCR
{

    /// <summary>
    /// JY Spectrum Class
    /// </summary>
    public class JYSpectrum
    {
        #region JYSpectrum public methods
        /// <summary>
        /// DateUnit: V/Vrms/DBV
        /// </summary>
        public enum DataUnit
        {
            V,
            Vrms,
            DBV
        }
        #endregion

        #region JYSpectrum public methods
        /// <summary>
        /// Initialize Matlab Engine
        /// </summary>
        public static void Initialize()
        {
            double[] Wave = new double[2];
            double df = 0;
            FFTSpectrum(Wave, 2, ref df, DataUnit.DBV);
        }
        /// <summary>
        /// FFT Spectrum 
        /// </summary>
        /// <param name="Wave">Input Wave</param>
        /// <param name="SampleRate">Sampling Rate</param>
        /// <param name="df">Frequency interval</param>
        /// <param name="Unit">Date Unit: V/Vrms/DBV</param>
        /// <returns>Spectrum result</returns>
        public static double[] FFTSpectrum(double[]Wave,int SampleRate, ref double df,DataUnit Unit)
        {
        
            MWNumericArray WaveMatlab = Wave;
            MWArray WavePointMatlab = Wave.Length;
            MWArray SampleRateMatlab = SampleRate;
            MWArray UnitMatlab = (int)Unit;
            MWArray[] FFTResult;

            double[,] X2D, Y2D;
            double[] Y;
            DSPClass DSPTask = new DSPClass();
            FFTResult = DSPTask.Spectrum(2, WaveMatlab, WavePointMatlab, SampleRateMatlab, UnitMatlab);

            X2D = (double[,])FFTResult[1].ToArray();
            Y2D = (double[,])FFTResult[0].ToArray();
            Y = new double[Y2D.Length];
            df = X2D[0, 0];
            Buffer.BlockCopy(Y2D, 0, Y, 0, Y2D.Length * sizeof(double));
            return Y;
        }
        #endregion
    }
}

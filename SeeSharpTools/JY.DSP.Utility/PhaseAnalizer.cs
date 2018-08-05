using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeeSharpTools.JY.DSP.Fundamental;

namespace SeeSharpTools.JY.DSP.Utility
{
    /// <summary>
    /// PeakSpectrum measurement class
    /// </summary>
    public static class PeakSpectrum
    {
        /// <summary>
        /// Get the fundamental frequency and array of harmonic power.
        /// </summary>
        /// <param name="timewaveform">the waveform of input signal assuming in voltage</param>
        /// <param name="dt">sampling interval of timewaveform (s)</param>
        /// <param name="peakFreq">the calculated peak tone frequency</param>
        /// <param name="peakAmp">the calculated peak tone voltage peak amplitude, which is 1.414*RMS</param>
        /// i.e. peakSignal=peakAmp*sin(2*pi*peakFreq*t)
        public static void PeakSpectrumAnalysis(double[] timewaveform, double dt, out double peakFreq, out double peakAmp)
        {
            double[] spectrum = new double[timewaveform.Length / 2];
            double df;
            var spectUnit = SpectrumUnits.V2; //this V^2 unit relates to power in band calculation, don't change
            var winType = WindowType.Hanning;  //relates to ENBW, must change in pair
            double ENBW = 1.5000; //ENBW for winType Hanning.ENBW = 1.500

            double approxFreq = -1;
            double searchRange = 0;

            double maxValue = 0;
            int maxValueIndex = 0;
            int i, approxFreqIndex, startIndex, endIndex;
            double powerInBand = 0;
            double powerMltIndex = 0;

            Spectrum.PowerSpectrum(timewaveform, 1 / dt, ref spectrum, out df, spectUnit, winType);
            if (approxFreq < 0)
            {
                startIndex = 0;
                endIndex = spectrum.Length;
            }
            else
            {
                approxFreqIndex = (int)(approxFreq / df);
                endIndex = (int)(searchRange / 200 / dt); // start earch with approx. Freq - 1/2 range
                startIndex = approxFreqIndex - endIndex;  //start search index
                endIndex = (int)(searchRange / 100 / dt); //search length in indexes
                if (startIndex < 0) startIndex = 0;  //start index protection
                if (startIndex > spectrum.Length - 2) startIndex = spectrum.Length - 2;  //start index protection
                if (endIndex < 1) endIndex = 1; //protect search range;
            };
            //Search spectrum from [i1] to i1+i2-1;
            maxValue = -1; //power spectrum can not be less than 0;

            maxValue = spectrum.Max();
            maxValueIndex = Array.FindIndex<double>(spectrum, s => s == maxValue);

            startIndex = maxValueIndex - 3;
            if (startIndex < 0) startIndex = 0;

            endIndex = startIndex + 7;

            if (endIndex > spectrum.Length - 1) endIndex = spectrum.Length - 1;

            for (i = startIndex; i < endIndex; i++)
            {
                powerInBand += spectrum[i];
                powerMltIndex += spectrum[i] * i;
            }
            peakFreq = powerMltIndex / powerInBand * df;     //Given the estimated frequency and power, the exact frequency can be calculated
            peakAmp = Math.Sqrt(powerInBand / ENBW * 2); //convert V^2 to V peak amplitude                        //Refer this formula to  ITU Handbook
        }
    }

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
        /// <returns>phase shift in unit of degree</returns>
        public static double CalPhaseShift(double[] signal1, double[] signal2)
        {
            double phase = Math.Acos(Dot(signal1, signal2) / (Norm(signal1) * Norm(signal2)));
            
            //convert radian to degree
            if (signal1[0] - signal2[0] < 0)
            {
                phase = - phase * 180 / Math.PI;
            }     
            else
            {
                phase = phase * 180 / Math.PI;
            }
            return phase;
        }

        /// <summary>
        /// Calculate the phase shift between two input waveform arrays, return value will be between -180° and 180°.
        /// </summary>
        /// <param name="signal1">waveform array1</param>
        /// <param name="signal2">waveform array2</param>
        /// <returns>phase shift in unit of degree</returns>
        public static float CalPhaseShift(float[] signal1, float[] signal2)
        {
            float phase = (float)Math.Acos(Dot(signal1, signal2) / (Norm(signal1) * Norm(signal2)));

            //convert radian to degree
            if (signal1[0] - signal2[0] < 0)
            {
                phase = -phase * 180 / (float)Math.PI;
            }
            else
            {
                phase = phase * 180 / (float)Math.PI;
            }
            return phase;
        }

        #endregion

        #region 私有方法
        private static double Dot(double[] data1, double[] data2)
        {
            double sum = 0;
            for (int i = 0; i < data1.Length; i++)
            {
                sum += data1[i] * data2[i];
            }
            return sum;
        }

        private static float Dot(float[] data1, float[] data2)
        {
            float sum = 0;
            for (int i = 0; i < data1.Length; i++)
            {
                sum += data1[i] * data2[i];
            }
            return sum;
        }

        private static double Norm(double[] data)
        {
            double squareSum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                squareSum += (Math.Abs(data[i]) * Math.Abs(data[i]));
            }
            return Math.Sqrt(squareSum);
        }

        private static float Norm(float[] data)
        {
            float squareSum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                squareSum += (Math.Abs(data[i]) * Math.Abs(data[i]));
            }
            return (float)Math.Sqrt(squareSum);
        }
        #endregion
    }
}

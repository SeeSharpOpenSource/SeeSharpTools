using System;
using System.Numerics;
using MathNet.Numerics;
using SeeSharpTools.JY.DSP.Fundamental;


namespace SeeSharpTools.JY.DSP.Utility
{

    /// <summary>
    /// ToneAnalysis Class
    /// </summary>
    public static class ToneAnalyzer
    {
        /// <summary>
        /// Perform substraction on two phase value, unit in radian. Thre result will be wrapped to range [-Pi, Pi] (*)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>The difference between p1 and p2. </returns>
        static private double _PhaseSub(double p1, double p2)
        {
            double diff = (p1 - p2) / Math.PI / 2.0;
            diff -= Math.Round(diff);       // Round() performs 'round to even'. Thus both -0.5 and +0.5 might show in result.
            return diff * Math.PI * 2.0;
        }

        /// <summary>
        /// Refine the peak estimation based on three cosequent peak spectrum values. 
        /// Ref: X.Ming, D.Kang “Corrections for frequency, amplitude and phase in a fast fourier transform of 
        /// a harmonic signal” Mechanical Systems and Signal Processing V. 10, 2, March 1996, Pages 211-221
        /// </summary>
        /// <param name="threeFingers">three spectrum</param>
        /// <returns>Tone information. Frequency unit in 'bins'. </returns>
        private static ToneInfo FFTPeakCorrection(Complex[] threeFingers)
        {
            ToneInfo toneInfo = new ToneInfo();
            double a, b, c;
            double delta;

            a = threeFingers[0].Magnitude;
            b = threeFingers[1].Magnitude;
            c = threeFingers[2].Magnitude;

            if (a == c)
            {
                delta = 0;
                toneInfo.Frequency = 1;
                toneInfo.Amplitude = b;
            }
            else
            {
                double sub_peak = a > c ? a : c;
                delta = (b - 2.0 * sub_peak) / (b + sub_peak) * (a > c ? 1 : -1);
                toneInfo.Frequency = 1 + delta;
                toneInfo.Amplitude = b * (1 - delta * delta) / Trig.Sinc(delta);
            }

            // Linear interpolate phase value
            double p0, p1;
            p0 = threeFingers[1].Phase;
            p1 = threeFingers[a > c ? 0 : 2].Phase;

            toneInfo.Phase = _PhaseSub(p0 + delta * _PhaseSub(p1, p0), 0);

            return toneInfo;
        }
        // ********************************************************************************
        /// <summary>
        /// Single Tone Analysis
        /// </summary>
        /// <param name="timewaveform">Waveform in time space</param>
        /// <param name="Fs">Sampling frequency, unit in Hz</param>
        /// <param name="initialGuess">Initial guess for the tone frequency, unit in Hz</param>
        /// <param name="searchRange">Peak search range near the initialGuess.</param>
        /// <returns>Tone information of the signal. Contains amplitude, frequency and phase.</returns>
        /// <created>Wei Jin,2019/11/29</created>
        /// <changed>Wei Jin,2019/11/29</changed>
        // ********************************************************************************
        public static ToneInfo SingleToneAnalysis(double[] timewaveform, double Fs=1.0, double initialGuess=0, double searchRange = 0.05)
        {
            ToneInfo toneInfo;
            int i; 

            int fftSize = timewaveform.Length;
            Complex[] spectrum = new Complex[fftSize];

            Spectrum.AdvanceComplexFFT(timewaveform, WindowType.Hanning, ref spectrum);

            // Use the center position as time '0' reference, which makes the spectrum of the window function pure real number 
            for (i =0;i < (fftSize + 1) / 4; i++)
            {
                spectrum[i * 2 + 1] = -spectrum[i * 2 + 1];
            }

            // Null DC components
            for (i = 0; i < 2; i++)
                spectrum[i] = 0;

            // Establish tone search range
            int searchStart = 2;
            int searchEnd = fftSize / 2 - 2;

            if (initialGuess > 0 && initialGuess < Fs / 2.0)
            {
                searchStart = Math.Max(searchStart, (int)((initialGuess / Fs - searchRange / 2) * fftSize));
                searchEnd = Math.Min(searchEnd, (int)((initialGuess / Fs + searchRange / 2) * fftSize));
            }

            // Gross search for the peak tone
            double peakVal = 0;
            int peakPos = 0;
            for (i = searchStart; i< searchEnd; i++)
            {
                if (peakVal < spectrum[i].Magnitude)
                {
                    peakVal = spectrum[i].Magnitude;
                    peakPos = i;
                }
            }

            // Refine peak result for the first round
            Complex[] threeFingers = new Complex[3];
            Array.Copy(spectrum, peakPos - 1, threeFingers, 0, 3);
            toneInfo = FFTPeakCorrection(threeFingers);
            toneInfo.Frequency += peakPos - 1;

            // Remove aliasing around DC and Fs/2
            // The Fourier transform of the hanning window has the following form
            //      h(z)    = sinc(z) / (z * z - 1)
            //              = sin(pi * z) / (pi * z * (z * z - 1))
            // Note: the negative frequency component has conjugate phase
            // Question(wjin): Why use add instead of sub?            
            double x_offset;
            for (i = 0; i < 3; i++)
            {
                x_offset = peakPos - 1 + i + toneInfo.Frequency;
                threeFingers[i] += Trig.Sinc(x_offset) / (x_offset * x_offset - 1.0) 
                    * Complex.FromPolarCoordinates(toneInfo.Amplitude, -toneInfo.Phase);

                x_offset -= fftSize;
                threeFingers[i] += Trig.Sinc(x_offset) / (x_offset * x_offset - 1.0)
                    * Complex.FromPolarCoordinates(toneInfo.Amplitude, -toneInfo.Phase);
            }

            // Refine peak result for the second round
            toneInfo = FFTPeakCorrection(threeFingers);
            toneInfo.Frequency += peakPos - 1;

            // Correct the results for output.
            toneInfo.Amplitude *= 2.0 / fftSize;
            toneInfo.Phase = _PhaseSub(toneInfo.Phase, toneInfo.Frequency * Math.PI);   // Change the reference position to the begining of the singal
            toneInfo.Phase = _PhaseSub(toneInfo.Phase, -0.5 * Math.PI);                 // Change Cos phase to Sin phase
            toneInfo.Frequency *= Fs / fftSize;                                         // Convert frequency unit from 'bins' to engineering units.

            return toneInfo;
        }
    }  



    /// <summary>
    /// Tone Analysis Result
    /// </summary>
    public class ToneInfo
    {
        /// <summary>
        /// Tone frequency
        /// </summary>
        public double Frequency;
        /// <summary>
        /// Tone amplitude
        /// </summary>
        public double Amplitude;
        /// <summary>
        /// Tone phase
        /// </summary>
        public double Phase;
    }
}

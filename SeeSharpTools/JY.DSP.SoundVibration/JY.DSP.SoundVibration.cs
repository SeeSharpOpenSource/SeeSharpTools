using SeeSharpTools.JY.DSP.Fundamental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JY.DSP.SoundVibration
{
    public static class HarmonicAnalyzer
    {
        /// <summary>
        /// Calculates the THD and level of all components of the input signal.
        /// THD in value, not %
        /// component levels in voltage peak which is 1.414*rms
        /// component[0]=DC level; [1]=fundamental frequency level; ...
        /// if the specified highest harmonic is higher than nyquest freq, 
        /// the exceeded frequency components will be 0; 
        /// </summary>
        /// <param name="timewaveform">the waveform of input signal assuming in voltage</param>
        /// <param name="dt">sampling interval of timewaveform (s)</param>
        /// <param name="detectedFundamentalFreq">the calculated peak tone frequency in the range of search</param>
        /// <param name="THD">total harmonic distortion in scale = sqrt(sum(harmonics power) / fundamental power)</param>
        /// <param name="componentsLevel">in voltage peak which is 1.414*rms, [0]for DC [1] for fundamental</param>
        /// <param name="highestHarmonic">the highest order to analysis, if too high, the exceeded harmonics level will be 0</param>
        public static void ToneAnalysis(double[] timewaveform, double dt, out double detectedFundamentalFreq, out double THD, ref double[] componentsLevel, int highestHarmonic = 10)
        {
            double[] spectrum = new double[timewaveform.Length / 2];
            double df;
            var spectUnit = SpectrumUnits.V2; //this V^2 unit relates to power in band calculation, don't change
            var winType = WindowType.Hanning;  //relates to ENBW, must change in pair
            double ENBW = 1.5000; //ENBW for winType Hanning.ENBW = 1.500

            double maxValue = 0;
            int maxValueIndex = 0;
            int i, approxFreqIndex, startIndex = 0, endIndex = spectrum.Length;
            double powerInBand = 0;
            double powerMltIndex = 0;
            double powerTotalHarmonic = 0;

            Spectrum.PowerSpectrum(timewaveform, 1 / dt, ref spectrum, out df, spectUnit, winType);
            //****************************************
            //Search peak

            //Search spectrum from [i1] to i1+i2-1;
            maxValue = -1; //power spectrum can not be less than 0;
            maxValue = spectrum.Max();
            maxValueIndex = Array.FindIndex<double>(spectrum, s => s == maxValue);


            //Search peak ends
            //****************************************
            //Peak analysis
            startIndex = maxValueIndex - 3;
            if (startIndex < 0) startIndex = 0;

            endIndex = startIndex + 7;

            if (endIndex > spectrum.Length - 1) endIndex = spectrum.Length - 1;

            for (i = startIndex; i < endIndex; i++)
            {
                powerInBand += spectrum[i];
                powerMltIndex += spectrum[i] * i;
            }
            //Given the estimated frequency and power, the exact frequency can be calculated
            detectedFundamentalFreq = powerMltIndex / powerInBand * df;

            componentsLevel[0] = spectrum[0] / ENBW;  //DC in V^2
            componentsLevel[1] = powerInBand / ENBW; //unit V^2 for amplitude  //Refer this formula to  ITU Handbook
                                                     //Peak analysis ends
                                                     //****************************************
                                                     //Power calculation for THD

            powerTotalHarmonic = 0;
            for (i = 2; i <= highestHarmonic; i++)
            {
                approxFreqIndex = (int)Math.Round(detectedFundamentalFreq / df * i - 2);
                if (approxFreqIndex < 0) approxFreqIndex = 0;

                powerInBand = 0;
                for (startIndex = 1; startIndex < 5; startIndex++)
                {
                    if (approxFreqIndex + startIndex < spectrum.Length)
                    {
                        powerInBand += spectrum[approxFreqIndex + startIndex];
                    }
                }
                componentsLevel[i] = powerInBand / ENBW;
                powerTotalHarmonic += componentsLevel[i];
            }
            THD = powerTotalHarmonic / componentsLevel[1];
            THD = Math.Sqrt(THD);
            //Power calculation ends
            //****************************************
            // transfer components level from V^2 to V peak amplitude;
            for (i = 0; i < highestHarmonic; i++)
            {
                componentsLevel[i] = Math.Sqrt(componentsLevel[i] * 2);
            }
            //transfer ends
            //****************************************
        }
    }
}

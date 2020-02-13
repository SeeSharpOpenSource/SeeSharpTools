using System;
using System.Reflection;
using System.Linq;
using SeeSharpTools.JY.DSP.Fundamental;

namespace SeeSharpTools.JY.DSP.Utility
{
    /// <summary>
    /// 系统噪声计算
    /// </summary>
    public static class SystemNoiseCalculation
    {
        /// <summary>
        /// Calculate System Noise In Target Band ( Frequency Domain Method ).
        /// No DC.
        /// </summary>
        /// <param name="timewaveform">Waveform in time space</param>
        /// <param name="dt"> Interval time of waveform </param>
        /// <param name="startFrequency">Start frequency( FFT result bin0 and bin1 removed )</param>
        /// <param name="stopFrequency">Stop frequency</param>
        /// <returns></returns>
        public static double CalculateSystemNoise(double[] timewaveform, double dt, double startFrequency, double stopFrequency)
        {
            if (stopFrequency < 0 || startFrequency < 0 || startFrequency > stopFrequency || stopFrequency > 1 / dt / 2)
            {
                throw new Exception("StartFrequency or StopFrequency is wrong.");
            }
            double[] spectrumForRMS = new double[timewaveform.Length / 2];
            var winTypeForRMS = WindowType.Hanning;
            double df;
            int startIndex = 0;
            int stopIndex = 0;
            double sumPower = 0;
            double rMSNoise;
            //Correction Factor of Hanning
            double correctionFactor = 0.8165;
            Spectrum.PowerSpectrum(timewaveform, (double)1 / dt, ref spectrumForRMS, out df, SpectrumUnits.V2, winTypeForRMS);
            startIndex = (int)(startFrequency / df);
            if(startIndex < 2)
            {
                startIndex = 2;
            }
            stopIndex = (int)(stopFrequency / df);
            for(int i = startIndex; i <= stopIndex-1; i++)
            {
                sumPower += spectrumForRMS[i];
            }
            rMSNoise = correctionFactor * Math.Sqrt(sumPower);
            return rMSNoise;
        }
        /// <summary>
        /// Calculate System Noise ( Time Domain Method ).
        /// No DC.
        /// </summary>
        /// <param name="timewaveform">Waveform in time space</param>
        /// <returns></returns>
        public static double CalculateSystemNoise(double[] timewaveform)
        {
            double sum = 0;
            double avg;
            double Vrms;
            for(int i = 0; i < timewaveform.Length; i++)
            {
                sum += timewaveform[i];
            }
            avg = sum / timewaveform.Length;
            sum = 0;
            for (int i = 0; i < timewaveform.Length; i++)
            {
                sum += (timewaveform[i]-avg)* (timewaveform[i] - avg);
            }
            Vrms = Math.Sqrt(sum / timewaveform.Length);
            return Vrms;
        }
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
        private static void ComponentsLevelCaculation(double[] timewaveform, double dt, out double detectedFundamentalFreq, out double THD, ref double[] componentsLevel, int highestHarmonic = 10)
        {
            double[] spectrum = new double[timewaveform.Length / 2];
            double df;
            var spectUnit = SpectrumUnits.V2; //this V^2 unit relates to power in band calculation, don't change
            //var winType = WindowType.Hanning;  //relates to ENBW, must change in pair
            //double ENBW = 1.5000; //ENBW for winType Hanning.ENBW = 1.500
            //var winType = WindowType.Hamming;  //relates to ENBW, must change in pair
            //double ENBW = 1.36283; //ENBW for winType Hanning.ENBW = 1.500
            var winType = WindowType.Seven_Term_B_Harris;  //relates to ENBW, must change in pair
            double ENBW = 2.63191; //ENBW for winType Hanning.ENBW = 1.500

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
            startIndex = maxValueIndex - 7;
            if (startIndex < 0) startIndex = 0;

            endIndex = startIndex + 14;


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
                //approxFreqIndex = (int)Math.Round(detectedFundamentalFreq / df * i - 4);
                approxFreqIndex = (int)Math.Round(detectedFundamentalFreq / df * i - 7);
                if (approxFreqIndex < 0) approxFreqIndex = 0;

                powerInBand = 0;
                //for (startIndex = 0; startIndex < 9; startIndex++)
                for (startIndex = 0; startIndex < 15; startIndex++)
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
            for (i = 0; i <= highestHarmonic; i++)
            {
                componentsLevel[i] = Math.Sqrt(componentsLevel[i] * 2);
            }
            //transfer ends
            //****************************************
        }
    }

    /// <summary>
    /// 谐波分析类
    /// </summary>
    [Obsolete]
    public static class HarmonicAnalysis
    {
        /// <summary>
        /// Calculate System Noise In Target Band ( Frequency Domain Method ).
        /// No DC.
        /// </summary>
        /// <param name="timewaveform">Waveform in time space</param>
        /// <param name="dt"> Interval time of waveform </param>
        /// <param name="startFrequency">Start frequency( FFT result bin0 and bin1 removed )</param>
        /// <param name="stopFrequency">Stop frequency</param>
        /// <returns></returns>
        public static double CalculateSystemNoise(double[] timewaveform, double dt, double startFrequency, double stopFrequency)
        {
            return SystemNoiseCalculation.CalculateSystemNoise(timewaveform, dt, startFrequency, stopFrequency);
        }

        /// <summary>
        /// Calculate System Noise ( Time Domain Method ).
        /// No DC.
        /// </summary>
        /// <param name="timewaveform">Waveform in time space</param>
        /// <returns></returns>
        public static double CalculateSystemNoise(double[] timewaveform)
        {
            return SystemNoiseCalculation.CalculateSystemNoise(timewaveform);
        }
    }
}

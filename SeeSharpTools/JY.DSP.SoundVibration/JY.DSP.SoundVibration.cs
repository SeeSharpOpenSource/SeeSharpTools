using SeeSharpTools.JY.DSP.Fundamental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SeeSharpTools.JY.DSP.SoundVibration
{
    /// <summary>
    /// Harmonic Analysis class
    /// </summary>
    public static class HarmonicAnalyzer
    {
        /// <summary>
        /// Tone Analysis
        /// </summary>
        /// <param name="timewaveform">Waveform in time space</param>
        /// <param name="dt"> Interval time of waveform </param>
        /// <param name="highestHarmonic">HighestHamonic level</param>
        /// <param name="resultInDB">If return result in DB</param>
        /// <returns></returns>
        public static ToneAnalysisResult ToneAnalysis(double[] timewaveform, double dt = 1, int highestHarmonic = 10, bool resultInDB = true)
        {
            ToneAnalysisResult taResult = new ToneAnalysisResult();
            double[] harmonicsLevel = new double[highestHarmonic + 1];
            double THD;
            double peakFrequency;
            ComponentsLevelCaculation(timewaveform, dt, out peakFrequency, out THD, ref harmonicsLevel, highestHarmonic);

            double sumPower = 0;
            double totalRMS = 0;
            double NoiseDistortionRMS = 0;
            double NoiseV2RMS = 0;
            double THDplusN = 0;
            double df = 0;
            //bug fix: this RMS calculation is wrong, as it has no window, so to have chances
            //for RMS<component[1], ths signal only
            //for (int i = 0; i < loadedSignal.Length; i++)
            //{
            //    sumPower += Math.Pow(loadedSignal[i],2);
            //}
            //totalRMS = Math.Sqrt(sumPower/ loadedSignal.Length); //RMS of siangl = S+N+D

            //RMS can be calculated by spectrum which will be the same as that in Tone Analysis
            double[] spectrumForRMS = new double[timewaveform.Length / 2];
            //var winTypeForRMS = WindowType.Hanning;  //relates to ENBW, must change in pair
            //double ENBWforwinType = 1.5000; //ENBW for winType Hanning.ENBW = 1.500
            //var winTypeForRMS = WindowType.Hamming;  //relates to ENBW, must change in pair
            //double ENBWforwinType = 1.36283; //ENBW for winType Hanning.ENBW = 1.500
            var winTypeForRMS = WindowType.Seven_Term_B_Harris;  //relates to ENBW, must change in pair
            double ENBWforwinType = 2.63191; //ENBW for winType Hanning.ENBW = 1.500

            Spectrum.PowerSpectrum(timewaveform, (double)1 / dt, ref spectrumForRMS, out df, SpectrumUnits.V2, winTypeForRMS);
            for (int i = 1; i < spectrumForRMS.Length; i++)
            {
                sumPower += spectrumForRMS[i];
            }
            totalRMS = Math.Sqrt(sumPower / ENBWforwinType);
            //end of total power calculation  -20170505 by JXISH

            //SINAD = (S+N+D)/(N+D), all in power, but we calculate it in dB
            //THD+N=(N+D)/(S+N+D), all in power
            //THD = D / S
            NoiseDistortionRMS = Math.Sqrt(totalRMS * totalRMS - harmonicsLevel[1] * harmonicsLevel[1] / 2);
            THDplusN = NoiseDistortionRMS / totalRMS;
            taResult.THD = THD;
            taResult.THDplusN = THDplusN;
            //taResult.SINAD = (double)1 / THDplusN;
            taResult.SINAD = totalRMS / NoiseDistortionRMS;
            //taResult.SNR = ((double)1 - THDplusN) / (THDplusN + THDplusN * THD - THD);
            //Noise = total - signal - distortion
            NoiseV2RMS = totalRMS * totalRMS - harmonicsLevel[1] * harmonicsLevel[1] / 2;
            for (int i = 2; i < harmonicsLevel.Length; i++)
            {
                NoiseV2RMS -= harmonicsLevel[i] * harmonicsLevel[i] / 2;
            }
            taResult.SNR = Math.Sqrt(harmonicsLevel[1] * harmonicsLevel[1] / 2 / NoiseV2RMS);
            //NoiseFloor = SNR + 10*log(M/2)
            taResult.NoiseFloor = taResult.SNR * Math.Sqrt(timewaveform.Length / 2);
            taResult.ENOB = 1;
            if (resultInDB)
            {
                Type t = typeof(ToneAnalysisResult);
                FieldInfo[] taInfo = t.GetFields();
                foreach (FieldInfo item in taInfo)
                {
                    item.SetValue(taResult, 20 * Math.Log10((double)item.GetValue(taResult)));
                }
                taResult.ENOB = (taResult.SINAD - 1.76) / 6.02;
            }
            else
            {
                taResult.ENOB = (20 * Math.Log10(taResult.SINAD) - 1.76) / 6.02;
            }
            return taResult;
        }

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
    /// Tone Analysis Result
    /// </summary>
    public class ToneAnalysisResult
    {
        /// <summary>
        /// THD
        /// </summary>
        public double THD;
        /// <summary>
        /// THD + Noise
        /// </summary>
        public double THDplusN;
        /// <summary>
        /// SINAD
        /// </summary>
        public double SINAD;
        /// <summary>
        /// SNR
        /// </summary>
        public double SNR;
        /// <summary>
        /// Noise Floor
        /// </summary>
        public double NoiseFloor;
        /// <summary>
        /// Effictive number of bits
        /// </summary>
        public double ENOB;
    }
}

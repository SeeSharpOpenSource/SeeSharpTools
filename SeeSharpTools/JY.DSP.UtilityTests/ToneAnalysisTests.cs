using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JY.DSP.Utility;
using System;
using MathNet.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JY.DSP.Utility.Tests
{
    [TestClass()]
    public class ToneAnalysisTests
    {
        [TestMethod()]
        public void SingleToneAnalysisTest()
        {
            const int FFT_SIZE = 256;
            const double SNR = 40;

            double noiseLevel = Math.Pow(10, SNR / (-20.0));
            Random rnd = new Random();
            double amplitude = 1.0;
            double freq = (rnd.NextDouble() * (FFT_SIZE / 2.0 - 4.0) + 2.0) / FFT_SIZE/4.0;
            //double freq = 8.5 / FFT_SIZE;
            double phase = rnd.NextDouble() * 2.0 * Math.PI;
            //double phase = 0.375 * 2.0 * Math.PI;

            double[] noise = Generate.Normal(FFT_SIZE, 0.0, noiseLevel);
            double[] wave = Generate.Sinusoidal(FFT_SIZE, 1.0, freq, amplitude, 0.0, phase);

            for (int i = 0; i < FFT_SIZE; i++)
                wave[i] += noise[i];


            ToneInfo toneInfo = ToneAnalyzer.SingleToneAnalysis(wave);

            Assert.IsTrue(Math.Abs(toneInfo.Frequency - freq) < 1.0E-3, "Frequency not equal.");
            double phaseErr = (toneInfo.Phase - phase) / (2.0 * Math.PI);
            phaseErr -= Math.Round(phaseErr);
            Assert.IsTrue(Math.Abs(phaseErr) < 1.0E-3, "Phase not equal.");
            Assert.IsTrue(Math.Abs(toneInfo.Amplitude - amplitude) < 1.0E-2, "Amplitude not equal.");

        }
    }
}
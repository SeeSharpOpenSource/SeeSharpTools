using SeeSharpTools.JY.Audio.Common;
using SeeSharpTools.JY.Audio.Waveform;

namespace SeeSharpTools.JY.Audio.Analyzer
{
    /// <summary>
    /// 波幅递增波形分析
    /// </summary>
    public class SteppedLevelSineAnalyzer : AnalyzerBase
    {
        private ManagedAudioLibrary.SteppedLevelSineAnalyzer analyzer;
        private SteppedLevelSineWaveform refWaveform;

        /// <summary>
        /// 构造波幅递增波形分析
        /// </summary>
        public SteppedLevelSineAnalyzer()
        {
            analyzer = new ManagedAudioLibrary.SteppedLevelSineAnalyzer();
            RawAnalyzer = analyzer;
        }

        /// <summary>
        /// 配置分析参数
        /// </summary>
        /// <param name="refWaveform">参考波形</param>
        public void SetAnalyzeParam(SteppedLevelSineWaveform refWaveform)
        {
            analyzer.SetReferenceWaveform(refWaveform.GetRawWaveform() as ManagedAudioLibrary.SteppedLevelSineWaveform);
            this.refWaveform = refWaveform;
            this.RefWaveform = refWaveform;

            IsAnalyzeParamSet = true;
        }

        /// <summary>
        /// 获取测试波形峰峰值
        /// </summary>
        /// <returns></returns>
        public double[] GetPeakToPeak()
        {
            double[] peakToPeak = new double[refWaveform.Steps];
            for (ushort i = 0; i < peakToPeak.Length; i++)
            {
                peakToPeak[i] = analyzer.GetPeakToPeak(i);
            }
            return peakToPeak;
        }

        /// <summary>
        /// 获取测试波形THD
        /// </summary>
        /// <returns></returns>
        public double[] GetTHDInDb()
        {
            double[] thdInDb = new double[refWaveform.Steps];
            for (ushort i = 0; i < thdInDb.Length; i++)
            {
                thdInDb[i] = analyzer.GetThd(i);
            }
            return thdInDb;
        }

        /// <summary>
        /// 获取测试波形噪声比
        /// </summary>
        /// <returns></returns>
        public double[] GetNoiseRatioInDb()
        {
            double[] nrInDb = new double[refWaveform.Steps];
            for (ushort i = 0; i < nrInDb.Length; i++)
            {
                nrInDb[i] = analyzer.GetNoiseRatio(i);
            }
            return nrInDb;
        }

        /// <summary>
        /// 获取测试波形THD和噪声与信号的比
        /// </summary>
        /// <returns></returns>
        public double[] GetTHDPlusNoiseInDb()
        {
            double[] thdPlusNInDb = new double[refWaveform.Steps];
            for (ushort i = 0; i < thdPlusNInDb.Length; i++)
            {
                thdPlusNInDb[i] = analyzer.GetThdPlusN(i);
            }
            return thdPlusNInDb;
        }

        /// <summary>
        /// 获取测试波形有效电平
        /// </summary>
        /// <returns></returns>
        public double[] GetRms()
        {
            double[] rms = new double[refWaveform.Steps];
            for (ushort i = 0; i < rms.Length; i++)
            {
                rms[i] = analyzer.GetRMS(i);
            }
            return rms;
        }

        /// <summary>
        /// 获取测试波形各阶AC的强度
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetACPart()
        {
            double[] orders = new double[refWaveform.Steps];
            double[] acPart = new double[refWaveform.Steps];
            for (ushort i = 0; i < acPart.Length; i++)
            {
                orders[i] = i;
                acPart[i] = analyzer.GetAcPart(i);
            }
            return new ArrayPair<double, double>(orders ,acPart);
        }

        /// <summary>
        /// 获取测试波形各阶DC的强度
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetDCPart()
        {
            double[] orders = new double[refWaveform.Steps];
            double[] dcPart = new double[refWaveform.Steps];
            for (ushort i = 0; i < dcPart.Length; i++)
            {
                orders[i] = i;
                dcPart[i] = analyzer.GetDcPart(i);
            }
            return new ArrayPair<double, double>(orders, dcPart);
        }

        /// <summary>
        /// 获取测试波形各阶的最大值
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetMax()
        {
            double[] orders = new double[refWaveform.Steps];
            double[] max = new double[refWaveform.Steps];
            for (ushort i = 0; i < max.Length; i++)
            {
                orders[i] = i;
                max[i] = analyzer.GetMax(i);
            }
            return new ArrayPair<double, double>(orders, max);
        }

        /// <summary>
        /// 获取测试波形各阶的最小值
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetMin()
        {
            double[] orders = new double[refWaveform.Steps];
            double[] min = new double[refWaveform.Steps];
            for (ushort i = 0; i < min.Length; i++)
            {
                orders[i] = i;
                min[i] = analyzer.GetMin(i);
            }
            return new ArrayPair<double, double>(orders, min);
        }

        /// <summary>
        /// 获取测试波形各阶的功率谱
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double>[] GetPowerSpectrum()
        {
            ArrayPair<double, double>[] spectrums = new ArrayPair<double, double>[refWaveform.Steps];
            for (ushort i = 0; i < spectrums.Length; i++)
            {
                double[] xData = new double[analyzer.GetSpectrumLength((ushort) (i + 1))];
                double[] yData = new double[analyzer.GetSpectrumLength((ushort) (i + 1))];

                spectrums[i] = new ArrayPair<double, double>(xData, yData);
            }

            double maxFrequency = refWaveform.GetSampleRate()/2;

            for (ushort stepIndex = 1; stepIndex < refWaveform.Steps; stepIndex++)
            {
                uint spectrumLength = analyzer.GetSpectrumLength(stepIndex);
                double frequencyStep = maxFrequency/spectrumLength;
                for (int j = 0; j < spectrumLength; j++)
                {
                    spectrums[stepIndex].XData[j] = (j + 1)*frequencyStep;
                }
                analyzer.GetPowerSpectrum(spectrums[stepIndex].YData, stepIndex);
            }
            return spectrums;
        }

        /// <summary>
        /// 获取测试波形各阶的相位谱
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double>[] GetPhaseSpectrum()
        {
            ArrayPair<double, double>[] spectrums = new ArrayPair<double, double>[refWaveform.Steps];
            for (ushort i = 0; i < spectrums.Length; i++)
            {
                double[] xData = new double[analyzer.GetSpectrumLength((ushort)(i + 1))];
                double[] yData = new double[analyzer.GetSpectrumLength((ushort)(i + 1))];

                spectrums[i] = new ArrayPair<double, double>(xData, yData);
            }

            double maxFrequency = refWaveform.GetSampleRate() / 2;

            for (ushort stepIndex = 1; stepIndex < refWaveform.Steps; stepIndex++)
            {
                uint spectrumLength = analyzer.GetSpectrumLength(stepIndex);
                double frequencyStep = maxFrequency / spectrumLength;
                for (int j = 0; j < spectrumLength; j++)
                {
                    spectrums[stepIndex].XData[j] = (j + 1) * frequencyStep;
                }
                analyzer.GetPhaseSpectrum(spectrums[stepIndex].YData, stepIndex);
            }
            return spectrums;
        }
    }
}
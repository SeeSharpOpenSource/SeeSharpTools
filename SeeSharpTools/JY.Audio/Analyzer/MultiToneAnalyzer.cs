using SeeSharpTools.JY.Audio.Common;
using SeeSharpTools.JY.Audio.Waveform;

namespace SeeSharpTools.JY.Audio.Analyzer
{
    /// <summary>
    /// 多音色波形分析
    /// </summary>
    public class MultiToneAnalyzer : AnalyzerBase
    {
        private ManagedAudioLibrary.MultiToneAnalyzer analyzer;

        private MultiToneWaveform refWaveform;

        /// <summary>
        /// 构造多音色波形分析
        /// </summary>
        public MultiToneAnalyzer()
        {
            this.analyzer = new ManagedAudioLibrary.MultiToneAnalyzer();
            RawAnalyzer = analyzer;
        }

        /// <summary>
        /// 配置分析的参数
        /// </summary>
        /// <param name="refWaveform">参考波形</param>
        public void SetAnalyzeParam(MultiToneWaveform refWaveform)
        {
            analyzer.SetReferenceWaveform(refWaveform.GetRawWaveform() as ManagedAudioLibrary.MultiToneWaveform);
            this.refWaveform = refWaveform;
            this.RefWaveform = refWaveform;
//            analyzer.set
            IsAnalyzeParamSet = true;
        }

        /// <summary>
        /// 获取峰峰值
        /// </summary>
        /// <returns></returns>
        public double GetPeakToPeak()
        {
            return analyzer.GetPeakToPeak();
        }

        /// <summary>
        /// 获取有效电平
        /// </summary>
        /// <returns></returns>
        public double GetRms()
        {
            return analyzer.GetRMS();
        }

        /// <summary>
        /// 获取AC的有效电平
        /// </summary>
        /// <returns></returns>
        public double GetACRms()
        {
            return analyzer.GetAcPart();
        }

        /// <summary>
        /// 获取DC的有效电平
        /// </summary>
        /// <returns></returns>
        public double GetDCRms()
        {
            return analyzer.GetDcPart();
        }

        /// <summary>
        /// 获取测试波形最大值
        /// </summary>
        /// <returns></returns>
        public double GetMax()
        {
            return analyzer.GetMax();
        }

        /// <summary>
        /// 获取测试波形最小值
        /// </summary>
        /// <returns></returns>
        public double GetMin()
        {
            return analyzer.GetMin();
        }

        /// <summary>
        /// 获取测试波形的TD+N
        /// </summary>
        /// <returns></returns>
        public double GetTDPlusN()
        {
            return analyzer.GetTdPlusN();
        }

        /// <summary>
        /// 获取每个频率波形的强度
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetACPart()
        {
            double[] frequencies = new double[refWaveform.FrequencyPoints];
            double[] acPart = new double[refWaveform.FrequencyPoints];
            double frequencyStep = (refWaveform.FrequencyPoints <= 1)
                ? 0
                : (refWaveform.FrequencyMax - refWaveform.FrequencyMin)/(refWaveform.FrequencyPoints - 1);
            for (uint i = 0; i < refWaveform.FrequencyPoints; i++)
            {
                frequencies[i] = refWaveform.FrequencyMin + frequencyStep*i;
                acPart[i] = analyzer.GetAcPart((ushort)i);
            }
            return new ArrayPair<double, double>(frequencies, acPart);
        }

        /// <summary>
        /// 获取测试波形功率谱
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetPowerSpectrum()
        {
            uint spectrumLength = analyzer.GetSpectrumLength();
            double[] frequencies = new double[spectrumLength];
            double frequencyStep = refWaveform.SampleRate/(spectrumLength);
            for (int i = 0; i < spectrumLength; i++)
            {
                frequencies[i] = (i + 1)*frequencyStep;
            }
            double[] powerSpectrum = new double[spectrumLength];
            analyzer.GetPowerSpectrum(powerSpectrum);
            return new ArrayPair<double, double>(frequencies, powerSpectrum);
        }

        /// <summary>
        /// 获取测试波形相位谱
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetPhaseSpectrum()
        {
            uint spectrumLength = analyzer.GetSpectrumLength();
            double[] frequencies = new double[spectrumLength];
            double frequencyStep = refWaveform.SampleRate / (spectrumLength);
            for (int i = 0; i < spectrumLength; i++)
            {
                frequencies[i] = (i + 1) * frequencyStep;
            }
            double[] phaseSpectrum = new double[spectrumLength];
            analyzer.GetPhaseSpectrum(phaseSpectrum);
            return new ArrayPair<double, double>(frequencies, phaseSpectrum);
        }


    }
}
using ManagedAudioLibrary;
using SeeSharpTools.JY.Audio.Common;

namespace SeeSharpTools.JY.Audio.Analyzer
{
    /// <summary>
    /// 双音色波形分析
    /// </summary>
    public class DualToneAnalyzer : AnalyzerBase
    {
        private ManagedAudioLibrary.DualToneAnalyzer analyzer;
        /// <summary>
        /// 构造双音色波形分析
        /// </summary>
        public DualToneAnalyzer()
        {
            analyzer = new ManagedAudioLibrary.DualToneAnalyzer();
            RawAnalyzer = analyzer;
        }

        /// <summary>
        /// 配置分析的参数
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="imdType">IMD类型</param>
        /// <param name="frequency1">频率1</param>
        /// <param name="frequency2">频率2</param>
        public void SetAnalyzeParam(double sampleRate, IMDType imdType, double frequency1 = 0, double frequency2 = 0)
        {
            analyzer.SetImdType((ImdType)imdType);
            analyzer.SetDataSampleRate(sampleRate);
            if (frequency1 > 0 && frequency2 > 0)
            {
                analyzer.SetTargetToneFrequency(frequency1, frequency2, sampleRate);
            }
            SampleRate = sampleRate;

            this.IsAnalyzeParamSet = true;
        }

        /// <summary>
        /// 获取频率1
        /// </summary>
        /// <returns></returns>
        public double GetFrequency1()
        {
            CheckIfAnalyzed();
            return analyzer.GetFrequency1();
        }

        /// <summary>
        /// 获取频率2
        /// </summary>
        /// <returns></returns>
        public double GetFrequency2()
        {
            CheckIfAnalyzed();
            return analyzer.GetFrequency2();
        }

        /// <summary>
        /// 获取频率1和频率2波形的波幅比
        /// </summary>
        /// <returns></returns>
        public double GetAmplitudeRatio()
        {
            CheckIfAnalyzed();
            return analyzer.GetAmplitudeRatioF1toF2();
        }

        /// <summary>
        /// 获取IMD
        /// </summary>
        /// <returns></returns>
        public double GetIMDInDb()
        {
            CheckIfAnalyzed();
            return analyzer.GetIMD();
        }

        /// <summary>
        /// 获取AC的有效电平
        /// </summary>
        /// <returns></returns>
        public double GetACRms()
        {
            CheckIfAnalyzed();
            return analyzer.GetAcPart();
        }

        /// <summary>
        /// 获取DC的有效电平
        /// </summary>
        /// <returns></returns>
        public double GetDCRms()
        {
            CheckIfAnalyzed();
            return analyzer.GetDcPart();
        }

        /// <summary>
        /// 获取测试波形最大点
        /// </summary>
        /// <returns></returns>
        public double GetMax()
        {
            CheckIfAnalyzed();
            return analyzer.GetMax();
        }

        /// <summary>
        /// 获取测试波形最小点
        /// </summary>
        /// <returns></returns>
        public double GetMin()
        {
            CheckIfAnalyzed();
            return analyzer.GetMin();
        }

        /// <summary>
        /// 获取测试波形峰峰值
        /// </summary>
        /// <returns></returns>
        public double GetPeakToPeak()
        {
            return analyzer.GetPeakToPeak();
        }

        /// <summary>
        /// 获取测试波形的相位谱
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetPhaseSpectrum()
        {
            CheckIfAnalyzed();
            uint spectrumLength = analyzer.GetSpectrumLength();
            double maxFrequency = SampleRate/2;
            double[] frequencies = new double[spectrumLength];
            double[] phaseSpectrum = new double[spectrumLength];
            for (int i = 0; i < spectrumLength; i++)
            {
                frequencies[i] = maxFrequency*spectrumLength/(i + 1);
            }
            analyzer.GetPhaseSpectrum(phaseSpectrum);
            return new ArrayPair<double, double>(frequencies, phaseSpectrum);            
        }

        /// <summary>
        /// 获取测试波形的功率谱
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetPowerSpectrum()
        {
            CheckIfAnalyzed();
            uint spectrumLength = analyzer.GetSpectrumLength();
            double maxFrequency = SampleRate / 2;
            double[] frequencies = new double[spectrumLength];
            double[] powerpectrum = new double[spectrumLength];
            for (int i = 0; i < spectrumLength; i++)
            {
                frequencies[i] = maxFrequency * spectrumLength / (i + 1);
            }
            analyzer.GetPhaseSpectrum(powerpectrum);
            return new ArrayPair<double, double>(frequencies, powerpectrum);
        }

        /// <summary>
        /// 获取波形的有效电平
        /// </summary>
        /// <returns></returns>
        public double GetRms()
        {
            CheckIfAnalyzed();
            return analyzer.GetRMS();
        }

    }
}
using SeeSharpTools.JY.Audio.Common;

namespace SeeSharpTools.JY.Audio.Analyzer
{
    /// <summary>
    /// 单音色波形分析
    /// </summary>
    public class SingleToneAnalyzer : AnalyzerBase
    {
        private ManagedAudioLibrary.SingleToneAnalyzer analyzer;

        /// <summary>
        /// 构造单音色波形分析
        /// </summary>
        public SingleToneAnalyzer()
        {
            analyzer = new ManagedAudioLibrary.SingleToneAnalyzer();
            RawAnalyzer = analyzer;
        }

        /// <summary>
        /// 配置分析参数
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="targetFrequency">目标频率</param>
        public void SetAnalyzeParam(double sampleRate, double targetFrequency = 0)
        {
            if (targetFrequency >= 0)
            {
                analyzer.SetTargetToneFrequency(targetFrequency, sampleRate);
            }
            else
            {
                analyzer.SetDataSampleRate(sampleRate);
            }
            
            SampleRate = sampleRate;

            this.IsAnalyzeParamSet = true;
        }

        /// <summary>
        /// 获取测试波形峰峰值
        /// </summary>
        /// <returns></returns>
        public double GetPeakToPeak()
        {
            CheckIfAnalyzed();
            return analyzer.GetPeakToPeak();
        }

        /// <summary>
        /// 获取测试波形THD
        /// </summary>
        /// <returns></returns>
        public double GetTHDInDb()
        {
            CheckIfAnalyzed();
            return analyzer.GetThd();
        }

        /// <summary>
        /// 获取噪声比
        /// </summary>
        /// <returns></returns>
        public double GetNoiseRatioInDb()
        {
            CheckIfAnalyzed();
            return analyzer.GetNoiseRatio();
        }

        /// <summary>
        /// 获取THD和噪声与信号的比
        /// </summary>
        /// <returns></returns>
        public double GetTHDPlusNoiseRatioInDb()
        {
            CheckIfAnalyzed();
            return analyzer.GetThdPlusN();
        }

        /// <summary>
        /// 获取各阶谐波的功率
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetHarmonicPower()
        {
            CheckIfAnalyzed();
            const int harmonicOrderCount = 6;
            double[] harmonicOrder = new double[harmonicOrderCount];
            double[] harmonicPowers = new double[harmonicOrderCount];
            for (ushort i = 0; i < harmonicOrderCount; i++)
            {
                harmonicOrder[i] = i;
                harmonicPowers[i] = analyzer.GetHarmonicPower(i);
            }
            return new ArrayPair<double, double>(harmonicOrder, harmonicPowers);
        }

    }
}
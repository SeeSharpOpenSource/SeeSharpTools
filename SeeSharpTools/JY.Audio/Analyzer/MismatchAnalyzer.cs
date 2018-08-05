namespace SeeSharpTools.JY.Audio.Analyzer
{
    /// <summary>
    /// 波形不匹配度分析
    /// </summary>
    public class MismatchAnalyzer : AnalyzerBase
    {
        private ManagedAudioLibrary.MismatchAnalyzer analyzer;
        /// <summary>
        /// 构造波形不匹配度分析
        /// </summary>
        public MismatchAnalyzer()
        {
            analyzer = new ManagedAudioLibrary.MismatchAnalyzer();
            RawAnalyzer = analyzer;
        }

        /// <summary>
        /// 配置分析的参数
        /// </summary>
        /// <param name="refData">参考信号</param>
        /// <param name="sampleDelay">参考信号前的无效样点数</param>
        /// <param name="dataSize">有效参考信号长度</param>
        /// <param name="targetFrequency">目标频率</param>
        /// <param name="sampleRate">采样率</param>
        public void SetAnalyzeParam(double[] refData, uint sampleDelay = 0, 
            uint dataSize = 0, double targetFrequency = 0, double sampleRate = 0)
        {
            double[] validRefData = GetValidTestData(refData, ref dataSize, sampleDelay);
            analyzer.SetReferenceData(validRefData);
            if (targetFrequency > 0 && sampleRate > 0)
            {
                analyzer.SetTargetToneFrequency(targetFrequency, sampleRate);
            }

            IsAnalyzeParamSet = true;
        }

        /// <summary>
        /// 获取增益不匹配度
        /// </summary>
        /// <returns></returns>
        public double GetGainMismatch()
        {
            CheckIfAnalyzed();
            return analyzer.GetGainMismatch();
        }

        /// <summary>
        /// 获取相位不匹配度
        /// </summary>
        /// <returns></returns>
        public double GetPhaseMismatch()
        {
            CheckIfAnalyzed();
            return analyzer.GetPhaseMismatch();
        }
    }
}

using ManagedAudioLibrary;
using SeeSharpTools.JY.Audio.Common;

namespace SeeSharpTools.JY.Audio.Analyzer
{
    /// <summary>
    /// 啁啾波形分析
    /// </summary>
    public class LogChirpAnalyzer : AnalyzerBase
    {
        private ManagedAudioLibrary.LogChirpAnalyzer analyzer;

        /// <summary>
        /// 构造啁啾波形分析
        /// </summary>
        public LogChirpAnalyzer()
        {
            analyzer = new ManagedAudioLibrary.LogChirpAnalyzer();
            RawAnalyzer = analyzer;
        }

        /// <summary>
        /// 配置分析的参数
        /// </summary>
        /// <param name="refWaveform">参考波形</param>
        /// <param name="sampleRate">采样率</param>
        public void SetAnalyzeParam(Waveform.LogChirpWaveform refWaveform, double sampleRate)
        {
            analyzer.SetReferenceWaveform((LogChirpWaveform) refWaveform.GetRawWaveform());
            analyzer.SetDataSampleRate(sampleRate);
            RefWaveform = refWaveform;

            IsAnalyzeParamSet = true;
        }

        /// <summary>
        /// 分析波形
        /// </summary>
        /// <param name="testData">测试波形</param>
        /// <param name="sampleDelay">测试数据前面的无效数据长度</param>
        /// <param name="dataSize">有效测试波形长度</param>
        public override void Analyze(double[] testData, uint sampleDelay = 0, uint dataSize = 0)
        {
            double[] validTestData = GetValidTestData(testData, ref dataSize, sampleDelay);
            uint pathDelay = AudioAnalyzer.AnalyzePathDelay(validTestData, RefWaveform.GetWaveData());
            //analyzer.SetDataDelayCounts(pathDelay);
            base.Analyze(validTestData, 0, 0);
        }

        /// <summary>
        /// 获取THD
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetTHD()
        {
            CheckIfAnalyzed();
            double[] thdX = new double[analyzer.GetLengthOfThd()];
            double[] thdY = new double[analyzer.GetLengthOfThd()];
            analyzer.GetThd(thdX, thdY);
            return new ArrayPair<double, double>(thdX, thdY);
        }

        /// <summary>
        /// 获取频率响应
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetResponse()
        {
            CheckIfAnalyzed();
            double[] responseX = new double[analyzer.GetLengthOfResponse()];
            double[] responseY = new double[analyzer.GetLengthOfResponse()];
            analyzer.GetResponse(responseX, responseY);
            return new ArrayPair<double, double>(responseX, responseY);
        }
    }
}
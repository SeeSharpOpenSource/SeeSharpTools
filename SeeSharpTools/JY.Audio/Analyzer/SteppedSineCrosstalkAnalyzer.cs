using System;
using ManagedAudioLibrary;
using SeeSharpTools.JY.Audio.Common;

namespace SeeSharpTools.JY.Audio.Analyzer
{
    /// <summary>
    /// 频率阶梯递增串扰分析
    /// </summary>
    public class SteppedSineCrosstalkAnalyzer : AnalyzerBase
    {
        private ManagedAudioLibrary.SteppedSineCrosstalkAnalyzer analyzer;

        /// <summary>
        /// 构造频率阶梯递增串扰分析
        /// </summary>
        public SteppedSineCrosstalkAnalyzer()
        {
            analyzer = new ManagedAudioLibrary.SteppedSineCrosstalkAnalyzer();
            RawAnalyzer = analyzer;
        }

        /// <summary>
        /// 配置分析参数
        /// </summary>
        /// <param name="refWaveform">参考波形</param>
        /// <param name="sampleRate">采样率</param>
        public void SetAnalyzeParam(Waveform.SteppedSineWaveform refWaveform,double[] referenceData, double sampleRate, uint sampleDelay = 0, uint dataSize = 0)
        {
            analyzer.SetDataSampleRate(sampleRate);
            double[] validReferenceData = GetValidTestData(referenceData, ref dataSize, sampleDelay);
            analyzer.SetReferenceData(validReferenceData);
            analyzer.SetReferenceWaveform((SteppedSineWaveform) refWaveform.GetRawWaveform());

            SampleRate = sampleRate;

            this.RefWaveform = refWaveform;
            this.IsAnalyzeParamSet = true;
        }

        /// <summary>
        /// 获取测试波形各阶频率的串扰
        /// </summary>
        /// <returns></returns>
        public ArrayPair<double, double> GetCrossTalk()
        {
            CheckIfAnalyzed();
            Waveform.SteppedSineWaveform refWaveform = RefWaveform as Waveform.SteppedSineWaveform;
            double[] frequency = new double[refWaveform.Steps];
            double[] crossTalk = new double[refWaveform.Steps];
            for (ushort index = 0; index < refWaveform.Steps; index++)
            {
                frequency[index] = refWaveform.GetFrequencyPoint(index);
                crossTalk[index] = analyzer.GetCrosstalk(index);
            }
            return new ArrayPair<double, double>(frequency, crossTalk);
        } 
    }
}
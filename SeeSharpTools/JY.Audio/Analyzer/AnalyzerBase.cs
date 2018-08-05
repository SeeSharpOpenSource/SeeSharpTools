using System;
using ManagedAudioLibrary;
using SeeSharpTools.JY.Audio.Common;
using SeeSharpTools.JY.Audio.Common.i18n;
using SeeSharpTools.JY.Audio.Waveform;

namespace SeeSharpTools.JY.Audio.Analyzer
{
    /// <summary>
    /// 波形分析基类
    /// </summary>
    public abstract class AnalyzerBase : IDisposable
    {
        protected DataAnalyzer RawAnalyzer;

        /// <summary>
        /// 参考波形，如果没有则为null
        /// </summary>
        public WaveformBase RefWaveform { get; protected set; }

        protected bool IsAnalyzeParamSet = false;
        protected bool IsAnalyzed = false;
        protected uint DataSize = 0;
        protected double SampleRate = 0;

        internal I18nEntity i18n = I18nEntity.GetInstance(I18nLocalWrapper.Name);

        /// <summary>
        /// 分析波形
        /// </summary>
        /// <param name="testData">测试波形</param>
        /// <param name="sampleDelay">测试数据前面的无效数据长度</param>
        /// <param name="dataSize">有效测试波形长度</param>
        public virtual void Analyze(double[] testData, uint sampleDelay = 0, uint dataSize = 0)
        {
            if (!IsAnalyzeParamSet)
            {
                throw new SeeSharpAudioException(SeeSharpAudioErrorCode.AnalyzeParamNotSet,
                    i18n.GetStr("Runtime.AnalyzeBeforeSetParam"));
            }
            double[] validTestData = GetValidTestData(testData, ref dataSize, sampleDelay);
            try
            {
                RawAnalyzer.Analyze(validTestData, dataSize);
                DataSize = dataSize;
                IsAnalyzed = true;
            }
            catch (Exception ex)
            {
                throw new SeeSharpAudioException(SeeSharpAudioErrorCode.RuntimeError,
                    i18n.GetFStr("Runtime.RuntimeError", ex.Message), ex);
            }
        }

        protected double[] GetValidTestData(double[] testData, ref uint dataSize, uint sampleDelay)
        {
//            if (0 == sampleDelay)
//            {
//                sampleDelay = AudioAnalyzer.AnalyzePathDelay(testData, RefWaveform.GetWaveData());
//            }
            //RawAnalyzer?.SetDataDelayCounts(sampleDelay);

            if (sampleDelay >= testData.Length)
            {
                throw new SeeSharpAudioException(SeeSharpAudioErrorCode.InvalidDataSize,
                    i18n.GetStr("ParamCheck.InvalidAnalyzeSize"));
            }
            if (0 == dataSize || dataSize + sampleDelay > testData.Length)
            {
                dataSize = (uint)(testData.Length - sampleDelay);
            }

            if (0 == sampleDelay && dataSize == testData.Length)
            {
                return testData;
            }

            double[] tmpData = new double[dataSize];
            Buffer.BlockCopy(testData, (int)(sampleDelay * sizeof(double)), tmpData, 0, 
                (int)(dataSize * sizeof(double)));
            return tmpData;
        }


        protected void CheckIfAnalyzed()
        {
            if (!IsAnalyzed)
            {
                throw new SeeSharpAudioException(SeeSharpAudioErrorCode.AnalyzeNotPerformed, 
                    i18n.GetStr("Runtime.AnalyzedNotPerformed"));
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            ReleaseResouce(RawAnalyzer);
            ReleaseResouce(RefWaveform);
        }

        internal static void ReleaseResouce(IDisposable resource)
        {
            try
            {
                resource?.Dispose();
            }
            catch (Exception)
            {
                //igore
            }
        }
    }
}
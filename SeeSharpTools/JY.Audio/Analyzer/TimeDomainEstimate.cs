using System;
using ManagedAudioLibrary;
using SeeSharpTools.JY.Audio.Common;

namespace SeeSharpTools.JY.Audio.Analyzer
{
    /// <summary>
    /// 时间域分析
    /// </summary>
    public class TimeDomainEstimate : AnalyzerBase
    {
        private TimeDomainEstimator analyzer;

        // TimeDomainEstimate没有继承DataAnalyzer类，需要特殊处理
        /// <summary>
        /// 构造时间域分析
        /// </summary>
        public TimeDomainEstimate()
        {
            analyzer = new TimeDomainEstimator();
//            RawAnalyzer = analyzer;

            IsAnalyzeParamSet = true;
        }

        /// <summary>
        /// 配置分析参数
        /// </summary>
        public void SetAnalyzeParam()
        {
            // NOT NEED, just for interface
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
            try
            {
                analyzer.Estimate(validTestData, dataSize);
                DataSize = dataSize;
                IsAnalyzed = true;
            }
            catch (Exception ex)
            {
                throw new SeeSharpAudioException(SeeSharpAudioErrorCode.RuntimeError,
                    i18n.GetFStr("Runtime.RuntimeError", ex.Message), ex);
            }
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
        /// 获取测试波形有效电平
        /// </summary>
        /// <returns></returns>
        public double GetRms()
        {
            return analyzer.GetRMS();
        }

        /// <summary>
        /// 获取测试波形DC有效电平
        /// </summary>
        /// <returns></returns>
        public double GetDCRms()
        {
            return analyzer.GetDcPart();
        }

        /// <summary>
        /// 获取测试波形AC有效电平
        /// </summary>
        /// <returns></returns>
        public double GetACRms()
        {
            return analyzer.GetAcPart();
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
    }
}
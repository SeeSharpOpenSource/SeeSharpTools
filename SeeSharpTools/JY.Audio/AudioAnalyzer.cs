using System;
using System.Linq;
using ManagedAudioLibrary;
using SeeSharpTools.JY.Audio.Common;
using LogChirpWaveform = SeeSharpTools.JY.Audio.Waveform.LogChirpWaveform;
using MultiToneWaveform = SeeSharpTools.JY.Audio.Waveform.MultiToneWaveform;
using SteppedLevelSineWaveform = SeeSharpTools.JY.Audio.Waveform.SteppedLevelSineWaveform;

namespace SeeSharpTools.JY.Audio
{
    public static class AudioAnalyzer
    {
        /// <summary>
        /// 分析SteppedSine波形的串扰
        /// </summary>
        /// <param name="refWaveForm">参考波形</param>
        /// <param name="testWaveform">测试波形</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="maxFrequency">波形最大频率</param>
        /// <param name="minFrequency">波形最低频率</param>
        /// <param name="steps">阶数</param>
        /// <param name="minCycles">最小周期</param>
        /// <param name="minDuration">最小时间</param>
        /// <param name="amplitude">振幅</param>
        /// <param name="isLog">是否对数波形</param>
        /// <returns>返回串扰值，X和Y分别为各自的频率和对应的串扰值</returns>
        public static ArrayPair<double, double> AnalyzeSteppedSineCrosstalk(double[] refWaveForm, 
            double[] testWaveform, double sampleRate, double maxFrequency, double minFrequency, 
            ushort steps, ushort minCycles, double minDuration, double amplitude, bool isLog = false)
        {
//            const double refDataSampleRate = 96000.0;
//            const double minDuration = 40.0 / 1000.0;
//            const ushort minCycles = 40;
//            const double amplitude = 1;
//
            SteppedSineWaveform steppedSineWaveform = new SteppedSineWaveform();
            steppedSineWaveform.CreateData(1, minFrequency, maxFrequency, steps, false, false, minCycles,
                minDuration, sampleRate);
            double[] refData = new double[steppedSineWaveform.GetTotalPoints()];
            steppedSineWaveform.GetCopyOfData(refData);
            double[] testData = new double[refWaveForm.Length + testWaveform.Length];
            Buffer.BlockCopy(refWaveForm, 0, testData, 0, refWaveForm.Length * sizeof(double));
            Buffer.BlockCopy(testWaveform, 0, testData, refWaveForm.Length * sizeof(double), 
                testWaveform.Length * sizeof(double));

            SteppedSineCrosstalkAnalyzer analyzer = new SteppedSineCrosstalkAnalyzer();
//            pathDelay = 6000;
            analyzer.SetDataSampleRate(sampleRate);
            uint pathDelay = AnalyzePathDelay(testData, refData.Take(refWaveForm.Length).ToArray());
            analyzer.SetDataDelayCounts(pathDelay);
            analyzer.SetReferenceData(testData);
            analyzer.SetReferenceWaveform(steppedSineWaveform);
            analyzer.Analyze(testWaveform, (uint) testWaveform.Length);
            double[] frequency = new double[steppedSineWaveform.GetNumberOfSteps()];
            double[] crossTalk = new double[steppedSineWaveform.GetNumberOfSteps()];
            for (ushort index = 0; index < steppedSineWaveform.GetNumberOfSteps(); index++)
            {
                frequency[index] = steppedSineWaveform.GetFrequencyPoint(index);
                crossTalk[index] = analyzer.GetCrosstalk(index);
            }
            return new ArrayPair<double, double>(frequency, crossTalk);
        }

        /// <summary>
        /// 分析SteppedSineWave波形串扰
        /// </summary>
        /// <param name="testWaveform">测试波形</param>
        /// <param name="refWaveform">参考波形</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="maxFrequency">波形最大频率</param>
        /// <param name="minFrequency">波形最低频率</param>
        /// <param name="steps">阶数</param>
        /// <param name="minCycles">最小周期</param>
        /// <param name="minDuration">最小时间</param>
        /// <param name="amplitude">振幅</param>
        /// <param name="isLog">是否对数波形</param>
        /// <returns>返回串扰值，X和Y分别为各自的频率和对应的串扰值</returns>
        public static ArrayPair<double, double> AnalyzeSteppedSineCrosstalk(double[] testWaveform, 
            Waveform.SteppedSineWaveform refWaveform, double sampleRate, double maxFrequency, 
            double minFrequency, ushort steps, ushort minCycles, double minDuration, 
            double amplitude, bool isLog = false)
        {
            SteppedSineCrosstalkAnalyzer analyzer = new SteppedSineCrosstalkAnalyzer();
            //            pathDelay = 6000;
            analyzer.SetDataSampleRate(sampleRate);
            double[] refWaveData = refWaveform.GetWaveData();
            uint pathDelay = AnalyzePathDelay(testWaveform, refWaveData);
            analyzer.SetDataDelayCounts(pathDelay);
            analyzer.SetReferenceData(refWaveData);
            analyzer.SetReferenceWaveform((SteppedSineWaveform) refWaveform.GetRawWaveform());
            analyzer.Analyze(testWaveform, (uint)testWaveform.Length);

            double[] frequency = new double[refWaveform.Steps];
            double[] crossTalk = new double[refWaveform.Steps];
            for (ushort index = 0; index < refWaveform.Steps; index++)
            {
                frequency[index] = refWaveform.GetFrequencyPoint(index);
                crossTalk[index] = analyzer.GetCrosstalk(index);
            }
            return new ArrayPair<double, double>(frequency, crossTalk);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waveForm">测试波形</param>
        /// <param name="refData">参考波形</param>
        /// <param name="waveFormLength">测试波形长度</param>
        /// <param name="refDataLength">参考波形长度</param>
        /// <returns></returns>
        public static uint AnalyzePathDelay(double[] waveForm, double[] refData, 
            uint waveFormLength = 0, uint refDataLength = 0)
        {
            if (0 == waveFormLength)
            {
                waveFormLength = (uint) waveForm.Length;
            }
            if (0 == refDataLength)
            {
                refDataLength = (uint) refData.Length;
            }

            return DelayEstimatorUtility.Estimate(waveForm, refData, waveFormLength, refDataLength);
        }

        /// <summary>
        /// 分析单音色波形
        /// </summary>
        /// <param name="waveform">测试波形</param>
        /// <param name="targetFreq">目标频率</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="amplitudeInVol">波幅</param>
        /// <param name="thdInDb">谐波失真(dB)</param>
        /// <param name="nrInDb">噪声比(dB)</param>
        /// <param name="thdPlusNRatioInDb">噪声和谐波失真与信号的比(dB)</param>
        /// <param name="harmonicPower">谐波分量功率</param>
        public static void AnalyzeSingleToneWave(double[] waveform, double targetFreq, double sampleRate, 
            out double amplitudeInVol, out double thdInDb, out double nrInDb, out double thdPlusNRatioInDb, 
            out double[] harmonicPower)
        {
            const ushort harmonicOrder = 6;
            SingleToneAnalyzer analyzer = new SingleToneAnalyzer();
            analyzer.SetTargetToneFrequency(targetFreq, sampleRate);
            analyzer.Analyze(waveform, (uint) waveform.Length);
            amplitudeInVol = analyzer.GetPeakToPeak();
            thdInDb = analyzer.GetThd();
            nrInDb = analyzer.GetNoiseRatio();
            thdPlusNRatioInDb = analyzer.GetThdPlusN();
            harmonicPower = new double[harmonicOrder];
            for (ushort i = 0; i < harmonicOrder; i++)
            {
                harmonicPower[i] = analyzer.GetHarmonicPower(i);
            }
        }

        /// <summary>
        /// 分析波形不匹配度
        /// </summary>
        /// <param name="waveform">测试波形</param>
        /// <param name="refData">参考波形</param>
        /// <param name="gainMismatch">增益比</param>
        /// <param name="phaseMismatch">相移</param>
        public static void AnalyzeMismatch(double[] waveform, double[] refData, out double gainMismatch, 
            out double phaseMismatch)
        {
            MismatchAnalyzer analyzer = new MismatchAnalyzer();
            analyzer.SetReferenceData(refData);
//            analyzer.SetDataDelayCounts(AnalyzePathDelay(waveform, refData));
            uint dataLength = (uint) (waveform.Length >= refData.Length ? refData.Length : waveform.Length);
            uint pathDelay = AnalyzePathDelay(waveform, refData);
            analyzer.Analyze(waveform, dataLength);
            analyzer.SetDataDelayCounts(pathDelay);
            gainMismatch = analyzer.GetGainMismatch();
            phaseMismatch = analyzer.GetPhaseMismatch();
        }

        /// <summary>
        /// 分析双音色波形
        /// </summary>
        /// <param name="testData">测试波形</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="imdType">IMD类型</param>
        /// <param name="frequency1">第一频率</param>
        /// <param name="frequency2">第二频率</param>
        /// <param name="amplitudeRatio">幅度比</param>
        /// <param name="IMDInDb">IMD数值(dB)</param>
        public static void AnalyzeDualToneWave(double[] testData, double sampleRate, IMDType imdType, out double frequency1,
            out double frequency2, out double amplitudeRatio, out double IMDInDb)
        {
            DualToneAnalyzer analyzer = new DualToneAnalyzer();
            analyzer.SetImdType((ImdType) imdType);
            analyzer.SetDataSampleRate(sampleRate);
            analyzer.Analyze(testData, (uint) testData.Length);
            frequency1 = analyzer.GetFrequency1();
            frequency2 = analyzer.GetFrequency2();
            amplitudeRatio = analyzer.GetAmplitudeRatioF1toF2();
            IMDInDb = analyzer.GetIMD();
        }

//        public static void AnalyzeMultiToneWave(double[] testWave)
//        {
//            
//        }
        /// <summary>
        /// 分析LogChirp波形
        /// </summary>
        /// <param name="testData">测试波形</param>
        /// <param name="refWaveform">参考波形</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="responseX">频响的频率值</param>
        /// <param name="responseY">频响的功率值</param>
        /// <param name="thdX">谐波失真的频率值</param>
        /// <param name="thdY">谐波失真的值</param>
        public static void AnalyzeLogChripWave(double[] testData, Waveform.LogChirpWaveform refWaveform, double sampleRate, 
            out double[] responseX, out double[] responseY, out double[] thdX, out double[] thdY)
        {
            LogChirpAnalyzer analyzer = new LogChirpAnalyzer();
            ManagedAudioLibrary.LogChirpWaveform rawWaveform =
                refWaveform.GetRawWaveform() as ManagedAudioLibrary.LogChirpWaveform;
            analyzer.SetDataDelayCounts(AnalyzePathDelay(testData, refWaveform.GetWaveData()));
            analyzer.SetDataSampleRate(sampleRate);
            analyzer.SetReferenceWaveform(rawWaveform);
            analyzer.Analyze(testData, (uint) testData.Length);
            responseX = new double[analyzer.GetLengthOfResponse()];
            responseY = new double[analyzer.GetLengthOfResponse()];
            thdX = new double[analyzer.GetLengthOfThd()];
            thdY = new double[analyzer.GetLengthOfThd()];
            analyzer.GetThd(thdX, thdY);
            analyzer.GetResponse(responseX, responseY);
        }

        /// <summary>
        /// 分析SteppedLevelSine波形
        /// </summary>
        /// <param name="testData">测试波形</param>
        /// <param name="refWaveform">参考波形</param>
        /// <param name="peakToPeak">峰峰值</param>
        /// <param name="thdInDb">谐波失真(dB)</param>
        /// <param name="nrInDb">噪声比(dB)</param>
        /// <param name="thdPlusNInDb">谐波失真和噪声与信号的比(dB)</param>
        /// <param name="rms">有效电平</param>
        public static void AnalyzeSteppedLevelSineWaveform(double[] testData, Waveform.SteppedLevelSineWaveform refWaveform, 
            out double[] peakToPeak, out double[] thdInDb, out double[] nrInDb, out double[] thdPlusNInDb, out double[] rms)
        {

            SteppedLevelSineAnalyzer analyzer = new SteppedLevelSineAnalyzer();
            ManagedAudioLibrary.SteppedLevelSineWaveform rawWaveform =
                refWaveform.GetRawWaveform() as ManagedAudioLibrary.SteppedLevelSineWaveform;
            analyzer.SetReferenceWaveform(rawWaveform);

            double[] refData = refWaveform.GetWaveData();
            uint pathDelay = AnalyzePathDelay(testData, refData);
            analyzer.SetDataDelayCounts(pathDelay);
            analyzer.Analyze(testData, (uint) testData.Length);


            peakToPeak = new double[rawWaveform.GetNumberOfSteps()];
            thdInDb = new double[rawWaveform.GetNumberOfSteps()];
            nrInDb = new double[rawWaveform.GetNumberOfSteps()];
            thdPlusNInDb = new double[rawWaveform.GetNumberOfSteps()];
            rms = new double[rawWaveform.GetNumberOfSteps()];

            for (ushort i = 0; i < rawWaveform.GetNumberOfSteps(); i++)
            {
                peakToPeak[i] = analyzer.GetPeakToPeak(i);
                thdInDb[i] = analyzer.GetThd(i);
                nrInDb[i] = analyzer.GetNoiseRatio(i);
                thdPlusNInDb[i] = analyzer.GetThdPlusN(i);
                rms[i] = analyzer.GetRMS(i);
            }
        }

        /// <summary>
        /// 时域分析
        /// </summary>
        /// <param name="testData">测试波形</param>
        /// <param name="peakToPeak">峰峰值</param>
        /// <param name="rms">有效电平</param>
        /// <param name="dc">直流信号</param>
        /// <param name="rmsOfAc">AC的有效电平</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        public static void TimeDomainEstimate(double[] testData, out double peakToPeak, out double rms, out double dc,
            out double rmsOfAc, out double max, out double min)
        {
            TimeDomainEstimator estimator = new TimeDomainEstimator();
            estimator.Estimate(testData, (uint) testData.Length);
            peakToPeak = estimator.GetPeakToPeak();
            rms = estimator.GetRMS();
            dc = estimator.GetDcPart();
            rmsOfAc = estimator.GetAcPart();
            max = estimator.GetMax();
            min = estimator.GetMin();
        }

        /// <summary>
        /// 分析多音色波形
        /// </summary>
        /// <param name="testData">测试波形</param>
        /// <param name="refWaveform">参考波形</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="peakToPeak">峰峰值</param>
        /// <param name="acRms">交流有效电平</param>
        /// <param name="dcRms">直流有效电平</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <param name="tdPlusN">失真和噪声与信号比</param>
        /// <param name="acPart">AC部分</param>
        /// <param name="powerSpectrum">功率谱值</param>
        /// <param name="phaseSpectrum">相位谱</param>
        public static void AnalyzeMultiToneWaveform(double[] testData, MultiToneWaveform refWaveform, 
            double sampleRate, out double peakToPeak, out double acRms, out double dcRms, 
            out double max, out double min, out double tdPlusN, out double[] acPart, 
            out double[] powerSpectrum, out double[] phaseSpectrum)
        {
            MultiToneAnalyzer analyzer = new MultiToneAnalyzer();
            analyzer.SetReferenceWaveform((ManagedAudioLibrary.MultiToneWaveform) refWaveform.GetRawWaveform());
//            uint pathDelay = AnalyzePathDelay(testData, refWaveform.GetWaveData());
//            analyzer.SetDataDelayCounts(pathDelay);
            analyzer.SetDataSampleRate(sampleRate);
            analyzer.Analyze(testData, (uint) testData.Length);
            
            peakToPeak = analyzer.GetPeakToPeak();
            acRms = analyzer.GetAcPart();
            dcRms = analyzer.GetDcPart();
            max = analyzer.GetMax();
            min = analyzer.GetMin();
            tdPlusN = analyzer.GetTdPlusN();

            acPart = new double[refWaveform.FrequencyPoints];
            for (uint i = 0; i < refWaveform.FrequencyPoints; i++)
            {
                acPart[i] = analyzer.GetAcPart((ushort) i);
            }

            uint spectrumLength = analyzer.GetSpectrumLength();
            powerSpectrum = new double[spectrumLength];
            phaseSpectrum = new double[spectrumLength];
            analyzer.GetPowerSpectrum(powerSpectrum);
            analyzer.GetPhaseSpectrum(phaseSpectrum);
        }
    }

    /// <summary>
    /// IMD类型
    /// </summary>
    public enum IMDType
    {
        /// <summary>
        /// CCIF
        /// </summary>
        CCIF = ImdType.ccif,
        /// <summary>
        /// SMPTE
        /// </summary>
        SMPTE = ImdType.smpte
    }
}
using System;

namespace SeeSharpTools.JY.Audio.Waveform
{
    /// <summary>
    /// 波形类的基类
    /// </summary>
    public abstract class WaveformBase : IDisposable
    {
        /// <summary>
        /// 采样率
        /// </summary>
        public double SampleRate { protected set; get; }

        /// <summary>
        /// 最大波幅
        /// </summary>
        public double Amplitude { protected set; get; }

        protected ManagedAudioLibrary.Waveform RawWaveform;

        /// <summary>
        /// 获取波形采样率
        /// </summary>
        /// <returns>波形采样率</returns>
        public double GetSampleRate()
        {
            return SampleRate;
        }

        /// <summary>
        /// 获取最大波幅
        /// </summary>
        /// <returns>最大波幅</returns>
        public double GetAmplitude()
        {
            return Amplitude;
        }

        internal object GetRawWaveform()
        {
            return RawWaveform;
        }

        /// <summary>
        /// 获取double数组类型的波形数据
        /// </summary>
        /// <returns>double数组类型的波形数据</returns>
        public double[] GetWaveData()
        {
            double[] waveData = new double[RawWaveform.GetTotalPoints()];
            RawWaveform.GetCopyOfData(waveData);
            return waveData;
        }

        /// <summary>
        /// 获取通道总数
        /// </summary>
        /// <returns>通道总数</returns>
        public uint GetChannelCount()
        {
            return RawWaveform.GetChannelCounts();
        }

        /// <summary>
        /// 获取各通道样点数
        /// </summary>
        /// <returns>各通道样点数</returns>
        public uint GetSamplesPerChannel()
        {
            if (RawWaveform.GetChannelCounts() <= 0)
            {
                return 0;
            }
            return RawWaveform.GetTotalPoints() / RawWaveform.GetChannelCounts();
        }

        /// <summary>
        /// 获取波形数据的总长度
        /// </summary>
        /// <returns>波形数据总长度</returns>
        public uint GetTotalPoints()
        {
            return RawWaveform.GetTotalPoints();
        }

        public void Dispose()
        {
            RawWaveform?.Dispose();
        }
    }
}
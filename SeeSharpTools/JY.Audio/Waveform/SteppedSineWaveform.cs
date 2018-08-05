
using System;
using SeeSharpTools.JY.Audio.Equilizer;

namespace SeeSharpTools.JY.Audio.Waveform
{
    public class SteppedSineWaveform : WaveformBase
    {
        /// <summary>
        /// 最大频率
        /// </summary>
        public double FrequencyMax { protected set; get; }
        /// <summary>
        /// 最小频率
        /// </summary>
        public double FrequencyMin { protected set; get; }
        /// <summary>
        /// 频率阶数
        /// </summary>
        public ushort Steps { protected set; get; }
        /// <summary>
        /// 最小周期数
        /// </summary>
        public ushort MinCycle { protected set; get; }
        /// <summary>
        /// 单阶最小持续时间
        /// </summary>
        public double MinDuration { protected set; get; }
        /// <summary>
        /// 是否对数波形
        /// </summary>
        public bool IsLog { protected set; get; }
        /// <summary>
        /// 是否反转波形
        /// </summary>
        public bool IsInverse { protected set; get; }

        /// <summary>
        /// 构造SteppedSineWaveform
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="amplitude">波形幅度</param>
        /// <param name="frequencyMin">最小频率</param>
        /// <param name="frequencyMax">最大频率</param>
        /// <param name="steps">频率阶数</param>
        /// <param name="minCycle">最小周期数</param>
        /// <param name="minDuration">单阶最小时间</param>
        /// <param name="isLog">是否对数波形</param>
        /// <param name="isInverse">是否反转</param>
        public SteppedSineWaveform(double sampleRate, double amplitude,
            double frequencyMin, double frequencyMax, ushort steps, ushort minCycle,
            double minDuration, bool isLog = false, bool isInverse = false)
        {
            this.SampleRate = sampleRate;
            this.Amplitude = amplitude;
            this.FrequencyMax = frequencyMax;
            this.FrequencyMin = frequencyMin;
            this.Steps = steps;
            this.MinCycle = minCycle;
            this.MinDuration = minDuration;
            this.IsLog = isLog;
            this.IsInverse = isInverse;

            var waveform = new ManagedAudioLibrary.SteppedSineWaveform();
            waveform.CreateData(amplitude, frequencyMin, frequencyMax, steps, isLog, isInverse, minCycle, 
                minDuration, sampleRate);
            RawWaveform = waveform;
        }

        /// <summary>
        /// 构造SteppedSineWaveform
        /// </summary>
        /// <param name="sourceWaveform">源波形</param>
        /// <param name="equalizer">均衡器</param>
        public SteppedSineWaveform(SteppedSineWaveform sourceWaveform, SteppedSineEqualizer equalizer)
        {
            this.SampleRate = sourceWaveform.SampleRate;

            double maxEqalizer = 0;
            for (ushort i = 0; i < equalizer.GetPointsOfEQ(); i++)
            {
                // TODO 凌华的均衡器实际数值是倒数，待确认
                maxEqalizer = Math.Abs(equalizer.GetEQ(i)) < maxEqalizer
                    ? Math.Abs(equalizer.GetEQ(i))
                    : maxEqalizer;
            }
            this.Amplitude = sourceWaveform.Amplitude / (maxEqalizer > 0 ? maxEqalizer : 1);
            this.FrequencyMax = sourceWaveform.FrequencyMax;
            this.FrequencyMin = sourceWaveform.FrequencyMin;
            this.Steps = sourceWaveform.Steps;
            this.MinCycle = sourceWaveform.MinCycle;
            this.MinDuration = sourceWaveform.MinDuration;
            this.IsLog = sourceWaveform.IsLog;
            this.IsInverse = sourceWaveform.IsInverse;

            var waveform = new ManagedAudioLibrary.SteppedSineWaveform();
            var rawWaveform = sourceWaveform.GetRawWaveform() as ManagedAudioLibrary.SteppedSineWaveform;
            var rawEqualizer = equalizer.GetRawEqualizer() as ManagedAudioLibrary.SteppedSineEqualizer;
            waveform.CreateData(rawWaveform, rawEqualizer);
            RawWaveform = waveform;
        }

        /// <summary>
        /// 获取指定索引的频率值
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns>索引值对应频率</returns>
        public double GetFrequencyPoint(ushort index)
        {
            return ((ManagedAudioLibrary.SteppedSineWaveform) RawWaveform).GetFrequencyPoint(index);
        }
    }
}
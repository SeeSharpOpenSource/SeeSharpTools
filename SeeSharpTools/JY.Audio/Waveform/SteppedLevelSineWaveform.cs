namespace SeeSharpTools.JY.Audio.Waveform
{
    public class SteppedLevelSineWaveform : WaveformBase
    {
        /// <summary>
        /// 最小幅度
        /// </summary>
        public double AmplitudeMin { protected set; get; }
        /// <summary>
        /// 最大幅度
        /// </summary>
        public double AmplitudeMax { protected set; get; }
        /// <summary>
        /// 频率
        /// </summary>
        public double Frequency { protected set; get; }
        /// <summary>
        /// 波幅阶数
        /// </summary>
        public ushort Steps { protected set; get; }
        /// <summary>
        /// 最小周期数
        /// </summary>
        public ushort MinCycle { protected set; get; }
        /// <summary>
        /// 单阶最小时间
        /// </summary>
        public double MinDuration { protected set; get; }
        /// <summary>
        /// 是否对数波形
        /// </summary>
        public bool IsLog { protected set; get; }
        /// <summary>
        /// 是否反转
        /// </summary>
        public bool IsInverse { protected set; get; }

        /// <summary>
        /// 构造SteppedLevelSine波形
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="amplitudeMin">最小波幅</param>
        /// <param name="amplitudeMax">最大波幅</param>
        /// <param name="frequency">频率</param>
        /// <param name="steps">频率阶数</param>
        /// <param name="minCycle">最小周期</param>
        /// <param name="minDuration">单阶最小时间</param>
        /// <param name="isLog">是否对数波形</param>
        /// <param name="isInverse">是否反转</param>
        public SteppedLevelSineWaveform(double sampleRate, double amplitudeMin, double amplitudeMax, 
            double frequency, ushort steps, ushort minCycle, double minDuration, bool isLog = false, 
            bool isInverse = false)
        {
            this.SampleRate = sampleRate;
            this.Amplitude = amplitudeMax > amplitudeMin ? amplitudeMax : amplitudeMin;
            this.AmplitudeMin = amplitudeMin;
            this.AmplitudeMax = amplitudeMax;
            this.Frequency = frequency;
            this.Steps = steps;
            this.MinCycle = minCycle;
            this.MinDuration = minDuration;
            this.IsLog = isLog;
            this.IsInverse = isInverse;

            var waveform = new ManagedAudioLibrary.SteppedLevelSineWaveform();
            waveform.CreateData(amplitudeMin, amplitudeMax, frequency, steps, isLog, isInverse, minCycle, 
                minDuration, sampleRate);
            RawWaveform = waveform;
        }
    }
}
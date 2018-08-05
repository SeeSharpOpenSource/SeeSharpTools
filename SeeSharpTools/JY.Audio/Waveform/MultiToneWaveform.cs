namespace SeeSharpTools.JY.Audio.Waveform
{
    public class MultiToneWaveform : WaveformBase
    {
        /// <summary>
        /// 最小频率
        /// </summary>
        public double FrequencyMin { protected set; get; }
        /// <summary>
        /// 最大频率
        /// </summary>
        public double FrequencyMax { protected set; get; }
        /// <summary>
        /// 频率总个数
        /// </summary>
        public ushort FrequencyPoints { protected set; get; }
        /// <summary>
        /// 是否为对数波形
        /// </summary>
        public bool IsLog { protected set; get; }
        /// <summary>
        /// 是否优化参数
        /// </summary>
        public bool OptimizeCrestFactor { protected set; get; }
        /// <summary>
        /// 波形长度
        /// </summary>
        public uint WaveLength { protected set; get; }

        /// <summary>
        /// 构造多音色波形
        /// </summary>
        /// <param name="frequencyMin">最低频率</param>
        /// <param name="frequencyMax">最大频率</param>
        /// <param name="amplitude">波幅</param>
        /// <param name="frequencyPoints">频率个数</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="waveLength">样点个数</param>
        /// <param name="isLog">是否为对数</param>
        /// <param name="optimizeCrestFactor">是否优化参数</param>
        public MultiToneWaveform(double frequencyMin, double frequencyMax, double amplitude,
            ushort frequencyPoints, double sampleRate, uint waveLength, bool isLog = false, 
            bool optimizeCrestFactor = false)
        {
            SampleRate = sampleRate;
            Amplitude = amplitude * frequencyPoints;

            FrequencyMin = frequencyMin;
            FrequencyMax = frequencyMax;
            FrequencyPoints = frequencyPoints;
            IsLog = isLog;
            OptimizeCrestFactor = optimizeCrestFactor;
            WaveLength = waveLength;

            ManagedAudioLibrary.MultiToneWaveform waveform = new ManagedAudioLibrary.MultiToneWaveform();
            waveform.CreateData(amplitude, frequencyMin, frequencyMax, frequencyPoints, waveLength, isLog, 
                optimizeCrestFactor, sampleRate);
            RawWaveform = waveform;
        }
    }
}
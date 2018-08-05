namespace SeeSharpTools.JY.Audio.Waveform
{
    public class LogChirpWaveform : WaveformBase
    {
        /// <summary>
        /// 最低频率
        /// </summary>
        public double FrequencyMin { protected set; get; }
        /// <summary>
        /// 最大频率
        /// </summary>
        public double FrequencyMax { protected set; get; }
        /// <summary>
        /// 前向延伸比
        /// </summary>
        public double PreSweepRatio { protected set; get; }
        /// <summary>
        /// 后向延伸比
        /// </summary>
        public double PostSweepRatio { protected set; get; }

        /// <summary>
        /// 构造LogChirp波形
        /// </summary>
        /// <param name="freqencyMin">最小频率</param>
        /// <param name="freqencyMax">最大频率</param>
        /// <param name="amplitude">波幅</param>
        /// <param name="preSweepRatio">前向延伸比</param>
        /// <param name="postSweepRatio">后向延伸比</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="waveLength">波形样点数</param>
        public LogChirpWaveform(double freqencyMin, double freqencyMax, double amplitude, double preSweepRatio,
            double postSweepRatio, double sampleRate, uint waveLength)
        {
            this.SampleRate = sampleRate;
            this.Amplitude = amplitude;
            this.FrequencyMin = freqencyMin;
            this.FrequencyMax = freqencyMax;
            this.PreSweepRatio = preSweepRatio;
            this.PostSweepRatio = postSweepRatio;

            ManagedAudioLibrary.LogChirpWaveform waveform = new ManagedAudioLibrary.LogChirpWaveform();
            waveform.CreateData(amplitude, freqencyMin, freqencyMax, preSweepRatio, postSweepRatio, waveLength,
                sampleRate);
            RawWaveform = waveform;
        }
    }
}
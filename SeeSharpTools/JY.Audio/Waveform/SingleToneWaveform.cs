namespace SeeSharpTools.JY.Audio.Waveform
{
    public class SingleToneWaveform : WaveformBase
    {
        /// <summary>
        /// 频率
        /// </summary>
        public double Frequency { protected set; get; }
        /// <summary>
        /// 初始相位
        /// </summary>
        public double Phase { protected set; get; }

        /// <summary>
        /// 构造单音色波形
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="amplitude">波幅</param>
        /// <param name="frequency">频率</param>
        /// <param name="phase">初始相位</param>
        /// <param name="waveLength">波形长度</param>
        public SingleToneWaveform(double sampleRate, double amplitude,
            double frequency, double phase, uint waveLength)
        {
            this.SampleRate = sampleRate;
            this.Amplitude = amplitude;

            this.Frequency = frequency;
            this.Phase = phase;

            ManagedAudioLibrary.SingleToneWaveform waveform = new ManagedAudioLibrary.SingleToneWaveform();
            waveform.CreateData(amplitude, frequency, sampleRate, waveLength, phase);
            this.RawWaveform = waveform;
        }
    }
}
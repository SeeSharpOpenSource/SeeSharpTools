namespace SeeSharpTools.JY.Audio.Waveform
{
    /// <summary>
    /// 双音色波形类
    /// </summary>
    public class DualToneWaveform : WaveformBase
    {
        /// <summary>
        /// 第一个频率
        /// </summary>
        public double Frequency1 { protected set; get; }
        /// <summary>
        /// 第二个频率
        /// </summary>
        public double Frequency2 { protected set; get; }
        /// <summary>
        /// 第一频率波形的幅度
        /// </summary>
        public double Amplitude1 { protected set; get; }
        /// <summary>
        /// 第二频率波形的幅度
        /// </summary>
        public double Amplitude2 { protected set; get; }
        /// <summary>
        /// 波形数据的长度
        /// </summary>
        public uint WaveLength { protected set; get; }

        /// <summary>
        /// 构造双音色波形
        /// </summary>
        /// <param name="frequency1">第一波形频率</param>
        /// <param name="frequency2">第二波形频率</param>
        /// <param name="amplitude1">第一波形波幅</param>
        /// <param name="amplitude2">第二波形波幅</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="waveLength">波形样点长度</param>
        public DualToneWaveform(double frequency1, double frequency2, double amplitude1, 
            double amplitude2, double sampleRate, uint waveLength)
        {
            SampleRate = sampleRate;
            Amplitude = amplitude1 + amplitude2;

            Frequency1 = frequency1;
            Frequency2 = frequency2;
            Amplitude1 = amplitude1;
            Amplitude2 = amplitude2;
            WaveLength = waveLength;

            ManagedAudioLibrary.DualToneWaveform waveform = new ManagedAudioLibrary.DualToneWaveform();
            waveform.CreateData(frequency1, frequency2, amplitude1, amplitude1/amplitude2, sampleRate, 
                waveLength);
            RawWaveform = waveform;
        }
    }
}
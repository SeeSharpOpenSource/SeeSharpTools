using System;
using ManagedAudioLibrary;

namespace SeeSharpTools.JY.Audio
{
    public static class AudioGenerator
    {
        // TODO No buf check and exception
        /// <summary>
        /// 任意波生成
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="waveLength">样点数</param>
        /// <returns>波形数据</returns>
        public static double[] AribitraryWaveform(double sampleRate, uint waveLength)
        {
            ArbitraryWaveform arbitraryWaveform = new ArbitraryWaveform();
            double[] waveData = new double[waveLength];
            arbitraryWaveform.CreateData(waveData, waveLength, sampleRate);
            return waveData;

        }

        /// <summary>
        /// LogChirp波形生成
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="waveLength">波形长度</param>
        /// <param name="amplitude">波幅</param>
        /// <param name="frequencyMin">最低频率</param>
        /// <param name="frequencyMax">最高频率</param>
        /// <param name="preSweepRatio">前向延伸比</param>
        /// <param name="postSweepRatio">后向延伸比</param>
        /// <returns>波形数据</returns>
        public static double[] LogChirpWaveform(double sampleRate, uint waveLength, double amplitude,
            double frequencyMin, double frequencyMax, double preSweepRatio, double postSweepRatio)
        {
            LogChirpWaveform logChirpWaveform = new LogChirpWaveform();
            logChirpWaveform.CreateData(amplitude, frequencyMin, frequencyMax, preSweepRatio,
                postSweepRatio, waveLength, sampleRate);
            double[] waveData = new double[logChirpWaveform.GetTotalPoints()];
            logChirpWaveform.GetCopyOfData(waveData);
            return waveData;
        }

        /// <summary>
        /// 多音色波形生成
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="waveLength">波形长度</param>
        /// <param name="amplitude">波幅</param>
        /// <param name="frequencyMin">最低频率</param>
        /// <param name="frequencyMax">最高频率</param>
        /// <param name="frequencyPoints">频率个数</param>
        /// <param name="isLog">是否对数波形</param>
        /// <param name="optimizeCrestFactor">是否优化参数</param>
        /// <returns>波形数据</returns>
        public static double[] MultiToneWaveform(double sampleRate, uint waveLength, double amplitude, 
            double frequencyMin, double frequencyMax, ushort frequencyPoints, bool isLog = false, 
            bool optimizeCrestFactor = false)
        {
            MultiToneWaveform multiToneWaveform = new MultiToneWaveform();
            multiToneWaveform.CreateData(amplitude, frequencyMin, frequencyMax, frequencyPoints,
                waveLength, isLog, optimizeCrestFactor, sampleRate);
            double[] waveData = new double[multiToneWaveform.GetTotalPoints()];
            multiToneWaveform.GetCopyOfData(waveData);
            return waveData;
        }

        /// <summary>
        /// 单音色波形生成
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="waveLength">波形长度</param>
        /// <param name="amplitude">波幅</param>
        /// <param name="frequency">频率</param>
        /// <param name="phase">初始相位</param>
        /// <returns>波形数据</returns>
        public static double[] SingleToneWaveform(double sampleRate, uint waveLength,
            double amplitude, double frequency, double phase)
        {
            SingleToneWaveform singleToneWaveform = new SingleToneWaveform();
            singleToneWaveform.CreateData(amplitude, frequency, sampleRate, waveLength, 
                phase);
            double[] waveData = new double[singleToneWaveform.GetTotalPoints()];
            singleToneWaveform.GetCopyOfData(waveData);
            return waveData;
        }

        /// <summary>
        /// SteppedLevelSine波形生成
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="amplitudeMin">最小频率</param>
        /// <param name="amplitudeMax">最大频率</param>
        /// <param name="frequency">频率</param>
        /// <param name="steps">波幅阶数</param>
        /// <param name="minCycle">最小周期</param>
        /// <param name="minDuration">最小时间</param>
        /// <param name="isLog">是否对数波形</param>
        /// <param name="isInverse">是否反转</param>
        /// <returns>波形数据</returns>
        public static double[] SteppedLevelSineWaveform(double sampleRate, 
            double amplitudeMin, double amplitudeMax, double frequency, ushort steps, 
            ushort minCycle, double minDuration, bool isLog = false, bool isInverse = false)
        {
            SteppedLevelSineWaveform steppedLevelSineWaveform = new SteppedLevelSineWaveform();
            steppedLevelSineWaveform.CreateData(amplitudeMin, amplitudeMax, frequency, steps,
                isLog, isInverse, minCycle, minDuration, sampleRate);
            double[] waveData = new double[steppedLevelSineWaveform.GetTotalPoints()];
            steppedLevelSineWaveform.GetCopyOfData(waveData);
            return waveData;
        }

        /// <summary>
        /// SteppedSine波形生成
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="amplitude">波幅</param>
        /// <param name="frequencyMin">最低频率</param>
        /// <param name="frequencyMax">最大频率</param>
        /// <param name="steps">频率阶数</param>
        /// <param name="minCycle">最小周期数</param>
        /// <param name="minDuration">最小持续时间</param>
        /// <param name="isLog">是否对数波形</param>
        /// <param name="isInverse">是否反转</param>
        /// <returns>波形数据</returns>
        public static double[] SteppedSineWaveform(double sampleRate, double amplitude, 
            double frequencyMin, double frequencyMax, ushort steps, ushort minCycle, 
            double minDuration, bool isLog = false, bool isInverse = false)
        {
            SteppedSineWaveform steppedSineWaveform = new SteppedSineWaveform();
            steppedSineWaveform.CreateData(amplitude, frequencyMin, frequencyMax, steps, isLog, isInverse, minCycle,
                minDuration, sampleRate);
            double[] waveData = new double[steppedSineWaveform.GetTotalPoints()];
            steppedSineWaveform.GetCopyOfData(waveData);
            return waveData;
        }

        /// <summary>
        /// 从Wav文件读取波形数据
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="channelCount">通道个数</param>
        /// <returns>波形数据</returns>
        public static double[] FromWavFile(string path, out int channelCount)
        {
            WavReader reader = new WavReader();
            reader.ReadFile(path);
            double[] waveData = new double[reader.GetTotalPoints()];
            reader.GetCopyOfData(waveData);
            channelCount = reader.GetChannelCounts();
            return waveData;
        }

        /// <summary>
        /// 双音色波形生成
        /// </summary>
        /// <param name="sampleRate">采样率</param>
        /// <param name="frequency1">频率1</param>
        /// <param name="frequency2">频率2</param>
        /// <param name="amplitude1">频率1的波幅</param>
        /// <param name="amplitude2">频率2的波幅</param>
        /// <param name="waveLength">波形长度</param>
        /// <returns>波形数据</returns>
        public static double[] DualToneWaveform(double sampleRate, double frequency1, double frequency2, double amplitude1, double amplitude2, uint waveLength)
        {
            DualToneWaveform dualToneWaveform = new DualToneWaveform();
            dualToneWaveform.CreateData(frequency1, frequency2, amplitude1, amplitude1/amplitude2, sampleRate,
                waveLength);
            double[] waveData = new double[dualToneWaveform.GetTotalPoints()];
            dualToneWaveform.GetCopyOfData(waveData);
            return waveData;
        }
    }
}
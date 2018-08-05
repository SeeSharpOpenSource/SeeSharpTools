using System;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    /// <summary>
    /// <para>Generation of sine wave, square wave, ramp and uniform white noise.</para>
    /// <para>提供正弦波、方波、白噪声和等差数列的生成。</para>
    /// </summary>
    public static class Generation
    {

        /// <summary>
        /// <para>Generates a sequence containing a ramp pattern specified by the first value and delta.</para>
        /// <para>Chinese Simplified: 产生一个等差数列，数列的项数由数组x的长度给出，首项由start给出，公差由delta给出。</para>
        /// </summary>
        /// <param name="x">
        /// <para>contains the ramp pattern sequence. </para>
        /// <para>Chinese Simplified: 返回的等差数列</para>
        /// </param>
        /// <param name="start">
        /// <para>the first value of ramp pattern. </para>
        /// <para>Chinese Simplified: 等差数列的首项</para>
        /// </param>
        /// <param name="delta">
        /// <para>the interval between two adjacent values in ramp pattern. </para>
        /// <para>Chinese Simplified: 等差数列的公差</para>
        /// </param>
        public static void Ramp(ref double[] x, double start = 0, double delta = 1)
        {
            for (int i = 0; i < x.Length; i++) { x[i] = start + delta * i; }
        }

        /// <summary>
        /// <para>Generates a sequence containing a sine wave with specified frequency and sampling rate.</para>
        /// <para>Chinese Simplified: 生成一个正弦波形，可设定正弦波的幅度、初始相位、频率和采样率。</para>
        /// </summary>
        /// <param name="x">
        /// <para>contains the sine wave sequence. </para>
        /// <para>Chinese Simplified: 返回的正弦波形。</para>
        /// </param>
        /// <param name="amplitude">
        /// <para>the amplitude of sine wave. </para>
        /// <para>Chinese Simplified: 幅度。</para>
        /// </param>
        /// <param name="phase">
        /// <para>the initial phase of the sine wave in degrees.</para>
        /// <para>Chinese Simplified: 初始相位，以角度(Degree)为单位。</para>
        /// </param>
        /// <param name="frequency">
        /// <para>the frequency of sine wave in Hz.  </para>
        /// <para>Chinese Simplified: 正弦波的频率，以Hz为单位。</para>
        /// </param>
        /// <param name="samplingRate">
        /// <para>the sampling rate of sine wave in samples/second. </para>
        /// <para>Chinese Simplified: 采样率，以S/s为单位。</para>
        /// </param>
        public static void SineWave(ref double[] x, double amplitude, double phase, double frequency, double samplingRate) 
        {
            double phseIncrement = 2 * Math.PI * frequency / samplingRate;
            double phaseStart = phase * Math.PI / 180;  // convert input phase from Degree to Rad
            for (int i = 0; i < x.Length; i++) x[i] = amplitude * Math.Sin(phseIncrement * i + phaseStart);
        }

        /// <summary>
        /// <para>Generates a sequence containing a sine wave with complete periods.</para>
        /// <para>Chinese Simplified: 生成一个包含整数个周期的正弦波形，可设定正弦波的幅度、初始相位和周期数。</para>
        /// </summary>
        /// <param name="x">
        /// <para>contains the sine wave sequence. </para>
        /// <para>Chinese Simplified: 返回的正弦波形。</para>
        /// </param>
        /// <param name="amplitude">
        /// <para>the amplitude of sine wave. </para>
        /// <para>Chinese Simplified: 幅度。</para>
        /// </param>
        /// <param name="phase">
        /// <para>the initial phase of the sine wave in degrees.</para>
        /// <para>Chinese Simplified: 初始相位，以角度(Degree)为单位。</para>
        /// </param>
        /// <param name="numberOfCycles">
        /// <para>the number of complete periods of the sine wave. </para>
        /// <para>Chinese Simplified: 正弦波周期数。</para>
        /// </param>
        public static void SineWave(ref double[] x, double amplitude = 1, double phase = 0, int numberOfCycles = 1)
        {
            double phseIncrement = 2 * Math.PI * numberOfCycles / x.Length;
            double phaseStart = phase * Math.PI / 180;  // convert input phase from Degree to Rad
            for (int i = 0; i < x.Length; i++) x[i] = amplitude * Math.Sin(phseIncrement * i + phaseStart);
        }

        /// <summary>
        /// <para>Generates a sequence containing a square wave with complete periods.</para>
        /// <para>Chinese Simplified: 生成一个包含整数个周期的方波波形，可设定方波的幅度、占空比和周期数。</para>
        /// </summary>
        /// <param name="x">
        /// <para>contains the square wave sequence.</para>
        /// <para>Chinese Simplified: 返回的方波波形。</para>
        /// </param>
        /// <param name="amplitude">
        /// <para>the amplitude of square wave.</para>
        /// <para>Chinese Simplified: 幅度。</para>
        /// </param>
        /// <param name="dutyCycle">
        /// <para>the percentage of time the square wave remains high in one period.</para>
        /// <para>Chinese Simplified: 占空百分比，为0~100之间的数值。</para>
        /// </param>
        /// <param name="numberOfCycles">
        /// <para>the number of complete periods of the square wave.</para>
        /// <para>Chinese Simplified:方波周期数。</para>
        /// </param>
        public static void SquareWave(ref double[] x, double amplitude = 1, double dutyCycle = 50, int numberOfCycles = 1)
        {
            double position, cycleIncrement;

            // Validate input arguments, each cycle must have more than 2 samples
            if (x.Length <= 0 || amplitude <= 0 || dutyCycle <0 || dutyCycle > 100 || numberOfCycles <=0 || numberOfCycles * 2 > x.Length)
                { throw new ArgumentException(); }

            cycleIncrement = (double)numberOfCycles / x.Length;
            // Generate pulse pattern
            for (int i = 0; i < x.Length; i++)
            {
                position = cycleIncrement * i; // calculate position of x[i] in cycles
                if (position - Math.Floor(position) < dutyCycle / 100)
                    { x[i] = amplitude; }
                else
                    { x[i] = -amplitude; }
            }
        }

        /// <summary>
        /// <para>Generates a sequence containing a square wave with specified frequency and sampling rate.</para>
        /// <para>Chinese Simplified: 生成一个方波波形，可设定方波的幅度、占空比、频率和采样率。</para>
        /// </summary>
        /// <param name="x">
        /// <para>contains the square wave sequence.</para>
        /// <para>Chinese Simplified: 返回的方波波形。</para>
        /// </param>
        /// <param name="amplitude">
        /// <para>the amplitude of square wave.</para>
        /// <para>Chinese Simplified: 幅度。</para>
        /// </param>
        /// <param name="dutyCycle">
        /// <para>the percentage of time the square wave remains high in one period.</para>
        /// <para>Chinese Simplified: 占空百分比，为0~100之间的数值。</para>
        /// </param>
        /// <param name="frequency">
        /// <para>the frequency of square wave in Hz. </para>
        /// <para>Chinese Simplified: 方波的频率，以Hz为单位。</para>
        /// </param>
        /// <param name="samplingRate">
        /// <para>the sampling rate of square wave in samples/second.</para>
        /// <para>Chinese Simplified: 采样率，以S/s为单位。</para>
        /// </param>
        public static void SquareWave(ref double[] x, double amplitude, double dutyCycle, double frequency, double samplingRate)
        {
            double position, cycleIncrement;

            // Validate input arguments, each cycle must have more than 2 samples
            if (x.Length <= 0 || amplitude <= 0 || dutyCycle < 0 || dutyCycle > 100 || frequency <= 0 || frequency * 2 > samplingRate)
                { throw new ArgumentException(); }

            cycleIncrement = (double) frequency / samplingRate;
            // Generate pulse pattern
            for (int i = 0; i < x.Length; i++)
            {
                position = cycleIncrement * i; // calculate position of x[i] in cycles
                if (position - Math.Floor(position) < dutyCycle / 100)
                    { x[i] = amplitude; }
                else
                    { x[i] = -amplitude; }
            }
        }

        /// <summary>
        /// <para>Generates a uniformly distributed pseudorandom sequence in the range [–amplitude, amplitude].</para>
        /// <para>Chinese Simplified: 生成一个幅度在[–amplitude, amplitude]之间的随机白噪声波形。</para>
        /// </summary>
        /// <param name="x">
        /// <para>contains the uniformly distributed, pseudorandom sequence.</para>
        /// <para>Chinese Simplified: 返回的白噪声波形。</para>
        /// </param>
        /// <param name="amplitude">
        ///<para> the amplitude of uniform white noise.</para>
        /// <para>Chinese Simplified: 噪声幅度。</para>
        /// </param>
        public static void UniformWhiteNoise(ref double[] x, double amplitude = 1)
        {
            Random rn = new Random();
            for (int i = 0; i < x.Length; i++) { x[i] = amplitude * (rn.NextDouble() * 2 - 1) ; }
        }

    }
}

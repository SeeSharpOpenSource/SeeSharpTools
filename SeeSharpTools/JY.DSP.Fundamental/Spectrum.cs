using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Runtime.InteropServices;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    /// <summary>
    /// <para>Computes spectrum of a time-domain signal.</para>
    /// <para>Chinese Simplified: 计算信号的频谱。</para>
    /// </summary>
    public static class Spectrum
    {
        #region -----------Private Var & Initializtion------------

        /// <summary>
        /// Maximiun Spectral Line Count
        /// </summary>
        private const int MaxSpectralLine = 8 * 1024 * 1024;//65536;

        /// <summary>
        /// Sqrt 2
        /// </summary>
        private const double Sqrt2 = 1.4142135623730950488016887242097;

        #endregion

        #region ---------------Public: Power Spectrum-------------

        /// <summary>
        /// <para>Computes the power spectrum of input time-domain signal.</para>
        /// <para>Chinese Simplified: 计算输入信号的功率频谱。</para>
        /// </summary>
        /// <param name="waveform">
        /// <para>input time-domain signal.</para>
        /// <para>Chinese Simplified: 输入的时域波形。</para>
        /// </param>
        /// <param name="samplingRate">
        /// <para>sampling rate of the input time-domain signal, in samples per second.</para>
        /// <para>Chinese Simplified: 输入信号的采样率，以S/s为单位。</para>
        /// </param>
        /// <param name="spectrum">
        /// <para>output sequence containing the power spectrum.</para>
        /// <para>Chinese Simplified: 输出功率谱。</para>
        /// </param>
        /// <param name="df">
        /// <para>the frequency resolution of the spectrum,  in hertz.</para>
        /// <para>Chinese Simplified: 功率谱的频谱间隔，以Hz为单位。</para>
        /// </param>
        /// <param name="unit">
        /// <para>unit of the output power spectrum</para>
        /// <para>Chinese Simplified: 设置功率谱的单位。</para>
        /// </param>
        /// <param name="windowType">
        /// <para>the time-domain window to apply to the time signal.</para>
        /// <para>Chinese Simplified: 窗类型。</para>
        /// </param>
        /// <param name="windowPara">
        /// <para>parameter for a Kaiser/Gaussian/Dolph-Chebyshev window, If window is any other window, this parameter is ignored</para>
        /// <para>Chinese Simplified: 窗调整系数，仅用于Kaiser/Gaussian/Dolph-Chebyshev窗。</para>
        /// </param>
        /// <param name="PSD">
        /// <para>specifies whether the output power spectrum is converted to power spectral density.</para>
        /// <para>Chinese Simplified: 输出的频谱是否为功率谱密度。</para>
        /// </param>
        public static void PowerSpectrum(double[] waveform, double samplingRate, ref double[] spectrum, out double df,
            SpectrumUnits unit = SpectrumUnits.V2, WindowType windowType = WindowType.Hanning,
            double windowPara = double.NaN, bool PSD = false)
        {
            int spectralLines = spectrum.Length; //谱线数是输出数组的大小
            SpectralInfo spectralInfo = new SpectralInfo();
            AdvanceComplexFFT(waveform, spectralLines, windowType, spectrum, ref spectralInfo);
            double scale = 1.0 / spectralInfo.FFTSize;
            CBLASNative.cblas_dscal(spectralLines, scale, spectrum, 1);
            df = 0.5 * samplingRate / spectralInfo.spectralLines; //计算频率间隔

            //Unit Conversion
            UnitConvSetting unitSettings = new UnitConvSetting(unit, PeakScaling.Rms, 50.00, PSD);
            UnitConversion(spectrum, df, SpectrumType.Amplitude, unitSettings, Window.WindowENBWFactor[(int)windowType]);
        }

        /// <summary>
        /// <para>Computes the power spectrum of input time-domain signal.</para>
        /// <para>Chinese Simplified: 计算输入信号的功率频谱。</para>
        /// </summary>
        /// <param name="waveform">
        /// <para>input time-domain signal.</para>
        /// <para>Chinese Simplified: 输入的时域波形。</para>
        /// </param>
        /// <param name="samplingRate">
        /// <para>sampling rate of the input time-domain signal, in samples per second.</para>
        /// <para>Chinese Simplified: 输入信号的采样率，以S/s为单位。</para>
        /// </param>
        /// <param name="spectrum">
        /// <para>output sequence containing the power spectrum.</para>
        /// <para>Chinese Simplified: 输出功率谱。</para>
        /// </param>
        /// <param name="df">
        /// <para>the frequency resolution of the spectrum,  in hertz.</para>
        /// <para>Chinese Simplified: 功率谱的频谱间隔，以Hz为单位。</para>
        /// </param>
        /// <param name="unitSettings">
        /// <para>unit settings of the output power spectrum</para>
        /// <para>Chinese Simplified: 设置功率谱的单位。</para>
        /// </param>
        /// <param name="windowType">
        /// <para>the time-domain window to apply to the time signal.</para>
        /// <para>Chinese Simplified: 窗类型。</para>
        /// </param>
        /// <param name="windowPara">
        /// <para>parameter for a Kaiser/Gaussian/Dolph-Chebyshev window, If window is any other window, this parameter is ignored</para>
        /// <para>Chinese Simplified: 窗调整系数，仅用于Kaiser/Gaussian/Dolph-Chebyshev窗。</para>
        /// </param>
        public static void PowerSpectrum(double[] waveform, double samplingRate, ref double[] spectrum, out double df,
            UnitConvSetting unitSettings, WindowType windowType, double windowPara)
        {
            int spectralLines = spectrum.Length; //谱线数是输出数组的大小
            SpectralInfo spectralInfo = new SpectralInfo();
            AdvanceComplexFFT(waveform, spectralLines, windowType, spectrum, ref spectralInfo);

            double scale = 1.0 / spectralInfo.FFTSize;
            CBLASNative.cblas_dscal(spectralLines, scale, spectrum, 1);

            df = 0.5 * samplingRate / spectralInfo.spectralLines; //计算频率间隔

            //Unit Conversion
            UnitConversion(spectrum, df, SpectrumType.Amplitude, unitSettings, Window.WindowENBWFactor[(int)windowType]);
        }
        #endregion

        #region ------------Public: PeakSpectrumAnalysis------------
        /// <summary>
        /// Get the fundamental frequency and amplitude.
        /// </summary>
        /// <param name="waveform">the waveform of input signal assuming in voltage</param>
        /// <param name="dt">sampling interval of timewaveform (s)</param>
        /// <param name="peakFreq">the calculated peak tone frequency</param>
        /// <param name="peakAmp">the calculated peak tone voltage peak amplitude, which is 1.414*RMS</param>
        /// i.e. peakSignal=peakAmp*sin(2*pi*peakFreq*t)
        public static void PeakSpectrumAnalysis(double[] waveform, double dt, out double peakFreq, out double peakAmp)
        {
            double[] spectrum = new double[waveform.Length / 2];
            double df;
            var spectUnit = SpectrumUnits.V2; //this V^2 unit relates to power in band calculation, don't change
            var winType = WindowType.Hanning;  //relates to ENBW, must change in pair
            double ENBW = 1.5000; //ENBW for winType Hanning.ENBW = 1.500

            double approxFreq = -1;
            double searchRange = 0;

            double maxValue = 0;
            int maxValueIndex = 0;
            int i, approxFreqIndex, startIndex, endIndex;
            double powerInBand = 0;
            double powerMltIndex = 0;

            Spectrum.PowerSpectrum(waveform, 1 / dt, ref spectrum, out df, spectUnit, winType);
            if (approxFreq < 0)
            {
                startIndex = 0;
                endIndex = spectrum.Length;
            }
            else
            {
                approxFreqIndex = (int)(approxFreq / df);
                endIndex = (int)(searchRange / 200 / dt); // start earch with approx. Freq - 1/2 range
                startIndex = approxFreqIndex - endIndex;  //start search index
                endIndex = (int)(searchRange / 100 / dt); //search length in indexes
                if (startIndex < 0) startIndex = 0;  //start index protection
                if (startIndex > spectrum.Length - 2) startIndex = spectrum.Length - 2;  //start index protection
                if (endIndex < 1) endIndex = 1; //protect search range;
            };
            //Search spectrum from [i1] to i1+i2-1;
            maxValue = -1; //power spectrum can not be less than 0;

            maxValue = spectrum.Max();
            maxValueIndex = Array.FindIndex<double>(spectrum, s => s == maxValue);

            startIndex = maxValueIndex - 3;
            if (startIndex < 0) startIndex = 0;

            endIndex = startIndex + 7;

            if (endIndex > spectrum.Length - 1) endIndex = spectrum.Length - 1;

            for (i = startIndex; i < endIndex; i++)
            {
                powerInBand += spectrum[i];
                powerMltIndex += spectrum[i] * i;
            }
            peakFreq = powerMltIndex / powerInBand * df;     //Given the estimated frequency and power, the exact frequency can be calculated
            peakAmp = Math.Sqrt(powerInBand / ENBW * 2); //convert V^2 to V peak amplitude                        //Refer this formula to  ITU Handbook
        }

        #endregion

        #region ---------------Private: UnitConversion------------

        /// <summary>
        /// 频谱单位转换函数
        /// </summary>
        /// <param name="spectrum">输入频谱，单位转换后返回的序列也保存在里面</param>
        /// <param name="spectrumType">输入频谱类型，功率谱或者幅度谱</param>
        /// <param name="df">频谱间隔</param>
        /// <param name="unitSetting">单位转换设置</param>
        /// <param name="equivalentNoiseBw">计算频谱时，加窗所用窗函数的等效噪声带宽</param>
        /// <returns></returns>
        private static void UnitConversion(double[] spectrum, double df, SpectrumType spectrumType,
            UnitConvSetting unitSetting, double equivalentNoiseBw)
        {
            double scale = 1.0;
            int freq0Idx = 0, N = spectrum.Length;
            //VMLNative.vdSqr(N, spectrum, spectrum);

            if (unitSetting.PeakScaling == PeakScaling.Peak) //峰峰值要乘以2
            {
                switch (spectrumType)
                {
                    case SpectrumType.Amplitude: // Sqrt2
                        scale *= Sqrt2;
                        break;
                    case SpectrumType.Power: // 2
                        scale *= 2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(spectrumType), spectrumType, null);
                }
                CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                spectrum[0] /= scale; //零频不用
            }

            //根据设置的转换单位进行转换
            switch (unitSetting.Unit)
            {
                case SpectrumUnits.V:
                    {
                        if (SpectrumType.Power == spectrumType)
                        {
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        break;
                    }
                case SpectrumUnits.dBV:
                    {
                        if (SpectrumType.Power == spectrumType)
                        {
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        //lg
                        VMLNative.vdLog10(N, spectrum, spectrum);
                        scale = 20;
                        //20*lg
                        CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                        break;
                    }
                case SpectrumUnits.dBmV:
                    {
                        if (SpectrumType.Power == spectrumType)
                        {
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        CBLASNative.cblas_dscal(N, 1e3, spectrum, 1);  //V To mV                
                        VMLNative.vdLog10(N, spectrum, spectrum);    //To Lg
                        scale = 20;

                        CBLASNative.cblas_dscal(N, scale, spectrum, 1); //To 20*Lg
                        break;
                    }
                case SpectrumUnits.dBuV:
                    {
                        if (SpectrumType.Power == spectrumType)
                        {
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        CBLASNative.cblas_dscal(N, 1e6, spectrum, 1); //V To uV                
                        VMLNative.vdLog10(N, spectrum, spectrum);//To Lg
                        scale = 20;
                        CBLASNative.cblas_dscal(N, scale, spectrum, 1);//To 20*Lg
                        break;
                    }
                case SpectrumUnits.V2:
                    {
                        if (SpectrumType.Amplitude == spectrumType)
                        {
                            VMLNative.vdSqr(N, spectrum, spectrum);
                        }
                        break;
                    }
                case SpectrumUnits.W:
                case SpectrumUnits.dBW:
                case SpectrumUnits.dBm:
                    {
                        if (SpectrumType.Amplitude == spectrumType)
                        {
                            VMLNative.vdSqr(N, spectrum, spectrum);
                        }
                        scale = 1.0 / unitSetting.Impedance;            //1/R
                        CBLASNative.cblas_dscal(N, scale, spectrum, 1); //W = V^2/R

                        if (unitSetting.Unit == SpectrumUnits.dBW)   //dBW = 20lgW
                        {
                            //lg
                            VMLNative.vdLog10(N, spectrum, spectrum);
                            scale = 20;
                            //20*lg
                            CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                        }
                        else if (unitSetting.Unit == SpectrumUnits.dBm) // dBm = 10lg(W/1mW)
                        {
                            CBLASNative.cblas_dscal(N, 1e3, spectrum, 1); // W/1mW
                                                                          //lg
                            VMLNative.vdLog10(N, spectrum, spectrum);
                            scale = 10;
                            //10*lg
                            CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            if (!unitSetting.PSD)
            {
                return;
            }
            //谱密度计算
            scale = 1.0 / (equivalentNoiseBw * df);
            CBLASNative.cblas_dscal(N, scale, spectrum, 1);
        }

        #endregion

        #region  ---------------Private: Adance FFT----------------
        /// <summary>
        /// 计算时域信号的复数频谱，包含幅度谱和相位谱信息
        /// </summary>
        /// <param name="waveform">时域波形数据</param>
        /// <param name="spectralLines">频谱线的条数</param>
        /// <param name="windowType">窗类型</param>
        /// <param name="spectrum">计算后的复数频谱数据</param>
        /// <param name="spectralInfo">返回的频谱数据的参数信息</param>
        public static void AdvanceComplexFFT(double[] waveform, int spectralLines, WindowType windowType,
                                     double[] spectrum, ref SpectralInfo spectralInfo)
        {
            int n = waveform.Length, windowsize = 0, fftcnt = 0; //做FFT的次数
            int fftsize = 0; //做FFT点数
            double cg = 0, enbw = 0, scale = 0.0;
            double[] xInTmp = null;
            double[] windowData = null;
            Complex[] xOutCTmp = null;

            //输入的线数超过最大支持的线数则使用最大支持线数
            if (spectralLines > MaxSpectralLine)
            {
                spectralLines = MaxSpectralLine;
            }

            //输入的点数小于线数，则窗长度为N，先加窗再补零到2*spectralLines再做FFT
            if (n <= 2 * spectralLines)
            {
                windowsize = n;
                fftcnt = 1;
            }
            else
            {
                windowsize = 2 * spectralLines;
                fftcnt = n / (2 * spectralLines);
            }

            fftsize = 2 * spectralLines; //不管N与2*spectralLines的关系是怎么样，FFT的点数都应该为 2*spectralLines

            xInTmp = new double[fftsize];
            xOutCTmp = new Complex[fftsize / 2 + 1];

            if (n < (2 * spectralLines))
            {
                //memset(x_in + N, 0, (fftsize - N) * sizeof(double)); //补零至spectralLines
                for (int i = n; i < fftsize; i++)
                {
                    xInTmp[i] = 0;
                }
            }
            //memset(xOut, 0, spectralLines * sizeof(double));
            //生成窗函数的数据
            windowData = new double[windowsize];
            Window.GetWindow(windowType, ref windowData, out cg, out enbw);
            CBLASNative.cblas_dscal(windowsize, 1 / cg, windowData, 1); //窗系数归一化
            CBLASNative.cblas_dscal(spectrum.Length, 0, spectrum, 1); //将xOut清零
            GCHandle gch = GCHandle.Alloc(waveform, GCHandleType.Pinned);
            var xInPtr = gch.AddrOfPinnedObject();
            for (int i = 0; i < fftcnt; i++)
            {
                //拷贝数据到临时内存中
                //memcpy(x_in, x + i * windowsize, fftsize * sizeof(double));
                /*TIME_DOMAIN_WINDOWS(windowType, x_in, &CG, &ENBW, windowsize);*//*(double*)(xIn + i * windowsize)*/
                VMLNative.vdMul(windowsize, windowData, xInPtr + i * fftsize * sizeof(double), xInTmp);
                BasicFFT.RealFFT(xInTmp, ref xOutCTmp);

                //计算FFT结果复数的模,复用x_in做中间存储
                VMLNative.vzAbs(fftsize / 2 + 1, xOutCTmp, xInTmp);

                //每次计算结果累加起来
                VMLNative.vdAdd(spectralLines, xInTmp, spectrum, spectrum);
            }

            scale = 2 * (1.0 / fftcnt) / Sqrt2; //双边到单边有一个二倍关系,输出为Vrms要除以根号2

            //fftcnt次的频谱做平均
            CBLASNative.cblas_dscal(spectralLines, scale, spectrum, 1);

            spectrum[0] = spectrum[0] / Sqrt2; //上一步零频上多除了根号2，这里乘回来（Rms在零频上不用除根号2，单双边到单边还是要乘2 ?）

            spectralInfo.spectralLines = spectralLines;
            spectralInfo.FFTSize = fftsize;
            spectralInfo.windowSize = windowsize;
            spectralInfo.windowType = windowType;
			gch.Free();
        }

        /// <summary>
        /// 计算时域信号的复数频谱，包含幅度谱和相位谱信息
        /// </summary>
        /// <param name="waveform">时域波形数据</param>
        /// <param name="windowType">窗类型</param>
        /// <param name="spectrum">计算后的复数频谱数据</param>
        public static void AdvanceComplexFFT(double[] waveform, WindowType windowType, ref Complex[] spectrum)
        {
            if(waveform == null || spectrum == null || spectrum.Length < (waveform.Length / 2 + 1))
            {
                throw new JYDSPUserBufferException();
            }

            int n = waveform.Length, windowsize = waveform.Length; //做FFT的次数
            int fftsize = windowsize; //做FFT点数
            double cg = 0, enbw = 0;
            double[] xInTmp = null;
            double[] windowData = null;

            xInTmp = new double[fftsize];

            GCHandle gchXIn = GCHandle.Alloc(waveform, GCHandleType.Pinned);
            var xInPtr = gchXIn.AddrOfPinnedObject();
            GCHandle gchXout = GCHandle.Alloc(spectrum, GCHandleType.Pinned);
            var xOutPtr = gchXout.AddrOfPinnedObject();

            try
            {
                //生成窗函数的数据
                windowData = new double[windowsize];
                Window.GetWindow(windowType, ref windowData, out cg, out enbw);
                CBLASNative.cblas_dscal(windowsize, 1 / cg, windowData, 1); //窗系数归一化
                CBLASNative.cblas_dscal(spectrum.Length, 0, xOutPtr, 1); //将xOut清零
                /*TIME_DOMAIN_WINDOWS(windowType, x_in, &CG, &ENBW, windowsize);*//*(double*)(xIn + i * windowsize)*/
                VMLNative.vdMul(windowsize, windowData, xInPtr, xInTmp);
                BasicFFT.RealFFT(xInTmp, ref spectrum);
            }
            finally
            {
                gchXIn.Free();
                gchXout.Free();
            }
           
        }
        #endregion
    }
}

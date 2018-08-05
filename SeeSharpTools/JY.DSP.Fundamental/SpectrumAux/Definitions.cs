using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    #region  ---------公共枚举定义----------
    ///// <summary>
    ///// double类型的复数
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential, Pack = 4)]
    //public struct Complex64
    //{
    //    /// <summary>
    //    /// 实部
    //    /// </summary>
    //    [MarshalAs(UnmanagedType.R8)]
    //    public double re;

    //    /// <summary>
    //    /// 虚部
    //    /// </summary>
    //    [MarshalAs(UnmanagedType.R8)]
    //    public double im;
    //}

    ///// <summary>
    ///// float类型的复数
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential, Pack = 4)]
    //public struct Complex32
    //{
    //    /// <summary>
    //    /// 实部
    //    /// </summary>
    //    [MarshalAs(UnmanagedType.R4)]
    //    public float re;

    //    /// <summary>
    //    /// 虚部
    //    /// </summary>
    //    [MarshalAs(UnmanagedType.R4)]
    //    public float im;
    //}

    /// <summary>
    /// <para>window type</para>
    /// <para>Chinese Simplified: 窗类型</para>
    /// </summary>
    public enum WindowType : int //窗函数类型枚举
    {
        /// <summary>
        /// <para>Rectangle</para>
        /// <para>Chinese Simplified: 矩形窗</para>
        /// </summary>
        None,

        /// <summary>
        /// <para>Hanning</para>
        /// <para>Chinese Simplified: 汉宁窗</para>
        /// </summary>
        Hanning,

        /// <summary>
        /// <para>Hamming</para>
        /// <para>Chinese Simplified: 海明窗</para>
        /// </summary>
        Hamming,

        /// <summary>
        /// <para>Blackman Harris</para>
        /// <para>Chinese Simplified: 布莱克曼-哈里斯窗</para>
        /// </summary>
        Blackman_Harris,

        /// <summary>
        /// <para>Exact Blackman</para>
        /// </summary>
        Exact_Blackman,

        /// <summary>
        /// <para>Blackman</para>
        /// <para>Chinese Simplified: 布莱克曼窗</para>
        /// </summary>
        Blackman,

        /// <summary>
        /// <para>Flat Top</para>
        /// <para>Chinese Simplified: 平顶窗</para>	
        /// </summary>
        Flat_Top,

        /// <summary>
        /// <para>4 Term B-Harris</para>
        /// </summary>																																						
        Four_Term_B_Harris,

        /// <summary>
        /// <para>7 Term B-Harris</para>
        /// </summary>	
        Seven_Term_B_Harris
    }

    /// <summary>
    /// <para>unit of power spectrum</para>
    /// <para>Chinese Simplified: 频谱的单位</para>
    /// </summary>
    public enum SpectrumUnits : int
    {
        /// <summary>
        /// Voltage
        /// </summary>
        V = 0,

        /// <summary>
        /// V^2
        /// </summary>
        V2,

        /// <summary>
        /// Watt
        /// </summary>
        W,

        /// <summary>
        /// dBm
        /// </summary>
        dBm,

        /// <summary>
        /// dBW
        /// </summary>
        dBW,

        /// <summary>
        /// dBV
        /// </summary>
        dBV,

        /// <summary>
        /// dBmV
        /// </summary>
        dBmV,

        /// <summary>
        /// dBuV
        /// </summary>
        dBuV
    }

    /// <summary>
    /// <para>spectrum type</para>
    /// <para>Chinese Simplified: 频谱类型</para>
    /// </summary>
    internal enum SpectrumType : int
    {
        /// <summary>
        /// <para>amplitude spectrum</para>
        /// <para>Chinese Simplified: 幅度谱</para>
        /// </summary>
        Amplitude = 0,

        /// <summary>
        /// <para>power spectrum</para>
        /// <para>Chinese Simplified: 功率谱</para>
        /// </summary>
        Power = 1
    }

    /// <summary>
    /// Spectrum peak scaling
    /// </summary>
    public enum PeakScaling : int
    {
        /// <summary>
        /// Rms
        /// </summary>
        Rms = 0,

        /// <summary>
        /// Peak
        /// </summary>
        Peak = 1,
    }


    /// <summary>
    /// <para>spectrum unit convertion settings</para>
    /// <para>Chinese Simplified: 单位转换定义结构体</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct UnitConvSetting
    {
        [MarshalAs(UnmanagedType.I4)]
        public SpectrumUnits Unit;

        [MarshalAs(UnmanagedType.I4)]
        public PeakScaling PeakScaling;

        [MarshalAs(UnmanagedType.R8)]
        public double Impedance; // for converting V to Watt

        [MarshalAs(UnmanagedType.I1)]
        public bool PSD; // whether convert to power spectral density.

        public UnitConvSetting(SpectrumUnits unit = SpectrumUnits.dBV, PeakScaling peakScaling = PeakScaling.Rms,
            double impedance = 50.00, bool psd = false)
        {
            Unit = unit;
            PeakScaling = peakScaling;
            Impedance = impedance;
            PSD = psd;
        }
    }

    /// <summary>
    /// <para>spectral information.</para>
    /// <para>Chinese Simplified: 谱信息结构</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SpectralInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int spectralLines;

        [MarshalAs(UnmanagedType.I4)]
        public WindowType windowType;

        [MarshalAs(UnmanagedType.I4)]
        public int windowSize;

        [MarshalAs(UnmanagedType.I4)]
        public int FFTSize;
    }

    #endregion
}

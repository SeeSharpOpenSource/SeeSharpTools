using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SeeSharpTools.JY.DSP.Utility.Fundamental
{
    internal static class Window
    {

        /// <summary>
        /// 内部使用，以WindowType为索引的调整系数Coherent Gain
        /// </summary>
        public static readonly double[] WindowCGFactor = new double[]
        {
            0.5,//Bartlett
            0.5,//BartlettHann
            0.42,//Blackman
            0.35875,//BlackmanHarris
            0.36582,//BlackmanNuttalll
            0.63662,//Cosine
            1,//Dirichlet
            1,//FlatTop
            0.53836,//Hamming
            0.5,//Hann
            0.58949,//Lanczos
            0.355768,//Nuttall
            0.5,//Triangular
        };

        /// <summary>
        /// 以WindowType为索引的ENBW值
        /// </summary>
        public static readonly double[] WindowENBWFactor = new double[]
        {
            1.33333,//Bartlett
            1.45585,//BartlettHann
            1.72676,//Blackman
            2.00435,//BlackmanHarris
            1.97611,//BlackmanNuttall
            1.2337,//Cosine
            1,//Dirichlet
            3.77028,//FlatTop
            1.36765,//Hamming
            1.5,//Hann
            1.29903,//Lanczos
            2.02123,//Nuttall
            1.33333,//Triangular
        };

        public static void GetWindow(WindowType windowType, ref double[] windowdata,
                                     out double CG, out double ENBW)
        {
            if (windowdata == null || windowdata.Length <= 0)
            {
                throw new JYDSPUserBufferException("windowdata length is null!");
            }

            //not need to assign new values
            //for (int i = 0; i < size; i++)
            //{
            //    windowdata[i] = i;
            //}
            var size = windowdata.Length;
            var pi2DSize = Math.PI * 2 / windowdata.Length;
            var pi4DSize = pi2DSize * 2.0;

            CG = 1;
            ENBW = 0;
            //根据windowType:窗函数类型产生单位窗函数
            //CG:相干增益 ENBW:等效噪声宽度 
            switch (windowType)
            {
                case WindowType.Bartlett:
                    CG = 0.5;//Bartlett窗的相干增益:0.5
                    windowdata=Bartlett(size, CG);
                    ENBW = 1.33333;//Bartlett窗的等效噪声宽度:1.5
                    break;
                case WindowType.BartlettHann:
                    CG = 0.5;//BartlettHann窗的相干增益:0.5
                    windowdata = BartlettHann(size, CG);
                    ENBW = 1.45585;//BartlettHann窗的等效噪声宽度:1.45585
                    break;
                case WindowType.Blackman:
                    CG = 0.42;//Blackman窗的相干增益:0.42
                    windowdata = Blackman(size, CG);
                    ENBW = 1.72676;//Blackman窗的等效噪声宽度:1.72676
                    break;
                case WindowType.BlackmanHarris:
                    CG = 0.35875;//BlackmanHarris窗的相干增益:0.35875
                    windowdata = BlackmanHarris(size, CG);
                    ENBW = 2.00435;//BlackmanHarris窗的等效噪声宽度:2.00435
                    break;
                case WindowType.BlackmanNuttall:
                    CG = 0.36582;//BlackmanNuttall窗的相干增益:0.36582
                    windowdata = BlackmanNuttall(size, CG);
                    ENBW = 1.97611;//BlackmanNuttall窗的等效噪声宽度:1.97611
                    break;
                case WindowType.Cosine:
                    CG = 0.63662;//Cosine窗的相干增益:0.63662
                    windowdata = Cosine(size, CG);
                    ENBW = 1.2337;//Cosine窗的等效噪声宽度:1.2337
                    break;
                case WindowType.Dirichlet:
                    CG = 1;//Dirichlet窗的相干增益:1
                    windowdata = Dirichlet(size, CG);
                    ENBW = 1;//Dirichlet窗的等效噪声宽度:1
                    break;
                case WindowType.FlatTop:
                    CG = 1;//FlatTop窗的相干增益:1
                    windowdata = FlatTop(size, CG);
                    ENBW = 3.77028;//FlatTop窗的等效噪声宽度:3.77028
                    break;
                case WindowType.Hamming:
                    CG = 0.53836;//Hamming窗的相干增益:0.53836
                    windowdata = Hamming(size, CG);
                    ENBW = 1.36765;//Hamming窗的等效噪声宽度:1.36765
                    break;
                case WindowType.Hann:
                    CG = 0.5;//Hann窗的相干增益:0.5
                    windowdata = Hann(size, CG);
                    ENBW = 1.5;//Hann窗的等效噪声宽度:1.5
                    break;
                case WindowType.Lanczos:
                    CG = 0.58949;//Lanczos窗的相干增益:0.58949
                    Lanczos(size, CG);
                    ENBW = 1.29903;//Lanczos窗的等效噪声宽度:1.29903
                    break;
                case WindowType.Nuttall:
                    CG = 0.355768;//Nuttall窗的相干增益:0.355768
                    windowdata = Nuttall(size, CG);
                    ENBW = 2.02123;//Nuttall窗的等效噪声宽度:2.02123
                    break;
                case WindowType.Triangular:
                    CG = 0.5;//Triangular窗的相干增益:0.5
                    windowdata = Triangular(size, CG);
                    ENBW = 1.33333;//Triangular窗的等效噪声宽度:1.33333
                    break;
                default:
                    throw new JYDSPParamException("Window type is out of range!");
            }
        }


        #region Source code from MathNet Window.cs
        /// <summary>
        /// Hamming window. Named after Richard Hamming.
        /// Symmetric version, useful e.g. for filter design purposes.
        /// </summary>
        private static double[] Hamming(int size, double cg)
        {
            const double a = 0.53836;
            const double b = -0.46164;

            double phaseStep = (2.0 * Math.PI) / (size - 1.0);
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a + b * Math.Cos(i * phaseStep)) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Hamming window. Named after Richard Hamming.
        /// Periodic version, useful e.g. for FFT purposes.
        /// </summary>
        private static double[] HammingPeriodic(int size, double cg)
        {
            const double a = 0.53836;
            const double b = -0.46164;

            double phaseStep = (2.0 * Math.PI) / size;
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a + b * Math.Cos(i * phaseStep)) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Hann window. Named after Julius von Hann.
        /// Symmetric version, useful e.g. for filter design purposes.
        /// </summary>
        private static double[] Hann(int size, double cg)
        {
            double phaseStep = (2.0 * Math.PI) / (size - 1.0);
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (0.5 - 0.5 * Math.Cos(i * phaseStep)) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Hann window. Named after Julius von Hann.
        /// Periodic version, useful e.g. for FFT purposes.
        /// </summary>
        private static double[] HannPeriodic(int size, double cg)
        {
            double phaseStep = (2.0 * Math.PI) / size;
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (0.5 - 0.5 * Math.Cos(i * phaseStep)) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Cosine window.
        /// Symmetric version, useful e.g. for filter design purposes.
        /// </summary>
        private static double[] Cosine(int size, double cg)
        {
            double phaseStep = Math.PI / (size - 1.0);
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (Math.Sin(i * phaseStep)) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Cosine window.
        /// Periodic version, useful e.g. for FFT purposes.
        /// </summary>
        private static double[] CosinePeriodic(int size, double cg)
        {
            double phaseStep = Math.PI / size;
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (Math.Sin(i * phaseStep)) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Lanczos window.
        /// Symmetric version, useful e.g. for filter design purposes.
        /// </summary>
        private static double[] Lanczos(int size, double cg)
        {
            double phaseStep = 2.0 / (size - 1.0);
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = ((Trig.Sinc(i * phaseStep - 1.0))) / cg;
            }));
            return waveData;

        }

        /// <summary>
        /// Lanczos window.
        /// Periodic version, useful e.g. for FFT purposes.
        /// </summary>
        private static double[] LanczosPeriodic(int size, double cg)
        {
            double phaseStep = 2.0 / size;
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (Trig.Sinc(i * phaseStep - 1.0)) / cg;
            }));
            return waveData;

        }

        /// <summary>
        /// Gauss window.
        /// </summary>
        private static double[] Gauss(int size, double sigma, double cg)
        {
            double a = (size - 1) / 2.0;
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                double exponent = (i - a) / (sigma * a);
                waveData[i] = (Math.Exp(-0.5 * exponent * exponent)) / cg;
            }));
            return waveData;

        }

        /// <summary>
        /// Blackman window.
        /// </summary>
        private static double[] Blackman(int size, double cg)
        {
            const double alpha = 0.16;
            const double a = 0.5 - 0.5 * alpha;
            const double b = 0.5 * alpha;
            int last = size - 1;
            double c = 2.0 * Math.PI / last;
            double d = 2.0 * c;

            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a - 0.5 * Math.Cos(i * c) + b * Math.Cos(i * d)) / cg;
            }));
            return waveData;

        }

        /// <summary>
        /// Blackman-Harris window.
        /// </summary>
        private static double[] BlackmanHarris(int size, double cg)
        {
            const double a = 0.35875;
            const double b = -0.48829;
            const double c = 0.14128;
            const double d = -0.01168;
            int last = size - 1;
            double e = 2.0 * Math.PI / last;
            double f = 2.0 * e;
            double g = 3.0 * e;

            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a + b * Math.Cos(e * i) + c * Math.Cos(f * i) + d * Math.Cos(g * i)) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Blackman-Nuttall window.
        /// </summary>
        private static double[] BlackmanNuttall(int size, double cg)
        {
            const double a = 0.3635819;
            const double b = -0.4891775;
            const double c = 0.1365995;
            const double d = -0.0106411;
            int last = size - 1;
            double e = 2.0 * Math.PI / last;
            double f = 2.0 * e;
            double g = 3.0 * e;

            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a + b * Math.Cos(e * i) + c * Math.Cos(f * i) + d * Math.Cos(g * i)) / cg;
            }));
            return waveData;

        }

        /// <summary>
        /// Bartlett window.
        /// </summary>
        private static double[] Bartlett(int size, double cg)
        {
            int last = size - 1;
            double a = 2.0 / last;
            double b = last / 2.0;

            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a * (b - Math.Abs(i - b))) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Bartlett-Hann window.
        /// </summary>
        private static double[] BartlettHann(int size, double cg)
        {
            const double a = 0.62;
            const double b = -0.48;
            const double c = -0.38;
            int last = size - 1;
            double d = 1.0 / last;
            double e = 2.0 * Math.PI / last;

            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a
                       + b * Math.Abs(i * d - 0.5)
                       + c * Math.Cos(i * e)) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Nuttall window.
        /// </summary>
        private static double[] Nuttall(int size, double cg)
        {
            const double a = 0.355768;
            const double b = -0.487396;
            const double c = 0.144232;
            const double d = -0.012604;

            int last = size - 1;
            double e = 2.0 * Math.PI / last;
            double f = 2.0 * e;
            double g = 3.0 * e;

            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a
                       + b * Math.Cos(e * i)
                       + c * Math.Cos(f * i)
                       + d * Math.Cos(g * i)) / cg;
            }));
            return waveData;

        }

        /// <summary>
        /// Flat top window.
        /// </summary>
        private static double[] FlatTop(int size, double cg)
        {
            const double a = 1.0;
            const double b = -1.93;
            const double c = 1.29;
            const double d = -0.388;
            const double e = 0.032;
            int last = size - 1;
            double f = 2.0 * Math.PI / last;
            double g = 2.0 * f;
            double h = 3.0 * f;
            double k = 4.0 * f;

            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a
                       + b * Math.Cos(f * i)
                       + c * Math.Cos(g * i)
                       + d * Math.Cos(h * i)
                       + e * Math.Cos(k * i)) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Uniform rectangular (Dirichlet) window.
        /// </summary>
        private static double[] Dirichlet(int size, double cg)
        {
            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (1.0) / cg;
            }));
            return waveData;
        }

        /// <summary>
        /// Triangular window.
        /// </summary>
        private static double[] Triangular(int size, double cg)
        {
            double a = 2.0 / size;
            double b = size / 2.0;
            double c = (size - 1) / 2.0;

            var waveData = new double[size];
            Parallel.For(0, size, new Action<int>(i =>
            {
                waveData[i] = (a * (b - Math.Abs(i - c))) / cg;
            }));
            return waveData;
        }

        #endregion
    }
}

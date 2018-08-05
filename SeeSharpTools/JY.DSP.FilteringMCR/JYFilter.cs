/*************************************************************
 * Matlab Code:
 * 
 * function [Y,b,a,n] = Cheb2( X,Wp,Ws,Wn,Rp,Rs,Type )
 * [n] = cheb2ord(Wp,Ws,Rp,Rs);
 * [b,a] = cheby2(n,Rs,Wn,Type);
 * Y = filter(b,a,X);
 * end
 * 
 * function [Y,b,a,n] = Butter( X,Wp,Ws,Rp,Rs,Type )
 * [n,Wn] = buttord(Wp,Ws,Rp,Rs);
 * [b,a] = butter(n,Wn,Type);
 * Y = filter(b,a,X);
 * end
 * 
 * function [Y,b,a,n] = Ellip( X,Wp,Ws,Wn,Rp,Rs,Type )
 * [n] = ellipord(Wp,Ws,Rp,Rs);
 * [b,a] = ellip(n,Rp,Rs,Wn,Type);
 * Y = filter(b,a,X);
 * end
 * 
 * function [Y,n,Wn,beta,ftype] = Kaiser(X,f,a,dev,fs)
 * [n,Wn,beta,ftype] = kaiserord(f,a,dev,fs);
 * b = fir1(n, Wn,ftype, kaiser(n+1,beta));
 * Y = filter(b,1,X);
 * end
 * 
**************************************************************/
using System;
using DSPMatlab;
using MathWorks.MATLAB.NET.Arrays;

namespace SeeSharpTools.JY.DSP.FilteringMCR
{
    #region Public fields
    /// <summary>
    /// Filter type: Lowpass/Highpass/Bandpass/Bandstop
    /// </summary>
    public enum FilterType
    {
        Lowpass,
        Highpass,
        Bandpass,
        Bandstop
    }
    #endregion

    /// <summary>
    /// IIR Filter Class
    /// </summary>
    public class IIRFilter
    {
        #region IIRFilter public fileds
        /// <summary>
        /// Filter design method: InvChebyshev/Butterworth/Elliptic
        /// </summary>
        public enum DesignMethod
        {
            InvChebyshev,
            Butterworth,
            Elliptic
        }
        #endregion

        #region IIRFilter public methods
        /// <summary>
        /// Initialize Matlab Engine.
        /// </summary>
        public static void Initialize()
        {
            double[] inputdata = new double[2];
            ProcessLowpass(inputdata, 0.1, 0.3, 2, DesignMethod.Butterworth);
            ProcessLowpass(inputdata, 0.1, 0.3, 2, DesignMethod.Elliptic);
            ProcessLowpass(inputdata, 0.1, 0.3, 2, DesignMethod.InvChebyshev);
        }

        /// <summary>
        /// IIR lowpass filter, design and process
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="fPass">Passband frequency</param>
        /// <param name="fStop">Stopband frequency</param>
        /// <param name="sampleRate">Samplerate</param>
        /// <param name="method">Design method</param>
        /// <param name="passbandRipple_dB">PassbandRipple(dB)</param>
        /// <param name="stopbandAttenuation_dB">StopbandAttenuation(dB)</param>
        /// <returns></returns>
        public static double[] ProcessLowpass(double[] inputData, double fPass, double fStop, double sampleRate = 1, DesignMethod method = DesignMethod.InvChebyshev, double passbandRipple_dB = 0.05, double stopbandAttenuation_dB = 60)
        {
            return Filter(inputData, method, FilterType.Lowpass, fPass, fStop, 0, 0, sampleRate, passbandRipple_dB, stopbandAttenuation_dB);
        }

        /// <summary>
        /// IIR highpass filter, design and process
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="fPass">Passband frequency</param>
        /// <param name="fStop">Stopband frequency</param>
        /// <param name="sampleRate">Samplerate</param>
        /// <param name="method">Design method</param>
        /// <param name="passbandRipple_dB">PassbandRipple(dB)</param>
        /// <param name="stopbandAttenuation_dB">StopbandAttenuation(dB)</param>
        /// <returns></returns>
        public static double[] ProcessHighpass(double[] inputData, double fPass, double fStop, double sampleRate = 1, DesignMethod method = DesignMethod.InvChebyshev, double passbandRipple_dB = 0.05, double stopbandAttenuation_dB = 60)
        {
            return Filter(inputData, method, FilterType.Highpass, fPass, fStop, 0, 0, sampleRate, passbandRipple_dB, stopbandAttenuation_dB);
        }

        /// <summary>
        /// IIR passband filter, design and process
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="fPass1">Passband left frequency</param>
        /// <param name="fPass2">Passband right frequency</param>
        /// <param name="fStop1">Stopband left frequency</param>
        /// <param name="fStop2">Stopband right frequency</param>
        /// <param name="sampleRate">Samplerate</param>
        /// <param name="method">Design method</param>
        /// <param name="passbandRipple_dB">PassbandRipple(dB)</param>
        /// <param name="stopbandAttenuation_dB">StopbandAttenuation(dB)</param>
        /// <returns></returns>
        public static double[] ProcessBandpass(double[] inputData, double fPass1, double fPass2, double fStop1, double fStop2, double sampleRate = 1, DesignMethod method = DesignMethod.InvChebyshev, double passbandRipple_dB = 0.05, double stopbandAttenuation_dB = 60)
        {
            return Filter(inputData, method, FilterType.Bandpass, fPass1, fStop1, fPass2, fStop2, sampleRate, passbandRipple_dB, stopbandAttenuation_dB);
        }

        /// <summary>
        /// IIR bandstop filter, design and process
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="fPass1">Passband left frequency</param>
        /// <param name="fPass2">Passband right frequency</param>
        /// <param name="fStop1">Stopband left frequency</param>
        /// <param name="fStop2">Stopband right frequency</param>
        /// <param name="sampleRate">Samplerate</param>
        /// <param name="method">Design method</param>
        /// <param name="passbandRipple_dB">PassbandRipple(dB)</param>
        /// <param name="stopbandAttenuation_dB">StopbandAttenuation(dB)</param>
        /// <returns></returns>
        public static double[] ProcessBandstop(double[] inputData, double fPass1, double fPass2, double fStop1, double fStop2, double sampleRate = 1, DesignMethod method = DesignMethod.InvChebyshev, double passbandRipple_dB = 0.05, double stopbandAttenuation_dB = 60)
        {
            return Filter(inputData, method, FilterType.Bandstop, fPass1, fStop1, fPass2, fStop2, sampleRate, passbandRipple_dB, stopbandAttenuation_dB);
        }

        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="mode">Folter design method</param>
        /// <param name="type">Filter type</param>
        /// <param name="fpass1">PassBand left frequency</param>
        /// <param name="fstop1">StopBand left frequency</param>
        /// <param name="fpass2">PassBand right frequency</param>
        /// <param name="fstop2">StopBand right frequency</param>
        /// <param name="fs">Samplerate</param>
        /// <param name="rp">Passband ripple</param>
        /// <param name="rs">Stopband attenuation</param>
        /// <returns>Output waveform data</returns>
        private static double[] Filter(double[] inputData,DesignMethod mode, FilterType type, double fpass1, double fstop1, double fpass2, double fstop2, double fs, double rp = 0.1, double rs = 50)
        {
            MWNumericArray waveMatlab = inputData;
            MWNumericArray wpMatlab;
            MWNumericArray wsMatlab;
            MWNumericArray wnMatlab;
            MWArray typeMatlab;
            MWArray rpMatlab = rp;
            MWArray rsMatlab = rs;
            MWArray[] resultMatlab;
            double[] fpassandFstop = new double[4];
            double[] frange = new double[2];
            double[,] result2D;
            double[] result;
            switch (type)
            {
                case FilterType.Highpass:
                    if (fpass1 > fstop1)
                    {
                        wpMatlab = fpass1 / (fs / 2);
                        wsMatlab = fstop1 / (fs / 2);
                    }
                    else
                    {
                        wpMatlab = fstop1 / (fs / 2);
                        wsMatlab = fpass1 / (fs / 2);
                    }
                    typeMatlab = "high";
                    if (mode == DesignMethod.InvChebyshev)
                        wnMatlab = wsMatlab;
                    else
                    {
                        wnMatlab = wpMatlab;
                    }
                    break;
                case FilterType.Lowpass:
                    if (fpass1 < fstop1)
                    {
                        wpMatlab = fpass1 / (fs / 2);
                        wsMatlab = fstop1 / (fs / 2);
                    }
                    else
                    {
                        wpMatlab = fstop1 / (fs / 2);
                        wsMatlab = fpass1 / (fs / 2);
                    }
                    typeMatlab = "low";
                    if (mode == DesignMethod.InvChebyshev)
                        wnMatlab = wsMatlab;
                    else
                    {
                        wnMatlab = wpMatlab;
                    }
                    break;

                case FilterType.Bandpass:
                    fpassandFstop[0] = fpass1 / (fs / 2);
                    fpassandFstop[1] = fstop1 / (fs / 2);
                    fpassandFstop[2] = fpass2 / (fs / 2);
                    fpassandFstop[3] = fstop2 / (fs / 2);
                    Array.Sort(fpassandFstop);
                    frange[0] = fpassandFstop[1];
                    frange[1] = fpassandFstop[2];
                    wpMatlab = frange;
                    frange[0] = fpassandFstop[0];
                    frange[1] = fpassandFstop[3];
                    wsMatlab = frange;
                    typeMatlab = "bandpass";
                    if (mode == DesignMethod.InvChebyshev)
                        wnMatlab = wsMatlab;
                    else
                    {
                        wnMatlab = wpMatlab;
                    }
                    break;
                case FilterType.Bandstop:
                default:
                    fpassandFstop[0] = fpass1 / (fs / 2);
                    fpassandFstop[1] = fstop1 / (fs / 2);
                    fpassandFstop[2] = fpass2 / (fs / 2);
                    fpassandFstop[3] = fstop2 / (fs / 2);
                    Array.Sort(fpassandFstop);
                    frange[0] = fpassandFstop[1];
                    frange[1] = fpassandFstop[2];
                    wpMatlab = frange;
                    frange[0] = fpassandFstop[0];
                    frange[1] = fpassandFstop[3];
                    wsMatlab = frange;
                    typeMatlab = "stop";
                    if (mode == DesignMethod.InvChebyshev)
                        wnMatlab = wpMatlab;
                    else
                    {
                        wnMatlab = wsMatlab;
                    }
                    break;

            }

            DSPClass dspTask = new DSPClass();
            switch (mode)
            {
                case DesignMethod.Butterworth:
                    resultMatlab = dspTask.Butter(1, waveMatlab, wpMatlab, wsMatlab, rpMatlab, rsMatlab, typeMatlab);
                    break;
                case DesignMethod.Elliptic:
                    resultMatlab = dspTask.Ellip(1, waveMatlab, wpMatlab, wsMatlab, wnMatlab, rpMatlab, rsMatlab, typeMatlab);
                    break;
                case DesignMethod.InvChebyshev:
                default:
                    resultMatlab = dspTask.Cheb2(1, waveMatlab, wpMatlab, wsMatlab, wnMatlab, rpMatlab, rsMatlab, typeMatlab);
                    break;
            }

            result2D = (double[,])resultMatlab[0].ToArray();
            result = new double[result2D.Length];
            Buffer.BlockCopy(result2D, 0, result, 0, result2D.Length * sizeof(double));
            return result;
        }
        #endregion

    }

    /// <summary>
    /// FIR filter class
    /// </summary>
    public class FIRFilter
    {
        #region FIRFilter public fileds
        /// <summary>
        /// FIR Design Method: Kaiser
        /// </summary>
        public enum DesignMethod
        { Kaiser }
        #endregion

        #region FIRFilter public methods
        /// <summary>
        /// Initialize Matlab Engine.
        /// </summary>
        public static void Initialize()
        {
            double[] inputdata = new double[2];
            ProcessLowpass(inputdata, 0.1, 0.3, 2);
        }

        /// <summary>
        /// FIR lowpass filter, design and process
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="fPass">Passband frequency</param>
        /// <param name="fStop">Stopband frequency</param>
        /// <param name="sampleRate">Samplerate</param>
        /// <param name="passbandRipple_dB">PassbandRipple(dB)</param>
        /// <param name="stopbandAttenuation_dB">StopbandAttenuation(dB)</param>
        /// <returns></returns>
        public static double[] ProcessLowpass(double[] inputData, double fPass, double fStop, double sampleRate = 1, double passbandRipple_dB = 0.05, double stopbandAttenuation_dB = 60)
        {
            double[] f = new double[2] { fPass, fStop };
            double[] a = new double[2] { 1, 0 };
            double passbandRipple_lin = 1 - (Math.Pow(10, passbandRipple_dB * (-1) / 20));
            double stopbandAttenuation_lin = Math.Pow(10, stopbandAttenuation_dB * (-1) / 20);
            double[] dev = new double[2] { passbandRipple_lin, stopbandAttenuation_lin };            
            Array.Sort(f);
            return Kaiser(inputData, f,a,dev, sampleRate);
        }

        /// <summary>
        /// FIR highpass filter, design and process
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="fPass">Passband frequency</param>
        /// <param name="fStop">Stopband frequency</param>
        /// <param name="sampleRate">Samplerate</param>
        /// <param name="passbandRipple_dB">PassbandRipple(dB)</param>
        /// <param name="stopbandAttenuation_dB">StopbandAttenuation(dB)</param>
        /// <returns></returns>
        public static double[] ProcessHighpass(double[] inputData, double fPass, double fStop, double sampleRate = 1, double passbandRipple_dB = 0.05, double stopbandAttenuation_dB = 60)
        {
            double[] f = new double[2] { fPass, fStop };
            double[] a = new double[2] { 0, 1 };
            double passbandRipple_lin = 1 - (Math.Pow(10, passbandRipple_dB * (-1) / 20));
            double stopbandAttenuation_lin = Math.Pow(10, stopbandAttenuation_dB * (-1) / 20);
            double[] dev = new double[2] { passbandRipple_lin, stopbandAttenuation_lin };
            Array.Sort(f);
            return Kaiser(inputData, f, a, dev, sampleRate);
        }

        /// <summary>
        /// FIR bandpass filter, design and process
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="fPass1">Passband left frequency</param>
        /// <param name="fPass2">Passband right frequency</param>
        /// <param name="fStop1">Stopband left frequency</param>
        /// <param name="fStop2">Stopband right frequency</param>
        /// <param name="sampleRate">Samplerate</param>
        /// <param name="passbandRipple_dB">PassbandRipple(dB)</param>
        /// <param name="stopbandAttenuation_dB">StopbandAttenuation(dB)</param>
        /// <returns></returns>
        public static double[] ProcessBandpass(double[] inputData, double fPass1, double fPass2, double fStop1, double fStop2, double sampleRate = 1, double passbandRipple_dB = 0.05, double stopbandAttenuation_dB = 60)
        {
            double[] f = new double[4] { fStop1,fPass1,fPass2, fStop2 };
            double[] a = new double[3] { 0, 1 ,0};
            double passbandRipple_lin = 1 - (Math.Pow(10, passbandRipple_dB * (-1) / 20));
            double stopbandAttenuation_lin = Math.Pow(10, stopbandAttenuation_dB * (-1) / 20);
            double[] dev = new double[3] { stopbandAttenuation_lin, passbandRipple_lin, stopbandAttenuation_lin };
            Array.Sort(f);
            return Kaiser(inputData, f, a, dev, sampleRate);
        }

        /// <summary>
        /// FIR bandpass filter, design and process
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="fPass1">Passband left frequency</param>
        /// <param name="fPass2">Passband right frequency</param>
        /// <param name="fStop1">Stopband left frequency</param>
        /// <param name="fStop2">Stopband right frequency</param>
        /// <param name="sampleRate">Samplerate</param>
        /// <param name="passbandRipple_dB">PassbandRipple(dB)</param>
        /// <param name="stopbandAttenuation_dB">StopbandAttenuation(dB)</param>
        /// <returns></returns>
        public static double[] ProcessBandstop(double[] inputData, double fPass1, double fPass2, double fStop1, double fStop2, double sampleRate = 1, double passbandRipple_dB = 0.05, double stopbandAttenuation_dB = 60)
        {
            double[] f = new double[4] { fPass1, fStop1, fStop2, fPass2 };
            double[] a = new double[3] { 1, 0, 1 };
            double passbandRipple_lin = 1 - (Math.Pow(10, passbandRipple_dB * (-1) / 20));
            double stopbandAttenuation_lin = Math.Pow(10, stopbandAttenuation_dB * (-1) / 20);
            double[] dev = new double[3] { passbandRipple_lin, stopbandAttenuation_lin, passbandRipple_lin };
            Array.Sort(f);
            return Kaiser(inputData, f, a, dev, sampleRate);
        }
        /// <summary>
        /// Kaiser Window Filter
        /// </summary>
        /// <param name="inputData">Input waform data</param>
        /// <param name="f">Band edges</param>
        /// <param name="a"> Desired amplitude on the bands define by f</param>
        /// <param name="dev">Passband ripple and stopband attenuation</param>
        /// <param name="fs">Samplerate</param>
        /// <returns>Output waveform data</returns>
        public static double[] Kaiser(double[] inputData, double[] f, double[] a, double[] dev, double fs)
        {

            MWNumericArray inputDataMatlab = inputData;
            MWNumericArray fMatlab = f;
            MWNumericArray aMatlab = a;
            MWNumericArray devMatlab = dev;
            MWArray fsMatlab = fs;
            MWArray[] resultMatlab;
            double[,] result2D;
            double[] result;
        
            DSPClass dspTask = new DSPClass();
            resultMatlab = dspTask.Kaiser(1, inputDataMatlab, fMatlab, aMatlab, devMatlab, fsMatlab);

            result2D = (double[,])resultMatlab[0].ToArray();
            result = new double[result2D.Length];
            Buffer.BlockCopy(result2D, 0, result, 0, result2D.Length * sizeof(double));
            return result;
        }
        #endregion
    }

}

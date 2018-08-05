using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    internal static class BasicFFT
    {
        /// <summary>
        /// DFT的描述符管理器
        /// </summary>
        private static readonly DFTIDescMgr DftiDescMgr;

        static BasicFFT()
        {
            DftiDescMgr = DFTIDescMgr.GetInstance();
        }

        #region ----------------Public: Basic FFT-----------------

        /// <summary>
        /// Real FFT
        /// </summary>
        /// <param name="xIn">time domain datas</param>
        /// <param name="xOut">complex frequency datas as array of Complex64</param>
        public static void RealFFT(double[] xIn, ref Complex[] xOut)
        {
            if (xIn == null || xOut == null || xOut.Length < xIn.Length / 2 + 1)
            {
                throw new JYDSPUserBufferException();
            }
            var gcXout = GCHandle.Alloc(xOut, GCHandleType.Pinned);
            var xOutPtr = gcXout.AddrOfPinnedObject();
            RealFFT(xIn, ref xOutPtr);
            gcXout.Free();
        }

        /// <summary>
        /// Real FFT
        /// </summary>
        /// <param name="xIn">time domain datas</param>
        /// <param name="xOut">complex frequency datas as array of double, re0 im0 re1 im1...</param>
        public static void RealFFT(double[] xIn, ref double[] xOut)
        {
            if (xIn == null || xOut == null || xOut.Length < xIn.Length + 2)
            {
                throw new JYDSPUserBufferException();
            }
            var gcXout = GCHandle.Alloc(xOut, GCHandleType.Pinned);
            var xOutPtr = gcXout.AddrOfPinnedObject();
            RealFFT(xIn, ref xOutPtr);
            gcXout.Free();
        }

        /// <summary>
        /// Real FFT
        /// </summary>
        /// <param name="xIn">complex frequency domain datas as array of double, re0 im0 re1 im1...</param>
        /// <param name="xOut">time domain datas</param>
        public static void RealIFFT(double[] xIn, ref double[] xOut)
        {
            if (xIn == null || xOut == null || xOut.Length < xIn.Length - 2)
            {
                throw new JYDSPUserBufferException();
            }
            var gcXin = GCHandle.Alloc(xIn, GCHandleType.Pinned);
            var xInPtr = gcXin.AddrOfPinnedObject();
            RealIFFT(xInPtr, ref xOut);
            gcXin.Free();
        }

        /// <summary>
        /// Real FFT
        /// </summary>
        /// <param name="xIn">complex frequency domain datas as array of double, re0 im0 re1 im1...</param>
        /// <param name="xOut">time domain datas</param>
        public static void RealIFFT(Complex[] xIn, ref double[] xOut)
        {
            if (xIn == null || xOut == null || xOut.Length < (xIn.Length - 1) * 2)
            {
                throw new JYDSPUserBufferException();
            }
            var gcXin = GCHandle.Alloc(xIn, GCHandleType.Pinned);
            var xInPtr = gcXin.AddrOfPinnedObject();
            RealIFFT(xInPtr, ref xOut);
            gcXin.Free();
        }

        /// <summary>
        /// Complex FFT
        /// </summary>
        /// <param name="xIn">time domain complex datas, x0.re x0.im x1.re x1.im...</param>
        /// <param name="xOut">complex frequency datas as array of Complex64</param>
        public static void ComplexFFT(double[] xIn, ref Complex[] xOut)
        {
            //not null, xOut length is enough, xIn Length is even
            if (xIn == null || xOut == null || xOut.Length < xIn.Length / 2 || ((xIn.Length & 1) == 1))
            {
                throw new JYDSPUserBufferException();
            }
            var gcXin = GCHandle.Alloc(xIn, GCHandleType.Pinned);
            var xInPtr = gcXin.AddrOfPinnedObject();
            var gcXOut = GCHandle.Alloc(xOut, GCHandleType.Pinned);
            var xOutPtr = gcXOut.AddrOfPinnedObject();
            try
            {
                ComplexFFT(xInPtr, xIn.Length / 2, ref xOutPtr);
            }
            finally
            {
                gcXin.Free();
                gcXOut.Free();
            }

        }

        /// <summary>
        /// Complex FFT
        /// </summary>
        /// <param name="xIn">time domain complex datas, x0.re x0.im x1.re x1.im...</param>
        /// <param name="xOut">complex frequency datas, y[0].re y[0].im y[1].re y[1].im...</param>
        public static void ComplexFFT(double[] xIn, ref double[] xOut)
        {
            //not null, xOut length is enough, xIn Length is even
            if (xIn == null || xOut == null || xOut.Length < xIn.Length / 2 || ((xIn.Length & 1) == 1))
            {
                throw new JYDSPUserBufferException();
            }
            var gcXin = GCHandle.Alloc(xIn, GCHandleType.Pinned);
            var xInPtr = gcXin.AddrOfPinnedObject();
            var gcXOut = GCHandle.Alloc(xOut, GCHandleType.Pinned);
            var xOutPtr = gcXOut.AddrOfPinnedObject();
            try
            {
                ComplexFFT(xInPtr, xIn.Length / 2, ref xOutPtr);
            }
            finally
            {
                gcXin.Free();
                gcXOut.Free();
            }

        }

        /// <summary>
        /// Complex FFT
        /// </summary>
        /// <param name="xIn">time domain complex datas, x0.re x0.im x1.re x1.im...</param>
        /// <param name="xOut">complex frequency datas, y[0].re y[0].im y[1].re y[1].im...</param>
        public static void ComplexFFT(Complex[] xIn, ref Complex[] xOut)
        {
            //not null, xOut length is enough
            if (xIn == null || xOut == null || xOut.Length < xIn.Length)
            {
                throw new JYDSPUserBufferException();
            }
            var gcXin = GCHandle.Alloc(xIn, GCHandleType.Pinned);
            var xInPtr = gcXin.AddrOfPinnedObject();
            var gcXOut = GCHandle.Alloc(xOut, GCHandleType.Pinned);
            var xOutPtr = gcXOut.AddrOfPinnedObject();
            try
            {
                ComplexFFT(xInPtr, xIn.Length, ref xOutPtr);
            }
            finally
            {
                gcXin.Free();
                gcXOut.Free();
            }
        }

        /// <summary>
        /// Complex IFFT
        /// </summary>
        /// <param name="xIn">frequency domain complex datas</param>
        /// <param name="xOut">complex time domain datas</param>
        public static void ComplexIFFT(Complex[] xIn, ref Complex[] xOut)
        {
            //not null, xOut length is enough
            if (xIn == null || xOut == null || xOut.Length < xIn.Length)
            {
                throw new JYDSPUserBufferException();
            }
            var gcXin = GCHandle.Alloc(xIn, GCHandleType.Pinned);
            var xInPtr = gcXin.AddrOfPinnedObject();
            var gcXOut = GCHandle.Alloc(xOut, GCHandleType.Pinned);
            var xOutPtr = gcXOut.AddrOfPinnedObject();
            try
            {
                ComplexIFFT(xInPtr, xIn.Length, ref xOutPtr);
            }
            finally
            {
                gcXin.Free();
                gcXOut.Free();
            }
        }

        /// <summary>
        /// Complex IFFT
        /// </summary>
        /// <param name="xIn">frequency domain complex datas, x0.re x0.im x1.re x1.im...</param>
        /// <param name="xOut">complex time domain datas, y0.re y0.im y1.re y1.im.../</param>
        public static void ComplexIFFT(double[] xIn, ref double[] xOut)
        {
            //not null, xOut length is enough
            if (xIn == null || xOut == null || xOut.Length < xIn.Length)
            {
                throw new JYDSPUserBufferException();
            }
            var gcXin = GCHandle.Alloc(xIn, GCHandleType.Pinned);
            var xInPtr = gcXin.AddrOfPinnedObject();
            var gcXOut = GCHandle.Alloc(xOut, GCHandleType.Pinned);
            var xOutPtr = gcXOut.AddrOfPinnedObject();
            try
            {
                ComplexIFFT(xInPtr, xIn.Length / 2, ref xOutPtr);
            }
            finally
            {
                gcXin.Free();
                gcXOut.Free();
            }
        }

        /// <summary>
        /// Complex IFFT
        /// </summary>
        /// <param name="xIn">frequency domain complex datas...</param>
        /// <param name="xOut">complex time domain datas, y0.re y0.im y1.re y1.im.../</param>
        public static void ComplexIFFT(Complex[] xIn, ref double[] xOut)
        {
            //not null, xOut length is enough
            if (xIn == null || xOut == null || xOut.Length < xIn.Length * 2)
            {
                throw new JYDSPUserBufferException();
            }
            var gcXin = GCHandle.Alloc(xIn, GCHandleType.Pinned);
            var xInPtr = gcXin.AddrOfPinnedObject();
            var gcXOut = GCHandle.Alloc(xOut, GCHandleType.Pinned);
            var xOutPtr = gcXOut.AddrOfPinnedObject();
            try
            {
                ComplexIFFT(xInPtr, xIn.Length, ref xOutPtr);
            }
            finally
            {
                gcXin.Free();
                gcXOut.Free();
            }
        }

        #endregion

        #region --------------Private Internal Method-------------

        private static void RealFFT(double[] xIn, ref IntPtr xOut)
        {
            int status;
            var fftDesc = DftiDescMgr.GetDFTDesc(xIn.Length, DFTType.Double1DRealInComplexOut);
            //ComputeForward
            if (0 != (status = DFTI.DftiComputeForward(fftDesc, xIn, xOut))) //计算FFT
            {
                throw new JYDSPInnerException("Dfti Compute Forward failed! error code=" + status);
            }
        }

        private static void ComplexFFT(IntPtr xIn, int n, ref IntPtr xOut)
        {
            int status;
            var fftDesc = DftiDescMgr.GetDFTDesc(n, DFTType.Double1DComplexInComplexOut);
            //ComputeForward
            if (0 != (status = DFTI.DftiComputeForward(fftDesc, xIn, xOut))) //计算FFT
            {
                throw new JYDSPInnerException("Dfti Compute Forward failed! error code=" + status);
            }
        }

        private static void RealIFFT(IntPtr xIn, ref double[] xOut)
        {
            int status;
            var fftDesc = DftiDescMgr.GetDFTDesc(xOut.Length, DFTType.Double1DRealInComplexOut);
            //ComputeForward
            if (0 != (status = DFTI.DftiComputeBackward(fftDesc, xIn, xOut)))//计算FFT
            {
                throw new JYDSPInnerException("Dfti Compute Forward failed! error code=" + status);
            }
        }

        private static void ComplexIFFT(IntPtr xIn, int n, ref IntPtr xOut)
        {
            int status;
            var fftDesc = DftiDescMgr.GetDFTDesc(n, DFTType.Double1DComplexInComplexOut);
            //ComputeForward
            if (0 != (status = DFTI.DftiComputeBackward(fftDesc, xIn, xOut)))//计算FFT
            {
                throw new JYDSPInnerException("Dfti Compute Forward failed! error code=" + status);
            }
        }

        #endregion
    }
}

/**********************************************
 * function[peakValue,peakIndex] = FindPeaks(y)
 * [peakValue,peakIndex] = findpeaks(y);
 * end
 * 
 * function[valleyValue,valleyIndex] = FindValleys(y)
 * y_inverted = -y;
 * [valleyValue_inverted,valleyIndex] = findpeaks(y_inverted);
 * valleyValue = -valleyValue_inverted;
 * end
 * 
 * *******************************************/
using FindPeaksAndValleysMatlab;
using MathWorks.MATLAB.NET.Arrays;
using System;

namespace SeeSharpTools.JY.DSP.Utility
{
    /// <summary>
    /// Peak and valley analysis
    /// </summary>
    public class PeakValleyAnalysis
    {
        /// <summary>
        /// Find peaks
        /// </summary>
        /// <param name="data">input values</param>
        /// <param name="minPeakProminence">min peak prominence</param>
        /// <param name="index">indexes of peaks</param>
        /// <param name="value">values of peaks</param>
        public static void FindPeaks(double[]data,double minPeakProminence,out double[]index,out double[]value)
        {
            MWNumericArray yMatlab = data;
            MWNumericArray minPeakProminenceMatlab = minPeakProminence;
            MWArray[] resultMatlab;
            double[,] resultx2D;
            double[,] resulty2D;
            PeakValleyMatlabClass matlabClass = new PeakValleyMatlabClass();
            resultMatlab = matlabClass.FindPeaks(2,yMatlab, minPeakProminenceMatlab);
            resulty2D = (double[,])resultMatlab[0].ToArray();
            resultx2D = (double[,])resultMatlab[1].ToArray();
            index = new double[resultx2D.Length];
            value = new double[resulty2D.Length];
            for(int i = 0; i < index.Length; i++)
            {
                index[i] = resultx2D[0, i] - 1;
            }
            Buffer.BlockCopy(resulty2D, 0, value, 0, value.Length * sizeof(double));
        }
        /// <summary>
        /// Find valleys
        /// </summary>
        /// <param name="data">input values</param>
        /// <param name="minPeakProminence">min peak prominence</param>
        /// <param name="index">indexes of valleys</param>
        /// <param name="value">values of valleys</param>
        public static void FindValleys(double[] data, double minPeakProminence, out double[] index, out double[] value)
        {
            MWNumericArray yMatlab = data;
            MWNumericArray minPeakProminenceMatlab = minPeakProminence;
            MWArray[] resultMatlab;
            double[,] resultx2D;
            double[,] resulty2D;
            PeakValleyMatlabClass matlabClass = new PeakValleyMatlabClass();
            resultMatlab = matlabClass.FindValleys(2,yMatlab, minPeakProminenceMatlab);
            resulty2D = (double[,])resultMatlab[0].ToArray();
            resultx2D = (double[,])resultMatlab[1].ToArray();
            index = new double[resultx2D.Length];
            value = new double[resulty2D.Length];
            for (int i = 0; i < index.Length; i++)
            {
                index[i] = resultx2D[0, i] - 1;
            }
            Buffer.BlockCopy(resulty2D, 0, value, 0, value.Length * sizeof(double));
        }
    }
}

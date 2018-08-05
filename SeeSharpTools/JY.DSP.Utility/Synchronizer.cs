using System;

namespace SeeSharpTools.JY.DSP.Utility
{
    /// <summary>
    /// Synchronizer class
    /// </summary>
    public static class Synchronizer
    {
        #region 公共方法
        /// <summary>
        /// <para>Synchronize method that makes all the input channels simultaneous.</para>
        /// <para>This method only applies to bandlimited signal such as sinusoidal waveform.</para>
        /// <para>Note: </para>
        /// <para>1 Array size should be numberOfChannels * SamplesPerChannel.</para>
        /// <para>2 Size of return array will be smaller than input array because of settlepoints truncation.</para>
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>syncdata after resample and filtering</returns>
        public static double[,] Sync(double[,] data)
        {
            int numOfChannels = data.GetLength(0);
            double dt = 1 / (double)numOfChannels;
            return Sync(data,dt);
        }

        /// <summary>
        /// <para>Synchronize method that makes all the input channels simultaneous.</para>
        /// <para>This method only applies to bandlimited signal such as sinusoidal waveform.</para>
        /// <para>Note: </para>
        /// <para>1 Array size should be numberOfChannels * SamplesPerChannel.</para>
        /// <para>2 Size of return array will be smaller than input array because of settlepoints truncation.</para>
        /// </summary>
        /// <param name="data">input data</param>
        /// <param name="shiftPoints">number of points you want to shift</param>
        /// <returns>syncdata after resample and filtering</returns>
        public static double[,] Sync(double[,] data, double shiftPoints)
        {
            double[] coef;//FIR interpolation filter coefficients 
            int interpFactor;//time of interpolation
            int settlePoints;//number of settle points

            //Initiallize synchronizer, which loads the interpolation filter coeficients
            //string filePath = @"\Desktop\相位差对齐\JYFilter_Resample.csv";
            //string[] allLines = File.ReadAllLines(filePath);

            //read filter cofficients from resource file
            string cofficient = Properties.Resources.Coefficient;
            char[] separator = { '\r', '\n' };
            string[] allLines = cofficient.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            coef = new double[allLines.Length - 2];
            for (int i = 0; i < allLines.Length - 2; i++)
            {
                coef[i] = Convert.ToDouble(allLines[i + 2]);
            }
            interpFactor = (int)Convert.ToDouble(allLines[0]);
            settlePoints = (coef.Length - 1) / 2 / interpFactor;

            int numOfChannels = data.GetLength(0);
            int samplesPerChannel = data.GetLength(1);
            double dt = shiftPoints;
            int outputLength = samplesPerChannel - settlePoints * 2;
            double tPhase = 1 / (double)interpFactor;
            double[,] syncData = new double[numOfChannels, outputLength];
            int filterIndexForXi = 0; //interpolation filter coef index for X[i]
            int filterIndexStart = 0;
            int subsamplePhaseJ = 0;
            int subsamplePhase = 0;
            double t0 = 0;
            double tFromI = 0;
            int i1 = 0;
            int filterCenterIndex = (coef.Length - 1) / 2;
            double filterScale = interpFactor;
            int iStart = 0;
            double[] Yi = new double[2];
            int m;
            double subphasePosition = 0;

            for (int chNum = 0; chNum < numOfChannels; chNum++)
            {
                t0 = settlePoints + 1 - chNum * dt;
                i1 = (int)Math.Floor(t0);
                tFromI = t0 - i1;
                subsamplePhaseJ = (int)Math.Floor(tFromI / tPhase);
                subphasePosition = tFromI / tPhase - subsamplePhaseJ;
                for (int i = i1; i < i1 + outputLength; i++)
                {
                    int outputIndex = i - i1;
                    for (int k = 0; k < 2; k++)
                    {
                        subsamplePhase = subsamplePhaseJ + k;
                        filterIndexForXi = filterCenterIndex - subsamplePhase;
                        filterIndexStart = filterIndexForXi % interpFactor;
                        iStart = i - (int)Math.Floor(filterIndexForXi / (double)interpFactor);
                        Yi[k] = 0;
                        for (int l = 0; l < (coef.Length / interpFactor); l++)
                        {
                            m = iStart + l;
                            if (m >= 0 && m < samplesPerChannel)
                            {
                                Yi[k] += data[chNum, m] * coef[filterIndexStart + l * interpFactor];
                            }
                        }
                        syncData[chNum, outputIndex] = ((Yi[1] - Yi[0]) * subphasePosition + Yi[0]) * filterScale;
                    }
                }
            }
            return syncData;
        }
        #endregion
    }
}

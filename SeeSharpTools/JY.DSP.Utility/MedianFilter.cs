using System;
using System.Threading.Tasks;

namespace SeeSharpTools.JY.DSP.Utility
{
    /// <summary>
    /// The Median Filter is a non-linear digital filtering technique, often used to remove noise from a signal.
    /// </summary>
    public static class MedianFilter
    {
        /// <summary>
        /// The block uses the sliding window method to compute the moving median. 
        /// In this method, a window of specified length moves  sample by sample, and the block computes the median of the data in the window. 
        /// This block performs median filtering on the input data over time.
        /// </summary>
        /// <param name="signal">Input signal</param>
        /// <param name="windowLength">Median filter window length, it should be 2N+1, and >=3</param>
        /// <returns></returns>
        public static double[] Process(double[] signal, int windowLength = 5)
        {
            //Verify window length 
            if (windowLength < 3 || windowLength % 2 == 0)
            {
                throw new Exception("Window length setting is wrong");
            }
            //Creat signal extension
            double[] signalExtension = new double[signal.Length + windowLength / 2*2];
            int signalLength = signal.Length;
            double[] result = new double[signalLength];

            Buffer.BlockCopy(signal, 0, signalExtension, windowLength / 2 * sizeof(double), signalLength * sizeof(double));
            for (int i = 0; i < windowLength / 2; i++)
            {
                signalExtension[i] = signal[windowLength / 2 - 1 - i];
                signalExtension[signalLength + windowLength / 2 + i] = signal[signalLength - 1 - i];
            }
            //Parallel caculate each window
            Parallel.For(0, signalLength, i => 
            {
                double[] window = new double[windowLength];
                Buffer.BlockCopy(signalExtension, i * sizeof(double), window, 0,windowLength * sizeof(double));
                //Order elements (only half of them)
                for(int j = 0; j< windowLength/2+1; j++)
                {
                    int min = j;
                    for(int k = j + 1; k < windowLength; k++)
                    {
                        if (window[k] < window[min])
                            min = k;
                    }
                    //Switch minimum element to front
                    double temp = window[j];
                    window[j] = window[min];
                    window[min] = temp;
                }
                //Get result - the middle element of window
                result[i] = window[windowLength / 2];
            });
            return result;                   
        }
    }
}

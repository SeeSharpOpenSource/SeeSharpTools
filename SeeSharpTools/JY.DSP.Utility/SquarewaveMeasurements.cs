using System;
using System.Linq;

namespace SeeSharpTools.JY.DSP.Utility
{
    /// <summary>
    /// Squarewave Measurements Class
    /// </summary>
    public class SquarewaveMeasurements
    {
        #region Private fileds
        private double[] waveform;
        private double highLevel=double.NaN;
        private double lowLevel = double.NaN;
        private double period = double.NaN;
        private double[] waveform2;
        private double phase=double.NaN;
        #endregion

        #region Public methods
        /// <summary>
        /// Set 1st square waveform.
        /// </summary>
        /// <param name="inputWaveform">Input square waveform.</param>
        public void SetWaveform(double[] inputWaveform)
        {
            waveform = new double[inputWaveform.Length];
            Array.Copy(inputWaveform, waveform, inputWaveform.Length);
            //waveform = inputWaveform will be buggy as the waveform points to the inputwaveform;
            InitStatus();
        }
        /// <summary>
        /// Get high state level of square waveform.
        /// (1st square waveform.)
        /// </summary>
        /// <returns></returns>
        public double GetHighStateLevel()
        {
            if (double.IsNaN(highLevel))
            {
                AmplitudeAnalysis();
            }
            return (highLevel);
        }
        /// <summary>
        /// Get low state level of square waveform.
        /// (1st square waveform.)
        /// </summary>
        /// <returns></returns>
        public double GetLowStateLevel()
        {
            if (double.IsNaN(lowLevel))
            {
                AmplitudeAnalysis();
            }
            return (lowLevel);
        }
        /// <summary>
        /// Get period of squere waveform.
        /// (1st squere waveform.)
        /// </summary>
        /// <returns></returns>
        public double GetPeriod()
        {
            if (double.IsNaN(period))
            {
                PeriodAnalysis();
            }
            return (period);
        }
        /// <summary>
        /// Set 2nd square waveform.
        /// </summary>
        /// <param name="inputWaveform"></param>
        public void SetWaveform2(double[] inputWaveform)
        {
            waveform2 = new double[inputWaveform.Length];
            Array.Copy(inputWaveform, waveform2, inputWaveform.Length);
            phase = double.NaN;
        }
        /// <summary>
        /// Get phase between 1st and 2nd square waveform.
        /// </summary>
        /// <returns></returns>
        public double GetPhase()
        {
            if (double.IsNaN(phase))
            {
                PhaseAnalysis();
            }
            return (phase);
        }
        #endregion

        #region Private methods
        private void InitStatus()
        {
            highLevel = double.NaN;
            lowLevel = double.NaN;
            period = double.NaN;
            phase = double.NaN;
        }
        private void AmplitudeAnalysis()
        {
            //devide the absolute max to min by 'intervals'
            //count the waveform levels within each interval
            //find the high level in the peak count from above
            //find the low level in the peak cout from bellow
            //get high and low levels by calculating the average of each peak interval;

            int intervals = 10;
            double intervalTopScale = 0.99;
            double[] intervalIntegrator = new double[intervals];
            int[] intervalCounter = new int[intervals];
            double waveformMax = waveform.Max();
            double waveformMin = waveform.Min();
            double waveformPeakPeak = waveformMax - waveformMin;
            if (waveformPeakPeak == 0)
            {
                return;
            }
            //integration of the histogram
            int i = 0;
            int intervalIndex = 0;
            for (i = 0; i < waveform.Length; i++)
            {
                intervalIndex = (int)((waveform[i] - waveformMin) / waveformPeakPeak * intervals * intervalTopScale);
                intervalIntegrator[intervalIndex] += waveform[i];
                intervalCounter[intervalIndex]++;
            }
            //find the peak interval
            int peakIndex = intervals - 1;
            while (intervalCounter[peakIndex] < waveform.Length / intervals
            || intervalCounter[peakIndex - 1] >= intervalCounter[peakIndex])
            {
                peakIndex--;
            }
            highLevel = intervalIntegrator[peakIndex] / intervalCounter[peakIndex];
            //find the valley interval
            int valleyIndex = 0;
            while (intervalCounter[valleyIndex] < waveform.Length / intervals
                || intervalCounter[valleyIndex + 1] >= intervalCounter[valleyIndex])
            {
                valleyIndex++;
            }
            lowLevel = intervalIntegrator[valleyIndex] / intervalCounter[valleyIndex];

        }
        private void PeriodAnalysis()
        {
            // make sure amplitude analysis is done
            GetHighStateLevel();
            // use high low levels to analysis the period
            double amplitude = highLevel - lowLevel;
            double stateTollerance = 0.1*amplitude;
            double highTreshold = highLevel - stateTollerance;
            double lowThreshold = lowLevel + stateTollerance;
            bool enteringHigh = false;
            bool enteredLow = false;
            int edgeCount = 0;
            int lastEdge = 0;
            int periodCount = 0;
            double periodIntegrator = 0;
            for (int i = 1; i < waveform.Length; i++)
            {
                enteredLow = enteredLow || 
                    (waveform[i - 1] > lowThreshold && waveform[i] <= lowThreshold);
                enteringHigh = waveform[i - 1] < highTreshold && waveform[i] >= highTreshold;
                if(enteringHigh && enteredLow)
                {
                    enteredLow = false;
                    edgeCount++;
                    if(edgeCount>1)
                    {
                        periodCount++;
                        periodIntegrator += i - lastEdge;
                    }
                    lastEdge = i;
                }
            }
            period = periodIntegrator / periodCount;

        }
        private void PhaseAnalysis()
        {
            double productSum = 0;
            double maxProductSum = 0;
            double offsetOfMax = 0;
            //make sure amplitude and period are calculated
            GetHighStateLevel();
            GetPeriod();
            if(Math.Min(waveform.Length,waveform2.Length)>2*period)
            {
                int compareLength = Math.Min((int) (waveform.Length - period),waveform2.Length);
                for (int indexOffset = 0; indexOffset < period; indexOffset++)
                {
                    productSum = 0;
                    for (int i = 0; i < compareLength; i++)
                    {
                        productSum += waveform[i] * waveform2[i + indexOffset];
                    }
                    if(productSum>maxProductSum)
                    {
                        maxProductSum = productSum;
                        offsetOfMax = indexOffset;
                    }
                }
                phase = 360 * offsetOfMax / period;
            }
        }
        #endregion
    }
}

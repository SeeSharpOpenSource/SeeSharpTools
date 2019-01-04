using MathNet.Numerics.Statistics;
using SeeSharpTools.JY.Mathematics.Interfaces;
using System;
using System.Threading.Tasks;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    public partial class ProviderBase : IStatistics
    {
        public virtual int[] Histogram(double[] data, int binSize, double min = 0, double max = 0)
        {
            try
            {
                Histogram hgram;
                int[] stats = new int[binSize];
                if (max <= min)
                {
                    hgram = new Histogram(data, binSize);
                }
                else
                {
                    hgram = new Histogram(data, binSize, min, max);
                }
                Parallel.For(0, binSize, new Action<int>(i => { stats[i] = (int)hgram[i].Count; }));
                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual double Kurtosis(double[] src)
        {
            return src.Kurtosis();
        }

        public virtual double Mean(double[] src)
        {
            return src.Mean();
        }

        public virtual double Median(double[] src)
        {
            return src.Median();
        }

        public virtual double Percentile(double[] data, int place)
        {
            return data.Percentile(place);
        }

        public virtual double RMS(double[] src)
        {
            return src.RootMeanSquare();
        }

        public virtual double Skewness(double[] src)
        {
            return src.Skewness();
        }

        public virtual double StandardDeviation(double[] src)
        {
            return src.StandardDeviation();
        }

        public virtual double Variance(double[] src)
        {
            return src.Variance();
        }
    }
}
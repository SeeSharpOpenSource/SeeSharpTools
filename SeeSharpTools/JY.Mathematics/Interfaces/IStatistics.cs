namespace SeeSharpTools.JY.Mathematics.Interfaces
{
    internal interface IStatistics
    {
        int[] Histogram(double[] data, int binSize, double min = 0, double max = 0);

        double Kurtosis(double[] src);

        double Mean(double[] src);

        double Median(double[] src);

        double Percentile(double[] data, int place);

        double RMS(double[] src);

        double Skewness(double[] src);

        double StandardDeviation(double[] src);

        double Variance(double[] src);
    }
}
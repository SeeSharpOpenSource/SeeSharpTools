using System;

namespace SeeSharpTools.JY.Mathematics.Interfaces
{
    internal interface IFitting
    {
        Func<double, double> LinearFittingFunc(double[] x, double[] y);

        Tuple<double, double> LinearFitting(double[] x, double[] y);

        Func<double, double> ExponentialFittingFunc(double[] x, double[] y);

        Tuple<double, double> ExponentialFitting(double[] x, double[] y);

        Func<double, double> PolynomialFittingFunc(double[] x, double[] y, int order);

        double[] PolynomialFitting(double[] x, double[] y, int order);
    }
}
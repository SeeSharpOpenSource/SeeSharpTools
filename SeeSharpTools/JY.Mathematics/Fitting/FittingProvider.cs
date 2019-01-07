using MathNet.Numerics;
using SeeSharpTools.JY.Mathematics.Interfaces;
using System;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    public partial class ProviderBase : IFitting
    {
        public virtual Func<double, double> ExponentialFittingFunc(double[] x, double[] y)
        {
            return Fit.ExponentialFunc(x, y);
        }

        public virtual Tuple<double, double> ExponentialFitting(double[] x, double[] y)
        {
            return Fit.Exponential(x, y);
        }

        public virtual Func<double, double> LinearFittingFunc(double[] x, double[] y)
        {
            return Fit.LineFunc(x, y);
        }

        public virtual Tuple<double, double> LinearFitting(double[] x, double[] y)
        {
            return Fit.Line(x, y);
        }

        public virtual Func<double, double> PolynomialFittingFunc(double[] x, double[] y, int order)
        {
            return Fit.PolynomialFunc(x, y, order);
        }

        public virtual double[] PolynomialFitting(double[] x, double[] y, int order)
        {
            return Fit.Polynomial(x, y, order);
        }
    }
}
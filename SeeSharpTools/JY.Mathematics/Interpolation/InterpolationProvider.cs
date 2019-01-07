using MathNet.Numerics.Interpolation;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    public partial class ProviderBase : Interfaces.IInterpolation
    {
        public virtual double Interpolate_CubicSpline(double[] x, double[] y, double xValue)
        {
            return CubicSpline.InterpolateAkima(x, y).Interpolate(xValue);
        }

        public virtual double Interpolate_Linear(double[] x, double[] y, double xValue)
        {
            return LinearSpline.Interpolate(x, y).Interpolate(xValue);
        }

        public virtual double Interpolate_LogLinear(double[] x, double[] y, double xValue)
        {
            return LogLinear.Interpolate(x, y).Interpolate(xValue);
        }

        public virtual double Interpolate_Polynomial(double[] x, double[] y, double xValue)
        {
            return NevillePolynomialInterpolation.Interpolate(x, y).Interpolate(xValue);
        }

        public virtual double Interpolate_Step(double[] x, double[] y, double xValue)
        {
            return StepInterpolation.Interpolate(x, y).Interpolate(xValue);
        }
    }
}
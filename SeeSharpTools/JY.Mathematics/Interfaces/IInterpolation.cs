namespace SeeSharpTools.JY.Mathematics.Interfaces
{
    internal interface IInterpolation
    {
        double Interpolate_CubicSpline(double[] x, double[] y, double xValue);

        double Interpolate_Linear(double[] x, double[] y, double xValue);

        double Interpolate_LogLinear(double[] x, double[] y, double xValue);

        double Interpolate_Polynomial(double[] x, double[] y, double xValue);

        double Interpolate_Step(double[] x, double[] y, double xValue);
    }
}
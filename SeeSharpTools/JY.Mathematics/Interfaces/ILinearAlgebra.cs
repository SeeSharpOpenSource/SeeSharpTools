namespace SeeSharpTools.JY.Mathematics.Interfaces
{
    internal interface ILinearAlgebra
    {
        double Dot(double[] a, double[] b);

        float Dot(float[] a, float[] b);

        double Norm(double[] a, double p);

        double Norm(float[] a, double p);
    }
}
using SeeSharpTools.JY.Mathematics.Interfaces;
using Double = MathNet.Numerics.LinearAlgebra.Double;
using Single = MathNet.Numerics.LinearAlgebra.Single;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    public partial class ProviderBase : ILinearAlgebra
    {
        public virtual double Dot(double[] a, double[] b)
        {
            Double.DenseVector vector_a = new Double.DenseVector(a);
            return vector_a.DotProduct(new Double.DenseVector(b));
        }

        public virtual float Dot(float[] a, float[] b)
        {
            Single.DenseVector vector_a = new Single.DenseVector(a);
            return vector_a.DotProduct(new Single.DenseVector(b));
        }

        public virtual double Norm(double[] a, double p)
        {
            Double.DenseVector vector_a = new Double.DenseVector(a);
            return vector_a.Norm(p);
        }

        public virtual double Norm(float[] a, double p)
        {
            Single.DenseVector vector_a = new Single.DenseVector(a);
            return vector_a.Norm(p);
        }
    }
}
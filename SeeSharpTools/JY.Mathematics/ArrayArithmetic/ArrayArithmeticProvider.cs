using SeeSharpTools.JY.Mathematics.Interfaces;
using System;
using System.Linq;
using SeeSharpTools.JY.ArrayUtility;
using Double = MathNet.Numerics.LinearAlgebra.Double;
using Single = MathNet.Numerics.LinearAlgebra.Single;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    public partial class ProviderBase : IArrayArithmetic
    {
        #region ArrayArithmetic

        public virtual void Absolute(double[] src, ref double[] dest)
        {
            ArrayCalculation.Abs(src, ref dest);
        }

        public virtual void Absolute(float[] src, ref float[] dest)
        {
            ArrayCalculation.Abs(src, ref dest);
        }

        public virtual void ACos(double value, ref double returnValue)
        {
            Double.Vector input = new Double.DenseVector(new double[] { value });
            Double.Vector output = new Double.DenseVector(new double[] { returnValue });
            output = (Double.DenseVector)Double.Vector.Acos(input);
            returnValue = output[0];
        }

        public virtual void ACos(double[] value, ref double[] returnValue)
        {
            Double.Vector input = new Double.DenseVector(value);
            Double.Vector output = new Double.DenseVector(returnValue);
            output = (Double.DenseVector)Double.Vector.Acos(input);
            returnValue = output.ToArray();
        }

        public virtual void ACos(float value, ref float returnValue)
        {
            Single.Vector input = new Single.DenseVector(new float[] { value });
            Single.Vector output = new Single.DenseVector(new float[] { returnValue });
            output = (Single.DenseVector)Single.Vector.Acos(input);
            returnValue = output[0];
        }

        public virtual void ACos(float[] value, ref float[] returnValue)
        {
            Single.Vector input = new Single.DenseVector(value);
            Single.Vector output = new Single.DenseVector(returnValue);
            output = (Single.DenseVector)Single.Vector.Acos(input);
            returnValue = output.ToArray();
        }

        public virtual void Add(double[] src1, double[] src2, ref double[] dest)
        {
            ArrayCalculation.Add(src1, src2, ref dest);
        }

        public virtual void Add(float[] src1, float[] src2, ref float[] dest)
        {
            ArrayCalculation.Add(src1, src2, ref dest);
        }

        public virtual void Add(short[] src1, short[] src2, ref short[] dest)
        {
            ArrayCalculation.Add(src1, src2, ref dest);
        }

        public virtual void Add(ref double[] srcDest, double[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] += src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Add(ref float[] srcDest, float[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] += src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Add(ref short[] srcDest, short[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] += src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Add(double[] src1, double scalar, ref double[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] + scalar; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Add(float[] src1, float scalar, ref float[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] + scalar; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Add(ref double[] srcDest, double scalar)
        {
            for (int i = 0; i < srcDest.Length; i++) { srcDest[i] += scalar; }
        }

        public virtual void Add(ref float[] srcDest, float scalar)
        {
            for (int i = 0; i < srcDest.Length; i++) { srcDest[i] += scalar; }
        }

        public virtual void Add(ref short[] srcDest, short scalar)
        {
            for (int i = 0; i < srcDest.Length; i++) { srcDest[i] += scalar; }
        }

        public virtual void ASin(double value, ref double returnValue)
        {
            Double.Vector input = new Double.DenseVector(new double[] { value });
            Double.Vector output = new Double.DenseVector(new double[] { returnValue });
            output = (Double.DenseVector)Double.Vector.Asin(input);
            returnValue = output[0];
        }

        public virtual void ASin(double[] value, ref double[] returnValue)
        {
            Double.Vector input = new Double.DenseVector(value);
            Double.Vector output = new Double.DenseVector(returnValue);
            output = (Double.DenseVector)Double.Vector.Asin(input);
            returnValue = output.ToArray();
        }

        public virtual void ASin(float value, ref float returnValue)
        {
            Single.Vector input = new Single.DenseVector(new float[] { value });
            Single.Vector output = new Single.DenseVector(new float[] { returnValue });
            output = (Single.DenseVector)Single.Vector.Asin(input);
            returnValue = output[0];
        }

        public virtual void ASin(float[] value, ref float[] returnValue)
        {
            Single.Vector input = new Single.DenseVector(value);
            Single.Vector output = new Single.DenseVector(returnValue);
            output = (Single.DenseVector)Single.Vector.Asin(input);
            returnValue = output.ToArray();
        }

        public virtual void ATan(double value, ref double returnValue)
        {
            Double.Vector input = new Double.DenseVector(new double[] { value });
            Double.Vector output = new Double.DenseVector(new double[] { returnValue });
            output = (Double.DenseVector)Double.Vector.Atan(input);
            returnValue = output[0];
        }

        public virtual void ATan(double[] value, ref double[] returnValue)
        {
            Double.Vector input = new Double.DenseVector(value);
            Double.Vector output = new Double.DenseVector(returnValue);
            output = (Double.DenseVector)Double.Vector.Atan(input);
            returnValue = output.ToArray();
        }

        public virtual void ATan(float value, ref float returnValue)
        {
            Single.Vector input = new Single.DenseVector(new float[] { value });
            Single.Vector output = new Single.DenseVector(new float[] { returnValue });
            output = (Single.DenseVector)Single.Vector.Atan(input);
            returnValue = output[0];
        }

        public virtual void ATan(float[] value, ref float[] returnValue)
        {
            Single.Vector input = new Single.DenseVector(value);
            Single.Vector output = new Single.DenseVector(returnValue);
            output = (Single.DenseVector)Single.Vector.Atan(input);
            returnValue = output.ToArray();
        }

        public virtual void Cos(double value, ref double returnValue)
        {
            Double.Vector input = new Double.DenseVector(new double[] { value });
            Double.Vector output = new Double.DenseVector(new double[] { returnValue });
            output = (Double.DenseVector)Double.Vector.Cos(input);
            returnValue = output[0];
        }

        public virtual void Cos(double[] value, ref double[] returnValue)
        {
            Double.Vector input = new Double.DenseVector(value);
            Double.Vector output = new Double.DenseVector(returnValue);
            output = (Double.DenseVector)Double.Vector.Cos(input);
            returnValue = output.ToArray();
        }

        public virtual void Cos(float value, ref float returnValue)
        {
            Single.Vector input = new Single.DenseVector(new float[] { value });
            Single.Vector output = new Single.DenseVector(new float[] { returnValue });
            output = (Single.DenseVector)Single.Vector.Cos(input);
            returnValue = output[0];
        }

        public virtual void Cos(float[] value, ref float[] returnValue)
        {
            Single.Vector input = new Single.DenseVector(value);
            Single.Vector output = new Single.DenseVector(returnValue);
            output = (Single.DenseVector)Single.Vector.Cos(input);
            returnValue = output.ToArray();
        }

        public virtual void Divide(double[] src1, double[] src2, ref double[] dest)
        {
            if (src1.Length == src2.Length && src2.Length == dest.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] / src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Divide(float[] src1, float[] src2, ref float[] dest)
        {
            if (src1.Length == src2.Length && src2.Length == dest.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] / src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Divide(short[] src1, short[] src2, ref short[] dest)
        {
            if (src1.Length == src2.Length && src2.Length == dest.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = (short)((float)src1[i] / src2[i]); }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Divide(ref double[] srcDest, double[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] = srcDest[i] / src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Divide(ref float[] srcDest, float[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] = srcDest[i] / src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Divide(ref short[] srcDest, short[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] = (short)((float)srcDest[i] / src2[i]); }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Divide(double[] src1, double scalar, ref double[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] / scalar; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Divide(float[] src1, float scalar, ref float[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] / scalar; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Divide(ref double[] srcDest, double scalar)
        {
            for (int i = 0; i < srcDest.Length; i++) { srcDest[i] = srcDest[i] / scalar; }
        }

        public virtual void Divide(ref float[] srcDest, float scalar)
        {
            for (int i = 0; i < srcDest.Length; i++) { srcDest[i] = srcDest[i] / scalar; }
        }

        public virtual void Divide(ref short[] srcDest, short scalar)
        {
            for (int i = 0; i < srcDest.Length; i++) { srcDest[i] = (short)((float)srcDest[i] / scalar); }
        }

        public virtual void Exp(double[] src, ref double[] dest)
        {
            dest = Double.Vector.Exp(new Double.DenseVector(src)).ToArray();
        }

        public virtual void Exp(float[] src, ref float[] dest)
        {
            dest = Single.Vector.Exp(new Single.DenseVector(src)).ToArray();
        }

        public virtual void FindMaxMin(double[] src, out double maxValue, out double minValue, out int maxIdx, out int minIdx)
        {
            double max = src.Max();
            double min = src.Min();
            maxValue = max;
            minValue = min;
            maxIdx = Array.FindIndex(src, x => x == max);
            minIdx = Array.FindIndex(src, x => x == min);
        }

        public virtual void FindMaxMin(float[] src, out float maxValue, out float minValue, out int maxIdx, out int minIdx)
        {
            float max = src.Max();
            float min = src.Min();
            maxValue = max;
            minValue = min;
            maxIdx = Array.FindIndex(src, x => x == max);
            minIdx = Array.FindIndex(src, x => x == min);
        }

        public virtual void FindMaxMin(short[] src, out short maxValue, out short minValue, out int maxIdx, out int minIdx)
        {
            short max = src.Max();
            short min = src.Min();
            maxValue = max;
            minValue = min;
            maxIdx = Array.FindIndex(src, x => x == max);
            minIdx = Array.FindIndex(src, x => x == min);
        }

        public virtual void Initialize(ref double[] dest, double initValue)
        {
            ArrayCalculation.InitializeArray(ref dest, initValue);
        }

        public virtual void Initialize(ref float[] dest, float initValue)
        {
            ArrayCalculation.InitializeArray(ref dest, initValue);
        }

        public virtual void Ln(double[] src, ref double[] dest)
        {
            dest = Double.Vector.Log(new Double.DenseVector(src)).ToArray();
        }

        public virtual void Ln(float[] src, ref float[] dest)
        {
            dest = Single.Vector.Log(new Single.DenseVector(src)).ToArray();
        }

        public virtual void Log(double[] src, ref double[] dest)
        {
            dest = Double.Vector.Log10(new Double.DenseVector(src)).ToArray();
        }

        public virtual void Log(float[] src, ref float[] dest)
        {
            dest = Single.Vector.Log10(new Single.DenseVector(src)).ToArray();
        }

        public virtual void Multiply(double[] src1, double[] src2, ref double[] dest)
        {
            ArrayCalculation.Multiply(src1, src2, ref dest);
        }

        public virtual void Multiply(float[] src1, float[] src2, ref float[] dest)
        {
            ArrayCalculation.Multiply(src1, src2, ref dest);
        }

        public virtual void Multiply(short[] src1, short[] src2, ref short[] dest)
        {
            ArrayCalculation.Multiply(src1, src2, ref dest);
        }

        public virtual void Multiply(ref double[] srcDest, double[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] *= src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Multiply(ref float[] srcDest, float[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] *= src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Multiply(ref short[] srcDest, short[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] *= src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Multiply(double[] src1, double scalar, ref double[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] * scalar; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Multiply(float[] src1, float scalar, ref float[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] * scalar; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Multiply(ref double[] srcDest, double scalar)
        {
            ArrayCalculation.MultiplyScale(ref srcDest, scalar);
        }

        public virtual void Multiply(ref float[] srcDest, float scalar)
        {
            ArrayCalculation.MultiplyScale(ref srcDest, scalar);
        }

        public virtual void Multiply(ref short[] srcDest, short scalar)
        {
            ArrayCalculation.MultiplyScale(ref srcDest, scalar);
        }

        public virtual void Pow(double[] src1, double scalar, ref double[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = Math.Pow(src1[i], scalar); }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Pow(float[] src1, float scalar, ref float[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = (float)Math.Pow((double)src1[i], (double)scalar); }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual double Product(double[] src1)
        {
            double ans = 1;
            for (int i = 0; i < src1.Length; i++) { ans *= src1[i]; }
            return ans;
        }

        public virtual double Product(float[] src1)
        {
            double ans = 1;
            for (int i = 0; i < src1.Length; i++) { ans *= src1[i]; }
            return ans;
        }

        public virtual void Sin(double value, ref double returnValue)
        {
            Double.Vector input = new Double.DenseVector(new double[] { value });
            Double.Vector output = new Double.DenseVector(new double[] { returnValue });
            output = (Double.DenseVector)Double.Vector.Sin(input);
            returnValue = output[0];
        }

        public virtual void Sin(double[] value, ref double[] returnValue)
        {
            Double.Vector input = new Double.DenseVector(value);
            Double.Vector output = new Double.DenseVector(returnValue);
            output = (Double.DenseVector)Double.Vector.Sin(input);
            returnValue = output.ToArray();
        }

        public virtual void Sin(float value, ref float returnValue)
        {
            Single.Vector input = new Single.DenseVector(new float[] { value });
            Single.Vector output = new Single.DenseVector(new float[] { returnValue });
            output = (Single.DenseVector)Single.Vector.Sin(input);
            returnValue = output[0];
        }

        public virtual void Sin(float[] value, ref float[] returnValue)
        {
            Single.Vector input = new Single.DenseVector(value);
            Single.Vector output = new Single.DenseVector(returnValue);
            output = (Single.DenseVector)Single.Vector.Sin(input);
            returnValue = output.ToArray();
        }

        public virtual void Sqrt(double[] src, ref double[] dest)
        {
            dest = Double.Vector.Sqrt(new Double.DenseVector(src)).ToArray();
        }

        public virtual void Sqrt(float[] src, ref float[] dest)
        {
            dest = Single.Vector.Sqrt(new Single.DenseVector(src)).ToArray();
        }

        public virtual void Substract(double[] src1, double[] src2, ref double[] dest)
        {
            ArrayCalculation.Subtract(src1, src2, ref dest);
        }

        public virtual void Substract(float[] src1, float[] src2, ref float[] dest)
        {
            ArrayCalculation.Subtract(src1, src2, ref dest);
        }

        public virtual void Substract(short[] src1, short[] src2, ref short[] dest)
        {
            ArrayCalculation.Subtract(src1, src2, ref dest);
        }

        public virtual void Substract(ref double[] srcDest, double[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] -= src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Substract(ref float[] srcDest, float[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] -= src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Substract(ref short[] srcDest, short[] src2)
        {
            if (srcDest.Length == src2.Length)
            {
                for (int i = 0; i < srcDest.Length; i++) { srcDest[i] -= src2[i]; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Substract(double[] src1, double scalar, ref double[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] - scalar; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Substract(float[] src1, float scalar, ref float[] dest)
        {
            if (dest.Length == src1.Length)
            {
                for (int i = 0; i < dest.Length; i++) { dest[i] = src1[i] - scalar; }
            }
            else
            {
                throw new ArgumentException("Array length inconsistent");
            }
        }

        public virtual void Substract(ref double[] srcDest, double scalar)
        {
            ArrayCalculation.SubtractOffset(ref srcDest, scalar);
        }

        public virtual void Substract(ref float[] srcDest, float scalar)
        {
            ArrayCalculation.SubtractOffset(ref srcDest, scalar);
        }

        public virtual void Substract(ref short[] srcDest, short scalar)
        {
            ArrayCalculation.SubtractOffset(ref srcDest, scalar);
        }

        public virtual double Sum(double[] src1)
        {
            return ArrayCalculation.Sum(src1);
        }

        public virtual double Sum(float[] src1)
        {
            return ArrayCalculation.Sum(src1);
        }

        public virtual void Tan(double value, ref double returnValue)
        {
            Double.Vector input = new Double.DenseVector(new double[] { value });
            Double.Vector output = new Double.DenseVector(new double[] { returnValue });
            output = (Double.DenseVector)Double.Vector.Tan(input);
            returnValue = output[0];
        }

        public virtual void Tan(double[] value, ref double[] returnValue)
        {
            Double.Vector input = new Double.DenseVector(value);
            Double.Vector output = new Double.DenseVector(returnValue);
            output = (Double.DenseVector)Double.Vector.Tan(input);
            returnValue = output.ToArray();
        }

        public virtual void Tan(float value, ref float returnValue)
        {
            Single.Vector input = new Single.DenseVector(new float[] { value });
            Single.Vector output = new Single.DenseVector(new float[] { returnValue });
            output = (Single.DenseVector)Single.Vector.Tan(input);
            returnValue = output[0];
        }

        public virtual void Tan(float[] value, ref float[] returnValue)
        {
            Single.Vector input = new Single.DenseVector(value);
            Single.Vector output = new Single.DenseVector(returnValue);
            output = (Single.DenseVector)Single.Vector.Tan(input);
            returnValue = output.ToArray();
        }

        #endregion ArrayArithmetic
    }
}
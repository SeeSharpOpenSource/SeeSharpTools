using System;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    public class ProviderIPP : ProviderBase
    {
        public unsafe override void Absolute(double[] src, ref double[] dest)
        {
            if (src.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = src, pDst = dest)
            {
                IPPNative.ippsAbs_64f(pSrc, pDst, src.Length);
            }
        }

        public unsafe override void Absolute(float[] src, ref float[] dest)
        {
            if (src.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = src, pDst = dest)
            {
                IPPNative.ippsAbs_32f(pSrc, pDst, src.Length);
            }
        }

        public unsafe override void ACos(double value, ref double returnValue)
        {
            double[] input = new double[1] { value };
            double[] output = new double[1] { returnValue };
            fixed (double* pSrc = input, pDst = output)
            {
                IPPNative.ippsAcos_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void ACos(double[] value, ref double[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsAcos_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void ACos(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            fixed (float* pSrc = input, pDst = output)
            {
                IPPNative.ippsAcos_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void ACos(float[] value, ref float[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsAcos_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void Add(double[] src1, double[] src2, ref double[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (double* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsAdd_64f(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Add(float[] src1, float[] src2, ref float[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (float* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsAdd_32f(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Add(short[] src1, short[] src2, ref short[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (short* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsAdd_16s(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Add(ref double[] srcDest, double[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (double* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsAdd_64f_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Add(ref float[] srcDest, float[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (float* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsAdd_32f_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Add(ref short[] srcDest, short[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (short* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsAdd_16s_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Add(double[] src1, double scalar, ref double[] dest)
        {
            if (src1.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of dest");
            }
            fixed (double* pSrc1 = src1, pDst = dest)
            {
                IPPNative.ippsAddC_64f(pSrc1, scalar, pDst, dest.Length);
            }
        }

        public unsafe override void Add(float[] src1, float scalar, ref float[] dest)
        {
            if (src1.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of dest");
            }
            fixed (float* pSrc1 = src1, pDst = dest)
            {
                IPPNative.ippsAddC_32f(pSrc1, scalar, pDst, dest.Length);
            }
        }

        public unsafe override void Add(ref double[] srcDest, double scalar)
        {
            fixed (double* pSrcDst = srcDest)
            {
                IPPNative.ippsAddC_64f_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override void Add(ref float[] srcDest, float scalar)
        {
            fixed (float* pSrcDst = srcDest)
            {
                IPPNative.ippsAddC_32f_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override void Add(ref short[] srcDest, short scalar)
        {
            fixed (short* pSrcDst = srcDest)
            {
                IPPNative.ippsAddC_16s_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override void ASin(double value, ref double returnValue)
        {
            double[] input = new double[1] { value };
            double[] output = new double[1] { returnValue };
            fixed (double* pSrc = input, pDst = output)
            {
                IPPNative.ippsAsin_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void ASin(double[] value, ref double[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsAsin_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void ASin(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            fixed (float* pSrc = input, pDst = output)
            {
                IPPNative.ippsAsin_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void ASin(float[] value, ref float[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsAsin_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void ATan(double value, ref double returnValue)
        {
            double[] input = new double[1] { value };
            double[] output = new double[1] { returnValue };
            fixed (double* pSrc = input, pDst = output)
            {
                IPPNative.ippsAtan_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void ATan(double[] value, ref double[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsAtan_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void ATan(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            fixed (float* pSrc = input, pDst = output)
            {
                IPPNative.ippsAtan_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void ATan(float[] value, ref float[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsAtan_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void Concatenate<T>(T[] src1, T[] src2, ref T[] dest)
        {
            base.Concatenate(src1, src2, ref dest);
        }

        public unsafe override void Concatenate<T>(T[] src1, T[] src2, ref T[,] dest)
        {
            base.Concatenate(src1, src2, ref dest);
        }

        public unsafe override void Concatenate<T>(T[,] src1, T[] src2, ref T[,] dest)
        {
            base.Concatenate(src1, src2, ref dest);
        }

        public unsafe override void ConvertTo<Tin, Tout>(Tin[] input, ref Tout[] output)
        {
            base.ConvertTo(input, ref output);
        }

        public unsafe override void ConvertTo<Tin, Tout>(Tin[,] input, ref Tout[,] output)
        {
            base.ConvertTo(input, ref output);
        }

        public unsafe override void Copy<T>(T[] src, ref T[] dest)
        {
            base.Copy(src, ref dest);
        }

        public unsafe override void Copy<T>(T[,] src, ref T[,] dest)
        {
            base.Copy(src, ref dest);
        }

        public unsafe override void Cos(double value, ref double returnValue)
        {
            double[] input = new double[1] { value };
            double[] output = new double[1] { returnValue };
            fixed (double* pSrc = input, pDst = output)
            {
                IPPNative.ippsCos_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void Cos(double[] value, ref double[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsCos_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void Cos(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            fixed (float* pSrc = input, pDst = output)
            {
                IPPNative.ippsCos_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void Cos(float[] value, ref float[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsCos_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void Delete<T>(T[] src, int index, ref T[] dest)
        {
            base.Delete(src, index, ref dest);
        }

        public unsafe override void Derivative_2ndOrderCentral(double[] x, double dt, ref double[] y, double initialCondition, double finalCondition)
        {
            base.Derivative_2ndOrderCentral(x, dt, ref y, initialCondition, finalCondition);
        }

        public unsafe override void Derivative_4thOrderCentral(double[] x, double dt, ref double[] y, double[] initialCondition, double[] finalCondition)
        {
            base.Derivative_4thOrderCentral(x, dt, ref y, initialCondition, finalCondition);
        }

        public unsafe override void Derivative_Backward(double[] x, double dt, ref double[] y, double initialCondition)
        {
            base.Derivative_Backward(x, dt, ref y, initialCondition);
        }

        public unsafe override void Derivative_Forward(double[] x, double dt, ref double[] y, double finalCondition)
        {
            base.Derivative_Forward(x, dt, ref y, finalCondition);
        }

        public unsafe override void Divide(double[] src1, double[] src2, ref double[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (double* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsDiv_64f(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Divide(float[] src1, float[] src2, ref float[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (float* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsDiv_32f(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Divide(short[] src1, short[] src2, ref short[] dest)
        {
            base.Divide(src1, src2, ref dest);
        }

        public unsafe override void Divide(ref double[] srcDest, double[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (double* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsDiv_64f_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Divide(ref float[] srcDest, float[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (float* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsAdd_32f_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Divide(ref short[] srcDest, short[] src2)
        {
            base.Divide(ref srcDest, src2);
        }

        public unsafe override void Divide(double[] src1, double scalar, ref double[] dest)
        {
            if (src1.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of dest");
            }
            fixed (double* pSrc1 = src1, pDst = dest)
            {
                IPPNative.ippsDivC_64f(pSrc1, scalar, pDst, dest.Length);
            }
        }

        public unsafe override void Divide(float[] src1, float scalar, ref float[] dest)
        {
            base.Divide(src1, scalar, ref dest);
        }

        public unsafe override void Divide(ref double[] srcDest, double scalar)
        {
            fixed (double* pSrcDst = srcDest)
            {
                IPPNative.ippsDivC_64f_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override void Divide(ref float[] srcDest, float scalar)
        {
            base.Divide(ref srcDest, scalar);
        }

        public unsafe override void Divide(ref short[] srcDest, short scalar)
        {
            base.Divide(ref srcDest, scalar);
        }

        public unsafe override double Dot(double[] a, double[] b)
        {
            if (a.Length != b.Length)
            {
                throw new Exception("Length of a is not equal to the length of b");
            }
            double[] ret = new double[1];
            fixed (double* pSrc1 = a, pSrc2 = b, pDp = ret)
            {
                IPPNative.ippsDotProd_64f(pSrc1, pSrc2, a.Length, pDp);
            }
            return ret[0];
        }

        public unsafe override float Dot(float[] a, float[] b)
        {
            if (a.Length != b.Length)
            {
                throw new Exception("Length of a is not equal to the length of b");
            }
            float[] ret = new float[1];
            fixed (float* pSrc1 = a, pSrc2 = b, pDp = ret)
            {
                IPPNative.ippsDotProd_32f(pSrc1, pSrc2, a.Length, pDp);
            }
            return ret[0];
        }

        public unsafe override void Exp(double[] src, ref double[] dest)
        {
            if (src.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = src, pDst = dest)
            {
                IPPNative.ippsExp_64f_A53(pSrc, pDst, dest.Length);
            }
        }

        public unsafe override void Exp(float[] src, ref float[] dest)
        {
            if (src.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = src, pDst = dest)
            {
                IPPNative.ippsExp_32f_A24(pSrc, pDst, dest.Length);
            }
        }

        public unsafe override Func<double, double> ExponentialFittingFunc(double[] x, double[] y)
        {
            return base.ExponentialFittingFunc(x, y);
        }

        public unsafe override Tuple<double, double> ExponentialFitting(double[] x, double[] y)
        {
            return base.ExponentialFitting(x, y);
        }

        public unsafe override void FindMaxMin(double[] src, out double maxValue, out double minValue, out int maxIdx, out int minIdx)
        {
            double[] max = new double[1];
            double[] min = new double[1];
            int[] maxIndex = new int[1];
            int[] minIndex = new int[1];
            fixed (double* pSrc = src, pMin = min, pMax = max)
            {
                fixed (int* pMinIdx = minIndex, pMaxIdx = maxIndex)
                {
                    IPPNative.ippsMinMaxIndx_64f(pSrc, src.Length, pMin, pMinIdx, pMax, pMaxIdx);
                }
            }
            maxValue = max[0];
            minValue = min[0];
            maxIdx = maxIndex[0];
            minIdx = minIndex[0];
        }

        public unsafe override void FindMaxMin(float[] src, out float maxValue, out float minValue, out int maxIdx, out int minIdx)
        {
            float[] max = new float[1];
            float[] min = new float[1];
            int[] maxIndex = new int[1];
            int[] minIndex = new int[1];
            fixed (float* pSrc = src, pMin = min, pMax = max)
            {
                fixed (int* pMinIdx = minIndex, pMaxIdx = maxIndex)
                {
                    IPPNative.ippsMinMaxIndx_32f(pSrc, src.Length, pMin, pMinIdx, pMax, pMaxIdx);
                }
            }
            maxValue = max[0];
            minValue = min[0];
            maxIdx = maxIndex[0];
            minIdx = minIndex[0];
        }

        public unsafe override void FindMaxMin(short[] src, out short maxValue, out short minValue, out int maxIdx, out int minIdx)
        {
            short[] max = new short[1];
            short[] min = new short[1];
            int[] maxIndex = new int[1];
            int[] minIndex = new int[1];
            fixed (short* pSrc = src, pMin = min, pMax = max)
            {
                fixed (int* pMinIdx = minIndex, pMaxIdx = maxIndex)
                {
                    IPPNative.ippsMinMaxIndx_16s(pSrc, src.Length, pMin, pMinIdx, pMax, pMaxIdx);
                }
            }
            maxValue = max[0];
            minValue = min[0];
            maxIdx = maxIndex[0];
            minIdx = minIndex[0];
        }

        public unsafe override void GetSubset<T>(T[] src, int index, ref T[] dest)
        {
            base.GetSubset(src, index, ref dest);
        }

        public unsafe override void GetSubset<T>(T[,] src, int index, ref T[] dest, bool byRow = false)
        {
            base.GetSubset(src, index, ref dest, byRow);
        }

        public unsafe override int[] Histogram(double[] data, int binSize, double min = 0, double max = 0)
        {
            return base.Histogram(data, binSize, min, max);
        }

        public unsafe override void Initialize(ref double[] dest, double initValue)
        {
            fixed (double* pDst = dest)
            {
                IPPNative.ippsSet_64f(initValue, pDst, dest.Length);
            }
        }

        public unsafe override void Initialize(ref float[] dest, float initValue)
        {
            fixed (float* pDst = dest)
            {
                IPPNative.ippsSet_32f(initValue, pDst, dest.Length);
            }
        }

        public unsafe override void Insert<T>(T[] src, int startIdx, T element, ref T[] dest)
        {
            base.Insert(src, startIdx, element, ref dest);
        }

        public unsafe override void Integral_Bode(double[] x, double dt, ref double[] y, double[] initialCondition, double[] finalCondition)
        {
            base.Integral_Bode(x, dt, ref y, initialCondition, finalCondition);
        }

        public unsafe override void Integral_Simpsons(double[] x, double dt, ref double[] y, double initialCondition, double finalCondition)
        {
            base.Integral_Simpsons(x, dt, ref y, initialCondition, finalCondition);
        }

        public unsafe override void Integral_Simpsons38(double[] x, double dt, ref double[] y, double[] initialCondition, double finalCondition)
        {
            base.Integral_Simpsons38(x, dt, ref y, initialCondition, finalCondition);
        }

        public unsafe override void Integral_Trapezodial(double[] x, double dt, ref double[] y, double initialCondition)
        {
            base.Integral_Trapezodial(x, dt, ref y, initialCondition);
        }

        public unsafe override double Interpolate_CubicSpline(double[] x, double[] y, double xValue)
        {
            return base.Interpolate_CubicSpline(x, y, xValue);
        }

        public unsafe override double Interpolate_Linear(double[] x, double[] y, double xValue)
        {
            return base.Interpolate_Linear(x, y, xValue);
        }

        public unsafe override double Interpolate_LogLinear(double[] x, double[] y, double xValue)
        {
            return base.Interpolate_LogLinear(x, y, xValue);
        }

        public unsafe override double Interpolate_Polynomial(double[] x, double[] y, double xValue)
        {
            return base.Interpolate_Polynomial(x, y, xValue);
        }

        public unsafe override double Interpolate_Step(double[] x, double[] y, double xValue)
        {
            return base.Interpolate_Step(x, y, xValue);
        }

        public unsafe override void Inverse<T>(ref T[] src)
        {
            switch (typeof(T).ToString())
            {
                case "System.Double":
                    fixed (double* pSrcDst = src as double[])
                    {
                        IPPNative.ippsFlip_64f_I(pSrcDst, src.Length);
                    }
                    break;

                case "System.Single":
                    fixed (float* pSrcDst = src as float[])
                    {
                        IPPNative.ippsFlip_32f_I(pSrcDst, src.Length);
                    }
                    break;

                default:
                    base.Inverse(ref src);
                    break;
            }
        }

        public unsafe override double Kurtosis(double[] src)
        {
            return base.Kurtosis(src);
        }

        public unsafe override Func<double, double> LinearFittingFunc(double[] x, double[] y)
        {
            return base.LinearFittingFunc(x, y);
        }

        public unsafe override Tuple<double, double> LinearFitting(double[] x, double[] y)
        {
            return base.LinearFitting(x, y);
        }

        public unsafe override void Ln(double[] src, ref double[] dest)
        {
            if (src.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = src, pDst = dest)
            {
                IPPNative.ippsLn_64f_A53(pSrc, pDst, dest.Length);
            }
        }

        public unsafe override void Ln(float[] src, ref float[] dest)
        {
            if (src.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = src, pDst = dest)
            {
                IPPNative.ippsLn_32f_A24(pSrc, pDst, dest.Length);
            }
        }

        public unsafe override void Log(double[] src, ref double[] dest)
        {
            if (src.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = src, pDst = dest)
            {
                IPPNative.ippsLog10_64f_A53(pSrc, pDst, dest.Length);
            }
        }

        public unsafe override void Log(float[] src, ref float[] dest)
        {
            if (src.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = src, pDst = dest)
            {
                IPPNative.ippsLog10_32f_A24(pSrc, pDst, dest.Length);
            }
        }

        public unsafe override double Mean(double[] src)
        {
            double[] ret = new double[1];
            fixed (double* pSrc = src, pMean = ret)
            {
                IPPNative.ippsMean_64f(pSrc, src.Length, pMean);
            }
            return ret[0];
        }

        public unsafe override double Median(double[] src)
        {
            return base.Median(src);
        }

        public unsafe override void Multiply(double[] src1, double[] src2, ref double[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (double* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsMul_64f(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Multiply(float[] src1, float[] src2, ref float[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (float* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsMul_32f(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Multiply(short[] src1, short[] src2, ref short[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (short* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsMul_16s(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Multiply(ref double[] srcDest, double[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (double* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsMul_64f_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Multiply(ref float[] srcDest, float[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (float* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsMul_32f_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Multiply(ref short[] srcDest, short[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (short* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsMul_16s_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Multiply(double[] src1, double scalar, ref double[] dest)
        {
            if (src1.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of dest");
            }
            fixed (double* pSrc1 = src1, pDst = dest)
            {
                IPPNative.ippsMulC_64f(pSrc1, scalar, pDst, dest.Length);
            }
        }

        public unsafe override void Multiply(float[] src1, float scalar, ref float[] dest)
        {
            if (src1.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of dest");
            }
            fixed (float* pSrc1 = src1, pDst = dest)
            {
                IPPNative.ippsMulC_32f(pSrc1, scalar, pDst, dest.Length);
            }
        }

        public unsafe override void Multiply(ref double[] srcDest, double scalar)
        {
            fixed (double* pSrcDst = srcDest, pDst = srcDest)
            {
                IPPNative.ippsMulC_64f_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override void Multiply(ref float[] srcDest, float scalar)
        {
            fixed (float* pSrcDst = srcDest, pDst = srcDest)
            {
                IPPNative.ippsMulC_32f_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override void Multiply(ref short[] srcDest, short scalar)
        {
            fixed (short* pSrcDst = srcDest, pDst = srcDest)
            {
                IPPNative.ippsMulC_16s_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override double Norm(double[] a, double p)
        {
            return base.Norm(a, p);
        }

        public unsafe override double Norm(float[] a, double p)
        {
            return base.Norm(a, p);
        }

        public unsafe override double Percentile(double[] data, int place)
        {
            return base.Percentile(data, place);
        }

        public unsafe override Func<double, double> PolynomialFittingFunc(double[] x, double[] y, int order)
        {
            return base.PolynomialFittingFunc(x, y, order);
        }

        public unsafe override double[] PolynomialFitting(double[] x, double[] y, int order)
        {
            return base.PolynomialFitting(x, y, order);
        }

        public unsafe override void Pow(double[] src1, double scalar, ref double[] dest)
        {
            if (src1.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = src1, pDst = dest)
            {
                IPPNative.ippsPowx_64f_A53(pSrc, scalar, pDst, dest.Length);
            }
        }

        public unsafe override void Pow(float[] src1, float scalar, ref float[] dest)
        {
            if (src1.Length != dest.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = src1, pDst = dest)
            {
                IPPNative.ippsPowx_32f_A24(pSrc, scalar, pDst, dest.Length);
            }
        }

        public unsafe override double Product(double[] src1)
        {
            return base.Product(src1);
        }

        public unsafe override double Product(float[] src1)
        {
            return base.Product(src1);
        }

        public unsafe override void ReplaceSubset<T>(ref T[] src, int startIdx, T element)
        {
            base.ReplaceSubset(ref src, startIdx, element);
        }

        public unsafe override void ReplaceSubset<T>(ref T[] src, int startIdx, T[] elements)
        {
            base.ReplaceSubset(ref src, startIdx, elements);
        }

        public unsafe override double RMS(double[] src)
        {
            return base.RMS(src);
        }

        public unsafe override void Sin(double value, ref double returnValue)
        {
            double[] input = new double[1] { value };
            double[] output = new double[1] { returnValue };
            fixed (double* pSrc = input, pDst = output)
            {
                IPPNative.ippsSin_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void Sin(double[] value, ref double[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsSin_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void Sin(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            fixed (float* pSrc = input, pDst = output)
            {
                IPPNative.ippsSin_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void Sin(float[] value, ref float[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsSin_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override double Skewness(double[] src)
        {
            return base.Skewness(src);
        }

        public unsafe override void Sqrt(double[] src, ref double[] dest)
        {
            base.Sqrt(src, ref dest);
        }

        public unsafe override void Sqrt(float[] src, ref float[] dest)
        {
            base.Sqrt(src, ref dest);
        }

        public unsafe override double StandardDeviation(double[] src)
        {
            double[] ret = new double[1];
            fixed (double* pSrc = src, pStdDev = ret)
            {
                IPPNative.ippsStdDev_64f(pSrc, src.Length, pStdDev);
            }
            return ret[0];
        }

        public unsafe override void Substract(double[] src1, double[] src2, ref double[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (double* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsSub_64f(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Substract(float[] src1, float[] src2, ref float[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (float* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsSub_32f(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Substract(short[] src1, short[] src2, ref short[] dest)
        {
            if (src1.Length != dest.Length && src2.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of src2 and dest");
            }
            fixed (short* pSrc1 = src1, pSrc2 = src2, pDst = dest)
            {
                IPPNative.ippsSub_16s(pSrc1, pSrc2, pDst, dest.Length);
            }
        }

        public unsafe override void Substract(ref double[] srcDest, double[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (double* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsSub_64f_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Substract(ref float[] srcDest, float[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (float* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsSub_32f_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Substract(ref short[] srcDest, short[] src2)
        {
            if (srcDest.Length != src2.Length)
            {
                throw new Exception("Length of src2 is not equal to the length of srcDest");
            }
            fixed (short* pSrc = src2, pSrcDst = srcDest)
            {
                IPPNative.ippsSub_16s_I(pSrc, pSrcDst, src2.Length);
            }
        }

        public unsafe override void Substract(double[] src1, double scalar, ref double[] dest)
        {
            if (src1.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of dest");
            }
            fixed (double* pSrc1 = src1, pDst = dest)
            {
                IPPNative.ippsSubC_64f(pSrc1, scalar, pDst, dest.Length);
            }
        }

        public unsafe override void Substract(float[] src1, float scalar, ref float[] dest)
        {
            if (src1.Length != dest.Length)
            {
                throw new Exception("Length of src1 is not equal to the length of dest");
            }
            fixed (float* pSrc1 = src1, pDst = dest)
            {
                IPPNative.ippsSubC_32f(pSrc1, scalar, pDst, dest.Length);
            }
        }

        public unsafe override void Substract(ref double[] srcDest, double scalar)
        {
            fixed (double* pSrcDst = srcDest, pDst = srcDest)
            {
                IPPNative.ippsSubC_64f_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override void Substract(ref float[] srcDest, float scalar)
        {
            fixed (float* pSrcDst = srcDest, pDst = srcDest)
            {
                IPPNative.ippsSubC_32f_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override void Substract(ref short[] srcDest, short scalar)
        {
            fixed (short* pSrcDst = srcDest, pDst = srcDest)
            {
                IPPNative.ippsSubC_16s_I(scalar, pSrcDst, srcDest.Length);
            }
        }

        public unsafe override double Sum(double[] src1)
        {
            double[] ret = new double[1];
            fixed (double* pSrc = src1, pSum = ret)
            {
                IPPNative.ippsSum_64f(pSrc, src1.Length, pSum);
            }
            return ret[0];
        }

        public unsafe override double Sum(float[] src1)
        {
            return base.Sum(src1);
        }

        public unsafe override void Tan(double value, ref double returnValue)
        {
            double[] input = new double[1] { value };
            double[] output = new double[1] { returnValue };
            fixed (double* pSrc = input, pDst = output)
            {
                IPPNative.ippsTan_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void Tan(double[] value, ref double[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (double* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsTan_64f_A53(pSrc, pDst, 1);
            }
        }

        public unsafe override void Tan(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            fixed (float* pSrc = input, pDst = output)
            {
                IPPNative.ippsTan_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void Tan(float[] value, ref float[] returnValue)
        {
            if (value.Length != returnValue.Length)
            {
                throw new Exception("Length of src is not equal to the length of dest");
            }
            fixed (float* pSrc = value, pDst = returnValue)
            {
                IPPNative.ippsTan_32f_A24(pSrc, pDst, 1);
            }
        }

        public unsafe override void Transpose<T>(T[,] src, ref T[,] dest)
        {
            base.Transpose(src, ref dest);
        }

        public unsafe override double Variance(double[] src)
        {
            return base.Variance(src);
        }
    }
}
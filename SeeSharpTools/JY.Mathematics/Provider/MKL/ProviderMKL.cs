using System;
using System.Linq;
using Estimates = SeeSharpTools.JY.Mathematics.Provider.VSLNative.VSLSS_ComputeRoutine;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    public class ProvideMKL : ProviderBase
    {
        #region Staitstics related fields

        private int vslssHandle = -1;
        private int dim_n = 1;
        private VSLNative.VSLSS_Storage storage = VSLNative.VSLSS_Storage.VSL_SS_MATRIX_STORAGE_ROWS;
        private double[] dataBuffer = new double[1];
        private CurrentVSLStage currentMethod = CurrentVSLStage.Idle;

        #endregion Staitstics related fields

        ~ProvideMKL()
        {
            if (vslssHandle != -1)
            {
                VSLNative.vslSSDeleteTask(ref vslssHandle);
            }
        }

        public unsafe override void Absolute(double[] src, ref double[] dest)
        {
            VMLNative.vdAbs(src.Length, src, dest);
        }

        public unsafe override void Absolute(float[] src, ref float[] dest)
        {
            VMLNative.vsAbs(src.Length, src, dest);
        }

        public unsafe override void ACos(double value, ref double returnValue)
        {
            double[] a = new double[1] { value };
            double[] b = new double[1];
            VMLNative.vdAcos(a.Length, a, b);
            returnValue = b[0];
        }

        public unsafe override void ACos(double[] value, ref double[] returnValue)
        {
            VMLNative.vdAcos(value.Length, value, returnValue);
        }

        public unsafe override void ACos(float value, ref float returnValue)
        {
            float[] a = new float[1] { value };
            float[] b = new float[1];
            VMLNative.vsAcos(a.Length, a, b);
            returnValue = b[0];
        }

        public unsafe override void ACos(float[] value, ref float[] returnValue)
        {
            VMLNative.vsAcos(value.Length, value, returnValue);
        }

        public unsafe override void Add(double[] src1, double[] src2, ref double[] dest)
        {
            VMLNative.vdAdd(src1.Length, src1, src2, dest);
        }

        public unsafe override void Add(float[] src1, float[] src2, ref float[] dest)
        {
            VMLNative.vsAdd(src1.Length, src1, src2, dest);
        }

        public unsafe override void Add(short[] src1, short[] src2, ref short[] dest)
        {
            base.Add(src1, src2, ref dest);
        }

        public unsafe override void Add(ref double[] srcDest, double[] src2)
        {
            base.Add(ref srcDest, src2);
        }

        public unsafe override void Add(ref float[] srcDest, float[] src2)
        {
            base.Add(ref srcDest, src2);
        }

        public unsafe override void Add(ref short[] srcDest, short[] src2)
        {
            base.Add(ref srcDest, src2);
        }

        public unsafe override void Add(double[] src1, double scalar, ref double[] dest)
        {
            base.Add(src1, scalar, ref dest);
        }

        public unsafe override void Add(float[] src1, float scalar, ref float[] dest)
        {
            base.Add(src1, scalar, ref dest);
        }

        public unsafe override void Add(ref double[] srcDest, double scalar)
        {
            base.Add(ref srcDest, scalar);
        }

        public unsafe override void Add(ref float[] srcDest, float scalar)
        {
            base.Add(ref srcDest, scalar);
        }

        public unsafe override void Add(ref short[] srcDest, short scalar)
        {
            base.Add(ref srcDest, scalar);
        }

        public unsafe override void ASin(double value, ref double returnValue)
        {
            double[] input = new double[1] { value };
            double[] output = new double[1] { returnValue };
            VMLNative.vdAsin(input.Length, input, output);
            returnValue = output[0];
        }

        public unsafe override void ASin(double[] value, ref double[] returnValue)
        {
            VMLNative.vdAsin(value.Length, value, returnValue);
        }

        public unsafe override void ASin(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            VMLNative.vsAsin(input.Length, input, output);
            returnValue = output[0];
        }

        public unsafe override void ASin(float[] value, ref float[] returnValue)
        {
            VMLNative.vsAsin(value.Length, value, returnValue);
        }

        public unsafe override void ATan(double value, ref double returnValue)
        {
            double[] input = new double[1] { value };
            double[] output = new double[1] { returnValue };
            VMLNative.vdAtan(input.Length, input, output);
            returnValue = output[0];
        }

        public unsafe override void ATan(double[] value, ref double[] returnValue)
        {
            VMLNative.vdAtan(value.Length, value, returnValue);
        }

        public unsafe override void ATan(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            VMLNative.vsAtan(input.Length, input, output);
            returnValue = output[0];
        }

        public unsafe override void ATan(float[] value, ref float[] returnValue)
        {
            VMLNative.vsAtan(value.Length, value, returnValue);
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
            VMLNative.vdCos(input.Length, input, output);
        }

        public unsafe override void Cos(double[] value, ref double[] returnValue)
        {
            VMLNative.vdCos(value.Length, value, returnValue);
        }

        public unsafe override void Cos(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            VMLNative.vsCos(input.Length, input, output);
            returnValue = output[0];
        }

        public unsafe override void Cos(float[] value, ref float[] returnValue)
        {
            VMLNative.vsCos(value.Length, value, returnValue);
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
            base.Divide(src1, src2, ref dest);
        }

        public unsafe override void Divide(float[] src1, float[] src2, ref float[] dest)
        {
            base.Divide(src1, src2, ref dest);
        }

        public unsafe override void Divide(short[] src1, short[] src2, ref short[] dest)
        {
            base.Divide(src1, src2, ref dest);
        }

        public unsafe override void Divide(ref double[] srcDest, double[] src2)
        {
            base.Divide(ref srcDest, src2);
        }

        public unsafe override void Divide(ref float[] srcDest, float[] src2)
        {
            base.Divide(ref srcDest, src2);
        }

        public unsafe override void Divide(ref short[] srcDest, short[] src2)
        {
            base.Divide(ref srcDest, src2);
        }

        public unsafe override void Divide(double[] src1, double scalar, ref double[] dest)
        {
            base.Divide(src1, scalar, ref dest);
        }

        public unsafe override void Divide(float[] src1, float scalar, ref float[] dest)
        {
            base.Divide(src1, scalar, ref dest);
        }

        public unsafe override void Divide(ref double[] srcDest, double scalar)
        {
            base.Divide(ref srcDest, scalar);
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
            int[] indx = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                indx[i] = i;
            }
            return CBLASNative.cblas_ddoti(a.Length, a, indx, b);
        }

        public unsafe override float Dot(float[] a, float[] b)
        {
            int[] indx = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                indx[i] = i;
            }
            return CBLASNative.cblas_sdoti(a.Length, a, indx, b);
        }

        public unsafe override void Exp(double[] src, ref double[] dest)
        {
            VMLNative.vdExp(src.Length, src, dest);
        }

        public unsafe override void Exp(float[] src, ref float[] dest)
        {
            VMLNative.vsExp(src.Length, src, dest);
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
            VSLSS_Initial(src, CurrentVSLStage.Max);
            VSLSS_Initial(src, CurrentVSLStage.Min);
            VSLNative.vsldSSEditTask(vslssHandle, VSLNative.VSLSS_EditTaskParam.VSL_SS_ED_MAX, max);
            VSLNative.vsldSSEditTask(vslssHandle, VSLNative.VSLSS_EditTaskParam.VSL_SS_ED_MIN, min);
            VSLNative.vsldSSCompute(vslssHandle, VSLNative.VSLSS_ComputeRoutine.VSL_SS_MAX | VSLNative.VSLSS_ComputeRoutine.VSL_SS_MIN, VSLNative.VSLSS_Method.VSL_SS_METHOD_FAST);
            maxValue = max[0];
            minValue = min[0];
            maxIdx = src.ToList().FindIndex(x => x == max[0]);
            minIdx = src.ToList().FindIndex(x => x == min[0]);
        }

        public unsafe override void FindMaxMin(float[] src, out float maxValue, out float minValue, out int maxIdx, out int minIdx)
        {
            base.FindMaxMin(src, out maxValue, out minValue, out maxIdx, out minIdx);
        }

        public unsafe override void FindMaxMin(short[] src, out short maxValue, out short minValue, out int maxIdx, out int minIdx)
        {
            base.FindMaxMin(src, out maxValue, out minValue, out maxIdx, out minIdx);
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
            base.Initialize(ref dest, initValue);
        }

        public unsafe override void Initialize(ref float[] dest, float initValue)
        {
            base.Initialize(ref dest, initValue);
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
            base.Inverse(ref src);
        }

        public unsafe override double Kurtosis(double[] src)
        {
            double[] mean = new double[1];
            double[] r2m = new double[1];
            double[] c2m = new double[1];
            double[] r3m = new double[1];
            double[] c3m = new double[1];
            double[] r4m = new double[1];
            double[] c4m = new double[1];
            double[] kurtosis = new double[1];
            Estimates est = Estimates.VSL_SS_KURTOSIS | Estimates.VSL_SS_MEAN | Estimates.VSL_SS_2R_MOM | Estimates.VSL_SS_3R_MOM | Estimates.VSL_SS_4R_MOM;
            VSLSS_Initial(src, CurrentVSLStage.Kurtosis);
            VSLNative.vsldSSEditTask(vslssHandle, VSLNative.VSLSS_EditTaskParam.VSL_SS_ED_KURTOSIS, kurtosis);
            VSLNative.vsldSSEditMoments(vslssHandle, mean, r2m, r3m, r4m, c2m, c3m, c4m);
            VSLNative.vsldSSCompute(vslssHandle, est, VSLNative.VSLSS_Method.VSL_SS_METHOD_FAST);
            return kurtosis[0];
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
            VMLNative.vdLn(src.Length, src, dest);
        }

        public unsafe override void Ln(float[] src, ref float[] dest)
        {
            VMLNative.vsLn(src.Length, src, dest);
        }

        public unsafe override void Log(double[] src, ref double[] dest)
        {
            VMLNative.vdLog10(src.Length, src, dest);
        }

        public unsafe override void Log(float[] src, ref float[] dest)
        {
            VMLNative.vsLog10(src.Length, src, dest);
        }

        public unsafe override double Mean(double[] src)
        {
            double[] mean = new double[1];
            VSLSS_Initial(src, CurrentVSLStage.Mean);
            VSLNative.vsldSSEditTask(vslssHandle, VSLNative.VSLSS_EditTaskParam.VSL_SS_ED_MEAN, mean);
            VSLNative.vsldSSCompute(vslssHandle, VSLNative.VSLSS_ComputeRoutine.VSL_SS_MEAN, VSLNative.VSLSS_Method.VSL_SS_METHOD_FAST);
            return mean[0];
        }

        public unsafe override double Median(double[] src)
        {
            return base.Median(src);
        }

        public unsafe override void Multiply(double[] src1, double[] src2, ref double[] dest)
        {
            VMLNative.vdMul(src1.Length, src1, src2, dest);
        }

        public unsafe override void Multiply(float[] src1, float[] src2, ref float[] dest)
        {
            VMLNative.vsMul(src1.Length, src1, src2, dest);
        }

        public unsafe override void Multiply(short[] src1, short[] src2, ref short[] dest)
        {
            base.Multiply(src1, src2, ref dest);
        }

        public unsafe override void Multiply(ref double[] srcDest, double[] src2)
        {
            base.Multiply(ref srcDest, src2);
        }

        public unsafe override void Multiply(ref float[] srcDest, float[] src2)
        {
            base.Multiply(ref srcDest, src2);
        }

        public unsafe override void Multiply(ref short[] srcDest, short[] src2)
        {
            base.Multiply(ref srcDest, src2);
        }

        public unsafe override void Multiply(double[] src1, double scalar, ref double[] dest)
        {
            base.Multiply(src1, scalar, ref dest);
        }

        public unsafe override void Multiply(float[] src1, float scalar, ref float[] dest)
        {
            base.Multiply(src1, scalar, ref dest);
        }

        public unsafe override void Multiply(ref double[] srcDest, double scalar)
        {
            base.Multiply(ref srcDest, scalar);
        }

        public unsafe override void Multiply(ref float[] srcDest, float scalar)
        {
            base.Multiply(ref srcDest, scalar);
        }

        public unsafe override void Multiply(ref short[] srcDest, short scalar)
        {
            base.Multiply(ref srcDest, scalar);
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
            VMLNative.vdPowx(src1.Length, src1, scalar, dest);
        }

        public unsafe override void Pow(float[] src1, float scalar, ref float[] dest)
        {
            VMLNative.vsPowx(src1.Length, src1, scalar, dest);
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
            VMLNative.vdSin(input.Length, input, output);
            returnValue = output[0];
        }

        public unsafe override void Sin(double[] value, ref double[] returnValue)
        {
            VMLNative.vdSin(value.Length, value, returnValue);
        }

        public unsafe override void Sin(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            VMLNative.vsSin(input.Length, input, output);
            returnValue = output[0];
        }

        public unsafe override void Sin(float[] value, ref float[] returnValue)
        {
            VMLNative.vsSin(value.Length, value, returnValue);
        }

        public unsafe override double Skewness(double[] src)
        {
            double[] mean = new double[1];
            double[] r2m = new double[1];
            double[] c2m = new double[1];
            double[] r3m = new double[1];
            double[] c3m = new double[1];
            double[] r4m = new double[1];
            double[] c4m = new double[1];
            double[] skewness = new double[1];
            Estimates est = Estimates.VSL_SS_SKEWNESS | Estimates.VSL_SS_MEAN | Estimates.VSL_SS_2R_MOM | Estimates.VSL_SS_3R_MOM | Estimates.VSL_SS_4R_MOM;
            VSLSS_Initial(src, CurrentVSLStage.Skewness);
            VSLNative.vsldSSEditTask(vslssHandle, VSLNative.VSLSS_EditTaskParam.VSL_SS_ED_SKEWNESS, skewness);
            VSLNative.vsldSSEditMoments(vslssHandle, mean, r2m, r3m, r4m, c2m, c3m, c4m);
            VSLNative.vsldSSCompute(vslssHandle, est, VSLNative.VSLSS_Method.VSL_SS_METHOD_FAST);
            return skewness[0];
        }

        public unsafe override void Sqrt(double[] src, ref double[] dest)
        {
            VMLNative.vdSqrt(src.Length, src, dest);
        }

        public unsafe override void Sqrt(float[] src, ref float[] dest)
        {
            VMLNative.vsSqrt(src.Length, src, dest);
        }

        public unsafe override double StandardDeviation(double[] src)
        {
            return base.StandardDeviation(src);
        }

        public unsafe override void Substract(double[] src1, double[] src2, ref double[] dest)
        {
            VMLNative.vdSub(src1.Length, src1, src2, dest);
        }

        public unsafe override void Substract(float[] src1, float[] src2, ref float[] dest)
        {
            VMLNative.vsSub(src1.Length, src1, src2, dest);
        }

        public unsafe override void Substract(short[] src1, short[] src2, ref short[] dest)
        {
            base.Substract(src1, src2, ref dest);
        }

        public unsafe override void Substract(ref double[] srcDest, double[] src2)
        {
            base.Substract(ref srcDest, src2);
        }

        public unsafe override void Substract(ref float[] srcDest, float[] src2)
        {
            base.Substract(ref srcDest, src2);
        }

        public unsafe override void Substract(ref short[] srcDest, short[] src2)
        {
            base.Substract(ref srcDest, src2);
        }

        public unsafe override void Substract(double[] src1, double scalar, ref double[] dest)
        {
            base.Substract(src1, scalar, ref dest);
        }

        public unsafe override void Substract(float[] src1, float scalar, ref float[] dest)
        {
            base.Substract(src1, scalar, ref dest);
        }

        public unsafe override void Substract(ref double[] srcDest, double scalar)
        {
            base.Substract(ref srcDest, scalar);
        }

        public unsafe override void Substract(ref float[] srcDest, float scalar)
        {
            base.Substract(ref srcDest, scalar);
        }

        public unsafe override void Substract(ref short[] srcDest, short scalar)
        {
            base.Substract(ref srcDest, scalar);
        }

        public unsafe override double Sum(double[] src1)
        {
            double[] sum = new double[1];
            VSLSS_Initial(src1, CurrentVSLStage.Sum);
            VSLNative.vsldSSEditTask(vslssHandle, VSLNative.VSLSS_EditTaskParam.VSL_SS_ED_SUM, sum);
            VSLNative.vsldSSCompute(vslssHandle, VSLNative.VSLSS_ComputeRoutine.VSL_SS_SUM, VSLNative.VSLSS_Method.VSL_SS_METHOD_FAST);
            return sum[0];
        }

        public unsafe override double Sum(float[] src1)
        {
            return base.Sum(src1);
        }

        public unsafe override void Tan(double value, ref double returnValue)
        {
            double[] input = new double[1] { value };
            double[] output = new double[1] { returnValue };
            VMLNative.vdTan(input.Length, input, output);
            returnValue = output[0];
        }

        public unsafe override void Tan(double[] value, ref double[] returnValue)
        {
            VMLNative.vdTan(value.Length, value, returnValue);
        }

        public unsafe override void Tan(float value, ref float returnValue)
        {
            float[] input = new float[1] { value };
            float[] output = new float[1] { returnValue };
            VMLNative.vsTan(input.Length, input, output);
            returnValue = output[0];
        }

        public unsafe override void Tan(float[] value, ref float[] returnValue)
        {
            VMLNative.vsTan(value.Length, value, returnValue);
        }

        public unsafe override void Transpose<T>(T[,] src, ref T[,] dest)
        {
            base.Transpose(src, ref dest);
        }

        public unsafe override double Variance(double[] src)
        {
            return base.Variance(src);
        }

        private unsafe void VSLSS_Initial(double[] src, CurrentVSLStage stage)
        {
            if (dataBuffer.Length != src.Length || currentMethod != stage)
            {
                dataBuffer = new double[src.Length];
                dim_n = src.Length;
                Buffer.BlockCopy(src, 0, dataBuffer, 0, src.Length * sizeof(double));
                fixed (double* ptr = dataBuffer)
                {
                    fixed (int* p = new int[] { 1 }, n = new int[] { dim_n }, xStorage = new int[] { (int)storage })
                    {
                        VSLNative.vsldSSNewTask(ref vslssHandle, p, n, xStorage, ptr);
                    }
                }
                currentMethod = stage;
            }
            else
            {
                Buffer.BlockCopy(src, 0, dataBuffer, 0, src.Length * sizeof(double));
            }
        }

        internal enum CurrentVSLStage
        {
            Idle,
            Mean,
            Sum,
            Kurtosis,
            Variance,
            Skewness,
            Min,
            Max,
        }
    }
}
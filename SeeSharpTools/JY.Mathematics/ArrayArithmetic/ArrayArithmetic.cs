using SeeSharpTools.JY.ArrayUtility;
using System;
using System.Linq;

namespace SeeSharpTools.JY.Mathematics
{
    /// <summary>
    /// 数组计算类库（对数组元素的值进行计算）
    /// </summary>
    public class ArrayArithmetic
    {
        /// <summary>
        /// 对double数组内的元素取绝对值
        /// </summary>
        /// <param name="src">原数组</param>
        /// <param name="dest">返回数组</param>
        public static void Absolute(double[] src, ref double[] dest)
        {
            Engine.Base.Absolute(src, ref dest);
        }

        /// <summary>
        /// 对float数组内的元素取绝对值
        /// </summary>
        /// <param name="src">原数组</param>
        /// <param name="dest">返回数组</param>
        public static void Absolute(float[] src, ref float[] dest)
        {
            Engine.Base.Absolute(src, ref dest);
        }

        /// <summary>
        /// 取ACos值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ACos(double value, ref double returnValue)
        {
            Engine.Base.ACos(value, ref returnValue);
        }

        /// <summary>
        /// 取ACos值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ACos(double[] value, ref double[] returnValue)
        {
            Engine.Base.ACos(value, ref returnValue);
        }

        /// <summary>
        /// 取ACos值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ACos(float value, ref float returnValue)
        {
            Engine.Base.ACos(value, ref returnValue);
        }

        /// <summary>
        /// 取ACos值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ACos(float[] value, ref float[] returnValue)
        {
            Engine.Base.ACos(value, ref returnValue);
        }

        /// <summary>
        /// 将两个double数组相加
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Add(double[] src1, double[] src2, ref double[] dest)
        {
            Engine.Base.Add(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个float数组相加
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Add(float[] src1, float[] src2, ref float[] dest)
        {
            Engine.Base.Add(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个short数组相加
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Add(short[] src1, short[] src2, ref short[] dest)
        {
            Engine.Base.Add(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个double数组相加，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Add(double[] srcDest, double[] src2)
        {
            Engine.Base.Add(ref srcDest, src2);
        }

        /// <summary>
        /// 将两个float数组相加，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Add(float[] srcDest, float[] src2)
        {
            Engine.Base.Add(ref srcDest, src2);
        }

        /// <summary>
        /// 将两个short数组相加，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Add(short[] srcDest, short[] src2)
        {
            Engine.Base.Add(ref srcDest, src2);
        }

        /// <summary>
        /// 将double数组内元素和double元素相加
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">double元素</param>
        /// <param name="dest">返回数组</param>
        public static void Add(double[] src1, double scalar, ref double[] dest)
        {
            Engine.Base.Add(src1, scalar, ref dest);
        }

        /// <summary>
        /// 将float数组内元素和float元素相加
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">float元素</param>
        /// <param name="dest">返回数组</param>
        public static void Add(float[] src1, float scalar, ref float[] dest)
        {
            Engine.Base.Add(src1, scalar, ref dest);
        }

        /// <summary>
        /// 将double数组内元素和double元素相加，返回原数组
        /// </summary>
        /// <param name="srcDest">数组</param>
        /// <param name="scalar">double 元素</param>
        public static void Add(double[] srcDest, double scalar)
        {
            Engine.Base.Add(ref srcDest, scalar);
        }

        /// <summary>
        /// 将float数组内元素和float元素相加，返回原数组
        /// </summary>
        /// <param name="srcDest">数组</param>
        /// <param name="scalar">float 元素</param>
        public static void Add(float[] srcDest, float scalar)
        {
            Engine.Base.Add(ref srcDest, scalar);
        }

        /// <summary>
        /// 将short数组内元素和short元素相加，返回原数组
        /// </summary>
        /// <param name="srcDest">数组</param>
        /// <param name="scalar">short 元素</param>
        public static void Add(short[] srcDest, short scalar)
        {
            Engine.Base.Add(ref srcDest, scalar);
        }

        /// <summary>
        /// 取ASin值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ASin(double value, ref double returnValue)
        {
            Engine.Base.ASin(value, ref returnValue);
        }

        /// <summary>
        /// 取ASin值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ASin(double[] value, ref double[] returnValue)
        {
            Engine.Base.ASin(value, ref returnValue);
        }

        /// <summary>
        /// 取ASin值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ASin(float value, ref float returnValue)
        {
            Engine.Base.ASin(value, ref returnValue);
        }

        /// <summary>
        /// 取ASin值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ASin(float[] value, ref float[] returnValue)
        {
            Engine.Base.ASin(value, ref returnValue);
        }

        /// <summary>
        /// 取ATan值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ATan(double value, ref double returnValue)
        {
            Engine.Base.ATan(value, ref returnValue);
        }

        /// <summary>
        /// 取ATan值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ATan(double[] value, ref double[] returnValue)
        {
            Engine.Base.ATan(value, ref returnValue);
        }

        /// <summary>
        /// 取ATan值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ATan(float value, ref float returnValue)
        {
            Engine.Base.ATan(value, ref returnValue);
        }

        /// <summary>
        /// 取ATan值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void ATan(float[] value, ref float[] returnValue)
        {
            Engine.Base.ATan(value, ref returnValue);
        }

        /// <summary>
        /// 取Cos值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Cos(double value, ref double returnValue)
        {
            Engine.Base.Cos(value, ref returnValue);
        }

        /// <summary>
        /// 取Cos值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Cos(double[] value, ref double[] returnValue)
        {
            Engine.Base.Cos(value, ref returnValue);
        }

        /// <summary>
        /// 取Cos值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Cos(float value, ref float returnValue)
        {
            Engine.Base.Cos(value, ref returnValue);
        }

        /// <summary>
        /// 取Cos值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Cos(float[] value, ref float[] returnValue)
        {
            Engine.Base.Cos(value, ref returnValue);
        }

        /// <summary>
        /// 将两个double数组相除
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Divide(double[] src1, double[] src2, ref double[] dest)
        {
            Engine.Base.Divide(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个float数组相除
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Divide(float[] src1, float[] src2, ref float[] dest)
        {
            Engine.Base.Divide(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个short数组相除
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Divide(short[] src1, short[] src2, ref short[] dest)
        {
            Engine.Base.Divide(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个double数组相除，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Divide(double[] srcDest, double[] src2)
        {
            Engine.Base.Divide(ref srcDest, src2);
        }

        /// <summary>
        /// 将两个float数组相除，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Divide(float[] srcDest, float[] src2)
        {
            Engine.Base.Divide(ref srcDest, src2);
        }

        /// <summary>
        /// 将两个short数组相除，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Divide(short[] srcDest, short[] src2)
        {
            Engine.Base.Divide(ref srcDest, src2);
        }

        /// <summary>
        /// 将double数组内元素和double元素相除
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">double元素</param>
        /// <param name="dest">返回数组</param>
        public static void Divide(double[] src1, double scalar, ref double[] dest)
        {
            Engine.Base.Divide(src1, scalar, ref dest);
        }

        /// <summary>
        /// 将float数组内元素和float元素相除
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">float元素</param>
        /// <param name="dest">返回数组</param>
        public static void Divide(float[] src1, float scalar, ref float[] dest)
        {
            Engine.Base.Divide(src1, scalar, ref dest);
        }

        /// <summary>
        /// 将short数组内元素和short元素相除
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">short元素</param>
        /// <param name="dest">返回数组</param>
        public static void Divide(double[] srcDest, double scalar)
        {
            Engine.Base.Divide(ref srcDest, scalar);
        }

        /// <summary>
        /// 将double数组内元素和double元素相除，返回原数组
        /// </summary>
        /// <param name="srcDest">数组</param>
        /// <param name="scalar">double 元素</param>
        public static void Divide(float[] srcDest, float scalar)
        {
            Engine.Base.Divide(ref srcDest, scalar);
        }

        /// <summary>
        /// 将double数组内元素和double元素相除，返回原数组
        /// </summary>
        /// <param name="srcDest">数组</param>
        /// <param name="scalar">double 元素</param>
        public static void Divide(short[] srcDest, short scalar)
        {
            Engine.Base.Divide(ref srcDest, scalar);
        }

        /// <summary>
        /// 将double数组内元素取Exp值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回数组</param>
        public static void Exp(double[] src, ref double[] dest)
        {
            Engine.Base.Exp(src, ref dest);
        }

        /// <summary>
        /// 将float数组内元素取Exp值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回数组</param>
        public static void Exp(float[] src, ref float[] dest)
        {
            Engine.Base.Exp(src, ref dest);
        }

        /// <summary>
        /// 搜寻double数组的最大最小值和索引
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxIdx">最大值索引</param>
        /// <param name="minIdx">最小值索引</param>
        public static void FindMaxMin(double[] src, out double maxValue, out double minValue, out int maxIdx, out int minIdx)
        {
            double max = src.Max();
            double min = src.Min();
            maxValue = max;
            minValue = min;
            maxIdx = Array.FindIndex(src, x => x == max);
            minIdx = Array.FindIndex(src, x => x == min);
        }

        /// <summary>
        /// 搜寻float数组的最大最小值和索引
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxIdx">最大值索引</param>
        /// <param name="minIdx">最小值索引</param>
        public static void FindMaxMin(float[] src, out float maxValue, out float minValue, out int maxIdx, out int minIdx)
        {
            float max = src.Max();
            float min = src.Min();
            maxValue = max;
            minValue = min;
            maxIdx = Array.FindIndex(src, x => x == max);
            minIdx = Array.FindIndex(src, x => x == min);
        }

        /// <summary>
        /// 搜寻short数组的最大最小值和索引
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxIdx">最大值索引</param>
        /// <param name="minIdx">最小值索引</param>
        public static void FindMaxMin(short[] src, out short maxValue, out short minValue, out int maxIdx, out int minIdx)
        {
            short max = src.Max();
            short min = src.Min();
            maxValue = max;
            minValue = min;
            maxIdx = Array.FindIndex(src, x => x == max);
            minIdx = Array.FindIndex(src, x => x == min);
        }

        /// <summary>
        /// 初始化double数组并赋值
        /// </summary>
        /// <param name="dest">数组</param>
        /// <param name="initValue">初始值</param>
        public static void Initialize(ref double[] dest, double initValue)
        {
            ArrayCalculation.InitializeArray(ref dest, initValue);
        }

        /// <summary>
        /// 初始化float数组并赋值
        /// </summary>
        /// <param name="dest">数组</param>
        /// <param name="initValue">初始值</param>
        public static void Initialize(ref float[] dest, float initValue)
        {
            ArrayCalculation.InitializeArray(ref dest, initValue);
        }

        /// <summary>
        /// 将double数组内元素取Ln值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回数组</param>
        public static void Ln(double[] src, ref double[] dest)
        {
            Engine.Base.Ln(src, ref dest);
        }

        /// <summary>
        /// 将float数组内元素取Ln值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回数组</param>
        public static void Ln(float[] src, ref float[] dest)
        {
            Engine.Base.Ln(src, ref dest);
        }

        /// <summary>
        /// 将double数组内元素取Log值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回数组</param>
        public static void Log(double[] src, ref double[] dest)
        {
            Engine.Base.Log(src, ref dest);
        }

        /// <summary>
        /// 将float数组内元素取Log值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回数组</param>
        public static void Log(float[] src, ref float[] dest)
        {
            Engine.Base.Log(src, ref dest);
        }

        /// <summary>
        /// 将两个double数组相乘
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Multiply(double[] src1, double[] src2, ref double[] dest)
        {
            Engine.Base.Multiply(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个float数组相乘
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Multiply(float[] src1, float[] src2, ref float[] dest)
        {
            Engine.Base.Multiply(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个short数组相乘
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Multiply(short[] src1, short[] src2, ref short[] dest)
        {
            Engine.Base.Multiply(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个double数组相乘，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Multiply(double[] srcDest, double[] src2)
        {
            Engine.Base.Multiply(ref srcDest, src2);
        }

        /// <summary>
        /// 将两个float数组相乘，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Multiply(float[] srcDest, float[] src2)
        {
            Engine.Base.Multiply(ref srcDest, src2);
        }

        /// <summary>
        /// 将两个short数组相乘，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Multiply(short[] srcDest, short[] src2)
        {
            Engine.Base.Multiply(ref srcDest, src2);
        }

        /// <summary>
        /// 将double数组内元素和double元素相乘
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">double元素</param>
        /// <param name="dest">返回数组</param>
        public static void Multiply(double[] src1, double scalar, ref double[] dest)
        {
            Engine.Base.Multiply(src1, scalar, ref dest);
        }

        /// <summary>
        /// 将float数组内元素和float元素相乘
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">float元素</param>
        /// <param name="dest">返回数组</param>
        public static void Multiply(float[] src1, float scalar, ref float[] dest)
        {
            Engine.Base.Multiply(src1, scalar, ref dest);
        }

        /// <summary>
        /// 将short数组内元素和short元素相乘
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">short元素</param>
        /// <param name="dest">返回数组</param>
        public static void Multiply(double[] srcDest, double scalar)
        {
            Engine.Base.Multiply(ref srcDest, scalar);
        }

        /// <summary>
        /// 将double数组内元素和double元素相乘，返回原数组
        /// </summary>
        /// <param name="srcDest">数组</param>
        /// <param name="scalar">double 元素</param>
        public static void Multiply(float[] srcDest, float scalar)
        {
            Engine.Base.Multiply(ref srcDest, scalar);
        }

        /// <summary>
        /// 将float数组内元素和float元素相乘，返回原数组
        /// </summary>
        /// <param name="srcDest">数组</param>
        /// <param name="scalar">float 元素</param>
        public static void Multiply(short[] srcDest, short scalar)
        {
            Engine.Base.Multiply(ref srcDest, scalar);
        }

        /// <summary>
        /// 将double数组内元素取指数值(x的y次方)
        /// </summary>
        /// <param name="src1">来源数组(x）</param>
        /// <param name="scalar">次方数(y)</param>
        /// <param name="dest">返回数组</param>
        public static void Pow(double[] src1, double scalar, ref double[] dest)
        {
            Engine.Base.Pow(src1, scalar, ref dest);
        }

        /// <summary>
        /// 将float数组内元素取指数值(x的y次方)
        /// </summary>
        /// <param name="src1">来源数组(x）</param>
        /// <param name="scalar">次方数(y)</param>
        /// <param name="dest">返回数组</param>
        public static void Pow(float[] src1, float scalar, ref float[] dest)
        {
            Engine.Base.Pow(src1, scalar, ref dest);
        }

        /// <summary>
        /// 返回double数组内所有元素取乘积值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回乘积值</param>
        public static double Product(double[] src1)
        {
            return Engine.Base.Product(src1);
        }

        /// <summary>
        /// 返回float数组内所有元素取乘积值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回乘积值</param>
        public static double Product(float[] src1)
        {
            return Engine.Base.Product(src1);
        }

        /// <summary>
        /// 取Sin值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Sin(double value, ref double returnValue)
        {
            Engine.Base.Sin(value, ref returnValue);
        }

        /// <summary>
        /// 取Sin值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Sin(double[] value, ref double[] returnValue)
        {
            Engine.Base.Sin(value, ref returnValue);
        }

        /// <summary>
        /// 取Sin值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Sin(float value, ref float returnValue)
        {
            Engine.Base.Sin(value, ref returnValue);
        }

        /// <summary>
        /// 取Sin值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Sin(float[] value, ref float[] returnValue)
        {
            Engine.Base.Sin(value, ref returnValue);
        }

        /// <summary>
        /// 将double数组内元素取平方根值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回数组</param>
        public static void Sqrt(double[] src, ref double[] dest)
        {
            Engine.Base.Sqrt(src, ref dest);
        }

        /// <summary>
        /// 将float数组内元素取平方根值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回数组</param>
        public static void Sqrt(float[] src, ref float[] dest)
        {
            Engine.Base.Sqrt(src, ref dest);
        }

        /// <summary>
        /// 将两个double数组相减
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Substract(double[] src1, double[] src2, ref double[] dest)
        {
            Engine.Base.Substract(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个float数组相减
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Substract(float[] src1, float[] src2, ref float[] dest)
        {
            Engine.Base.Substract(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个short数组相减
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">返回数组</param>
        public static void Substract(short[] src1, short[] src2, ref short[] dest)
        {
            Engine.Base.Substract(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个double数组相减，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Substract(double[] srcDest, double[] src2)
        {
            Engine.Base.Substract(ref srcDest, src2);
        }

        /// <summary>
        /// 将两个float数组相减，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Substract(float[] srcDest, float[] src2)
        {
            Engine.Base.Substract(ref srcDest, src2);
        }

        /// <summary>
        /// 将两个short数组相减，写入到原数组
        /// </summary>
        /// <param name="srcDest">数组1, 返回的数组写回数组1</param>
        /// <param name="src2">数组2</param>
        public static void Substract(short[] srcDest, short[] src2)
        {
            Engine.Base.Substract(ref srcDest, src2);
        }

        /// <summary>
        /// 将double数组内元素和double元素相减
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">double元素</param>
        /// <param name="dest">返回数组</param>
        public static void Substract(double[] src1, double scalar, ref double[] dest)
        {
            Engine.Base.Substract(src1, scalar, ref dest);
        }

        /// <summary>
        /// 将float数组内元素和float元素相减
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">float元素</param>
        /// <param name="dest">返回数组</param>
        public static void Substract(float[] src1, float scalar, ref float[] dest)
        {
            Engine.Base.Substract(src1, scalar, ref dest);
        }

        /// <summary>
        /// 将short数组内元素和short元素相减
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">short元素</param>
        /// <param name="dest">返回数组</param>
        public static void Substract(double[] srcDest, double scalar)
        {
            Engine.Base.Substract(ref srcDest, scalar);
        }

        /// <summary>
        /// 将double数组内元素和double元素相减，返回原数组
        /// </summary>
        /// <param name="srcDest">数组</param>
        /// <param name="scalar">double 元素</param>
        public static void Substract(float[] srcDest, float scalar)
        {
            Engine.Base.Substract(ref srcDest, scalar);
        }

        /// <summary>
        /// 将float数组内元素和float元素相减，返回原数组
        /// </summary>
        /// <param name="srcDest">数组</param>
        /// <param name="scalar">float 元素</param>
        public static void Substract(short[] srcDest, short scalar)
        {
            Engine.Base.Substract(ref srcDest, scalar);
        }

        /// <summary>
        /// 返回double数组内所有元素的总和值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回总和值</param>
        public static double Sum(double[] src1)
        {
            return Engine.Base.Sum(src1);
        }

        /// <summary>
        /// 返回float数组内所有元素的总和值
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回总和值</param>
        public static double Sum(float[] src1)
        {
            return Engine.Base.Sum(src1);
        }

        /// <summary>
        /// 取Tan值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Tan(double value, ref double returnValue)
        {
            Engine.Base.Tan(value, ref returnValue);
        }

        /// <summary>
        /// 取Tan值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Tan(double[] value, ref double[] returnValue)
        {
            Engine.Base.Tan(value, ref returnValue);
        }

        /// <summary>
        /// 取Tan值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Tan(float value, ref float returnValue)
        {
            Engine.Base.Tan(value, ref returnValue);
        }

        /// <summary>
        /// 取Tan值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="returnValue"></param>
        public static void Tan(float[] value, ref float[] returnValue)
        {
            Engine.Base.Tan(value, ref returnValue);
        }
    }
}
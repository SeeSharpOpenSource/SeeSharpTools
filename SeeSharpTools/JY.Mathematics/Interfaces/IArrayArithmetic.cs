namespace SeeSharpTools.JY.Mathematics.Interfaces
{
    internal interface IArrayArithmetic
    {
        /// <summary>
        /// 对数组内double元素取绝对值
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void Absolute(double[] src, ref double[] dest);

        /// <summary>
        /// 对数组float元素取绝对值
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void Absolute(float[] src, ref float[] dest);

        void ACos(double value, ref double returnValue);

        void ACos(double[] value, ref double[] returnValue);

        void ACos(float value, ref float returnValue);

        void ACos(float[] value, ref float[] returnValue);

        /// <summary>
        /// double数组相加
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1+数组2相加结果</param>
        void Add(double[] src1, double[] src2, ref double[] dest);

        /// <summary>
        /// float数组相加
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1+数组2相加结果</param>
        void Add(float[] src1, float[] src2, ref float[] dest);

        /// <summary>
        /// short数组相加
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1+数组2相加结果</param>
        void Add(short[] src1, short[] src2, ref short[] dest);

        /// <summary>
        /// double数组相加,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Add(ref double[] srcDest, double[] src2);

        /// <summary>
        /// float数组相加,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Add(ref float[] srcDest, float[] src2);

        /// <summary>
        /// short数组相加,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Add(ref short[] srcDest, short[] src2);

        /// <summary>
        /// double数组和单一double数值相加
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">值</param>
        /// <param name="dest">目标数组</param>
        void Add(double[] src1, double scalar, ref double[] dest);

        /// <summary>
        /// float数组和单一float数值相加
        /// </summary>
        /// <param name="src1"></param>
        /// <param name="scalar"></param>
        /// <param name="dest"></param>
        void Add(float[] src1, float scalar, ref float[] dest);

        /// <summary>
        /// double数组和单一double数值相加，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相加的值</param>
        void Add(ref double[] srcDest, double scalar);

        /// <summary>
        /// float数组和单一float数值相加，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相加的值</param>
        void Add(ref float[] srcDest, float scalar);

        /// <summary>
        /// short数组和单一short数值相加，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相加的值</param>
        void Add(ref short[] srcDest, short scalar);

        void ASin(double value, ref double returnValue);

        void ASin(double[] value, ref double[] returnValue);

        void ASin(float value, ref float returnValue);

        void ASin(float[] value, ref float[] returnValue);

        void ATan(double value, ref double returnValue);

        void ATan(double[] value, ref double[] returnValue);

        void ATan(float value, ref float returnValue);

        void ATan(float[] value, ref float[] returnValue);

        void Cos(double value, ref double returnValue);

        void Cos(double[] value, ref double[] returnValue);

        void Cos(float value, ref float returnValue);

        void Cos(float[] value, ref float[] returnValue);

        /// <summary>
        /// double数组相除
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1/数组2相除结果</param>
        void Divide(double[] src1, double[] src2, ref double[] dest);

        /// <summary>
        /// float数组相除
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1/数组2相除结果</param>
        void Divide(float[] src1, float[] src2, ref float[] dest);

        /// <summary>
        /// short数组相除
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1/数组2相除结果</param>
        void Divide(short[] src1, short[] src2, ref short[] dest);

        /// <summary>
        /// double数组相除,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Divide(ref double[] srcDest, double[] src2);

        /// <summary>
        /// float数组相除,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Divide(ref float[] srcDest, float[] src2);

        /// <summary>
        /// short数组相除,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Divide(ref short[] srcDest, short[] src2);

        /// <summary>
        /// double数组和单一double数值相除
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">值</param>
        /// <param name="dest">目标数组</param>
        void Divide(double[] src1, double scalar, ref double[] dest);

        /// <summary>
        /// float数组和单一float数值相除
        /// </summary>
        /// <param name="src1"></param>
        /// <param name="scalar"></param>
        /// <param name="dest"></param>
        void Divide(float[] src1, float scalar, ref float[] dest);

        /// <summary>
        /// double数组和单一double数值相除，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相除的值</param>
        void Divide(ref double[] srcDest, double scalar);

        /// <summary>
        /// float数组和单一float数值相除，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相除的值</param>
        void Divide(ref float[] srcDest, float scalar);

        /// <summary>
        /// short数组和单一short数值相除，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相除的值</param>
        void Divide(ref short[] srcDest, short scalar);

        /// <summary>
        /// 对double数组内元素取Exponential值
        /// </summary>
        /// <param name="src">原始数组</param>
        /// <param name="dest">结果数组</param>
        void Exp(double[] src, ref double[] dest);

        /// <summary>
        /// 对float数组内元素取Exponential值
        /// </summary>
        /// <param name="src">原始数组</param>
        /// <param name="dest">结果数组</param>
        void Exp(float[] src, ref float[] dest);

        /// <summary>
        /// 搜寻double数组的最大最小值和索引
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxIdx">最大值索引</param>
        /// <param name="minIdx">最小值索引</param>
        void FindMaxMin(double[] src, out double maxValue, out double minValue, out int maxIdx, out int minIdx);

        /// <summary>
        /// 搜寻float数组的最大最小值和索引
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxIdx">最大值索引</param>
        /// <param name="minIdx">最小值索引</param>
        void FindMaxMin(float[] src, out float maxValue, out float minValue, out int maxIdx, out int minIdx);

        /// <summary>
        /// 搜寻short数组的最大最小值和索引
        /// </summary>
        /// <param name="src">来源数组</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxIdx">最大值索引</param>
        /// <param name="minIdx">最小值索引</param>
        void FindMaxMin(short[] src, out short maxValue, out short minValue, out int maxIdx, out int minIdx);

        /// <summary>
        /// 初始化double数组并赋值
        /// </summary>
        /// <param name="dest">数组</param>
        /// <param name="initValue">初始值</param>
        void Initialize(ref double[] dest, double initValue);

        /// <summary>
        /// 初始化float数组并赋值
        /// </summary>
        /// <param name="dest">数组</param>
        /// <param name="initValue">初始值</param>
        void Initialize(ref float[] dest, float initValue);

        /// <summary>
        /// 对double数组内元素取Ln值
        /// </summary>
        /// <param name="src">原始数组</param>
        /// <param name="dest">结果数组</param>
        void Ln(double[] src, ref double[] dest);

        /// <summary>
        /// 对float数组内元素取Ln值
        /// </summary>
        /// <param name="src">原始数组</param>
        /// <param name="dest">结果数组</param>
        void Ln(float[] src, ref float[] dest);

        /// <summary>
        /// 对double数组内元素取Log10值
        /// </summary>
        /// <param name="src">原始数组</param>
        /// <param name="dest">结果数组</param>
        void Log(double[] src, ref double[] dest);

        /// <summary>
        /// 对float数组内元素取Log10值
        /// </summary>
        /// <param name="src">原始数组</param>
        /// <param name="dest">结果数组</param>
        void Log(float[] src, ref float[] dest);

        /// <summary>
        /// double数组相乘
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1*数组2相乘结果</param>
        void Multiply(double[] src1, double[] src2, ref double[] dest);

        /// <summary>
        /// float数组相乘
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1*数组2相乘结果</param>
        void Multiply(float[] src1, float[] src2, ref float[] dest);

        /// <summary>
        /// short数组相乘
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1*数组2相乘结果</param>
        void Multiply(short[] src1, short[] src2, ref short[] dest);

        /// <summary>
        /// double数组相乘,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Multiply(ref double[] srcDest, double[] src2);

        /// <summary>
        /// float数组相乘,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Multiply(ref float[] srcDest, float[] src2);

        /// <summary>
        /// short数组相乘,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Multiply(ref short[] srcDest, short[] src2);

        /// <summary>
        /// double数组和单一double数值相乘
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">值</param>
        /// <param name="dest">目标数组</param>
        void Multiply(double[] src1, double scalar, ref double[] dest);

        /// <summary>
        /// float数组和单一float数值相乘
        /// </summary>
        /// <param name="src1"></param>
        /// <param name="scalar"></param>
        /// <param name="dest"></param>
        void Multiply(float[] src1, float scalar, ref float[] dest);

        /// <summary>
        /// double数组和单一double数值相乘，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相乘的值</param>
        void Multiply(ref double[] srcDest, double scalar);

        /// <summary>
        /// float数组和单一float数值相乘，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相乘的值</param>
        void Multiply(ref float[] srcDest, float scalar);

        /// <summary>
        /// short数组和单一short数值相乘，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相乘的值</param>
        void Multiply(ref short[] srcDest, short scalar);

        /// <summary>
        /// 对double数组内元素取取指数(x[i]的y次方)
        /// </summary>
        /// <param name="src1">x数组</param>
        /// <param name="scalar">y数值</param>
        /// <param name="dest">结果数组</param>
        void Pow(double[] src1, double scalar, ref double[] dest);

        /// <summary>
        /// 对float数组内元素取取指数(x[i]的y次方)
        /// </summary>
        /// <param name="src1">x数组</param>
        /// <param name="scalar">y数值</param>
        /// <param name="dest">结果数组</param>
        void Pow(float[] src1, float scalar, ref float[] dest);

        /// <summary>
        /// 将double数组内所有元素相乘
        /// </summary>
        /// <param name="src1">来源数组</param>
        /// <returns>返回的乘积</returns>
        double Product(double[] src1);

        /// <summary>
        /// 将float数组内所有元素相乘
        /// </summary>
        /// <param name="src1">来源数组</param>
        /// <returns>返回的乘积</returns>
        double Product(float[] src1);

        void Sin(double value, ref double returnValue);

        void Sin(double[] value, ref double[] returnValue);

        void Sin(float value, ref float returnValue);

        void Sin(float[] value, ref float[] returnValue);

        /// <summary>
        /// double数组相减
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1+数组2相减结果</param>
        void Substract(double[] src1, double[] src2, ref double[] dest);

        /// <summary>
        /// float数组相减
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1-数组2相减结果</param>
        void Substract(float[] src1, float[] src2, ref float[] dest);

        /// <summary>
        /// short数组相减
        /// </summary>
        /// <param name="src1">数组1</param>
        /// <param name="src2">数组2</param>
        /// <param name="dest">数组1+数组2相减结果</param>
        void Substract(short[] src1, short[] src2, ref short[] dest);

        /// <summary>
        /// double数组相减,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Substract(ref double[] srcDest, double[] src2);

        /// <summary>
        /// float数组相减,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Substract(ref float[] srcDest, float[] src2);

        /// <summary>
        /// short数组相减,将结果写回同个数组
        /// </summary>
        /// <param name="srcDest">数组1，结果会存放在数组1上</param>
        /// <param name="src2">数组2</param>
        void Substract(ref short[] srcDest, short[] src2);

        /// <summary>
        /// double数组和单一double数值相减
        /// </summary>
        /// <param name="src1">数组</param>
        /// <param name="scalar">值</param>
        /// <param name="dest">目标数组</param>
        void Substract(double[] src1, double scalar, ref double[] dest);

        /// <summary>
        /// float数组和单一float数值相减
        /// </summary>
        /// <param name="src1"></param>
        /// <param name="scalar"></param>
        /// <param name="dest"></param>
        void Substract(float[] src1, float scalar, ref float[] dest);

        /// <summary>
        /// double数组和单一double数值相减，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相减的值</param>
        void Substract(ref double[] srcDest, double scalar);

        /// <summary>
        /// float数组和单一float数值相减，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相减的值</param>
        void Substract(ref float[] srcDest, float scalar);

        /// <summary>
        /// short数组和单一short数值相减，结果写回同个数组
        /// </summary>
        /// <param name="srcDest">double数组</param>
        /// <param name="scalar">相减的值</param>
        void Substract(ref short[] srcDest, short scalar);

        /// <summary>
        /// 将double数组内所有元素相加
        /// </summary>
        /// <param name="src1">来源数组</param>
        /// <returns>返回的相加值</returns>
        double Sum(double[] src1);

        /// <summary>
        /// 将float数组内所有元素相加
        /// </summary>
        /// <param name="src1">来源数组</param>
        /// <returns>返回的相加值</returns>
        double Sum(float[] src1);

        /// <summary>
        /// 对double数组内元素取平方根值
        /// </summary>
        /// <param name="src">原始数组</param>
        /// <param name="dest">结果数组</param>
        void Sqrt(double[] src, ref double[] dest);

        /// <summary>
        /// 对float数组内元素取平方根值
        /// </summary>
        /// <param name="src">原始数组</param>
        /// <param name="dest">结果数组</param>
        void Sqrt(float[] src, ref float[] dest);

        void Tan(double value, ref double returnValue);

        void Tan(double[] value, ref double[] returnValue);

        void Tan(float value, ref float returnValue);

        void Tan(float[] value, ref float[] returnValue);
    }
}
using System;

namespace SeeSharpTools.JY.ArrayUtility
{
    /// <summary>
    /// <para>Add/Subtract/Mutiply all elements in a 1D/2D numeric array with a value.</para>
    /// <para>Add/Subtract/Multiply two 1D numeric array with same length, element by element in sequence.</para>
    /// <para>Calculate Sum/Average/RMS/Absolute value of a 1D numeric array.</para>
    /// <para>Compare if two 1D numeric array are the same.</para>
    /// <para>一维或二维数值数组与单一数值的加、减、乘运算。</para>
    ///<para> 两个等长一维数值数组之间逐个元素的加、减、乘运算。</para>
    /// <para>一维数值数组的和、平均值、均方根值、绝对值的计算。</para>
    /// <para>比较两个一维数组是否相同。</para>
    /// </summary>
    public static class ArrayCalculation
    {
       const string errArrayLengthConflict = "Array length inconsistent.";


        /// <summary>
        /// InitializeArray
        /// </summary>
        /// <param name="_src"></param>
        /// <param name="_initialValue"></param>
        public static void InitializeArray(ref double[] _src, double _initialValue)
        {
            for (int i = 0; i < _src.Length; i++) { _src[i] = _initialValue; }
        }
        /// <summary>
        /// InitializeArray
        /// </summary>
        /// <param name="_src"></param>
        /// <param name="_initialValue"></param>
        public static void InitializeArray(ref float[] _src, float _initialValue)
        {
            for (int i = 0; i < _src.Length; i++) { _src[i] = _initialValue; }
        }
        /// <summary>
        /// InitializeArray
        /// </summary>
        /// <param name="_src"></param>
        /// <param name="_initialValue"></param>
        public static void InitializeArray(ref int[] _src, int _initialValue)
        {
            for (int i = 0; i < _src.Length; i++) { _src[i] = _initialValue; }
        }
        /// <summary>
        /// InitializeArray
        /// </summary>
        /// <param name="_src"></param>
        /// <param name="_initialValue"></param>
        public static void InitializeArray(ref short[] _src, short _initialValue)
        {
            for (int i = 0; i < _src.Length; i++) { _src[i] = _initialValue; }
        }

        /// <summary>
        /// <para>Add two numeric arrays with same length, element by element in sequence. a[i] + b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相加。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to add. </para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of adding.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Add(double[] a, double[] b, ref double[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = a[i] + b[i]; }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }

        /// <summary>
        /// <para>Add two numeric arrays with same length, element by element in sequence. a[i] + b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相加。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to add. </para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of adding.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Add(float[] a, float[] b, ref float[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = a[i] + b[i]; }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }

        /// <summary>
        /// <para>Add two numeric arrays with same length, element by element in sequence. a[i] + b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相加。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to add. </para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of adding.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Add(int[] a, int[] b, ref int[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = a[i] + b[i]; }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }

        /// <summary>
        /// <para>Add two numeric arrays with same length, element by element in sequence. a[i] + b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相加。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to add. </para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of adding.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Add(short[] a, short[] b, ref short[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = (short)(a[i] + b[i]); }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }


        /// <summary>
        /// <para>Add all elements in an numeric array with a value. a[i] = a[i] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都加上一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of adding will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to add.</para>
        /// <para>Chinese Simplified: 输入的加数。</para>
        /// </param>
        public static void AddOffset(ref double[] a, double offset) 
        {
            for (int i = 0; i < a.Length; i++) { a[i] += offset; }
        }

        /// <summary>
        /// <para>Add all elements in an numeric array with a value. a[i] = a[i] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都加上一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of adding will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to add.</para>
        /// <para>Chinese Simplified: 输入的加数。</para>
        /// </param>
        public static void AddOffset(ref float[] a, float offset)
        {
            for (int i = 0; i < a.Length; i++) { a[i] += offset; }
        }

        /// <summary>
        /// <para>Add all elements in an numeric array with a value. a[i] = a[i] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都加上一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of adding will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to add.</para>
        /// <para>Chinese Simplified: 输入的加数。</para>
        /// </param>
        public static void AddOffset(ref int[] a, int offset)
        {
            for (int i = 0; i < a.Length; i++) { a[i] += offset; }
        }

        /// <summary>
        /// <para>Add all elements in an numeric array with a value. a[i] = a[i] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都加上一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of adding will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to add.</para>
        /// <para>Chinese Simplified: 输入的加数。</para>
        /// </param>
        public static void AddOffset(ref short[] a, short offset)
        {
            for (int i = 0; i < a.Length; i++) { a[i] += offset; }
        }


        /// <summary>
        /// <para>Add all elements in an numeric array with a value. a[i,j] = a[i,j] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都加上一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of adding will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to add.</para>
        /// <para>Chinese Simplified: 输入的加数。</para>
        /// </param>
        public static void AddOffset(ref double[,] a, double offset)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] += offset;
                }
        }

        /// <summary>
        /// <para>Add all elements in an numeric array with a value. a[i,j] = a[i,j] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都加上一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of adding will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to add.</para>
        /// <para>Chinese Simplified: 输入的加数。</para>
        /// </param>
        public static void AddOffset(ref float[,] a, float offset)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] += (float)offset;
                }
        }

        /// <summary>
        /// <para>Add all elements in an numeric array with a value. a[i,j] = a[i,j] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都加上一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of adding will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to add.</para>
        /// <para>Chinese Simplified: 输入的加数。</para>
        /// </param>
        public static void AddOffset(ref int[,] a, int offset)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] += (int)offset;
                }
        }

        /// <summary>
        /// <para>Add all elements in an numeric array with a value. a[i,j] = a[i,j] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都加上一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of adding will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to add.</para>
        /// <para>Chinese Simplified: 输入的加数。</para>
        /// </param>
        public static void AddOffset(ref short[,] a, short offset)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] += (short)offset;
                }
        }


        /// <summary>
        /// <para>Subtract two numeric arrays with same length, element by element in sequence. a[i] - b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相减。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to subtract.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of subtracting.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Subtract(double[] a, double[] b, ref double[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = a[i] - b[i]; }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }

        /// <summary>
        /// <para>Subtract two numeric arrays with same length, element by element in sequence. a[i] - b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相减。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to subtract.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of subtracting.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Subtract(float[] a, float[] b, ref float[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = a[i] - b[i]; }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }

        /// <summary>
        /// <para>Subtract two numeric arrays with same length, element by element in sequence. a[i] - b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相减。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to subtract.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of subtracting.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Subtract(int[] a, int[] b, ref int[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = a[i] - b[i]; }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }

        /// <summary>
        /// <para>Subtract two numeric arrays with same length, element by element in sequence. a[i] - b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相减。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to subtract.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of subtracting.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Subtract(short[] a, short[] b, ref short[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = (short)(a[i] - b[i]); }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }


        /// <summary>
        /// <para>Subtract all elements in an numeric array with a value. a[i] = a[i] - offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都减去一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of subtracting will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to subtract.</para>
        /// <para>Chinese Simplified: 输入的减数。</para>
        /// </param>
        public static void SubtractOffset(ref double[] a, double offset)
        {
            for (int i = 0; i < a.Length; i++) { a[i] -= offset; }
        }

        /// <summary>
        /// <para>Subtract all elements in an numeric array with a value. a[i] = a[i] - offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都减去一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of subtracting will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to subtract.</para>
        /// <para>Chinese Simplified: 输入的减数。</para>
        /// </param>
        public static void SubtractOffset(ref float[] a, float offset)
        {
            for (int i = 0; i < a.Length; i++) { a[i] -= offset; }
        }

        /// <summary>
        /// <para>Subtract all elements in an numeric array with a value. a[i] = a[i] - offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都减去一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of subtracting will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to subtract.</para>
        /// <para>Chinese Simplified: 输入的减数。</para>
        /// </param>
        public static void SubtractOffset(ref int[] a, int offset)
        {
            for (int i = 0; i < a.Length; i++) { a[i] -= offset; }
        }

        /// <summary>
        /// <para>Subtract all elements in an numeric array with a value. a[i] = a[i] - offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都减去一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of subtracting will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to subtract.</para>
        /// <para>Chinese Simplified: 输入的减数。</para>
        /// </param>
        public static void SubtractOffset(ref short[] a, short offset)
        {
            for (int i = 0; i < a.Length; i++) { a[i] -= offset; }
        }



        /// <summary>
        /// <para>Subtract all elements in an numeric array with a value. a[i,j] = a[i,j] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都减去一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of subtracting will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to subtract.</para>
        /// <para>Chinese Simplified: 输入的减数。</para>
        /// </param>
        public static void SubtractOffset(ref double[,] a, double offset)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] -= offset;
                }
        }

        /// <summary>
        /// <para>Subtract all elements in an numeric array with a value. a[i,j] = a[i,j] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都减去一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of subtracting will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to subtract.</para>
        /// <para>Chinese Simplified: 输入的减数。</para>
        /// </param>
        public static void SubtractOffset(ref float[,] a, float offset)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] -= offset;
                }
        }

        /// <summary>
        /// <para>Subtract all elements in an numeric array with a value. a[i,j] = a[i,j] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都减去一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of subtracting will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to subtract.</para>
        /// <para>Chinese Simplified: 输入的减数。</para>
        /// </param>
        public static void SubtractOffset(ref int[,] a, int offset)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] -= offset;
                }
        }

        /// <summary>
        /// <para>Subtract all elements in an numeric array with a value. a[i,j] = a[i,j] + offset.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都减去一个偏置量。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of subtracting will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="offset">
        /// <para>the value to subtract.</para>
        /// <para>Chinese Simplified: 输入的减数。</para>
        /// </param>
        public static void SubtractOffset(ref short[,] a, short offset)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] -= offset;
                }
        }


        /// <summary>
        /// <para>Multiply two numeric arrays with same length, element by element in sequence. a[i] * b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相乘。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to multiply.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of multiplying.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Multiply(double[] a, double[] b, ref double[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = a[i] * b[i]; }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }

        /// <summary>
        /// <para>Multiply two numeric arrays with same length, element by element in sequence. a[i] * b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相乘。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to multiply.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of multiplying.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Multiply(float[] a, float[] b, ref float[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = a[i] * b[i]; }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }

        /// <summary>
        /// <para>Multiply two numeric arrays with same length, element by element in sequence. a[i] * b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相乘。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to multiply.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of multiplying.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Multiply(int[] a, int[] b, ref int[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = a[i] * b[i]; }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }

        /// <summary>
        /// <para>Multiply two numeric arrays with same length, element by element in sequence. a[i] * b[i] = c[i]</para>
        /// <para>Chinese Simplified: 将两个一维数组按照顺序进行数值相乘。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence to multiply.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="c">
        /// <para>output sequence containing the result of multiplying.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Multiply(short[] a, short[] b, ref short[] c)
        {
            if ((a.Length == b.Length) && (a.Length == c.Length))
            {
                for (int i = 0; i < a.Length; i++) { c[i] = (short)(a[i] * b[i]); }
            }
            else
            {
                throw new ArgumentException(errArrayLengthConflict);
            }
        }


        /// <summary>
        /// <para>Multiply all elements in an numeric array with a value. a[i] = a[i] * scale.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都乘以一个系数。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of multiplying will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="scale">
        /// <para>the value to multiply.</para>
        /// <para>Chinese Simplified: 输入的乘数。</para>
        /// </param>
        public static void MultiplyScale(ref double[] a, double scale)
        {
            for (int i = 0; i < a.Length; i++) { a[i] *= scale; }
        }

        /// <summary>
        /// <para>Multiply all elements in an numeric array with a value. a[i] = a[i] * scale.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都乘以一个系数。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of multiplying will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="scale">
        /// <para>the value to multiply.</para>
        /// <para>Chinese Simplified: 输入的乘数。</para>
        /// </param>
        public static void MultiplyScale(ref float[] a, float scale)
        {
            for (int i = 0; i < a.Length; i++) { a[i] *= scale; }
        }

        /// <summary>
        /// <para>Multiply all elements in an numeric array with a value. a[i] = a[i] * scale.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都乘以一个系数。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of multiplying will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="scale">
        /// <para>the value to multiply.</para>
        /// <para>Chinese Simplified: 输入的乘数。</para>
        /// </param>
        public static void MultiplyScale(ref int[] a, int scale)
        {
            for (int i = 0; i < a.Length; i++) { a[i] *= scale; }
        }

        /// <summary>
        /// <para>Multiply all elements in an numeric array with a value. a[i] = a[i] * scale.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都乘以一个系数。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of multiplying will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="scale">
        /// <para>the value to multiply.</para>
        /// <para>Chinese Simplified: 输入的乘数。</para>
        /// </param>
        public static void MultiplyScale(ref short[] a, short scale)
        {
            for (int i = 0; i < a.Length; i++) { a[i] *= scale; }
        }


        /// <summary>
        /// <para>Multiply all elements in an numeric array with a value. a[i,j] = a[i,j] * scale.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都乘以一个系数。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of multiplying will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="scale">
        /// <para>the value to multiply.</para>
        /// <para>Chinese Simplified: 输入的乘数。</para>
        /// </param>
        public static void MultiplyScale(ref double[,] a, double scale)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] *= scale;
                }
        }

        /// <summary>
        /// <para>Multiply all elements in an numeric array with a value. a[i,j] = a[i,j] * scale.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都乘以一个系数。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of multiplying will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="scale">
        /// <para>the value to multiply.</para>
        /// <para>Chinese Simplified: 输入的乘数。</para>
        /// </param>
        public static void MultiplyScale(ref float[,] a, float scale)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] *= scale;
                }
        }

        /// <summary>
        /// <para>Multiply all elements in an numeric array with a value. a[i,j] = a[i,j] * scale.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都乘以一个系数。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of multiplying will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="scale">
        /// <para>the value to multiply.</para>
        /// <para>Chinese Simplified: 输入的乘数。</para>
        /// </param>
        public static void MultiplyScale(ref int[,] a, int scale)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] *= scale;
                }
        }

        /// <summary>
        /// <para>Multiply all elements in an numeric array with a value. a[i,j] = a[i,j] * scale.</para>
        /// <para>Chinese Simplified: 将数组中每个元素都乘以一个系数。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input and output sequence, the result of multiplying will be stored in original position.</para>
        /// <para>Chinese Simplified: 输入输出共用数组。</para>
        /// </param>
        /// <param name="scale">
        /// <para>the value to multiply.</para>
        /// <para>Chinese Simplified: 输入的乘数。</para>
        /// </param>
        public static void MultiplyScale(ref short[,] a, short scale)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] *= scale;
                }
        }

        /// <summary>
        /// <para>Set value of all elements in an numeric array to zero.</para>
        /// <para>Chinese Simplified: 将一维数组中所有元素的值置为零。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static void Zero(ref double[] a)
        {
            for (int i = 0; i < a.Length; i++) { a[i] = 0; }
        }

        /// <summary>
        /// <para>Set value of all elements in an numeric array to zero.</para>
        /// <para>Chinese Simplified: 将一维数组中所有元素的值置为零。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static void Zero(ref float[] a)
        {
            for (int i = 0; i < a.Length; i++) { a[i] = 0; }
        }

        /// <summary>
        /// <para>Set value of all elements in an numeric array to zero.</para>
        /// <para>Chinese Simplified: 将二维数组中所有元素的值置为零。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static void Zero(ref int[] a)
        {
            for (int i = 0; i < a.Length; i++) { a[i] = 0; }
        }

        /// <summary>
        /// <para>Set value of all elements in an numeric array to zero.</para>
        /// <para>Chinese Simplified: 将一维数组中所有元素的值置为零。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static void Zero(ref short[] a)
        {
            for (int i = 0; i < a.Length; i++) { a[i] = 0; }
        }


        /// <summary>
        /// <para>Set value of all elements in an numeric array to zero.</para>
        /// <para>Chinese Simplified: 将二维数组中所有元素的值置为零。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static void Zero(ref double[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] *= 0;
                }
        }

        /// <summary>
        /// <para>Set value of all elements in an numeric array to zero.</para>
        /// <para>Chinese Simplified: 将二维数组中所有元素的值置为零。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static void Zero(ref float[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] *= 0;
                }
        }

        /// <summary>
        /// <para>Set value of all elements in an numeric array to zero.</para>
        /// <para>Chinese Simplified: 将二维数组中所有元素的值置为零。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static void Zero(ref int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] *= 0;
                }
        }

        /// <summary>
        /// <para>Set value of all elements in an numeric array to zero.</para>
        /// <para>Chinese Simplified: 将二维数组中所有元素的值置为零。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static void Zero(ref short[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] *= 0;
                }

        }

        /// <summary>
        /// <para>Returns the sum of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的和。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double Sum(double[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++) { sum += a[i]; }
            return sum;
        }

        /// <summary>
        /// <para>Returns the sum of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的和。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double Sum(float[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++) { sum += a[i]; }
            return sum;
        }

        /// <summary>
        /// <para>Returns the sum of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的和。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double Sum(int[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++) { sum += a[i]; }
            return sum;
        }

        /// <summary>
        /// <para>Returns the sum of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的和。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double Sum(short[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++) { sum += a[i]; }
            return sum;
        }


        /// <summary>
        /// <para>Returns the averaging value of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的平均值。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double Average(double[] a)
        {
            double average = 0;
            int length = a.Length;

            for (int i = 0; i < length; i++) { average += a[i] / length; }
            return average;
        }

        /// <summary>
        /// <para>Returns the averaging value of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的平均值。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double Average(float[] a)
        {
            double average = 0;
            int length = a.Length;

            for (int i = 0; i < length; i++) { average += a[i] / length; }
            return average;
        }

        /// <summary>
        /// <para>Returns the averaging value of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的平均值。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double Average(int[] a)
        {
            double average = 0;
            int length = a.Length;

            for (int i = 0; i < length; i++) { average += a[i] / length; }
            return average;
        }

        /// <summary>
        /// <para>Returns the averaging value of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的平均值。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double Average(short[] a)
        {
            double average = 0;
            int length = a.Length;

            for (int i = 0; i < length; i++) { average += a[i] / length; }
            return average;
        }


        /// <summary>
        /// <para>Returns the Root Mean Square (RMS) of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的均方根值(RMS)。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double RMS(double[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++) { sum += a[i] * a[i]; }
            return Math.Sqrt(sum / a.Length);
        }

        /// <summary>
        /// <para>Returns the Root Mean Square (RMS) of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的均方根值(RMS)。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double RMS(float[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++) { sum += a[i] * a[i]; }
            return Math.Sqrt(sum / a.Length);
        }

        /// <summary>
        /// <para>Returns the Root Mean Square (RMS) of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的均方根值(RMS)。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double RMS(int[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++) { sum += a[i] * a[i]; }
            return Math.Sqrt(sum / a.Length);
        }

        /// <summary>
        /// <para>Returns the Root Mean Square (RMS) of the input numeric sequence.</para>
        /// <para>Chinese Simplified: 计算一组数的均方根值(RMS)。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static double RMS(short[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++) { sum += a[i] * a[i]; }
            return Math.Sqrt(sum / a.Length);
        }


        /// <summary>
        /// <para>Return true if two numeric arrays are the same. Otherwise, this fuction returns false.</para>
        /// <para>Chinese Simplified: 比较两个一维数组中的元素是否依次相等。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence. </para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static bool AreEqual(double[] a, double[] b)
        {
            if (a.Length != b.Length) return false;

            // compare elements
            for (int i = 0; i < a.Length; i++) { if (a[i] != b[i]) return false; }

            return true;
        }

        /// <summary>
        /// <para>Return true if two numeric arrays are the same. Otherwise, this fuction returns false.</para>
        /// <para>Chinese Simplified: 比较两个一维数组中的元素是否依次相等。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence. </para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static bool AreEqual(float[] a, float[] b)
        {
            if (a.Length != b.Length) return false;

            // compare elements
            for (int i = 0; i < a.Length; i++) { if (a[i] != b[i]) return false; }

            return true;
        }

        /// <summary>
        /// <para>Return true if two numeric arrays are the same. Otherwise, this fuction returns false.</para>
        /// <para>Chinese Simplified: 比较两个一维数组中的元素是否依次相等。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence. </para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static bool AreEqual(int[] a, int[] b)
        {
            if (a.Length != b.Length) return false;

            // compare elements
            for (int i = 0; i < a.Length; i++) { if (a[i] != b[i]) return false; }

            return true;
        }

        /// <summary>
        /// <para>Return true if two numeric arrays are the same. Otherwise, this fuction returns false.</para>
        /// <para>Chinese Simplified: 比较两个一维数组中的元素是否依次相等。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input sequence. </para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        public static bool AreEqual(short[] a, short[] b)
        {
            if (a.Length != b.Length) return false;

            // compare elements
            for (int i = 0; i < a.Length; i++) { if (a[i] != b[i]) return false; }

            return true;
        }


        /// <summary>
        /// <para>Calculate the absolute value of each element in input sequence, b[i] = |a[i]|.</para>
        /// <para>Chinese Simplified: 计算输入一维数值数组中各元素的绝对值。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>output sequence containing the absolute value of input sequence.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Abs(double[] a, ref double[] b)
        {
            if (a.Length != b.Length) { throw new ArgumentException(errArrayLengthConflict); }

            for (int i = 0; i < a.Length; i++) { b[i] = Math.Abs(a[i]);  }
        }

        /// <summary>
        /// <para>Calculate the absolute value of each element in input sequence, b[i] = |a[i]|.</para>
        /// <para>Chinese Simplified: 计算输入一维数值数组中各元素的绝对值。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>output sequence containing the absolute value of input sequence.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Abs(float[] a, ref float[] b)
        {
            if (a.Length != b.Length) { throw new ArgumentException(errArrayLengthConflict); }

            for (int i = 0; i < a.Length; i++) { b[i] = Math.Abs(a[i]); }
        }

        /// <summary>
        /// <para>Calculate the absolute value of each element in input sequence, b[i] = |a[i]|.</para>
        /// <para>Chinese Simplified: 计算输入一维数值数组中各元素的绝对值。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>output sequence containing the absolute value of input sequence.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Abs(int[] a, ref int[] b)
        {
            if (a.Length != b.Length) { throw new ArgumentException(errArrayLengthConflict); }

            for (int i = 0; i < a.Length; i++) { b[i] = Math.Abs(a[i]); }
        }

        /// <summary>
        /// <para>Calculate the absolute value of each element in input sequence, b[i] = |a[i]|.</para>
        /// <para>Chinese Simplified: 计算输入一维数值数组中各元素的绝对值。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>output sequence containing the absolute value of input sequence.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void Abs(short[] a, ref short[] b)
        {
            if (a.Length != b.Length) { throw new ArgumentException(errArrayLengthConflict); }

            for (int i = 0; i < a.Length; i++) { b[i] = Math.Abs(a[i]); }
        }
    }


}

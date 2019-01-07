using System;

namespace SeeSharpTools.JY.ArrayUtility
{
    /// <summary>
    /// <para>Get/Replace subset of a 1D/2D array.
    ///     For 1D array, subset is a portion of the 1D array, specified by "Start Index" and "Length", 
    ///     For 2D array, subset is one row or one column of 2D array, specified by "Index of Row/Column" and "Row/Column selector".
    /// </para>
    /// <para>
    /// Transpose 2D array. 
    ///     Rearrange 2D array a[,] with M rows N columns to b[,] with N rows M columns, where b[i,j] = a[j,i].
    /// </para>
    /// <para>Chinese Simplified: 一维及二维数组的数据提取和更新，二维数组转置。</para>
    /// </summary>
    public static class ArrayManipulation
    {
        /// <summary>
        /// Index type for accessing a 2D array, could be by row or by column.
        /// Chinese Simplified: 二维数组的索引方式，row为按行索引，column为按列索引。
        /// </summary>
        public enum IndexType {
            /// <summary>
            /// accesing one row of a 2D array.
            /// </summary>
            row,
            /// <summary>
            /// accessing one column of a 2D array.
            /// </summary>
            column
        };


        /// <summary>
        /// Insert 1 element in a 1_D Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_src"></param>
        /// <param name="_insertElement"></param>
        /// <param name="index"></param>
        /// <param name="_dst"></param>
        public static void Insert_1D_Array<T>(T[] _src, T _insertElement, int index, ref T[] _dst)
        {
            Array.Copy(_src, 0, _dst, 0, index);
            Array.Copy(_src, index, _dst, index + 1, _src.Length - index);
            _dst[index] = _insertElement;
        }
        /// <summary>
        /// Insert 1 Array in a 2D_Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_src"></param>
        /// <param name="_insertArray"></param>
        /// <param name="_columnIndex"></param>
        /// <param name="_dst"></param>
        public static void Insert_2D_Array<T>(T[,] _src, T[] _insertArray, int _columnIndex, ref T[,] _dst)
        {
            for (int j = 0; j < _columnIndex; j++)
            {
                for (int i = 0; i < _dst.GetLength(0); i++)
                {
                    _dst[i, j] = _src[i, j];
                }
            };

            for (int i = 0; i < _src.GetLength(0); i++)
            {
                _dst[i, _columnIndex] = _insertArray[i];
            }

            for (int j = _columnIndex + 1; j < _src.GetLength(1) + 1; j++)
            {
                for (int i = 0; i < _src.GetLength(0); i++)
                {
                    _dst[i, j] = _src[i, j - 1];
                }
            };
        }
        /// <summary>
        /// 将一维数组_src1和一维数组_src2连接为长度为_src1和_src2长度之和的一维数组
        /// </summary>
        /// <param name="_src1"></param>
        /// <param name="_src2"></param>
        /// <param name="_dst"></param>
        /// <returns></returns>
        public static void Connect_1D_Array<T>(T[] _src1, T[] _src2, ref T[] _dst)
        {
            if (_dst.Length != _src1.Length + _src2.Length) throw new InvalidOperationException("输入参数不匹配");
            Array.Copy(_src1, 0, _dst, 0, _src1.Length);
            Array.Copy(_src2, 0, _dst, _src1.Length, _src2.Length);
        }

        /// <summary>
        /// 将一维数组_src1和一维数组_src2连接为列数为2，行数为_src1和_src2最小长度的二维数组
        /// </summary>
        /// <param name="_src1"></param>
        /// <param name="_src2"></param>
        /// <param name="_dst"></param>
        /// <returns></returns>
        public static void Connected_2D_Array<T>(T[] _src1, T[] _src2, ref T[,] _dst)
        {
            if (_dst.GetLength(0) != _src1.Length || _dst.GetLength(0) != _src2.Length) throw new InvalidOperationException("输入参数不匹配");
            for (int i = 0; i < _dst.GetLength(1); i++)
            {
                if (i == 0) { for (int j = 0; j < _dst.GetLength(0); j++) { _dst[j, i] = _src1[j]; } }
                else { for (int j = 0; j < _dst.GetLength(0); j++) { _dst[j, i] = _src2[j]; } }
            }
        }
        
        /// <summary>
        /// 将二维数组_src1和一维数组_src2连接为列数为_src1列数+1，行数为_src1长度和_src2行数最小值的二维数组
        /// </summary>
        /// <param name="_src1"></param>
        /// <param name="_src2"></param>
        /// <param name="_dst"></param>
        /// <returns></returns>
        public static void ArrayConnect2<T>(T[,] _src1, T[] _src2, ref T[,] _dst)
        {
            for (int j = 0; j < _dst.GetLength(0); j++) { _dst[j, _src1.GetLength(1)] = _src2[j]; }
            for (int j = 0; j < _dst.GetLength(1) - 1; j++)
            {
                for (int i = 0; i < _dst.GetLength(0); i++)
                {
                    _dst[i, j] = _src1[i, j];
                }
            }
        }

        /// <summary>
        /// 将一维数组_src1和一维数组_src2连接为列数(行数)为2，行数(列数)为_src1和_src2最小长度的二维数组
        /// </summary>
        /// <param name="_src1"></param>
        /// <param name="_src2"></param>
        /// <param name="_dst"></param>
        /// <param name="majorOrder"></param>
        /// <returns></returns>
        public static void Concatenate<T>(T[] _src1, T[] _src2, ref T[,] _dst, MajorOrder majorOrder )
        {
            if (majorOrder == MajorOrder.Column)
            {
                if (_dst.GetLength(0) != _src1.Length || _dst.GetLength(0) != _src2.Length) throw new InvalidOperationException("输入参数不匹配");

                for (int i = 0; i < _dst.GetLength(1); i++)
                {
                    if (i == 0)
                    {
                        for (int j = 0; j < _dst.GetLength(0); j++)
                        {
                            _dst[j, i] = _src1[j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < _dst.GetLength(0); j++)
                        {
                            _dst[j, i] = _src2[j];
                        }
                    }
                }
            }
            else
            {
                if (_dst.GetLength(1) != _src1.Length || _dst.GetLength(1) != _src2.Length) throw new InvalidOperationException("输入参数不匹配");

                for (int i = 0; i < _dst.GetLength(0); i++)
                {
                    if (i == 0)
                    {
                        for (int j = 0; j < _dst.GetLength(1); j++)
                        {
                            _dst[i, j] = _src1[j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < _dst.GetLength(1); j++)
                        {
                            _dst[i, j] = _src2[j];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将二维数组_src1和一维数组_src2连接为列(行)数为_src1列(行)数+1，行(列)数为_src1长度和_src2行(列)数最小值的二维数组
        /// </summary>
        /// <param name="_src1"></param>
        /// <param name="_src2"></param>
        /// <param name="_dst"></param>
        /// <param name="majorOrder"></param>
        /// <returns></returns>
        public static void Concatenate<T>(T[,] _src1, T[] _src2, ref T[,] _dst, MajorOrder majorOrder)
        {
            if(majorOrder == MajorOrder.Column)
            {
                if (_dst.GetLength(0) != _src1.GetLength(0)|| _dst.GetLength(0) != _src2.Length||_dst.GetLength(1)!=_src1.GetLength(1)+1) throw new InvalidOperationException("输入参数不匹配");

                for (int j = 0; j < _dst.GetLength(0); j++){ _dst[j, _src1.GetLength(1)] = _src2[j]; }  //一维数组贴在最后一列
                for (int j = 0; j < _dst.GetLength(1) - 1; j++)
                {
                    for (int i = 0; i < _dst.GetLength(0); i++)
                    {
                        _dst[i, j] = _src1[i,j];
                    }
                }
            }
            else
            {
                if (_dst.GetLength(1) != _src1.GetLength(1) || _dst.GetLength(1) != _src2.Length||_dst.GetLength(0) != _src1.GetLength(0) + 1) throw new InvalidOperationException("输入参数不匹配");
                for (int j = 0; j < _dst.GetLength(1); j++) { _dst[_src1.GetLength(0),j] = _src2[j]; } //一维数组贴在最后一行
                for (int j = 0; j < _dst.GetLength(0) - 1; j++)
                {
                    for (int i = 0; i < _dst.GetLength(1); i++)
                    {
                        _dst[j, i] = _src1[j, i];
                    }
                }
            }
        }
        /// <summary>
        /// 将二维数组_src1和一维数组_src2连接为列(行)数为_src1列(行)数+1，行(列)数为_src1长度和_src2行(列)数最小值的二维数组
        /// </summary>
        /// <param name="_src1"></param>
        /// <param name="_src2"></param>
        /// <param name="_dst"></param>
        /// <param name="majorOrder"></param>
        /// <returns></returns>
        public static void Concatenate<T>(T[] _src1, T[,] _src2, ref T[,] _dst, MajorOrder majorOrder)
        {
            if (majorOrder == MajorOrder.Column)
            {
                if (_dst.GetLength(0) != _src1.Length || _dst.GetLength(0) != _src2.GetLength(0) || _dst.GetLength(1) != _src2.GetLength(1) + 1) throw new InvalidOperationException("输入参数不匹配");

                for (int j = 0; j < _dst.GetLength(0); j++) { _dst[j,0] = _src1[j]; }  //一维数组贴在第一列
                for (int j = 1; j < _dst.GetLength(1); j++)
                {
                    for (int i = 0; i < _dst.GetLength(0); i++)
                    {
                        _dst[i, j] = _src2[i, j-1];
                    }
                }
            }
            else
            {
                if (_dst.GetLength(1) != _src1.Length || _dst.GetLength(1) != _src2.GetLength(1) || _dst.GetLength(0) != _src2.GetLength(0) + 1) throw new InvalidOperationException("输入参数不匹配");
                for (int j = 0; j < _dst.GetLength(1); j++) {_dst[ 0,j] = _src1[j]; } //一维数组贴在第一行
                for (int j = 1; j < _dst.GetLength(0); j++)
                {
                    for (int i = 0; i < _dst.GetLength(1); i++)
                    {
                        _dst[j, i] = _src2[j-1, i];
                    }
                }
            }
        }
        /// <summary>
        /// 将二维数组_src1和二维数组_src2连接为列(行)数为_src1列(行)数+1，行(列)数为_src1长度和_src2行(列)数最小值的二维数组
        /// </summary>
        /// <param name="_src1"></param>
        /// <param name="_src2"></param>
        /// <param name="_dst"></param>
        /// <param name="majorOrder"></param>
        /// <returns></returns>
        public static void Concatenate<T>(T[,] _src1, T[,] _src2, ref T[,] _dst, MajorOrder majorOrder)
        {
            if (majorOrder == MajorOrder.Column)
            {
                if (_dst.GetLength(0) != _src1.GetLength(0) || _dst.GetLength(0) != _src2.GetLength(0)||_dst.GetLength(1)!=_src1.GetLength(1)+_src2.GetLength(1)) throw new InvalidOperationException("输入参数不匹配");
                for (int j = 0; j <_src1.GetLength(1); j++)
                {
                    for (int i = 0; i < _dst.GetLength(0); i++)
                    {
                        _dst[i, j] = _src1[i, j];
                    }
                }
                for (int j = _src1.GetLength(1); j < _dst.GetLength(1); j++)
                {
                    for (int i = 0; i < _dst.GetLength(0); i++)
                    {
                        _dst[i, j] = _src2[i, j-_src1.GetLength(1)];
                    }
                }
            }
            else
            {
                if (_dst.GetLength(1) != _src1.GetLength(1) || _dst.GetLength(1) != _src2.GetLength(1) || _dst.GetLength(0) != _src1.GetLength(0) + _src2.GetLength(0)) throw new InvalidOperationException("输入参数不匹配");
                for (int j = 0; j < _src1.GetLength(0); j++)
                {
                    for (int i = 0; i < _src1.GetLength(1); i++)
                    {
                        _dst[j, i] = _src1[j, i];
                    }
                }
                for (int j = _src1.GetLength(0); j < _dst.GetLength(0); j++)
                {
                    for (int i = 0; i < _dst.GetLength(1); i++)
                    {
                        _dst[j, i] = _src2[j-_src1.GetLength(0), i];
                    }
                }
            }
        }
        /// <summary>
        /// Convert 1_D array 2 StringArray
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_src"></param>
        /// <param name="_dst"></param>
        public static void Convert2StringArray<T>(T[] _src, ref string[] _dst)
        {
            for (int i = 0; i < _src.Length; i++)
            {
                _dst[i] = _src[i].ToString();
            }
        }
        /// <summary>
        /// Convert 2_D array 2 StringArray
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_src"></param>
        /// <param name="_dst"></param>
        public static void Convert2StringArray<T>(T[,] _src, ref string[,] _dst)
        {
            for (int i = 0; i < _src.GetLength(0); i++)
            {
                for (int j = 0; j < _src.GetLength(1); j++)
                {
                    _dst[i, j] = _src[i, j].ToString();
                }
            }
        }
        
        ///// <summary>
        ///// <para>Copying a portion of array a[] to array b[], starting at specified startIndex, number of elements equals to length of b[].</para>
        ///// <para>Chinese Simplified: 将一维数组a[]中从指定起始位置起的后续元素拷贝至一维数组b[]，起始索引值由startIndex给出，元素个数由数组b的长度给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence.</para>
        ///// <para>Chinese Simplified: 输入数组。</para>
        ///// </param>
        ///// <param name="startIndex">
        ///// <para>index of the first element to copy.</para>
        ///// <para>Chinese Simplified: 起始索引值。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>output sequence containing copied elements.</para>
        ///// <para>Chinese Simplified: 输出数组。</para>
        ///// </param>
        //public static void GetArraySubset(double[] a, int startIndex, ref double[] b)
        //{
        //    GetArraySubsetT(a, startIndex, ref b);
        //}

        ///// <summary>
        ///// <para>Copying a portion of array a[] to array b[], starting at specified startIndex, number of elements equals to length of b[].</para>
        ///// <para>Chinese Simplified: 将一维数组a[]中从指定起始位置起的后续元素拷贝至一维数组b[]，起始索引值由startIndex给出，元素个数由数组b的长度给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence.</para>
        ///// <para>Chinese Simplified: 输入数组。</para>
        ///// </param>
        ///// <param name="startIndex">
        ///// <para>index of the first element to copy.</para>
        ///// <para>Chinese Simplified: 起始索引值。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>output sequence containing copied elements.</para>
        ///// <para>Chinese Simplified: 输出数组。</para>
        ///// </param>
        //public static void GetArraySubset(float[] a, int startIndex, ref float[] b)
        //{
        //    GetArraySubsetT(a, startIndex, ref b);
        //}

        ///// <summary>
        ///// <para>Copying a portion of array a[] to array b[], starting at specified startIndex, number of elements equals to length of b[].</para>
        ///// <para>Chinese Simplified: 将一维数组a[]中从指定起始位置起的后续元素拷贝至一维数组b[]，起始索引值由startIndex给出，元素个数由数组b的长度给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence.</para>
        ///// <para>Chinese Simplified: 输入数组。</para>
        ///// </param>
        ///// <param name="startIndex">
        ///// <para>index of the first element to copy.</para>
        ///// <para>Chinese Simplified: 起始索引值。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>output sequence containing copied elements.</para>
        ///// <para>Chinese Simplified: 输出数组。</para>
        ///// </param>
        //public static void GetArraySubset(int[] a, int startIndex, ref int[] b)
        //{
        //    GetArraySubsetT(a, startIndex, ref b);
        //}

        ///// <summary>
        ///// <para>Copying a portion of array a[] to array b[], starting at specified startIndex, number of elements equals to length of b[].</para>
        ///// <para>Chinese Simplified: 将一维数组a[]中从指定起始位置起的后续元素拷贝至一维数组b[]，起始索引值由startIndex给出，元素个数由数组b的长度给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence.</para>
        ///// <para>Chinese Simplified: 输入数组。</para>
        ///// </param>
        ///// <param name="startIndex">
        ///// <para>index of the first element to copy.</para>
        ///// <para>Chinese Simplified: 起始索引值。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>output sequence containing copied elements.</para>
        ///// <para>Chinese Simplified: 输出数组。</para>
        ///// </param>
        //public static void GetArraySubset(short[] a, int startIndex, ref short[] b)
        //{
        //    GetArraySubsetT(a, startIndex, ref b);
        //}


        ///// <summary>
        ///// <para>Copying one row or column of array a[,] to b[], the row/column to copy is specified by index of row/column.</para>
        ///// <para>Chinese Simplified: 将二维数组a[,]的指定行或列拷贝至一维数组b[]，行(或列)索引值由index给出，按行索引或按列索引由indexType给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input 2D array.</para>
        ///// <para>Chinese Simplified: 原始二维数组。</para>
        ///// </param>
        ///// <param name="index">
        ///// <para>index of the row or column to copy.</para>
        ///// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        ///// <param name="b">
        ///// <para>output sequence containing copied row or column.</para>
        ///// <para>Chinese Simplified: 返回的指定行或列一维数组。</para>
        ///// </param>
        ///// </param>
        ///// <param name="indexType">
        ///// <para>specifies whether to copy row or column, the default is by row.</para>
        ///// <para>Chinese Simplified: 设定获取行还是列。</para>
        ///// </param>
        //public static void GetArraySubset(double[,] a, int index, ref double[] b, IndexType indexType = IndexType.row)
        //{
        //    GetArraySubsetT(a, index, ref b, indexType);
        //}

        ///// <summary>
        ///// <para>Copying one row or column of array a[,] to b[], the row/column to copy is specified by index of row/column.</para>
        ///// <para>Chinese Simplified: 将二维数组a[,]的指定行或列拷贝至一维数组b[]，行(或列)索引值由index给出，按行索引或按列索引由indexType给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input 2D array.</para>
        ///// <para>Chinese Simplified: 原始二维数组。</para>
        ///// </param>
        ///// <param name="index">
        ///// <para>index of the row or column to copy.</para>
        ///// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        ///// <param name="b">
        ///// <para>output sequence containing copied row or column.</para>
        ///// <para>Chinese Simplified: 返回的指定行或列一维数组。</para>
        ///// </param>
        ///// </param>
        ///// <param name="indexType">
        ///// <para>specifies whether to copy row or column, the default is by row.</para>
        ///// <para>Chinese Simplified: 设定获取行还是列。</para>
        ///// </param>
        //public static void GetArraySubset(float[,] a, int index, ref float[] b, IndexType indexType = IndexType.row)
        //{
        //    GetArraySubsetT(a, index, ref b, indexType);
        //}

        ///// <summary>
        ///// <para>Copying one row or column of array a[,] to b[], the row/column to copy is specified by index of row/column.</para>
        ///// <para>Chinese Simplified: 将二维数组a[,]的指定行或列拷贝至一维数组b[]，行(或列)索引值由index给出，按行索引或按列索引由indexType给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input 2D array.</para>
        ///// <para>Chinese Simplified: 原始二维数组。</para>
        ///// </param>
        ///// <param name="index">
        ///// <para>index of the row or column to copy.</para>
        ///// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        ///// <param name="b">
        ///// <para>output sequence containing copied row or column.</para>
        ///// <para>Chinese Simplified: 返回的指定行或列一维数组。</para>
        ///// </param>
        ///// </param>
        ///// <param name="indexType">
        ///// <para>specifies whether to copy row or column, the default is by row.</para>
        ///// <para>Chinese Simplified: 设定获取行还是列。</para>
        ///// </param>
        //public static void GetArraySubset(int[,] a, int index, ref int[] b, IndexType indexType = IndexType.row)
        //{
        //    GetArraySubsetT(a, index, ref b, indexType);
        //}

        ///// <summary>
        ///// <para>Copying one row or column of array a[,] to b[], the row/column to copy is specified by index of row/column.</para>
        ///// <para>Chinese Simplified: 将二维数组a[,]的指定行或列拷贝至一维数组b[]，行(或列)索引值由index给出，按行索引或按列索引由indexType给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input 2D array.</para>
        ///// <para>Chinese Simplified: 原始二维数组。</para>
        ///// </param>
        ///// <param name="index">
        ///// <para>index of the row or column to copy.</para>
        ///// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        ///// <param name="b">
        ///// <para>output sequence containing copied row or column.</para>
        ///// <para>Chinese Simplified: 返回的指定行或列一维数组。</para>
        ///// </param>
        ///// </param>
        ///// <param name="indexType">
        ///// <para>specifies whether to copy row or column, the default is by row.</para>
        ///// <para>Chinese Simplified: 设定获取行还是列。</para>
        ///// </param>
        //public static void GetArraySubset(short[,] a, int index, ref short[] b, IndexType indexType = IndexType.row)
        //{
        //    GetArraySubsetT(a, index, ref b, indexType);
        //}


        ///// <summary>
        ///// <para>Replace a portion of b[] with all elements of array a[], starting at specified startIndex, number of elements equals to length of a[].</para>
        ///// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至一维数组b[]中的指定位置，替换原有元素，起始索引值由startIndex给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence. </para>
        ///// <para>Chinese Simplified: 输入一维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>input and output sequence, a portion of it will be replaced with elements in a[].</para>
        ///// <para>Chinese Simplified: 更新后的一维数组。</para>
        ///// </param>
        ///// <param name="startIndex">
        ///// <para>index of the first element in b[] to be replaced.</para>
        ///// <para>Chinese Simplified: 起始索引值。</para>
        ///// </param>
        //public static void ReplaceArraySubset(double[] a, ref double[] b, int startIndex)
        //{
        //    ReplaceArraySubsetT(a, ref b, startIndex);
        //}

        ///// <summary>
        ///// <para>Replace a portion of b[] with all elements of array a[], starting at specified startIndex, number of elements equals to length of a[].</para>
        ///// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至一维数组b[]中的指定位置，替换原有元素，起始索引值由startIndex给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence. </para>
        ///// <para>Chinese Simplified: 输入一维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>input and output sequence, a portion of it will be replaced with elements in a[].</para>
        ///// <para>Chinese Simplified: 更新后的一维数组。</para>
        ///// </param>
        ///// <param name="startIndex">
        ///// <para>index of the first element in b[] to be replaced.</para>
        ///// <para>Chinese Simplified: 起始索引值。</para>
        ///// </param>
        //public static void ReplaceArraySubset(float[] a, ref float[] b, int startIndex)
        //{
        //    ReplaceArraySubsetT(a, ref b, startIndex);
        //}

        ///// <summary>
        ///// <para>Replace a portion of b[] with all elements of array a[], starting at specified startIndex, number of elements equals to length of a[].</para>
        ///// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至一维数组b[]中的指定位置，替换原有元素，起始索引值由startIndex给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence. </para>
        ///// <para>Chinese Simplified: 输入一维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>input and output sequence, a portion of it will be replaced with elements in a[].</para>
        ///// <para>Chinese Simplified: 更新后的一维数组。</para>
        ///// </param>
        ///// <param name="startIndex">
        ///// <para>index of the first element in b[] to be replaced.</para>
        ///// <para>Chinese Simplified: 起始索引值。</para>
        ///// </param>
        //public static void ReplaceArraySubset(int[] a, ref int[] b, int startIndex)
        //{
        //    ReplaceArraySubsetT(a, ref b, startIndex);
        //}

        ///// <summary>
        ///// <para>Replace a portion of b[] with all elements of array a[], starting at specified startIndex, number of elements equals to length of a[].</para>
        ///// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至一维数组b[]中的指定位置，替换原有元素，起始索引值由startIndex给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence. </para>
        ///// <para>Chinese Simplified: 输入一维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>input and output sequence, a portion of it will be replaced with elements in a[].</para>
        ///// <para>Chinese Simplified: 更新后的一维数组。</para>
        ///// </param>
        ///// <param name="startIndex">
        ///// <para>index of the first element in b[] to be replaced.</para>
        ///// <para>Chinese Simplified: 起始索引值。</para>
        ///// </param>
        //public static void ReplaceArraySubset(short[] a, ref short[] b, int startIndex)
        //{
        //    ReplaceArraySubsetT(a, ref b, startIndex);
        //}


        ///// <summary>
        ///// <para>Replace one row or column of b[,] with all elements of array a[], the row/column to be replaced is specified by index of row/column.</para>
        ///// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至二维数组b[,]中的指定行或列，替换该行或列的元素，行数(列数)由index给出，按行索引或按列索引由indexType给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence.</para>
        ///// <para>Chinese Simplified: 输入一维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>input and output 2D array, one row or column of it will be replaced with elements in a[].</para>
        ///// <para>Chinese Simplified: 更新后的二维数组。</para>
        ///// </param>
        ///// <param name="index">
        ///// <para>index of the row or column to copy.</para>
        ///// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        ///// </param>
        ///// <param name="indexType">
        ///// <para>specifies whether to replace row or column, the default is by row.</para>
        ///// <para>Chinese Simplified: 设定替换行还是列。</para>
        ///// </param>
        //public static void ReplaceArraySubset(double[] a, ref double[,] b, int index, IndexType indexType = IndexType.row)
        //{
        //    ReplaceArraySubsetT(a, ref b, index, indexType);
        //}

        ///// <summary>
        ///// <para>Replace one row or column of b[,] with all elements of array a[], the row/column to be replaced is specified by index of row/column.</para>
        ///// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至二维数组b[,]中的指定行或列，替换该行或列的元素，行数(列数)由index给出，按行索引或按列索引由indexType给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence.</para>
        ///// <para>Chinese Simplified: 输入一维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>input and output 2D array, one row or column of it will be replaced with elements in a[].</para>
        ///// <para>Chinese Simplified: 更新后的二维数组。</para>
        ///// </param>
        ///// <param name="index">
        ///// <para>index of the row or column to copy.</para>
        ///// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        ///// </param>
        ///// <param name="indexType">
        ///// <para>specifies whether to replace row or column, the default is by row.</para>
        ///// <para>Chinese Simplified: 设定替换行还是列。</para>
        ///// </param>
        //public static void ReplaceArraySubset(float[] a, ref float[,] b, int index, IndexType indexType = IndexType.row)
        //{
        //    ReplaceArraySubsetT(a, ref b, index, indexType);
        //}

        ///// <summary>
        ///// <para>Replace one row or column of b[,] with all elements of array a[], the row/column to be replaced is specified by index of row/column.</para>
        ///// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至二维数组b[,]中的指定行或列，替换该行或列的元素，行数(列数)由index给出，按行索引或按列索引由indexType给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence.</para>
        ///// <para>Chinese Simplified: 输入一维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>input and output 2D array, one row or column of it will be replaced with elements in a[].</para>
        ///// <para>Chinese Simplified: 更新后的二维数组。</para>
        ///// </param>
        ///// <param name="index">
        ///// <para>index of the row or column to copy.</para>
        ///// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        ///// </param>
        ///// <param name="indexType">
        ///// <para>specifies whether to replace row or column, the default is by row.</para>
        ///// <para>Chinese Simplified: 设定替换行还是列。</para>
        ///// </param>
        //public static void ReplaceArraySubset(int[] a, ref int[,] b, int index, IndexType indexType = IndexType.row)
        //{
        //    ReplaceArraySubsetT(a, ref b, index, indexType);
        //}

        ///// <summary>
        ///// <para>Replace one row or column of b[,] with all elements of array a[], the row/column to be replaced is specified by index of row/column.</para>
        ///// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至二维数组b[,]中的指定行或列，替换该行或列的元素，行数(列数)由index给出，按行索引或按列索引由indexType给出。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input sequence.</para>
        ///// <para>Chinese Simplified: 输入一维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>input and output 2D array, one row or column of it will be replaced with elements in a[].</para>
        ///// <para>Chinese Simplified: 更新后的二维数组。</para>
        ///// </param>
        ///// <param name="index">
        ///// <para>index of the row or column to copy.</para>
        ///// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        ///// </param>
        ///// <param name="indexType">
        ///// <para>specifies whether to replace row or column, the default is by row.</para>
        ///// <para>Chinese Simplified: 设定替换行还是列。</para>
        ///// </param>
        //public static void ReplaceArraySubset(short[] a, ref short[,] b, int index, IndexType indexType = IndexType.row)
        //{
        //    ReplaceArraySubsetT(a, ref b, index, indexType);
        //}

        ///// <summary>
        ///// <para>Rearrange 2D array a[,] with M rows N columns to b[,] with N rows M columns, where b[i,j] = a[j,i].</para>
        ///// <para>Chinese Simplified: 二维数组行列转置，即将N行M列的数组转换为M行N列。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input 2D array.</para>
        ///// <para>Chinese Simplified: 原始二维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>output transposed 2D array.</para>
        ///// <para>Chinese Simplified: 转置后的二维数组。</para>
        ///// </param>
        //public static void Transpose(double[,] a, ref double[,] b)
        //{
        //    TransposeT(a, ref b);
        //}

        ///// <summary>
        ///// <para>Rearrange 2D array a[,] with M rows N columns to b[,] with N rows M columns, where b[i,j] = a[j,i].</para>
        ///// <para>Chinese Simplified: 二维数组行列转置，即将N行M列的数组转换为M行N列。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input 2D array.</para>
        ///// <para>Chinese Simplified: 原始二维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>output transposed 2D array.</para>
        ///// <para>Chinese Simplified: 转置后的二维数组。</para>
        ///// </param>
        //public static void Transpose(float[,] a, ref float[,] b)
        //{
        //    TransposeT(a, ref b);
        //}

        ///// <summary>
        ///// <para>Rearrange 2D array a[,] with M rows N columns to b[,] with N rows M columns, where b[i,j] = a[j,i].</para>
        ///// <para>Chinese Simplified: 二维数组行列转置，即将N行M列的数组转换为M行N列。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input 2D array.</para>
        ///// <para>Chinese Simplified: 原始二维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>output transposed 2D array.</para>
        ///// <para>Chinese Simplified: 转置后的二维数组。</para>
        ///// </param>
        //public static void Transpose(int[,] a, ref int[,] b)
        //{
        //    TransposeT(a, ref b);
        //}

        ///// <summary>
        ///// <para>Rearrange 2D array a[,] with M rows N columns to b[,] with N rows M columns, where b[i,j] = a[j,i].</para>
        ///// <para>Chinese Simplified: 二维数组行列转置，即将N行M列的数组转换为M行N列。</para>
        ///// </summary>
        ///// <param name="a">
        ///// <para>input 2D array.</para>
        ///// <para>Chinese Simplified: 原始二维数组。</para>
        ///// </param>
        ///// <param name="b">
        ///// <para>output transposed 2D array.</para>
        ///// <para>Chinese Simplified: 转置后的二维数组。</para>
        ///// </param>
        //public static void Transpose(short[,] a, ref short[,] b)
        //{
        //    TransposeT(a, ref b);
        //}

        #region public static methods using Generics Type as parameter
        /// <summary>
        /// <para>Copying a portion of array a[] to array b[], starting at specified startIndex, number of elements equals to length of b[].</para>
        /// <para>Chinese Simplified: 将一维数组a[]中从指定起始位置起的后续元素拷贝至一维数组b[]，起始索引值由startIndex给出，元素个数由数组b的长度给出。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入数组。</para>
        /// </param>
        /// <param name="startIndex">
        /// <para>index of the first element to copy.</para>
        /// <para>Chinese Simplified: 起始索引值。</para>
        /// </param>
        /// <param name="b">
        /// <para>output sequence containing copied elements.</para>
        /// <para>Chinese Simplified: 输出数组。</para>
        /// </param>
        public static void GetArraySubset<T>(T[] a, int startIndex, ref T[] b)
        {
            int actualLength;

            actualLength = Math.Min(b.Length, a.Length - startIndex);
            for (int i=0; i<actualLength; i++) { b[i] = a[i + startIndex]; }
        }

        /// <summary>
        /// <para>Copying one row or column of array a[,] to b[], the row/column to copy is specified by index of row/column.</para>
        /// <para>Chinese Simplified: 将二维数组a[,]的指定行或列拷贝至一维数组b[]，行(或列)索引值由index给出，按行索引或按列索引由indexType给出。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input 2D array.</para>
        /// <para>Chinese Simplified: 原始二维数组。</para>
        /// </param>
        /// <param name="index">
        /// <para>index of the row or column to copy.</para>
        /// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        /// <param name="b">
        /// <para>output sequence containing copied row or column.</para>
        /// <para>Chinese Simplified: 返回的指定行或列一维数组。</para>
        /// </param>
        /// </param>
        /// <param name="indexType">
        /// <para>specifies whether to copy row or column, the default is by row.</para>
        /// <para>Chinese Simplified: 设定获取行还是列。</para>
        /// </param>

        public static void GetArraySubset<T>(T[,] a, int index, ref T[] b, IndexType indexType = IndexType.row)
        {
            int actualLength;

            if (indexType == IndexType.row)
            {
                // if index is out of range, return immediately
                if (index >= a.GetLength(0)) { return; }
                // calculate actual length and copy data
                actualLength = Math.Min(b.Length, a.GetLength(1));
                for (int i = 0; i < actualLength; i++) { b[i] = a[index,i]; }
            }
            else
            {
                // if index is out of range, return immediately
                if (index >= a.GetLength(1)) { return; }
                // calculate actual length and copy data
                actualLength = Math.Min(b.Length, a.GetLength(0));
                for (int i = 0; i < actualLength; i++) { b[i] = a[i,index]; }
            }
        }

        /// <summary>
        /// <para>Replace a portion of b[] with all elements of array a[], starting at specified startIndex, number of elements equals to length of a[].</para>
        /// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至一维数组b[]中的指定位置，替换原有元素，起始索引值由startIndex给出。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence. </para>
        /// <para>Chinese Simplified: 输入一维数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input and output sequence, a portion of it will be replaced with elements in a[].</para>
        /// <para>Chinese Simplified: 更新后的一维数组。</para>
        /// </param>
        /// <param name="startIndex">
        /// <para>index of the first element in b[] to be replaced.</para>
        /// <para>Chinese Simplified: 起始索引值。</para>
        /// </param>
        public static void ReplaceArraySubset<T>(T[] a, ref T[]b, int startIndex)
        {
            int actualLength;

            actualLength = Math.Min(a.Length, b.Length - startIndex);
            for (int i = 0; i < actualLength; i++) { b[i + startIndex] = a[i]; }
        }

        /// <summary>
        /// <para>Replace one row or column of b[,] with all elements of array a[], the row/column to be replaced is specified by index of row/column.</para>
        /// <para>Chinese Simplified: 将一维数组a[]的所有元素拷贝至二维数组b[,]中的指定行或列，替换该行或列的元素，行数(列数)由index给出，按行索引或按列索引由indexType给出。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input sequence.</para>
        /// <para>Chinese Simplified: 输入一维数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>input and output 2D array, one row or column of it will be replaced with elements in a[].</para>
        /// <para>Chinese Simplified: 更新后的二维数组。</para>
        /// </param>
        /// <param name="index">
        /// <para>index of the row or column to copy.</para>
        /// <para>Chinese Simplified: 行或列索引值，从0开始。</para>
        /// </param>
        /// <param name="indexType">
        /// <para>specifies whether to replace row or column, the default is by row.</para>
        /// <para>Chinese Simplified: 设定替换行还是列。</para>
        /// </param>
        public static void ReplaceArraySubset<T>(T[] a, ref T[,] b, int index, IndexType indexType = IndexType.row)
        {
            int actualLength;

            if (indexType == IndexType.row)
            {
                // if index is out of range, return immediately
                if (index >= b.GetLength(0)) { return; }
                // calculate actual length and copy data
                actualLength = Math.Min(b.GetLength(1), a.Length);
                for (int i = 0; i < actualLength; i++) { b[index, i] = a[i]; }
            }
            else
            {
                // if index is out of range, return immediately
                if (index >= b.GetLength(1)) { return; }
                // calculate actual length and copy data
                actualLength = Math.Min(b.GetLength(0), a.Length);
                for (int i = 0; i < actualLength; i++) { b[i, index] = a[i]; }
            }
        }

        /// <summary>
        /// <para>Rearrange 2D array a[,] with M rows N columns to b[,] with N rows M columns, where b[i,j] = a[j,i].</para>
        /// <para>Chinese Simplified: 二维数组行列转置，即将N行M列的数组转换为M行N列。</para>
        /// </summary>
        /// <param name="a">
        /// <para>input 2D array.</para>
        /// <para>Chinese Simplified: 原始二维数组。</para>
        /// </param>
        /// <param name="b">
        /// <para>output transposed 2D array.</para>
        /// <para>Chinese Simplified: 转置后的二维数组。</para>
        /// </param>
        public static void Transpose<T>(T[,] a, ref T[,] b)
        {
            if (a.GetLength(0) == b.GetLength(1) && a.GetLength(1) == b.GetLength(0))
            {
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        b[j, i] = a[i, j];
                    }
            }
            else
            {
                throw new ArgumentException();
            }
        }
        #endregion

    }
}

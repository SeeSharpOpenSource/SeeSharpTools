namespace SeeSharpTools.JY.Mathematics
{
    /// <summary>
    /// 数组操作类库（对数组元素进行操作）
    /// </summary>
    public class ArrayOperation
    {
        /// <summary>
        /// 将两个一维数组拼接成一维数组（src1在前src2在后)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src1">一维数组1</param>
        /// <param name="src2">一维数组2</param>
        /// <param name="dest">返回一维数组</param>
        public static void Concatenate<T>(T[] src1, T[] src2, ref T[] dest)
        {
            Engine.Base.Concatenate(src1, src2, ref dest);
        }

        /// <summary>
        /// 将两个一维数组拼接成二维数组（按列拼接）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src1">一维数组1</param>
        /// <param name="src2">一维数组2</param>
        /// <param name="dest">返回二维数组</param>
        public static void Concatenate<T>(T[] src1, T[] src2, ref T[,] dest)
        {
            Engine.Base.Concatenate(src1, src2, ref dest);
        }

        /// <summary>
        /// 将一个一维数组拼接到另一个二维数组二维数组（按列拼接）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src1">二维数组1</param>
        /// <param name="src2">一维数组2</param>
        /// <param name="dest">返回二维数组</param>
        public static void Concatenate<T>(T[,] src1, T[] src2, ref T[,] dest)
        {
            Engine.Base.Concatenate(src1, src2, ref dest);
        }

        /// <summary>
        /// 转换值类型
        /// </summary>
        /// <typeparam name="Tin">泛型</typeparam>
        /// <typeparam name="Tout">泛型</typeparam>
        /// <param name="input">输入一维数组</param>
        /// <param name="output">输出一维数组</param>
        public static void ConvertTo<Tin, Tout>(Tin[] input, ref Tout[] output)
        {
            Engine.Base.ConvertTo(input, ref output);
        }

        /// <summary>
        /// 转换值类型
        /// </summary>
        /// <typeparam name="Tin">泛型</typeparam>
        /// <typeparam name="Tout">泛型</typeparam>
        /// <param name="input">输入二维数组</param>
        /// <param name="output">输出二维数组</param>
        public static void ConvertTo<Tin, Tout>(Tin[,] input, ref Tout[,] output)
        {
            Engine.Base.ConvertTo(input, ref output);
        }

        /// <summary>
        /// 拷贝数组
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">输入一维数组</param>
        /// <param name="dest">输出一维数组</param>
        public static void Copy<T>(T[] src, ref T[] dest)
        {
            Engine.Base.Copy(src, ref dest);
        }

        /// <summary>
        /// 拷贝数组
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">输入二维数组</param>
        /// <param name="dest">输出二维数组</param>
        public static void Copy<T>(T[,] src, ref T[,] dest)
        {
            Engine.Base.Copy(src, ref dest);
        }

        /// <summary>
        /// 从数组中删除元素
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">来源数组</param>
        /// <param name="index">索引</param>
        /// <param name="dest">返回数组</param>
        public static void Delete<T>(T[] src, int index, ref T[] dest)
        {
            Engine.Base.Delete(src, index, ref dest);
        }

        /// <summary>
        /// 从一维数组中获取局部资料
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">来源数组</param>
        /// <param name="index">索引</param>
        /// <param name="dest">返回数组</param>
        public static void GetSubset<T>(T[] src, int index, ref T[] dest)
        {
            Engine.Base.GetSubset(src, index, ref dest);
        }

        /// <summary>
        /// 从二维维数组中获取局部资料（根据行或列指定）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">来源数组</param>
        /// <param name="index">索引</param>
        /// <param name="dest">返回数组</param>
        /// <param name="byRow">True:按行获取,False:按列获取</param>
        public static void GetSubset<T>(T[,] src, int index, ref T[] dest, bool byRow = false)
        {
            Engine.Base.GetSubset(src, index, ref dest);
        }

        /// <summary>
        /// 从一维数组中插入资料
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">来源数组</param>
        /// <param name="startIdx">开始的索引位置</param>
        /// <param name="element">插入元素</param>
        /// <param name="dest">返回数组</param>
        public static void Insert<T>(T[] src, int startIdx, T element, ref T[] dest)
        {
            Engine.Base.Insert(src, startIdx, element, ref dest);
        }

        /// <summary>
        /// 反向排列数组
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">数组</param>
        public static void Inverse<T>(ref T[] src)
        {
            Engine.Base.Inverse(ref src);
        }

        /// <summary>
        /// 取代数组中指定索引位置的值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">来源数组</param>
        /// <param name="startIdx">索引位置</param>
        /// <param name="element">替换元素</param>
        public static void ReplaceSubset<T>(ref T[] src, int startIdx, T element)
        {
            Engine.Base.ReplaceSubset(ref src, startIdx, element);
        }

        /// <summary>
        /// 取代数组中指定索引位置之后的值，替换长度依据elements长度决定
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">来源数组</param>
        /// <param name="startIdx">索引位置</param>
        /// <param name="elements">替换的一维数组</param>
        public static void ReplaceSubset<T>(ref T[] src, int startIdx, T[] elements)
        {
            Engine.Base.ReplaceSubset(ref src, startIdx, elements);
        }

        /// <summary>
        /// 转置二维数组
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="src">来源数组</param>
        /// <param name="dest">返回数组</param>
        public static void Transpose<T>(T[,] src, ref T[,] dest)
        {
            Engine.Base.Transpose(src, ref dest);
        }
    }
}
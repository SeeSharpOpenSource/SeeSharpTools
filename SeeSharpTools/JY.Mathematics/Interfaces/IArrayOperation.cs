namespace SeeSharpTools.JY.Mathematics.Interfaces
{
    internal interface IArrayOperation
    {
        void Concatenate<T>(T[] src1, T[] src2, ref T[] dest);

        void Concatenate<T>(T[] src1, T[] src2, ref T[,] dest);

        void Concatenate<T>(T[,] src1, T[] src2, ref T[,] dest);

        void ConvertTo<Tin, Tout>(Tin[] input, ref Tout[] output);

        void ConvertTo<Tin, Tout>(Tin[,] input, ref Tout[,] output);

        void Copy<T>(T[] src, ref T[] dest);

        void Copy<T>(T[,] src, ref T[,] dest);

        void Delete<T>(T[] src, int index, ref T[] dest);

        void GetSubset<T>(T[] src, int index, ref T[] dest);

        void GetSubset<T>(T[,] src, int index, ref T[] dest, bool byRow = false);

        void Insert<T>(T[] src, int startIdx, T element, ref T[] dest);

        void Inverse<T>(ref T[] src);

        void ReplaceSubset<T>(ref T[] src, int startIdx, T element);

        void ReplaceSubset<T>(ref T[] src, int startIdx, T[] elements);

        void Transpose<T>(T[,] src, ref T[,] dest);
    }
}
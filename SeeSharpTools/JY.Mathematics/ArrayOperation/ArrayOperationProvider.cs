using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JY.Mathematics.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    public partial class ProviderBase : IArrayOperation
    {
        public virtual void Concatenate<T>(T[] src1, T[] src2, ref T[] dest)
        {
            ArrayManipulation.Connect_1D_Array(src1, src2, ref dest);
        }

        public virtual void Concatenate<T>(T[] src1, T[] src2, ref T[,] dest)
        {
            ArrayManipulation.Connected_2D_Array(src1, src2, ref dest);
        }

        public virtual void Concatenate<T>(T[,] src1, T[] src2, ref T[,] dest)
        {
            ArrayManipulation.ArrayConnect2(src1, src2, ref dest);
        }

        public virtual void ConvertTo<Tin, Tout>(Tin[] input, ref Tout[] output)
        {
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (Tout)Convert.ChangeType(input[i], typeof(Tout));
            }
        }

        public virtual void ConvertTo<Tin, Tout>(Tin[,] input, ref Tout[,] output)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    output[i, j] = (Tout)Convert.ChangeType(input[i, j], typeof(Tout));
                }
            }
        }

        public virtual void Copy<T>(T[] src, ref T[] dest)
        {
            int len = Math.Min(src.Length, dest.Length);
            int size = Marshal.SizeOf(src[0]);
            Buffer.BlockCopy(src, 0, dest, 0, len * size);
        }

        public virtual void Copy<T>(T[,] src, ref T[,] dest)
        {
            int len = Math.Min(src.Length, dest.Length);
            int size = Marshal.SizeOf(src[0, 0]);
            Buffer.BlockCopy(src, 0, dest, 0, len * size);
        }

        public virtual void Delete<T>(T[] src, int index, ref T[] dest)
        {
            int size = Marshal.SizeOf(src[0]);

            Buffer.BlockCopy(src, 0, dest, 0, size * (index <= 1 ? 1 : index - 1));
            Buffer.BlockCopy(src, (index + 1) * size, dest, index * size, (src.Length - index - 1) * size);
        }

        public virtual void GetSubset<T>(T[] src, int index, ref T[] dest)
        {
            ArrayManipulation.GetArraySubset(src, index, ref dest);
        }

        public virtual void GetSubset<T>(T[,] src, int index, ref T[] dest, bool byRow = false)
        {
            ArrayManipulation.GetArraySubset(src, index, ref dest, byRow ? ArrayManipulation.IndexType.row : ArrayManipulation.IndexType.column);
        }

        public virtual void Insert<T>(T[] src, int startIdx, T element, ref T[] dest)
        {
            ArrayManipulation.Insert_1D_Array(src, element, startIdx, ref dest);
        }

        public virtual void Inverse<T>(ref T[] src)
        {
            Array.Reverse(src);
        }

        public virtual void ReplaceSubset<T>(ref T[] src, int startIdx, T element)
        {
            ArrayManipulation.ReplaceArraySubset(new T[1] { element }, ref src, startIdx);
        }

        public virtual void ReplaceSubset<T>(ref T[] src, int startIdx, T[] elements)
        {
            ArrayManipulation.ReplaceArraySubset(elements, ref src, startIdx);
        }

        public virtual void Transpose<T>(T[,] src, ref T[,] dest)
        {
            ArrayManipulation.Transpose(src, ref dest);
        }
    }
}
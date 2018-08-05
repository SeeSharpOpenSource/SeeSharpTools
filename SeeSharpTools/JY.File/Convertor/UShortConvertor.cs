using System;

namespace SeeSharpTools.JY.File.Convertor
{
    internal class UShortConvertor : IConvertor
    {
        public object Convert(string str)
        {
            return System.Convert.ToUInt16(str);
        }

        public byte[] ToBytes(object value)
        {
            return BitConverter.GetBytes((ushort) value);
        }
    }
}
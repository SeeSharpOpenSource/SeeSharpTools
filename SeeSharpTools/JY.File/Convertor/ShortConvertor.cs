using System;

namespace SeeSharpTools.JY.File.Convertor
{
    internal class ShortConvertor : IConvertor
    {
        public object Convert(string str)
        {
            return System.Convert.ToInt16(str);
        }

        public byte[] ToBytes(object value)
        {
            return BitConverter.GetBytes((short) value);
        }
    }
}
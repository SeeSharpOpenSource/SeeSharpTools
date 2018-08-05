using System;

namespace SeeSharpTools.JY.File.Convertor
{
    internal class DoubleConvertor : IConvertor
    {
        public object Convert(string str)
        {
            return System.Convert.ToDouble(str);
        }

        public byte[] ToBytes(object value)
        {
            byte[] data = new byte[sizeof(double)];
            return BitConverter.GetBytes((double)value);
        }
    }
}
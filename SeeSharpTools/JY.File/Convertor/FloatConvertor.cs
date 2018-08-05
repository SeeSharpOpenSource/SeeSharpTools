using System;

namespace SeeSharpTools.JY.File.Convertor
{
    internal class FloatConvertor : IConvertor
    {
        public object Convert(string str)
        {
            return System.Convert.ToSingle(str);
        }

        public byte[] ToBytes(object value)
        {
            byte[] data = new byte[sizeof(float)];
            return BitConverter.GetBytes((float)value);
        }
    }
}
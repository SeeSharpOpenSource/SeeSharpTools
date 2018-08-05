using System;

namespace SeeSharpTools.JY.File.Convertor
{
    internal class UIntConvertor : IConvertor
    {
        public object Convert(string str)
        {
            return System.Convert.ToUInt32(str);
        }

        public byte[] ToBytes(object value)
        {
            return BitConverter.GetBytes((uint) value);
        }
    }
}
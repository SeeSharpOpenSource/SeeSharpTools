using System;
using SeeSharpTools.JY.File.Common;

namespace SeeSharpTools.JY.File.Convertor
{
    internal class IntConvertor : IConvertor
    {
        public object Convert(string str)
        {
            return System.Convert.ToInt32(str);
        }

        public byte[] ToBytes(object value)
        {
            return BitConverter.GetBytes((int) value);
        }
    }
}
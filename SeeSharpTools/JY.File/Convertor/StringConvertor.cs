using SeeSharpTools.JY.File.Common;

namespace SeeSharpTools.JY.File.Convertor
{
    internal class StringConvertor : IConvertor
    {
        public object Convert(string str)
        {
            return str;
        }

        public byte[] ToBytes(object value)
        {
            return Constants.StrEncode.GetBytes((string)value);
        }
    }
}
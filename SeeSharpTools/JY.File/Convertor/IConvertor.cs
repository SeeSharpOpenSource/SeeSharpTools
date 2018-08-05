namespace SeeSharpTools.JY.File.Convertor
{
    internal interface IConvertor
    {
        object Convert(string str);
        byte[] ToBytes(object value);
    }

}
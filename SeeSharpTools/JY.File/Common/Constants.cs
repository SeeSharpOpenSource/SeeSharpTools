using System.Text;

namespace SeeSharpTools.JY.File.Common
{
    internal static class Constants
    {
        /// <summary>
        /// Version Position In File
        /// </summary>
        public const int VersionPosition = 0;

        public const int PropertySize = 20;

        public const int VersionCount = 3;
        public const uint IllegalVersion = 0;

        public const int CheckerPosition = 4;
        public const int CheckerSize = 16;
        public const string CheckerValue = "SeeSharpBinary";
        public const char CheckerTail = '\0';
        public static Encoding StrEncode = Encoding.UTF8;
    }
}
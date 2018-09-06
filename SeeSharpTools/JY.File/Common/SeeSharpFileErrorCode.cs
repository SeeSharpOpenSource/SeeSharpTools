
namespace SeeSharpTools.JY.File.Common
{
    // TODO 暂时不开放
    /// <summary>
    /// File operation module error code class
    /// </summary>
    internal static class SeeSharpFileErrorCode
    {
        /// <summary>
        /// Invalid row index
        /// </summary>
        public const int ParamCheckError = -10;

        /// <summary>
        /// Run time exception
        /// </summary>
        public const int RuntimeError = -101;

        /// <summary>
        /// File format error
        /// </summary>
        public const int FileFormatError = -102;

        /// <summary>
        /// File format error
        /// </summary>
        public const int FileDataError = -103;

        /// <summary>
        /// Unsupported data type
        /// </summary>
        public const int UnsupportedDataType = -104;
    }
}
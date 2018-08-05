using System;

namespace SeeSharpTools.JY.File
{
    /// <summary>
    /// File operation exception class
    /// </summary>
    public class SeeSharpFileException : Exception
    {
        /// <summary>
        /// Error Code
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errCode">Error code</param>
        /// <param name="message">Error message</param>
        public SeeSharpFileException(int errCode, string message) : base(message)
        {
            this.ErrorCode = errCode;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errCode">Error code</param>
        /// <param name="message">Error message</param>
        /// <param name="innerException"></param>
        public SeeSharpFileException(int errCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCode = errCode;
        }

    }
}
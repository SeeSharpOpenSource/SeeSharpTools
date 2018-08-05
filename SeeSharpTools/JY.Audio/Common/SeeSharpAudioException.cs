using System;

namespace SeeSharpTools.JY.Audio.Common
{
    public class SeeSharpAudioException : Exception
    {
        public int ErrorCode { get; private set; }

        public SeeSharpAudioException(int errCode, string message) : base(message)
        {
            this.ErrorCode = errCode;
        }

        public SeeSharpAudioException(int errCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCode = errCode;
        }
    }
}
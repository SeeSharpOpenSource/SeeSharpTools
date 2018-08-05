using System;
using System.IO;
using System.Text;

namespace SeeSharpTools.JY.Report.Log
{
    internal class SingleFileLog : FileLogBase
    {
        public SingleFileLog(LogConfig logConfig) : base(logConfig)
        {
            LogStream = new FileStream(Config.FileLog.Path, FileMode.OpenOrCreate);
            LogWriter = new StreamWriter(LogStream, Config.FileLog.Encode);
            LogWriter.AutoFlush = false;
            if (!string.IsNullOrWhiteSpace(Config.Header) && LogStream.Length < Config.Header.Length)
            {
                LogWriter.WriteLine(Config.Header);
            }
            LogStream.Seek(LogStream.Length, SeekOrigin.Begin);
        }

        internal override void HostInfo(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        internal override void Log(LogLevel logLevel, string message)
        {
            bool getLock = false;
            try
            {
                WriteLock.Enter(ref getLock);
                int messageLength = Config.FileLog.Encode.GetByteCount(message);
                // TODO 日志写满后目前先直接清空，后续再考虑移除前半部分
                if (LogStream.Length + messageLength >= Config.FileLog.MaxLogSize)
                {
                    LogStream.SetLength(0);
                    if (!string.IsNullOrWhiteSpace(Config.Header))
                    {
                        LogWriter.WriteLine(Config.Header);
                    }
                }
                LogWriter.WriteLine(Config.LogFormat, logLevel, DateTime.Now.ToString(Config.TimeStampFormat), message);
                FlushData();
            }
            finally
            {
                if (getLock)
                {
                    WriteLock.Exit();
                }
            }
        }

        internal override void Log(LogLevel logLevel, Exception exception, string message)
        {
            bool getLock = false;
            try
            {
                string stackTrace = exception.StackTrace?? "";
                WriteLock.Enter(ref getLock);
                int messageLength = Config.FileLog.Encode.GetByteCount(message);
                messageLength += Config.FileLog.Encode.GetByteCount(stackTrace);
                // TODO 日志写满后目前先直接清空，后续再考虑移除前半部分
                if (LogStream.Length + messageLength >= Config.FileLog.MaxLogSize)
                {
                    LogStream.SetLength(0);
                    if (!string.IsNullOrWhiteSpace(Config.Header))
                    {
                        LogWriter.WriteLine(Config.Header);
                    }
                }
                LogWriter.WriteLine(Config.ExceptionFormat, logLevel, DateTime.Now.ToString(Config.TimeStampFormat), 
                    exception.GetType().Name, message);
                LogWriter.WriteLine(stackTrace);
                FlushData();
            }
            finally
            {
                if (getLock)
                {
                    WriteLock.Exit();
                }
            }
        }
    }
}
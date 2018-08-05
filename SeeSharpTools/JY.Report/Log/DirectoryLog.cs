using System;
using System.IO;

namespace SeeSharpTools.JY.Report.Log
{
    // 目录日志类
    internal class DirectoryLog : FileLogBase
    {
        private readonly string _logPathFormat;

        public DirectoryLog(LogConfig logConfig) : base(logConfig)
        {
            if (!Directory.Exists(Config.FileLog.Path))
            {
                Directory.CreateDirectory(Config.FileLog.Path);
            }
            _logPathFormat = $"{Config.FileLog.Path}{Path.DirectorySeparatorChar}{{0}}.{Config.FileLog.Extension}";
            string logPath = string.Format(_logPathFormat, DateTime.Now.ToString(Config.FileLog.LogNameFormat));
            LogStream = new FileStream(logPath, FileMode.OpenOrCreate);
            LogWriter = new StreamWriter(LogStream, Config.FileLog.Encode);
            LogWriter.AutoFlush = false;
            if (!string.IsNullOrWhiteSpace(Config.Header))
            {
                LogWriter.WriteLine(Config.Header);
            }
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
                // 如果文件写满，则重新创建日志文件
                if (LogStream.Length + messageLength >= Config.FileLog.MaxLogSize)
                {
                    LogWriter.Dispose();
                    LogStream.Dispose();
                    string logPath = string.Format(_logPathFormat, DateTime.Now.ToString(Config.FileLog.LogNameFormat));
                    LogStream = new FileStream(logPath, FileMode.OpenOrCreate);
                    LogWriter = new StreamWriter(LogStream, Config.FileLog.Encode);
                    LogWriter.AutoFlush = false;
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
                string stackTrace = exception.StackTrace ?? "";
                WriteLock.Enter(ref getLock);
                int messageLength = Config.FileLog.Encode.GetByteCount(message);
                messageLength += Config.FileLog.Encode.GetByteCount(stackTrace);
                // 如果文件写满，则重新创建日志文件
                if (LogStream.Length + messageLength >= Config.FileLog.MaxLogSize)
                {
                    LogWriter.Dispose();
                    LogStream.Dispose();
                    string logPath = string.Format(_logPathFormat, DateTime.Now.ToString(Config.FileLog.LogNameFormat));
                    LogStream = new FileStream(logPath, FileMode.OpenOrCreate);
                    LogWriter = new StreamWriter(LogStream, Config.FileLog.Encode);
                    LogWriter.AutoFlush = false;
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
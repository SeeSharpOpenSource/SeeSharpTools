using System;
using System.Threading;

namespace SeeSharpTools.JY.Report.Log
{
    internal abstract class LogBase : IDisposable
    {
        public LogConfig Config { get; }
        private int _logLevel;
        public LogLevel LogLevel
        {
            get {return (LogLevel)_logLevel;}
            set { Thread.VolatileWrite(ref _logLevel, (int)value);}
        }

        protected LogBase(LogConfig logConfig)
        {
            this.Config = logConfig;
            this.LogLevel = LogLevel.Info;
        }

        internal abstract void Log(LogLevel logLevel, string message);
        internal abstract void Log(LogLevel logLevel, Exception exception, string message);

        internal abstract void HostInfo(LogLevel logLevel);

        internal abstract void Close();

        internal static LogBase CreateLogWriter(LogConfig config)
        {
            LogBase logWriter = null;
            switch (config.Type)
            {
                case LogType.FileLog:
                    switch (config.FileLog.LogMode)
                    {
                        case FileLogMode.SingleFile:
                            logWriter = new SingleFileLog(config);
                            break;
                        case FileLogMode.Directory:
                            logWriter = new DirectoryLog(config);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return logWriter;
        }

        public void Dispose()
        {
            this.Close();
        }

        ~LogBase()
        {
            this.Close();
        }
    }
}
using System;

namespace SeeSharpTools.JY.Report.Log
{
    internal interface ILogger
    {
        void Trace(string message, params object[] args);
        void Debug(string message, params object[] args);
        void Info(string message, params object[] args);

        void Warn(string message, params object[] args);
        void Warn(Exception exception, string message, params object[] args);

        void Error(string message, params object[] args);
        void Error(Exception exception, string message, params object[] args);

        void Fatal(string message, params object[] args);
        void Fatal(Exception exception, string message, params object[] args);

        void StackTrace(string message, LogLevel logLevel = LogLevel.Debug);

        void ThreadInfo(LogLevel logLevel = LogLevel.Debug);
        void HostInfo(LogLevel logLevel = LogLevel.Debug);
    }
}
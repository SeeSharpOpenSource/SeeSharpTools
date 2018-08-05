using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using SeeSharpTools.JY.Report.Log;

namespace SeeSharpTools.JY.Report
{
    /// <summary>
    /// Log class
    /// </summary>
    public static class Logger
    {
        // log function instance
        private static LogBase _logInstance = null;

        /// <summary>
        /// Get log configuration class
        /// </summary>
        public static LogConfig Config => _logInstance?.Config;

        /// <summary>
        /// Current log level. Log information with lower level will not be recorded.
        /// </summary>
        public static LogLevel LogLevel
        {
            get { return _logInstance.LogLevel; }
            set { _logInstance.LogLevel = value; }
        }

        private const int LogEnabled = 1;
        private const int LogDisabled = 0;
        private static int _enabled = LogEnabled;
        /// <summary>
        /// Set or get whether log enabled.
        /// </summary>
        public static bool Enabled
        {
            get { return LogEnabled == _enabled; }
            set
            {
                Thread.VolatileWrite(ref _enabled, value ? LogEnabled : LogDisabled);
            }
        }

        /// <summary>
        /// Initialize a single file log instance. Thread safe not supported.
        /// </summary>
        /// <param name="filePath">Log file path</param>
        public static void Initialize(string filePath)
        {
            LogConfig logConfig = new LogConfig();
            logConfig.Type = LogType.FileLog;
            logConfig.FileLog.LogMode = FileLogMode.SingleFile;
            logConfig.FileLog.Path = filePath;
            Initialize(logConfig);
        }

        /// <summary>
        /// Initialize a log instance by specified configuration.
        /// </summary>
        /// <param name="logConfig">The specified log configuration.</param>
        public static void Initialize(LogConfig logConfig)
        {
            _logInstance?.Close();
            _logInstance = LogBase.CreateLogWriter(logConfig);
        }

        /// <summary>
        /// Recored message. Message will not be recored when property LogLevel is higher than the specified log level.
        /// </summary>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="logLevel">Log level of the message.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Print(string message, LogLevel logLevel = LogLevel.Debug, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || logLevel < _logInstance.LogLevel)
            {
                return;
            }
            _logInstance.Log(logLevel, string.Format(message, args));
        }

        /// <summary>
        /// Recored message. Message will not be recored when property LogLevel is higher than the specified log level.
        /// </summary>
        /// <param name="exception">The exception instance to record.</param>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="logLevel">Log level of the message.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Print(Exception exception, string message, LogLevel logLevel = LogLevel.Debug, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || logLevel < _logInstance.LogLevel)
            {
                return;
            }
            _logInstance.Log(logLevel, exception, string.Format(message, args));
        }

        /// <summary>
        /// Recored message as 'Trace' level. Message will not be recored when property LogLevel is higher than 'Trace'.
        /// </summary>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Trace(string message, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || LogLevel.Trace < _logInstance.LogLevel)
            {
                return;
            }
            _logInstance.Log(LogLevel.Trace, string.Format(message, args));
        }

        /// <summary>
        /// Record message as 'Debug' level. Message will not be recored when property LogLevel is higher than 'Debug'.
        /// </summary>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Debug(string message, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || LogLevel.Debug < LogLevel)
            {
                return;
            }
            _logInstance.Log(LogLevel.Debug, string.Format(message, args));
        }

        /// <summary>
        /// Record message as 'Info' level. Message will not be recored when property LogLevel is higher than 'Info'.
        /// </summary>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Info(string message, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || LogLevel.Info < LogLevel)
            {
                return;
            }
            _logInstance.Log(LogLevel.Info, string.Format(message, args));
        }

        /// <summary>
        /// Record message as 'Warn' level. Message will not be recored when property LogLevel is higher than 'Warn'.
        /// </summary>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Warn(string message, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || LogLevel.Warn < LogLevel)
            {
                return;
            }
            _logInstance.Log(LogLevel.Warn, string.Format(message, args));
        }

        /// <summary>
        /// Record message and exception as 'Warn' level. Message will not be recored when property 
        /// LogLevel is higher than 'Warn'.
        /// </summary>
        /// <param name="exception">The exception instance to record.</param>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Warn(Exception exception, string message, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || LogLevel.Warn < LogLevel)
            {
                return;
            }
            _logInstance.Log(LogLevel.Warn, exception, string.Format(message, args));
        }

        /// <summary>
        /// Record message as 'Error' level. Message will not be recored when property LogLevel is higher than 'Error'.
        /// </summary>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Error(string message, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || LogLevel.Error < LogLevel)
            {
                return;
            }
            _logInstance.Log(LogLevel.Error, string.Format(message, args));
        }


        /// <summary>
        /// Record message and exception as 'Error' level. Message will not be recored when 
        /// property LogLevel is higher than 'Error'.
        /// </summary>
        /// <param name="exception">The exception instance to record.</param>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Error(Exception exception, string message, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || LogLevel.Error < LogLevel)
            {
                return;
            }
            _logInstance.Log(LogLevel.Error, exception, string.Format(message, args));
        }

        /// <summary>
        /// Record message as 'Fatal' level. Message will not be recored when property LogLevel 
        /// is higher than 'Fatal'.
        /// </summary>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Fatal(string message, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || LogLevel.Fatal < LogLevel)
            {
                return;
            }
            _logInstance.Log(LogLevel.Fatal, string.Format(message, args));
        }

        /// <summary>
        /// Record message and exception as 'Debug' level. Message will not be recored when property 
        /// LogLevel is higher than 'Fatal'.
        /// </summary>
        /// <param name="exception">The exception instance to record.</param>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="args">The parameters of message format.</param>
        public static void Fatal(Exception exception, string message, params object[] args)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || LogLevel.Fatal < LogLevel)
            {
                return;
            }
            _logInstance.Log(LogLevel.Fatal, exception, string.Format(message, args));
        }

        /// <summary>
        /// Record current stack trace to log. Message will not be recored when property LogLevel 
        /// is higher than the specified log level in parameter.
        /// </summary>
        /// <param name="message">The message/message format to record.</param>
        /// <param name="logLevel">The log level of current operation</param>
        public static void StackTrace(string message, LogLevel logLevel = LogLevel.Debug)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || logLevel < LogLevel)
            {
                return;
            }
            _logInstance.Log(logLevel, $"{string.Format(Config.StackTraceFormat, message)}{Environment.NewLine}{new StackTrace()}" );
        }

        /// <summary>
        /// Record current thread information to log file. Message will not be recored when property LogLevel 
        /// is higher than the specified log level in parameter
        /// </summary>
        /// <param name="logLevel">The log level of current operation</param>
        public static void ThreadInfo(LogLevel logLevel = LogLevel.Debug)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || logLevel < LogLevel)
            {
                return;
            }
            Thread currentThread = Thread.CurrentThread;
            _logInstance.Log(logLevel, string.Format(Config.ThreadInfoFormat, currentThread.ManagedThreadId, currentThread.Name));
        }

        /// <summary>
        /// Record Host info to log file. Message will not be recored when property LogLevel is higher 
        /// than the specified log level in parameter
        /// </summary>
        /// <param name="logLevel">The log level of current operation</param>
        public static void HostInfo(LogLevel logLevel = LogLevel.Debug)
        {
            if (null == _logInstance)
            {
                throw new InvalidOperationException("Logger should be initialized before using.");
            }
            if (_enabled == LogDisabled || logLevel < LogLevel)
            {
                return;
            }
            _logInstance.HostInfo(logLevel);
        }

        /// <summary>
        /// Close the current log instance. Thread safe not supported.
        /// </summary>
        public static void Close()
        {
            _logInstance?.Close();
            _logInstance = null;
        }
    }
}
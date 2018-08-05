using System;
using System.Text;

namespace SeeSharpTools.JY.Report
{
    /// <summary>
    /// 
    /// </summary>
    public class LogConfig
    {
        /// <summary>
        /// The type of log.
        /// </summary>
        public LogType Type { get; set; }
        /// <summary>
        /// The instance of file log configuration.
        /// </summary>
        public FileLogConfig FileLog { get; }

        /// <summary>
        /// Log format.{0} for log level; {1} for time stamp; {2} for message
        /// </summary>
        public string LogFormat { get; set; }

        /// <summary>
        /// Exception information format.{0} for log level; {1} for time stamp; {2} for exception type {3} for exception message
        /// </summary>
        public string ExceptionFormat { get; set; }

        /// <summary>
        /// Stack trace information format. {0} for message.
        /// </summary>
        public string StackTraceFormat { get; set; }

        /// <summary>
        /// Thread information format. {0} for thread id; {1} for thread name
        /// </summary>
        public string ThreadInfoFormat { get; set; }

        /// <summary>
        /// Time stamp format
        /// </summary>
        public string TimeStampFormat { get; set; }

        /// <summary>
        /// Log file header
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Asynchonous flush interval
        /// </summary>
        public int FlushInterval { get; set; }

        /// <summary>
        /// Create a new instance of log configuration class.
        /// </summary>
        public LogConfig()
        {
            const string exceptionInfo = "[{0}] [{1}] ExceptionType:{2}  Message:{3}  StackTrace:";
            const string logInfo = "[{0}] [{1}] {2}";
            const string stackTrace = "{0} StackTrace:";
            const string threadFormat = "ThreadId:{0} ThreadName:{1}";
            const string timeStampFormat = "yyyy-MM-dd hh:mm:ss:fff";
            this.Type = LogType.FileLog;
            this.FileLog = new FileLogConfig();
           
            this.LogFormat = logInfo;
            this.ExceptionFormat = exceptionInfo;
            this.StackTraceFormat = stackTrace;
            this.ThreadInfoFormat = threadFormat;
            this.TimeStampFormat = timeStampFormat;
            this.FlushInterval = 1000;

            string newLine = Environment.NewLine;
            StringBuilder defaultHeader = new StringBuilder(300);
            defaultHeader.Append("**************************************************").Append(newLine);
            defaultHeader.Append("**                                              **").Append(newLine);
            defaultHeader.Append("**        SeeSharpTools.JY.Report.Logger        **").Append(newLine);
            defaultHeader.Append("**                                              **").Append(newLine);
            defaultHeader.Append("**************************************************").Append(newLine);
            this.Header = defaultHeader.ToString();
        }
    }

    /// <summary>
    /// File log configuration class.
    /// </summary>
    public class FileLogConfig
    {
        /// <summary>
        /// The extension of log file
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// The way to save log file.
        /// </summary>
        public FileLogMode LogMode { get; set; }

        /// <summary>
        /// The path of log file or log directory.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The log file name format when LogMode is 'Directory'.
        /// </summary>
        public string LogNameFormat { get; set; }

        /// <summary>
        /// The maximum size of single log file.
        /// </summary>
        public long MaxLogSize { get; set; }

        /// <summary>
        /// The encoding of log file.
        /// </summary>
        public Encoding Encode { get; set; }

        /// <summary>
        /// Flush type of file
        /// </summary>
        public FlushType Flush { get; set; }

        internal FileLogConfig()
        {
            this.Extension = "log";
            this.LogMode = FileLogMode.SingleFile;
            this.LogNameFormat = "yyyy-MM-dd hh-mm-ss";
            this.Path = "SeeSharpLog.log";
            this.MaxLogSize = 100000000L;
            this.Encode = Encoding.Unicode;
            this.Flush = FlushType.SyncFlush;
        }
    }
}
namespace SeeSharpTools.JY.Report
{
    /// <summary>
    /// The type of log
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Log to file
        /// </summary>
        FileLog = 0,
    }


    /// <summary>
    /// File log mode
    /// </summary>
    public enum FileLogMode
    {
        /// <summary>
        /// Save log to single file
        /// </summary>
        SingleFile,

        /// <summary>
        /// Save log files to directory
        /// </summary>
        Directory
    }

    /// <summary>
    /// Log level. Fatal>Error>Warn>Info>Debug>Trace.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Trace log level
        /// </summary>
        Trace = 0,

        /// <summary>
        /// Debug log level
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Info log level
        /// </summary>
        Info = 2,

        /// <summary>
        /// Warn log level
        /// </summary>
        Warn = 3,

        /// <summary>
        /// Error log level
        /// </summary>
        Error = 4,

        /// <summary>
        /// Fatal log level
        /// </summary>
        Fatal = 5
    }

    /// <summary>
    /// Flush type of log file
    /// </summary>
    public enum FlushType
    {
        /// <summary>
        /// Flush immediately when logged.
        /// </summary>
        SyncFlush = 0,

        /// <summary>
        /// Flush asynchronously. Faster while may lost data when process crashed.
        /// </summary>
        AsyncFlush = 1
    }

}
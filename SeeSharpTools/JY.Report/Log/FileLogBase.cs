using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace SeeSharpTools.JY.Report.Log
{
    // 文件日志功能基类
    internal abstract class FileLogBase : LogBase
    {
        protected StreamWriter LogWriter;
        protected FileStream LogStream;
        public string TimeStampFormat { get; set; }

        protected SpinLock WriteLock;

        private readonly Timer _timer;

        const int HasDataInStream = 1;
        const int NoDataInStream = 0;
        // 流中是否有数据的标识位：0为没有、1为有
        private int _hasDataInStream;
        const int FlushWaitTime = Timeout.Infinite;

        protected FileLogBase(LogConfig logConfig) : base(logConfig)
        {
            this.WriteLock = new SpinLock();
            _timer = new Timer(FlushCallBack, null, FlushWaitTime, FlushWaitTime);
            _hasDataInStream = NoDataInStream;
        }

        // 异步flush的代码
        private void FlushCallBack(object obj)
        {
            bool getLock = false;
            try
            {
                WriteLock.Enter(ref getLock);
                if (NoDataInStream == _hasDataInStream)
                {
                    return;
                }
                LogStream.Flush();
                Thread.VolatileWrite(ref _hasDataInStream, NoDataInStream);
            }
            finally
            {
                if (getLock)
                {
                    WriteLock.Exit();
                }
            }
        }

        // Flush数据到文件，异步的flush通过timer实现
        protected void FlushData()
        {
            if (FlushType.SyncFlush == this.Config.FileLog.Flush)
            {
                // 调用该方法的外围已获得锁，无需额外获取锁
                LogWriter.Flush();
                LogStream.Flush();
            }
            else
            {
                if (_hasDataInStream == HasDataInStream)
                {
                    return;
                }
                Thread.VolatileWrite(ref _hasDataInStream, HasDataInStream);
                _timer.Change(Config.FlushInterval, FlushWaitTime);
            }
        }
        
        // 释放资源
        internal override void Close()
        {
            bool getLock = false;
            try
            {
//                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                WriteLock.Enter(ref getLock);
                bool canWrite = LogWriter?.BaseStream.CanWrite??false;
                if (null != LogWriter)
                {
                    if (LogWriter.BaseStream.CanWrite)
                    {
                        LogWriter?.Flush();
                    }
                    LogStream.Close();
                    LogWriter = null;
                }
                LogStream = null;
                _timer?.Dispose();
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
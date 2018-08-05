using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeeSharpTools.JY.TCP
{
    /// <summary>
    /// 
    /// </summary>
    public class JYTCPServer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="listenPort">监听的端口</param>
        public JYTCPServer(int listenPort)
        {
            _server = new JYAsyncTcpServer(listenPort);
            if (_server == null)
            {
                throw new Exception("监听端口创建失败！");
            }

            _bufferSize = 1024 * 1024 * 10;//10MB
        }

        ~JYTCPServer()
        {
            Stop();
        }
        #region------------------Private----------------------
        private int _bufferSize;

        /// <summary>
        /// 服务器连接句柄
        /// </summary>
        JYAsyncTcpServer _server;

        /// <summary>
        /// 标识开始
        /// </summary>
        private bool isStart = false;

        


        /// <summary>
        /// 内部缓冲区
        /// </summary>
        CircularBuffer<byte> _cricularBuffer;

        private TcpDatagramReceivedEventArgs<byte[]> eventArgs;
        #endregion

        #region------------------Public-----------------------

        /// <summary>
        /// 缓冲区大小，字节数
        /// </summary>
        public int BufferSize
        {
            get { return _bufferSize; }
            set
            {
                _bufferSize = value;
            }
        }


        /// <summary>
        /// 缓冲区可读取的字节数
        /// </summary>
        public int AvailableSamples
        {
            get
            {
                if (isStart)
                {
                    return _cricularBuffer.NumOfElement;
                }
                else
                {
                    return 0;
                }
            }

        }


        private bool _isClientConnect;

        /// <summary>
        /// 客户端是否链接成功
        /// </summary>
        public bool IsClientConnect
        {
            get { return _isClientConnect; }

            set { _isClientConnect = value; }
        }
        #endregion


        #region----------------------PublicMethod------------------------
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            _isClientConnect = false;
            _cricularBuffer = new CircularBuffer<byte>((int)_bufferSize);
            _server.DatagramReceived += _server_DatagramReceived;
            _server.ClientConnected += _server_ClientConnected;
            _server.ClientDisconnected += _server_ClientDisconnected;
            _server.ReceiveBufferSize = 100000;

            _server.Start();
            isStart = true;
        }

        private void _server_ClientDisconnected(object sender, TcpClientDisconnectedEventArgs e)
        {
            _isClientConnect = false;
        }

        private void _server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            _isClientConnect = true;
        }



        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            _server.Stop();
            isStart = false;
        }

        /// <summary>
        /// 读取一维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref byte[] Buf, int TimeOut)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (Buf != null && _cricularBuffer.NumOfElement >= (Buf.Length * sizeof(byte)))
            {
                var dataBuf = new byte[Buf.Length * sizeof(byte)];
                _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                dataBuf = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 读取一维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref double[] Buf, int TimeOut)
        {
            if(!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if(Buf != null && _cricularBuffer.NumOfElement >= (Buf.Length*sizeof(double)))
            {
                var dataBuf = new byte[Buf.Length * sizeof(double)];
                _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                dataBuf = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 读取一维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref float[] Buf, int TimeOut)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (Buf != null && _cricularBuffer.NumOfElement >= (Buf.Length * sizeof(float)))
            {
                var dataBuf = new byte[Buf.Length * sizeof(float)];
                _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                dataBuf = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 读取二维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref double[,] Buf, int TimeOut)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (Buf!=null&&_cricularBuffer.NumOfElement >= (Buf.Length * sizeof(double)))
            {
                var dataBuf = new byte[Buf.Length * sizeof(double)];
                _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                dataBuf = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 读取二维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref float[,] Buf, int TimeOut)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (Buf != null && _cricularBuffer.NumOfElement >= (Buf.Length * sizeof(float)))
            {
                var dataBuf = new byte[Buf.Length * sizeof(float)];
                _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                dataBuf = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(byte[] dataBuf)
        {
            if (isStart && dataBuf.Length != 0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(byte)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _server.SendAll(buffer);
                GC.Collect();
            }


        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(double[] dataBuf)
        {
            if (isStart&&dataBuf.Length!=0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(double)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _server.SendAll(buffer);
                GC.Collect();
            }


        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(float[] dataBuf)
        {
            if (isStart && dataBuf.Length != 0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(float)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _server.SendAll(buffer);
                GC.Collect();
            }


        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(double[,] dataBuf)
        {
            if (isStart && dataBuf.Length != 0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(double)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _server.SendAll(buffer);
                GC.Collect();
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(float[,] dataBuf)
        {
            if (isStart && dataBuf.Length != 0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(float)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _server.SendAll(buffer);
                GC.Collect();
            }
        }

        #endregion

        #region----------------------------PrivateMethod----------------------------------
        private void _server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {
            eventArgs = e;
            _cricularBuffer.Enqueue(eventArgs.Datagram);
        }
        #endregion

    }
}

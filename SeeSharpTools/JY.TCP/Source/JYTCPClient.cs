using System;
using System.Net;

namespace SeeSharpTools.JY.TCP
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class JYTCPClient
    {
        #region Private Fields
        private string _ipAddress;
        private int _port;
        private ChannelDataType _dataType;
        private JYAsyncTcpClient _client;
        private bool _isClose = false;
        private IBuffer _buffer;
        private int _bufferSize = 1;

        #endregion

        #region Ctor
        /// <summary>
        /// JYTCPClient类构造函数
        /// </summary>
        /// <param name="ipAddress">连接的远端ip位置</param>
        /// <param name="port">连接的远端端口号</param>
        /// <param name="dataType">TCP缓存存放的资料类型</param>
        /// <param name="bufferSize">TCP缓存的大小</param>
        public JYTCPClient(string ipAddress, int port, ChannelDataType dataType = ChannelDataType.DataStream, int bufferSize = 131072)
        {
            _ipAddress = ipAddress;
            _port = port;
            _dataType = dataType;

            _client = new JYAsyncTcpClient(_ipAddress, _port);
            if (_client == null)
            {
                throw new Exception("客户端建立失败！");
            }
            _bufferSize = bufferSize;//默认100KB

            switch (_dataType)
            {
                case ChannelDataType.DataStream: 
                    _buffer = new CircularBuffer<byte>((int)_bufferSize);
                    _client.DatagramReceived += _client_DatagramReceived;
                    break;

                case ChannelDataType.String:
                    _buffer = new StringBuffer((int)_bufferSize);
                    _client.PlaintextReceived += _client_PlaintextReceived;
                    break;

                default:
                    break;
            }
            _client.ServerDisconnected += _client_ServerDisconnected;
            _client.Retries = 10;
            _client.RetryInterval = 1;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 返回是否已连接
        /// </summary>
        public bool Connected
        {
            get { return _client.Connected; }
        }

        /// <summary>
        /// 缓冲区大小(选择DataStream类型时单位是字节数，String类型时单位是字符串的个数）
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
        /// 缓冲区可读取的元素个数
        /// </summary>
        public int AvailableSamples
        {
            get
            {
                if (_client.Connected)
                {
                    return _buffer.NumOfElement;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// 开始连接服务器
        /// </summary>
        public void Connect()
        {
            _isClose = false;
            _client.Connect();
        }

        /// <summary>
        /// 关闭与服务器连接
        /// </summary>
        public void DisConnect()
        {
            if (_client.Connected)
            {
                _isClose = true;
                _client.Close();
            }
        }

        /// <summary>
        /// 从String缓存读取一个字符串（如果选择DataStream模式，请选择ReadDataStream方法)
        /// </summary>
        /// <param name="Buf"></param>
        public void ReadString(ref string Buf)
        {
            if (_client.Connected)
            {
                if (_dataType == ChannelDataType.String)
                {
                    if (_buffer.NumOfElement > 0)
                    {
                        ((StringBuffer)_buffer).Dequeue(ref Buf);
                    }
                }
                else
                {
                    throw new Exception("Please Use ReadDataStream() method for channel type of DataStream");
                }
            }
        }

        /// <summary>
        /// 从DataStream缓存读取一维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadDataStream(ref byte[] Buf)
        {
            if (_client.Connected)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    if (_buffer.NumOfElement >= (Buf.Length * sizeof(byte)))
                    {
                        var dataBuf = new byte[Buf.Length * sizeof(byte)];
                        ((CircularBuffer<byte>)_buffer).Dequeue(ref dataBuf, dataBuf.Length);
                        Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                        dataBuf = null;
                        GC.Collect();
                    }
                }
                else
                {
                    throw new Exception("Please Use ReadString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 从DataStream缓存读取一维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadDataStream(ref double[] Buf)
        {
            if (_client.Connected)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    if (_buffer.NumOfElement >= (Buf.Length * sizeof(double)))
                    {
                        var dataBuf = new byte[Buf.Length * sizeof(double)];
                        ((CircularBuffer<byte>)_buffer).Dequeue(ref dataBuf, dataBuf.Length);
                        Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                        dataBuf = null;
                        GC.Collect();
                    }
                }
                else
                {
                    throw new Exception("Please Use ReadString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 从DataStream缓存读取一维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadDataStream(ref float[] Buf)
        {
            if (_client.Connected && Buf != null)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    if (_buffer.NumOfElement >= (Buf.Length * sizeof(float)))
                    {
                        var dataBuf = new byte[Buf.Length * sizeof(float)];
                        ((CircularBuffer<byte>)_buffer).Dequeue(ref dataBuf, dataBuf.Length);
                        Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                        dataBuf = null;
                        GC.Collect();
                    }
                }
                else
                {
                    throw new Exception("Please Use ReadString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 从DataStream缓存读取二维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadDataStream(ref double[,] Buf)
        {
            if (_client.Connected && Buf != null)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    if (_buffer.NumOfElement >= (Buf.Length * sizeof(double)))
                    {
                        var dataBuf = new byte[Buf.Length * sizeof(double)];
                        ((CircularBuffer<byte>)_buffer).Dequeue(ref dataBuf, dataBuf.Length);
                        Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                        dataBuf = null;
                        GC.Collect();
                    }
                }
                else
                {
                    throw new Exception("Please Use ReadString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 从DataStream缓存读取二维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadDataStream(ref float[,] Buf)
        {
            if (_client.Connected && Buf != null)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    if (_buffer.NumOfElement >= (Buf.Length * sizeof(float)))
                    {
                        var dataBuf = new byte[Buf.Length * sizeof(float)];
                        ((CircularBuffer<byte>)_buffer).Dequeue(ref dataBuf, dataBuf.Length);
                        Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                        dataBuf = null;
                        GC.Collect();
                    }
                }
                else
                {
                    throw new Exception("Please Use ReadString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 发送字符串数据（如果选择DataStream模式，请选择ReadDataStream方法)
        /// </summary>
        /// <param name="dataBuf"></param>
        public void SendString(string dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.String)
                {
                    _client.Send(dataBuf);
                }
                else
                {
                    throw new Exception("Please Use SendDataStream() method for channel type of DataStream");
                }
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendDataStream(byte[] dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    var buffer = new byte[dataBuf.Length * sizeof(byte)];
                    Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                    _client.Send(buffer);
                    GC.Collect();
                }
                else
                {
                    throw new Exception("Please Use SendString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendDataStream(double[] dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    var buffer = new byte[dataBuf.Length * sizeof(double)];
                    Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                    _client.Send(buffer);
                    GC.Collect();
                }
                else
                {
                    throw new Exception("Please Use SendString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendDataStream(float[] dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    var buffer = new byte[dataBuf.Length * sizeof(float)];
                    Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                    _client.Send(buffer);
                    GC.Collect();
                }
                else
                {
                    throw new Exception("Please Use SendString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendDataStream(double[,] dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    var buffer = new byte[dataBuf.Length * sizeof(double)];
                    Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                    _client.Send(buffer);
                    GC.Collect();
                }
                else
                {
                    throw new Exception("Please Use SendString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendDataStream(float[,] dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    var buffer = new byte[dataBuf.Length * sizeof(float)];
                    Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                    _client.Send(buffer);
                    GC.Collect();
                }
                else
                {
                    throw new Exception("Please Use SendString() method for channel type of String");
                }
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// 服务器断线事件
        /// </summary>
        public event EventHandler ServerDisconnected;

        private TcpDatagramReceivedEventArgs<byte[]> eventArgs;

        private void _client_PlaintextReceived(object sender, TcpDatagramReceivedEventArgs<string> e)
        {
            ((StringBuffer)_buffer).Enqueue(e.Datagram);
        }

        private void _client_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {
            ((CircularBuffer<byte>)_buffer).Enqueue(e.Datagram);
        }
        private void _client_ServerDisconnected(object sender, TcpServerDisconnectedEventArgs e)
        {
            if (!_isClose)
            {
                ServerDisconnected?.Invoke(sender, e);
            }
        }

        #endregion

    }
}
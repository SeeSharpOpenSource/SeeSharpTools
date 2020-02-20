using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace SeeSharpTools.JY.TCP
{
    /// <summary>
    ///服务器端
    /// </summary>
    public class JYTCPServer
    {
        #region Private Fields
        private int _bufferSize;
        private ChannelDataType _dataType;
        private JYAsyncTcpServer _server;
        private bool isStart = false;
        private List<ClientInformation> _clientsInfo;
        private IBuffer buffer;

        #endregion

        #region Ctor
        /// <summary>
        /// 服务器构造函数
        /// </summary>
        /// <param name="listenPort">监听的端口号</param>
        /// <param name="dataType">缓存区存放资料的类型</param>
        /// <param name="bufferSize">缓存区大小</param>
        public JYTCPServer(int listenPort, ChannelDataType dataType = ChannelDataType.DataStream, int bufferSize = 131072)
        {
            //            LocalIP = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == AddressFamily.InterNetwork).First();
            LocalIP = IPAddress.Any;
            _server = new JYAsyncTcpServer(LocalIP, listenPort);
            _server.ReceiveBufferSize = (uint)bufferSize;
            _clientsInfo = new List<ClientInformation>();
            _dataType = dataType;
            if (_server == null)
            {
                throw new Exception("监听端口创建失败！");
            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~JYTCPServer()
        {
            Stop();
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// 缓存区大小
        /// </summary>
        public int BufferSize
        {
            get { return (int)_server.ReceiveBufferSize; }
            set
            {
                _server.ReceiveBufferSize = (uint)value;
            }
        }

        /// <summary>
        /// 连接到当前服务端口的客户端信息
        /// </summary>
        public List<ClientInformation> ConnectedClients => isStart ? _clientsInfo : null;

        /// <summary>
        /// 本地IP地址信息
        /// </summary>
        public IPAddress LocalIP { get; set; }
        #endregion

        #region Public Methods

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            switch (_dataType)
            {
                case ChannelDataType.DataStream:
                    _server.DatagramReceived += _server_DatagramReceived;
                    break;

                case ChannelDataType.String:
                    _server.PlaintextReceived += _server_PlaintextReceived;
                    break;

                default:
                    break;
            }

            _server.ClientConnected += _server_ClientConnected;
            _server.ClientDisconnected += _server_ClientDisconnected;
            _server.ReceiveBufferSize = 100000;

            _server.Start();
            isStart = true;
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
        /// 从String缓存读取一个字符串（如果选择DataStream模式，请选择ReadDataStream方法)
        /// </summary>
        /// <param name="Buf"></param>
        /// <param name="client"></param>
        public void ReadString(ref string Buf, TcpClient client)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            buffer = _clientsInfo.Find(x => x.Client == client).Buffer;
            if (buffer.NumOfElement > 0)
            {
                if (_dataType == ChannelDataType.String)
                {
                    ((StringBuffer)buffer).Dequeue(ref Buf);
                }
                else
                {
                    throw new Exception("Please Use ReadDataStream() method for channel type of DataStream");
                }
            }
        }

        /// <summary>
        /// 从指定客户端读取读取一维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="buf">用户内存</param>
        /// <param name="client">读取指定的Client</param>
        public int ReadDataStream(ref byte[] buf, TcpClient client)
        {
            return InternalReadStreamData(buf, client, sizeof(byte));
        }

        /// <summary>
        /// 从指定客户端读取读取一维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="buf">用户内存</param>
        /// <param name="client">读取指定的Client</param>
        public int ReadDataStream(ref double[] buf, TcpClient client)
        {
            return InternalReadStreamData(buf, client, sizeof(double));
        }

        /// <summary>
        /// 从DataStream缓存读取一维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="buf">用户内存</param>
        /// <param name="client">读取指定的Client</param>
        public int ReadDataStream(ref float[] buf, TcpClient client)
        {
            return InternalReadStreamData(buf, client, sizeof(float));
        }

        /// <summary>
        /// 从DataStream缓存读取二维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="buf">用户内存</param>
        /// <param name="client">读取指定的Client</param>
        public int ReadDataStream(ref double[,] buf, TcpClient client)
        {
            return InternalReadStreamData(buf, client, sizeof(double));
        }

        /// <summary>
        /// 从DataStream缓存读取二维数组数据（如果选择String模式，请选择ReadString方法)
        /// </summary>
        /// <param name="buf">用户内存</param>
        /// <param name="client">读取指定的Client</param>
        public int ReadDataStream(ref float[,] buf, TcpClient client)
        {
            return InternalReadStreamData(buf, client, sizeof (float));
        }

        private int InternalReadStreamData(Array buf, TcpClient client, int sizeOfType)
        {
            if (!isStart)
            {
                throw new ApplicationException(" 服务器未开始，不能读取！");
            }
            if (_dataType != ChannelDataType.DataStream)
            {
                throw new ApplicationException("Please Use ReadString() method for channel type of String");
            }
            buffer = null;
            if (null == buf || client == null ||
                null == (buffer = _clientsInfo.FirstOrDefault(x => x.Client == client)?.Buffer) ||
                buffer.NumOfElement < (buf.Length * sizeOfType))
            {
                return 0;
            }
            int readSize = ((CircularBuffer<byte>)buffer).Dequeue(ref buf, buf.Length*sizeOfType);
            return readSize > 0 ? readSize / sizeOfType : 0;
        }

        /// <summary>
        /// 发送字符串数据（如果选择DataStream模式，请选择ReadDataStream方法，client=null是广播发送)
        /// </summary>
        /// <param name="dataBuf"></param>
        /// <param name="client"></param>
        public void SendString(string dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.String)
                {
                    if (client != null)
                    {
                        _server.Send(client, dataBuf);
                    }
                    else
                    {
                        _server.SendAll(dataBuf);
                    }
                }
                else
                {
                    throw new Exception("Please Use SendDataStream() method for channel type of DataStream");
                }
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法，client=null是广播发送)
        /// </summary>
        /// <param name="dataBuf"></param>
        /// <param name="client"></param>
        public void SendDataStream(byte[] dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                }
                else
                {
                    throw new Exception("Please Use SendString() method for channel type of String");
                }
                var buffer = new byte[dataBuf.Length * sizeof(byte)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                if (client != null)
                {
                    _server.Send(client, buffer);
                }
                else
                {
                    _server.SendAll(buffer);
                }
                GC.Collect();
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法，client=null是广播发送)
        /// </summary>
        /// <param name="dataBuf"></param>
        /// <param name="client"></param>
        public void SendDataStream(double[] dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    var buffer = new byte[dataBuf.Length * sizeof(double)];
                    Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                    if (client != null)
                    {
                        _server.Send(client, buffer);
                    }
                    else
                    {
                        _server.SendAll(buffer);
                    }
                    GC.Collect();
                }
                else
                {
                    throw new Exception("Please Use SendString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法，client=null是广播发送)
        /// </summary>
        /// <param name="dataBuf"></param>
        /// <param name="client"></param>
        public void SendDataStream(float[] dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    var buffer = new byte[dataBuf.Length * sizeof(float)];
                    Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                    if (client != null)
                    {
                        _server.Send(client, buffer);
                    }
                    else
                    {
                        _server.SendAll(buffer);
                    }
                    GC.Collect();
                }
                else
                {
                    throw new Exception("Please Use SendString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法，client=null是广播发送)
        /// </summary>
        /// <param name="dataBuf"></param>
        /// <param name="client"></param>
        public void SendDataStream(double[,] dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    var buffer = new byte[dataBuf.Length * sizeof(double)];
                    Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                    if (client != null)
                    {
                        _server.Send(client, buffer);
                    }
                    else
                    {
                        _server.SendAll(buffer);
                    }
                    GC.Collect();
                }
                else
                {
                    throw new Exception("Please Use SendString() method for channel type of String");
                }
            }
        }

        /// <summary>
        /// 发送数据(如果选择String模式，请选择ReadString方法，client=null是广播发送)
        /// </summary>
        /// <param name="dataBuf"></param>
        /// <param name="client"></param>
        public void SendDataStream(float[,] dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
            {
                if (_dataType == ChannelDataType.DataStream)
                {
                    var buffer = new byte[dataBuf.Length * sizeof(float)];
                    Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                    if (client != null)
                    {
                        _server.Send(client, buffer);
                    }
                    else
                    {
                        _server.SendAll(buffer);
                    }
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

        public delegate void ClientConnect(TcpClient clientInfo);
        public event ClientConnect ClientConnected;

        public delegate void ClientDisconnect(TcpClient clientInfo);
        public event ClientDisconnect ClientDisconnected;

        private void _server_PlaintextReceived(object sender, TcpDatagramReceivedEventArgs<string> e)
        {
            (_clientsInfo.Find(x => x.Client == e.TcpClient).Buffer as StringBuffer).Enqueue(e.Datagram);

        }

        private void _server_ClientDisconnected(object sender, TcpClientDisconnectedEventArgs e)
        {
            _clientsInfo.RemoveAt(_clientsInfo.FindIndex(x => x.Client == e.TcpClient));
            ClientDisconnected?.Invoke(e.TcpClient);
        }

        private void _server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            switch (_dataType)
            {
                case ChannelDataType.DataStream:
                    _clientsInfo.Add(new ClientInformation() { Client = e.TcpClient, Buffer = new CircularBuffer<byte>((int)_server.ReceiveBufferSize) });
                    break;

                case ChannelDataType.String:
                    _clientsInfo.Add(new ClientInformation() { Client = e.TcpClient, Buffer = new StringBuffer((int)_server.ReceiveBufferSize) });
                    break;

                default:
                    break;
            }
            ClientConnected?.Invoke(e.TcpClient);
        }
        private void _server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {
            ((CircularBuffer<byte>)_clientsInfo.Find(x => x.Client == e.TcpClient).Buffer).Enqueue(e.Datagram);
        }


        #endregion
    }

    /// <summary>
    /// ClientInformation类，存放连接成功的TCPClient资讯以及缓存对象
    /// </summary>
    public class ClientInformation
    {
        /// <summary>
        /// 连接上的TCPClient对象
        /// </summary>
        public TcpClient Client { get; set; }
        /// <summary>
        /// 创建的缓存区对象
        /// </summary>
        public IBuffer Buffer { get; set; }
        /// <summary>
        /// 缓存区内的可读取元素数目
        /// </summary>
        public int AvailableSamples { get { return Buffer.NumOfElement; } }
    }
}
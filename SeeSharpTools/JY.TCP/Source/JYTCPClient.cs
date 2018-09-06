using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SeeSharpTools.JY.TCP
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class JYTCPClient
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="port">端口</param>
        public JYTCPClient(String ipAddress, int port,ChannelDataType dataType= ChannelDataType.DataStream)
        {
            _ipAddress = ipAddress;
            _port = port;
            _dataType = dataType;

            _client = new JYAsyncTcpClient(_ipAddress, _port);
            if (_client == null)
            {
                throw new Exception("客户端建立失败！");

            }
            _bufferSize = 1024 * 1024 * 100;//10MB

            _cricularBuffer = new CircularBuffer<byte>((int)_bufferSize);
            _stringBuffer = new StringBuffer((int)_bufferSize);

            switch (_dataType)
            {
                case ChannelDataType.DataStream:
                    _client.DatagramReceived += _client_DatagramReceived;

                    break;
                case ChannelDataType.String:
                    _client.PlaintextReceived += _client_PlaintextReceived;

                    break;
                default:
                    break;
            }
            _client.ServerDisconnected += _client_ServerDisconnected;
            _client.Retries = 10;
            _client.RetryInterval = 1;
        }

        private void _client_PlaintextReceived(object sender, TcpDatagramReceivedEventArgs<string> e)
        {
            _stringBuffer.Enqueue(e.Datagram);
        }

        private void _client_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {
            _cricularBuffer.Enqueue(e.Datagram);
        }

        #region-----------------------------private------------------

        private string _ipAddress;
        private int _port;
        public event EventHandler ServerDisconnected;

        private ChannelDataType _dataType;
        /// <summary>
        /// 客户端句柄
        /// </summary>
        JYAsyncTcpClient _client;

        /// <summary>
        /// 
        /// </summary>
        private bool _isClose = false;

        /// <summary>
        /// 内部缓冲区
        /// </summary>
        CircularBuffer<byte> _cricularBuffer;
        private StringBuffer _stringBuffer;

        private TcpDatagramReceivedEventArgs<byte[]> eventArgs;

        #endregion
        #region-----------------------------public-------------------

        private int _bufferSize;
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
                if (_client.Connected)
                {
                    switch (_dataType)
                    {
                        case ChannelDataType.DataStream:
                            return _cricularBuffer.NumOfElement;
                        case ChannelDataType.String:
                            return _stringBuffer.NumOfElement;
                        default:
                            return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }

        }

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
            if(_client.Connected)
            {
                _isClose = true;
                _client.Close();
            }
            
        }

        public void ReadString(ref string Buf, int TimeOut)
        {
            if (_client.Connected)
            {
                if (_stringBuffer.NumOfElement> 0)
                {
                    _stringBuffer.Dequeue(ref Buf);
                }
            }
        }

        /// <summary>
        /// 读取一维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref byte[] Buf, int TimeOut)
        {
            if (_client.Connected)
            {
                if (_cricularBuffer.NumOfElement >= (Buf.Length * sizeof(byte)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(byte)];
                    _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                    Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                    dataBuf = null;
                    GC.Collect();
                }
            }
        }

        /// <summary>
        /// 读取一维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref double[] Buf, int TimeOut)
        {
            if (_client.Connected)
            {
                if (_cricularBuffer.NumOfElement >= (Buf.Length * sizeof(double)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(double)];
                    _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                    Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                    dataBuf = null;
                    GC.Collect();
                }
            }
        }

        /// <summary>
        /// 读取一维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref float[] Buf, int TimeOut)
        {
            if (_client.Connected && Buf != null)
            {
                if (_cricularBuffer.NumOfElement >= (Buf.Length * sizeof(float)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(float)];
                    _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                    Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                    dataBuf = null;
                    GC.Collect();
                }
            }
        }

        /// <summary>
        /// 读取二维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref double[,] Buf, int TimeOut)
        {
            if (_client.Connected && Buf != null)
            {
                if (_cricularBuffer.NumOfElement >= (Buf.Length * sizeof(double)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(double)];
                    _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                    Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                    dataBuf = null;
                    GC.Collect();
                }
            }

        }

        /// <summary>
        /// 读取二维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref float[,] Buf, int TimeOut)
        {
            if (_client.Connected&&Buf!=null)
            {
                if (_cricularBuffer.NumOfElement >= (Buf.Length * sizeof(float)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(float)];
                    _cricularBuffer.Dequeue(ref dataBuf, dataBuf.Length);
                    Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                    dataBuf = null;
                    GC.Collect();
                }
            }

        }

        public void SendData(string dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                _client.Send(dataBuf);
            }

        }


        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(byte[] dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(byte)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _client.Send(buffer);
                GC.Collect();
            }


        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(double[] dataBuf)
        {
            if(_client.Connected&&dataBuf.Length!=0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(double)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _client.Send(buffer);
                GC.Collect();
            }


        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(float[] dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(float)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _client.Send(buffer);
                GC.Collect();
            }


        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(double[,] dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(double)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _client.Send(buffer);
                GC.Collect();
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(float[,] dataBuf)
        {
            if (_client.Connected && dataBuf.Length != 0)
            {
                var buffer = new byte[dataBuf.Length * sizeof(float)];
                Buffer.BlockCopy(dataBuf, 0, buffer, 0, buffer.Length);
                _client.Send(buffer);
                GC.Collect();
            }
        }

        #endregion
        #region-----------------------------publicMethod---------------
        /// <summary>
        /// 链接是否成功
        /// </summary>
        public bool Connected
        {
            get { return _client.Connected; }
        }
        #endregion
        #region-----------------------------privateMethod--------------
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

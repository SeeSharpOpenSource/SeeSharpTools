using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

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
        public JYTCPServer(int listenPort, ChannelDataType dataType = ChannelDataType.DataStream)
        {
            _server = new JYAsyncTcpServer(listenPort);
            _dataType = dataType;
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
        private ChannelDataType _dataType;
        /// <summary>
        /// 服务器连接句柄
        /// </summary>
        JYAsyncTcpServer _server;

        
        /// <summary>
        /// 标识开始
        /// </summary>
        private bool isStart = false;

        private Dictionary<TcpClient,CircularBuffer<byte>> _channelDataBuffer;
        private Dictionary<TcpClient, StringBuffer> _channelStringBuffer;


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


        public TcpClient[] ConnectedClients
        {
            get
            {
                if (isStart)
                {
                    switch (_dataType)
                    {
                        case ChannelDataType.DataStream:
                            return _channelDataBuffer.Keys.ToArray();
                        case ChannelDataType.String:
                            return _channelStringBuffer.Keys.ToArray();
                        default:
                            return null;
                    }
                }
                else
                {
                    return null;
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

        public int AvailableSamplesInBuffer(TcpClient client)
        {
            switch (_dataType)
            {
                case ChannelDataType.DataStream:
                    return _channelDataBuffer[client].NumOfElement;
                case ChannelDataType.String:
                    return _channelStringBuffer[client].NumOfElement;
                default:
                    return -1;
            }
        }
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            _isClientConnect = false;
            _channelDataBuffer = new Dictionary<TcpClient, CircularBuffer<byte>>();
            _channelStringBuffer = new Dictionary<TcpClient, StringBuffer>();

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

        private void _server_PlaintextReceived(object sender, TcpDatagramReceivedEventArgs<string> e)
        {
            _channelStringBuffer[e.TcpClient].Enqueue(e.Datagram);
        }

        private void _server_ClientDisconnected(object sender, TcpClientDisconnectedEventArgs e)
        {
            _isClientConnect = false;
            switch (_dataType)
            {
                case ChannelDataType.DataStream:
                    _channelDataBuffer.Remove(e.TcpClient);
                    break;
                case ChannelDataType.String:
                    _channelStringBuffer.Remove(e.TcpClient);
                    break;
                default:
                    break;
            }

        }

        private void _server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            switch (_dataType)
            {
                case ChannelDataType.DataStream:
                    _channelDataBuffer.Add(e.TcpClient,new CircularBuffer<byte>((int)_server.ReceiveBufferSize));
                    break;
                case ChannelDataType.String:
                    _channelStringBuffer.Add(e.TcpClient, new StringBuffer((int)_server.ReceiveBufferSize));
                    break;
                default:
                    break;
            }
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

        public void ReadString(ref string Buf, TcpClient client)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (_channelStringBuffer[client].NumOfElement > 0)
            {
                _channelStringBuffer[client].Dequeue(ref Buf);

            }


        }


        /// <summary>
        /// 读取一维数组数据
        /// </summary>
        /// <param name="Buf">用户内存</param>
        /// <param name="TimeOut">超时时间</param>
        public void ReadData(ref byte[] Buf, TcpClient client)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (_channelDataBuffer.Count != 0 && client != null && _channelDataBuffer.Keys.Contains(client))
            {

                if (Buf != null && _channelDataBuffer[client].NumOfElement >= (Buf.Length * sizeof(byte)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(byte)];
                    _channelDataBuffer[client].Dequeue(ref dataBuf, dataBuf.Length);
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
        public void ReadData(ref double[] Buf, TcpClient client)
        {
            if(!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (_channelDataBuffer.Count != 0 && client != null && _channelDataBuffer.Keys.Contains(client))
            {
                if (Buf != null && _channelDataBuffer[client].NumOfElement >= (Buf.Length * sizeof(double)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(double)];
                    _channelDataBuffer[client].Dequeue(ref dataBuf, dataBuf.Length);
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
        public void ReadData(ref float[] Buf, TcpClient client)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (_channelDataBuffer.Count != 0 && client != null && _channelDataBuffer.Keys.Contains(client))
            {

                if ( Buf != null && _channelDataBuffer[client].NumOfElement >= (Buf.Length * sizeof(float)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(float)];
                    _channelDataBuffer[client].Dequeue(ref dataBuf, dataBuf.Length);
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
        public void ReadData(ref double[,] Buf, TcpClient client)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (_channelDataBuffer.Count != 0 && client != null && _channelDataBuffer.Keys.Contains(client))
            {

                if ( Buf != null && _channelDataBuffer[client].NumOfElement >= (Buf.Length * sizeof(double)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(double)];
                    _channelDataBuffer[client].Dequeue(ref dataBuf, dataBuf.Length);
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
        public void ReadData(ref float[,] Buf, TcpClient client)
        {
            if (!isStart)
            {
                throw new Exception(" 服务器未开始，不能读取！");
            }
            if (_channelDataBuffer.Count != 0 && client != null && _channelDataBuffer.Keys.Contains(client))
            {

                if ( Buf != null && _channelDataBuffer[client].NumOfElement >= (Buf.Length * sizeof(float)))
                {
                    var dataBuf = new byte[Buf.Length * sizeof(float)];
                    _channelDataBuffer[client].Dequeue(ref dataBuf, dataBuf.Length);
                    Buffer.BlockCopy(dataBuf, 0, Buf, 0, dataBuf.Length);
                    dataBuf = null;
                    GC.Collect();
                }
            }
        }

        public void SendData(string dataBuf,TcpClient client=null)
        {
            if (isStart && dataBuf.Length != 0)
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


        }


        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(byte[] dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
            {
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
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(double[] dataBuf, TcpClient client = null)
        {
            if (isStart&&dataBuf.Length!=0)
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


        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(float[] dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
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


        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(double[,] dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
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
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="dataBuf">发送数据</param>
        public void SendData(float[,] dataBuf, TcpClient client = null)
        {
            if (isStart && dataBuf.Length != 0)
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
        }

        #endregion

        #region----------------------------PrivateMethod----------------------------------
        private void _server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {
            _channelDataBuffer[e.TcpClient].Enqueue(e.Datagram);
        }
        #endregion

    }
}

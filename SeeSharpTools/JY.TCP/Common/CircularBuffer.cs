using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpTools.JY.TCP
{
    /// <summary>
    /// 循环缓冲队列类，旧版本，托管内存
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    internal class CircularBuffer<T>
    {
        private readonly int _sizeOfT; //T的Size，创建队列的时候初始化
        private T[] _buffer;           //缓冲区

        private int _WRIdx;           //队列写指针
        private int _RDIdx;           //队列读指针

        private volatile int _numOfElement;
        /// <summary>
        /// 当前的元素个数
        /// </summary>
        public int NumOfElement
        {

            get
            {
                lock (this)
                {
                    return _numOfElement;
                }
            }
        }

        private int _bufferSize;       //循环队列缓冲的大小 
        /// <summary>
        /// 缓冲区的大小
        /// </summary>
        public int BufferSize
        {
            get { return _bufferSize; }
        }

        /// <summary>
        /// 当前能容纳的点数
        /// </summary>
        public int CurrentCapacity
        {
            get
            {
                lock (this)
                {
                    return _bufferSize - _numOfElement;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bufferSize"></param>
        public CircularBuffer(int bufferSize)
        {
            if (bufferSize <= 0) //输入的size无效，创建默认大小的缓冲区
            {
                bufferSize = 1024;
            }
            _bufferSize = bufferSize;

            _buffer = new T[_bufferSize]; //新建对应大小的缓冲区

            _WRIdx = 0;
            _RDIdx = 0;    //初始化读写指针

            _numOfElement = 0;

            _sizeOfT = Marshal.SizeOf(_buffer[0]);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CircularBuffer()
        {
            _bufferSize = 1024;

            _buffer = new T[_bufferSize]; //新建对应大小的缓冲区

            _WRIdx = 0;
            _RDIdx = 0;    //初始化读写指针

            _numOfElement = 0;

            _sizeOfT = Marshal.SizeOf(_buffer[0]);
        }

        /// <summary>
        /// 调整缓冲区大小，数据会被清空
        /// </summary>
        /// <param name="size"></param>
        public void AdjustSize(int size)
        {
            lock (this)
            {
                if (size <= 0)
                {
                    size = 1; //最小size应当为1
                }
                this.Clear();
                _bufferSize = size;
                _buffer = new T[_bufferSize];
            }
        }

        /// <summary>
        /// 清空缓冲区内的数据
        /// </summary>
        public void Clear()
        {
            lock (this)
            {
                _numOfElement = 0;
                _WRIdx = 0;
                _RDIdx = 0;
            }
        }

        /// <summary>
        /// 向缓冲队列中放入一个数据
        /// </summary>
        /// <param name="element"></param>
        public int Enqueue(T element)
        {
            lock (this)
            {
                if (_numOfElement >= _bufferSize)
                {
                    return -1;
                }
                _buffer[_WRIdx] = element;

                if (_WRIdx + 1 >= _bufferSize)
                {
                    _WRIdx = 0;
                }
                else
                {
                    _WRIdx++;
                }

                _numOfElement++;
                return 1;
            }
        }

        /// <summary>
        /// 向缓冲队列中放入一组数据
        /// </summary>
        /// <param name="elements"></param>
        public int Enqueue(T[] elements)
        {
            lock (this)
            {
                if (_numOfElement + elements.Length > _bufferSize)
                {
                    //JYLog.Print("Enqueue error");
                    return -1;
                }

                //超出数组尾部了，应该分两次拷贝进去，先拷贝_WRIdx到结尾的，再从头开始拷贝
                if (_WRIdx + elements.Length > _bufferSize)
                {
                    Buffer.BlockCopy(elements, 0, _buffer, _WRIdx * _sizeOfT, (_bufferSize - _WRIdx) * _sizeOfT);
                    int PutCnt = _bufferSize - _WRIdx;
                    int remainCnt = elements.Length - PutCnt;
                    _WRIdx = 0;
                    Buffer.BlockCopy(elements, PutCnt * _sizeOfT, _buffer, _WRIdx * _sizeOfT, remainCnt * _sizeOfT);
                    _WRIdx = remainCnt;
                }
                else
                {
                    Buffer.BlockCopy(elements, 0, _buffer, _WRIdx * _sizeOfT, elements.Length * _sizeOfT);
                    if (_WRIdx + elements.Length == _bufferSize)
                    {
                        _WRIdx = 0;
                    }
                    else
                    {
                        _WRIdx += elements.Length;
                    }
                }
                _numOfElement += elements.Length;

                return elements.Length;
            }
        }

        /// <summary>
        /// 向缓冲队列中放入一组数据
        /// </summary>
        /// <param name="elements"></param>
        public int Enqueue(T[] elements, int len)
        {
            lock (this)
            {
                if (_numOfElement + len > _bufferSize)
                {
                    return -1;
                }

                //超出数组尾部了，应该分两次拷贝进去，先拷贝_WRIdx到结尾的，再从头开始拷贝
                if (_WRIdx + len > _bufferSize)
                {
                    Buffer.BlockCopy(elements, 0, _buffer, _WRIdx * _sizeOfT, (_bufferSize - _WRIdx) * _sizeOfT);
                    int PutCnt = _bufferSize - _WRIdx;
                    int remainCnt = len - PutCnt;
                    _WRIdx = 0;
                    Buffer.BlockCopy(elements, PutCnt * _sizeOfT, _buffer, _WRIdx * _sizeOfT, remainCnt * _sizeOfT);
                    _WRIdx = remainCnt;
                }
                else
                {
                    Buffer.BlockCopy(elements, 0, _buffer, _WRIdx * _sizeOfT, len * _sizeOfT);
                    if (_WRIdx + len == _bufferSize)
                    {
                        _WRIdx = 0;
                    }
                    else
                    {
                        _WRIdx += len;
                    }
                }
                _numOfElement += len;

                return len;
            }
        }

        /// <summary>
        /// 向缓冲队列中放入一组数据
        /// </summary>
        /// <param name="elements"></param>
        public int Enqueue(T[,] elements)
        {
            lock (this)
            {
                if (_numOfElement + elements.Length > _bufferSize)
                {
                    return -1;
                }

                //超出数组尾部了，应该分两次拷贝进去，先拷贝_WRIdx到结尾的，再从头开始拷贝
                if (_WRIdx + elements.Length > _bufferSize)
                {
                    Buffer.BlockCopy(elements, 0, _buffer, _WRIdx * _sizeOfT, (_bufferSize - _WRIdx) * _sizeOfT);
                    int PutCnt = _bufferSize - _WRIdx;
                    int remainCnt = elements.Length - PutCnt;
                    _WRIdx = 0;
                    Buffer.BlockCopy(elements, PutCnt * _sizeOfT, _buffer, _WRIdx * _sizeOfT, remainCnt * _sizeOfT);
                    _WRIdx = remainCnt;
                }
                else
                {
                    Buffer.BlockCopy(elements, 0, _buffer, _WRIdx * _sizeOfT, elements.Length * _sizeOfT);
                    if (_WRIdx + elements.Length == _bufferSize)
                    {
                        _WRIdx = 0;
                    }
                    else
                    {
                        _WRIdx += elements.Length;
                    }
                }
                _numOfElement += elements.Length;

                return elements.Length;
            }
        }

        /// <summary>
        /// 从缓冲队列中取一个数据
        /// </summary>
        /// <returns>失败：-1，1：返回一个数据</returns>
        public int Dequeue(ref T reqElem)
        {
            lock (this)
            {
                if (_numOfElement <= 0)
                {
                    return -1;
                }
                _numOfElement--;

                reqElem = _buffer[_RDIdx];

                if (_RDIdx + 1 >= _bufferSize)
                {
                    _RDIdx = 0;
                }
                else
                {
                    _RDIdx++;
                }

                return 1;
            }
        }

        /// <summary>
        /// 从缓冲队列中取出指定长度的数据
        /// </summary>
        /// <param name="reqBuffer">请求读取缓冲区</param>
        /// <returns>返回实际取到的数据长度</returns>
        public int Dequeue(ref T[] reqBuffer, int len)
        {
            lock (this)
            {
                int getCnt = len;

                if (len > _numOfElement || _numOfElement <= 0)
                {
                    return -1;
                }
                else if (len <= 0)
                {
                    getCnt = _numOfElement;
                }

                if (_RDIdx + getCnt > _bufferSize)   //取数据的总大小超过了应该分两次拷贝，先拷贝尾部，剩余的从头开始拷贝
                {
                    Buffer.BlockCopy(_buffer, _RDIdx * _sizeOfT, reqBuffer, 0, (_bufferSize - _RDIdx) * _sizeOfT);
                    int fetchedCnt = (_bufferSize - _RDIdx);
                    int remainCnt = getCnt - fetchedCnt;
                    _RDIdx = 0;
                    Buffer.BlockCopy(_buffer, _RDIdx * _sizeOfT, reqBuffer, fetchedCnt * _sizeOfT, remainCnt * _sizeOfT);
                    _RDIdx = remainCnt;
                }
                else
                {
                    Buffer.BlockCopy(_buffer, _RDIdx * _sizeOfT, reqBuffer, 0, getCnt * _sizeOfT);
                    if (_RDIdx + getCnt == _bufferSize)
                    {
                        _RDIdx = 0;
                    }
                    else
                    {
                        _RDIdx += getCnt;
                    }
                }
                _numOfElement -= getCnt;
                return getCnt;
            }
        }

        /// <summary>
        /// 从缓冲队列中取出指定长度的数据
        /// </summary>
        /// <param name="reqBuffer">请求读取缓冲区</param>
        /// <returns>返回实际取到的数据长度</returns>
        public int Dequeue(ref T[,] reqBuffer, int len)
        {
            lock (this)
            {
                int getCnt = len;

                if (len > _numOfElement || _numOfElement <= 0)
                {
                    return -1;
                }
                else if (len <= 0)
                {
                    getCnt = _numOfElement;
                }

                if (_RDIdx + getCnt > _bufferSize)   //取数据的总大小超过了应该分两次拷贝，先拷贝尾部，剩余的从头开始拷贝
                {
                    Buffer.BlockCopy(_buffer, _RDIdx * _sizeOfT, reqBuffer, 0, (_bufferSize - _RDIdx) * _sizeOfT);
                    int fetchedCnt = (_bufferSize - _RDIdx);
                    int remainCnt = getCnt - fetchedCnt;
                    _RDIdx = 0;
                    Buffer.BlockCopy(_buffer, _RDIdx * _sizeOfT, reqBuffer, fetchedCnt * _sizeOfT, remainCnt * _sizeOfT);
                    _RDIdx = remainCnt;
                }
                else
                {
                    Buffer.BlockCopy(_buffer, _RDIdx * _sizeOfT, reqBuffer, 0, getCnt * _sizeOfT);
                    if (_RDIdx + getCnt == _bufferSize)
                    {
                        _RDIdx = 0;
                    }
                    else
                    {
                        _RDIdx += getCnt;
                    }
                }
                _numOfElement -= getCnt;
                return getCnt;
            }
        }
    }


}

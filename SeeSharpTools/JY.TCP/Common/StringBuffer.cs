using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JY.TCP
{
    class StringBuffer
    {
        private int _bufferSize;       //循环队列缓冲的大小 
        private List<string> _buffer;           //缓冲区

        private int _WRIdx;           //队列写指针
        private int _RDIdx;           //队列读指针
        private volatile int _numOfElement;

        public StringBuffer(int bufferSize)
        {
            if (bufferSize <= 0) //输入的size无效，创建默认大小的缓冲区
            {
                bufferSize = 1024;
            }
            _bufferSize = bufferSize;

            _buffer = new List<string>(_bufferSize); //新建对应大小的缓冲区

            _WRIdx = 0;
            _RDIdx = 0;    //初始化读写指针

            _numOfElement = 0;
        }

        public int BufferSize
        {
            get { return _bufferSize; }
        }

        /// <summary>
        /// 当前能容纳的点数
        /// </summary>
        public int NumOfElement
        {
            get
            {
                lock (this)
                {
                    return _buffer.Count;
                }
            }
        }
        public void Clear()
        {
            lock (this)
            {
                _buffer.Clear();
            }
        }
        public int Enqueue(string element)
        {
            lock (this)
            {
                _buffer.Add(element);
                return 1;
            }
        }

        public int Dequeue(ref string reqElem)
        {
            lock (this)
            {
                reqElem = _buffer[0];
                _buffer.RemoveAt(0);
                return 1;
            }
        }

    }
}

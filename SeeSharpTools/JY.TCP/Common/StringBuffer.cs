using System.Collections.Generic;

namespace SeeSharpTools.JY.TCP
{
    /// <summary>
    /// 字符串缓存空间
    /// </summary>
    internal class StringBuffer:IBuffer
    {
        #region Private Fields
        private int _bufferSize;       //循环队列缓冲的大小
        private List<string> _buffer;           //缓冲区

        #endregion

        #region Public Properties
        /// <summary>
        /// 缓存空间大小
        /// </summary>
        public int BufferSize
        {
            get { return _bufferSize; }
        }

        /// <summary>
        /// 缓存内未读取元素数目
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

        #endregion
        #region Ctor
        public StringBuffer(int bufferSize)
        {
            if (bufferSize <= 0) //输入的size无效，创建默认大小的缓冲区
            {
                bufferSize = 1024;
            }
            _bufferSize = bufferSize;

            _buffer = new List<string>(_bufferSize); //新建对应大小的缓冲区
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// 清除缓存内所有内容
        /// </summary>
        public void Clear()
        {
            lock (this)
            {
                _buffer.Clear();
            }
        }
        
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public int Enqueue(string element)
        {
            lock (this)
            {
                _buffer.Add(element);
                return 1;
            }
        }

        /// <summary>
        /// 从缓存读出
        /// </summary>
        /// <param name="reqElem"></param>
        /// <returns></returns>
        public int Dequeue(ref string reqElem)
        {
            lock (this)
            {
                reqElem = _buffer[0];
                _buffer.RemoveAt(0);
                return 1;
            }
        }

        #endregion

        #region Override Methods
        public override bool Equals(object obj)
        {
            var buffer = obj as StringBuffer;
            return buffer != null &&
                   NumOfElement == buffer.NumOfElement;
        }

        #endregion
    }
}
using System;
using System.Collections;
using System.Threading;

namespace SeeSharpTools.JY.ThreadSafeQueue
{
    /// <summary> 
    /// <para>Thread Safe Queue is similar to Queue except Dequeue blocks when no elements are in queue until one is present or queue is destroyed..</para>
    /// <para>Chinese Simplified: 安全线程队列是原始Queue的一种补充，当没有数据在队列中时，会在出队列中进行阻挡性操作，直到数据出现或者队列销毁。</para>
    /// </summary>  
    public class ThreadSafeQueue : Queue
    {
        #region Private Properties

        //判断是否退出TSQ的Flag
        private int _existFlag;
        private bool Exists => _existFlag == 1;

        #endregion

        #region Public Properties
        /// <summary>  
        /// <para>Gets flag indicating if queue has been destroyed.</para>
        /// <para>Chinese Simplified: 判断当前队列是否已经被销毁。</para>
        /// </summary>  
        public bool Destroyed => _existFlag != 1;

        #endregion

        #region Constructors/Destructors

        /// <summary>  
        /// <para>Create a new TSQ.</para>
        /// <para>Chinese Simplified:创建一个安全线程队列 。</para>
        /// </summary>
        /// <param name="col">
        /// <para>The System.Collections.ICollection from which to copy elements.</para>  
        /// <param>Chinese Simplified: 从该system.collections.icollection复制元素。</param>  
        /// </param>
        public ThreadSafeQueue(ICollection col) : base(col)
        {
            _existFlag = 1;
        }

        /// <summary>  
        /// <para>Create a new TSQ.</para>
        /// <para>Chinese Simplified:创建一个安全线程队列 。</para>
        /// </summary>
        /// <param name="capacity">
        /// <para>The initial maximum number of elements.</para>  
        /// <param>Chinese Simplified: 初始化最大的元素数值。</param>  
        /// </param>
        /// <param name="growFactor">
        /// <para>The expansion factor for the capacity of the queue.</para>  
        /// <param>Chinese Simplified: 队列容量的扩展因子 。</param>  
        /// </param>
        public ThreadSafeQueue(int capacity, float growFactor) : base(capacity, growFactor)
        {
            _existFlag = 1;
        }

        /// <summary>  
        /// <para>Create a new TSQ.</para>
        /// <para>Chinese Simplified:创建一个安全线程队列 。</para>
        /// </summary>
        /// <param name="capacity">
        /// <para>The initial maximum number of elements.</para>  
        /// <param>Chinese Simplified: 初始化最大的元素数值。</param>  
        /// </param>
        public ThreadSafeQueue(int capacity) : base(capacity)
        {
            _existFlag = 1;
        }

        /// <summary>  
        /// <para>Create a new TSQ.</para>
        /// <para>Chinese Simplified:创建一个安全线程队列 。</para>
        /// </summary>
        public ThreadSafeQueue() : base()
        {
            _existFlag = 1;
        }

        /// <summary>  
        /// <para>Destroy queue, resume any waiting thread.</para>
        /// <para>Chinese Simplified:销毁队列，析构函数 。</para>
        /// </summary>
        ~ThreadSafeQueue()
        {
            Destroy();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Remove all elements from Queue.
        /// </summary>

        /// <summary>  
        /// <para>Remove all elements from Queue.</para>
        /// <para>Chinese Simplified:清空队列元素。</para>
        /// </summary>
        public override void Clear()
        {
            lock (base.SyncRoot)
                base.Clear();
        }



        /// <summary>
        /// <para>Removes and returns the element at the beginning of the Queue.</para>
        /// <para>Chinese Simplified:获取队列中最前端的数据。</para>
        /// </summary>
        /// <returns>first element in queue.</returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the Queue is destroyed.</exception>
        public override object Dequeue()
        {
            return Dequeue(Timeout.Infinite);
        }

        /// <summary>
        /// <para>Removes and returns the element at the beginning of the Queue.</para>
        /// <para>Chinese Simplified:获取队列中最前端的数据。</para>
        /// </summary>
        /// <param name="timeout">
        /// <para>time to wait before returning if no element exists.</para>  
        /// <param>Chinese Simplified: 如果没有元素存在等待的时间。</param>  
        /// </param>
        /// <returns>element in queue.</returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if no element exists in queue 
        /// longer than specified Timeout or if the Queue is destroyed.</exception>
        public object Dequeue(TimeSpan timeout)
        {
            return Dequeue(timeout.Milliseconds);
        }

         /// <summary>
        /// <para>Removes and returns the element at the beginning of the Queue.</para>
        /// <para>Chinese Simplified:获取队列中最前端的数据。</para>
        /// </summary>
        /// <param name="timeout">
        /// <para>time to wait before returning (in milliseconds) if no element exists.</para>  
        /// <param>Chinese Simplified: 如果没有元素存在等待的时间(ms)。</param>  
        /// </param>
        /// <returns>first element in queue.</returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if no element exists in queue 
        /// longer than specified Timeout or if the Queue is destroyed.</exception>
        public object Dequeue(int timeout)
        {
            lock (base.SyncRoot)
            {
                while (Exists && (base.Count == 0))
                {
                    if (!Monitor.Wait(base.SyncRoot, timeout))
                        throw new TimeoutException("TSQ Timeout");
                }
                if (Exists)
                {                    
                    Monitor.Pulse(base.SyncRoot);
                    return base.Dequeue();
                }
                throw new ObjectDisposedException("The queue has already been disposed.");
            }
        }

        /// <summary>
        /// Dequeue the left datas after the queue destroyed.
        /// </summary>
        public object[] DequeueLeftElements()
        {
            lock (base.SyncRoot)
            {
                if (_existFlag != 0)
                {
                    throw new InvalidOperationException("Invalid operation. The queue has not been disposed.");
                }
                object[] leftDatas = new object[this.Count];
                if (this.Count > 0)
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        leftDatas[i] = base.Dequeue();
                    }
                }
                return leftDatas;
            }
        }

        /// <summary>
        /// Dequeue the left datas after the queue destroyed.
        /// </summary>
        public bool TryDequeueLeftElements(out object[] leftDatas)
        {
            leftDatas = null;
            lock (base.SyncRoot)
            {
                if (_existFlag != 0)
                {
                    return false;
                }
                leftDatas = new object[this.Count];
                if (this.Count > 0)
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        leftDatas[i] = base.Dequeue();
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// <para>Adds an element to the end of Queue.</para>
        /// <para>Chinese Simplified:添加元素到队列中。</para>
        /// </summary>
        public override void Enqueue(object obj)
        {
            lock (base.SyncRoot)
            {
                if (!Exists)
                {
                    throw new ObjectDisposedException("The queue has already been disposed.");
                }
                base.Enqueue(obj);
                Monitor.Pulse(base.SyncRoot);
            }
        }
        #endregion

        #region  Private Methods

        /// <summary>  
        /// <para>Remove all elements from Queue, resume any blocked dequeue threads.</para>
        /// <para>Chinese Simplified:销毁队列，同时恢复所有被阻挡的队列以及线程。</para>
        /// </summary>
        private void Destroy()
        {
            lock (base.SyncRoot)
            {
                // 配置存在状态标志为0
                Thread.VolatileWrite(ref _existFlag, 0);
                base.Clear();
                Monitor.PulseAll(base.SyncRoot);    // resume any waiting deque loops
            }
        }
        #endregion
    }
}



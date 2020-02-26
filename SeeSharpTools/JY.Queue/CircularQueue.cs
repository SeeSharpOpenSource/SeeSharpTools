using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using SeeSharpTools.JY.ThreadSafeQueue.Common;
using SeeSharpTools.JY.ThreadSafeQueue.Common.i18n;

namespace SeeSharpTools.JY.ThreadSafeQueue
{
    /// <summary>
    /// Thread safe circular data queue. String type is not supported.
    /// </summary>
    /// <typeparam name="TDataType">Type of data in queue. String type not supported.</typeparam>
    public class CircularQueue<TDataType> : IList<TDataType>, IDisposable
    {
        /// <summary>
        /// Create a new instance of CircularQueue
        /// </summary>
        /// <param name="capacity">The maximum count of data can be stored in queue. An overflow exception will be raised when the count of queue data exceeed this value</param>
        public CircularQueue(int capacity)
        {
            const int defaultCapacity = 1024;
            if (capacity <= 0) //输入的size无效，创建默认大小的缓冲区
            {
                capacity = defaultCapacity;
            }
            _capacity = capacity;
            _dataTypeSize = Marshal.SizeOf(typeof (TDataType));

            _dataBuffer = new TDataType[_capacity];
            _startIndex = 0;
            _endIndex = 0;
            _dataCount = 0;
            _queueLock = new SpinLock();
            _propertyReadTimeout = -1;
            _autoLock = AutoLockValue;
            BlockWait = true;
            _parallel = new ParallelHandler<TDataType>(_dataBuffer);
            
        }

        #region Private fields
        // Size of TDataType
        private readonly int _dataTypeSize;
        // Data buffer
        private readonly TDataType[] _dataBuffer;
        // Capacity of buffer
        private readonly int _capacity;
        // Queue start index
        private int _startIndex;
        // Queue end index
        private int _endIndex;
        // Data count in queue
        private int _dataCount;
        // Mutex for queue operation
        private SpinLock _queueLock;
        private int _propertyReadTimeout;
        private readonly I18nEntity i18n = I18nLocalWrapper.GetInstance(null);
        private readonly ParallelHandler<TDataType> _parallel;
        const int AutoLockValue = 1;
        private int _autoLock;
        // The thread that hold mutex. null means mutex free.
        private int _mutexThreadId;
        // Whether block thread if data count not available
        private bool _blockWaiting;
        // TODO 暂时取消入列阻塞事件
//        private WaitBlockEvent _enqueueEvent; 
        private SemaphoreSlim _dequeueWaitHandle;

        // 当前队列是否可用的标志位
        private int _availableFlag;

        #endregion

        #region Public property

        /// <summary>
        /// Property read timeout. Default value is -1(Infinity).
        /// </summary>
        public int PropertyReadTimeout {
            get
            {
                return _propertyReadTimeout;
            }
            set
            {
                _propertyReadTimeout = value >= -1 ? value : -1;
            }
        }

        /// <summary>
        /// The data count in queue
        /// </summary>
        public int Count => _dataCount;

        public bool IsReadOnly => false;

        /// <summary>
        /// The capacity of queue. An OverflowException will be raised when count greater than capacity.
        /// </summary>
        public int Capacity => _capacity;

        /// <summary>
        /// The avaiable capacity of queue.
        /// </summary>
        public int AvailableCapacity
        {
            get
            {
                return _capacity - _dataCount;
            }
        }

        /// <summary>
        /// Whether auto lock is enabled. When set to false, the lock should be maintained by user with methods: GetLock/TryGetLock/ReleaseLock. 
        /// </summary>
        public bool AutoLock
        {
            get
            {
                return _autoLock == AutoLockValue; 
            }
            set
            {
                GetMutex(_propertyReadTimeout);
                bool isLastAutoLock = (_autoLock == AutoLockValue);
                int autoLockValue = value ? AutoLockValue : 0;
                Thread.VolatileWrite(ref _autoLock, autoLockValue);
                // Mutex should be released as original ways
                if (isLastAutoLock)
                {
                    _queueLock.Exit();
                }
            }
        }

        /// <summary>
        /// 在数据不足或者容量不够的情况下是否阻塞操作线程执行等待。非线程安全
        /// </summary>
        public bool BlockWait
        {
            get { return _blockWaiting; }
            set
            {
                if (value == _blockWaiting)
                {
                    return;
                }
                _blockWaiting = value;
                if (_blockWaiting)
                {
                    _dequeueWaitHandle = new SemaphoreSlim(1);
                }
                else
                {
                    _dequeueWaitHandle?.Dispose();
                    _dequeueWaitHandle = null;
                }
            }
        }

        #endregion

        #region Enqueue Operation

        /// <summary>
        /// Enqueue a single data to queue. A TimtoutException will be raised when mutex acquization failed.
        /// </summary>
        /// <param name="data">Data to enqueue</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        public void Enqueue(TDataType data, int timeout = -1)
        {
            CheckCapacityAndGetMutex(1, timeout);
            _dataBuffer[_endIndex] = data;
            _endIndex = (_endIndex + 1)%_capacity;
            Thread.VolatileWrite(ref _dataCount, _dataCount + 1);
            ReleaseDequeueEventsAndMutex();
        }
        
        /// <summary>
        /// Enqueue a group of data from array to queue. A TimtoutException will be raised when mutex acquization failed.
        /// </summary>
        /// <param name="datas">Datas to enqueue</param>
        /// <param name="enqueueCount">The number of data to enqueue. Default value is 0(Enqueue all array data element).</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        public void Enqueue(TDataType[] datas, int enqueueCount = 0, int timeout = -1)
        {
            if (enqueueCount <= 0)
            {
                enqueueCount = datas.Length;
            }
            if (0 == enqueueCount)
            {
                return;
            }
            CheckInOutParams(datas, enqueueCount);
            CheckCapacityAndGetMutex(enqueueCount, timeout);
            EnqueueArrayData(datas, enqueueCount);
            ReleaseDequeueEventsAndMutex();
        }



        /// <summary>
        /// Enqueue a group of data from 2-dimentional array to queue. A TimtoutException will be raised when mutex acquization failed.
        /// </summary>
        /// <param name="datas">Datas to enqueue</param>
        /// <param name="enqueueCount">The number of data to enqueue. Default value is 0(Enqueue all array data element).</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        public void Enqueue(TDataType[,] datas, int enqueueCount = 0, int timeout = -1)
        {
            if (enqueueCount <= 0 || enqueueCount > datas.Length)
            {
                enqueueCount = datas.Length;
            }
            if (0 == enqueueCount)
            {
                return;
            }
            CheckCapacityAndGetMutex(enqueueCount, timeout);
            EnqueueArrayData(datas, enqueueCount);
            ReleaseDequeueEventsAndMutex();
        }

        /// <summary>
        /// Enqueue a group of data(Any type except string) from array to queue. A TimtoutException will be raised when mutex acquization failed.
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="enqueueCount">The number of data to enqueue. Default value is 0(Enqueue all array data element).</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        public void Enqueue(Array datas, int enqueueCount = 0, int timeout = -1)
        {
            if (enqueueCount <= 0 || enqueueCount > datas.Length)
            {
                enqueueCount = datas.Length;
            }
            int writeDataSize = Marshal.SizeOf(datas.GetValue(0))*enqueueCount;
            if (writeDataSize%_dataTypeSize != 0)
            {
                throw new ArgumentException(i18n.GetFStr("RunTime.InvalidDataSize", writeDataSize,
                    typeof (TDataType).Name));
            }
            enqueueCount = writeDataSize/_dataTypeSize;
            if (0 == enqueueCount)
            {
                return;
            }
            CheckCapacityAndGetMutex(enqueueCount, timeout);
            EnqueueArrayData(datas, enqueueCount);
            ReleaseDequeueEventsAndMutex();
        }

        /// <summary>
        /// Enqueue a group of data from IList collection to queue. A TimtoutException will be raised when mutex acquization failed.
        /// </summary>
        /// <param name="datas">Datas to enqueue</param>
        /// <param name="enqueueCount">The number of data to enqueue. Default value is 0(Enqueue all array data element).</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        public void Enqueue(IList<TDataType> datas, int enqueueCount = 0, int timeout = -1)
        {
            if (enqueueCount <= 0 || enqueueCount > datas.Count)
            {
                enqueueCount = datas.Count;
            }
            if (0 == enqueueCount)
            {
                return;
            }
            CheckCapacityAndGetMutex(enqueueCount, timeout);
            EnqueueListDatas(datas, enqueueCount);
            ReleaseDequeueEventsAndMutex();
        }

        private void CheckCapacityAndGetMutex(int enqueueCount, int timeout)
        {
            GetMutex(timeout);
            Thread.MemoryBarrier();
            if (_availableFlag == 0)
            {
                ReleaseMutex();
                throw new ObjectDisposedException("The target queue has already been disposed.");
            }
            if (CheckEnqueueCondition(_dataCount, enqueueCount))
            {
                return;
            }
            // 如果入列空间不足直接抛出异常
//            if (_blockWaiting)
//            {
//                _enqueueEvent.WaitOne(enqueueCount, timeout);
//            }

            int availableCapacity = _capacity - _dataCount;
            ReleaseMutex();
            throw new ArgumentOutOfRangeException(nameof(enqueueCount),i18n.GetFStr("ParamCheck.BufOverFlow", enqueueCount,
                    availableCapacity));
        }
        
        /// <summary>
        /// Enqueue a group of data from 2-dimentional array to queue. This method will return false when mutex acquization failed.
        /// </summary>
        /// <param name="data">Data to enqueue</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>True: Operation success. False: Mutex acquization failed.</returns>
        public bool TryEnqueue(TDataType data, int timeout = -1)
        {
            bool lockAcquiredAndCapacityEnough = TryCheckCapacityAndGetMutex(1, timeout);
            if (!lockAcquiredAndCapacityEnough)
            {
                return false;
            }
            _dataBuffer[_endIndex] = data;
            _endIndex = (_endIndex + 1) % _capacity;
            Thread.VolatileWrite(ref _dataCount, _dataCount + 1);
            ReleaseDequeueEventsAndMutex();
            return true;
        }

        /// <summary>
        /// Enqueue a group of data from array to queue. This method will return false when mutex acquization failed.
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="enqueueCount">The number of data to enqueue. Default value is 0(Enqueue all array data element).</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>True: Operation success. False: Mutex acquization failed.</returns>
        public bool TryEnqueue(TDataType[] datas, int enqueueCount = 0, int timeout = -1)
        {
            if (enqueueCount <= 0 || enqueueCount > datas.Length)
            {
                enqueueCount = datas.Length;
            }
            if (0 == enqueueCount)
            {
                return true;
            }
            if (!TryCheckCapacityAndGetMutex(enqueueCount, timeout))
            {
                return false;
            }
            EnqueueArrayData(datas, enqueueCount);
            ReleaseDequeueEventsAndMutex();
            return true;
        }

        /// <summary>
        /// Enqueue a group of data from 2-dimentional array to queue. This method will return false when mutex acquization failed.
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="enqueueCount">The number of data to enqueue. Default value is 0(Enqueue all array data element).</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>True: Operation success. False: Mutex acquization failed.</returns>
        public bool TryEnqueue(TDataType[,] datas, int enqueueCount = 0, int timeout = -1)
        {
            if (enqueueCount <= 0 || enqueueCount > datas.Length)
            {
                enqueueCount = datas.Length;
            }
            if (0 == enqueueCount)
            {
                return true;
            }
            if (!TryCheckCapacityAndGetMutex(enqueueCount, timeout))
            {
                return false;
            }
            EnqueueArrayData(datas, enqueueCount);
            ReleaseDequeueEventsAndMutex();
            return true;
        }

        /// <summary>
        /// Enqueue a group of data(Any type except string) from array to queue. This method will return false when mutex acquization failed.
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="enqueueCount">The number of data to enqueue. Default value is 0(Enqueue all array data element).</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>True: Operation success. False: Mutex acquization failed.</returns>
        public bool TryEnqueue(Array datas, int enqueueCount = 0, int timeout = -1)
        {
            if (enqueueCount <= 0 || enqueueCount > datas.Length)
            {
                enqueueCount = datas.Length;
            }
            int writeDataSize = Marshal.SizeOf(datas.GetValue(0)) * enqueueCount;
            if (writeDataSize % _dataTypeSize != 0)
            {
                throw new ArgumentException(i18n.GetFStr("RunTime.InvalidDataSize", writeDataSize,
                    typeof(TDataType).Name));
            }
            enqueueCount = writeDataSize / _dataTypeSize;
            if (0 == enqueueCount)
            {
                return true;
            }
            if (!TryCheckCapacityAndGetMutex(enqueueCount, timeout))
            {
                return false;
            }
            EnqueueArrayData(datas, enqueueCount);
            ReleaseDequeueEventsAndMutex();
            return true;
        }

        /// <summary>
        /// Enqueue a group of data from IList collection to queue. This method will return false when mutex acquization failed.
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="enqueueCount">The number of data to enqueue. Default value is 0(Enqueue all array data element).</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>True: Operation success. False: Mutex acquization failed.</returns>
        public bool TryEnqueue(IList<TDataType> datas, int enqueueCount = 0, int timeout = -1)
        {
            if (enqueueCount <= 0 || enqueueCount > datas.Count)
            {
                enqueueCount = datas.Count;
            }
            if (0 == enqueueCount)
            {
                return true;
            }
            if (!TryCheckCapacityAndGetMutex(enqueueCount, timeout))
            {
                return false;
            }
            EnqueueListDatas(datas, enqueueCount);
            ReleaseDequeueEventsAndMutex();
            return true;
        }

        private bool TryCheckCapacityAndGetMutex(int enqueueCount, int timeout)
        {
            if (_availableFlag == 0 || !TryGetMutex(timeout))
            {
                return false;
            }
            Thread.MemoryBarrier();
            if (_availableFlag == 0)
            {
                ReleaseMutex();
                return false;
            }
            Thread.MemoryBarrier();
            if (CheckEnqueueCondition(_dataCount, enqueueCount))
            {
                return true;
            }
            // 如果入列空间不足直接退出而不阻塞
//            if (_blockWaiting)
//            {
//                ReleaseMutex();
//                return _enqueueEvent.TryWaitOne(timeout) && TryGetMutex(timeout);
//            }
            int availableCapacity = _capacity - _dataCount;
            ReleaseMutex();
            throw new ArgumentOutOfRangeException(nameof(enqueueCount), i18n.GetFStr("ParamCheck.BufOverFlow", 1,
                    availableCapacity));
        }

        private void EnqueueArrayData(Array datas, int enqueueCount)
        {
            // 如果写入长度超过endIndex以后容量，分两次写入
            if (_endIndex + enqueueCount > _capacity)
            {
                int firstWriteCount = _capacity - _endIndex;
                int secondWriteCount = enqueueCount - firstWriteCount;
                Buffer.BlockCopy(datas, 0, _dataBuffer, _endIndex*_dataTypeSize, firstWriteCount*_dataTypeSize);
                Buffer.BlockCopy(datas, firstWriteCount*_dataTypeSize, _dataBuffer, 0, secondWriteCount*_dataTypeSize);
                _endIndex = secondWriteCount;
            }
            else
            {
                Buffer.BlockCopy(datas, 0, _dataBuffer, _endIndex*_dataTypeSize, enqueueCount*_dataTypeSize);
                _endIndex = (_endIndex + enqueueCount)%_capacity;
            }
            Thread.VolatileWrite(ref _dataCount, _dataCount + enqueueCount);
        }

        private void EnqueueListDatas(IList<TDataType> datas, int enqueueCount)
        {
            // 如果写入长度超过endIndex以后容量，分两次写入
            if (_endIndex + enqueueCount > _capacity)
            {
                int firstWriteCount = _capacity - _endIndex;
                int secondWriteCount = enqueueCount - firstWriteCount;
                _parallel.EnqueData(datas, _endIndex, 0, firstWriteCount);
                _parallel.EnqueData(datas, 0, firstWriteCount, secondWriteCount);
                _endIndex = secondWriteCount;
            }
            else
            {
                _parallel.EnqueData(datas, _endIndex, 0, enqueueCount);
                _endIndex = (_endIndex + enqueueCount)%_capacity;
            }
            Thread.VolatileWrite(ref _dataCount, _dataCount + enqueueCount);
        }

        private void ReleaseDequeueEventsAndMutex()
        {
            //释放第一个等待的出列线程
            _dequeueWaitHandle.Release();
            ReleaseMutex();
        }

        #endregion

        #region Dequeue Operation

        /// <summary>
        /// Dequeue single data. A TimtoutException will be raised when mutex acquization failed.
        /// </summary>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>The value of dequeued data.</returns>
        public TDataType Dequeue(int timeout = -1)
        {
            CheckDataCountAndGetMutex(1, timeout);
            TDataType dequeData = _dataBuffer[_startIndex];
            _startIndex = (++_startIndex)%_capacity;
            Thread.VolatileWrite(ref _dataCount, _dataCount - 1);
            ReleaseEnqueueEventsAndMutex();
            return dequeData;
        }

        /// <summary>
        /// Dequeue specified count of datas to array. A TimtoutException will be raised when mutex acquization failed.
        /// </summary>
        /// <param name="dequeueBuffer">Buffer that store dequeued data. Cannot be null.</param>
        /// <param name="dequeueCount">The number of data to dequeue. Default value is 0(dequeueCount set to the length of dequeueBuffer.</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>返回实际取到的数据长度</returns>
        public void Dequeue(TDataType[] dequeueBuffer, int dequeueCount = 0, int timeout = -1)
        {
            if (0 >= dequeueCount && null != dequeueBuffer)
            {
                dequeueCount = dequeueBuffer.Length;
            }
            CheckInOutParams(dequeueBuffer, dequeueCount);
            if (dequeueCount <= 0)
            {
                dequeueCount = dequeueBuffer.Length;
            }
            CheckDataCountAndGetMutex(dequeueCount, timeout);
            DequeueArrayData(dequeueBuffer, dequeueCount);
            ReleaseEnqueueEventsAndMutex();
        }

        /// <summary>
        /// Dequeue specified count of datas to array. A TimtoutException will be raised when mutex acquization failed.
        /// </summary>
        /// <param name="dequeueBuffer">Buffer that store dequeued data. Cannot be null.</param>
        /// <param name="dequeueCount">The number of data to dequeue. Default value is 0(dequeueCount set to the length of dequeueBuffer.</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>返回实际取到的数据长度</returns>
        public void Dequeue(TDataType[,] dequeueBuffer, int dequeueCount = 0, int timeout = -1)
        {
            if (0 >= dequeueCount && null != dequeueBuffer)
            {
                dequeueCount = dequeueBuffer.Length;
            }
            CheckInOutParams(dequeueBuffer, dequeueCount);
            if (dequeueCount <= 0)
            {
                dequeueCount = dequeueBuffer.Length;
            }
            CheckDataCountAndGetMutex(dequeueCount, timeout);
            DequeueArrayData(dequeueBuffer, dequeueCount);
            ReleaseEnqueueEventsAndMutex();
        }

        /// <summary>
        /// Dequeue specified count of data to IList collection. A TimtoutException will be raised when mutex acquization failed.
        /// </summary>
        /// <param name="dequeueBuffer">Buffer that store dequeued data. Cannot be null.</param>
        /// <param name="dequeueCount">The number of data to dequeue. This value should be greater than 0.</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        public void Dequeue(IList<TDataType> dequeueBuffer, int dequeueCount, int timeout = -1)
        {
            if (null == dequeueBuffer)
            {
                throw new ArgumentNullException(nameof(dequeueBuffer), i18n.GetFStr("ParamCheck.NullParameter",
                    nameof(dequeueBuffer)));
            }
            CheckDataCountAndGetMutex(dequeueCount, timeout);
            DequeueListData(dequeueBuffer, dequeueCount);
            ReleaseEnqueueEventsAndMutex();
        }

        /// <summary>
        /// Dequeue the left datas after the queue disposed.
        /// </summary>
        public TDataType[] DequeueLeftElements()
        {
            GetMutex(1000);
            if (_availableFlag != 0)
            {
                ReleaseMutex();
                throw new InvalidOperationException("Invalid operation. The queue has not been disposed.");
            }
            TDataType[] leftDatas = new TDataType[_dataCount];
            if (_dataCount > 0)
            {
                DequeueArrayData(leftDatas, _dataCount);
            }
            ReleaseMutex();
            return leftDatas;
        }

        /// <summary>
        /// Dequeue the left datas after the queue disposed.
        /// </summary>
        public bool TryDequeueLeftElements(out TDataType[] leftDatas)
        {
            leftDatas = null;
            if (!TryGetMutex(1000))
            {
                return false;
            }
            if (_availableFlag != 0)
            {
                ReleaseMutex();
                return false;
            }
            leftDatas = new TDataType[_dataCount];
            if (_dataCount > 0)
            {
                DequeueArrayData(leftDatas, _dataCount);
            }
            ReleaseMutex();
            return true;
        }

        // 校验当前数据长度是否足够出列，并获取锁
        private void CheckDataCountAndGetMutex(int dequeueCount, int timeout)
        {
            GetMutex(timeout);
            Thread.MemoryBarrier();
            if (_availableFlag == 0)
            {
                ReleaseMutex();
                throw new ObjectDisposedException("The target queue has already been disposed.");
            }
            if (DequeueCheckCondition(_dataCount, dequeueCount))
            {
                return;
            }
            if (_blockWaiting)
            {
                // 循环等待，直到数据足够
                do
                {
                    ReleaseMutex();
                    bool eventSet = _dequeueWaitHandle.Wait(timeout);
                    if (!eventSet)
                    {
                        throw new TimeoutException(i18n.GetStr("RunTime.Timeout"));
                    }
                    GetMutex(timeout);
                    Thread.MemoryBarrier();
                    if (_availableFlag == 0)
                    {
                        ReleaseMutex();
                        throw new ObjectDisposedException("The target queue has already been disposed.");
                    }
                } while (!DequeueCheckCondition(_dataCount, dequeueCount));
                // 如果有等待的线程则Release其线程，由该线程自行检查是否有足够的数据出列
                _dequeueWaitHandle.Release();
            }
            else
            {
                int dataCount = _dataCount;
                ReleaseMutex();
                throw new ArgumentException(i18n.GetFStr("ParamCheck.BufUnderFlow", dequeueCount, dataCount));
            }
        }

        /// <summary>
        /// Dequeue single data. This method will return false when mutex acquization failed.
        /// </summary>
        /// <param name="dequeueData">Buffer that store dequeued data.</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>True: Operation success. False: Mutex acquization failed.</returns>
        public bool TryDequeue(ref TDataType dequeueData, int timeout = -1)
        {
            if (!TryCheckDataCountAndGetMutex(1, timeout))
            {
                return false;
            }
            dequeueData = _dataBuffer[_startIndex];
            _startIndex = (++_startIndex)%_capacity;
            Thread.VolatileWrite(ref _dataCount, _dataCount - 1);
            ReleaseEnqueueEventsAndMutex();
            return true;
        }

        /// <summary>
        /// Dequeue specified count of datas to array. This method will return false when mutex acquization failed.
        /// </summary>
        /// <param name="dequeueBuffer">Buffer that store dequeued data. Cannot be null.</param>
        /// <param name="dequeueCount">The number of data to dequeue. Default value is 0(dequeueCount set to the length of dequeueBuffer.</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>True: Operation success. False: Mutex acquization failed.</returns>
        public bool TryDequeue(TDataType[] dequeueBuffer, int dequeueCount = 0, int timeout = -1)
        {
            if (_availableFlag == 0)
            {
                return false;
            }
            if (0 >= dequeueCount && null != dequeueBuffer)
            {
                dequeueCount = dequeueBuffer.Length;
            }
            CheckInOutParams(dequeueBuffer, dequeueCount);
            if (dequeueCount <= 0)
            {
                dequeueCount = dequeueBuffer.Length;
            }
            if (!TryCheckDataCountAndGetMutex(dequeueCount, timeout))
            {
                return false;
            }
            DequeueArrayData(dequeueBuffer, dequeueCount);
            ReleaseEnqueueEventsAndMutex();
            return true;
        }

        /// <summary>
        /// Dequeue specified count of datas to 2-dimentional array. This method will return false when mutex acquization failed.
        /// </summary>
        /// <param name="dequeueBuffer">Buffer that store dequeued data. Cannot be null.</param>
        /// <param name="dequeueCount">The number of data to dequeue. Default value is 0(dequeueCount set to the length of dequeueBuffer.</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>True: Operation success. False: Mutex acquization failed.</returns>
        public bool TryDequeue(TDataType[,] dequeueBuffer, int dequeueCount = 0, int timeout = -1)
        {
            if (_availableFlag == 0)
            {
                return false;
            }
            if (0 >= dequeueCount && null != dequeueBuffer)
            {
                dequeueCount = dequeueBuffer.Length;
            }
            CheckInOutParams(dequeueBuffer, dequeueCount);
            if (dequeueCount <= 0)
            {
                dequeueCount = dequeueBuffer.Length;
            }
            if (!TryCheckDataCountAndGetMutex(dequeueCount, timeout))
            {
                return false;
            }
            DequeueArrayData(dequeueBuffer, dequeueCount);
            ReleaseEnqueueEventsAndMutex();
            return true;
        }

        /// <summary>
        /// Dequeue specified count of data to IList Collection. This method will return false when mutex acquization failed.
        /// </summary>
        /// <param name="dequeueBuffer">Buffer that store dequeued data. Cannot be null.</param>
        /// <param name="dequeueCount">The number of data to dequeue. This value should be greater than 0.</param>
        /// <param name="timeout">Operation timeout in milliseconds. default value is -1(Infinity).</param>
        /// <returns>True: Operation success. False: Mutex acquization failed.</returns>
        public bool TryDequeue(IList<TDataType> dequeueBuffer, int dequeueCount, int timeout = -1)
        {
            if (_availableFlag == 0)
            {
                return false;
            }
            if (null == dequeueBuffer)
            {
                throw new ArgumentNullException(nameof(dequeueBuffer), i18n.GetFStr("ParamCheck.NullParameter",
                    nameof(dequeueBuffer)));
            }
            if (!TryCheckDataCountAndGetMutex(dequeueCount, timeout))
            {
                return false;
            }
            DequeueListData(dequeueBuffer, dequeueCount);
            ReleaseEnqueueEventsAndMutex();
            return true;
        }

        // 尝试校验数据并获取互斥权限，如果失败返回false
        private bool TryCheckDataCountAndGetMutex(int dequeueCount, int timeout)
        {
            if (_availableFlag == 0 || !DequeueCheckCondition(_dataCount, dequeueCount))
            {
                return false;
            }
            if (_blockWaiting)
            {
                do
                {
                    ReleaseMutex();
                    bool eventSet = _dequeueWaitHandle.Wait(timeout);
                    if (!eventSet)
                    {
                        return false;
                    }
                    bool getLock = TryGetMutex(timeout);
                    if (!getLock)
                    {
                        return false;
                    }
                    Thread.MemoryBarrier();
                    if (!getLock || _availableFlag == 0)
                    {
                        return false;
                    }
                } while (DequeueCheckCondition(_dataCount, dequeueCount));
                return true;
            }
            else
            {
                int dataCount = _dataCount;
                ReleaseMutex();
                throw new ArgumentException(i18n.GetFStr("ParamCheck.BufUnderFlow", dequeueCount, dataCount));
            }
        }

        private void DequeueArrayData(Array dequeBuffer, int dequeCount)
        {
            if (_startIndex + dequeCount > _capacity) //取数据的总大小超过了应该分两次拷贝，先拷贝尾部，剩余的从头开始拷贝
            {
                int firstTimeDequeCount = _capacity - _startIndex;
                int secondTimeDequeCount = dequeCount - firstTimeDequeCount;
                Buffer.BlockCopy(_dataBuffer, _startIndex*_dataTypeSize, dequeBuffer, 0,
                    firstTimeDequeCount*_dataTypeSize);
                Buffer.BlockCopy(_dataBuffer, 0, dequeBuffer, firstTimeDequeCount*_dataTypeSize,
                    secondTimeDequeCount*_dataTypeSize);
                _startIndex = secondTimeDequeCount;
            }
            else
            {
                Buffer.BlockCopy(_dataBuffer, _startIndex*_dataTypeSize, dequeBuffer, 0, dequeCount*_dataTypeSize);
                _startIndex = (_startIndex + dequeCount)%_capacity;
            }
            Thread.VolatileWrite(ref _dataCount, _dataCount - dequeCount);
        }

        private void DequeueListData(IList<TDataType> dequeBuffer, int dequeCount)
        {
            if (_startIndex + dequeCount > _capacity) //取数据的总大小超过了应该分两次拷贝，先拷贝尾部，剩余的从头开始拷贝
            {
                int secondTimeDequeCount = dequeCount - (_capacity - _startIndex);
                for (int index = _startIndex; index < _capacity; index++)
                {
                    dequeBuffer.Add(_dataBuffer[index]);
                }
                for (int index = 0; index < secondTimeDequeCount; index++)
                {
                    dequeBuffer.Add(_dataBuffer[index]);
                }
                _startIndex = secondTimeDequeCount;
            }
            else
            {
                for (int index = _startIndex; index < _startIndex + dequeCount; index++)
                {
                    dequeBuffer.Add(_dataBuffer[index]);
                }
                _startIndex = (_startIndex + dequeCount)%_capacity;
            }
            Thread.VolatileWrite(ref _dataCount, _dataCount - dequeCount); ;
        }

        private void CheckInOutParams(Array operationBuffer, int operationCount)
        {
            if (null == operationBuffer)
            {
                throw new ArgumentNullException(nameof(operationBuffer),
                    i18n.GetFStr("ParamCheck.NullParameter", nameof(operationBuffer)));
            }
            if (operationCount > _capacity)
            {
                throw new ArgumentException(nameof(operationCount), i18n.GetFStr("ParamCheck.InvalidInOutCount"));
            }
            if (operationBuffer.Length < operationCount)
            {
                throw new ArgumentOutOfRangeException(nameof(operationCount), i18n.GetStr("ParamCheck.InvalidDataBuf"));
            }
        }

        private void ReleaseEnqueueEventsAndMutex()
        {
            // 控件不足时不阻塞入列操作，所以不再释放入列等待队列
//            int dataCount = _dataCount;
//            int operationCount;
//            while (_enqueueEvent.StopWaitIfConditionSatisfied(dataCount, out operationCount))
//            {
//                dataCount += operationCount;
//            }
            ReleaseMutex();
        }

        #endregion

        #region Mutex Operation

        /// <summary>
        /// Get queue operation lock. Available when 'AutoLock' is false. TimeoutException will be raised  when lock not acquired in specified timeout.
        /// </summary>
        /// <param name="timeout">Lock operation timeout in milliseconds. 0: no wait; -1: always wait until lock acquired.</param>
        public void Enter(int timeout = -1)
        {
            if (_autoLock == AutoLockValue)
            {
                return;
            }
            bool getLock = false;
            _queueLock.TryEnter(timeout, ref getLock);
            if (!getLock)
            {
                throw new TimeoutException(i18n.GetStr("RunTime.Timeout"));
            }
            Interlocked.Exchange(ref _mutexThreadId, Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// Try get queue operation lock. Available when 'AutoLock' is false. Returns true when lock acquired during specified timeout, otherwise return false.
        /// </summary>
        /// <param name="timeout">Lock operation timeout in milliseconds. 0: no wait; -1: always wait until lock acquired.</param>
        /// <returns>True: Lock acuqired during timeout; False: Otherwise</returns>
        public bool TryEnter(int timeout = -1)
        {
            if (_autoLock == AutoLockValue)
            {
                return false;
            }
            bool getLock = false;
            _queueLock.TryEnter(timeout, ref getLock);
            if (getLock)
            {
                _mutexThreadId = Thread.CurrentThread.ManagedThreadId;
            }
            return getLock;
        }

        /// <summary>
        /// Release queue Lock. Available when 'AutoLock' is false. Recommanded to be called in finally block.
        /// </summary>
        public void Leave()
        {
            if (_autoLock != AutoLockValue && Thread.CurrentThread.ManagedThreadId == _mutexThreadId)
            {
                _queueLock.Exit();
                Interlocked.Exchange(ref _mutexThreadId, -1);
            }
        }

        private void GetMutex(int timeout)
        {
            if (_autoLock != AutoLockValue)
            {
                if (Thread.CurrentThread.ManagedThreadId != _mutexThreadId)
                {
                    throw new InvalidOperationException(i18n.GetStr("RunTime.MutexNotAcquired"));
                }
                return;
            }
            bool getLock = false;
            _queueLock.TryEnter(timeout, ref getLock);
            if (!getLock)
            {
                throw new TimeoutException(i18n.GetStr("RunTime.Timeout"));
            }
        }

        private bool TryGetMutex(int timeout)
        {
            if (_autoLock != AutoLockValue)
            {
                return Thread.CurrentThread.ManagedThreadId == _mutexThreadId;
            }
            bool getLock = false;
            _queueLock.TryEnter(timeout, ref getLock);
            return getLock;
        }

        private void ReleaseMutex()
        {
            if (_autoLock == AutoLockValue)
            {
                _queueLock.Exit();
            }
        }

        #endregion

        private bool CheckEnqueueCondition(int dataCount, int operationCount)
        {
            return (_capacity - dataCount) >= operationCount;
        }

        private bool DequeueCheckCondition(int dataCount, int operationCount)
        {
            return dataCount >= operationCount;
        }

        public void Dispose()
        {
            bool getLock = false;
            try
            {
                _queueLock.TryEnter(ref getLock);
                Thread.VolatileWrite(ref _availableFlag, 0);
                if (null != _dequeueWaitHandle)
                {
                    _dequeueWaitHandle.Release(_dequeueWaitHandle.CurrentCount);
                }
                Thread.MemoryBarrier();
                _dequeueWaitHandle?.Dispose();
            }
            finally
            {
                if (getLock)
                {
                    _queueLock.Exit();
                }
            }
        }

        #region IList interface

        public IEnumerator<TDataType> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TDataType item)
        {
            Enqueue(item);
        }

        /// <summary>
        /// 清空缓冲区内的数据
        /// </summary>
        public void Clear()
        {
            GetMutex(-1);
            _startIndex = 0;
            _endIndex = 0;
            const int maxReleaseCount = 100;
            _dequeueWaitHandle.Release(maxReleaseCount);
            Thread.VolatileWrite(ref _dataCount, 0);
            ReleaseMutex();
        }

        public bool Contains(TDataType item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TDataType[] array, int arrayIndex)
        {
            return;
        }

        public bool Remove(TDataType item)
        {
            return false;
        }

        public int IndexOf(TDataType item)
        {
            return -1;
        }

        public void Insert(int index, TDataType item)
        {
            return;
        }

        public void RemoveAt(int index)
        {
            return;
        }

        public TDataType this[int index]
        {
            get { return _dataBuffer[(index + _startIndex)%_capacity]; }
            set { _dataBuffer[(index + _startIndex)%_capacity] = value; }
        }

        #endregion

       
    }
}
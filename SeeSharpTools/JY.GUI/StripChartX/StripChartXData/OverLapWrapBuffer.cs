using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SeeSharpTools.JY.GUI.StripChartXData
{
    internal class OverLapWrapBuffer<TDataType> : IDisposable, IList<TDataType>
    {
        private readonly int _typeSize;
        private TDataType[] _dataBuf;
        public int DataSize { get; private set; }
        private int _startIndex;
        private int _endIndex;
        public int BufSize { get; }
        // 是否已经开始覆盖的标识位
        public bool IsOverLap { get; private set; }
        private DataEnumerator<TDataType> _enumerator; 

        public OverLapWrapBuffer(int bufSize)
        {
            this.BufSize = bufSize;
            this._dataBuf = new TDataType[bufSize];
            this.DataSize = 0;
            this._startIndex = 0;
            this._endIndex = 0;
            this.IsOverLap = false;
            this._typeSize = Marshal.SizeOf(typeof(TDataType));
            this._enumerator = new DataEnumerator<TDataType>(this);
        }

        public void Add(TDataType data)
        {
            DataSize++;
            _dataBuf[_endIndex++] = data;
            _endIndex %= BufSize;
            UpdateOverLapStatus();
        }

        public void Add(Array data, int dataLength, int offSet = 0)
        {
            int copySize = dataLength > BufSize ? BufSize : dataLength;
            DataSize = DataSize + copySize;
            if (_endIndex + copySize > BufSize)
            {
                int firstCopySize = BufSize - _endIndex;
                Buffer.BlockCopy(data, offSet * _typeSize, _dataBuf, _endIndex*_typeSize, 
                    firstCopySize*_typeSize);
                Buffer.BlockCopy(data, (firstCopySize + offSet) * _typeSize, _dataBuf, 0, 
                    (copySize - firstCopySize)*_typeSize);
            }
            else
            {
                Buffer.BlockCopy(data, offSet * _typeSize, _dataBuf, _endIndex * _typeSize, 
                    copySize * _typeSize);
            }
            _endIndex = (_endIndex + copySize)%BufSize;
            UpdateOverLapStatus();
        }
        
        public void ReadData(int readLength, ICollection<TDataType> buf)
        {
            if (readLength > DataSize)
            {
                readLength = DataSize;
            }
            while (readLength-- > 0)
            {
                buf.Add(Dequeue());
            }
        }

        public int RemoveTopData(int removeSize)
        {
            if (removeSize > DataSize)
            {
                removeSize = DataSize;
            }
            DataSize -= removeSize;
            _startIndex = (_startIndex + removeSize)%BufSize;
            UpdateOverLapStatus();
            return removeSize;
        }

        public TDataType Dequeue()
        {
            int index = _startIndex;
            _startIndex = (_startIndex + 1)%BufSize;
            DataSize--;
            return _dataBuf[index];
        }

        public int FetchData(int readLength, ICollection<TDataType> buf, int offSet = 0, int step = 1)
        {
            if (offSet > DataSize)
            {
                return 0;
            }
            int maxCount = (DataSize - offSet - 1)/step + 1;
            if (maxCount < readLength || readLength <= 0)
            {
                readLength = maxCount;
            }
            int readIndex = (_startIndex + offSet)%BufSize;
            while (readLength -- > 0)
            {
                buf.Add(_dataBuf[readIndex]);
                readIndex = (readIndex + step) % BufSize;
            }
            return readLength;
        }

        public void Clear()
        {
            _startIndex = 0;
            _endIndex = 0;
            DataSize = 0;
            IsOverLap = false;
        }

        public ReadOnlyBuf<TDataType> GetRange(int start, int count)
        {
            return new ReadOnlyBuf<TDataType>(this, start, count);
        }

        public bool Contains(TDataType item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TDataType[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TDataType item)
        {
            throw new NotImplementedException();
        }

        public int Count => DataSize;
        public bool IsReadOnly => false;

        public void Dispose()
        {
            _dataBuf = null;
        }

        private void UpdateOverLapStatus()
        {
            if (DataSize >= BufSize)
            {
                IsOverLap = true;
                _startIndex = _endIndex;
                DataSize = BufSize;
            }
            else
            {
                IsOverLap = false;
            }
        }

        public void GetMaxAndMin(out TDataType max, out TDataType min)
        {
            max = _dataBuf.Max();
            min = _dataBuf.Min();
        }

        public void SetBufFull()
        {
            IsOverLap = true;
            _endIndex = _startIndex;
            DataSize = BufSize;
        }

        #region 迭代器代码

        public IEnumerator<TDataType> GetEnumerator()
        {
//            for (int i = 0; i < DataSize; i++)
//            {
//                yield return _dataBuf[(_startIndex + i)%BufSize];
//            }
//            _enumerator.Reset();
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
//            _enumerator.Reset();
            return _enumerator;
        }


        #endregion


        public int IndexOf(TDataType item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, TDataType item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public TDataType this[int index]
        {
            get
            {
                int readIndex = (_startIndex + index) % BufSize;
                return _dataBuf[readIndex];
            }
            set
            {
                int readIndex = (_startIndex + index) % BufSize;
                _dataBuf[readIndex] = value;
            }
        }
    }

    internal class OverLapStrBuffer : IDisposable, IList<string>
    {
        private string[] _dataBuf;
        public int DataSize { get; private set; }
        private int _startIndex;
        private int _endIndex;
        public int BufSize { get; }
        // 是否已经开始覆盖的标识位
        public bool IsOverLap { get; private set; }
        private IEnumerator<string> _enumerator;

        public OverLapStrBuffer(int bufSize)
        {
            this.BufSize = bufSize;
            this._dataBuf = new string[bufSize];
            this.DataSize = 0;
            this._startIndex = 0;
            this._endIndex = 0;
            this.IsOverLap = false;
            this._enumerator = new DataEnumerator<string>(this);
        }

        public void Add(string data)
        {
            _dataBuf[_endIndex++] = data;
            DataSize++;
            _endIndex %= BufSize;
            UpdateOverLapStatus();
        }

        public void Add(IList<string> data, int dataLength, int offset = 0)
        {
            int copySize = dataLength > BufSize ? BufSize : dataLength;
            DataSize = DataSize + copySize;
            int index = 0;
            if (_endIndex + copySize > BufSize)
            {
                int firstCopySize = BufSize - _endIndex;
                while (index < firstCopySize)
                {
                    _dataBuf[index + _endIndex] = data[index + offset];
                    index++;
                }
                int writeIndex = 0;
                while (index < copySize)
                {
                    _dataBuf[writeIndex++] = data[offset + index++];
                }
            }
            else
            {
                while (index < copySize)
                {
                    _dataBuf[index + _endIndex] = data[index + offset];
                    index++;
                }
            }
            _endIndex = (_endIndex + copySize) % BufSize;
            UpdateOverLapStatus();
        }

        public string Get(int index)
        {
            int readIndex = (_startIndex + index) % BufSize;
            return _dataBuf[readIndex];
        }

        public void ReadData(int readLength, ICollection<string> buf)
        {
            if (readLength > DataSize)
            {
                readLength = DataSize;
            }
            while (readLength-- > 0)
            {
                buf.Add(Dequeue());
            }
        }

        public int RemoveTopData(int removeSize)
        {
            if (removeSize > DataSize)
            {
                removeSize = DataSize;
            }
            DataSize -= removeSize;
            _startIndex = (_startIndex + removeSize) % BufSize;
            UpdateOverLapStatus();
            return removeSize;
        }

        public string Dequeue()
        {
            int index = _startIndex;
            _startIndex = (_startIndex + 1) % BufSize;
            DataSize--;
            return _dataBuf[index];
        }

        public int FetchData(int readLength, ICollection<string> buf, int offSet = 0, int step = 1)
        {
            if (offSet > DataSize)
            {
                return 0;
            }
            int maxCount = (DataSize - offSet - 1) / step + 1;
            if (maxCount < readLength || readLength <= 0)
            {
                readLength = maxCount;
            }
            int readIndex = (_startIndex + offSet)% BufSize;
            while (readLength-- > 0)
            {
                buf.Add(_dataBuf[readIndex]);
                readIndex = (readIndex + step) % BufSize;
            }
            return readLength;
        }

        public void Clear()
        {
            _startIndex = 0;
            _endIndex = 0;
            DataSize = 0;
            IsOverLap = false;
        }

        public bool Contains(string item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string item)
        {
            throw new NotImplementedException();
        }

        public int Count => DataSize;
        public bool IsReadOnly => false;

        public void Dispose()
        {
            _dataBuf = null;
        }

        private void UpdateOverLapStatus()
        {
            if (DataSize >= BufSize)
            {
                IsOverLap = true;
                _startIndex = _endIndex;
                DataSize = BufSize;
            }
            else
            {
                IsOverLap = false;
            }
        }

        public void SetBufFull()
        {
            IsOverLap = true;
            _endIndex = _startIndex;
            DataSize = BufSize;
        }

        public IEnumerator<string> GetEnumerator()
        {
//            _enumerator.Reset();
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
//            _enumerator.Reset();
            return _enumerator;
        }

        public int IndexOf(string item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, string item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public string this[int index]
        {
            get
            {
                int readIndex = (_startIndex + index) % BufSize;
                return _dataBuf[readIndex];
            }
            set
            {
                int readIndex = (_startIndex + index) % BufSize;
                _dataBuf[readIndex] = value;
            }
        }
    }

    internal class DataEnumerator<TDataType> : IEnumerator<TDataType>
    {
        private IList<TDataType> _data;
        private int _pos;
        public DataEnumerator(IList<TDataType> data)
        {
            this._data = data;
            this._pos = -1;
        } 

        public bool MoveNext()
        {
            _pos++;
            return (_pos < _data.Count);
        }

        public void Reset()
        {
            _pos = -1;
        }

        public object Current {
            get
            {
                try
                {
                    return _data[_pos];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        TDataType IEnumerator<TDataType>.Current
        {
            get
            {
                try
                {
                    return _data[_pos];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {
            // ignore
        }
    }
}
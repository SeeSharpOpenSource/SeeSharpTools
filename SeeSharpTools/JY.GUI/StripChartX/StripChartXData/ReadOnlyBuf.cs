using System.Collections;
using System.Collections.Generic;

namespace SeeSharpTools.JY.GUI.StripChartXData
{
    internal class ReadOnlyBuf<TDataType> : IList<TDataType>
    {
        private readonly OverLapWrapBuffer<TDataType> _parentBuf;

        private readonly int _start;

        private readonly int _count;

        public ReadOnlyBuf(OverLapWrapBuffer<TDataType> parentBuf, int start, int count)
        {
            this._parentBuf = parentBuf;
            this._start = start;
            this._count = count;
        }


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
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(TDataType item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(TDataType[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(TDataType item)
        {
            throw new System.NotImplementedException();
        }

        public int Count => _count;
        public bool IsReadOnly => true;
        public int IndexOf(TDataType item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, TDataType item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public TDataType this[int index]
        {
            get { return _parentBuf[_start + index]; }
            set { throw new System.NotImplementedException(); }
        }
    }
}
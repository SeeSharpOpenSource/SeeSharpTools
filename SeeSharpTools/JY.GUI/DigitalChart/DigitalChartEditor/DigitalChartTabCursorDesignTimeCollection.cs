using System;
using System.Collections;

namespace SeeSharpTools.JY.GUI.DigitalChartEditor
{
    public class DigitalChartTabCursorDesignTimeCollection : IList
    {
        private readonly DigitalChartTabCursorCollection _collection;
        internal DigitalChartTabCursorDesignTimeCollection(DigitalChartTabCursorCollection collection)
        {
            this._collection = collection;
            this.SyncRoot = new object();
        }
        public IEnumerator GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => _collection.Count;
        public object SyncRoot { get; }
        public bool IsSynchronized => false;
        public int Add(object value)
        {
            _collection.Add(value as DigitalChartTabCursor);
            return _collection.Count - 1;
        }

        public bool Contains(object value)
        {
            return _collection.Contains(value as DigitalChartTabCursor);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public int IndexOf(object value)
        {
            return _collection.IndexOf(value as DigitalChartTabCursor);
        }

        public void Insert(int index, object value)
        {
            _collection.Insert(index, value as DigitalChartTabCursor);
        }

        public void Remove(object value)
        {
            _collection.Remove(value as DigitalChartTabCursor);
        }

        public void RemoveAt(int index)
        {
            _collection.RemoveAt(index);
        }

        public object this[int index]
        {
            get { return _collection[index]; }
            set { _collection[index] = value as DigitalChartTabCursor; }
        }

        public bool IsReadOnly => _collection.IsReadOnly;
        public bool IsFixedSize => false;
    }
}
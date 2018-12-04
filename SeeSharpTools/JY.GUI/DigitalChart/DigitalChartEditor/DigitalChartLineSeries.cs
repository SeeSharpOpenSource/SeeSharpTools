using System;
using System.Collections;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI.DigitalChartEditor
{
    public class DigitalChartLineSeries : BaseCollection, IList
    {
        private readonly DigitalChartSeriesCollection _seriesCollection;
        public DigitalChartLineSeries(DigitalChartSeriesCollection seriesCollection)
        {
            this._seriesCollection = seriesCollection;
            this.SyncRoot = _seriesCollection;
        }

        public IEnumerator GetEnumerator()
        {
            return _seriesCollection.GetEnumerator();
        }

        public int Count => _seriesCollection.Count;
        public object SyncRoot { get; }
        public bool IsSynchronized => false;
        public int Add(object value)
        {
            _seriesCollection.Add(value as DigitalChartSeries);
            return _seriesCollection.Count - 1;
        }

        public bool Contains(object value)
        {
            return _seriesCollection.Contains(value as DigitalChartSeries);
        }

        public void Clear()
        {
            _seriesCollection.Clear();
        }

        public int IndexOf(object value)
        {
            return _seriesCollection.IndexOf(value as DigitalChartSeries);
        }

        public void Insert(int index, object value)
        {
            _seriesCollection.Insert(index, value as DigitalChartSeries);
        }

        public void Remove(object value)
        {
            _seriesCollection.Remove(value as DigitalChartSeries);
        }

        public void RemoveAt(int index)
        {
            _seriesCollection.RemoveAt(index);
        }

        public object this[int index]
        {
            get { return _seriesCollection[index]; }
            set { _seriesCollection[index] = value as DigitalChartSeries; }
        }

        public bool IsReadOnly => false;
        public bool IsFixedSize => false;
    }
}
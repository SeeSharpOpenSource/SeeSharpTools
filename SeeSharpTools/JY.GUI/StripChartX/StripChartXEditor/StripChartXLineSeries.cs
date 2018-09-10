using System;
using System.Collections;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI.StripChartXEditor
{
    public class StripChartXLineSeries : BaseCollection, IList
    {
        private readonly StripChartXSeriesCollection _seriesCollection;
        public StripChartXLineSeries(StripChartXSeriesCollection seriesCollection)
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
            _seriesCollection.Add(value as StripChartXSeries);
            return _seriesCollection.Count - 1;
        }

        public bool Contains(object value)
        {
            return _seriesCollection.Contains(value as StripChartXSeries);
        }

        public void Clear()
        {
            _seriesCollection.Clear();
        }

        public int IndexOf(object value)
        {
            return _seriesCollection.IndexOf(value as StripChartXSeries);
        }

        public void Insert(int index, object value)
        {
            _seriesCollection.Insert(index, value as StripChartXSeries);
        }

        public void Remove(object value)
        {
            _seriesCollection.Remove(value as StripChartXSeries);
        }

        public void RemoveAt(int index)
        {
            _seriesCollection.RemoveAt(index);
        }

        public object this[int index]
        {
            get { return _seriesCollection[index]; }
            set { _seriesCollection[index] = value as StripChartXSeries; }
        }

        public bool IsReadOnly => false;
        public bool IsFixedSize => false;
    }
}
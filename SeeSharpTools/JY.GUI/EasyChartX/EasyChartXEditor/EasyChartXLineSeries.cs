using System;
using System.Collections;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI.EasyChartXEditor
{
    public class EasyChartXLineSeries : BaseCollection, IList
    {
        private readonly EasyChartXSeriesCollection _seriesCollection;
        public EasyChartXLineSeries(EasyChartXSeriesCollection seriesCollection)
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
            _seriesCollection.Add(value as EasyChartXSeries);
            return _seriesCollection.Count - 1;
        }

        public bool Contains(object value)
        {
            return _seriesCollection.Contains(value as EasyChartXSeries);
        }

        public void Clear()
        {
            _seriesCollection.Clear();
        }

        public int IndexOf(object value)
        {
            return _seriesCollection.IndexOf(value as EasyChartXSeries);
        }

        public void Insert(int index, object value)
        {
            _seriesCollection.Insert(index, value as EasyChartXSeries);
        }

        public void Remove(object value)
        {
            _seriesCollection.Remove(value as EasyChartXSeries);
        }

        public void RemoveAt(int index)
        {
            _seriesCollection.RemoveAt(index);
        }

        public object this[int index]
        {
            get { return _seriesCollection[index]; }
            set { _seriesCollection[index] = value as EasyChartXSeries; }
        }

        public bool IsReadOnly => false;
        public bool IsFixedSize => false;
    }
}
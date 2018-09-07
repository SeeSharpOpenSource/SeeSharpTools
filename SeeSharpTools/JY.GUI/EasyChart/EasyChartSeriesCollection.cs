using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
    public class EasyChartSeriesCollection : BaseCollection, IList
    {
        private readonly List<EasyChartSeries> _lineSeries;

        private readonly Color[] _seriesPalette = new Color[]
        {
            Color.Red, Color.Blue, Color.DeepPink, Color.Navy, Color.DarkGreen, Color.OrangeRed, Color.DarkCyan,
            Color.Black
        };

        const int MaxSeriesToDraw = 32;
        private readonly SeriesCollection _plotSeries;

        internal EasyChartSeriesCollection(SeriesCollection chartSeries)
        {
            this._plotSeries = chartSeries;
            _lineSeries = new List<EasyChartSeries>(MaxSeriesToDraw);
            // 默认添加n个Series
            foreach (Series plotSeries in _plotSeries)
            {
                EasyChartSeries series = new EasyChartSeries();
                series.AdaptBaseSeries(plotSeries);
                _lineSeries.Add(series);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _lineSeries.GetEnumerator();
        }

        public int Count => _lineSeries.Count;

        private const string SeriesNameFormat = "Series{0}";

        private bool _designEndFlag = false;
        public int Add(object value)
        {
            EasyChartSeries series = value as EasyChartSeries;

            if (null == series || _lineSeries.Count >= MaxSeriesToDraw)
            {
                return _lineSeries.Count - 1;
            }
            if (!_designEndFlag)
            {
                _lineSeries.Clear();
//                _designEndFlag = true;
            }
            AddInternal(series);
            return _lineSeries.Count - 1;
        }

        internal void AddInternal(EasyChartSeries series)
        {
            _designEndFlag = true;
            int index = _lineSeries.FindIndex(existItem => existItem.Name.Equals(series.Name));
            if (index >= 0 && index < _lineSeries.Count)
            {
                _lineSeries[index] = series;
                if (_plotSeries.Count > index)
                {
                    series.AdaptBaseSeries(_plotSeries[index]);
                }
            }
            else
            {
                // TODO 暂时封闭不做适配
                if (null == series.Name || "".Equals(series.Name))
                {
                    string seriesName = "";
                    int seriesIndex = _lineSeries.Count + 1;
                    do
                    {
                        seriesName = string.Format(SeriesNameFormat, seriesIndex++);
                    } while (_lineSeries.Any(existItem => existItem.Name.Equals(seriesName)));
                    series.Name = seriesName;
                    //                series.Color = _seriesPalette[_lineSeries.Count % _seriesPalette.Length];
                }
                //            series.SetSeriesCollecton(this);
                _lineSeries.Add(series);
                if (_plotSeries.Count >= _lineSeries.Count)
                {
                    series.AdaptBaseSeries(_plotSeries[_lineSeries.Count - 1]);
                }
            }
        }

        public bool Contains(object value)
        {
            EasyChartSeries series = value as EasyChartSeries;
            if (null != series)
            {
                return _lineSeries.Contains(series);
            }
            return false;
        }

        public void Clear()
        {
            _lineSeries.Clear();
        }

        public int IndexOf(object value)
        {
            EasyChartSeries series = value as EasyChartSeries;
            if (null != series)
            {
                return _lineSeries.IndexOf(series);
            }
            return -1;
        }

        public void Insert(int index, object value)
        {
            EasyChartSeries series = value as EasyChartSeries;
            if (null != series)
            {
                _lineSeries.Insert(index, series);
            }
        }

        public void Remove(object value)
        {
            EasyChartSeries series = value as EasyChartSeries;
            if (null != series)
            {
                _lineSeries.Remove(series);
            }
        }

        public void RemoveAt(int index)
        {
            _lineSeries.RemoveAt(index);
        }

        public object this[int index]
        {
            get { return _lineSeries[index]; }
            set
            {
                EasyChartSeries series = value as EasyChartSeries;
                if (null != series)
                {
                    _lineSeries[index] = series;
                }
            }
        }

        public bool IsReadOnly => false;
        public bool IsFixedSize => false;
    }
}
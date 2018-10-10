using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI
{
    public class StripChartXPlotAreaCollection : IList<StripChartXPlotArea>
    {
        private readonly StripChartX _parentChart;
        private readonly ChartAreaCollection _chartAreas;
        private readonly List<StripChartXPlotArea> _plotAreas = new List<StripChartXPlotArea>(Constants.DefaultMaxSeriesCount); 

        public StripChartXPlotAreaCollection(StripChartX parentChart, ChartAreaCollection chartAreas)
        {
            this._parentChart = parentChart;
            this._chartAreas = chartAreas;
        }

        public IEnumerator<StripChartXPlotArea> GetEnumerator()
        {
            return _plotAreas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(StripChartXPlotArea item)
        {
            // ignore
        }

        public void Clear()
        {
            // ignore
        }

        public bool Contains(StripChartXPlotArea item)
        {
            return _plotAreas.Contains(item);
        }

        public void CopyTo(StripChartXPlotArea[] array, int arrayIndex)
        {
            // ignore
        }

        public bool Remove(StripChartXPlotArea item)
        {
            // ignore
            return false;
        }

        public int Count => _plotAreas.Count;
        public bool IsReadOnly => true;
        public int IndexOf(StripChartXPlotArea item)
        {
            return _plotAreas.IndexOf(item);
        }

        public void Insert(int index, StripChartXPlotArea item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public StripChartXPlotArea this[int index]
        {
            get { return _plotAreas[index]; }
            set { return; }
        }

        internal void AdaptPlotAreaCount(int seriesCount)
        {
            while (seriesCount > _plotAreas.Count)
            {
                _plotAreas.Add(CreatePlotArea());
            }
            while (seriesCount < _plotAreas.Count)
            {
                int lastPlotArea = _plotAreas.Count - 1;
                _chartAreas.Remove(_plotAreas[lastPlotArea].ChartArea);
                _plotAreas.RemoveAt(lastPlotArea);
            }
        }

        private StripChartXPlotArea CreatePlotArea()
        {
            const string chartAreaNameFormat = "ChartArea{0}";
            // TODO to add more
            ChartArea baseChartArea = new ChartArea(string.Format(chartAreaNameFormat, _chartAreas.Count + 1));
            baseChartArea.AxisX.CustomLabels.Clear();
            baseChartArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            baseChartArea.AxisX.Enabled = AxisEnabled.True;

            baseChartArea.AxisX2.CustomLabels.Clear();
            baseChartArea.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

            baseChartArea.AxisY.CustomLabels.Clear();
            baseChartArea.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            baseChartArea.AxisY.Enabled = AxisEnabled.True;

            baseChartArea.AxisY2.CustomLabels.Clear();
            baseChartArea.AxisY2.IntervalAutoMode = IntervalAutoMode.VariableCount;

            _chartAreas.Add(baseChartArea);
            return new StripChartXPlotArea(_parentChart, baseChartArea);
        }

        internal int FindIndexByBaseChartArea(ChartArea baseArea)
        {
            return _plotAreas.FindIndex(item => ReferenceEquals(item.ChartArea, baseArea));
        }
    }
}
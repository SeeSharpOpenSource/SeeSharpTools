using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.EasyChartXUtility;

namespace SeeSharpTools.JY.GUI
{
    public class EasyChartXPlotAreaCollection : IList<EasyChartXPlotArea>
    {
        private readonly EasyChartX _baseEasyChart;
        private readonly ChartAreaCollection _chartAreas;
        private readonly List<EasyChartXPlotArea> _plotAreas = new List<EasyChartXPlotArea>(Constants.DefaultMaxSeriesCount); 

        public EasyChartXPlotAreaCollection(EasyChartX baseEasyChart, ChartAreaCollection chartAreas)
        {
            this._baseEasyChart = baseEasyChart;
            this._chartAreas = chartAreas;
        }

        public IEnumerator<EasyChartXPlotArea> GetEnumerator()
        {
            return _plotAreas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(EasyChartXPlotArea item)
        {
            // ignore
        }

        public void Clear()
        {
            // ignore
        }

        public bool Contains(EasyChartXPlotArea item)
        {
            return _plotAreas.Contains(item);
        }

        public void CopyTo(EasyChartXPlotArea[] array, int arrayIndex)
        {
            // ignore
        }

        public bool Remove(EasyChartXPlotArea item)
        {
            // ignore
            return false;
        }

        public int Count => _plotAreas.Count;
        public bool IsReadOnly => true;
        public int IndexOf(EasyChartXPlotArea item)
        {
            return _plotAreas.IndexOf(item);
        }

        public void Insert(int index, EasyChartXPlotArea item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public EasyChartXPlotArea this[int index]
        {
            get { return _plotAreas[index]; }
            set { return; }
        }

        internal void AdaptPlotAreaCount(int seriesCount)
        {
            while (seriesCount > _plotAreas.Count)
            {
                _plotAreas.Add(CreateBaseChartArea());
            }
            while (seriesCount < _plotAreas.Count)
            {
                int lastPlotArea = _plotAreas.Count - 1;
                _chartAreas.Remove(_plotAreas[lastPlotArea].ChartArea);
                _plotAreas.RemoveAt(lastPlotArea);
            }
        }

        private EasyChartXPlotArea CreateBaseChartArea()
        {
            const string chartAreaNameFormat = "ChartArea{0}";
            // TODO to add more
            ChartArea baseChartArea = new ChartArea(string.Format(chartAreaNameFormat, _chartAreas.Count + 1));
            baseChartArea.AxisX.CustomLabels.Clear();
            baseChartArea.AxisX.Enabled = AxisEnabled.True;
            baseChartArea.AxisX2.CustomLabels.Clear();
            baseChartArea.AxisY.CustomLabels.Clear();
            baseChartArea.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            baseChartArea.AxisY.Enabled = AxisEnabled.True;
            baseChartArea.AxisY2.CustomLabels.Clear();
            baseChartArea.AxisY2.IntervalAutoMode = IntervalAutoMode.VariableCount;
            _chartAreas.Add(baseChartArea);
            return new EasyChartXPlotArea(_baseEasyChart, baseChartArea);
        }

        internal int FindIndexByBaseChartArea(ChartArea baseArea)
        {
            return _plotAreas.FindIndex(item => ReferenceEquals(item.ChartArea, baseArea));
        }
    }
}
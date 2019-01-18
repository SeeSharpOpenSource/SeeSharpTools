using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.EasyChartXMarker.Painters;

namespace SeeSharpTools.JY.GUI.TabCursorUtility
{
    internal class DataMarkerManager
    {
        private readonly EasyChartX _parentChart;
        private readonly EasyChartXPlotArea _parentPlotArea;
        private readonly Chart _baseChart;
        private readonly PositionAdapter _adapter;

        private readonly List<MarkerPainter> _painters;
        private int _shownCount;

        private int _markerSize;

        public int MarkerSize
        {
            get
            {
                return _markerSize;
            }
            set
            {
                if (value <= 2 || value > 25)
                {
                    return;
                }
                if (0 == value % 2)
                {
                    value++;
                }
                this._markerSize = value;
            }
        }

        private bool _isShown;
        public bool IsShown
        {
            get { return _isShown; }
            set
            {
                if (value == _isShown)
                {
                    return;
                }
                this._isShown = value;
                RefreshChartPaintEvent();
            }
        }

        private bool _enabled;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (value == _enabled)
                {
                    return;
                }
                _enabled = value;
                RefreshChartPaintEvent();
            }
        }

        private void RefreshChartPaintEvent()
        {
            if (_enabled && _isShown)
            {
                this._baseChart.PostPaint += RefreshMarkers;
            }
            else
            {
                this._baseChart.PostPaint -= RefreshMarkers;
            }
        }

        public Color MarkerColor { get; set; }

        internal DataMarkerManager(EasyChartX parentChart, Chart baseChart, EasyChartXPlotArea parentPlotArea)
        {
            this._parentChart = parentChart;
            this._parentPlotArea = parentPlotArea;
            this._baseChart = baseChart;
            this._adapter = new PositionAdapter(baseChart, parentPlotArea);

            const int defaultMarkerSeries = 4;
            this._painters = new List<MarkerPainter>(defaultMarkerSeries);

            this._markerSize = 7;

            this._shownCount = 0;

            this._isShown = false;
            this._enabled = true;
            RefreshChartPaintEvent();
        }

        internal void Show(IList<double> xValue, IList<double> yValue, Color markerColor, DataMarkerType markerType, EasyChartXAxis.PlotAxis xAxis, EasyChartXAxis.PlotAxis yAxis)
        {
            if (null == xValue || null == yValue || 0 == xValue.Count || 0 == yValue.Count || xValue.Count != yValue.Count)
            {
                throw new ArgumentException("Invalid Marker data.");
            }
            this._shownCount++;
            if (_painters.Count < _shownCount || markerType != _painters[_shownCount - 1].Type)
            {
                MarkerPainter painter = MarkerPainter.CreatePainter(markerType, _adapter, markerColor, _baseChart.Controls);
                while (_painters.Count > _shownCount)
                {
                    _painters.RemoveAt(_painters.Count - 1);
                }
                _painters.Add(painter);
            }
            if (!_isShown)
            {
                _adapter.RefreshPosition();
            }
            int index = _shownCount - 1;
            _painters[index].Initialize(markerColor, MarkerSize, xAxis, yAxis, _parentPlotArea);
            _painters[index].InitializeMarkerControls(xValue, yValue);
            _painters[index].RefreshMarkerPosition();
            this.IsShown = true;
        }

        internal void Hide()
        {
            if (!_isShown)
            {
                return;
            }
            this.IsShown = false;
            this._shownCount = 0;
            foreach (MarkerPainter markerPainter in _painters)
            {
                markerPainter.Hide();
            }
        }

        internal void Clear()
        {
            this.IsShown = false;
            foreach (MarkerPainter markerPainter in _painters)
            {
                markerPainter.Clear();
            }
        }

        internal void RefreshMarkers(object sender, ChartPaintEventArgs args)
        {
            // 如果不是绘制Chart或者无需更新位置时将不执行重绘
            if (!(_isShown && _enabled) || !ReferenceEquals(args.ChartElement.GetType(), typeof(Chart)))
            {
                return;
            }
            bool markerRefreshed = false;
            for (int i = 0; i < _shownCount; i++)
            {
                markerRefreshed |= _painters[i].MoveAndShow();
            }
            // 如果marker被更新，则需要刷新X轴的Label(如果不更新会在一次缩放后不再更新X轴的Label，具体原因不明)
            if (markerRefreshed)
            {
                _parentPlotArea.AxisX.RefreshLabels();
                // 因为目前副X坐标轴被封闭，所以暂时清理副坐标轴
//                _parentPlotArea.AxisX2.RefreshLabels();
            }
        }
    }
}
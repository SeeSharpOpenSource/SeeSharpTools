using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SeeSharpTools.JY.GUI.EasyChartXUtility;
using SeeSharpTools.JY.GUI.TabCursorUtility;

namespace SeeSharpTools.JY.GUI.EasyChartXMarker.Painters
{
    /// <summary>
    /// 实现Marker的移动和内容渲染
    /// </summary>
    internal abstract class MarkerPainter : IDisposable
    {
        private readonly PositionAdapter _adapter;

        public DataMarkerType Type { get; }

        public Color Color { get; set; }

        protected readonly List<MarkerControl> Markers;

        protected int MarkerSize { get; private set; }

        public int DataCount { get; private set; }

        private readonly Control.ControlCollection _container;

        private EasyChartXPlotArea _parentPlotArea;

        private EasyChartXAxis.PlotAxis _xAxis;
        private EasyChartXAxis.PlotAxis _yAxis;

        #region 移动控件的参数

        private double _xAxisRatio;
        private double _yAxisRatio;
        private double _xOffset;
        private double _yOffset;
        private double _xMax;
        private double _yMax;
        private double _xMin;
        private double _yMin;

        #endregion


        protected MarkerPainter(DataMarkerType type, PositionAdapter adapter, Color color, Control.ControlCollection container)
        {
            const int defaultMarkerCount = 100;
            this.Type = type;
            this._adapter = adapter;
            this.Color = color;
            this.Markers = new List<MarkerControl>(defaultMarkerCount);
            this._container = container;
        }

        public abstract void PaintMarker(object sender, PaintEventArgs args);

        protected abstract void InitializePaintComponent(Color color);

        public void Initialize(Color color, int markerSize, EasyChartXAxis.PlotAxis xAxis, EasyChartXAxis.PlotAxis yAxis, 
            EasyChartXPlotArea plotArea)
        {
            this._xAxis = xAxis;
            this._yAxis = yAxis;
            this._parentPlotArea = plotArea;
            this.Color = color;
            this.MarkerSize = markerSize;
            InitializeMoveParameter();
            InitializePaintComponent(color);
        }

        public void InitializeMarkerControls(IList<double> xValues, IList<double> yValues)
        {
            DataCount = xValues.Count;
            while (Markers.Count < DataCount)
            {
                MarkerControl markerControl = new MarkerControl();
                markerControl.Paint += PaintMarker;
                markerControl.MouseEnter += ShowMarkerValue;
                markerControl.MouseLeave += HideMarkerValue;
                _container.Add(markerControl);
                Markers.Add(markerControl);
            }
            int removeIndex = Markers.Count - 1;
            while (Markers.Count > DataCount)
            {
                MarkerControl removedMarker = Markers[removeIndex];
                _container.Remove(removedMarker);
                removedMarker.Dispose();
                Markers.RemoveAt(removeIndex);
            }
            for (int i = 0; i < DataCount; i++)
            {
                if (Markers[i].Height != MarkerSize)
                {
                    Markers[i].Width = MarkerSize;
                    Markers[i].Height = MarkerSize;
                }
                Markers[i].XValue = xValues[i];
                Markers[i].YValue = yValues[i];
            }
        }
        
        public void Hide()
        {
            foreach (MarkerControl markerControl in Markers)
            {
                markerControl.Visible = false;
            }
        }

        // 移动和显示Marker，如果有更新则返回true，没有更新位置则返回false
        public bool MoveAndShow()
        {
            bool positionChanged = _adapter.RefreshPosition();
            bool viewRangeChanged = InitializeMoveParameter();
            // 如果图表位置和图像范围都没有变化，则无需更新位置
            if (!positionChanged && !viewRangeChanged)
            {
                return false;
            }
            RefreshMarkerPosition();
            return true;
        }

        public void RefreshMarkerPosition()
        {
            Point location = new Point();
            foreach (MarkerControl markerControl in Markers)
            {
                double xValue = markerControl.XValue;
                double yValue = markerControl.YValue;
                if (xValue > _xMax || xValue < _xMin || yValue > _yMax || yValue < _yMin)
                {
                    if (markerControl.Visible)
                    {
                        markerControl.Visible = false;
                    }
                    continue;
                }
                location.X = (int)Math.Round((xValue - _xMin) * _xAxisRatio + _xOffset);
                location.Y = (int)Math.Round((yValue - _yMax) * _yAxisRatio + _yOffset);
                if (location.X != markerControl.Location.X || location.Y != markerControl.Location.Y)
                {
                    markerControl.Location = location;
                }
                if (!markerControl.Visible)
                {
                    markerControl.Visible = true;
                }
            }
        }

        public void Clear()
        {
            foreach (MarkerControl markerControl in Markers)
            {
                _container.Remove(markerControl);
                markerControl.Dispose();
            }
            Markers.Clear();
        }

        public virtual void Dispose()
        {
            Clear();
        }

        private bool InitializeMoveParameter()
        {
            bool rangeChanged = false;
            EasyChartXAxis xAxis = (EasyChartXAxis.PlotAxis.Primary == _xAxis)
                ? _parentPlotArea.AxisX
                : _parentPlotArea.AxisX2;
            EasyChartXAxis yAxis = (EasyChartXAxis.PlotAxis.Primary == _yAxis)
                ? _parentPlotArea.AxisY
                : _parentPlotArea.AxisY2;
            if (Math.Abs(_xMax - xAxis.ViewMaximum) > Constants.MinDoubleValue || Math.Abs(_yMax - yAxis.ViewMaximum) > Constants.MinDoubleValue ||
                Math.Abs(_xMin - xAxis.ViewMinimum) > Constants.MinDoubleValue || Math.Abs(_yMin - yAxis.ViewMinimum) > Constants.MinDoubleValue)
            {
                _xMax = xAxis.ViewMaximum;
                _yMax = yAxis.ViewMaximum;
                _xMin = xAxis.ViewMinimum;
                _yMin = yAxis.ViewMinimum;
                rangeChanged = true;
            }
            _xAxisRatio = _adapter.PlotRealWidth/(_xMax - _xMin);
            _yAxisRatio = _adapter.PlotRealHeight/(_yMin - _yMax);

            int sizeOffset = (MarkerSize - 1)/2;
            _xOffset = _adapter.PlotRealX - sizeOffset;
            _yOffset = _adapter.PlotRealY - sizeOffset;
            return rangeChanged;
        }

        public static MarkerPainter CreatePainter(DataMarkerType type, PositionAdapter adapter, Color markerColor, 
            Control.ControlCollection containers)
        {
            switch (type)
            {
                case DataMarkerType.Square:
                    return new SquareMarkerPainter(adapter, markerColor, containers);
                    break;
                case DataMarkerType.Circle:
                    return new CircleMarkerPainter(adapter, markerColor, containers);
                    break;
                case DataMarkerType.Diamond:
                    return new DiamondMarkerPainter(adapter, markerColor, containers);
                    break;
                case DataMarkerType.Triangle:
                    return new TriangleMarkerPainter(adapter, markerColor, containers);
                    break;
                case DataMarkerType.Cross:
                    return new CrossMarkerPainter(adapter, markerColor, containers);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void ShowMarkerValue(object sender, EventArgs eventArgs)
        {
            MarkerControl markerControl = sender as MarkerControl;
            EasyChartX parentChart = markerControl.Parent.Parent as EasyChartX;
            Point tipLocation = markerControl.Location;
            tipLocation.X += MarkerSize;
            tipLocation.Y += MarkerSize;
            const string markerValueFormat = "X:{0}{1}Y:{2}";
            parentChart.ShowMarkerValue(string.Format(markerValueFormat, markerControl.XValue, 
                Environment.NewLine, markerControl.YValue), tipLocation, true);
        }

        private void HideMarkerValue(object sender, EventArgs eventArgs)
        {
            MarkerControl markerControl = sender as MarkerControl;
            EasyChartX parentChart = markerControl.Parent.Parent as EasyChartX;
            parentChart.ShowMarkerValue(string.Format(string.Empty), Point.Empty, false);
        }
    }
}
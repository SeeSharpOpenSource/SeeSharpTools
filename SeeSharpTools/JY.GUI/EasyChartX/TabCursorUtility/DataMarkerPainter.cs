using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI.TabCursorUtility
{
    internal class DataMarkerPainter
    {
        private readonly EasyChartX _parentChart;
        private readonly EasyChartXPlotArea _parentPlotArea;
        private readonly Chart _baseChart;
        private readonly PositionAdapter _adapter;

        private readonly List<IList<double>> _xValues;
        private readonly List<IList<double>> _yValues;

        private readonly List<DataMarkerType> _markerTypes;
        private readonly List<Pen> _markerPens;
        private readonly List<EasyChartXAxis.PlotAxis> _xAxis;
        private readonly List<EasyChartXAxis.PlotAxis> _yAxis;

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
                if (0 == value%1)
                {
                    value--;
                }
                this._markerSize = value;
            }
        }

        private bool _isShown;
        public bool IsShown
        {
            get { return _isShown;}
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

        internal DataMarkerPainter(EasyChartX parentChart, Chart baseChart, EasyChartXPlotArea parentPlotArea)
        {
            this._parentChart = parentChart;
            this._parentPlotArea = parentPlotArea;
            this._baseChart = baseChart;
            this._adapter = new PositionAdapter(baseChart, parentPlotArea);

            const int defaultMarkerSeries = 4;
            this._xValues = new List<IList<double>>(defaultMarkerSeries);
            this._yValues = new List<IList<double>>(defaultMarkerSeries);
            this._markerTypes = new List<DataMarkerType>(defaultMarkerSeries);
            this._markerPens = new List<Pen>(defaultMarkerSeries);
            this._xAxis = new List<EasyChartXAxis.PlotAxis>(defaultMarkerSeries);
            this._yAxis = new List<EasyChartXAxis.PlotAxis>(defaultMarkerSeries);

            this._markerSize = 5;

            this._isShown = false;
            this._enabled = true;
            RefreshChartPaintEvent();
        }

        internal void Show(IList<double> xValue, IList<double> yValue, Color markerColor, DataMarkerType markerType, EasyChartXAxis.PlotAxis xAxis, EasyChartXAxis.PlotAxis yAxis)
        {
            const int markerPenWidth = 1;
            if (null == xValue || null == yValue || 0 == xValue.Count || 0 == yValue.Count || xValue.Count != yValue.Count)
            {
                throw new ArgumentException("Invalid Marker data.");
            }
            this._xValues.Add(xValue);
            this._yValues.Add(yValue);
            this._markerPens.Add(new Pen(markerColor, markerPenWidth));
            this._markerTypes.Add(markerType);
            this._xAxis.Add(xAxis);
            this._yAxis.Add(yAxis);
            this.IsShown = true;
        }

        internal void Hide()
        {
            if (!_isShown)
            {
                return;
            }
            this.IsShown = false;
            this._xValues.Clear();
            this._yValues.Clear();
            this._markerTypes.Clear();
            this._markerPens.Clear();
            this._xAxis.Clear();
            this._yAxis.Clear();
        }

        internal void RefreshMarkers(object sender, ChartPaintEventArgs args)
        {
            // 如果不是绘制Chart或者无需更新位置时将不执行重绘
            if (!(_isShown && _enabled) || !ReferenceEquals(args.ChartElement.GetType(), typeof(Chart)))
            {
                return;
            }
            this._adapter.RefreshPosition();
            Graphics graphics = args.ChartGraphics.Graphics;
            for (int i = 0; i < _xValues.Count; i++)
            {
                switch (_markerTypes[i])
                {
                    case DataMarkerType.Square:
                        DrawSquareMarker(graphics, i);
                        break;
                    case DataMarkerType.Circle:
                        DrawCircleMarker(graphics, i);
                        break;
                    case DataMarkerType.Diamond:
                        DrawDiamondMarker(graphics, i);
                        break;
                    case DataMarkerType.Triangle:
                        DrawTriangleMarker(graphics, i);
                        break;
                    case DataMarkerType.Cross:
                        DrawCrossMarker(graphics, i);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void DrawSquareMarker(Graphics graphics, int index)
        {
            double xMax, yMax, xMin, yMin;
            GetAxisRange(index, out xMax, out yMax, out xMin, out yMin);

            double xAxisRatio = _adapter.PlotRealWidth/(xMax - xMin);
            double yAxisRatio = _adapter.PlotRealHeight/(yMin - yMax);

            double xOffset = _adapter.PlotRealX;
            double yOffset = _adapter.PlotRealY;

            const int borderOffset = 2;
            Pen pen = _markerPens[index];
            Rectangle rectangle = new Rectangle(0, 0, _markerSize, _markerSize);
            for (int i = 0; i < _xValues[index].Count; i++)
            {
                rectangle.X = (int) ((_xValues[index][i] - xMin)*xAxisRatio + xOffset) - borderOffset;
                rectangle.Y = (int) ((_yValues[index][i] - yMax)*yAxisRatio + yOffset) - borderOffset;
//                graphics.DrawRectangle(pen, rectangle);
                graphics.FillRectangle(new SolidBrush(pen.Color), rectangle);
            }
        }

        private void DrawCircleMarker(Graphics graphics, int index)
        {
            double xMax, yMax, xMin, yMin;
            GetAxisRange(index, out xMax, out yMax, out xMin, out yMin);

            double xAxisRatio = _adapter.PlotRealWidth / (xMax - xMin);
            double yAxisRatio = _adapter.PlotRealHeight / (yMin - yMax);

            double xOffset = _adapter.PlotRealX;
            double yOffset = _adapter.PlotRealY;

            const int circleWidth = 3;
            const int borderOffset = 1;
            Pen pen = _markerPens[index];
            Rectangle rectangle = new Rectangle(0, 0, circleWidth, circleWidth);
            for (int i = 0; i < _xValues[index].Count; i++)
            {
                rectangle.X = (int)((_xValues[index][i] - xMin) * xAxisRatio + xOffset) - borderOffset;
                rectangle.Y = (int)((_yValues[index][i] - yMax) * yAxisRatio + yOffset) - borderOffset;
                graphics.DrawEllipse(pen, rectangle);
            }
            throw new NotImplementedException();
        }

        private void DrawDiamondMarker(Graphics graphics, int index)
        {
            double xMax, yMax, xMin, yMin;
            GetAxisRange(index, out xMax, out yMax, out xMin, out yMin);

            double xAxisRatio = _adapter.PlotRealWidth / (xMax - xMin);
            double yAxisRatio = _adapter.PlotRealHeight / (yMin - yMax);

            double xOffset = _adapter.PlotRealX;
            double yOffset = _adapter.PlotRealY;

            const int squareWidth = 3;
            const int borderOffset = 1;
            Pen pen = _markerPens[index];
            Rectangle rectangle = new Rectangle(0, 0, squareWidth, squareWidth);
            for (int i = 0; i < _xValues[index].Count; i++)
            {
                rectangle.X = (int)((_xValues[index][i] - xMin) * xAxisRatio + xOffset) - borderOffset;
                rectangle.Y = (int)((_yValues[index][i] - yMax) * yAxisRatio + yOffset) - borderOffset;
                graphics.DrawEllipse(pen, rectangle);
            }
            throw new NotImplementedException();
        }

        private void DrawTriangleMarker(Graphics graphics, int index)
        {
            double xMax, yMax, xMin, yMin;
            GetAxisRange(index, out xMax, out yMax, out xMin, out yMin);

            double xAxisRatio = _adapter.PlotRealWidth / (xMax - xMin);
            double yAxisRatio = _adapter.PlotRealHeight / (yMin - yMax);

            double xOffset = _adapter.PlotRealX;
            double yOffset = _adapter.PlotRealY;

            const int squareWidth = 3;
            const int borderOffset = 1;
            Pen pen = _markerPens[index];
            Rectangle rectangle = new Rectangle(0, 0, squareWidth, squareWidth);
            for (int i = 0; i < _xValues[index].Count; i++)
            {
                rectangle.X = (int)((_xValues[index][i] - xMin) * xAxisRatio + xOffset) - borderOffset;
                rectangle.Y = (int)((_yValues[index][i] - yMax) * yAxisRatio + yOffset) - borderOffset;
                graphics.DrawRectangle(pen, rectangle);
            }
            throw new NotImplementedException();
        }

        private void DrawCrossMarker(Graphics graphics, int index)
        {
            double xMax, yMax, xMin, yMin;
            GetAxisRange(index, out xMax, out yMax, out xMin, out yMin);

            double xAxisRatio = _adapter.PlotRealWidth / (xMax - xMin);
            double yAxisRatio = _adapter.PlotRealHeight / (yMin - yMax);

            double xOffset = _adapter.PlotRealX;
            double yOffset = _adapter.PlotRealY;

            const int squareWidth = 3;
            const int borderOffset = 1;
            Pen pen = _markerPens[index];
            Rectangle rectangle = new Rectangle(0, 0, squareWidth, squareWidth);
            for (int i = 0; i < _xValues[index].Count; i++)
            {
                rectangle.X = (int)Math.Round((_xValues[index][i] - xMin) * xAxisRatio + xOffset) - borderOffset;
                rectangle.Y = (int)Math.Round((_yValues[index][i] - yMax) * yAxisRatio + yOffset) - borderOffset;
                graphics.DrawRectangle(pen, rectangle);
            }
            throw new NotImplementedException();
        }

        private void GetAxisRange(int index, out double xMax, out double yMax, out double xMin, out double yMin)
        {
            EasyChartXAxis xAxis = (EasyChartXAxis.PlotAxis.Primary == _xAxis[index])
                ? _parentPlotArea.AxisX
                : _parentPlotArea.AxisX2;
            EasyChartXAxis yAxis = (EasyChartXAxis.PlotAxis.Primary == _yAxis[index])
                ? _parentPlotArea.AxisY
                : _parentPlotArea.AxisY2;
            xMax = xAxis.ViewMaximum;
            yMax = yAxis.ViewMaximum;
            xMin = xAxis.ViewMinimum;
            yMin = yAxis.ViewMinimum;
        }
    }
}
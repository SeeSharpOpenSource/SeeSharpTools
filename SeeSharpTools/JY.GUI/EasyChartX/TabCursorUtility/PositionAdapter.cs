using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.EasyChartXUtility;

namespace SeeSharpTools.JY.GUI.TabCursorUtility
{
    internal class PositionAdapter
    {
        private readonly Chart _baseChart;
        private readonly EasyChartXPlotArea _chartArea;

        private Size _chartSize;
        private readonly ElementPosition _areaPosition;
        private readonly ElementPosition _plotPosition;

        /// <summary>
        /// 绘图区真实的起始X起点
        /// </summary>
        public double PlotRealX { get; private set; }
        /// <summary>
        /// 绘图区真实的起始Y起点
        /// </summary>
        public double PlotRealY { get; private set; }
        /// <summary>
        /// 绘图区真实的起始宽度
        /// </summary>
        public double PlotRealWidth { get; private set; }
        /// <summary>
        /// 绘图区真实的起始高度
        /// </summary>
        public int PlotRealHeight { get; private set; }
        
        private double _maxX;
        private double _minX;
        
        public PositionAdapter(Chart baseChart, EasyChartXPlotArea plotArea)
        {
            this._baseChart = baseChart;
            this._chartArea = plotArea;
            ChartArea chartArea = plotArea.ChartArea;
            this._chartSize = new Size(_baseChart.Width, _baseChart.Height);
            this._areaPosition = new ElementPosition(chartArea.Position.X, chartArea.Position.Y,
                chartArea.Position.Width, chartArea.Position.Height);
            this._plotPosition = new ElementPosition(chartArea.InnerPlotPosition.X, chartArea.InnerPlotPosition.Y,
                chartArea.InnerPlotPosition.Width, chartArea.InnerPlotPosition.Height);
            this._maxX = chartArea.AxisX.ScaleView.ViewMaximum;
            this._minX = chartArea.AxisX.ScaleView.ViewMinimum;

            CalculatePointPosition();
        }

        /// <summary>
        /// 更新每个元素的位置信息，如果完全相同无需更新则返回false
        /// </summary>
        /// <returns>是否需要更新cursor</returns>
        public bool RefreshPosition()
        {
            ChartArea chartArea = this._chartArea.ChartArea;
            Axis axisX = chartArea.AxisX;
            double viewMaxX = axisX.ScaleView.ViewMaximum;
            if (double.IsNaN(viewMaxX) || axisX.Maximum < viewMaxX)
            {
                viewMaxX = axisX.Maximum;
            }
            double viewMinX = axisX.ScaleView.ViewMinimum;
            if (double.IsNaN(viewMinX) || axisX.Minimum > viewMinX)
            {
                viewMinX = axisX.Minimum;
            }

            if (Math.Abs(_maxX - viewMaxX) < Constants.MinFloatDiff && Math.Abs(_minX - viewMinX) < Constants.MinFloatDiff &&
                Math.Abs(_areaPosition.X - chartArea.Position.X) < Constants.MinFloatDiff &&
                Math.Abs(_areaPosition.Y - chartArea.Position.Y) < Constants.MinFloatDiff &&
                Math.Abs(_areaPosition.Width - chartArea.Position.Width) < Constants.MinFloatDiff &&
                Math.Abs(_areaPosition.Height - chartArea.Position.Height) < Constants.MinFloatDiff &&
                Math.Abs(_plotPosition.X - chartArea.InnerPlotPosition.X) < Constants.MinFloatDiff &&
                Math.Abs(_plotPosition.Y - chartArea.InnerPlotPosition.Y) < Constants.MinFloatDiff &&
                Math.Abs(_plotPosition.Width - chartArea.InnerPlotPosition.Width) < Constants.MinFloatDiff &&
                Math.Abs(_plotPosition.Height - chartArea.InnerPlotPosition.Height) < Constants.MinFloatDiff &&
                Math.Abs(_chartSize.Width - _baseChart.Width) < Constants.MinFloatDiff &&
                Math.Abs(_chartSize.Height - _baseChart.Height) < Constants.MinFloatDiff)
            {
                return false;
            }
            _maxX = viewMaxX;
            _minX = viewMinX;
            _areaPosition.X = chartArea.Position.X;
            _areaPosition.Y = chartArea.Position.Y;
            _areaPosition.Width = chartArea.Position.Width;
            _areaPosition.Height = chartArea.Position.Height;
            _plotPosition.X = chartArea.InnerPlotPosition.X;
            _plotPosition.Y = chartArea.InnerPlotPosition.Y;
            _plotPosition.Width = chartArea.InnerPlotPosition.Width;
            _plotPosition.Height = chartArea.InnerPlotPosition.Height;
            _chartSize.Width = _baseChart.Width;
            _chartSize.Height = _baseChart.Height;
            CalculatePointPosition();
            return true;
        }
        
        private void CalculatePointPosition()
        {
            //根据InnerLocation计算的绘图区的宽度有一定的误差，需要通过参数修正
            const int plotAreaWidthOffset = -1;
            float chartAreaWidth = _areaPosition.Width* _chartSize.Width/100;
            float chartAreaHeight = _areaPosition.Height* _chartSize.Height/100;
            PlotRealX = Math.Round((_areaPosition.X * _chartSize.Width + _plotPosition.X * chartAreaWidth)/100);
            PlotRealY = Math.Round(_areaPosition.Y*_chartSize.Height + _plotPosition.Y*chartAreaHeight)/100;
            PlotRealWidth = Math.Round(chartAreaWidth * _plotPosition.Width/100) + plotAreaWidthOffset;
            PlotRealHeight = (int) Math.Round(chartAreaHeight*_plotPosition.Height/100);
        }

        public void MoveCursorToTarget(TabCursor cursor)
        {
            double cursorPosition = (cursor.Value - _minX)/(_maxX - _minX);
            if (cursorPosition < 0 || double.IsNaN(cursorPosition))
            {
                cursorPosition = 0;
            }
            else if (cursorPosition > 1)
            {
                cursorPosition = 1;
            }

            // 减去控件宽度的一半保证游标控件中间对齐到位置
            int cursorXPosition = (int) Math.Round(PlotRealX + cursorPosition*PlotRealWidth) - (cursor.Control.Width - 1)/2;
            int cursorYPosition = (int)PlotRealY;
            if (cursor.Control.Location.X != cursorXPosition || cursor.Control.Location.Y != cursorYPosition)
            {
                cursor.Control.Location = new Point(cursorXPosition, cursorYPosition);
            }
            if (PlotRealHeight != cursor.Control.Height)
            {
                cursor.Control.Height = PlotRealHeight;
            }
        }

        public void RefreshCursorValue(TabCursor cursor)
        {
            // 游标真实位置需要加上cursor视图和控件本身像素差的偏移
            int cursorPosition = cursor.Control.Location.X + TabCursorControl.ViewPixelOffset;
            double valueRatio = (cursorPosition - PlotRealX)/PlotRealWidth;
            double value = (_maxX - _minX)*valueRatio+_minX;
            if (value > _maxX)
            {
                value = _maxX;
            }
            else if (value < _minX)
            {
                value = _minX;
            }
            cursor.Value = value;
        }
    }
}
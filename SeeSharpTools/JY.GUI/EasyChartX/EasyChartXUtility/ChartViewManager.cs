using System;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI.EasyChartXUtility
{
    internal class ChartViewManager
    {
        internal EasyChartXPlotArea MainPlotArea { get;}
        internal bool UseMainAreaConfig { get; set; }
        internal EasyChartXPlotAreaCollection SplitPlotAreas { get; }

//        private double _maxXRange;
//        private double _minXRange;
//        public double MinXInterval { get; set; }
//        public double MinYInterval { get; set; }

        private bool _isSplitView;
        internal bool IsSplitView
        {
            get
            {
                return _isSplitView;
            }
            set
            {
                if (value != _isSplitView)
                {
                    _isSplitView = value;
                    _parentChart.AdaptPlotSeriesAndChartView();
                    if (_isSplitView)
                    {
                        if (UseMainAreaConfig)
                        {
                            ApplyMainPlotAreaToAll();
                        }
                        // 切换到分区时候时所有的曲线都显示
                        foreach (Series plotSeries in _plotManager.PlotSeries.Where(item => !item.Enabled))
                        {
                            plotSeries.Enabled = true;
                        }
                    }
                    // 更新所有线条的坐标轴(分区视图时都在主坐标轴)
                    _plotManager.Series.RefreshPlotAxis(null);
                    if (_parentChart.IsPlotting())
                    {
                        RefreshAxesAndCursors();
                        _parentChart.PlotDataInRange();
                    }
                    else
                    {
                        // 未绘图时不执行该方法会导致Chart在重绘时抛出轴范围错误的异常
                        Clear();
                    }
                }
            }
        }

        private bool _autoLayout = true;
        public bool AutoLayout
        {
            get
            {
                return _autoLayout;
            }
            set
            {
                if (value != _autoLayout)
                {
                    _autoLayout = value;
                    AdaptPlotAreas();
                }
            }
        }

        private LayoutDirection _layoutDirection;

        public LayoutDirection LayoutDirection
        {
            get { return _layoutDirection; }
            set
            {
                if (_layoutDirection != value)
                {
                    this._layoutDirection = value;
                    AdaptPlotAreas();
                }
            }
        }

        private int _oneWayChartNum;

        public int OneWayChartNum
        {
            get { return _oneWayChartNum; }
            set
            {
                _oneWayChartNum = value;
                if (!_autoLayout && _isSplitView)
                {
                    this._oneWayChartNum = value;
                    AdaptPlotAreas();
                }
            }
        }

        private float _columnInterval = 0;

        public float ColumnInterval
        {
            get
            {
                return _columnInterval;
            }
            set
            {
                this._columnInterval = value;
                if (_isSplitView)
                {
                    AdaptPlotAreas();
                }
            }
        }

        private float _rowInterval = 0;

        public float RowInterval
        {
            get
            {
                return _rowInterval;
            }
            set
            {
                _rowInterval = value;
                if (_isSplitView)
                {
                    AdaptPlotAreas();
                }
            }
        }

        private readonly EasyChartX _parentChart = null;
        private readonly Chart _plotChart = null;
        private readonly PlotManager _plotManager;

        internal ChartViewManager(EasyChartX parentChart, Chart plotChart, PlotManager plotManager)
        {
            this._parentChart = parentChart;
            this._plotChart = plotChart;
            this._plotManager = plotManager;
            this.MainPlotArea = new EasyChartXPlotArea(parentChart, plotChart.ChartAreas[0]);
            this.MainPlotArea.ChartArea.Position.Auto = true;
            this._layoutDirection = LayoutDirection.LeftToRight;
            this._oneWayChartNum = 3;
            this.UseMainAreaConfig = true;

            this.SplitPlotAreas = new EasyChartXPlotAreaCollection(parentChart, plotChart.ChartAreas);
            AdaptView();
        }

        internal void SetAxisLabelStyle()
        {
            MainPlotArea.AxisX.SetAxisLabelStyle();
            MainPlotArea.AxisX2.SetAxisLabelStyle();
            MainPlotArea.AxisY.SetAxisLabelStyle();
            MainPlotArea.AxisY2.SetAxisLabelStyle();
            foreach (EasyChartXPlotArea splitPlotArea in SplitPlotAreas)
            {
                splitPlotArea.AxisX.SetAxisLabelStyle();
                splitPlotArea.AxisX2.SetAxisLabelStyle();
                splitPlotArea.AxisY.SetAxisLabelStyle();
                splitPlotArea.AxisY2.SetAxisLabelStyle();
            }
        }

        /// <summary>
        /// 匹配绘图区
        /// </summary>
        internal void AdaptView()
        {
            MainPlotArea.Enabled = !_isSplitView;
            if (IsSplitView && _plotManager.SeriesCount != SplitPlotAreas.Count)
            {
                SplitPlotAreas.AdaptPlotAreaCount(_plotManager.SeriesCount);
                ApplyMainPlotAreaToAll();
            }
            for (int i = 0; i < SplitPlotAreas.Count; i++)
            {
                SplitPlotAreas[i].Enabled = (IsSplitView && i < _plotManager.SeriesCount);
            }
            // TODO to check
            AdaptPlotAreas();
        }

        private void AdaptPlotAreas()
        {
            if (!IsSplitView)
            {
                ArrangeMasterPlotArea();
            }
            else
            {
                ArrangeSplitPlotAreas();
            }
        }

        internal void ArrangeMasterPlotArea()
        {
            MainPlotArea.ChartArea.Position.Auto = true;

//            MainPlotArea.XPosition = 0;
//            MainPlotArea.YPosition = 0;
//
//            MainPlotArea.Width = (_parentChart.LegendVisible) ? 100 - _plotChart.Legends[0].Position.Width : 100;
//            MainPlotArea.Height = 100;
        }

        internal void ArrangeSplitPlotAreas()
        {
            int colCount, rowCount;
            GetArrangedColAndRow(out colCount, out rowCount);
            float totalChartWidth = (_parentChart.LegendVisible) ? _plotChart.Legends[0].Position.X : 100;
            // 总的宽度减去边界的宽度
            totalChartWidth -= Constants.XBoundRatio;
            float columnIntervalWidth = (colCount - 1)*_columnInterval*100/_parentChart.Width;
            float rowintervalWidth = ((rowCount - 1) * _rowInterval)*100 /_parentChart.Height;

            // 总的高度减去边界的高度
            float totalChartHeight = 100 - Constants.YBoundRatio;
            float singlePlotAreaWidth = (totalChartWidth - columnIntervalWidth)/colCount;
            float singlePlotAreaHeight = (totalChartHeight - rowintervalWidth) / rowCount;
            switch (LayoutDirection)
            {
                case LayoutDirection.LeftToRight:
                    ArrangeByRow(colCount, singlePlotAreaWidth, singlePlotAreaHeight);
                    break;
                case LayoutDirection.TopToBottom:
                    ArrangeByColumn(rowCount, singlePlotAreaWidth, singlePlotAreaHeight);
                    break;
                default:
                    break;
            }
        }

        private void ArrangeByRow(int colCount, float singlePlotAreaWidth, float singlePlotAreaHeight)
        {
            float columnIntervalRatio = _columnInterval * 100 / _parentChart.Width;
            float rowIntervalRatio = _rowInterval * 100 / _parentChart.Height;

            // 起始位置为边界宽度的一半
            float xPosition = Constants.XBoundRatio/2;
            float yPosition = Constants.YBoundRatio/2;
            for (int i = 0; i < _plotManager.SeriesCount; i++)
            {
                SplitPlotAreas[i].XPosition = xPosition;
                SplitPlotAreas[i].YPosition = yPosition;
                SplitPlotAreas[i].Width = singlePlotAreaWidth;
                SplitPlotAreas[i].Height = singlePlotAreaHeight;
                if (0 == (i + 1) % colCount)
                {
                    // 起始位置为边界宽度的一半
                    xPosition = Constants.XBoundRatio/2;
                    yPosition += singlePlotAreaHeight + rowIntervalRatio;
                }
                else
                {
                    xPosition += singlePlotAreaWidth + columnIntervalRatio;
                }
            }
        }

        private void ArrangeByColumn(int rowCount, float singlePlotAreaWidth, float singlePlotAreaHeight)
        {
            float columnIntervalRatio = _columnInterval * 100 / _parentChart.Width;
            float rowIntervalRatio = _rowInterval * 100 / _parentChart.Height;

            // 起始位置为边界宽度的一半
            float xPosition = Constants.XBoundRatio/2;
            float yPosition = Constants.YBoundRatio/2;
            for (int i = 0; i < _plotManager.SeriesCount; i++)
            {
                SplitPlotAreas[i].XPosition = xPosition;
                SplitPlotAreas[i].YPosition = yPosition;
                SplitPlotAreas[i].Width = singlePlotAreaWidth;
                SplitPlotAreas[i].Height = singlePlotAreaHeight;
                if (0 == (i + 1) % rowCount)
                {
                    // 起始位置为边界宽度的一半
                    yPosition = Constants.YBoundRatio/2;
                    xPosition += singlePlotAreaWidth + columnIntervalRatio;
                }
                else
                {
                    yPosition += singlePlotAreaHeight + rowIntervalRatio;
                }
            }
        }

        private void GetArrangedColAndRow(out int colCount, out int rowCount)
        {
            double dim1, dim2;
            if (AutoLayout)
            {
                const int autoOneLineMaxDim = 3;
                if (_plotManager.SeriesCount <= autoOneLineMaxDim)
                {
                    dim1 = _plotManager.SeriesCount;
                    dim2 = 1;
                }
                else
                {
                    dim1 = Math.Ceiling(Math.Sqrt(_plotManager.SeriesCount));
                    dim2 = Math.Ceiling(_plotManager.SeriesCount / dim1);
                }
            }
            else
            {
                dim1 = _plotManager.SeriesCount >= _oneWayChartNum ? _oneWayChartNum : _plotManager.SeriesCount;
                dim2 = Math.Ceiling((double)_plotManager.SeriesCount / _oneWayChartNum);
            }
            switch (LayoutDirection)
            {
                case LayoutDirection.LeftToRight:
                    colCount = (int) dim1;
                    rowCount = (int) dim2;
                    break;
                case LayoutDirection.TopToBottom:
                    colCount = (int) dim2;
                    rowCount = (int) dim1;
                    break;
                default:
                    colCount = (int) dim1;
                    rowCount = (int) dim2;
                    break;
            }
        }

        // TODO 暂时只同步Y轴和Y2轴，X轴的后期再说
        /// <summary>
        /// 更新坐标轴和游标：坐标轴的范围、缩放视图、缩放参数；游标绑定的坐标、游标缩放参数
        /// </summary>
        public void RefreshAxesAndCursors()
        {
            double maxXRange = _plotManager.GetMaxXData();
            double minXRange = _plotManager.GetMinXData();
            double minXInterval = _plotManager.GetMinXInterval();
            double minYInterval;
            if (!_isSplitView)
            {
                AdaptMainPlotAreaAxesRange(maxXRange, minXRange);
                minYInterval = GetMinYInterval(MainPlotArea);
                MainPlotArea.AdaptCursors(minXInterval, minYInterval);
            }
            else
            {
                for (int i = 0; i < SplitPlotAreas.Count; i++)
                {
                    AdaptSplitPlotAreaAxesRange(i, maxXRange, minXRange);
                    minYInterval = GetMinYInterval(SplitPlotAreas[i]);
                    SplitPlotAreas[i].AdaptCursors(minXInterval, minYInterval);
                }
            }
        }

        public void RefreshAxesRange(EasyChartXPlotArea plotArea)
        {
            double maxXRange = _plotManager.GetMaxXData();
            double minXRange = _plotManager.GetMinXData();
            if (ReferenceEquals(MainPlotArea, plotArea))
            {
                AdaptMainPlotAreaAxesRange(maxXRange, minXRange);
            }
            else
            {
                int areaIndex = SplitPlotAreas.IndexOf(plotArea);
                if (areaIndex >= 0)
                {
                    AdaptSplitPlotAreaAxesRange(areaIndex, maxXRange, minXRange);
                }
            }
        }

        private void AdaptMainPlotAreaAxesRange(double maxXRange, double minXRange)
        {
            double maxYRange = double.NaN;
            double minYRange = double.NaN;
            if (MainPlotArea.AxisY.AutoScale)
            {
                _plotManager.GetMaxAndMinYValue(MainPlotArea, out maxYRange, out minYRange, -1);
            }
            MainPlotArea.AdaptPrimaryAxes(maxXRange, minXRange, maxYRange, minYRange);

            // 默认不同步，如果需要打开在AdaptSecondaryAxes方法中打开
            MainPlotArea.YAxisSync.NeedSync = false;
            if (_plotManager.HasSeriesInYAxis(MainPlotArea, EasyChartXAxis.PlotAxis.Secondary))
            {
                double maxY2Range;
                double minY2Range;
                if (MainPlotArea.AxisY2.AutoScale)
                {
                    _plotManager.GetMaxAndMinY2Value(MainPlotArea, out maxY2Range, out minY2Range, -1);
                }
                else
                {
                    MainPlotArea.AxisY2.GetSpecifiedRange(out maxY2Range, out minY2Range);
                }
                MainPlotArea.AdaptSecondaryAxes(maxXRange, minXRange, maxY2Range, minY2Range);
            }
        }

        private void AdaptSplitPlotAreaAxesRange(int areaIndex, double maxXRange, double minXRange)
        {
            double maxYRange = double.NaN;
            double minYRange = double.NaN;
            
            if (SplitPlotAreas[areaIndex].AxisY.AutoScale)
            {
                _plotManager.GetMaxAndMinYValue(SplitPlotAreas[areaIndex], out maxYRange, out minYRange, areaIndex);
            }
            SplitPlotAreas[areaIndex].AdaptPrimaryAxes(maxXRange, minXRange, maxYRange, minYRange);

            SplitPlotAreas[areaIndex].AxisX.RefreshLabels();
            SplitPlotAreas[areaIndex].AxisY.RefreshLabels();
            // 在分区视图时不会在副坐标轴显示数据
            // 默认不同步，如果需要打开在AdaptSecondaryAxes方法中打开
//            SplitPlotAreas[areaIndex].YAxisSync.NeedSync = false;
//            if (_plotManager.HasSeriesInYAxis(SplitPlotAreas[areaIndex], EasyChartXAxis.PlotAxis.Secondary))
//            {
//                double maxY2Range;
//                double minY2Range;
//                if (MainPlotArea.AxisY2.AutoScale)
//                {
//                    if (double.IsNaN(maxYRange))
//                    {
//                        maxY2Range = maxYRange;
//                        minY2Range = minYRange;
//                    }
//                    else
//                    {
//                        _plotManager.GetMaxAndMinY2Value(SplitPlotAreas[areaIndex], out maxY2Range, out minY2Range, areaIndex);
//                    }
//                }
//                else
//                {
//                    SplitPlotAreas[areaIndex].AxisY2.GetSpecifiedRange(out maxY2Range, out minY2Range);
//                }
//
//                SplitPlotAreas[areaIndex].AdaptSecondaryAxes(maxXRange, minXRange, maxY2Range, minY2Range);
//                //            SplitPlotAreas[areaIndex].AxisX2.RefreshLabels();
//                SplitPlotAreas[areaIndex].AxisY2.RefreshLabels();
//            }
        }

        private double GetMinYInterval(EasyChartXPlotArea plotArea)
        {
            if (double.IsNaN(plotArea.AxisY.Maximum) || double.IsNaN(plotArea.AxisY.Minimum))
            {
                return 1E-3;
            }
            return (plotArea.AxisY.Maximum - plotArea.AxisY.Minimum)/ Constants.AutoYIntervalPrecision;
        }

        public void Clear()
        {
            MainPlotArea.Clear();
            foreach (EasyChartXPlotArea splitPlotArea in SplitPlotAreas)
            {
                splitPlotArea.Clear();
            }
        }

        public EasyChartXPlotArea GetHitPlotArea(HitTestResult result)
        {
            ChartElementType hitObject = result.ChartElementType;
            ChartArea hitChartArea = result.ChartArea;
            if (null == hitChartArea || (hitObject != ChartElementType.PlottingArea &&
                hitObject != ChartElementType.Gridlines && hitObject != ChartElementType.StripLines &&
                hitObject != ChartElementType.DataPoint && hitObject != ChartElementType.Axis))
            {
                return null;
            }
            EasyChartXPlotArea hitPlotArea = null;
            if (ReferenceEquals(MainPlotArea.ChartArea, hitChartArea))
            {
                hitPlotArea = MainPlotArea;
            }
            else
            {
                int plotAreaIndex = SplitPlotAreas.FindIndexByBaseChartArea(hitChartArea);
                if (plotAreaIndex >= 0 && plotAreaIndex < SplitPlotAreas.Count)
                {
                    hitPlotArea = SplitPlotAreas[plotAreaIndex];
                }
            }
            return hitPlotArea;
        }

        public void ApplyMainPlotAreaToAll()
        {
            foreach (EasyChartXPlotArea splitPlotArea in SplitPlotAreas)
            {
                splitPlotArea.ApplyConfig(MainPlotArea);
            }
        }
        
    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.StripChartUtility;

namespace SeeSharpTools.JY.GUI
{
    internal class StripPlotter : IDisposable
    {
        private Chart _chart;
        internal SeriesCollection PlotSeries;
        public int MaxSampleNum = 4000;
        internal int LineNum { get; private set; }

        internal bool ScrollMode { get; set; }
        internal PlotStatus PlotStatus { get; private set; }
        internal PlotAction PlotAction = null;
        private readonly AxisViewAdapter _axisViewAdapter;

        private bool _autoYRange;

        internal bool AutoYRange
        {
            get { return _autoYRange;}
            set
            {
                if (_autoYRange == value)
                {
                    return;
                }
                _autoYRange = value;
                PlotAction?.RefreshYAxisRange(0, double.NaN, double.NaN);
            }
        }

        internal void InitPlotAction()
        {
            if (MaxSampleNum <= Constants.MaxPointsInSingleSeries)
            {
                if (!(PlotAction is CommonStripPlotter))
                {
                    PlotAction = new CommonStripPlotter(this, _axisViewAdapter);
                }
            }
            else
            {
                
                if (!(PlotAction is CommonStripPlotter))
                {
                    PlotAction = new CommonStripPlotter(this, _axisViewAdapter);
                }
                PlotAction = new BindStripPlotter(this, _axisViewAdapter);
            }
            PlotAction.AdaptBufAndPoints();
        }

        public StripPlotter(Chart chart, AxisViewAdapter axisViewAdapter)
        {
            this._chart = chart;
            this._autoYRange = true;
            this.PlotSeries = chart.Series;
            this.PlotStatus = PlotStatus.Idle;
            this.LineNum = 1;
            this.ScrollMode = false;
            this._axisViewAdapter = axisViewAdapter;
        }

        public void StartPlot()
        {
            this.LineNum = PlotSeries.Count;
            ClearAllPoints();
            InitPlotAction();
            PlotStatus = PlotStatus.Plot;
        }

        public void Clear()
        {
//            ClearAllPoints();
            PlotAction?.Clear();
            PlotStatus = PlotStatus.Idle;
        }

        private void ClearAllPoints()
        {
            foreach (Series series in PlotSeries)
            {
                series.Points.Clear();
            }
        }

        public void Dispose()
        {
            if (PlotStatus.Idle == PlotStatus)
            {
                return;
            }
            PlotAction.Dispose();
            PlotStatus = PlotStatus.Idle;
        }
    }

    internal enum PlotStatus
    {
        Waiting,
        Plot,
        Error,
        Idle
    }
}
using System.ComponentModel;

namespace SeeSharpTools.JY.GUI.EasyChartXUtility
{
    /// <summary>
    /// Split view layout configure
    /// </summary>
    public class MiscellaneousConfiguration : PropertyClonableClass
    {
        /// <summary>
        /// The maximum series count that can be plot.
        /// </summary>
        [
            Category("Series"),
            Description("The maximum series count that can be plot")
        ]
        public int MaxSeriesCount
        {
            get { return _plotManager.MaxSeriesCount; }
            set
            {
                if (_plotManager.MaxSeriesCount <= 0)
                {
                    return;
                }
                _plotManager.MaxSeriesCount = value;
            }
        }

        /// <summary>
        /// The maximum point count in a series.
        /// </summary>
        // TODO 该属性暂时配置对序列化
        [
            Category("Series"),
            Description("The maximum point count in a series")
        ]
        public int MaxSeriesPointCount
        {
            get { return Constants.MaxPointsInSingleSeries; }
            set
            {
                if (value <= 0)
                {
                    return;
                }
                Constants.MaxPointsInSingleSeries = value;
            }
        }

        /// <summary>
        /// Get or specify whether check NaN data.
        /// </summary>
        [
            Browsable(true),
            Category("Data"),
            Description("Get or specify whether check NaN data.")
        ]
        public bool CheckNaN
        {
            get
            {
                return _plotManager.DataCheckParams.CheckNaN;
            }

            set
            {
                _plotManager.DataCheckParams.CheckNaN = value;
            }
        }

        /// <summary>
        /// Get or specify whether check negtive or zero data.
        /// </summary>
        [
            Browsable(true),
            Category("Data"),
            Description("Get or specify whether check negtive or zero data.")
        ]
        public bool CheckNegtiveOrZero
        {
            get
            {
                return _plotManager.DataCheckParams.CheckNegtiveOrZero;
            }

            set
            {
                _plotManager.DataCheckParams.CheckNegtiveOrZero = value;
            }
        }

        /// <summary>
        /// Get or specify whether check infinity data.
        /// </summary>
        [
            Browsable(true),
            Category("Data"),
            Description("Get or specify whether check infinity data.")
        ]
        public bool CheckInfinity
        {
            get
            {
                return _plotManager.DataCheckParams.CheckInfinity;
            }

            set
            {
                _plotManager.DataCheckParams.CheckInfinity = value;
            }
        }

        /// <summary>
        /// Specify or get the fitting algorithm type when point sparse enabled.
        /// </summary>
        [
            Browsable(true),
            Category("Data"),
            Description("Specify or get the fitting algorithm type when point sparse enabled."),
            EditorBrowsable(EditorBrowsableState.Never),
        ]
        public EasyChartX.FitType Fitting
        {
            get { return _plotManager.FitType; }
            set { _plotManager.FitType = value; }
        }

        #region Split View layout configuire

        /// <summary>
        /// The layout direction of split view: LeftToRight/TopToBottom.
        /// </summary>
        [
            Category("Split View Layout"),
            Description("The layout direction of split view: LeftToRight/TopToBottom.")
        ]
        public LayoutDirection SplitLayoutDirection
        {
            get { return _viewManager.LayoutDirection; }
            set { _viewManager.LayoutDirection = value; }
        }

        /// <summary>
        /// Specify whether enabled split view auto layout.
        /// </summary>
        [
            Category("Split View Layout"),
            Description("Specify whether enabled split view auto layout.")
        ]
        public bool SplitViewAutoLayout
        {
            get { return _viewManager.AutoLayout; }
            set { _viewManager.AutoLayout = value; }
        }

        /// <summary>
        /// Specify the chart count in single row(LeftToRight) or single column(TopToBottom).
        /// </summary>
        [
            Category("Split View Layout"),
            Description("Specify the chart count in single row(LeftToRight) or single column(TopToBottom).")
        ]
        public int DirectionChartCount
        {
            get { return _viewManager.OneWayChartNum; }
            set { _viewManager.OneWayChartNum = value; }
        }

        /// <summary>
        /// Specify the interval in pixel between adjacent columns.
        /// </summary>
        [
            Category("Split View Layout"),
            Description("Specify the interval in pixel between adjacent columns.")
        ]
        public float SplitLayoutColumnInterval
        {
            get { return _viewManager.ColumnInterval; }
            set { _viewManager.ColumnInterval = value; }
        }

        /// <summary>
        /// Specify the interval in pixel between adjacent rows.
        /// </summary>
        [
            Category("Split View Layout"),
            Description("Specify the interval in pixel between adjacent rows.")
        ]
        public float SplitLayoutRowInterval
        {
            get { return _viewManager.RowInterval; }
            set { _viewManager.RowInterval = value; }
        }

        #endregion

        private readonly EasyChartX _parentChart;
        private readonly PlotManager _plotManager;
        private readonly ChartViewManager _viewManager;

        internal MiscellaneousConfiguration(EasyChartX parentChart, ChartViewManager viewManager, PlotManager plotManager)
        {
            this._parentChart = parentChart;
            this._plotManager = plotManager;
            this._viewManager = viewManager;
        }
    }
}
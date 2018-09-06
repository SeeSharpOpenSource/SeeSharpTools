using System.ComponentModel;

namespace SeeSharpTools.JY.GUI.EasyChartXUtility
{
    /// <summary>
    /// Split view layout configure
    /// </summary>
    public class SplitViewLayout : PropertyClonableClass
    {
        /// <summary>
        /// The layout direction: LeftToRight/TopToBottom.
        /// </summary>
        [Description("The layout direction: LeftToRight/TopToBottom.")]
        public LayoutDirection Direction
        {
            get { return _viewManager.LayoutDirection; }
            set { _viewManager.LayoutDirection = value; }
        }

        /// <summary>
        /// Specify whether enabled auto layout.
        /// </summary>
        [Description("Specify whether enabled auto layout.")]
        public bool AutoLayout
        {
            get { return _viewManager.AutoLayout; }
            set { _viewManager.AutoLayout = value; }
        }

        /// <summary>
        /// Specify the chart count in single row(LeftToRight) or single column(TopToBottom).
        /// </summary>
        [Description("Specify the chart count in single row(LeftToRight) or single column(TopToBottom).")]
        public int DirectionChartCount
        {
            get { return _viewManager.OneWayChartNum; }
            set { _viewManager.OneWayChartNum = value; }
        }

        /// <summary>
        /// Specify the interval in pixel between adjacent columns.
        /// </summary>
        [Description("Specify the interval in pixel between adjacent columns.")]
        public float ColumnInterval
        {
            get { return _viewManager.ColumnInterval; }
            set { _viewManager.ColumnInterval = value; }
        }

        /// <summary>
        /// Specify the interval in pixel between adjacent rows.
        /// </summary>
        [Description("Specify the interval in pixel between adjacent rows.")]
        public float RowInterval
        {
            get { return _viewManager.RowInterval; }
            set { _viewManager.RowInterval = value; }
        }

        private readonly ChartViewManager _viewManager;

        internal SplitViewLayout(ChartViewManager viewManager)
        {
            this._viewManager = viewManager;
        }
    }
}
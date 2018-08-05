using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// EasyChart cursor related event arguments
    /// </summary>
    public class EasyChartXCursorEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public EasyChartXCursor Cursor { get; set; }

        /// <summary>
        /// Parent easyChart
        /// </summary>
        public EasyChartX ParentChart { get; set; }

        /// <summary>
        /// Cursor series index
        /// </summary>
        public int SeriesIndex { get; set; }

        /// <summary>
        /// Parent chart area
        /// </summary>
        public EasyChartXPlotArea ParentChartArea { get; set; }

        /// <summary>
        /// Event caused by mouse click or user defined
        /// </summary>
        public bool IsRaisedByMouseEvent { get; set; }
    }
}
using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// StripChartX cursor related event arguments
    /// </summary>
    public class StripChartXCursorEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public StripChartXCursor Cursor { get; set; }

        /// <summary>
        /// Parent StripChartX
        /// </summary>
        public StripChartX ParentChart { get; set; }

        /// <summary>
        /// Cursor series index
        /// </summary>
        public int SeriesIndex { get; set; }

        /// <summary>
        /// Parent chart area
        /// </summary>
        public StripChartXPlotArea ParentChartArea { get; set; }

        /// <summary>
        /// Event caused by mouse click or user defined
        /// </summary>
        public bool IsRaisedByMouseEvent { get; set; }
    }
}
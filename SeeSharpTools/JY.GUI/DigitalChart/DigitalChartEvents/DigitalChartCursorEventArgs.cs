using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// EasyChart cursor related event arguments
    /// </summary>
    public class DigitalChartCursorEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public DigitalChartCursor Cursor { get; set; }

        /// <summary>
        /// Parent EasyChartX
        /// </summary>
        public DigitalChart ParentChart { get; set; }

        /// <summary>
        /// Cursor series index
        /// </summary>
        public int SeriesIndex { get; set; }

        /// <summary>
        /// Parent chart area
        /// </summary>
        public DigitalChartPlotArea ParentChartArea { get; set; }

        /// <summary>
        /// Event caused by mouse click or user defined
        /// </summary>
        public bool IsRaisedByMouseEvent { get; set; }
    }
}
using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// EasyChart cursor related event arguments
    /// </summary>
    public class DigitalChartTabCursorEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public DigitalChartTabCursor Cursor { get; set; }

        /// <summary>
        /// Parent EasyChartX
        /// </summary>
        public DigitalChart ParentChart { get; set; }

        public TabCursorOperation Operation { get; set; }

        /// <summary>
        /// Parent chart area
        /// </summary>
        public DigitalChartPlotArea ParentChartArea { get; set; }
    }

    public enum TabCursorOperation
    {
        ValueChanged = 0,
        CursorAdded = 1,
        CursorDeleted = 2
    }
}
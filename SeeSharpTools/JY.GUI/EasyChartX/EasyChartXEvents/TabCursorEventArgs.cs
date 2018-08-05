using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// EasyChart cursor related event arguments
    /// </summary>
    public class TabCursorEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public TabCursor Cursor { get; set; }

        /// <summary>
        /// Parent easyChart
        /// </summary>
        public EasyChartX ParentChart { get; set; }

        public TabCursorOperation Operation { get; set; }

        /// <summary>
        /// Parent chart area
        /// </summary>
        public EasyChartXPlotArea ParentChartArea { get; set; }
    }

    public enum TabCursorOperation
    {
        ValueChanged = 0,
        CursorAdded = 1,
        CursorDeleted = 2
    }
}
using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// StripChartX cursor related event arguments
    /// </summary>
    public class StripTabCursorEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public StripTabCursor Cursor { get; set; }

        /// <summary>
        /// Parent StripChartX
        /// </summary>
        public StripChartX ParentChart { get; set; }

        public StripTabCursorOperation Operation { get; set; }

        /// <summary>
        /// Parent chart area
        /// </summary>
        public StripChartXPlotArea ParentChartArea { get; set; }
    }

    public enum StripTabCursorOperation
    {
        ValueChanged = 0,
        CursorAdded = 1,
        CursorDeleted = 2
    }
}
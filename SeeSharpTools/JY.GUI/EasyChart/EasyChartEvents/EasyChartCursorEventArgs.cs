using System;

namespace SeeSharpTools.JY.GUI.EasyChartEvents
{
    /// <summary>
    /// EasyChart cursor related event arguments
    /// </summary>
    public class EasyChartCursorEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public EasyChartCursor Cursor { get; set; }

        /// <summary>
        /// Parent easyChart
        /// </summary>
        public EasyChart ParentChart { get; set; }

        /// <summary>
        /// Event caused by mouse click or user defined
        /// </summary>
        public bool IsRaisedByMouseEvent { get; set; }
    }
}
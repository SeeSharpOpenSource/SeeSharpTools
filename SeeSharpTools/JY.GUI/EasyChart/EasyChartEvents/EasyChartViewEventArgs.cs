using System;

namespace SeeSharpTools.JY.GUI.EasyChartEvents
{
    /// <summary>
    /// EasyChart view related event arguments
    /// </summary>
    public class EasyChartViewEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public EasyChartAxis Axis { get; set; }

        /// <summary>
        /// Parent easyChart
        /// </summary>
        public EasyChart ParentChart { get; set; }

        /// <summary>
        /// Event caused by scale view changed or total range changed
        /// </summary>
        public bool IsScaleViewChanged { get; set; }


        /// <summary>
        /// Event caused by mouse click or user defined
        /// </summary>
        public bool IsRaisedByMouseEvent { get; set; }
    }
}
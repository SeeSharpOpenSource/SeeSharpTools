using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// EasyChart view related event arguments
    /// </summary>
    public class DigitalChartViewEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public DigitalChartAxis Axis { get; set; }

        /// <summary>
        /// Parent EasyChartX
        /// </summary>
        public DigitalChart ParentChart { get; set; }

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
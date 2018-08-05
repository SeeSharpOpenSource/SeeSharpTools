using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// EasyChart view related event arguments
    /// </summary>
    public class EasyChartXViewEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public EasyChartXAxis Axis { get; set; }

        /// <summary>
        /// Parent easyChart
        /// </summary>
        public EasyChartX ParentChart { get; set; }

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
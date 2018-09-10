using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// StripChart view related event arguments
    /// </summary>
    public class StripChartXViewEventArgs : EventArgs
    {
        /// <summary>
        /// Axis that view changed
        /// </summary>
        public StripChartXAxis Axis { get; set; }

        /// <summary>
        /// Parent StripChartX
        /// </summary>
        public StripChartX ParentChart { get; set; }

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
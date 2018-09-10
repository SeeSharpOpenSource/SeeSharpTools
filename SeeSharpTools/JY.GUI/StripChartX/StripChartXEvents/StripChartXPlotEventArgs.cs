using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// StripChart cursor related event arguments
    /// </summary>
    public class StripChartXPlotEventArgs : EventArgs
    {
        /// <summary>
        /// Indicate whether event raised by clear operation.
        /// </summary>
        public bool IsClear { get; set; }

        /// <summary>
        /// Parent StripChartX
        /// </summary>
        public StripChartX ParentChart { get; set; }
    }
}
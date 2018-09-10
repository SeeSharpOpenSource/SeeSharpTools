using System;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// EasyChart cursor related event arguments
    /// </summary>
    public class EasyChartXPlotEventArgs : EventArgs
    {
        /// <summary>
        /// Indicate whether event raised by clear operation.
        /// </summary>
        public bool IsClear { get; set; }

        /// <summary>
        /// Parent EasyChartX
        /// </summary>
        public EasyChartX ParentChart { get; set; }
    }
}
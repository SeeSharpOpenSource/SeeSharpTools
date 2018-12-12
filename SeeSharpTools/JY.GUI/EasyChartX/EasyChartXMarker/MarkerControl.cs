using System.Drawing;
using System.Windows.Forms;
using SeeSharpTools.JY.GUI.EasyChartXMarker.Painters;

namespace SeeSharpTools.JY.GUI.EasyChartXMarker
{
    internal class MarkerControl : UserControl
    {
        public double XValue { get; set; }
        public double YValue { get; set; }

        public MarkerControl()
        {
            const int defaultMarkerSize = 5;
            this.BackColor = Color.Transparent;
            this.Size = new Size(defaultMarkerSize, defaultMarkerSize);
        }
    }
}
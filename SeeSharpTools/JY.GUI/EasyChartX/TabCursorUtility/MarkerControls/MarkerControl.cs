using System.Drawing;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI.TabCursorUtility.MarkerControls
{
    public abstract class MarkerControl : Control
    {
        protected double XValue;
        protected double YValue;
        protected DataMarkerType Type { get; }

        protected MarkerControl(DataMarkerType type)
        {
            const int defaultMarkerSize = 5;
            this.BackColor = Color.Transparent;
            this.Size = new Size(defaultMarkerSize, defaultMarkerSize);
        }

        public void Hide
    }
}
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
    public class StripChartSeries
    {
        private const int MaxLineWidth = 10;
        private const int MinLineWidth = 1;
        private readonly Chart _baseChart;
        private readonly int _index;

        internal StripChartSeries(Chart baseChart, int index)
        {
            _baseChart = baseChart;
            _index = index;
        }

        public string Name
        {
            get { return _baseChart.Series[_index].Name; }
            set { _baseChart.Series[_index].Name = value; }
        }

        public int LineWidth
        {
            get { return _baseChart.Series[_index].BorderWidth; }
            set
            {
                if (value > MaxLineWidth)
                {
                    _baseChart.Series[_index].BorderWidth = MaxLineWidth;
                }
                else if (value < MinLineWidth)
                {
                    _baseChart.Series[_index].BorderWidth = MinLineWidth;
                }
                else
                {
                    _baseChart.Series[_index].BorderWidth = value;
                }
            }
        }

        public Color Color
        {
            get { return _baseChart.Series[_index].Color; }
            set { _baseChart.Series[_index].Color = value; }
        }
    }
}
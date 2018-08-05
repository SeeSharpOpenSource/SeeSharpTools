using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
    public class StripChartApperance
    {
        private readonly Chart _baseChart;
        private readonly StripChart _stripChart;
        internal StripChartApperance(StripChart stripChart, Chart baseChart)
        {
            this._stripChart = stripChart;
            this._baseChart = baseChart;
        }

        public Color ChartBackColor
        {
            get { return _baseChart.BackColor; }
            set
            {
                _baseChart.BackColor = value;
                _stripChart.BackColor = value;
            }
        }

        public Color ChartAreaColor
        {
            get { return _baseChart.ChartAreas[0].BackColor; }
            set { _baseChart.ChartAreas[0].BackColor = value; }
        }

        public bool MinorGridEnabled
        {
            get { return _baseChart.ChartAreas[0].AxisX.MinorGrid.Enabled && _baseChart.ChartAreas[0].AxisY.MinorGrid.Enabled; }
            set
            {
                _baseChart.ChartAreas[0].AxisX.MinorGrid.Enabled = value;
                _baseChart.ChartAreas[0].AxisY.MinorGrid.Enabled = value;
            }
        }

        /// <summary>
        /// Set Legend visible.
        /// 设置标签是否可见
        /// </summary>
        public bool LegendVisible
        {
            get { return _baseChart.Legends[0].Enabled; }
            set
            {
                _baseChart.Legends[0].Enabled = value;
            }
        }
    }
}
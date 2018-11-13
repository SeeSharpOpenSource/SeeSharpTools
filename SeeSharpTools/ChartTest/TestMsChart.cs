using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartTest
{
    public partial class TestMsChart : Form
    {
        public TestMsChart()
        {
            InitializeComponent();
        }

        private void TestMsChart_Load(object sender, EventArgs e)
        {
            const int length = 1000;
            string[] xDatas = new string[length];
            double[] xDataValue = new double[length];
            double[] yDatas = new double[length];
            for (int i = 0; i < length; i++)
            {
                xDatas[i] = "str" + i;
                xDataValue[i] = i;
                yDatas[i] = i%10;
            }
            yDatas[length - 1] = 100;
            chart1.Series[0].Points.DataBindXY(xDataValue, yDatas);
            for (int i = 0; i < length; i++)
            {
                chart1.Series[0].Points[i].AxisLabel = xDatas[i];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int lastIndex = chart1.Series[0].Points.Count -1;
            DataPoint lastPoint = chart1.Series[0].Points[lastIndex];
            chart1.Series[0].Points.RemoveAt(lastIndex);
            chart1.Series[0].Points.Insert(0, lastPoint);
        }
    }
}

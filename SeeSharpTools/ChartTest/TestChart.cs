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
    public partial class TestChart : Form
    {
        public TestChart()
        {
            InitializeComponent();
        }

        const int PointCount = 8000;

        private double[] data1;
        private double[] data2;
        private double[] xData;


        private void button1_Click(object sender, EventArgs e)
        {
            SeriesCollection series = chart1.Series;
            DateTime start = DateTime.Now;

            for (int i = 0; i < PointCount; i++)
            {
                series[0].Points.AddXY(xData, data1[i]);
                series[1].Points.AddXY(xData, data2[i]);
            }

            label1.Text = (DateTime.Now - start).TotalMilliseconds.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SeriesCollection series = chart1.Series;
            DateTime start = DateTime.Now;

            series[0].Points.DataBindY(data1);
            series[1].Points.DataBindY(data1);

            label1.Text = (DateTime.Now - start).TotalMilliseconds.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SeriesCollection series = chart1.Series;
            DateTime start = DateTime.Now;

            series[0].Points.DataBindXY(xData, data1);
            series[1].Points.DataBindXY(xData, data1);

            label1.Text = (DateTime.Now - start).TotalMilliseconds.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SeriesCollection series = chart1.Series;
            DateTime start = DateTime.Now;

            for (int i = 0; i < PointCount; i++)
            {
                series[0].Points[i].SetValueXY(xData, data1[i]);
                series[1].Points[i].SetValueXY(xData, data2[i]);
            }

            label1.Text = (DateTime.Now - start).TotalMilliseconds.ToString();
        }

        private void TestChart_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            for (int i = 0; i < PointCount; i++)
            {
                xData[i] = i;
                data1[i] = random.NextDouble();
                data2[i] = random.NextDouble();
            }
        }
    }
}

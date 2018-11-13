using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using JYUSB101;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JY.File;
using SeeSharpTools.JY.GUI;
using SeeSharpTools.JY.Report;

namespace ChartTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            easyChartX1.TabCursors.CursorValueFormat = "0.###";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //            double[] data = new double[] {1, 2, 3, 4, 1, 2, 3, 4};
            //            chart1.Series.Clear();
            //            foreach (ChartArea chartArea in chart1.ChartAreas)
            //            {
            //                Series series = new Series(chartArea.Name);
            //                series.Points.DataBindY(data);
            //                series.ChartArea = chartArea.Name;
            //                chart1.Series.Add(series);
            //            }
            const int length = 1000000;
            const double ratio = 0.0000001;
            const double offset = 0;
            _data = new double[6, length];
            _data2 = new double[6, length];

            const int cycle = 100;
            for (int i = 0; i < length; i++)
            {
                _data[0, i] = Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data[1, i] = 2 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data[2, i] = 3 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data[3, i] = 4 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data[4, i] = 5 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data[5, i] = 6 * Math.Sin(i * 2 * Math.PI / cycle) + offset;

                _data2[0, i] = (double)i/length * ratio * 2 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data2[1, i] = (double)i / length * ratio * 4 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data2[2, i] = (double)i / length * ratio * 6 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data2[3, i] = (double)i / length * ratio * 8 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data2[4, i] = (double)i / length * ratio * 10 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
                _data2[5, i] = (double)i / length * ratio * 12 * Math.Sin(i * 2 * Math.PI / cycle) + offset;
            }
        }

        private double[,] test;
        private double[,] _data;
        private double[,] _data2;
        private IEnumerable<double> enumerable;

        private void button1_Click(object sender, EventArgs e)
        {
//            chart1.ChartAreas.Add(new ChartArea($"ChartArea{chart1.ChartAreas.Count + 1}"));
//            Form1_Load(null, null);
//            if (0 == chart1.Series.Count%2)
//            {
//                chart1.ChartAreas[chart1.ChartAreas.Count - 1].Visible = false;
//            }
//            ChartValues<double> data = new ChartValues<double>{ 1, 2, 3, 4, 5, 6, 7, 8, 2 };
//            cartesianChart1.Series.Add(new LineSeries());
//            cartesianChart1.Series[0].Values = data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[,] test1, test2, test3;
            DateTime start = DateTime.Now;
            test = new double[10000, 10000];
            test1 = new double[10000, 10000];
            test2 = new double[10000, 10000];
            test3 = new double[10000, 10000];
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IEnumerable<double> enumerable1, enumerable2, enumerable3;
            DateTime start = DateTime.Now;
            enumerable = test.Cast<double>();
            enumerable1 = test.Cast<double>();
            enumerable2 = test.Cast<double>();
            enumerable3 = test.Cast<double>();
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            easyChartX1.Plot(_data, -50, 0.0001);
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
            label_easyChartXTime.Text = totalMilliseconds.ToString("F0");
//            TestOpen();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int[,] convertData = new int[_data.GetLength(0), _data.GetLength(1)];
            for (int i = 0; i < convertData.GetLength(0); i++)
            {
                for (int j = 0; j < convertData.GetLength(1); j++)
                {
                    convertData[i, j] = (int)_data[i, j];
                }
            }

            DateTime start = DateTime.Now;
            easyChartX1.Plot(convertData, 0, 0.01);
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
            label_easyChartXTime.Text = totalMilliseconds.ToString("F0");
        }


        private void button9_Click(object sender, EventArgs e)
        {
            uint[,] convertData = new uint[_data.GetLength(0), _data.GetLength(1)];
            for (int i = 0; i < convertData.GetLength(0); i++)
            {
                for (int j = 0; j < convertData.GetLength(1); j++)
                {
                    convertData[i, j] = (uint)_data[i, j];
                }
            }

            DateTime start = DateTime.Now;
            easyChartX1.Plot(convertData, 0, 0.01);
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
            label_easyChartXTime.Text = totalMilliseconds.ToString("F0");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            short[,] convertData = new short[_data.GetLength(0), _data.GetLength(1)];
            for (int i = 0; i < convertData.GetLength(0); i++)
            {
                for (int j = 0; j < convertData.GetLength(1); j++)
                {
                    convertData[i, j] = (short)_data[i, j];
                }
            }

            DateTime start = DateTime.Now;
            easyChartX1.Plot(convertData, 0, 0.01);
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
            label_easyChartXTime.Text = totalMilliseconds.ToString("F0");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ushort[,] convertData = new ushort[_data.GetLength(0), _data.GetLength(1)];
            for (int i = 0; i < convertData.GetLength(0); i++)
            {
                for (int j = 0; j < convertData.GetLength(1); j++)
                {
                    convertData[i, j] = (ushort)_data[i, j];
                }
            }

            DateTime start = DateTime.Now;
            easyChartX1.Plot(convertData, 0, 0.01);
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
            label_easyChartXTime.Text = totalMilliseconds.ToString("F0");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            float[,] convertData = new float[_data.GetLength(0), _data.GetLength(1)];
            for (int i = 0; i < convertData.GetLength(0); i++)
            {
                for (int j = 0; j < convertData.GetLength(1); j++)
                {
                    convertData[i, j] = (float)_data[i, j];
                }
            }

            DateTime start = DateTime.Now;
            easyChartX1.Plot(convertData, 0, 0.01);
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
            label_easyChartXTime.Text = totalMilliseconds.ToString("F0");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
//            easyChartX1.LineSeries[2].XPlotAxis = EasyChartX.PlotAxis.Primary;
//            easyChartX1.LineSeries[2].YPlotAxis = EasyChartX.PlotAxis.Secondary;
            easyChartX1.Plot(_data2);

            for (int i = 0; i < _data.GetLength(1); i++)
            {
                if (Math.Abs(_data[5, i] + 4.373811) < 0.001)
                {
                    return;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            easyChartX1.Clear();
            easyChart1.Clear();
            timer1.Enabled = false;
            _count = 0;
            _totalTime = 0;
            _totalXTime = 0;
            Chart msChart = easyChartX1.Controls[0] as Chart;
//            Logger.Close();

//            Test();
//            easyChartX_function.SplitView = true;
//            ViewControllerDialog viewControllerDialog = new ViewControllerDialog(viewController1, this);
//            viewControllerDialog.Show();

//            Logger.Initialize(@"D:\test.log");
//            try
//            {
//                JYUSB101AITask aiTask = new JYUSB101AITask(0);
//            }
//            catch (Exception ex)
//            {
//                Logger.Error(ex, "aitask error");
//            }
//            Logger.Warn("This is a test.");

            viewController1.State = "idle";
        }

        private void Test()
        {
            FileStream stream = new FileStream(@"D:\test.txt", FileMode.Open, FileAccess.Write, FileShare.None);
            stream.Close();
        }

        private void TestOpen()
        {
            FileStream stream = new FileStream(@"D:\test.txt", FileMode.Open, FileAccess.Write, FileShare.None);
            stream.WriteByte((byte)0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            JYUSB101AITask aiTask = new JYUSB101AITask(0);
            //            double[,] data = new double[,] { {1, 2, 3, 4, 5}, { 1, 2, 3, 4, 5 } };
            DateTime start = DateTime.Now;
            easyChart1.Plot(_data);
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
            label_easyChartTime.Text = totalMilliseconds.ToString("F0");
            
        }
        
        private void button2_Click_1(object sender, EventArgs e)
        {
            //            double[,] data = new double[,] { {1, 2, 3, 4, 5}, { 1, 2, 3, 4, 5 } };
            DateTime start = DateTime.Now;
            easyChart1.Plot(_data2);
            double totalMilliseconds = (DateTime.Now - start).TotalMilliseconds;
            label_easyChartTime.Text = totalMilliseconds.ToString("F0");
            viewController1.State = "run";
        }

        private int _count = 0;
        private uint _totalXTime = 0;
        private uint _totalTime = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
//            DateTime startTime = DateTime.Now;
////            for (int i = 0; i < 1000; i++)
////            {
////                Logger.Info($"This is just a test.{i}");
////            }
//
//            ParallelOptions options = new ParallelOptions();
//            options.MaxDegreeOfParallelism = 20;
//            Parallel.For(0, 1000, new Action<int>(i =>
//            {
//                Logger.Info($"This is just a test.{i}");
//            }));
//
//            Console.WriteLine((DateTime.Now - startTime).TotalMilliseconds);


            double[,] data = _count%2 == 0 ? _data : _data2;

            DateTime start = DateTime.Now;
            easyChartX1.Plot(data);
            _totalXTime += (uint)(DateTime.Now - start).TotalMilliseconds;

            start = DateTime.Now;
            easyChart1.Plot(data);
            _totalTime += (uint)(DateTime.Now - start).TotalMilliseconds;

            _count++;

            label_easyChartXTime.Text = ((double)_totalXTime/_count).ToString("F2");
            label_easyChartTime.Text = ((double)_totalTime/_count).ToString("F2");
            
        }

        private double[,] _buffer;
        private double[,] _dispBuffer;
        private void button6_Click(object sender, EventArgs e)
        {
            double[,] emptyData = new double[3, 1000];
            easyChart1.Plot(emptyData);
            easyChartX1.Plot(emptyData);
        }

        private void button7_Click(object sender, EventArgs e)
        {
//            Logger.Initialize(@"D:\test.log");

            LogConfig logConfig = new LogConfig();
            logConfig.FileLog.LogMode = FileLogMode.Directory;
            logConfig.FileLog.Path = @"D:\test";
            logConfig.FileLog.MaxLogSize = 10000000L;
            Logger.Initialize(logConfig);

            timer1.Enabled = true;
        }

        private void easyChartX1_AxisViewChanged(object sender, EasyChartXViewEventArgs e)
        {
            //ignore
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button13_Click(object sender, EventArgs e)
        {
            const int dataLength = 2000;
            const int dataLength2 = 20000;
            double[] datax1 = new double[dataLength];
            double[] datax2 = new double[dataLength2];
            double[] datay1 = new double[dataLength];
            double[] datay2 = new double[dataLength2];
            for (int i = 0; i < datax1.Length; i++)
            {
                datax1[i] = i*0.1;
                datay1[i] = Math.Sin(i* 2 * Math.PI / (dataLength/100));
            }
            for (int i = 0; i < datax2.Length; i++)
            {
                datax2[i] = i;
                datay2[i] = Math.Sin(i * 2 * Math.PI / (dataLength2/100));
            }
            double[][] datax = new double[2][];
            double[][] datay = new double[2][];
            datax[1] = datax1;
            datax[0] = datax2;
            datay[1] = datay1;
            datay[0] = datay2;
            easyChartX1.Plot(datax, datay);
        }

        private void easyChartX1_CursorPositionChanged(object sender, EasyChartXCursorEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            double[,] data = new double[500, 1024];
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j] = j*2 + i;
                }
            }
            easyChartX1.Plot(data);
//            easyChart1.Plot(data);
        }

        private void knobControl1_ValueChanged(object Sender)
        {
            Console.WriteLine(knobControl1.Value.ToString("F2"));
        }

        private void slide1_ValueChanged(object sender, double value)
        {
            Console.WriteLine(slide1.Value.ToString("F2"));
        }

        private void slide1_ValueChanging(object sender, double value)
        {
            Console.WriteLine("Changing");
        }

        private void knobControl1_ValueChanging(object Sender)
        {
            Console.WriteLine("Changing");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            byte[] data = new byte[10000];
            random.NextBytes(data);
            easyChartX1.Plot(data);
        }

        private void easyChartX1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JY.GUI;

namespace ChartTest
{
    public partial class TestStripChart : Form
    {
        public TestStripChart()
        {
            InitializeComponent();
        }

        private void numericUpDown_lineNum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                stripChartX_test.SeriesCount = (int) numericUpDown_lineNum.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_single_dim1_Click(object sender, EventArgs e)
        {
            double[] data = new double[5];
            var random = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = random.NextDouble();
            }
            try
            {
                stripChartX_test.SeriesCount = 1;
                stripChartX_test.Plot(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_single_dim2_Click(object sender, EventArgs e)
        {
            double[] data = new double[5];
            var random = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = random.NextDouble();
            }
            try
            {
                stripChartX_test.SeriesCount = 5;
                stripChartX_test.Plot(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_multi_dim1_Click(object sender, EventArgs e)
        {
            timer_plot.Enabled = true;
        }

        private void button_multi_dim2_Click(object sender, EventArgs e)
        {
            double[,] data = new double[5, 100];
            var random = new Random();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j] = random.NextDouble();
                }
            }
            try
            {
                stripChartX_test.SeriesCount = 5;
                DateTime start = DateTime.Now;
                stripChartX_test.Plot(data);
                label_time.Text = (DateTime.Now - start).TotalMilliseconds.ToString("F1") + "ms";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            timer_plot.Enabled = false;
            stripChartX_test.Clear();
        }

        private void comboBox_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            stripChartX_test.XDataType = (StripChartX.XAxisDataType) Enum.Parse(typeof (StripChartX.XAxisDataType), comboBox_type.Text);
        }

        private void TestStripChart_Load(object sender, EventArgs e)
        {
            this.comboBox_type.SelectedIndex = 0;
            this.numericUpDown_timeInterval.Value = 1;
        }

        double[,] data = new double[5, 20];
        private void timer_plot_Tick(object sender, EventArgs e)
        {
            var random = new Random();
            int cycle = data.GetLength(1);
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
//                    data[i, j] = random.NextDouble();
                    data[i, j] = i * Math.Sin(2*Math.PI*j/cycle);
                }
            }
            try
            {
                stripChartX_test.SeriesCount = 5;
                DateTime start = DateTime.Now;
                stripChartX_test.Plot(data);
                label_time.Text = (DateTime.Now - start).TotalMilliseconds.ToString("F1") + "ms";
            }
            catch (ApplicationException ex)
            {
                timer_plot.Enabled = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDown_timeInterval_ValueChanged(object sender, EventArgs e)
        {
           stripChartX_test.TimeInterval = new TimeSpan(0, 0, 0, 0, (int) numericUpDown_timeInterval.Value);
        }

        public void CalculateLength(double[] x, double[] y, double[] lengths)
        {
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount;
            Parallel.For(0, x.Length, options, new Action<int>((int index) =>
            {
                lengths[index] = Math.Sqrt(x[index]*x[index] + y[index]*y[index]);
            }));
        }

//        public struct ComplexNumber
//        {
//            public double Real { get; set; }
//            public double Imaginary { get; set; }
//            public double Modulus { get; set; }
//        }

        public void CalculateModulus(ComplexNumber[] datas)
        {
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount;
            Parallel.ForEach(datas, options, CaluculateSingleModulus);
        }

        public void CaluculateSingleModulus(ComplexNumber data, ParallelLoopState state, long index)
        {
            double real = data.Real;
            double imaginary = data.Imaginary;
            data.Modulus = Math.Sqrt(real*real + imaginary*imaginary);
        }

        public class ComplexNumber
        {
            public double Real { get; set; }
            public double Imaginary { get; set; }
            public double Modulus { get; set; }

            public void CalculateModulus()
            {
                double real = this.Real;
                double imaginary = this.Imaginary;
                this.Modulus = Math.Sqrt(real*real + imaginary*imaginary);
            }
        }

        public void CalculateModulusByInvoke(ComplexNumber[] datas)
        {
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount;
            Action[] actions = new Action[datas.Length];
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i] = datas[i].CalculateModulus;
            }
            Parallel.Invoke(options, actions);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ChartTest
{
    
    public partial class TestArrayAndList : Form
    {
        public TestArrayAndList()
        {
            InitializeComponent();
        }

        private List<double> _dstBuf = new List<double>(2000000);
        private List<double> _dstBuf3 = new List<double>(2000000);
        private double[] _dstBuf2 = null;
        private double[,] _srcBuf2 = null;
        private double[] _srcBuf = null;

        private void button3_Click(object sender, EventArgs e)
        {
            int lineCount = ((int) numericUpDown1.Value);
            int dataCount = ((int) numericUpDown2.Value);
            if (lineCount == 1)
            {
                _srcBuf = new double[lineCount * dataCount];
            }
            else
            {
                _srcBuf2 = new double[lineCount, dataCount];
            }
            _dstBuf2 = new double[lineCount * dataCount];
            timer1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            _srcBuf2 = null;
            _srcBuf = null;
            _arrayTime = 0;
            _listTime = 0;
            _listListTime = 0;
            _count = 0;
        }

        private ulong _arrayTime = 0;
        private ulong _listTime = 0;
        private ulong _listListTime = 0;
        private double _count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            _dstBuf.Clear();
            _dstBuf3.Clear();
            int lineCount = ((int)numericUpDown1.Value);
            int dataCount = ((int)numericUpDown2.Value);
            if (lineCount == 1)
            {
                _srcBuf = new double[lineCount * dataCount];
            }
            else
            {
                _srcBuf2 = new double[lineCount, dataCount];
            }

            DateTime start;
            if (((int)numericUpDown1.Value) == 1)
            {
                start = DateTime.Now;
                Buffer.BlockCopy(_srcBuf, 0, _dstBuf2, 0, sizeof (double)*_srcBuf.Length);
                _arrayTime += (uint)(DateTime.Now - start).TotalMilliseconds;
                start = DateTime.Now;
                _dstBuf.AddRange(_srcBuf);
                _listTime += (uint)(DateTime.Now - start).TotalMilliseconds;
            }
            else
            {
                start = DateTime.Now;
                Buffer.BlockCopy(_srcBuf2, 0, _dstBuf2, 0, sizeof(double) * _srcBuf2.Length);
                _arrayTime += (uint)(DateTime.Now - start).TotalMilliseconds;
                start = DateTime.Now;
                _dstBuf.AddRange(_srcBuf2.Cast<double>());

//                GCHandle handle = GCHandle.Alloc(_srcBuf2, GCHandleType.Pinned);
//                IntPtr ptr = handle.AddrOfPinnedObject();
//                Marshal.PtrToStructure()

                _listTime += (uint)(DateTime.Now - start).TotalMilliseconds;
            }

            start = DateTime.Now;
            _dstBuf3.AddRange(_dstBuf);
            _listListTime += (uint)(DateTime.Now - start).TotalMilliseconds;
            _count++;
            textBox1.Text = (_arrayTime/_count).ToString("F3");
            textBox2.Text = (_listTime / _count).ToString("F3");
            textBox3.Text = (_listListTime / _count).ToString("F3");
        }
    }
}

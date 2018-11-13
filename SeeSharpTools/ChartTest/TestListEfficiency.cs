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
    public partial class TestListEfficiency : Form
    {
        public TestListEfficiency()
        {
            InitializeComponent();
            AppDomain.MonitoringIsEnabled = true;
            GC.Collect();
        }

        private List<double> _testData;
        private List<double> _subData;
        private double[] _arrayData;

        private void button_run_Click(object sender, EventArgs e)
        {
            int dataSize = (int) numericUpDown_size.Value;
            DateTime start = DateTime.Now;
            _testData = new List<double>(dataSize);
            for (int i = 0; i < dataSize; i++)
            {
                _testData.Add(0);
            }
            ShowValue(start);
        }


        private void button_Range_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            _subData = _testData?.GetRange(0, _testData.Count/2);
            ShowValue(start);
        }

        private void button_ToArray_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            if (null != _subData)
            {
                _arrayData = _subData.ToArray();
            }
            ShowValue(start);

        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            _subData = null;
            _testData = null;
            _arrayData = null;
        }

        private void button_GC_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            GC.Collect();
            ShowValue(start);

        }

        private void ShowValue(DateTime start)
        {
            GC.Collect();
            TimeSpan timeSpan = DateTime.Now - start;
            label_timeValue.Text = timeSpan.TotalMilliseconds.ToString("F2");
            AppDomain currentDomain = AppDomain.CurrentDomain;
            label_allocatedMemValue.Text = ((double)currentDomain.MonitoringTotalAllocatedMemorySize/1000000).ToString("F3");
            label_usedMemValue.Text = ((double)currentDomain.MonitoringSurvivedMemorySize/1000000).ToString("F3");
        }
    }
}

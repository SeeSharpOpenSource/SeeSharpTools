using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    public partial class Set_YAxis_Range : Form
    {
        public Set_YAxis_Range(EasyChart easyChartObj)
        {
            InitializeComponent();
            easyChartForm = easyChartObj;
            YAutoScaleFlag = easyChartForm.YAutoEnable;
            if (YAutoScaleFlag)
            {
                easyChartForm.YAutoEnable = false;
            }
            double[] yAxisRange = new double[2];
            yAxisRange = easyChartForm.GetYAxisRange();
            numericUpDownYMax.Value = (decimal)yAxisRange[0];
            numericUpDownYMin.Value = (decimal)yAxisRange[1];
        }
        private EasyChart easyChartForm;
        private bool YAutoScaleFlag;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            easyChartForm.AxisYMax = (double)numericUpDownYMax.Value;
            easyChartForm.AxisYMin = (double)numericUpDownYMin.Value;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (YAutoScaleFlag)
            {
                easyChartForm.YAutoEnable = YAutoScaleFlag;
            }
            this.Close();
        }
    }
}

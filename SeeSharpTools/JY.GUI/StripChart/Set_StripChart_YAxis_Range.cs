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
    public partial class Set_StripChart_YAxis_Range : Form
    {
        public Set_StripChart_YAxis_Range(StripChart stripChartObj)
        {
            InitializeComponent();
            stripChartForm = stripChartObj;
            YAutoScaleFlag = stripChartForm.YAutoEnable;
            if (YAutoScaleFlag)
            {
                stripChartForm.YAutoEnable = false;
            }
            numericUpDownYMax.Value = (decimal)stripChartForm.AxisYMax;
            numericUpDownYMin.Value = (decimal)stripChartForm.AxisYMin;
        }
        private StripChart stripChartForm;
        private bool YAutoScaleFlag;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            stripChartForm.AxisYMax = (double)numericUpDownYMax.Value;
            stripChartForm.AxisYMin = (double)numericUpDownYMin.Value;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (YAutoScaleFlag)
            {
                stripChartForm.YAutoEnable = YAutoScaleFlag;
            }
            this.Close();
        }
    }
}

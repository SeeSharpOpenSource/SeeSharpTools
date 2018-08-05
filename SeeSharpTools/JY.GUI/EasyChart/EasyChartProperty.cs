using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
    public partial class EasyChartProperty : Form
    {
        private EasyChart _changedCtrl;
        private Control _beforeCtrl;
        //标志位，判断是由于调用this.close()完成的窗口关闭(true)，还是由于鼠标按下导致的窗口关闭(false)
        private bool windowCloseButton;

        public EasyChartProperty(EasyChart _ctrl)
        {
            InitializeComponent();
            _beforeCtrl = ControlFactory.CloneCtrl(_ctrl);
            _changedCtrl = _ctrl;
            windowCloseButton = false;
        }

        private void EasyChartProperty_Load(object sender, EventArgs e)
        {
            //update groupbox of Size
            numericUpDown_Height.Value = _changedCtrl.Size.Height;
            numericUpDown_Width.Value = _changedCtrl.Size.Width;
            //update groupbox of LegendVisible
            checkBox_LegendVisible.Checked = _changedCtrl.LegendVisible;
            //update Tickwidth
            checkBox_AutoYaxis.Checked = _changedCtrl.YAutoEnable;
            if (_changedCtrl.YAutoEnable == true)
            {
                numericUpDown_YaxisMax.Enabled = false;
                numericUpDown_YaxisMin.Enabled = false;
            }
            else
            {
                numericUpDown_YaxisMax.Enabled = true;
                numericUpDown_YaxisMin.Enabled = true;
            }
            if (!_changedCtrl.YAutoEnable)
            {
                double[] yAxisRange = new double[2];
                yAxisRange = _changedCtrl.GetYAxisRange();
                numericUpDown_YaxisMax.Value = (decimal)yAxisRange[0];
                numericUpDown_YaxisMin.Value = (decimal)yAxisRange[1];
            }
            //update Colors
            button_BackColor.BackColor = _changedCtrl.EasyChartBackColor;
            button_ChartAreaBackColor.BackColor = _changedCtrl.ChartAreaBackColor;
            button_ChartAreaBackColor.BackColor = _changedCtrl.LegendBackColor;
            //调用驱动中AITerminal的枚举作为选单
            comboBox_GradientStyle.Items.AddRange(Enum.GetNames(typeof(System.Windows.Forms.DataVisualization.Charting.GradientStyle)));
            if (_changedCtrl.ChartAreaBackColor == Color.Transparent)
            {
                checkBox_AreaTransparent.Checked = true;
            }
            else
                checkBox_AreaTransparent.Checked = false;
            
            if (_changedCtrl.LegendBackColor == Color.Transparent)
            {
                checkBox_LegendTransparent.Checked = true;
            }
            else
                checkBox_LegendTransparent.Checked = false;

            comboBox_GradientStyle.SelectedText =  _changedCtrl.GradientStyle.ToString();
            //update X&Y Logarithmic
            checkBox_XAxisLogarithmic.Checked = _changedCtrl.XAxisLogarithmic;
            checkBox_YAxisLogarithmic.Checked = _changedCtrl.YAxisLogarithmic;
        }

        private void numericUpDown_Height_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Size = new System.Drawing.Size(_changedCtrl.Width, (int)numericUpDown_Height.Value);
        }

        private void numericUpDown_Width_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Size = new System.Drawing.Size((int)numericUpDown_Width.Value, _changedCtrl.Height);
        }

        private void checkBox_LegendVisible_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.LegendVisible = checkBox_LegendVisible.Checked;
        }

        private void checkBox_AutoYaxis_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.YAutoEnable = checkBox_AutoYaxis.Checked;
            if (_changedCtrl.YAutoEnable)
            {
                numericUpDown_YaxisMax.Enabled = false;
                numericUpDown_YaxisMin.Enabled = false;
            }
            else
            {
                numericUpDown_YaxisMax.Enabled = true;
                numericUpDown_YaxisMin.Enabled = true;

                double[] yAxisRange = new double[2];
                yAxisRange = _changedCtrl.GetYAxisRange();
                numericUpDown_YaxisMax.Value = (decimal)yAxisRange[0];
                numericUpDown_YaxisMin.Value = (decimal)yAxisRange[1];
            }
        }

        private void numericUpDown_YaxisMax_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.AxisYMax = (double)numericUpDown_YaxisMax.Value;
        }

        private void numericUpDown_YaxisMin_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.AxisYMin = (double)numericUpDown_YaxisMin.Value;
        }

        private void button_BackColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_BackColor.BackColor = BorderColor.Color;
                _changedCtrl.EasyChartBackColor = BorderColor.Color;
            }
        }

        private void button_ChartAreaBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_ChartAreaBackColor.BackColor = BorderColor.Color;
                _changedCtrl.ChartAreaBackColor = BorderColor.Color;
            }
            checkBox_AreaTransparent.Checked = false;
        }

        private void button_LegendBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_LegendBackColor.BackColor = BorderColor.Color;
                _changedCtrl.LegendBackColor = BorderColor.Color;
            }
            checkBox_LegendTransparent.Checked = false;
        }

        private void checkBox_AreaTransparent_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_AreaTransparent.Checked)
                _changedCtrl.ChartAreaBackColor = Color.Transparent;
            else
                _changedCtrl.ChartAreaBackColor = button_ChartAreaBackColor.BackColor;
        }

        private void checkBox_LegendTransparent_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_LegendTransparent.Checked)
                _changedCtrl.LegendBackColor = Color.Transparent;
            else
                _changedCtrl.LegendBackColor = button_LegendBackColor.BackColor;
        }

        private void comboBox_GradientStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.GradientStyle =  (EasyChart.EasyChartGradientStyle)Enum.Parse(typeof(EasyChart.EasyChartGradientStyle), comboBox_GradientStyle.Text, true);
        }

        private void checkBox_XAxisLogarithmic_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.XAxisLogarithmic = checkBox_XAxisLogarithmic.Checked;
        }

        private void checkBox_YAxisLogarithmic_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.YAxisLogarithmic = checkBox_YAxisLogarithmic.Checked;
        }

        private void button_Confirm_Click(object sender, EventArgs e)
        {
            windowCloseButton = true;
            this.Close();
        }

        private void EasyChartProperty_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (windowCloseButton == false)
            {
                CancelConfigure();
            }
        }

        private void CancelConfigure()
        {
            EasyChart beforeEasyChart = ((EasyChart)_beforeCtrl);
            _changedCtrl.Size = beforeEasyChart.Size;

            _changedCtrl.LegendVisible = beforeEasyChart.LegendVisible;
            _changedCtrl.YAutoEnable = beforeEasyChart.YAutoEnable;
            double[] yAxisRange = new double[2];
            yAxisRange = beforeEasyChart.GetYAxisRange();
            if (!double.IsNaN(yAxisRange[0]))
            {
                _changedCtrl.AxisYMax = (int) yAxisRange[0];
            }
            if (!double.IsNaN(yAxisRange[1]))
            {
                _changedCtrl.AxisYMin = (int)yAxisRange[1];
            }

            _changedCtrl.EasyChartBackColor = beforeEasyChart.EasyChartBackColor;
            _changedCtrl.ChartAreaBackColor = beforeEasyChart.ChartAreaBackColor;
            _changedCtrl.LegendBackColor = beforeEasyChart.LegendBackColor;
            _changedCtrl.GradientStyle = beforeEasyChart.GradientStyle;
            _changedCtrl.XAxisLogarithmic = beforeEasyChart.XAxisLogarithmic;
            _changedCtrl.YAxisLogarithmic = beforeEasyChart.YAxisLogarithmic;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            CancelConfigure();
            this.Close();
        }
    }
}

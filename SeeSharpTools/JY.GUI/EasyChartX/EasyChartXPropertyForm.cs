using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.EasyChartXUtility;

namespace SeeSharpTools.JY.GUI
{
    public partial class EasyChartXPropertyForm : Form
    {
        private EasyChartX _changedCtrl;
        private Control _beforeCtrl;
        //标志位，判断是由于调用this.close()完成的窗口关闭(true)，还是由于鼠标按下导致的窗口关闭(false)
        private bool windowCloseButton;

        public EasyChartXPropertyForm(EasyChartX _ctrl)
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
                textBox_primaryYMax.Enabled = false;
                textBox_primaryYMin.Enabled = false;
            }
            else
            {
                textBox_primaryYMax.Enabled = true;
                textBox_primaryYMin.Enabled = true;
            }
            if (!_changedCtrl.YAutoEnable)
            {
                textBox_primaryYMax.Text = _changedCtrl.AxisY.Maximum.ToString();
                textBox_primaryYMin.Text = _changedCtrl.AxisY.Minimum.ToString();
            }
            //update Colors
            button_BackColor.BackColor = _changedCtrl.BackColor;
            button_ChartAreaBackColor.BackColor = _changedCtrl.ChartAreaBackColor;
            button_ChartAreaBackColor.BackColor = _changedCtrl.LegendBackColor;
            checkBox_XAxisLogarithmic.Checked = _changedCtrl.AxisX.IsLogarithmic;
            checkBox_YAxisLogarithmic.Checked = _changedCtrl.AxisY.IsLogarithmic;
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
                textBox_primaryYMax.Enabled = false;
                textBox_primaryYMin.Enabled = false;
            }
            else
            {
                textBox_primaryYMax.Enabled = true;
                textBox_primaryYMin.Enabled = true;
                textBox_primaryYMax.Text = _changedCtrl.AxisY.Maximum.ToString();
                textBox_primaryYMin.Text = _changedCtrl.AxisY.Minimum.ToString();
            }
        }

        private void button_BackColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_BackColor.BackColor = BorderColor.Color;
                _changedCtrl.BackColor = BorderColor.Color;
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
        }

        private void button_LegendBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_LegendBackColor.BackColor = BorderColor.Color;
                _changedCtrl.LegendBackColor = BorderColor.Color;
            }
        }

        private void checkBox_XAxisLogarithmic_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.AxisX.IsLogarithmic = checkBox_XAxisLogarithmic.Checked;
        }

        private void checkBox_YAxisLogarithmic_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.AxisY.IsLogarithmic = checkBox_YAxisLogarithmic.Checked;
        }

        private void button_Confirm_Click(object sender, EventArgs e)
        {
            double yMax = double.Parse(textBox_primaryYMax.Text);
            double yMin = double.Parse(textBox_primaryYMin.Text);
            if (!checkBox_AutoYaxis.Checked && yMax - yMin < Constants.MinDoubleValue)
            {
                MessageBox.Show("Invalid Y range value.", "EasyChartX");
                return;
            }
            SetAxisValue(_changedCtrl.AxisY, yMax, yMin);
            SetAxisValue(_changedCtrl.AxisY2, yMax, yMin);
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
            EasyChartX beforeEasyChart = ((EasyChartX)_beforeCtrl);
            _changedCtrl.Size = beforeEasyChart.Size;

            _changedCtrl.LegendVisible = beforeEasyChart.LegendVisible;
            _changedCtrl.YAutoEnable = beforeEasyChart.YAutoEnable;
            double yAxisRange = beforeEasyChart.AxisY.Maximum;
            if (!double.IsNaN(yAxisRange))
            {
                _changedCtrl.AxisY.Maximum = (int)yAxisRange;
            }
            yAxisRange = beforeEasyChart.AxisY.Minimum;
            if (!double.IsNaN(yAxisRange))
            {
                _changedCtrl.AxisY.Minimum = (int)yAxisRange;
            }

            _changedCtrl.BackColor = beforeEasyChart.BackColor;
            _changedCtrl.ChartAreaBackColor = beforeEasyChart.ChartAreaBackColor;
            _changedCtrl.LegendBackColor = beforeEasyChart.LegendBackColor;
            _changedCtrl.GradientStyle = beforeEasyChart.GradientStyle;
//            _changedCtrl.XAxisLogarithmic = beforeEasyChart.XAxisLogarithmic;
//            _changedCtrl.YAxisLogarithmic = beforeEasyChart.YAxisLogarithmic;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            CancelConfigure();
            this.Close();
        }

        private void textBox_DoubleTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            double value = 0;
            if (double.TryParse(textBox.Text, out value))
            {
                return;
            }
            textBox.Text = GetOriginalValue(textBox).ToString();
        }

        private double GetOriginalValue(TextBox textBox)
        {
            if (ReferenceEquals(textBox, textBox_primaryYMax))
            {
                return _changedCtrl.AxisY.Maximum;
            }
            else if (ReferenceEquals(textBox, textBox_primaryYMin))
            {
                return _changedCtrl.AxisY.Minimum;
            }
            return 0;
        }

        private void SetAxisValue(EasyChartXAxis axis, double max, double min)
        {
            if (max > axis.Minimum)
            {
                axis.Maximum = max;
                axis.Minimum = min;
            }
            else if (min < axis.Maximum)
            {
                axis.Minimum = min;
                axis.Maximum = max;
            }
        }
    }
}

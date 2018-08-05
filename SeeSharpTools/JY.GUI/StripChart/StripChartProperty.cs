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
    public partial class StripChartProperty : Form
    {
        private StripChart _changedCtrl;
        private Control _beforeCtrl;
        //标志位，判断是由于调用this.close()完成的窗口关闭(true)，还是由于鼠标按下导致的窗口关闭(false)
        private bool windowCloseButton;

        public StripChartProperty(StripChart _ctrl)
        {
            InitializeComponent();
            _beforeCtrl = ControlFactory.CloneCtrl(_ctrl);
            _changedCtrl = _ctrl;
            windowCloseButton = false;
        }

        private void StripChartProperty_Load(object sender, EventArgs e)
        {
            InitializeComboList();
            //Size
            numericUpDown_Height.Value = _changedCtrl.Size.Height;
            numericUpDown_Width.Value = _changedCtrl.Size.Width;
            //YAxis
            checkBox_YAxisLogarithmic.Checked = _changedCtrl.YAxisLogarithmic;
            //XAxis
            comboBox_XAxisDataType.Text = _changedCtrl.XAxisTypes.ToString();
            RefreshXAxisConfControlVisible();
            numericUpDown_XAxisStartIndex.Value = _changedCtrl.XAxisStartIndex;
            textBox_TimeStampFormat.Text = _changedCtrl.TimeStampFormat;
            //Appearence
            checkBox_LegendVisible.Checked = _changedCtrl.LegendVisible;
            checkBox_MinorGridEnabled.Checked = _changedCtrl.MinorGridEnabled;
            //Data
            numericUpDown_LineNum.Value = _changedCtrl.LineNum;
            numericUpDown_DisplayPoints.Value = _changedCtrl.DisPlayPoints;
            comboBox_DisplayDirection.Text = _changedCtrl.Displaydirection.ToString();
        }

        private void InitializeComboList()
        {
            BindComboBoxToEnum(typeof(StripChart.XAxisDataType), comboBox_XAxisDataType);
            BindComboBoxToEnum(typeof(StripChart.DisplayDirection), comboBox_DisplayDirection);
        }

        private void BindComboBoxToEnum(Type enumType, ComboBox control)
        {
            List<object> items = new List<object>();
            foreach (object item in Enum.GetNames(enumType))
            {
                items.Add(item);
            }
            control.Items.AddRange(items.ToArray());
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

        private void checkBox_MinorGridEnabled_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.MinorGridEnabled = checkBox_MinorGridEnabled.Checked;
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

        private void StripChartProperty_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (windowCloseButton == false)
            {
                CancelConfigure();
            }
        }

        private void CancelConfigure()
        {
            StripChart beforeStripChart = ((StripChart)_beforeCtrl);
            //Size
            _changedCtrl.Size = beforeStripChart.Size;
            //YAxis
            _changedCtrl.YAxisLogarithmic = beforeStripChart.YAxisLogarithmic;
            //XAxis
            _changedCtrl.XAxisTypes = beforeStripChart.XAxisTypes;
            RefreshXAxisConfControlVisible();
            _changedCtrl.XAxisStartIndex = beforeStripChart.XAxisStartIndex;
            _changedCtrl.TimeStampFormat = beforeStripChart.TimeStampFormat;
            //Appearence
            _changedCtrl.LegendVisible = beforeStripChart.LegendVisible;
            _changedCtrl.MinorGridEnabled = beforeStripChart.MinorGridEnabled;
            //Data
            _changedCtrl.LineNum = beforeStripChart.LineNum;
            _changedCtrl.DisPlayPoints = beforeStripChart.DisPlayPoints;
            _changedCtrl.Displaydirection = beforeStripChart.Displaydirection;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            CancelConfigure();
            this.Close();
        }

        private void comboBox_XAxisDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.XAxisTypes = (StripChart.XAxisDataType)Enum.Parse(typeof(StripChart.XAxisDataType), 
                comboBox_XAxisDataType.Text);
            RefreshXAxisConfControlVisible();
        }

        private void RefreshXAxisConfControlVisible()
        {
            switch (_changedCtrl.XAxisTypes)
            {
                case StripChart.XAxisDataType.TimeStamp:
                    label_TimeStampFormat.Visible = true;
                    textBox_TimeStampFormat.Visible = true;

                    label_XAxisStartIndex.Visible = false;
                    numericUpDown_XAxisStartIndex.Visible = false;
                    break;
                case StripChart.XAxisDataType.Index:
                    label_TimeStampFormat.Visible = false;
                    textBox_TimeStampFormat.Visible = false;

                    label_XAxisStartIndex.Visible = true;
                    numericUpDown_XAxisStartIndex.Visible = true;
                    break;
                case StripChart.XAxisDataType.String:
                    label_TimeStampFormat.Visible = false;
                    textBox_TimeStampFormat.Visible = false;

                    label_XAxisStartIndex.Visible = false;
                    numericUpDown_XAxisStartIndex.Visible = false;
                    break;
                default:
                    label_TimeStampFormat.Visible = false;
                    textBox_TimeStampFormat.Visible = false;

                    label_XAxisStartIndex.Visible = false;
                    numericUpDown_XAxisStartIndex.Visible = false;
                    break;
            }
        }

        private void textBox_TimeStampFormat_TextChanged(object sender, EventArgs e)
        {
            _changedCtrl.TimeStampFormat = textBox_TimeStampFormat.Text;
        }

        private void numericUpDown_XAxisStartIndex_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.XAxisStartIndex = (int) numericUpDown_XAxisStartIndex.Value;
        }

        private void numericUpDown_LineNum_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.LineNum = (int) numericUpDown_LineNum.Value;
        }

        private void comboBox_DisplayDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.Displaydirection =
                (StripChart.DisplayDirection) Enum.Parse(typeof (StripChart.DisplayDirection),
                    comboBox_DisplayDirection.Text);
        }

        private void numericUpDown_DisplayPoints_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.DisPlayPoints = (int) numericUpDown_DisplayPoints.Value;
        }
    }
}

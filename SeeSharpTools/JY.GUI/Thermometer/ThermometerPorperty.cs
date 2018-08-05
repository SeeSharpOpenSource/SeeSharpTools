using System;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    internal partial class ThermometerPorperty : Form
    {
        private Thermometer _changedCtrl;
        private Control _beforeCtrl;
        //标志位，判断是由于调用this.close()完成的窗口关闭(true)，还是由于鼠标按下导致的窗口关闭(false)
        private bool windowCloseButton;
        public ThermometerPorperty(Thermometer _ctrl)
        {
            InitializeComponent();
            _beforeCtrl = ControlFactory.CloneCtrl(_ctrl);
            _changedCtrl = _ctrl;
        }

        private void ThermometerPorperty_Load(object sender, EventArgs e)
        {
            numericUpDown_BallSize.Value = _changedCtrl.BallSize;
            button_ForeColor.BackColor = _changedCtrl.ForeColor;
            fontDialog1.Font = _changedCtrl.Font;
            //Text Setting
            foreach (TickStyle ThermometerTextStyle in Enum.GetValues(typeof(TickStyle)))
            {
                comboBox_TextStyle.Items.Add(ThermometerTextStyle);
            }
            comboBox_TextStyle.Text = _changedCtrl.TextStyle.ToString();
            buttonTextColor.BackColor = _changedCtrl.TextColor;
            //  Tick Setting
            foreach (TickStyle ThermometerTickStyle in Enum.GetValues(typeof(TickStyle)))
            {
                comboBox_TickStyle.Items.Add(ThermometerTickStyle);
            }
            comboBox_TickStyle.Text = _changedCtrl.TickStyle.ToString();
            numericUpDown_TickWidth.Value = _changedCtrl.TickWidth;
            button_TickColor.BackColor = _changedCtrl.TickColor;
            //Line Setting
            numericUpDown_LineWidth.Value = _changedCtrl.LineWidth;
            button_LineColor.BackColor = _changedCtrl.LineColor;
            //Data
            numericUpDown_Maximun.Value = (int)_changedCtrl.Max;
            numericUpDown_Minimun.Value = (int)_changedCtrl.Min;
            numericUp_DownDivisions.Value = _changedCtrl.NumberOfDivisions;
            numericUpDown_TextDecimals.Value = _changedCtrl.TextDecimals;

            
        }

        private void numericUpDown_BallSize_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.BallSize =(int)numericUpDown_BallSize.Value;
        }

        private void button_ForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_ForeColor.BackColor = BorderColor.Color;
                _changedCtrl.ForeColor = BorderColor.Color;
            }
        }

        private void button_Font_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _changedCtrl.Font = fontDialog1.Font;
            }
        }

        private void comboBox_TextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.TextStyle = (TickStyle)Enum.Parse(typeof(TickStyle), comboBox_TextStyle.SelectedItem.ToString());
        }


        private void comboBox_TickStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.TickStyle = (TickStyle)Enum.Parse(typeof(TickStyle), comboBox_TickStyle.SelectedItem.ToString());
        }

        private void numericUpDown_TickWidth_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.TickWidth = (int)numericUpDown_TickWidth.Value;
        }

        private void button_TickColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_TickColor.BackColor = BorderColor.Color;
                _changedCtrl.TickColor = BorderColor.Color;
            }            
        }

        private void numericUpDown_LineWidth_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.LineWidth = (int)numericUpDown_LineWidth.Value;
        }

        private void button_LineColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_LineColor.BackColor = BorderColor.Color;
                _changedCtrl.LineColor = BorderColor.Color;
            }
        }

        private void buttonTextColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                buttonTextColor.BackColor = BorderColor.Color;
                _changedCtrl.TextColor = BorderColor.Color;
            }
        }

        private void numericUpDown_Maximun_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Max =(int) numericUpDown_Maximun.Value;
        }

        private void numericUpDown_Minimun_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Min = (int)numericUpDown_Minimun.Value;
        }

        private void numericUp_DownDivisions_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.NumberOfDivisions = (int)numericUp_DownDivisions.Value;
        }

        private void numericUpDown_TextDecimals_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.TextDecimals = (int)numericUpDown_TextDecimals.Value;
        }

        private void button_Confirm_Click(object sender, EventArgs e)
        {
            windowCloseButton = true;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            CancelConfigure();
            this.Close();
        }


        private void CancelConfigure()
        {
            _changedCtrl.BallSize = ((Thermometer)_beforeCtrl).BallSize;
            _changedCtrl.ForeColor = ((Thermometer)_beforeCtrl).ForeColor;
            _changedCtrl.Font = ((Thermometer)_beforeCtrl).Font;
            //Text Setting
            _changedCtrl.TextStyle = ((Thermometer)_beforeCtrl).TextStyle;
            _changedCtrl.TextColor = ((Thermometer)_beforeCtrl).TextColor;
            //Tick Setting
            _changedCtrl.TickStyle = ((Thermometer)_beforeCtrl).TickStyle;
            _changedCtrl.TickWidth = ((Thermometer)_beforeCtrl).TickWidth;
            _changedCtrl.TickColor = ((Thermometer)_beforeCtrl).TickColor;
            //Line Setting
            _changedCtrl.LineWidth = ((Thermometer)_beforeCtrl).LineWidth;
            _changedCtrl.LineColor = ((Thermometer)_beforeCtrl).LineColor;
            //Data
            _changedCtrl.Max = ((Thermometer)_beforeCtrl).Max; ;
            _changedCtrl.Min = ((Thermometer)_beforeCtrl).Min;
            _changedCtrl.NumberOfDivisions = ((Thermometer)_beforeCtrl).NumberOfDivisions;
            _changedCtrl.TextDecimals = ((Thermometer)_beforeCtrl).TextDecimals;
        }

        private void ThermometerPorperty_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (windowCloseButton == false)
            {
                CancelConfigure();
            }
        }
    }
}

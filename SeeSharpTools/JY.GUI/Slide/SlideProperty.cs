using System;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{    
    internal partial class SlideProperty : Form
    {
        private Slide _changedCtrl;
        private Control _beforeCtrl;
        //标志位，判断是由于调用this.close()完成的窗口关闭(true)，还是由于鼠标按下导致的窗口关闭(false)
        private bool windowCloseButton;
        public SlideProperty()
        {
            InitializeComponent();
        }


        public SlideProperty(Slide _ctrl)
        {
            InitializeComponent();
            _beforeCtrl = ControlFactory.CloneCtrl(_ctrl);
            _changedCtrl = _ctrl;
        }

        private void SlideProperty_Load(object sender, EventArgs e)
        {
            //update groupbox of Font
            fontDialog1.Font = _changedCtrl.Font;
            //update groupbox of Size
            numericUpDown_Height.Value = _changedCtrl.Size.Height;
            numericUpDown_Width.Value = _changedCtrl.Size.Width;
            //update Tracker Size
            numericUpDown_TrackerHeight.Value = _changedCtrl.TrackerSize.Height;
            numericUpDown_TrackerWidth.Value = _changedCtrl.TrackerSize.Width;
            buttonTackerColor.BackColor = _changedCtrl.TrackerColor;
            //update Orientation
            foreach (Orientation SlideOrientation in Enum.GetValues(typeof(Orientation)))
            {
                //   Tank.TextStyleType.
                comboBox_Orientation.Items.Add(SlideOrientation);
                comboBox_Orientation.Text = _changedCtrl.Orientation.ToString();
            }

            //update groupbox of Text
            foreach (TickStyle SlideTextStyle in Enum.GetValues(typeof(TickStyle)))
            {
                comboBox_TextStyle.Items.Add(SlideTextStyle);
                comboBox_TextStyle.Text = _changedCtrl.TextStyle.ToString();
            }
            buttonTextColor.BackColor = _changedCtrl.ForeColor;
            //update groupbox of Tick
            foreach (TickStyle SlideTickStyle in Enum.GetValues(typeof(TickStyle)))
            {
                comboBox_TickStyle.Items.Add(SlideTickStyle);
                comboBox_TickStyle.Text = _changedCtrl.TextStyle.ToString();
            }
            numericUpDown_TickWidth.Value = _changedCtrl.TickWidth;
            button_TickColor.BackColor = _changedCtrl.TickColor;
            //update Line Setting
            numericUpDown_LineWidth.Value = _changedCtrl.LineWidth;
            button_LineColor.BackColor = _changedCtrl.LineColor;
            //update Maxium an Min
            numericUpDown_Maximun.Value = (int)_changedCtrl.Max;
            numericUpDown_Minimun.Value = (int)_changedCtrl.Min;
            //update Number of Divisions
            numericUp_DownDivisions.Value = _changedCtrl.NumberOfDivisions;
            numericUpDown_TextDecimals.Value = _changedCtrl.TextDecimals;
            numericUpDown_VauleDecimals.Value = _changedCtrl.Valuedecimals;

            checkBox_Fill.Checked = _changedCtrl.Fill;
            button_FillColor.BackColor = _changedCtrl.FillColor;
        }

        private void numericUpDown_Height_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Size = new System.Drawing.Size(_changedCtrl.Width, (int)numericUpDown_Height.Value);
        }

        private void numericUpDown_Width_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Size = new System.Drawing.Size((int)numericUpDown_Width.Value, _changedCtrl.Height);
        }

        private void button_Font_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _changedCtrl.Font = fontDialog1.Font;
            }
        }

        private void numericUpDown_TrackerHeight_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_TrackerHeight.Value >= numericUpDown_TrackerWidth.Value)
            {
                _changedCtrl.TrackerSize = new System.Drawing.Size(_changedCtrl.TrackerSize.Width, (int)numericUpDown_TrackerHeight.Value);

            }
            else
            {
                numericUpDown_TrackerHeight.Value = numericUpDown_TrackerWidth.Value;
            }

            
        }

        private void numericUpDown_TrackerWidth_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.TrackerSize = new System.Drawing.Size((int)numericUpDown_TrackerWidth.Value, _changedCtrl.TrackerSize.Height);
        }

        private void buttonTackerColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                buttonTackerColor.BackColor = BorderColor.Color;
                _changedCtrl.TrackerColor = BorderColor.Color;
            }
        }

        private void comboBox_TextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.TextStyle = (TickStyle)Enum.Parse(typeof(TickStyle), comboBox_TextStyle.SelectedItem.ToString());
        }

        private void buttonTextColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                buttonTextColor.BackColor = BorderColor.Color;
                _changedCtrl.ForeColor = BorderColor.Color;
            }
        }

        private void comboBox_TickStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.TickStyle = (TickStyle)Enum.Parse(typeof(TickStyle), comboBox_TickStyle.SelectedItem.ToString());

        }

        private void numericUpDown_TickWidth_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.TickWidth=(int)numericUpDown_TickWidth.Value;
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

        private void numericUpDown_Maximun_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Max = (int)numericUpDown_Maximun.Value;
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

        private void numericUpDown_VauleDecimals_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Valuedecimals = (int)numericUpDown_VauleDecimals.Value;
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
            _changedCtrl.Font = ((Slide)_beforeCtrl).Font;
            _changedCtrl.Size = ((Slide)_beforeCtrl).Size;

            _changedCtrl.TrackerSize = ((Slide)_beforeCtrl).TrackerSize;
            _changedCtrl.TrackerColor = ((Slide)_beforeCtrl).TrackerColor;

            _changedCtrl.TextStyle = ((Slide)_beforeCtrl).TextStyle;
            _changedCtrl.ForeColor = ((Slide)_beforeCtrl).ForeColor;

            _changedCtrl.TickStyle = ((Slide)_beforeCtrl).TickStyle;
            _changedCtrl.TickWidth = ((Slide)_beforeCtrl).TickWidth;
            _changedCtrl.TickColor = ((Slide)_beforeCtrl).TickColor;

            _changedCtrl.LineWidth = ((Slide)_beforeCtrl).LineWidth;
            _changedCtrl.LineColor = ((Slide)_beforeCtrl).LineColor;
            _changedCtrl.Orientation = ((Slide)_beforeCtrl).Orientation;

            _changedCtrl.Fill = ((Slide)_beforeCtrl).Fill;
            _changedCtrl.FillColor = ((Slide)_beforeCtrl).FillColor;


            _changedCtrl.Min = ((Slide)_beforeCtrl).Min;
            _changedCtrl.Max = ((Slide)_beforeCtrl).Max;
            _changedCtrl.NumberOfDivisions = ((Slide)_beforeCtrl).NumberOfDivisions;
            _changedCtrl.TextDecimals = ((Slide)_beforeCtrl).TextDecimals;
            _changedCtrl.Valuedecimals = ((Slide)_beforeCtrl).Valuedecimals;
        }

        private void comboBox_Orientation_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.Orientation = (Orientation)Enum.Parse(typeof(Orientation), comboBox_Orientation.SelectedItem.ToString());
        }

        private void checkBox_Fill_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.Fill = checkBox_Fill.Checked;
        }

        private void button_FillColor_Click(object sender, EventArgs e)
        {
            ColorDialog FillColor = new ColorDialog();
            if (FillColor.ShowDialog() == DialogResult.OK)
            {
                button_FillColor.BackColor = FillColor.Color;
                _changedCtrl.FillColor = FillColor.Color;
            }
        }

        private void SlideProperty_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (windowCloseButton == false)
            {
                CancelConfigure();
            }
        }
    }
}

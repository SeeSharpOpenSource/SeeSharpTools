using System;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    internal partial class TankProperty : Form
    {
        private Tank _changedCtrl;
        private Control _beforeCtrl;
        //标志位，判断是由于调用this.close()完成的窗口关闭(true)，还是由于鼠标按下导致的窗口关闭(false)
        private bool windowCloseButton;
        public TankProperty(Tank _ctrl)
        {
            InitializeComponent();
            _beforeCtrl = ControlFactory.CloneCtrl(_ctrl);
            _changedCtrl = _ctrl;
        }

        private void button_BackColor_Click(object sender, EventArgs e)
        {
            ColorDialog backColor = new ColorDialog();
            if (backColor.ShowDialog() == DialogResult.OK)
            {
                button_BackColor.BackColor = backColor.Color;
                _changedCtrl.BackColor = backColor.Color;
            }
        }


        private void TankProperty_Load(object sender, EventArgs e)
        {
            //update groupbox of Enable
            fontDialog1.Font = _changedCtrl.Font;
            //update groupbox of Size
            numericUpDown_Height.Value = _changedCtrl.Size.Height;
            numericUpDown_Width.Value = _changedCtrl.Size.Width;
            //update groupbox of Image
            foreach (Tank.TankStyles TankStyle in Enum.GetValues(typeof(Tank.TankStyles)))
            {
                comboBoxStyles.Items.Add(TankStyle);
                comboBoxStyles.Text = _changedCtrl.Style.ToString();
            }
            //update Max an Min Value
            numericUpDown_Maximun.Value = (int)_changedCtrl.Maximum;
            numericUpDown_Minimun.Value = (int)_changedCtrl.Minimum;

            //update groupbox of TextStyle
            foreach (Tank.TextStyleType TankTextStyle in Enum.GetValues(typeof(Tank.TextStyleType)))
            {
             //   Tank.TextStyleType.
                comboBoxTextStyle.Items.Add(TankTextStyle);
                comboBoxTextStyle.Text = _changedCtrl.TextStyle.ToString();
            }
            //update Orientation
            foreach (Orientation TankOrientation in Enum.GetValues(typeof(Orientation)))
            {
                //   Tank.TextStyleType.
                comboBox_Orientation.Items.Add(TankOrientation);
                comboBox_Orientation.Text = _changedCtrl.Orientation.ToString();
            }
            //update text on Textbox
            textBox_Text.Text = _changedCtrl.Text;
            //update isbright
            checkBoxIsBright.Checked = _changedCtrl.IsBright;
            //update Color
            button_BackColor.BackColor = _changedCtrl.BackColor;
            button_BorderColor.BackColor = _changedCtrl.BorderColor;
            button_ForeColor.BackColor = _changedCtrl.BackColor;
            button_ForeColor.BackColor = _changedCtrl.ForeColor;
        }

        private void numericUpDown_Height_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Size = new System.Drawing.Size(_changedCtrl.Width, (int)numericUpDown_Height.Value);
        }

        private void numericUpDown_Width_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Size = new System.Drawing.Size((int)numericUpDown_Width.Value, _changedCtrl.Height);
        }

        private void comboBoxStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.Style = (Tank.TankStyles)Enum.Parse(typeof(Tank.TankStyles), comboBoxStyles.SelectedItem.ToString());
        }

        private void comboBoxTextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.TextStyle = (Tank.TextStyleType)Enum.Parse(typeof(Tank.TextStyleType), comboBoxTextStyle.SelectedItem.ToString());
        }

        private void numericUpDown_Minimun_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Minimum = (int)numericUpDown_Minimun.Value;
        }

        private void numericUpDown_Maximun_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Maximum = (int)numericUpDown_Maximun.Value;
        }

        private void textBox_Text_TextChanged(object sender, EventArgs e)
        {
            _changedCtrl.Text = textBox_Text.Text;
        }

        private void comboBox_Orientation_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.Orientation = (Orientation)Enum.Parse(typeof(Orientation), comboBox_Orientation.SelectedItem.ToString());

        }

        private void checkBoxIsBright_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.IsBright = checkBoxIsBright.Checked;
        }

        private void button_ForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog foreColor = new ColorDialog();
            if (foreColor.ShowDialog() == DialogResult.OK)
            {
                button_ForeColor.BackColor = foreColor.Color;
                _changedCtrl.ForeColor = foreColor.Color;
            }
        }

        private void button_BorderColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_BorderColor.BackColor = BorderColor.Color;
                _changedCtrl.BorderColor = BorderColor.Color;
            }
        }

        private void button_TextColor_Click(object sender, EventArgs e)
        {
            ColorDialog TextColor = new ColorDialog();
            if (TextColor.ShowDialog() == DialogResult.OK)
            {
                button_TextColor.BackColor = TextColor.Color;
                _changedCtrl.TextColor = TextColor.Color;
            }
        }

        private void button_Font_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _changedCtrl.Font = fontDialog1.Font;
            }
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
            _changedCtrl.Font = ((Tank)_beforeCtrl).Font;
            _changedCtrl.Size = ((Tank)_beforeCtrl).Size;

            _changedCtrl.IsBright = ((Tank)_beforeCtrl).IsBright;
            _changedCtrl.TextStyle = ((Tank)_beforeCtrl).TextStyle;
            _changedCtrl.Style = ((Tank)_beforeCtrl).Style;
            _changedCtrl.Text = ((Tank)_beforeCtrl).Text;
            _changedCtrl.Orientation = ((Tank)_beforeCtrl).Orientation;

            _changedCtrl.Minimum = ((Tank)_beforeCtrl).Minimum;
            _changedCtrl.Maximum = ((Tank)_beforeCtrl).Maximum;

            _changedCtrl.BackColor = ((Tank)_beforeCtrl).BackColor;
            _changedCtrl.ForeColor = ((Tank)_beforeCtrl).ForeColor;
            _changedCtrl.TextColor = ((Tank)_beforeCtrl).TextColor;
            _changedCtrl.BorderColor = ((Tank)_beforeCtrl).BorderColor;
        }
        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void TankProperty_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (windowCloseButton == false)
            {
                CancelConfigure();
            }
        }
    }
}

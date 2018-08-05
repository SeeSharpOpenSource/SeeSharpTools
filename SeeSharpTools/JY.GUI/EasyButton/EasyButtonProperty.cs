using System;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    internal partial class EasyButtonProperty : Form
    {
        private EasyButton _changedCtrl;
        private Control _beforeCtrl;
        //标志位，判断是由于调用this.close()完成的窗口关闭(true)，还是由于鼠标按下导致的窗口关闭(false)
        private bool windowCloseButton;

        public EasyButtonProperty(EasyButton _ctrl )
        {
            InitializeComponent();
            _beforeCtrl = ControlFactory.CloneCtrl(_ctrl);
            _changedCtrl = _ctrl;    
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _changedCtrl.Text = textBox_Text.Text;
        }



        private void ButtonPickColor_Click(object sender, EventArgs e)
        {
            ColorDialog backColor = new ColorDialog();
            if (backColor.ShowDialog() == DialogResult.OK)
            {
                button_Backcolor.BackColor = backColor.Color;
                _changedCtrl.BackColor = backColor.Color;

            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void EasyButtonProperty_Load(object sender, EventArgs e)
        {
            //update groupbox of Enable
            fontDialog1.Font = _changedCtrl.Font;

            //update groupbox of Size
            numericUpDown_Height.Value = _changedCtrl.Size.Height;
            numericUpDown_Width.Value  = _changedCtrl.Size.Width;
            //update groupbox of Image
            foreach (EasyButton.ButtonPresetImage buttonStyle in Enum.GetValues(typeof(EasyButton.ButtonPresetImage)))
            {
                presentImageCombox.Items.Add(buttonStyle);
                presentImageCombox.Text = _changedCtrl.PreSetImage.ToString();
            }
            if (_changedCtrl.ImageAlign == System.Drawing.ContentAlignment.MiddleLeft)
                radioButton_Imageleft.Checked = true;
            if (_changedCtrl.ImageAlign == System.Drawing.ContentAlignment.MiddleCenter)
                radioButton_ImageMiddle.Checked = true;
            if (_changedCtrl.ImageAlign == System.Drawing.ContentAlignment.MiddleRight)
                radioButton_Imageright.Checked = true;
            //update groupbox of Text
            textBox_Text.Text = _changedCtrl.Text;
            if (_changedCtrl.TextAlign == System.Drawing.ContentAlignment.MiddleLeft)
                radioButton_Textleft.Checked = true;
            if (_changedCtrl.TextAlign == System.Drawing.ContentAlignment.MiddleCenter)
                radioButton_TextMiddle.Checked = true;
            if (_changedCtrl.TextAlign == System.Drawing.ContentAlignment.MiddleRight)
                radioButton_TextRight.Checked = true;
            //update groupbox of Color
            button_Backcolor.BackColor = _changedCtrl.BackColor;
            button_Textcolor.BackColor = _changedCtrl.ForeColor;
            //update Font of Color
            fontDialog1.Font = _changedCtrl.Font ;

        }
        private void numericUpDown_Height_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Size = new System.Drawing.Size(_changedCtrl.Width,(int)numericUpDown_Height.Value);
        }
        private void numericUpDown_Width_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.Size = new System.Drawing.Size((int)numericUpDown_Width.Value, _changedCtrl.Height);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _changedCtrl.Font = fontDialog1.Font;
            }            
        }

        private void presentImageCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changedCtrl.PreSetImage = (EasyButton.ButtonPresetImage)Enum.Parse(typeof(EasyButton.ButtonPresetImage), presentImageCombox.SelectedItem.ToString());
        }

        private void radioButton_Image_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Imageleft.Checked == true)
                _changedCtrl.ImageAlign =  System.Drawing.ContentAlignment.MiddleLeft;
            if (radioButton_ImageMiddle.Checked == true)
                _changedCtrl.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            if (radioButton_Imageright.Checked == true)
                _changedCtrl.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
        }

        private void radioButton_Text_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Textleft.Checked == true)
                _changedCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            if (radioButton_TextMiddle.Checked == true)
                _changedCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            if (radioButton_TextRight.Checked == true)
                _changedCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        }

        private void button_Textcolor_Click(object sender, EventArgs e)
        {
            ColorDialog backColor = new ColorDialog();
            if (backColor.ShowDialog() == DialogResult.OK)
            {
                button_Textcolor.BackColor = backColor.Color;
                _changedCtrl.ForeColor = backColor.Color;
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

        private void EasyButtonProperty_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (windowCloseButton == false)
            {
                CancelConfigure();
            }

        }

        private void CancelConfigure()
        {
            _changedCtrl.BackColor = ((EasyButton)_beforeCtrl).BackColor;
            _changedCtrl.ForeColor = ((EasyButton)_beforeCtrl).ForeColor;
            _changedCtrl.Font = ((EasyButton)_beforeCtrl).Font;
            _changedCtrl.Size = ((EasyButton)_beforeCtrl).Size;
            _changedCtrl.PreSetImage = ((EasyButton)_beforeCtrl).PreSetImage;
            _changedCtrl.ImageAlign = ((EasyButton)_beforeCtrl).ImageAlign;
            _changedCtrl.Text = ((EasyButton)_beforeCtrl).Text;
            _changedCtrl.TextAlign = ((EasyButton)_beforeCtrl).TextAlign;
        }
    }
}

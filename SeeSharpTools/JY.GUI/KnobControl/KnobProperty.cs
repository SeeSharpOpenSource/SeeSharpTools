using System;
using System.Windows.Forms;
namespace SeeSharpTools.JY.GUI
{


    public partial class KnobProperty : Form
    {
        private KnobControl _changedCtrl;
        private Control _beforeCtrl;
        //标志位，判断是由于调用this.close()完成的窗口关闭(true)，还是由于鼠标按下导致的窗口关闭(false)
        private bool windowCloseButton;

        public KnobProperty(KnobControl _ctrl)
        {
            InitializeComponent();
            _beforeCtrl = ControlFactory.CloneCtrl(_ctrl);
            _changedCtrl = _ctrl;
            windowCloseButton = false;
        }

        private void KnobProperty_Load(object sender, EventArgs e)
        {
            //update groupbox of Font
            fontDialog1.Font = _changedCtrl.Font;
            //update groupbox of Size
            numericUpDown_Height.Value = _changedCtrl.Size.Height;
            numericUpDown_Width.Value = _changedCtrl.Size.Width;
            //update Tickwidth
            numericUpDown_TickWidth.Value = _changedCtrl.TickWidth;
            checkBox_IsTextShow.Checked = _changedCtrl.IsTextShow;
            //update Colors
            button_BackColor.BackColor = _changedCtrl.BackColor;
            button_KnobColor.BackColor = _changedCtrl.KnobColor;
            button_ForeColor.BackColor = _changedCtrl.ForeColor;

            numericUpDown_Maximun.Value = (int)_changedCtrl.Max;
            numericUpDown_Minimun.Value = (int)_changedCtrl.Min;
            numericUp_DownDivisions.Value = (int)_changedCtrl.NumberOfDivisions;
            numericUpDown_TextDecimals.Value = _changedCtrl.TextDecimals;
            numericUpDown_VauleDecimals.Value = _changedCtrl.Valuedecimals;
        }

        private void numericUpDown_TickWidth_ValueChanged(object sender, EventArgs e)
        {
            _changedCtrl.TickWidth = (int)numericUpDown_TickWidth.Value;
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

        private void checkBox_IsTextShow_CheckedChanged(object sender, EventArgs e)
        {
            _changedCtrl.IsTextShow = checkBox_IsTextShow.Checked;
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

        private void button_ForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_ForeColor.BackColor = BorderColor.Color;
                _changedCtrl.ForeColor = BorderColor.Color;
            }
        }

        private void button_KnobColor_Click(object sender, EventArgs e)
        {
            ColorDialog BorderColor = new ColorDialog();
            if (BorderColor.ShowDialog() == DialogResult.OK)
            {
                button_KnobColor.BackColor = BorderColor.Color;
                _changedCtrl.KnobColor = BorderColor.Color;
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

        private void KnobProperty_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (windowCloseButton == false)
            {
                CancelConfigure();
            }



        }
        private void CancelConfigure()
        {
            _changedCtrl.Font = ((KnobControl)_beforeCtrl).Font;
            _changedCtrl.Size = ((KnobControl)_beforeCtrl).Size;

            _changedCtrl.TickWidth = ((KnobControl)_beforeCtrl).TickWidth;
            _changedCtrl.IsTextShow = ((KnobControl)_beforeCtrl).IsTextShow;

            _changedCtrl.ForeColor = ((KnobControl)_beforeCtrl).ForeColor;
            _changedCtrl.BackColor = ((KnobControl)_beforeCtrl).BackColor;
            _changedCtrl.KnobColor = ((KnobControl)_beforeCtrl).KnobColor;

            _changedCtrl.Min = ((KnobControl)_beforeCtrl).Min;
            _changedCtrl.Max = ((KnobControl)_beforeCtrl).Max;
            _changedCtrl.NumberOfDivisions = ((KnobControl)_beforeCtrl).NumberOfDivisions;
            _changedCtrl.TextDecimals = ((KnobControl)_beforeCtrl).TextDecimals;
            _changedCtrl.Valuedecimals = ((KnobControl)_beforeCtrl).Valuedecimals;
        }
    }
}

namespace SeeSharpTools.JY.GUI
{
    partial class TankProperty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TankProperty));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_Width = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Height = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Font = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_TextColor = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button_BorderColor = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_ForeColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button_BackColor = new System.Windows.Forms.Button();
            this.checkBoxIsBright = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_Maximun = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxStyles = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxTextStyle = new System.Windows.Forms.ComboBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.numericUpDown_Minimun = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_Text = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.comboBox_Orientation = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Maximun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Minimun)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numericUpDown_Width);
            this.groupBox2.Controls.Add(this.numericUpDown_Height);
            this.groupBox2.Location = new System.Drawing.Point(18, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 72);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Height";
            // 
            // numericUpDown_Width
            // 
            this.numericUpDown_Width.Location = new System.Drawing.Point(92, 45);
            this.numericUpDown_Width.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_Width.Name = "numericUpDown_Width";
            this.numericUpDown_Width.Size = new System.Drawing.Size(63, 21);
            this.numericUpDown_Width.TabIndex = 4;
            this.numericUpDown_Width.ValueChanged += new System.EventHandler(this.numericUpDown_Width_ValueChanged);
            // 
            // numericUpDown_Height
            // 
            this.numericUpDown_Height.Location = new System.Drawing.Point(6, 45);
            this.numericUpDown_Height.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_Height.Name = "numericUpDown_Height";
            this.numericUpDown_Height.Size = new System.Drawing.Size(63, 21);
            this.numericUpDown_Height.TabIndex = 3;
            this.numericUpDown_Height.ValueChanged += new System.EventHandler(this.numericUpDown_Height_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Font);
            this.groupBox1.Location = new System.Drawing.Point(188, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 72);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FontSetting";
            // 
            // button_Font
            // 
            this.button_Font.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Font.Location = new System.Drawing.Point(29, 28);
            this.button_Font.Name = "button_Font";
            this.button_Font.Size = new System.Drawing.Size(30, 20);
            this.button_Font.TabIndex = 0;
            this.button_Font.Text = " ...";
            this.button_Font.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Font.UseVisualStyleBackColor = true;
            this.button_Font.Click += new System.EventHandler(this.button_Font_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.button_TextColor);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.button_BorderColor);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.button_ForeColor);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.button_BackColor);
            this.groupBox5.Location = new System.Drawing.Point(18, 229);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(278, 65);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Color";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(200, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "TextColor";
            // 
            // button_TextColor
            // 
            this.button_TextColor.BackColor = System.Drawing.SystemColors.Control;
            this.button_TextColor.Location = new System.Drawing.Point(215, 37);
            this.button_TextColor.Name = "button_TextColor";
            this.button_TextColor.Size = new System.Drawing.Size(22, 22);
            this.button_TextColor.TabIndex = 24;
            this.button_TextColor.UseVisualStyleBackColor = false;
            this.button_TextColor.Click += new System.EventHandler(this.button_TextColor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "BorderColor";
            // 
            // button_BorderColor
            // 
            this.button_BorderColor.BackColor = System.Drawing.SystemColors.Control;
            this.button_BorderColor.Location = new System.Drawing.Point(145, 37);
            this.button_BorderColor.Name = "button_BorderColor";
            this.button_BorderColor.Size = new System.Drawing.Size(22, 22);
            this.button_BorderColor.TabIndex = 20;
            this.button_BorderColor.UseVisualStyleBackColor = false;
            this.button_BorderColor.Click += new System.EventHandler(this.button_BorderColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "ForeColor";
            // 
            // button_ForeColor
            // 
            this.button_ForeColor.Location = new System.Drawing.Point(84, 37);
            this.button_ForeColor.Name = "button_ForeColor";
            this.button_ForeColor.Size = new System.Drawing.Size(22, 22);
            this.button_ForeColor.TabIndex = 18;
            this.button_ForeColor.UseVisualStyleBackColor = true;
            this.button_ForeColor.Click += new System.EventHandler(this.button_ForeColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "BackColor";
            // 
            // button_BackColor
            // 
            this.button_BackColor.Location = new System.Drawing.Point(17, 37);
            this.button_BackColor.Name = "button_BackColor";
            this.button_BackColor.Size = new System.Drawing.Size(22, 22);
            this.button_BackColor.TabIndex = 17;
            this.button_BackColor.UseVisualStyleBackColor = true;
            this.button_BackColor.Click += new System.EventHandler(this.button_BackColor_Click);
            // 
            // checkBoxIsBright
            // 
            this.checkBoxIsBright.AutoSize = true;
            this.checkBoxIsBright.Location = new System.Drawing.Point(217, 209);
            this.checkBoxIsBright.Name = "checkBoxIsBright";
            this.checkBoxIsBright.Size = new System.Drawing.Size(15, 14);
            this.checkBoxIsBright.TabIndex = 20;
            this.checkBoxIsBright.UseVisualStyleBackColor = true;
            this.checkBoxIsBright.CheckedChanged += new System.EventHandler(this.checkBoxIsBright_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(215, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "IsBright";
            // 
            // numericUpDown_Maximun
            // 
            this.numericUpDown_Maximun.Location = new System.Drawing.Point(220, 111);
            this.numericUpDown_Maximun.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_Maximun.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDown_Maximun.Name = "numericUpDown_Maximun";
            this.numericUpDown_Maximun.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_Maximun.TabIndex = 23;
            this.numericUpDown_Maximun.ValueChanged += new System.EventHandler(this.numericUpDown_Maximun_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(218, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "Maximun";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(23, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "Styles";
            // 
            // comboBoxStyles
            // 
            this.comboBoxStyles.FormattingEnabled = true;
            this.comboBoxStyles.Location = new System.Drawing.Point(24, 110);
            this.comboBoxStyles.Name = "comboBoxStyles";
            this.comboBoxStyles.Size = new System.Drawing.Size(101, 20);
            this.comboBoxStyles.TabIndex = 25;
            this.comboBoxStyles.SelectedIndexChanged += new System.EventHandler(this.comboBoxStyles_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(23, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 26;
            this.label10.Text = "TextStyle";
            // 
            // comboBoxTextStyle
            // 
            this.comboBoxTextStyle.FormattingEnabled = true;
            this.comboBoxTextStyle.Location = new System.Drawing.Point(24, 156);
            this.comboBoxTextStyle.Name = "comboBoxTextStyle";
            this.comboBoxTextStyle.Size = new System.Drawing.Size(101, 20);
            this.comboBoxTextStyle.TabIndex = 27;
            this.comboBoxTextStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxTextStyle_SelectedIndexChanged);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(180, 311);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 29;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Location = new System.Drawing.Point(49, 311);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(75, 23);
            this.button_Confirm.TabIndex = 28;
            this.button_Confirm.Text = "Confirm";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // numericUpDown_Minimun
            // 
            this.numericUpDown_Minimun.Location = new System.Drawing.Point(140, 111);
            this.numericUpDown_Minimun.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_Minimun.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDown_Minimun.Name = "numericUpDown_Minimun";
            this.numericUpDown_Minimun.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_Minimun.TabIndex = 31;
            this.numericUpDown_Minimun.ValueChanged += new System.EventHandler(this.numericUpDown_Minimun_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(138, 93);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 30;
            this.label11.Text = "Minimun";
            // 
            // textBox_Text
            // 
            this.textBox_Text.Location = new System.Drawing.Point(163, 156);
            this.textBox_Text.Name = "textBox_Text";
            this.textBox_Text.Size = new System.Drawing.Size(117, 21);
            this.textBox_Text.TabIndex = 32;
            this.textBox_Text.TextChanged += new System.EventHandler(this.textBox_Text_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(161, 139);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 33;
            this.label12.Text = "Text";
            // 
            // comboBox_Orientation
            // 
            this.comboBox_Orientation.FormattingEnabled = true;
            this.comboBox_Orientation.Location = new System.Drawing.Point(23, 201);
            this.comboBox_Orientation.Name = "comboBox_Orientation";
            this.comboBox_Orientation.Size = new System.Drawing.Size(150, 20);
            this.comboBox_Orientation.TabIndex = 34;
            this.comboBox_Orientation.SelectedIndexChanged += new System.EventHandler(this.comboBox_Orientation_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(22, 187);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 12);
            this.label13.TabIndex = 35;
            this.label13.Text = "Orientation";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // TankProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 346);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboBox_Orientation);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBox_Text);
            this.Controls.Add(this.numericUpDown_Minimun);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Confirm);
            this.Controls.Add(this.comboBoxTextStyle);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBoxStyles);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDown_Maximun);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkBoxIsBright);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(326, 385);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(326, 385);
            this.Name = "TankProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tank Property";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TankProperty_FormClosing);
            this.Load += new System.EventHandler(this.TankProperty_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Maximun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Minimun)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Width;
        private System.Windows.Forms.NumericUpDown numericUpDown_Height;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Font;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_ForeColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_BackColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_TextColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_BorderColor;
        private System.Windows.Forms.CheckBox checkBoxIsBright;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_Maximun;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxStyles;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxTextStyle;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.NumericUpDown numericUpDown_Minimun;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_Text;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ComboBox comboBox_Orientation;
        private System.Windows.Forms.Label label13;
    }
}
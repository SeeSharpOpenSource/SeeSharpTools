namespace SeeSharpTools.JY.GUI
{
    internal partial class ThermometerPorperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThermometerPorperty));
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_ForeColor = new System.Windows.Forms.Button();
            this.numericUpDown_BallSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button_Font = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button_LineColor = new System.Windows.Forms.Button();
            this.numericUpDown_LineWidth = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDown_TickWidth = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button_TickColor = new System.Windows.Forms.Button();
            this.comboBox_TickStyle = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonTextColor = new System.Windows.Forms.Button();
            this.comboBox_TextStyle = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.numericUpDown_TextDecimals = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.numericUp_DownDivisions = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.numericUpDown_Minimun = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.numericUpDown_Maximun = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BallSize)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineWidth)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TickWidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TextDecimals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUp_DownDivisions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Minimun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Maximun)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.button_ForeColor);
            this.groupBox6.Controls.Add(this.numericUpDown_BallSize);
            this.groupBox6.Location = new System.Drawing.Point(9, 15);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(169, 72);
            this.groupBox6.TabIndex = 25;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Ball";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "BallSize]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "ForeColor";
            // 
            // button_ForeColor
            // 
            this.button_ForeColor.BackColor = System.Drawing.Color.Black;
            this.button_ForeColor.Location = new System.Drawing.Point(106, 38);
            this.button_ForeColor.Name = "button_ForeColor";
            this.button_ForeColor.Size = new System.Drawing.Size(22, 22);
            this.button_ForeColor.TabIndex = 30;
            this.button_ForeColor.UseVisualStyleBackColor = false;
            this.button_ForeColor.Click += new System.EventHandler(this.button_ForeColor_Click);
            // 
            // numericUpDown_BallSize
            // 
            this.numericUpDown_BallSize.Location = new System.Drawing.Point(7, 39);
            this.numericUpDown_BallSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_BallSize.Name = "numericUpDown_BallSize";
            this.numericUpDown_BallSize.Size = new System.Drawing.Size(63, 21);
            this.numericUpDown_BallSize.TabIndex = 3;
            this.numericUpDown_BallSize.ValueChanged += new System.EventHandler(this.numericUpDown_BallSize_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button_Font);
            this.groupBox4.Location = new System.Drawing.Point(184, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(95, 72);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "FontSetting";
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.button_LineColor);
            this.groupBox3.Controls.Add(this.numericUpDown_LineWidth);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(9, 241);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 60);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Line Setting";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(195, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 40;
            this.label16.Text = "Line Color";
            // 
            // button_LineColor
            // 
            this.button_LineColor.BackColor = System.Drawing.Color.Black;
            this.button_LineColor.Location = new System.Drawing.Point(211, 32);
            this.button_LineColor.Name = "button_LineColor";
            this.button_LineColor.Size = new System.Drawing.Size(22, 22);
            this.button_LineColor.TabIndex = 13;
            this.button_LineColor.UseVisualStyleBackColor = false;
            this.button_LineColor.Click += new System.EventHandler(this.button_LineColor_Click);
            // 
            // numericUpDown_LineWidth
            // 
            this.numericUpDown_LineWidth.Location = new System.Drawing.Point(18, 35);
            this.numericUpDown_LineWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_LineWidth.Name = "numericUpDown_LineWidth";
            this.numericUpDown_LineWidth.Size = new System.Drawing.Size(63, 21);
            this.numericUpDown_LineWidth.TabIndex = 5;
            this.numericUpDown_LineWidth.ValueChanged += new System.EventHandler(this.numericUpDown_LineWidth_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "Line Width";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.numericUpDown_TickWidth);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.button_TickColor);
            this.groupBox7.Controls.Add(this.comboBox_TickStyle);
            this.groupBox7.Location = new System.Drawing.Point(9, 171);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(270, 64);
            this.groupBox7.TabIndex = 28;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Tick Setting";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(123, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 41;
            this.label17.Text = "Tick Width";
            // 
            // numericUpDown_TickWidth
            // 
            this.numericUpDown_TickWidth.Location = new System.Drawing.Point(127, 33);
            this.numericUpDown_TickWidth.Name = "numericUpDown_TickWidth";
            this.numericUpDown_TickWidth.Size = new System.Drawing.Size(61, 21);
            this.numericUpDown_TickWidth.TabIndex = 40;
            this.numericUpDown_TickWidth.ValueChanged += new System.EventHandler(this.numericUpDown_TickWidth_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(195, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 39;
            this.label15.Text = "Tick Color";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 14;
            this.label13.Text = "Tick Style";
            // 
            // button_TickColor
            // 
            this.button_TickColor.BackColor = System.Drawing.Color.Black;
            this.button_TickColor.Location = new System.Drawing.Point(211, 31);
            this.button_TickColor.Name = "button_TickColor";
            this.button_TickColor.Size = new System.Drawing.Size(22, 22);
            this.button_TickColor.TabIndex = 12;
            this.button_TickColor.UseVisualStyleBackColor = false;
            this.button_TickColor.Click += new System.EventHandler(this.button_TickColor_Click);
            // 
            // comboBox_TickStyle
            // 
            this.comboBox_TickStyle.FormattingEnabled = true;
            this.comboBox_TickStyle.Location = new System.Drawing.Point(6, 33);
            this.comboBox_TickStyle.Name = "comboBox_TickStyle";
            this.comboBox_TickStyle.Size = new System.Drawing.Size(104, 20);
            this.comboBox_TickStyle.TabIndex = 5;
            this.comboBox_TickStyle.SelectedIndexChanged += new System.EventHandler(this.comboBox_TickStyle_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.buttonTextColor);
            this.groupBox1.Controls.Add(this.comboBox_TextStyle);
            this.groupBox1.Location = new System.Drawing.Point(9, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 59);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Text Setting";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(195, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 38;
            this.label14.Text = "Text Color";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "Text Style";
            // 
            // buttonTextColor
            // 
            this.buttonTextColor.BackColor = System.Drawing.Color.Black;
            this.buttonTextColor.Location = new System.Drawing.Point(211, 31);
            this.buttonTextColor.Name = "buttonTextColor";
            this.buttonTextColor.Size = new System.Drawing.Size(22, 22);
            this.buttonTextColor.TabIndex = 12;
            this.buttonTextColor.UseVisualStyleBackColor = false;
            this.buttonTextColor.Click += new System.EventHandler(this.buttonTextColor_Click);
            // 
            // comboBox_TextStyle
            // 
            this.comboBox_TextStyle.FormattingEnabled = true;
            this.comboBox_TextStyle.Location = new System.Drawing.Point(6, 33);
            this.comboBox_TextStyle.Name = "comboBox_TextStyle";
            this.comboBox_TextStyle.Size = new System.Drawing.Size(104, 20);
            this.comboBox_TextStyle.TabIndex = 5;
            this.comboBox_TextStyle.SelectedIndexChanged += new System.EventHandler(this.comboBox_TextStyle_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(299, 345);
            this.tabControl1.TabIndex = 30;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(291, 319);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "layout";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.numericUpDown_TextDecimals);
            this.tabPage2.Controls.Add(this.label24);
            this.tabPage2.Controls.Add(this.numericUp_DownDivisions);
            this.tabPage2.Controls.Add(this.label25);
            this.tabPage2.Controls.Add(this.numericUpDown_Minimun);
            this.tabPage2.Controls.Add(this.label26);
            this.tabPage2.Controls.Add(this.numericUpDown_Maximun);
            this.tabPage2.Controls.Add(this.label27);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(291, 319);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_TextDecimals
            // 
            this.numericUpDown_TextDecimals.Location = new System.Drawing.Point(11, 189);
            this.numericUpDown_TextDecimals.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_TextDecimals.Name = "numericUpDown_TextDecimals";
            this.numericUpDown_TextDecimals.Size = new System.Drawing.Size(85, 21);
            this.numericUpDown_TextDecimals.TabIndex = 43;
            this.numericUpDown_TextDecimals.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDown_TextDecimals.ValueChanged += new System.EventHandler(this.numericUpDown_TextDecimals_ValueChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.Location = new System.Drawing.Point(9, 174);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(83, 12);
            this.label24.TabIndex = 42;
            this.label24.Text = "Text Decimals";
            // 
            // numericUp_DownDivisions
            // 
            this.numericUp_DownDivisions.Location = new System.Drawing.Point(11, 109);
            this.numericUp_DownDivisions.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUp_DownDivisions.Name = "numericUp_DownDivisions";
            this.numericUp_DownDivisions.Size = new System.Drawing.Size(85, 21);
            this.numericUp_DownDivisions.TabIndex = 41;
            this.numericUp_DownDivisions.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUp_DownDivisions.ValueChanged += new System.EventHandler(this.numericUp_DownDivisions_ValueChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(9, 94);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(119, 12);
            this.label25.TabIndex = 40;
            this.label25.Text = "Number Of Divisions";
            // 
            // numericUpDown_Minimun
            // 
            this.numericUpDown_Minimun.Location = new System.Drawing.Point(123, 34);
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
            this.numericUpDown_Minimun.TabIndex = 39;
            this.numericUpDown_Minimun.ValueChanged += new System.EventHandler(this.numericUpDown_Minimun_ValueChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(121, 16);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 12);
            this.label26.TabIndex = 38;
            this.label26.Text = "Minimun";
            // 
            // numericUpDown_Maximun
            // 
            this.numericUpDown_Maximun.Location = new System.Drawing.Point(11, 34);
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
            this.numericUpDown_Maximun.TabIndex = 37;
            this.numericUpDown_Maximun.ValueChanged += new System.EventHandler(this.numericUpDown_Maximun_ValueChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.Location = new System.Drawing.Point(9, 16);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(47, 12);
            this.label27.TabIndex = 36;
            this.label27.Text = "Maximun";
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(167, 351);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 33;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Location = new System.Drawing.Point(47, 351);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(75, 23);
            this.button_Confirm.TabIndex = 32;
            this.button_Confirm.Text = "Confirm";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // ThermometerPorperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 385);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Confirm);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThermometerPorperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thermometer Porperty";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThermometerPorperty_FormClosing);
            this.Load += new System.EventHandler(this.ThermometerPorperty_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BallSize)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineWidth)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TickWidth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TextDecimals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUp_DownDivisions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Minimun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Maximun)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numericUpDown_BallSize;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button_Font;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button_LineColor;
        private System.Windows.Forms.NumericUpDown numericUpDown_LineWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericUpDown_TickWidth;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button_TickColor;
        private System.Windows.Forms.ComboBox comboBox_TickStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_ForeColor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonTextColor;
        private System.Windows.Forms.ComboBox comboBox_TextStyle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.NumericUpDown numericUpDown_TextDecimals;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown numericUp_DownDivisions;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.NumericUpDown numericUpDown_Minimun;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown numericUpDown_Maximun;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}
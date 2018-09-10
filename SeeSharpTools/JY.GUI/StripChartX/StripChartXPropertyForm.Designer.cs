namespace SeeSharpTools.JY.GUI
{
    partial class StripChartXPropertyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StripChartXPropertyForm));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox_LegendTransparent = new System.Windows.Forms.CheckBox();
            this.checkBox_AreaTransparent = new System.Windows.Forms.CheckBox();
            this.comboBox_GradientStyle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_LegendBackColor = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_ChartAreaBackColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button_BackColor = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_Width = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Height = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_LegendVisible = new System.Windows.Forms.CheckBox();
            this.checkBox_AutoYaxis = new System.Windows.Forms.CheckBox();
            this.Yaxis = new System.Windows.Forms.GroupBox();
            this.textBox_primaryYMin = new System.Windows.Forms.TextBox();
            this.textBox_primaryYMax = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox_XAxisLogarithmic = new System.Windows.Forms.CheckBox();
            this.checkBox_YAxisLogarithmic = new System.Windows.Forms.CheckBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.Yaxis.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.checkBox_LegendTransparent);
            this.groupBox5.Controls.Add(this.checkBox_AreaTransparent);
            this.groupBox5.Controls.Add(this.comboBox_GradientStyle);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.button_LegendBackColor);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.button_ChartAreaBackColor);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.button_BackColor);
            this.groupBox5.Location = new System.Drawing.Point(3, 190);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(284, 111);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Color";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "GradientStyle";
            // 
            // checkBox_LegendTransparent
            // 
            this.checkBox_LegendTransparent.AutoSize = true;
            this.checkBox_LegendTransparent.Location = new System.Drawing.Point(190, 65);
            this.checkBox_LegendTransparent.Name = "checkBox_LegendTransparent";
            this.checkBox_LegendTransparent.Size = new System.Drawing.Size(90, 16);
            this.checkBox_LegendTransparent.TabIndex = 27;
            this.checkBox_LegendTransparent.Text = "Transparent";
            this.checkBox_LegendTransparent.UseVisualStyleBackColor = true;
            this.checkBox_LegendTransparent.CheckedChanged += new System.EventHandler(this.checkBox_LegendTransparent_CheckedChanged);
            // 
            // checkBox_AreaTransparent
            // 
            this.checkBox_AreaTransparent.AutoSize = true;
            this.checkBox_AreaTransparent.Location = new System.Drawing.Point(99, 65);
            this.checkBox_AreaTransparent.Name = "checkBox_AreaTransparent";
            this.checkBox_AreaTransparent.Size = new System.Drawing.Size(90, 16);
            this.checkBox_AreaTransparent.TabIndex = 1;
            this.checkBox_AreaTransparent.Text = "Transparent";
            this.checkBox_AreaTransparent.UseVisualStyleBackColor = true;
            this.checkBox_AreaTransparent.CheckedChanged += new System.EventHandler(this.checkBox_AreaTransparent_CheckedChanged);
            // 
            // comboBox_GradientStyle
            // 
            this.comboBox_GradientStyle.FormattingEnabled = true;
            this.comboBox_GradientStyle.Location = new System.Drawing.Point(4, 85);
            this.comboBox_GradientStyle.Name = "comboBox_GradientStyle";
            this.comboBox_GradientStyle.Size = new System.Drawing.Size(88, 20);
            this.comboBox_GradientStyle.TabIndex = 26;
            this.comboBox_GradientStyle.SelectedIndexChanged += new System.EventHandler(this.comboBox_GradientStyle_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "Legend";
            // 
            // button_LegendBackColor
            // 
            this.button_LegendBackColor.BackColor = System.Drawing.Color.Transparent;
            this.button_LegendBackColor.Location = new System.Drawing.Point(194, 37);
            this.button_LegendBackColor.Name = "button_LegendBackColor";
            this.button_LegendBackColor.Size = new System.Drawing.Size(22, 22);
            this.button_LegendBackColor.TabIndex = 20;
            this.button_LegendBackColor.UseVisualStyleBackColor = false;
            this.button_LegendBackColor.Click += new System.EventHandler(this.button_LegendBackColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "ChartArea";
            // 
            // button_ChartAreaBackColor
            // 
            this.button_ChartAreaBackColor.BackColor = System.Drawing.Color.Transparent;
            this.button_ChartAreaBackColor.Location = new System.Drawing.Point(99, 37);
            this.button_ChartAreaBackColor.Name = "button_ChartAreaBackColor";
            this.button_ChartAreaBackColor.Size = new System.Drawing.Size(22, 22);
            this.button_ChartAreaBackColor.TabIndex = 18;
            this.button_ChartAreaBackColor.UseVisualStyleBackColor = false;
            this.button_ChartAreaBackColor.Click += new System.EventHandler(this.button_ChartAreaBackColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "BackColor";
            // 
            // button_BackColor
            // 
            this.button_BackColor.Location = new System.Drawing.Point(14, 37);
            this.button_BackColor.Name = "button_BackColor";
            this.button_BackColor.Size = new System.Drawing.Size(22, 22);
            this.button_BackColor.TabIndex = 17;
            this.button_BackColor.UseVisualStyleBackColor = true;
            this.button_BackColor.Click += new System.EventHandler(this.button_BackColor_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numericUpDown_Width);
            this.groupBox2.Controls.Add(this.numericUpDown_Height);
            this.groupBox2.Location = new System.Drawing.Point(9, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 72);
            this.groupBox2.TabIndex = 20;
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
            this.groupBox1.Controls.Add(this.checkBox_LegendVisible);
            this.groupBox1.Location = new System.Drawing.Point(184, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(103, 72);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LegendVisible";
            // 
            // checkBox_LegendVisible
            // 
            this.checkBox_LegendVisible.AutoSize = true;
            this.checkBox_LegendVisible.Location = new System.Drawing.Point(13, 45);
            this.checkBox_LegendVisible.Name = "checkBox_LegendVisible";
            this.checkBox_LegendVisible.Size = new System.Drawing.Size(84, 16);
            this.checkBox_LegendVisible.TabIndex = 23;
            this.checkBox_LegendVisible.Text = "IsViisible";
            this.checkBox_LegendVisible.UseVisualStyleBackColor = true;
            this.checkBox_LegendVisible.CheckedChanged += new System.EventHandler(this.checkBox_LegendVisible_CheckedChanged);
            // 
            // checkBox_AutoYaxis
            // 
            this.checkBox_AutoYaxis.AutoSize = true;
            this.checkBox_AutoYaxis.Location = new System.Drawing.Point(14, 20);
            this.checkBox_AutoYaxis.Name = "checkBox_AutoYaxis";
            this.checkBox_AutoYaxis.Size = new System.Drawing.Size(78, 16);
            this.checkBox_AutoYaxis.TabIndex = 24;
            this.checkBox_AutoYaxis.Text = "AutoYaxis";
            this.checkBox_AutoYaxis.UseVisualStyleBackColor = true;
            this.checkBox_AutoYaxis.CheckedChanged += new System.EventHandler(this.checkBox_AutoYaxis_CheckedChanged);
            // 
            // Yaxis
            // 
            this.Yaxis.Controls.Add(this.textBox_primaryYMin);
            this.Yaxis.Controls.Add(this.textBox_primaryYMax);
            this.Yaxis.Controls.Add(this.label8);
            this.Yaxis.Controls.Add(this.label7);
            this.Yaxis.Controls.Add(this.checkBox_AutoYaxis);
            this.Yaxis.Location = new System.Drawing.Point(3, 90);
            this.Yaxis.Name = "Yaxis";
            this.Yaxis.Size = new System.Drawing.Size(284, 94);
            this.Yaxis.TabIndex = 25;
            this.Yaxis.TabStop = false;
            this.Yaxis.Text = "Yaxis";
            // 
            // textBox_primaryYMin
            // 
            this.textBox_primaryYMin.Location = new System.Drawing.Point(159, 62);
            this.textBox_primaryYMin.Name = "textBox_primaryYMin";
            this.textBox_primaryYMin.Size = new System.Drawing.Size(111, 21);
            this.textBox_primaryYMin.TabIndex = 31;
            this.textBox_primaryYMin.Leave += new System.EventHandler(this.textBox_DoubleTextChanged);
            // 
            // textBox_primaryYMax
            // 
            this.textBox_primaryYMax.Location = new System.Drawing.Point(12, 62);
            this.textBox_primaryYMax.Name = "textBox_primaryYMax";
            this.textBox_primaryYMax.Size = new System.Drawing.Size(111, 21);
            this.textBox_primaryYMax.TabIndex = 30;
            this.textBox_primaryYMax.Leave += new System.EventHandler(this.textBox_DoubleTextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(162, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 29;
            this.label8.Text = "YaxisMin";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "YaxisMax";
            // 
            // checkBox_XAxisLogarithmic
            // 
            this.checkBox_XAxisLogarithmic.AutoSize = true;
            this.checkBox_XAxisLogarithmic.Location = new System.Drawing.Point(14, 307);
            this.checkBox_XAxisLogarithmic.Name = "checkBox_XAxisLogarithmic";
            this.checkBox_XAxisLogarithmic.Size = new System.Drawing.Size(120, 16);
            this.checkBox_XAxisLogarithmic.TabIndex = 26;
            this.checkBox_XAxisLogarithmic.Text = "XAxisLogarithmic";
            this.checkBox_XAxisLogarithmic.UseVisualStyleBackColor = true;
            this.checkBox_XAxisLogarithmic.CheckedChanged += new System.EventHandler(this.checkBox_XAxisLogarithmic_CheckedChanged);
            // 
            // checkBox_YAxisLogarithmic
            // 
            this.checkBox_YAxisLogarithmic.AutoSize = true;
            this.checkBox_YAxisLogarithmic.Location = new System.Drawing.Point(167, 307);
            this.checkBox_YAxisLogarithmic.Name = "checkBox_YAxisLogarithmic";
            this.checkBox_YAxisLogarithmic.Size = new System.Drawing.Size(120, 16);
            this.checkBox_YAxisLogarithmic.TabIndex = 27;
            this.checkBox_YAxisLogarithmic.Text = "YAxisLogarithmic";
            this.checkBox_YAxisLogarithmic.UseVisualStyleBackColor = true;
            this.checkBox_YAxisLogarithmic.CheckedChanged += new System.EventHandler(this.checkBox_YAxisLogarithmic_CheckedChanged);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(165, 344);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 36;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Location = new System.Drawing.Point(39, 344);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(75, 23);
            this.button_Confirm.TabIndex = 35;
            this.button_Confirm.Text = "Confirm";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // StripChartXPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 391);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Confirm);
            this.Controls.Add(this.checkBox_YAxisLogarithmic);
            this.Controls.Add(this.checkBox_XAxisLogarithmic);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Yaxis);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "StripChartXPropertyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StripChartX Property";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StripChartXProperty_FormClosing);
            this.Load += new System.EventHandler(this.StripChartProperty_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Yaxis.ResumeLayout(false);
            this.Yaxis.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_LegendBackColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_ChartAreaBackColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_BackColor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Width;
        private System.Windows.Forms.NumericUpDown numericUpDown_Height;
        private System.Windows.Forms.ComboBox comboBox_GradientStyle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_LegendVisible;
        private System.Windows.Forms.CheckBox checkBox_AutoYaxis;
        private System.Windows.Forms.GroupBox Yaxis;
        private System.Windows.Forms.CheckBox checkBox_XAxisLogarithmic;
        private System.Windows.Forms.CheckBox checkBox_YAxisLogarithmic;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.CheckBox checkBox_LegendTransparent;
        private System.Windows.Forms.CheckBox checkBox_AreaTransparent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_primaryYMin;
        private System.Windows.Forms.TextBox textBox_primaryYMax;
    }
}
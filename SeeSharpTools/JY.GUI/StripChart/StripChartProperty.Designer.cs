namespace SeeSharpTools.JY.GUI
{
    partial class StripChartProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StripChartProperty));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_LegendVisible = new System.Windows.Forms.CheckBox();
            this.checkBox_MinorGridEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_Width = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Height = new System.Windows.Forms.NumericUpDown();
            this.Yaxis = new System.Windows.Forms.GroupBox();
            this.checkBox_YAxisLogarithmic = new System.Windows.Forms.CheckBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label_TimeStampFormat = new System.Windows.Forms.Label();
            this.label_XAxisStartIndex = new System.Windows.Forms.Label();
            this.numericUpDown_XAxisStartIndex = new System.Windows.Forms.NumericUpDown();
            this.textBox_TimeStampFormat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_XAxisDataType = new System.Windows.Forms.ComboBox();
            this.checkBox_XAxisLogarithmic = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label_DisplayPoints = new System.Windows.Forms.Label();
            this.numericUpDown_DisplayPoints = new System.Windows.Forms.NumericUpDown();
            this.label_DisplayDirection = new System.Windows.Forms.Label();
            this.comboBox_DisplayDirection = new System.Windows.Forms.ComboBox();
            this.label_LineNum = new System.Windows.Forms.Label();
            this.numericUpDown_LineNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).BeginInit();
            this.Yaxis.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_XAxisStartIndex)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_DisplayPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineNum)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox_LegendVisible);
            this.groupBox5.Controls.Add(this.checkBox_MinorGridEnabled);
            this.groupBox5.Location = new System.Drawing.Point(4, 210);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(284, 43);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Appearence";
            // 
            // checkBox_LegendVisible
            // 
            this.checkBox_LegendVisible.AutoSize = true;
            this.checkBox_LegendVisible.Location = new System.Drawing.Point(158, 20);
            this.checkBox_LegendVisible.Name = "checkBox_LegendVisible";
            this.checkBox_LegendVisible.Size = new System.Drawing.Size(108, 16);
            this.checkBox_LegendVisible.TabIndex = 23;
            this.checkBox_LegendVisible.Text = "Legend Visible";
            this.checkBox_LegendVisible.UseVisualStyleBackColor = true;
            this.checkBox_LegendVisible.CheckedChanged += new System.EventHandler(this.checkBox_LegendVisible_CheckedChanged);
            // 
            // checkBox_MinorGridEnabled
            // 
            this.checkBox_MinorGridEnabled.AutoSize = true;
            this.checkBox_MinorGridEnabled.Location = new System.Drawing.Point(13, 20);
            this.checkBox_MinorGridEnabled.Name = "checkBox_MinorGridEnabled";
            this.checkBox_MinorGridEnabled.Size = new System.Drawing.Size(114, 16);
            this.checkBox_MinorGridEnabled.TabIndex = 24;
            this.checkBox_MinorGridEnabled.Text = "EnableMinorGrid";
            this.checkBox_MinorGridEnabled.UseVisualStyleBackColor = true;
            this.checkBox_MinorGridEnabled.CheckedChanged += new System.EventHandler(this.checkBox_MinorGridEnabled_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numericUpDown_Width);
            this.groupBox2.Controls.Add(this.numericUpDown_Height);
            this.groupBox2.Location = new System.Drawing.Point(9, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 53);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 29);
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
            this.numericUpDown_Width.Location = new System.Drawing.Point(191, 26);
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
            this.numericUpDown_Height.Location = new System.Drawing.Point(55, 26);
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
            // Yaxis
            // 
            this.Yaxis.Controls.Add(this.checkBox_YAxisLogarithmic);
            this.Yaxis.Location = new System.Drawing.Point(3, 71);
            this.Yaxis.Name = "Yaxis";
            this.Yaxis.Size = new System.Drawing.Size(284, 42);
            this.Yaxis.TabIndex = 25;
            this.Yaxis.TabStop = false;
            this.Yaxis.Text = "Yaxis";
            // 
            // checkBox_YAxisLogarithmic
            // 
            this.checkBox_YAxisLogarithmic.AutoSize = true;
            this.checkBox_YAxisLogarithmic.Location = new System.Drawing.Point(17, 20);
            this.checkBox_YAxisLogarithmic.Name = "checkBox_YAxisLogarithmic";
            this.checkBox_YAxisLogarithmic.Size = new System.Drawing.Size(120, 16);
            this.checkBox_YAxisLogarithmic.TabIndex = 27;
            this.checkBox_YAxisLogarithmic.Text = "YAxisLogarithmic";
            this.checkBox_YAxisLogarithmic.UseVisualStyleBackColor = true;
            this.checkBox_YAxisLogarithmic.CheckedChanged += new System.EventHandler(this.checkBox_YAxisLogarithmic_CheckedChanged);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(165, 381);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 36;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Location = new System.Drawing.Point(39, 381);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(75, 23);
            this.button_Confirm.TabIndex = 35;
            this.button_Confirm.Text = "Confirm";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label_TimeStampFormat);
            this.groupBox4.Controls.Add(this.label_XAxisStartIndex);
            this.groupBox4.Controls.Add(this.numericUpDown_XAxisStartIndex);
            this.groupBox4.Controls.Add(this.textBox_TimeStampFormat);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.comboBox_XAxisDataType);
            this.groupBox4.Location = new System.Drawing.Point(3, 123);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(284, 80);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "XAxis";
            // 
            // label_TimeStampFormat
            // 
            this.label_TimeStampFormat.AutoSize = true;
            this.label_TimeStampFormat.Location = new System.Drawing.Point(18, 54);
            this.label_TimeStampFormat.Name = "label_TimeStampFormat";
            this.label_TimeStampFormat.Size = new System.Drawing.Size(71, 12);
            this.label_TimeStampFormat.TabIndex = 52;
            this.label_TimeStampFormat.Text = "Time Format";
            // 
            // label_XAxisStartIndex
            // 
            this.label_XAxisStartIndex.AutoSize = true;
            this.label_XAxisStartIndex.Location = new System.Drawing.Point(16, 54);
            this.label_XAxisStartIndex.Name = "label_XAxisStartIndex";
            this.label_XAxisStartIndex.Size = new System.Drawing.Size(71, 12);
            this.label_XAxisStartIndex.TabIndex = 51;
            this.label_XAxisStartIndex.Text = "Start Index";
            // 
            // numericUpDown_XAxisStartIndex
            // 
            this.numericUpDown_XAxisStartIndex.Location = new System.Drawing.Point(117, 50);
            this.numericUpDown_XAxisStartIndex.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDown_XAxisStartIndex.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
            this.numericUpDown_XAxisStartIndex.Name = "numericUpDown_XAxisStartIndex";
            this.numericUpDown_XAxisStartIndex.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_XAxisStartIndex.TabIndex = 6;
            this.numericUpDown_XAxisStartIndex.ValueChanged += new System.EventHandler(this.numericUpDown_XAxisStartIndex_ValueChanged);
            // 
            // textBox_TimeStampFormat
            // 
            this.textBox_TimeStampFormat.Location = new System.Drawing.Point(116, 49);
            this.textBox_TimeStampFormat.Name = "textBox_TimeStampFormat";
            this.textBox_TimeStampFormat.Size = new System.Drawing.Size(121, 21);
            this.textBox_TimeStampFormat.TabIndex = 5;
            this.textBox_TimeStampFormat.TextChanged += new System.EventHandler(this.textBox_TimeStampFormat_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "XAxisDataType";
            // 
            // comboBox_XAxisDataType
            // 
            this.comboBox_XAxisDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_XAxisDataType.FormattingEnabled = true;
            this.comboBox_XAxisDataType.Location = new System.Drawing.Point(116, 20);
            this.comboBox_XAxisDataType.Name = "comboBox_XAxisDataType";
            this.comboBox_XAxisDataType.Size = new System.Drawing.Size(121, 20);
            this.comboBox_XAxisDataType.TabIndex = 0;
            this.comboBox_XAxisDataType.SelectedIndexChanged += new System.EventHandler(this.comboBox_XAxisDataType_SelectedIndexChanged);
            // 
            // checkBox_XAxisLogarithmic
            // 
            this.checkBox_XAxisLogarithmic.AutoSize = true;
            this.checkBox_XAxisLogarithmic.Location = new System.Drawing.Point(20, 983);
            this.checkBox_XAxisLogarithmic.Name = "checkBox_XAxisLogarithmic";
            this.checkBox_XAxisLogarithmic.Size = new System.Drawing.Size(120, 16);
            this.checkBox_XAxisLogarithmic.TabIndex = 49;
            this.checkBox_XAxisLogarithmic.Text = "XAxisLogarithmic";
            this.checkBox_XAxisLogarithmic.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label_DisplayPoints);
            this.groupBox6.Controls.Add(this.numericUpDown_DisplayPoints);
            this.groupBox6.Controls.Add(this.label_DisplayDirection);
            this.groupBox6.Controls.Add(this.comboBox_DisplayDirection);
            this.groupBox6.Controls.Add(this.label_LineNum);
            this.groupBox6.Controls.Add(this.numericUpDown_LineNum);
            this.groupBox6.Location = new System.Drawing.Point(4, 255);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(284, 111);
            this.groupBox6.TabIndex = 51;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Data";
            // 
            // label_DisplayPoints
            // 
            this.label_DisplayPoints.AutoSize = true;
            this.label_DisplayPoints.Location = new System.Drawing.Point(18, 55);
            this.label_DisplayPoints.Name = "label_DisplayPoints";
            this.label_DisplayPoints.Size = new System.Drawing.Size(71, 12);
            this.label_DisplayPoints.TabIndex = 57;
            this.label_DisplayPoints.Text = "Point Count";
            // 
            // numericUpDown_DisplayPoints
            // 
            this.numericUpDown_DisplayPoints.Location = new System.Drawing.Point(112, 51);
            this.numericUpDown_DisplayPoints.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_DisplayPoints.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_DisplayPoints.Name = "numericUpDown_DisplayPoints";
            this.numericUpDown_DisplayPoints.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_DisplayPoints.TabIndex = 56;
            this.numericUpDown_DisplayPoints.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_DisplayPoints.ValueChanged += new System.EventHandler(this.numericUpDown_DisplayPoints_ValueChanged);
            // 
            // label_DisplayDirection
            // 
            this.label_DisplayDirection.AutoSize = true;
            this.label_DisplayDirection.Location = new System.Drawing.Point(19, 84);
            this.label_DisplayDirection.Name = "label_DisplayDirection";
            this.label_DisplayDirection.Size = new System.Drawing.Size(59, 12);
            this.label_DisplayDirection.TabIndex = 55;
            this.label_DisplayDirection.Text = "Direction";
            // 
            // comboBox_DisplayDirection
            // 
            this.comboBox_DisplayDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DisplayDirection.FormattingEnabled = true;
            this.comboBox_DisplayDirection.Location = new System.Drawing.Point(112, 81);
            this.comboBox_DisplayDirection.Name = "comboBox_DisplayDirection";
            this.comboBox_DisplayDirection.Size = new System.Drawing.Size(121, 20);
            this.comboBox_DisplayDirection.TabIndex = 54;
            this.comboBox_DisplayDirection.SelectedIndexChanged += new System.EventHandler(this.comboBox_DisplayDirection_SelectedIndexChanged);
            // 
            // label_LineNum
            // 
            this.label_LineNum.AutoSize = true;
            this.label_LineNum.Location = new System.Drawing.Point(18, 24);
            this.label_LineNum.Name = "label_LineNum";
            this.label_LineNum.Size = new System.Drawing.Size(65, 12);
            this.label_LineNum.TabIndex = 53;
            this.label_LineNum.Text = "Line Count";
            // 
            // numericUpDown_LineNum
            // 
            this.numericUpDown_LineNum.Location = new System.Drawing.Point(112, 20);
            this.numericUpDown_LineNum.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown_LineNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_LineNum.Name = "numericUpDown_LineNum";
            this.numericUpDown_LineNum.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_LineNum.TabIndex = 7;
            this.numericUpDown_LineNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_LineNum.ValueChanged += new System.EventHandler(this.numericUpDown_LineNum_ValueChanged);
            // 
            // StripChartProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 411);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.checkBox_XAxisLogarithmic);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Confirm);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Yaxis);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "StripChartProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StripChart Property";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StripChartProperty_FormClosing);
            this.Load += new System.EventHandler(this.StripChartProperty_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).EndInit();
            this.Yaxis.ResumeLayout(false);
            this.Yaxis.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_XAxisStartIndex)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_DisplayPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Width;
        private System.Windows.Forms.NumericUpDown numericUpDown_Height;
        private System.Windows.Forms.CheckBox checkBox_LegendVisible;
        private System.Windows.Forms.CheckBox checkBox_MinorGridEnabled;
        private System.Windows.Forms.GroupBox Yaxis;
        private System.Windows.Forms.CheckBox checkBox_YAxisLogarithmic;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBox_XAxisLogarithmic;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_XAxisDataType;
        private System.Windows.Forms.TextBox textBox_TimeStampFormat;
        private System.Windows.Forms.NumericUpDown numericUpDown_XAxisStartIndex;
        private System.Windows.Forms.Label label_XAxisStartIndex;
        private System.Windows.Forms.Label label_TimeStampFormat;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numericUpDown_LineNum;
        private System.Windows.Forms.Label label_LineNum;
        private System.Windows.Forms.ComboBox comboBox_DisplayDirection;
        private System.Windows.Forms.Label label_DisplayDirection;
        private System.Windows.Forms.Label label_DisplayPoints;
        private System.Windows.Forms.NumericUpDown numericUpDown_DisplayPoints;
    }
}
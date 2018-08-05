namespace SeeSharpTools.JY.GUI
{
    partial class Set_YAxis_Range
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Set_YAxis_Range));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownYMax = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownYMin = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMin)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "YAxis Maximum";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "YAxis Minimum";
            // 
            // numericUpDownYMax
            // 
            this.numericUpDownYMax.DecimalPlaces = 6;
            this.numericUpDownYMax.Location = new System.Drawing.Point(126, 33);
            this.numericUpDownYMax.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownYMax.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownYMax.Name = "numericUpDownYMax";
            this.numericUpDownYMax.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownYMax.TabIndex = 2;
            this.numericUpDownYMax.ThousandsSeparator = true;
            // 
            // numericUpDownYMin
            // 
            this.numericUpDownYMin.DecimalPlaces = 6;
            this.numericUpDownYMin.Location = new System.Drawing.Point(126, 73);
            this.numericUpDownYMin.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownYMin.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownYMin.Name = "numericUpDownYMin";
            this.numericUpDownYMin.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownYMin.TabIndex = 3;
            this.numericUpDownYMin.ThousandsSeparator = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(38, 146);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(161, 146);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownYMax);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownYMin);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 116);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "YAxis Range";
            // 
            // Set_YAxis_Range
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 191);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Set_YAxis_Range";
            this.Text = "Set_YAxis_Range";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYMin)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownYMax;
        private System.Windows.Forms.NumericUpDown numericUpDownYMin;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
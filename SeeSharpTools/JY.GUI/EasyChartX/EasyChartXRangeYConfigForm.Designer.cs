namespace SeeSharpTools.JY.GUI
{
    partial class EasyChartXRangeYConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasyChartXRangeYConfigForm));
            this.label_yRangeMax = new System.Windows.Forms.Label();
            this.label_yRangeMin = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox_rangeConfig = new System.Windows.Forms.GroupBox();
            this.textBox_primaryYMin = new System.Windows.Forms.TextBox();
            this.textBox_primaryYMax = new System.Windows.Forms.TextBox();
            this.groupBox_rangeConfig2 = new System.Windows.Forms.GroupBox();
            this.textBox_secondaryYMin = new System.Windows.Forms.TextBox();
            this.textBox_secondaryYMax = new System.Windows.Forms.TextBox();
            this.label_yRangeMax2 = new System.Windows.Forms.Label();
            this.label_yRangeMin2 = new System.Windows.Forms.Label();
            this.groupBox_rangeConfig.SuspendLayout();
            this.groupBox_rangeConfig2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_yRangeMax
            // 
            this.label_yRangeMax.AutoSize = true;
            this.label_yRangeMax.Location = new System.Drawing.Point(14, 37);
            this.label_yRangeMax.Name = "label_yRangeMax";
            this.label_yRangeMax.Size = new System.Drawing.Size(89, 12);
            this.label_yRangeMax.TabIndex = 0;
            this.label_yRangeMax.Text = "Y Axis Maximum";
            // 
            // label_yRangeMin
            // 
            this.label_yRangeMin.AutoSize = true;
            this.label_yRangeMin.Location = new System.Drawing.Point(14, 77);
            this.label_yRangeMin.Name = "label_yRangeMin";
            this.label_yRangeMin.Size = new System.Drawing.Size(89, 12);
            this.label_yRangeMin.TabIndex = 1;
            this.label_yRangeMin.Text = "Y Axis Minimum";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(126, 128);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(297, 128);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox_rangeConfig
            // 
            this.groupBox_rangeConfig.Controls.Add(this.textBox_primaryYMin);
            this.groupBox_rangeConfig.Controls.Add(this.textBox_primaryYMax);
            this.groupBox_rangeConfig.Controls.Add(this.label_yRangeMax);
            this.groupBox_rangeConfig.Controls.Add(this.label_yRangeMin);
            this.groupBox_rangeConfig.Location = new System.Drawing.Point(12, 7);
            this.groupBox_rangeConfig.Name = "groupBox_rangeConfig";
            this.groupBox_rangeConfig.Size = new System.Drawing.Size(231, 115);
            this.groupBox_rangeConfig.TabIndex = 6;
            this.groupBox_rangeConfig.TabStop = false;
            this.groupBox_rangeConfig.Text = "Y Axis Range";
            // 
            // textBox_primaryYMin
            // 
            this.textBox_primaryYMin.Location = new System.Drawing.Point(111, 72);
            this.textBox_primaryYMin.Name = "textBox_primaryYMin";
            this.textBox_primaryYMin.Size = new System.Drawing.Size(111, 21);
            this.textBox_primaryYMin.TabIndex = 3;
            this.textBox_primaryYMin.Leave += new System.EventHandler(this.textBox_DoubleTextChanged);
            // 
            // textBox_primaryYMax
            // 
            this.textBox_primaryYMax.Location = new System.Drawing.Point(111, 34);
            this.textBox_primaryYMax.Name = "textBox_primaryYMax";
            this.textBox_primaryYMax.Size = new System.Drawing.Size(111, 21);
            this.textBox_primaryYMax.TabIndex = 2;
            this.textBox_primaryYMax.Leave += new System.EventHandler(this.textBox_DoubleTextChanged);
            // 
            // groupBox_rangeConfig2
            // 
            this.groupBox_rangeConfig2.Controls.Add(this.textBox_secondaryYMin);
            this.groupBox_rangeConfig2.Controls.Add(this.textBox_secondaryYMax);
            this.groupBox_rangeConfig2.Controls.Add(this.label_yRangeMax2);
            this.groupBox_rangeConfig2.Controls.Add(this.label_yRangeMin2);
            this.groupBox_rangeConfig2.Location = new System.Drawing.Point(249, 7);
            this.groupBox_rangeConfig2.Name = "groupBox_rangeConfig2";
            this.groupBox_rangeConfig2.Size = new System.Drawing.Size(231, 115);
            this.groupBox_rangeConfig2.TabIndex = 7;
            this.groupBox_rangeConfig2.TabStop = false;
            this.groupBox_rangeConfig2.Text = "Y2 Axis Range";
            // 
            // textBox_secondaryYMin
            // 
            this.textBox_secondaryYMin.Location = new System.Drawing.Point(111, 72);
            this.textBox_secondaryYMin.Name = "textBox_secondaryYMin";
            this.textBox_secondaryYMin.Size = new System.Drawing.Size(111, 21);
            this.textBox_secondaryYMin.TabIndex = 5;
            this.textBox_secondaryYMin.Leave += new System.EventHandler(this.textBox_DoubleTextChanged);
            // 
            // textBox_secondaryYMax
            // 
            this.textBox_secondaryYMax.Location = new System.Drawing.Point(111, 34);
            this.textBox_secondaryYMax.Name = "textBox_secondaryYMax";
            this.textBox_secondaryYMax.Size = new System.Drawing.Size(111, 21);
            this.textBox_secondaryYMax.TabIndex = 4;
            this.textBox_secondaryYMax.Leave += new System.EventHandler(this.textBox_DoubleTextChanged);
            // 
            // label_yRangeMax2
            // 
            this.label_yRangeMax2.AutoSize = true;
            this.label_yRangeMax2.Location = new System.Drawing.Point(10, 37);
            this.label_yRangeMax2.Name = "label_yRangeMax2";
            this.label_yRangeMax2.Size = new System.Drawing.Size(95, 12);
            this.label_yRangeMax2.TabIndex = 0;
            this.label_yRangeMax2.Text = "Y2 Axis Maximum";
            // 
            // label_yRangeMin2
            // 
            this.label_yRangeMin2.AutoSize = true;
            this.label_yRangeMin2.Location = new System.Drawing.Point(10, 77);
            this.label_yRangeMin2.Name = "label_yRangeMin2";
            this.label_yRangeMin2.Size = new System.Drawing.Size(95, 12);
            this.label_yRangeMin2.TabIndex = 1;
            this.label_yRangeMin2.Text = "Y2 Axis Minimum";
            // 
            // EasyChartXRangeYConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 160);
            this.Controls.Add(this.groupBox_rangeConfig2);
            this.Controls.Add(this.groupBox_rangeConfig);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(510, 198);
            this.MinimumSize = new System.Drawing.Size(510, 198);
            this.Name = "EasyChartXRangeYConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Y Axis Range";
            this.groupBox_rangeConfig.ResumeLayout(false);
            this.groupBox_rangeConfig.PerformLayout();
            this.groupBox_rangeConfig2.ResumeLayout(false);
            this.groupBox_rangeConfig2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_yRangeMax;
        private System.Windows.Forms.Label label_yRangeMin;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox_rangeConfig;
        private System.Windows.Forms.GroupBox groupBox_rangeConfig2;
        private System.Windows.Forms.Label label_yRangeMax2;
        private System.Windows.Forms.Label label_yRangeMin2;
        private System.Windows.Forms.TextBox textBox_primaryYMax;
        private System.Windows.Forms.TextBox textBox_primaryYMin;
        private System.Windows.Forms.TextBox textBox_secondaryYMin;
        private System.Windows.Forms.TextBox textBox_secondaryYMax;
    }
}
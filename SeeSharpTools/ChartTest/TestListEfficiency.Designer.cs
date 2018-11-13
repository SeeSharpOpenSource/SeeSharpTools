namespace ChartTest
{
    partial class TestListEfficiency
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
            this.label_size = new System.Windows.Forms.Label();
            this.numericUpDown_size = new System.Windows.Forms.NumericUpDown();
            this.label_time = new System.Windows.Forms.Label();
            this.label_timeValue = new System.Windows.Forms.Label();
            this.label_usedMemValue = new System.Windows.Forms.Label();
            this.label_memoryUsed = new System.Windows.Forms.Label();
            this.label_allocatedMemValue = new System.Windows.Forms.Label();
            this.label_allocMem = new System.Windows.Forms.Label();
            this.button_new = new System.Windows.Forms.Button();
            this.button_GC = new System.Windows.Forms.Button();
            this.button_Range = new System.Windows.Forms.Button();
            this.button_ToArray = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_size)).BeginInit();
            this.SuspendLayout();
            // 
            // label_size
            // 
            this.label_size.AutoSize = true;
            this.label_size.Location = new System.Drawing.Point(76, 43);
            this.label_size.Name = "label_size";
            this.label_size.Size = new System.Drawing.Size(29, 12);
            this.label_size.TabIndex = 0;
            this.label_size.Text = "Size";
            // 
            // numericUpDown_size
            // 
            this.numericUpDown_size.Location = new System.Drawing.Point(141, 39);
            this.numericUpDown_size.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDown_size.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_size.Name = "numericUpDown_size";
            this.numericUpDown_size.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_size.TabIndex = 1;
            this.numericUpDown_size.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown_size.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Location = new System.Drawing.Point(76, 87);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(29, 12);
            this.label_time.TabIndex = 2;
            this.label_time.Text = "Time";
            // 
            // label_timeValue
            // 
            this.label_timeValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_timeValue.Location = new System.Drawing.Point(139, 83);
            this.label_timeValue.Name = "label_timeValue";
            this.label_timeValue.Size = new System.Drawing.Size(122, 21);
            this.label_timeValue.TabIndex = 3;
            this.label_timeValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_usedMemValue
            // 
            this.label_usedMemValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_usedMemValue.Location = new System.Drawing.Point(139, 130);
            this.label_usedMemValue.Name = "label_usedMemValue";
            this.label_usedMemValue.Size = new System.Drawing.Size(122, 21);
            this.label_usedMemValue.TabIndex = 5;
            this.label_usedMemValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_memoryUsed
            // 
            this.label_memoryUsed.AutoSize = true;
            this.label_memoryUsed.Location = new System.Drawing.Point(76, 134);
            this.label_memoryUsed.Name = "label_memoryUsed";
            this.label_memoryUsed.Size = new System.Drawing.Size(47, 12);
            this.label_memoryUsed.TabIndex = 4;
            this.label_memoryUsed.Text = "UsedMem";
            // 
            // label_allocatedMemValue
            // 
            this.label_allocatedMemValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_allocatedMemValue.Location = new System.Drawing.Point(139, 180);
            this.label_allocatedMemValue.Name = "label_allocatedMemValue";
            this.label_allocatedMemValue.Size = new System.Drawing.Size(122, 21);
            this.label_allocatedMemValue.TabIndex = 7;
            this.label_allocatedMemValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_allocMem
            // 
            this.label_allocMem.AutoSize = true;
            this.label_allocMem.Location = new System.Drawing.Point(76, 184);
            this.label_allocMem.Name = "label_allocMem";
            this.label_allocMem.Size = new System.Drawing.Size(53, 12);
            this.label_allocMem.TabIndex = 6;
            this.label_allocMem.Text = "AllocMem";
            // 
            // button_new
            // 
            this.button_new.Location = new System.Drawing.Point(345, 37);
            this.button_new.Name = "button_new";
            this.button_new.Size = new System.Drawing.Size(75, 23);
            this.button_new.TabIndex = 8;
            this.button_new.Text = "New";
            this.button_new.UseVisualStyleBackColor = true;
            this.button_new.Click += new System.EventHandler(this.button_run_Click);
            // 
            // button_GC
            // 
            this.button_GC.Location = new System.Drawing.Point(345, 189);
            this.button_GC.Name = "button_GC";
            this.button_GC.Size = new System.Drawing.Size(75, 23);
            this.button_GC.TabIndex = 9;
            this.button_GC.Text = "GC";
            this.button_GC.UseVisualStyleBackColor = true;
            this.button_GC.Click += new System.EventHandler(this.button_GC_Click);
            // 
            // button_Range
            // 
            this.button_Range.Location = new System.Drawing.Point(345, 75);
            this.button_Range.Name = "button_Range";
            this.button_Range.Size = new System.Drawing.Size(75, 23);
            this.button_Range.TabIndex = 10;
            this.button_Range.Text = "Range";
            this.button_Range.UseVisualStyleBackColor = true;
            this.button_Range.Click += new System.EventHandler(this.button_Range_Click);
            // 
            // button_ToArray
            // 
            this.button_ToArray.Location = new System.Drawing.Point(345, 113);
            this.button_ToArray.Name = "button_ToArray";
            this.button_ToArray.Size = new System.Drawing.Size(75, 23);
            this.button_ToArray.TabIndex = 11;
            this.button_ToArray.Text = "Array";
            this.button_ToArray.UseVisualStyleBackColor = true;
            this.button_ToArray.Click += new System.EventHandler(this.button_ToArray_Click);
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(345, 151);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(75, 23);
            this.button_delete.TabIndex = 12;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // TestListEfficiency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 227);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_ToArray);
            this.Controls.Add(this.button_Range);
            this.Controls.Add(this.button_GC);
            this.Controls.Add(this.button_new);
            this.Controls.Add(this.label_allocatedMemValue);
            this.Controls.Add(this.label_allocMem);
            this.Controls.Add(this.label_usedMemValue);
            this.Controls.Add(this.label_memoryUsed);
            this.Controls.Add(this.label_timeValue);
            this.Controls.Add(this.label_time);
            this.Controls.Add(this.numericUpDown_size);
            this.Controls.Add(this.label_size);
            this.Name = "TestListEfficiency";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestListEfficiency";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_size)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_size;
        private System.Windows.Forms.NumericUpDown numericUpDown_size;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label label_timeValue;
        private System.Windows.Forms.Label label_usedMemValue;
        private System.Windows.Forms.Label label_memoryUsed;
        private System.Windows.Forms.Label label_allocatedMemValue;
        private System.Windows.Forms.Label label_allocMem;
        private System.Windows.Forms.Button button_new;
        private System.Windows.Forms.Button button_GC;
        private System.Windows.Forms.Button button_Range;
        private System.Windows.Forms.Button button_ToArray;
        private System.Windows.Forms.Button button_delete;
    }
}
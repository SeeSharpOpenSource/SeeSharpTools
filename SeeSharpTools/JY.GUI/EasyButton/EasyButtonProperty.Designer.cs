namespace SeeSharpTools.JY.GUI
{
    partial class EasyButtonProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasyButtonProperty));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_Width = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Height = new System.Windows.Forms.NumericUpDown();
            this.presentImageCombox = new System.Windows.Forms.ComboBox();
            this.textBox_Text = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton_Imageright = new System.Windows.Forms.RadioButton();
            this.radioButton_ImageMiddle = new System.Windows.Forms.RadioButton();
            this.radioButton_Imageleft = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton_TextRight = new System.Windows.Forms.RadioButton();
            this.radioButton_TextMiddle = new System.Windows.Forms.RadioButton();
            this.radioButton_Textleft = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_Textcolor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Backcolor = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.groupBox2.TabIndex = 2;
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
            // presentImageCombox
            // 
            this.presentImageCombox.FormattingEnabled = true;
            this.presentImageCombox.Location = new System.Drawing.Point(6, 30);
            this.presentImageCombox.Name = "presentImageCombox";
            this.presentImageCombox.Size = new System.Drawing.Size(96, 20);
            this.presentImageCombox.TabIndex = 7;
            this.presentImageCombox.SelectedIndexChanged += new System.EventHandler(this.presentImageCombox_SelectedIndexChanged);
            // 
            // textBox_Text
            // 
            this.textBox_Text.Location = new System.Drawing.Point(2, 26);
            this.textBox_Text.Name = "textBox_Text";
            this.textBox_Text.Size = new System.Drawing.Size(100, 21);
            this.textBox_Text.TabIndex = 9;
            this.textBox_Text.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton_Imageright);
            this.groupBox3.Controls.Add(this.radioButton_ImageMiddle);
            this.groupBox3.Controls.Add(this.radioButton_Imageleft);
            this.groupBox3.Controls.Add(this.presentImageCombox);
            this.groupBox3.Location = new System.Drawing.Point(12, 93);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 65);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PresetImage";
            // 
            // radioButton_Imageright
            // 
            this.radioButton_Imageright.AutoSize = true;
            this.radioButton_Imageright.Location = new System.Drawing.Point(224, 31);
            this.radioButton_Imageright.Name = "radioButton_Imageright";
            this.radioButton_Imageright.Size = new System.Drawing.Size(53, 16);
            this.radioButton_Imageright.TabIndex = 14;
            this.radioButton_Imageright.TabStop = true;
            this.radioButton_Imageright.Text = "Right";
            this.radioButton_Imageright.UseVisualStyleBackColor = true;
            this.radioButton_Imageright.CheckedChanged += new System.EventHandler(this.radioButton_Image_CheckedChanged);
            // 
            // radioButton_ImageMiddle
            // 
            this.radioButton_ImageMiddle.AutoSize = true;
            this.radioButton_ImageMiddle.Location = new System.Drawing.Point(166, 31);
            this.radioButton_ImageMiddle.Name = "radioButton_ImageMiddle";
            this.radioButton_ImageMiddle.Size = new System.Drawing.Size(59, 16);
            this.radioButton_ImageMiddle.TabIndex = 13;
            this.radioButton_ImageMiddle.TabStop = true;
            this.radioButton_ImageMiddle.Text = "Middle";
            this.radioButton_ImageMiddle.UseVisualStyleBackColor = true;
            this.radioButton_ImageMiddle.CheckedChanged += new System.EventHandler(this.radioButton_Image_CheckedChanged);
            // 
            // radioButton_Imageleft
            // 
            this.radioButton_Imageleft.AutoSize = true;
            this.radioButton_Imageleft.Location = new System.Drawing.Point(120, 30);
            this.radioButton_Imageleft.Name = "radioButton_Imageleft";
            this.radioButton_Imageleft.Size = new System.Drawing.Size(47, 16);
            this.radioButton_Imageleft.TabIndex = 12;
            this.radioButton_Imageleft.TabStop = true;
            this.radioButton_Imageleft.Text = "Left";
            this.radioButton_Imageleft.UseVisualStyleBackColor = true;
            this.radioButton_Imageleft.CheckedChanged += new System.EventHandler(this.radioButton_Image_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton_TextRight);
            this.groupBox4.Controls.Add(this.radioButton_TextMiddle);
            this.groupBox4.Controls.Add(this.radioButton_Textleft);
            this.groupBox4.Controls.Add(this.textBox_Text);
            this.groupBox4.Location = new System.Drawing.Point(15, 164);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(278, 65);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Text";
            // 
            // radioButton_TextRight
            // 
            this.radioButton_TextRight.AutoSize = true;
            this.radioButton_TextRight.Location = new System.Drawing.Point(221, 30);
            this.radioButton_TextRight.Name = "radioButton_TextRight";
            this.radioButton_TextRight.Size = new System.Drawing.Size(53, 16);
            this.radioButton_TextRight.TabIndex = 14;
            this.radioButton_TextRight.TabStop = true;
            this.radioButton_TextRight.Text = "right";
            this.radioButton_TextRight.UseVisualStyleBackColor = true;
            this.radioButton_TextRight.CheckedChanged += new System.EventHandler(this.radioButton_Text_CheckedChanged);
            // 
            // radioButton_TextMiddle
            // 
            this.radioButton_TextMiddle.AutoSize = true;
            this.radioButton_TextMiddle.Location = new System.Drawing.Point(163, 30);
            this.radioButton_TextMiddle.Name = "radioButton_TextMiddle";
            this.radioButton_TextMiddle.Size = new System.Drawing.Size(59, 16);
            this.radioButton_TextMiddle.TabIndex = 13;
            this.radioButton_TextMiddle.TabStop = true;
            this.radioButton_TextMiddle.Text = "Middle";
            this.radioButton_TextMiddle.UseVisualStyleBackColor = true;
            this.radioButton_TextMiddle.CheckedChanged += new System.EventHandler(this.radioButton_Text_CheckedChanged);
            // 
            // radioButton_Textleft
            // 
            this.radioButton_Textleft.AutoSize = true;
            this.radioButton_Textleft.Location = new System.Drawing.Point(117, 30);
            this.radioButton_Textleft.Name = "radioButton_Textleft";
            this.radioButton_Textleft.Size = new System.Drawing.Size(47, 16);
            this.radioButton_Textleft.TabIndex = 12;
            this.radioButton_Textleft.TabStop = true;
            this.radioButton_Textleft.Text = "Left";
            this.radioButton_Textleft.UseVisualStyleBackColor = true;
            this.radioButton_Textleft.CheckedChanged += new System.EventHandler(this.radioButton_Text_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.button_Textcolor);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.button_Backcolor);
            this.groupBox5.Location = new System.Drawing.Point(12, 235);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(278, 65);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Color";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(182, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "ForeColor";
            // 
            // button_Textcolor
            // 
            this.button_Textcolor.Location = new System.Drawing.Point(198, 37);
            this.button_Textcolor.Name = "button_Textcolor";
            this.button_Textcolor.Size = new System.Drawing.Size(22, 22);
            this.button_Textcolor.TabIndex = 18;
            this.button_Textcolor.UseVisualStyleBackColor = true;
            this.button_Textcolor.Click += new System.EventHandler(this.button_Textcolor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "BackColor";
            // 
            // button_Backcolor
            // 
            this.button_Backcolor.Location = new System.Drawing.Point(47, 37);
            this.button_Backcolor.Name = "button_Backcolor";
            this.button_Backcolor.Size = new System.Drawing.Size(22, 22);
            this.button_Backcolor.TabIndex = 17;
            this.button_Backcolor.UseVisualStyleBackColor = true;
            this.button_Backcolor.Click += new System.EventHandler(this.ButtonPickColor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(188, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 72);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FontSetting";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(29, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 20);
            this.button1.TabIndex = 0;
            this.button1.Text = " ...";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Location = new System.Drawing.Point(49, 311);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(75, 23);
            this.button_Confirm.TabIndex = 18;
            this.button_Confirm.Text = "Confirm";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(185, 311);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 19;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // EasyButtonProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 347);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Confirm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(326, 385);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(326, 385);
            this.Name = "EasyButtonProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EasyButton Property";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EasyButtonProperty_FormClosing);
            this.Load += new System.EventHandler(this.EasyButtonProperty_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Width;
        private System.Windows.Forms.NumericUpDown numericUpDown_Height;
        private System.Windows.Forms.ComboBox presentImageCombox;
        private System.Windows.Forms.TextBox textBox_Text;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton_Imageright;
        private System.Windows.Forms.RadioButton radioButton_ImageMiddle;
        private System.Windows.Forms.RadioButton radioButton_Imageleft;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton_TextRight;
        private System.Windows.Forms.RadioButton radioButton_TextMiddle;
        private System.Windows.Forms.RadioButton radioButton_Textleft;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_Backcolor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_Textcolor;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.Button button_Cancel;
    }
}
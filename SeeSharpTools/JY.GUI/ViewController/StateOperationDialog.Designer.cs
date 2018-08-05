namespace SeeSharpTools.JY.GUI
{
    partial class StateOperationDialog
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
            this.textBox_stateName = new System.Windows.Forms.TextBox();
            this.label_stateName = new System.Windows.Forms.Label();
            this.button_confirm = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_stateName
            // 
            this.textBox_stateName.Location = new System.Drawing.Point(112, 35);
            this.textBox_stateName.MaxLength = 52;
            this.textBox_stateName.Name = "textBox_stateName";
            this.textBox_stateName.Size = new System.Drawing.Size(162, 21);
            this.textBox_stateName.TabIndex = 0;
            // 
            // label_stateName
            // 
            this.label_stateName.AutoSize = true;
            this.label_stateName.Location = new System.Drawing.Point(33, 39);
            this.label_stateName.Name = "label_stateName";
            this.label_stateName.Size = new System.Drawing.Size(71, 12);
            this.label_stateName.TabIndex = 1;
            this.label_stateName.Text = "State Name:";
            // 
            // button_confirm
            // 
            this.button_confirm.Location = new System.Drawing.Point(61, 81);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(75, 23);
            this.button_confirm.TabIndex = 2;
            this.button_confirm.Text = "Comfirm";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(166, 81);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // StateOperationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 122);
            this.ControlBox = false;
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.Controls.Add(this.label_stateName);
            this.Controls.Add(this.textBox_stateName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "StateOperationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View Controller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_stateName;
        private System.Windows.Forms.Label label_stateName;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.Button button_cancel;
    }
}
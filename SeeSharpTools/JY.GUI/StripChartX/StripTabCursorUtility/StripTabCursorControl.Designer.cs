using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI.StripTabCursorUtility
{
    partial class StripTabCursorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_view = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel_view
            // 
            this.panel_view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_view.BackColor = System.Drawing.Color.Red;
            this.panel_view.Location = new System.Drawing.Point(2, 0);
            this.panel_view.Name = "panel_view";
            this.panel_view.Size = new System.Drawing.Size(2, 197);
            this.panel_view.TabIndex = 0;
            this.panel_view.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FlowCursor_MouseDown);
            this.panel_view.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FlowCursor_MouseMove);
            this.panel_view.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FlowCursor_MouseUp);
            // 
            // TabCursorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel_view);
            this.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.Name = "StripTabCursorControl";
            this.Size = new System.Drawing.Size(6, 197);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FlowCursor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FlowCursor_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FlowCursor_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel_view;
    }
}

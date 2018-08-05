namespace SeeSharpTools.JY.GUI
{
    partial class PathControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_filePath = new System.Windows.Forms.TextBox();
            this.button_browse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_filePath
            // 
            this.textBox_filePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_filePath.Location = new System.Drawing.Point(0, 0);
            this.textBox_filePath.Multiline = true;
            this.textBox_filePath.Name = "textBox_filePath";
            this.textBox_filePath.Size = new System.Drawing.Size(459, 50);
            this.textBox_filePath.TabIndex = 0;
            this.textBox_filePath.Validated += new System.EventHandler(this.textBox_filePath_Validated);
            // 
            // button_browse
            // 
            this.button_browse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_browse.Location = new System.Drawing.Point(0, 0);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(37, 50);
            this.button_browse.TabIndex = 0;
            this.button_browse.Text = "...";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox_filePath);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button_browse);
            this.splitContainer1.Size = new System.Drawing.Size(500, 50);
            this.splitContainer1.SplitterDistance = 459;
            this.splitContainer1.TabIndex = 1;
            // 
            // FilePath
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FilePath";
            this.Size = new System.Drawing.Size(500, 50);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FilePath_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FilePath_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_filePath;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

namespace SeeSharpTools.JY.Graph3D
{
    partial class SurfaceGraph
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
            this.hScrollBar_Phi = new System.Windows.Forms.HScrollBar();
            this.vScrollBar_Rho = new System.Windows.Forms.VScrollBar();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown_distance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_rho = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_phi = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.checkBox_showCB = new System.Windows.Forms.CheckBox();
            this.checkBox_2DMode = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_distance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_rho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_phi)).BeginInit();
            this.SuspendLayout();
            // 
            // hScrollBar_Phi
            // 
            this.hScrollBar_Phi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar_Phi.LargeChange = 100;
            this.hScrollBar_Phi.Location = new System.Drawing.Point(0, 684);
            this.hScrollBar_Phi.Maximum = 18000;
            this.hScrollBar_Phi.Name = "hScrollBar_Phi";
            this.hScrollBar_Phi.Size = new System.Drawing.Size(871, 10);
            this.hScrollBar_Phi.TabIndex = 2;
            this.hScrollBar_Phi.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Phi_Scroll);
            // 
            // vScrollBar_Rho
            // 
            this.vScrollBar_Rho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar_Rho.Location = new System.Drawing.Point(871, 0);
            this.vScrollBar_Rho.Maximum = 18000;
            this.vScrollBar_Rho.Name = "vScrollBar_Rho";
            this.vScrollBar_Rho.Size = new System.Drawing.Size(10, 684);
            this.vScrollBar_Rho.TabIndex = 3;
            this.vScrollBar_Rho.Value = 4500;
            this.vScrollBar_Rho.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Rho_Scroll);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel.Controls.Add(this.vScrollBar_Rho, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.hScrollBar_Phi, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(501, 350);
            this.tableLayoutPanel.TabIndex = 4;
            // 
            // numericUpDown_distance
            // 
            this.numericUpDown_distance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_distance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown_distance.Location = new System.Drawing.Point(439, 351);
            this.numericUpDown_distance.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDown_distance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_distance.Name = "numericUpDown_distance";
            this.numericUpDown_distance.Size = new System.Drawing.Size(52, 21);
            this.numericUpDown_distance.TabIndex = 5;
            this.numericUpDown_distance.ValueChanged += new System.EventHandler(this.numericUpDown_distance_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 353);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Distance";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Rho";
            // 
            // numericUpDown_rho
            // 
            this.numericUpDown_rho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_rho.DecimalPlaces = 2;
            this.numericUpDown_rho.Font = new System.Drawing.Font("宋体", 9F);
            this.numericUpDown_rho.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_rho.Location = new System.Drawing.Point(328, 351);
            this.numericUpDown_rho.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDown_rho.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDown_rho.Name = "numericUpDown_rho";
            this.numericUpDown_rho.Size = new System.Drawing.Size(52, 21);
            this.numericUpDown_rho.TabIndex = 7;
            this.numericUpDown_rho.ValueChanged += new System.EventHandler(this.numericUpDown_rho_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Phi";
            // 
            // numericUpDown_phi
            // 
            this.numericUpDown_phi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_phi.DecimalPlaces = 2;
            this.numericUpDown_phi.Font = new System.Drawing.Font("宋体", 9F);
            this.numericUpDown_phi.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_phi.Location = new System.Drawing.Point(247, 351);
            this.numericUpDown_phi.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDown_phi.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDown_phi.Name = "numericUpDown_phi";
            this.numericUpDown_phi.Size = new System.Drawing.Size(52, 21);
            this.numericUpDown_phi.TabIndex = 9;
            this.numericUpDown_phi.ValueChanged += new System.EventHandler(this.numericUpDown_phi_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip1.Location = new System.Drawing.Point(0, 350);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(501, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // checkBox_showCB
            // 
            this.checkBox_showCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_showCB.AutoSize = true;
            this.checkBox_showCB.Location = new System.Drawing.Point(12, 353);
            this.checkBox_showCB.Name = "checkBox_showCB";
            this.checkBox_showCB.Size = new System.Drawing.Size(114, 16);
            this.checkBox_showCB.TabIndex = 11;
            this.checkBox_showCB.Text = "ColorBarVisible";
            this.checkBox_showCB.UseVisualStyleBackColor = true;
            this.checkBox_showCB.CheckedChanged += new System.EventHandler(this.checkBox_showCB_CheckedChanged);
            // 
            // checkBox_2DMode
            // 
            this.checkBox_2DMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_2DMode.AutoSize = true;
            this.checkBox_2DMode.Location = new System.Drawing.Point(142, 353);
            this.checkBox_2DMode.Name = "checkBox_2DMode";
            this.checkBox_2DMode.Size = new System.Drawing.Size(66, 16);
            this.checkBox_2DMode.TabIndex = 12;
            this.checkBox_2DMode.Text = "2D Mode";
            this.checkBox_2DMode.UseVisualStyleBackColor = true;
            this.checkBox_2DMode.CheckedChanged += new System.EventHandler(this.checkBox_2DMode_CheckedChanged);
            // 
            // SurfaceGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.checkBox_2DMode);
            this.Controls.Add(this.checkBox_showCB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown_phi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown_rho);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_distance);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.MinimumSize = new System.Drawing.Size(500, 370);
            this.Name = "SurfaceGraph";
            this.Size = new System.Drawing.Size(501, 372);
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_distance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_rho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_phi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.HScrollBar hScrollBar_Phi;
        private System.Windows.Forms.VScrollBar vScrollBar_Rho;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.NumericUpDown numericUpDown_distance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_rho;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_phi;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.CheckBox checkBox_showCB;
        private System.Windows.Forms.CheckBox checkBox_2DMode;
    }
}

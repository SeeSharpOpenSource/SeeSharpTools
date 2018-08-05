namespace SeeSharpTools.JY.GUI
{
    partial class StripChart
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_zoomX = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_zoomY = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_zoomWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_zoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_showValue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.legendVisibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.YAutoScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setYAxisRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SavePicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cursorSeriesRootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.contextMenuRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea1.AxisX.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(463, 354);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart";
            this.chart.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.chart_GetToolTipText);
            this.chart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart_AxisViewChanged);
            this.chart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_MouseDown);
            // 
            // contextMenuRightClick
            // 
            this.contextMenuRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_zoomX,
            this.ToolStripMenuItem_zoomY,
            this.ToolStripMenuItem_zoomWindow,
            this.toolStripMenuItem_zoomReset,
            this.ToolStripMenuItem_showValue,
            this.toolStripSeparator4,
            this.legendVisibleToolStripMenuItem,
            this.YAutoScaleToolStripMenuItem,
            this.setYAxisRangeToolStripMenuItem,
            this.toolStripSeparator1,
            this.SavePicToolStripMenuItem,
            this.saveAsCSVToolStripMenuItem,
            this.toolStripSeparator2,
            this.cursorSeriesRootMenuItem,
            this.toolStripSeparator3,
            this.clearToolStripMenuItem});
            this.contextMenuRightClick.Name = "contextMenuStripXYzoom";
            this.contextMenuRightClick.Size = new System.Drawing.Size(170, 314);
            // 
            // ToolStripMenuItem_zoomX
            // 
            this.ToolStripMenuItem_zoomX.Name = "ToolStripMenuItem_zoomX";
            this.ToolStripMenuItem_zoomX.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_zoomX.Text = "Zoom X Axis";
            this.ToolStripMenuItem_zoomX.Click += new System.EventHandler(this.ZoomMenuItemAction);
            // 
            // ToolStripMenuItem_zoomY
            // 
            this.ToolStripMenuItem_zoomY.Name = "ToolStripMenuItem_zoomY";
            this.ToolStripMenuItem_zoomY.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_zoomY.Text = "Zoom Y Axis";
            this.ToolStripMenuItem_zoomY.Click += new System.EventHandler(this.ZoomMenuItemAction);
            // 
            // ToolStripMenuItem_zoomWindow
            // 
            this.ToolStripMenuItem_zoomWindow.Name = "ToolStripMenuItem_zoomWindow";
            this.ToolStripMenuItem_zoomWindow.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_zoomWindow.Text = "Zoom Window";
            this.ToolStripMenuItem_zoomWindow.Click += new System.EventHandler(this.ZoomMenuItemAction);
            // 
            // toolStripMenuItem_zoomReset
            // 
            this.toolStripMenuItem_zoomReset.Name = "toolStripMenuItem_zoomReset";
            this.toolStripMenuItem_zoomReset.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem_zoomReset.Text = "Zoom Reset";
            this.toolStripMenuItem_zoomReset.Click += new System.EventHandler(this.toolStripMenuItem_zoomReset_Click);
            // 
            // ToolStripMenuItem_showValue
            // 
            this.ToolStripMenuItem_showValue.Name = "ToolStripMenuItem_showValue";
            this.ToolStripMenuItem_showValue.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_showValue.Text = "Show XYValue";
            this.ToolStripMenuItem_showValue.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(166, 6);
            // 
            // legendVisibleToolStripMenuItem
            // 
            this.legendVisibleToolStripMenuItem.Checked = true;
            this.legendVisibleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.legendVisibleToolStripMenuItem.Name = "legendVisibleToolStripMenuItem";
            this.legendVisibleToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.legendVisibleToolStripMenuItem.Text = "LegendVisible";
            this.legendVisibleToolStripMenuItem.Click += new System.EventHandler(this.legendVisibleToolStripMenuItem_Click);
            // 
            // YAutoScaleToolStripMenuItem
            // 
            this.YAutoScaleToolStripMenuItem.Checked = true;
            this.YAutoScaleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.YAutoScaleToolStripMenuItem.Name = "YAutoScaleToolStripMenuItem";
            this.YAutoScaleToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.YAutoScaleToolStripMenuItem.Text = "Auto YScale";
            this.YAutoScaleToolStripMenuItem.Click += new System.EventHandler(this.YAutoScaleToolStripMenuItem_Click);
            // 
            // setYAxisRangeToolStripMenuItem
            // 
            this.setYAxisRangeToolStripMenuItem.Name = "setYAxisRangeToolStripMenuItem";
            this.setYAxisRangeToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.setYAxisRangeToolStripMenuItem.Text = "Set YAxis Range";
            this.setYAxisRangeToolStripMenuItem.Visible = false;
            this.setYAxisRangeToolStripMenuItem.Click += new System.EventHandler(this.setYAxisRangeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // SavePicToolStripMenuItem
            // 
            this.SavePicToolStripMenuItem.Name = "SavePicToolStripMenuItem";
            this.SavePicToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.SavePicToolStripMenuItem.Text = "Save Picture";
            this.SavePicToolStripMenuItem.Visible = false;
            // 
            // saveAsCSVToolStripMenuItem
            // 
            this.saveAsCSVToolStripMenuItem.Name = "saveAsCSVToolStripMenuItem";
            this.saveAsCSVToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveAsCSVToolStripMenuItem.Text = "Save as CSV";
            this.saveAsCSVToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // cursorSeriesRootMenuItem
            // 
            this.cursorSeriesRootMenuItem.Name = "cursorSeriesRootMenuItem";
            this.cursorSeriesRootMenuItem.Size = new System.Drawing.Size(169, 22);
            this.cursorSeriesRootMenuItem.Text = "Cursor Series";
            this.cursorSeriesRootMenuItem.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(166, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // StripChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart);
            this.Name = "StripChart";
            this.Size = new System.Drawing.Size(463, 354);
            this.Load += new System.EventHandler(this.StripChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.contextMenuRightClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ContextMenuStrip contextMenuRightClick;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_showValue;
        private System.Windows.Forms.ToolStripMenuItem legendVisibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem YAutoScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SavePicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem setYAxisRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cursorSeriesRootMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_zoomX;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_zoomY;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_zoomWindow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_zoomReset;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    }
}

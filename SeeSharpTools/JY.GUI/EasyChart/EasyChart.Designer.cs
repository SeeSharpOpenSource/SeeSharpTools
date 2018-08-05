using System.Collections.Generic;

namespace SeeSharpTools.JY.GUI
{
    partial class EasyChart
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
            this._chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Zoom_XAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom_YAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom_WindowtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom_ResettoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Show_XYValuetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.legendVisibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.YAutoScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SavePicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.setYAxisRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_CursorSelect = new System.Windows.Forms.ToolStripSeparator();
            this.cursorSeriesRootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tt_xyValTips = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuLeftClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.curveColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineWidthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LinethinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LinemiddleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LinethickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interpolationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InterFastlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InterpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InterstepLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ponintStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStylenoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStylesquareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStylecircleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStylediamodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStyletriangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStylecrossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStylestart4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStylestart5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStylestart6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointStylestart10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InterLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this._chart)).BeginInit();
            this.contextMenuRightClick.SuspendLayout();
            this.contextMenuLeftClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // _chart
            // 
            chartArea1.Name = "ChartArea1";
            this._chart.ChartAreas.Add(chartArea1);
            this._chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend1";
            this._chart.Legends.Add(legend1);
            this._chart.Location = new System.Drawing.Point(0, 0);
            this._chart.Margin = new System.Windows.Forms.Padding(2);
            this._chart.Name = "_chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this._chart.Series.Add(series1);
            this._chart.Size = new System.Drawing.Size(450, 320);
            this._chart.TabIndex = 0;
            this._chart.Text = "chart1";
            this._chart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this._chart_AxisViewChanged);
            this._chart.MouseClick += new System.Windows.Forms.MouseEventHandler(this._chart_MouseClick);
            this._chart.MouseDown += new System.Windows.Forms.MouseEventHandler(this._chart_MouseDown);
            this._chart.MouseMove += new System.Windows.Forms.MouseEventHandler(this._chart_MouseMove);
            // 
            // contextMenuRightClick
            // 
            this.contextMenuRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Zoom_XAxisToolStripMenuItem,
            this.Zoom_YAxisToolStripMenuItem,
            this.Zoom_WindowtoolStripMenuItem,
            this.Zoom_ResettoolStripMenuItem,
            this.Show_XYValuetoolStripMenuItem,
            this.toolStripMenuItem1,
            this.legendVisibleToolStripMenuItem,
            this.YAutoScaleToolStripMenuItem,
            this.toolStripSeparator1,
            this.SavePicToolStripMenuItem,
            this.saveAsCSVToolStripMenuItem,
            this.toolStripSeparator2,
            this.setYAxisRangeToolStripMenuItem,
            this.toolStripSeparator_CursorSelect,
            this.cursorSeriesRootMenuItem,
            this.toolStripSeparator3});
            this.contextMenuRightClick.Name = "contextMenuStripXYzoom";
            this.contextMenuRightClick.Size = new System.Drawing.Size(170, 276);
            this.contextMenuRightClick.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuRightClick_ItemClicked);
            // 
            // Zoom_XAxisToolStripMenuItem
            // 
            this.Zoom_XAxisToolStripMenuItem.Checked = true;
            this.Zoom_XAxisToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Zoom_XAxisToolStripMenuItem.Name = "Zoom_XAxisToolStripMenuItem";
            this.Zoom_XAxisToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.Zoom_XAxisToolStripMenuItem.Text = "Zoom XAxis";
            this.Zoom_XAxisToolStripMenuItem.Click += new System.EventHandler(this.Zoom_XAxisToolStripMenuItem_Click);
            // 
            // Zoom_YAxisToolStripMenuItem
            // 
            this.Zoom_YAxisToolStripMenuItem.Name = "Zoom_YAxisToolStripMenuItem";
            this.Zoom_YAxisToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.Zoom_YAxisToolStripMenuItem.Text = "Zoom YAxis";
            this.Zoom_YAxisToolStripMenuItem.Click += new System.EventHandler(this.Zoom_YAxisToolStripMenuItem_Click);
            // 
            // Zoom_WindowtoolStripMenuItem
            // 
            this.Zoom_WindowtoolStripMenuItem.Name = "Zoom_WindowtoolStripMenuItem";
            this.Zoom_WindowtoolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.Zoom_WindowtoolStripMenuItem.Text = "Zoom Window";
            this.Zoom_WindowtoolStripMenuItem.Click += new System.EventHandler(this.Zoom_WindowtoolStripMenuItem_Click);
            // 
            // Zoom_ResettoolStripMenuItem
            // 
            this.Zoom_ResettoolStripMenuItem.Name = "Zoom_ResettoolStripMenuItem";
            this.Zoom_ResettoolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.Zoom_ResettoolStripMenuItem.Text = "Zoom Reset";
            this.Zoom_ResettoolStripMenuItem.Click += new System.EventHandler(this.Zoom_ResettoolStripMenuItem_Click);
            // 
            // Show_XYValuetoolStripMenuItem
            // 
            this.Show_XYValuetoolStripMenuItem.Name = "Show_XYValuetoolStripMenuItem";
            this.Show_XYValuetoolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.Show_XYValuetoolStripMenuItem.Text = "Show XYValue";
            this.Show_XYValuetoolStripMenuItem.Click += new System.EventHandler(this.Show_XYValuetoolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(166, 6);
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
            this.SavePicToolStripMenuItem.Click += new System.EventHandler(this.SavePicToolStripMenuItem_Click);
            // 
            // saveAsCSVToolStripMenuItem
            // 
            this.saveAsCSVToolStripMenuItem.Name = "saveAsCSVToolStripMenuItem";
            this.saveAsCSVToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveAsCSVToolStripMenuItem.Text = "Save as CSV";
            this.saveAsCSVToolStripMenuItem.Click += new System.EventHandler(this.saveAsCSVToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // setYAxisRangeToolStripMenuItem
            // 
            this.setYAxisRangeToolStripMenuItem.Name = "setYAxisRangeToolStripMenuItem";
            this.setYAxisRangeToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.setYAxisRangeToolStripMenuItem.Text = "Set YAxis Range";
            this.setYAxisRangeToolStripMenuItem.Click += new System.EventHandler(this.setYAxisRangeToolStripMenuItem_Click);
            // 
            // toolStripSeparator_CursorSelect
            // 
            this.toolStripSeparator_CursorSelect.Name = "toolStripSeparator_CursorSelect";
            this.toolStripSeparator_CursorSelect.Size = new System.Drawing.Size(166, 6);
            this.toolStripSeparator_CursorSelect.Visible = false;
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
            // 
            // contextMenuLeftClick
            // 
            this.contextMenuLeftClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.curveColorToolStripMenuItem,
            this.lineWidthToolStripMenuItem,
            this.interpolationToolStripMenuItem,
            this.ponintStyleToolStripMenuItem});
            this.contextMenuLeftClick.Name = "contextMenuLeftClick";
            this.contextMenuLeftClick.Size = new System.Drawing.Size(152, 92);
            // 
            // curveColorToolStripMenuItem
            // 
            this.curveColorToolStripMenuItem.Name = "curveColorToolStripMenuItem";
            this.curveColorToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.curveColorToolStripMenuItem.Text = "Curve Color";
            this.curveColorToolStripMenuItem.Click += new System.EventHandler(this.curveColorToolStripMenuItem_Click);
            // 
            // lineWidthToolStripMenuItem
            // 
            this.lineWidthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LinethinToolStripMenuItem,
            this.LinemiddleToolStripMenuItem,
            this.LinethickToolStripMenuItem});
            this.lineWidthToolStripMenuItem.Name = "lineWidthToolStripMenuItem";
            this.lineWidthToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.lineWidthToolStripMenuItem.Text = "Line Width";
            // 
            // LinethinToolStripMenuItem
            // 
            this.LinethinToolStripMenuItem.Name = "LinethinToolStripMenuItem";
            this.LinethinToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.LinethinToolStripMenuItem.Text = "Thin";
            this.LinethinToolStripMenuItem.Click += new System.EventHandler(this.LinethinToolStripMenuItem_Click);
            // 
            // LinemiddleToolStripMenuItem
            // 
            this.LinemiddleToolStripMenuItem.Name = "LinemiddleToolStripMenuItem";
            this.LinemiddleToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.LinemiddleToolStripMenuItem.Text = "Middle";
            this.LinemiddleToolStripMenuItem.Click += new System.EventHandler(this.LinemiddleToolStripMenuItem_Click);
            // 
            // LinethickToolStripMenuItem
            // 
            this.LinethickToolStripMenuItem.Name = "LinethickToolStripMenuItem";
            this.LinethickToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.LinethickToolStripMenuItem.Text = "Thick";
            this.LinethickToolStripMenuItem.Click += new System.EventHandler(this.LinethickToolStripMenuItem_Click);
            // 
            // interpolationToolStripMenuItem
            // 
            this.interpolationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InterFastlineToolStripMenuItem,
            this.InterpointToolStripMenuItem,
            this.InterstepLineToolStripMenuItem,
            this.InterLineToolStripMenuItem});
            this.interpolationToolStripMenuItem.Name = "interpolationToolStripMenuItem";
            this.interpolationToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.interpolationToolStripMenuItem.Text = "Interpolation";
            // 
            // InterFastlineToolStripMenuItem
            // 
            this.InterFastlineToolStripMenuItem.Name = "InterFastlineToolStripMenuItem";
            this.InterFastlineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.InterFastlineToolStripMenuItem.Text = "Fast Line";
            this.InterFastlineToolStripMenuItem.Click += new System.EventHandler(this.InterFastlineToolStripMenuItem_Click);
            // 
            // InterpointToolStripMenuItem
            // 
            this.InterpointToolStripMenuItem.Name = "InterpointToolStripMenuItem";
            this.InterpointToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.InterpointToolStripMenuItem.Text = "Point";
            this.InterpointToolStripMenuItem.Click += new System.EventHandler(this.InterpointToolStripMenuItem_Click);
            // 
            // InterstepLineToolStripMenuItem
            // 
            this.InterstepLineToolStripMenuItem.Name = "InterstepLineToolStripMenuItem";
            this.InterstepLineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.InterstepLineToolStripMenuItem.Text = "Step Line";
            this.InterstepLineToolStripMenuItem.Click += new System.EventHandler(this.InterstepLineToolStripMenuItem_Click);
            // 
            // ponintStyleToolStripMenuItem
            // 
            this.ponintStyleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PointStylenoneToolStripMenuItem,
            this.PointStylesquareToolStripMenuItem,
            this.PointStylecircleToolStripMenuItem,
            this.PointStylediamodToolStripMenuItem,
            this.PointStyletriangleToolStripMenuItem,
            this.PointStylecrossToolStripMenuItem,
            this.PointStylestart4ToolStripMenuItem,
            this.PointStylestart5ToolStripMenuItem,
            this.PointStylestart6ToolStripMenuItem,
            this.PointStylestart10ToolStripMenuItem});
            this.ponintStyleToolStripMenuItem.Name = "ponintStyleToolStripMenuItem";
            this.ponintStyleToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.ponintStyleToolStripMenuItem.Text = "Ponit Style";
            // 
            // PointStylenoneToolStripMenuItem
            // 
            this.PointStylenoneToolStripMenuItem.Name = "PointStylenoneToolStripMenuItem";
            this.PointStylenoneToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStylenoneToolStripMenuItem.Text = "None";
            this.PointStylenoneToolStripMenuItem.Click += new System.EventHandler(this.PointStylenoneToolStripMenuItem_Click);
            // 
            // PointStylesquareToolStripMenuItem
            // 
            this.PointStylesquareToolStripMenuItem.Name = "PointStylesquareToolStripMenuItem";
            this.PointStylesquareToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStylesquareToolStripMenuItem.Text = "Square";
            this.PointStylesquareToolStripMenuItem.Click += new System.EventHandler(this.PointStylesquareToolStripMenuItem_Click);
            // 
            // PointStylecircleToolStripMenuItem
            // 
            this.PointStylecircleToolStripMenuItem.Name = "PointStylecircleToolStripMenuItem";
            this.PointStylecircleToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStylecircleToolStripMenuItem.Text = "Circle";
            this.PointStylecircleToolStripMenuItem.Click += new System.EventHandler(this.PointStylecircleToolStripMenuItem_Click);
            // 
            // PointStylediamodToolStripMenuItem
            // 
            this.PointStylediamodToolStripMenuItem.Name = "PointStylediamodToolStripMenuItem";
            this.PointStylediamodToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStylediamodToolStripMenuItem.Text = "Diamod";
            this.PointStylediamodToolStripMenuItem.Click += new System.EventHandler(this.PointStylediamodToolStripMenuItem_Click);
            // 
            // PointStyletriangleToolStripMenuItem
            // 
            this.PointStyletriangleToolStripMenuItem.Name = "PointStyletriangleToolStripMenuItem";
            this.PointStyletriangleToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStyletriangleToolStripMenuItem.Text = "Triangle";
            this.PointStyletriangleToolStripMenuItem.Click += new System.EventHandler(this.PointStyletriangleToolStripMenuItem_Click);
            // 
            // PointStylecrossToolStripMenuItem
            // 
            this.PointStylecrossToolStripMenuItem.Name = "PointStylecrossToolStripMenuItem";
            this.PointStylecrossToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStylecrossToolStripMenuItem.Text = "Cross";
            this.PointStylecrossToolStripMenuItem.Click += new System.EventHandler(this.PointStylecrossToolStripMenuItem_Click);
            // 
            // PointStylestart4ToolStripMenuItem
            // 
            this.PointStylestart4ToolStripMenuItem.Name = "PointStylestart4ToolStripMenuItem";
            this.PointStylestart4ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStylestart4ToolStripMenuItem.Text = "Start4";
            this.PointStylestart4ToolStripMenuItem.Click += new System.EventHandler(this.PointStylestart4ToolStripMenuItem_Click);
            // 
            // PointStylestart5ToolStripMenuItem
            // 
            this.PointStylestart5ToolStripMenuItem.Name = "PointStylestart5ToolStripMenuItem";
            this.PointStylestart5ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStylestart5ToolStripMenuItem.Text = "Start5";
            this.PointStylestart5ToolStripMenuItem.Click += new System.EventHandler(this.PointStylestart5ToolStripMenuItem_Click);
            // 
            // PointStylestart6ToolStripMenuItem
            // 
            this.PointStylestart6ToolStripMenuItem.Name = "PointStylestart6ToolStripMenuItem";
            this.PointStylestart6ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStylestart6ToolStripMenuItem.Text = "Start6";
            this.PointStylestart6ToolStripMenuItem.Click += new System.EventHandler(this.PointStylestart6ToolStripMenuItem_Click);
            // 
            // PointStylestart10ToolStripMenuItem
            // 
            this.PointStylestart10ToolStripMenuItem.Name = "PointStylestart10ToolStripMenuItem";
            this.PointStylestart10ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.PointStylestart10ToolStripMenuItem.Text = "Start10";
            this.PointStylestart10ToolStripMenuItem.Click += new System.EventHandler(this.PointStylestart10ToolStripMenuItem_Click);
            // 
            // InterLineToolStripMenuItem
            // 
            this.InterLineToolStripMenuItem.Name = "InterLineToolStripMenuItem";
            this.InterLineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.InterLineToolStripMenuItem.Text = "Line";
            this.InterLineToolStripMenuItem.Click += new System.EventHandler(this.InterLineToolStripMenuItem_Click);
            // 
            // EasyChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._chart);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EasyChart";
            this.Size = new System.Drawing.Size(450, 320);
            ((System.ComponentModel.ISupportInitialize)(this._chart)).EndInit();
            this.contextMenuRightClick.ResumeLayout(false);
            this.contextMenuLeftClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart _chart;
        private System.Windows.Forms.ContextMenuStrip contextMenuRightClick;
        private System.Windows.Forms.ToolStripMenuItem Zoom_XAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Zoom_YAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SavePicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem YAutoScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Zoom_WindowtoolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveAsCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem legendVisibleToolStripMenuItem;
        private System.Windows.Forms.ToolTip tt_xyValTips;
        private System.Windows.Forms.ToolStripMenuItem Show_XYValuetoolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuLeftClick;
        private System.Windows.Forms.ToolStripMenuItem curveColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineWidthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LinethinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LinemiddleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LinethickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interpolationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InterpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InterFastlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InterstepLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ponintStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylenoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylesquareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylecircleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylediamodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStyletriangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylestart4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylecrossToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem Zoom_ResettoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setYAxisRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_CursorSelect;
        private System.Windows.Forms.ToolStripMenuItem cursorSeriesRootMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem PointStylestart5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylestart6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylestart10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InterLineToolStripMenuItem;
    }
}

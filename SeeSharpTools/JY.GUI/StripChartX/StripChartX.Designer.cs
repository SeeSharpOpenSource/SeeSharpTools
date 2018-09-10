using System.Collections.Generic;

namespace SeeSharpTools.JY.GUI
{
    partial class StripChartX
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
            this.ChartFunctionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_xAxisZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_yAxisZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_windowZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_zoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_showValue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_view = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_legendVisible = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_splitView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_apperance = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_saveAsPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_saveAsCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_save = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_yAxisAutoScale = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_yAxisRangeConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_range = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_showSeriesParent = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_cursorSeriesParent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_series = new System.Windows.Forms.ToolStripSeparator();
            this.tabCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValueDisplayToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ChartSeriesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripTextBox_seriesName = new System.Windows.Forms.ToolStripTextBox();
            this.curveColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineWidthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LinethinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LinemiddleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LinethickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interpolationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InterFastlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InterpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InterstepLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InterLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolTip_tabCursorValue = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._chart)).BeginInit();
            this.ChartFunctionMenu.SuspendLayout();
            this.ChartSeriesMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _chart
            // 
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.Name = "ChartArea1";
            this._chart.ChartAreas.Add(chartArea1);
            this._chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend1";
            this._chart.Legends.Add(legend1);
            this._chart.Location = new System.Drawing.Point(0, 0);
            this._chart.Name = "_chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this._chart.Series.Add(series1);
            this._chart.Size = new System.Drawing.Size(450, 320);
            this._chart.TabIndex = 2;
            this._chart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.Chart_AxisViewChanged);
            this._chart.MouseDown += new System.Windows.Forms.MouseEventHandler(this._chart_MouseDown);
            // 
            // StripChartXFunctionMenu
            // 
            this.ChartFunctionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_xAxisZoom,
            this.ToolStripMenuItem_yAxisZoom,
            this.ToolStripMenuItem_windowZoom,
            this.ToolStripMenuItem_zoomReset,
            this.ToolStripMenuItem_showValue,
            this.toolStripMenuItem_view,
            this.ToolStripMenuItem_legendVisible,
            this.toolStripMenuItem_splitView,
            this.toolStripSeparator_apperance,
            this.ToolStripMenuItem_saveAsPicture,
            this.ToolStripMenuItem_saveAsCsv,
            this.toolStripSeparator_save,
            this.ToolStripMenuItem_yAxisAutoScale,
            this.ToolStripMenuItem_yAxisRangeConfig,
            this.toolStripSeparator_range,
            this.ToolStripMenuItem_showSeriesParent,
            this.ToolStripMenuItem_cursorSeriesParent,
            this.toolStripSeparator_series,
            this.tabCursorToolStripMenuItem});
            this.ChartFunctionMenu.Name = "contextMenuStripXYzoom";
            this.ChartFunctionMenu.Size = new System.Drawing.Size(170, 342);
            // 
            // ToolStripMenuItem_xAxisZoom
            // 
            this.ToolStripMenuItem_xAxisZoom.Checked = true;
            this.ToolStripMenuItem_xAxisZoom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_xAxisZoom.Name = "ToolStripMenuItem_xAxisZoom";
            this.ToolStripMenuItem_xAxisZoom.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_xAxisZoom.Text = "Zoom X Axis";
            this.ToolStripMenuItem_xAxisZoom.Click += new System.EventHandler(this.Zoom_XAxisToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_yAxisZoom
            // 
            this.ToolStripMenuItem_yAxisZoom.Name = "ToolStripMenuItem_yAxisZoom";
            this.ToolStripMenuItem_yAxisZoom.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_yAxisZoom.Text = "Zoom Y Axis";
            this.ToolStripMenuItem_yAxisZoom.Click += new System.EventHandler(this.Zoom_YAxisToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_windowZoom
            // 
            this.ToolStripMenuItem_windowZoom.Name = "ToolStripMenuItem_windowZoom";
            this.ToolStripMenuItem_windowZoom.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_windowZoom.Text = "Zoom Window";
            this.ToolStripMenuItem_windowZoom.Click += new System.EventHandler(this.Zoom_WindowtoolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_zoomReset
            // 
            this.ToolStripMenuItem_zoomReset.Name = "ToolStripMenuItem_zoomReset";
            this.ToolStripMenuItem_zoomReset.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_zoomReset.Text = "Zoom Reset";
            this.ToolStripMenuItem_zoomReset.Click += new System.EventHandler(this.Zoom_ResettoolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_showValue
            // 
            this.ToolStripMenuItem_showValue.Name = "ToolStripMenuItem_showValue";
            this.ToolStripMenuItem_showValue.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_showValue.Text = "Show Value";
            this.ToolStripMenuItem_showValue.Click += new System.EventHandler(this.Show_XYValuetoolStripMenuItem_Click);
            // 
            // toolStripMenuItem_view
            // 
            this.toolStripMenuItem_view.Name = "toolStripMenuItem_view";
            this.toolStripMenuItem_view.Size = new System.Drawing.Size(166, 6);
            // 
            // ToolStripMenuItem_legendVisible
            // 
            this.ToolStripMenuItem_legendVisible.Checked = true;
            this.ToolStripMenuItem_legendVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_legendVisible.Name = "ToolStripMenuItem_legendVisible";
            this.ToolStripMenuItem_legendVisible.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_legendVisible.Text = "Legend Visible";
            this.ToolStripMenuItem_legendVisible.Click += new System.EventHandler(this.legendVisibleToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_splitView
            // 
            this.toolStripMenuItem_splitView.Name = "toolStripMenuItem_splitView";
            this.toolStripMenuItem_splitView.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem_splitView.Text = "Split View";
            this.toolStripMenuItem_splitView.Click += new System.EventHandler(this.toolStripMenuItem_splitView_Click);
            // 
            // toolStripSeparator_apperance
            // 
            this.toolStripSeparator_apperance.Name = "toolStripSeparator_apperance";
            this.toolStripSeparator_apperance.Size = new System.Drawing.Size(166, 6);
            // 
            // ToolStripMenuItem_saveAsPicture
            // 
            this.ToolStripMenuItem_saveAsPicture.Name = "ToolStripMenuItem_saveAsPicture";
            this.ToolStripMenuItem_saveAsPicture.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_saveAsPicture.Text = "Save As Image";
            this.ToolStripMenuItem_saveAsPicture.Click += new System.EventHandler(this.SavePicToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_saveAsCsv
            // 
            this.ToolStripMenuItem_saveAsCsv.Name = "ToolStripMenuItem_saveAsCsv";
            this.ToolStripMenuItem_saveAsCsv.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_saveAsCsv.Text = "Save as Csv";
            this.ToolStripMenuItem_saveAsCsv.Click += new System.EventHandler(this.saveAsCSVToolStripMenuItem_Click);
            // 
            // toolStripSeparator_save
            // 
            this.toolStripSeparator_save.Name = "toolStripSeparator_save";
            this.toolStripSeparator_save.Size = new System.Drawing.Size(166, 6);
            // 
            // ToolStripMenuItem_yAxisAutoScale
            // 
            this.ToolStripMenuItem_yAxisAutoScale.Checked = true;
            this.ToolStripMenuItem_yAxisAutoScale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_yAxisAutoScale.Name = "ToolStripMenuItem_yAxisAutoScale";
            this.ToolStripMenuItem_yAxisAutoScale.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_yAxisAutoScale.Text = "Auto Y Range";
            this.ToolStripMenuItem_yAxisAutoScale.Click += new System.EventHandler(this.YAutoScaleToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_yAxisRangeConfig
            // 
            this.ToolStripMenuItem_yAxisRangeConfig.Name = "ToolStripMenuItem_yAxisRangeConfig";
            this.ToolStripMenuItem_yAxisRangeConfig.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_yAxisRangeConfig.Text = "Specify Y Range";
            this.ToolStripMenuItem_yAxisRangeConfig.Click += new System.EventHandler(this.setYAxisRangeToolStripMenuItem_Click);
            // 
            // toolStripSeparator_range
            // 
            this.toolStripSeparator_range.Name = "toolStripSeparator_range";
            this.toolStripSeparator_range.Size = new System.Drawing.Size(166, 6);
            // 
            // ToolStripMenuItem_showSeriesParent
            // 
            this.ToolStripMenuItem_showSeriesParent.Name = "ToolStripMenuItem_showSeriesParent";
            this.ToolStripMenuItem_showSeriesParent.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_showSeriesParent.Text = "Show Series";
            // 
            // ToolStripMenuItem_cursorSeriesParent
            // 
            this.ToolStripMenuItem_cursorSeriesParent.Name = "ToolStripMenuItem_cursorSeriesParent";
            this.ToolStripMenuItem_cursorSeriesParent.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItem_cursorSeriesParent.Text = "Cursor Series";
            // 
            // toolStripSeparator_series
            // 
            this.toolStripSeparator_series.Name = "toolStripSeparator_series";
            this.toolStripSeparator_series.Size = new System.Drawing.Size(166, 6);
            // 
            // tabCursorToolStripMenuItem
            // 
            this.tabCursorToolStripMenuItem.Name = "tabCursorToolStripMenuItem";
            this.tabCursorToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.tabCursorToolStripMenuItem.Text = "Tab Cursor";
            this.tabCursorToolStripMenuItem.Click += new System.EventHandler(this.tabCursorToolStripMenuItem_Click);
            // 
            // StripChartXSeriesMenu
            // 
            this.ChartSeriesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox_seriesName,
            this.curveColorToolStripMenuItem,
            this.lineWidthToolStripMenuItem,
            this.interpolationToolStripMenuItem,
            this.ponintStyleToolStripMenuItem});
            this.ChartSeriesMenu.Name = "contextMenuLeftClick";
            this.ChartSeriesMenu.Size = new System.Drawing.Size(161, 117);
            // 
            // toolStripTextBox_seriesName
            // 
            this.toolStripTextBox_seriesName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox_seriesName.Enabled = false;
            this.toolStripTextBox_seriesName.Name = "toolStripTextBox_seriesName";
            this.toolStripTextBox_seriesName.ReadOnly = true;
            this.toolStripTextBox_seriesName.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox_seriesName.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // curveColorToolStripMenuItem
            // 
            this.curveColorToolStripMenuItem.Name = "curveColorToolStripMenuItem";
            this.curveColorToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.curveColorToolStripMenuItem.Text = "Line Color";
            this.curveColorToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesLineColor);
            // 
            // lineWidthToolStripMenuItem
            // 
            this.lineWidthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LinethinToolStripMenuItem,
            this.LinemiddleToolStripMenuItem,
            this.LinethickToolStripMenuItem});
            this.lineWidthToolStripMenuItem.Name = "lineWidthToolStripMenuItem";
            this.lineWidthToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.lineWidthToolStripMenuItem.Text = "Line Width";
            // 
            // LinethinToolStripMenuItem
            // 
            this.LinethinToolStripMenuItem.Name = "LinethinToolStripMenuItem";
            this.LinethinToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.LinethinToolStripMenuItem.Tag = "Thin";
            this.LinethinToolStripMenuItem.Text = "Thin";
            this.LinethinToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesLineWidth);
            // 
            // LinemiddleToolStripMenuItem
            // 
            this.LinemiddleToolStripMenuItem.Name = "LinemiddleToolStripMenuItem";
            this.LinemiddleToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.LinemiddleToolStripMenuItem.Tag = "Middle";
            this.LinemiddleToolStripMenuItem.Text = "Middle";
            this.LinemiddleToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesLineWidth);
            // 
            // LinethickToolStripMenuItem
            // 
            this.LinethickToolStripMenuItem.Name = "LinethickToolStripMenuItem";
            this.LinethickToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.LinethickToolStripMenuItem.Tag = "Thick";
            this.LinethickToolStripMenuItem.Text = "Thick";
            this.LinethickToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesLineWidth);
            // 
            // interpolationToolStripMenuItem
            // 
            this.interpolationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InterFastlineToolStripMenuItem,
            this.InterpointToolStripMenuItem,
            this.InterstepLineToolStripMenuItem,
            this.InterLineToolStripMenuItem});
            this.interpolationToolStripMenuItem.Name = "interpolationToolStripMenuItem";
            this.interpolationToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.interpolationToolStripMenuItem.Text = "Line Type";
            // 
            // InterFastlineToolStripMenuItem
            // 
            this.InterFastlineToolStripMenuItem.Name = "InterFastlineToolStripMenuItem";
            this.InterFastlineToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.InterFastlineToolStripMenuItem.Tag = "FastLine";
            this.InterFastlineToolStripMenuItem.Text = "FastLine";
            this.InterFastlineToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesLineType);
            // 
            // InterpointToolStripMenuItem
            // 
            this.InterpointToolStripMenuItem.Name = "InterpointToolStripMenuItem";
            this.InterpointToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.InterpointToolStripMenuItem.Tag = "Point";
            this.InterpointToolStripMenuItem.Text = "Point";
            this.InterpointToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesLineType);
            // 
            // InterstepLineToolStripMenuItem
            // 
            this.InterstepLineToolStripMenuItem.Name = "InterstepLineToolStripMenuItem";
            this.InterstepLineToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.InterstepLineToolStripMenuItem.Tag = "StepLine";
            this.InterstepLineToolStripMenuItem.Text = "StepLine";
            this.InterstepLineToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesLineType);
            // 
            // InterLineToolStripMenuItem
            // 
            this.InterLineToolStripMenuItem.Name = "InterLineToolStripMenuItem";
            this.InterLineToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.InterLineToolStripMenuItem.Tag = "Line";
            this.InterLineToolStripMenuItem.Text = "Line";
            this.InterLineToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesLineType);
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
            this.ponintStyleToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ponintStyleToolStripMenuItem.Text = "Marker Type";
            // 
            // PointStylenoneToolStripMenuItem
            // 
            this.PointStylenoneToolStripMenuItem.Name = "PointStylenoneToolStripMenuItem";
            this.PointStylenoneToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStylenoneToolStripMenuItem.Tag = "None";
            this.PointStylenoneToolStripMenuItem.Text = "None";
            this.PointStylenoneToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // PointStylesquareToolStripMenuItem
            // 
            this.PointStylesquareToolStripMenuItem.Name = "PointStylesquareToolStripMenuItem";
            this.PointStylesquareToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStylesquareToolStripMenuItem.Tag = "Square";
            this.PointStylesquareToolStripMenuItem.Text = "Square";
            this.PointStylesquareToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // PointStylecircleToolStripMenuItem
            // 
            this.PointStylecircleToolStripMenuItem.Name = "PointStylecircleToolStripMenuItem";
            this.PointStylecircleToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStylecircleToolStripMenuItem.Tag = "Circle";
            this.PointStylecircleToolStripMenuItem.Text = "Circle";
            this.PointStylecircleToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // PointStylediamodToolStripMenuItem
            // 
            this.PointStylediamodToolStripMenuItem.Name = "PointStylediamodToolStripMenuItem";
            this.PointStylediamodToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStylediamodToolStripMenuItem.Tag = "Diamond";
            this.PointStylediamodToolStripMenuItem.Text = "Diamond";
            this.PointStylediamodToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // PointStyletriangleToolStripMenuItem
            // 
            this.PointStyletriangleToolStripMenuItem.Name = "PointStyletriangleToolStripMenuItem";
            this.PointStyletriangleToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStyletriangleToolStripMenuItem.Tag = "Triangle";
            this.PointStyletriangleToolStripMenuItem.Text = "Triangle";
            this.PointStyletriangleToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // PointStylecrossToolStripMenuItem
            // 
            this.PointStylecrossToolStripMenuItem.Name = "PointStylecrossToolStripMenuItem";
            this.PointStylecrossToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStylecrossToolStripMenuItem.Tag = "Cross";
            this.PointStylecrossToolStripMenuItem.Text = "Cross";
            this.PointStylecrossToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // PointStylestart4ToolStripMenuItem
            // 
            this.PointStylestart4ToolStripMenuItem.Name = "PointStylestart4ToolStripMenuItem";
            this.PointStylestart4ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStylestart4ToolStripMenuItem.Tag = "Star4";
            this.PointStylestart4ToolStripMenuItem.Text = "Star4";
            this.PointStylestart4ToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // PointStylestart5ToolStripMenuItem
            // 
            this.PointStylestart5ToolStripMenuItem.Name = "PointStylestart5ToolStripMenuItem";
            this.PointStylestart5ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStylestart5ToolStripMenuItem.Tag = "Star5";
            this.PointStylestart5ToolStripMenuItem.Text = "Star5";
            this.PointStylestart5ToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // PointStylestart6ToolStripMenuItem
            // 
            this.PointStylestart6ToolStripMenuItem.Name = "PointStylestart6ToolStripMenuItem";
            this.PointStylestart6ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStylestart6ToolStripMenuItem.Tag = "Star6";
            this.PointStylestart6ToolStripMenuItem.Text = "Star6";
            this.PointStylestart6ToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // PointStylestart10ToolStripMenuItem
            // 
            this.PointStylestart10ToolStripMenuItem.Name = "PointStylestart10ToolStripMenuItem";
            this.PointStylestart10ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.PointStylestart10ToolStripMenuItem.Tag = "Star10";
            this.PointStylestart10ToolStripMenuItem.Text = "Star10";
            this.PointStylestart10ToolStripMenuItem.Click += new System.EventHandler(this.SetSeriesMarkerType);
            // 
            // StripChartX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._chart);
            this.Name = "StripChartX";
            this.Size = new System.Drawing.Size(450, 320);
            ((System.ComponentModel.ISupportInitialize)(this._chart)).EndInit();
            this.ChartFunctionMenu.ResumeLayout(false);
            this.ChartSeriesMenu.ResumeLayout(false);
            this.ChartSeriesMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart _chart;
        private System.Windows.Forms.ContextMenuStrip ChartFunctionMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_view;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_apperance;
        private System.Windows.Forms.ToolTip ValueDisplayToolTip;
        private System.Windows.Forms.ContextMenuStrip ChartSeriesMenu;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_range;
        private System.Windows.Forms.ToolStripMenuItem PointStylestart5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylestart6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointStylestart10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InterLineToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_cursorSeriesParent;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_showSeriesParent;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_yAxisRangeConfig;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_yAxisAutoScale;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_saveAsCsv;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_saveAsPicture;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_legendVisible;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_showValue;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_zoomReset;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_windowZoom;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_yAxisZoom;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_xAxisZoom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_splitView;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_seriesName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_series;
        private System.Windows.Forms.ToolStripMenuItem tabCursorToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip_tabCursorValue;
    }
}

namespace ChartTest
{
    partial class TestStripChart
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
            this.components = new System.ComponentModel.Container();
            SeeSharpTools.JY.GUI.StripChartXSeries stripChartXSeries1 = new SeeSharpTools.JY.GUI.StripChartXSeries();
            SeeSharpTools.JY.GUI.StripChartXSeries stripChartXSeries2 = new SeeSharpTools.JY.GUI.StripChartXSeries();
            SeeSharpTools.JY.GUI.StripChartXSeries stripChartXSeries3 = new SeeSharpTools.JY.GUI.StripChartXSeries();
            SeeSharpTools.JY.GUI.StripChartXSeries stripChartXSeries4 = new SeeSharpTools.JY.GUI.StripChartXSeries();
            SeeSharpTools.JY.GUI.StripChartXSeries stripChartXSeries5 = new SeeSharpTools.JY.GUI.StripChartXSeries();
            this.button_single_dim1 = new System.Windows.Forms.Button();
            this.button_single_dim2 = new System.Windows.Forms.Button();
            this.button_continuous = new System.Windows.Forms.Button();
            this.button_multi_dim2 = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.numericUpDown_lineNum = new System.Windows.Forms.NumericUpDown();
            this.label_lineNum = new System.Windows.Forms.Label();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.label_type = new System.Windows.Forms.Label();
            this.timer_plot = new System.Windows.Forms.Timer(this.components);
            this.label_interval = new System.Windows.Forms.Label();
            this.numericUpDown_timeInterval = new System.Windows.Forms.NumericUpDown();
            this.label_time = new System.Windows.Forms.Label();
            this.stripChartX_test = new SeeSharpTools.JY.GUI.StripChartX();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_lineNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timeInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // button_single_dim1
            // 
            this.button_single_dim1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_single_dim1.Location = new System.Drawing.Point(56, 424);
            this.button_single_dim1.Margin = new System.Windows.Forms.Padding(2);
            this.button_single_dim1.Name = "button_single_dim1";
            this.button_single_dim1.Size = new System.Drawing.Size(127, 54);
            this.button_single_dim1.TabIndex = 1;
            this.button_single_dim1.Text = "SingleDim1";
            this.button_single_dim1.UseVisualStyleBackColor = true;
            this.button_single_dim1.Click += new System.EventHandler(this.button_single_dim1_Click);
            // 
            // button_single_dim2
            // 
            this.button_single_dim2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_single_dim2.Location = new System.Drawing.Point(222, 424);
            this.button_single_dim2.Margin = new System.Windows.Forms.Padding(2);
            this.button_single_dim2.Name = "button_single_dim2";
            this.button_single_dim2.Size = new System.Drawing.Size(127, 54);
            this.button_single_dim2.TabIndex = 2;
            this.button_single_dim2.Text = "SingleDim2";
            this.button_single_dim2.UseVisualStyleBackColor = true;
            this.button_single_dim2.Click += new System.EventHandler(this.button_single_dim2_Click);
            // 
            // button_continuous
            // 
            this.button_continuous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_continuous.Location = new System.Drawing.Point(389, 424);
            this.button_continuous.Margin = new System.Windows.Forms.Padding(2);
            this.button_continuous.Name = "button_continuous";
            this.button_continuous.Size = new System.Drawing.Size(127, 54);
            this.button_continuous.TabIndex = 3;
            this.button_continuous.Text = "Continuous";
            this.button_continuous.UseVisualStyleBackColor = true;
            this.button_continuous.Click += new System.EventHandler(this.button_multi_dim1_Click);
            // 
            // button_multi_dim2
            // 
            this.button_multi_dim2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_multi_dim2.Location = new System.Drawing.Point(552, 424);
            this.button_multi_dim2.Margin = new System.Windows.Forms.Padding(2);
            this.button_multi_dim2.Name = "button_multi_dim2";
            this.button_multi_dim2.Size = new System.Drawing.Size(127, 54);
            this.button_multi_dim2.TabIndex = 4;
            this.button_multi_dim2.Text = "MultiDim2";
            this.button_multi_dim2.UseVisualStyleBackColor = true;
            this.button_multi_dim2.Click += new System.EventHandler(this.button_multi_dim2_Click);
            // 
            // button_clear
            // 
            this.button_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_clear.Location = new System.Drawing.Point(722, 424);
            this.button_clear.Margin = new System.Windows.Forms.Padding(2);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(127, 54);
            this.button_clear.TabIndex = 5;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // numericUpDown_lineNum
            // 
            this.numericUpDown_lineNum.Location = new System.Drawing.Point(104, 394);
            this.numericUpDown_lineNum.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_lineNum.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown_lineNum.Name = "numericUpDown_lineNum";
            this.numericUpDown_lineNum.Size = new System.Drawing.Size(90, 21);
            this.numericUpDown_lineNum.TabIndex = 6;
            this.numericUpDown_lineNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_lineNum.ValueChanged += new System.EventHandler(this.numericUpDown_lineNum_ValueChanged);
            // 
            // label_lineNum
            // 
            this.label_lineNum.AutoSize = true;
            this.label_lineNum.Location = new System.Drawing.Point(38, 395);
            this.label_lineNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_lineNum.Name = "label_lineNum";
            this.label_lineNum.Size = new System.Drawing.Size(47, 12);
            this.label_lineNum.TabIndex = 7;
            this.label_lineNum.Text = "LineNum";
            // 
            // comboBox_type
            // 
            this.comboBox_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "TimeStamp",
            "Index"});
            this.comboBox_type.Location = new System.Drawing.Point(281, 394);
            this.comboBox_type.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(92, 20);
            this.comboBox_type.TabIndex = 8;
            this.comboBox_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_type_SelectedIndexChanged);
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Location = new System.Drawing.Point(218, 397);
            this.label_type.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(59, 12);
            this.label_type.TabIndex = 9;
            this.label_type.Text = "XDataType";
            // 
            // timer_plot
            // 
            this.timer_plot.Tick += new System.EventHandler(this.timer_plot_Tick);
            // 
            // label_interval
            // 
            this.label_interval.AutoSize = true;
            this.label_interval.Location = new System.Drawing.Point(400, 396);
            this.label_interval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_interval.Name = "label_interval";
            this.label_interval.Size = new System.Drawing.Size(101, 12);
            this.label_interval.TabIndex = 11;
            this.label_interval.Text = "TimeInterval(ms)";
            // 
            // numericUpDown_timeInterval
            // 
            this.numericUpDown_timeInterval.Location = new System.Drawing.Point(506, 393);
            this.numericUpDown_timeInterval.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_timeInterval.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown_timeInterval.Name = "numericUpDown_timeInterval";
            this.numericUpDown_timeInterval.Size = new System.Drawing.Size(90, 21);
            this.numericUpDown_timeInterval.TabIndex = 10;
            this.numericUpDown_timeInterval.ValueChanged += new System.EventHandler(this.numericUpDown_timeInterval_ValueChanged);
            // 
            // label_time
            // 
            this.label_time.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_time.Location = new System.Drawing.Point(634, 394);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(86, 23);
            this.label_time.TabIndex = 13;
            this.label_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stripChartX_test
            // 
            this.stripChartX_test.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stripChartX_test.AxisX.AutoScale = false;
            this.stripChartX_test.AxisX.AutoZoomReset = false;
            this.stripChartX_test.AxisX.Color = System.Drawing.Color.Black;
            this.stripChartX_test.AxisX.InitWithScaleView = false;
            this.stripChartX_test.AxisX.IsLogarithmic = false;
            this.stripChartX_test.AxisX.LabelEnabled = true;
            this.stripChartX_test.AxisX.LabelFormat = null;
            this.stripChartX_test.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.stripChartX_test.AxisX.MajorGridCount = 6;
            this.stripChartX_test.AxisX.MajorGridEnabled = true;
            this.stripChartX_test.AxisX.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.stripChartX_test.AxisX.Maximum = 1000D;
            this.stripChartX_test.AxisX.Minimum = 0D;
            this.stripChartX_test.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.stripChartX_test.AxisX.MinorGridEnabled = false;
            this.stripChartX_test.AxisX.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.stripChartX_test.AxisX.TickWidth = 1F;
            this.stripChartX_test.AxisX.Title = "";
            this.stripChartX_test.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.stripChartX_test.AxisX.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.stripChartX_test.AxisX.ViewMaximum = 1000D;
            this.stripChartX_test.AxisX.ViewMinimum = 0D;
            this.stripChartX_test.AxisX2.AutoScale = false;
            this.stripChartX_test.AxisX2.AutoZoomReset = false;
            this.stripChartX_test.AxisX2.Color = System.Drawing.Color.Black;
            this.stripChartX_test.AxisX2.InitWithScaleView = false;
            this.stripChartX_test.AxisX2.IsLogarithmic = false;
            this.stripChartX_test.AxisX2.LabelEnabled = true;
            this.stripChartX_test.AxisX2.LabelFormat = null;
            this.stripChartX_test.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.stripChartX_test.AxisX2.MajorGridCount = 6;
            this.stripChartX_test.AxisX2.MajorGridEnabled = true;
            this.stripChartX_test.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.stripChartX_test.AxisX2.Maximum = 1000D;
            this.stripChartX_test.AxisX2.Minimum = 0D;
            this.stripChartX_test.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.stripChartX_test.AxisX2.MinorGridEnabled = false;
            this.stripChartX_test.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.stripChartX_test.AxisX2.TickWidth = 1F;
            this.stripChartX_test.AxisX2.Title = "";
            this.stripChartX_test.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.stripChartX_test.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.stripChartX_test.AxisX2.ViewMaximum = 1000D;
            this.stripChartX_test.AxisX2.ViewMinimum = 0D;
            this.stripChartX_test.AxisY.AutoScale = false;
            this.stripChartX_test.AxisY.AutoZoomReset = false;
            this.stripChartX_test.AxisY.Color = System.Drawing.Color.Black;
            this.stripChartX_test.AxisY.InitWithScaleView = false;
            this.stripChartX_test.AxisY.IsLogarithmic = false;
            this.stripChartX_test.AxisY.LabelEnabled = true;
            this.stripChartX_test.AxisY.LabelFormat = null;
            this.stripChartX_test.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.stripChartX_test.AxisY.MajorGridCount = 6;
            this.stripChartX_test.AxisY.MajorGridEnabled = true;
            this.stripChartX_test.AxisY.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.stripChartX_test.AxisY.Maximum = 50000D;
            this.stripChartX_test.AxisY.Minimum = 5000D;
            this.stripChartX_test.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.stripChartX_test.AxisY.MinorGridEnabled = false;
            this.stripChartX_test.AxisY.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.stripChartX_test.AxisY.TickWidth = 1F;
            this.stripChartX_test.AxisY.Title = "";
            this.stripChartX_test.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.stripChartX_test.AxisY.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.stripChartX_test.AxisY.ViewMaximum = 3.5D;
            this.stripChartX_test.AxisY.ViewMinimum = 0.5D;
            this.stripChartX_test.AxisY2.AutoScale = false;
            this.stripChartX_test.AxisY2.AutoZoomReset = false;
            this.stripChartX_test.AxisY2.Color = System.Drawing.Color.Black;
            this.stripChartX_test.AxisY2.InitWithScaleView = false;
            this.stripChartX_test.AxisY2.IsLogarithmic = false;
            this.stripChartX_test.AxisY2.LabelEnabled = true;
            this.stripChartX_test.AxisY2.LabelFormat = null;
            this.stripChartX_test.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.stripChartX_test.AxisY2.MajorGridCount = 6;
            this.stripChartX_test.AxisY2.MajorGridEnabled = true;
            this.stripChartX_test.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.stripChartX_test.AxisY2.Maximum = 50000D;
            this.stripChartX_test.AxisY2.Minimum = 5000D;
            this.stripChartX_test.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.stripChartX_test.AxisY2.MinorGridEnabled = false;
            this.stripChartX_test.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.stripChartX_test.AxisY2.TickWidth = 1F;
            this.stripChartX_test.AxisY2.Title = "";
            this.stripChartX_test.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.stripChartX_test.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.stripChartX_test.AxisY2.ViewMaximum = 3.5D;
            this.stripChartX_test.AxisY2.ViewMinimum = 0.5D;
            this.stripChartX_test.BackColor = System.Drawing.Color.White;
            this.stripChartX_test.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.stripChartX_test.Direction = SeeSharpTools.JY.GUI.StripChartX.ScrollDirection.LeftToRight;
            this.stripChartX_test.DisplayPoints = 10000000;
            this.stripChartX_test.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stripChartX_test.GradientStyle = SeeSharpTools.JY.GUI.StripChartX.ChartGradientStyle.None;
            this.stripChartX_test.LegendBackColor = System.Drawing.Color.Transparent;
            this.stripChartX_test.LegendVisible = true;
            stripChartXSeries1.Color = System.Drawing.Color.Red;
            stripChartXSeries1.Marker = SeeSharpTools.JY.GUI.StripChartXSeries.MarkerType.None;
            stripChartXSeries1.Name = "CH1";
            stripChartXSeries1.Type = SeeSharpTools.JY.GUI.StripChartXSeries.LineType.FastLine;
            stripChartXSeries1.Visible = true;
            stripChartXSeries1.Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Thin;
            stripChartXSeries1.XPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries1.YPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries2.Color = System.Drawing.Color.Blue;
            stripChartXSeries2.Marker = SeeSharpTools.JY.GUI.StripChartXSeries.MarkerType.None;
            stripChartXSeries2.Name = "CH2";
            stripChartXSeries2.Type = SeeSharpTools.JY.GUI.StripChartXSeries.LineType.FastLine;
            stripChartXSeries2.Visible = true;
            stripChartXSeries2.Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Thin;
            stripChartXSeries2.XPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries2.YPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries3.Color = System.Drawing.Color.DeepPink;
            stripChartXSeries3.Marker = SeeSharpTools.JY.GUI.StripChartXSeries.MarkerType.None;
            stripChartXSeries3.Name = "CH3";
            stripChartXSeries3.Type = SeeSharpTools.JY.GUI.StripChartXSeries.LineType.FastLine;
            stripChartXSeries3.Visible = true;
            stripChartXSeries3.Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Thin;
            stripChartXSeries3.XPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries3.YPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries4.Color = System.Drawing.Color.Navy;
            stripChartXSeries4.Marker = SeeSharpTools.JY.GUI.StripChartXSeries.MarkerType.None;
            stripChartXSeries4.Name = "CH4";
            stripChartXSeries4.Type = SeeSharpTools.JY.GUI.StripChartXSeries.LineType.FastLine;
            stripChartXSeries4.Visible = true;
            stripChartXSeries4.Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Thin;
            stripChartXSeries4.XPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries4.YPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries5.Color = System.Drawing.Color.DarkGreen;
            stripChartXSeries5.Marker = SeeSharpTools.JY.GUI.StripChartXSeries.MarkerType.None;
            stripChartXSeries5.Name = "CH5";
            stripChartXSeries5.Type = SeeSharpTools.JY.GUI.StripChartXSeries.LineType.FastLine;
            stripChartXSeries5.Visible = true;
            stripChartXSeries5.Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Thin;
            stripChartXSeries5.XPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries5.YPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            this.stripChartX_test.LineSeries.Add(stripChartXSeries1);
            this.stripChartX_test.LineSeries.Add(stripChartXSeries2);
            this.stripChartX_test.LineSeries.Add(stripChartXSeries3);
            this.stripChartX_test.LineSeries.Add(stripChartXSeries4);
            this.stripChartX_test.LineSeries.Add(stripChartXSeries5);
            this.stripChartX_test.Location = new System.Drawing.Point(12, 12);
            this.stripChartX_test.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stripChartX_test.Miscellaneous.CheckInfinity = false;
            this.stripChartX_test.Miscellaneous.CheckNaN = false;
            this.stripChartX_test.Miscellaneous.CheckNegtiveOrZero = false;
            this.stripChartX_test.Miscellaneous.DirectionChartCount = 3;
            this.stripChartX_test.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.StripChartX.FitType.Range;
            this.stripChartX_test.Miscellaneous.MaxSeriesCount = 32;
            this.stripChartX_test.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.stripChartX_test.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.StripChartXUtility.LayoutDirection.LeftToRight;
            this.stripChartX_test.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.stripChartX_test.Miscellaneous.SplitViewAutoLayout = true;
            this.stripChartX_test.Name = "stripChartX_test";
            this.stripChartX_test.NextTimeStamp = new System.DateTime(((long)(0)));
            this.stripChartX_test.ScrollType = SeeSharpTools.JY.GUI.StripChartX.StripScrollType.Cumulation;
            this.stripChartX_test.SeriesCount = 5;
            this.stripChartX_test.Size = new System.Drawing.Size(844, 343);
            this.stripChartX_test.SplitView = false;
            this.stripChartX_test.StartIndex = 0;
            this.stripChartX_test.TabIndex = 12;
            this.stripChartX_test.TimeInterval = System.TimeSpan.Parse("00:00:00");
            this.stripChartX_test.TimeStampFormat = "hh:mm:ss:fff";
            this.stripChartX_test.XCursor.AutoInterval = true;
            this.stripChartX_test.XCursor.Color = System.Drawing.Color.Red;
            this.stripChartX_test.XCursor.Interval = 0.001D;
            this.stripChartX_test.XCursor.Mode = SeeSharpTools.JY.GUI.StripChartXCursor.CursorMode.Zoom;
            this.stripChartX_test.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.stripChartX_test.XCursor.Value = double.NaN;
            this.stripChartX_test.XDataType = SeeSharpTools.JY.GUI.StripChartX.XAxisDataType.Index;
            this.stripChartX_test.YCursor.AutoInterval = true;
            this.stripChartX_test.YCursor.Color = System.Drawing.Color.Red;
            this.stripChartX_test.YCursor.Interval = 0.001D;
            this.stripChartX_test.YCursor.Mode = SeeSharpTools.JY.GUI.StripChartXCursor.CursorMode.Disabled;
            this.stripChartX_test.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.stripChartX_test.YCursor.Value = double.NaN;
            // 
            // TestStripChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 519);
            this.Controls.Add(this.label_time);
            this.Controls.Add(this.stripChartX_test);
            this.Controls.Add(this.label_interval);
            this.Controls.Add(this.numericUpDown_timeInterval);
            this.Controls.Add(this.label_type);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.label_lineNum);
            this.Controls.Add(this.numericUpDown_lineNum);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.button_multi_dim2);
            this.Controls.Add(this.button_continuous);
            this.Controls.Add(this.button_single_dim2);
            this.Controls.Add(this.button_single_dim1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TestStripChart";
            this.Text = "TestStripChart";
            this.Load += new System.EventHandler(this.TestStripChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_lineNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timeInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_single_dim1;
        private System.Windows.Forms.Button button_single_dim2;
        private System.Windows.Forms.Button button_continuous;
        private System.Windows.Forms.Button button_multi_dim2;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.NumericUpDown numericUpDown_lineNum;
        private System.Windows.Forms.Label label_lineNum;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.Timer timer_plot;
        private System.Windows.Forms.Label label_interval;
        private System.Windows.Forms.NumericUpDown numericUpDown_timeInterval;
        private SeeSharpTools.JY.GUI.StripChartX stripChartX_test;
        private System.Windows.Forms.Label label_time;
    }
}
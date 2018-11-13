namespace ChartTest
{
    partial class TestViewController
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
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries2 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries1 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries1 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestViewController));
            this.button_idle = new System.Windows.Forms.Button();
            this.button_state1 = new System.Windows.Forms.Button();
            this.button_state2 = new System.Windows.Forms.Button();
            this.groupBox_seeSharp = new System.Windows.Forms.GroupBox();
            this.groupBox_common = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.easyChartX2 = new SeeSharpTools.JY.GUI.EasyChartX();
            this.thermometer1 = new SeeSharpTools.JY.GUI.Thermometer();
            this.tank1 = new SeeSharpTools.JY.GUI.Tank();
            this.stripChart1 = new SeeSharpTools.JY.GUI.StripChart();
            this.slide1 = new SeeSharpTools.JY.GUI.Slide();
            this.sevenSegment1 = new SeeSharpTools.JY.GUI.SevenSegment();
            this.segmentBright1 = new SeeSharpTools.JY.GUI.SegmentBright();
            this.scrollingText1 = new SeeSharpTools.JY.GUI.ScrollingText();
            this.pressureGauge1 = new SeeSharpTools.JY.GUI.PressureGauge();
            this.ledBright1 = new SeeSharpTools.JY.GUI.LEDBright();
            this.ledArrow1 = new SeeSharpTools.JY.GUI.LedArrow();
            this.led1 = new SeeSharpTools.JY.GUI.LED();
            this.knobControl1 = new SeeSharpTools.JY.GUI.KnobControl();
            this.industrySwitch1 = new SeeSharpTools.JY.GUI.IndustrySwitch();
            this.gaugeLinear1 = new SeeSharpTools.JY.GUI.GaugeLinear();
            this.easyChartX1 = new SeeSharpTools.JY.GUI.EasyChartX();
            this.easyChart1 = new SeeSharpTools.JY.GUI.EasyChart();
            this.easyButton1 = new SeeSharpTools.JY.GUI.EasyButton();
            this.buttonSwitch1 = new SeeSharpTools.JY.GUI.ButtonSwitch();
            this.aquaGauge1 = new SeeSharpTools.JY.GUI.AquaGauge();
            this.viewController1 = new SeeSharpTools.JY.GUI.ViewController(this.components);
            this.groupBox_seeSharp.SuspendLayout();
            this.groupBox_common.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmentBright1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_idle
            // 
            this.button_idle.Location = new System.Drawing.Point(326, 650);
            this.button_idle.Name = "button_idle";
            this.button_idle.Size = new System.Drawing.Size(75, 23);
            this.button_idle.TabIndex = 0;
            this.button_idle.Text = "idle";
            this.button_idle.UseVisualStyleBackColor = true;
            this.button_idle.Click += new System.EventHandler(this.button_idle_Click);
            // 
            // button_state1
            // 
            this.button_state1.Location = new System.Drawing.Point(508, 650);
            this.button_state1.Name = "button_state1";
            this.button_state1.Size = new System.Drawing.Size(75, 23);
            this.button_state1.TabIndex = 1;
            this.button_state1.Text = "State1";
            this.button_state1.UseVisualStyleBackColor = true;
            this.button_state1.Click += new System.EventHandler(this.button_state1_Click);
            // 
            // button_state2
            // 
            this.button_state2.Location = new System.Drawing.Point(713, 650);
            this.button_state2.Name = "button_state2";
            this.button_state2.Size = new System.Drawing.Size(75, 23);
            this.button_state2.TabIndex = 2;
            this.button_state2.Text = "State2";
            this.button_state2.UseVisualStyleBackColor = true;
            this.button_state2.Click += new System.EventHandler(this.button_state2_Click);
            // 
            // groupBox_seeSharp
            // 
            this.groupBox_seeSharp.Controls.Add(this.thermometer1);
            this.groupBox_seeSharp.Controls.Add(this.tank1);
            this.groupBox_seeSharp.Controls.Add(this.stripChart1);
            this.groupBox_seeSharp.Controls.Add(this.slide1);
            this.groupBox_seeSharp.Controls.Add(this.sevenSegment1);
            this.groupBox_seeSharp.Controls.Add(this.segmentBright1);
            this.groupBox_seeSharp.Controls.Add(this.scrollingText1);
            this.groupBox_seeSharp.Controls.Add(this.pressureGauge1);
            this.groupBox_seeSharp.Controls.Add(this.ledBright1);
            this.groupBox_seeSharp.Controls.Add(this.ledArrow1);
            this.groupBox_seeSharp.Controls.Add(this.led1);
            this.groupBox_seeSharp.Controls.Add(this.knobControl1);
            this.groupBox_seeSharp.Controls.Add(this.industrySwitch1);
            this.groupBox_seeSharp.Controls.Add(this.gaugeLinear1);
            this.groupBox_seeSharp.Controls.Add(this.easyChartX1);
            this.groupBox_seeSharp.Controls.Add(this.easyChart1);
            this.groupBox_seeSharp.Controls.Add(this.easyButton1);
            this.groupBox_seeSharp.Controls.Add(this.buttonSwitch1);
            this.groupBox_seeSharp.Controls.Add(this.aquaGauge1);
            this.groupBox_seeSharp.Location = new System.Drawing.Point(21, 12);
            this.groupBox_seeSharp.Name = "groupBox_seeSharp";
            this.groupBox_seeSharp.Size = new System.Drawing.Size(526, 609);
            this.groupBox_seeSharp.TabIndex = 3;
            this.groupBox_seeSharp.TabStop = false;
            this.groupBox_seeSharp.Text = "groupBox1";
            // 
            // groupBox_common
            // 
            this.groupBox_common.Controls.Add(this.treeView1);
            this.groupBox_common.Controls.Add(this.radioButton1);
            this.groupBox_common.Controls.Add(this.panel1);
            this.groupBox_common.Controls.Add(this.pictureBox1);
            this.groupBox_common.Controls.Add(this.numericUpDown1);
            this.groupBox_common.Controls.Add(this.listBox1);
            this.groupBox_common.Controls.Add(this.label1);
            this.groupBox_common.Controls.Add(this.groupBox2);
            this.groupBox_common.Controls.Add(this.comboBox1);
            this.groupBox_common.Controls.Add(this.checkedListBox1);
            this.groupBox_common.Controls.Add(this.checkBox1);
            this.groupBox_common.Controls.Add(this.button4);
            this.groupBox_common.Location = new System.Drawing.Point(553, 12);
            this.groupBox_common.Name = "groupBox_common";
            this.groupBox_common.Size = new System.Drawing.Size(526, 427);
            this.groupBox_common.TabIndex = 4;
            this.groupBox_common.TabStop = false;
            this.groupBox_common.Text = "groupBox1";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(145, 314);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(121, 97);
            this.treeView1.TabIndex = 11;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(40, 374);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 16);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(303, 247);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(166, 247);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(40, 247);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 7;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(345, 130);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 88);
            this.listBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(40, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(382, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(230, 20);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 84);
            this.checkedListBox1.TabIndex = 2;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(136, 36);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 16);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(40, 36);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // easyChartX2
            // 
            this.easyChartX2.AxisX.AutoScale = true;
            this.easyChartX2.AxisX.AutoZoomReset = false;
            this.easyChartX2.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartX2.AxisX.InitWithScaleView = false;
            this.easyChartX2.AxisX.IsLogarithmic = false;
            this.easyChartX2.AxisX.LabelEnabled = true;
            this.easyChartX2.AxisX.LabelFormat = null;
            this.easyChartX2.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartX2.AxisX.MajorGridEnabled = true;
            this.easyChartX2.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX2.AxisX.Maximum = 1000D;
            this.easyChartX2.AxisX.Minimum = 0D;
            this.easyChartX2.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartX2.AxisX.MinorGridEnabled = false;
            this.easyChartX2.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX2.AxisX.Title = "";
            this.easyChartX2.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartX2.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartX2.AxisX.ViewMaximum = 1000D;
            this.easyChartX2.AxisX.ViewMinimum = 0D;
            this.easyChartX2.AxisX2.AutoScale = true;
            this.easyChartX2.AxisX2.AutoZoomReset = false;
            this.easyChartX2.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartX2.AxisX2.InitWithScaleView = false;
            this.easyChartX2.AxisX2.IsLogarithmic = false;
            this.easyChartX2.AxisX2.LabelEnabled = true;
            this.easyChartX2.AxisX2.LabelFormat = null;
            this.easyChartX2.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartX2.AxisX2.MajorGridEnabled = true;
            this.easyChartX2.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX2.AxisX2.Maximum = 1000D;
            this.easyChartX2.AxisX2.Minimum = 0D;
            this.easyChartX2.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartX2.AxisX2.MinorGridEnabled = false;
            this.easyChartX2.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX2.AxisX2.Title = "";
            this.easyChartX2.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartX2.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartX2.AxisX2.ViewMaximum = 1000D;
            this.easyChartX2.AxisX2.ViewMinimum = 0D;
            this.easyChartX2.AxisY.AutoScale = true;
            this.easyChartX2.AxisY.AutoZoomReset = false;
            this.easyChartX2.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartX2.AxisY.InitWithScaleView = false;
            this.easyChartX2.AxisY.IsLogarithmic = false;
            this.easyChartX2.AxisY.LabelEnabled = true;
            this.easyChartX2.AxisY.LabelFormat = null;
            this.easyChartX2.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartX2.AxisY.MajorGridEnabled = true;
            this.easyChartX2.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX2.AxisY.Maximum = 3.5D;
            this.easyChartX2.AxisY.Minimum = 0D;
            this.easyChartX2.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartX2.AxisY.MinorGridEnabled = false;
            this.easyChartX2.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX2.AxisY.Title = "";
            this.easyChartX2.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartX2.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartX2.AxisY.ViewMaximum = 3.5D;
            this.easyChartX2.AxisY.ViewMinimum = 0D;
            this.easyChartX2.AxisY2.AutoScale = true;
            this.easyChartX2.AxisY2.AutoZoomReset = false;
            this.easyChartX2.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartX2.AxisY2.InitWithScaleView = false;
            this.easyChartX2.AxisY2.IsLogarithmic = false;
            this.easyChartX2.AxisY2.LabelEnabled = true;
            this.easyChartX2.AxisY2.LabelFormat = null;
            this.easyChartX2.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartX2.AxisY2.MajorGridEnabled = true;
            this.easyChartX2.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX2.AxisY2.Maximum = 3.5D;
            this.easyChartX2.AxisY2.Minimum = 0D;
            this.easyChartX2.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartX2.AxisY2.MinorGridEnabled = false;
            this.easyChartX2.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX2.AxisY2.Title = "";
            this.easyChartX2.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartX2.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartX2.AxisY2.ViewMaximum = 3.5D;
            this.easyChartX2.AxisY2.ViewMinimum = 0D;
            this.easyChartX2.BackColor = System.Drawing.Color.White;
            this.easyChartX2.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartX2.CheckInfinity = false;
            this.easyChartX2.CheckNaN = false;
            this.easyChartX2.CheckNegtiveOrZero = false;
            this.easyChartX2.Cumulitive = false;
            this.easyChartX2.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartX2.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartX2.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartX2.LegendVisible = true;
            easyChartXSeries2.Color = System.Drawing.Color.Red;
            easyChartXSeries2.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries2.Name = "Series1";
            easyChartXSeries2.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries2.Visible = true;
            easyChartXSeries2.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries2.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries2.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartX2.LineSeries.Add(easyChartXSeries2);
            this.easyChartX2.Location = new System.Drawing.Point(637, 446);
            this.easyChartX2.Name = "easyChartX2";
            this.easyChartX2.SeriesCount = 1;
            this.easyChartX2.Size = new System.Drawing.Size(381, 175);
            this.easyChartX2.SplitView = false;
            this.easyChartX2.TabIndex = 6;
            this.easyChartX2.XCursor.AutoInterval = true;
            this.easyChartX2.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartX2.XCursor.Interval = 0.001D;
            this.easyChartX2.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartX2.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartX2.XCursor.Value = double.NaN;
            this.easyChartX2.YCursor.AutoInterval = true;
            this.easyChartX2.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartX2.YCursor.Interval = 0.001D;
            this.easyChartX2.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartX2.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartX2.YCursor.Value = double.NaN;
            // 
            // thermometer1
            // 
            this.thermometer1.BackColor = System.Drawing.Color.Transparent;
            this.thermometer1.BallSize = 15;
            this.thermometer1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thermometer1.ForeColor = System.Drawing.Color.Red;
            this.thermometer1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.thermometer1.LineWidth = 5;
            this.thermometer1.Location = new System.Drawing.Point(219, 433);
            this.thermometer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.thermometer1.Max = 100D;
            this.thermometer1.Min = 0D;
            this.thermometer1.Name = "thermometer1";
            this.thermometer1.NumberOfDivisions = 10;
            this.thermometer1.Size = new System.Drawing.Size(59, 163);
            this.thermometer1.TabIndex = 16;
            this.thermometer1.TextColor = System.Drawing.Color.Black;
            this.thermometer1.TextDecimals = 3;
            this.thermometer1.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.thermometer1.TickWidth = 4;
            this.thermometer1.Value = 0D;
            // 
            // tank1
            // 
            this.tank1.BackColor = System.Drawing.Color.DarkGray;
            this.tank1.Font = new System.Drawing.Font("Consolas", 10.25F);
            this.tank1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.tank1.IsBright = true;
            this.tank1.Location = new System.Drawing.Point(205, 174);
            this.tank1.Maximum = 100D;
            this.tank1.Minimum = 0D;
            this.tank1.Name = "tank1";
            this.tank1.Size = new System.Drawing.Size(70, 98);
            this.tank1.Style = SeeSharpTools.JY.GUI.Tank.TankStyles.Solid;
            this.tank1.TabIndex = 15;
            this.tank1.Value = 0D;
            // 
            // stripChart1
            // 
            this.stripChart1.AxisYMax = 3.5D;
            this.stripChart1.AxisYMin = 0D;
            this.stripChart1.BackColor = System.Drawing.Color.White;
            this.stripChart1.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.stripChart1.ChartBackColor = System.Drawing.Color.White;
            this.stripChart1.Displaydirection = SeeSharpTools.JY.GUI.StripChart.DisplayDirection.RightToLeft;
            this.stripChart1.DisPlayPoints = 4000;
            this.stripChart1.LegendVisible = false;
            this.stripChart1.LineNum = 1;
            this.stripChart1.LineWidth = new int[] {
        1};
            this.stripChart1.Location = new System.Drawing.Point(368, 101);
            this.stripChart1.MajorGridEnabled = true;
            this.stripChart1.MinorGridEnabled = true;
            this.stripChart1.Name = "stripChart1";
            this.stripChart1.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red};
            this.stripChart1.ScrollMode = false;
            this.stripChart1.ScrollType = SeeSharpTools.JY.GUI.StripChart.StripScrollType.Cumulation;
            this.stripChart1.SeriesNames = new string[] {
        "Series1"};
            this.stripChart1.Size = new System.Drawing.Size(139, 140);
            this.stripChart1.TabIndex = 0;
            this.stripChart1.XAxisStartIndex = 0;
            this.stripChart1.XAxisTitle = "";
            this.stripChart1.XAxisTypes = SeeSharpTools.JY.GUI.StripChart.XAxisDataType.Index;
            this.stripChart1.XTitleOrientation = SeeSharpTools.JY.GUI.StripChart.TitleOrientation.Auto;
            this.stripChart1.XTitlePosition = SeeSharpTools.JY.GUI.StripChart.TitlePosition.Center;
            this.stripChart1.YAutoEnable = true;
            this.stripChart1.YAxisLogarithmic = false;
            this.stripChart1.YAxisTitle = "";
            this.stripChart1.YTitleOrientation = SeeSharpTools.JY.GUI.StripChart.TitleOrientation.Auto;
            this.stripChart1.YTitlePosition = SeeSharpTools.JY.GUI.StripChart.TitlePosition.Center;
            // 
            // slide1
            // 
            this.slide1.BackColor = System.Drawing.Color.Transparent;
            this.slide1.Fill = false;
            this.slide1.FillColor = System.Drawing.Color.Blue;
            this.slide1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slide1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.slide1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.slide1.LineWidth = 3;
            this.slide1.Location = new System.Drawing.Point(34, 226);
            this.slide1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.slide1.Max = 10D;
            this.slide1.Min = 0D;
            this.slide1.Name = "slide1";
            this.slide1.NumberOfDivisions = 10;
            this.slide1.Size = new System.Drawing.Size(138, 46);
            this.slide1.TabIndex = 14;
            this.slide1.TextDecimals = 3;
            this.slide1.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.slide1.TickWidth = 4;
            this.slide1.TrackerColor = System.Drawing.Color.DimGray;
            this.slide1.TrackerSize = new System.Drawing.Size(5, 15);
            this.slide1.Value = 0D;
            this.slide1.Valuedecimals = 3;
            // 
            // sevenSegment1
            // 
            this.sevenSegment1.BackgroundColor = System.Drawing.Color.Black;
            this.sevenSegment1.DarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sevenSegment1.IsDecimalShow = true;
            this.sevenSegment1.ItalicFactor = -0.08F;
            this.sevenSegment1.LightColor = System.Drawing.Color.Red;
            this.sevenSegment1.Location = new System.Drawing.Point(368, 480);
            this.sevenSegment1.Name = "sevenSegment1";
            this.sevenSegment1.NumberOfChars = 4;
            this.sevenSegment1.Size = new System.Drawing.Size(139, 54);
            this.sevenSegment1.TabIndex = 13;
            this.sevenSegment1.TabStop = false;
            this.sevenSegment1.Value = null;
            // 
            // segmentBright1
            // 
            this.segmentBright1.BackColor = System.Drawing.Color.Transparent;
            this.segmentBright1.BackColor1 = System.Drawing.Color.Black;
            this.segmentBright1.BackColor2 = System.Drawing.Color.DimGray;
            this.segmentBright1.DarkColor = System.Drawing.Color.DimGray;
            this.segmentBright1.ForeColor = System.Drawing.Color.LightGreen;
            this.segmentBright1.IsHighlightOpaque = ((byte)(50));
            this.segmentBright1.Location = new System.Drawing.Point(357, 548);
            this.segmentBright1.Name = "segmentBright1";
            this.segmentBright1.Size = new System.Drawing.Size(145, 55);
            this.segmentBright1.TabIndex = 12;
            this.segmentBright1.Text = "segmentBright1";
            this.segmentBright1.Value = "";
            // 
            // scrollingText1
            // 
            this.scrollingText1.BorderColor = System.Drawing.Color.Black;
            this.scrollingText1.BorderVisible = true;
            this.scrollingText1.Cursor = System.Windows.Forms.Cursors.Default;
            this.scrollingText1.Location = new System.Drawing.Point(304, 434);
            this.scrollingText1.Name = "scrollingText1";
            this.scrollingText1.ScrollDirection = SeeSharpTools.JY.GUI.ScrollingText.TextDirection.RightToLeft;
            this.scrollingText1.ScrollSpeed = 25;
            this.scrollingText1.Size = new System.Drawing.Size(216, 40);
            this.scrollingText1.TabIndex = 11;
            this.scrollingText1.Text = "scrollingText1";
            this.scrollingText1.VerticleAligment = SeeSharpTools.JY.GUI.ScrollingText.TextVerticalAlignment.Center;
            // 
            // pressureGauge1
            // 
            this.pressureGauge1.BackColor = System.Drawing.Color.Silver;
            this.pressureGauge1.BorderWidth = 2;
            this.pressureGauge1.Font = new System.Drawing.Font("Calibri", 11F);
            this.pressureGauge1.ForeColor = System.Drawing.Color.Black;
            this.pressureGauge1.Glossiness = 45F;
            this.pressureGauge1.Location = new System.Drawing.Point(6, 416);
            this.pressureGauge1.Max = 100D;
            this.pressureGauge1.Min = 0D;
            this.pressureGauge1.Name = "pressureGauge1";
            this.pressureGauge1.Size = new System.Drawing.Size(188, 180);
            this.pressureGauge1.TabIndex = 5;
            this.pressureGauge1.UnitText = "°C";
            this.pressureGauge1.Value = 0D;
            // 
            // ledBright1
            // 
            this.ledBright1.BlinkColor = System.Drawing.Color.Lime;
            this.ledBright1.BlinkInterval = 1000;
            this.ledBright1.BlinkOn = false;
            this.ledBright1.Color = System.Drawing.Color.Lime;
            this.ledBright1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ledBright1.Interacton = SeeSharpTools.JY.GUI.LEDBright.InteractionStyle.Indicator;
            this.ledBright1.Location = new System.Drawing.Point(147, 174);
            this.ledBright1.Name = "ledBright1";
            this.ledBright1.Size = new System.Drawing.Size(34, 30);
            this.ledBright1.Style = SeeSharpTools.JY.GUI.LEDBright.LedBrghtStyle.Circular;
            this.ledBright1.TabIndex = 10;
            this.ledBright1.Value = false;
            // 
            // ledArrow1
            // 
            this.ledArrow1.BlinkColor = System.Drawing.Color.Lime;
            this.ledArrow1.BlinkInterval = 1000;
            this.ledArrow1.BlinkOn = false;
            this.ledArrow1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ledArrow1.Direction = SeeSharpTools.JY.GUI.LedArrow.DirectionQ.Right;
            this.ledArrow1.Interacton = SeeSharpTools.JY.GUI.LedArrow.InteractionStyle.Indicator;
            this.ledArrow1.Location = new System.Drawing.Point(69, 172);
            this.ledArrow1.Name = "ledArrow1";
            this.ledArrow1.OffColor = System.Drawing.Color.DarkGreen;
            this.ledArrow1.OnColor = System.Drawing.Color.Lime;
            this.ledArrow1.Size = new System.Drawing.Size(60, 30);
            this.ledArrow1.Style = SeeSharpTools.JY.GUI.LedArrow.LedArrowStyle.SingleHead;
            this.ledArrow1.TabIndex = 9;
            this.ledArrow1.Value = false;
            // 
            // led1
            // 
            this.led1.BlinkColor = System.Drawing.Color.Lime;
            this.led1.BlinkInterval = 1000;
            this.led1.BlinkOn = false;
            this.led1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.led1.Interacton = SeeSharpTools.JY.GUI.LED.InteractionStyle.Indicator;
            this.led1.Location = new System.Drawing.Point(17, 171);
            this.led1.Name = "led1";
            this.led1.OffColor = System.Drawing.Color.Gray;
            this.led1.OnColor = System.Drawing.Color.Lime;
            this.led1.Size = new System.Drawing.Size(32, 33);
            this.led1.Style = SeeSharpTools.JY.GUI.LED.LedStyle.Circular;
            this.led1.TabIndex = 8;
            this.led1.Value = false;
            // 
            // knobControl1
            // 
            this.knobControl1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.knobControl1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.knobControl1.IsTextShow = true;
            this.knobControl1.KnobColor = System.Drawing.SystemColors.Control;
            this.knobControl1.Location = new System.Drawing.Point(191, 89);
            this.knobControl1.Max = 100D;
            this.knobControl1.Min = 0D;
            this.knobControl1.Name = "knobControl1";
            this.knobControl1.Size = new System.Drawing.Size(93, 93);
            this.knobControl1.TabIndex = 7;
            this.knobControl1.TextDecimals = 3;
            this.knobControl1.TickWidth = 5;
            this.knobControl1.Value = 0D;
            this.knobControl1.Valuedecimals = 3;
            // 
            // industrySwitch1
            // 
            this.industrySwitch1.BackColor = System.Drawing.Color.Transparent;
            this.industrySwitch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.industrySwitch1.Interacton = SeeSharpTools.JY.GUI.IndustrySwitch.InteractionStyle.SwitchWhenPressed;
            this.industrySwitch1.Location = new System.Drawing.Point(357, 20);
            this.industrySwitch1.Name = "industrySwitch1";
            this.industrySwitch1.OffColor = System.Drawing.Color.Silver;
            this.industrySwitch1.OnColor = System.Drawing.Color.Silver;
            this.industrySwitch1.Size = new System.Drawing.Size(82, 84);
            this.industrySwitch1.Style = SeeSharpTools.JY.GUI.IndustrySwitch.SwitchStyles.Vertical;
            this.industrySwitch1.TabIndex = 6;
            this.industrySwitch1.Value = false;
            // 
            // gaugeLinear1
            // 
            this.gaugeLinear1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gaugeLinear1.LineColor = System.Drawing.Color.White;
            this.gaugeLinear1.Location = new System.Drawing.Point(281, 74);
            this.gaugeLinear1.Maximum = 100D;
            this.gaugeLinear1.Minimum = 0D;
            this.gaugeLinear1.Name = "gaugeLinear1";
            this.gaugeLinear1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.gaugeLinear1.Sidedirection = SeeSharpTools.JY.GUI.GaugeLinear.SideDirection.LeftToRight;
            this.gaugeLinear1.Size = new System.Drawing.Size(70, 154);
            this.gaugeLinear1.TabIndex = 5;
            this.gaugeLinear1.TextColor = System.Drawing.Color.Black;
            this.gaugeLinear1.TextDecimals = 0;
            this.gaugeLinear1.TickColor = System.Drawing.Color.Black;
            this.gaugeLinear1.TickMajorLength = 10;
            this.gaugeLinear1.TickMinorLength = 3;
            this.gaugeLinear1.TrackerColor = System.Drawing.SystemColors.Control;
            this.gaugeLinear1.TrackerSize = new System.Drawing.Size(20, 10);
            this.gaugeLinear1.Value = 0D;
            // 
            // easyChartX1
            // 
            this.easyChartX1.AxisX.AutoScale = true;
            this.easyChartX1.AxisX.AutoZoomReset = false;
            this.easyChartX1.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartX1.AxisX.InitWithScaleView = false;
            this.easyChartX1.AxisX.IsLogarithmic = false;
            this.easyChartX1.AxisX.LabelEnabled = true;
            this.easyChartX1.AxisX.LabelFormat = null;
            this.easyChartX1.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartX1.AxisX.MajorGridEnabled = true;
            this.easyChartX1.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX1.AxisX.Maximum = 1000D;
            this.easyChartX1.AxisX.Minimum = 0D;
            this.easyChartX1.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartX1.AxisX.MinorGridEnabled = false;
            this.easyChartX1.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX1.AxisX.Title = "";
            this.easyChartX1.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartX1.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartX1.AxisX.ViewMaximum = 1000D;
            this.easyChartX1.AxisX.ViewMinimum = 0D;
            this.easyChartX1.AxisX2.AutoScale = true;
            this.easyChartX1.AxisX2.AutoZoomReset = false;
            this.easyChartX1.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartX1.AxisX2.InitWithScaleView = false;
            this.easyChartX1.AxisX2.IsLogarithmic = false;
            this.easyChartX1.AxisX2.LabelEnabled = true;
            this.easyChartX1.AxisX2.LabelFormat = null;
            this.easyChartX1.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartX1.AxisX2.MajorGridEnabled = true;
            this.easyChartX1.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX1.AxisX2.Maximum = 1000D;
            this.easyChartX1.AxisX2.Minimum = 0D;
            this.easyChartX1.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartX1.AxisX2.MinorGridEnabled = false;
            this.easyChartX1.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX1.AxisX2.Title = "";
            this.easyChartX1.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartX1.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartX1.AxisX2.ViewMaximum = 1000D;
            this.easyChartX1.AxisX2.ViewMinimum = 0D;
            this.easyChartX1.AxisY.AutoScale = true;
            this.easyChartX1.AxisY.AutoZoomReset = false;
            this.easyChartX1.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartX1.AxisY.InitWithScaleView = false;
            this.easyChartX1.AxisY.IsLogarithmic = false;
            this.easyChartX1.AxisY.LabelEnabled = true;
            this.easyChartX1.AxisY.LabelFormat = null;
            this.easyChartX1.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartX1.AxisY.MajorGridEnabled = true;
            this.easyChartX1.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX1.AxisY.Maximum = 3.5D;
            this.easyChartX1.AxisY.Minimum = 0D;
            this.easyChartX1.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartX1.AxisY.MinorGridEnabled = false;
            this.easyChartX1.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX1.AxisY.Title = "";
            this.easyChartX1.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartX1.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartX1.AxisY.ViewMaximum = 3.5D;
            this.easyChartX1.AxisY.ViewMinimum = 0D;
            this.easyChartX1.AxisY2.AutoScale = true;
            this.easyChartX1.AxisY2.AutoZoomReset = false;
            this.easyChartX1.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartX1.AxisY2.InitWithScaleView = false;
            this.easyChartX1.AxisY2.IsLogarithmic = false;
            this.easyChartX1.AxisY2.LabelEnabled = true;
            this.easyChartX1.AxisY2.LabelFormat = null;
            this.easyChartX1.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartX1.AxisY2.MajorGridEnabled = true;
            this.easyChartX1.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX1.AxisY2.Maximum = 3.5D;
            this.easyChartX1.AxisY2.Minimum = 0D;
            this.easyChartX1.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartX1.AxisY2.MinorGridEnabled = false;
            this.easyChartX1.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Solid;
            this.easyChartX1.AxisY2.Title = "";
            this.easyChartX1.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartX1.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartX1.AxisY2.ViewMaximum = 3.5D;
            this.easyChartX1.AxisY2.ViewMinimum = 0D;
            this.easyChartX1.BackColor = System.Drawing.Color.White;
            this.easyChartX1.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartX1.CheckInfinity = false;
            this.easyChartX1.CheckNaN = false;
            this.easyChartX1.CheckNegtiveOrZero = false;
            this.easyChartX1.Cumulitive = false;
            this.easyChartX1.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartX1.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartX1.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartX1.LegendVisible = false;
            easyChartXSeries1.Color = System.Drawing.Color.Red;
            easyChartXSeries1.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries1.Name = "Series1";
            easyChartXSeries1.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries1.Visible = true;
            easyChartXSeries1.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries1.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries1.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartX1.LineSeries.Add(easyChartXSeries1);
            this.easyChartX1.Location = new System.Drawing.Point(305, 247);
            this.easyChartX1.Name = "easyChartX1";
            this.easyChartX1.SeriesCount = 1;
            this.easyChartX1.Size = new System.Drawing.Size(215, 174);
            this.easyChartX1.SplitView = false;
            this.easyChartX1.TabIndex = 4;
            this.easyChartX1.XCursor.AutoInterval = true;
            this.easyChartX1.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartX1.XCursor.Interval = 0.001D;
            this.easyChartX1.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartX1.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartX1.XCursor.Value = double.NaN;
            this.easyChartX1.YCursor.AutoInterval = true;
            this.easyChartX1.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartX1.YCursor.Interval = 0.001D;
            this.easyChartX1.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartX1.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartX1.YCursor.Value = double.NaN;
            // 
            // easyChart1
            // 
            this.easyChart1.AxisX.AutoScale = true;
            this.easyChart1.AxisX.InitWithScaleView = false;
            this.easyChart1.AxisX.LabelEnabled = true;
            this.easyChart1.AxisX.LabelFormat = "";
            this.easyChart1.AxisX.Maximum = 1001D;
            this.easyChart1.AxisX.Minimum = 0D;
            this.easyChart1.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart1.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChart1.AxisX.Title = "";
            this.easyChart1.AxisX.ViewMaximum = 1001D;
            this.easyChart1.AxisX.ViewMinimum = 0D;
            this.easyChart1.AxisY.AutoScale = true;
            this.easyChart1.AxisY.InitWithScaleView = false;
            this.easyChart1.AxisY.LabelEnabled = true;
            this.easyChart1.AxisY.LabelFormat = "";
            this.easyChart1.AxisY.Maximum = 3.5D;
            this.easyChart1.AxisY.Minimum = 0D;
            this.easyChart1.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart1.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChart1.AxisY.Title = "";
            this.easyChart1.AxisY.ViewMaximum = 3.5D;
            this.easyChart1.AxisY.ViewMinimum = 0D;
            this.easyChart1.AxisYMax = 3.5D;
            this.easyChart1.AxisYMin = 0D;
            this.easyChart1.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChart1.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChart1.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChart1.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChart1.LegendVisible = false;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChart1.LineSeries.Add(easyChartSeries1);
            this.easyChart1.Location = new System.Drawing.Point(5, 277);
            this.easyChart1.MajorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MajorGridEnabled = true;
            this.easyChart1.Margin = new System.Windows.Forms.Padding(2);
            this.easyChart1.MinorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MinorGridEnabled = false;
            this.easyChart1.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChart1.Name = "easyChart1";
            this.easyChart1.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChart1.SeriesNames = new string[] {
        "Series1",
        "Series2",
        "Series3",
        "Series4",
        "Series5",
        "Series6",
        "Series7",
        "Series8",
        "Series9",
        "Series10",
        "Series11",
        "Series12",
        "Series13",
        "Series14",
        "Series15",
        "Series16",
        "Series17",
        "Series18",
        "Series19",
        "Series20",
        "Series21",
        "Series22",
        "Series23",
        "Series24",
        "Series25",
        "Series26",
        "Series27",
        "Series28",
        "Series29",
        "Series30",
        "Series31",
        "Series32"};
            this.easyChart1.Size = new System.Drawing.Size(288, 134);
            this.easyChart1.TabIndex = 3;
            this.easyChart1.XAxisLogarithmic = false;
            this.easyChart1.XAxisTitle = "";
            this.easyChart1.XCursor.AutoInterval = true;
            this.easyChart1.XCursor.Color = System.Drawing.Color.Red;
            this.easyChart1.XCursor.Interval = 1D;
            this.easyChart1.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChart1.XCursor.Value = double.NaN;
            this.easyChart1.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart1.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChart1.YAutoEnable = true;
            this.easyChart1.YAxisLogarithmic = false;
            this.easyChart1.YAxisTitle = "";
            this.easyChart1.YCursor.AutoInterval = true;
            this.easyChart1.YCursor.Color = System.Drawing.Color.Red;
            this.easyChart1.YCursor.Interval = 0.001D;
            this.easyChart1.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChart1.YCursor.Value = double.NaN;
            this.easyChart1.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart1.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // easyButton1
            // 
            this.easyButton1.Location = new System.Drawing.Point(271, 36);
            this.easyButton1.Name = "easyButton1";
            this.easyButton1.PreSetImage = SeeSharpTools.JY.GUI.EasyButton.ButtonPresetImage.None;
            this.easyButton1.Size = new System.Drawing.Size(80, 32);
            this.easyButton1.TabIndex = 2;
            this.easyButton1.Text = "easyButton1";
            // 
            // buttonSwitch1
            // 
            this.buttonSwitch1.Location = new System.Drawing.Point(191, 50);
            this.buttonSwitch1.Name = "buttonSwitch1";
            this.buttonSwitch1.OffFont = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSwitch1.OnFont = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSwitch1.Size = new System.Drawing.Size(50, 19);
            this.buttonSwitch1.TabIndex = 1;
            // 
            // aquaGauge1
            // 
            this.aquaGauge1.BackColor = System.Drawing.Color.Silver;
            this.aquaGauge1.DescriptionText = null;
            this.aquaGauge1.Glossiness = 80F;
            this.aquaGauge1.Location = new System.Drawing.Point(17, 29);
            this.aquaGauge1.Max = 100D;
            this.aquaGauge1.Min = 0D;
            this.aquaGauge1.Name = "aquaGauge1";
            this.aquaGauge1.NumberOfSubDivisions = 5;
            this.aquaGauge1.Size = new System.Drawing.Size(136, 136);
            this.aquaGauge1.TabIndex = 0;
            this.aquaGauge1.Value = 0D;
            // 
            // viewController1
            // 
            this.viewController1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("viewController1.BackgroundImage")));
            this.viewController1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.viewController1.ControlInfos = new string[0];
            this.viewController1.Location = new System.Drawing.Point(101, 641);
            this.viewController1.MaximumSize = new System.Drawing.Size(30, 30);
            this.viewController1.MinimumSize = new System.Drawing.Size(30, 30);
            this.viewController1.Name = "viewController1";
            this.viewController1.Size = new System.Drawing.Size(30, 30);
            this.viewController1.State = "";
            this.viewController1.StateNames = new string[0];
            this.viewController1.StateValue = -1;
            this.viewController1.TabIndex = 7;
            // 
            // TestViewController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 685);
            this.Controls.Add(this.viewController1);
            this.Controls.Add(this.easyChartX2);
            this.Controls.Add(this.groupBox_common);
            this.Controls.Add(this.groupBox_seeSharp);
            this.Controls.Add(this.button_state2);
            this.Controls.Add(this.button_state1);
            this.Controls.Add(this.button_idle);
            this.Name = "TestViewController";
            this.Text = "TestViewController";
            this.groupBox_seeSharp.ResumeLayout(false);
            this.groupBox_common.ResumeLayout(false);
            this.groupBox_common.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmentBright1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_idle;
        private System.Windows.Forms.Button button_state1;
        private System.Windows.Forms.Button button_state2;
        private System.Windows.Forms.GroupBox groupBox_seeSharp;
        private System.Windows.Forms.GroupBox groupBox_common;
        private SeeSharpTools.JY.GUI.AquaGauge aquaGauge1;
        private SeeSharpTools.JY.GUI.ButtonSwitch buttonSwitch1;
        private SeeSharpTools.JY.GUI.EasyButton easyButton1;
        private SeeSharpTools.JY.GUI.EasyChart easyChart1;
        private SeeSharpTools.JY.GUI.EasyChartX easyChartX1;
        private SeeSharpTools.JY.GUI.GaugeLinear gaugeLinear1;
        private SeeSharpTools.JY.GUI.IndustrySwitch industrySwitch1;
        private SeeSharpTools.JY.GUI.KnobControl knobControl1;
        private SeeSharpTools.JY.GUI.LED led1;
        private SeeSharpTools.JY.GUI.LedArrow ledArrow1;
        private SeeSharpTools.JY.GUI.LEDBright ledBright1;
        private SeeSharpTools.JY.GUI.PressureGauge pressureGauge1;
        private SeeSharpTools.JY.GUI.ScrollingText scrollingText1;
        private SeeSharpTools.JY.GUI.SegmentBright segmentBright1;
        private SeeSharpTools.JY.GUI.SevenSegment sevenSegment1;
        private SeeSharpTools.JY.GUI.Slide slide1;
        private SeeSharpTools.JY.GUI.StripChart stripChart1;
        private SeeSharpTools.JY.GUI.Tank tank1;
        private SeeSharpTools.JY.GUI.Thermometer thermometer1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.RadioButton radioButton1;
        private SeeSharpTools.JY.GUI.EasyChartX easyChartX2;
        private SeeSharpTools.JY.GUI.ViewController viewController1;
    }
}
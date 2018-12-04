namespace SeeSharpTools.JY.GUI.DigitalChartTabCursorUtility
{
    partial class DigitalChartTabCursorInfoForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DigitalChartTabCursorInfoForm));
            this.dataGridView_cursorInfo = new System.Windows.Forms.DataGridView();
            this.ColumnSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CursorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeriesIndex = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.XValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_add = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.tableLayoutPanel_buttonPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_cursorInfo)).BeginInit();
            this.tableLayoutPanel_buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_cursorInfo
            // 
            this.dataGridView_cursorInfo.AllowUserToAddRows = false;
            this.dataGridView_cursorInfo.AllowUserToDeleteRows = false;
            this.dataGridView_cursorInfo.AllowUserToResizeRows = false;
            this.dataGridView_cursorInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_cursorInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_cursorInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_cursorInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_cursorInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSelect,
            this.ColorColumn,
            this.CursorColumn,
            this.SeriesIndex,
            this.XValueColumn,
            this.YValue});
            this.dataGridView_cursorInfo.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_cursorInfo.Name = "dataGridView_cursorInfo";
            this.dataGridView_cursorInfo.RowHeadersVisible = false;
            this.dataGridView_cursorInfo.RowTemplate.Height = 23;
            this.dataGridView_cursorInfo.Size = new System.Drawing.Size(411, 106);
            this.dataGridView_cursorInfo.TabIndex = 0;
            this.dataGridView_cursorInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_cursorInfo_CellContentClick);
            this.dataGridView_cursorInfo.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_cursorInfo_CellContentClick);
            this.dataGridView_cursorInfo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_cursorInfo_CellValueChanged);
            // 
            // ColumnSelect
            // 
            this.ColumnSelect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnSelect.Frozen = true;
            this.ColumnSelect.HeaderText = "Enable";
            this.ColumnSelect.Name = "ColumnSelect";
            this.ColumnSelect.ReadOnly = true;
            this.ColumnSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnSelect.Width = 45;
            // 
            // ColorColumn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColorColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColorColumn.Frozen = true;
            this.ColorColumn.HeaderText = "Color";
            this.ColorColumn.Name = "ColorColumn";
            this.ColorColumn.ReadOnly = true;
            this.ColorColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColorColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColorColumn.Width = 50;
            // 
            // CursorColumn
            // 
            this.CursorColumn.HeaderText = "Name";
            this.CursorColumn.Name = "CursorColumn";
            this.CursorColumn.Width = 70;
            // 
            // SeriesIndex
            // 
            this.SeriesIndex.HeaderText = "Series";
            this.SeriesIndex.Name = "SeriesIndex";
            this.SeriesIndex.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SeriesIndex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SeriesIndex.Width = 80;
            // 
            // XValueColumn
            // 
            this.XValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.XValueColumn.HeaderText = "X Value";
            this.XValueColumn.MaxInputLength = 50;
            this.XValueColumn.Name = "XValueColumn";
            // 
            // YValue
            // 
            this.YValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.YValue.HeaderText = "Y Value";
            this.YValue.MaxInputLength = 50;
            this.YValue.Name = "YValue";
            this.YValue.ReadOnly = true;
            // 
            // button_add
            // 
            this.button_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_add.Location = new System.Drawing.Point(3, 3);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(69, 21);
            this.button_add.TabIndex = 1;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_delete
            // 
            this.button_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_delete.Location = new System.Drawing.Point(121, 3);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(69, 21);
            this.button_delete.TabIndex = 2;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_clear
            // 
            this.button_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_clear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_clear.Location = new System.Drawing.Point(239, 3);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(69, 21);
            this.button_clear.TabIndex = 3;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // tableLayoutPanel_buttonPanel
            // 
            this.tableLayoutPanel_buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_buttonPanel.ColumnCount = 5;
            this.tableLayoutPanel_buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel_buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel_buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel_buttonPanel.Controls.Add(this.button_add, 0, 0);
            this.tableLayoutPanel_buttonPanel.Controls.Add(this.button_clear, 4, 0);
            this.tableLayoutPanel_buttonPanel.Controls.Add(this.button_delete, 2, 0);
            this.tableLayoutPanel_buttonPanel.Location = new System.Drawing.Point(46, 112);
            this.tableLayoutPanel_buttonPanel.Name = "tableLayoutPanel_buttonPanel";
            this.tableLayoutPanel_buttonPanel.RowCount = 1;
            this.tableLayoutPanel_buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_buttonPanel.Size = new System.Drawing.Size(311, 27);
            this.tableLayoutPanel_buttonPanel.TabIndex = 4;
            // 
            // TabCursorInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 145);
            this.Controls.Add(this.tableLayoutPanel_buttonPanel);
            this.Controls.Add(this.dataGridView_cursorInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DigitalChartTabCursorInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CursorInfoForm";
            this.Deactivate += new System.EventHandler(this.DynamicCursorInfoForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DynamicCursorInfoForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_cursorInfo)).EndInit();
            this.tableLayoutPanel_buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_cursorInfo;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CursorColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn SeriesIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn XValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn YValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_buttonPanel;
    }
}
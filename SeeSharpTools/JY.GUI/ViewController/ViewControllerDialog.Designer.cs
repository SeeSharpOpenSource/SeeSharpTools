namespace SeeSharpTools.JY.GUI
{
    partial class ViewControllerDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button_addState = new System.Windows.Forms.Button();
            this.button_deleteState = new System.Windows.Forms.Button();
            this.button__cancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel_buttonPanel = new System.Windows.Forms.TableLayoutPanel();
            this.button_confirm = new System.Windows.Forms.Button();
            this.splitContainer_selectView = new System.Windows.Forms.SplitContainer();
            this.button_renameState = new System.Windows.Forms.Button();
            this.listBox_stateNames = new System.Windows.Forms.ListBox();
            this.checkBox_selectAll = new System.Windows.Forms.CheckBox();
            this.tabControl_selectView = new System.Windows.Forms.TabControl();
            this.tabPage_enabledSelect = new System.Windows.Forms.TabPage();
            this.dataGridView_enabledSelect = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ControlName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabPage_visibleSelect = new System.Windows.Forms.TabPage();
            this.dataGridView_visibleSelect = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.checkedListBox_selectControl = new System.Windows.Forms.CheckedListBox();
            this.splitContainer_operationPanel = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel_buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_selectView)).BeginInit();
            this.splitContainer_selectView.Panel1.SuspendLayout();
            this.splitContainer_selectView.Panel2.SuspendLayout();
            this.splitContainer_selectView.SuspendLayout();
            this.tabControl_selectView.SuspendLayout();
            this.tabPage_enabledSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_enabledSelect)).BeginInit();
            this.tabPage_visibleSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_visibleSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_operationPanel)).BeginInit();
            this.splitContainer_operationPanel.Panel1.SuspendLayout();
            this.splitContainer_operationPanel.Panel2.SuspendLayout();
            this.splitContainer_operationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_addState
            // 
            this.button_addState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addState.Location = new System.Drawing.Point(5, 190);
            this.button_addState.Name = "button_addState";
            this.button_addState.Size = new System.Drawing.Size(110, 25);
            this.button_addState.TabIndex = 4;
            this.button_addState.Text = "Add State";
            this.button_addState.UseVisualStyleBackColor = true;
            this.button_addState.Click += new System.EventHandler(this.button_addState_Click);
            // 
            // button_deleteState
            // 
            this.button_deleteState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_deleteState.Location = new System.Drawing.Point(5, 244);
            this.button_deleteState.Name = "button_deleteState";
            this.button_deleteState.Size = new System.Drawing.Size(110, 25);
            this.button_deleteState.TabIndex = 5;
            this.button_deleteState.Text = "Delete State";
            this.button_deleteState.UseVisualStyleBackColor = true;
            this.button_deleteState.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button__cancel
            // 
            this.button__cancel.Location = new System.Drawing.Point(219, 3);
            this.button__cancel.Name = "button__cancel";
            this.button__cancel.Size = new System.Drawing.Size(110, 25);
            this.button__cancel.TabIndex = 6;
            this.button__cancel.Text = "Cancel";
            this.button__cancel.UseVisualStyleBackColor = true;
            this.button__cancel.Click += new System.EventHandler(this.button__cancel_Click);
            // 
            // tableLayoutPanel_buttonPanel
            // 
            this.tableLayoutPanel_buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_buttonPanel.ColumnCount = 2;
            this.tableLayoutPanel_buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_buttonPanel.Controls.Add(this.button_confirm, 0, 0);
            this.tableLayoutPanel_buttonPanel.Controls.Add(this.button__cancel, 1, 0);
            this.tableLayoutPanel_buttonPanel.Location = new System.Drawing.Point(94, 494);
            this.tableLayoutPanel_buttonPanel.Name = "tableLayoutPanel_buttonPanel";
            this.tableLayoutPanel_buttonPanel.RowCount = 1;
            this.tableLayoutPanel_buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel_buttonPanel.Size = new System.Drawing.Size(432, 37);
            this.tableLayoutPanel_buttonPanel.TabIndex = 7;
            // 
            // button_confirm
            // 
            this.button_confirm.Location = new System.Drawing.Point(3, 3);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(110, 25);
            this.button_confirm.TabIndex = 7;
            this.button_confirm.Text = "Confirm";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // splitContainer_selectView
            // 
            this.splitContainer_selectView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer_selectView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_selectView.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_selectView.Name = "splitContainer_selectView";
            // 
            // splitContainer_selectView.Panel1
            // 
            this.splitContainer_selectView.Panel1.Controls.Add(this.button_renameState);
            this.splitContainer_selectView.Panel1.Controls.Add(this.listBox_stateNames);
            this.splitContainer_selectView.Panel1.Controls.Add(this.button_addState);
            this.splitContainer_selectView.Panel1.Controls.Add(this.button_deleteState);
            // 
            // splitContainer_selectView.Panel2
            // 
            this.splitContainer_selectView.Panel2.Controls.Add(this.checkBox_selectAll);
            this.splitContainer_selectView.Panel2.Controls.Add(this.tabControl_selectView);
            this.splitContainer_selectView.Size = new System.Drawing.Size(515, 276);
            this.splitContainer_selectView.SplitterDistance = 125;
            this.splitContainer_selectView.TabIndex = 8;
            // 
            // button_renameState
            // 
            this.button_renameState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_renameState.Location = new System.Drawing.Point(5, 217);
            this.button_renameState.Name = "button_renameState";
            this.button_renameState.Size = new System.Drawing.Size(110, 25);
            this.button_renameState.TabIndex = 6;
            this.button_renameState.Text = "Rename";
            this.button_renameState.UseVisualStyleBackColor = true;
            this.button_renameState.Click += new System.EventHandler(this.button_renameState_Click);
            // 
            // listBox_stateNames
            // 
            this.listBox_stateNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_stateNames.FormattingEnabled = true;
            this.listBox_stateNames.ItemHeight = 12;
            this.listBox_stateNames.Location = new System.Drawing.Point(3, 2);
            this.listBox_stateNames.Name = "listBox_stateNames";
            this.listBox_stateNames.Size = new System.Drawing.Size(113, 184);
            this.listBox_stateNames.TabIndex = 0;
            this.listBox_stateNames.SelectedIndexChanged += new System.EventHandler(this.listBox_StateNames_SelectedIndexChanged);
            // 
            // checkBox_selectAll
            // 
            this.checkBox_selectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_selectAll.AutoSize = true;
            this.checkBox_selectAll.Location = new System.Drawing.Point(7, 254);
            this.checkBox_selectAll.Name = "checkBox_selectAll";
            this.checkBox_selectAll.Size = new System.Drawing.Size(84, 16);
            this.checkBox_selectAll.TabIndex = 1;
            this.checkBox_selectAll.Text = "Select All";
            this.checkBox_selectAll.UseVisualStyleBackColor = true;
            this.checkBox_selectAll.CheckedChanged += new System.EventHandler(this.checkBox_selectAll_CheckedChanged);
            // 
            // tabControl_selectView
            // 
            this.tabControl_selectView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_selectView.Controls.Add(this.tabPage_enabledSelect);
            this.tabControl_selectView.Controls.Add(this.tabPage_visibleSelect);
            this.tabControl_selectView.Location = new System.Drawing.Point(0, 0);
            this.tabControl_selectView.Name = "tabControl_selectView";
            this.tabControl_selectView.SelectedIndex = 0;
            this.tabControl_selectView.Size = new System.Drawing.Size(382, 252);
            this.tabControl_selectView.TabIndex = 0;
            this.tabControl_selectView.SelectedIndexChanged += new System.EventHandler(this.tabControl_selectView_SelectedIndexChanged);
            // 
            // tabPage_enabledSelect
            // 
            this.tabPage_enabledSelect.Controls.Add(this.dataGridView_enabledSelect);
            this.tabPage_enabledSelect.Location = new System.Drawing.Point(4, 22);
            this.tabPage_enabledSelect.Name = "tabPage_enabledSelect";
            this.tabPage_enabledSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_enabledSelect.Size = new System.Drawing.Size(374, 226);
            this.tabPage_enabledSelect.TabIndex = 0;
            this.tabPage_enabledSelect.Text = "Enabled";
            this.tabPage_enabledSelect.UseVisualStyleBackColor = true;
            // 
            // dataGridView_enabledSelect
            // 
            this.dataGridView_enabledSelect.AllowUserToAddRows = false;
            this.dataGridView_enabledSelect.AllowUserToDeleteRows = false;
            this.dataGridView_enabledSelect.AllowUserToResizeRows = false;
            this.dataGridView_enabledSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_enabledSelect.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView_enabledSelect.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_enabledSelect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView_enabledSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_enabledSelect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.ControlName,
            this.Value});
            this.dataGridView_enabledSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_enabledSelect.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_enabledSelect.MultiSelect = false;
            this.dataGridView_enabledSelect.Name = "dataGridView_enabledSelect";
            this.dataGridView_enabledSelect.RowHeadersVisible = false;
            this.dataGridView_enabledSelect.RowTemplate.Height = 23;
            this.dataGridView_enabledSelect.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_enabledSelect.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_enabledSelect.Size = new System.Drawing.Size(368, 220);
            this.dataGridView_enabledSelect.TabIndex = 0;
            this.dataGridView_enabledSelect.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_enabledSelect_CellValueChanged);
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Select.FillWeight = 1F;
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Select.ToolTipText = "Select the control type";
            this.Select.Width = 50;
            // 
            // ControlName
            // 
            this.ControlName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ControlName.DefaultCellStyle = dataGridViewCellStyle8;
            this.ControlName.HeaderText = "Control Name";
            this.ControlName.Name = "ControlName";
            this.ControlName.ReadOnly = true;
            // 
            // Value
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Value.DefaultCellStyle = dataGridViewCellStyle9;
            this.Value.FillWeight = 1F;
            this.Value.HeaderText = "Value";
            this.Value.Items.AddRange(new object[] {
            "True",
            "False"});
            this.Value.Name = "Value";
            // 
            // tabPage_visibleSelect
            // 
            this.tabPage_visibleSelect.Controls.Add(this.dataGridView_visibleSelect);
            this.tabPage_visibleSelect.Location = new System.Drawing.Point(4, 22);
            this.tabPage_visibleSelect.Name = "tabPage_visibleSelect";
            this.tabPage_visibleSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_visibleSelect.Size = new System.Drawing.Size(374, 226);
            this.tabPage_visibleSelect.TabIndex = 1;
            this.tabPage_visibleSelect.Text = "Visible";
            this.tabPage_visibleSelect.UseVisualStyleBackColor = true;
            // 
            // dataGridView_visibleSelect
            // 
            this.dataGridView_visibleSelect.AllowUserToAddRows = false;
            this.dataGridView_visibleSelect.AllowUserToDeleteRows = false;
            this.dataGridView_visibleSelect.AllowUserToResizeRows = false;
            this.dataGridView_visibleSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_visibleSelect.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView_visibleSelect.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_visibleSelect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView_visibleSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_visibleSelect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewComboBoxColumn1});
            this.dataGridView_visibleSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_visibleSelect.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_visibleSelect.MultiSelect = false;
            this.dataGridView_visibleSelect.Name = "dataGridView_visibleSelect";
            this.dataGridView_visibleSelect.RowHeadersVisible = false;
            this.dataGridView_visibleSelect.RowTemplate.Height = 23;
            this.dataGridView_visibleSelect.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_visibleSelect.Size = new System.Drawing.Size(368, 220);
            this.dataGridView_visibleSelect.TabIndex = 1;
            this.dataGridView_visibleSelect.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_visibleSelect_CellValueChanged);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewCheckBoxColumn1.FillWeight = 1F;
            this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.ToolTipText = "Select the control type";
            this.dataGridViewCheckBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn1.HeaderText = "Control Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewComboBoxColumn1
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewComboBoxColumn1.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewComboBoxColumn1.FillWeight = 1F;
            this.dataGridViewComboBoxColumn1.HeaderText = "Value";
            this.dataGridViewComboBoxColumn1.Items.AddRange(new object[] {
            "True",
            "False"});
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            // 
            // checkedListBox_selectControl
            // 
            this.checkedListBox_selectControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox_selectControl.FormattingEnabled = true;
            this.checkedListBox_selectControl.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox_selectControl.Name = "checkedListBox_selectControl";
            this.checkedListBox_selectControl.Size = new System.Drawing.Size(511, 192);
            this.checkedListBox_selectControl.TabIndex = 1;
            this.checkedListBox_selectControl.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_selectControl_ItemCheck);
            // 
            // splitContainer_operationPanel
            // 
            this.splitContainer_operationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_operationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer_operationPanel.Location = new System.Drawing.Point(11, 12);
            this.splitContainer_operationPanel.Name = "splitContainer_operationPanel";
            this.splitContainer_operationPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_operationPanel.Panel1
            // 
            this.splitContainer_operationPanel.Panel1.Controls.Add(this.checkedListBox_selectControl);
            // 
            // splitContainer_operationPanel.Panel2
            // 
            this.splitContainer_operationPanel.Panel2.Controls.Add(this.splitContainer_selectView);
            this.splitContainer_operationPanel.Size = new System.Drawing.Size(515, 476);
            this.splitContainer_operationPanel.SplitterDistance = 196;
            this.splitContainer_operationPanel.TabIndex = 9;
            // 
            // ViewControllerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 533);
            this.Controls.Add(this.splitContainer_operationPanel);
            this.Controls.Add(this.tableLayoutPanel_buttonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ViewControllerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Controller";
            this.tableLayoutPanel_buttonPanel.ResumeLayout(false);
            this.splitContainer_selectView.Panel1.ResumeLayout(false);
            this.splitContainer_selectView.Panel2.ResumeLayout(false);
            this.splitContainer_selectView.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_selectView)).EndInit();
            this.splitContainer_selectView.ResumeLayout(false);
            this.tabControl_selectView.ResumeLayout(false);
            this.tabPage_enabledSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_enabledSelect)).EndInit();
            this.tabPage_visibleSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_visibleSelect)).EndInit();
            this.splitContainer_operationPanel.Panel1.ResumeLayout(false);
            this.splitContainer_operationPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_operationPanel)).EndInit();
            this.splitContainer_operationPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_addState;
        private System.Windows.Forms.Button button_deleteState;
        private System.Windows.Forms.Button button__cancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_buttonPanel;
        private System.Windows.Forms.SplitContainer splitContainer_selectView;
        private System.Windows.Forms.ListBox listBox_stateNames;
        private System.Windows.Forms.TabControl tabControl_selectView;
        private System.Windows.Forms.TabPage tabPage_enabledSelect;
        private System.Windows.Forms.DataGridView dataGridView_enabledSelect;
        private System.Windows.Forms.TabPage tabPage_visibleSelect;
        private System.Windows.Forms.DataGridView dataGridView_visibleSelect;
        private System.Windows.Forms.CheckedListBox checkedListBox_selectControl;
        private System.Windows.Forms.SplitContainer splitContainer_operationPanel;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.Button button_renameState;
        private System.Windows.Forms.CheckBox checkBox_selectAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlName;
        private System.Windows.Forms.DataGridViewComboBoxColumn Value;
    }
}
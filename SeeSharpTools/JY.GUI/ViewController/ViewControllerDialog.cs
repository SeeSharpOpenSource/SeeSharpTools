using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    public partial class ViewControllerDialog : Form
    {
        public ViewControllerDialog(ViewController viewController, Form parentForm)
        {
            InitializeComponent();
            this._viewController = viewController;
            ClearSelectionPanel();
            InitStateNameList();
            CopyControlElements();
            InitControlList(parentForm);
            RefreshSelectAllState();
            // TODO init Controls
        }

        private void InitControlList(Form parentForm)
        {
            _controls.Clear();
            if (null == parentForm ||  0 == parentForm.Controls.Count)
            {
                return;
            }
            RecursiveAddControlsToList(parentForm.Controls);

            for (int i = 0; i < _controls.Count; i++)
            {
                Control item = _controls[i];
                string controlName = item.Name;
                ViewControlElement controlElement = _controlElements.Find(control => controlName.Equals(control.Name));
                if (null == controlElement)
                {
                    _controlElements.Add(new ViewControlElement(item));
                }
                else if (!controlElement.IsEmpty)
                {
                    checkedListBox_selectControl.SetItemChecked(i, true);
                }
            }
        }

        private void RecursiveAddControlsToList(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (null == control || ReferenceEquals(control, _viewController) || string.IsNullOrWhiteSpace(control.Name))
                {
                    continue;
                }
                _controls.Add(control);
                checkedListBox_selectControl.Items.Add(control.Name);
                if (!(control is UserControl) && control.Controls.Count > 0)
                {
                    RecursiveAddControlsToList(control.Controls);
                }
            }
        }

        private readonly List<string> _stateNames = new List<string>(8);
        private readonly IList<Control> _controls = new List<Control>(20);

        private readonly ViewController _viewController;

        private List<ViewControlElement> _controlElements;

        private void CopyControlElements()
        {
            _controlElements = new List<ViewControlElement>(20);
            if (null == _viewController.ControlInfos)
            {
                return;
            }
            foreach (string controlInfo in _viewController.ControlInfos)
            {
                _controlElements.Add(new ViewControlElement(controlInfo, _viewController.StateNames));
            }
        }

        private void InitStateNameList()
        {
            listBox_stateNames.Items.Clear();
            _stateNames.Clear();
            if (null == _viewController.StateNames || 0 == _viewController.StateNames.Length)
            {
                _stateNames.Add("State1");
            }
            else
            {
                _stateNames.AddRange(_viewController.StateNames);
            }
            listBox_stateNames.Items.AddRange(_stateNames.ToArray());
            listBox_stateNames.SelectedIndex = 0;
        }

        private void ClearSelectionPanel()
        {
            dataGridView_enabledSelect.Rows.Clear();
            dataGridView_visibleSelect.Rows.Clear();
        }

        private void listBox_StateNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGridViewData(dataGridView_enabledSelect.Rows, 0, ViewControlElement.DefaultValue[0]);
            RefreshDataGridViewData(dataGridView_visibleSelect.Rows, 1, ViewControlElement.DefaultValue[1]);
            RefreshSelectAllState();
        }

        private bool _isInternalRefreshOperation = false;
        private void RefreshDataGridViewData(DataGridViewRowCollection rows, int propertyIndex, object defaultValue)
        {
            _isInternalRefreshOperation = true;
            if (listBox_stateNames.SelectedIndex < 0)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    rows[i].Cells[0].Value = false;
                    rows[i].Cells[2].Value = defaultValue.ToString();
                }
            }
            else
            {
                string currentState = listBox_stateNames.Text;
                for (int i = 0; i < rows.Count; i++)
                {
                    DataGridViewRow rowData = rows[i];
                    string controlName = rowData.Cells[1].Value.ToString();
                    ViewControlElement controlElement = GetViewControlElement(controlName);
                    object[] values = controlElement.GetValue(currentState);
                    if (null == values)
                    {
                        rows[i].Cells[0].Value = false;
                        rows[i].Cells[2].Value = defaultValue.ToString();
                    }
                    else
                    {
                        object propertyValue = values[propertyIndex];
                        rows[i].Cells[0].Value = (null != propertyValue);
                        rows[i].Cells[2].Value = propertyValue?.ToString() ?? defaultValue.ToString();
                    }
                }
            }
            _isInternalRefreshOperation = false;
        }

        private void button_addState_Click(object sender, EventArgs e)
        {
            StateOperationDialog stateOperationDialog = new StateOperationDialog(string.Empty);
            stateOperationDialog.ShowDialog(this);
            if (_stateNames.Any(item => stateOperationDialog.StateName.ToLower().Equals(item.ToLower())))
            {
                MessageBox.Show("State name already exist", "ViewController", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(stateOperationDialog.StateName))
            {
                return;
            }
            _stateNames.Add(stateOperationDialog.StateName);
            listBox_stateNames.Items.Add(stateOperationDialog.StateName);
            if (1 == _stateNames.Count)
            {
                listBox_stateNames.SelectedIndex = 0;
            }

            RefreshDataGridViewData(dataGridView_enabledSelect.Rows, 0, ViewControlElement.DefaultValue[0]);
            RefreshDataGridViewData(dataGridView_visibleSelect.Rows, 1, ViewControlElement.DefaultValue[1]);
        }


        private void button_renameState_Click(object sender, EventArgs e)
        {
            string originalStateName = listBox_stateNames.Text;
            StateOperationDialog stateOperationDialog = new StateOperationDialog(originalStateName);
            stateOperationDialog.ShowDialog(this);
            string newStateName = stateOperationDialog.StateName;
            if (originalStateName.ToLower().Equals(newStateName.ToLower()))
            {
                return;
            }
            if (_stateNames.Any(item => stateOperationDialog.StateName.ToLower().Equals(item.ToLower())))
            {
                MessageBox.Show("State name already exist", "ViewController", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            _stateNames[_stateNames.IndexOf(originalStateName)] = newStateName;
            listBox_stateNames.Items[listBox_stateNames.SelectedIndex] = newStateName;
            foreach (ViewControlElement controlElement in _controlElements)
            {
                controlElement.RenameState(originalStateName, newStateName);
            }

            RefreshDataGridViewData(dataGridView_enabledSelect.Rows, 0, ViewControlElement.DefaultValue[0]);
            RefreshDataGridViewData(dataGridView_visibleSelect.Rows, 1, ViewControlElement.DefaultValue[1]);
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            string currentState = listBox_stateNames.Text;
            _stateNames.Remove(currentState);
            listBox_stateNames.Items.RemoveAt(listBox_stateNames.SelectedIndex);
            if (0 != _stateNames.Count)
            {
                listBox_stateNames.SelectedIndex = 0;
            }

            RefreshDataGridViewData(dataGridView_enabledSelect.Rows, 0, ViewControlElement.DefaultValue[0]);
            RefreshDataGridViewData(dataGridView_visibleSelect.Rows, 1, ViewControlElement.DefaultValue[1]);
        }

        private void checkedListBox_selectControl_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == e.CurrentValue)
            {
                return;
            }
            Control control = _controls[e.Index];
            ViewControlElement controlElement =
                    _controlElements.Find(item => item.Name.Equals(control.Name));
            int rowIndex = -1;
            for (int i = 0; i < dataGridView_enabledSelect.Rows.Count; i++)
            {
                if (control.Name.Equals(dataGridView_enabledSelect.Rows[i].Cells[1].Value.ToString()))
                {
                    rowIndex = i;
                }
            }
            if (rowIndex >= 0)
            {
                dataGridView_enabledSelect.Rows.RemoveAt(rowIndex);
                dataGridView_visibleSelect.Rows.RemoveAt(rowIndex);
            }
            if (e.NewValue == CheckState.Checked)
            {
                AddDataToTable(controlElement);
            }
            RefreshSelectAllState();
        }

        private void AddDataToTable(ViewControlElement controlElement)
        {
            
            object[] values = controlElement.GetValue(listBox_stateNames.Text);
            if (null == values)
            {
                dataGridView_enabledSelect.Rows.Add(false, controlElement.Name, ViewControlElement.DefaultValue[0].ToString());
                dataGridView_visibleSelect.Rows.Add(false, controlElement.Name, ViewControlElement.DefaultValue[1].ToString());
            }
            else
            {
                AddSingleTableData(dataGridView_enabledSelect.Rows, controlElement.Name, values[0], ViewControlElement.DefaultValue[0]);
                AddSingleTableData(dataGridView_visibleSelect.Rows, controlElement.Name, values[1], ViewControlElement.DefaultValue[1]);
            }
        }

        private void AddSingleTableData(DataGridViewRowCollection rows, string propertyName, object value, object defaultValue)
        {
            if (null == value)
            {
                rows.Add(false, propertyName, defaultValue.ToString());
            }
            else
            {
                rows.Add(true, propertyName, value.ToString());
            }
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            _viewController.StateNames = new string[listBox_stateNames.Items.Count];
            for (int i = 0; i < listBox_stateNames.Items.Count; i++)
            {
                _viewController.StateNames[i] = listBox_stateNames.Items[i].ToString();
            }

            List<string> controlInfos = new List<string>(_controlElements.Count);
            foreach (ViewControlElement controlElement in _controlElements)
            {
                if (controlElement.IsEmpty)
                {
                    continue;
                }
                controlInfos.Add(controlElement.ToString());
            }
            _viewController.ControlInfos = controlInfos.ToArray();
            this.Dispose();
        }

        private void button__cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView_enabledSelect_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            RefreshControlElementValue(dataGridView_enabledSelect.Rows, 0, e);
        }

        private void dataGridView_visibleSelect_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            RefreshControlElementValue(dataGridView_visibleSelect.Rows, 1, e);
        }

        private void RefreshControlElementValue(DataGridViewRowCollection rows, int propertyIndex, 
            DataGridViewCellEventArgs eventArgs)
        {
            if (_isInternalRefreshOperation || 1 == eventArgs.ColumnIndex || eventArgs.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow rowData = rows[eventArgs.RowIndex];
            string controlName = rowData.Cells[1].Value.ToString();
            ViewControlElement controlElement = GetViewControlElement(controlName);
            bool isSelected = (bool)rowData.Cells[0].Value;
            object propertyValue = isSelected ? rowData.Cells[2].Value : null;
            controlElement.SetValue(listBox_stateNames.Text, ViewControlElement.PropertyNames[propertyIndex],
                propertyValue);
            RefreshSelectAllState();
        }

        private ViewControlElement GetViewControlElement(string controlName)
        {
            ViewControlElement controlElement = _controlElements.First(item => item.Name.Equals(controlName));
            if (null == controlElement)
            {
                controlElement = new ViewControlElement(_controls.First(item => item.Name.Equals(controlName)));
                _controlElements.Add(controlElement);
            }
            return controlElement;
        }

        private void checkBox_selectAll_CheckedChanged(object sender, EventArgs e)
        {
            DataGridView selectView = tabControl_selectView.SelectedTab.Controls[0] as DataGridView;
            if (null == selectView || CheckState.Indeterminate == checkBox_selectAll.CheckState)
            {
                return;
            }
            _isAllSelectOperation = true;
            for (int i = 0; i < selectView.RowCount; i++)
            {
                DataGridViewRow rowData = selectView.Rows[i];
                rowData.Cells[0].Value = checkBox_selectAll.Checked;
            }
            _isAllSelectOperation = false;
        }

        private void tabControl_selectView_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectAllState();
        }

        private bool _isAllSelectOperation = false;
        private void RefreshSelectAllState()
        {
            DataGridView selectView = tabControl_selectView.SelectedTab.Controls[0] as DataGridView;
            if (null == selectView || _isAllSelectOperation)
            {
                return;
            }
            bool allSelect = true;
            bool allDeSelect = true;
            for (int i = 0; i < selectView.RowCount; i++)
            {
                DataGridViewRow rowData = selectView.Rows[i];
                bool selected = (bool) rowData.Cells[0].Value;
                allSelect &= selected;
                allDeSelect &= !selected;
            }
            if (allSelect)
            {
                checkBox_selectAll.CheckState = CheckState.Checked;
            }
            else if (allDeSelect)
            {
                checkBox_selectAll.CheckState = CheckState.Unchecked;
            }
            else
            {
                checkBox_selectAll.CheckState = CheckState.Indeterminate;
            }
        }
    }
}

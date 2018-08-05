using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/// <summary>
/// /// 修改日期：2017.06.24
/// 作者： 简仪科技
/// 软件版本： SeeSharpTool v1.2.1
/// 描述：  1.新增row数字标记
///         2.新增状态显示，标记选择的位置和总数组的大小
///         3.可变更column名称
///         4.可读出数组(double)
/// </summary>
namespace SeeSharpTools.JY.GUI
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(JYArray), "JYArray.JYArray.bmp")]
    public partial class JYArray : UserControl
    {
        #region Private Data
        private int _rank;
        private DataTable dt;
        private int _maxWidth;
        private bool _autoSizeMode;
        /// <summary>
        /// temp table for initial usage
        /// </summary>
        private DataTable initTable;
        private bool showStatusBar;
        private int columnNumber;
        private int size = 0;
        private bool rotate1DArray=false;

        #endregion

        #region Constructor
        public JYArray()
        {
            InitializeComponent();
            // 设计器中自动配置了Name会导致在设计时获取控件名称失败
            this.Name = "";
            dgv.AllowUserToAddRows = false;
            dt = new DataTable();
            dgv.RowHeadersVisible = true;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.AllowUserToOrderColumns = false;            
            dgv.AllowUserToAddRows = false;                        
            dgv.AutoResizeColumns();
            Rank = 0;
            AutoSizeToContent = true;

            initTable = new DataTable();
            initTable.Columns.Add("Col_0");
            initTable.Columns.Add("Col_1");
            initTable.Columns.Add("Col_2");
            initTable.Columns.Add("Col_3");
            initTable.Columns.Add("Col_4");
            dgv.DataSource = initTable;
            dgv.Invalidate();
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Enable the autosize mode
        /// </summary>
        public bool AutoSizeToContent
        {
            get { return _autoSizeMode; }
            set { _autoSizeMode = value; }
        }

        /// <summary>
        /// Get/Set the names of columns
        /// </summary>
        public string[] ColumnTitles
        {
            get { return GetColNames(); }
            set { SetColNames(value); }
        }

        /// <summary>
        /// Enable/Diable the description of the selection
        /// </summary>
        public bool ShowDescription
        {
            get { return showStatusBar; }
            set
            {
                showStatusBar = value;
                statusStrip1.Visible = showStatusBar;
            }
        }

        /// <summary>
        /// Get/Set the size of the columns
        /// </summary>
        public int ColumnSize
        {
            get { return columnNumber; }
            set
            {
                columnNumber = value;
                ChangeColumnSize(columnNumber);
            }
        }

        public bool Rotate1DArray
        {
            get { return rotate1DArray; }
            set { rotate1DArray = value; }
        }

        /// <summary>
        /// Control reference for customer usage
        /// </summary>
        public DataGridView ControlRef
        {
            get { return dgv; }
        }
        #endregion

        #region Private Properties
        private int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add 2D array
        /// </summary>
        public void Add<T>(T[,] obj)
        {
            DataRow dr;
            dgv.DataSource = null;

            switch (Rank)
            {
                case 0:
                    dt.Clear();
                    dt.Columns.Clear();
                    ColumnSize = obj.GetLength(1);

                    for (int i = 0; i < obj.GetLength(0); i++)
                    {
                        dr = dt.NewRow();
                        for (int j = 0; j < obj.GetLength(1); j++)
                            dr[j] = obj[i, j];
                        dt.Rows.Add(dr);
                    }
                    Rank = 2;

                    break;
                case 1:
                    //dt是一维数组但新增二维数组
                    throw new Exception("Please add dataset with rank less than 2");
                case 2:
                    for (int i = 0; i < obj.GetLength(0); i++)
                    {
                        dr = dt.NewRow();
                        for (int j = 0; j < columnNumber; j++)
                            dr[j] = obj[i, j];
                        dt.Rows.Add(dr);
                    }

                    break;
                default:
                    return;
            }
            Apply2DGV(dt);
            ReSize();
            GetSize();

        }

        /// <summary>
        /// Add 1D array, 1D array will be initialize as row if isBuiling2DArray is true
        /// </summary>
        public void Add<T>(T[] obj)
        {
            DataRow dr;
            dgv.DataSource = null;

            switch (Rank)
            {
                case 0:
                    if (rotate1DArray)
                    {
                        dt.Clear();
                        dt.Columns.Clear();
                        ColumnSize = obj.Length;

                        dr = dt.NewRow();

                        for (int i = 0; i < obj.Length; i++)
                        {                            
                            dr[i] = obj[i];                        
                        }
                        dt.Rows.Add(dr);
                        Rank = 2;
                    }
                    else
                    {
                        dt.Clear();
                        dt.Columns.Clear();
                        ColumnSize = 1;
                        for (int i = 0; i < obj.Length; i++)
                        {
                            dr = dt.NewRow();
                            dr[0] = obj[i];
                            dt.Rows.Add(dr);
                        }
                        Rank = 1;
                    }
                    break;
                case 1:
                    for (int i = 0; i < obj.Length; i++)
                    {
                        dr = dt.NewRow();
                        dr[0] = obj[i];
                        dt.Rows.Add(dr);
                    }
                    break;
                case 2:
                    dr = dt.NewRow();

                    for (int i = 0; i < columnNumber; i++)
                        dr[i] = obj[i];
                    dt.Rows.Add(dr);
                    break;
                default:
                    break;
            }

            Apply2DGV(dt);
            ReSize();
            GetSize();

        }

        /// <summary>
        /// Add scalar
        /// </summary>
        public void Add<T>(T obj)
        {
            DataRow dr;

            dgv.DataSource = null;

            switch (Rank)
            {
                case 0:
                    dt.Clear();
                    dt.Columns.Clear();
                    ColumnSize = 1;
                    dr = dt.NewRow();
                    dr[0] = obj;
                    dt.Rows.Add(dr);
                    Rank = 1;

                    break;
                case 1:
                    dr = dt.NewRow();
                    dr[0] = obj;
                    dt.Rows.Add(dr);

                    break;
                case 2:
                    throw new Exception("Single element could not be added into 2D dataset.");
                default:
                    break;
            }

            Apply2DGV(dt);
            ReSize();
            GetSize();


        }

        /// <summary>
        /// Clear all the data and columns
        /// </summary>
        public void Clear()
        {
            dt.Clear();
            dt.Columns.Clear();
            
            dgv.DataSource = initTable;
            dgv.Invalidate();
            toolStripStatusLabel1.Text = toolStripStatusLabel2.Text = "";
            size = 0;
            Rank = 0;
        }

        /// <summary>
        /// Adaptively change the DGV's width according to content
        /// </summary>
        public void ReSize()
        {

            _maxWidth = this.Width;
            if (_autoSizeMode)
            {
                int width = 0;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    width += dgv.Columns[i].Width;
                }

                if (width <= _maxWidth)
                {
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    this.Width = _maxWidth;

                }
            }

        }

        public void GetValues(out double[,] data,int rowCnt=0, int colCnt=0)
        {
            int rowSize = rowCnt == 0 ? dt.Rows.Count : rowCnt;
            int colSize = colCnt == 0 ? dt.Columns.Count : colCnt;
            double[,] tmp = new double[rowSize, colSize];

            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    tmp[i, j] = Double.Parse(dt.Rows[i][j].ToString());
                }
            }
            data = tmp;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// [Internal] Get the size of the Data
        /// </summary>
        private void GetSize()
        {
            toolStripStatusLabel1.Text = String.Format("Size: R={0}, C={1}", dgv.RowCount.ToString(), dgv.ColumnCount.ToString());
        }

        /// <summary>
        /// Scale the width of the column according to the UserControl Width
        /// </summary>
        private void ChangeColumnSize(int colSize)
        {

            for (int i = 0; i < colSize; i++)
            {
                dt.Columns.Add("Col_" + (i.ToString()));
            }

        }

        /// <summary>
        /// [Internal] Get the names of columns
        /// </summary>
        private string[] GetColNames()
        {
            string[] names = new string[dgv.Columns.Count];
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                names[i] = dgv.Columns[i].Name;
            }
            return names;
        }

        /// <summary>
        /// [Internal] Set the names of columns
        /// </summary>
        private void SetColNames(string[] names)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = names[i];
            }
            Apply2DGV(dt);
        }

        /// <summary>
        /// When click, show the selected index
        /// </summary>
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripStatusLabel2.Text = String.Format("Selected Index: (R,C)=({0},{1})", dgv.SelectedCells[0].RowIndex.ToString(), dgv.SelectedCells[0].ColumnIndex.ToString());
        }

        private void Apply2DGV(DataTable table)
        {
            dgv.DataSource = table;
            dgv.Invalidate();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = i.ToString();
            }

        }

        #endregion

    }

}

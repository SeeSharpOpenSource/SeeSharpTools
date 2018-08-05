using System;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Drawing;

/// <summary>
/// PathControl
/// 作者： 简仪科技
/// 时间： 2017.07.13
/// 描述： 档案路径选择器，支持
///         1. 浏览电脑文件夹功能
///         2. 拖拉文件夹档案功能
///         3. 程序直接读写档案路径
/// </summary>
namespace SeeSharpTools.JY.GUI
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(PathControl), "PathControl.PathControl.bmp")]
    [Designer(typeof(PathControlDesigner))]
    public partial class PathControl : UserControl
    {
        #region Private Fields
        private string filePath = "";
        private string extFileType = "";
        private PathMode mode = PathMode.File;
        private FileInfo fi;
        string originalPath = "";

        #endregion

        #region Constructor
        public PathControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get/Set the file path of the control
        /// </summary>
        public string Path
        {
            get { return filePath; }
            set
            {
                filePath = FileChcek(value);
                textBox_filePath.Text = filePath;
            }
        }

        public PathMode BrowseMode
        {
            get { return mode; }
            set { mode = value; }
        }

        public string ExtFileType
        {
            get { return extFileType; }
            set
            {
                if (extFileType != value)
                {
                    filePath = "";
                    textBox_filePath.Text = filePath;
                    extFileType = value;
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Browse the folder in the PC when user click the browse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_browse_Click(object sender, EventArgs e)
        {
            switch (mode)
            {
                case PathMode.Folder:
                    if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
                    {
                        Path = folderBrowserDialog1.SelectedPath;
                    }
                    break;
                case PathMode.File:
                    if (string.IsNullOrEmpty(extFileType))
                    {
                        openFileDialog1.Filter = "所有档案|*.*";
                    }
                    else
                    {
                        openFileDialog1.Filter = extFileType + "档案|*." + extFileType;

                    }
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (openFileDialog1.CheckFileExists)
                        {
                            Path= openFileDialog1.FileName;
                        }
                        else
                        {
                            throw new Exception("File is not found!");
                        }
                    }
                    break;
                default:
                    break;
            }


        }

        /// <summary>
        /// Update the textbox when user drag the file onto the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePath_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files != null && files.Length != 0)
            {
                string path = FileChcek(files[0]);
                textBox_filePath.Text = path;
                filePath = path;
            }
        }

        /// <summary>
        /// Update the cursor indicator when mouse is moving into the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files != null && files.Length != 0)
                {
                    FileAttributes fa = File.GetAttributes(files[0]);
                    
                    if ((fa & FileAttributes.Directory) == FileAttributes.Directory )
                    {
                        if (mode == PathMode.Folder)
                        {
                            e.Effect = DragDropEffects.All;
                        }
                        else
                        {
                            e.Effect = DragDropEffects.None;
                        }
                    }
                    else
                    {
                        if (mode == PathMode.File )
                        {
                            if (new FileInfo(files[0]).Extension == "." + extFileType||extFileType=="")
                            {
                                e.Effect = DragDropEffects.All;
                            }
                        }
                        else
                        {
                            e.Effect = DragDropEffects.None;
                        }
                    }
                }

            }
        }

        private void textBox_filePath_Validated(object sender, EventArgs e)
        {
            Path = FileChcek(textBox_filePath.Text);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check if the file exists when new path is updated in the control
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string FileChcek(string path)
        {
            originalPath = Path;

            bool isFile = false; //file or folder
            bool isPathValid = false;

            //check 
            //1. if path exists
            //2. if the path is file or folder      
            if (path!="")
            {
                fi = new FileInfo(path);
                if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    isFile = false;
                    DirectoryInfo di = new DirectoryInfo(path);
                    isPathValid = di.Exists;
                }
                else
                {
                    isFile = true;
                    isPathValid = fi.Exists;
                }
            }
            else
            {
                return path;
            }
            
            if (isPathValid)
            {
                switch (mode)
                {
                    case PathMode.Folder:
                        if (isFile)       //browse mode is folder and path is filetype 
                        {
                            MessageBox.Show("Path is automatically set to the directory");
                            return fi.DirectoryName;
                        }
                        else              //browse mode is folder and path is foldertype
                            return path;
                    case PathMode.File:
                        if (isFile)       //browse mode is file and path is filetype
                        {
                            if (fi.Extension == "." + extFileType || extFileType == "")
                                return fi.FullName;
                            else          //the extention file type is not the same as user-assigned type
                            {
                                MessageBox.Show("Extension type is not matched, please assign the correct one or change the extention type.");
                                return originalPath;
                            }
                        }
                        else             //browse mode is file and path is foldertype
                        {
                            MessageBox.Show("Please assign the file path instead of directory");
                            return originalPath;
                        }

                    default:
                        return originalPath;
                }
            }
            else
            {
                MessageBox.Show("Cannot find the assigned path or folder");
                return originalPath;
            }


        }

        #endregion


    }

    public enum PathMode
    {
        Folder,
        File,
    }

}

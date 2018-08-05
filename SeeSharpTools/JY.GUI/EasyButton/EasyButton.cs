using System.ComponentModel;
using System.Drawing;
namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// Easy Button to use
    /// </summary>
    [Designer(typeof(EasyButtonDesigner))]
    [ToolboxBitmap(typeof(EasyButton), "EasyButton.EasyButton.PNG")]
    public partial class EasyButton : System.Windows.Forms.Button
    {
        #region Enum
        /// <summary>
        /// EasyButton类型的类型
        /// </summary>
        public enum ButtonPresetImage
        {
            /// <summary>
            /// 没有图标
            /// </summary>
            None,
            /// <summary>
            /// 确认图标
            /// </summary>
            Check,
            /// <summary>
            /// 关闭图标
            /// </summary>
            Close,
            /// <summary>
            /// 取消图标
            /// </summary>
            Cancel,
            /// <summary>
            /// 退后图标
            /// </summary>
            Back,
            /// <summary>
            /// 向下图标
            /// </summary>
            Down,
            /// <summary>
            /// 前进图标
            /// </summary>
            Go,
            /// <summary>
            /// 向上图标
            /// </summary>
            Up,
            /// <summary>
            /// 文件夹图标
            /// </summary>
            Folder,
            /// <summary>
            /// 刷新图标
            /// </summary>
            Refresh,
            /// <summary>
            /// 设置图标
            /// </summary>
            Setting,
            /// <summary>
            /// 文件打开图标
            /// </summary>
            FolderOpen,
            /// <summary>
            /// 文件删除图标
            /// </summary>
            DocumentDelete,
            /// <summary>
            /// 文件图标
            /// </summary>
            Document,
            /// <summary>
            /// 文件编辑图标
            /// </summary>
            DocumentEdit,
            /// <summary>
            /// 信息图标
            /// </summary>
            Info,
            /// <summary>
            /// 文件添加图标
            /// </summary>
            DocumentAdd,
            /// <summary>
            /// 全局图标
            /// </summary>
            Gobal,
            /// <summary>
            /// 计算图标
            /// </summary>
            Calculator,
            /// <summary>
            /// 日期图标
            /// </summary>
            Calendar,
            /// <summary>
            /// 打印图标
            /// </summary>
            Printer
        }

        #endregion


        #region Fields
        ButtonPresetImage _buttontype;
        #endregion


        #region Properties
        /// <summary>
        /// To show different Image and Text
        /// </summary>
        [Description("To show different Image and Text.")]
        [Category("Appearance")]
        public ButtonPresetImage PreSetImage
        {
            get
            {
                return _buttontype;
            }

            set
            {
                _buttontype = value;
                switch (_buttontype)
                {
                    case ButtonPresetImage.None:
                        this.Image = null;
                        break;
                    case ButtonPresetImage.Check:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.check;
                        break;
                    case ButtonPresetImage.Close:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.close;
                        break;
                    case ButtonPresetImage.Cancel:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.cancel;
                        break;
                    case ButtonPresetImage.Back:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.back;
                        break;
                    case ButtonPresetImage.Down:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.down;
                        break;
                    case ButtonPresetImage.Go:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.go;
                        break;
                    case ButtonPresetImage.Up:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.up;
                        break;
                    case ButtonPresetImage.Folder:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.folder;
                        break;
                    case ButtonPresetImage.Refresh:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.refresh;
                        break;
                    case ButtonPresetImage.Setting:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.setting;
                        break;
                    case ButtonPresetImage.FolderOpen:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.folder_open;
                        break;
                    case ButtonPresetImage.DocumentDelete:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.document_delete;
                        break;
                    case ButtonPresetImage.Document:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.document;
                        break;
                    case ButtonPresetImage.DocumentEdit:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.document_edit;
                        break;
                    case ButtonPresetImage.Info:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.info; ;
                        break;
                    case ButtonPresetImage.DocumentAdd:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.document_add;
                        break;
                    case ButtonPresetImage.Gobal:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.web;
                        break;
                    case ButtonPresetImage.Calculator:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.calculator;
                        break;
                    case ButtonPresetImage.Calendar:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.calendar;
                        break;
                    case ButtonPresetImage.Printer:
                        this.Image = SeeSharpTools.JY.GUI.Properties.Resources.printer;
                        break;
                    default:
                        break;
                }

                this.Invalidate();
            }
        }


        #endregion



        #region Construction / Deconstruction
        /// <summary>
        /// Easy Button to use
        /// </summary>
        public EasyButton()
        {
            InitializeComponent();
            // 设计器中自动配置了Name会导致在设计时获取控件名称失败
            this.Name = "";
            this.PreSetImage = ButtonPresetImage.None;
            this.Size = new Size(80, 32);
            //this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Text = "";
        }


        #endregion


        #region Public Methods
        #endregion
        
        #region Private Methods

        #endregion
        
        #region Threading

        #endregion
    }
}

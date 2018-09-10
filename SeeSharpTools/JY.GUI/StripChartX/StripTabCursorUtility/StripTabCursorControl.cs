using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI.StripTabCursorUtility
{
    internal partial class StripTabCursorControl : UserControl
    {
        // 视图显示区域和真实控件的像素差
        internal const int ViewPixelOffset = 2;
        public StripTabCursorControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.  
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            _minXBound = 0;
            _maxXBound = 10000;
            // 在PanelView上也生成MouseEnter和Leave的事件
            this.panel_view.MouseEnter += (sender, args) => this.OnMouseEnter(args);
            this.panel_view.MouseLeave += (sender, args) => this.OnMouseLeave(args);
        }

        private int _minXBound, _maxXBound;

        public Action RefreshAndShowView;

        public Color CursorColor
        {
            get { return panel_view.BackColor; }
            set { panel_view.BackColor = value; }
        }

        public void SetXBoundary(int minX, int maxX)
        {
            // 控件的位置需要视图和控件边界的偏移
            _minXBound = minX - ViewPixelOffset;
            _maxXBound = maxX - ViewPixelOffset;
        }

        private bool _isSelected = false;
        private void FlowCursor_MouseDown(object sender, MouseEventArgs e)
        {
            MoveCursorPosition(sender, e);
            RefreshAndShowView.Invoke();
            _isSelected = true;
        }

        private void FlowCursor_MouseUp(object sender, MouseEventArgs e)
        {
            _isSelected = false;
            RefreshAndShowView.Invoke();
        }

        private void FlowCursor_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelected)
            {
                MoveCursorPosition(sender, e);
            }
        }

        private void MoveCursorPosition(object sender, MouseEventArgs e)
        {
            // 获取如果鼠标对齐到valuePanel时控件的真实位置
            int xPos = this.Location.X + e.X;
            // 如果是控件本身触发，则前移两个像素是真正的位置。
            if (ReferenceEquals(sender, this))
            {
                xPos -= ViewPixelOffset;
            }
            if (xPos > _maxXBound)
            {
                xPos = _maxXBound;
            }
            else if (xPos < _minXBound)
            {
                xPos = _minXBound;
            }
            this.Location = new Point(xPos, this.Location.Y);
        }
    }
}

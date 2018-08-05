using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    [ToolboxBitmap(typeof(EasyChartX), "ViewController.ViewController.bmp")]
    [Designer(typeof(ViewControllerDesigner))]
    public class ViewController : UserControl
    {
        public event Action<int, int> PostListeners;
        public event Action<int, int> PreListeners;

        [Browsable(false)]
        private List<ViewControlElement> _controlStatusInfo;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = false; }
        }
        
        public ViewController(IContainer container) : base()
        {
            SetControlView();
            _stateNames = new string[0];
            _controlInfos = new string[0];
            _state = "";
            container.Add(this);
            const int defaultControlCount = 10;
            _controlStatusInfo = new List<ViewControlElement>(defaultControlCount);
        }

        public ViewController() : base()
        {
            SetControlView();
            _stateNames = new string[0];
            _controlInfos = new string[0];
            _state = "";
            const int defaultControlCount = 10;
            _controlStatusInfo = new List<ViewControlElement>(defaultControlCount);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Location = new Point(0, 0);
        }

        private string[] _stateNames;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string[] StateNames
        {
            get { return _stateNames; }
            set
            {
                if (null == value)
                {
                    return;
                }
                _stateNames = value; 
            }
        }

        private void SetControlView()
        {
            base.Visible = false;
            this.Width = 30;
            this.Height = 30;
            this.BorderStyle = BorderStyle.None;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ViewController));
            this.BackgroundImage = (Image)(resources.GetObject("$this.BackgroundImage"));
            this.MaximumSize = new Size(30, 30);
            this.MinimumSize = new Size(30, 30);
        }

        private string[] _controlInfos;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string[] ControlInfos
        {
            get { return _controlInfos; }
            set { _controlInfos = value; }
        }

        private string _state;
        [Browsable(false)]
        public string State
        {
            get { return _state; }
            set
            {
                int stateValue = FindIndex(_stateNames, value);
                if (null == _stateNames || stateValue < 0)
                {
                    _state = "";
                    return;
                }
                int lastStateValue = FindIndex(_stateNames, _state);
                _state = _stateNames[stateValue];
                PreListeners?.Invoke(lastStateValue, stateValue);
                SetControlStatus();
                PostListeners?.Invoke(lastStateValue, stateValue);
            }
        }

        private void ConstructControlInfo()
        {
            _controlStatusInfo.Clear();
            foreach (string controlInfo in _controlInfos)
            {
                _controlStatusInfo.Add(new ViewControlElement(controlInfo, this.ParentForm));
            }
        }

        [Browsable(false)]
        public int StateValue
        {
            get { return FindIndex(_stateNames, _state); }
            set
            {
                if (value < 0 || value >= _stateNames.Length)
                {
                    return;
                }
                State = _stateNames[value];
            }
        }

        private void SetControlStatus()
        {
            if (null != _controlInfos && _controlStatusInfo.Count != _controlInfos.Length)
            {
                ConstructControlInfo();
            }
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    foreach (ViewControlElement viewControlElement in _controlStatusInfo)
                    {
                        viewControlElement.ApplyConfig(_state);
                    }
                }));
            }
            else
            {
                foreach (ViewControlElement viewControlElement in _controlStatusInfo)
                {
                    viewControlElement.ApplyConfig(_state);
                }
            }
        }

        private void ClearControlStatus()
        {
//            foreach (var VARIABLE in COLLECTION)
//            {
//                
//            }
        }

        public void Clear()
        {
            // TODO to implement
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewController));
            this.SuspendLayout();
            // 
            // ViewController
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MaximumSize = new System.Drawing.Size(30, 30);
            this.MinimumSize = new System.Drawing.Size(30, 30);
            this.Name = "ViewController";
            this.Size = new System.Drawing.Size(30, 30);
            this.ResumeLayout(false);

        }

        private static int FindIndex(string[] collection, string value)
        {
            if (null == collection)
            {
                return -1;
            }
            for (int i = 0; i < collection.Length; i++)
            {
                if (collection[i].Equals(value, StringComparison.CurrentCultureIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
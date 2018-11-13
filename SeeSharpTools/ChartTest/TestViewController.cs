using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeeSharpTools.JY.GUI;

namespace ChartTest
{
    public partial class TestViewController : Form
    {
        public TestViewController()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
//            ViewControllerDialog dialog = new ViewControllerDialog(viewController1, this);
//            dialog.Show();
        }

        private void button_idle_Click(object sender, EventArgs e)
        {
//            viewController1.State = "idle";
        }

        private void button_state1_Click(object sender, EventArgs e)
        {
//            viewController1.State = "state1";
        }

        private void button_state2_Click(object sender, EventArgs e)
        {
//            viewController1.State = "state2";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeeSharpTools.JY.GUI
{
    public partial class StripChartXRangeYConfigForm : Form
    {
        public StripChartXRangeYConfigForm(StripChartXPlotArea hitPlotArea)
        {
            this._hitPlotArea = hitPlotArea;
            InitializeComponent();
            _lastYAutoScale = this._hitPlotArea.AxisY.AutoScale;
            _lastY2AutoScale = this._hitPlotArea.AxisY2.AutoScale;
            if (_lastYAutoScale)
            {
                this._hitPlotArea.AxisY.AutoScale = false;
                this._hitPlotArea.AxisY2.AutoScale = false;
            }

            textBox_primaryYMax.Text = _hitPlotArea.AxisY.Maximum.ToString();
            textBox_primaryYMin.Text = _hitPlotArea.AxisY.Minimum.ToString();

            textBox_secondaryYMax.Text = _hitPlotArea.AxisY2.Maximum.ToString();
            textBox_secondaryYMin.Text = _hitPlotArea.AxisY2.Minimum.ToString();
        }

        private bool _lastYAutoScale;
        private bool _lastY2AutoScale;
        private StripChartXPlotArea _hitPlotArea;

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SetAxisValue(_hitPlotArea.AxisY, double.Parse(textBox_primaryYMax.Text), 
                double.Parse(textBox_primaryYMin.Text));
            SetAxisValue(_hitPlotArea.AxisY2, double.Parse(textBox_secondaryYMax.Text),
                double.Parse(textBox_secondaryYMin.Text));
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (_lastYAutoScale)
            {
                _hitPlotArea.AxisY.AutoScale = _lastYAutoScale;
                _hitPlotArea.AxisY2.AutoScale = _lastY2AutoScale;
            }
            this.Close();
        }

        private void textBox_DoubleTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            double value = 0;
            if (double.TryParse(textBox.Text, out value))
            {
                return;
            }
            textBox.Text = GetOriginalValue(textBox).ToString();
        }

        private double GetOriginalValue(TextBox textBox)
        {
            if (ReferenceEquals(textBox, textBox_primaryYMax))
            {
                return _hitPlotArea.AxisY.Maximum;
            }
            else if (ReferenceEquals(textBox, textBox_primaryYMin))
            {
                return _hitPlotArea.AxisY.Minimum;
            }
            else if (ReferenceEquals(textBox, textBox_secondaryYMax))
            {
                return _hitPlotArea.AxisY2.Maximum;
            }
            else if (ReferenceEquals(textBox, textBox_secondaryYMin))
            {
                return _hitPlotArea.AxisY2.Minimum;
            }
            return 0;
        }

        private void SetAxisValue(StripChartXAxis axis, double max, double min)
        {
            if (max > axis.Minimum)
            {
                axis.Maximum = max;
                axis.Minimum = min;
            }
            else if (min < axis.Maximum)
            {
                axis.Minimum = min;
                axis.Maximum = max;
            }
        }
    }
}

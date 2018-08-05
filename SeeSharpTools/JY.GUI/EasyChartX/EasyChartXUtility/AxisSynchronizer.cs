using System;

namespace SeeSharpTools.JY.GUI.EasyChartXUtility
{
    internal class AxisSynchronizer
    {
        private readonly EasyChartXAxis _masterAxis;
        private readonly EasyChartXAxis _slaveAxis;

        public double SlaveMaxValue { get; private set; }
        public double SlaveMinValue { get; private set; }

        private double _shrinkRatio;
        private double _offset;

        public bool NeedSync { get; set; }

        public AxisSynchronizer(EasyChartXAxis masterAxis, EasyChartXAxis slaveAxis)
        {
            this._masterAxis = masterAxis;
            this._slaveAxis = slaveAxis;
            this._shrinkRatio = 1;
            this._offset = 0;
            this.NeedSync = false;
        }

        /// <summary>
        /// 初始化同步参数
        /// </summary>
        /// <param name="slaveMaxValue">无需同步时值为double.NaN</param>
        /// <param name="slaveMinValue">无需同步时值为double.NaN</param>
        public void InitSyncParam(double slaveMaxValue, double slaveMinValue)
        {
            if (double.IsNaN(slaveMaxValue) || double.IsNaN(slaveMinValue))
            {
                this.NeedSync = false;
            }
            else
            {
                if (_slaveAxis.AutoScale)
                {
                    Utility.RoundYRange(ref slaveMaxValue, ref slaveMinValue, _slaveAxis.IsLogarithmic);
                }
                else
                {
                    _slaveAxis.GetSpecifiedRange(out slaveMaxValue, out slaveMinValue);
                }
                
                if (_slaveAxis.IsLogarithmic)
                {
                    slaveMaxValue = Math.Log10(slaveMaxValue);
                    slaveMinValue = Math.Log10(slaveMinValue);
                }
                double masterMaxValue = _masterAxis.Maximum;
                double masterMinValue = _masterAxis.Minimum;
                if (_masterAxis.IsLogarithmic)
                {
                    masterMaxValue = Math.Log10(_masterAxis.Maximum);
                    masterMinValue = Math.Log10(_masterAxis.Minimum);
                }

                this.NeedSync = true;
                this._shrinkRatio = (slaveMaxValue - slaveMinValue) / (masterMaxValue - masterMinValue);
                this._offset = slaveMinValue - this._shrinkRatio* masterMinValue;

                this.SlaveMaxValue = slaveMaxValue;
                this.SlaveMinValue = slaveMinValue;
            }
        }

        public void SyncAxis()
        {
            if (!NeedSync)
            {
                return;
            }

            if (!_masterAxis.IsZoomed || double.IsNaN(_masterAxis.ViewMaximum) || double.IsNaN(_masterAxis.ViewMinimum))
            {
                double slaveMaxValue = SlaveMaxValue;
                double slaveMinValue = SlaveMinValue;
                if (_slaveAxis.IsLogarithmic)
                {
                    slaveMaxValue = Math.Pow(10, slaveMaxValue);
                    slaveMinValue = Math.Pow(10, slaveMinValue);
                }
                this._slaveAxis.SetSlaveAxisRange(slaveMaxValue, slaveMinValue);
            }
            else
            {
                double masterViewMax = _masterAxis.ViewMaximum;
                double masterViewMin = _masterAxis.ViewMinimum;
//                if (_masterAxis.IsLogarithmic)
//                {
//                    masterViewMax = Math.Pow(10, masterViewMax);
//                    masterViewMin = Math.Pow(10, masterViewMin);
//                }
                double maxValue = _shrinkRatio* masterViewMax + _offset;
                double minValue = _shrinkRatio* masterViewMin + _offset;
                if (_slaveAxis.IsLogarithmic)
                {
                    maxValue = Math.Pow(10, maxValue);
                    minValue = Math.Pow(10, minValue);
                }
                this._slaveAxis.SetSlaveAxisRange(maxValue, minValue);
            }
        }
    }
}
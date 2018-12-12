using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.TabCursorUtility;

namespace SeeSharpTools.JY.GUI
{
    public class TabCursorCollection : IList<TabCursor>
    {
        private readonly EasyChartX _parentChart;
        private readonly EasyChartXPlotArea _parentPlotArea;
        private readonly Chart _baseChart;
        private IList<TabCursor> _cursors;
        private readonly PositionAdapter _adapter;
        private bool _flowCursorEnableFlag;

        const int MaxCursorCount = 32;
        const string CursorNameFormat = "TabCursor{0}";

        private readonly Color[] _cursorPalette = new Color[]
        {
            Color.Cyan, Color.Yellow, Color.DeepPink, Color.Blue, Color.DarkGreen, Color.OrangeRed, Color.YellowGreen,
            Color.Brown
        };

        internal TabCursorCollection(EasyChartX parentChart, Chart baseChart, EasyChartXPlotArea parentPlotArea)
        {
            this._parentChart = parentChart;
            this._parentPlotArea = parentPlotArea;
            this._baseChart = baseChart;
            this._adapter = new PositionAdapter(baseChart, parentPlotArea);
            this._cursors = new List<TabCursor>(MaxCursorCount);
            this.CursorValueFormat = null;
            this.RunTimeEditable = true;
            _flowCursorEnableFlag = false;
//            this._baseChart.PostPaint += BaseChartOnPostPaint;
            // TODO to add _cursor code, get from parentchart
        }


        #region IList interface

        public IEnumerator<TabCursor> GetEnumerator()
        {
            return _cursors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TabCursor item)
        {
            if (_cursors.Any(cursor => cursor.Name.Equals(item.Name)) || _cursors.Count >= MaxCursorCount)
            {
                return;
            }
            if (0 == _cursors.Count)
            {
                _adapter.RefreshPosition();
            }
            item.Initialize(this);
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                for (int i = 1; i < MaxCursorCount + 1; i++)
                {
                    string cursorName = string.Format(CursorNameFormat, i);
                    if (!_cursors.Any(cursor => cursor.Name.Equals(cursorName)))
                    {
                        item.Name = cursorName;
                        item.Color = _cursorPalette[(i - 1)%_cursorPalette.Length];
                        break;
                    }
                }
            }
            _cursors.Add(item);
            _baseChart.Controls.Add(item.Control);
            _adapter.MoveCursorToTarget(item);
            SetCursorXBoundry(item);
            AttachOrDetachPaintEvent();
            _parentChart.OnTabCursorChanged(item, TabCursorOperation.CursorAdded, null);
        }

        

        public void Clear()
        {
            for (int i = _baseChart.Controls.Count - 1; i >= 0; i--)
            {
                if (_baseChart.Controls[i] is TabCursorControl)
                {
                    _baseChart.Controls.RemoveAt(i);
                }
            }
            _cursors.Clear();
            AttachOrDetachPaintEvent();
            _parentChart.OnTabCursorChanged(null, TabCursorOperation.CursorDeleted, null);
        }

        public bool Contains(TabCursor item)
        {
            return _cursors.Contains(item);
        }

        public void CopyTo(TabCursor[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public bool Remove(TabCursor item)
        {
            for (int i = _baseChart.Controls.Count - 1; i >= 0; i--)
            {
                if (!ReferenceEquals(_baseChart.Controls[i], item.Control)) continue;
                _baseChart.Controls.RemoveAt(i);
                break;
            }
            bool remove = _cursors.Remove(item);
            AttachOrDetachPaintEvent();
            _parentChart.OnTabCursorChanged(null, TabCursorOperation.CursorDeleted, null);
            return remove;
        }

        public int Count => _cursors.Count;
        public bool IsReadOnly => false;

        public int IndexOf(TabCursor item)
        {
            return _cursors.IndexOf(item);
        }

        public void Insert(int index, TabCursor item)
        {
            // TODO other task
            if (_cursors.Any(cursor => cursor.Name.Equals(item.Name)) || _cursors.Count >= MaxCursorCount)
            {
                return;
            }
            if (0 == _cursors.Count)
            {
                _adapter.RefreshPosition();
            }
            item.Initialize(this);
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                for (int i = 1; i < MaxCursorCount + 1; i++)
                {
                    string cursorName = string.Format(CursorNameFormat, i);
                    if (!_cursors.Any(cursor => cursor.Name.Equals(cursorName)))
                    {
                        item.Name = cursorName;
                        item.Color = _cursorPalette[(i - 1) % _cursorPalette.Length];
                        break;
                    }
                }
            }
            _cursors.Insert(index, item);
            _baseChart.Controls.Add(item.Control);
            _adapter.MoveCursorToTarget(item);
            SetCursorXBoundry(item);
            AttachOrDetachPaintEvent();
            _parentChart.OnTabCursorChanged(item, TabCursorOperation.CursorAdded, null);
        }

        public void RemoveAt(int index)
        {
            // TODO other task
            for (int i = 0; i < _baseChart.Controls.Count; i++)
            {
                if (ReferenceEquals(_baseChart.Controls[i], _cursors[index].Control))
                {
                    _baseChart.Controls.RemoveAt(i);
                    break;
                }
            }
            _cursors.RemoveAt(index);
            AttachOrDetachPaintEvent();
            _parentChart.OnTabCursorChanged(null, TabCursorOperation.CursorDeleted, null);
        }

        public TabCursor this[int index]
        {
            get { return _cursors[index]; }
            set { throw new NotSupportedException(); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Specify or get the format of cursor value that will be shown in tabcursor value tip.
        /// </summary>
        [Description("Specify or get the format of cursor value that will be shown in tabcursor value tip.")]
        public string CursorValueFormat { get; set; }

        /// <summary>
        /// Specify whether the element in collection can be add or delete by TabCursor Form
        /// </summary>
        [Description("Specify whether the element in collection can be add or delete by TabCursor Form")]
        public bool RunTimeEditable { get; set; }

        #endregion

        internal void AttachOrDetachPaintEvent()
        {
            bool cursorEnabled = _cursors.Any(item => item.Enabled);
            if (cursorEnabled && !_flowCursorEnableFlag)
            {
                RefreshAdapterAndCursorBoundry();
                this._baseChart.PostPaint += BaseChartOnPostPaint;
                _flowCursorEnableFlag = true;
            }
            else if (!cursorEnabled && _flowCursorEnableFlag)
            {
                this._baseChart.PostPaint -= BaseChartOnPostPaint;
                _flowCursorEnableFlag = false;
            }
        }

        private void BaseChartOnPostPaint(object sender, ChartPaintEventArgs eventArgs)
        {
            // 如果不是绘制Chart或者无需更新位置时将不执行重绘
            if (!ReferenceEquals(eventArgs.ChartElement.GetType(), typeof (Chart)) || !_cursors.Any(item => item.Enabled))
            {
                return;
            }
            if (_parentChart.IsPlotting() && _parentPlotArea.Enabled)
            {
                RefreshAdapterAndCursorBoundry();
            }
            else
            {
                foreach (TabCursor cursor in _cursors)
                {
                    cursor.Control.Visible = false;
                }
            }
        }

        private void RefreshAdapterAndCursorBoundry()
        {
            _adapter.RefreshPosition();
            int minXBound = (int)Math.Round(_adapter.PlotRealX);
            int maxXBound = (int)Math.Round(_adapter.PlotRealX + _adapter.PlotRealWidth);
            foreach (TabCursor cursor in _cursors)
            {
                cursor.Control.Visible = cursor.Enabled;
                cursor.Control.SetXBoundary(minXBound, maxXBound);
                _adapter.MoveCursorToTarget(cursor);
            }
        }

        public void RefreshCursorPosition(TabCursor cursor)
        {
            if (_parentChart.IsPlotting() && cursor.Enabled)
            {
                cursor.Control.Visible = true;
                
                _adapter.RefreshPosition();
                _adapter.MoveCursorToTarget(cursor);
            }
            else
            {
                cursor.Control.Visible = false;
            }
            // TODO 暂时Enable变化和Value变化都触发ValueChanged事件，后期再优化
            _parentChart.OnTabCursorChanged(cursor, TabCursorOperation.ValueChanged, null);
        }
        
        internal void RefreshCursorValue(TabCursor cursor)
        {
            _adapter.RefreshCursorValue(cursor);
        }

        internal void ShowCursorValue(TabCursor cursor, bool isShow)
        {
            double yValue = cursor.YValue;
            const string tabCursorInfoFormat = "{0}{1}X:{2}";
            string showInfo = string.Format(tabCursorInfoFormat, cursor.Name, Environment.NewLine, cursor.ValueString);
            // TODO just for test
            if (!double.IsNaN(yValue))
            {
                showInfo += $"{Environment.NewLine}Y:{yValue}";
            }

            _parentChart.ShowMarkerValue(showInfo, cursor.Control.Location, isShow);
        }

        private void SetCursorXBoundry(TabCursor item)
        {
            int minXBound = (int)Math.Round(_adapter.PlotRealX);
            int maxXBound = (int)Math.Round(_adapter.PlotRealX + _adapter.PlotRealWidth);
            item.Control.SetXBoundary(minXBound, maxXBound);
        }

        internal double GetYValue(ref double xValue, int seriesIndex)
        {
            double yValue = double.NaN;
            if (seriesIndex < 0 || seriesIndex > _parentChart.SeriesCount || !_parentChart.IsPlotting())
            {
                return yValue;
            }
            _parentChart.GetNearestPoint(ref xValue, out yValue, seriesIndex);
            return yValue;
        }
    }
}
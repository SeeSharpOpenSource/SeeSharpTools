using System;
using System.Drawing;
using System.Windows.Forms;
using SeeSharpTools.JY.GUI.TabCursorUtility;

namespace SeeSharpTools.JY.GUI.EasyChartXMarker.Painters
{
    internal class CrossMarkerPainter : MarkerPainter
    {
        private Pen _pen;
        private Point[] _points;

        public CrossMarkerPainter(PositionAdapter adapter, Color color, Control.ControlCollection container) : 
            base(DataMarkerType.Cross, adapter, color, container)
        {
            this._points = new Point[4];
            for (int i = 0; i < _points.Length; i++)
            {
                _points[i] = new Point();
            }
        }

        protected override void InitializePaintComponent(Color color)
        {
            if (null == _pen || !color.Equals(_pen.Color))
            {
                this._pen = new Pen(color);
            }
            _points[0].X = -1;
            _points[0].Y = -1;

            _points[1].X = MarkerSize;
            _points[1].Y = MarkerSize;

            _points[2].X = -1;
            _points[2].Y = MarkerSize;

            _points[3].X = MarkerSize;
            _points[3].Y = -1;
        }

        public override void PaintMarker(object sender, PaintEventArgs args)
        {
            args.Graphics.DrawLine(_pen, _points[0], _points[1]);
            args.Graphics.DrawLine(_pen, _points[2], _points[3]);
        }

        public override void Dispose()
        {
            base.Dispose();
            this._pen.Dispose();
        }
    }
}
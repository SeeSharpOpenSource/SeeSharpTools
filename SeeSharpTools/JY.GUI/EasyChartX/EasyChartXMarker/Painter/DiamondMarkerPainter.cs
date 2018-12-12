using System;
using System.Drawing;
using System.Windows.Forms;
using SeeSharpTools.JY.GUI.TabCursorUtility;

namespace SeeSharpTools.JY.GUI.EasyChartXMarker.Painters
{
    internal class DiamondMarkerPainter : MarkerPainter
    {
        private SolidBrush _brush;
        private Point[] _points;

        public DiamondMarkerPainter(PositionAdapter adapter, Color color, Control.ControlCollection container) : 
            base(DataMarkerType.Diamond, adapter, color, container)
        {
            this._points = new Point[4];
            for (int i = 0; i < _points.Length; i++)
            {
                _points[i] = new Point();
            }
        }

        protected override void InitializePaintComponent(Color color)
        {
            if (null == _brush || !color.Equals(_brush.Color))
            {
                this._brush = new SolidBrush(color);
            }
            int middlePoint = (MarkerSize - 1)/2;
            _points[0].X = middlePoint;
            _points[0].Y = -1;

            _points[1].X = MarkerSize;
            _points[1].Y = middlePoint;

            _points[2].X = middlePoint;
            _points[2].Y = MarkerSize;

            _points[3].X = 0;
            _points[3].Y = middlePoint;
        }

        public override void PaintMarker(object sender, PaintEventArgs args)
        {
            args.Graphics.FillPolygon(_brush, _points);
        }

        public override void Dispose()
        {
            base.Dispose();
            this._brush.Dispose();
        }
    }
}
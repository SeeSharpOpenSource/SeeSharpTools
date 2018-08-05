using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
//    [TypeConverter(typeof(ExpandableObjectConverter))]
//    [Serializable]
    public class EasyChartSeries : ISerializable
    {
        internal EasyChartSeries(string name)
        {
            _baseSeries = null;
            _name = name;
            _color = Color.Red;
            _lineWidth = LineWidth.Thin;
            _interpolation = Interpolation.FastLine;
            _markerType = PointStyle.None;
        }

        public EasyChartSeries()
        {
            _baseSeries = null;
            _name = "";
            _color = Color.Red;
            _lineWidth = LineWidth.Thin;
            _interpolation = Interpolation.FastLine;
            _markerType = PointStyle.None;
        }

        private Series _baseSeries;
//        private EasyChartSeriesCollection _collection;

        private string _name;
        [
            NotifyParentProperty(true)
        ]
        public string Name
        {
            get { return _name; }
            // TODO 暂时删除，不再做适配
            internal set
            {
                _name = value;
                if (null != _baseSeries)
                {
                    _baseSeries.Name = _name;
                }
            }
        }

        private Color _color;
        
        [
            NotifyParentProperty(true)
        ]
        public Color Color
        {
            get { return _color; }
            // TODO 暂时删除，不再做适配
            internal set
            {
                _color = value;
                if (null != _baseSeries)
                {
                    _baseSeries.Color = _color;
                }
            }
        }

        private LineWidth _lineWidth;
        [
            NotifyParentProperty(true)
        ]
        public LineWidth Width
        {
            get { return _lineWidth; }
            set
            {
                _lineWidth = value;
                if (null != _baseSeries)
                {
                    _baseSeries.BorderWidth = (int)_lineWidth;
                    _baseSeries.MarkerSize = 3*_baseSeries.BorderWidth + 2;
                }
            }
        }

        private Interpolation _interpolation;
        [
            NotifyParentProperty(true)
        ]
        public Interpolation InterpolationStyle
        {
            get { return _interpolation;}
            set
            {
                _interpolation = value;
                if (null != _baseSeries)
                {
                    _baseSeries.ChartType = (SeriesChartType) _interpolation;
                }
            }
        }

        private PointStyle _markerType;
        [
            NotifyParentProperty(true)
        ]
        public PointStyle MarkerType
        {
            get { return _markerType;}
            set
            {
                _markerType = value;
                if (null != _baseSeries)
                {
                    _baseSeries.MarkerStyle = (MarkerStyle) _markerType;
                }
            }
        }

        internal void AdaptBaseSeries(Series baseSeries)
        {
            if (null == baseSeries)
            {
                this._baseSeries = null;
                return;
            }
            // TODO 暂时删除，不再做适配
            this._name = baseSeries.Name;
            this._color = baseSeries.Color;
//            baseSeries.Name = this._name;
//            baseSeries.Color = this._color;
            baseSeries.BorderWidth = (int) this._lineWidth;
            baseSeries.ChartType = (SeriesChartType) this._interpolation;
            baseSeries.MarkerStyle = (MarkerStyle) this._markerType;

            this._baseSeries = baseSeries;
        }

        public enum LineWidth
        {
            Thin = 1,
            Middle = 2,
            Thick = 3
        }

        public enum Interpolation
        {
            Point = SeriesChartType.Point,
            FastLine = SeriesChartType.FastLine,
            Line = SeriesChartType.Line,
            StepLine = SeriesChartType.StepLine
        }

        public enum PointStyle
        {
            None = MarkerStyle.None,
            Square = MarkerStyle.Square,
            Circle = MarkerStyle.Circle,
            Diamond = MarkerStyle.Diamond,
            Triangle = MarkerStyle.Triangle,
            Cross = MarkerStyle.Cross,
            Star4 = MarkerStyle.Star4,
            Star5 = MarkerStyle.Star5,
            Star6 = MarkerStyle.Star6,
            Star10 = MarkerStyle.Star10,

        }

        public void Dispose()
        {
            // ignore
        }

//        public void SetSeriesCollecton(EasyChartSeriesCollection collection)
//        {
//            this._collection = collection;
//        }

        public void DetachBaseSeries()
        {
            this._baseSeries = null;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Color", Color);
            info.AddValue("Width", Width);
            info.AddValue("InterpolationStyle", InterpolationStyle);
            info.AddValue("MarkerType", MarkerType);
            info.AddValue("InterpolationStyle", InterpolationStyle);
            info.AddValue("MarkerType", MarkerType);


        }

    }
}
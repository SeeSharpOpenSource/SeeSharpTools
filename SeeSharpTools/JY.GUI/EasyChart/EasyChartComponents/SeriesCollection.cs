using System;
using System.Collections.Generic;
using SeeSharpTools.JY.GUI;

namespace SeeSharpTools.JY.GUI.EasyChartComponents
{
    public class SeriesCollection : List<EasyChartSeries>
    {
        public SeriesCollection() : base(EasyChart.maxSeriesToDraw)
        {

        }
    }
}
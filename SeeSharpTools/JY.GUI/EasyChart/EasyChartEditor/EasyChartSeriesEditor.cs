using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI.EasyChartEditor
{
    public class EasyChartSeriesEditor : CollectionEditor
    {
        public EasyChartSeriesEditor(Type type) : base(type)
        {
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(EasyChartSeries);
        }

        protected override object CreateInstance(Type itemType)
        {
            EasyChartSeries instance = (EasyChartSeries)itemType.Assembly.CreateInstance(itemType.FullName);
//            Context.Container.Add(instance); //重要！自动生成组件的设计时代码！ 
            EasyChart chart = Context.Instance as EasyChart;
            if (null == chart)
            {
                return instance;
            }
            chart.LineSeries.Add(instance);
            return instance;
        }

        protected override void DestroyInstance(object instance)
        {
            base.DestroyInstance(instance);
            if (!(instance is EasyChart))
            {
                return;
            }
            foreach (IComponent component in Context.Container.Components)
            {
                Context.Container.Remove(component);
            }
        }
        
    }
}
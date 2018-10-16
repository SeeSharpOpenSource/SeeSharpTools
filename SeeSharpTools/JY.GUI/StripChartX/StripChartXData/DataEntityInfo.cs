using System;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI.StripChartXData
{
    internal class DataEntityInfo
    {
        public int Capacity { get; set; }
        public StripChartX.XAxisDataType XType { get; set; }
        public int LineCount { get; set; }
        public Type DataType { get; set; }

        public DataEntityInfo()
        {
            this.Capacity = 0;
            this.XType = StripChartX.XAxisDataType.Index;
            this.LineCount = 0;
            this.DataType = null;
        }

        public bool IsEqual(DataEntityInfo src)
        {
            return this.XType == src.XType && ReferenceEquals(DataType, src.DataType) && 
                   this.LineCount == src.LineCount && this.Capacity == src.Capacity;
        }

        public void Copy(DataEntityInfo src)
        {
            this.Capacity = src.Capacity;
            this.XType = src.XType;
            this.LineCount = src.LineCount;
            this.DataType = src.DataType;
        }
    }
}

using SeeSharpTools.JY.GUI.DigitalChartUtility;

namespace SeeSharpTools.JY.GUI.DigitalChartData
{
    internal class DataEntityInfo
    {
        public int Size { get; set; }
        public bool IsDeepCopy { get; set; }
        public XDataInputType XDataInputType { get; set; }
//        public XDataType XDataType { get; set; }
        public int LineNum { get; set; }

        public DataEntityInfo()
        {
            this.Size = 0;
            this.IsDeepCopy = true;
            this.XDataInputType = XDataInputType.Array;
//            this.XDataType = XDataType.Number;
            this.LineNum = 0;
        }

        public bool Equals(DataEntityInfo src)
        {
            return this.IsDeepCopy == src.IsDeepCopy && this.XDataInputType == src.XDataInputType &&
                   this.LineNum == src.LineNum &&
                   !(this.Size <= Constants.MaxPointsInSingleSeries ^ src.Size <= Constants.MaxPointsInSingleSeries);
        }

        public bool IsNeedAdaptXBuffer(DataEntityInfo latestInfo)
        {
//            return NotNeedDeepCopyXPlotBuffer() ^ latestInfo.NotNeedDeepCopyXPlotBuffer();
            // TODO 为了保证稳定性，暂时强制配置
            return true;
        }

        public bool IsNeedAdaptYBuffer(DataEntityInfo latestInfo)
        {
            bool notNeedDeepCopyYPlotBuffer = NotNeedDeepCopyYPlotBuffer();
            return (notNeedDeepCopyYPlotBuffer ^ latestInfo.NotNeedDeepCopyYPlotBuffer()) ||
                   this.LineNum != latestInfo.LineNum;
        }

        public bool NotNeedDeepCopyXPlotBuffer()
        {
            return (XDataInputType.Array == this.XDataInputType && this.Size <= Constants.MaxPointsInSingleSeries);
        }

        public bool NotNeedDeepCopyYPlotBuffer()
        {
            return (this.Size <= Constants.MaxPointsInSingleSeries);
        }

        public void Copy(DataEntityInfo src)
        {
            this.Size = src.Size;
            this.IsDeepCopy = src.IsDeepCopy;
            this.XDataInputType = src.XDataInputType;
            this.LineNum = src.LineNum;
//            this.XDataType = src.XDataType;
        }
    }
}

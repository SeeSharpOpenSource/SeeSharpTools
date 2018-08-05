namespace SeeSharpTools.JY.GUI.EasyChartXUtility
{
    /// <summary>
    /// 数据校验类，校验最终在DataEntity执行SaveData操作后处理
    /// </summary>
    internal class DataCheckParameters
    {
        public bool CheckNaN { get; set; }
        public bool CheckNegtiveOrZero { get; set; }
        public bool CheckInfinity { get; set; }

        public DataCheckParameters()
        {
            this.CheckNaN = false;
            this.CheckNegtiveOrZero = false;
            this.CheckInfinity = false;
        }

        public bool IsCheckDisabled()
        {
            return !CheckNaN && !CheckNegtiveOrZero && !CheckInfinity;
        }
    }
}
namespace SeeSharpTools.JY.GUI.StripChartUtility
{
    internal static class Constants
    {
        // 小数点位数或者小数点以上位数大于该值后使用数学计数法显示
        public const int MinDecimalOfScientificNotition = 6;
        // 通过移点实现绘图移动的最大样点数
        public const int MaxMovePointCount = 100;
        // 用来配置Y轴范围舍进比率的常数
        public const double RangeRoundOffset = 0.7;
        // Clear时坐标轴默认的LabelFormat
        public const string DefaultLabelFormat = "0.##";
        // Show value模式数值显示的字符串format
        public const string DataValueFormat = "X Val: {0}{2}Y Val: {1}";
        // 主X坐标轴的名称
        public const string PrimaryXAxisName = "X axis";
        // 副X轴坐标轴名称
        public const string PrimaryYAxisName = "Y (Value) axis";
        // Clear时Y轴主网格间隔
        public const double ClearYInterval = 0.5;
//        public const string DefaultTimeFormat = "hh:mm:ss:fff";
        // 非对数绘图时Y轴主网格的个数
        public const int YMajorGridCount = 6;
        // 非对数绘图时X轴主网格的个数
        public const int XLabelCount = 6;
        // 单条线最多显示的点数
        public const int MaxPointsInSingleSeries = 2000;
        // 单个视图中最多显示的段数
        public const int MaxSegmentInSinglePlot = 1000;
        // 默认每个DataEntity的线条容量
        public const int DefaultLineCapacity = 8;
        // 默认的Series的线条容量
        public const int DefaultDataSeriesCount = 8;
        // 没条线最少显示的点数
        public const int MinPoints = 3;
        // Array的X入参方式，相同X范围内默认的线条个数容量
        public const int DefaultSegmentCapacity = 4;
        // 缩放时默认的扩展常数(该常数确定X轴缩放范围以外额外的点数)
        public const double ScaleDataExpandRatio = 0.05;
        // 默认的最大绘图个数
        public const int MaxSeriesToDraw = 32;
        // Double类型的最小接受值范围
        public const double MinDoubleValue = 1E-40;
        // Y轴配置范围时根据最大值最小值向上向下的扩展系数
        public const double YAutoExpandRatio = 0.1;
        // 最小接受的游标缩放尺度
        public const double MinLegalInterval = 1E-40;
        // 导出CSV时每行默认的字符容量
        public const int DefaultCsvLineSize = 32;
    }
}
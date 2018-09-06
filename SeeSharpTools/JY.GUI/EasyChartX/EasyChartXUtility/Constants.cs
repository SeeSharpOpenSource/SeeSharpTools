namespace SeeSharpTools.JY.GUI.EasyChartXUtility
{
    internal static class Constants
    {
        
        // 小数点位数或者小数点以上位数大于该值后使用数学计数法显示
        public const int MinDecimalOfScientificNotition = 6;
        // 用来配置Y轴范围舍进比率的常数
        public const double RangeRoundOffset = 0.75;
        // Clear时坐标轴默认的LabelFormat
        public const string DefaultLabelFormat = "0.##";
        // Show value模式数值显示的字符串format
        public const string DataValueFormat = "X: {0}{2}Y: {1}";
        // 主X坐标轴的名称
        public const string PrimaryXAxisName = "X axis";
        // 副X轴坐标轴名称
        public const string PrimaryYAxisName = "Y (Value) axis";
        // Clear时X轴主网格间隔
        public const double ClearXInterval = 200;
        // Clear时Y轴主网格间隔
        public const double ClearYInterval = 0.5;
//        public const string DefaultTimeFormat = "hh:mm:ss:fff";
        // 非对数绘图时Y轴主网格的个数
        public const int MaxXGridCount = 10;
        // 非对数绘图时Y轴主网格的个数
        public const int YMajorGridCount = 6;
        // 单条线最多显示的点数
        public const int MaxPointsInSingleSeries = 4000;
        // 默认每个DataEntity的线条容量
        public const int DefaultLineCapacity = 8;
        // 默认的Series的线条容量
        public const int DefaultDataSeriesCount = 8;
        // Array的X入参方式，相同X范围内默认的线条个数容量
        public const int DefaultSegmentCapacity = 4;
        // 缩放时默认的扩展常数(该常数确定X轴缩放范围以外额外的点数)
        public const double ScaleDataExpandRatio = 0.05;
        // 默认的最大绘图个数
        public const int MaxSeriesToDraw = 32;
        // Double类型的最小接受值范围
        public const double MinDoubleValue = 1E-50;
        // Y轴配置范围时根据最大值最小值向上向下的扩展系数
        public const double YAutoExpandRatio = 0.05;
        // 最小接受的游标缩放尺度
        public const double MinLegalInterval = 1E-40;
        // 导出CSV时每行默认的字符容量
        public const int DefaultCsvLineSize = 500;
        // 不允许Nan数据时最小正数据的默认值
        public const double MinPositiveDoubleValue = 1E-200;
        // 不允许Nan数据时最小数据的默认值
        public const double MinNegtiveDoubleValue = -1E200;
        // 不允许Nan数据时最大数据的默认值
        public const double MaxPositiveDoubleValue = 1E200;
        // 不允许Nan数据时Nan的点数值
        public const double NanDataFakeValue = 1.5E-200;
        // 判定为Nan点的范围区间
        public const double FakeNanCheckRange = 2E-201;
        // 非法数据缓存的默认容量
        public const int InvalidDataBufCapacity = 1000;
        // Y轴的自动缩放间隔
        public const double AutoYIntervalPrecision = 300;
        // X轴不取消缩放时最大支持的范围差比例
        public const double MaxDiffToKeepXScaleview = 0.1;
        // Y轴不取消缩放时最大支持的范围差比例
        public const double MaxDiffToKeepYScaleview = 5;
        // 最小的float差
        public const float MinFloatDiff = (float) 1E-20;
        // 默认的Y轴最大最小值
        public const double DefaultYMax = 3.5;
        public const double DefaultYMin = 0.5;
        // 默认的X轴最大最小值
        public const double DefaultXMax = 1000;
        public const double DefaultXMin = 0;
        // 分区视图时X轴和Y轴的边界比例(百分比)
        public const float XBoundRatio = 5f;
        public const float YBoundRatio = 5f;
    }
}
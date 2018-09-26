using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.StripChartXData;
using SeeSharpTools.JY.GUI.StripChartXData.DataEntities;
using SeeSharpTools.JY.GUI.StripChartXEditor;

namespace SeeSharpTools.JY.GUI.StripChartXUtility
{
    internal class PlotManager
    {
        public readonly SeriesCollection PlotSeries;
        private readonly StripChartXSeriesCollection _series;

        internal DataCheckParameters DataCheckParams;

        private StripChartX.FitType _fitType;
        public StripChartX.FitType FitType
        {
            get
            {
                return _fitType;
            }
            set
            {
                if (null != DataEntity)
                {
                    DataEntity.FitType = value;
                }
                this._fitType = value;
            }
        }

        public int MaxSeriesCount { get; set; }

        public Type DataType { get; set; }

        internal DataEntityBase DataEntity { get; private set; }

        /// <summary>
        /// Maximum point count to show in single line
        /// 单条线最多显示的点数
        /// </summary>
        public int DisplayPoints { get; set; }

        /// <summary>
        /// Specify the x axis label type
        /// 配置X轴显示的类型
        /// </summary>
        public StripChartX.XAxisDataType XDataType { get; set; }

        /// <summary>
        /// Time stamp format
        /// 时间戳格式
        /// </summary>
        public string TimeStampFormat { get; set; }

        /// <summary>
        /// Get or set the next time stamp value
        /// 获取或配置下一个绘图时的其实时间戳
        /// </summary>
        public DateTime NextTimeStamp { get; set; }

        /// <summary>
        /// Get or set the time interval between two samples
        /// 获取或配置相邻两个样点之间的时间间隔
        /// </summary>
        public TimeSpan TimeInterval { get; set; }

        /// <summary>
        /// Start value of X axis index
        /// X轴索引起始值
        /// </summary>
        public int StartIndex { get; set; }

        private int _plotSeriesCount;

        public StripChartXSeriesCollection Series => _series;

        public StripChartXLineSeries LineSeries { get; }

        public int SamplesInChart { get; private set; }

        public int SeriesCount
        {
            get { return _plotSeriesCount;}
            set
            {
                if (_plotSeriesCount == value)
                {
                    return;
                }
                _plotSeriesCount = value;
                _parentChart?.AdaptPlotSeriesAndChartView();
            }
        }
        

        public bool IsPlotting { get; private set; }
        private readonly StripChartX _parentChart;

        internal PlotManager(StripChartX parentChart, SeriesCollection plotSeries)
        {

            // LineSeries只是一个用于维护对外接口的属性
            this._fitType = StripChartX.FitType.Range;

            this.IsPlotting = false;
            this._parentChart = parentChart;
            this._plotSeriesCount = 1;

            this.PlotSeries = plotSeries;
            this._plotSeriesCount = PlotSeries.Count;

            this.DataEntity = null;

            this._series = new StripChartXSeriesCollection(plotSeries, parentChart);
            LineSeries = new StripChartXLineSeries(_series);

            this.DataCheckParams = new DataCheckParameters();

            this.SamplesInChart = 0;

            this.MaxSeriesCount = Constants.DefaultMaxSeriesCount;
        }

        public void InitializeDataEntity<TDataType>(int lineCount, int sampleCount)
        {
            DataEntityInfo dataEntityInfo = new DataEntityInfo()
            {
                Capacity = this.DisplayPoints,
                DataType = typeof (TDataType),
                LineCount = lineCount,
            };
            if (null == DataEntity || !DataEntity.DataInfo.IsEqual(dataEntityInfo))
            {
                switch (XDataType)
                {
                    case StripChartX.XAxisDataType.Index:
                        DataEntity = new IndexDataEntity<TDataType>(this, dataEntityInfo);
                        break;
                    case StripChartX.XAxisDataType.String:
                        DataEntity = new StringDataEntity<TDataType>(this, dataEntityInfo);
                        break;
                    case StripChartX.XAxisDataType.TimeStamp:
                        DataEntity = new TimeStampDataEntity<TDataType>(this, dataEntityInfo, sampleCount);
                        break;
                }
            }
            else
            {
                DataEntity.Clear();
                DataEntity.Initialize(sampleCount);
            }
        }

        // TODO 后期优化，记录当前Series的Plot范围，然后再判断是否需要重新Plot
        // 在begin和end区间内绘制单条曲线。如果forceRefresh为true时(调用Plot方法时)始终更新绘图。
        // 如果forceRefresh为false时(用户进行缩放等操作时)如果判断数据区间和原来的兼容(稀疏比相同且是上次绘图范围的子集则无需更新)
        // 使用forceRefresh是为了避免在数据量过大，用户缩放后拖动滚动条会比较卡顿的问题
        internal void PlotDataInRange(double beginXValue, double endXValue, int seriesIndex, bool forceRefresh = false)
        {
            int lineIndex;
            DataEntityBase dataEntity = GetDataEntityBySeriesIndex(seriesIndex, out lineIndex);
            if (null == dataEntity)
            {
                return;
            }

            bool isNeedRefreshPlot = dataEntity.FillPlotDataInRange(beginXValue, endXValue, forceRefresh, lineIndex);
            if (isNeedRefreshPlot)
            {
                IList<double> xDataBuf = dataEntity.PlotBuf.GetXData();
                IList<IList<double>> yDataBuf = dataEntity.PlotBuf.GetYData();
                if (PlotSeries[seriesIndex].Points.Count == dataEntity.PlotBuf.PlotSize)
                {
                    for (int i = 0; i < dataEntity.PlotBuf.PlotSize; i++)
                    {
                        if (PlotSeries[seriesIndex].Points[i].IsEmpty && !double.IsNaN(yDataBuf[lineIndex][i]))
                        {
                            PlotSeries[seriesIndex].Points[i].IsEmpty = false;
                        }
                        PlotSeries[seriesIndex].Points[i].SetValueXY(xDataBuf[i], yDataBuf[lineIndex][i]);
                    }
                }
                else
                {
                    PlotSeries[seriesIndex].Points.DataBindXY(xDataBuf, yDataBuf[lineIndex]);
                }
                // 如果有校验Nan数据，则将标记为Nan数据的点不显示
                if (DataCheckParams.CheckNaN)
                {
                    HideNanDataPoint(seriesIndex);
                }
                //if (dataEntity.DataInfo.XDataType != XDataType.Number)
                //{
                //    RefreshTimeToXLabel(seriesIndex);
                //}
            }
        }

        // 在begin和end区间内绘制所有曲线。如果forceRefresh为true时(调用Plot方法时)始终更新绘图。
        // 如果forceRefresh为false时(用户进行缩放等操作时)如果判断数据区间和原来的兼容(稀疏比相同且是上次绘图范围的子集则无需更新)
        // 使用forceRefresh是为了避免在数据量过大，用户缩放后拖动滚动条会比较卡顿的问题
        internal void PlotDataInRange(double beginXValue, double endXValue, bool forceRefresh = false)
        {
            int seriesIndex = 0;
            foreach (DataEntity dataEntity in PlotDatas)
            {
                // 根据begin和end的范围，将数据填充到PlotBuffer中。如果无需更新绘图则返回false。
                bool isNeedRefreshPlot = dataEntity.FillPlotDataInRange(beginXValue, endXValue, forceRefresh, -1);
                
                if (isNeedRefreshPlot)
                {
                    IList<double> xDataBuf = dataEntity.PlotBuf.GetXData();
                    IList<IList<double>> yDataBuf = dataEntity.PlotBuf.GetYData();
                    for (int lineIndex = 0; lineIndex < dataEntity.DataInfo.LineNum; lineIndex++)
                    {
                        //                        PlotSeries[seriesIndex++].Points.DataBindXY(dataEntity.Buffer.XPlotBuffer,
                        //                            dataEntity.Buffer.YPlotBuffer[j]);
                        if (PlotSeries[seriesIndex].Points.Count == dataEntity.PlotBuf.PlotSize)
                        {
                            for (int i = 0; i < dataEntity.PlotBuf.PlotSize; i++)
                            {
                                if (PlotSeries[seriesIndex].Points[i].IsEmpty && !double.IsNaN(yDataBuf[lineIndex][i]))
                                {
                                    PlotSeries[seriesIndex].Points[i].IsEmpty = false;
                                }
                                PlotSeries[seriesIndex].Points[i].SetValueXY(xDataBuf[i], yDataBuf[lineIndex][i]);
                            }
                        }
                        else
                        {
                            PlotSeries[seriesIndex].Points.DataBindXY(xDataBuf, yDataBuf[lineIndex]);
                        }
                        // 如果有校验Nan数据，则将标记为Nan数据的点不显示
                        if (DataCheckParams.CheckNaN)
                        {
                            HideNanDataPoint(seriesIndex);
                        }
                        seriesIndex++;
                    }
                }
                else
                {
                    seriesIndex += dataEntity.DataInfo.LineNum;
                }
            }
        }

        private void HideNanDataPoint(int seriesIndex)
        {
            for (int i = 0; i < PlotSeries[seriesIndex].Points.Count; i++)
            {
                DataPoint dataPoint = PlotSeries[seriesIndex].Points[i];
                if (Math.Abs(dataPoint.XValue - Constants.NanDataFakeValue) <= Constants.FakeNanCheckRange ||
                    Math.Abs(dataPoint.YValues[0] - Constants.NanDataFakeValue) <= Constants.FakeNanCheckRange)
                {
                    dataPoint.IsEmpty = true;
                }
            }
        }

        /// <summary>
        /// 根据数据需要的线数匹配Chart上的线条个数
        /// </summary>
        internal void AdaptSeriesCount()
        {
            if (PlotSeries.Count == _plotSeriesCount)
            {
                return;
            }
            _series.AdaptSeriesCount(_plotSeriesCount);
            if (_plotSeriesCount > PlotSeries.Count)
            {
                for (int i = PlotSeries.Count; i < _plotSeriesCount; i++)
                {
                    Series newSeries = new Series();
                    ((StripChartXSeries)_series[i]).AdaptBaseSeries(newSeries);
                    PlotSeries.Add(newSeries);
                }
                // 如果当前没有绘图则使用默认数据填充
                if (!IsPlotting)
                {
                    PlotDefaultView();
                }
            }
            else
            {
                while (PlotSeries.Count > _plotSeriesCount)
                {
                    int indexToRemove = PlotSeries.Count - 1;
                    PlotSeries.RemoveAt(indexToRemove);
                }
            }
        }

//        internal void AdaptPlotSeriesAndDataEntity()
//        {
//            //            if (_isPlot)
//            //            {
//            //                AdaptSeriesCount();
//            //                AdaptPlotDatasCount(0);
//            //            }
//            AdaptSeriesCount();
//            AdaptPlotDatasCount(0);
//        }
        
        // 清空绘图
        internal void Clear()
        {
            IsPlotting = false;
            PlotDefaultView();
            SamplesInChart = 0;
            DataType = null;
            foreach (DataEntity dataEntity in PlotDatas)
            {
                dataEntity.Clear();
            }
        }

        private void PlotDefaultView()
        {
            const int defaultViewSize = 2;
            const int startOffset = 500;
            const int yValue = 100;
            double[] xData = new double[defaultViewSize];
            double[] yData = new double[defaultViewSize];
            for (int i = 0; i < defaultViewSize; i++)
            {
                xData[i] = startOffset + i;
                yData[i] = yValue;
            }
            foreach (Series singleSeries in PlotSeries)
            {
                singleSeries.Points.DataBindXY(xData, yData);
                foreach (DataPoint point in singleSeries.Points)
                {
                    point.IsEmpty = true;
                }
            }
        }

        public int GetEndIndex()
        {

        }

        public int GetStartIndex()
        {
        }

        public double GetMinXInterval()
        {
            return 1;
        }

        public int GetMaxDataSize()
        {
            int maxData = int.MinValue;
            foreach (DataEntity dataEntity in PlotDatas)
            {
                if (dataEntity.DataInfo.Size > maxData)
                {
                    maxData = dataEntity.DataInfo.Size;
                }
            }
            return maxData;
        }

        public void GetMaxAndMinYValue(StripChartXPlotArea plotArea, out double maxYValue, out double minYValue, 
            int seriesIndex)
        {
            maxYValue = double.NaN;
            minYValue = double.NaN;
            double tmpMaxYValue, tmpMinYValue;
            if (seriesIndex <= -1)
            {
                AxisType yAxisType = PlotSeries[0].YAxisType;
                // 所有的类型都相同则计算全局最大值
                if (PlotSeries.All(item => item.YAxisType == yAxisType))
                {
                    PlotDatas[0].GetMaxAndMinYValue(out maxYValue, out minYValue, -1);
                    for (int i = 1; i < PlotDatas.Count; i++)
                    {
                        PlotDatas[i].GetMaxAndMinYValue(out tmpMaxYValue, out tmpMinYValue, -1);
                        if (tmpMaxYValue > maxYValue)
                        {
                            maxYValue = tmpMaxYValue;
                        }
                        if (tmpMinYValue < minYValue)
                        {
                            minYValue = tmpMinYValue;
                        }
                    }
                }
                else
                {
                    maxYValue = double.MinValue;
                    minYValue = double.MaxValue;
                    int lineIndex;
                    for (int index = 0; index < PlotSeries.Count; index++)
                    {
                        if (PlotSeries[index].YAxisType != AxisType.Primary)
                        {
                            continue;
                        }
                        DataEntity dataEntity = GetDataEntityBySeriesIndex(index, out lineIndex);
                        dataEntity.GetMaxAndMinYValue(out tmpMaxYValue, out tmpMinYValue, lineIndex);
                        if (tmpMaxYValue > maxYValue)
                        {
                            maxYValue = tmpMaxYValue;
                        }
                        if (tmpMinYValue < minYValue)
                        {
                            minYValue = tmpMinYValue;
                        }
                    }
                    if (Math.Abs(double.MaxValue - maxYValue) < Constants.MinDoubleValue)
                    {
                        maxYValue = double.NaN;
                        minYValue = double.NaN;
                    }
                }
            }
            else
            {
                // 无论是副坐标轴还是主坐标轴，都给主坐标轴赋最大值
                int lineIndex;
                DataEntity dataEntity = GetDataEntityBySeriesIndex(seriesIndex, out lineIndex);
                dataEntity?.GetMaxAndMinYValue(out maxYValue, out minYValue, lineIndex);
            }
        }

        public void GetMaxAndMinY2Value(StripChartXPlotArea plotArea, out double maxYValue, out double minYValue, 
            int seriesIndex)
        {
            maxYValue = double.NaN;
            minYValue = double.NaN;
            // 副坐标轴没有数据则返回
            if (!HasSeriesInYAxis(plotArea, StripChartXAxis.PlotAxis.Secondary))
            {
                return;
            }
            double tmpMaxYValue, tmpMinYValue;
            if (seriesIndex <= -1)
            {
                maxYValue = double.MinValue;
                minYValue = double.MaxValue;
                int lineIndex;
                for (int index = 0; index < PlotSeries.Count; index++)
                {
                    if (PlotSeries[index].YAxisType != AxisType.Secondary)
                    {
                        continue;
                    }
                    DataEntity dataEntity = GetDataEntityBySeriesIndex(index, out lineIndex);
                    dataEntity.GetMaxAndMinYValue(out tmpMaxYValue, out tmpMinYValue, lineIndex);
                    if (tmpMaxYValue > maxYValue)
                    {
                        maxYValue = tmpMaxYValue;
                    }
                    if (tmpMinYValue < minYValue)
                    {
                        minYValue = tmpMinYValue;
                    }
                }
                if (Math.Abs(double.MaxValue - maxYValue) < Constants.MinDoubleValue)
                {
                    maxYValue = double.NaN;
                    minYValue = double.NaN;
                }
            }
            else
            {
                int lineIndex;
                DataEntity dataEntity = GetDataEntityBySeriesIndex(seriesIndex, out lineIndex);
                dataEntity.GetMaxAndMinYValue(out maxYValue, out minYValue, lineIndex);
            }
        }

        public bool HasSeriesInXAxis(StripChartXAxis.PlotAxis plotAxis, StripChartXPlotArea plotArea)
        {
            return PlotSeries.Any(item => item.ChartArea.Equals(plotArea.Name) && item.XAxisType == (AxisType) plotAxis);
        }

        public bool HasSeriesInYAxis(StripChartXPlotArea plotArea, StripChartXAxis.PlotAxis plotAxis)
        {
            return PlotSeries.Any(item => item.ChartArea.Equals(plotArea.Name) && item.YAxisType == (AxisType)plotAxis);
        }

        // 获取副Y坐标轴的最大最小值

        public DataEntity GetDataEntityBySeriesIndex(int seriesIndex, out int lineIndex)
        {
            int lineCount = 0;
            foreach (DataEntity dataEntity in PlotDatas)
            {
                if (lineCount <= seriesIndex && lineCount + dataEntity.DataInfo.LineNum > seriesIndex)
                {
                    lineIndex = seriesIndex - lineCount;
                    return dataEntity;
                }
                lineCount += dataEntity.DataInfo.LineNum;
            }
            lineIndex = 0;
            return null;
        }

        /// <summary>
        /// Save chart data to csv file
        /// </summary>
        /// <param name="filePath">Csv file path</param>
        public void SaveAsCsv(string filePath)
        {
            if (!IsPlotting || PlotDatas.Count <= 0)
            {
                return;
            }
            FileStream stream = null;
            StreamWriter writer = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(stream, Encoding.UTF8);
                if (1 == PlotDatas.Count)
                {
                    SaveOneDimXDataToCsv(writer);
                }
                else
                {
                    SaveMultiDimXDataToCsv(writer);
                }
            }
            finally
            {
                ReleaseResource(writer);
                ReleaseResource(stream);
            }
        }

        private static void ReleaseResource(IDisposable resource)
        {
            try
            {
                resource?.Dispose();
            }
            catch (Exception)
            {
                // igore
            }
        }

        private const string XAxisCsvLabel = "T";
        private const char CsvDelim = ',';
        private void SaveOneDimXDataToCsv(StreamWriter writer)
        {
            StringBuilder lineData = new StringBuilder(Constants.DefaultCsvLineSize);
            lineData.Append(XAxisCsvLabel).Append(CsvDelim);
            //写出列名称
            foreach (Series plotSeries in PlotSeries)
            {
                lineData.Append(plotSeries.Name).Append(CsvDelim);
            }

            if (lineData.Length > 0)
            {
                lineData.Remove(lineData.Length - 1, 1);
            }
            writer.WriteLine(lineData);
            lineData.Clear();
            //写出各行数据
            DataEntity saveData = PlotDatas[0];
            for (int i = 0; i < saveData.DataInfo.Size; i++)
            {
                lineData.Append(saveData.GetXData(i)).Append(CsvDelim);
                for (int j = 0; j < saveData.DataInfo.LineNum; j++)
                {
                    lineData.Append(saveData.GetYData(j, i)).Append(CsvDelim);
                }
                lineData.Remove(lineData.Length - 1, 1);
                writer.WriteLine(lineData);
                lineData.Clear();
            }
        }

        private void SaveMultiDimXDataToCsv(StreamWriter writer)
        {
            StringBuilder lineData = new StringBuilder(Constants.DefaultCsvLineSize);

            //写出列名称
            foreach (Series plotSeries in PlotSeries)
            {
                lineData.Append(XAxisCsvLabel).Append(CsvDelim);
                lineData.Append(plotSeries.Name).Append(CsvDelim);
            }

            if (lineData.Length > 0)
            {
                lineData.Remove(lineData.Length - 1, 1);
            }
            writer.WriteLine(lineData);
            lineData.Clear();
            //写出各行数据
            int maxDataSize = GetMaxDataSize();
            for (int i = 0; i < maxDataSize; i++)
            {
                foreach (DataEntity dataEntity in PlotDatas)
                {
                    int lineIndex = 0;
                    for (int j = 0; j < dataEntity.DataInfo.LineNum; j++)
                    {
                        if (dataEntity.DataInfo.Size > i)
                        {
                            lineData.Append(dataEntity.GetXData(i)).Append(CsvDelim);
                            lineData.Append(dataEntity.GetYData(lineIndex, i)).Append(CsvDelim);
                        }
                        else
                        {
                            lineData.Append(CsvDelim).Append(CsvDelim);
                        }
                        lineIndex++;
                    }
                }
                lineData.Remove(lineData.Length - 1, 1);
                writer.WriteLine(lineData);
                lineData.Clear();
            }
        }
        
    }
}
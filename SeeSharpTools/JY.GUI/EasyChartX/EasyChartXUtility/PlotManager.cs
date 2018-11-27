using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JY.GUI.EasyChartXData;
using SeeSharpTools.JY.GUI.EasyChartXEditor;

namespace SeeSharpTools.JY.GUI.EasyChartXUtility
{
    internal class PlotManager
    {
        public readonly SeriesCollection PlotSeries;
        private readonly EasyChartXSeriesCollection _series;

        internal DataCheckParameters DataCheckParams;

        private EasyChartX.FitType _fitType;
        public EasyChartX.FitType FitType
        {
            get
            {
                return _fitType;
            }
            set
            {
                foreach (DataEntity dataEntity in PlotDatas)
                {
                    dataEntity.PlotBuf.FitType = value;
                }
                this._fitType = value;
            }
        }

        public bool CumulativePlot { get; set; }

        public int MaxSeriesCount { get; set; }

        private int _plotSeriesCount;

        public EasyChartXSeriesCollection Series => _series;

        public EasyChartXLineSeries LineSeries { get; }

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
        private readonly EasyChartX _parentChart;

        public readonly List<DataEntity> PlotDatas;

        internal PlotManager(EasyChartX parentChart, SeriesCollection plotSeries)
        {

            // LineSeries只是一个用于维护对外接口的属性
            this._fitType = EasyChartX.FitType.Range;

            this.IsPlotting = false;
            this._parentChart = parentChart;
            this.CumulativePlot = false;
            this._plotSeriesCount = 1;

            this.PlotSeries = plotSeries;
            this._plotSeriesCount = PlotSeries.Count;

            this._series = new EasyChartXSeriesCollection(plotSeries, parentChart);
            LineSeries = new EasyChartXLineSeries(_series);

            this.PlotDatas = new List<DataEntity>(Constants.MaxPointsInSingleSeries);
            this.DataCheckParams = new DataCheckParameters();

            this.MaxSeriesCount = Constants.DefaultMaxSeriesCount;
        }

        /// <summary>
        /// 添加绘图数据到缓存中
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="xstep"></param>
        /// <param name="yData"></param>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        public void AddPlotData(double xStart, double xstep, IList<double> yData, int xSize, int ySize)
        {
            // 如果是连续绘图且已经达到线数上限则直接返回
            if (CumulativePlot && _plotSeriesCount >= MaxSeriesCount)
            {
                return;
            }
            int lineNum = ySize / xSize;
            _plotSeriesCount = ((!CumulativePlot) || (!IsPlotting)) ? lineNum : _plotSeriesCount + lineNum;
            if (_plotSeriesCount > MaxSeriesCount)
            {
                ySize -= (_plotSeriesCount - MaxSeriesCount) *xSize;
                _plotSeriesCount = MaxSeriesCount;
            }
            AdaptPlotDatasCount(1);
            IsPlotting = true;
            AdaptSeriesCount();
            PlotDatas[PlotDatas.Count - 1].SaveData(xStart, xstep, yData, xSize, ySize);
        }

        /// <summary>
        /// 添加绘图数据到缓存中
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="xstep"></param>
        /// <param name="yData"></param>
        /// <param name="rowDirection"></param>
        public void AddPlotData(double xStart, double xstep, double[,] yData, bool rowDirection)
        {
            // 如果是连续绘图且已经达到线数上限则直接返回
            if (CumulativePlot && _plotSeriesCount >= MaxSeriesCount)
            {
                return;
            }
            int lineNum = rowDirection ? yData.GetLength(0) : yData.GetLength(1);
            _plotSeriesCount = ((!CumulativePlot) || (!IsPlotting)) ? lineNum : _plotSeriesCount + lineNum;
            if (_plotSeriesCount > MaxSeriesCount)
            {
                lineNum -= _plotSeriesCount - MaxSeriesCount;
                _plotSeriesCount = MaxSeriesCount;
            }
            AdaptPlotDatasCount(1);
            IsPlotting = true;
            AdaptSeriesCount();
            PlotDatas[PlotDatas.Count - 1].SaveData(xStart, xstep, yData, lineNum, rowDirection);
        }

        /*
                public void AddPlotData(DateTime startTime, double sampleRate, IEnumerable<double> yData, int xSize, int ySize)
                {
                    int lineNum = ySize / xSize;
                    int seriesCountAfterPlot = !CumulativePlot ? lineNum : _plotSeriesCount + lineNum;
                    if (_plotSeriesCount > Constants.MaxSeriesToDraw)
                    {
                        return;
                    }
                    _plotSeriesCount = seriesCountAfterPlot;
                    IsPlotting = true;
                    AdaptPlotDatasCount(1);
                    AdaptSeriesCount();
                    PlotDatas[PlotDatas.Count - 1].SaveData(startTime, sampleRate, _easyChart.AxisX.LabelFormat, yData, xSize, ySize);
                }
        */

        /// <summary>
        /// 添加绘图数据到缓存中
        /// </summary>
        /// <param name="xData"></param>
        /// <param name="yData"></param>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        public void AddPlotData(IList<double> xData, IList<double> yData, int xSize, int ySize)
        {
            // 如果是连续绘图且已经达到线数上限则直接返回
            if (CumulativePlot && _plotSeriesCount >= MaxSeriesCount)
            {
                return;
            }
            int lineNum = ySize / xSize;
            _plotSeriesCount = ((!CumulativePlot) || (!IsPlotting)) ? lineNum : _plotSeriesCount + lineNum;
            if (_plotSeriesCount > MaxSeriesCount)
            {
                ySize -= (_plotSeriesCount - MaxSeriesCount) *xSize;
                _plotSeriesCount = MaxSeriesCount;
            }
            AdaptPlotDatasCount(1);
            IsPlotting = true;
            AdaptSeriesCount();
            PlotDatas[PlotDatas.Count - 1].SaveData(xData, yData, xSize, ySize);
        }

        /// <summary>
        /// 添加绘图数据到缓存中
        /// </summary>
        /// <param name="xData"></param>
        /// <param name="yData"></param>
        public void AddPlotData(IList<IList<double>> xData, IList<IList<double>> yData)
        {
            // 如果是连续绘图且已经达到线数上限则直接返回
            if (CumulativePlot && _plotSeriesCount >= MaxSeriesCount)
            {
                return;
            }
            int lineNum = xData.Count;
            _plotSeriesCount = ((!CumulativePlot) || (!IsPlotting)) ? lineNum : _plotSeriesCount + lineNum;
            if (_plotSeriesCount > MaxSeriesCount)
            {
                lineNum -= _plotSeriesCount - MaxSeriesCount;
                _plotSeriesCount = MaxSeriesCount;
            }
            AdaptPlotDatasCount(lineNum);
            IsPlotting = true;
            AdaptSeriesCount();
            int plotDataIndex = PlotDatas.Count - lineNum;
            for (int i = 0; i < lineNum; i++)
            {
                PlotDatas[plotDataIndex++].SaveData(xData[i], yData[i], xData[i].Count, yData[i].Count);
            }
        }

        // TODO 后期优化，记录当前Series的Plot范围，然后再判断是否需要重新Plot
        // 在begin和end区间内绘制单条曲线，分区视图使用。如果forceRefresh为true时(调用Plot方法时)始终更新绘图。
        // 如果forceRefresh为false时(用户进行缩放等操作时)如果判断数据区间和原来的兼容(稀疏比相同且是上次绘图范围的子集则无需更新)
        // 使用forceRefresh是为了避免在数据量过大，用户缩放后拖动滚动条会比较卡顿的问题
        internal void PlotDataInRange(double beginXValue, double endXValue, int seriesIndex, bool forceRefresh = false)
        {
            int lineIndex;
            DataEntity dataEntity = GetDataEntityBySeriesIndex(seriesIndex, out lineIndex);
            if (null == dataEntity)
            {
                return;
            }

            bool isNeedRefreshPlot = dataEntity.FillPlotDataInRange(beginXValue, endXValue, forceRefresh, lineIndex);
            if (isNeedRefreshPlot)
            {
                PlotBuffer plotBuffer = dataEntity.PlotBuf;
                if (PlotSeries[seriesIndex].Points.Count == plotBuffer.PlotSize)
                {
                    for (int i = 0; i < plotBuffer.PlotSize; i++)
                    {
                        double yValue = plotBuffer.YPlotBuffer[lineIndex][i];
                        PlotSeries[seriesIndex].Points[i].SetValueXY(plotBuffer.XPlotBuffer[i], yValue);
                        if (PlotSeries[seriesIndex].Points[i].IsEmpty && !double.IsNaN(yValue))
                        {
                            PlotSeries[seriesIndex].Points[i].IsEmpty = false;
                        }
                    }
                }
                else
                {
                    IList<double> xDataBuf = plotBuffer.GetXPlotDataCollection();
                    IList<double> yDataBuf = plotBuffer.GetYPlotDataCollection(lineIndex);
                    PlotSeries[seriesIndex].Points.DataBindXY(xDataBuf, yDataBuf);
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

        // 在begin和end区间内绘制所有曲线，在主视图使用。如果forceRefresh为true时(调用Plot方法时)始终更新绘图。
        // 如果forceRefresh为false时(用户进行缩放等操作时)如果判断数据区间和原来的兼容(稀疏比相同且是上次绘图范围的子集则无需更新)
        // 使用forceRefresh是为了避免在数据量过大，用户缩放后拖动滚动条会比较卡顿的问题
        internal void PlotDataInRange(double beginXValue, double endXValue, bool forceRefresh = false)
        {
            int seriesIndex = 0;
            foreach (DataEntity dataEntity in PlotDatas)
            {
                // 根据begin和end的范围，将数据填充到PlotBuffer中。如果无需更新绘图则返回false。
                bool isNeedRefreshPlot = dataEntity.FillPlotDataInRange(beginXValue, endXValue, forceRefresh, -1);
                PlotBuffer plotBuffer = dataEntity.PlotBuf;
                if (isNeedRefreshPlot)
                {
                    bool pointCountChanged = false;
                    for (int i = seriesIndex; i < seriesIndex + dataEntity.DataInfo.LineNum; i++)
                    {
                        if (PlotSeries[i].Points.Count != plotBuffer.PlotSize)
                        {
                            pointCountChanged = true;
                            break;
                        }
                    }
                    //如果点的个数和上次相同，则直接使用PlotBuffer的数据直接更新点的数据
                    if (!pointCountChanged)
                    {
                        for (int lineIndex = 0; lineIndex < dataEntity.DataInfo.LineNum; lineIndex++)
                        {
                            for (int i = 0; i < plotBuffer.PlotSize; i++)
                            {
                                double yValue = plotBuffer.YPlotBuffer[lineIndex][i];
                                PlotSeries[seriesIndex].Points[i].SetValueXY(plotBuffer.XPlotBuffer[i], yValue);
                                if (PlotSeries[seriesIndex].Points[i].IsEmpty && !double.IsNaN(yValue))
                                {
                                    PlotSeries[seriesIndex].Points[i].IsEmpty = false;
                                }
                            }
                            // 如果有校验Nan数据，则将标记为Nan数据的点不显示
                            if (DataCheckParams.CheckNaN)
                            {
                                HideNanDataPoint(seriesIndex);
                            }
                            seriesIndex++;
                        }
                    }
                    // 如果点的个数和上次不同，则使用数据绑定，则获取当前点的数据集合，直接绑定点的数据到Chart
                    else
                    {
                        IList<double> xDataBuf = plotBuffer.GetXPlotDataCollection();
                        IList<IList<double>> yDataBuf = plotBuffer.GetYPlotDataCollections();
                        for (int lineIndex = 0; lineIndex < dataEntity.DataInfo.LineNum; lineIndex++)
                        {
                            PlotSeries[seriesIndex].Points.DataBindXY(xDataBuf, yDataBuf[lineIndex]);
                            // 如果有校验Nan数据，则将标记为Nan数据的点不显示
                            if (DataCheckParams.CheckNaN)
                            {
                                HideNanDataPoint(seriesIndex);
                            }
                            seriesIndex++;
                        }
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
/*
        private void RefreshTimeToXLabel(int seriesIndex)
        {
            if (seriesIndex < 0)
            {
                for (int index = 0; index < PlotSeries.Count; index++)
                {
                    RefreshTimeToXLabel(index);
                }
            }
            else
            {
                int lineIndex;
                DataEntity dataEntity = GetDataEntityBySeriesIndex(seriesIndex, out lineIndex);
                IList<string> xTimeBuffer = dataEntity.Buffer.XTimeBuffer;
                if (null == xTimeBuffer)
                {
                    return;
                }
                for (int i = 0; i < xTimeBuffer.Count; i++)
                {
                    PlotSeries[seriesIndex].Points[i].AxisLabel = xTimeBuffer[i];
                }
            }
        }
*/
        
        internal void AdaptPlotDatasCount(int plotDataCount)
        {
            // 如果是累积绘图则绘图个数是当前DataEntity个数与待添加DataEntity个数之和
            if (CumulativePlot && IsPlotting)
            {
                plotDataCount += PlotDatas.Count;
            }

            if (PlotDatas.Count == plotDataCount)
            {
                return;
            }

            while (PlotDatas.Count < plotDataCount)
            {
                DataEntity dataEntity = new DataEntity(this.DataCheckParams);
                dataEntity.PlotBuf.FitType = _fitType;
                PlotDatas.Add(dataEntity);
            }

            while (PlotDatas.Count > plotDataCount)
            {
                PlotDatas.RemoveAt(PlotDatas.Count - 1);
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
                    ((EasyChartXSeries)_series[i]).AdaptBaseSeries(newSeries);
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

        public double GetMinXData()
        {
            double minData = double.MaxValue;
            foreach (DataEntity dataEntity in PlotDatas)
            {
                if (dataEntity.MinXValue < minData)
                {
                    minData = dataEntity.MinXValue;
                }
            }
            return minData;
        }

        public double GetMaxXData()
        {
            double maxData = double.MinValue;
            DataEntity maxEntity = null;
            foreach (DataEntity dataEntity in PlotDatas)
            {
                if (dataEntity.MaxXValue > maxData)
                {
                    maxData = dataEntity.MaxXValue;
                    maxEntity = dataEntity;
                }
            }
            // TODO fix later.为了避免0-999这种情况做的特殊处理
            if (null != maxEntity && XDataInputType.Increment == maxEntity.DataInfo.XDataInputType)
            {
                maxData += maxEntity.XIncrement;
            }
            return maxData;
        }

        public double GetMinXInterval()
        {
            double minInterval = double.MaxValue;
            foreach (DataEntity dataEntity in PlotDatas)
            {
                if (dataEntity.XMinInterval < minInterval)
                {
                    minInterval = dataEntity.XMinInterval;
                }
            }
            return minInterval;
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

        public void GetMaxAndMinYValue(EasyChartXPlotArea plotArea, out double maxYValue, out double minYValue, 
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

        public void GetMaxAndMinY2Value(EasyChartXPlotArea plotArea, out double maxYValue, out double minYValue, 
            int seriesIndex)
        {
            maxYValue = double.NaN;
            minYValue = double.NaN;
            // 副坐标轴没有数据则返回
            if (!HasSeriesInYAxis(plotArea, EasyChartXAxis.PlotAxis.Secondary))
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

        public bool HasSeriesInXAxis(EasyChartXAxis.PlotAxis plotAxis, EasyChartXPlotArea plotArea)
        {
            return PlotSeries.Any(item => item.ChartArea.Equals(plotArea.Name) && item.XAxisType == (AxisType) plotAxis);
        }

        public bool HasSeriesInYAxis(EasyChartXPlotArea plotArea, EasyChartXAxis.PlotAxis plotAxis)
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
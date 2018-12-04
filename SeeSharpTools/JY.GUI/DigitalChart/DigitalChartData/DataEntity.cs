using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeeSharpTools.JY.GUI.DigitalChartUtility;

namespace SeeSharpTools.JY.GUI.DigitalChartData
{
    internal class DataEntity
    {
        public double MaxXValue { get; protected set; }
        public double MinXValue { get; protected set; }

        public IList<double> XData { get; private set; }
        public IList<double> YData { get; private set; }

        //为了将二维数组拷贝到一维数组，进而进行转换的buffer。用来提升二维数组保存到list的效率
        private double[] _transBuf;
//        public List<string> YStrData { get; private set; } 

        public PlotBuffer PlotBuf { get; }

        public double XStart { get; private set; }
        public double XIncrement { get; private set; }
        public double XMinInterval { get; private set; }

//        public DateTime XStartTime { get; private set; }
//        public double SampleRate { get; private set; }
//        public string TimeFormat { get; private set; }

        // 每条线当前绘图中的最大值最小值
        private readonly List<double> _viewStart = new List<double>(Constants.DefaultDataSeriesCount); 
        private readonly List<double> _viewEnd = new List<double>(Constants.DefaultDataSeriesCount); 
        private readonly List<int> _sparseRatio = new List<int>(Constants.DefaultDataSeriesCount);
        // 数据校验参数类
        private readonly DataCheckParameters _dataCheckParams;
        //保存非法数据真实值的缓存
        private Dictionary<int, double> _invalidXData;
        private Dictionary<int, double> _invalidYData;

        // 并行加速工具类
        internal readonly ParallelHandler Parallel;

        public readonly DataEntityInfo DataInfo;

        public DataEntity(DataCheckParameters dataCheckParams)
        {
            DataInfo = new DataEntityInfo();
            PlotBuf = new PlotBuffer(this);
            _transBuf = null;
            _dataCheckParams = dataCheckParams;
            Parallel = new ParallelHandler(this, dataCheckParams);
        }

        public void SaveData(IList<double> xData, IList<double> yData, int xSize, int ySize)
        {
            DataInfo.XDataInputType = XDataInputType.Array;
//            DataInfo.XDataType = XDataType.Number;
            DataInfo.Size = xSize <= ySize ? xSize : ySize;
            DataInfo.LineNum = ySize / DataInfo.Size;

            XData = SaveDataToBuf(XData, xData);
            YData = SaveDataToBuf(YData, yData);
            
            CheckInvalidData();

            _transBuf = null;

            XMinInterval = GetArrayXMinInternval();

            double maxX, minX;
            Parallel.GetMaxAndMin(XData, out maxX, out minX);
            MaxXValue = maxX;
            MinXValue = minX;

            PlotBuf.AdaptPlotBuffer();

            InitViewRange();
        }

        public void SaveData(double xStart, double xIncrement, IList<double> yData, int xSize, int ySize)
        {
            DataInfo.XDataInputType = XDataInputType.Increment;
//            DataInfo.XDataType = XDataType.Number;
            XStart = xStart;
            XIncrement = xIncrement;
            DataInfo.Size = xSize <= ySize ? xSize : ySize;

            MaxXValue = xStart + (DataInfo.Size - 1) * xIncrement;
            MinXValue = xStart;

            XMinInterval = xIncrement > Constants.MinLegalInterval ? xIncrement : Constants.MinLegalInterval;
            
            DataInfo.LineNum = ySize / DataInfo.Size;

            XData?.Clear();
            YData = SaveDataToBuf(YData, yData);

            CheckInvalidData();

            _transBuf = null;

            PlotBuf.AdaptPlotBuffer();

            InitViewRange();
        }

        public void SaveData(double xStart, double xIncrement, double[,] yData, int lineCount, bool rowDirection)
        {
            DataInfo.XDataInputType = XDataInputType.Increment;
            //            DataInfo.XDataType = XDataType.Number;
            XStart = xStart;
            XIncrement = xIncrement;
            DataInfo.Size = rowDirection ? yData.GetLength(1) : yData.GetLength(0);

            MaxXValue = xStart + (DataInfo.Size - 1) * xIncrement;
            MinXValue = xStart;

            XMinInterval = xIncrement > Constants.MinLegalInterval ? xIncrement : Constants.MinLegalInterval;

            //            InitYDataBuf(yData, yDataSize);
            DataInfo.LineNum = lineCount;

            XData?.Clear();
            if (null == _transBuf || _transBuf.Length != yData.Length)
            {
                _transBuf = new double[yData.Length];
            }
            if (rowDirection || 1 == lineCount)
            {
                Buffer.BlockCopy(yData, 0, _transBuf, 0, lineCount * DataInfo.Size * sizeof(double));
            }
            else
            {
                Parallel.Transpose(yData, _transBuf);
            }
            YData = SaveDataToBuf(YData, _transBuf);

            CheckInvalidData();

            //            YStrData = null;

            PlotBuf.AdaptPlotBuffer();

            InitViewRange();
        }

        /*
                public void SaveData(DateTime xStart, double sampleRate, string timeFormat, IEnumerable<double> yData, 
                    int xSize, int ySize)
                {
                    DataInfo.XDataInputType = XDataInputType.Increment;
                    DataInfo.XDataType = XDataType.DateTime;
                    XStart = 0;
                    XIncrement = 1;
                    DataInfo.Size = xSize <= ySize ? xSize : ySize;

                    MaxXValue = xSize;
                    MinXValue = 0;
                    XMinInterval = 1;

                    XStartTime = xStart;
                    SampleRate = sampleRate;
                    if (string.IsNullOrWhiteSpace(timeFormat))
                    {
                        timeFormat = Constants.DefaultTimeFormat;
                    }
                    TimeFormat = timeFormat;

                    //            InitYDataBuf(yData, yDataSize);
                    DataInfo.LineNum = ySize / DataInfo.Size;

                    XData?.Clear();
                    YData = SaveDataToBuf(YData, yData);

                    Buffer.AdaptPlotBuffer(this);

                    InitViewRange();
                }
        */
        public static void FillMultiDataEntity(IList<IList<double>> xData, IList<IList<double>> yData, 
            List<DataEntity> dataEntities, DataCheckParameters dataCheckParams, bool isShallowCopy = false)
        {
            int seriesCount = xData.Count;
            if (dataEntities.Count != seriesCount)
            {
                while (dataEntities.Count < seriesCount)
                {
                    dataEntities.Add(new DataEntity(dataCheckParams));
                }
                while (dataEntities.Count > seriesCount)
                {
                    dataEntities.RemoveAt(dataEntities.Count - 1);
                }
            }

            for (int i = 0; i < seriesCount; i++)
            {
//                dataEntities[i].SaveData(xData[i], yData[i], isShallowCopy);
                dataEntities[i].SaveData(xData[i], yData[i], xData.Count, yData.Count);
            }
        }

        private void CheckInvalidData()
        {
            if (_dataCheckParams.IsCheckDisabled())
            {
                _invalidXData = null;
                _invalidYData = null;
                return;
            }

            if (null == _invalidXData)
            {
                _invalidXData = (XDataInputType.Array == DataInfo.XDataInputType) ? 
                    new Dictionary<int, double>(Constants.InvalidDataBufCapacity) : null;
            }
            else
            {
                _invalidXData?.Clear();
            }

            if (null == _invalidYData)
            {
                _invalidYData = new Dictionary<int, double>(Constants.InvalidDataBufCapacity);
            }
            else
            {
                _invalidYData.Clear();
            }

            switch (DataInfo.XDataInputType)
            {
                case XDataInputType.Increment:
                    Parallel.InvalidDataCheck(YData, _invalidYData);
                    break;
                case XDataInputType.Array:
                    Parallel.InvalidDataCheck(XData, _invalidXData);
                    Parallel.InvalidDataCheck(YData, _invalidYData);
                    break;
                default:
                    break;
            }
        }

        // TODO not perfect, just for time efficiency
        private double GetArrayXMinInternval()
        {
            const double minDoubleValue = 1.0E-40;
            double minDiff = double.MaxValue;
            for (int i = 1; i < DataInfo.Size; i++)
            {
                double diff = Math.Abs(XData[i] - XData[i - 1]);
                if (diff > Constants.MinLegalInterval && minDiff - diff >= minDoubleValue)
                {
                    minDiff = diff;
                }
            }
            return minDiff;
        }

        public IList<double> GetXData()
        {
            IList<double> tmpXData = null;
            switch (DataInfo.XDataInputType)
            {
                case XDataInputType.Array:
                    tmpXData = XData;
                    break;
                case XDataInputType.Increment:
                    tmpXData = new List<double>(DataInfo.Size);
                    for (int i = 0; i < DataInfo.Size; i++)
                    {
                        tmpXData.Add(XStart + XIncrement * i);
                    }
                    break;
            }
            return tmpXData;
        }

        public double GetXData(int index)
        {
            switch (DataInfo.XDataInputType)
            {
                case XDataInputType.Increment:
                    return XStart + index*XIncrement;
                case XDataInputType.Array:
                    if (_dataCheckParams.IsCheckDisabled())
                    {
                        return XData[index];
                    }
                    return (false == _invalidXData.ContainsKey(index)) ? XData[index] : _invalidXData[index];
                default:
                    return 0;
            }
        }

        public double GetYData(int lineIndex, int pointIndex)
        {
            int realIndex = lineIndex * DataInfo.Size + pointIndex;
            if (_dataCheckParams.IsCheckDisabled())
            {
                return YData[realIndex];
            }
            return (false == _invalidYData.ContainsKey(realIndex)) ? YData[realIndex] :
                _invalidYData[realIndex];
        }

        public bool FillPlotDataInRange(double xStart, double xEnd, bool forceRefresh, int seriesIndex)
        {
            if (double.IsNaN(xStart))
            {
                xStart = MinXValue;
            }
            if (double.IsNaN(xEnd))
            {
                xEnd = MaxXValue;
            }

            if (seriesIndex < 0)
            {
                PlotBuf.PlotXStart = _viewStart.Max();
                PlotBuf.PlotXEnd = _viewEnd.Min();
                PlotBuf.SparseRatio = _sparseRatio.Max();
            }
            else
            {
                PlotBuf.PlotXStart = _viewStart[seriesIndex];
                PlotBuf.PlotXEnd = _viewEnd[seriesIndex];
                PlotBuf.SparseRatio = _sparseRatio[seriesIndex];
            }
            bool isNeedRefreshPlot = FillPlotDataInRange(xStart, xEnd, forceRefresh);
            if (isNeedRefreshPlot)
            {
                if (seriesIndex < 0)
                {
                    for (int i = 0; i < DataInfo.LineNum; i++)
                    {
                        _viewStart[i] = PlotBuf.PlotXStart;
                        _viewEnd[i] = PlotBuf.PlotXEnd;
                        _sparseRatio[i] = PlotBuf.SparseRatio;
                    }
                }
                else
                {
                    _viewStart[seriesIndex] = PlotBuf.PlotXStart;
                    _viewEnd[seriesIndex] = PlotBuf.PlotXEnd;
                    _sparseRatio[seriesIndex] = PlotBuf.SparseRatio;
                }
            }
            return isNeedRefreshPlot;
        }

        private bool FillPlotDataInRange(double xStart, double xEnd, bool forceRefresh)
        {
//            double expandRange = (MaxXValue - MinXValue) * Constants.ScaleDataExpandRatio;
            double expandRange = (xEnd - xStart) * Constants.ScaleDataExpandRatio;
            PlotBuf.CurrentXStart = xStart - expandRange;
            PlotBuf.CurrentXEnd = xEnd + expandRange;

            // 如果超出数据边界则清空绘图区
            if (PlotBuf.CurrentXStart >= MaxXValue || PlotBuf.CurrentXEnd <= MinXValue)
            {
                PlotBuf.FillEmptyDataToBuffer(DataInfo.LineNum);
                return forceRefresh;
            }
            if (PlotBuf.CurrentXEnd > MaxXValue)
            {
                PlotBuf.CurrentXEnd = MaxXValue;
            }
            if (PlotBuf.CurrentXStart < MinXValue)
            {
                PlotBuf.CurrentXStart = MinXValue;
            }

            bool isNeedRefreshPlot = false;

            if (PlotBuf.CurrentXStart - MinXValue < Constants.MinDoubleValue && 
                MaxXValue - PlotBuf.CurrentXEnd < Constants.MinDoubleValue)
            {
                isNeedRefreshPlot = PlotBuf.FillPlotDataToBuffer(forceRefresh);
            }
            else
            {
                switch (DataInfo.XDataInputType)
                {
                    case XDataInputType.Increment:
                        isNeedRefreshPlot = FillIncrementPlotDataInRange(forceRefresh);
                        break;
                    case XDataInputType.Array:
                        isNeedRefreshPlot = FillArrayPlotDataInRange(forceRefresh);
                        break;
                }
            }
            return isNeedRefreshPlot;
        }

        private bool FillIncrementPlotDataInRange(bool forceRefresh)
        {
            int startIndex = (int)((PlotBuf.CurrentXStart - XStart) / XIncrement);
            int endIndex = (int) ((PlotBuf.CurrentXEnd - XStart)/XIncrement);
            if (startIndex >= DataInfo.Size || endIndex < 0)
            {
                if (forceRefresh)
                {
                    PlotBuf.FillEmptyDataToBuffer(this.DataInfo.LineNum);
                }
                return forceRefresh;
            }
            return PlotBuf.FillPlotDataToBuffer(startIndex, endIndex - startIndex + 1, forceRefresh);
        }

        private bool FillArrayPlotDataInRange(bool forceRefresh)
        {
            // 如果不在数据最大值最小值范围内则暂时为两个Buffer写入空数据
            if (MaxXValue < PlotBuf.CurrentXStart || MinXValue > PlotBuf.CurrentXEnd)
            {
                if (forceRefresh)
                {
                    PlotBuf.FillEmptyDataToBuffer(this.DataInfo.LineNum);
                }
                return forceRefresh;
            }

            List<int> startIndexes = new List<int>(Constants.DefaultSegmentCapacity);
            List<int> counts = new List<int>(Constants.DefaultSegmentCapacity);

            bool isLastInRange = false;

            for (int i = 0; i < DataInfo.Size; i++)
            {
                bool isInRange = !(XData[i] < PlotBuf.CurrentXStart || XData[i] > PlotBuf.CurrentXEnd);
                // 上次不在范围，当前在范围则将StartIndex放入
                if (!isLastInRange && isInRange)
                {
                    startIndexes.Add(i);
                    isLastInRange = true;
                }
                // 上次在范围内，该次不在范围则将Count放入
                else if (isLastInRange && !isInRange)
                {
                    counts.Add(i - startIndexes[startIndexes.Count - 1]);
                    isLastInRange = false;
                }
            }
            // 如果结束时仍然在范围中则计算最后一个count
            if (isLastInRange)
            {
                counts.Add(DataInfo.Size - startIndexes[startIndexes.Count - 1]);
            }
            return PlotBuf.FillPlotDataToBuffer(startIndexes, counts, forceRefresh);
        }

        //        public List<double> GetYData(int lineIndex)
        //        {
        //            return YData.GetRange(lineIndex * Size, Size);
        //        }
        
        public bool IsEqual(DataEntity dataEntity)
        {
            if (dataEntity.DataInfo.XDataInputType != DataInfo.XDataInputType)
            {
                return false;
            }

            switch (DataInfo.XDataInputType)
            {
                case XDataInputType.Increment:
                    return XStart.Equals(dataEntity.XStart) && XIncrement.Equals(dataEntity.XIncrement) &&
                        DataInfo.Size == dataEntity.DataInfo.Size;
                case XDataInputType.Array:
                    return ReferenceEquals(XData, dataEntity.XData);
                default:
                    return false;
            }
        }

        public int FindeNearestIndex(ref double xValue, ref double yValue, int seriesIndex)
        {
            if (double.IsNaN(xValue))
            {
                return -1;
            }
            int nearIndexes;
            switch (DataInfo.XDataInputType)
            {
                case XDataInputType.Increment:
                    nearIndexes = FindIncrementNearIndex(ref xValue, ref yValue, seriesIndex);
                    break;
                case XDataInputType.Array:
                    nearIndexes = double.IsNaN(yValue) ? 
                        FindArrayNearIndexByXValue(ref xValue, out yValue, seriesIndex) : 
                        FindArrayNearIndex(ref xValue, ref yValue, seriesIndex);
                    break;
                default:
                    nearIndexes = 0;
                    break;
            }
            return nearIndexes;
        }

        private int FindIncrementNearIndex(ref double xValue, ref double yValue, int seriesIndex)
        {
            int nearIndex = (int) Math.Round((xValue - XStart)/XIncrement);
            if (nearIndex < 0)
            {
                nearIndex = 0;
            }
            else if (nearIndex > DataInfo.Size - 1)
            {
                nearIndex = DataInfo.Size - 1;
            }
            xValue = GetXData(nearIndex);
            yValue = GetYData(seriesIndex, nearIndex);
            return nearIndex;
        }

        private int FindArrayNearIndexByXValue(ref double xValue, out double yValue, int seriesIndex)
        {
            yValue = double.NaN;
            int nearestIndex = -1;
            double minDValue = double.MaxValue;
            for (int i = 0; i < DataInfo.Size; i++)
            {
                double diff = Math.Abs(XData[i] - xValue);
                if (diff < minDValue)
                {
                    nearestIndex = i;
                    minDValue = diff;
                }
            }
            if (nearestIndex >= 0)
            {
                xValue = GetXData(nearestIndex);
                yValue = GetYData(seriesIndex, nearestIndex);
            }
            return nearestIndex;
        }

        private int FindArrayNearIndex(ref double xValue, ref double yValue, int seriesIndex)
        {
            double minDiff = XMinInterval*4;
            List<int> nearestIndexes = new List<int>(5);
            double minDValue = double.MaxValue;
            for (int i = 0; i < DataInfo.Size; i++)
            {
                double diff = Math.Abs(XData[i] - xValue);
                if (diff - minDValue > minDiff)
                {
                    continue;
                }
                // 如果最小的差值小于当前差值的一半，则原来的临近点全部清除
                if (diff > 2*minDValue)
                {
                    nearestIndexes.Clear();
                }
                minDValue = minDValue < diff ? minDValue : diff;
                nearestIndexes.Add(i);
            }

            int nearestIndex = -1;
            minDValue = double.MaxValue;
            foreach (int index in nearestIndexes)
            {
                double yData = GetYData(seriesIndex, index);
                if (double.IsNaN(yData))
                {
                    continue;
                }
                double diff = Math.Pow(yData - yValue, 2) + Math.Pow(XData[index] - xValue, 2);
                if (diff < minDValue)
                {
                    nearestIndex = index;
                    minDValue = diff;
                }
            }
            if (-1 != nearestIndex)
            {
                xValue = GetXData(nearestIndex);
                yValue = GetYData(seriesIndex, nearestIndex);
            }
            return nearestIndex;
        }

        private static IList<T> SaveDataToBuf<T>(IList<T> buffer, IList<T> data)
        {
            if (null == buffer)
            {
                buffer = new List<T>(data);
            }
            else
            {
                List<T> listBuf = buffer as List<T>;
                listBuf.Clear();
                AdaptBufferCapacity(data.Count, listBuf);
                listBuf.AddRange(data);
            }
            return buffer;
        }
        
        public void GetMaxAndMinYValue(out double maxYValue, out double minYValue, int lineIndex)
        {
            if (lineIndex <= -1)
            {
                Parallel.GetMaxAndMin(YData, out maxYValue, out minYValue);
//                maxYValue = YData.Max();
//                minYValue = YData.Min();
            }
            else
            {
                List<double> yDataList = YData as List<double>;
                List<double> singleLineData = yDataList.GetRange(lineIndex*DataInfo.Size, DataInfo.Size);
                Parallel.GetMaxAndMin(singleLineData, out maxYValue, out minYValue);
            }
        }

        private static void AdaptBufferCapacity<TType>(int size, List<TType> buffer)
        {
            if (buffer.Capacity < size)
            {
                buffer.Capacity = size;
            }
            else if (size < buffer.Capacity/5)
            {
                buffer.Capacity = size;
            }
        }

        private void InitViewRange()
        {
            _viewStart.Clear();
            _viewEnd.Clear();
            _sparseRatio.Clear();
            for (int i = 0; i < DataInfo.LineNum; i++)
            {
                _viewStart.Add(double.NaN);
                _viewEnd.Add(double.NaN);
                _sparseRatio.Add(int.MaxValue);
            }
        }

        public void Clear()
        {
            _transBuf = null;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeeSharpTools.JY.GUI.DigitalChartUtility;

namespace SeeSharpTools.JY.GUI.DigitalChartData
{
    internal class PlotBuffer
    {
        private List<ushort> _xPlotBuffer;
        internal IList<ushort> XPlotBuffer { get; set; }

        internal DigitalChart.FitType FitType { get; set; }

        private readonly List<IList<ushort>> _yPlotBuffer;
        private readonly List<IList<ushort>> _yShallowBuffer;
        internal IList<IList<ushort>> YPlotBuffer { get; set; }

        // 绘图缓存中X轴数据的稀疏比和最大最小值
        public int SparseRatio { get; set; }
        public double PlotXStart { get; set; }
        public double PlotXEnd { get; set; }

        // 最新一次绘图中X轴数据的最大最小值
        public double CurrentXStart { get; set; }
        public double CurrentXEnd { get; set; }

        private int _plotSize;

        public int PlotSize => _plotSize;

        private readonly DataEntityInfo _dataInfo;
        private readonly DataEntity _dataEntity;

        public PlotBuffer(DataEntity dataEntity)
        {
            this._dataEntity = dataEntity;
            _dataInfo = new DataEntityInfo();
            _plotSize = 0;
            SparseRatio = int.MaxValue;
            _xPlotBuffer = null;
            _yPlotBuffer = new List<IList<ushort>>(Constants.DefaultLineCapacity);
            _yShallowBuffer = new List<IList<ushort>>(Constants.DefaultLineCapacity);
            this.PlotXStart = double.NaN;
            this.PlotXEnd = double.NaN;
        }

        #region 适配缓存

        public void AdaptPlotBuffer()
        {
            // TODO 为了保证稳定性，暂时强制配置
            //            if (null == _xPlotBuffer || _dataInfo.IsNeedAdaptXBuffer(dataEntity.DataInfo))
            if (null == _xPlotBuffer)
            {
                AdaptXPlotBuffer();
            }
            if (0 == _yPlotBuffer.Count || _dataInfo.IsNeedAdaptYBuffer(_dataEntity.DataInfo))
            {
                AdaptYPlotBuffer();
            }
            _dataInfo.Copy(_dataEntity.DataInfo);
            this._plotSize = 0;
            this.SparseRatio = 0;
            this.PlotXStart = double.NaN;
            this.PlotXEnd = double.NaN;
        }

        private void AdaptXPlotBuffer()
        {
            if (null == _xPlotBuffer)
            {
                _xPlotBuffer = new List<ushort>(Constants.MaxPointsInSingleSeries);
                FillDefaultToListBuffer(_xPlotBuffer, 0, Constants.MaxPointsInSingleSeries);
            }
        }

        private void AdaptYPlotBuffer()
        {
            while (_yPlotBuffer.Count > _dataEntity.DataInfo.LineNum)
            {
                _yPlotBuffer.RemoveAt(_yPlotBuffer.Count - 1);
            }
            for (int i = 0; i < _dataEntity.DataInfo.LineNum; i++)
            {
                if (i >= _yPlotBuffer.Count)
                {
                    List<ushort> newBuf = new List<ushort>(Constants.MaxPointsInSingleSeries);
                    FillDefaultToListBuffer(newBuf, 0, Constants.MaxPointsInSingleSeries);
                    _yPlotBuffer.Add(newBuf);
                }
            }
            _yShallowBuffer.Clear();
        }

        #endregion

        #region Step方式传入X的绘图

        public bool FillPlotDataToBuffer(int startIndex, int count, bool forceRefresh)
        {
            bool isNeedRefreshPlot = false;
            if (count <= Constants.MaxPointsInSingleSeries)
            {
                XPlotBuffer = _xPlotBuffer;
                YPlotBuffer = _yShallowBuffer;
                isNeedRefreshPlot = FillPlotDataWithoutSparse(startIndex, count, forceRefresh);
            }
            else
            {
                XPlotBuffer = _xPlotBuffer;
                YPlotBuffer = _yPlotBuffer;
                isNeedRefreshPlot = FillPlotDataWithSparse(startIndex, count, forceRefresh);
            }
            return isNeedRefreshPlot;
        }

        private bool FillPlotDataWithoutSparse(int startIndex, int count, bool forceRefresh)
        {
            bool isNeedRefreshPlot = IsNeedRefreshPlot(1, forceRefresh);
            if (isNeedRefreshPlot)
            {
                _plotSize = count;
                SparseRatio = 1;

                List<ushort> yData = _dataEntity.YData as List<ushort>;
                if (XDataInputType.Increment == _dataEntity.DataInfo.XDataInputType)
                {
                    double xValue = startIndex*_dataEntity.XIncrement + _dataEntity.XStart;
                    for (int i = 0; i < _plotSize; i++)
                    {
                        XPlotBuffer[i] = xValue;
                        xValue += _dataEntity.XIncrement;
                    }
                }
                else
                {
                    List<ushort> xData = _dataEntity.XData as List<ushort>;
                    XPlotBuffer = xData.GetRange(startIndex, count);
                }

                YPlotBuffer.Clear();
                int yStartIndex = startIndex;
                for (int i = 0; i < _dataEntity.DataInfo.LineNum; i++)
                {
                    YPlotBuffer.Add(yData.GetRange(yStartIndex, count));
                    yStartIndex += _dataEntity.DataInfo.Size;
                }
            }
            return isNeedRefreshPlot;
        }

        private bool FillPlotDataWithSparse(int startIndex, int count, bool forceRefresh)
        {
            int newSparseRatio = GetSparseRatio(count, out _plotSize);
            bool isNeedRefreshPlot = IsNeedRefreshPlot(newSparseRatio, forceRefresh);
            if (isNeedRefreshPlot)
            {
                SparseRatio = newSparseRatio;
                switch (FitType)
                {
                    case DigitalChart.FitType.None:
                        _dataEntity.Parallel.FillNoneFitPlotData(startIndex);
                        break;
                    case DigitalChart.FitType.Range:
                        // 使用半拟合时PlotSize必须为2的整数倍
                        _plotSize = (_plotSize / 2) * 2;
                        _dataEntity.Parallel.FillRangeFitPlotData(startIndex);
                        break;
                    default:
                        break;
                }
            }
            return isNeedRefreshPlot;
        }
        #endregion

        #region Array方式传入X数据的绘图

        public bool FillPlotDataToBuffer(List<int> startIndexes, List<int> counts, bool forceRefresh)
        {
            // 如果只有一个数据段需要绘图，则和Step输入的绘图方式相同
            if (1 == startIndexes.Count)
            {
                return FillPlotDataToBuffer(startIndexes[0], counts[0], forceRefresh);
            }

            bool isNeedRefreshPlot = false;
            // 每段的后面都有一个空点
            int totalCount = counts.Sum() + counts.Count;
            if (totalCount <= Constants.MaxPointsInSingleSeries)
            {
                XPlotBuffer = _xPlotBuffer;
                YPlotBuffer = _yPlotBuffer;
                isNeedRefreshPlot = FillPlotDataWithoutSparse(startIndexes, counts, forceRefresh);
            }
            else
            {
                XPlotBuffer = _xPlotBuffer;
                YPlotBuffer = _yPlotBuffer;
                isNeedRefreshPlot = FillPlotDataWithSparse(startIndexes, counts, forceRefresh);
            }
            return isNeedRefreshPlot;
        }

        private bool FillPlotDataWithoutSparse(List<int> startIndexes, List<int> counts, bool forceRefresh)
        {
            bool isNeedRefreshPlot = IsNeedRefreshPlot(1, forceRefresh);
            if (isNeedRefreshPlot)
            {
                SparseRatio = 1;
                int segmentCount = startIndexes.Count;
                _plotSize = 0;
                if (XDataInputType.Increment == _dataInfo.XDataInputType)
                {
                    int pointIndex = 0;
                    for (int segmentIndex = 0; segmentIndex < segmentCount; segmentIndex++)
                    {
                        int plotCount = counts[segmentIndex];
                        double xDataValue = _dataEntity.XStart + _dataEntity.XIncrement*startIndexes[segmentIndex];
                        for (int index = 0; index < plotCount; index++)
                        {
                            int pointBufIndex = startIndexes[segmentIndex] + index;
                            XPlotBuffer[pointIndex] = xDataValue;
                            xDataValue += _dataEntity.XIncrement;
                            for (int lineIndex = 0; lineIndex < _dataInfo.LineNum; lineIndex++)
                            {
                                YPlotBuffer[lineIndex][pointIndex] = _dataEntity.YData[pointBufIndex];
                                pointBufIndex += _dataInfo.Size;
                            }
                            pointIndex++;
                        }
                        // 每段之间要互相断开
                        XPlotBuffer[pointIndex] = 0;
                        foreach (IList<double> plotBuffer in YPlotBuffer)
                        {
                            plotBuffer[pointIndex] = double.NaN;
                        }
                        _plotSize += plotCount + 1;
                        pointIndex++;
                    }
                }
                else
                {
                    int pointIndex = 0;
                    for (int segmentIndex = 0; segmentIndex < segmentCount; segmentIndex++)
                    {
                        int plotCount = counts[segmentIndex];
                        for (int index = 0; index < plotCount; index++)
                        {
                            int pointBufIndex = startIndexes[segmentIndex] + index;
                            XPlotBuffer[pointIndex] = _dataEntity.XData[pointBufIndex];
                            for (int lineIndex = 0; lineIndex < _dataInfo.LineNum; lineIndex++)
                            {
                                // TODO to simplified
                                YPlotBuffer[lineIndex][pointIndex] = _dataEntity.YData[pointBufIndex];
                                pointBufIndex += _dataInfo.Size;
                            }
                            pointIndex++;
                        }

                        // 每段之间要互相断开
                        XPlotBuffer[pointIndex] = 0;
                        foreach (IList<ushort> plotBuffer in YPlotBuffer)
                        {
                            plotBuffer[pointIndex] = Double.NaN;
                        }
                        _plotSize += plotCount + 1;
                        pointIndex++;
                    }
                }
            }
            return isNeedRefreshPlot;
        }

        // Array类型入参的暂时不执行插值处理
        private bool FillPlotDataWithSparse(List<int> startIndexes, List<int> counts, bool forceRefresh)
        {
            int totalCounts = counts.Sum();
            int segmentCount = startIndexes.Count;
            int plotSize;
            int newSparseRatio = GetSparseRatio(totalCounts+segmentCount, out plotSize);
            bool isNeedRefreshPlot = IsNeedRefreshPlot(newSparseRatio, forceRefresh);
            int pointIndex = 0;
            if (isNeedRefreshPlot)
            {
                SparseRatio = newSparseRatio;
                _plotSize = 0;
                // 只可能是X轴时Array方式会进入该方法
                for (int segmentIndex = 0; segmentIndex < segmentCount; segmentIndex++)
                {
                    int plotCount = (counts[segmentIndex] + SparseRatio - 1) / SparseRatio;
                    for (int index = 0; index < plotCount; index++)
                    {
                        int pointBufIndex = startIndexes[segmentIndex] + index * SparseRatio;
                        XPlotBuffer[pointIndex] = _dataEntity.XData[pointBufIndex];
                        for (int lineIndex = 0; lineIndex < _dataInfo.LineNum; lineIndex++)
                        {
                            // TODO to simplified
                            YPlotBuffer[lineIndex][pointIndex] = _dataEntity.YData[pointBufIndex];
                            pointBufIndex += _dataInfo.Size;
                        }
                        pointIndex++;
                        // TODO Not Right, Fix Later
                        if (pointIndex >= XPlotBuffer.Count)
                        {
                            break;
                        }
                    }
                    _plotSize += plotCount;
//                    // 每段之间要互相断开
//                    XPlotBuffer[pointIndex] = 0;
//                    foreach (IList<double> plotBuffer in YPlotBuffer)
//                    {
//                        plotBuffer[pointIndex] = Double.NaN;
//                    }
//                    pointIndex++;
//                    _plotSize += plotCount + 1;
                }

                // TODO Not Right, Fix Later
                if (_plotSize > XPlotBuffer.Count)
                {
                    _plotSize = XPlotBuffer.Count;
                }
            }
            return isNeedRefreshPlot;

        }

        #endregion

        #region 所有点的绘图

        public bool FillPlotDataToBuffer(bool forceRefresh)
        {
            bool isNeedRefreshPlot = false;
            if (_dataEntity.DataInfo.Size <= Constants.MaxPointsInSingleSeries)
            {
                XPlotBuffer = _xPlotBuffer;
                YPlotBuffer = _yShallowBuffer;
                isNeedRefreshPlot = FillPlotDataWithoutSparse(forceRefresh);
            }
            else
            {
                XPlotBuffer = _xPlotBuffer;
                YPlotBuffer = _yPlotBuffer;
                isNeedRefreshPlot = FillPlotDataWithSparse(forceRefresh);
            }
            return isNeedRefreshPlot;
        }

        private bool FillPlotDataWithoutSparse(bool forceRefresh)
        {
            bool isNeedRefreshPlot = IsNeedRefreshPlot(1, forceRefresh);
            if (isNeedRefreshPlot)
            {
                SparseRatio = 1;
                _plotSize = _dataEntity.DataInfo.Size;

                if (XDataInputType.Increment == _dataEntity.DataInfo.XDataInputType)
                {
                    double xValue = _dataEntity.XStart;
                    for (int i = 0; i < _plotSize; i++)
                    {
                        XPlotBuffer[i] = xValue;
                        xValue += _dataEntity.XIncrement;
                    }
                }
                else
                {
                    XPlotBuffer = _dataEntity.XData as List<ushort>;
                }

                YPlotBuffer.Clear();
                int yStartIndex = 0;
                List<ushort> yData = _dataEntity.YData as List<ushort>;
                for (int i = 0; i < _dataEntity.DataInfo.LineNum; i++)
                {
                    YPlotBuffer.Add(yData.GetRange(yStartIndex, _dataEntity.DataInfo.Size));
                    yStartIndex += _dataEntity.DataInfo.Size;
                }
            }
            return isNeedRefreshPlot;
        }

        private bool FillPlotDataWithSparse(bool forceRefresh)
        {
            int newSparseRatio = GetSparseRatio(_dataEntity.DataInfo.Size, out _plotSize);
            bool isNeedRefreshPlot = IsNeedRefreshPlot(newSparseRatio, forceRefresh);
            if (isNeedRefreshPlot)
            {
                SparseRatio = newSparseRatio;

                switch (FitType)
                {
                    case DigitalChart.FitType.None:
                        _dataEntity.Parallel.FillNoneFitPlotData(0);
                        break;
                    case DigitalChart.FitType.Range:
                        // Array方式入参时不实用拟合算法
                        if (XDataInputType.Array == _dataEntity.DataInfo.XDataInputType)
                        {
                            _dataEntity.Parallel.FillNoneFitPlotData(0);
                        }
                        else
                        {
                            // 使用半拟合时PlotSize必须为2的整数倍
                            _plotSize = (_plotSize / 2) * 2;
                            _dataEntity.Parallel.FillRangeFitPlotData(0);
                        }
                        break;
                    default:
                        break;
                }
            }
            return isNeedRefreshPlot;
        }

        #endregion

        #region Utility

        public IList<double> GetXPlotDataCollection()
        {
//            if (ReferenceEquals(XPlotBuffer, _xPlotBuffer))
//            {
//                return _xPlotBuffer.GetRange(0, _plotSize);
//            }
//            else
//            {
//                return XPlotBuffer;
//            }
            return (XPlotBuffer as List<ushort>).GetRange(0, _plotSize);
        }

        public IList<IList<double>> GetYPlotDataCollections()
        {
            if (ReferenceEquals(YPlotBuffer, _yPlotBuffer))
            {
                _yShallowBuffer.Clear();
                foreach (List<ushort> yPlotData in _yPlotBuffer)
                {
                    _yShallowBuffer.Add(yPlotData.GetRange(0, _plotSize));
                }
            }
            return _yShallowBuffer;
        }

        public IList<double> GetYPlotDataCollection(int lineIndex)
        {
            if (ReferenceEquals(YPlotBuffer, _yPlotBuffer))
            {
                List<ushort> yPlotData = _yPlotBuffer[lineIndex] as List<ushort>;
                return yPlotData.GetRange(0, _plotSize);
            }
            else
            {
                return _yShallowBuffer[lineIndex];
            }
        }

        private static int GetSparseRatio(int count, out int plotCount)
        {
//            int sparseRatio = (int) Math.Ceiling((double) count/ Constants.MaxPointsInSingleSeries);
            int sparseRatio = 1;
            while ((double) count > (Constants.MaxPointsInSingleSeries*sparseRatio))
            {
                sparseRatio *= 2;
            }

            plotCount = (count + sparseRatio - 1)/sparseRatio;
            return sparseRatio;
        }

        //如果范围内全是空数据则将buffer赋值为空值
        private readonly List<ushort> _emptyData = new List<ushort>(1);
        public void FillEmptyDataToBuffer(int lineNum)
        {
            _plotSize = 0;
            PlotXStart = double.NaN;
            PlotXEnd = double.NaN;
            _yShallowBuffer.Clear();
            for (int i = 0; i < lineNum; i++)
            {
                _yShallowBuffer.Add(_emptyData);
            }
        }

        // 是否需要重新绘图，如果需要更新绘图则更新PlotXStart和PlotXEnd
        private bool IsNeedRefreshPlot(int newSparseRatio, bool forceRefresh)
        {
            bool isWithinLastRange = !double.IsNaN(PlotXStart) && !double.IsNaN(PlotXEnd) &&
                                     CurrentXStart > PlotXStart && CurrentXEnd < PlotXEnd;
            bool sparseRatioNotChanged = SparseRatio > 0 && SparseRatio <= newSparseRatio;

            bool isNeedRefreshPlot = !(isWithinLastRange && sparseRatioNotChanged);
            if (isNeedRefreshPlot)
            {
                PlotXStart = CurrentXStart;
                PlotXEnd = CurrentXEnd;
            }
            return isNeedRefreshPlot || forceRefresh;
        }

        private static void FillDefaultToListBuffer<T>(List<T> buffer, T value, int count)
        {
            int datasToAdd = count - buffer.Count;
            for (int i = 0; i < datasToAdd; i++)
            {
                buffer.Add(value);
            }
        }

        #endregion
    }
}
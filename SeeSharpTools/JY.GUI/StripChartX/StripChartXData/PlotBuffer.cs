using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI.StripChartXData
{
    internal class PlotBuffer<TDataType>
    {
        private List<int> _xPlotBuffer;
        internal List<int> XPlotBuffer => _xPlotBuffer;

        internal StripChartX.FitType FitType { get; set; }

        private readonly List<List<TDataType>> _yPlotBuffer;
        internal List<List<TDataType>> YPlotBuffer => _yPlotBuffer;
        internal List<List<TDataType>> YShallowBuffer { get; }

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

        public PlotBuffer(int lineCount)
        {
            _dataInfo = new DataEntityInfo();
            _plotSize = 0;
            SparseRatio = int.MaxValue;
            this.PlotXStart = double.NaN;
            this.PlotXEnd = double.NaN;

            _xPlotBuffer = new List<int>(Constants.MaxPointsInSingleSeries);
            FillDefaultToListBuffer(_xPlotBuffer, 0, Constants.MaxPointsInSingleSeries);
            _yPlotBuffer = new List<List<TDataType>>(Constants.DefaultLineCapacity);
            YShallowBuffer = new List<List<TDataType>>(Constants.DefaultLineCapacity);
            for (int i = 0; i < lineCount; i++)
            {
                if (i >= _yPlotBuffer.Count)
                {
                    List<TDataType> newBuf = new List<TDataType>(Constants.MaxPointsInSingleSeries);
                    FillDefaultToListBuffer(newBuf, 0, Constants.MaxPointsInSingleSeries);
                    _yPlotBuffer.Add(newBuf);
                }
            }
        }

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

                List<double> yData = _dataEntity.YData as List<double>;
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
                    List<double> xData = _dataEntity.XData as List<double>;
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
                    case StripChartX.FitType.None:
                        _dataEntity.Parallel.FillNoneFitPlotData(startIndex);
                        break;
                    case StripChartX.FitType.Range:
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
                    XPlotBuffer = _dataEntity.XData as List<double>;
                }

                YPlotBuffer.Clear();
                int yStartIndex = 0;
                List<double> yData = _dataEntity.YData as List<double>;
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
                    case StripChartX.FitType.None:
                        _dataEntity.Parallel.FillNoneFitPlotData(0);
                        break;
                    case StripChartX.FitType.Range:
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
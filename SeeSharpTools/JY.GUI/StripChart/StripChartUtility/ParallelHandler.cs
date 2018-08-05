using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI.StripChartUtility
{
    internal class ParallelHandler
    {
        private readonly ParallelOptions _option;
        private readonly object _parallelLock = new object();

        //每个计算块的大小
        private int _blockSize;
        private int _indexOffset;

        private readonly PlotAction _plotter;

        public ParallelHandler(PlotAction plotter)
        {
            this._plotter = plotter;
            this._option = new ParallelOptions();
            this._indexOffset = 0;
            // 计算限制型配置并行度为内核个数
            _option.MaxDegreeOfParallelism = Environment.ProcessorCount;

            this._maxDatas = new double[_option.MaxDegreeOfParallelism];
            this._minDatas = new double[_option.MaxDegreeOfParallelism];
        }

        #region Max / Min /Interval计算

        private IList<double> _datas;
        private readonly double[] _maxDatas;
        private readonly double[] _minDatas;

        public void GetMaxAndMin(IList<double> datas, out double max, out double min)
        {
            // 点数小于4000时手动计算
            if (datas.Count <= 4000)
            {
                max = datas[0];
                min = datas[0];
                for (int i = 1; i < datas.Count; i++)
                {
                    if (double.IsNaN(datas[i]))
                    {
                        continue;
                    }
                    if (datas[i] > max)
                    {
                        max = datas[i];
                    }
                    else if (datas[i] < min)
                    {
                        min = datas[i];
                    }
                }
            }
            else
            {
                _datas = datas;
                _blockSize = GetBlockSize(datas.Count);
                _indexOffset = 0;
                Parallel.For(0, _option.MaxDegreeOfParallelism, _option, FillMaxAndMinToBuf);
                _datas = null;
                max = _maxDatas.Max();
                min = _minDatas.Min();
            }
        }

        private void FillMaxAndMinToBuf(int blockIndex)
        {
            int startIndex = blockIndex * _blockSize + _indexOffset;
            int endIndex = startIndex + _blockSize;
            if (endIndex > _datas.Count)
            {
                endIndex = _datas.Count;
            }
            double maxValue = _datas[endIndex - 1];
            double minValue = _datas[endIndex - 1];
            if (double.IsNaN(maxValue) || double.IsNaN(minValue))
            {
                _maxDatas[blockIndex] = double.NaN;
                _minDatas[blockIndex] = double.NaN;
                return;
            }
            for (int index = startIndex; index < endIndex - 1; index++)
            {
                if (double.IsNaN(_datas[index]))
                {
                    continue;
                }
                if (_datas[index] > maxValue)
                {
                    maxValue = _datas[index];
                }
                else if (_datas[index] < minValue)
                {
                    minValue = _datas[index];
                }
            }
            _maxDatas[blockIndex] = maxValue;
            _minDatas[blockIndex] = minValue;
        }

        #endregion

        #region Fill Function
        
        #region No Fit

        public void FillNoneFitPlotData(int startIndex)
        {
            int plotIndex = 0;
            for (int index = 0; index < _plotter.PlotSize; index++)
            {
                int pointIndex = (startIndex + index);
                _plotter.XAxisData[plotIndex] = (_plotter.XWrapBuf[pointIndex]);
                for (int i = 0; i < _plotter.YWrapBufs.Count; i++)
                {
                    _plotter.YAxisData[i][plotIndex] = _plotter.YWrapBufs[i][pointIndex];
                }
                plotIndex++;
            }
        }

        #endregion

        #region Range Fit

        public void FillRangeFitPlotData(int indexOffset)
        {
            //将PlotSize的数据分为2*_option.MaxDegreeOfParallelism段，每段最长为_segmentSize
            _blockSize = GetBlockSize(this._plotter.PlotSize / 2);
            this._indexOffset = indexOffset;
            Parallel.For(0, _option.MaxDegreeOfParallelism, _option, FillRangeFitArrayXData);
        }

        private void FillRangeFitArrayXData(int segmentIndex)
        {
            List<string> xDataBuf = _plotter.XAxisData;
            List<List<double>> yDataBuf = _plotter.YAxisData;

            // 一个拟合对在真是数据中的索引起始位置
            int start = _blockSize*segmentIndex;
            // 一个拟合对在真是数据中的索引结束位置，不包含该位置
            int end = _blockSize*(segmentIndex + 1);
            if (end > this._plotter.PlotSize/2)
            {
                end = this._plotter.PlotSize/2;
            }
            // 两个相邻的数据作为拟合对。
            for (int dataPairIndex = start; dataPairIndex < end; dataPairIndex++)
            {
                // 待写入缓存的索引位置
                int startBufIndex = 2*dataPairIndex;
                // 真实数据的起始索引
                int pointStartIndex = startBufIndex* _plotter.SparseRatio + _indexOffset;
                // 真实数据的终止索引，不包含该点
                int pointEndIndex = pointStartIndex + 2*_plotter.SparseRatio;
                if (pointEndIndex > _plotter.XWrapBuf.DataSize)
                {
                    pointEndIndex = _plotter.XWrapBuf.DataSize;
                }

                xDataBuf[startBufIndex] = _plotter.XWrapBuf[pointStartIndex];
                xDataBuf[startBufIndex + 1] = _plotter.XWrapBuf[pointStartIndex + _plotter.SparseRatio];

                for (int lineIndex = 0; lineIndex < _plotter.YWrapBufs.Count; lineIndex++)
                {
                    double maxValue = _plotter.YWrapBufs[lineIndex][pointEndIndex - 1];
                    double minValue = _plotter.YWrapBufs[lineIndex][pointEndIndex - 1];
                    
                    int maxIndex = pointEndIndex - 1;
                    int minIndex = pointEndIndex - 1;
                    for (int pointIndex = pointStartIndex; pointIndex < pointEndIndex - 1; pointIndex++)
                    {
                        double value = _plotter.YWrapBufs[lineIndex][pointIndex];
                        if (value > maxValue)
                        {
                            maxIndex = pointIndex;
                            maxValue = value;
                        }
                        else if (value < minValue)
                        {
                            minIndex = pointIndex;
                            minValue = value;
                        }
                    }
                    if (maxIndex > minIndex)
                    {
                        yDataBuf[lineIndex][startBufIndex] = minValue;
                        yDataBuf[lineIndex][startBufIndex + 1] = maxValue;
                    }
                    else
                    {
                        yDataBuf[lineIndex][startBufIndex] = maxValue;
                        yDataBuf[lineIndex][startBufIndex + 1] = minValue;
                    }
                }
            }
        }

        #endregion

        #endregion

        private int GetBlockSize(int count)
        {
            return (count - 1) / _option.MaxDegreeOfParallelism + 1;
        }

        public void RefreshPointXValue(int startIndex, SeriesCollection plotSeries)
        {
            Parallel.For(0, plotSeries[0].Points.Count, _option, pointIndex =>
            {
                for (int lineIndex = 0; lineIndex < plotSeries.Count; lineIndex++)
                {
                    plotSeries[lineIndex].Points[pointIndex].XValue = startIndex;
                }
                startIndex += _plotter.SparseRatio;
            });
        }
    }
}
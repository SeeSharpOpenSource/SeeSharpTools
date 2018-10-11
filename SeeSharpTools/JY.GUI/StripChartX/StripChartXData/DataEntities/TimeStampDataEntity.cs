using System;
using System.Collections;
using System.Collections.Generic;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI.StripChartXData.DataEntities
{
    internal class TimeStampDataEntity<TDataType> : DataEntityBase
    {
        private readonly List<OverLapWrapBuffer<TDataType>> _yBuffers;

        private readonly PlotBuffer<TDataType> _plotBuffer;

        // 是否使用单点模式输入的标识位
        private bool _singleSamplePlotMode;

        private int _samplesInChart;

        // 是否每次输入的样点个数都相等的标识位
        private bool _isEven;

        // 保存每次写入时的第一个时间戳
        private readonly List<DateTime> _timeStamps;
        // 保存每次写入的样点个数
        private List<int> _blockCounts;

        public TimeStampDataEntity(PlotManager plotManager, DataEntityInfo dataInfo, int sampleCount) : base(plotManager, dataInfo)
        {
            _samplesInChart = 0;
            _singleSamplePlotMode = (sampleCount == 1);
            if (_singleSamplePlotMode)
            {
                _timeStamps = new List<DateTime>(dataInfo.Capacity);
                _blockCounts = null;
                _isEven = true;
            }
            else
            {
                _timeStamps = new List<DateTime>(dataInfo.Capacity/sampleCount);
                _blockCounts = new List<int>(dataInfo.Capacity/sampleCount);
                _isEven = true;
            }

            _yBuffers = new List<OverLapWrapBuffer<TDataType>>(DataInfo.LineCount);
            for (int i = 0; i < DataInfo.LineCount; i++)
            {
                _yBuffers.Add(new OverLapWrapBuffer<TDataType>(DataInfo.Capacity));
            }

            _plotBuffer = new PlotBuffer<TDataType>(DataInfo.LineCount, DataInfo.Capacity);
        }

        public override int PlotCount
        {
            get { return _plotBuffer.PlotCount; }
            set { _plotBuffer.PlotCount = value; }
        }

        public override int SamplesInChart => _samplesInChart;

        public override void Initialize(int sampleCount)
        {
            base.Initialize(sampleCount);
            _samplesInChart = 0;
            _singleSamplePlotMode = (sampleCount == 1);

            if (_singleSamplePlotMode)
            {
                _timeStamps.Clear();
                _blockCounts = null;
                _isEven = true;
            }
            else
            {
                _timeStamps.Clear();
                if (null == _blockCounts)
                {
                    _blockCounts = new List<int>(DataInfo.Capacity / sampleCount);
                }
                else
                {
                    _blockCounts.Clear();
                }
                _isEven = true;
            }
        }

        public override void AddPlotData(IList<string> xData, Array lineData)
        {
            throw new NotImplementedException();
        }

        public override void AddPlotData(DateTime[] startTime, Array lineData)
        {
            int sampleCount = startTime.Length;
            _timeStamps.AddRange(startTime);
            int offset = 0;
            RefreshIsEvenFlag(sampleCount);
            for (int i = 0; i < _yBuffers.Count; i++)
            {
                _yBuffers[i].Add(lineData, sampleCount, offset);
                offset += sampleCount;
            }
            if (!_singleSamplePlotMode)
            {
                for (int i = 0; i < sampleCount; i++)
                {
                    _blockCounts.Add(sampleCount);
                }
            }
            int samplesToRemove = RefreshSamplesInChart(sampleCount);
            RemoveXDataExceedDisplayPoints(samplesToRemove);
        }

        public override void AddPlotData(Array lineData, int sampleCount)
        {
            DateTime timeStamp = ParentManager.NextTimeStamp;
            if ((DateTime.MaxValue.Equals(timeStamp) || DateTime.MinValue.Equals(timeStamp)))
            {
                timeStamp = DateTime.Now;
            }
            _timeStamps.Add(timeStamp);
            int offset = 0;
            RefreshIsEvenFlag(sampleCount);
            for (int i = 0; i < _yBuffers.Count; i++)
            {
                _yBuffers[i].Add(lineData, sampleCount, offset);
                offset += sampleCount;
            }
            if (!_singleSamplePlotMode)
            {
                for (int i = 0; i < sampleCount; i++)
                {
                    _blockCounts.Add(sampleCount);
                }
            }
            int samplesToRemove = RefreshSamplesInChart(sampleCount);
            RemoveXDataExceedDisplayPoints(samplesToRemove);
        }

        public override List<int> GetXPlotBuffer()
        {
            return _plotBuffer.XPlotBuffer;
        }

        public override string GetXValue(int xIndex)
        {
            int blockIndex = 0;
            int posOffset = 0;
            if (_isEven)
            {
                int eachBlockSize = _blockCounts[_blockCounts.Count - 1];
                blockIndex = xIndex/eachBlockSize;
                posOffset = xIndex - blockIndex*eachBlockSize;
            }
            // 否则迭代计算需要删除的位置
            else
            {
                // 写入点数比较多，从后面开始计算
                if (xIndex > _samplesInChart / 2)
                {
                    int count = 0;
                    blockIndex = _blockCounts.Count;
                    do
                    {
                        blockIndex--;
                        count += _blockCounts[blockIndex];
                    } while (count < _samplesInChart - xIndex);
                    posOffset = count - (_samplesInChart - xIndex);
                }
                // 写入点数较少，从前面开始计算
                else
                {
                    int count = 0;
                    blockIndex = -1;
                    do
                    {
                        blockIndex++;
                        count += _blockCounts[blockIndex];
                    } while (count <= xIndex);
                    posOffset = _blockCounts[blockIndex] - (count - xIndex);
                }
            }
            DateTime time = _timeStamps[blockIndex]+TimeSpan.FromMilliseconds(posOffset*ParentManager.TimeInterval.TotalMilliseconds);
            return time.ToString(ParentManager.TimeStampFormat);
            
        }

        public override object GetYValue(int xIndex, int seriesIndex)
        {
            return  _yBuffers[seriesIndex][xIndex];
        }

        public override IList<TDataType1> GetPlotDatas<TDataType1>(int startIndex, int endIndex)
        {
            throw new NotImplementedException();
        }

        public override void GetMaxAndMinYValue(int seriesIndex, out double maxYValue, out double minYValue)
        {
            ParallelHandler.GetMaxAndMin(_yBuffers[seriesIndex], out maxYValue, out minYValue);
        }

        public override void GetMaxAndMinYValue(out double maxYValue, out double minYValue)
        {
            maxYValue = double.MinValue;
            minYValue = double.MaxValue;
            double tmpMaxYValue = 0;
            double tmpMinYValue = 0;

            foreach (OverLapWrapBuffer<TDataType> yBuffer in _yBuffers)
            {
                ParallelHandler.GetMaxAndMin(yBuffer, out tmpMaxYValue, out tmpMinYValue);
                if (maxYValue < tmpMaxYValue)
                {
                    maxYValue = tmpMaxYValue;
                }
                if (minYValue > tmpMinYValue)
                {
                    minYValue = tmpMinYValue;
                }
            }
        }

        private void RefreshIsEvenFlag(int newBlockSize)
        {
            // 如果已经不是均等增加则一直不再按照均等计算
            // 如果block个数大于2，则新的和上一个不同视为不均等
            // 如果block个数为1，且上一个block小于最大显示点数，且和新的不相等，则视为不均等
            int count = _blockCounts.Count;
            this._isEven = _isEven && !((count >= 2 &&  _blockCounts[count - 1] != newBlockSize) || 
                (1 == count && _blockCounts[0] < ParentManager.DisplayPoints && _blockCounts[0] != newBlockSize));
        }

        // 获取要删除的Stamp列表中的元素个数和需要后移的元素个数
        private void RemoveXDataExceedDisplayPoints(int samplesToRemove)
        {
            if (samplesToRemove <= 0)
            {
                return;
            }
            if (_singleSamplePlotMode)
            {
                RemoveSingleModeSamples(samplesToRemove);
            }
            else
            {
                RemoveNoSingleModeSamples(samplesToRemove);
            }
        }

        private void RemoveSingleModeSamples(int samplesToRemove)
        {
            _timeStamps.RemoveRange(0, samplesToRemove);
        }

        private void RemoveNoSingleModeSamples(int samplesToRemove)
        {
            int displayPoints = ParentManager.DisplayPoints;
            int removedStampCount;
            int offset;
            int stampCount = _timeStamps.Count;
            // 如果均等添加，则直接计算删除的位置
            if (_isEven)
            {
                int eachBlockSize = _blockCounts[_blockCounts.Count - 1];
                removedStampCount = samplesToRemove/eachBlockSize;
                offset = samplesToRemove - removedStampCount*eachBlockSize;
            }
            // 否则迭代计算需要删除的位置
            else
            {
                // 写入点数比较多，从后面开始计算
                if (samplesToRemove > displayPoints/2)
                {
                    int count = 0;
                    int index = stampCount;
                    do
                    {
                        index--;
                        count += _blockCounts[index];
                    } while (count < displayPoints);
                    offset = count - displayPoints;
                    removedStampCount = index;
                }
                // 写入点数较少，从前面开始计算
                else
                {
                    int index = 0;
                    int count = _blockCounts[index];
                    while (count <= samplesToRemove)
                    {
                        count += _blockCounts[++index];
                    }
                    offset = samplesToRemove - (count - _blockCounts[index]);
                    removedStampCount = index;
                }
            }
            _timeStamps.RemoveRange(0, removedStampCount);
            _blockCounts.RemoveRange(0, removedStampCount);

            _timeStamps[0] += TimeSpan.FromMilliseconds(ParentManager.TimeInterval.TotalMilliseconds*offset);
            _blockCounts[0] -= offset;
        }

        private int RefreshSamplesInChart(int sampleCount)
        {
            _samplesInChart += sampleCount;
            int samplesToRemove = _samplesInChart - ParentManager.DisplayPoints;
            if (samplesToRemove > 0)
            {
                _samplesInChart = ParentManager.DisplayPoints;
            }
            return samplesToRemove;
        }

        public override void Clear()
        {
            base.Clear();
            _samplesInChart = 0;
            _singleSamplePlotMode = false;
            _timeStamps?.Clear();
            _blockCounts?.Clear();
            _isEven = true;
            foreach (OverLapWrapBuffer<TDataType> yBuffer in _yBuffers)
            {
                yBuffer.Clear();
            }
        }

        public override IList GetXData()
        {
            return _plotBuffer.XPlotBuffer.GetRange(0, _plotBuffer.PlotCount);
        }

        public override IList GetYData()
        {
            _plotBuffer.YShallowBuffer.Clear();
            foreach (List<TDataType> yBuf in _plotBuffer.YPlotBuffer)
            {
                _plotBuffer.YShallowBuffer.Add(yBuf.GetRange(0, _plotBuffer.PlotCount));
            }
            return _plotBuffer.YShallowBuffer;
        }

        public override bool FillYPlotDatas(int beginXIndex, int endXIndex, bool forceRefresh, int seriesIndex, int newSparseRatio, int plotCount)
        {
            // 如果不需要强制更新，且当前起始位置等于上次起始位置、当前结束位置小于等于上次结束位置、新的SparseRatio等于上次的SpaseRatio时无需更新数据。
            if (!forceRefresh && LastYStartIndex[seriesIndex] == beginXIndex && LastYEndIndex[seriesIndex] >= endXIndex &&
                SparseRatio[seriesIndex] == newSparseRatio)
            {
                return false;
            }
            LastYStartIndex[seriesIndex] = beginXIndex;
            LastYEndIndex[seriesIndex] = endXIndex;
            SparseRatio[seriesIndex] = newSparseRatio;

            if (1 == newSparseRatio)
            {
                ParallelHandler.FillNoneFitPlotData(beginXIndex, SparseRatio[seriesIndex], _yBuffers[seriesIndex],
                            _plotBuffer.YPlotBuffer[seriesIndex], plotCount);
            }
            else
            {
                switch (ParentManager.FitType)
                {
                    case StripChartX.FitType.None:
                        ParallelHandler.FillNoneFitPlotData(beginXIndex, SparseRatio[seriesIndex], _yBuffers[seriesIndex],
                            _plotBuffer.YPlotBuffer[seriesIndex], plotCount);
                        break;
                    case StripChartX.FitType.Range:
                        ParallelHandler.FillRangeFitPlotData<TDataType>(beginXIndex, SparseRatio[seriesIndex], _yBuffers[seriesIndex],
                            _plotBuffer.YPlotBuffer[seriesIndex], plotCount);
                        break;
                }
            }

            return true;
        }
    }
}
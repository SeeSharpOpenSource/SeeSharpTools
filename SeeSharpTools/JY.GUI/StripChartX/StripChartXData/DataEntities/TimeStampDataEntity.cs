using System;
using System.Collections.Generic;
using SeeSharpTools.JY.GUI.StripChartXUtility;

namespace SeeSharpTools.JY.GUI.StripChartXData.DataEntities
{
    internal class TimeStampDataEntity<TDataType> : DataEntityBase
    {
        private readonly List<OverLapWrapBuffer<TDataType>> _yBuffers;

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
            if (_singleSamplePlotMode)
            {
                for (int i = 0; i < sampleCount; i++)
                {
                    _blockCounts.Add(1);
                }
            }
            RemoveXDataExceedDisplayPoints(sampleCount);
        }

        public override void AddPlotData(Array lineData, int sampleCount)
        {
            DateTime timeStamp = ParentManager.NextTimeStamp;
            if ((DateTime.MaxValue.Equals(timeStamp) || DateTime.MinValue.Equals(timeStamp)))
            {
                timeStamp = DateTime.Now;
            }
            _timeStamps.Add(timeStamp);
            _blockCounts?.Add(sampleCount);
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
                    _blockCounts.Add(1);
                }
            }
            RemoveXDataExceedDisplayPoints(sampleCount);
        }

        public override void GetXYValue(int xIndex, int seriesIndex, ref string xValue, ref string yValue)
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
            xValue = time.ToString(ParentManager.TimeStampFormat);
            yValue = _yBuffers[seriesIndex][xIndex].ToString();
        }

        public override IList<TDataType1> GetPlotDatas<TDataType1>(int startIndex, int endIndex)
        {
            throw new NotImplementedException();
        }

        public override void GetMaxAndMinYValue(int seriesIndex, out double maxYValue, out double minYValue)
        {
            throw new NotImplementedException();
        }

        public override void GetMaxAndMinYValue(out double maxYValue, out double minYValue)
        {
            throw new NotImplementedException();
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
        private void RemoveXDataExceedDisplayPoints(int sampleCount)
        {
            int displayPoints = ParentManager.DisplayPoints;
            int stampCount = _timeStamps.Count;
            int currentSampleCount = sampleCount + _samplesInChart;
            if (currentSampleCount < displayPoints)
            {
                return;
            }
            int removedStampCount;
            int offset;
            // 如果均等添加，则直接计算删除的位置
            if (_isEven)
            {
                int eachBlockSize = _blockCounts[_blockCounts.Count - 1];
                removedStampCount = (currentSampleCount - displayPoints)/eachBlockSize;
                offset = (stampCount - removedStampCount)*eachBlockSize - displayPoints;
            }
            // 否则迭代计算需要删除的位置
            else
            {
                // 写入点数比较多，从后面开始计算
                if (sampleCount > displayPoints/2)
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
                    int removedPointCount = currentSampleCount - displayPoints;
                    int count = 0;
                    int index = -1;
                    do
                    {
                        index++;
                        count += _blockCounts[index];
                    } while (count <= removedPointCount);
                    offset = _blockCounts[index] - (count - removedPointCount);
                    removedStampCount = index;
                }
            }
            _timeStamps.RemoveRange(0, removedStampCount + 1);
            _blockCounts.RemoveRange(0, removedStampCount + 1);

            _timeStamps[0] += TimeSpan.FromMilliseconds(ParentManager.TimeInterval.TotalMilliseconds*offset);
            _blockCounts[0] -= offset;
        }

        private void RefreshSamplesInChart(int sampleCount)
        {
            _samplesInChart += sampleCount;
            if (_samplesInChart > ParentManager.DisplayPoints)
            {
                _samplesInChart = ParentManager.DisplayPoints;
            }
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
    }
}
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

        private readonly List<IList<TDataType>> _yPlotBuffer;

        internal List<IList<TDataType>> YPlotBuffer => _yPlotBuffer;

        internal List<IList<TDataType>> YShallowBuffer { get; }

        // 绘图缓存中X轴数据的稀疏比和最大最小值
        public int SparseRatio { get; set; }
        public double PlotXStart { get; set; }
        public double PlotXEnd { get; set; }

        // 最新一次绘图中X轴数据的最大最小值
        public double CurrentXStart { get; set; }
        public double CurrentXEnd { get; set; }

        private int _plotCount;

        public int PlotCount
        {
            get { return _plotCount; }
            set { _plotCount = value; }
        }

        private readonly DataEntityInfo _dataInfo;

        public PlotBuffer(int lineCount, int displayCount)
        {
            _dataInfo = new DataEntityInfo();
            _plotCount = 0;
            SparseRatio = int.MaxValue;
            this.PlotXStart = double.NaN;
            this.PlotXEnd = double.NaN;

            _xPlotBuffer = new List<int>(Constants.MaxPointsInSingleSeries);
            FillDefaultToListBuffer(_xPlotBuffer, 0, Constants.MaxPointsInSingleSeries);
            _yPlotBuffer = new List<IList<TDataType>>(Constants.DefaultLineCapacity);
            YShallowBuffer = new List<IList<TDataType>>(Constants.DefaultLineCapacity);
            for (int i = 0; i < lineCount; i++)
            {
                IList<TDataType> yBuf = new List<TDataType>(Constants.MaxPointsInSingleSeries);
                _yPlotBuffer.Add(yBuf);
            }
            foreach (IList<TDataType> yBuf in _yPlotBuffer)
            {
                FillZeroToNumericCollection(yBuf);
            }
        }

        #region Utility

        private static void FillZeroToNumericCollection(IList<TDataType> yBuf)
        {
            string typeName = typeof(TDataType).FullName;
            if (typeName.Equals(typeof(double).FullName))
            {
                IList<double> collection = yBuf as IList<double>;
                FillDefaultToListBuffer(collection, 0, Constants.MaxPointsInSingleSeries);
            }
            else if (typeName.Equals(typeof(float).FullName))
            {
                IList<float> collection = yBuf as IList<float>;
                FillDefaultToListBuffer(collection, 0, Constants.MaxPointsInSingleSeries);
            }
            else if (typeName.Equals(typeof(int).FullName))
            {
                IList<int> collection = yBuf as IList<int>;
                FillDefaultToListBuffer(collection, 0, Constants.MaxPointsInSingleSeries);
            }
            else if (typeName.Equals(typeof(uint).FullName))
            {
                IList<uint> collection = yBuf as IList<uint>;
                FillDefaultToListBuffer(collection, (uint)0, Constants.MaxPointsInSingleSeries);
            }
            else if (typeName.Equals(typeof(short).FullName))
            {
                IList<short> collection = yBuf as IList<short>;
                FillDefaultToListBuffer(collection, (short)0, Constants.MaxPointsInSingleSeries);
            }
            else if (typeName.Equals(typeof(ushort).FullName))
            {
                IList<ushort> collection = yBuf as IList<ushort>;
                FillDefaultToListBuffer(collection, (ushort)0, Constants.MaxPointsInSingleSeries);
            }
            else if (typeName.Equals(typeof(byte).FullName))
            {
                IList<byte> collection = yBuf as IList<byte>;
                FillDefaultToListBuffer(collection, (byte)0, Constants.MaxPointsInSingleSeries);
            }
//            FillDefaultToListBuffer(yBuf, 0, Constants.MaxPointsInSingleSeries);
        }

        private static void FillDefaultToListBuffer<T>(IList<T> buffer, T value, int count)
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
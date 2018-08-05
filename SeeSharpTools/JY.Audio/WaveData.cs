using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace SeeSharpTools.JY.Audio
{
    public class WaveData<XDataType, YDataType>
    {
        private XDataType[] _xDataCache = null;
        private readonly YDataType[] _yDataCache = null;

        public XDataType[] XData { get { return _xDataCache;} }
        public YDataType[] YData { get { return _yDataCache; } }

        public int ChannelCount { get; private set; }

        public int SampleSize { get; private set; }

        private static readonly string[] ValidXDataType = {typeof (string).ToString(),
            typeof(int).ToString(), typeof(uint).ToString(), typeof(float).ToString(),
            typeof(double).ToString(), typeof(long).ToString(), typeof(ulong).ToString()};
        private static readonly string[] ValidYDataType = {typeof(int).ToString(),
            typeof(uint).ToString(), typeof(float).ToString(), typeof(double).ToString(),
            typeof(long).ToString(), typeof(ulong).ToString()};

        public WaveData(XDataType[] xData, YDataType[] yData, bool fullCopy = false)
        {
            CheckData(xData, yData);

            SampleSize = xData.Length;
            ChannelCount = yData.Length/xData.Length;

            if (fullCopy)
            {
                FullCopyXData(xData);
                int yTypeSize = Marshal.SizeOf(typeof(YDataType));
                _yDataCache = new YDataType[yData.Length];
                FullCopy(yData, _yDataCache, yTypeSize * yData.Length);
            }
            else
            {
                _xDataCache = xData;
                _yDataCache = yData;
            }
        }

        private void FullCopyXData(XDataType[] xData)
        {
            _xDataCache = new XDataType[SampleSize];
            if (typeof (string).ToString().Equals(typeof (XDataType).ToString()))
            {
                FullCopy(xData, ref _xDataCache);
            }
            else
            {
                int xTypeSize = Marshal.SizeOf(typeof (XDataType));
                FullCopy(xData, _xDataCache, xTypeSize*SampleSize);
            }
        }

        private static void CheckData(XDataType[] xData, YDataType[] yData)
        {
            if (null == xData || null == yData || yData.Length % xData.Length != 0)
            {
                // TODO raise exception
                return;
            }
            if (!ValidXDataType.Contains(typeof(XDataType).ToString()) ||
                ValidYDataType.Contains(typeof(YDataType).ToString()))
            {
                // TODO raise exception
                return;
            }
        }

        private static void CheckData(XDataType[] xData, YDataType[,] yData)
        {
            if (null == xData || null == yData || yData.GetLength(1) != xData.Length)
            {
                // TODO raise exception
                return;
            }
            if (!ValidXDataType.Contains(typeof(XDataType).ToString()) ||
                ValidYDataType.Contains(typeof(YDataType).ToString()))
            {
                // TODO raise exception
                return;
            }
        }

        private void FullCopy<DataType>(DataType[] data, ref DataType[] dataCache)
        {
            for (int i = 0; i < data.Length; i++)
            {
                dataCache[i] = data[i];
            }
        }

        private void FullCopy(Array data, Array dataCache, int dataSize)
        {
            Buffer.BlockCopy(data, 0, dataCache, 0, dataSize);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeeSharpTools.JY.GUI.StripChartXUtility
{
    internal class DataConvertor
    {
        delegate void ConvertMethod(Array rawData, int size);
        private readonly Dictionary<string, ConvertMethod> _convertMapping;
        private double[] _convertBuf1Dim;
        private double[,] _convertBuf2Dim;

        public DataConvertor()
        {
            _convertMapping = new Dictionary<string, ConvertMethod>(6);
            // IList接口实现后添加转换代码
            _convertMapping.Add(typeof(double).Name, null);
            _convertMapping.Add(typeof(float).Name, null);
            _convertMapping.Add(typeof(int).Name, null);
            _convertMapping.Add(typeof(uint).Name, null);
            _convertMapping.Add(typeof(short).Name, null);
            _convertMapping.Add(typeof(ushort).Name, null);
        }

        // 将一维数组转换为double数组
        internal double[] Convert(Array data, int size)
        {
            if (data.GetValue(0).GetType().Name.Equals(typeof (double).Name))
            {
                return data as double[];
            }

            if (null == _convertBuf1Dim || _convertBuf1Dim.Length != size)
            {
                _convertBuf1Dim = new double[size];
            }
            Array.Copy(data, _convertBuf1Dim, size);
            return _convertBuf1Dim;
        }

        // 将二维数组转换为double数组
        internal double[,] Convert(Array data, int rowCount, int colCount)
        {
            if (data.GetValue(0, 0).GetType().Name.Equals(typeof(double).Name))
            {
                return data as double[,];
            }

            if (null == _convertBuf2Dim || _convertBuf2Dim.GetLength(0) != rowCount || _convertBuf2Dim.GetLength(1) != colCount)
            {
                _convertBuf2Dim = new double[rowCount, colCount];
            }
            Array.Copy(data, _convertBuf2Dim, rowCount * colCount);
            return _convertBuf2Dim;
        }

        // 将锯齿数组转换为double数组
        internal double[][] Convert<TDataType>(TDataType[][] data)
        {
            double[][] dstBuf = new double[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                int dataLength = data[i].Length;
                double[] singleBuf = new double[dataLength];
                Array.Copy(data[i], singleBuf, dataLength);
                dstBuf[i] = singleBuf;
            }
            return dstBuf;
        }

        public bool IsValidType(Type dataType)
        {
            return _convertMapping.ContainsKey(dataType.Name);
        }

    }
}
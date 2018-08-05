using System;
using System.Collections.Generic;
using System.Linq;

namespace SeeSharpTools.JY.GUI.EasyChartComponents
{
    internal class DataEntity
    {
        internal double MaxXValue { get; }
        internal double MinXValue { get; }

        internal List<double> XData { get; private set; }
        internal List<double> YData { get; private set; }

        internal double XStart { get; }
        internal double XIncrement { get; }
        internal double XMinInterval { get; }
        internal int Size { get; }
        internal int LineNum { get; private set; }

        internal XDataInputType XDataInputType { get; }
        private const double MinLegalInterval = 1E-40;
        public DataEntity(double[] xData, IList<double> yData, int yDataSize)
        {
            XDataInputType = XDataInputType.Array;
            XData = new List<double>(xData);
            Size = xData.Length;

            MaxXValue = xData.Max();
            MinXValue = xData.Min();

            XMinInterval = GetArrayXMinInternval();

            //            InitYDataBuf(yData, yDataSize);
            LineNum = yDataSize / Size;
            YData = new List<double>(yData);
        }
        
        public DataEntity(double xStart, double xIncrement, int size, IList<double> yData, int yDataSize)
        {
            
            XDataInputType = XDataInputType.Increment;
            XStart = xStart;
            XIncrement = xIncrement;
            Size = size;

            MaxXValue = xStart + (size - 1) * xIncrement;
            MinXValue = xStart;

            XMinInterval = xIncrement > MinLegalInterval ? xIncrement : MinLegalInterval;

            //            InitYDataBuf(yData, yDataSize);
            LineNum = yDataSize / Size;
            YData = new List<double>(yData);
        }

        public static List<DataEntity> GetMultiDataEntity(double[][] xData, double[][] yData)
        {
            List<DataEntity> dataEntities = new List<DataEntity>(xData.Length);
            for (int i = 0; i < xData.Length; i++)
            {
                dataEntities.Add(new DataEntity(xData[i], yData[i], xData[i].Length));
            }
            return dataEntities;
        }

        // TODO not perfect, just for time efficiency
        private double GetArrayXMinInternval()
        {
            const double minDoubleValue = 1.0E-40;
            double lastDiff = double.MaxValue;
            for (int i = 1; i < Size; i++)
            {
                double diff = Math.Abs(XData[i] - XData[i - 1]);
                if (diff > MinLegalInterval && diff - lastDiff <= -1.0E-40)
                {
                    lastDiff = diff;
                }
            }
            return lastDiff < minDoubleValue ? lastDiff/10 : lastDiff;
        }

        //        public DataEntity(double[][] xData, double[][] yData, int lineIndex)
        //        {
        //            XDataInputType = XDataInputType.Array;
        //            int dataSize = xData[0].Length;
        //            double[] tmpBuf = new double[dataSize];
        //            int lineDataSize = dataSize*sizeof (double);
        //            int copyOffset = lineIndex * lineDataSize;
        //            Buffer.BlockCopy(xData, copyOffset, tmpBuf, 0, lineDataSize);
        //        }

        //        private void InitYDataBuf(Array yData, int yDataSize)
        //        {
        //            YData = new List<double[]>(EasyChart.maxSeriesToDraw);
        //            LineNum = yDataSize / Size;
        //            int offset = 0;
        //            int lineDataSize = Size * sizeof(double);
        //            for (int i = 0; i < LineNum; i++)
        //            {
        //                double[] lineData = new double[Size];
        //                Buffer.BlockCopy(yData, offset, lineData, 0, lineDataSize);
        //                offset += lineDataSize;
        //                YData.Add(lineData);
        //            }
        //        }

        public List<double> GetXData()
        {
            List<double> tmpXData = null;
            switch (XDataInputType)
            {
                case XDataInputType.Array:
                    tmpXData = XData;
                    break;
                case XDataInputType.Increment:
                    tmpXData = new List<double>(Size);
                    for (int i = 0; i < Size; i++)
                    {
                        tmpXData.Add(XStart + XIncrement * i);
                    }
                    break;
            }
            return tmpXData;
        }

        public double GetXData(int index)
        {
            switch (XDataInputType)
            {
                case XDataInputType.Increment:
                    return XStart + index*XIncrement;
                case XDataInputType.Array:
                    return XData[index];
                default:
                    return 0;
            }
        }

        public List<double> GetYData(int lineIndex)
        {
            return YData.GetRange(lineIndex * Size, Size);
        }

        public double GetYData(int lineIndex, int pointIndex)
        {
            return YData[lineIndex * Size + pointIndex];
        }

        public bool IsEqaual(DataEntity dataEntity)
        {
            if (dataEntity.XDataInputType != XDataInputType)
            {
                return false;
            }

            switch (XDataInputType)
            {
                case XDataInputType.Increment:
                    return XStart.Equals(dataEntity.XStart) && XIncrement.Equals(dataEntity.XIncrement) && 
                        Size == dataEntity.Size;
                case XDataInputType.Array:
                    return ReferenceEquals(XData, dataEntity.XData);
                default:
                    return false;
            }
        }

        public int FindeNearestIndex(ref double xValue, ref double yValue, int seriesIndex)
        {
            if (double.IsNaN(xValue) || double.IsNaN(yValue))
            {
                return -1;
            }
            int nearIndexes;
            switch (XDataInputType)
            {
                case XDataInputType.Increment:
                    nearIndexes = FindIncrementNearIndex(ref xValue, ref yValue, seriesIndex);
                    break;

                case XDataInputType.Array:
                    nearIndexes = FindArrayNearIndex(ref xValue, ref yValue, seriesIndex);
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
            else if (nearIndex > Size - 1)
            {
                nearIndex = Size - 1;
            }
            xValue = GetXData(nearIndex);
            yValue = GetYData(seriesIndex, nearIndex);
            return nearIndex;
        }

        private int FindArrayNearIndex(ref double xValue, ref double yValue, int seriesIndex)
        {
            double minDiff = XMinInterval*2;
            List<int> nearestIndexes = new List<int>(5);
            double minDValue = double.MaxValue;
            for (int i = 0; i < Size; i++)
            {
                double diff = Math.Abs(XData[i] - xValue);
                if (diff - minDValue > minDiff)
                {
                    continue;
                }
                if (minDValue - diff > minDValue)
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
    }
}
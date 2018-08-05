using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeeSharpTools.JY.GUI
{
    internal class SparseStripPlotter : PlotAction, IDisposable
    {
        private int _globalSparseRatio = 1;

        private AxisViewAdapter _axisViewAdapter;

        public SparseStripPlotter(StripPlotter plotter, AxisViewAdapter axisViewAdapter, int maxSparseRatio) : base(plotter, axisViewAdapter, maxSparseRatio)
        {
            XWrapBuf = new OverLapStrBuffer(plotter.MaxSampleNum);
            YWrapBufs = new List<OverLapWrapBuffer<double>>(plotter.LineNum);
            for (int i = 0; i < plotter.LineNum; i++)
            {
                YWrapBufs.Add(new OverLapWrapBuffer<double>(plotter.MaxSampleNum));
            }
        }

        private string[] tmpXBuf = new string[1];
        private double[] tmpYBuf = new double[1];


        public override void Plot(string xData, double yData)
        {
            tmpYBuf[0] = yData;
            Plot(tmpXBuf, tmpYBuf);
        }

        public override void Plot(string xData, double[] yData)
        {
            tmpXBuf[0] = xData;
            int pointsInBuf = XWrapBuf.DataSize;
            int sampleSize = 1;
            int newSparseRatio = CalcSparseRatio(sampleSize, pointsInBuf, _globalSparseRatio);
            
            //如果待写入点数和已写入点数是sparseRatio的整数倍则可以直接通过移点实现
            if (0 == sampleSize % newSparseRatio && 0 == pointsInBuf % newSparseRatio)
            {
                RemoveExtraPointsInChart(sampleSize);
                SparsePointsInChart(newSparseRatio, sampleSize);
                CacheXDataToBuf(tmpXBuf, newSparseRatio);
                CacheYDataToBuf(yData, newSparseRatio);
                DrawMultiPoints();
                _globalSparseRatio = newSparseRatio;
            }
            else
            {
                //如果点数没有满则从缓存中第一个开始取，否则从满足最大点数的位置开始取
                _globalSparseRatio = newSparseRatio;
                int readOffSet = pointsInBuf + sampleSize < Plotter.MaxSampleNum ? 0 :
                    pointsInBuf - Plotter.MaxSampleNum + sampleSize;
                CacheXBufToPlotBuf(XWrapBuf, _globalSparseRatio, readOffSet);
                CacheYBufToPlotBuf(YWrapBufs, _globalSparseRatio, readOffSet);
                CacheXDataToBuf(tmpXBuf, _globalSparseRatio);
                CacheYDataToBuf(yData, _globalSparseRatio);
                BindDataToChart();
            }
            FillDataToWrapBuf(tmpXBuf, yData, sampleSize);
        }

        public override void Plot(string[] xData, double[,] yData)
        {
            int pointsInBuf = XWrapBuf.DataSize;
            int sampleSize = xData.Length;
            int newSparseRatio = CalcSparseRatio(sampleSize, pointsInBuf, _globalSparseRatio);
            if (sampleSize >= Plotter.MaxSampleNum)
            {
                _globalSparseRatio = newSparseRatio;
                CacheXDataToBuf(xData, _globalSparseRatio);
                CacheYDataToBuf(yData, _globalSparseRatio);
                BindDataToChart();
            }
            //如果待写入点数和已写入点数是sparseRatio的整数倍则可以直接通过移点实现
            else if (0 == sampleSize%newSparseRatio && 0 == pointsInBuf%newSparseRatio)
            {
                RemoveExtraPointsInChart(sampleSize);
                SparsePointsInChart(newSparseRatio, sampleSize);
                CacheXDataToBuf(xData, newSparseRatio);
                CacheYDataToBuf(yData, newSparseRatio);
                DrawMultiPoints();
                _globalSparseRatio = newSparseRatio;
            }
            else
            {
                //如果点数没有满则从缓存中第一个开始取，否则从满足最大点数的位置开始取
                _globalSparseRatio = newSparseRatio;
                int readOffSet = pointsInBuf + sampleSize < Plotter.MaxSampleNum ? 0 :
                    pointsInBuf - Plotter.MaxSampleNum + sampleSize;
                CacheXBufToPlotBuf(XWrapBuf, _globalSparseRatio, readOffSet);
                CacheYBufToPlotBuf(YWrapBufs, _globalSparseRatio, readOffSet);
                CacheXDataToBuf(xData, _globalSparseRatio);
                CacheYDataToBuf(yData, _globalSparseRatio);
                BindDataToChart();
            }
            FillDataToWrapBuf(xData, yData, sampleSize);
        }

        

        public override void Plot(string[] xData, double[] yData)
        {
            int pointsInBuf = XWrapBuf.DataSize;
            int sampleSize = xData.Length;
            int newSparseRatio = CalcSparseRatio(sampleSize, pointsInBuf, _globalSparseRatio);
            if (sampleSize >= Plotter.MaxSampleNum)
            {
                _globalSparseRatio = newSparseRatio;
                CacheXDataToBuf(xData, _globalSparseRatio);
                CacheYDataToBuf(yData, _globalSparseRatio);
                BindDataToChart();
            }
            //如果待写入点数和已写入点数是sparseRatio的整数倍则可以直接通过移点实现
            else if (0 == sampleSize % newSparseRatio && 0 == pointsInBuf % newSparseRatio)
            {
                RemoveExtraPointsInChart(sampleSize);
                SparsePointsInChart(newSparseRatio, sampleSize);
                CacheXDataToBuf(xData, newSparseRatio);
                CacheYDataToBuf(yData, newSparseRatio);
                DrawMultiPoints();
                _globalSparseRatio = newSparseRatio;
            }
            else
            {
                //如果点数没有满则从缓存中第一个开始取，否则从满足最大点数的位置开始取
                _globalSparseRatio = newSparseRatio;
                int readOffSet = pointsInBuf + sampleSize < Plotter.MaxSampleNum ? 0 :
                    pointsInBuf - Plotter.MaxSampleNum + sampleSize;
                CacheXBufToPlotBuf(XWrapBuf, _globalSparseRatio, readOffSet);
                CacheYBufToPlotBuf(YWrapBufs, _globalSparseRatio, readOffSet);
                CacheXDataToBuf(xData, _globalSparseRatio);
                CacheYDataToBuf(yData, _globalSparseRatio);
                BindDataToChart();
            }
            FillDataToWrapBuf(xData, yData, sampleSize);
        }

        public override void RefreshPlot(int sampleSize)
        {
            throw new NotImplementedException();
        }

        private void RemoveExtraPointsInChart(int sampleSize)
        {
            // 点数不够最大长度时无需删除多余的点
            int pointsToRemove = XWrapBuf.DataSize + sampleSize - Plotter.MaxSampleNum;
            if (pointsToRemove <= 0)
            {
                return;
            }
            int lastRemovePointIndex = (pointsToRemove/_globalSparseRatio) - 1;
            foreach (Series series in PlotSeries)
            {
                int removePointIndex = lastRemovePointIndex;
                while (removePointIndex >= 0)
                {
                    series.Points.RemoveAt(removePointIndex--);
                }
            }
        }

        private void SparsePointsInChart(int newSparseRatio, int sampleSize)
        {
            // 如果筛选比没有变化则无需筛点
            while (newSparseRatio > _globalSparseRatio)
            {
                int pointNum = PlotSeries[0].Points.Count;
                //保留最后一个点，依次删除点
                int lastRemoveIndex = pointNum - 2;
                foreach (Series series in PlotSeries)
                {
                    int removeIndex = lastRemoveIndex;
                    //始终将第一个点保留下来
                    while (removeIndex >= 0)
                    {
                        series.Points.RemoveAt(removeIndex);
                        removeIndex -= 2;
                    }
                }
                _globalSparseRatio *= 2;
            }
            if (newSparseRatio > 1 && PlotSeries[0].Points.Count > 0)
            {
                //配置第一个点为第一个数据
                int firstPointIndex = XWrapBuf.DataSize - (Plotter.MaxSampleNum - sampleSize);
                if (firstPointIndex < 0)
                {
                    firstPointIndex = 0;
                }
                for (int i = 0; i < PlotSeries.Count; i++)
                {
                    PlotSeries[i].Points[0].AxisLabel = XWrapBuf.Get(firstPointIndex);
                    PlotSeries[i].Points[0].SetValueY(YWrapBufs[i].Get(firstPointIndex));
                }
            }
        }

        public void Dispose()
        {
            XWrapBuf = null;
            YWrapBufs = null;
        }

    }
}
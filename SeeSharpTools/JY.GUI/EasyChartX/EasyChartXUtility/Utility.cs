using System;

namespace SeeSharpTools.JY.GUI.EasyChartXUtility
{
    internal static class Utility
    {
        public static void RoundYRange(ref double maxYValue, ref double minYValue, bool isLogarithmic = false)
        {
            if (double.IsNaN(maxYValue) || double.IsNaN(minYValue))
            {
                maxYValue = double.NaN;
                minYValue = double.NaN;
                return;
            }
            if (!isLogarithmic || minYValue <= 0)
            {
                GetLinearRoundRange(ref maxYValue, ref minYValue);
            }
            else
            {
                GetLogarithmRoundRange(ref maxYValue, ref minYValue);
            }
        }
        
        private static void GetLinearRoundRange(ref double maxYValue, ref double minYValue)
        {
            double range = (maxYValue - minYValue);
            double expandRange = range*Constants.YAutoExpandRatio;

            // range小于最小范围
            if (range <= Constants.MinDoubleValue)
            {
                maxYValue = maxYValue + 1;
                minYValue = minYValue - 1;
            }
            else
            {
                double roundSegment = Math.Pow(10, Math.Floor(Math.Log10(range) - Constants.RangeRoundOffset));
                // 如果最大值在0和-1 * expandRange之间，配置最大值为0
                if (maxYValue <= Constants.MinDoubleValue && maxYValue >= -1*expandRange)
                {
                    maxYValue = 0;
                    minYValue = FloorRound(minYValue - expandRange, Constants.YMajorGridCount*roundSegment);
                }
                // 如果最小值在1 * expandRange和0之间，配置最小值为0
                else if (minYValue <= expandRange && minYValue >= -1*Constants.MinDoubleValue)
                {
                    maxYValue = CeilRound(maxYValue + expandRange, Constants.YMajorGridCount*roundSegment);
                    minYValue = 0;
                }
                else if (maxYValue > Constants.MinDoubleValue && minYValue < -1*Constants.MinDoubleValue)
                {
                    int maxRatio = (int) Math.Round((maxYValue/range)*Constants.YMajorGridCount);
                    if (0 >= maxRatio)
                    {
                        maxRatio = 1;
                    }
                    else if (Constants.YMajorGridCount <= maxRatio)
                    {
                        maxRatio = 5;
                    }
                    double maxSingleIntervalSize = CeilRound((maxYValue + expandRange)/maxRatio, roundSegment);
                    double minSingleIntervalSize = CeilRound((expandRange - minYValue)/(Constants.YMajorGridCount - maxRatio),
                        roundSegment);
                    double internvalSize = maxSingleIntervalSize > minSingleIntervalSize
                        ? maxSingleIntervalSize
                        : minSingleIntervalSize;
                    maxYValue = maxRatio*internvalSize;
                    minYValue = (maxRatio - Constants.YMajorGridCount)*internvalSize;
                }
                else
                {
                    double midRoundValue = CeilRound((maxYValue + minYValue)/2, roundSegment);
                    double roundRange = CeilRound(range/2 + expandRange, roundSegment*Constants.YMajorGridCount/2);

                    maxYValue = midRoundValue + roundRange;
                    minYValue = midRoundValue - roundRange;
                }
            }
        }

        private static void GetLogarithmRoundRange(ref double maxYValue, ref double minYValue)
        {
            maxYValue = Math.Log10(maxYValue);
            minYValue = Math.Log10(minYValue);

            double range = (maxYValue - minYValue);
            double expandRange = range * Constants.YAutoExpandRatio;

            // range小于最小范围
            if (range <= Constants.MinDoubleValue)
            {
                maxYValue = maxYValue + 1;
                minYValue = minYValue - 1;
            }
            else
            {
                maxYValue += expandRange;
                minYValue -= expandRange;
            }

            maxYValue = Math.Pow(10, maxYValue);
            minYValue = Math.Pow(10, minYValue);
        }


        // 获取range对应的取整对齐的数字
        private static double CeilRound(double data, double roundSegment)
        {
            return Math.Ceiling(data/roundSegment)*roundSegment;
        }

        private static double FloorRound(double data, double roundSegment)
        {
            return Math.Floor(data / roundSegment) * roundSegment;
        }

    }
}
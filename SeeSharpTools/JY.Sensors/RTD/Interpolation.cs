using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpTools.JY.Sensors
{
    internal static class Interpolation
    {
        /// <summary>
        /// 在单调递增的数组中，搜索目标值y所在的区间，返回这个区间的低值所对应得索引值x
        /// </summary>
        /// <param name="table">被查找的单调递增的数组</param>
        /// <param name="y">被查找的目标数值y</param>
        /// <returns>目标区间的低值的索引值x</returns>
        public static int FindIntervalLocation(int[] table, int y)
        {
            int lowIndex = 0;
            int highIndex = table.Length - 1;
            int middleIndex = (lowIndex + highIndex) / 2;

            int targetIndex = -1;

            //二分法找目标值所在的区间
            if (y <= table[lowIndex])
            {
                targetIndex = 0;
            }
            else if (y <= table[highIndex])
            {
                while ((highIndex - lowIndex) != 1)
                {
                    middleIndex = (lowIndex + highIndex) / 2;
                    if (y > table[middleIndex])
                    {
                        lowIndex = middleIndex;
                    }
                    else if (y < table[middleIndex])
                    {
                        highIndex = middleIndex;
                    }
                    else
                    {
                        lowIndex = middleIndex;
                        break;
                    }
                }
                targetIndex = lowIndex;
            }
            else
            {
                targetIndex = highIndex;
            }
            return targetIndex;
        }

        /// <summary>
        /// 在单调递增的数组中，搜索目标值y所在的区间，返回这个区间的低值所对应得索引值x
        /// </summary>
        /// <param name="table">被查找的单调递增的数组</param>
        /// <param name="y">被查找的目标数值y</param>
        /// <returns>目标区间的低值的索引值x</returns>
        public static int FindIntervalLocation(double[] table, double y)
        {
            int lowIndex = 0;
            int highIndex = table.Length - 1;
            int middleIndex = (lowIndex + highIndex) / 2;

            int targetIndex = -1;

            //二分法找目标值所在的区间
            if (y <= table[lowIndex])
            {
                targetIndex = 0;
            }
            else if (y <= table[highIndex])
            {
                while ((highIndex - lowIndex) != 1)
                {
                    middleIndex = (lowIndex + highIndex) / 2;
                    if (y > table[middleIndex])
                    {
                        lowIndex = middleIndex;
                    }
                    else if (y < table[middleIndex])
                    {
                        highIndex = middleIndex;
                    }
                    else
                    {
                        lowIndex = middleIndex;
                        break;
                    }
                }
                targetIndex = lowIndex;
            }
            else
            {
                targetIndex = highIndex;
            }
            return targetIndex;
        }


        /// <summary>
        /// 通过关于x的单调递增函数在有限个点处的取值y的情况进行分段插值，根据y反求x。
        /// </summary>
        /// <param name="table">单调递增函数的取值情况，数组的索引值为x的序号，数组的值为y</param>
        /// <param name="xIncreament">x的递增间隔</param>
        /// <param name="xOffset">x的起始值，即x0得值</param>
        /// <param name="y">要查找得y值</param>
        /// <returns>根据传入的y反求解出来的x</returns>
        public static double LinearInterpolation1D(int[] table, double xIncreament, double xOffset, int y)
        {
            int targetIndex = FindIntervalLocation(table, y);

            //基础值：即所在区间的低索引对应的值
            double baseValue;
            baseValue = xIncreament * targetIndex;

            //偏移值：在区间内进行线性插值
            double interpolatedValue;
            if (targetIndex == table.Length - 1) //如果已经到了Table的结尾，不再插值
            {
                interpolatedValue = 0;
            }
            else
            {
                //将xIncreament除以这个区间所对应的y的长度,再乘以目标y值与区间低y值的插值
                interpolatedValue = (xIncreament / (table[targetIndex + 1] - table[targetIndex])) * (y - table[targetIndex]);
            }

            return baseValue + interpolatedValue + xOffset;
        }

        /// <summary>
        /// 通过关于x的单调递增函数在有限个点处的取值y的情况进行分段插值，根据y反求x。
        /// </summary>
        /// <param name="table">单调递增函数的取值情况，数组的索引值为x的序号，数组的值为y</param>
        /// <param name="xIncreament">x的递增间隔</param>
        /// <param name="xOffset">x的起始值，即x0得值</param>
        /// <param name="y">要查找得y值</param>
        /// <returns>根据传入的y反求解出来的x</returns>
        public static double LinearInterpolation1D(double[] table, double xIncreament, double xOffset, double y)
        {
            int targetIndex = FindIntervalLocation(table, y);

            //基础值：即所在区间的低索引对应的值
            double baseValue;
            baseValue = xIncreament * targetIndex;

            //偏移值：在区间内进行线性插值
            double interpolatedValue;
            if (targetIndex == table.Length - 1) //如果已经到了Table的结尾，不再插值
            {
                interpolatedValue = 0;
            }
            else
            {
                //将xIncreament除以这个区间所对应的y的长度,再乘以目标y值与区间低y值的插值
                interpolatedValue = (xIncreament / (table[targetIndex + 1] - table[targetIndex])) * (y - table[targetIndex]);
            }

            return baseValue + interpolatedValue + xOffset;
        }
    }
}

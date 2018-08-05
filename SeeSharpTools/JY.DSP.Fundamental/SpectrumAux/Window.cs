using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    internal static class Window
    {

        /// <summary>
        /// 内部使用，以WindowType为索引的调整系数Coherent Gain
        /// </summary>
        public static readonly double[] WindowCGFactor = new double[]
        {
            1.0, //矩形窗（无窗）
            0.5, //Hanning
            0.54, //hamming
            0.42323, //Blackman_Harris
            0.42659, //Exact_Blackman
            0.42, //Blackman
            0.22, //Flat_Top
            0.35875, //Four_Term_B_Harris
            0.27105 //Seven_Term_B_Harris
        };

        /// <summary>
        /// 以WindowType为索引的ENBW值
        /// </summary>
        public static readonly double[] WindowENBWFactor = new double[]
        {
            1.0, //矩形窗（无窗）
            1.5, //Hanning
            1.36283, //hamming
            1.708538, //Blackman_Harris
            1.69369, //Exact_Blackman
            1.72676, //Blackman
            3.77, //Flat_Top
            2.00435, //Four_Term_B_Harris
            2.63191 //Seven_Term_B_Harris
        };

        public static void GetWindow(WindowType windowType, ref double[] windowdata,
                                     out double CG, out double ENBW)
        {
            if (windowdata == null || windowdata.Length <= 0)
            {
                throw new JYDSPUserBufferException("windowdata length is null!");
            }
            int size = windowdata.Length;
            double[] windowTmp = null;
            for (int i = 0; i < size; i++)
            {
                windowdata[i] = i;
            }
            var pi2DSize = Math.PI * 2 / size;
            var pi4DSize = pi2DSize * 2.0;

            CG = 1;
            ENBW = 0;
            //根据windowType:窗函数类型产生单位窗函数
            //CG:相干增益 ENBW:等效噪声宽度 
            switch (windowType)
            {
                case WindowType.Hanning://汉宁窗
                    //for (i = 0; i < size; i++)//产生单位窗函数
                    //{
                    //    windowdata[i] = 0.5 * (1 - cos(2 * M_PI * i / size));
                    //}
                    //汉宁窗公式:W[i]=1/2*[1-cos(2 * M_PI * i / size)] = 0.5 - 0.5 * cos(2 * M_PI * i / size);
                    CBLASNative.cblas_dscal(size, pi2DSize, windowdata, 1); //2 * M_PI * i / size
                    VMLNative.vdCos(size, windowdata, windowdata);          //cos(2 * M_PI * i / size)
                    CBLASNative.cblas_dscal(size, -0.5, windowdata, 1);     //-0.5 * cos(2 * M_PI * i / size)
                    for (int i = 0; i < size; i++)                          //0.5 - 0.5 * cos(2 * M_PI * i / size);
                    {
                        windowdata[i] += 0.5;
                    }
                    CG = 0.5;//汉宁窗的相干增益:0.5
                    ENBW = 1.5;//汉宁窗的等效噪声宽度;1.5
                    break;
                case WindowType.Hamming://海明窗
                    //for (int i = 0; i < size; i++)//产生单位窗函数
                    //{
                    //    windowdata[i] = 0.54 - 0.46 * cos(2 * M_PI * i / size);//海明窗公式:W[i]=0.54 -0.46cos(2iπ/N);
                    //}
                    //0.54 - 0.46 * cos(2 * M_PI * i / size)
                    CBLASNative.cblas_dscal(size, pi2DSize, windowdata, 1); //2 * M_PI * i / size
                    VMLNative.vdCos(size, windowdata, windowdata);          //cos(2 * M_PI * i / size)
                    CBLASNative.cblas_dscal(size, -0.46, windowdata, 1);     //-0.46 * cos(2 * M_PI * i / size)
                    for (int i = 0; i < size; i++)                          //0.54 - 0.46 * cos(2 * M_PI * i / size);
                    {
                        windowdata[i] += 0.54;
                    }

                    CG = 0.54;//海明窗的相干增益:0.54
                    ENBW = 1.36283;//海明窗的等效噪声宽度:1.36283
                    break;
                case WindowType.Blackman_Harris:// Blackman_Harris窗
                    //for (i = 0; i < size; i++)//产生单位窗函数
                    //{
                    //    windowdata[i] = 0.42323 - 0.49755 * cos(2 * M_PI * i / size) + 0.07922 * cos(4 * M_PI * i / size);// Blackman_Harris窗公式:W[i]=0.42323-0.49755cos⁡2iπ/N+0.07922cos4iπ/N;
                    //}
                    CBLASNative.cblas_dscal(size, pi2DSize, windowdata, 1); //2 * M_PI * i / size
                    VMLNative.vdCos(size, windowdata, windowdata);          //cos(2 * M_PI * i / size)
                    CBLASNative.cblas_dscal(size, -0.49755, windowdata, 1);     //-0.49755 * cos(2 * M_PI * i / size)
                    windowTmp = new double[size];
                    for (int i = 0; i < size; i++)
                    {
                        windowdata[i] += 0.42323;
                        windowTmp[i] = i;
                    }

                    CBLASNative.cblas_dscal(size, pi4DSize, windowTmp, 1); //4 * M_PI * i / size
                    VMLNative.vdCos(size, windowTmp, windowTmp);          //cos(4 * M_PI * i / size)
                    CBLASNative.cblas_dscal(size, 0.07922, windowTmp, 1);     //0.07922 * cos(4 * M_PI * i / size)
                    VMLNative.vdAdd(size, windowTmp, windowdata, windowdata); //0.42323 - 0.49755 * Math.Cos(2 * Math.PI * i / size) + 0.07922 * Math.Cos(4 * Math.PI * i / size);

                    CG = 0.42323;// Blackman_Harris窗的相干增益:0.42323
                    ENBW = 1.708538;// Blackman_Harris窗的等效噪声宽度:1.708538
                    break;
                case WindowType.Exact_Blackman://Exact Blackman窗
                    //for (i = 0; i < size; i++)//产生单位窗函数
                    //{
                    //    windowdata[i] = 0.42659 - 0.49656 * cos(2 * M_PI * i / size) + 0.07684 * cos(4 * M_PI * i / size);//Exact Blackman窗公式:W[i]=7938/18608-(9240/18608)*cos⁡2iπ/N+(1430/18608)*cos⁡4iπ/N;
                    //}
                    CBLASNative.cblas_dscal(size, pi2DSize, windowdata, 1); //2 * M_PI * i / size
                    VMLNative.vdCos(size, windowdata, windowdata);          //cos(2 * M_PI * i / size)
                    CBLASNative.cblas_dscal(size, -0.49656, windowdata, 1);     //-0.49656 * cos(2 * M_PI * i / size)
                    windowTmp = new double[size];
                    for (int i = 0; i < size; i++)                          //0.42659 - 0.49656 * cos(2 * M_PI * i / size);
                    {
                        windowdata[i] += 0.42659;
                        windowTmp[i] = i;
                    }

                    CBLASNative.cblas_dscal(size, pi4DSize, windowTmp, 1); //4 * M_PI * i / size
                    VMLNative.vdCos(size, windowTmp, windowTmp);          //cos(4 * M_PI * i / size)
                    CBLASNative.cblas_dscal(size, 0.07684, windowTmp, 1);     //0.07684 * cos(4 * M_PI * i / size)
                    VMLNative.vdAdd(size, windowTmp, windowdata, windowdata); //0.42659 - 0.49656 * Math.Cos(2 * Math.PI * i / size) + 0.07684 * Math.Cos(4 * Math.PI * i / size);

                    CG = 0.42659; //Exact Blackman窗的相干增益:0.42659
                    ENBW = 1.69369; //Exact Blackman窗的等效噪声宽度:1.69369
                    break;
                case WindowType.Blackman://Blackman窗
                    //for (i = 0; i < size; i++)//产生单位窗函数
                    //{
                    //    windowdata[i] = 0.42 - 0.5 * cos(2 * M_PI * i / size) + 0.08 * cos(4 * M_PI * i / size);//Blackman窗公式:W[i]=0.42-0.5cos2iπ/N+0.08cos4iπ/N;
                    //}
                    CBLASNative.cblas_dscal(size, pi2DSize, windowdata, 1); //2 * M_PI * i / size
                    VMLNative.vdCos(size, windowdata, windowdata);          //cos(2 * M_PI * i / size)
                    CBLASNative.cblas_dscal(size, -0.5, windowdata, 1);     //-0.5 * cos(2 * M_PI * i / size)
                    windowTmp = new double[size];
                    for (int i = 0; i < size; i++)                          //0.42 - 0.5 * cos(2 * M_PI * i / size);
                    {
                        windowdata[i] += 0.42;
                        windowTmp[i] = i;
                    }

                    CBLASNative.cblas_dscal(size, pi4DSize, windowTmp, 1); //4 * M_PI * i / size
                    VMLNative.vdCos(size, windowTmp, windowTmp);          //cos(4 * M_PI * i / size)
                    CBLASNative.cblas_dscal(size, 0.08, windowTmp, 1);     //0.08 * cos(4 * M_PI * i / size)
                    VMLNative.vdAdd(size, windowTmp, windowdata, windowdata); //0.42 - 0.5 * Math.Cos(2 * Math.PI * i / size) + 0.08 * Math.Cos(4 * Math.PI * i / size);

                    CG = 0.42;//Blackman窗的相干增益:0.42
                    ENBW = 1.72676;//Blackman窗的等效噪声宽度:1.72676
                    break;
                case WindowType.Flat_Top://平顶窗
                    for (int i = 0; i < size; i++)//产生单位窗函数
                    {
                        windowdata[i] = 0.21558 - 0.41663 * Math.Cos(2 * Math.PI * i / size)
                                                + 0.27726 * Math.Cos(4 * Math.PI * i / size)
                                                - 0.08358 * Math.Cos(6 * Math.PI * i / size)
                                                + 0.00695 * Math.Cos(8 * Math.PI * i / size);
                        //平顶窗的公式:W(i)=0.21557895-0.41663158cos⁡2iπ/N+0.277263158cos⁡4iπ/N-0.083578947cos⁡6iπ/N+0.006947368cos⁡8iπ/N;
                    }

                    CG = 0.22; //平顶窗的相干增益:0.22
                    ENBW = 3.77; //平顶窗的等效噪声宽度:3.77
                    break;
                case WindowType.Four_Term_B_Harris://4 Term B-Harris窗

                    for (int i = 0; i < size; i++)//产生单位窗函数
                    {
                        windowdata[i] = 0.35875 - 0.48829 * Math.Cos(2 * Math.PI * i / size)
                                                + 0.14128 * Math.Cos(4 * Math.PI * i / size)
                                                - 0.01168 * Math.Cos(6 * Math.PI * i / size);
                        //4 Term B-Harris窗的公式:W(i)=0.35875-0.48829cos⁡2iπ/N+0.14128cos⁡4iπ/N-0.01168cos⁡6iπ/N;
                    }
                    CG = 0.35875;//4 Term B-Harris窗的相干增益:0.35875
                    ENBW = 2.00435;//4 Term B-Harris窗的等效噪声宽度:2.00435
                    break;

                //7 Term B-Harris窗
                case WindowType.Seven_Term_B_Harris:
                    for (int i = 0; i < size; i++)//产生单位窗函数
                    {
                        windowdata[i] = 0.27105 - 0.43329793923448 * Math.Cos(1 * 2 * Math.PI * i / size)
                                                         + 0.21812299954311 * Math.Cos(2 * 2 * Math.PI * i / size)
                                                         - 0.06592544638803 * Math.Cos(3 * 2 * Math.PI * i / size)
                                                         + 0.01081174209837 * Math.Cos(4 * 2 * Math.PI * i / size)
                                                         - 0.00077658482522 * Math.Cos(5 * 2 * Math.PI * i / size)
                                                         + 0.00001388721735 * Math.Cos(6 * 2 * Math.PI * i / size);
                    }
                    CG = 0.27105;//7 Term B-Harris窗的相干增益:0.27105140069
                    ENBW = 2.63191;//7 Term B-Harris窗的等效噪声宽度:2.631905
                    break;

                //      //Low_sidelobe窗
                //case Low_sidelobe://无
                //	      //break;

                //other 默认矩形窗
                //矩形窗
                case WindowType.None:
                    for (int i = 0; i < size; i++)//产生单位窗函数
                    {
                        windowdata[i] = 1.0;
                    }

                    CG = 1.0; //矩形窗的相干增益:1.0
                    ENBW = 1.0;//矩形窗的等效噪声宽度:1.0
                    break;
                default:
                    throw new JYDSPParamException("Window type is out of range!");
            }
        }
    }
}

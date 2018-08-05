using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JY.DSP.Fundamental
{
    internal class DFTIDescMgr:IDisposable
    {
        /// <summary>
        /// 获取DFTIDescMgr的实体
        /// </summary>
        /// <returns></returns>
        public static DFTIDescMgr GetInstance()
        {
            return _instance ?? (_instance = new DFTIDescMgr());
        }

        private static DFTIDescMgr _instance;

        /// <summary>
        /// DFT desc Dictionary
        /// </summary>
        private readonly Dictionary<ulong, IntPtr> _dftDescDict;
        private DFTIDescMgr()
        {
            _dftDescDict = new Dictionary<ulong, IntPtr>();
        }

        /// <summary>
        /// Get DFTKey
        /// </summary>
        /// <param name="fftSampleCount">fft运算的点数</param>
        /// <param name="type">fft类型</param>
        /// <returns></returns>
        public IntPtr GetDFTDesc(int fftSampleCount, DFTType type)
        {
            var k = new DFTKey()
            {
                FFTSampleCount = (uint)fftSampleCount,
                Type = type
            };

            var keyId = k.GetKeyNum();

            lock (_dftDescDict)
            {
                //存在该Key则直接返回
                if (_dftDescDict.ContainsKey(keyId))
                {
                    return _dftDescDict[keyId];
                }

                var desc = IntPtr.Zero;
                int precision, forwardDomain, dimension, length = fftSampleCount, ret;
                //不存在，则创建并配置，保存后返回
                #region ------------根据不同变换类型创建描述符，并分别配置（如果有）-------------
                switch (type)
                {
                    case DFTType.Double1DRealInComplexOut:
                        precision = DFTI.DOUBLE;
                        forwardDomain = DFTI.REAL;
                        dimension = 1;
                        if (0 != (ret = DFTI.DftiCreateDescriptor(ref desc, precision, forwardDomain,
                                                        dimension, length)))
                        {
                            throw new JYDSPInnerException("DftiCreateDescriptor Failed, error code = " + ret);
                        }

                        if (0 != (ret = DFTI.DftiSetValue(desc, DFTI.PACKED_FORMAT, DFTI.CCS_FORMAT)))
                        {
                            throw new JYDSPInnerException("DftiSetValue PACKED_FORMAT Failed, error code = " + ret);
                        }
                        break;
                    case DFTType.Double1DComplexInComplexOut:
                        precision = DFTI.DOUBLE;
                        forwardDomain = DFTI.COMPLEX;
                        dimension = 1;
                        if (0 != (ret = DFTI.DftiCreateDescriptor(ref desc, precision, forwardDomain,
                                                        dimension, length)))
                        {
                            throw new JYDSPInnerException("DftiCreateDescriptor Failed, error code = " + ret);
                        }
                        break;
                    case DFTType.Single1DRealInComplexOut:
                        precision = DFTI.SINGLE;
                        forwardDomain = DFTI.REAL;
                        dimension = 1;
                        if (0 != (ret = DFTI.DftiCreateDescriptor(ref desc, precision, forwardDomain,
                                                        dimension, length)))
                        {
                            throw new JYDSPInnerException("DftiCreateDescriptor Failed, error code = " + ret);
                        }

                        if (0 != (ret = DFTI.DftiSetValue(desc, DFTI.PACKED_FORMAT, DFTI.CCS_FORMAT)))
                        {
                            throw new JYDSPInnerException("DftiSetValue PACKED_FORMAT Failed, error code = " + ret);
                        }
                        break;
                    case DFTType.Single1DComplexInComplexOut:
                        precision = DFTI.SINGLE;
                        forwardDomain = DFTI.COMPLEX;
                        dimension = 1;
                        if (0 != (ret = DFTI.DftiCreateDescriptor(ref desc, precision, forwardDomain,
                                                        dimension, length)))
                        {
                            throw new JYDSPInnerException("DftiCreateDescriptor Failed, error code = " + ret);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
                #endregion

                #region ------------------公共设置------------------
                var scaleFactor = 1.0 / length;
                //配置IFFT的ScaleFactor
                if (0 != (ret = DFTI.DftiSetValue(desc, DFTI.BACKWARD_SCALE, scaleFactor)))
                {
                    throw new JYDSPInnerException("DftiSetValue Failed, error code = " + ret);
                }

                //配置DFTI_PLACEMENT为DFTI_NOT_INPLACE,即FFT后的结果不覆盖原输入信号
                if (0 != (ret = DFTI.DftiSetValue(desc, DFTI.PLACEMENT, DFTI.NOT_INPLACE)))
                {
                    throw new JYDSPInnerException("DftiSetValue PLACEMENT Failed, error code = " + ret);
                }

                if (0 != (ret = DFTI.DftiCommitDescriptor(desc)))
                {
                    throw new JYDSPInnerException("DftiCommitDescriptor Failed, error code = " + ret);
                }
                #endregion

                //保存描述符并返回
                _dftDescDict.Add(keyId, desc);
                return desc;

            }
        }

        #region ----------内部使用的方法----------

        /// <summary>
        /// Contains DFTKey
        /// </summary>
        /// <param name="fftSampleCount">fft运算的点数</param>
        /// <param name="type">fft类型</param>
        /// <param name="keyId">返回的keyId</param>
        /// <returns></returns>
        private bool Contains(uint fftSampleCount, DFTType type, ref ulong keyId)
        {
            var k = new DFTKey()
            {
                FFTSampleCount = fftSampleCount,
                Type = type
            };
            keyId = k.GetKeyNum();
            return _dftDescDict.ContainsKey(keyId);
        }

        /// <summary>
        /// Contains DFTKey
        /// </summary>
        /// <param name="fftSampleCount">fft运算的点数</param>
        /// <param name="type">fft类型</param>
        /// <returns></returns>
        private bool Contains(uint fftSampleCount, DFTType type)
        {
            var k = new DFTKey()
            {
                FFTSampleCount = fftSampleCount,
                Type = type
            };
            ulong keyId = k.GetKeyNum();
            return _dftDescDict.ContainsKey(keyId);
        }

        /// <summary>
        /// 添加DFT Desc
        /// </summary>
        /// <param name="fftSampleCount"></param>
        /// <param name="type"></param>
        /// <param name="desc"></param>
        private void AddDFTDesc(uint fftSampleCount, DFTType type, IntPtr desc)
        {
            ulong keyId = 0;
            if(!Contains(fftSampleCount, type, ref keyId))
            {
                lock (_dftDescDict)
                {
                    _dftDescDict.Add(keyId, desc);
                }
            }
            else
            {
                throw new JYDSPInnerException("Descriptor have been exist.");
            }
        }

        /// <summary>
        /// 添加DFT Desc
        /// </summary>
        /// <param name="keyId">keyId, 由Contains返回</param>
        /// <param name="desc">描述符</param>
        private void AddDFTDesc(ulong keyId, IntPtr desc)
        {
            if (!_dftDescDict.ContainsKey(keyId))
            {
                lock (_dftDescDict)
                {
                    _dftDescDict.Add(keyId, desc);
                }
            }
            else
            {
                throw new JYDSPInnerException("Descriptor have been exist.");
            }
        }

        public void Dispose()
        {
            int ret;
            foreach (var desc in _dftDescDict)
            {
                var descs = desc.Value;
                if (0 != (ret = DFTI.DftiFreeDescriptor(ref descs)))
                {
                    throw new JYDSPInnerException("DftiFreeDescriptor Failed, error code = " + ret);
                }
            }
            _dftDescDict.Clear();
        }

        ~DFTIDescMgr()
        {
            Dispose();
        }
        #endregion

    }

    #region  ---------内部结构、枚举定义----------

    /// <summary>
    /// DFT Desc's Key
    /// </summary>
    internal class DFTKey
    {
        public DFTKey(uint fftSampleCount, DFTType type)
        {
            FFTSampleCount = fftSampleCount;
            Type = type;
        }

        public DFTKey()
        {
            FFTSampleCount = 0;
            Type = DFTType.Double1DRealInComplexOut;
        }

        /// <summary>
        /// FFT点数
        /// </summary>
        public uint FFTSampleCount { get; set; }

        /// <summary>
        /// FFT类型
        /// </summary>
        public DFTType Type { get; set; }        

        /// <summary>
        /// 获取Key的值
        /// [7][6][5][4]  [3][2][1][0]
        /// fftThreadID(4bytes) FFT SampleCount(28bit) FFT Type 4bit
        /// </summary>
        /// <returns></returns>
        public ulong GetKeyNum()
        {
            ulong keyId = 0;
            ulong fftSampleCount = FFTSampleCount;
            keyId += fftSampleCount << 32; // [7][6][5][4]            
            keyId += (ulong)Type;  //[3][2][1][0]
            return keyId;
        }
    }

    /// <summary>
    /// DFT Type
    /// </summary>
    internal enum DFTType:uint
    {
        /// <summary>
        /// 双精度类型的实数输入复数输出的FFT,或IFFT
        /// </summary>
        Double1DRealInComplexOut = 1,

        /// <summary>
        /// //双精度类型的复数输入复数输出的FFT,或IFFT
        /// </summary>
        Double1DComplexInComplexOut = 2,

        /// <summary>
        /// 双精度类型的实数输入复数输出的FFT,或IFFT
        /// </summary>
        Single1DRealInComplexOut = 3,

        /// <summary>
        /// //双精度类型的复数输入复数输出的FFT,或IFFT
        /// </summary>
        Single1DComplexInComplexOut = 4
    }

    #endregion
}

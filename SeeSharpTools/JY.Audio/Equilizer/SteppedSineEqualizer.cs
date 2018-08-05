using System;

namespace SeeSharpTools.JY.Audio.Equilizer
{
    public class SteppedSineEqualizer : EqualizerBase
    {
        private readonly ManagedAudioLibrary.SteppedSineEqualizer _equalizerInst;

        public double TargetAmplitude { protected set; get; }

        public SteppedSineEqualizer(double[] equalizerResponse, double targetAmplitude)
        {
            TargetAmplitude = targetAmplitude;
            _equalizerInst = new ManagedAudioLibrary.SteppedSineEqualizer();
            _equalizerInst.CreateEQ(equalizerResponse, (uint) equalizerResponse.Length, targetAmplitude);
            RawEqualizer = _equalizerInst;
        }

        public override IntPtr GetNativePtr()
        {
            return _equalizerInst?.GetNativePtr() ?? IntPtr.Zero;
        }
        /// <summary>
        /// 获取均衡器总点数
        /// </summary>
        /// <returns>均衡器总点数</returns>
        public override uint GetPointsOfEQ()
        {
            return _equalizerInst?.GetPointsOfEQ() ?? 0;
        }

        /// <summary>
        /// 获取对应索引值的均衡器值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>索引值对应均衡器值</returns>
        public override double GetEQ(ushort index)
        {
            return null != _equalizerInst && index < GetPointsOfEQ() ? 
                _equalizerInst.GetEQ(index) : double.NaN;
        }

        public override void Dispose()
        {
            _equalizerInst?.Dispose();
        }
    }
}
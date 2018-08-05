using System;

namespace SeeSharpTools.JY.Audio.Equilizer
{
    public abstract class EqualizerBase : IDisposable
    {
        protected object RawEqualizer;

        public object GetRawEqualizer()
        {
            return RawEqualizer;
        }

        public abstract IntPtr GetNativePtr();
        public abstract uint GetPointsOfEQ();
        public abstract double GetEQ(ushort index);
        public abstract void Dispose();
    }
}
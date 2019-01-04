using SeeSharpTools.JY.Mathematics.Provider;
using System;

namespace SeeSharpTools.JY.Mathematics
{
    /// <summary>
    /// 运行引擎（全局变量）
    /// </summary>
    public class Engine
    {
        private static ProviderEngine _providerType = ProviderEngine.CSharp;

        internal static ProviderBase Base { get; private set; } = new ProviderBase();

        /// <summary>
        /// 选择运行引擎的类型
        /// </summary>
        public static ProviderEngine Provider
        {
            get { return _providerType; }
            set
            {
                switch (value)
                {
                    case ProviderEngine.CSharp:
                        Base = TryUseCSharp();
                        break;

                    case ProviderEngine.IntelIPP:
                        Base = TryUseIPP();
                        break;

                    case ProviderEngine.IntelMKL:
                        Base = TryUseMKL();
                        break;

                    default:
                        break;
                }
                _providerType = value;
            }
        }

        private static ProviderBase TryUseCSharp()
        {
            try
            {
                return new ProviderBase();
            }
            catch (Exception ex)
            {
                return new ProviderBase();
            }
        }

        private static ProviderBase TryUseMKL()
        {
            try
            {
                return new ProvideMKL();
            }
            catch (Exception ex)
            {
                return new ProviderBase();
            }
        }

        private static ProviderBase TryUseIPP()
        {
            try
            {
                return new ProviderIPP();
            }
            catch (Exception ex)
            {
                return new ProviderBase();
            }
        }
    }

    /// <summary>
    /// 运行引擎选项
    /// </summary>
    public enum ProviderEngine
    {
        /// <summary>
        /// C# 类库（默认）
        /// </summary>
        CSharp = 0,

        /// <summary>
        /// Intel IPP类库
        /// </summary>
        IntelIPP = 2,

        /// <summary>
        /// Intel MKL类库
        /// </summary>
        IntelMKL = 3,
    }
}
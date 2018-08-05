namespace SeeSharpTools.JY.Statistics
{
    /// <summary>
    /// Global variable for the library
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Select the calculating engine
        /// </summary>
        public static ProviderEngine Provider = ProviderEngine.MathNet;
    }

    /// <summary>
    /// Provider engine selection
    /// </summary>
    public enum ProviderEngine
    {
        /// <summary>
        /// MathNet
        /// </summary>
        MathNet = 0,

        /// <summary>
        /// Intel IPP, place Intel IPP dlls under \NativeDLLs\intel64 for x64 platform and \NativeDLLs\ia32 for x86 platform before use Intel IPP as the provider engine
        /// </summary>
        IntelIPP = 1,
    }
}
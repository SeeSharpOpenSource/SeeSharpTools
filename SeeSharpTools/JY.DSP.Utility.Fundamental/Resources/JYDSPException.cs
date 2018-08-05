using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JY.DSP.Utility.Fundamental
{
    /// <summary>
    /// JY DSP Inner Exception
    /// </summary>
    public class JYDSPInnerException:ApplicationException
    {
        public JYDSPInnerException(string msg):base(msg)
        {

        }
        public JYDSPInnerException()
        {

        }
    }

    /// <summary>
    /// JY DSP User Buffer Exception
    /// </summary>
    public class JYDSPUserBufferException : ApplicationException
    {
        public JYDSPUserBufferException(string msg) : base(msg)
        {

        }

        public JYDSPUserBufferException()
        {

        }

    }

    /// <summary>
    /// JY DSP Param Exception
    /// </summary>
    public class JYDSPParamException : ApplicationException
    {
        public JYDSPParamException(string msg) : base(msg)
        {

        }

        public JYDSPParamException()
        {

        }

    }
}

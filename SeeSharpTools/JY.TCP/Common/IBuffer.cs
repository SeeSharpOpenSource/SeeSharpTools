using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JY.TCP
{
    /// <summary>
    /// 缓存的接口定义
    /// </summary>
    public interface IBuffer
    {
        /// <summary>
        /// 返回缓存的大小
        /// </summary>
        int BufferSize { get; }
        /// <summary>
        /// 返回缓存内未读取的元素数目
        /// </summary>
        int NumOfElement { get; }
        
    }
}

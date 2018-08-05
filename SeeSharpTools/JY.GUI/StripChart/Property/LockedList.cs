using System.Collections.Generic;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// 用来屏蔽List的Add和Remove方法的包装类
    /// </summary>
    /// <typeparam name="TDataType"></typeparam>
    public class LockedList<TDataType> : List<TDataType>
    {
        public LockedList() : base()
        {
        }

        public LockedList(int size) : base(size)
        {
        }

        internal new void Add(TDataType data)
        {
            base.Add(data);
        }

        internal new void Remove(TDataType data)
        {
            base.Remove(data);
        }

        internal new void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }
    }
}
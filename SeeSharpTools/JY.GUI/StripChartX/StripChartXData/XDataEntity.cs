using System;
using System.Collections;
using System.Collections.Generic;

namespace SeeSharpTools.JY.GUI.StripChartXData
{
    internal class XDataEntity : IDisposable
    {
        public XDataType Type { get; }
        public int StartIndex { get; set; }
        public int CurrentIndex { get; private set; }

        public int Capacity { get; }

        private OverLapStrBuffer _strWrapBuffer = null;

        public XDataEntity(int startIndex, XDataType type, int capacity)
        {
            this.Type = type;
            if (XDataType.String == this.Type)
            {
                _strWrapBuffer = new OverLapStrBuffer(capacity);
            }
            this.Capacity = capacity;
            this.StartIndex = startIndex;
            this.CurrentIndex = this.StartIndex;
        }
        
        // 字符串模式使用
        public void Add(string item)
        {
            _strWrapBuffer.Add(item);
        }

        public void AddRange(IList<string> items, int length, int offset)
        {
            _strWrapBuffer.Add(items, length, offset);
        }

        // 索引模式使用
        public void Add(int count)
        {
            CurrentIndex += count;
        }

        public int Count
        {
            get
            {
                switch (Type)
                {
                    case XDataType.Index:
                        int count = CurrentIndex - StartIndex;
                        return Capacity > count ? count : Capacity;
                        break;
                    case XDataType.String:
                        return _strWrapBuffer.Count;
                        break;
                    default:
                        return -1;
                        break;
                }
            }
        }

        public bool IsReadOnly => true;

        public string this[int index]
        {
            get {
                switch (Type)
                {
                    case XDataType.Index:
                        return CurrentIndex++.ToString();
                        break;
                    case XDataType.String:
                        return _strWrapBuffer[index];
                        break;
                    default:
                        return string.Empty;
                }
            }
        }

        public void Dispose()
        {
            this._strWrapBuffer = null;
        }
    }
}
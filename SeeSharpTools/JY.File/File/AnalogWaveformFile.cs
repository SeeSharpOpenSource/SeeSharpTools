using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace SeeSharpTools.JY.File
{
    public class AnalogWaveformFile
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AnalogWaveformFile()
        {
            InitPrivateFileds();
        }

        /// <summary>
        /// 创建WaveformFile对象，指定文件路径
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="mode">文件操作类型</param>
        public AnalogWaveformFile(string filePath, FileOperation mode)
        {
            _fileOperation = mode;
            InitPrivateFileds();
            InitFileContent(filePath, mode);
        }        

        #region ----------私有字段----------  
        /// <summary>
        /// 格式信息长度
        /// </summary>
        private const int FORMATLENGH = 4096;

        /// <summary>
        /// 文件头长度
        /// </summary>
        private const int FILEHEAD = 65536;

        /// <summary>
        /// 文件操作模式
        /// </summary>
        private FileOperation _fileOperation;           

        /// <summary>
        /// FileStream对象，可操作字节或字节数组
        /// </summary>
        private FileStream _fileStream;

        /// <summary>
        /// 采样信息，作用是在将部分属性信息打包成类，方便后续写文件时的序列化操作
        /// </summary>
        private SampleInformation _sampleInformation;

        /// <summary>
        /// 记录已写入的文件头的长度
        /// </summary>
        private int _fileHeadLengh;

        /// <summary>
        /// 文件是否关闭
        /// </summary>
        private bool _fileClosed;

        /// <summary>
        /// 写数据指针，记录已写入的数据长度，单位为sample
        /// </summary>
        private long _writeIndex;

        /// <summary>
        /// 读数据指针，指定读取数据的起始位置，单位为sample
        /// </summary>
        private long _readIndex;

        /// <summary>
        /// 文件目录信息，记录各类信息的长度
        /// </summary>
        private FileMenu _fileMenu;

        /// <summary>
        /// 读文件指针是否在文件头
        /// </summary>
        private bool _readFileIndexInHead;

        /// <summary>
        /// 写文件指针是否在文件头
        /// </summary>
        private bool _writeFileIndexInHead;

        /// <summary>
        /// 记录是否写入过数据
        /// </summary>
        private bool _dataWrited;

        /// <summary>
        /// 写文件时使用的byte数组
        /// </summary>
        private byte[] _rawByteData;

        /// <summary>
        /// 读文件时使用的byte数组
        /// </summary>
        private byte[] _byteData;
        #endregion

        #region ----------公共属性----------
        /// <summary>
        /// 文件格式，Stream/Multi-Record
        /// </summary>
        public FileFormat FileFormat { get; set; }

        /// <summary>
        /// 数据格式，Int16/Int32/Float/Double
        /// </summary>
        private DataType _dataType;
        public DataType DataType
        {
            get
            {
                return _dataType;
            }
            set
            {
                //如果已经写入过数据，或者文件为只读模式，不允许再修改数据类型
                if ((_dataWrited == true && _dataType != value) || _fileOperation == FileOperation.OpenWithReadOnly)
                {
                    throw new WaveformFileException(ErrorCode.DataTypeSetError);
                }
                _dataType = value;
            }
        }

        /// <summary>
        /// 字节序，Little Endian/Big Endian
        /// </summary>
        public ByteOrder ByteOrder { get; set; }
       
        private ArchiveInformation _archiveInformation;
        /// <summary>
        /// 归档信息
        /// </summary>
        public ArchiveInformation ArchiveInformation
        {
            get
            {
                return _archiveInformation;
            }
            set
            {
                ArchiveInformation = value;
            }
        }

        /// <summary>
        /// 通道数
        /// </summary>
        public int NumberOfChannels { get; set; }

        /// <summary>
        /// 采样率，S/s
        /// </summary>
        public double SampleRate { get; set; }

        /// <summary>
        /// 数据长度，以Sample为单位
        /// </summary>
        public long DataLength { get; set; }

        /// <summary>
        /// 各通道采样信息，包括通道名称、采集量程、换算
        /// 因子、物理单位名称和备注
        /// </summary>
        public List<ChannelInfo> Channels { get; set; }

        /// <summary>
        /// 数据起始点的时间
        /// </summary>
        public DateTime DataStartTime { get; set; }

        private List<TimeLabel> _timeLabels;
        /// <summary>
        /// 时间戳标签序列，可定义一组标签来标识指定数据采
        /// 样点所在的时间，当文件格式为Stream时可用于对长
        /// 时间连续数据的时间修正，当文件格式为MultiRecord时用于标识各Record的起始点时间信息
        /// </summary>
        public List<TimeLabel> TimeLabels
        {
            get
            {
                return _timeLabels;
            }
            set
            {
                TimeLabels = value;
            }
        }

        private List<CustomLabel> _customLabels;
        /// <summary>
        /// 自定义标签序列，可定义一组标签来保存备注信息
        /// </summary>
        public List<CustomLabel> CustomLabels
        {
            get
            {
                return _customLabels;
            }
            set
            {
                CustomLabels = value;
            }
        }

        //高级设置（暂未实现）
        public bool EnableWriteBuffering { get; set; }
        public bool EnableFastStream { get; set; }

        #endregion

        #region ----------公共方法----------
        /// <summary>
        /// 创建新文件或打开文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="mode">文件操作类型</param>
        /// <remarks>如果构造函数中已指定路径，则打开操作只能在文件close之后进行</remarks>
        public void Open(string filePath, FileOperation mode)
        {
            if (_fileClosed == false)//文件未关闭，不允许打开操作
            {
                throw new WaveformFileException(ErrorCode.FileNotClosed);
            }
            _fileOperation = mode;
            InitPrivateFileds();
            InitFileContent(filePath, mode);
        }

        /// <summary>
        /// 关闭文件,写入文件头和文件尾信息
        /// </summary>
        public void Close()
        {
            //如果是读写操作
            if (_fileOperation != FileOperation.OpenWithReadOnly)
            {
                if (Channels.Count != NumberOfChannels)
                {
                    throw new WaveformFileException(ErrorCode.ChannelCountNotEqualToNumberOfChannels,
                        "添加的通道数和NumberOfChannels不一致");
                }
                //保存类的7个公共属性信息，包括采样信息和存储格式
                _sampleInformation.ByteOrder = ByteOrder;
                _sampleInformation.Channels = Channels;
                _sampleInformation.DataLength = DataLength;
                _sampleInformation.FileFormat = FileFormat;
                _sampleInformation.NumberOfChannels = NumberOfChannels;
                _sampleInformation.SampleRate = SampleRate;
                _sampleInformation.DataStartTime = DataStartTime;
                _fileMenu._dataType = _dataType;
                //修改文件写指针到4096            
                byte[] str = new byte[] { };
                _fileStream.Seek(FORMATLENGH, SeekOrigin.Begin);
                _fileHeadLengh += FORMATLENGH;
                //写入采样信息
                _fileMenu._sampleInfoLength = JsonSerializer(_sampleInformation, ref str);
                _fileHeadLengh += _fileMenu._sampleInfoLength;
                WriteHeadInfoToFile(_fileStream, str, _fileMenu._sampleInfoLength);
                //写入归档信息
                _fileMenu._archiveInfoLength = JsonSerializer(_archiveInformation, ref str);
                _fileHeadLengh += _fileMenu._archiveInfoLength;
                WriteHeadInfoToFile(_fileStream, str, _fileMenu._archiveInfoLength);                
                //写入时间标签
                _fileMenu._timeLabelsLength = JsonSerializer(_timeLabels, ref str);
                _fileHeadLengh += _fileMenu._timeLabelsLength;
                WriteHeadInfoToFile(_fileStream, str, _fileMenu._timeLabelsLength);
                //写入自定义标签
                _fileMenu._customLabelsLength = JsonSerializer(_customLabels, ref str);
                _fileHeadLengh += _fileMenu._customLabelsLength;
                WriteHeadInfoToFile(_fileStream, str, _fileMenu._customLabelsLength);
                //写入文件目录信息
                _fileStream.Seek(0, SeekOrigin.Begin);
                int menuLength = JsonSerializer(_fileMenu, ref str);
                _fileStream.Write(str, 0, str.Length);
                _fileHeadLengh = 0;
            }
            //关闭文件流
            _fileStream.Close();
            _fileClosed = true;
        }

        /// <summary>
        /// 将二维数组的double类型数据转换成原始字节数据写入文件中
        /// </summary>
        /// <param name="data">存放数据的二维数组</param>
        public void Write(double[,] data)
        {
            //不允许写入不同数据类型的数据
            if (_dataWrited == true && _dataType != DataType.Double)
            {
                throw new WaveformFileException(ErrorCode.WriteDifferentDataType);
            }
            _dataType = DataType.Double;
            WriteDataToFile(_fileStream, data);
            _dataWrited = true;
        }

        /// <summary>
        /// 将二维数组的float类型数据转换成原始字节数据写入文件中
        /// </summary>
        /// <param name="data"></param>
        public void Write(float[,] data)
        {
            //不允许写入不同数据类型的数据
            if (_dataWrited == true && _dataType != DataType.Float)
            {
                throw new WaveformFileException(ErrorCode.WriteDifferentDataType);
            }
            _dataType = DataType.Float;
            WriteDataToFile(_fileStream, data);
            _dataWrited = true;
        }

        /// <summary>
        /// 将二维数组的short类型数据转换成原始字节数据写入文件中
        /// </summary>
        /// <param name="data"></param>
        public void Write(short[,] data)
        {
            //不允许写入不同数据类型的数据
            if (_dataWrited == true && _dataType != DataType.Int16)
            {
                throw new WaveformFileException(ErrorCode.WriteDifferentDataType);
            }
            _dataType = DataType.Int16;
            WriteDataToFile(_fileStream, data);
            _dataWrited = true;
        }

        /// <summary>
        /// 将二维数组的int类型数据转换成原始字节数据写入文件中
        /// </summary>
        /// <param name="data"></param>
        public void Write(int[,] data)
        {
            //不允许写入不同数据类型的数据
            if (_dataWrited == true && _dataType != DataType.Int32)
            {
                throw new WaveformFileException(ErrorCode.WriteDifferentDataType);
            }
            _dataType = DataType.Int32;
            WriteDataToFile(_fileStream, data);
            _dataWrited = true;
        }

        /// <summary>
        /// 将一维数组的double类型数据转换成原始字节数据写入文件中
        /// </summary>
        /// <param name="data">存放数据的一维数组</param>
        /// <remarks>如果是多通道数据，按通道交错排列</remarks>
        public void Write(double[] data)
        {
            //不允许写入不同数据类型的数据
            if (_dataWrited == true && _dataType != DataType.Double)
            {
                throw new WaveformFileException(ErrorCode.WriteDifferentDataType);
            }
            _dataType = DataType.Double;
            WriteDataToFile(_fileStream, data);
            _dataWrited = true;
        }

        /// <summary>
        /// 将一维数组的float类型数据转换成原始字节数据写入文件中
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>如果是多通道数据，按通道交错排列</remarks>
        public void Write(float[] data)
        {
            //不允许写入不同数据类型的数据
            if (_dataWrited == true && _dataType != DataType.Float)
            {
                throw new WaveformFileException(ErrorCode.WriteDifferentDataType);
            }
            _dataType = DataType.Float;
            WriteDataToFile(_fileStream, data);
            _dataWrited = true;
        }

        /// <summary>
        /// 将一维数组的short类型数据转换成原始字节数据写入文件中
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>如果是多通道数据，按通道交错排列</remarks>
        public void Write(short[] data)
        {
            //不允许写入不同数据类型的数据
            if (_dataWrited == true && _dataType != DataType.Int16)
            {
                throw new WaveformFileException(ErrorCode.WriteDifferentDataType);
            }
            _dataType = DataType.Int16;
            WriteDataToFile(_fileStream, data);
            _dataWrited = true;
        }

        /// <summary>
        /// 将一维数组的int类型数据转换成原始字节数据写入文件中
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>如果是多通道数据，按通道交错排列</remarks>
        public void Write(int[] data)
        {
            //不允许写入不同数据类型的数据
            if (_dataWrited == true && _dataType != DataType.Int32)
            {
                throw new WaveformFileException(ErrorCode.WriteDifferentDataType);
            }
            _dataType = DataType.Int32;
            WriteDataToFile(_fileStream, data);
            _dataWrited = true;
        }

        /// <summary>
        /// 从文件中读取字节数据，转换成指定的double类型
        /// 如果写入文件的数据类型是short或者int，读取的是scale转换后的数据
        /// </summary>
        /// <param name="data">存放数据的二维数组</param>
        public void Read(double[,] data)
        {
            if (_dataType == DataType.Double)
            {
                ReadDataFromFile(_fileStream, data);
            }
            else if (_dataType == DataType.Int16)
            {
                short[,] rawData = new short[data.GetLength(0), data.GetLength(1)];
                ReadDataFromFile(_fileStream, rawData);
                for (int i = 0; i < data.GetLength(0); i++)//Scale转换
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        data[i, j] = Channels[j].Scale * rawData[i, j] + Channels[j].Offset;
                    }
                }
            }
            else if (_dataType == DataType.Int32)
            {
                int[,] rawData = new int[data.GetLength(0), data.GetLength(1)];
                ReadDataFromFile(_fileStream, rawData);
                for (int i = 0; i < data.GetLength(0); i++)//Scale转换
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        data[i, j] = Channels[j].Scale * rawData[i, j] + Channels[j].Offset;
                    }
                }
            }
            else//读取数据类型错误
            {
                throw new WaveformFileException(ErrorCode.ReadDataTypeError);
            }
        }

        /// <summary>
        /// 从文件中读取字节数据，转换成指定的float类型
        /// </summary>
        /// <param name="data"></param>
        public void Read(float[,] data)
        {
            if (_dataType != DataType.Float)//读取数据类型错误
            {
                throw new WaveformFileException(ErrorCode.ReadDataTypeError);
            }
            ReadDataFromFile(_fileStream, data);
        }

        /// <summary>
        /// 从文件中读取字节数据，转换成指定的short类型
        /// </summary>
        /// <param name="data"></param>
        public void Read(short[,] data)
        {
            if (_dataType != DataType.Int16)//读取数据类型错误
            {
                throw new WaveformFileException(ErrorCode.ReadDataTypeError);
            }
            ReadDataFromFile(_fileStream, data);
        }

        /// <summary>
        /// 从文件中读取字节数据，转换成指定的int类型
        /// </summary>
        /// <param name="data"></param>
        public void Read(int[,] data)
        {
            if (_dataType != DataType.Int32)//读取数据类型错误
            {
                throw new WaveformFileException(ErrorCode.ReadDataTypeError);
            }
            ReadDataFromFile(_fileStream, data);
        }

        /// <summary>
        /// 从文件中读取字节数据，转换成指定的double类型
        /// 如果写入文件的数据类型是short或者int，读取的是scale转换后的数据
        /// </summary>
        /// <param name="data">存放数据的一维数组</param>
        /// <remarks>如果是多通道数据，按通道交错排列</remarks>
        public void Read(double[] data)
        {
            if (_dataType == DataType.Double)
            {
                ReadDataFromFile(_fileStream, data);
            }
            else if (_dataType == DataType.Int16)
            {
                short[] rawData = new short[data.Length];
                ReadDataFromFile(_fileStream, rawData);
                for (int i = 0; i < data.Length; i = i + Channels.Count)//Scale转换
                {
                    for (int j = 0; j < Channels.Count; j++)
                    {
                        data[i + j] = Channels[j].Scale * rawData[i + j] + Channels[j].Offset;
                    }
                }
            }
            else if (_dataType == DataType.Int32)
            {
                int[] rawData = new int[data.Length];
                ReadDataFromFile(_fileStream, rawData);
                for (int i = 0; i < data.Length; i = i + Channels.Count)//Scale转换
                {
                    for (int j = 0; j < Channels.Count; j++)
                    {
                        data[i + j] = Channels[j].Scale * rawData[i + j] + Channels[j].Offset;
                    }
                }
            }
            else//读取数据类型错误
            {
                throw new WaveformFileException(ErrorCode.ReadDataTypeError);
            }
        }

        /// <summary>
        /// 从文件中读取字节数据，转换成指定的float类型
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>如果是多通道数据，按通道交错排列</remarks>
        public void Read(float[] data)
        {
            if (_dataType != DataType.Float)//读取数据类型错误
            {
                throw new WaveformFileException(ErrorCode.ReadDataTypeError);
            }
            ReadDataFromFile(_fileStream, data);
        }

        /// <summary>
        /// 从文件中读取字节数据，转换成指定的short类型
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>如果是多通道数据，按通道交错排列</remarks>
        public void Read(short[] data)
        {
            if (_dataType != DataType.Int16)//读取数据类型错误
            {
                throw new WaveformFileException(ErrorCode.ReadDataTypeError);
            }
            ReadDataFromFile(_fileStream, data);
        }

        /// <summary>
        /// 从文件中读取字节数据，转换成指定的int类型
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>如果是多通道数据，按通道交错排列</remarks>
        public void Read(int[] data)
        {
            if (_dataType != DataType.Int32)//读取数据类型错误
            {
                throw new WaveformFileException(ErrorCode.ReadDataTypeError);
            }
            ReadDataFromFile(_fileStream, data);
        }

        /// <summary>
        /// 设置文件读取位置，以Sample为单位
        /// </summary>
        /// <param name="position"></param>
        public void SetFilePosition(long position)
        {
            _readIndex = position;
        }

        /// <summary>
        /// 获取当前文件读取位置，或已写入的数据长度，以Sample为单位
        /// </summary>
        /// <returns>当前已写入数据区的数据总长度</returns>
        public long GetFilePosition()
        {
            return _writeIndex;
        }

        /// <summary>
        /// 添加时间标签到时间标签列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="time"></param>
        /// <param name="description"></param>
        public void AddTimeLabel(string name, long position, DateTime time, string description)
        {
            if (_fileOperation == FileOperation.OpenWithReadOnly)
            {
                throw new WaveformFileException(ErrorCode.CannotCall);
            }
            TimeLabel label = new TimeLabel(name, position, time, description);
            if (TimeLabels.FindIndex(t => t.Name == name) >= 0) //不允许添加相同名字的标签
            {
                throw new WaveformFileException(ErrorCode.SameLabel);
            }
            TimeLabels.Add(label);//添加到时间标签列表
        }

        /// <summary>
        ///  添加时间标签到时间标签列表
        /// </summary>
        /// <param name="label"></param>
        public void AddTimeLabel(TimeLabel label)
        {
            if (_fileOperation == FileOperation.OpenWithReadOnly)
            {
                throw new WaveformFileException(ErrorCode.CannotCall);
            }
            if (TimeLabels.FindIndex(t => t.Name == label.Name) >= 0) //不允许添加相同名字的标签
            {
                throw new WaveformFileException(ErrorCode.SameLabel);
            }
            TimeLabels.Add(label);//添加到时间标签列表            
        }

        /// <summary>
        /// 获取指定时间标签信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TimeLabel GetTimeLabel(string name)
        {
            TimeLabel label = new TimeLabel();
            int idx = -1;
            if ((idx = TimeLabels.FindIndex(t => t.Name == name)) >= 0) //查找
            {
                label = TimeLabels[idx];
            }
            else//如果指定标签不存在
            {
                throw new WaveformFileException(ErrorCode.LabelNotExit);
            }
            return label;
        }

        /// <summary>
        /// 添加自定义备注，支持Int/Double/String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void AddCustomLabel<T>(string name, CustomLabelDataType type, T value)
        {
            if (_fileOperation == FileOperation.OpenWithReadOnly)
            {
                throw new WaveformFileException(ErrorCode.CannotCall);
            }
            CustomLabel label = new CustomLabel(name, type, value);
            if (CustomLabels.FindIndex(t => t.Name == name) >= 0)//不允许添加相同名字的标签
            {
                throw new WaveformFileException(ErrorCode.SameLabel);
            }
            CustomLabels.Add(label);//添加到自定义标签列表
        }

        /// <summary>
        /// 获取指定时间标签信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CustomLabel GetCustomLabel(string name)
        {
            CustomLabel label = new CustomLabel();
            int idx = -1;
            //如果指定的时间标签存在
            if ((idx = CustomLabels.FindIndex(t => t.Name == name)) >= 0)
            {
                label = CustomLabels[idx];
            }
            else//指定的标签不存在
            {
                throw new WaveformFileException(ErrorCode.LabelNotExit);
            }
            return label;            
        }
        #endregion

        #region ----------私有方法----------
        /// <summary>
        /// 初始化相关私有字段
        /// </summary>
        private void InitPrivateFileds()
        {
            _writeIndex = 0;
            _readIndex = 0;
            _fileClosed = false;
            _readFileIndexInHead = true;
            _writeFileIndexInHead = true;
            _fileMenu = new FileMenu();
            _timeLabels = new List<TimeLabel>();
            _customLabels = new List<CustomLabel>();
            _archiveInformation = new ArchiveInformation();
            _sampleInformation = new SampleInformation();
            Channels = new List<ChannelInfo>();
            _dataWrited = false;
        }

        /// <summary>
        /// 创建FileStream对象，提取或初始化文件头信息
        /// </summary>
        /// <remarks>如果是只读操作，读取文件头和文件尾信息，
        /// 如果是读写操作，用65536个回车符将文件头填满</remarks>
        private void InitFileContent(string filePath, FileOperation mode)
        {
            //如果只有读操作，则打开相应文件，将文件目录信息和文件头信息取出来
            if (mode == FileOperation.OpenWithReadOnly || mode == FileOperation.Open)
            {
                #region 取出文件头和文件尾信息
                _fileStream = new FileStream(filePath, FileMode.Open);
                //读取文件目录
                byte[] str0 = new byte[FORMATLENGH];
                ReadHeadInfoFromFile(_fileStream, ref str0, FORMATLENGH);
                JsonDeSerializer(str0, ref _fileMenu);
                _dataType = _fileMenu._dataType;
                //读取采样信息
                byte[] str2 = new byte[_fileMenu._sampleInfoLength];
                ReadHeadInfoFromFile(_fileStream, ref str2, _fileMenu._sampleInfoLength);
                JsonDeSerializer(str2, ref _sampleInformation);
                //取出类的7个公共属性信息，包括采样信息和存储格式
                ByteOrder = _sampleInformation.ByteOrder;
                Channels = _sampleInformation.Channels;
                DataLength = _sampleInformation.DataLength;
                FileFormat = _sampleInformation.FileFormat;
                NumberOfChannels = _sampleInformation.NumberOfChannels;
                SampleRate = _sampleInformation.SampleRate;
                DataStartTime = _sampleInformation.DataStartTime;
                //读取归档信息
                byte[] str1 = new byte[_fileMenu._archiveInfoLength];
                ReadHeadInfoFromFile(_fileStream, ref str1, _fileMenu._archiveInfoLength);
                JsonDeSerializer(str1, ref _archiveInformation);
                //读取时间标签
                byte[] str3 = new byte[_fileMenu._timeLabelsLength];
                ReadHeadInfoFromFile(_fileStream, ref str3, _fileMenu._timeLabelsLength);
                JsonDeSerializer(str3, ref _timeLabels);
                //读取自定义标签
                byte[] str4 = new byte[_fileMenu._customLabelsLength];
                ReadHeadInfoFromFile(_fileStream, ref str4, _fileMenu._customLabelsLength);
                JsonDeSerializer(str4, ref _customLabels);
                _fileHeadLengh = 0;
                #endregion

                //修改写指针
                if (_fileOperation== FileOperation.Open)
                {
                    _writeIndex = DataLength;
                }
            }
            else//如果是读写操作,根据指定的文件操作方式创建文件
            {
                _fileStream = new FileStream(filePath, (FileMode)mode);
                //先用65536个字节的回车符将文件头填满
                byte[] fileHead = new byte[FILEHEAD];
                for (int i = 0; i < FILEHEAD; i++)
                {
                    fileHead[i] = 0x0D;
                }
                _fileStream.Write(fileHead, 0, FILEHEAD);
            }
        }

        /// <summary>
        /// 将一个对象序列化为字符串，再转换成字节数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="rawByte"></param>
        private int JsonSerializer<T>(T t, ref byte[] rawByte)
        {
            DataContractJsonSerializer json = null;//序列化类
            string szJson = "";//序列化之后的字符串
            //写入归档信息
            json = new DataContractJsonSerializer(t.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, t);//注意，如果datetime未初始化此处会抛异常
                szJson = Encoding.UTF8.GetString(stream.ToArray());
                rawByte = Encoding.Default.GetBytes(szJson);
            }
            return rawByte.Length;
        }

        /// <summary>
        /// 将原始字节数组写入文件
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="rawByte"></param>
        /// <param name="length"></param>
        private void WriteHeadInfoToFile(FileStream fs, byte[] rawByte, int length)
        {
            if (_writeFileIndexInHead == true)//如果指针在文件头
            {
                if (_fileHeadLengh <= FILEHEAD)//如果没有超过65536，直接写入文件
                {
                    fs.Write(rawByte, 0, length);
                }
                else//如果超过65536，超过的部分往文件尾写
                {
                    int halfHeadLength = length - (_fileHeadLengh - FILEHEAD);
                    int halfEndLength = _fileHeadLengh - FILEHEAD;
                    //未超过的部分还是写在文件头
                    fs.Write(rawByte, 0, halfHeadLength);
                    //把文件指针指向文件尾
                    fs.Seek(0, SeekOrigin.End);
                    _writeFileIndexInHead = false;
                    fs.Write(rawByte, halfHeadLength, halfEndLength);
                }
            }
            else//如果指针在文件尾,直接写入文件
            {
                fs.Write(rawByte, 0, length);
            }
        }

        /// <summary>
        /// 从文件中读取原始字节数据
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="rawByte"></param>
        /// <param name="length"></param>
        private void ReadHeadInfoFromFile(FileStream fs, ref byte[] rawByte, int length)
        {
            if (_readFileIndexInHead == true)//如果指针在文件头
            {
                if ((_fileHeadLengh += length) <= FILEHEAD)//如果没有超过FILEHEAD，直接从文件中读取数据
                {
                    fs.Read(rawByte, 0, length);
                }
                else//如果超过FILEHEAD，超过的部分从文件尾读
                {
                    //先读取文件头的部分
                    int halfHeadLength = length - (_fileHeadLengh - FILEHEAD);
                    int halfEndLength = _fileHeadLengh - FILEHEAD;
                    fs.Read(rawByte, 0, halfHeadLength);
                    //计算数据区长度
                    long dataLengthInByte = 0;
                    switch (_dataType)
                    {
                        case (DataType.Int16):
                            dataLengthInByte = DataLength * sizeof(short);
                            break;
                        case (DataType.Int32):
                            dataLengthInByte = DataLength * sizeof(int);
                            break;
                        case (DataType.Float):
                            dataLengthInByte = DataLength * sizeof(float);
                            break;
                        case (DataType.Double):
                            dataLengthInByte = DataLength * sizeof(double);
                            break;
                    }
                    //把文件指针指向文件尾起始地址
                    fs.Seek(65536 + dataLengthInByte, SeekOrigin.Begin);
                    _readFileIndexInHead = false;
                    fs.Read(rawByte, halfHeadLength, halfEndLength);
                }
            }
            else//如果指针在文件尾,直接从文件中中读取数据
            {
                fs.Read(rawByte, 0, length);
            }
        }

        /// <summary>
        /// 将字节数组反序列化为指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawByte"></param>
        /// <returns></returns>
        private void JsonDeSerializer<T>(byte[] rawByte, ref T t)
        {
            string szJson = Encoding.Default.GetString(rawByte);
            //反序列化
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                try
                {
                    t = (T)serializer.ReadObject(ms);
                }
                catch (SerializationException e)
                {
                    throw new WaveformFileException(ErrorCode.OpenWrongFile);
                }
            }
        }

        /// <summary>
        /// 写入采样数据到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fs"></param>
        /// <param name="t"></param>
        private void WriteDataToFile<T>(FileStream fs, T[] t)
        {
            //Close之后不写数据
            if (_fileClosed == true)
            {
                throw new WaveformFileException(ErrorCode.FileAccessError);
            }
            if (_fileOperation == FileOperation.OpenWithReadOnly)
            {
                throw new WaveformFileException(ErrorCode.CannotCall);
            }
            int sizeOfT = Marshal.SizeOf(t[0]);
            //如果未创建内存或者写入大小改变
            if (_rawByteData == null || _rawByteData.Length != (t.Length * sizeOfT))
            {
                _rawByteData = new byte[t.Length * sizeOfT];
            }
            Buffer.BlockCopy(t, 0, _rawByteData, 0, t.Length * sizeOfT);
            //按字节将数据写入文件中
            fs.Seek(FILEHEAD + _writeIndex * sizeOfT, SeekOrigin.Begin);
            fs.Write(_rawByteData, 0, _rawByteData.Length);
            //修改写指针
            _writeIndex += t.Length;
            DataLength = _writeIndex;
        }

        /// <summary>
        /// 从文件中读取采样数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fs"></param>
        /// <param name="t"></param>
        private void ReadDataFromFile<T>(FileStream fs, T[] t)
        {
            //从文件中读取指定长度字节数组
            int sizeOfT = Marshal.SizeOf(t[0]);
            //如果未创建内存或者读入大小改变
            if (_byteData == null || _byteData.Length != t.Length * sizeOfT)
            {
                _byteData = new byte[t.Length * sizeOfT];
            }
            _fileStream.Seek(FILEHEAD + _readIndex * sizeOfT, SeekOrigin.Begin);
            _fileStream.Read(_byteData, 0, _byteData.Length);
            Buffer.BlockCopy(_byteData, 0, t, 0, _byteData.Length);
        }

        /// <summary>
        /// 写入采样数据到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fs"></param>
        /// <param name="t"></param>
        private void WriteDataToFile<T>(FileStream fs, T[,] t)
        {
            //Close之后不写数据
            if (_fileClosed == true)
            {
                throw new WaveformFileException(ErrorCode.FileAccessError);
            }
            if (_fileOperation == FileOperation.OpenWithReadOnly)
            {
                throw new WaveformFileException(ErrorCode.CannotCall);
            }
            int sizeOfT = Marshal.SizeOf(t[0, 0]);
            //如果未创建内存或者写入大小改变
            if (_rawByteData == null || _rawByteData.Length != (t.Length * sizeOfT))
            {
                _rawByteData = new byte[t.Length * sizeOfT];
            }
            Buffer.BlockCopy(t, 0, _rawByteData, 0, t.Length * sizeOfT);
            //按字节将数据写入文件中
            fs.Seek(FILEHEAD + _writeIndex * sizeOfT, SeekOrigin.Begin);
            fs.Write(_rawByteData, 0, _rawByteData.Length);
            //修改写指针
            _writeIndex += t.Length;
            DataLength = _writeIndex;
        }

        /// <summary>
        /// 从文件中读取采样数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fs"></param>
        /// <param name="t"></param>
        private void ReadDataFromFile<T>(FileStream fs, T[,] t)
        {
            //从文件中读取指定长度字节数组
            int sizeOfT = Marshal.SizeOf(t[0, 0]);
            //如果未创建内存或者读入大小改变
            if (_byteData == null || _byteData.Length != t.Length * sizeOfT)
            {
                _byteData = new byte[t.Length * sizeOfT];
            }
            _fileStream.Seek(FILEHEAD + _readIndex * sizeOfT, SeekOrigin.Begin);
            _fileStream.Read(_byteData, 0, _byteData.Length);
            Buffer.BlockCopy(_byteData, 0, t, 0, _byteData.Length);
        }
        #endregion
    }

    #region ----------类和枚举定义----------
    /// <summary>
    /// 文件目录信息
    /// </summary>
    [DataContract]
    internal class FileMenu
    {
        /// <summary>
        /// 归档信息长度
        /// </summary>
        [DataMember]
        public int _archiveInfoLength;

        /// <summary>
        /// 采样信息长度
        /// </summary>
        [DataMember]
        public int _sampleInfoLength;

        /// <summary>
        /// 时间标签信息长度
        /// </summary>
        [DataMember]
        public int _timeLabelsLength;

        /// <summary>
        /// 自定义标签信息长度
        /// </summary>
        [DataMember]
        public int _customLabelsLength;

        /// <summary>
        /// 数据格式，Int16/Int32/Float/Double
        /// </summary>
        [DataMember]
        public DataType _dataType;
    }

    /// <summary>
    /// 归档信息
    /// </summary>
    public class ArchiveInformation
    {
        /// <summary>
        /// 只读，WaveformFile的版本
        /// </summary>
        public string FileVersion
        {
            get
            {
                return "1.0.0";
            }
        }

        /// <summary>
        /// 可用于描述数据来源
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 可用于数据的归档标识
        /// </summary>
        public string DataLabel { get; set; }

        /// <summary>
        /// 可用于文件的关联标识
        /// </summary>
        public string DataGroupID { get; set; }

        /// <summary>
        /// 只读，文件建立的时间
        /// </summary>
        public DateTime FileCreatedDate { get; set; }

        /// <summary>
        /// 可用于描述地理位置信息
        /// </summary>
        public string GeographicLocation { get; set; }

        /// <summary>
        /// 可用于描述数据备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 归档信息，默认构造函数
        /// </summary>
        public ArchiveInformation()
        {
            FileCreatedDate = DateTime.Now;
        }
    }

    /// <summary>
    /// 通道信息
    /// </summary>
    public class ChannelInfo
    {
        /// <summary>
        /// 通道Group
        /// </summary>
        public string ChannelGroupId { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 通道量程上限
        /// </summary>
        public double RangeHigh { get; set; }

        /// <summary>
        /// 通道量程下限
        /// </summary>
        public double RangeLow { get; set; }

        /// <summary>
        /// 偏移
        /// </summary>
        public double Offset { get; set; }   // a of "a + b * x"

        /// <summary>
        /// 增益
        /// </summary>
        public double Scale { get; set; }    // b of "a + b * x"

        /// <summary>
        /// 样点数据单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 通道信息备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <remarks>设置偏移为0，斜率为1</remarks>
        public ChannelInfo()
        {
            Offset = 0;
            Scale = 1;
        }
    }

    /// <summary>
    /// 时间标签
    /// </summary>
    [DataContract]
    public class TimeLabel
    {
        /// <summary>
        /// 标签名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 样点位置，in samples from start
        /// </summary>
        [DataMember]
        public long Position { get; set; }

        /// <summary>
        /// 样点时间
        /// </summary>
        [DataMember]
        public DateTime Time { get; set; }

        /// <summary>
        /// 时间标签备注
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 构造函数，设置初始化值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="time"></param>
        /// <param name="description"></param>
        public TimeLabel(string name, long position, DateTime time, string description)
        {
            Name = name;
            Position = position;
            Time = time;
            Description = description;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TimeLabel()
        {
            Time = DateTime.Now;
        }
    }

    /// <summary>
    /// 自定义标签
    /// </summary>
    [DataContract]
    public class CustomLabel
    {
        /// <summary>
        /// 构造函数，设置初始化值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public CustomLabel(string name, CustomLabelDataType type, object value)
        {
            Name = name;
            _dataType = type;
            _dataValue = value;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CustomLabel()
        {
            _dataValue = new object();
        }

        /// <summary>
        /// 标签名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 自定义标签数据类型
        /// </summary>
        [DataMember]
        private CustomLabelDataType _dataType;

        /// <summary>
        /// 自定义标签数据类型所对应数据的值
        /// </summary>
        [DataMember]
        private object _dataValue;

        /// <summary>
        /// 自定义标签数据类型
        /// </summary>
        public CustomLabelDataType DataType//如果设为没有set的属性序列化时会抛异常？
        {
            get
            {
                return _dataType;
            }
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public void SetValue<T>(T value)
        {
            _dataValue = value;
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            return (T)_dataValue;
        }
    }

    /// <summary>
    /// 文件格式
    /// </summary>
    public enum FileFormat
    {
        /// <summary>
        /// Stream
        /// </summary>
        Stream,

        /// <summary>
        /// MultiRecord
        /// </summary>
        MultiRecord
    }

    /// <summary>
    /// 数据格式
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// short
        /// </summary>
        Int16,

        /// <summary>
        /// int
        /// </summary>
        Int32,

        /// <summary>
        /// float
        /// </summary>
        Float,

        /// <summary>
        /// double
        /// </summary>
        Double
    }

    /// <summary>
    /// 字节序
    /// </summary>
    public enum ByteOrder
    {
        /// <summary>
        /// 将低序字节存储在起始地址
        /// </summary>
        LittleEndian,

        /// <summary>
        /// 将高序字节存储在起始地址
        /// </summary>
        BigEndian
    }

    /// <summary>
    /// 自定义标签数据类型
    /// </summary>
    public enum CustomLabelDataType
    {
        /// <summary>
        /// short
        /// </summary>
        Int16,

        /// <summary>
        /// int
        /// </summary>
        Int32,

        /// <summary>
        /// double
        /// </summary>
        Double,

        /// <summary>
        /// string
        /// </summary>
        String
    }

    /// <summary>
    /// 文件操作类型
    /// </summary>
    public enum FileOperation
    {
        /// <summary>
        /// 指定操作系统应创建新文件
        /// </summary>
        CreateNew = 1,

        /// <summary>
        /// 指定操作系统应创建新文件。如果文件已存在，它将被覆盖。
        /// </summary>
        Create = 2,

        /// <summary>
        /// 指定操作系统应打开现有文件。
        /// </summary>
        Open = 3,

        /// <summary>
        /// 指定操作系统应打开文件（如果文件存在）；否则，应创建新文件。
        /// </summary>
        OpenOrCreate = 4,

        /// <summary>
        /// 指定操作系统应打开现有文件。
        /// </summary>
        Truncate = 5,

        /// <summary>
        /// 若存在文件，则打开该文件并查找到文件尾，或者创建一个新文件。
        /// </summary>
        Append = 6,

        /// <summary>
        /// 打开文件，只读模式
        /// </summary>
        OpenWithReadOnly
    }

    /// <summary>
    /// 采样信息
    /// </summary>
    [DataContract]
    internal class SampleInformation
    {
        /// <summary>
        /// 通道数
        /// </summary>
        [DataMember]
        public int NumberOfChannels { get; set; }

        /// <summary>
        /// 采样率，S/s
        /// </summary>
        [DataMember]
        public double SampleRate { get; set; }

        /// <summary>
        /// 数据长度，以Sample为单位
        /// </summary>
        [DataMember]
        public long DataLength { get; set; }

        /// <summary>
        /// 各通道采样信息，包括通道名称、采集量程、换算
        /// 因子、物理单位名称和备注
        /// </summary>
        [DataMember]
        public List<ChannelInfo> Channels { get; set; }

        /// <summary>
        /// 文件格式，Stream/Multi-Record
        /// </summary>
        [DataMember]
        public FileFormat FileFormat { get; set; }       

        /// <summary>
        /// 字节序，Little Endian/Big Endian
        /// </summary>
        [DataMember]
        public ByteOrder ByteOrder { get; set; }

        /// <summary>
        /// 数据起始点的时间
        /// </summary>
        [DataMember]
        public DateTime DataStartTime { get; set; }
    }

    /// <summary>
    /// 文件异常类
    /// </summary>
    public class WaveformFileException : ApplicationException
    {
        /// <summary>
        /// 异常错误代码
        /// </summary>
        public ErrorCode ErrorCode;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage;

        /// <summary>
        /// 构造函数，指定异常错误代码
        /// </summary>
        /// <param name="er"></param>
        public WaveformFileException(ErrorCode er)
        {
            ErrorCode = er;
        }

        /// <summary>
        /// 构造函数，指定异常错误代码和错误信息
        /// </summary>
        /// <param name="er"></param>
        public WaveformFileException(ErrorCode er, string errStr)
        {
            ErrorCode = er;
            ErrorMessage = errStr;
        }
    }

    /// <summary>
    /// 错误代码
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 添加相同标签
        /// </summary>
        SameLabel,

        /// <summary>
        /// 标签不存在
        /// </summary>
        LabelNotExit,

        /// <summary>
        /// 不允许调用
        /// </summary>
        CannotCall,

        /// <summary>
        /// 权限错误
        /// </summary>
        FileAccessError,

        /// <summary>
        /// 打开了错误文件
        /// </summary>
        OpenWrongFile,

        /// <summary>
        /// 文件还未关闭
        /// </summary>
        FileNotClosed,

        /// <summary>
        /// 读取数据类型指定错误
        /// </summary>
        ReadDataTypeError,

        /// <summary>
        /// 数据类型设置错误
        /// </summary>
        DataTypeSetError,

        /// <summary>
        /// 写入不同数据类型的数据
        /// </summary>
        WriteDifferentDataType,

        /// <summary>
        /// 添加的通道数目与NumberOfChannels属性不一致
        /// </summary>
        ChannelCountNotEqualToNumberOfChannels
    }
    #endregion
}

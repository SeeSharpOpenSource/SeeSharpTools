using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JY.ThreadSafeQueue;

namespace ChartTest
{
    public partial class TestCircularBuffer : Form
    {
        public TestCircularBuffer()
        {
            InitializeComponent();
        }

        private CircularQueue<double> _queue;
        private double[] _readBuf;
        private double[] _writeBuf;
        private double[] _readElapsed;
        private double[] _writeElapsed;
        private int _count;
        private void button1_Click(object sender, EventArgs e)
        {
            _queue = new CircularQueue<double>(500000);
            _readBuf = new double[50000];
            _writeBuf = new double[100000];
            for (int i = 0; i < _writeBuf.Length; i++)
            {
                _writeBuf[i] = i;
            }
            _writeElapsed = new double[10000];
            _readElapsed = new double[20000];

            _queue.BlockWait = true;
            _writeIndex = 0;
            _readIndex = 0;
            _count = 0;
            ThreadPool.SetMaxThreads(10, 10);


            _indexes = new HashSet<int>();
            for (int i = 0; i < _writeElapsed.Length; i++)
            {
                _indexes.Add(i);
            }

            for (int i = 0; i < _readElapsed.Length + _writeElapsed.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(TestQueue, i);
            }
            
//            ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = 20};
//            Parallel.For(0, 200, options, TestQueue);
        }

        private HashSet<int> _indexes;
        private int _writeIndex = 0;
        private int _readIndex = 0;
        private void TestQueue(object state)
        {
            Thread.Sleep((int) (new Random().NextDouble()*20));
            int index = (int)state;
            DateTime start = DateTime.Now;
            if (index%3 == 0)
            {
                int writeIndex = Interlocked.Increment(ref _writeIndex) - 1;
                _queue.Enqueue(_writeBuf);
                _writeElapsed[writeIndex] = (DateTime.Now - start).TotalMilliseconds;
                Invoke(new Action<string>((text => { textBox1.AppendText(text); })),
                     writeIndex + " " + _writeElapsed[writeIndex].ToString("F2") + Environment.NewLine);
            }
            else
            {
                int readIndex = Interlocked.Increment(ref _readIndex) - 1;
                double[] readBuf = new double[_readBuf.Length];
                _queue.Dequeue(readBuf);
                _readElapsed[readIndex] = (DateTime.Now - start).TotalMilliseconds;
                Invoke(new Action<string>((text => { textBox2.AppendText(text); })),
                    $"Index:{readIndex},Time:{_readElapsed[readIndex].ToString("F2")}, Thread:{Thread.CurrentThread.ManagedThreadId}, Average:{readBuf.Average()}{Environment.NewLine}");
                if (Math.Abs(readBuf.Average() - 24999.5) > 1 && Math.Abs(readBuf.Average() - 74999.5) > 1)
                {
                    // ignore
                }

                _indexes.Remove(readIndex);
            }
            Interlocked.Increment(ref _count);
            Invoke(new Action(() => { textBox3.Text = _count.ToString(); }));
//            Thread.Sleep(10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            timer1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(Environment.NewLine);
            textBox2.AppendText(Environment.NewLine);
            textBox1.AppendText($"Max:{_writeElapsed.Max().ToString("F2")} Min:{_writeElapsed.Min().ToString("F2")} Average:{_writeElapsed.Average().ToString("F2")}");
            textBox2.AppendText($"Max:{_readElapsed.Max().ToString("F2")} Min:{_readElapsed.Min().ToString("F2")} Average:{_readElapsed.Average().ToString("F2")}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _lockQueue = new ThreadSafeQueue(3000);
            _readBuf = new double[5000];
            _writeBuf = new double[10000];
            for (int i = 0; i < _writeBuf.Length; i++)
            {
                _writeBuf[i] = i;
            }
            _writeElapsed = new double[1500];
            _readElapsed = new double[1500];
            _writeIndex = 0;
            _readIndex = 0;
            _count = 0;
            ThreadPool.SetMaxThreads(12, 12);
            for (int i = 0; i < _readElapsed.Length + _writeElapsed.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(TestLock, i);
            }
        }

        private ThreadSafeQueue _lockQueue;
        private void TestLock(object state)
        {
            int index = (int)state;
            DateTime start = DateTime.Now;
            if (index % 2 == 0)
            {
                _lockQueue.Enqueue(_writeBuf);
                _writeElapsed[_writeIndex] = (DateTime.Now - start).TotalMilliseconds;
                Invoke(new Action<string>((text => { textBox1.AppendText(text); })),
                    _writeElapsed[_writeIndex].ToString("F2") + Environment.NewLine);
                Interlocked.Increment(ref _writeIndex);
            }
            else
            {
                double[] readBuf = (double[]) _lockQueue.Dequeue();
                _readElapsed[_readIndex] = (DateTime.Now - start).TotalMilliseconds;
                Invoke(new Action<string>((text => { textBox2.AppendText(text); })),
                    $"Time:{_readElapsed[_readIndex].ToString("F2")}, Thread:{Thread.CurrentThread.ManagedThreadId}, Average:{readBuf.Average()}{Environment.NewLine}");
                if (Math.Abs(readBuf.Average() - 2499.5) > 1 && Math.Abs(readBuf.Average() - 7499.5) > 1)
                {
                    // ignore
                }
                Interlocked.Increment(ref _readIndex);
            }
            Interlocked.Increment(ref _count);
            Invoke(new Action(() => { textBox3.Text = _count.ToString(); }));
            //            Thread.Sleep(1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TestQueue(_count);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _queue = new CircularQueue<double>(500000);
            _readBuf = new double[10000];
            _writeBuf = new double[10000];
            for (int i = 0; i < _writeBuf.Length; i++)
            {
                _writeBuf[i] = i;
            }
            _writeElapsed = new double[1000];
            _readElapsed = new double[1000];

            _queue.BlockWait = true;
            _writeIndex = 0;
            _readIndex = 0;
            _count = 0;
            timer1.Enabled = true;
        }
    }
}

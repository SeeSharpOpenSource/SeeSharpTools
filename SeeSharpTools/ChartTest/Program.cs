using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChartTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        private static double[,] test;
        private static IEnumerable<double> enumerable;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new Form1());
            Application.Run(new TestListEfficiency());
//            Application.Run(new TestStripChart());
//            Application.Run(new TestCircularBuffer());
//            Application.Run(new TestMsChart());
//            Application.Run(new TestArrayAndList());
        }
    }
}

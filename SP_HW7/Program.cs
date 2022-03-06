using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SP_HW7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyPrinter[] printer = new MyPrinter[2];
            for (int i = 0; i < printer.Length; i++)
            {
                printer[i] = new MyPrinter() { Name = $"{i+1}"};
            }
            Thread[] threads = new Thread[4];
            threads[0] = new Thread(() =>
            {
                try
                {
                    MyMonitor.Request(printer[0]);
                }
                finally
                {
                    MyMonitor.Release(printer[0]);
                }
            });
            threads[1] = new Thread(() =>
            {
                try
                {
                    MyMonitor.Request(printer[1]);
                }
                finally
                {
                    MyMonitor.Release(printer[1]);
                }
            });
            threads[2] = new Thread(() =>
            {
                MyMonitor.Lock(printer[0]);
            });
            threads[3] = new Thread(() =>
            {
                MyMonitor.Lock(printer[1]);
            });
            foreach (var item in threads)
            {
                item.Start();
            }
            foreach (var item in threads)
            {
                item.Join();
            }
            Console.ReadLine();
        }
    }
}

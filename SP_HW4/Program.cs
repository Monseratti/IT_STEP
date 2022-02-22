using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SP_HW4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ThreadStart start = new ThreadStart(MyTimer);
            Thread thread = new Thread(start);
            thread.Start();
            thread.Join();
            Console.ReadLine();

        }
        static void MyTimer()
        {
            Console.WriteLine("Press any key");
            DateTime date = DateTime.Now;
            Console.ReadKey();
            TimeSpan ts1 = DateTime.Now - date;
            Console.WriteLine($"Time is {ts1.TotalMilliseconds} ms");
        }
    }
}

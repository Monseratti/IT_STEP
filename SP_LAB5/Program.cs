using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace SP_LAB5
{
    internal class Program
    {

        static void Main(string[] args)
        {
            ///////////////////////////Task_1///////////////////////////
            //ThreadStart start = new ThreadStart(T1_5_1);
            //ThreadStart start2 = new ThreadStart(T1_5_2);
            //Thread t = new Thread(start);
            //Thread t1 = new Thread(start2);
            //t.Start();
            //t1.Start();
            ///////////////////////////Task_2///////////////////////////
            //List<int> vs = new List<int>();
            //Random random = new Random();
            //for (int i = 0; i < 15; i++)
            //{
            //    vs.Add(random.Next(0,100));
            //}
            //ParameterizedThreadStart threadStart = new ParameterizedThreadStart(T2_3_1);
            //ParameterizedThreadStart threadStart1 = new ParameterizedThreadStart(T2_3_2);
            //Thread t = new Thread(threadStart);
            //Thread t1 = new Thread(threadStart1);
            //t.Start(vs);
            //t1.Start(vs);
            //Console.ReadLine();
            ///////////////////////////Task_3///////////////////////////
            List<int> vs = new List<int>();
            List<int> vs1 = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                vs.Add(random.Next(0, 50));
            }
            for (int i = 0; i < 18; i++)
            {
                vs1.Add(random.Next(0, 100));
            }
            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(T3_1_1);
            ParameterizedThreadStart threadStart1 = new ParameterizedThreadStart(T3_1_2);
            Thread t = new Thread(threadStart);
            Thread t1 = new Thread(threadStart1);
            t.Start(vs);
            t1.Start(vs1);
            Console.ReadLine();
        }
        ///////////////////////////Task_1///////////////////////////
        static void T1_1_1()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{random.Next(1, 101)}");
            }
        }
        static void T1_1_2()
        {
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                Console.Write($"{(char)random.Next('A', 'Z')}");
            }
        }
        static void T1_2_1()
        {
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                Console.Write($"{random.Next(-5, 5)}; ");
            }
        }
        static void T1_2_2()
        {
            Random random = new Random();
            for (int i = 0; i < 40; i++)
            {
                Console.Write($"{(char)random.Next('А', 'Я')}; ");
            }
        }
        static void T1_3_1()
        {
            Random random = new Random();
            for (int i = 0; i < 35; i++)
            {
                int y = random.Next(1, 100);
                if (y % 2 != 0)
                    Console.Write($"{y}");
            }
        }
        static void T1_3_2()
        {
            Console.OutputEncoding = Encoding.Unicode;
            for (int i = 0; i < 70; i++)
            {
                Console.Write($"₴");
            }
        }
        static void T1_4_1()
        {
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                Console.Write($"{random.NextDouble():F2}");
            }
        }
        static void T1_4_2()
        {

            for (int i = 0; i < 20; i++)
            {
                Console.Write($"\\\\; ");
            }
        }
        static void T1_5_1()
        {
            Random random = new Random();
            for (int i = 0; i < 80; i++)
            {
                Console.Write($"{random.Next(-5, 5)}; ");
            }
        }
        static void T1_5_2()
        {
            Random random = new Random();
            for (int i = 0; i < 40; i++)
            {
                Console.Write($"{(char)random.Next('А', 'Я')}; ");
            }
        }
        ///////////////////////////Task_2///////////////////////////
        static void T2_1_1(object a)
        {
            foreach (int x in (a as List<int>))
            {
                Console.Write($"{Math.Pow(x, 2)}; ");
            }
        }
        static void T2_1_2(object a)
        {
            foreach (int x in (a as List<int>))
            {
                if (x < 50)
                    Console.Write($"{x}; ");
            }
        }
        static void T2_2_1(object a)
        {
            int count = 0;
            foreach (int x in (a as List<int>))
            {
                if (x > 10) count++;
            }
            Console.WriteLine(count);
        }
        static void T2_2_2(object a)
        {
            int count = 0;
            foreach (int x in (a as List<int>))
            {
                if (x % 2 == 0) count++;
            }
            Console.WriteLine(count);
        }
        static void T2_3_1(object a)
        {
            for (int i = 0; i < (a as List<int>).Count; i++)
            {
                if (i % 2 == 0) Console.Write($"{(a as List<int>)[i]}; ");
            }
        }
        static void T2_3_2(object a)
        {
            for (int i = 0; i < (a as List<int>).Count; i++)
            {
                if (i % 2 != 0) Console.Write($"~~{Math.Pow((a as List<int>)[i], 2)}; ");
            }
        }
        ///////////////////////////Task_3///////////////////////////
        ///особой разницы на своей машине в производительности вычислений не заметил. замеры времени выводятся в консоли
        ///в среднем 4 из 10 запусков потоки с более низким приоритетом заканчивают выполнение вычислений позже
        static void T3_1_1(object a)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            DateTime d = DateTime.Now;
            double sum = 0;
            foreach (int x in a as List<int>)
            {
                sum += Math.Pow(x * Math.Sin(Math.Pow(x, 2)), 1 / 3);
            }
            TimeSpan ts = DateTime.Now - d;
            Console.WriteLine($"Result A is {sum}. Time is {ts.TotalMilliseconds} ms");
        }
        static void T3_1_2(object a)
        {
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            DateTime d = DateTime.Now;
            double sum = 0;
            foreach (int x in a as List<int>)
            {
                sum += Math.Pow(x * Math.Cos(Math.Pow(x, 2)), 1 / 3);
            }
            TimeSpan ts = DateTime.Now - d;
            Console.WriteLine($"Result B is {sum}. Time is {ts.TotalMilliseconds} ms");
        }
        static void T3_2_1(object a)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
            DateTime d = DateTime.Now;
            double sum = 0;
            foreach (int x in a as List<int>)
            {
                sum += Math.Pow(x, 3) + 10;
            }
            TimeSpan ts = DateTime.Now - d;
            Console.WriteLine($"Result A is {sum}. Time is {ts.TotalMilliseconds} ms");
        }
        static void T3_2_2(object a)
        {
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
            DateTime d = DateTime.Now;
            double sum = 0;
            foreach (int x in a as List<int>)
            {
                sum += Math.Pow(x, 2) + Math.Pow(x, (double)1 / 4);
            }
            TimeSpan ts = DateTime.Now - d;
            Console.WriteLine($"Result B is {sum}. Time is {ts.TotalMilliseconds} ms");
        }


    }
}

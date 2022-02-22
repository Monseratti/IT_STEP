using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SP_LAB4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ParameterizedThreadStart threadStart = new ParameterizedThreadStart(MyMethod);
            //Thread thread = new Thread(threadStart);
            //thread.Start(new List<string>() { "Hello","world",});
            Bank bank = new Bank(10,8,"H");
            bank.SetMoney(100);
            bank.SetPercent(100);
            bank.SetName("SomeBank");
            Console.ReadLine();
        }
        static void MyMethod(object a)
        {
            foreach (var item in (a as List<string>))
            {
                Console.WriteLine(item);
            }
        }
    }
}

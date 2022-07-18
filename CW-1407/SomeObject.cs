using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_1407
{
    public class SomeObject
    {
        public enum Status
        {
            off = 0,
            waiting = 1,
            working = 2
        }
        public Form1 Form { get; set; }
        public int Number { get; set; }
        public int Counter { get; set; }
        public Thread Thr { get; set; }
        public Status St { get; set; } = Status.off;
        public SomeObject(int counter)
        {
            Number = counter;
            Counter = 0;
            Thr = new Thread(Count);
            Thr.Start();
        }
        public void Count(object obj)
        {
            while (true)
            {
                while (St == Status.waiting)
                {
                    if (Form.s.WaitOne())
                    {
                        St = Status.working;
                        Form.working_threads.Add(this);
                        Form.waiting_threads.Remove(this);
                        Form.Update();
                    }
                }
                while (St == Status.working)
                {
                    Counter++;
                    Thread.Sleep(1000);
                }
            }
        }
        public override string ToString()
        {
            switch (St)
            {
                case Status.off:
                    return $"Thread {Number} -> off -> {Counter}";
                case Status.waiting:
                    return $"Thread {Number} -> waiting";
                case Status.working:
                    return $"Thread {Number} -> working -> {Counter}";
                default:
                    return $"Thread {Number}";
            }
        }
    }
}

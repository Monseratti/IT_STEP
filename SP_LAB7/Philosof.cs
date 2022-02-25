using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SP_LAB7
{
    internal class Philosof
    {
        public string Name { set; get; }
        public bool IsHungry { get; set; }
        public int Eating { get; set; }
        public DateTime EatingDate { get; set; }
        public object Left_Fork { get; set; }
        public object Right_Fork { get; set; }
        public Philosof(object Fork1, object Fork2)
        {
            IsHungry = false;
            Left_Fork = Fork1;
            Right_Fork = Fork2;
            Eating = 0;
        }
        public void Life()
        {
            while (Eating < 3)
            {
                if (IsHungry || Eating < 3)
                {
                    try
                    {
                        if (Monitor.TryEnter(Left_Fork))
                        {
                            //Console.WriteLine($"{Name} take left fork");
                            if (Monitor.TryEnter(Right_Fork))
                            {
                                //Console.WriteLine($"{Name} take right fork");
                                IsHungry = false;
                                Eating++;
                                if (Eating == 3)
                                {
                                    EatingDate = DateTime.Now;
                                    //Console.WriteLine($"{Name} finish eat");
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (Monitor.IsEntered(Left_Fork))
                        {
                            Monitor.Exit(Left_Fork);
                            //Console.WriteLine($"{Name} took left fork");
                        }
                        if (Monitor.IsEntered(Right_Fork))
                        {
                            Monitor.Exit(Right_Fork);
                            //Console.WriteLine($"{Name} took right fork");
                        }
                    }
                }
                else IsHungry = true;
            }
        }
    }
}

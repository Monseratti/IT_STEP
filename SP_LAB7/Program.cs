using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SP_LAB7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            object[] forks = new object[5];
            for (int i = 0; i < forks.Length; i++)
            {
                forks[i] = new object();
            }
            PhilosofDinner(ref forks);
        }

        public static void PhilosofDinner(ref object[] forks)
        {
            Console.WriteLine("Philosof Dinner:");
            Philosof[] philosofs = new Philosof[5];
            for (int i = 0; i < philosofs.Length; i++)
            {
                if (i < philosofs.Length - 1)
                {
                    philosofs[i] = new Philosof(forks[i], forks[i + 1]) { Name = $"Philosof {i + 1}" };
                }
                else
                {
                    philosofs[i] = new Philosof(forks[i], forks[0]) { Name = $"Philosof {i + 1}" };
                }
            }
            Thread[] threads = new Thread[5];
            DateTime date = DateTime.Now;
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(philosofs[i].Life);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();
            foreach (var item in philosofs)
            {
                Console.WriteLine($"{item.Name} finish eated at {(item.EatingDate - date).TotalMilliseconds} ms ");
            }
            Console.ReadLine();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW7
{
    public class MyPrinter
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public MyPrinter()
        {
            ID = -1;
        }
        public void Print()
        {
            Console.WriteLine($"Hello! I'm printer {Name} with ID {ID}");
        }
    }
}

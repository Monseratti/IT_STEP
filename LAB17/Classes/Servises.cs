using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB17.Classes
{
    internal class Servises
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        
        public ICollection<Orders> Orders { get; set; }

        public Servises()
        {
            Orders = new List<Orders>();
        }
    }
}

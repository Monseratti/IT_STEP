using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB17.Classes
{
    internal class Autos
    {
        public int Id { get; set; }
        public string RegNumber { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string? Info { get; set; }
        public int ClientsId { get; set; }

        public ICollection<Orders> Orders { get; set; }

        public Autos()
        {
            Orders = new List<Orders>();
        }
    }
}

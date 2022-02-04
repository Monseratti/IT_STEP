using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity02
{
    internal class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public decimal Price { get; set; }
        public int MarkId { get; set; }
        public Mark Mark { get; set; }
        public ICollection<Sale> Sales { get; set; }

        public Car()
        {
            Sales = new List<Sale>();
        }
    }
}

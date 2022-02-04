using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity02
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public Customer()
        {
            Sales = new List<Sale>();
        }
    }
}

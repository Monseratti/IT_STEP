using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity02
{
    internal class Sale
    {
        public int Id { get; set; }
        public DateTime DateSale { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}

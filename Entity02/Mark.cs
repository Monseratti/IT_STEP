using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity02
{
    internal class Mark
    {
        public int ID { get; set; }
        public string MarkName { get; set; }
        public ICollection<Country> Countries { get; set; }
        public ICollection<Car> Cars { get; set; }

        public Mark()
        {
            Countries = new List<Country>();
            Cars = new List<Car>();
        }
    }
}

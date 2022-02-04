using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity02
{
    internal class Country
    {
        public int ID { get; set; }
        public string CountryName { get; set; }
        public ICollection<Mark> Marks { get; set; }

        public Country()
        {
            Marks = new List<Mark>();
        }
    }
}

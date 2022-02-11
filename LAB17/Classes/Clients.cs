using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB17.Classes
{
    internal class Clients
    {
        public int Id { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientPhoneNumber { get; set; }

        public int? Discount { get; set; }
        public ICollection<Autos> Autos { get; set; }
        public Clients()
        {
            Autos = new List<Autos>();
        }
    }
}

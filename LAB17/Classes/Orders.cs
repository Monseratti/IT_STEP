using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB17.Classes
{
    internal class Orders
    {
        public int Id { get; set; }
        public int ServisesId { get; set; }
        public int WorkersId { get; set; }
        public int AutosId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Ended { get; set; }
    }
}

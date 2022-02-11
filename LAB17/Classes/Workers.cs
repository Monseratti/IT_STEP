using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB17.Classes
{
    internal class Workers
    {
        public int Id { get; set; }
        public string WorkerFirstName { get; set; }
        public string WorkerLastName    { get; set; }
        public string WorkerPhoneNumber { get; set; }
        public DateTime WorkerStartEmpDate { get; set; }
        public DateTime? WorkerEndEmpDate { get; set; }
        public ICollection<Orders> Orders { get; set; }

        public Workers()
        {
            Orders = new List<Orders>();
        }
    }
}

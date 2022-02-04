using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isDebtor { get; set; }
        public ICollection<UsersBooks> UsersBooks { get; set; }
        public User()
        {
            UsersBooks = new List<UsersBooks>();
        }
    }
}

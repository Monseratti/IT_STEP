using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    internal class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AuthorsBooks> AuthorsBooks { get; set; }
        public Author()
        {
            AuthorsBooks = new List<AuthorsBooks>();
        }
    }
}

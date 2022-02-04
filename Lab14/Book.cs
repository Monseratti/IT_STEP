using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<AuthorsBooks> AuthorsBooks { get; set; }
        public ICollection<UsersBooks> UsersBooks { get; set; }
        public Book()
        {
            AuthorsBooks = new List<AuthorsBooks>();
            UsersBooks = new List<UsersBooks>();
        }
    }
}

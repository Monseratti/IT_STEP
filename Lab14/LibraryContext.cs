using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    internal class LibraryContext:DbContext
    {
        public LibraryContext() : base("ConnStr") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<UsersBooks> UsersBooks { get; set; }
        public DbSet<AuthorsBooks> AuthorsBooks { get; set; }
    }
}

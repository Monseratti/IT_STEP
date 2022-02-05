using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    [Table("TableBooks")]
    internal class Book
    {
        [Key]
        public int Id { get; set; }
        [Column("BookTitle", TypeName = "nvarchar")]
        [MaxLength(50)]
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

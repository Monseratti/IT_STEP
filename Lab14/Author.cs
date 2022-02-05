using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    [Table("TableAuthor")]
    internal class Author
    {
        [Key]
        public int Id { get; set; }
        [Column("AuthorName", TypeName ="nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<AuthorsBooks> AuthorsBooks { get; set; }
        public Author()
        {
            AuthorsBooks = new List<AuthorsBooks>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    [Table("TableAuthorsBooks")]
    internal class AuthorsBooks
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        [Required]
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        [Required]
        public Author Author { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    [Table("TableUser")]
    internal class User
    {
        [Key]
        public int Id { get; set; }
        [Column("UserName", TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool isDebtor { get; set; }
        public ICollection<UsersBooks> UsersBooks { get; set; }
        public User()
        {
            UsersBooks = new List<UsersBooks>();
        }
    }
}

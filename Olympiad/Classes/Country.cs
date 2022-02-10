using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Olympiad.Classes
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Column("CountryName")]
        public string Name { get; set; }
        public ICollection<Olympaid> Olympaid { get; set;}
        public ICollection<Sportsmen> Sportsmen { get; set;}

        public Country()
        {
            Olympaid = new List<Olympaid>();
            Sportsmen = new List<Sportsmen>();
        }
    }
}

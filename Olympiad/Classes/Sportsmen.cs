using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympiad.Classes
{
    public class Sportsmen
    {
        public int Id { get; set; }
        [Column("SportsmenName")]
        public string Name { get; set; }
        [Column(TypeName ="date")]
        public DateTime BirthDay { get; set; }
        [Column(TypeName ="varbinary")]
        public byte[] Photo { get; set; }
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public ICollection<OlympaidResults> Results { get; set; }
        public Sportsmen()
        {
            Results = new List<OlympaidResults>();
        }

    }
}

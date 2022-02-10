using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympiad.Classes
{
    public class OlympaidResults
    {
        public int Id { get; set; }
        public int OlSpId { get; set; }
        [ForeignKey("OlSpId")]
        public OlympaidSports OlympaidSports { get; set; }
        public int SportsmenId  { get; set; }
        [ForeignKey("SportsmenId")]
        public Sportsmen Sportsmen { get; set; }
        public int GoldenMedal { get; set; }
        public int SilverMedal { get; set; }
        public int BronzeMedal { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympiad.Classes
{
    internal class Olympaid
    {
        public int Id { get; set; }
        public short Year { get; set; }
        public bool IsWinter { get; set; }
        public int? ParentCountryId { get; set; }
        [ForeignKey("ParentCountryId")]
        public Country Country { get; set; }
        public string CityName { get; set; }
        public ICollection<OlympaidSports> OlympaidSports { get; set; }

        public Olympaid()
        {
            OlympaidSports = new List<OlympaidSports>();
        }
    }
}

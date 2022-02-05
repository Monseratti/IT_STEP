using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympiad.Classes
{
    internal class Sports
    {
        public int Id { get; set; }
        [Column("SportsName")]
        public string Name { get; set; }
        public ICollection<OlympaidSports> OlympaidSports { get; set; }

        public Sports()
        {
            OlympaidSports = new List<OlympaidSports>();
        }

    }
}

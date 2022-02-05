using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympiad.Classes
{
    internal class OlympaidSports
    {
        public int Id { get; set; }
        public int OlympaidId { get; set; }
        public Olympaid Olympaid { get; set; }
        public int SportsId { get; set; }
        public Sports Sports { get; set; }
        public ICollection<OlympaidResults> Results { get; set; }
        public OlympaidSports()
        {
            Results = new List<OlympaidResults>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympiad.Classes;

namespace Olympiad
{
    internal class OlympaidContext:DbContext
    {
        public OlympaidContext() : base("ConnStr") { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Sportsmen> Sportsmens { get; set; }
        public DbSet<Olympaid> Olympaids { get; set; }
        public DbSet<OlympaidResults> OlympaidResults { get; set; }
        public DbSet <OlympaidSports> OlympaidSports { get; set; }
        public DbSet<Sports> Sports { get; set; }
      
    }
}

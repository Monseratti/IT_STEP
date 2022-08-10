using MyStreet;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace HW_0908_Server.Classes
{
    public class StreetContext:DbContext
    {
        public StreetContext(): base("strDB") { }

        public DbSet<Streets> Streets { get; set; }
    }
}
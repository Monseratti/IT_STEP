using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB17.Classes;

namespace LAB17
{
    internal class AutoContext : DbContext
    {
        public DbSet<Autos> Autos { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Servises> Servises { get; set; }
        public DbSet<Workers> Workers { get; set; }
        public DbSet<Orders> Orders { get; set; }

        public AutoContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RCR1OM0\SQLEXPRESS;Initial Catalog=Autoservises;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}

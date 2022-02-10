using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympiad.Classes;
using Olympiad.Queries;

namespace Olympiad
{
    public class OlympaidContext:DbContext
    {
        public OlympaidContext() : base("ConnStr") { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Sportsmen> Sportsmens { get; set; }
        public DbSet<Olympaid> Olympaids { get; set; }
        public DbSet<OlympaidResults> OlympaidResults { get; set; }
        public DbSet <OlympaidSports> OlympaidSports { get; set; }
        public DbSet<Sports> Sports { get; set; }

        public DbRawSqlQuery<Query1> Query1()
        {
            var sql = @"Query1";
            return Database.SqlQuery<Query1>(sql);
        }                                                       //0
        public DbRawSqlQuery<Query2> Query2(SqlParameter parameter)
        {
            var sql = @"Query2 @year";
            return Database.SqlQuery<Query2>(sql, parameter);
        }                                 //1
        public DbRawSqlQuery<Query3> Query3()
        {
            var sql = @"Query3";
            return Database.SqlQuery<Query3>(sql);
        }                                                       //2
        public DbRawSqlQuery<Query4> Query4(SqlParameter parameter)
        {
            var sql = @"Query4 @year";
            return Database.SqlQuery<Query4>(sql, parameter);
        }                                 //3
        public DbRawSqlQuery<Query5> Query5()
        {
            var sql = @"Query5";
            return Database.SqlQuery<Query5>(sql);
        }                                                       //4
        public DbRawSqlQuery<Query6> Query6(SqlParameter parameter)
        {
            var sql = @"Query6 @year";
            return Database.SqlQuery<Query6>(sql, parameter);
        }                                 //5
        public DbRawSqlQuery<Query7> Query7(SqlParameter parameter)
        {
            var sql = @"Query7 @SportName";
            return Database.SqlQuery<Query7>(sql, parameter);
        }                                 //6
        public DbRawSqlQuery<Query8> Query8()
        {
            var sql = @"Query8";
            return Database.SqlQuery<Query8>(sql);
        }                                                       //7
        public DbRawSqlQuery<Query9> Query9(SqlParameter parameter)
        {
            var sql = @"Query9 @CountryName";
            return Database.SqlQuery<Query9>(sql, parameter);
        }                                 //8
        public DbRawSqlQuery<Query10> Query10(SqlParameter parameter)
        {
            var sql = @"Query10 @CountryName";
            return Database.SqlQuery<Query10>(sql, parameter);
        }                               //9
        public DbRawSqlQuery<Query11> Query11(SqlParameter parameter, SqlParameter parameter1)
        {
            var sql = @"Query11 @CountryName, @year";
            return Database.SqlQuery<Query11>(sql, parameter,parameter1);
        }      //10

    }
}

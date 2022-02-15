using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (DbConnection dbConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString))
            {
                var parameters = new { Mark = "BMW", Model = "M5" };
                string sql = "insert into Car values(@Mark,@Model)";
                //dbConn.Query(sql, parameters);
                string sql1 = "select * from Car";
                var rez =  dbConn.Query<Car>(sql1);
                foreach (Car car in rez)
                {
                    Console.WriteLine($"{car.Id} - {car.Mark} - {car.Model}");
                }
                Console.ReadLine();
            }
        }
    }
}

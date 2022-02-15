using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace LAB16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using (DbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString))
            //{
            //    Task1(conn);
            //    Console.ReadLine();
            //}
            using (DbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr1"].ConnectionString))
            {
                //List<Department> departments = new List<Department>()
                //{
                //    new Department(){ Id = 1, Country = "Ukraine", City = "Donetsk" },
                //    new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
                //    new Department(){ Id = 3, Country = "France", City = "Paris" },
                //    new Department(){ Id = 4, Country = "UK", City = "London" }
                //};
                //foreach (var item in departments)
                //{
                //    var parametres = new { Country = item.Country, City = item.City };
                //    string sql = "insert into Department values(@Country,@City)";
                //    conn.Query(sql, parametres);
                //}
                //List<Employee> employees = new List<Employee>()
                //{
                //    new Employee(){ Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age =22, DepId = 2 },
                //    new Employee(){ Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33,DepId = 1 },
                //    new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age =43, DepId = 3 },
                //    new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22,DepId = 2 },
                //    new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36,DepId = 4 },
                //    new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22,DepId = 2 },
                //    new Employee() { Id = 7, FirstName = "Nikita", LastName = " Krotov ", Age =27,DepId = 4 }
                // };
                //foreach (var item in employees)
                //{
                //    var parametres = new { FirstName = item.FirstName, LastName = item.LastName, Age = item.Age, DepId = item.DepId};
                //    string sql = "insert into Employee values(@FirstName,@LastName,@Age,@DepId)";
                //    conn.Query(sql, parametres);
                //}
                Task2(conn);
                Console.ReadLine();
            }
        }

        static void Task1(DbConnection conn)
        {
            Console.WriteLine("-----------------------1-------------------------------");
            var par = new { Age = 25 };
            string sql = "select * from Person where Age>@Age";
            var rez = conn.Query<Person>(sql,par);
            foreach (Person item in rez)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Age} - {item.City}");
            }
            Console.WriteLine("-----------------------2-------------------------------");
            var par1 = new { City = "Kyiv" };
            string sql1 = "select * from Person where City<>@City";
            var rez1 = conn.Query<Person>(sql1, par1);
            foreach (Person item in rez1)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Age} - {item.City}");
            }
            Console.WriteLine("-----------------------3-------------------------------");
            var par2 = new { City = "Kyiv" };
            string sql2 = "select * from Person where City = @City";
            var rez2 = conn.Query<Person>(sql2, par2);
            foreach (Person item in rez2)
            {
                Console.WriteLine($"{item.Id} - {item.Name}");
            }
            Console.WriteLine("-----------------------4-------------------------------");
            var par3 = new { Age = 35, Name = "Sergey" };
            string sql3 = "select * from Person where Age>@Age and Name = @Name";
            var rez3 = conn.Query<Person>(sql3, par3);
            foreach (Person item in rez3)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Age} - {item.City}");
            }
            Console.WriteLine("-----------------------5-------------------------------");
            var par4 = new { City = "Kryvyi Rih" };
            string sql4 = "select * from Person where City = @City";
            var rez4 = conn.Query<Person>(sql4, par4);
            foreach (Person item in rez4)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Age} - {item.City}");
            }
        }
        static  void Task2(DbConnection conn)
        {
            Console.WriteLine("-----------------------1-------------------------------");
            var param = new { City = "Donetsk", Country = "Ukraine" };
            string sql = "select e.FirstName, e.LastName " +
                "from Employee as e join Department as d on e.Depid = d.Id " +
                "where d.Country = @Country and d.City <> @City";
            var rez =  conn.Query(sql,param).ToList();
            foreach (var item in rez)
            {
                Console.WriteLine($"{item.FirstName} - {item.LastName}");
            }
            Console.WriteLine("-----------------------2-------------------------------");
            string sql1 = "select distinct d.Country from Department as d";
            var rez1 = conn.Query(sql1);
            foreach (var item in rez1)
            {
                Console.WriteLine($"{item.Country}");
            }
            Console.WriteLine("-----------------------3-------------------------------");
            var param2 = new { Age = 25};
            string sql2 = "select top (3) e.FirstName, e.LastName from Employee as e where e.Age > @Age";
            var rez2 = conn.Query(sql2, param2);
            foreach (var item in rez2)
            {
                Console.WriteLine($"{item.FirstName} - {item.LastName}");
            }
            Console.WriteLine("-----------------------4-------------------------------");
            var param3 = new { City = "Kyiv", Age = 23 };
            string sql3 = "select e.FirstName, e.LastName, e.Age, d.City from Employee as e join Department as d on e.Depid = d.Id " +
                "where e.Age>@Age and d.City = @City";
            var rez3 = conn.Query(sql3,param3).ToList();
            foreach (var item in rez3)
            {
                Console.WriteLine($"{item.FirstName} - {item.LastName} - {item.Age} - {item.City}");
            }

        }
    }
}

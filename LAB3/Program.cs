using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3
{
    internal class Program
    {
        SqlConnection connection = null;
        SqlDataReader reader = null;

        public Program()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
        }

        void Task1(string query, params SqlParameter[] pars)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            if (pars.Length != 0)
            {
                foreach (var par in pars)
                {
                    cmd.Parameters.Add(par);
                }
            }
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine(reader.GetValue(i).ToString() + "\t");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
            finally
            {
                connection?.Close();
                reader?.Close();
            }
        }

        void Task2(string StoredProcedure, params SqlParameter[] pars)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = StoredProcedure;
            if (pars.Length != 0)
            {
                foreach (var par in pars)
                {
                    cmd.Parameters.Add(par);
                }
            }
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
            finally
            {
                connection?.Close();
                reader?.Close();
            }
        }

        static void Main(string[] args)
        {
            string sqlT1 = "select CONCAT(c.FIRST_NAME,' ',c.LAST_NAME) as 'Client name' from CLIENTS_BOOKS " +
                "as cb join BOOKS as b on cb.ID_BOOK = b.ID_BOOK join CLIENTS as c on c.ID_CLIENT=cb.ID_CLIENT " +
                "where b.NAME = @nameT1;";
            string sqlT2 = "select CONCAT(a.FIRST_NAME,' ',a.LAST_NAME) as 'Autor name' " +
                "from AUTORS_BOOKS as ab " +
                "join BOOKS as b on ab.ID_BOOK = b.ID_BOOK " +
                "join AUTORS as a on a.ID_AUTOR = ab.ID_AUTOR " +
                "where b.NAME = @nameT2;";
            string sqlT3 = "select b.[NAME] as 'Book name' " +
                "from CLIENTS_BOOKS as cb " +
                "join BOOKS as b on cb.ID_BOOK = b.ID_BOOK " +
                "where cb.DATE_OF_ISSUE >= @currentdate;";
            string sqlT4 = "select b.[NAME], b.PRICE " +
                "from CLIENTS_BOOKS as cb " +
                "join BOOKS as b on cb.ID_BOOK = b.ID_BOOK " +
                "join CLIENTS as c on c.ID_CLIENT = cb.ID_CLIENT " +
                "where c.FIRST_NAME = @firstName and c.LAST_NAME = @lastName;";
            string sqlT5 = "select b.[NAME] as 'Book name' " +
                "from BOOKS as b " +
                "where b.Date_Of_Publish <= @currentdate and b.PRICE >= @currentprise;";

            SqlParameter parameterT1 =     new SqlParameter("@nameT1", System.Data.SqlDbType.NVarChar) { Value = "Война и мир" };
            SqlParameter parameterT2 =     new SqlParameter("@nameT2", System.Data.SqlDbType.NVarChar) { Value = "Война и мир" };
            SqlParameter parameterT3 =     new SqlParameter("@currentdate", System.Data.SqlDbType.Date) { Value = DateTime.Now };
            SqlParameter parameterT4_1 =   new SqlParameter("@firstName", System.Data.SqlDbType.NVarChar) { Value = "SomeClient 1" };
            SqlParameter parameterT4_2 =   new SqlParameter("@lastName", System.Data.SqlDbType.NVarChar) { Value = "LastName 3" };
            SqlParameter parameterT5_1 =   new SqlParameter("@currentdate", System.Data.SqlDbType.Date) { Value = DateTime.Now };
            SqlParameter parameterT5_2 =   new SqlParameter("@currentprise", System.Data.SqlDbType.Money) { Value = 20.000 };
            SqlParameter parameterT6 = new SqlParameter("@countbook", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
            SqlParameter parameterT7_1 = new SqlParameter("@authorFirstName", System.Data.SqlDbType.NVarChar) { Value = "SomeAutor 3" };
            SqlParameter parameterT7_2 = new SqlParameter("@authorLastName", System.Data.SqlDbType.NVarChar) { Value = "LastName 3" };
            SqlParameter parameterT7_3 = new SqlParameter("@sumPrices", System.Data.SqlDbType.Money) { Direction = System.Data.ParameterDirection.Output };
            SqlParameter parameterT7_4 = new SqlParameter("@countPages", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };

            Program program = new Program();
            program.Task1(sqlT1, parameterT1);
            program.Task1(sqlT2, parameterT2);
            program.Task1(sqlT3, parameterT3);
            program.Task1(sqlT4, parameterT4_1, parameterT4_2);
            program.Task1(sqlT5, parameterT5_1, parameterT5_2);

            program.Task2("CountBook", parameterT6);
            Console.WriteLine(parameterT6.Value.ToString());
            program.Task2("CountSumPages", parameterT7_1,parameterT7_2,parameterT7_3,parameterT7_4);
            Console.WriteLine(parameterT7_3.Value.ToString());
            Console.WriteLine(parameterT7_4.Value.ToString());

        }
    }
}

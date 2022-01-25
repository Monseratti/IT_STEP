using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        SqlConnection connection = null;
        SqlDataReader reader = null;

        public Program()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
        }
        
        void ViewIsDebtors() //t1
        {
            try
            {
                connection.Open();
                SqlCommand execProcedure = new SqlCommand("isDebtorProc",connection);
                execProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                execProcedure.Parameters.Add("@IsDebtor",System.Data.SqlDbType.Bit).Value = true;
                reader = execProcedure.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(1) + " " + reader.GetString(2));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                connection?.Close();
                reader?.Close();
            }
        }

        void ViewAutors()  //t2
        {
            try
            {
                connection.Open();
                string sql =
                    "select a.FIRST_NAME, a.LAST_NAME from AUTORS as a join AUTORS_BOOKS as ab on ab.ID_AUTOR = a.ID_AUTOR " +
                    "join BOOKS as b on b.ID_BOOK = ab.ID_BOOK " +
                    "where b.ID_BOOK = 3;";
                SqlCommand comm1 = new SqlCommand(sql, connection);
                reader = comm1.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0) + " " + reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                connection?.Close();
                reader?.Close();
            }
        }

        void ViewAvailableBookAndBookInSecondClient() //t3-4
        {
            try
            {
                connection.Open();
                string sql =
                    "select b.[NAME] from BOOKS as b where b.ID_BOOK not in (select cb.ID_BOOK from CLIENTS_BOOKS as cb);" +
                    "select b.[NAME] from BOOKS as b join CLIENTS_BOOKS as cb on cb.ID_BOOK = b.ID_BOOK where cb.ID_CLIENT = 2;";
                SqlCommand comm1 = new SqlCommand(sql, connection);
                reader = comm1.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(reader.GetString(i) + "\t");
                        }
                    }
                } while (reader.NextResult());
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                connection?.Close();
                reader?.Close();
            }
        }

        void LastTwoWeeksTakeBook() //t5
        {
            
         try
            {
                connection.Open();
                string sql =
                    "select distinct b.[NAME] from BOOKS as b" +
                    " join CLIENTS_BOOKS as cb on cb.ID_BOOK = b.ID_BOOK where DateDIFF(week, cb.Date_Of_Issue, GETDATE()) <= 2";
                SqlCommand comm1 = new SqlCommand(sql, connection);
                reader = comm1.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(reader.GetString(i) + "\t");
                        }
                    }
                } while (reader.NextResult());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                connection?.Close();
                reader?.Close();
            }
        }

        void UpdateDebtors() //t6
        {
            try
            {
                connection.Open();
                string sql =
                    "update dbo.CLIENTS set IS_DEBTOR = 0;";
                SqlCommand comm1 = new SqlCommand(sql, connection);
                comm1.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                connection?.Close();
                reader?.Close();
            }
        }

        void Task7()
        {
            try
            {
                connection.Open();
                SqlCommand execProcedure = new SqlCommand("select * from AllbookLastYear(@clientID)", connection);
                execProcedure.Parameters.Add("@clientID", System.Data.SqlDbType.Int).Value = 2;
                
                            Console.WriteLine(execProcedure.ExecuteScalar().ToString());
     
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                connection?.Close();
                reader?.Close();
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.ViewIsDebtors();
            program.ViewAutors();
            program.ViewAvailableBookAndBookInSecondClient();
            program.LastTwoWeeksTakeBook();
            program.UpdateDebtors();
            Console.WriteLine();
            program.Task7();

        }
    }
}

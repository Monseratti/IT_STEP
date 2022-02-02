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
            string cs = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            const string AsyncEnabled = "Asynchronous Processing=true";
            if (!cs.Contains(AsyncEnabled))
            {
                cs = String.Format($"{cs}; {AsyncEnabled}");
            }
            connection = new SqlConnection(cs);
        }
        
        async void ViewIsDebtors() //t1
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)  await connection.OpenAsync();
                SqlCommand execProcedure = new SqlCommand("isDebtorProc",connection);
                execProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                execProcedure.Parameters.Add("@IsDebtor",System.Data.SqlDbType.Bit).Value = true;
                reader = await execProcedure.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(await reader.GetFieldValueAsync<string>(1) + " " + await reader.GetFieldValueAsync<string>(2));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
        }

        async Task ViewAutors()  //t2
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open) await connection.OpenAsync();
                string sql =
                    "select a.FIRST_NAME, a.LAST_NAME from AUTORS as a join AUTORS_BOOKS as ab on ab.ID_AUTOR = a.ID_AUTOR " +
                    "join BOOKS as b on b.ID_BOOK = ab.ID_BOOK " +
                    "where b.ID_BOOK = 3;";
                SqlCommand comm1 = new SqlCommand(sql, connection);
                reader = await comm1.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(await reader.GetFieldValueAsync<string>(0) + " " + await reader.GetFieldValueAsync<string>(1));
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
                reader?.Close();
            }
        }

        async Task ViewAvailableBookAndBookInSecondClient() //t3-4
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open) await connection.OpenAsync();
                string sql =
                    "select b.[NAME] from BOOKS as b where b.ID_BOOK not in (select cb.ID_BOOK from CLIENTS_BOOKS as cb);" +
                    "select b.[NAME] from BOOKS as b join CLIENTS_BOOKS as cb on cb.ID_BOOK = b.ID_BOOK where cb.ID_CLIENT = 2;";
                SqlCommand comm1 = new SqlCommand(sql, connection);
                reader = await comm1.ExecuteReaderAsync();
                do
                {
                    while (await reader.ReadAsync())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(await reader.GetFieldValueAsync<string>(i) + "\t");
                        }
                    }
                } while (await reader.NextResultAsync());
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                reader?.Close();
            }
        }

        async void LastTwoWeeksTakeBook() //t5
        {
            
         try
            {
                if (connection.State != System.Data.ConnectionState.Open) await connection.OpenAsync();
                string sql =
                    "select distinct b.[NAME] from BOOKS as b" +
                    " join CLIENTS_BOOKS as cb on cb.ID_BOOK = b.ID_BOOK where DateDIFF(week, cb.Date_Of_Issue, GETDATE()) <= 2";
                SqlCommand comm1 = new SqlCommand(sql, connection);
                reader = await comm1.ExecuteReaderAsync();
                do
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(await reader.GetFieldValueAsync<string>(i) + "\t");
                        }
                    }
                } while (await reader.NextResultAsync());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                reader?.Close();
            }
        }

        async void UpdateDebtors() //t6
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open) await connection.OpenAsync();
                string sql =
                    "update dbo.CLIENTS set IS_DEBTOR = 0;";
                SqlCommand comm1 = new SqlCommand(sql, connection);
                await comm1.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                reader?.Close();
            }
        }

        void Task7()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open) connection.Open();
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
                reader?.Close();
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            Task task = Task.Run(() => program.ViewIsDebtors());
            task.Wait();
            Task task1 = Task.Run(() => program.ViewAutors());
            task1.Wait();
            Task task2 = Task.Run(() => program.ViewAvailableBookAndBookInSecondClient());
            task2.Wait();
            Task task3 = Task.Run(() => program.LastTwoWeeksTakeBook());
            task3.Wait();
            Task task4 = Task.Run(() => program.UpdateDebtors());
            task4.Wait();
            Task task5 = Task.Run(() => program.Task7());
            task5.Wait();
            Console.ReadLine();
        }
    }
}

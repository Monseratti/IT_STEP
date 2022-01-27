using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB6
{
    internal class Program
    {
        DataSet ShopDB = new DataSet();
        DataTable Sales = new DataTable() { TableName = "Sales" };
        DataTable Customers = new DataTable() { TableName = "Customers" };
        DataTable Sellers = new DataTable() { TableName = "Sellers" };

        public Program()
        {
            DataColumn ID_Sales = new DataColumn()
            {
                ColumnName = "ID",
                DataType = Type.GetType("System.Int32"),
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false
            };
            DataColumn ID_Customers = new DataColumn()
            {
                ColumnName = "ID",
                DataType = Type.GetType("System.Int32"),
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false
            };
            DataColumn ID_Sellers = new DataColumn()
            {
                ColumnName = "ID",
                DataType = Type.GetType("System.Int32"),
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false
            };
            DataColumn FirstName_Sellers = new DataColumn("FirstName", Type.GetType("System.String"));
            DataColumn LastName_Sellers = new DataColumn("LastName", Type.GetType("System.String"));
            DataColumn FirstName_Customers = new DataColumn("FirstName", Type.GetType("System.String"));
            DataColumn LastName_Customers = new DataColumn("LastName", Type.GetType("System.String"));

            DataColumn IDSeller = new DataColumn("IDSeller", Type.GetType("System.Int32"));
            DataColumn IDCustomer = new DataColumn("IDCustomer", Type.GetType("System.Int32"));
            DataColumn DateOfSale = new DataColumn("DateOfSale", Type.GetType("System.DateTime"));
            DataColumn Price = new DataColumn("Price", Type.GetType("System.Decimal"));

            Sellers.Columns.Add(ID_Sellers);
            Sellers.Columns.Add(FirstName_Sellers);
            Sellers.Columns.Add(LastName_Sellers);
            Sellers.PrimaryKey = new DataColumn[] { Sellers.Columns["ID"] };

            Customers.Columns.Add(ID_Customers);
            Customers.Columns.Add(FirstName_Customers);
            Customers.Columns.Add(LastName_Customers);
            Customers.PrimaryKey = new DataColumn[] { Customers.Columns["ID"] };

            Sales.Columns.Add(ID_Sales);
            Sales.Columns.Add(IDCustomer);
            Sales.Columns.Add(IDSeller);
            Sales.Columns.Add(DateOfSale);
            Sales.Columns.Add(Price);
            Sales.PrimaryKey = new DataColumn[] { Sales.Columns["ID"] };

            ForeignKeyConstraint FKSales_Sellers = new ForeignKeyConstraint("FKSales_Sellers", Sellers.Columns["ID"], Sales.Columns["IDSeller"]);
            ForeignKeyConstraint FKSales_Customers = new ForeignKeyConstraint("FKSales_Customers", Customers.Columns["ID"], Sales.Columns["IDCustomer"]);

            Sales.Constraints.Add(FKSales_Sellers);
            Sales.Constraints.Add(FKSales_Customers);

            Sellers.Rows.Add(new object[] { null, "Serhii", "Ruban" });
            Sellers.Rows.Add(new object[] { null, "Tetiana", "Ruban" });
            Sellers.Rows.Add(new object[] { null, "Anna", "Drozdova" });
            Sellers.Rows.Add(new object[] { null, "Andrii", "Drozdov" });

            Customers.Rows.Add(new object[] { null, "Ivan", "Ivanenko" });
            Customers.Rows.Add(new object[] { null, "Petro", "Petrenko" });
            Customers.Rows.Add(new object[] { null, "Mykyta", "Kozhemyaka" });
            Customers.Rows.Add(new object[] { null, "Oleksii", "Pugach" });

            Sales.Rows.Add(new object[] { null, 1, 2, new DateTime(2019, 01, 15), 50d });
            Sales.Rows.Add(new object[] { null, 1, 3, new DateTime(2019, 02, 12), 100d });
            Sales.Rows.Add(new object[] { null, 2, 1, new DateTime(2019, 01, 25), 75d });
            Sales.Rows.Add(new object[] { null, 4, 2, new DateTime(2018, 12, 24), 60d });
            Sales.Rows.Add(new object[] { null, 4, 1, new DateTime(2018, 11, 18), 35d });

            ShopDB.Tables.Add(Sales);
            ShopDB.Tables.Add(Customers);
            ShopDB.Tables.Add(Sellers);

        }

        void ViewShopDB()
        {
            Console.WriteLine("Tables in Data Set:" + ShopDB.Tables.Count);
            Console.WriteLine("Tables in Data Set:");
            foreach (DataTable item in ShopDB.Tables)
            {
                Console.WriteLine(item.TableName);
            }
            foreach (DataTable item in ShopDB.Tables)
            {
                Console.WriteLine($"--------------{item.TableName}--------------");
                foreach (DataColumn col in item.Columns)
                {
                    Console.Write($"\t{col.ColumnName}");
                }
                    Console.Write("\n");
                foreach (DataRow row in item.Rows)
                {
                    foreach (var obj in row.ItemArray)
                    {
                        Console.Write("\t{0}", obj);
                    }
                    Console.Write("\n");
                }
                Console.Write("\n");

            }
            foreach (DataRow row in ShopDB.Tables["Sellers"].Rows)
            {
                Console.WriteLine($"Seller:{row.ItemArray[1]} {row.ItemArray[2]}");
                foreach (DataRow rows in ShopDB.Tables["Sales"].Rows)
                {
                    if (row.ItemArray[0].ToString() == rows.ItemArray[2].ToString())
                    {
                        Console.WriteLine($"{rows.ItemArray[1]}\t{rows.ItemArray[3]}\t{rows.ItemArray[4]}");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.ViewShopDB();
            Console.ReadLine();
        }
    }
}

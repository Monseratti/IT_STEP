using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB4
{
    public partial class Form1 : Form
    {
        DataSet ds = null;
        SqlDataAdapter adapter = null;

        public Form1()
        {
            InitializeComponent();
            string sql = "SELECT * FROM CUSTOMER;" +
                "SELECT * FROM SALE;" +
                "SELECT * FROM SALER;";
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-RCR1OM0\SQLEXPRESS;Initial Catalog=Sales;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            ds = new DataSet();
            adapter = new SqlDataAdapter(sql,sqlConnection);
            adapter.TableMappings.Add("Table", "Customer");
            adapter.TableMappings.Add("Table1", "Sale");
            adapter.TableMappings.Add("Table2", "Saler");
            adapter.Fill(ds);
            foreach (DataTable item in ds.Tables)
            {
                comboBox1.Items.Add(item.TableName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex !=-1)
            {
                
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        dataGridView1.DataSource = ds.Tables[0];
                            break;
                    case 1:
                        dataGridView1.DataSource = ds.Tables[1];
                        break;
                    case 2:
                        dataGridView1.DataSource = ds.Tables[2];
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.Update(ds);
        }
    }
}

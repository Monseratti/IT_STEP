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

namespace LAB5
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=DESKTOP-RCR1OM0\SQLEXPRESS;Initial Catalog=Academy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) return;
            string sql = textBox1.Text;
            try
            {
                adapter = new SqlDataAdapter(sql, conn);
                ds = new DataSet();
                adapter.Fill(ds);
                foreach (DataTable item in ds.Tables)
                {
                    TabPage tab = new TabPage()
                    {
                        Name = item.TableName,
                        Text = item.TableName
                    };
                    tabControl1.Controls.Add(tab);
                    ListBox listBox = new ListBox()
                    {
                        Width = 800,
                        Height = 600
                    };
                    string colname = "";
                    foreach (DataColumn col in item.Columns)
                    {
                        colname += col.ColumnName + "\t";
                    }
                    listBox.Items.Add(colname);
                    foreach (DataRow row in item.Rows)
                    {
                        string rowitem = "";
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            rowitem += row.ItemArray[i].ToString() + "\t";
                        }
                        listBox.Items.Add(rowitem);
                    }

                    tab.Controls.Add(listBox);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

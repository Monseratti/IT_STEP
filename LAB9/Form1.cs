using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;

namespace LAB9
{
    public partial class Form1 : Form
    {
        DbConnection conn = null;
        DataTable dt = null;
        DbProviderFactory factory = null;
        string sql = "";
        string sql1 = "";
        string cs = "";
        public Form1()
        {
            InitializeComponent();
            dt = DbProviderFactories.GetFactoryClasses();
            foreach (DataRow item in dt.Rows)
            {
                comboBox1.Items.Add(item[2].ToString());
            }
            
        }
        private string GetConnectionStringByProviderName(string invariantName)
        {
            string connStr = "";
            try
            {
                ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

                foreach (ConnectionStringSettings item in settings)
                {
                    if (item.ProviderName == invariantName)
                    {
                        connStr = item.ConnectionString;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return connStr;
        }


        private async void button1_Click(object sender, EventArgs e)
        {

            sql = "WAITFOR DELAY '00:00:05'; select NAME from Books";
            sql1 = "WAITFOR DELAY '00:00:05'; select CONCAT(FIRST_NAME, LAST_NAME) from AUTORS";
            DbCommand cmd = conn.CreateCommand(); cmd.CommandText = sql;
            await conn.OpenAsync();
            DbDataReader reader = await cmd.ExecuteReaderAsync();
            int count1 = 0;
            while (await reader.ReadAsync())
            {
                count1 += reader.GetString(0).Length;
            }
            textBox1.Text = count1.ToString();
            reader.Close();
            DbCommand cmd1 = conn.CreateCommand(); cmd1.CommandText = sql1;
            DbDataReader reader1 = await cmd1.ExecuteReaderAsync();
            int count2 = 0;
            while (await reader1.ReadAsync())
            {
                count2 += reader1.GetString(0).Length;
            }
            textBox2.Text = count2.ToString();
            reader1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                cs = GetConnectionStringByProviderName(comboBox1.SelectedItem.ToString());
                factory = DbProviderFactories.GetFactory(comboBox1.SelectedItem.ToString());
                const string AsyncEnabled = "Asynchronous Processing=true";
                if (!cs.Contains(AsyncEnabled))
                {
                    cs = String.Format($"{cs}; {AsyncEnabled}");
                }
                conn = factory.CreateConnection();
                conn.ConnectionString = cs;
            }
        }
    }
}

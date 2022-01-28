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

namespace LAB9
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;
        DataTable dt = null;
        string sql = "";
        string sql1 = "";
        string cs = "";
        public Form1()
        {
            InitializeComponent();
            cs = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            const string AsyncEnabled = "Asynchronous Processing=true";
            if (!cs.Contains(AsyncEnabled))
            {
                cs = String.Format($"{cs}; {AsyncEnabled}");
            }
            conn = new SqlConnection(cs);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            sql = "WAITFOR DELAY '00:00:05'; select NAME from Books";
            sql1 = "WAITFOR DELAY '00:00:05'; select CONCAT(FIRST_NAME, LAST_NAME) from AUTORS";
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            int count1 = 0;
            while (await reader.ReadAsync())
            {
                count1 += reader.GetString(0).Length;
            }
            textBox1.Text = count1.ToString();
            reader.Close();
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            SqlDataReader reader1 = await cmd1.ExecuteReaderAsync();
            int count2 = 0;
            while (await reader1.ReadAsync())
            {
                count2 += reader1.GetString(0).Length;
            }
            textBox2.Text = count2.ToString();
            reader1.Close();
        }
    }
}

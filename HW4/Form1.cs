using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW4
{
    public partial class Form1 : Form
    {
        SqlDataAdapter adapter, adapter1, adapter2, adapter3;
        DataSet ds, ds1,ds2,ds3;
        SqlConnection conn;
        SqlCommand addStudents, addTeachers, addLectureRooms, addLectures;

      public Form1()
        {
            InitializeComponent();
            string sql = "select * from students";
            string sql1 = "select * from teachers";
            string sql2 = "select * from lecturerooms";
            string sql3 = "select * from lectures";
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Academy"].ConnectionString);
                adapter = new SqlDataAdapter(sql, conn);
                adapter1 = new SqlDataAdapter(sql1, conn);
                adapter2 = new SqlDataAdapter(sql2, conn);
                adapter3 = new SqlDataAdapter(sql3, conn);
                ds = new DataSet();
                ds1 = new DataSet();
                ds2 = new DataSet();
                ds3 = new DataSet();
                adapter.Fill(ds);
                adapter1.Fill(ds1);
                adapter2.Fill(ds2);
                adapter3.Fill(ds3);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView2.DataSource = ds1.Tables[0];
                dataGridView3.DataSource = ds2.Tables[0];
                dataGridView4.DataSource = ds3.Tables[0];
                addStudents = new SqlCommand("addStudents", conn);
                addStudents.CommandType = CommandType.StoredProcedure;
                addStudents.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name_St");
                addStudents.Parameters.Add("@Surname", SqlDbType.NVarChar, 50, "Surname_St");

                addTeachers = new SqlCommand("addTeachers", conn);
                addTeachers.CommandType = CommandType.StoredProcedure;
                addTeachers.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name_Teach");
                addTeachers.Parameters.Add("@Surname", SqlDbType.NVarChar, 50, "Surname_Teach");

                addLectureRooms = new SqlCommand("addLRs", conn);
                addLectureRooms.CommandType = CommandType.StoredProcedure;
                addLectureRooms.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name_LR");

                addLectures = new SqlCommand("addLectures", conn);
                addLectures.CommandType = CommandType.StoredProcedure;
                addLectures.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name_Lecture");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adapter.InsertCommand = addStudents;
            adapter.Update(ds);
            adapter1.InsertCommand = addTeachers;
            adapter1.Update(ds1);
            adapter2.InsertCommand = addLectureRooms;
            adapter2.Update(ds2);
            adapter3.InsertCommand = addLectures;
            adapter3.Update(ds3);
        }
    }
}

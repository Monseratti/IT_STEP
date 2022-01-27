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
using System.IO;
using System.Drawing.Imaging;

namespace HW5
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PictureStore"].ConnectionString);
        SqlDataAdapter adapter;
        DataSet ds;
        string filename = "";
        byte[] bytes;
        public Form1()
        {
            InitializeComponent();
            adapter = new SqlDataAdapter("select * from PictureStore", conn);
            ds = new DataSet();
            adapter.Fill(ds);
            List<string> list = new List<string>();

            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                list.Add(rows.ItemArray[1].ToString());
            }

            comboBox1.DataSource = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
                bytes = CreateCopy();
            }
        }
        private byte[] CreateCopy()
        {
            Image img = Image.FromFile(filename);
            int maxWidth = 300, maxHeight = 300;
            //размеры выбраны произвольно
            double ratioX = (double)maxWidth / img.Width;
            double ratioY = (double)maxHeight / img.Height;
            double ratio = Math.Min(ratioX, ratioY);
            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);
            Image mi = new Bitmap(newWidth, newHeight);
            pictureBox1.Image = mi;
            //рисунок в памяти
            Graphics g = Graphics.FromImage(mi);
            g.DrawImage(img, 0, 0, newWidth, newHeight);
            MemoryStream ms = new MemoryStream();
            //поток для ввода|вывода байт из памяти
            mi.Save(ms, ImageFormat.Jpeg);
            ms.Flush();//выносим в поток все данные
                       //из буфера
            ms.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(ms);
            byte[] buf = br.ReadBytes((int)ms.Length);
            return buf;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                foreach (DataRow rows in ds.Tables[0].Rows)
                {
                    if ((string)comboBox1.SelectedItem == rows.ItemArray[1].ToString())
                    {
                        MemoryStream ms = new MemoryStream((byte[])rows.ItemArray[2]);
                        Image mi = Image.FromStream(ms);
                        pictureBox2.Image = mi;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("insert into PictureStore(name_PS, picture) values (@name, @picture); ", conn);
                comm.Parameters.Add("@name", SqlDbType.NVarChar, 255).
                Value = filename;
                comm.Parameters.Add("@picture", SqlDbType.Image, bytes.Length).
                Value = bytes;
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image Files (*.bmp, *.jpg)|*.bmp;*.jpg";
            if (saveFileDialog.ShowDialog()== DialogResult.OK)
            {
                pictureBox2.Image.Save(saveFileDialog.FileName);
                MessageBox.Show("File save");
            }
        }
    }
}

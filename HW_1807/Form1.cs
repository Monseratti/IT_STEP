using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_1807
{
    public partial class Form1 : Form
    {
        //HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run
        RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
        string safeName = "";
        public Form1()
        {
            InitializeComponent();
            listView1.Columns.Add("Name", -2);
            listView1.Columns.Add("Path", -2);
            listView1.FullRowSelect = true;
            Update();
        }
        new void Update()
        {
            string[] vnames = rk.GetValueNames();
            listView1.Items.Clear();
            foreach (var item in vnames)
            {
                try
                {
                    string name = "";
                    if (item == name) name = "(default)";
                    else name = item;
                    ListViewItem viewItem = new ListViewItem(name);
                    viewItem.SubItems.Add(rk.GetValue(item).ToString());
                    listView1.Items.Add(viewItem);
                }
                catch (Exception)
                {
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opd = new OpenFileDialog())
            {
                if (opd.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = opd.FileName;
                    safeName = opd.SafeFileName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                rk.SetValue(safeName, textBox1.Text, RegistryValueKind.String);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                try
                {
                    rk.DeleteValue(listView1.SelectedItems[0].Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

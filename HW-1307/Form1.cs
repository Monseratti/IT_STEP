using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_1307
{
    public partial class Form1 : Form
    {
        string mutexName = "MyMutex";
        Mutex mt = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (Mutex.OpenExisting(mutexName) != null)
                {
                    MessageBox.Show("This application is already open");
                    this.Close();
                }
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                mt = new Mutex(true,mutexName);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mt.ReleaseMutex();
            }
            catch (Exception error)
            {
            }
        }
    }
}

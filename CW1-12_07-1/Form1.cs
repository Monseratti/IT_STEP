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

namespace CW1_12_07_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            object key = new object();
            Car[] cars = new Car[2]
            {
                new Car(true,10),
                new Car(false,10)
            };
            Thread thread = new Thread(() => cars[0].Run(pictureBox1, pictureBox2, key)){ IsBackground = true};
            Thread thread2 = new Thread(() => cars[1].Run(pictureBox3, pictureBox2, key)) { IsBackground = true};
            thread.Start();
            thread2.Start();

        }
    }
}

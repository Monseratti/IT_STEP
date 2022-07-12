using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW1_12_07_1
{
    public class Car
    {
        public bool IsLeft { get; set; }
        public int Speed { get; set; }

        public Car(bool isLeft, int speed)
        {
            IsLeft = isLeft;
            Speed = speed;
        }
        public void Moove(PictureBox picture)
        {
            if (picture.InvokeRequired)
            {
                picture.Invoke(new Action(()=>Moove(picture)));
            }
            else
            {
                if (IsLeft)                picture.Left += Speed;
                else                       picture.Left -= Speed;
            }
        }
        public void Run(PictureBox picture, PictureBox box, object obj)
        {
            while (true)
            {

                while ((picture.Location.X + picture.Width) >= box.Location.X && picture.Location.X <= (box.Location.X + box.Width))
                {
                    lock (obj)
                    {
                        do
                        {
                            Moove(picture);
                            Thread.Sleep(10);
                        } while ((picture.Location.X + picture.Width) >= box.Location.X && picture.Location.X <= (box.Location.X + box.Width));
                    }
                }
                Moove(picture);
                Thread.Sleep(10);
            }
        }
    }
}

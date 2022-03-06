using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace SP_HW7
{
    static public class MyMonitor
    {
        static int ID { get; set; }
        static MyMonitor()
        {
            ID = -1;
        }
        static public void Request(MyPrinter p)
        {
            if (p.ID == -1)
            {
                ID++;
                p.ID = ID;
                p.Print();
            }
        }
        static public void Release(MyPrinter p)
        {
            if (p.ID != -1)
            {
                ID--;
                p.ID = -1;
            }
        }
        static public void Lock(MyPrinter p)
        {
            try
            {
                Request(p);
            }
            finally
            {
                Release(p);
            }
        }
    }
}

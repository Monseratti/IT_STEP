using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(AutosalonContext autos = new AutosalonContext())
            {
                autos.Marks.First().Countries.Add(autos.Countries.First());
                autos.SaveChanges();
            }
        }
    }

}

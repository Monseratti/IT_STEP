using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_User
{
    public class User
    {
        public string Name { get; set; }
        public string Mark { get; set; }
        public int GameID { get; set; }
        public int GamePort { get; set; }
    }

    public class Game
    {
        public int Id { get; set; }
        public int GamePort { get; set; }
        public User User_One { get; set; }
        public User User_Two { get; set; }
    }
}

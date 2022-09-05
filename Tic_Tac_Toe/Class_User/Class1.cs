using System;
using System.Net;
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
        public string UserIP { get; set; }
        public int UserPort { get; set; }
        public int GameID { get; set; }
    }

    public class Game
    {
        public int Id { get; set; }
        public int GamePort { get; set; }
        public User User_One { get; set; }
        public User User_Two { get; set; }
        public List<string> Fields { get; set; }
        public Game()
        {
            Fields = new List<string>
            {
                "_",//11-0
                "_",//12-1
                "_",//13-2
                "_",//21-3
                "_",//22-4
                "_",//23-5
                "_",//31-6
                "_",//32-7
                "_",//33-8
            };
        }

        public string CheckWin()
        {
            string win;
            if ((Fields[0] == User_One.Mark && Fields[1] == User_One.Mark && Fields[2] == User_One.Mark) ||
                (Fields[3] == User_One.Mark && Fields[4] == User_One.Mark && Fields[5] == User_One.Mark) ||
                (Fields[6] == User_One.Mark && Fields[7] == User_One.Mark && Fields[8] == User_One.Mark) ||
                (Fields[0] == User_One.Mark && Fields[3] == User_One.Mark && Fields[6] == User_One.Mark) ||
                (Fields[1] == User_One.Mark && Fields[4] == User_One.Mark && Fields[7] == User_One.Mark) ||
                (Fields[2] == User_One.Mark && Fields[5] == User_One.Mark && Fields[8] == User_One.Mark) ||
                (Fields[0] == User_One.Mark && Fields[4] == User_One.Mark && Fields[8] == User_One.Mark) ||
                (Fields[2] == User_One.Mark && Fields[4] == User_One.Mark && Fields[6] == User_One.Mark)) win = $"{User_One.Name} win";
            else if ((Fields[0] == User_Two.Mark && Fields[1] == User_Two.Mark && Fields[2] == User_Two.Mark) ||
                (Fields[3] == User_Two.Mark && Fields[4] == User_Two.Mark && Fields[5] == User_Two.Mark) ||
                (Fields[6] == User_Two.Mark && Fields[7] == User_Two.Mark && Fields[8] == User_Two.Mark) ||
                (Fields[0] == User_Two.Mark && Fields[3] == User_Two.Mark && Fields[6] == User_Two.Mark) ||
                (Fields[1] == User_Two.Mark && Fields[4] == User_Two.Mark && Fields[7] == User_Two.Mark) ||
                (Fields[2] == User_Two.Mark && Fields[5] == User_Two.Mark && Fields[8] == User_Two.Mark) ||
                (Fields[0] == User_Two.Mark && Fields[4] == User_Two.Mark && Fields[8] == User_Two.Mark) ||
                (Fields[2] == User_Two.Mark && Fields[4] == User_Two.Mark && Fields[6] == User_Two.Mark)) win = $"{User_Two.Name} win";
            else win = string.Empty;
            return win;
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SP_LAB4
{
    [Serializable]
    internal class Bank
    {
        public int money;
        public int percent;
        public string name;

        public int GetMoney() { return money; }
        public int GetPrecent() { return percent; }
        public string GetName() { return name; }

        public void SetMoney(int money) { this.money = money;          ThreadSer(); }
        public void SetPercent(int percent) { this.percent = percent; ThreadSer(); }
        public void SetName(string name) { this.name = name; ThreadSer(); }
        public Bank(int m, int p, string n)
        {
            this.money = m;
            this.percent = p;
            this.name = n;
        }
        private void Serialize()
        {
            using (StreamWriter sw = new StreamWriter("log.json"))
            {
                JsonSerializer js = new JsonSerializer();
                js.NullValueHandling = NullValueHandling.Ignore;
                using (JsonTextWriter jsw = new JsonTextWriter(sw))
                {
                    js.Serialize(jsw, this);
                }
            }
        }
        private void ThreadSer()
        {
            ThreadStart ps = new ThreadStart(Serialize);
            Thread thread = new Thread(ps);
            thread.Start();
            thread.Join();
        }
    }
}

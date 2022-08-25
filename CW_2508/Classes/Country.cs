using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_2508.Classes
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class LocalNames
    {
        public string tr { get; set; }
        public string ca { get; set; }
        public string de { get; set; }
        public string uk { get; set; }
        public string fr { get; set; }
        public string lb { get; set; }
        public string et { get; set; }
        public string ar { get; set; }
        public string lt { get; set; }
        public string cs { get; set; }
        public string zh { get; set; }
        public string hr { get; set; }
        public string hu { get; set; }
        public string ru { get; set; }
        public string nl { get; set; }
        public string pl { get; set; }
        public string ro { get; set; }
        public string ja { get; set; }
        public string sr { get; set; }
        public string io { get; set; }
        public string eo { get; set; }
        public string es { get; set; }
        public string ko { get; set; }
        public string be { get; set; }
        public string az { get; set; }
        public string yi { get; set; }
        public string en { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }

        public override string ToString()
        {
            return $"{name}, {state}, {country}";
        }
    }
}

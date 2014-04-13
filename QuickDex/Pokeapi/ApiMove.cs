using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDex.Pokeapi
{
    public class ApiMove
    {
        public int accuracy { get; set; }
        public string category { get; set; }
        public DateTime created { get; set; }
        public string description { get; set; }
        public int id { get; set; }
        public DateTime modified { get; set; }
        public string name { get; set; }
        public int power { get; set; }
        public int pp { get; set; }
        public string resource_uri { get; set; }
    }

}

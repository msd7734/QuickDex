using System;
using System.Linq;
using System.Collections.Generic;

namespace QuickDex.Pokeapi
{
    /// <summary>
    /// Auto-generated class to deserialize Pokeapi JSON objects.
    /// Represents all of the entries of the NationalDex, including pointers to
    /// all pokemon contained therein.
    /// </summary>
    public class ApiPokedex
    {
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string name { get; set; }
        public Pokemon[] pokemon { get; set; }
        public string resource_uri { get; set; }

        public override string ToString()
        {
            return name + " (Pokedex):" +
                "\ncreated: " + created.ToString() +
                "\nmodified: " + modified.ToString() +
                "\nresource_uri: " + resource_uri + 
                "\n# Pokemon: " + pokemon.Length.ToString();
        }
    }

    public class Pokemon
    {
        public string name { get; set; }
        public string resource_uri { get; set; }
        public int national_id
        {
            get
            {
                List<string> list = resource_uri.Split('/').ToList();
                int id = -1;
                id = int.Parse(list.FirstOrDefault<string>(x => int.TryParse(x, out id)));
                return id;
            }

            set
            {
                national_id = value;
            }
        }
    }

}

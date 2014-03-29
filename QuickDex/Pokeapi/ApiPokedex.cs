using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        [JsonProperty("Pokemon")]
        public BasePokemon[] pokemon { get; set; }
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
}

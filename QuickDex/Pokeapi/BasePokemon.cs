using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickDex.Pokeapi
{
    /// <summary>
    /// Class for holding the most basic information necessary to a Pokemon.
    /// BasePokemon are loaded from an ApiPokedex when it's deserialized and
    /// can act as references to look up the more detailed ApiPokemon objects.
    /// </summary>
    public class BasePokemon
    {
        public string name { get; set; }
        public string resource_uri { get; set; }
        public int national_id
        {
            get
            {
                //maybe find a way to store this instaed of calculating every time
                //(it may make searching slow when using the Pokedex)
                List<string> list = resource_uri.Split('/').ToList();
                int id = -1;
                id = int.Parse(list.FirstOrDefault<string>(x => int.TryParse(x, out id)));
                return id;
            }
        }
    }
}

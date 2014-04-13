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
        //BasePokemon's members are backed by private fields because having
        //only properties will cause an infinite loop when trying to instantiate
        //a national_id by parsing the resource_uri.
        private string _name;
        private string _resource_uri;
        private int _national_id;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string resource_uri {
            get { return _resource_uri; }
            
            set
            {
                List<string> list = value.Split('/').ToList();
                int id = -1;
                id = int.Parse(list.FirstOrDefault<string>(x => int.TryParse(x, out id)));
                //TODO: MalformedResUriException? It would extend ArgumentException but is it really
                //always an argument?
                if (id == null)
                    throw new ArgumentException("The resource uri " + value + " was invalid.");
                _resource_uri = value;
                _national_id = id;
            }
        }

        public int national_id
        {
            get { return _national_id; }
            set { _national_id = value; }
        }
    }
}

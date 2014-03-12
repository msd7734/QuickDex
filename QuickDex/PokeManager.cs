using System;
using System.Collections.Generic;
using System.Linq;
using QuickDex.Pokeapi;

namespace QuickDex
{
    /// <summary>
    /// Manage calls that access the data contained in an ApiPokedex.
    /// </summary>
    public class PokeManager
    {

        #region Pokemon Name Aliases
        //Valid pokemon name searches that map to oddly named pokemon data from the API
        private static readonly Dictionary<string, List<string>> pokeNameAliases
            = new Dictionary<string, List<string>>()
        {
            { "nidoran-f", new List<string>() { "nidoran f", "nidoran(f)", "nidoran (f)", "nidoran♀" }},
            { "nidoran-m", new List<string>() { "nidoran m", "nidoran(m)", "nidoran (m)", "nidoran♂", "nidoran"}},
            { "mr-mime", new List<string>() { "mr.mime", "mr. mime", "mr mime" }},
            { "deoxys-normal", new List<string>() { "deoxys" }},
            { "wormadam-plant", new List<string>() { "wormadam" }},
            { "mime-jr", new List<string>() { "mime jr", "mime jr." }},
            { "porygon-z", new List<string>() { "porygon z" }},
            { "giratina-altered", new List<string>() { "giratina" }},
            { "shaymin-land", new List<string>() { "shaymin" }},
            { "basculin-red-striped", new List<string>() { "basculin" }},
            { "darmanitan-standard", new List<string>() { "darmanitan" }},
            { "tornadus-standard", new List<string>() { "tornadus" }},
            { "thundurus-incarnate", new List<string>() { "thundurus" }},
            { "landorus-incarnate", new List<string>() { "landorus" }},
            { "keldeo-incarnate", new List<string>() { "keldeo", }},
            { "meloetta-aria", new List<string>() { "meloetta" }},
            { "meowstic-male", new List<string>() { "meowstic" }},
            { "pumpkaboo-average", new List<string>() { "pumpkaboo", }},
            { "gourgeist-average", new List<string>() { "gourgeist" }}
        };

        private static readonly Dictionary<string, string> aliasToApiPokeName
            = ReverseNameAliasDict(pokeNameAliases);
        #endregion

        ApiPokedex pokedex;

        public PokeManager(ApiPokedex pokedex)
        {
            this.pokedex = pokedex;
        }

        /// <summary>
        /// Return a Pokemon name in English given its National Dex id.
        /// </summary>
        /// <param name="id">Id of Pokemon</param>
        /// <returns>A valid name if id is found, null otherwise.</returns>
        public string GetNameById(int id)
        {
            try
            {
                string res = pokedex.pokemon
                .First<Pokemon>(p => p.national_id == id)
                .name;
                return res;
            }
            catch (InvalidOperationException ioe)
            {
                //Couldn't find a pokemon with this id
                return null;
            }
        }

        /// <summary>
        /// Return a National Dex Pokemon id based on an English name
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns>A valid int id if pokemon name is found, null otherwise.</returns>
        public int? GetIdByName(string pokemonName)
        {
            try
            {
                //Create list of both raw pokemon names (directly from the API) and aliases
                var rawNames = pokedex.pokemon.Select<Pokemon,string>(p => p.name);
                var validNames = new List<string>(rawNames);
                validNames.AddRange( pokeNameAliases.Values.SelectMany<List<string>,string>(x => x) );

                string lower = pokemonName.ToLower();

                //If the lowercase given name is an alias, set it to the raw name
                if (aliasToApiPokeName.Keys.Contains(lower))
                    lower = aliasToApiPokeName[lower];

                int res = pokedex.pokemon
                    .First<Pokemon>(p => p.name == lower)
                    .national_id;

                return res;
            }
            catch (InvalidOperationException ioe)
            {
                //Couldn't find pokemon with this lowercase name
                return null;
            }
        }

        #region Private methods
        private static Dictionary<string,string> ReverseNameAliasDict(Dictionary<string, List<string>> dict)
        {
            var res = new Dictionary<string,string>();

            var newKeys = dict.Values.SelectMany<List<string>, string>(x => x).Distinct();
            var oldKeys = dict.Keys.ToList<string>();

            foreach (var newKey in newKeys)
            {
                string newVal = oldKeys.First( x => dict[x].Contains(newKey) );
                res.Add(newKey, newVal);
            }

            return res;
        }
        #endregion
    }
}

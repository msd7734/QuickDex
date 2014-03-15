using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDex.Pokeapi
{
    public struct ApiAliases
    {
        /// <summary>
        /// Valid pokemon name searches that map to oddly named pokemon data from the API.
        /// This can also be useful for pokemon with special characters in their names, even if
        /// they don't have an incorrect name in the API.
        /// </summary>
        public static readonly Dictionary<string, List<string>> PokeNameAliases
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
            { "tornadus-incarnate", new List<string>() { "tornadus" }},
            { "thundurus-incarnate", new List<string>() { "thundurus" }},
            { "landorus-incarnate", new List<string>() { "landorus" }},
            { "keldeo-incarnate", new List<string>() { "keldeo", }},
            { "meloetta-aria", new List<string>() { "meloetta" }},
            { "meowstic-male", new List<string>() { "meowstic" }},
            { "pumpkaboo-average", new List<string>() { "pumpkaboo", }},
            { "gourgeist-average", new List<string>() { "gourgeist" }}
        };

        /// <summary>
        /// A Dictionary where each key is a valid internal name alias, mapped to a value
        /// equal to its API equivalent.
        /// </summary>
        public static readonly Dictionary<string, string> AliasesToApiPokeName
            = ReverseNameAliasDict(PokeNameAliases);

        /// <summary>
        /// Map each Alias from PokeNameAliases dictionary to its own key and set its
        /// value to its API Pokemon name.
        /// </summary>
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
    }
}

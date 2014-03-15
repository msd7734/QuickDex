using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickDex.Pokeapi;

namespace QuickDex
{

    class BulbapediaReferrer : ISearchStrategy
    {
        private PokeManager manager;
        private bool? lastSearchSuccess;


        #region Pokemon name aliases
        //For mapping abnormal names to counterparts used by Bulbapedia URLs
        private static readonly Dictionary<string, string> pokeNameAlias
            = new Dictionary<string, string>()
        {
            { "nidoran-f", "Nidoran♀" },
            { "nidoran-m", "Nidoran♂" },
            { "mr-mime", "Mr. Mime" },
            { "deoxys-normal", "Deoxys" },
            { "wormadam-plant", "Wormadam" },
            { "mime-jr", "Mime Jr." },
            { "giratina-altered", "Giratina" },
            { "shaymin-land", "Shaymin" },
            { "basculin-red-striped", "Basculin" },
            { "darmanitan-standard", "Darmanitan" },
            { "tornadus-incarnate", "Tornadus" },
            { "thundurus-incarnate", "Thundurus" },
            { "landorus-incarnate", "Landorus" },
            { "keldeo-incarnate", "Keldeo" },
            { "meloetta-aria", "Meloetta" },
            { "meowstic-male", "Meowstic" },
            { "pumpkaboo-average", "Pumpkaboo" },
            { "gourgeist-average", "Gourgeist" }
        };
        #endregion

        /// <summary>
        /// Construct a SearchStrategy for serebii.com
        /// </summary>
        /// <param name="manager">The PokeManager to handle search operations</param>
        public BulbapediaReferrer(PokeManager manager)
        {
            this.manager = manager;
            lastSearchSuccess = null;
        }

        public string GetName()
        {
            return "Bulbapedia";
        }

        public bool? IsLastSearchSuccess()
        {
            return lastSearchSuccess;
        }

        public string GotoPokemonEntry(int dexNum, PokeGeneration gen)
        {
            //can only get pages on BP by pokemon name, so need to perform a lookup 
            string pkmName = manager.GetNameById(dexNum);
            string paddedDexNum = Util.To3DigitStr(dexNum);

            if (pkmName != null)
            {
                //alias name to one Bulbapedia expects if there's an alias rule for it
                List<string> apiNames = new List<string>(pokeNameAlias.Keys);
                if (apiNames.Contains(pkmName))
                    pkmName = pokeNameAlias[pkmName];

                string url = "http://bulbapedia.bulbagarden.net/wiki/" + pkmName.ToLower();
                Process.Start(url);
                lastSearchSuccess = true;
                return "Opening " + pkmName + "\'s entry on Bulbapedia.";
            }
            else
            {
                lastSearchSuccess = false;
                return "A Pokemon of ID #" + paddedDexNum + " was not found.";
            }
        }

        public string GotoPokemonEntry(string pokemon, PokeGeneration gen)
        {
            //pokemon name can be all lowercase or have first letter capitalized
            int? pkmId = manager.GetIdByName(pokemon);

            if (pkmId != null)
            {
                //map internal alias to external API name
                if (ApiAliases.AliasesToApiPokeName.ContainsKey(pokemon))
                    pokemon = ApiAliases.AliasesToApiPokeName[pokemon];

                //alias name to one Bulbapedia expects if there's an alias rule for it
                if (pokeNameAlias.ContainsKey(pokemon))
                    pokemon = pokeNameAlias[pokemon];

                string paddedDexNum = Util.To3DigitStr((int)pkmId);
                string url = "http://bulbapedia.bulbagarden.net/wiki/" + pokemon.ToLower();
                Process.Start(url);
                lastSearchSuccess = true;
                return "Opening " + pokemon + "\'s entry on Bulbapedia.";
            }
            else
            {
                lastSearchSuccess = false;
                return "A Pokemon with the name " + pokemon + " was not found.";
            }
        }


    }
}

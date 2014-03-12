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

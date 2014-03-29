using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickDex.Pokeapi;

namespace QuickDex
{
    public class SerebiiReferrer : ISearchStrategy
    {
        #region Generation Name Mapping
        private static readonly Dictionary<PokeGeneration, string> GEN_URL_MAP
            = new Dictionary<PokeGeneration, string>
            {
                {PokeGeneration.RBY, "pokedex"},
                {PokeGeneration.GSC, "pokedex"},
                {PokeGeneration.RSE, "pokedex-rs"},
                {PokeGeneration.DPP, "pokedex-dp"},
                {PokeGeneration.BW, "pokedex-bw"},
                {PokeGeneration.XY, "pokedex-xy"}
            };
        #endregion

        PokeManager manager;
        bool? lastSearchSuccess;

        /// <summary>
        /// Construct a SearchStrategy for serebii.com
        /// </summary>
        /// <param name="manager">The PokeManager to handle search operations</param>
        public SerebiiReferrer(PokeManager manager)
        {
            this.manager = manager;
            lastSearchSuccess = null;
        }

        public string GetName()
        {
            return "Serebii";
        }

        public bool? IsLastSearchSuccess()
        {
            return lastSearchSuccess;
        }

        public string GotoPokemonEntry(int dexNum, PokeGeneration gen)
        {
            string pkmName = manager.GetNameById(dexNum);
            string paddedDexNum = Util.To3DigitStr(dexNum);

            if (pkmName != null)
            {
                gen = Util.ValidateGeneration(dexNum, gen);
                string url = "http://www.serebii.net/" + GEN_URL_MAP[gen] + "/" + paddedDexNum + ".shtml";
                Process.Start(url);
                lastSearchSuccess = true;
                return "Opening " + pkmName + "\'s entry on Serebii.";
            }
            else
            {
                lastSearchSuccess = false;
                return "A Pokemon of ID #" + paddedDexNum + " was not found.";
            }
            
        }

        public string GotoPokemonEntry(string pokemon, PokeGeneration gen)
        {
            //Serebii's pages are based on National Dex #, so need to look up from given pokemon name
            int? pkmId = manager.GetIdByName(pokemon);
            
            if (pkmId != null)
            {
                gen = Util.ValidateGeneration((int)pkmId, gen);
                string paddedDexNum = Util.To3DigitStr((int)pkmId);
                string url = "http://www.serebii.net/" + GEN_URL_MAP[gen] + "/" + paddedDexNum + ".shtml";
                Process.Start(url);
                lastSearchSuccess = true;
                return "Opening " + pokemon + "\'s entry on Serebii.";
            }
            else
            {
                lastSearchSuccess = false;
                return "A Pokemon with the name " + pokemon + " was not found.";
            }
        }
    }
}

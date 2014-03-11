using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuickDex
{
    class SerebiiReferrer : ISearchStrategy
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

        private Cache cache;

        /// <summary>
        /// Construct a SearchStrategy for serebii.com
        /// </summary>
        /// <param name="dataCache">The cache of pokemon data to use in search operations</param>
        public SerebiiReferrer(Cache dataCache)
        {
            cache = dataCache;
        }

        public string GetName()
        {
            return "Serebii";
        }

        public string GotoPokemonEntry(int dexNum, PokeGeneration gen)
        {
            string paddedDexNum = Util.To3DigitStr(dexNum);
            string url = "http://www.serebii.net/" + GEN_URL_MAP[gen] + "/" + paddedDexNum + ".shtml";
            Process.Start(url);
            return "Success";
        }

        public string GotoPokemonEntry(string pokemon, PokeGeneration gen)
        {
            //Serebii's pages are based on National Dex #, so need to look up from given pokemon name
            throw new NotImplementedException();
        }
    }
}

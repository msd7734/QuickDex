using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickDex.Pokeapi;

namespace QuickDex
{

    class BulbapediaReferrer : ISearchStrategy
    {
        private PokeManager manager;

        /// <summary>
        /// Construct a SearchStrategy for serebii.com
        /// </summary>
        /// <param name="dataCache">The cache of pokemon data to use in search operations</param>
        public BulbapediaReferrer(PokeManager manager)
        {
            this.manager = manager;
        }

        public string GetName()
        {
            return "Bulbapedia";
        }

        public string GotoPokemonEntry(int dexNum, PokeGeneration gen)
        {
            //can only get pages on BP by pokemon name, so need to perform a lookup 
            throw new NotImplementedException();
        }

        public string GotoPokemonEntry(string pokemon, PokeGeneration gen)
        {
            //pokemon name can be all lowercase or have first letter capitalized
            throw new NotImplementedException();
        }
    }
}

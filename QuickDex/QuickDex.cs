using System;
using System.Collections.Generic;
using QuickDex.Pokeapi;

namespace QuickDex
{
    /// <summary>
    /// Local search on Pokemon information. Causes no redirect, instead returns
    /// strings with formatted Pokemon information entirely pulled from local cache
    /// or API (if uncached)
    /// </summary>
    public class QuickDex : ISearchStrategy
    {
        private ApiPokedex pokedex;

        /// <summary>
        /// Construct a SearchStrategy for serebii.com
        /// </summary>
        /// <param name="dataCache">The cache of pokemon data to use in search operations</param>
        public QuickDex(ApiPokedex pokedex)
        {
            this.pokedex = pokedex;
        }

        public string GetName()
        {
            return "QuickDex";
        }

        public string GotoPokemonEntry(int dexNum, PokeGeneration gen)
        {
            throw new NotImplementedException();
        }

        public string GotoPokemonEntry(string pokemon, PokeGeneration gen)
        {
            throw new NotImplementedException();
        }
    }
}

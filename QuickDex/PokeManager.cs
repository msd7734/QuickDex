using System;
using System.Collections.Generic;
using System.Linq;
using QuickDex.Pokeapi;

namespace QuickDex
{
    /// <summary>
    /// Manage calls that access the data contained in an ApiPokedex.
    /// </summary>
    class PokeManager
    {
        ApiPokedex pokedex;

        public PokeManager(ApiPokedex pokedex)
        {
            this.pokedex = pokedex;
        }


    }
}

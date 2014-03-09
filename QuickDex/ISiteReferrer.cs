﻿using System;
using System.Text;

namespace QuickDex
{
    /// <summary>
    /// Class to perform Pokemon lookup on third party sites.
    /// </summary>
    interface ISiteReferrer
    {
        /// <summary>
        /// Return a name describing the type of referrer or its destination site
        /// </summary>
        /// <returns>string</returns>
        string GetName();

        /// <summary>
        /// Open a webpage to view pokemon data from a given generation.
        /// </summary>
        /// <param name="dexNum">National Dex # of pokemon</param>
        /// <param name="gen">Generation of pokemon games</param>
        void GotoPokemonEntry(int dexNum, PokeGeneration gen);

        //TODO: Because certain sites use different url schemes, sometimes we want a pokedex # and sometimes a name
        //  We should overload GotoPokemonEntry to take a string as well as a dex #
        //  The Pokedex class will have the responsibility of looking up pokemon information (and thus translating here)
        //  It was intended only to be used as the backend for the UI's functions and for efficiency reasons (maintaining cache)
        //  So we have a couple solutions to let our ISiteReferrers use it:
        //  - Make Pokedex a singleton: Good IF it will be responsible for loading the cache and doing complex lookup
        //  - Pass an instantiated Pokedex as ref to the constructors in subclasses of ISiteReferrer: GOOD to avoid singleton

        /// <summary>
        /// Open a webpage to view pokemon data from a given generation.
        /// </summary>
        /// <param name="pokemon">Name of pokemon </param>
        /// <param name="gen"></param>
        void GotoPokemonEntry(string pokemon, PokeGeneration gen);

        //TODO: Define "Goto"s for lookup on type, attack, etc.
    }
}

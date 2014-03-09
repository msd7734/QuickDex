using System;
using System.Diagnostics;

namespace QuickDex
{

    class BulbapediaReferrer : ISiteReferrer
    {
        public BulbapediaReferrer() { }

        public string GetName()
        {
            return "Bulbapedia";
        }

        public void GotoPokemonEntry(int dexNum, PokeGeneration gen)
        {
            //can only get pages on BP by pokemon name, so need to perform a lookup 
            throw new NotImplementedException();
        }

        public void GotoPokemonEntry(string pokemon, PokeGeneration gen)
        {
            //pokemon name can be all lowercase or have first letter capitalized
            throw new NotImplementedException();
        }
    }
}

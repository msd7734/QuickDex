using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDex
{
    class SmogonReferrer : ISearchStrategy
    {

        private PokeManager manager;
        private bool? lastSearchSuccess;

        public SmogonReferrer(PokeManager manager)
        {
            this.manager = manager;
            lastSearchSuccess = null;
        }

        public string GetName()
        {
            return "Smogon";
        }

        public bool? IsLastSearchSuccess()
        {
            return lastSearchSuccess;
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

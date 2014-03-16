using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickDex.Pokeapi;

namespace QuickDex
{
    class PokemonDbReferrer : ISearchStrategy
    {
        //TODO: Add pokeNameAlias dictionary to map special names. Pkm with spaces/special chars
        //in their names use the API name, but alternate forms do not.

        PokeManager manager;
        bool? lastSearchSuccess;

        public PokemonDbReferrer(PokeManager manager)
        {
            this.manager = manager;
            lastSearchSuccess = null;
        }

        public string GetName()
        {
            return "Pokemon DB";
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
                string url = "http://pokemondb.net/pokedex/" + pkmName;
                Process.Start(url);
                lastSearchSuccess = true;
                return "Opening " + pkmName + "\'s entry on PokemonDB.";
            }
            else
            {
                lastSearchSuccess = false;
                return "A Pokemon of ID #" + paddedDexNum + " was not found.";
            }
        }

        public string GotoPokemonEntry(string pokemon, PokeGeneration gen)
        {
            //PokemonDB seems to be backed by the Pokeapi, so it will use all the API pkm names
           
            string lower = pokemon.ToLower();
            string apiName;

            if (ApiAliases.AliasesToApiPokeName.ContainsKey(lower))
                apiName = ApiAliases.AliasesToApiPokeName[lower];
            else
                apiName = pokemon;

            int? dexNum = manager.GetIdByName(pokemon);

            if (dexNum != null)
            {
                string url = "http://pokemondb.net/pokedex/" + apiName;
                Process.Start(url);
                lastSearchSuccess = true;
                return "Opening " + pokemon + "\'s entry on PokemonDB.";
            }
            else
            {
                lastSearchSuccess = false;
                return "A Pokemon with the name " + pokemon + " was not found.";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickDex.Pokeapi;

namespace QuickDex
{
    class PokemonDbReferrer : ISearchStrategy
    {
        //For mapping abnormal names to counterparts used by PokemonDB URLs
        private static readonly Dictionary<string, string> pokeNameAlias
            = new Dictionary<string, string>()
        {
            { "deoxys-normal", "Deoxys" },
            { "wormadam-plant", "Wormadam" },
            { "giratina-altered", "Giratina" },
            { "shaymin-land", "Shaymin" },
            { "basculin-red-striped", "Basculin" },
            { "darmanitan-standard", "Darmanitan" },
            { "tornadus-incarnate", "Tornadus" },
            { "thundurus-incarnate", "Thundurus" },
            { "landorus-incarnate", "Landorus" },
            { "keldeo-incarnate", "Keldeo" },
            { "meloetta-aria", "Meloetta" },
            { "meowstic-male", "Meowstic" },
            { "pumpkaboo-average", "Pumpkaboo" },
            { "gourgeist-average", "Gourgeist" }
        };

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
            //Exception: Pokemon with alternate forms (Basculin, Deoxys, etc.) use different names
           
            string lower = pokemon.ToLower();
            string apiName;

            //Get API-defined name if exists
            if (ApiAliases.AliasesToApiPokeName.ContainsKey(lower))
                apiName = ApiAliases.AliasesToApiPokeName[lower];
            else
                apiName = pokemon;

            int? dexNum = manager.GetIdByName(apiName);

            

            if (dexNum != null)
            {
                string url = "http://pokemondb.net/pokedex/";
                //Set actual name to alias of API name so it works with PokemonDB
                if (pokeNameAlias.ContainsKey(apiName))
                {
                    pokemon = pokeNameAlias[apiName];
                    url += pokemon;
                }
                else
                {
                    url += apiName;
                }

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

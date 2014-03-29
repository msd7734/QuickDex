using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickDex.Pokeapi;

namespace QuickDex
{
    class SmogonReferrer : ISearchStrategy
    {
        #region Generation Mapping
        private static readonly Dictionary<PokeGeneration, string> GEN_URL_MAP
            = new Dictionary<PokeGeneration, string>
            {
                {PokeGeneration.RBY, "rb"},
                {PokeGeneration.GSC, "gs"},
                {PokeGeneration.RSE, "rs"},
                {PokeGeneration.DPP, "dp"},
                {PokeGeneration.BW, "bw"},
                {PokeGeneration.XY, "bw"}  //Smogon doesn't yet support XY entries right now; defualt to BW
            };
        #endregion

        #region Pokemon name aliases
        //For mapping abnormal names to counterparts used by Bulbapedia URLs
        private static readonly Dictionary<string, string> pokeNameAlias
            = new Dictionary<string, string>()
        {
            { "mr-mime", "Mr_Mime" },
            { "deoxys-normal", "Deoxys" },
            { "wormadam-plant", "Wormadam" },
            { "mime-jr", "Mime_Jr" },
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
        #endregion

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
            string pkmName = manager.GetNameById(dexNum);
            string paddedDexNum = Util.To3DigitStr(dexNum);

            if (pkmName != null)
            {
                gen = Util.ValidateGeneration(dexNum, gen);
                
                string smogonName;
                if (pokeNameAlias.ContainsKey(pkmName))
                    smogonName = pokeNameAlias[pkmName];
                else
                    smogonName = pkmName;

                string url = "https://www.smogon.com/" + GEN_URL_MAP[gen] + "/pokemon/" + smogonName;               
                Process.Start(url);
                lastSearchSuccess = true;
                if (ApiAliases.PokeNameAliases.ContainsKey(pkmName))
                {
                    pkmName = ApiAliases.PokeNameAliases[pkmName][0];
                    pkmName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(pkmName);
                }
                return "Opening " + pkmName + "\'s entry on Smogon.";
            }
            else
            {
                lastSearchSuccess = false;
                return "A Pokemon of ID #" + paddedDexNum + " was not found.";
            }
        }

        public string GotoPokemonEntry(string pokemon, PokeGeneration gen)
        {
            int? pkmId = manager.GetIdByName(pokemon);

            string lower = pokemon.ToLower();
            string apiName;

            if (ApiAliases.AliasesToApiPokeName.ContainsKey(lower))
                apiName = ApiAliases.AliasesToApiPokeName[lower];
            else
                apiName = pokemon;

            if (pkmId != null)
            {
                gen = Util.ValidateGeneration((int)pkmId, gen);
                
                string smogonName;
                if (pokeNameAlias.ContainsKey(apiName))
                    smogonName = pokeNameAlias[apiName];
                else
                    smogonName = apiName;

                string url = "https://www.smogon.com/" + GEN_URL_MAP[gen] + "/pokemon/" + smogonName;
                Process.Start(url);
                lastSearchSuccess = true;
                return "Opening " + pokemon + "\'s entry on Smogon.";
            }
            else
            {
                lastSearchSuccess = false;
                return "A Pokemon with the name " + pokemon + " was not found.";
            }
        }
    }
}

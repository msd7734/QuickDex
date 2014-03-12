using System;
using System.Collections.Generic;
using System.Linq;
using QuickDex.Pokeapi;

namespace QuickDex
{
    /// <summary>
    /// Manage calls that access the data contained in an ApiPokedex.
    /// </summary>
    public class PokeManager
    {
        ApiPokedex pokedex;

        public PokeManager(ApiPokedex pokedex)
        {
            this.pokedex = pokedex;
        }

        /// <summary>
        /// Return a Pokemon name in English given its National Dex id.
        /// </summary>
        /// <param name="id">Id of Pokemon</param>
        /// <returns>A valid name if id is found, null otherwise.</returns>
        public string GetNameById(int id)
        {
            try
            {
                string res = pokedex.pokemon
                .First<Pokemon>(p => p.national_id == id)
                .name;
                return res;
            }
            catch (InvalidOperationException ioe)
            {
                //Couldn't find a pokemon with this id
                return null;
            }
        }

        /// <summary>
        /// Return a National Dex Pokemon id based on an English name
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns>A valid int id if pokemon name is found, null otherwise.</returns>
        public int? GetIdByName(string pokemonName)
        {
            try
            {
                string lower = pokemonName.ToLower();
                int res = pokedex.pokemon
                    .First<Pokemon>(p => p.name == lower)
                    .national_id;

                return res;
            }
            catch (InvalidOperationException ioe)
            {
                //Couldn't find pokemon with this lowercase name
                return null;
            }
        }
    }
}

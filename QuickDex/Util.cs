using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using QuickDex.Pokeapi;
using QuickDex.Exceptions;

namespace QuickDex
{
    class Util
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        //Mapping of lower and upper national id ranges for each generation
        public static IDictionary<PokeGeneration, Tuple<int, int>> GenerationRanges =
            new Dictionary<PokeGeneration, Tuple<int, int>>
            {
                { PokeGeneration.RBY, Tuple.Create(1, 151) },
                { PokeGeneration.GSC, Tuple.Create(152, 251) },
                { PokeGeneration.RSE, Tuple.Create(252, 386) },
                { PokeGeneration.DPP, Tuple.Create(387, 493) },
                { PokeGeneration.BW, Tuple.Create(494, 649) },
                { PokeGeneration.XY, Tuple.Create(650, 719) }
            };

        public delegate void VoidDelegate();
        
        private Util() { }

        /// <summary>
        /// Formats an integer into a minimum length 3 string with front-padded 0's
        /// </summary>
        /// <param name="i">int to format</param>
        /// <returns>String representation of i, padded with 0's if less than 3 digits</returns>
        public static string To3DigitStr(int i)
        {
            if (i.ToString().Length > 2)
                return i.ToString();
            else
                return string.Format("{0,0:D3}", i);
        }

        /// <summary>
        /// Formats an integer string to a minimum length of 3 with front-padded 0's
        /// </summary>
        /// <param name="str">String to format</param>
        /// <returns>String front-padded with 0's if less than 3 characters</returns>
        public static string To3DigitStr(string str)
        {
            //Doing it this way to force only numeric strings
            return To3DigitStr(int.Parse(str));
        }

        /// <summary>
        /// Check if a .cache file exists in this directory.
        /// </summary>
        /// <returns>True if .cache exists, false otherwise.</returns>
        public static bool CacheExists()
        {
            return (File.Exists(Cache.FileName));
        }

        /// <summary>
        /// Determine if a given pokemon falls within a given generation.
        /// </summary>
        /// <param name="nationalId">National Id of the pokemon to check</param>
        /// <param name="gen">PokeGeneration to check</param>
        /// <returns>True if nationalId falls within gen, false otherwise</returns>
        public static bool IsPokemonInGeneration(int nationalId, PokeGeneration gen)
        {
            int lower = 1;
            int upper = GenerationRanges[gen].Item2;
            return (nationalId >= lower && nationalId <= upper);
        }

        /// <summary>
        /// Determine if a given pokemon falls within a given generation.
        /// </summary>
        /// <param name="pokemon">BasePokemon to check</param>
        /// <param name="gen">PokeGeneration to check</param>
        /// <returns>True if pokemon's id falls within gen, false otherwise</returns>
        public static bool IsPokemonInGeneration(BasePokemon pokemon, PokeGeneration gen)
        {
            return IsPokemonInGeneration(pokemon.national_id, gen);
        }

        /// <summary>
        /// Get the earliest vaild generation for a given national ID
        /// </summary>
        /// <param name="nationalId">National ID to check</param>
        /// <returns>PokeGeneration enum</returns>
        public static PokeGeneration GetEarliestGeneration(int nationalId)
        {
            PokeGeneration mostRecentGen = GenerationRanges.Keys.Last<PokeGeneration>();

            if (nationalId > GenerationRanges[mostRecentGen].Item2)
                throw new BadNationalIdException();

            PokeGeneration res = PokeGeneration.RBY;
            int i = (int)res;
            while (!IsPokemonInGeneration(nationalId, res) && i < Enum.GetValues(typeof(PokeGeneration)).Length)
            {
                ++i;
                res = (PokeGeneration)i;
            }

            return res;
        }

        /// <summary>
        /// Get the earliest vaild generation for a given pokemon
        /// </summary>
        /// <param name="pokemon">BasePokemon to check</param>
        /// <returns>PokeGeneration enum</returns>
        public static PokeGeneration GetEarliestGeneration(BasePokemon pokemon)
        {
            return GetEarliestGeneration(pokemon.national_id);
            
        }

        /// <summary>
        /// Produce a valid pokemon generation from a national ID.
        /// </summary>
        /// <param name="nationalId">National ID to check</param>
        /// <param name="gen">PokeGeneration to check</param>
        /// <returns>Passed in gen if valid, the earliest valid generation otherwise.</returns>
        public static PokeGeneration ValidateGeneration(int nationalId, PokeGeneration gen)
        {
            if (!Util.IsPokemonInGeneration(nationalId, gen))
                return Util.GetEarliestGeneration(nationalId);
            else
                return gen;
        }

        /// <summary>
        /// Produce a valid pokemon generation from a BasePokemon.
        /// </summary>
        /// <param name="pokemon">BasePokemon whose national ID to check</param>
        /// <param name="gen">PokeGeneration to check</param>
        /// <returns>Passed in gen if valid, the earliest valid generation otherwise.</returns>
        public static PokeGeneration ValidateGeneration(BasePokemon pokemon, PokeGeneration gen)
        {
            return ValidateGeneration(pokemon.national_id, gen);
        }

        /// <summary>
        /// Returns true if the current application has focus, false otherwise
        /// Code courtesy of StackOverflow: http://stackoverflow.com/a/7162873
        /// </summary>
        public static bool ApplicationIsActivated()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;       // No window is currently activated
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }
    }
}

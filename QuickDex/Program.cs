using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using QuickDex.Pokeapi;


namespace QuickDex
{
    static class Program
    {

        [DllImport("kernel32")]
        static extern bool AllocConsole();

        [DllImport("kernel32")]
        static extern bool FreeConsole();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Cache myCache;
            ApiPokedex myPokedex;

            //check for presence of cache, then validate it
            //TODO: Validate cache with PRAGMA integrity_check
            //For more info, see: http://fmansoor.wordpress.com/2013/03/01/must-periodically-check-sqlite-db-integrity/
            //May also be prudent to compute hash on program exit and validte on next startup (as reg entry?)
            if (Util.CacheExists())
            {
                //read from cache to deserialize Pokedex (?)
                myCache = Cache.GetExistingCache();
                myPokedex = myCache.LoadPokedex();
            }
            else
            {
                //create new cache and persist Pokedex (incl. Pokemon)
                myCache = Cache.InitializeNewCache(true);
                myPokedex = PokeQuery.GetPokedex();
                myCache.CachePokedex(myPokedex);
            }

            List<ISearchStrategy> searchStrats = new List<ISearchStrategy>()
            {
                new QuickDex(myPokedex),
                new BulbapediaReferrer(myPokedex),
                new SerebiiReferrer(myPokedex)
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWnd(searchStrats));
            
            //Code for testing through the command line.

            /*
            AllocConsole();

            ApiPokedex pokedex = PokeQuery.GetPokedex();

            Console.WriteLine("Search for a pokemon by name: ");
            string pkmStr = Console.ReadLine();
            string pkmId = "";

            IList<Pokemon> list = new List<Pokemon>(pokedex.pokemon);
            try
            {
                string pkmStrLower = pkmStr.ToLower();

                pkmId = list.FirstOrDefault<Pokemon>(
                    x => x.name == pkmStrLower
                ).national_id.ToString();

                Console.WriteLine(pkmStr + "'s id is: " + Util.To3DigitStr(pkmId));
            }
            catch (NullReferenceException nre)
            {
                Console.WriteLine("The pokemon \"" + pkmStr + "\" was not found.");
            }

            Console.ReadKey();

            FreeConsole();
             * */
        }
    }
}

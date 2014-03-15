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
        /// </summary
        [STAThread]
        static void Main(string[] args)
        {
            Cache myCache;
            ApiPokedex myPokedex;
            PokeManager myManager;

            if (Util.CacheExists())
            {
                //read from cache to deserialize Pokedex
                myCache = Cache.GetExistingCache();

                if ((string)Properties.Settings.Default["Cache"] != myCache.ComputeMD5String())
                {
                    MessageBox.Show("Your cache seems to be invalid. A new one will now be built.", "Corrupted cache file");
                    myCache = Cache.InitializeNewCache(true);
                    myPokedex = PokeQuery.GetPokedex();
                    myCache.CachePokedex(myPokedex);
                }
                else
                    myPokedex = myCache.LoadPokedex();
            }
            else
            {
                //create new cache and persist Pokedex (incl. Pokemon)
                myCache = Cache.InitializeNewCache(true);
                myPokedex = PokeQuery.GetPokedex();
                myCache.CachePokedex(myPokedex);
            }

            myManager = new PokeManager(myPokedex);
                        
            List<ISearchStrategy> searchStrats = new List<ISearchStrategy>()
            {
                //new QuickDex(myManager),
                new BulbapediaReferrer(myManager),
                new SerebiiReferrer(myManager)
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWnd wnd = new MainWnd(searchStrats);

            using (Cache c = myCache)
            using (KeyHookManager hookManager = new KeyHookManager(wnd.ShortcutFormShow))
            {
                
                Application.Run(wnd);
            }

            /*
            Cache myCache;
            ApiPokedex myPokedex;
            PokeManager myManager;

            //check for presence of cache, then validate it
            //TODO: Validate cache with PRAGMA integrity_check
            //For more info, see: http://fmansoor.wordpress.com/2013/03/01/must-periodically-check-sqlite-db-integrity/
            //May also be prudent to compute hash on program exit and validte on next startup (as reg entry?)
            //TODO: CacheLoader form that can wrap Cache methods and provide a dialog with a message and load bar
            //Would give feedback to user when doing potentially slow cache operation
            if (Util.CacheExists())
            {
                //read from cache to deserialize Pokedex (?)
                myCache = Cache.GetExistingCache();
                //should we read all of this into memory? may be able to get away with querying as needed
                myPokedex = myCache.LoadPokedex();
            }
            else
            {
                //create new cache and persist Pokedex (incl. Pokemon)
                myCache = Cache.InitializeNewCache(true);
                myPokedex = PokeQuery.GetPokedex();
                myCache.CachePokedex(myPokedex);
            }

            myManager = new PokeManager(myPokedex);

            
            List<ISearchStrategy> searchStrats = new List<ISearchStrategy>()
            {
                new QuickDex(myManager),
                new BulbapediaReferrer(myManager),
                new SerebiiReferrer(myManager)
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWnd(searchStrats));
            */
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

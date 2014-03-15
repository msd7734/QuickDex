using System;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Threading;
using System.Security.Cryptography;
using System.Linq;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using System.Windows.Forms;
using QuickDex.Pokeapi;

namespace QuickDex
{
    /// <summary>
    /// SQLite implementation of the cache. This is absolutely essential
    /// to prevent the need for many calls to Pokeapi (locked at 300 per
    /// resource per day). 
    /// On disapose, save the MD5 on dispose to maintain cache integrity
    /// </summary>
    public class Cache : IDisposable
    {
        public static readonly string FileName = ".cache";

        private static readonly string CON_STR = "Data Source=" + FileName + ";Version=3;MultipleActiveResultSets=False";

        #region Stored queries
        private static readonly string initQuery =
            @"CREATE TABLE IF NOT EXISTS [Pokedex] (
                [name] TEXT NOT NULL PRIMARY KEY,
                [created] TEXT,
                [modified] TEXT,
                [resource_uri] TEXT
            );
            CREATE TABLE IF NOT EXISTS [Pokemon] (
                [national_id] INTEGER NOT NULL PRIMARY KEY,
                [name] TEXT NOT NULL,
                [resource_uri] TEXT
            );";

        private static readonly string insertPokedexQuery =
            "INSERT INTO Pokedex(name, created, modified, resource_uri)" +
            "VALUES (@dexName, @dexCreated, @dexModified, @dexResUri)";

        private static readonly string insertPokemonQuery =
            "INSERT INTO Pokemon(national_id, name, resource_uri)" +
            "VALUES (@pkmId, @pkmName, @pkmResUri)";

        private static readonly string selectPokedexQuery =
            "SELECT * FROM Pokedex";

        private static readonly string selectPokemonQuery =
            "SELECT * FROM Pokemon";
        #endregion

        #region Cache Column Index Dictionaries
        /// <summary>
        /// These dictionaries are meant to be used with an SQLiteDataReader and its Get[Datatype](int) methods
        /// </summary>
        private static readonly Dictionary<string, int> PokedexCol = new Dictionary<string, int>()
        {
            {"name", 0},
            {"created",1},
            {"modified",2},
            {"resource_uri",3}
        };

        private static readonly Dictionary<string, int> PokemonCol = new Dictionary<string, int>()
        {
            {"national_id",0},
            {"name",1},
            {"resource_uri",2}
        };
        #endregion

        protected Cache() { }

        /// <summary>
        /// Initialize a new Cache and return it.
        /// </summary>
        /// <param name="forceNew">If true, delete an old .cache file if there is one.</param>
        /// <returns></returns>
        public static Cache InitializeNewCache(bool forceNew)
        {
            Cache cache = new Cache();

            string conStr = CON_STR;
            if (forceNew)
                conStr += ";New=True";

            SQLiteConnection.CreateFile(FileName);
            using (SQLiteConnection con  = new SQLiteConnection(conStr))
            {
                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = initQuery;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return cache;
        }

        /// <summary>
        /// Get the cache for the QuickDex.
        /// </summary>
        /// <returns>A valid Cache object, or null if the .cache file doesn't exist.</returns>
        public static Cache GetExistingCache()
        {
            if (Util.CacheExists())
                return new Cache();
            else
                return null;
        }

        public string ComputeMD5String()
        {
            //Garbage collect to fix resource leak
            ForceDisposeSQLite();

            MD5 md5 = MD5.Create();
            try
            {
                using (var stream = File.OpenRead(FileName))
                {
                    var hash = md5.ComputeHash(stream);
                    return "qkdx:" + BitConverter.ToString(hash).Replace("-", "");
                }
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "Exception occurred while saving cache file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Properties.Settings.Default.Cache;
            }
            
        }

        /// <summary>
        /// Persist an ApiPokedex in the cache, including all its basic information on
        /// pokemon. Note that ApiPokemon and Pokemon objects are different; the former
        /// is a 1-to-1 mapping of a JSON object returned from the PokeAPI, the latter is
        /// encapsulates basic info and is contained in the ApiPokedex.
        /// </summary>
        /// <param name="pokedex">The ApiPokedex to cache.</param>
        public void CachePokedex(ApiPokedex pokedex)
        {
            using (SQLiteConnection con = new SQLiteConnection(CON_STR))
            {
                con.Open();

                using(SQLiteTransaction trans = con.BeginTransaction())
                using(SQLiteCommand insDexCmd = con.CreateCommand())
                using(SQLiteCommand insPkmCmd = con.CreateCommand())
                {
                    insDexCmd.CommandText = insertPokedexQuery;
                    SQLiteParameter dexName = new SQLiteParameter("@dexName", System.Data.DbType.String);
                    SQLiteParameter created = new SQLiteParameter("@dexCreated", System.Data.DbType.DateTime);
                    SQLiteParameter modified = new SQLiteParameter("@dexModified", System.Data.DbType.DateTime);
                    SQLiteParameter resUri = new SQLiteParameter("@dexResUri", System.Data.DbType.String);
                    dexName.Value = pokedex.name;
                    created.Value = pokedex.created;
                    modified.Value = pokedex.modified;
                    resUri.Value = pokedex.resource_uri;
                    insDexCmd.Parameters.Add(dexName);
                    insDexCmd.Parameters.Add(created);
                    insDexCmd.Parameters.Add(modified);
                    insDexCmd.Parameters.Add(resUri);
                    insDexCmd.ExecuteNonQuery();


                    insPkmCmd.CommandText = insertPokemonQuery;
                    SQLiteParameter pkmName = new SQLiteParameter("@pkmName", System.Data.DbType.String);
                    SQLiteParameter pkmId = new SQLiteParameter("@pkmId", System.Data.DbType.String);
                    SQLiteParameter pkmResUri = new SQLiteParameter("@pkmResUri", System.Data.DbType.String);

                    foreach (Pokemon pkm in new List<Pokemon>(pokedex.pokemon))
                    {
                        pkmName.Value = pkm.name;
                        pkmId.Value = pkm.national_id;
                        pkmResUri.Value = pkm.resource_uri;
                        insPkmCmd.Parameters.Add(pkmName);
                        insPkmCmd.Parameters.Add(pkmId);
                        insPkmCmd.Parameters.Add(pkmResUri);
                        insPkmCmd.ExecuteNonQuery();

                        insPkmCmd.Parameters.Clear();
                    }
                    trans.Commit();
                }
                con.Close();
            }
        }

        /// <summary>
        /// Deserialize a cached ApiPokedex.
        /// </summary>
        /// <returns>ApiPokedex object</returns>
        public ApiPokedex LoadPokedex()
        {
            ApiPokedex pokedex = new ApiPokedex();
            
            using(SQLiteConnection con = new SQLiteConnection(CON_STR))
            {
                con.Open();
                
                using (SQLiteCommand selDex = new SQLiteCommand(selectPokedexQuery, con))
                using (SQLiteCommand selPkm = new SQLiteCommand(selectPokemonQuery, con))
                {
                    SQLiteDataReader reader = selDex.ExecuteReader();

                    while(reader.Read())
                    {
                        pokedex.name = reader.GetString(PokedexCol["name"]);
                        pokedex.created = Convert.ToDateTime(
                            reader.GetDateTime(PokedexCol["created"])
                        );
                        pokedex.modified = Convert.ToDateTime(
                            reader.GetDateTime(PokedexCol["modified"])
                        );
                        pokedex.resource_uri = reader.GetString(PokedexCol["resource_uri"]);
                    }

                    reader = selPkm.ExecuteReader();
                    List<Pokemon> pkm = new List<Pokemon>();

                    while (reader.Read())
                    {
                        Pokemon p = new Pokemon();
                        //national_id is not stored, it's computed on each call from the resource_uri
                        //no need to set this but it may be slower, think of an alternative
                        //p.national_id = reader.GetInt32(PokemonCol["national_id"]);
                        p.name = reader.GetString(PokemonCol["name"]);
                        p.resource_uri = reader.GetString(PokemonCol["resource_uri"]);

                        pkm.Add(p);
                    }

                    pokedex.pokemon = pkm.ToArray<Pokemon>();
                }

                con.Close();
            }

            return pokedex;
        }

        public void Dispose()
        {
            //Save MD5 to validate cache on next application run
            string md5 = ComputeMD5String();
            Properties.Settings.Default.Cache = md5;
            Properties.Settings.Default.Save();
        }

        #region Private methods
        /// <summary>
        /// Fix SQLite resource leak before opening stream on Cache file.
        /// There are better solutions but this is the easiest for now.
        /// See: http://stackoverflow.com/questions/12532729/
        /// </summary>
        private void ForceDisposeSQLite()
        {
            GC.Collect();
            //Give the 'ol GC a few ms to catch up
            Thread.Sleep(50);
        }
        #endregion
    }
}

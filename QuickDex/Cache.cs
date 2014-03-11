using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using QuickDex.Pokeapi;

namespace QuickDex
{
    /// <summary>
    /// SQLite implementation of the cache. This is absolutely essential
    /// to prevent the need for many calls to Pokeapi (locked at 300 per
    /// resource per day). 
    /// IDEA: Make this disposable and save the MD5 on dispose to maintain cache integrity
    /// </summary>
    public class Cache
    {
        public static readonly string FileName = ".cache";

        private static readonly string CON_STR = "Data Source=" + FileName + ";Version=3";

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
                    SQLiteParameter created = new SQLiteParameter("@dexCreated", System.Data.DbType.String);
                    SQLiteParameter modified = new SQLiteParameter("@dexModified", System.Data.DbType.String);
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
            //not yet implemented
            return pokedex;
        }
    }
}

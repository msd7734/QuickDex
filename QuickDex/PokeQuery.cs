using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using QuickDex.Pokeapi;

namespace QuickDex
{
    /// <summary>
    /// Manage queries to the PokeAPI: http://pokeapi.co/
    /// Documentation at:
    /// http://pokeapi.co/docs/
    /// </summary>
    class PokeQuery
    {
        //Base string for query
        private static readonly string Q_BASE = @"http://pokeapi.co/api/v1/";

        protected PokeQuery() { }

        /// <summary>
        /// Query the API to get a Pokedex object.
        /// A Pokedex contains the names and resource_uri for all pokemon, in addition
        /// to some metadata of its own.
        /// </summary>
        /// <returns></returns>
        public static ApiPokedex GetPokedex()
        {
            //special resource call to get entire pokedex for a part
            string query = Q_BASE + "pokedex/1/";

            ApiPokedex pokedex = null;

            try
            {
                HttpWebResponse wr = DoRequest(query);
                string response = "";

                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                    pokedex = JsonConvert.DeserializeObject<ApiPokedex>(response);
                }
            }
            catch (NullReferenceException nre)
            {
                //Getting here simply means that DoRequest failed with an WebException.
                //For now, DoRequest handles its own failure but we might want to do something
                //here in the future other than just pass through to inform our caller.
            }

            return pokedex;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Pokmeon data based on National Dex number
        /// </summary>
        /// <param name="dexNum"></param>
        /// <returns>An ApiPokemon object from a deserialized JSON response</returns>
        public static ApiPokemon GetPokemon(int dexNum)
        {
            string query = Q_BASE + "pokemon/" + dexNum.ToString();

            ApiPokemon deserializedPoke = InternalGetPokemon(query);
            return deserializedPoke;
        }

        /// <summary>
        /// Get Pokemon data based on (English) name
        /// </summary>
        /// <param name="pokemon">English Pokemon name</param>
        /// <returns>An ApiPokemon object from a deserialized JSON response</returns>
        public static ApiPokemon GetPokemon(string pokemon)
        {
            //note than pokemon can only be searched for by name if it is in all lower case
            string query = Q_BASE + "pokemon/" + pokemon.ToLower();

            ApiPokemon deserializedPoke = InternalGetPokemon(query);
            return deserializedPoke;
        }

        /// <summary>
        /// Generalize pokemon requests to cut down on code duplication.
        /// </summary>
        /// <param name="query">A pre-formatted query to pass to DoRequest</param>
        /// <returns>An ApiPokemon object from a deserialized JSON response</returns>
        private static ApiPokemon InternalGetPokemon(string query)
        {
            ApiPokemon deserializedPoke = null;

            try
            {
                HttpWebResponse wr = DoRequest(query);
                string response = "";

                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                    deserializedPoke = JsonConvert.DeserializeObject<ApiPokemon>(response);
                }
            }
            catch (NullReferenceException nre)
            {
                //Getting here simply means that DoRequest failed with an WebException.
                //For now, DoRequest handles its own failure but we might want to do something
                //here in the future other than just pass through to inform our caller.
            }

            return deserializedPoke;
        }

        /// <summary>
        /// Get type information based on a PokeType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static HttpWebResponse GetType(PokeType type)
        {
            int typeNum = (int)type;
            string query = Q_BASE + "type/" + typeNum.ToString();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Build a query from a linked resource_uri string from API response JSON
        /// resource_uri stings will be in the following format:
        /// "/api/v1/[CALL]/[PARAM]/"
        /// </summary>
        /// <param name="resourceUri"></param>
        /// <returns></returns>
        public static HttpWebResponse QueryFromResourceUri(string resourceUri)
        {
            string query = "";

            //maybe try to identify the type of call being made based on PARAM in the future
            //data returned may be useless/cryptic otherwise
            try
            {
                string[] arr = resourceUri.Split('/');
                query = Q_BASE + string.Join("/", arr[2], arr[3]);
            }
            catch (Exception e)
            {
                throw new ArgumentException("The resource URI appears to be malformed. It should be in the format: \"/api/v1/[CALL]/[PARAM]/\"", resourceUri, e);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Utility to build and execute a WebRequest. Enforces only GET requests to Pokeapi.
        /// </summary>
        /// <param name="url">URL of API call.</param>
        /// <returns>WebResponse object</returns>
        private static HttpWebResponse DoRequest(string url)
        {
            WebRequest request =  WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                response = null;
                Console.Error.WriteLine(we.Message);
            }

            return response;
        }
    }
}

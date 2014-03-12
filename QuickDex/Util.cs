using System;
using System.IO;

namespace QuickDex
{
    class Util
    {
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

        public static bool CacheExists()
        {
            return (File.Exists(Cache.FileName));
        }
    }
}

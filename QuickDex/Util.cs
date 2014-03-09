using System;
using System.IO;

namespace QuickDex
{
    class Util
    {
        private Util() { }

        public static string To3DigitStr(int i)
        {
            return string.Format("{0,0:D3}", i);
        }

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

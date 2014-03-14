using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace QuickDex
{
    class Util
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

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

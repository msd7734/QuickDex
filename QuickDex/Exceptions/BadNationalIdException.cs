using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDex.Exceptions
{
    public class BadNationalIdException : Exception
    {
        public BadNationalIdException() :
            base("A provided National ID was invalid (either out of range or not a number).")
        { }
    }
}

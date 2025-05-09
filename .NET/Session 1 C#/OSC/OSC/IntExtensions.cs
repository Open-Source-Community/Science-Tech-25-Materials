using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSC
{
    public static class IntExtensions
    {
        public static bool IsEven(this int number) //Extension Method
        {
            return number % 2 == 0;
        }

        public static bool IsOdd(this int number) //Extension Method
        {
            return number % 2 != 0;
        }
    }
}

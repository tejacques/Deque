using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deque
{
    internal class Utility
    {
        public static int ClosestPowerOfTwoGreaterThan(int x)
        {
            x--;
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            return (x+1);
        }
    }
}

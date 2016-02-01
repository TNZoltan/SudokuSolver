using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    static class Controller
    {
        internal static void BigMethodThatDoesAlotOfThings()
        {
            System.Threading.Thread.Sleep(10000);
        }
    }
}

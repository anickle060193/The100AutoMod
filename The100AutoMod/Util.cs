using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The100AutoMod
{
    public static class Util
    {
        public static String Script( params String[] lines )
        {
            return String.Join( "\n", lines );
        }
    }
}

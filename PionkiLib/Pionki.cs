using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PionkiLib
{
    public static class Pionki
    {
        /// <summary>
        /// 
        /// Białe:
        /// 1-Pionek 2-Wieza 3-Kon 4-Goniec 5-Qrolowa 6-Krol 7-Krol w szachu 
        /// Czarne:
        /// 8-Pionek 9-Wieża 10-Koń 11-Goniec 12-Qrolowa 13-Krol 14-Krol w szachu
        /// 
        /// </summary>
        public static int promoted=0;
        /// <summary>
        /// 0-White 1-Black
        /// </summary>
        public static int whiteOrBlack=0;
        /// <summary>
        /// 0-remis 1-mat białych 2-mat czarnych 3-kapitulacja czarnych 4-kapitulacja białych
        /// </summary>
        public static int result = 1;
    }
}

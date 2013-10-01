using System;
using System.Collections.Generic;
using System.Text;

namespace PonovoSMS
{
    class Core
    {
        public static void Init() 
        {
            Config.Load();
        }
    }
}

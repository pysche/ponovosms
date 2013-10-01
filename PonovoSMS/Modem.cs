using System;
using System.Collections.Generic;
using System.Text;

using JinDI_SmsComLib;

namespace PonovoSMS
{
    class Modem
    {
        private static SmsControlClass jd;

        private static SmsControlClass getJD()
        {
            if (jd == null)
            {
                jd = new SmsControlClass();
            }

            return jd;
        }

        public static Boolean Init()
        {
            Boolean connected = false;

            return connected;
        }

        public static void Close()
        {
        }
    }
}

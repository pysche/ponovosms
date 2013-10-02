using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace PonovoSMS
{
    class Logger
    {
        public static TextBox panel = null;

        public static void Write(String Msg)
        {
            String Log = "";
            
            Log += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Log += " ";
            Log += Msg;
            Log += "\n";

            panel.Text += Log;
        }
    }
}

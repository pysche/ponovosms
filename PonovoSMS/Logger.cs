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

        protected static void Write(String Msg)
        {
            String Log = "";
            
            Log += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Log += " ";
            Log += Msg;
            Log += "\r\n";

            panel.Text += Log;

            Console.WriteLine(Log);
        }

        public static void Write(String Msg, String Level)
        {
            switch (Config.LOG_LEVEL)
            {
                case "debug":
                    Write("[" + Level + "] " + Msg);
                    break;
                case "alert":
                    if (Level == "alert" || Level == "error")
                    {
                        Write("[" + Level + "] " + Msg);
                    }
                    break;
                case "error":
                    if (Level == "error")
                    {
                        Write("[" + Level + "] " + Msg);
                    }
                    break;
            }
        }
    }
}

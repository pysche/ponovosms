using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

namespace PonovoSMS
{
    class Config
    {
        public static string MYSQL_HOST = "127.0.0.1";
        public static string MYSQL_USER = "root";
        public static string MYSQL_PASS = "leipang";
        public static string MYSQL_PORT = "3306";
        public static string MYSQL_NAME = "sms";
        public static string MYSQL_CHARSET = "utf8";

        public static string COM_PORT = "3";
        public static string COM_RATE = "9600";
        public static string LOOP_TIMER = "15";

        public static string LOG_LEVEL = "alert";

        public static string SMS_SIGN = "[±±¾©²©µç]";

        public static void Load()
        {
            MYSQL_HOST = Properties.Settings.Default.mysql_host;
            MYSQL_NAME = Properties.Settings.Default.mysql_name;
            MYSQL_USER = Properties.Settings.Default.mysql_user;
            MYSQL_PASS = Properties.Settings.Default.mysql_pass;
            MYSQL_CHARSET = Properties.Settings.Default.mysql_charset;

            COM_RATE = Properties.Settings.Default.com_rate;
            COM_PORT = Properties.Settings.Default.com_port;
            LOG_LEVEL = Properties.Settings.Default.log_level;
            LOOP_TIMER = Properties.Settings.Default.loop_timer;
            SMS_SIGN = Properties.Settings.Default.sms_sign;
        }
    }
}

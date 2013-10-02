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
        public static string LOOP_TIMER = "15";

        public static string LOG_LEVEL = "alert";

        private static string _INI_FILE = "";

        public static void Load()
        {
            _INI_FILE = System.Environment.CurrentDirectory + "\\PonovoSms.ini";
            FileInfo fileInfo = new FileInfo(_INI_FILE);

            if (fileInfo.Exists) {
                IniReader reader = new IniReader(_INI_FILE);

                if (reader.get("mysql_host") != "")
                {
                    MYSQL_HOST = reader.get("mysql_host");
                }

                if (reader.get("mysql_user") != "")
                {
                    MYSQL_USER = reader.get("mysql_user");
                }

                if (reader.get("mysql_pass") != "")
                {
                    MYSQL_PASS = reader.get("mysql_pass");
                }

                if (reader.get("mysql_port") != "")
                {
                    MYSQL_PORT = reader.get("mysql_port");
                }

                if (reader.get("mysql_charset") != "")
                {
                    MYSQL_CHARSET = reader.get("mysql_charset");
                }

                if (reader.get("mysql_name") != "")
                {
                    MYSQL_NAME = reader.get("mysql_name");
                }

                if (reader.get("com_port") != "")
                {
                    COM_PORT = reader.get("com_port");
                }

                if (reader.get("log_level") != "")
                {
                    LOG_LEVEL = reader.get("log_level");
                }
            }
        }
    }
}

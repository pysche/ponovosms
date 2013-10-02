using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace PonovoSMS
{
    class Db
    {
        private static MySqlConnection conn;
        public static Boolean connected = false;

        public static void Init()
        {
            String connStr = "Database=" + Config.MYSQL_NAME + ";Data Source=" + Config.MYSQL_HOST + ";User Id=" + Config.MYSQL_USER + ";Password=" + Config.MYSQL_PASS + ";pooling=false;CharSet=" + Config.MYSQL_CHARSET + ";port=" + Config.MYSQL_PORT;
            conn = new MySqlConnection(connStr);
      
            Connect();
        }

        public static void Connect()
        {
            try
            {
                if (connected==false)
                {
                    conn.Open();
                    connected = true;
                }
            }
            catch (Exception e)
            {
                Logger.Write(Config.MYSQL_USER+":"+Config.MYSQL_PASS+", "+e.ToString());

                connected = false;
                conn = null;
            }
        }

        public static void Disconnect()
        {
            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                Logger.Write(e.ToString());
            }
        }

        public static Sms[] LoadSms()
        {
            Connect();
            Sms[] Rows = new Sms[0];

            if (conn!=null)
            {
                String sql = "SELECT * FROM `sms_queue` WHERE `deleted`='0' AND `sent`=0 ORDER BY `qid` DESC LIMIT 10";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rs = cmd.ExecuteReader();
                Rows = new Sms[10];

                try
                {
                    int i = 0;
                    while (rs.Read())
                    {
                        if (rs.HasRows)
                        {
                            Sms sms = new Sms();
                            sms.Qid = rs.GetString(0);
                            sms.Receiver = rs.GetString(1);
                            sms.Content = rs.GetString(2);

                            Rows[i] = sms;
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Write(e.ToString());
                }
                finally
                {
                    rs.Close();
                    Disconnect();
                }
            }

            return Rows;
        }
    }
}
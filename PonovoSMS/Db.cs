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
            Logger.Write(connStr, "debug");
            conn = new MySqlConnection(connStr);
      
            Connect();

            if (connected == true)
            {
                Logger.Write("Connected to MySQL", "debug");
            }
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
                Logger.Write(Config.MYSQL_USER+":"+Config.MYSQL_PASS+", "+e.ToString(), "error");

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
                Logger.Write(e.ToString(), "error");
            }
        }

        public static void SetSent(String Qid)
        {
            Connect();

            String Sql = "UPDATE `sms_queue` SET `sent`='1', `send_time`=NOW() WHERE `qid`='"+Qid+"'";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Sql;
            cmd.ExecuteNonQuery();
        }

        public static void Save(String Number, String Content)
        {
            Connect();

            String Sql = "INSERT INTO `sms` (`msg_from`, `receive_time`, `content`, `deleted`) VALUES ('"+Number+"', UNIX_TIMESTAMP(), '"+Content+"', '0')";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Sql;
            cmd.ExecuteNonQuery();
        }

        public static Sms[] LoadSms()
        {
            Connect();
            Sms[] Rows = new Sms[10];

            if (conn != null && connected == true)
            {
                String sql = "SELECT * FROM `sms_queue` WHERE `deleted`='0' AND `sent`='0' ORDER BY `qid` DESC LIMIT 10";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rs = cmd.ExecuteReader();
                Rows = new Sms[10];

                try
                {
                    int i = 0;
                    while (rs.Read())
                    {
                        Sms sms = new Sms();
                        sms.Qid = rs["qid"].ToString();
                        sms.Receiver = rs["receiver"].ToString();
                        sms.Content = rs["content"].ToString();

                        Rows[i++] = sms;
                    }
                }
                catch (Exception e)
                {
                    Logger.Write(e.ToString(), "error");
                }
                finally
                {
                    rs.Close();
                    cmd.Dispose();
                    Disconnect();
                    connected = false;
                }
            }
            else
            {
                Logger.Write("Connection Error", "error");
            }

            return Rows;
        }
    }
}

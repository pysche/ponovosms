using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

namespace PonovoSMS
{
    class IniReader
    {
        private string iniFile = "";
        private string section = "Ponovo";

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        public IniReader(string fileName)
        {
            iniFile = fileName;
        }

        public string get(string Ident)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(this.section, Ident, "", Buffer, Buffer.GetUpperBound(0), this.iniFile);
            string s = Encoding.GetEncoding(0).GetString(Buffer);

            return s.Substring(0, bufLen).Trim();
        }
    }
}

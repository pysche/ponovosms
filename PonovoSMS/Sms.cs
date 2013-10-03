using System;
using System.Collections.Generic;
using System.Text;

namespace PonovoSMS
{
    class Sms
    {
        public String Receiver = "";
        public String Content = "";
        public String Qid = "";

        public void SetSent()
        {
            Db.SetSent(Qid);
        }
    }
}

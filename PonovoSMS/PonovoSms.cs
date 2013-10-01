using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PonovoSMS
{
    public partial class PonovoSms : Form
    {
        public PonovoSms()
        {
            InitializeComponent();
            Core.Init();
        }

        private void PonovoSms_Load(object sender, EventArgs e)
        {
            tsCom.Text = Config.COM_PORT;
        }
    }
}
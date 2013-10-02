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
        }

        private void PonovoSms_Load(object sender, EventArgs e)
        {
            Logger.panel = textBox1;

            Core.Init();

            tsCom.Text = Config.COM_PORT;

            Sms[] Queue = Db.LoadSms();
        }

        private void PonovoSms_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("��ȷ��Ҫ�ر���", "��ʾ��Ϣ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  
            }
            else
            {
                e.Cancel = true;
            }
        }  

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("��ȷ��Ҫ�ر���", "��ʾ��Ϣ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                this.Dispose();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  ɾ��sim������
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //  �����������
        }
    }
}
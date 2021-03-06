using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using JinDI_SmsComLib;

namespace PonovoSMS
{
    public partial class PonovoSms : Form
    {
        private SmsControlClass JDSmsControl;

        public PonovoSms()
        {
            InitializeComponent();

            JDSmsControl = new SmsControlClass();
            JDSmsControl.ConnectModemResult += new _ISmsControlEvents_ConnectModemResultEventHandler(JDSmsControl_ConnectModemResult);
            JDSmsControl.NewMessage += new _ISmsControlEvents_NewMessageEventHandler(JDSmsControl_NewMessage);
            JDSmsControl.SentMsgStatus += new _ISmsControlEvents_SentMsgStatusEventHandler(JDSmsControl_SentMsgStatus);
            JDSmsControl.SimCardNoMemory += new _ISmsControlEvents_SimCardNoMemoryEventHandler(JDSmsControl_SimCardNoMemory);
            JDSmsControl.SimCardNoMoney += new _ISmsControlEvents_SimCardNoMoneyEventHandler(JDSmsControl_SimCardNoMoney);
        }

        private void ForClose()
        {
            try
            {
                JDSmsControl.CloseCom();
            }
            catch (Exception)
            {
            }

            this.Dispose();
            this.Close();
            Application.Exit();
        }

        private void PonovoSms_Load(object sender, EventArgs e)
        {
            Logger.panel = textBox1;
            Core.Init();
            tsCom.Text = Config.COM_PORT;

            ConnectToModem();
        }

        private void PonovoSms_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要关闭吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  
            }
            else
            {
                e.Cancel = true;

                ForClose();
            }
        }  

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要关闭吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                ForClose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  删除sim卡短信
            try
            {
                JDSmsControl.ClearSimMsg();
                MessageBox.Show("Sim卡内短信已清空！");
            }
            catch (Exception e1)
            {
                Logger.Write(e1.ToString(), "error");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendSms();
        }

        private void SendSms()
        {
            Sms[] Queue = Db.LoadSms();
            for (int i = 0; i < Queue.Length; i++)
            {
                try
                {
                    if (Queue[i].Qid != "")
                    {
                        short sReturn = JDSmsControl.SendMsg(Queue[i].Receiver, Queue[i].Content);
                        Queue[i].SetSent();

                        Logger.Write("已发送，接收人：" + Queue[i].Receiver + "，内容："+Queue[i].Content, "debug");
                    }
                }
                catch (Exception e)
                {
                    Logger.Write(e.ToString(), "error");
                }
            }
        }

        void JDSmsControl_SimCardNoMoney()
        {
            Logger.Write("SIM卡余额不足", "alert");
        }

        void JDSmsControl_NewMessage(short sMsgType, short sSimPosition, string bstrFromNumber, string bstrContent, DateTime dtSentTime)
        {
            Logger.Write(bstrFromNumber + "来信：" + bstrContent, "debug");
            Db.Save(bstrFromNumber, bstrContent);
        }

        void JDSmsControl_SimCardNoMemory()
        {
            Logger.Write("SIM卡短信已满", "alert");
        }

        void JDSmsControl_SentMsgStatus(uint ulMsgID, string bstrDestNumber, string bstrContent, short sSplitIndex, bool bSucceed)
        {
            if (bSucceed == true)
            {
                Logger.Write(" 目标：" + bstrDestNumber + " 内容：" + bstrContent + " 发送成功！", "debug");
            }
            else
            {
                Logger.Write(" 目标：" + bstrDestNumber + " 内容：" + bstrContent + " 发送失败！", "error");
            }
        }

        void JDSmsControl_ConnectModemResult(bool bSucceed)
        {
            if (bSucceed == true)
            {
                Logger.Write("成功连接到短信猫设备", "debug");

                //  启动定时器，开始查询数据库
                timer1.Enabled = true;
            }
            else
            {
                Logger.Write("打开端口失败！请确认设备是否正常连接、设备是否已经被其它应用打开、COM端口和通讯波特率是否正确。", "alert");
                MessageBox.Show("打开端口失败！请确认设备是否正常连接、设备是否已经被其它应用打开、COM端口和通讯波特率是否正确。");
                ForClose();
            }
       }

        void ConnectToModem()
        {
            Logger.Write("正在连接到短信猫设备 ...", "debug");

            JDSmsControl.SyncWorkMode = false;
            JDSmsControl.AutoDelMsg = true;

            JDSmsControl.Timeouts = 15;
            JDSmsControl.CommPort = (short)Convert.ToInt16(Config.COM_PORT);
            JDSmsControl.Settings = (short)Convert.ToInt16(Config.COM_RATE);
            JDSmsControl.CountryCode = "86";

            JDSmsControl.isStatusReport = false;           
            JDSmsControl.EnglishMsg = false;       
            JDSmsControl.AutoSplite = true;
            JDSmsControl.IsFlash = false;

            JDSmsControl.SignName = Config.SMS_SIGN;
            JDSmsControl.SendPriority = 16;
            JDSmsControl.MsgValidMinute = 1440;

            short sReturn = JDSmsControl.OpenCom();

            if (JDSmsControl.SyncWorkMode == true)
            {
                if (sReturn == 0)
                {
                    Logger.Write("成功连接到短信猫设备", "debug");

                    //  启动定时器，开始查询数据库
                    timer1.Enabled = true;
                }
                else
                {
                    Logger.Write("打开端口失败！请确认设备是否正常连接、设备是否已经被其它应用打开、COM端口和通讯波特率是否正确。", "alert");
                    MessageBox.Show("打开端口失败！请确认设备是否正常连接、设备是否已经被其它应用打开、COM端口和通讯波特率是否正确。");
                    ForClose();
                }
            }
        }
    }
}
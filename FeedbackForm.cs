using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace RiverSimulationApplication
{
    public partial class FeedbackForm : Form
    {
        public FeedbackForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "mailto:hsieh0182@itri.org.tw?subject=水理動床模組問題回饋&body= ";
            proc.Start();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "mailto:yang.jc4169@gmail.com?subject=水理動床模組問題回饋&body= ";
            proc.Start();
        }

        private void FeedbackForm_Load(object sender, EventArgs e)
        {
            mrRdo.Checked = true;
        }

        public void send_gmail(string msg, string mysubject, string address)
        {
            MailMessage message = new MailMessage("Alex@mymail.com", address);//MailMessage(寄信者, 收信者)
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;//E-mail編碼
            message.SubjectEncoding = System.Text.Encoding.UTF8;//E-mail編碼
            message.Priority = MailPriority.Normal;//設定優先權
            message.Subject = mysubject;//E-mail主旨
            message.Body = msg;//E-mail內容
            string file = @"D:\Project\GitCodes\RiverSimulation\Documents\RiverSimulationLite-20141112-2.eee";
            message.Attachments.Add(new System.Net.Mail.Attachment(file));

            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);//設定gmail的smtp
            MySmtp.Credentials = new System.Net.NetworkCredential("accbb@gmail.com", "************");//gmail的帳號密碼System.Net.NetworkCredential(帳號,密碼)
            MySmtp.EnableSsl = true;//開啟ssl
            MySmtp.Send(message);

            MySmtp = null;
            message.Dispose();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {

            //send_gmail("測試內容", "測試主旨標題", "alex1@skyraker.com.tw, alcor2@ms33.url.com.tw");



            this.Close();

        }
    }
}

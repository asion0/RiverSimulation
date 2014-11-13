using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            StringBuilder sb = new StringBuilder();
            foreach(string s in Program.programSetting.feedMailTo)
            {
                string[] ss = s.Split('\t');
                if (sb.Length != 0)
                {
                    sb.Append(", ");
                }
                sb.Append(ss[0] + "<" + ss[1] + ">");
            }
            sendToTxt.Text = sb.ToString();
            sendToTxt.Select(0, -1);
            nameTxt.Select();
        }



        private void sendBtn_Click(object sender, EventArgs e)
        {
            if(mailTxt.Text.Length == 0 || !GmailUtility.IsMailAddressValidate(mailTxt.Text))
            {
                MessageBox.Show("請輸入正確的電子郵件！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(nameTxt.Text.Length == 0)
            {
                MessageBox.Show("請留下您的大名！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (bodyRich.Text.Length == 0)
            {
                MessageBox.Show("請填寫問題內容！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string[] attach = new string[attachListBox.Items.Count];
            long size = 0;
            long total = 0;
            for (int i = 0; i < attachListBox.Items.Count; ++i)
            {
                attach[i] = attachListBox.Items[i].ToString();
                if (!CheckAttachFile(attach[i], ref size))
                {
                    MessageBox.Show("無法讀取附件檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                total += size;
            }

            if (total > 8 * 1024 * 1024)    //8M Bytes
            {
                MessageBox.Show("附件超過8M bytes，請分割或是減少檔案尺寸！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("作業系統版本 : " + Utility.GetOSType() + " - " + System.Environment.OSVersion.ToString());
            mailBody.Append("<br>Resed Model " + Program.GetVersionString());
            mailBody.Append("<br>Resed Model " + Program.GetBuildDayString());
            mailBody.Append("<br><br>");
            mailBody.Append("<br>寄件者姓名 : " + nameTxt.Text);
            mailBody.Append("<br>寄件者信箱 : " + mailTxt.Text);
            mailBody.Append("<br>寄件者電話 : " + telTxt.Text);
            mailBody.Append("<hr>");


            mailBody.Append(bodyRich.Text.Replace("\n", "<br>"));

            status.Visible = true;
            status.Refresh();
            Cursor.Current = Cursors.WaitCursor;
            GmailUtility.SendGmail(
                Program.programSetting.feedMailAddress, 
                Program.programSetting.feedMailKey,
                Program.programSetting.feedMailTo.ToArray(),
                "",     //mailTxt.Text,
                "ResedModel問題回饋" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                mailBody.ToString(),
                attach);
            Cursor.Current = Cursors.Default;
            this.Close();
        }

        private void addAttachBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //dlg.InitialDirectory = @"c:\MyFolder\Default\";
            // 設定可以選擇的檔案類型
            dlg.Filter = "All Files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.Multiselect = true;
            dlg.CheckFileExists = true;             // 若檔案/路徑 不存在是否顯示錯誤訊息
            dlg.CheckPathExists = false;
            DialogResult result = dlg.ShowDialog();     // 顯示檔案對話方框並回傳狀態（DialogResult.OK、DialogResult.Cancel）
            if (result == DialogResult.OK) 
            {
                foreach (string filename in dlg.FileNames)
                {
                    attachListBox.Items.Add(filename);
                }
            }
        }

        private void clearAttachBtn_Click(object sender, EventArgs e)
        {
            attachListBox.Items.Clear();
        }

        private bool CheckAttachFile(string file, ref long size)
        {
            try
            {
                FileInfo f = new FileInfo(file);
                size = f.Length;
                if (size == 0)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void attachListBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            bool warning = false;
            foreach (string file in files)
            {
                long size = 0;
                if(!CheckAttachFile(file, ref size))
                {
                    warning = true;
                    continue;
                }

                if (attachListBox.Items.Contains(file))
                {
                    warning = true;
                    continue;
                }
                attachListBox.Items.Add(file);
                attachListBox.SelectedIndex = attachListBox.Items.Count - 1;
                attachListBox.SelectedIndex = -1;

            }
            if(warning)
            {
                MessageBox.Show("已忽略不支援的檔案或重複的檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void attachListBox_DragEnter(object sender, DragEventArgs e)
        {
            // 確定使用者抓進來的是檔案
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                // 允許拖拉動作繼續 (這時滑鼠游標應該會顯示 +)
                e.Effect = DragDropEffects.All;
            }
        }
    }
}

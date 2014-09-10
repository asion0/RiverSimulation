using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiverSimulationApplication
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private string FindNewProjectName()
        {
            int index = 0;
            string folder;
            do
            {
                index++;
                folder = "新專案" + index.ToString("D03");
            } while (System.IO.Directory.Exists(Program.documentPath + "\\" + folder));
            return folder;
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            FindNewProjectName();
            InputForm dlg = new InputForm();
            dlg.Text = "建立專案";
            dlg.desc.Text = "請輸入新專案名稱";
            dlg.inputTxt.Text = FindNewProjectName();
            if(DialogResult.OK != dlg.ShowDialog())
            {
                return;
            }

            if (System.IO.Directory.Exists(Program.documentPath + "\\" + dlg.inputTxt.Text))
            {
                MessageBox.Show("此專案已存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                System.IO.Directory.CreateDirectory(Program.documentPath + "\\" + dlg.inputTxt.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("無法建立專案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Program.projectFile = dlg.inputTxt.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            string path;
            folderOpen.RootFolder = Environment.SpecialFolder.MyDocuments;
            if (folderOpen.ShowDialog() == DialogResult.OK) 
            {
                path = folderOpen.SelectedPath;
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            //string path;
            folderOpen.RootFolder = Environment.SpecialFolder.MyDocuments;
            if (folderOpen.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("是否刪除所選取的目錄？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                //path = folderOpen.SelectedPath;
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

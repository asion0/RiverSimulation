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

            RiverSimulationProfile.profile = new RiverSimulationProfile();
            Program.projectFolder = dlg.inputTxt.Text;

            if(File.Exists(Program.documentPath + @"\resed.exe"))
            {
                File.Delete(Program.documentPath + @"\resed.exe");
            }
            if (File.Exists(Program.documentPath + @"\TempSave.txt"))
            {
                File.Delete(Program.documentPath + @"\TempSave.txt");
            }
            if (File.Exists(Program.documentPath + @"\resed.i"))
            {
                File.Delete(Program.documentPath + @"\resed.i");
            }
            if (File.Exists(Program.documentPath + @"\3Dinput.dat"))
            {
                File.Delete(Program.documentPath + @"\3Dinput.dat");
            }
            if (File.Exists(Program.documentPath + @"\sed.dat"))
            {
                File.Delete(Program.documentPath + @"\sed.dat");
            }
            if (File.Exists(Program.documentPath + @"\resed.O"))
            {
                File.Delete(Program.documentPath + @"\resed.o");
            }
            if (File.Exists(Program.documentPath + @"\out"))
            {
                File.Delete(Program.documentPath + @"\out");
            }
            if (File.Exists(Program.documentPath + @"\123"))
            {
                File.Delete(Program.documentPath + @"\123");
            }
            if (File.Exists(Program.documentPath + @"\resed.er"))
            {
                File.Delete(Program.documentPath + @"\resed.er");
            } 
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            string tempSave = Program.documentPath + Program.tempSaveName;
            if (File.Exists(tempSave))
            {
                RiverSimulationProfile.profile = RiverSimulationProfile.DeSerialize(tempSave);
                RiverSimulationProfile.profile.ResetGoogleStaticMap();
            }
            else
            {
                RiverSimulationProfile.profile = new RiverSimulationProfile();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            
            //folderOpen.RootFolder = Environment.SpecialFolder.MyDocuments;
            //if (folderOpen.ShowDialog() == DialogResult.OK) 
            //{
            //    Program.projectFolder = folderOpen.SelectedPath;
            //}
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            RiverSimulationApplication.Properties.Settings s = RiverSimulationApplication.Properties.Settings.Default;
            if(!Directory.Exists(s.DefaultOpenProjectFolder))
            {
                s.DefaultOpenProjectFolder = Program.documentPath;
                s.Save();
            }
        
            folderOpen.ShowNewFolderButton = false;
            folderOpen.SelectedPath = s.DefaultOpenProjectFolder;
            if (folderOpen.ShowDialog(this) == DialogResult.OK)
            {
                DialogResult dialogResult = MessageBox.Show("是否刪除下列所選取的目錄？\r\n" + folderOpen.SelectedPath, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                //path = folderOpen.SelectedPath;
                if (dialogResult == DialogResult.Yes)
                {
                    Utility.DeleteFileOrFolder(folderOpen.SelectedPath);
                }
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

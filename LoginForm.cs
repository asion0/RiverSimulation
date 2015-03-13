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
            Program.projectFolder = Program.documentPath + "\\" + dlg.inputTxt.Text;
            Program.SaveDefaultProjectFolder();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            RiverSimulationApplication.Properties.Settings s = RiverSimulationApplication.Properties.Settings.Default;
            folderOpen.ShowNewFolderButton = false;
            if (s.DefaultOpenProjectFolder != null && s.DefaultOpenProjectFolder.Length > 0)
            {
                folderOpen.SelectedPath = s.DefaultOpenProjectFolder;
            }
            else
            {
                folderOpen.SelectedPath = Program.documentPath;
            }

            //string tempSave = Program.documentPath + Program.tempSaveName;
            string tempSave;
            if (folderOpen.ShowDialog(this) == DialogResult.OK)
            {
                tempSave = folderOpen.SelectedPath + Program.tempSaveName;
                Program.projectFolder = folderOpen.SelectedPath;
            }
            else
            {
                return;
            }

            if (File.Exists(tempSave))
            {
                RiverSimulationProfile.profile = RiverSimulationProfile.DeSerialize(tempSave);
                RiverSimulationProfile.profile.ResetGoogleStaticMap();
            }
            else
            {
                //RiverSimulationProfile.profile = new RiverSimulationProfile();
                //MessageBox.Show("此目錄無存檔，將建立新檔案。\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("此目錄無存檔！\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
          
            //if(File.Exists(Program.projectFolder + @"\resed.exe"))
            //{
            //    File.Delete(Program.projectFolder + @"\resed.exe");
            //}
            if (File.Exists(Program.projectFolder + @"\resed.i"))
            {
                File.Delete(Program.projectFolder + @"\resed.i");
            }
            if (File.Exists(Program.projectFolder + @"\3Dinput.dat"))
            {
                File.Delete(Program.projectFolder + @"\3Dinput.dat");
            }
            if (File.Exists(Program.projectFolder + @"\sed.dat"))
            {
                File.Delete(Program.projectFolder + @"\sed.dat");
            }
            if (File.Exists(Program.projectFolder + @"\resed.O"))
            {
                File.Delete(Program.projectFolder + @"\resed.o");
            }
            if (File.Exists(Program.projectFolder + @"\out"))
            {
                File.Delete(Program.projectFolder + @"\out");
            }
            if (File.Exists(Program.projectFolder + @"\123"))
            {
                File.Delete(Program.projectFolder + @"\123");
            }
            if (File.Exists(Program.projectFolder + @"\resed.er"))
            {
                File.Delete(Program.projectFolder + @"\resed.er");
            }

            Program.SaveDefaultProjectFolder();

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

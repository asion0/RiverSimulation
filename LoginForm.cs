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
using System.Xml;

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

        /*
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
        */

        private void SetProjectTitle(string t)
        {
            projectTitle.Text = "專案：" + t;
        }

        private void newProjectBtn_Click(object sender, EventArgs e)
        {
            if (!FunctionlUtility.NewProject(this))
            {
                return;
            }
            newFileBtn.Enabled = true;
            openFileBtn.Enabled = false;
            projectDescriptBtn.Enabled = true;
            SetProjectTitle(Program.projectName);
        }

        private void selectProject_Click(object sender, EventArgs e)
        {
            if (!FunctionlUtility.OpenProject(this))
            {
                return;
            }
            newFileBtn.Enabled = true;
            openFileBtn.Enabled = !FunctionlUtility.IsEmptyProject(Program.GetProjectFullPath());
            projectDescriptBtn.Enabled = true;
            SetProjectTitle(Program.projectName);
        }

        private void newFileBtn_Click(object sender, EventArgs e)
        {
            if (!FunctionlUtility.NewProjectFile(this, Program.GetProjectFullPath()))
            {
                return;
            }
            //RiverSimulationProfile.profile = new RiverSimulationProfile();
            //Program.projectFolder = Program.documentPath + "\\" + dlg.inputTxt.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void openFileBtn_Click(object sender, EventArgs e)
        {
            if (!FunctionlUtility.OpenProjectFile(this, Program.GetProjectFileFullPath()))
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();       
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void projectDescriptBtn_Click(object sender, EventArgs e)
        {
            if(!File.Exists(Program.GetDescriptionFileFullPath()))
            {
                MessageBox.Show("尚未建立說明檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Program.GetDescriptionFileFullPath());
            XmlNode files = XmlDoc.SelectSingleNode("Files");

            string tempTxtFile = Program.GetProjectFullPath() + @"\Description.txt";
            StreamWriter fs = new StreamWriter(tempTxtFile);
            fs.WriteLine("專案名稱：" + Program.projectName);
            fs.WriteLine("\r\n\r\n");

            foreach (XmlNode n in files.ChildNodes)
            {
                String fileName = (n as XmlElement).Name;
                String fileDesc = (n as XmlElement).GetAttribute("Text");

                fs.WriteLine(fileName + "\t\t" + fileDesc);
            }
            fs.Close();
            System.Diagnostics.Process.Start(tempTxtFile);
        }
    }
}

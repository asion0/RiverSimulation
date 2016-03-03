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

        private Image descBmpNormal = new Bitmap(RiverSimulationApplication.Properties.Resources.desc_n);
        private Image descBmpPush = new Bitmap(RiverSimulationApplication.Properties.Resources.desc_p);
        private Image descBmpHighlight = new Bitmap(RiverSimulationApplication.Properties.Resources.desc_h);
        private Image descBmpDisable = new Bitmap(RiverSimulationApplication.Properties.Resources.desc_d);
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
            //this.DialogResult = DialogResult.OK;
            //this.Close();

            RiverSimulationForm form = new RiverSimulationForm();
            if (DialogResult.OK != form.ShowDialog())
            {
                //this.Close();
            }


        }

        private void openFileBtn_Click(object sender, EventArgs e)
        {
            if (!FunctionlUtility.OpenProjectFile(this, Program.GetProjectFileFullPath()))
            {
                return;
            }
            //this.DialogResult = DialogResult.OK;
            //this.Close();       
            RiverSimulationForm form = new RiverSimulationForm();
            if (DialogResult.OK != form.ShowDialog())
            {
                //this.Close();
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void projectDescriptBtn_Click(object sender, EventArgs e)
        {
            if(!File.Exists(Program.GetDescriptionFileFullPath()) ||
                Program.GetDescriptionFileVersion() != Utility.GetDescriptXMLVersion(Program.GetDescriptionFileFullPath()))
            {
                MessageBox.Show("尚未建立說明檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string tempTxtFile = Program.GetProjectFullPath() + @"\Description.txt";
            Utility.ConvertDescriptionText(Program.GetDescriptionFileFullPath(), tempTxtFile);
            System.Diagnostics.Process.Start(tempTxtFile);
        }
    }
}

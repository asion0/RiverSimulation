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



        private void newBtn_Click(object sender, EventArgs e)
        {
            if(!FunctionlUtility.NewProject())
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            if(!FunctionlUtility.OpenProject(this))
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            

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

        private void folderOpen_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}

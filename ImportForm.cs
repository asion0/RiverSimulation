using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32; //Registry
using System.Diagnostics;
using System.IO;

namespace RiverSimulationApplication
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            //bitmapGrp.Enabled = RiverSimulationProfile.profile.IsMapPosition();
            if (RiverSimulationProfile.profile.inputGrid != null)
            {
                mapPicBox.Grid = RiverSimulationProfile.profile.inputGrid;
            }
            UpdateStatus();
        }

        private void inputFileRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            inputFileBtn.Enabled = chk;
            //inputFilePath.Enabled = chk;
            /*
            if(chk)
            {
                //Auto select file.
                DialogResult result = inputFileDlg.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    inputFilePath.Text = inputFileDlg.FileName;
                }
            }
            */
        }

        private void inputFileBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = inputFileDlg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                //inputFilePath.Text = inputFileDlg.FileName;
                if(!RiverSimulationProfile.profile.ReadInputGridGeo(inputFileDlg.FileName))
                {
                    MessageBox.Show("無法讀取所選檔案", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                mapPicBox.Grid = RiverSimulationProfile.profile.inputGrid;
                UpdateStatus();
            }
        }

        private void inputGridRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            inputGridBtn.Enabled = chk;
            /*
            if(chk)
            { 
                //Auto show input form
                ImportTableForm form = new ImportTableForm();
                if (DialogResult.OK == form.ShowDialog())
                {

                }
            }
            */
        }

        private void inputGridBtn_Click(object sender, EventArgs e)
        {
            ImportTableForm form = new ImportTableForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void noBgRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            //RiverSimulationProfile.profile.ClearBackgroundBitmap();
            if (chk)
            {
                mapPicBox.ClearMapBackground();
                UpdateStatus();
            }
        }

        private void useGoogleBgRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (chk)
            {
                RiverSimulationProfile.profile.DownloadGoogleStaticMap();
                mapPicBox.SetMapBackground(p.tl, p.tr, p.bl, p.br);
                UpdateStatus();
            }

        }

        private void selectBgRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            selectBgBtn.Enabled = chk;
            if (chk)
            {
                RiverSimulationProfile.profile.SetImportImageMode();
                mapPicBox.SetMapBackground(p.imagePath, p.sourceE, p.sourceN, p.sourceW, p.sourceH);
                UpdateStatus();
            }
        }

        private void selectBgBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = selectBgDlg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                selectBgFilePath.Text = selectBgDlg.FileName;
                ImportImageForm form = new ImportImageForm();
                form.SetImage(selectBgDlg.FileName);
                if (DialogResult.OK == form.ShowDialog())
                {
                    imgInfoBtn.Enabled = true;
                    RiverSimulationProfile.profile.SetImportImage(selectBgDlg.FileName, form.e, form.n, form.w, form.h);
                    mapPicBox.SetMapBackground(selectBgDlg.FileName, form.e, form.n, form.w, form.h);
                    UpdateStatus();
                }
                else
                {
                    selectBgFilePath.Text = "";

                }

            }
        }

        private void imgInfoBtn_Click(object sender, EventArgs e)
        {
            ImportImageForm form = new ImportImageForm();
            form.SetImage(selectBgDlg.FileName);
            form.e = RiverSimulationProfile.profile.sourceE;
            form.n = RiverSimulationProfile.profile.sourceN;
            form.w = RiverSimulationProfile.profile.sourceW;
            form.h = RiverSimulationProfile.profile.sourceH;
            if (DialogResult.OK == form.ShowDialog())
            {
                imgInfoBtn.Enabled = true;
                RiverSimulationProfile.profile.SetImportImage(selectBgDlg.FileName, form.e, form.n, form.w, form.h);
                mapPicBox.SetMapBackground(selectBgDlg.FileName, form.e, form.n, form.w, form.h);
                UpdateStatus();
            }

        }   

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void runExcel_Click(object sender, EventArgs e)
        {
            if (IsExcelInstalled())
            {
                string path = GetExcelPath();
                //MessageBox.Show(path);
                ProcessStartInfo start = new ProcessStartInfo(path);
                start.WindowStyle = ProcessWindowStyle.Normal;
                Process.Start(start);
            }
            else
            {
                MessageBox.Show("Microsoft Excel 未安裝！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private string GetExcelPath()
        {
            RegistryKey reg = Registry.ClassesRoot.OpenSubKey("Excel.Workspace\\shell\\New\\command", false);
            string ccheCmd = null;
            try
            {
                ccheCmd = reg.GetValue("") as string;
                reg.Close();

                ccheCmd = ccheCmd.Substring(1, ccheCmd.Length - 2);

                if (File.Exists(ccheCmd))
                    return ccheCmd;
                else
                    return null;
            }
            catch
            {
                if (reg != null)
                {
                    reg.Close();
                }

            }
            return ccheCmd;
        }

        private bool IsExcelInstalled()
        {
            return GetExcelPath() != null;
        }

        private string GetCcheMeshPath()
        {
            RegistryKey reg = Registry.ClassesRoot.OpenSubKey("CCHE2D.Geometry.File\\shell\\open\\command", false);
            string ccheCmd = null;
            try
            {
                ccheCmd = reg.GetValue("") as string;
                reg.Close(); 
                
                ccheCmd = ccheCmd.Replace(" \"%1\"", "");
                ccheCmd = ccheCmd.Substring(1, ccheCmd.Length - 2);
                if (!File.Exists(ccheCmd))
                {
                    int n = ccheCmd.LastIndexOf('-');
                    if(n > 0)
                    {
                        ccheCmd = ccheCmd.Remove(n, 1);
                        ccheCmd = ccheCmd.Insert(n, "_");
                        if (File.Exists(ccheCmd))
                            return ccheCmd;
                        else
                            return null;
                    }

                }
            }
            catch
            {
                if (reg != null)
                {
                    reg.Close();
                }

            }
            return ccheCmd;
        }

        private bool IsCcheMeshInstalled()
        {
            return GetCcheMeshPath() != null;
        }

        private void runCcheMeshBtn_Click(object sender, EventArgs e)
        {
            if (IsCcheMeshInstalled())
            {
                string path = GetCcheMeshPath();
                //MessageBox.Show(path);
                ProcessStartInfo start = new ProcessStartInfo(path);
                start.WindowStyle = ProcessWindowStyle.Normal;
                Process.Start(start);

            }
            else
            {
                MessageBox.Show("CCHE-Mesh doesn't install!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void separateProportionBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(SeparateNumTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if (n < 2)
            {
                MessageBox.Show("請輸入正確的數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(separateProportionBtn.Text, true, 1, n);
            if (DialogResult.OK == form.ShowDialog())
            {
               
            }
        }
        
        private void UpdateStatus()
        {
            bitmapGrp.Enabled = RiverSimulationProfile.profile.IsMapPosition();
        } 
    }
}

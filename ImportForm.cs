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
            RiverSimulationProfile.profile.ClearBackgroundBitmap();
            UpdateStatus();
        }

        private void useGoogleBgRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            if (chk)
            {
                RiverSimulationProfile.profile.DownloadGoogleStaticMap();
                UpdateStatus();
            }

        }

        private void selectBgRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            selectBgBtn.Enabled = chk;
            if (chk)
            {
                RiverSimulationProfile.profile.SetImportImageMode();
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
                    RiverSimulationProfile.profile.SetImportImage(selectBgDlg.FileName, form.e, form.n, form.w, form.h);
                    UpdateStatus();
                }
                else
                {
                    selectBgFilePath.Text = "";

                }

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


        private Color bkColor = Color.White;
        private Color lineColor = Color.Orange;
        private float lineWidth = 2.0F;
        private void DrawGrid()
        {
            RiverGrid rg = RiverSimulationProfile.profile.inputGrid;
            //CoordinateTransform ct = new CoordinateTransform();

            //CoorPoint pt = new CoorPoint();
            //pt = RiverSimulationProfile.profile.GetTopLeft();
            //CoorPoint lt = ct.CalLonLatDegToTwd97(pt.x, pt.y);
            //pt = RiverSimulationProfile.profile.GetBottomRight();
            //CoorPoint rb = ct.CalLonLatDegToTwd97(pt.x, pt.y);
            CoorPoint lt = RiverSimulationProfile.profile.GetTopLeft();
            CoorPoint rb = RiverSimulationProfile.profile.GetBottomRight();

            int w = 0;
            int h = 0;
            Bitmap picBoxBmp;
            Graphics g;
            if (RiverSimulationProfile.BackgroundMapType.None == RiverSimulationProfile.profile.GetBackgroundMapType())
            {
                w = 640 * 2;
                h = 640 * 2;
                picBoxBmp = new Bitmap(w, h);
                g = Graphics.FromImage(picBoxBmp);
                g.Clear(bkColor);
            }
            else if (RiverSimulationProfile.BackgroundMapType.GoogleStaticMap == RiverSimulationProfile.profile.GetBackgroundMapType())
            {
                w = RiverSimulationProfile.profile.GetGridBitmap().Width;
                h = RiverSimulationProfile.profile.GetGridBitmap().Height;
                picBoxBmp = new Bitmap(w, h);
                g = Graphics.FromImage(picBoxBmp);
                g.DrawImage(RiverSimulationProfile.profile.GetGridBitmap(), 0, 0);
            }
            else
            {
                w = RiverSimulationProfile.profile.GetGridBitmap().Width;
                h = RiverSimulationProfile.profile.GetGridBitmap().Height;
                picBoxBmp = new Bitmap(w, h);
                g = Graphics.FromImage(picBoxBmp);
                g.DrawImage(RiverSimulationProfile.profile.GetGridBitmap(), 0, 0);
            }

            Pen pen = new Pen(lineColor, lineWidth);
            for (int i = 0; i < rg.GetI; ++i)
            {
                for (int j = 0; j < rg.GetJ; ++j)
                {
                    int x1 = (int)(w * (rg.inputCoor[i, j].x - lt.x) / (rb.x - lt.x));
                    int y1 = (int)(h * (rg.inputCoor[i, j].y - lt.y) / (rb.y - lt.y));
                    int x2 = 0, y2 = 0;
                    if (j != rg.GetJ - 1)
                    {
                        x2 = (int)(w * (rg.inputCoor[i, j + 1].x - lt.x) / (rb.x - lt.x));
                        y2 = (int)(h * (rg.inputCoor[i, j + 1].y - lt.y) / (rb.y - lt.y));
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                    if (i != rg.GetI - 1)
                    {
                        x2 = (int)(w * (rg.inputCoor[i + 1, j].x - lt.x) / (rb.x - lt.x));
                        y2 = (int)(h * (rg.inputCoor[i + 1, j].y - lt.y) / (rb.y - lt.y));
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            }
            g.Dispose();
            mapPicBox.BackgroundImage = picBoxBmp;
        }

        private void UpdateStatus()
        {
            bitmapGrp.Enabled = RiverSimulationProfile.profile.IsMapPosition();
            if(RiverSimulationProfile.profile.inputGrid != null)
            {
                DrawGrid();
                mapPicBox.Refresh();
            }
        }    
    }
}

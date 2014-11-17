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
            SeparateNumTxt.Enabled = RiverSimulationProfile.profile.Is3DMode();
            separateProportionBtn.Enabled = RiverSimulationProfile.profile.Is3DMode();
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
                ShowGridMap(true);
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
                ShowGridMap(true);
                UpdateStatus();
            }
        }

        private void ShowGridMap(bool b)
        {
            if(b)
            {
                mapPicBox.Visible = true;
                previewSpratePanel.Visible = false;
            }
            else
            {
                mapPicBox.Visible = false;
                previewSpratePanel.Visible = true;
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
                ShowGridMap(true);
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
                ShowGridMap(true);
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
                    ShowGridMap(true);
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
                ShowGridMap(true);
                UpdateStatus();
            }

        }   

        private void ok_Click(object sender, EventArgs e)
        {
            if (!DoConvert())
            {
                return;
            }
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
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (!DoConvert())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(separateProportionBtn.Text, 1, p.separateNum, separateProportionBtn.Text, "分層比例", "格網分層",
                TableInputForm.InputFormType.SeparateForm, 90, 120, true, true, false, p.separateArray);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.separateArray = (double[])form.SeparateData().Clone();
                ShowGridMap(false);
                DrawPreview();
            }
        }
        
        private void UpdateStatus()
        {
            bitmapGrp.Enabled = RiverSimulationProfile.profile.IsMapPosition();
        }

        private bool DoConvert()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int n = 0;
            if(p.Is3DMode())
            { 
                if (!ControllerUtility.CheckConvertInt32(ref n, SeparateNumTxt.Text, "請輸入正確的垂向格網分層數目！", ControllerUtility.CheckType.GreaterThanTwo))
                {
                    return false;
                }
            }
            if (p.separateNum != n && p.separateNum != 0)
            {
                MessageBox.Show("變更垂向格網分層數目將清除原先輸入之資料", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                p.separateArray = null;
            }
            p.separateNum = n;
            return true;
        }

        private void DrawPreview()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (p.separateArray == null)
            {
                return;
            }

            const int SeparateWidth = 400;     //右方顯示泥砂顆粒狀態欄位寬度
            const int SeparatePicWidth = 125;     //右方顯示泥砂顆粒狀態欄位寬度
            const int SeparateTextX = 200;     //右方顯示泥砂顆粒狀態欄位寬度
            const int SeparateTextSize = 12;     //右方顯示泥砂顆粒狀態欄位寬度
            const int SeparateTextHeight = 40;     //右方顯示泥砂顆粒狀態欄位寬度
            const int SeparateMinHeight = 400;     //右方顯示泥砂顆粒狀態欄位寬度
            const int SeparateHeightGap = 5;     //右方顯示泥砂顆粒狀態欄位寬度
            const int textGap = 5;     //右方顯示泥砂顆粒狀態欄位寬度

            Font leftFont = new Font("微軟正黑體", SeparateTextSize, FontStyle.Regular, GraphicsUnit.Point);

            int w = SeparateWidth;
            int h = p.separateNum * SeparateTextHeight;
            if (h < SeparateMinHeight)
                h = SeparateMinHeight;

            Bitmap picBoxBmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(picBoxBmp);

            Pen pen = new Pen(Color.Black, 1.0f);
            SolidBrush brush = new SolidBrush(Color.Black);

            float startY = SeparateHeightGap;
            float textY = SeparateHeightGap;
            float realH = h - 2 * SeparateHeightGap;
            Point pt1 = new Point(0, (int)startY);
            Point pt2 = new Point(SeparatePicWidth, (int)(startY));
            g.DrawLine(pen, pt1, pt2);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;

            RectangleF rcText = new RectangleF(SeparateTextX, 0, SeparateWidth - SeparateTextX, SeparateTextSize + SeparateHeightGap);
            string txt = "1";
            g.DrawString(txt, leftFont, brush, rcText, stringFormat);

            pt1 = new Point(SeparatePicWidth + textGap, (int)startY);
            pt2 = new Point(SeparateTextX - textGap, (int)(textY + SeparateTextSize / 2));
            g.DrawLine(pen, pt1, pt2);

            textY += realH / p.separateNum;
            for (int i = 1; i < p.separateNum - 1; ++i)
            {
                startY = SeparateHeightGap + (float)(1.0 - p.separateArray[p.separateNum - i - 1]) * realH;
                pt1 = new Point(0, (int)startY);
                pt2 = new Point(SeparatePicWidth, (int)(startY));
                g.DrawLine(pen, pt1, pt2);


                rcText = new RectangleF(SeparateTextX, textY, SeparateWidth - SeparateTextX, SeparateTextSize + SeparateHeightGap);
                txt = (p.separateArray[p.separateNum - i - 1]).ToString();
                g.DrawString(txt, leftFont, brush, rcText, stringFormat);

                pt1 = new Point(SeparatePicWidth + textGap, (int)startY);
                pt2 = new Point(SeparateTextX - textGap, (int)(textY + SeparateTextSize / 2));
                g.DrawLine(pen, pt1, pt2);

                textY += realH / p.separateNum;


            }
            pt1 = new Point(0, h - SeparateHeightGap);
            pt2 = new Point(SeparatePicWidth, h - SeparateHeightGap);
            g.DrawLine(pen, pt1, pt2);

            pt1 = new Point(SeparatePicWidth, SeparateHeightGap);
            pt2 = new Point(SeparatePicWidth, h - SeparateHeightGap);
            g.DrawLine(pen, pt1, pt2);

            rcText = new RectangleF(SeparateTextX, h - SeparateTextSize - SeparateHeightGap, SeparateWidth - SeparateTextX, SeparateTextSize + SeparateHeightGap);
            txt = "0";
            g.DrawString(txt, leftFont, brush, rcText, stringFormat);

            pt1 = new Point(SeparatePicWidth + textGap, h - SeparateHeightGap);
            pt2 = new Point(SeparateTextX - textGap, h - SeparateTextSize - SeparateHeightGap + SeparateTextSize / 2);
            g.DrawLine(pen, pt1, pt2);

            g.Dispose();
            this.previewSpratePicBox.Image = picBoxBmp;

            int picW = (previewSpratePicBox.Width > w) ? previewSpratePanel.Width : w;
            int picH = (previewSpratePicBox.Height > h) ? previewSpratePanel.Height : h;
            previewSpratePicBox.Width = picW;
            previewSpratePicBox.Height = picH;

            previewSpratePanel.AutoScrollMinSize = new Size(w, h);
        }

    }
}

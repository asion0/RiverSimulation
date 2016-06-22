using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32; //Registry
using System.Diagnostics;
using System.IO;
using PictureBoxCtrl;

namespace RiverSimulationApplication
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private RiverSimulationProfile p = RiverSimulationProfile.profile;
        private SliderPanel sp = new SliderPanel();
        public void SetForm(RiverSimulationProfile profile)
        {
            p = profile;
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            ControllerUtility.SetHtmlUrl(comment, "Logo.html");
            if (p.inputGrid != null)
            {
                mapPicBox.Grid = p.inputGrid;
            }
            mapPicBox.Visible = true;
            previewSpratePanel.Size = mapPicBox.Size;
            previewSpratePanel.Top = mapPicBox.Top;
            previewSpratePanel.Left = mapPicBox.Left;
            InitPreviewCombo();
            previewCombo.SelectedIndex = (int)(PreviewType.GridMap) - 1;

            LoadStatus();
            UpdateStatus();
        }

        private void InitPreviewCombo()
        {
            previewCombo.Items.Add("格網預覽圖");
            if (p.Is3DMode())
            {   //3D Mode才有垂向格網分布圖
                previewCombo.Items.Add("垂向格網分布圖");
            }
            else
            {
                previewCombo.Enabled = false;
            }
        }

        private void LoadStatus()
        {
            switch(p.importSource)
            {
                case RiverSimulationProfile.ImportSource.ImportFile:
                    inputFileRdo.Checked = true;
                    break;
                case RiverSimulationProfile.ImportSource.UserInput:
                    inputGridRdo.Checked = true;
                    break;
                default:
                    inputFileRdo.Checked = false;
                    inputGridRdo.Checked = false;
                    break;
            }
            switch (p.coorType)
            {
                case PictureBoxCtrl.GridPictureBox.CoorType.None:
                    noneRdo.Checked = true;
                    break;
                case PictureBoxCtrl.GridPictureBox.CoorType.TWD97:
                twd97Rdo.Checked = true;
                    break;
                case PictureBoxCtrl.GridPictureBox.CoorType.TWD67:
                twd67Rdo.Checked = true;
                    break;
            }

            verticalLevelNumberTxt.Text = p.verticalLevelNumber.ToString();
            switch (p.GetBackgroundMapType())
            {
                case RiverSimulationProfile.BackgroundMapType.None:
                    noBgRdo.Checked = true;
                    break;
                case RiverSimulationProfile.BackgroundMapType.GoogleStaticMap:
                    useGoogleBgRdo.Checked = true;
                    break;
                case RiverSimulationProfile.BackgroundMapType.ImportImage:
                    selectBgRdo.Checked = true;
                    break;
            }
        }

        private void UpdateStatus()
        {
            bitmapGrp.Enabled = p.IsMapPosition();
            coorTypeGroup.Enabled = p.IsMapPosition();
            gridDataBtn.Enabled = (p.importSource == RiverSimulationProfile.ImportSource.ImportFile) && (p.inputGrid != null) && (p.inputGrid.GetI > 0) && (p.inputGrid.GetJ > 0);
            reverseGridBtn.Enabled = (p.inputGrid != null);
            //coorSelCombo.Enabled = (p.GetBackgroundMapType() == RiverSimulationProfile.BackgroundMapType.GoogleStaticMap);
            if(p.coorType == PictureBoxCtrl.GridPictureBox.CoorType.None)
            {
                useGoogleBgRdo.Enabled = false;
                selectBgRdo.Enabled = false;
            }
            else
            {
                useGoogleBgRdo.Enabled = true;
                selectBgRdo.Enabled = true;
            }
            UpdateActiveFunctions();
        }

        private void UpdateActiveFunctions()
        {
            verticalLevelNumberTxt.Enabled = p.Is3DMode();
            levelProportionBtn.Enabled = p.Is3DMode();
        }

        private enum PreviewType
        {
            None,
            GridMap,    //格網地圖
            Sprate,     //垂向格網分布圖
        }

        private void SwitchPreivewCombo(PreviewType n)
        {
            previewCombo.SelectedIndex = (int)(n) - 1;
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

        private bool ConvertVerticalLevelNumber()
        {
            if (!ControllerUtility.CheckConvertInt32(ref p.verticalLevelNumber, verticalLevelNumberTxt, "請輸入正確的垂向格網分層數目！", ControllerUtility.CheckType.GreaterThanTwo))
            {
                return false;
            }

            if (p.levelProportion != null && p.levelProportion.Length != p.verticalLevelNumber)
            {
                MessageBox.Show("修改過垂向格網分層數目，需重新輸入分層比例！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                p.levelProportion = null;
            }
            return true;
        }

        private bool DoConvert()
        {
            if(p.inputGrid == null)
            {
                MessageBox.Show("尚未輸入水平格網！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (p.Is3DMode() && !ConvertVerticalLevelNumber())
            {   //3D模式要輸入分層比例
                return false;
            }

            if (p.Is3DMode() && p.levelProportion == null)
            {   ////3D模式要輸入分層比例
                MessageBox.Show("尚未輸入分層比例！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void inputFileRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            inputFileBtn.Enabled = chk;
            if (chk)
            {
                gridDataBtn.Enabled = (p.inputGrid != null) && (p.inputGrid.GetI > 0) && (p.inputGrid.GetJ > 0);
            }
            else
            {
                gridDataBtn.Enabled = chk;
            }

            if(chk)
            {
                p.importSource = RiverSimulationProfile.ImportSource.ImportFile;
             /*               //Auto select file.
                DialogResult result = inputFileDlg.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    inputFilePath.Text = inputFileDlg.FileName;
                }
            */
             }
       }

        private void inputFileBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = inputFileDlg.ShowDialog(); // Show the dialog.
            if (result != DialogResult.OK) // Test result.
            {
                return;
            }

            string ext = Path.GetExtension(inputFileDlg.FileName);
            bool loadOK = false;
            if(ext.ToLower() == ".geo")
            {
                loadOK = RiverSimulationProfile.profile.ReadInputGridGeo(inputFileDlg.FileName);
            }
            else if (ext.ToLower() == ".grd")
            {
                DialogResult depSel = selectDepFile.ShowDialog(); // Show the dialog.
                string depPath = "";
                if (depSel == DialogResult.OK) // Test result.
                {
                    depPath = selectDepFile.FileName;
                }
                loadOK = RiverSimulationProfile.profile.ReadInputGridGrd(inputFileDlg.FileName, depPath);
            }
            //inputFilePath.Text = inputFileDlg.FileName;
            if (!loadOK)
            {
                MessageBox.Show("無法讀取所選檔案", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            mapPicBox.Grid = RiverSimulationProfile.profile.inputGrid;
            SwitchPreivewCombo(PreviewType.GridMap);
            UpdateStatus();
        }

        private void inputGridRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            inputGridBtn.Enabled = chk;
            if(chk)
            {
                p.importSource = RiverSimulationProfile.ImportSource.UserInput;
             /*
               //Auto show input form
                ImportTableForm form = new ImportTableForm();
                if (DialogResult.OK == form.ShowDialog())
                {

                }
            */
            }
        }

        private void inputGridBtn_Click(object sender, EventArgs e)
        {
            ImportTableForm form = new ImportTableForm();
            if (p.inputGrid != null)
            {
                form.SetFormMode(true, p.inputGrid.GetJ, p.inputGrid.GetI, p.inputGrid);
            }
            else
            {
                form.SetFormMode(false, 0, 0);
            }
            if (DialogResult.OK == form.ShowDialog())
            {
                mapPicBox.Grid = form.gridData;
                RiverSimulationProfile.profile.inputGrid = form.gridData;
                SwitchPreivewCombo(PreviewType.GridMap);
                UpdateStatus();
            }
        }

        private void noBgRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            if (chk)
            {
                p.ClearBackgroundMapType();
                mapPicBox.ClearMapBackground();
                SwitchPreivewCombo(PreviewType.GridMap);
                UpdateStatus();
            }
        }

        private void useGoogleBgRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            //RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (chk)
            {
                if(p.DownloadGoogleStaticMap())
                {
                    mapPicBox.SetMapBackground(p.tl, p.tr, p.bl, p.br);
                }
                SwitchPreivewCombo(PreviewType.GridMap);
                UpdateStatus();
            }

        }

        private void selectBgRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            //RiverSimulationProfile p = RiverSimulationProfile.profile;
            selectBgBtn.Enabled = chk;
            if (chk)
            {
                p.SetImportImageMode();
                mapPicBox.SetMapBackground(p.imagePath, p.sourceE, p.sourceN, p.sourceW, p.sourceH);
                SwitchPreivewCombo(PreviewType.GridMap);
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
                if (form.HasJgw() || DialogResult.OK == form.ShowDialog())
                {
                    imgInfoBtn.Enabled = true;
                    RiverSimulationProfile.profile.SetImportImage(selectBgDlg.FileName, form.e, form.n, form.w, form.h);
                    mapPicBox.SetMapBackground(selectBgDlg.FileName, form.e, form.n, form.w, form.h);
                    SwitchPreivewCombo(PreviewType.GridMap);
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
                SwitchPreivewCombo(PreviewType.GridMap);
                UpdateStatus();
            }

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
                MessageBox.Show("CCHE-Mesh未安裝!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void separateProportionBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertVerticalLevelNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(levelProportionBtn.Text, 1, p.verticalLevelNumber, levelProportionBtn.Text, "分層比例", "格網分層",
                TableInputForm.InputFormType.SeparateForm, 90, 120, true, true, false, p.levelProportion);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.levelProportion = (double[])form.SeparateData().Clone();
                SwitchPreivewCombo(PreviewType.Sprate);
                DrawPreview();
            }
        }
        

        private void DrawPreview()
        {
            //RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (p.levelProportion == null)
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
            int h = p.verticalLevelNumber * SeparateTextHeight;
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

            textY += realH / p.verticalLevelNumber;
            for (int i = 1; i < p.verticalLevelNumber - 1; ++i)
            {
                startY = SeparateHeightGap + (float)(1.0 - p.levelProportion[p.verticalLevelNumber - i - 1]) * realH;
                pt1 = new Point(0, (int)startY);
                pt2 = new Point(SeparatePicWidth, (int)(startY));
                g.DrawLine(pen, pt1, pt2);


                rcText = new RectangleF(SeparateTextX, textY, SeparateWidth - SeparateTextX, SeparateTextSize + SeparateHeightGap);
                txt = (p.levelProportion[p.verticalLevelNumber - i - 1]).ToString();
                g.DrawString(txt, leftFont, brush, rcText, stringFormat);

                pt1 = new Point(SeparatePicWidth + textGap, (int)startY);
                pt2 = new Point(SeparateTextX - textGap, (int)(textY + SeparateTextSize / 2));
                g.DrawLine(pen, pt1, pt2);

                textY += realH / p.verticalLevelNumber;


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

        private void showGridMapCtrls_MouseHover(object sender, EventArgs e)
        {
            //ShowGridMap(PicBoxType.GridMap);

        }

        private void showSeparateCtrls_MouseHover(object sender, EventArgs e)
        {
            //ShowGridMap(PicBoxType.Sprate);
        }

        private void previewCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreviewType n = (PreviewType)(previewCombo.SelectedIndex + 1);
            //SwitchPreivewCombo(n);
            switch (n)
            {
                case PreviewType.GridMap:
                    mapPicBox.Visible = true;
                    previewSpratePanel.Visible = false;
                    break;
                case PreviewType.Sprate:
                    mapPicBox.Visible = false;
                    previewSpratePanel.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void reverseGridBtn_Click(object sender, EventArgs e)
        {
            RiverGrid grid = new RiverGrid(p.inputGrid);
            for(int i = 0; i < grid.GetI; ++i)
            {
                for(int j = 0; j < grid.GetJ; ++j)
                {
                    grid.inputCoor[i, j] = p.inputGrid.inputCoor[grid.GetI - i - 1, grid.GetJ - j - 1];
                }
            }
            p.inputGrid = new RiverGrid(grid);
            mapPicBox.Grid = RiverSimulationProfile.profile.inputGrid;
            SwitchPreivewCombo(PreviewType.GridMap);
            //UpdateStatus();

        }

        private void coorSelCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (p.coorType != coorSelCombo.SelectedIndex)
            //{
            //    if (coorSelCombo.SelectedIndex == (int)RiverSimulationProfile.TWD97)
            //    {
            //        p.ConvertToTwd97();
            //    }
            //    else
            //    {
            //        p.ConvertToTwd67();
            //    }
            //    mapPicBox.Grid = RiverSimulationProfile.profile.inputGrid;
                
            //    if (p.DownloadGoogleStaticMap())
            //    {
            //        mapPicBox.SetMapBackground(p.tl, p.tr, p.bl, p.br);
            //    }
            //    //SwitchPreivewCombo(PreviewType.GridMap);
            //    UpdateStatus();
            //}
        }

        private void twd97Rdo_CheckedChanged(object sender, EventArgs e)
        {
            if (p.coorType == PictureBoxCtrl.GridPictureBox.CoorType.TWD97)
                return;

            if (!(sender as RadioButton).Checked)
                return;
            //p.ConvertToTwd97();
            //mapPicBox.Grid = RiverSimulationProfile.profile.inputGrid;
            p.coorType = PictureBoxCtrl.GridPictureBox.CoorType.TWD97;
            if (p.GetBackgroundMapType() == RiverSimulationProfile.BackgroundMapType.GoogleStaticMap)
            {
                if (p.DownloadGoogleStaticMap())
                {
                    mapPicBox.SetMapBackground(p.tl, p.tr, p.bl, p.br);
                }
            }
            UpdateStatus();
        }

        private void twd67Rdo_CheckedChanged(object sender, EventArgs e)
        {
            if (p.coorType == PictureBoxCtrl.GridPictureBox.CoorType.TWD67)
                return;
            if (!(sender as RadioButton).Checked)
                return;

            //p.ConvertToTwd67();
            //mapPicBox.Grid = RiverSimulationProfile.profile.inputGrid;
            p.coorType = PictureBoxCtrl.GridPictureBox.CoorType.TWD67;
            if (p.GetBackgroundMapType() == RiverSimulationProfile.BackgroundMapType.GoogleStaticMap)
            {
                if (p.DownloadGoogleStaticMap())
                {
                    mapPicBox.SetMapBackground(p.tl, p.tr, p.bl, p.br);
                }
            }
            UpdateStatus();
        }

        private void noneRdo_CheckedChanged(object sender, EventArgs e)
        {
            if (p.coorType == PictureBoxCtrl.GridPictureBox.CoorType.None)
                return;
            if (!(sender as RadioButton).Checked)
                return;

            //p.ConvertToTwd97();
            //mapPicBox.Grid = RiverSimulationProfile.profile.inputGrid;
            p.coorType = PictureBoxCtrl.GridPictureBox.CoorType.None;
            if (p.GetBackgroundMapType() != RiverSimulationProfile.BackgroundMapType.None)
            {
                //p.ClearBackgroundMapType();
                //mapPicBox.ClearMapBackground();
                //SwitchPreivewCombo(PreviewType.GridMap);
                noBgRdo.Checked = true;
            }
            UpdateStatus();
        }

        private void runDelft3dBtn_Click(object sender, EventArgs e)
        {
            Delft3dMeshForm form = new Delft3dMeshForm();
            if (DialogResult.OK == form.ShowDialog())
            {
                //mapPicBox.Grid = form.gridData;
                //RiverSimulationProfile.profile.inputGrid = form.gridData;
                //SwitchPreivewCombo(PreviewType.GridMap);
                //UpdateStatus();
            }
            //{
            //    MessageBox.Show("CCHE-Mesh未安裝!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void docCcheMeshBtn_Click(object sender, EventArgs e)
        {
            if (!Utility.ShellExecute(Environment.CurrentDirectory + "\\CCHE-MESH 3.x User's Manual.pdf"))
            {
                MessageBox.Show("請安裝PDF閱讀程式！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void docDelft3dBtn_Click(object sender, EventArgs e)
        {
            if (!Utility.ShellExecute(Environment.CurrentDirectory + "\\Delft3D RGFGRID&QUICKIN UserManual.pdf"))
            {
                MessageBox.Show("請安裝PDF閱讀程式！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
    }
}

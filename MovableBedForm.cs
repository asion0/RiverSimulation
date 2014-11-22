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
    public partial class MovableBedForm : Form
    {
        public MovableBedForm()
        {
            InitializeComponent();
        }

        private SliderPanel sp = new SliderPanel();
        RiverSimulationProfile p = RiverSimulationProfile.profile;

        private void SettingButton_Click(object sender, EventArgs e)
        {
            Button orgBtn = sender as Button;
            if (orgBtn == valueParamBtn)
            {
                sp.SlidePanel(valueParamPanel, SliderPanel.Direction.ToRight, this.Size);
            }
            else if (orgBtn == physicalParamBtn)
            {
                sp.SlidePanel(physicalParamPanel, SliderPanel.Direction.ToRight, this.Size);
            }
            else if (orgBtn == transSandMethodBtn)
            {
                sp.SlidePanel(transSandMethodPanel, SliderPanel.Direction.ToRight, this.Size);
            }
            else if (orgBtn == seabedCompositionBtn)
            {
                sp.SlidePanel(seabedCompositionPanel, SliderPanel.Direction.ToRight, this.Size);
            }
            else if (orgBtn == rockbedBtn)
            {
                sp.SlidePanel(rockbedPanel, SliderPanel.Direction.ToRight, this.Size);
            }
            else if (orgBtn == rockStableBtn)
            {
                sp.SlidePanel(rockStablePanel, SliderPanel.Direction.ToRight, this.Size);
            }
            //else if (orgBtn == immersionBtn)
            //{
            //    sp.SlidePanel(immersionPanel, SliderPanel.Direction.ToRight);
            //}

        }

        private void MovableBedForm_Load(object sender, EventArgs e)
        {
            //string url = "file:///./" + Environment.CurrentDirectory + "\\D1-1.html";
            //comment.Navigate(new Uri(url));
            if (Program.programVersion.LiteVersion)
            {
                fullPanel.Visible = false;
            }

            if(p.waterHighSandContentEffectFunction)
            {
                this.normalSandMethodText.Visible = false;
                this.normalSandMethodPanel.Visible = false;
                this.highSandMethodText.Visible = true;
                this.highSandMethodPanel.Visible = true;
                this.highSandMethodText.Top = this.normalSandMethodText.Location.Y;
                this.highSandMethodPanel.Top = this.normalSandMethodPanel.Location.Y;
                this.highSandMethodPanel.Enabled = true;
            }
            else
            {
                this.normalSandMethodText.Visible = true;
                this.normalSandMethodPanel.Visible = true;
                this.highSandMethodText.Visible = false;
                this.highSandMethodPanel.Visible = false;
            }

            //this.Width = 1000;
            //this.Height = 720;
            valueParamPanel.Visible = false;
            physicalParamPanel.Visible = false;
            seabedCompositionPanel.Visible = false;
            rockStablePanel.Visible = false;
            rockbedPanel.Visible = false;
            transSandMethodPanel.Visible = false;

            this.CenterToParent();

            this.bedrockGrp.Enabled = RiverSimulationProfile.profile.bedrockFunction;
            this.quayStableAnalysisGrp.Enabled = RiverSimulationProfile.profile.quayStableAnalysisFunction;
            this.highSandContentFlowGrp.Enabled = p.waterHighSandContentEffectFunction;
            //this.highSandMethodPanel.Enabled = RiverSimulationProfile.profile.highSandContentEffectFunction;
            //highSandMethodPanel.Enabled = RiverSimulationProfile.profile.highSandContentFlowFunction;
            previewPicBox.Width = previewPanel.Width;
            previewPicBox.Height = previewPanel.Height;
            UpdateStatus();

        }

        private void UpdateStatus()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            sedimentParticlesNumTxt.Text = p.sedimentParticlesNum.ToString();
            seabedLevelNumTxt.Text = p.seabedLevelNum.ToString();
            outputCtrl3DGrp.Enabled = p.Is3DMode();

            DrawPreview();
        }

        private bool DoConvert()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int n = 0;
            if (!ControllerUtility.CheckConvertInt32(ref n, seabedLevelNumTxt.Text, "請輸入正確的底床分層數目！", ControllerUtility.CheckType.GreaterThanThree))
            {
                return false;
            }
            if (p.seabedLevelNum != n)
            {
                MessageBox.Show("變更底床分層數目將清除原先輸入之資料", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                p.seabedLevelArray = null;
                p.sedimentCompositionRatioArray = null;
            }
            p.seabedLevelNum = n;

            if (!ControllerUtility.CheckConvertInt32(ref n, sedimentParticlesNumTxt.Text, "請輸入正確的泥砂顆粒數目！", ControllerUtility.CheckType.GreaterThanTwo))
            {
                return false;
            }
            if (p.sedimentParticlesNum != n)
            {
                MessageBox.Show("變更泥砂顆粒數目將清除原先輸入之資料", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                p.sedimentCompositionRatioArray = null;
            }
            p.sedimentParticlesNum = n;


            return true;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (DoConvert())
            {
                sp.SlidePanel(null, SliderPanel.Direction.Back, this.Size);
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

        private void normalSandyFlowRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void highSandyFlowRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void selTranSandEquChk_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void selExpandEquChk_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void sedimentParticleSizeBtn_Click(object sender, EventArgs e)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;

            int n = 0;
            try
            {
                n = Convert.ToInt32(sedimentParticlesNumTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if(n < 1)
            {
                MessageBox.Show("請輸入正確的泥砂顆粒數目", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentParticleSizeBtn.Text, true, 1, n);
            if (DialogResult.OK == form.ShowDialog())
            {

            }

        }

        private void erosionMechanismsChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;

            if (chk)
            {
                if (waterErosionChk.Checked)
                {
                    waterErosionChk.Checked = false;
                    waterErosionChk.Checked = true;
                }
                if (sedimentErosionChk.Checked)
                {
                    sedimentErosionChk.Checked = false;
                    sedimentErosionChk.Checked = true;
                }
            }
            else
            {
                criticalShearStressBtn.Enabled = false;
                elasticityBtn.Enabled = false;
                tensileStrengthBtn.Enabled = false;
            }
            waterErosionChk.Enabled = chk;
            sedimentErosionChk.Enabled = chk;
        }

        private void sedimentErosionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            elasticityBtn.Enabled = chk;
            tensileStrengthBtn.Enabled = chk;
        }

        private void waterErosionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            criticalShearStressBtn.Enabled = chk;
        }

        private void criticalShearStressBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(criticalShearStressBtn.Text, false, 26, 50);   //二選一
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void elasticityBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(elasticityBtn.Text, false, 26, 50);   //二選一
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void tensileStrengthBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(tensileStrengthBtn.Text, false, 26, 50);   //二選一
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void bedrockElevationBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(bedrockElevationBtn.Text, false, 26, 50);   //二選一
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void cohesiveSedimentChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            cohesiveSedimentPanel.Enabled = chk;
        }

        private void DrawPreview()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            if(p.seabedLevelArray == null)
            {
                return;
            }

            const int SedimentParticlesWidth = 215;     //右方顯示泥砂顆粒狀態欄位寬度
            const int SeabedLevelWidth = 215;           //左方顯示底床分層狀態欄位寬度
            const int SedimentParticlesLineHeight = 12; //右方顯示泥砂顆粒狀態文字高度
            const int SeabedLevelLineHeight = 12;           //左方顯示底床分層狀態文字高度
            const int SeabedLevelGap = 8;               //左方顯示底床分層狀態文字垂直間隔
            const int SedimentParticlesGap = 8;               //右方顯示泥砂顆粒狀態文字垂直間隔
            Font leftFont = new Font("微軟正黑體", SeabedLevelLineHeight, FontStyle.Regular, GraphicsUnit.Point);
            Font rightFont = new Font("微軟正黑體", SedimentParticlesLineHeight, FontStyle.Regular, GraphicsUnit.Point);

            int cellH = (p.sedimentParticlesNum * SedimentParticlesLineHeight) + (p.sedimentParticlesNum + 1) * SedimentParticlesGap;


            int w = SedimentParticlesWidth + SeabedLevelWidth;
            int h = cellH * p.seabedLevelNum;
            Bitmap picBoxBmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(picBoxBmp);

            Pen pen = new Pen(Color.Black, 1.0f);
            SolidBrush leftBrush = new SolidBrush(Color.Black);
            SolidBrush rightBrush = new SolidBrush(Color.Black);
            Point pt1 = new Point(SeabedLevelWidth, 0);
            Point pt2 = new Point(SeabedLevelWidth, h);
            g.DrawLine(pen, pt1, pt2);


            float startY = (cellH - SeabedLevelGap * 3 - SeabedLevelLineHeight * 2) / 2.0f;
            for (int i = 0; i < p.seabedLevelArray.Length; ++i)
            {
                pt1 = new Point(0, cellH * (i + 1));
                 pt2 = new Point(w, cellH * (i + 1));
                g.DrawLine(pen, pt1, pt2);

                RectangleF rcLeftCell = new RectangleF(0, cellH  * i, SeabedLevelWidth, cellH);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                RectangleF rcLeftTxt1 = new RectangleF(0, startY, SeabedLevelWidth, SeabedLevelLineHeight + SeabedLevelGap);
                string txt = "底床分層" + " " + (p.seabedLevelArray.Length - i).ToString();
                g.DrawString(txt, leftFont, leftBrush, rcLeftTxt1, stringFormat);

                RectangleF rcLeftTxt2 = new RectangleF(0, startY + SeabedLevelGap + SeabedLevelLineHeight, SeabedLevelWidth, SeabedLevelLineHeight + SeabedLevelGap);
                txt = "厚度 :" + " " + p.seabedLevelArray[p.seabedLevelArray.Length - i - 1].ToString() + "m";
                g.DrawString(txt, leftFont, leftBrush, rcLeftTxt2, stringFormat);

                startY += cellH;
                if (p.sedimentCompositionRatioArray == null)
                {
                    continue;
                }
                //g.DrawRectangle(pen, rcLeftCell.X, rcLeftCell.Y, rcLeftCell.Width, SeabedLevelLineHeight);
                int sedimentCount = p.sedimentCompositionRatioArray.GetLength(0);
                float startRY = cellH * i + SedimentParticlesGap / 2.0f;
                StringFormat stringFormatR = new StringFormat();
                stringFormatR.Alignment = StringAlignment.Near;
                stringFormatR.LineAlignment = StringAlignment.Near;

                for (int j = 0; j < p.sedimentCompositionRatioArray.GetLength(0); ++j)
                {

                    RectangleF rcRightText = new RectangleF(SeabedLevelWidth + 40, startRY, SedimentParticlesWidth, SeabedLevelLineHeight + SedimentParticlesGap);
                    txt = "泥砂顆粒 " + (j + 1).ToString() + " : " + p.sedimentCompositionRatioArray[j, p.seabedLevelArray.Length - i - 1].ToString() + "%";
                    g.DrawString(txt, rightFont, rightBrush, rcRightText, stringFormatR);

                    startRY += SeabedLevelLineHeight + SedimentParticlesGap;
                    //g.DrawRectangle(pen, rcLeftCell.X, rcLeftCell.Y, rcLeftCell.Width, SeabedLevelLineHeight);
                }
            }




            g.Dispose();
            previewPicBox.Image = picBoxBmp;

            int picW = (previewPanel.Width > w) ? previewPanel.Width : w;
            int picH = (previewPanel.Height > h) ? previewPanel.Height : h;
            previewPicBox.Width = picW;
            previewPicBox.Height = picH;

            previewPanel.AutoScrollMinSize = new Size(w, h); 
        }

        private void seabedThicknessBtn_Click(object sender, EventArgs e)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (!DoConvert())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(seabedThicknessBtn.Text, p.seabedLevelNum, 1, seabedThicknessBtn.Text, "", "底床分層厚度",
                TableInputForm.InputFormType.SeabedThicknessForm, 90, 120, true, false, true, p.seabedLevelArray);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.seabedLevelArray = (double[])form.SeabedThicknessData().Clone();
                DrawPreview();
            }
        }


        private void sedimentCompositionRatioBtn_Click(object sender, EventArgs e)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (!DoConvert())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentCompositionRatioBtn.Text, p.sedimentParticlesNum, p.seabedLevelNum, sedimentCompositionRatioBtn.Text, "泥砂顆粒", "底床分層",
                TableInputForm.InputFormType.SedimentCompositionRatioForm ,90, 120, true, false, false, p.sedimentCompositionRatioArray);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.sedimentCompositionRatioArray = (double[,])form.SedimentCompositionRatioData().Clone();
                DrawPreview();
            }
        }

        private void noScourElevationBtn_Click(object sender, EventArgs e)
        {

            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentParticleSizeBtn.Text, true, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void diffusionEquationChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            diffusionEquationCombo.Enabled = chk;
        }

        //private void selTransSandMethodChk_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool chk = (sender as CheckBox).Checked;
        //    selTransSandMethodCombo.Enabled = chk;
        //}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void alluviumChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            //cohesiveSedimentPanel.Enabled = chk;
        }

        private void alluviumPanel_EnabledChanged(object sender, EventArgs e)
        {
        //    bool eab = (sender as Panel).Enabled;
        //    seabedCompositionChk.Enabled = eab;
        //    noScourElevationBtn.Enabled = eab;
        }

        private void bedrockChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            rockbedPanel2.Enabled = chk;
        }

        private void rockbedPanel2_EnabledChanged(object sender, EventArgs e)
        {
            bool eab = (sender as Panel).Enabled;
            bedrockErosionMechanismChk.Enabled = eab;
            bedrockElevationBtn.Enabled = eab;
        }

        private void analysisPositionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            analysisPositionPanel.Enabled = chk;
        }

        private void infiltrationEffectChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            infiltrationEffectPanel.Enabled = chk;

        }

        private void quayGeometryChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            quayGeometryPanel.Enabled = chk;

        }

        private void quaySoilPropertiesChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            quaySoilPropertiesPanel.Enabled = chk;

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void noScourElevationChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            noScourElevationBtn.Enabled = chk;

        }

        private void hourRdo_CheckedChanged(object sender, EventArgs e)
        {
            timeLbl.Text = hourRdo.Text;
        }

        private void minuteRdo_CheckedChanged(object sender, EventArgs e)
        {
            timeLbl.Text = minuteRdo.Text;
        }

        private void sedimentParticlesNumTxt_MouseHover(object sender, EventArgs e)
        {

            string url = "file:///" + Environment.CurrentDirectory.Replace('\\', '/') + "/D1-2-1-3.html"; ;
            if (comment.Url == null || comment.Url.ToString() != url)
            {
                comment.Navigate(new Uri(url));
            }

        }
 


    }
}

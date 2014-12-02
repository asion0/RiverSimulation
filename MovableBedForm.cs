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

        RiverSimulationProfile p = RiverSimulationProfile.profile;
        private SliderPanel sp = new SliderPanel();
        public void SetForm(RiverSimulationProfile profile)
        {
            p = profile;
        }

        private void MovableBedForm_Load(object sender, EventArgs e)
        {
            valueParamPanel.Visible = false;            
            physicalParamPanel.Visible = false;
            seabedCompositionPanel.Visible = false;
            rockStablePanel.Visible = false;
            rockbedPanel.Visible = false;
            transSandMethodPanel.Visible = false;

            ControllerUtility.SetHtmlUrl(comment, "Logo.html");
            if (p.inputGrid != null)
            {
                mapPicBox.Grid = p.inputGrid;
            }
            mapPicBox.Visible = true;
            previewPanel.Size = mapPicBox.Size;
            previewPanel.Top = mapPicBox.Top;
            previewPanel.Left = mapPicBox.Left;

            previewPicBox.Size = previewPanel.Size;
            previewPicBox.Top = previewPanel.Top;
            previewPicBox.Left = previewPanel.Left;

            LoadStatus();
            UpdateStatus();
        }

        private void LoadStatus()
        {
            
            //動床參數
            //2.1 數值參數 =========================================
            waterTimeSpanTxt.Text = p.waterTimeSpan.ToString();
            waterOutputFrequencyTxt.Text = p.waterOutputFrequency.ToString();

            //2.1.3 輸出控制
            //2D
            outputControlBottomElevationChk.Checked = p.outputControlBottomElevation;
            outputControlAverageDepthDensityChk.Checked = p.outputControlAverageDepthDensity;
            outputControlErosionDepthChk.Checked = p.outputControlErosionDepth;

            //3D
            outputControlDensityInformation3DChk.Checked = p.outputControlDensityInformation3D;

            //2.1.4 選擇擴散公式
            diffusionFormulaUseChk.Checked = p.diffusionFormulaUse;
            diffusionFormulaCombo.SelectedIndex = (int)p.diffusionFormula - 1;

            diffusionBonusProportionalInMainstreamTxt.Text = p.diffusionBonusProportionalInMainstream.ToString();
            diffusionBonusProportionalInSideflowTxt.Text = p.diffusionBonusProportionalInSideflow.ToString();
            diffusionBonusProportionalInSurfaceTxt.Text = p.diffusionBonusProportionalInSurface.ToString();
            diffusionBonusProportionalInBottomTxt.Text = p.diffusionBonusProportionalInBottom.ToString();
            
            //2.2 物理參數
            kinematicViscosityCoefficientTxt.Text = p.kinematicViscosityCoefficient.ToString();
            sedimentPoreRatioTxt.Text = p.sedimentPoreRatio.ToString();
            sedimentDensityTxt.Text = p.sedimentDensity.ToString();
            sedimentParticlesNumberTxt.Text = p.sedimentParticlesNumber.ToString();

            //2.3 底床組成
            bottomLevelNumberTxt.Text = p.bottomLevelNumber.ToString();                 //2.3.1 底床分層數目單一數值 整數(>0) a. 使用者輸入底床分層數目後
            shenCohesiveSedimentChk.Checked = p.shenCohesiveSediment;
            surfaceErosionChk.Checked = p.surfaceErosion;
            surfaceErosionCoefficientTxt.Text = p.surfaceErosionCoefficient.ToString();
            surfaceErosionCriticalShearStressTxt.Text = p.surfaceErosionCriticalShearStress.ToString();
            massiveErosionChk.Checked = p.massiveErosion;
            massiveErosionCriticalShearStressTxt.Text = p.massiveErosionCriticalShearStress.ToString();
            noErosionElevationChk.Checked = p.noErosionElevation;

            //2.4 輸砂公式
            switch(p.sandTransportEquation)
            {
                case RiverSimulationProfile.SandTransportEquationType.SandTransportEquation1:
                    sandTransportEquation1Rdo.Checked = true;
                    highSandTransportEquation1Rdo.Checked = true;
                    break;
                case RiverSimulationProfile.SandTransportEquationType.SandTransportEquation2:
                    sandTransportEquation2Rdo.Checked = true;
                     highSandTransportEquation2Rdo.Checked = true;
                   break;
                case RiverSimulationProfile.SandTransportEquationType.SandTransportEquation3:
                   sandTransportEquation3Rdo.Checked = true;
                   highSandTransportEquation3Rdo.Checked = true;
                   break;
                case RiverSimulationProfile.SandTransportEquationType.HighSandTransportEquation4:
                   highSandTransportEquation4Rdo.Checked = true;
                   break;
                case RiverSimulationProfile.SandTransportEquationType.HighSandTransportEquation5:
                   highSandTransportEquation5Rdo.Checked = true;
                   break;
                case RiverSimulationProfile.SandTransportEquationType.HighSandTransportEquation6:
                   highSandTransportEquation6Rdo.Checked = true;
                   break;
                default:
                   sandTransportEquation1Rdo.Checked = false;
                   sandTransportEquation2Rdo.Checked = false;
                   sandTransportEquation3Rdo.Checked = false;
                   highSandTransportEquation1Rdo.Checked = false;
                   highSandTransportEquation2Rdo.Checked = false;
                   highSandTransportEquation3Rdo.Checked = false;
                   highSandTransportEquation4Rdo.Checked = false;
                   highSandTransportEquation5Rdo.Checked = false;
                   highSandTransportEquation6Rdo.Checked = false;
                   break;
            }
        }

        private void UpdateStatus()
        {
            //RiverSimulationProfile p = RiverSimulationProfile.profile;
            //sedimentParticlesNumTxt.Text = p.sedimentParticlesNum.ToString();
            //seabedLevelNumTxt.Text = p.seabedLevelNum.ToString();
            diffusionFormulaCombo.Enabled = p.diffusionFormulaUse;


            //DrawPreview();
            UpdateActiveFunctions();
        }

        private void UpdateActiveFunctions()
        {
            //數值參數 輸出控制3D 面板
            outputCtrl3DGrp.Enabled = p.Is3DMode();
            bedrockGrp.Enabled = RiverSimulationProfile.profile.bedrockFunction;
            quayStableAnalysisGrp.Enabled = RiverSimulationProfile.profile.quayStableAnalysisFunction;
            highSandContentFlowGrp.Enabled = p.waterHighSandContentEffectFunction;

            if (p.waterHighSandContentEffectFunction)
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

            if (Program.programVersion.LiteVersion)
            {
                fullPanel.Visible = false;
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

        private bool DoConvert()
        {
            if(!ConvertValueParam())
            {
                return false;
            }

            if (!ConvertPhysicalParam())
            {
                return false;
            }

            if (!ConvertSeabedComposition())
            {
                return false;
            }

            if(!ConvertTransSandMethod())
            {
                return false;
            }

            if (!ConvertBedrock())
            {
                return false;
            }
            









            /*
            int n = 0;
            if (!ControllerUtility.CheckConvertInt32(ref n, seabedLevelNumTxt, "請輸入正確的底床分層數目！", ControllerUtility.CheckType.GreaterThanThree))
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

            if (!ControllerUtility.CheckConvertInt32(ref n, sedimentParticlesNumTxt, "請輸入正確的泥砂顆粒數目！", ControllerUtility.CheckType.GreaterThanTwo))
            {
                return false;
            }
            if (p.sedimentParticlesNum != n)
            {
                MessageBox.Show("變更泥砂顆粒數目將清除原先輸入之資料", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                p.sedimentCompositionRatioArray = null;
            }
            p.sedimentParticlesNum = n;
            */

            return true;
        }

        private bool ConvertValueParam()
        {
            if (!ControllerUtility.CheckConvertDouble(ref p.waterTimeSpan, waterTimeSpanTxt, "請輸入正確的時間間距！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertInt32(ref p.waterOutputFrequency, waterOutputFrequencyTxt, "請輸入正確的輸出頻率！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }

            if(p.diffusionFormulaUse)
            {
                if(p.diffusionFormula == RiverSimulationProfile.DiffusionFormulaType.None)
                {
                    MessageBox.Show("請選擇擴散公式 ！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.diffusionBonusProportionalInMainstream, diffusionBonusProportionalInMainstreamTxt, "請輸入正確的主流方向擴散係數加成比例！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.diffusionBonusProportionalInSideflow, diffusionBonusProportionalInSideflowTxt, "請輸入正確的側方向擴散係數加成比例！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }
            if (!ControllerUtility.CheckConvertDouble(ref p.diffusionBonusProportionalInSurface, diffusionBonusProportionalInSurfaceTxt, "請輸入正確的水面擴散係數加成比例！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.diffusionBonusProportionalInBottom, diffusionBonusProportionalInBottomTxt, "請輸入正確的底床擴散係數加成比例！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }


            return true;
        }

        private bool ConvertPhysicalParam()
        {
            if (!ControllerUtility.CheckConvertDouble(ref p.kinematicViscosityCoefficient, kinematicViscosityCoefficientTxt, "請輸入正確的動力黏滯係數！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.sedimentPoreRatio, sedimentPoreRatioTxt, "請輸入正確的泥砂孔隙比！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.sedimentDensity, sedimentDensityTxt, "請輸入正確的泥砂密度！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }

            if (!ConvertSedimentParticlesNumber())  //讀取泥砂顆粒數目
            {   
                return false;
            }
            return true;
        }

        private bool ConvertSedimentParticlesNumber()
        {
            if (!ControllerUtility.CheckConvertInt32(ref p.sedimentParticlesNumber, sedimentParticlesNumberTxt, "請輸入正確的泥砂顆粒數目！", ControllerUtility.CheckType.GreaterThanTwo))
            {
                return false;
            }
            return true;
        }

        private bool ConvertBottomLevelNumber()
        {
            if (!ControllerUtility.CheckConvertInt32(ref p.bottomLevelNumber, bottomLevelNumberTxt, "請輸入正確的底床分層數目！", ControllerUtility.CheckType.GreaterThanThree))
            {
                return false;
            }
            return true;
        }

        private bool ConvertSeabedComposition()
        {
            if (!ConvertBottomLevelNumber())
            {
                return false;
            }

            if (p.shenCohesiveSediment)
            {   //凝聚性沉滓
                if (p.surfaceErosion)
                {   //表層沖刷
                    if (!ControllerUtility.CheckConvertDouble(ref p.surfaceErosionCoefficient, surfaceErosionCoefficientTxt, "請輸入正確的表層沖刷係數！", ControllerUtility.CheckType.GreaterThanZero))
                    {
                        return false;
                    }
                    if (!ControllerUtility.CheckConvertDouble(ref p.surfaceErosionCriticalShearStress, surfaceErosionCriticalShearStressTxt, "請輸入正確的表層沖刷臨界剪應力！", ControllerUtility.CheckType.GreaterThanZero))
                    {
                        return false;
                    }
                }
                if (p.massiveErosion)
                {   //塊狀沖蝕
                    if (!ControllerUtility.CheckConvertDouble(ref p.massiveErosionCriticalShearStress, massiveErosionCriticalShearStressTxt, "請輸入正確的塊狀沖蝕臨界剪應力！", ControllerUtility.CheckType.GreaterThanZero))
                    {
                        return false;
                    }
                }
                if(p.noErosionElevation)
                {
                    //Check p.noErosionElevationArray
                }
            }
            return true;
        }

        private bool ConvertTransSandMethod()
        {
            if (transSandMethodBtn.Enabled && p.sandTransportEquation == RiverSimulationProfile.SandTransportEquationType.None)
            {
                MessageBox.Show("請輸入正確的輸砂公式 ！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private bool ConvertBedrock()
        {
            if (!ControllerUtility.CheckConvertDouble(ref p.waterJettingAlpha, waterJettingAlphaTxt, "", ControllerUtility.CheckType.NoCheck))
            {
                return false;
            }
            if (!ControllerUtility.CheckConvertDouble(ref p.waterJettingBeta, waterJettingBetaTxt, "", ControllerUtility.CheckType.NoCheck))
            {
                return false;
            }
            return true;
        }
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
                ControllerUtility.SetHtmlUrl(comment, "D2-4.html");
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

 

        private void Back_Click(object sender, EventArgs e)
        {
            //if (DoConvert())
            //{
                sp.SlidePanel(null, SliderPanel.Direction.Back, this.Size);
            //}
            ControllerUtility.SetHtmlUrl(comment, "Logo.html");

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
                n = Convert.ToInt32(sedimentParticlesNumberTxt.Text);
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
            form.SetFormMode(sedimentParticlesNumberBtn.Text, true, 1, n);
            if (DialogResult.OK == form.ShowDialog())
            {

            }

        }

        private void DrawPreview()
        {
            //RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (p.bottomLevelArray == null)
            {
                return;
            }

            const int SedimentParticlesWidth = 215;     //右方顯示泥砂顆粒狀態欄位寬度
            const int SeabedLevelWidth = 215;           //左方顯示底床分層狀態欄位寬度
            const int SedimentParticlesLineHeight = 12; //右方顯示泥砂顆粒狀態文字高度
            const int SeabedLevelLineHeight = 12;           //左方顯示底床分層狀態文字高度
            const int SeabedLevelGap = 8;               //左方顯示底床分層狀態文字垂直間隔
            const int SedimentParticlesGap = 8;               //右方顯示泥砂顆粒狀態文字垂直間隔

            Font leftFont = Control.DefaultFont;
            Font rightFont = Control.DefaultFont;
            //Font leftFont = new Font("微軟正黑體", SeabedLevelLineHeight, FontStyle.Regular, GraphicsUnit.Point);
            //Font rightFont = new Font("微軟正黑體", SedimentParticlesLineHeight, FontStyle.Regular, GraphicsUnit.Point);

            int cellH = (p.bottomLevelNumber * SedimentParticlesLineHeight) + (p.sedimentParticlesNumber + 1) * SedimentParticlesGap;


            int w = SedimentParticlesWidth + SeabedLevelWidth;
            int h = cellH * p.bottomLevelNumber;
            Bitmap picBoxBmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(picBoxBmp);

            Pen pen = new Pen(Color.Black, 1.0f);
            SolidBrush leftBrush = new SolidBrush(Color.Black);
            SolidBrush rightBrush = new SolidBrush(Color.Black);
            Point pt1 = new Point(SeabedLevelWidth, 0);
            Point pt2 = new Point(SeabedLevelWidth, h);
            g.DrawLine(pen, pt1, pt2);


            float startY = (cellH - SeabedLevelGap * 3 - SeabedLevelLineHeight * 2) / 2.0f;
            for (int i = 0; i < p.bottomLevelArray.Length; ++i)
            {
                pt1 = new Point(0, cellH * (i + 1));
                 pt2 = new Point(w, cellH * (i + 1));
                g.DrawLine(pen, pt1, pt2);

                RectangleF rcLeftCell = new RectangleF(0, cellH  * i, SeabedLevelWidth, cellH);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                RectangleF rcLeftTxt1 = new RectangleF(0, startY, SeabedLevelWidth, SeabedLevelLineHeight + SeabedLevelGap);
                string txt = "底床分層" + " " + (p.bottomLevelArray.Length - i).ToString();
                if(i == 0)
                {
                    txt = "作用層";
                }
                g.DrawString(txt, leftFont, leftBrush, rcLeftTxt1, stringFormat);

                RectangleF rcLeftTxt2 = new RectangleF(0, startY + SeabedLevelGap + SeabedLevelLineHeight, SeabedLevelWidth, SeabedLevelLineHeight + SeabedLevelGap);
                txt = "厚度 :" + " " + p.bottomLevelArray[p.bottomLevelArray.Length - i - 1].ToString() + "m";
                g.DrawString(txt, leftFont, leftBrush, rcLeftTxt2, stringFormat);

                startY += cellH;
                if (p.sedimentCompositionArray == null)
                {
                    continue;
                }
                //g.DrawRectangle(pen, rcLeftCell.X, rcLeftCell.Y, rcLeftCell.Width, SeabedLevelLineHeight);
                int sedimentCount = p.sedimentCompositionArray.GetLength(0);
                float startRY = cellH * i + SedimentParticlesGap / 2.0f;
                StringFormat stringFormatR = new StringFormat();
                stringFormatR.Alignment = StringAlignment.Near;
                stringFormatR.LineAlignment = StringAlignment.Near;

                for (int j = 0; j < p.sedimentCompositionArray.GetLength(0); ++j)
                {

                    RectangleF rcRightText = new RectangleF(SeabedLevelWidth + 40, startRY, SedimentParticlesWidth, SeabedLevelLineHeight + SedimentParticlesGap);
                    txt = "泥砂顆粒 " + (j + 1).ToString() + " : " + p.sedimentCompositionArray[j, p.bottomLevelArray.Length - i - 1].ToString() + "%";
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



        private void noScourElevationBtn_Click(object sender, EventArgs e)
        {

            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentParticlesNumberBtn.Text, true, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
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

        private void analysisPositionChk_CheckedChanged(object sender, EventArgs e)
        {

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
            ControllerUtility.SetHtmlUrl(comment, "D1-2-1-3.html");
        }
        //============================================================================================================================
        private void outputControlBottomElevationChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.outputControlBottomElevation = chk;
        }

        private void outputControlAverageDepthDensityChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.outputControlAverageDepthDensity = chk;
        }

        private void outputControlErosionDepthChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.outputControlErosionDepth = chk;
        }

        private void outputControlDensityInformation3DChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.outputControlDensityInformation3D = chk;
        }

        private void diffusionFormulaChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.diffusionFormulaUse = chk;
            UpdateStatus();
        }

        private void diffusionFormulaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.diffusionFormula = (RiverSimulationProfile.DiffusionFormulaType)((sender as ComboBox).SelectedIndex + 1);
        }

        private void sedimentParticlesNumberBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertSedimentParticlesNumber())
            {
                return;
            }


            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentParticlesNumberBtn.Text, true, 1, p.sedimentParticlesNumber);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void seabedThicknessBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertBottomLevelNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(bottomLevelBtn.Text, 1, p.bottomLevelNumber, bottomLevelBtn.Text, "底床分層厚度", "底床分層",
                TableInputForm.InputFormType.SeabedThicknessForm, 90, 120, true, true, false, p.bottomLevelArray);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.bottomLevelArray = (double[])form.SeabedThicknessData().Clone();
                DrawPreview();
            }
        }

        private void sedimentCompositionRatioBtn_Click(object sender, EventArgs e)
        {
            if(!ConvertSedimentParticlesNumber())
            {
                return;
            }

            if (!ConvertBottomLevelNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentCompositionBtn.Text, p.sedimentParticlesNumber, p.bottomLevelNumber, sedimentCompositionBtn.Text, "泥砂顆粒", "底床分層",
                TableInputForm.InputFormType.SedimentCompositionRatioForm, 90, 120, true, false, false, p.sedimentCompositionArray);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.sedimentCompositionArray = (double[,])form.SedimentCompositionRatioData().Clone();
                DrawPreview();
            }
        }

        private void shenCohesiveSedimentChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.shenCohesiveSediment = chk;
            shenCohesiveSedimentPanel.Enabled = chk;
        }

        private void surfaceErosionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.surfaceErosion = chk;
            surfaceErosionCoefficientTxt.Enabled = chk;
            surfaceErosionCriticalShearStressTxt.Enabled = chk;
        }

        private void massiveErosionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.massiveErosion = chk;
            massiveErosionCriticalShearStressTxt.Enabled = chk;
        }

        private void noErosionElevationChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.noErosionElevation = chk;
            noErosionElevationBtn.Enabled = chk;

        }

        private void sandTransportEquationRdo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = sender as RadioButton;

            if(rdo == sandTransportEquation1Rdo)
            {
                p.sandTransportEquation = RiverSimulationProfile.SandTransportEquationType.SandTransportEquation1;
            }
            else if (rdo == sandTransportEquation2Rdo)
            {
                p.sandTransportEquation = RiverSimulationProfile.SandTransportEquationType.SandTransportEquation2;
            }
            else if (rdo == sandTransportEquation3Rdo)
            {
                p.sandTransportEquation = RiverSimulationProfile.SandTransportEquationType.SandTransportEquation3;
            }
            else if (rdo == highSandTransportEquation1Rdo)
            {
                p.sandTransportEquation = RiverSimulationProfile.SandTransportEquationType.SandTransportEquation1;
            }
            else if (rdo == highSandTransportEquation2Rdo)
            {
                p.sandTransportEquation = RiverSimulationProfile.SandTransportEquationType.SandTransportEquation2;
            }
            else if (rdo == highSandTransportEquation3Rdo)
            {
                p.sandTransportEquation = RiverSimulationProfile.SandTransportEquationType.SandTransportEquation3;
            }
            else if (rdo == highSandTransportEquation4Rdo)
            {
                p.sandTransportEquation = RiverSimulationProfile.SandTransportEquationType.HighSandTransportEquation4;
            }
            else if (rdo == highSandTransportEquation5Rdo)
            {
                p.sandTransportEquation = RiverSimulationProfile.SandTransportEquationType.HighSandTransportEquation5;
            }
            else if (rdo == highSandTransportEquation6Rdo)
            {
                p.sandTransportEquation = RiverSimulationProfile.SandTransportEquationType.HighSandTransportEquation6;
            }
        }
        //岩床===========================================================================================================
        private void waterJettingChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.waterJetting = chk;
            waterJettingAlphaTxt.Enabled = chk;
            waterJettingBetaTxt.Enabled = chk;
        }
        
        private void sedimentErosionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.sedimentErosion = chk;
            sedimentErosionElasticModulusValueBtn.Enabled = chk;
            sedimentErosionTensileStrengthValueBtn.Enabled = chk;
        }

        private void bedrockElevationChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.bedrockElevation = chk;
            bedrockElevationValueBtn.Enabled = chk;
        }

        private void elasticityBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentErosionElasticModulusValueBtn.Text, false, p.inputGrid.GetJ, p.inputGrid.GetI);   //二選一
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void tensileStrengthBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentErosionTensileStrengthValueBtn.Text, false, p.inputGrid.GetJ, p.inputGrid.GetI);   //二選一
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void bedrockElevationBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(bedrockElevationValueBtn.Text, false, p.inputGrid.GetJ, p.inputGrid.GetI);   //二選一
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

    }
}

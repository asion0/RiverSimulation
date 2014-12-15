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
    public partial class BoundaryConditionsForm : Form
    {
        public BoundaryConditionsForm()
        {
            InitializeComponent();
        }

        RiverSimulationProfile p = RiverSimulationProfile.profile;
        private SliderPanel sp = new SliderPanel();
        public void SetForm(RiverSimulationProfile profile)
        {
            p = profile;
        }

        private void BoundaryConditionsForm_Load(object sender, EventArgs e)
        {
            waterModelingPanel.Visible = false;
            moveableBedPanel.Visible = false;

            ControllerUtility.SetHtmlUrl(comment, "Logo.html");
  
            LoadStatus();
            UpdateStatus();
        }

        private void LoadStatus()
        {
            //4. 邊界條件
            //4.1 水理模組
            //4.1.1 上游
            switch (p.upFlowCondition)
            {
                case RiverSimulationProfile.FlowConditionType.SuperCriticalFlow:
                    upSuperCriticalFlowRdo.Checked = true;
                    break;
                case RiverSimulationProfile.FlowConditionType.SubCriticalFlow:
                    upSubCriticalFlowRdo.Checked = true;
                    break;
                case RiverSimulationProfile.FlowConditionType.None:
                    upSuperCriticalFlowRdo.Checked = false;
                    upSubCriticalFlowRdo.Checked = false;
                    break;
            }

            //4.1.1.1.1 超臨界流
            superBoundaryConditionNumberTxt.Text = p.superBoundaryConditionNumber.ToString();                   //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入

            //4.1.1.1.2 亞臨界流
            subBoundaryConditionNumberTxt.Text = p.subBoundaryConditionNumber.ToString();                 //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入

            verticalVelocityDistributionChk.Checked = p.verticalVelocityDistribution;       //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0)

            //4.1.2 下游 二選一
            switch (p.downFlowCondition)
            {
                case RiverSimulationProfile.FlowConditionType.SuperCriticalFlow:
                    downSuperCriticalFlowRdo.Checked = true;
                    break;
                case RiverSimulationProfile.FlowConditionType.SubCriticalFlow:
                    downSubCriticalFlowRdo.Checked = true;
                    break;
                case RiverSimulationProfile.FlowConditionType.None:
                    downSubCriticalFlowRdo.Checked = false;
                    downSubCriticalFlowRdo.Checked = false;
                    break;
            }

            //4.1.3 側壁
            if (p.sidewallBoundarySlip)
            {
                sidewallBoundarySlipRdo.Checked = true;
            }
            else
            {
                nonSidewallBoundarySlipRdo.Checked = true;
            }

            //4.1.4 水面 三維 only。(”即時互動處”不放圖示)
            mainstreamWindShearTxt.Text = p.mainstreamWindShear.ToString();              //4.1.4.1 主流方向風剪 單一數值 N/m2 0 實數 實數 8 格
            sideWindShearTxt.Text = p.sideWindShear.ToString();                    //4.1.4.2 側方向風剪 單一數值 N/m2 0 實數 實數 8 格
            coriolisForceTxt.Text = p.coriolisForce.ToString();                    //4.1.4.3 科氏力 單一數值 N/m2 0 實數 實數 8 格

            //4.1.5 底床 實數 三維 only。(”即時互動處”不放圖示)
            boundaryLayerThicknessCombo.SelectedIndex = p.boundaryLayerThickness - 1; //4.1.5.1 邊界層厚度 三選一 3 整數(>0) 整數 8 格 1、2、3，三維 only，下拉選單。
            seabedBoundarySlipCombo.SelectedIndex = (int)p.seabedBoundarySlip;           //4.1.5.2 底床邊界滑移 三選一 -- 0 整數(>0) 整數 8 格 a. 三維 only，下拉選單 b. 0：非滑移、1：滑移、2：壁函數

            //4.2 動床模組
            //4.2.1 上游
            //4.2.1.1 入流泥砂設定
            if (p.bottomBedLoadFluxType == RiverSimulationProfile.BottomBedLoadFluxType.Auto)
            {   //4.2.1.1.1 底床載通量 實數(>=0)“模式自動計算”
                bottomBedLoadFluxAutoRdo.Checked = true;
            }
            else
            {
                bottomBedLoadFluxInputRdo.Checked = true;
            }

            //4.2.1.2 上游邊界底床
            if(p.upBoundaryElevationType == RiverSimulationProfile.BottomBedLoadFluxType.Auto)
            {   //4.2.1.2.1 可採用初始上游邊界底床高程或自行輸入
                upBoundaryElevationAutoRdo.Checked = true;
            }
            else
            {
                upBoundaryElevationInputRdo.Checked = true;
            }
            //4.2.2 下游 圖5，“即時互動處”不放圖示
            if (p.movableBedDownType == RiverSimulationProfile.BottomBedLoadFluxType.Auto)
            {   //4.2.2.2 濃度 實數(>=0) 如圖 2.2.2.1 所示
                movableBedDownAutoRdo.Checked = true;
            }
            else
            {
                movableBedDownInputRdo.Checked = true;
            }

            switch(p.nearBedBoundaryType)
            {   //4.2.3.2 通量/給定濃度二選一 a. 先令使用者選擇是通量或者是給定濃
                case RiverSimulationProfile.NearBedBoundaryType.ConcentrationCalculation:
                    nearBedBoundaryAutoRdo.Checked = true;
                    break;
                case RiverSimulationProfile.NearBedBoundaryType.Input:
                    nearBedBoundaryInputRdo.Checked = true;
                    break;
                case RiverSimulationProfile.NearBedBoundaryType.None:
                   nearBedBoundaryAutoRdo.Checked = false;
                   nearBedBoundaryInputRdo.Checked = false;
                   break;
            }
            nearBedBoundaryAutoCombo.SelectedIndex = (int)p.concentrationCalculation - 1;
        }

        private void UpdateStatus()
        {
            UpdateActiveFunctions();
        }

        private void UpdateActiveFunctions()
        {
            if (!p.IsVariableFlowType())
            {
                superBoundaryConditionNumberTxt.Enabled = false;        //定量流不輸入
                subBoundaryConditionNumberTxt.Enabled = false;          //定量流不輸入
                superFlowQuantityBtn.Enabled = false;
                superWaterLevelBtn.Enabled = false;
                subFlowQuantityBtn.Enabled = false;
                downSuperWaterLevelBtn.Enabled = false;
            }

            if (!p.Is3DMode())
            {
                verticalVelocityDistributionChk.Enabled = false;    //垂向流速分布(3D)
                verticalVelocityDistributionBtn.Enabled = false;    //垂向流速分布(3D)
                waterSurfacePanel.Enabled = false;                  //水面，三維only。(”即時互動處”不放圖示)
                bottomBedPanel.Enabled = false;                     //底床，三維only。(”即時互動處”不放圖示)
                movableBedVerticalConcentrationDistributionBtn.Enabled = false;
                movableBedDownVerticalConcentrationDistributionInputBtn.Enabled = false;
                nearBedBoundaryPanel.Enabled = false;
            }
            sideInOutFlowPanel.Enabled = p.sideInOutFlowFunction;       //模擬功能之特殊功能中如有勾選側出/入流，則4.1.3.2 才會出現於介面中。

            if(Program.programVersion.DemoVersion)
            {
                bottomBedLoadFluxInputRdo.Enabled = false;
                bottomBedLoadFluxInputBtn.Enabled = false;
                upBoundaryElevationInputRdo.Enabled = false;
                upBoundaryElevationInputBtn.Enabled = false;
                movableBedDownInputRdo.Enabled = false;
                movableBedDownInputBtn.Enabled = false;
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
            if (!ConvertWaterModeling())
            {
                return false;
            }

            if (!ConvertMoveableBed())
            {
                return false;
            }
            return true;
        }

        private bool ConvertWaterModeling()
        {
            if (!ConvertupSuperBoundaryConditionNumber())
            {
                return false;
            }

            if (!ConvertupSubBoundaryConditionNumber())
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.mainstreamWindShear, mainstreamWindShearTxt, "請輸入正確的主流方向風剪！", ControllerUtility.CheckType.NoCheck))
            {
                return false;
            }
            if (!ControllerUtility.CheckConvertDouble(ref p.sideWindShear, sideWindShearTxt, "請輸入正確的側方向風剪！", ControllerUtility.CheckType.NoCheck))
            {
                return false;
            }
            if (!ControllerUtility.CheckConvertDouble(ref p.coriolisForce, coriolisForceTxt, "請輸入正確的科氏力！", ControllerUtility.CheckType.NoCheck))
            {
                return false;
            }

            if (p.upFlowCondition == RiverSimulationProfile.FlowConditionType.None)
            {
                MessageBox.Show("請選擇上游流況設定！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (p.downFlowCondition == RiverSimulationProfile.FlowConditionType.None)
            {
                MessageBox.Show("請選擇下游游流況設定！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private bool ConvertupSuperBoundaryConditionNumber()
        {
            if (superBoundaryConditionNumberTxt.Enabled && !ControllerUtility.CheckConvertInt32(ref p.superBoundaryConditionNumber, superBoundaryConditionNumberTxt, "請輸入正確的邊界條件數目！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }
            return true;
        }

        private bool ConvertupSubBoundaryConditionNumber()
        {
            if (subBoundaryConditionNumberTxt.Enabled && !ControllerUtility.CheckConvertInt32(ref p.subBoundaryConditionNumber, subBoundaryConditionNumberTxt, "請輸入正確的邊界條件數目！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }
            return true;
        }

        private bool ConvertMoveableBed()
        {
            //if (!ControllerUtility.CheckConvertDouble(ref p.waterTimeSpan, waterTimeSpanTxt, "請輸入正確的時間間距！", ControllerUtility.CheckType.GreaterThanZero))
            //{
            //    return false;
            //}
            return true;
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            Button orgBtn = sender as Button;
            if (orgBtn == waterModelingBtn)
            {
                sp.SlidePanel(waterModelingPanel, SliderPanel.Direction.ToRight, this.ClientSize);
            }
            else if (orgBtn == moveableBedBtn)
            {
                sp.SlidePanel(moveableBedPanel, SliderPanel.Direction.ToRight, this.ClientSize);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            sp.SlidePanel(null, SliderPanel.Direction.Back, this.ClientSize);
        }


 /*

        private void upSuperFlowBtn_Click(object sender, EventArgs e)
        {
            //int n = 0;
            //try
            //{
            //    n = Convert.ToInt32(condNoTxt.Text);
            //}
            //catch
            //{
            //    n = -1;
            //}

            //if (n < 2)
            //{
            //    MessageBox.Show("請輸入正確的邊界條件數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}


            //TableInputForm form = new TableInputForm();
            //form.SetFormMode(upSupercriticalFlowRdo.Text + " " + upSuperFlowBtn.Text, false, n, 50);
            //if (DialogResult.OK == form.ShowDialog())
            //{

            //}
        }

        private void upSubFlowBtn_Click(object sender, EventArgs e)
        {
        //    int n = 0;
        //    try
        //    {
        //        n = Convert.ToInt32(condNoTxt.Text);
        //    }
        //    catch
        //    {
        //        n = -1;
        //    }

        //    if (n < 2)
        //    {
        //        MessageBox.Show("請輸入正確的邊界條件數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }



        //    TableInputForm form = new TableInputForm();
        //    form.SetFormMode(upSupercriticalFlowRdo.Text + " " + upSubFlowBtn.Text, false, n, 50);
        //    if (DialogResult.OK == form.ShowDialog())
        //    {

        //    }
        }

        private void downSuperWaterLevelBtn_Click(object sender, EventArgs e)
        {
        //    int n = 0;
        //    try
        //    {
        //        n = Convert.ToInt32(condNoTxt.Text);
        //    }
        //    catch
        //    {
        //        n = -1;
        //    }

        //    if (n < 2)
        //    {
        //        MessageBox.Show("請輸入正確的邊界條件數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }


        //    TableInputForm form = new TableInputForm();
        //    form.SetFormMode(downSubcriticalFlowRdo.Text + " " + downSuperWaterLevelBtn.Text, false, n, 50);
        //    if (DialogResult.OK == form.ShowDialog())
        //    {

        //    }

        }

        */
        //==============================================================================
        private void upSuperCriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            superBoundaryConditionNumberTxt.Enabled = chk;
            superFlowQuantityBtn.Enabled = chk;
            superWaterLevelBtn.Enabled = chk;
            p.upFlowCondition = RiverSimulationProfile.FlowConditionType.SuperCriticalFlow;
            UpdateStatus();
        }

        private void upSubCriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            subBoundaryConditionNumberTxt.Enabled = chk;
            subFlowQuantityBtn.Enabled = chk;
            p.upFlowCondition = RiverSimulationProfile.FlowConditionType.SubCriticalFlow;
            UpdateStatus();
        }

        private void superFlowQuantityBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertupSuperBoundaryConditionNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode("上游超臨界流流量", p.inputGrid.GetJ, p.superBoundaryConditionNumber, "上游超臨界流流量", "Q", "T",
                TableInputForm.InputFormType.FlowQuantity, 90, 120, false, false, false, p.superFlowQuantity);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.superFlowQuantity = new RiverSimulationProfile.TwoInOne(form.FlowQuantityData());
            }
        }

        private void superWaterLevelBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertupSuperBoundaryConditionNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode("上游超臨界流水位", p.inputGrid.GetJ, p.superBoundaryConditionNumber, "上游超臨界流水位", "W", "T",
                TableInputForm.InputFormType.FlowQuantity, 90, 120, false, false, false, p.superWaterLevel);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.superWaterLevel = new RiverSimulationProfile.TwoInOne(form.FlowQuantityData());
            }
        }

        private void subFlowQuantityBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertupSubBoundaryConditionNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode("上游亞臨界流流量", p.inputGrid.GetJ, p.subBoundaryConditionNumber, "上游亞臨界流流量", "Q", "T",
                TableInputForm.InputFormType.FlowQuantity, 90, 120, false, false, false, p.subFlowQuantity);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.subFlowQuantity = new RiverSimulationProfile.TwoInOne(form.FlowQuantityData());
            }
        }

        private void verticalVelocityDistributionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            verticalVelocityDistributionBtn.Enabled = chk;
            p.verticalVelocityDistribution = chk;
            UpdateStatus();
        }

        private void verticalVelocityDistributionBtn_Click(object sender, EventArgs e)
        {
            //TableInputForm form = new TableInputForm();
            //form.SetFormMode(verticalVelocityDistributionBtn.Text, 1, p.verticalLevelNumber, verticalVelocityDistributionBtn.Text, "分層比例", "格網分層",
            //    TableInputForm.InputFormType.SeparateForm, 90, 120, true, true, false, p.levelProportion);
            //DialogResult r = form.ShowDialog();
            //if (DialogResult.OK == r)
            //{
            //    p.verticalVelocityDistributionArray = (double[,])form.SeparateData().Clone();
            //    //SwitchPreivewCombo(PreviewType.Sprate);
            //    //DrawPreview();
            //}
        }

        private void downSuperCriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.downFlowCondition = RiverSimulationProfile.FlowConditionType.SuperCriticalFlow;
        }

        private void downSubCriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            downSuperWaterLevelBtn.Enabled = chk;
            p.downFlowCondition = RiverSimulationProfile.FlowConditionType.SubCriticalFlow;
        }


        private void downSuperWaterLevelBtn_Click_1(object sender, EventArgs e)
        {
            if (!ConvertupSuperBoundaryConditionNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode("下游亞臨界流水位", p.inputGrid.GetJ, p.subBoundaryConditionNumber, "下游亞臨界流水位", "W", "T",
                TableInputForm.InputFormType.FlowQuantity, 90, 120, false, false, false, p.downSubWaterLevel);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.downSubWaterLevel = new RiverSimulationProfile.TwoInOne(form.FlowQuantityData());
            }
        }

        private void nonSidewallBoundarySlipRdo_CheckedChanged(object sender, EventArgs e)
        {
            //bool chk = (sender as RadioButton).Checked;
            //p.sidewallBoundarySlip = chk;

        }

        private void sidewallBoundarySlipRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.sidewallBoundarySlip = chk;
        }

        private void boundaryLayerThicknessCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = (sender as ComboBox).SelectedIndex;
            p.boundaryLayerThickness = sel + 1;
        }

        private void seabedBoundarySlipCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RiverSimulationProfile.SeabedBoundarySlipType sel = (RiverSimulationProfile.SeabedBoundarySlipType)(sender as ComboBox).SelectedIndex;
            p.seabedBoundarySlip = sel;
        }

        //========================================================
        private void bottomBedLoadFluxAutoRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.bottomBedLoadFluxType = RiverSimulationProfile.BottomBedLoadFluxType.Auto;
        }

        private void bottomBedLoadFluxInputRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.bottomBedLoadFluxType = RiverSimulationProfile.BottomBedLoadFluxType.Input;
            if(chk)
            {
                bottomBedLoadFluxInputBtn.Enabled = true;
            }
        }

        private void bottomBedLoadFluxInputBtn_Click(object sender, EventArgs e)
        {

        }

        private void suspendedLoadDepthAvgConcentrationBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode("懸浮載水深平均濃度", p.sedimentParticlesNumber, p.inputGrid.GetI, "懸浮載水深平均濃度", "粒徑 ", "",
                TableInputForm.InputFormType.GenericDoubleGreaterThanOrEqualZero, 90, 120, true, false, false, p.suspendedLoadDepthAvgConcentration);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.suspendedLoadDepthAvgConcentration = (double[,])form.GenericDoubleData().Clone();
            }
        }

        private void movableBedVerticalConcentrationDistributionBtn_Click(object sender, EventArgs e)
        {

        }

        private void upBoundaryElevationAutoRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.upBoundaryElevationType = RiverSimulationProfile.BottomBedLoadFluxType.Auto;

        }

        private void upBoundaryElevationInputRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.upBoundaryElevationType = RiverSimulationProfile.BottomBedLoadFluxType.Input;
            if (chk)
            {
                upBoundaryElevationInputBtn.Enabled = true;
            }
        }

        private void upBoundaryElevationInputBtn_Click(object sender, EventArgs e)
        {

        }

        private void bottomBedParticleSizeRatioBtn_Click(object sender, EventArgs e)
        {

        }

        private void movableBedDownAutoRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.movableBedDownType = RiverSimulationProfile.BottomBedLoadFluxType.Auto;

        }

        private void movableBedDownInputRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.movableBedDownType = RiverSimulationProfile.BottomBedLoadFluxType.Input;
            if (chk)
            {
                movableBedDownInputBtn.Enabled = true;
            }
        }

        private void movableBedDownInputBtn_Click(object sender, EventArgs e)
        {

        }

        private void movableBedDownVerticalConcentrationDistributionAutoRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void movableBedDownVerticalConcentrationDistributionInputBtn_Click(object sender, EventArgs e)
        {

        }

        private void nearBedBoundaryAutoRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.nearBedBoundaryType = RiverSimulationProfile.NearBedBoundaryType.ConcentrationCalculation;
            nearBedBoundaryAutoCombo.Enabled = chk;

        }

        private void nearBedBoundaryAutoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RiverSimulationProfile.ConcentrationCalculationType sel = (RiverSimulationProfile.ConcentrationCalculationType)((sender as ComboBox).SelectedIndex + 1);
            p.concentrationCalculation = sel;
        }

        private void nearBedBoundaryInputRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.nearBedBoundaryType = RiverSimulationProfile.NearBedBoundaryType.Input;
            nearBedBoundaryInputBtn.Enabled = chk;

        }

        private void nearBedBoundaryInputBtn_Click(object sender, EventArgs e)
        {

        }

    }

}

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
            boundaryTimeNumberTxt.Text = p.boundaryTimeNumber.ToString();                   //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入

            //4.1.1.1.2 亞臨界流
            //subBoundaryConditionNumberTxt.Text = p.subBoundaryConditionNumber.ToString();                 //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入

            verticalVelocityDistributionChk.Checked = p.verticalVelocityDistribution;       //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0)
            verticalVelocityDistributionTxt.Text = p.verticalVelocityDistributionNumber.ToString();       //4.1.1.2 垂向流速分布(3D) 分層數目P 整數(>=2)

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
                boundaryTimeNumberTxt.Enabled = false;        //定量流不輸入
                boundaryTimeBtn.Enabled = false;
                //subBoundaryConditionNumberTxt.Enabled = false;          //定量流不輸入
                //superMainFlowQuantityBtn.Enabled = false;
                //superWaterLevelBtn.Enabled = false;
                //subMainFlowQuantityBtn.Enabled = false;
                //downSuperWaterLevelBtn.Enabled = false;
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

            if (!p.sideInOutFlowFunction)   //模擬功能之特殊功能中如有勾選側出/入流，則4.1.3.2 才會出現於介面中。
            {
                sideInFlowChk.Enabled = p.sideInOutFlowFunction;
                sideInFlowBtn.Enabled = p.sideInOutFlowFunction;
                sideOutFlowChk.Enabled = p.sideInOutFlowFunction;
                sideOutFlowBtn.Enabled = p.sideInOutFlowFunction;
            }

            if(Program.programVersion.DemoVersion)
            {
                //bottomBedLoadFluxInputRdo.Enabled = false;
                //bottomBedLoadFluxInputBtn.Enabled = false;
                //upBoundaryElevationInputRdo.Enabled = false;
                //upBoundaryElevationInputBtn.Enabled = false;
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
            if (!ConvertBoundaryTimeNumber())
            {
                return false;
            }

            if (!ConvertVerticalVelocityDistributionNumber())
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

        private bool ConvertBoundaryTimeNumber()
        {
            if (boundaryTimeNumberTxt.Enabled && !ControllerUtility.CheckConvertInt32(ref p.boundaryTimeNumber, boundaryTimeNumberTxt, "邊界時間數目必須是大於1的整數！", ControllerUtility.CheckType.GreaterThanOne))
            {
                return false;
            }
            return true;
        }

        private bool ConvertVerticalVelocityDistributionNumber()
        {
            if (verticalVelocityDistributionTxt.Enabled && !ControllerUtility.CheckConvertInt32(ref p.verticalVelocityDistributionNumber, verticalVelocityDistributionTxt, "分層數目必須是大於2的整數！", ControllerUtility.CheckType.GreaterThanTwo))
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

        //==============================================================================
        private void upSuperCriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            boundaryTimeNumberTxt.Enabled = chk;
            superMainFlowQuantityBtn.Enabled = chk;
            superSideFlowQuantityBtn.Enabled = chk;
            superWaterLevelBtn.Enabled = chk;
            p.upFlowCondition = RiverSimulationProfile.FlowConditionType.SuperCriticalFlow;
            UpdateStatus();
        }

        private void upSubCriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            boundaryTimeNumberTxt.Enabled = chk;
            subMainFlowQuantityBtn.Enabled = chk;
            subSideFlowQuantityBtn.Enabled = chk;
            p.upFlowCondition = RiverSimulationProfile.FlowConditionType.SubCriticalFlow;
            UpdateStatus();
        }

        private bool AlertBoundaryTimeChange(ref RiverSimulationProfile.TwoInOne o)
        {
            if (o == null)
            {
                return false;
            }
            if(p.IsConstantFlowType())
            {   //定量流時不檢查
                return true;
            }
            if (p.boundaryTime == null || p.boundaryTime.GetLength(0) != p.boundaryTimeNumber)
            {
                MessageBox.Show("邊界時間尚未輸入完成", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (o.dataArray != null && o.dataArray2D().GetLength(1) != p.boundaryTimeNumber)
            {   //邊界時間有輸入，但與邊界時間數目不符合
                if (DialogResult.OK == MessageBox.Show("改變過邊界時間數目需要重新輸入所有流況資料，請確認？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {   
                    RiverSimulationProfile.TwoInOne.Type tp = o.type;
                    o = new RiverSimulationProfile.TwoInOne();
                    o.type = tp;
                    return true;
                }
                else
                {
                    p.boundaryTimeNumber = o.dataArray2D().GetLength(1);
                    boundaryTimeNumberTxt.Text = p.boundaryTimeNumber.ToString();
                    return false;
                }
            }

            return true;
        }

        private void superMainFlowQuantityBtn_Click(object sender, EventArgs e)
        {
            if (null == p.superMainFlowQuantity)
            {
                p.superMainFlowQuantity = new RiverSimulationProfile.TwoInOne();
            }

            if (!ConvertBoundaryTimeNumber())
            {
                return;
            }
            if (!AlertBoundaryTimeChange(ref p.superMainFlowQuantity))
            {
                return;
            }

            ThreeWayTableForm form = new ThreeWayTableForm();
            form.SetFormMode(ThreeWayTableForm.FormType.FlowQuantity, "上游超臨界流主流方向流量", "流量Q", p.inputGrid.GetJ, p.boundaryTimeNumber, p, p.superMainFlowQuantity);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.superMainFlowQuantity = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }
        }

        private void superSideFlowQuantityBtn_Click(object sender, EventArgs e)
        {
            if (null == p.superSideFlowQuantity)
            {
                p.superSideFlowQuantity = new RiverSimulationProfile.TwoInOne();
            }

            if (!ConvertBoundaryTimeNumber())
            {
                return;
            }
            if (!AlertBoundaryTimeChange(ref p.superSideFlowQuantity))
            {
                return;
            }

            ThreeWayTableForm form = new ThreeWayTableForm();
            form.SetFormMode(ThreeWayTableForm.FormType.FlowQuantity, "上游超臨界流側方向流量", "流量Q", p.inputGrid.GetJ, p.boundaryTimeNumber, p, p.superSideFlowQuantity);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.superSideFlowQuantity = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }

/*
            TableInputForm.InputFormType t = (p.IsConstantFlowType() ? TableInputForm.InputFormType.FlowConditionsSettingConstant : TableInputForm.InputFormType.FlowConditionsSettingVariable);
            TableInputForm form = new TableInputForm();
            form.p = p;
            form.SetFormMode("上游超臨界流側方向流量", p.inputGrid.GetJ, (p.IsVariableFlowType()) ? p.boundaryTimeNumber : 1, "流量", "流量Q", "",
                t, 90, 64, false, false, false, p.superSideFlowQuantity);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.superSideFlowQuantity = new RiverSimulationProfile.TwoInOne(form.FlowQuantityData());
            }
 * */
        }

        private void superWaterLevelBtn_Click(object sender, EventArgs e)
        {
            if (null == p.superWaterLevel)
            {
                p.superWaterLevel = new RiverSimulationProfile.TwoInOne();
            }

            if (!ConvertBoundaryTimeNumber())
            {
                return;
            }
            if (!AlertBoundaryTimeChange(ref p.superWaterLevel))
            {
                return;
            }
            ThreeWayTableForm form = new ThreeWayTableForm();
            form.SetFormMode(ThreeWayTableForm.FormType.FlowQuantity, "上游超臨界流水位", "水位", p.inputGrid.GetJ, p.boundaryTimeNumber, p, p.superWaterLevel);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.superWaterLevel = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }
            /*
            TableInputForm.InputFormType t = (p.IsConstantFlowType() ? TableInputForm.InputFormType.FlowConditionsSettingConstant : TableInputForm.InputFormType.FlowConditionsSettingVariable);
            TableInputForm form = new TableInputForm();
            form.p = p;
            form.SetFormMode("上游超臨界流水位", p.inputGrid.GetJ, (p.IsVariableFlowType()) ? p.boundaryTimeNumber : 1, "水位", "水位", "",
                t, 90, 64, false, false, false, p.superWaterLevel);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.superWaterLevel = new RiverSimulationProfile.TwoInOne(form.FlowQuantityData());
            }
             * */
        }

        private void subMainFlowQuantityBtn_Click(object sender, EventArgs e)
        {
            if (null == p.subMainFlowQuantity)
            {
                p.subMainFlowQuantity = new RiverSimulationProfile.TwoInOne();
            }

            if (!ConvertBoundaryTimeNumber())
            {
                return;
            }
            if (!AlertBoundaryTimeChange(ref p.subMainFlowQuantity))
            {
                return;
            }

            ThreeWayTableForm form = new ThreeWayTableForm();
            form.SetFormMode(ThreeWayTableForm.FormType.FlowQuantity, "上游亞臨界流主流方向流量", "流量Q", p.inputGrid.GetJ, p.boundaryTimeNumber, p, p.subMainFlowQuantity);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.subMainFlowQuantity = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }
            /*
            TableInputForm.InputFormType t = (p.IsConstantFlowType() ? TableInputForm.InputFormType.FlowConditionsSettingConstant : TableInputForm.InputFormType.FlowConditionsSettingVariable);
            TableInputForm form = new TableInputForm();
            form.p = p;
            form.SetFormMode("上游亞臨界流主流方向流量", p.inputGrid.GetJ, (p.IsVariableFlowType()) ? p.boundaryTimeNumber : 1, "流量", "流量Q", "",
                t, 90, 64, false, false, false, p.subMainFlowQuantity);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.subMainFlowQuantity = new RiverSimulationProfile.TwoInOne(form.FlowQuantityData());
            }
             * */
        }

        private void subSideFlowQuantityBtn_Click(object sender, EventArgs e)
        {
            if (null == p.subSideFlowQuantity)
            {
                p.subSideFlowQuantity = new RiverSimulationProfile.TwoInOne();
            }

            if (!ConvertBoundaryTimeNumber())
            {
                return;
            }
            if (!AlertBoundaryTimeChange(ref p.subSideFlowQuantity))
            {
                return;
            }

            ThreeWayTableForm form = new ThreeWayTableForm();
            form.SetFormMode(ThreeWayTableForm.FormType.FlowQuantity, "上游亞臨界流側方向流量", "流量Q", p.inputGrid.GetJ, p.boundaryTimeNumber, p, p.subSideFlowQuantity);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.subSideFlowQuantity = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }
            /*
            TableInputForm.InputFormType t = (p.IsConstantFlowType() ? TableInputForm.InputFormType.FlowConditionsSettingConstant : TableInputForm.InputFormType.FlowConditionsSettingVariable);
            TableInputForm form = new TableInputForm();
            form.p = p;
            form.SetFormMode("上游亞臨界流側方向流量", p.inputGrid.GetJ, (p.IsVariableFlowType()) ? p.boundaryTimeNumber : 1, "流量", "流量Q", "",
                t, 90, 64, false, false, false, p.subSideFlowQuantity);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.subSideFlowQuantity = new RiverSimulationProfile.TwoInOne(form.FlowQuantityData());
            }
             * */
        }

        private void verticalVelocityDistributionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            verticalVelocityDistributionTxt.Enabled = chk;
            verticalVelocityDistributionBtn.Enabled = chk;
            p.verticalVelocityDistribution = chk;
            //UpdateStatus();
        }

        private void verticalVelocityDistributionBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertVerticalVelocityDistributionNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode("垂直流速分布(3D)", 2, p.verticalVelocityDistributionNumber, "", "", "分層",
                 TableInputForm.InputFormType.VerticalVelocityDistributionForm, 90, 80, true, true, false, p.verticalVelocityDistributionArray);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
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
            if (null == p.downSubWaterLevel)
            {
                p.downSubWaterLevel = new RiverSimulationProfile.TwoInOne();
            }

            if (!ConvertBoundaryTimeNumber())
            {
                return;
            }
            if (!AlertBoundaryTimeChange(ref p.downSubWaterLevel))
            {
                return;
            }

            ThreeWayTableForm form = new ThreeWayTableForm();
            form.SetFormMode(ThreeWayTableForm.FormType.FlowQuantity, "下游亞臨界流水位", "水位", p.inputGrid.GetJ, p.boundaryTimeNumber, p, p.downSubWaterLevel);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.downSubWaterLevel = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }
            /*
            TableInputForm.InputFormType t = (p.IsConstantFlowType() ? TableInputForm.InputFormType.FlowConditionsSettingConstant : TableInputForm.InputFormType.FlowConditionsSettingVariable);
            TableInputForm form = new TableInputForm();
            form.p = p;
            form.SetFormMode("下游亞臨界流水位", p.inputGrid.GetJ, (p.IsVariableFlowType()) ? p.boundaryTimeNumber : 1, "水位", "水位", "",
                t, 90, 64, false, false, false, p.downSubWaterLevel);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.downSubWaterLevel = new RiverSimulationProfile.TwoInOne(form.FlowQuantityData());
            }
             * */
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
            ThreeWayTableForm form = new ThreeWayTableForm();
            form.SetFormMode(ThreeWayTableForm.FormType.BottomBedLoadFlux, "底床載通量", "粒徑", p.sedimentParticlesNumber, p.boundaryTimeNumber, p, p.bottomBedLoadFluxArray);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.bottomBedLoadFluxArray = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }
        }

        private void suspendedLoadDepthAvgConcentrationBtn_Click(object sender, EventArgs e)
        {
            ThreeWayTableForm form = new ThreeWayTableForm();
            form.SetFormMode(ThreeWayTableForm.FormType.BottomBedLoadFlux, "懸浮載水深平均濃度", "粒徑", p.sedimentParticlesNumber, p.boundaryTimeNumber, p, p.suspendedLoadDepthAvgConcentration);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.suspendedLoadDepthAvgConcentration = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }
            
/*
            TableInputForm form = new TableInputForm();
            form.SetFormMode("懸浮載水深平均濃度", p.sedimentParticlesNumber, p.inputGrid.GetI, "懸浮載水深平均濃度", "粒徑 ", "",
                TableInputForm.InputFormType.GenericDoubleGreaterThanOrEqualZero, 90, 120, true, false, false, p.suspendedLoadDepthAvgConcentration);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.suspendedLoadDepthAvgConcentration = (double[,])form.GenericDoubleData().Clone();
            }
 */
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
            TableInputForm form = new TableInputForm();
            form.SetFormMode(bottomBedParticleSizeRatioBtn.Text, p.sedimentParticlesNumber, 1, bottomBedParticleSizeRatioBtn.Text, "粒徑 ", "泥砂比例",
                TableInputForm.InputFormType.GenericDoubleGreaterThanOrEqualZero, 90, 120, true, false, true, p.bottomBedParticleSizeRatio);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.bottomBedParticleSizeRatio = (double[,])form.GenericDoubleData().Clone();
            }

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
            //p.inputConcentration
            TableInputForm form = new TableInputForm();
            form.SetFormMode(nearBedBoundaryInputBtn.Text, p.sedimentParticlesNumber, 1, nearBedBoundaryInputBtn.Text, "粒徑 ", "底床濃度",
                TableInputForm.InputFormType.TwoInOneDouble, 90, 120, false, false, true, p.inputConcentration);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.inputConcentration = new RiverSimulationProfile.TwoInOne(form.GenericTwoInOneData());
            }

        }

        private void boundaryTimeBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertBoundaryTimeNumber())
            {
                return;
            }
            if(p.boundaryTime != null && p.boundaryTime.GetLength(0) != p.boundaryTimeNumber)
            {   //邊界時間數目已被邊更
                if (DialogResult.OK == MessageBox.Show("改變過邊界時間數目會清除已輸入的邊界時間，請確認？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {
                    p.boundaryTime = null;
                }
                else
                {
                    boundaryTimeNumberTxt.Text = p.boundaryTime.GetLength(0).ToString();
                    return;
                }
            }


            TableInputForm form = new TableInputForm();
            form.SetFormMode(boundaryTimeBtn.Text, 1, p.boundaryTimeNumber, "邊界時間數目", "時間 T(sec)", "",
                TableInputForm.InputFormType.BoundaryTime, 90, 90, true, true, false, p.boundaryTime);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.boundaryTime = (double[])form.BoundaryTimeData().Clone();
            }
        }


    }

}

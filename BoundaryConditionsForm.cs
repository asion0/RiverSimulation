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
            //p.ClearBackgroundMapType();
            ControllerUtility.SetHtmlUrl(comment, "Logo.html");
            ControllerUtility.InitialGridPictureBoxByProfile(ref mapPicBox, p);

            //p.boundaryUpVerticalDistribution = null;
            //p.boundaryDownVerticalDistribution = null;
            if (p.boundaryUpVerticalDistribution == null)
            {
                p.boundaryUpVerticalDistribution = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.Double, RiverSimulationProfile.TwoInOne.ArrayType.TwoDim);
            }
            if (p.boundaryUpVerticalDistribution.ArrayNull() || p.boundaryUpVerticalDistribution.ValueNull())
            {
                p.boundaryUpVerticalDistribution.CreateDouble2D(0, 0);
            }

            if (p.boundaryDownVerticalDistribution == null)
            {
                p.boundaryDownVerticalDistribution = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.Double, RiverSimulationProfile.TwoInOne.ArrayType.TwoDim);
            }
            if (p.boundaryDownVerticalDistribution.ArrayNull() || p.boundaryDownVerticalDistribution.ValueNull())
            {
                p.boundaryDownVerticalDistribution.CreateDouble2D(0, 0);
            } 
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
            
            //4.1.1.2 垂向流速分布(3D)
            switch (p.verticalVelocityType)
            {
                case RiverSimulationProfile.VerticalVelocityType.Auto:
                    up3dValue.Checked = true;
                     break;
               case RiverSimulationProfile.VerticalVelocityType.Input:
                    up3dArray.Checked = true;
                    break;
            }

            //verticalVelocityDistributionChk.Checked = true; //20150330 修改規格取消選取方塊
            //verticalVelocityDistributionChk.Checked = p.verticalVelocityDistribution;       //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0)
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
            sideOutFlowChk.Checked = p.sideOutFlowSet;
            sideOutFlowCountTxt.Text = p.sideOutFlowNumber.ToString();

            sideInFlowChk.Checked = p.sideInFlowSet;
            sideInFlowCountTxt.Text = p.sideInFlowNumber.ToString();

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

            //4.2.1.1.3 上游垂向濃度分布(3D)
            switch (p.boundaryUpVerticalDistribution.type)
            {   //4.2.3.2 通量/給定濃度二選一 a. 先令使用者選擇是通量或者是給定濃
                case RiverSimulationProfile.TwoInOne.Type.UseValue:
                    upVdValueRdo.Checked = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    upVdArrayRdo.Checked = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.None:
                    upVdValueRdo.Checked = false;
                    upVdArrayRdo.Checked = false;
                    break;
            }
            upVdValueTxt.Text = p.boundaryUpVerticalDistribution.ValueDouble()[0].ToString();
            boundaryUpVerticalDistributionNumTxt.Text = p.boundaryUpVerticalDistributionNum.ToString();

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

            //4.2.2.3 下游垂向濃度分布(3D)
            switch (p.boundaryDownVerticalDistribution.type)
            {   //4.2.3.2 通量/給定濃度二選一 a. 先令使用者選擇是通量或者是給定濃
                case RiverSimulationProfile.TwoInOne.Type.UseValue:
                    downVdValueRdo.Checked = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    downVdArrayRdo.Checked = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.None:
                    downVdValueRdo.Checked = false;
                    downVdArrayRdo.Checked = false;
                    break;
            }
            downVdValueTxt.Text = p.boundaryDownVerticalDistribution.ValueDouble()[0].ToString();
            boundaryDownVerticalDistributionNumTxt.Text = p.boundaryDownVerticalDistributionNum.ToString();

            switch(p.nearBedBoundaryType)
            {   //4.2.3.2 通量/給定濃度二選一 a. 先令使用者選擇是通量或者是給定濃
                case RiverSimulationProfile.NearBedBoundaryType.ConcentrationCalculation:
                    nearBedBoundaryAutoRdo.Checked = true;
                    break;
                case RiverSimulationProfile.NearBedBoundaryType.Input:
                    nearBedBoundaryInputRdo.Checked = true;
                    break;
                case RiverSimulationProfile.NearBedBoundaryType.Flux:
                    nearBedBoundaryFluxRdo.Checked = true;
                    break;
                case RiverSimulationProfile.NearBedBoundaryType.None:
                   nearBedBoundaryAutoRdo.Checked = false;
                    nearBedBoundaryInputRdo.Checked = false;
                    nearBedBoundaryFluxRdo.Checked = false;
                    break;
            }
            nearBedBoundaryAutoCombo.SelectedIndex = (int)p.concentrationCalculation - 1;
        }

        private void UpdateStatus()
        {
            sideOutFlowCountTxt.Enabled = p.sideOutFlowSet;
            sideInFlowCountTxt.Enabled = p.sideInFlowSet;
            
            UpdateActiveFunctions();
        }

        private void UpdateActiveFunctions()
        {
            if (!p.IsMovableBedMode())
            {   //水理模式不支援動床模組設定
                movableTypeGroup.Enabled = false;
            }

            if (!p.IsVariableFlowType())
            {
                boundaryTimeNumberTxt.Enabled = false;        //定量流不輸入
                boundaryTimeBtn.Enabled = false;
                //suspendedLoadDepthAvgConcentrationBtn.Enabled = false;  //定量流不支援 20150121, 20150303 改為要輸入
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
                //boundaryUpVerticalDistributionBtn.Enabled = false;
                boundaryUpVerticalDistributionPanel.Enabled = false;
                //boundaryDownVerticalDistributionBtn.Enabled = false;
                boundaryDownVerticalDistributionPanel.Enabled = false;
                nearBedBoundaryPanel.Enabled = false;
            }

            if (!p.sideInOutFlowFunction)   //模擬功能之特殊功能中如有勾選側出/入流，則4.1.3.2 才會出現於介面中。
            {
                sideInOutPanel.Enabled = p.sideInOutFlowFunction;
                //sideInFlowChk.Enabled = p.sideInOutFlowFunction;
                //sideInFlowBtn.Enabled = p.sideInOutFlowFunction;
                //sideOutFlowChk.Enabled = p.sideInOutFlowFunction;
                //sideInOutFlowSettingBtn.Enabled = p.sideInOutFlowFunction;
            }

            if(Program.programVersion.DemoVersion)
            {
                //bottomBedLoadFluxInputRdo.Enabled = false;
                //bottomBedLoadFluxInputBtn.Enabled = false;
                //upBoundaryElevationInputRdo.Enabled = false;
                //upBoundaryElevationInputBtn.Enabled = false;
                movableBedDownInputRdo.Enabled = false;
                movableBedDownInputBtn.Enabled = false;
                nearBedBoundaryInputRdo.Enabled = false;    //20150731介面測試問題 9.
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

            if (p.IsMovableBedMode() && !ConvertMoveableBed())
            {
                return false;
            }
            return true;
        }

        private bool ConvertWaterModeling()
        {
            if (boundaryTimeBtn.Enabled)
            {
                if (!ConvertBoundaryTimeNumber())
                {
                    return false;
                }

                if (p.boundaryTime == null || p.boundaryTime.Length != p.boundaryTimeNumber)
                {
                    MessageBox.Show("請輸入水理模式-邊界時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            if (p.upFlowCondition == RiverSimulationProfile.FlowConditionType.None)
            {
                MessageBox.Show("請選擇水理模式-上游-流況設定！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if(p.upFlowCondition == RiverSimulationProfile.FlowConditionType.SuperCriticalFlow)
            {
                if(!p.superMainFlowQuantity.HasInputed())
                {
                    MessageBox.Show("請輸入水理模式-上游-超臨界流-主流方向流量！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (!p.superSideFlowQuantity.HasInputed())
                {
                    MessageBox.Show("請輸入水理模式-上游-超臨界流-側方向流量！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (!p.superWaterLevel.HasInputed())
                {
                    MessageBox.Show("請輸入水理模式-上游-超臨界流-水位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            else if (p.upFlowCondition == RiverSimulationProfile.FlowConditionType.SubCriticalFlow)
            {
                if (!p.subMainFlowQuantity.HasInputed())
                {
                    MessageBox.Show("請輸入水理模式-上游-亞臨界流-主流方向流量！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (!p.subSideFlowQuantity.HasInputed())
                {
                    MessageBox.Show("請輸入水理模式-上游亞臨界流-側方向流量！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            if (p.downFlowCondition == RiverSimulationProfile.FlowConditionType.None)
            {
                MessageBox.Show("請選擇水理模式-下游-流況設定！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if(p.downFlowCondition == RiverSimulationProfile.FlowConditionType.SubCriticalFlow)
            {
                if (!p.downSubWaterLevel.HasInputed())
                {
                    MessageBox.Show("請輸入水理模式-下游-亞臨界流-水位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            if (!ConvertVerticalVelocityDistributionNumber())
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.mainstreamWindShear, mainstreamWindShearTxt, "請輸入正確的水理模式-主流方向風剪！", ControllerUtility.CheckType.NoCheck))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.sideWindShear, sideWindShearTxt, "請輸入正確的水理模式-側方向風剪！", ControllerUtility.CheckType.NoCheck))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.coriolisForce, coriolisForceTxt, "請輸入正確的水理模式-科氏力！", ControllerUtility.CheckType.NoCheck))
            {
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
            if (p.bottomBedLoadFluxType == RiverSimulationProfile.BottomBedLoadFluxType.Input && !p.bottomBedLoadFluxArray.HasInputed())
            {
                MessageBox.Show("請設定動床模組-底床載通量！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!p.suspendedLoadDepthAvgConcentration.HasInputed())
            {
                MessageBox.Show("請輸入動床模組-懸浮載水深平均濃度！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if(p.Is3DMode())
            {
                if (!ConvertUpVerticalDistributionNumber())
                {
                    return false;
                }

                if (p.boundaryUpVerticalDistribution.type == RiverSimulationProfile.TwoInOne.Type.UseValue)
                {
                    if (!ConvertUpVerticalDistributionValue())
                    {
                        return false;
                    }
                }
                else if(p.boundaryUpVerticalDistribution.type == RiverSimulationProfile.TwoInOne.Type.UseArray)
                {
                    if (!p.boundaryUpVerticalDistribution.HasInputed())
                    {
                        MessageBox.Show("請設定動床模組-上游垂直濃度分布(3D)！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }

            }

            if (p.upBoundaryElevationType == RiverSimulationProfile.BottomBedLoadFluxType.Input && p.upBoundaryElevationArray == null)
            {
                MessageBox.Show("請設定動床模組-上游邊界底床-高程！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (p.bottomBedParticleSizeRatio == null)
            {
                MessageBox.Show("請設定動床模組-底床粒徑比例！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (p.movableBedDownType == RiverSimulationProfile.BottomBedLoadFluxType.Input && p.movableBedDownConcentration == null)
            {
                MessageBox.Show("請設定動床模組-下游-給定濃度！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if(p.Is3DMode())
            {


                if (!ConvertDownVerticalDistributionNumber())
                {
                    return false;
                }

                if (p.boundaryDownVerticalDistribution.type == RiverSimulationProfile.TwoInOne.Type.UseValue)
                {
                    if (!ConvertDownVerticalDistributionValue())
                    {
                        return false;
                    }
                }
                else if (p.boundaryDownVerticalDistribution.type == RiverSimulationProfile.TwoInOne.Type.UseArray)
                {
                    if (!p.boundaryDownVerticalDistribution.HasInputed())
                    {
                        MessageBox.Show("請設定動床模組-下游-垂直濃度分布(3D)！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }

            }

            if (p.Is3DMode() && p.nearBedBoundaryType == RiverSimulationProfile.NearBedBoundaryType.None)
            {
                MessageBox.Show("請設定動床模組-近底床濃度邊界！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (p.nearBedBoundaryType == RiverSimulationProfile.NearBedBoundaryType.Input && !p.inputConcentration.HasInputed())
            {
                MessageBox.Show("請設定動床模組-近底床濃度邊界-給定濃度！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (p.nearBedBoundaryType == RiverSimulationProfile.NearBedBoundaryType.ConcentrationCalculation && p.concentrationCalculation == RiverSimulationProfile.ConcentrationCalculationType.None)
            {
                MessageBox.Show("請選取動床模組-近底床濃度邊界-濃度計算公式！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
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

        private void up3dValue_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            if (chk)
            {
                p.verticalVelocityType = RiverSimulationProfile.VerticalVelocityType.Auto;
            }
        }

        private void up3dArray_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            verticalVelocityDistributionTxt.Enabled = chk;
            verticalVelocityDistributionBtn.Enabled = chk;
            if (chk)
            {
                p.verticalVelocityType = RiverSimulationProfile.VerticalVelocityType.Input;
            }
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

            if (!o.ArrayNull() && o.Array2D().GetLength(1) != p.boundaryTimeNumber)
            {   //邊界時間有輸入，但與邊界時間數目不符合
                if (DialogResult.OK == MessageBox.Show("改變過邊界時間數目需要重新輸入所有流況資料，請確認？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {
                    RiverSimulationProfile.TwoInOne.Type tp = o.type;
                    RiverSimulationProfile.TwoInOne.ValueType vt = o.valueType;
                    RiverSimulationProfile.TwoInOne.ArrayType at = o.arrayType;
                    o = new RiverSimulationProfile.TwoInOne(vt, at, tp);
                    return true;
                }
                else
                {
                    p.boundaryTimeNumber = o.Array2D().GetLength(1);
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
                p.superMainFlowQuantity = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.TwoDim, RiverSimulationProfile.TwoInOne.ArrayType.TwoDim);
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
                p.superSideFlowQuantity = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.TwoDim, RiverSimulationProfile.TwoInOne.ArrayType.TwoDim);
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
        }

        private void superWaterLevelBtn_Click(object sender, EventArgs e)
        {
            if (null == p.superWaterLevel)
            {
                p.superWaterLevel = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.TwoDim, RiverSimulationProfile.TwoInOne.ArrayType.TwoDim);
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
            form.SetFormMode(ThreeWayTableForm.FormType.WaterLevel, "上游超臨界流水位", "水位", p.inputGrid.GetJ, p.boundaryTimeNumber, p, p.superWaterLevel);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.superWaterLevel = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }
        }

        private void subMainFlowQuantityBtn_Click(object sender, EventArgs e)
        {
            if (null == p.subMainFlowQuantity)
            {
                p.subMainFlowQuantity = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.TwoDim, RiverSimulationProfile.TwoInOne.ArrayType.TwoDim);
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
        }

        private void subSideFlowQuantityBtn_Click(object sender, EventArgs e)
        {
            if (null == p.subSideFlowQuantity)
            {
                p.subSideFlowQuantity = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.TwoDim, RiverSimulationProfile.TwoInOne.ArrayType.TwoDim);
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
        }

        //private void verticalVelocityDistributionChk_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool chk = (sender as CheckBox).Checked;
        //    verticalVelocityDistributionTxt.Enabled = chk;
        //    verticalVelocityDistributionBtn.Enabled = chk;
        //    p.verticalVelocityDistribution = chk;
        //    //UpdateStatus();
        //}

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
                p.downSubWaterLevel = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.TwoDim, RiverSimulationProfile.TwoInOne.ArrayType.TwoDim);
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
            form.SetFormMode(ThreeWayTableForm.FormType.WaterLevel, "下游亞臨界流水位", "水位", p.inputGrid.GetJ, p.boundaryTimeNumber, p, p.downSubWaterLevel);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.downSubWaterLevel = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
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
            if (!ConvertBoundaryTimeNumber())
            {
                return;
            }

            if (!AlertBoundaryTimeChange(ref p.superMainFlowQuantity))
            {
                return;
            }

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
            if (!ConvertBoundaryTimeNumber())
            {
                return;
            }

            if (!AlertBoundaryTimeChange(ref p.superMainFlowQuantity))
            {
                return;
            }
            //p.suspendedLoadDepthAvgConcentration = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.ThreeDim, RiverSimulationProfile.TwoInOne.ArrayType.ThreeDim);
            //p.suspendedLoadDepthAvgConcentration.Clear();
            ThreeWayTableForm form = new ThreeWayTableForm();
            form.SetFormMode(ThreeWayTableForm.FormType.BottomBedLoadFlux, "懸浮載水深平均濃度", "粒徑", p.sedimentParticlesNumber, (p.IsConstantFlowType() ? 1 : p.boundaryTimeNumber), p, p.suspendedLoadDepthAvgConcentration);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.suspendedLoadDepthAvgConcentration = new RiverSimulationProfile.TwoInOne(form.GetData() as RiverSimulationProfile.TwoInOne);
            }
            
        }

        private void boundaryUpVerticalDistributionBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertUpVerticalDistributionNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode("上游垂直濃度分布", 2, p.boundaryUpVerticalDistributionNum, "", "", "分層",
                 TableInputForm.InputFormType.VerticalVelocityDistributionForm, 90, 80, true, true, false, p.boundaryUpVerticalDistribution.Array2D());
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.boundaryUpVerticalDistribution.SetArrayObject(form.VerticalVelocityDistributionData().Clone());
            }
        }

        private bool ConvertUpVerticalDistributionNumber()
        {
            if (!ControllerUtility.CheckConvertInt32(ref p.boundaryUpVerticalDistributionNum, boundaryUpVerticalDistributionNumTxt, "請輸入正確的上游垂直濃度分布分層數目！", ControllerUtility.CheckType.GreaterThanTwo))
            {
                return false;
            }

            if ((p.boundaryUpVerticalDistribution.type == RiverSimulationProfile.TwoInOne.Type.UseArray) && !p.boundaryUpVerticalDistribution.ArrayNull() && p.boundaryUpVerticalDistribution.Array2D().GetLength(1) != p.boundaryUpVerticalDistributionNum)
            {
                MessageBox.Show("修改過上游垂直濃度分布分層數目，需重新輸入上游垂直濃度分布比例！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                p.boundaryUpVerticalDistribution.Clear();
                p.boundaryUpVerticalDistribution.CreateDouble2D(0, 0);
            } 
            return true;
        }

        private bool ConvertDownVerticalDistributionNumber()
        {
            if (!ControllerUtility.CheckConvertInt32(ref p.boundaryDownVerticalDistributionNum, boundaryDownVerticalDistributionNumTxt, "請輸入正確的下游垂直濃度分布分層數目！", ControllerUtility.CheckType.GreaterThanTwo))
            {
                return false;
            }

            if ((p.boundaryDownVerticalDistribution.type == RiverSimulationProfile.TwoInOne.Type.UseArray) && !p.boundaryDownVerticalDistribution.ArrayNull() && p.boundaryDownVerticalDistribution.Array2D().GetLength(1) != p.boundaryDownVerticalDistributionNum)
            {
                MessageBox.Show("修改過上游垂直濃度分布分層數目，需重新輸入下游垂直濃度分布比例！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                p.boundaryDownVerticalDistribution.Clear();
                p.boundaryDownVerticalDistribution.CreateDouble2D(0, 0);
            }
            return true;
        }

        private bool ConvertUpVerticalDistributionValue()
        {
            if (!ControllerUtility.CheckConvertDouble(ref p.boundaryUpVerticalDistribution.ValueDouble()[0], upVdValueTxt, "請輸入正確的上游垂直濃度分布值！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }
            return true;
        }

        private bool ConvertDownVerticalDistributionValue()
        {
            if (!ControllerUtility.CheckConvertDouble(ref p.boundaryDownVerticalDistribution.ValueDouble()[0], downVdValueTxt, "請輸入正確的下游垂直濃度分布值！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }
            return true;
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
            TableInputForm form = new TableInputForm();
            form.SetFormMode("上游邊界底床底床高程", p.inputGrid.GetJ, p.boundaryTimeNumber, "邊界時間", "", "邊界時間 ",
                TableInputForm.InputFormType.GenericDoubleGreaterThanOrEqualZero, 90, 120, true, false, false, p.upBoundaryElevationArray);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.upBoundaryElevationArray = (double[,])form.GenericDoubleData().Clone();
            }
        }

        private void bottomBedParticleSizeRatioBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            if (p.bottomBedParticleSizeRatio != null && p.bottomBedParticleSizeRatio.GetLongLength(1) == 0)
            {
                p.bottomBedParticleSizeRatio = null;
            }
            form.p = p;
            form.SetFormMode("底床粒徑比例(百分比)", p.sedimentParticlesNumber, (p.IsConstantFlowType()) ? 1 : p.boundaryTimeNumber, "邊界時間", "粒徑 ", "邊界時間",
                TableInputForm.InputFormType.BottomBedParticleSizeRatio, 90, 120, true, false, false, p.bottomBedParticleSizeRatio);
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

        private void boundaryDownVerticalDistributionBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertDownVerticalDistributionNumber())
            {
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode("下游垂直濃度分布", 2, p.boundaryDownVerticalDistributionNum, "", "", "分層",
                 TableInputForm.InputFormType.VerticalVelocityDistributionForm, 90, 80, true, true, false, p.boundaryDownVerticalDistribution.Array2D());
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.boundaryDownVerticalDistribution.SetArrayObject(form.VerticalVelocityDistributionData().Clone());
            }
        }

        private void nearBedBoundaryAutoRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.nearBedBoundaryType = RiverSimulationProfile.NearBedBoundaryType.ConcentrationCalculation;
            //160303 - 20160127介面討論綜整 近底床濃度邊界-“濃度計算公式”的下拉選單灰階隱藏。
            nearBedBoundaryAutoCombo.Enabled = chk;
        }

        private void nearBedBoundaryAutoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RiverSimulationProfile.ConcentrationCalculationType sel = (RiverSimulationProfile.ConcentrationCalculationType)((sender as ComboBox).SelectedIndex + 1);
            p.concentrationCalculation = sel;
        }

        private void nearBedBoundaryFluxRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            p.nearBedBoundaryType = RiverSimulationProfile.NearBedBoundaryType.Flux;
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

        private void upVdValueRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;

            upVdValueTxt.Enabled = chk;
            boundaryUpVerticalDistributionNumTxt.Enabled = !chk;
            boundaryUpVerticalDistributionBtn.Enabled = !chk;

            if(chk)
            {
                p.boundaryUpVerticalDistribution.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
            }
        }

        private void upVdArrayRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;

            upVdValueTxt.Enabled = !chk;
            boundaryUpVerticalDistributionNumTxt.Enabled = chk;
            boundaryUpVerticalDistributionBtn.Enabled = chk;

            if (chk)
            {
                p.boundaryUpVerticalDistribution.type = RiverSimulationProfile.TwoInOne.Type.UseArray;
            }
        }

        private void downVdValueRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;

            downVdValueTxt.Enabled = chk;
            boundaryDownVerticalDistributionNumTxt.Enabled = !chk;
            boundaryDownVerticalDistributionBtn.Enabled = !chk;
        
            if (chk)
            {
                p.boundaryDownVerticalDistribution.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
            }
        }

        private void downVdArrayRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;

            downVdValueTxt.Enabled = !chk;
            boundaryDownVerticalDistributionNumTxt.Enabled = chk;
            boundaryDownVerticalDistributionBtn.Enabled = chk;

            if (chk)
            {
                p.boundaryDownVerticalDistribution.type = RiverSimulationProfile.TwoInOne.Type.UseArray;
            }
        }

        private void sideInFlowChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.sideInFlowSet = chk;
            UpdateStatus(); //操作此UI會有互動變化則需呼叫
        }

        private void sideOutFlowChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.sideOutFlowSet = chk;
            UpdateStatus(); //操作此UI會有互動變化則需呼叫
        }

        private bool ConvertStructureSetNumber()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int n = 0;
            if (p.sideOutFlowSet)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, sideOutFlowCountTxt, "請輸入正確的側出流數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.sideOutFlowNumber = n;

            if (p.sideInFlowSet)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, sideInFlowCountTxt, "請輸入正確的側入流數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.sideInFlowNumber = n;
            return true;
        }

        private void sideInOutFlowSettingBtn_Click(object sender, EventArgs e)
        {
            if (!ConvertStructureSetNumber())
            {
                return;
            }

            if (p.sideOutFlowNumber + p.sideInFlowNumber == 0)
            {
                MessageBox.Show("請設置側出/入流數量！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool alreadyShow = false;
            RiverSimulationProfile.StructureChangeType tp = p.CheckSideFlowChanged(p.sideOutFlowSet, ref p.sideOutFlowObjs, p.sideOutFlowNumber, true);
            if (!alreadyShow && tp == RiverSimulationProfile.StructureChangeType.SelectionAndDataNoMatch)
            {
                MessageBox.Show("側出流數量或設置已變更，請重新設定側出流！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                alreadyShow = true;
            }

            tp = p.CheckSideFlowChanged(p.sideInFlowSet, ref p.sideInFlowObjs, p.sideInFlowNumber, true);
            if (!alreadyShow && tp == RiverSimulationProfile.StructureChangeType.SelectionAndDataNoMatch)
            {
                MessageBox.Show("側入流數量或設置已變更，請重新設定側入流！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                alreadyShow = true;
            }

            p.ResizeSideInOutFlowSets(p.sideOutFlowNumber, p.sideInFlowNumber);
            SideInOutFlowSetForm form = new SideInOutFlowSetForm();
            form.SetFormMode("側出/入流",
                p.sideOutFlowObjs, sideOutFlowChk.Text,
                p.sideInFlowObjs, sideInFlowChk.Text);
            DialogResult r = form.ShowDialog();
            if(r == System.Windows.Forms.DialogResult.OK)
            {
                p.sideOutFlowObjs = form.sideOutObjects;
                p.sideInFlowObjs = form.sideInObjects;
            }

        }
    }

}

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
    public partial class WaterModelingForm : Form
    {
        public WaterModelingForm()
        {
            InitializeComponent();
        }

        private RiverSimulationProfile p = RiverSimulationProfile.profile;
        private SliderPanel sp = new SliderPanel();
        public void SetForm(RiverSimulationProfile profile)
        {
            p = profile;
        }

        private void WaterModelingForm_Load(object sender, EventArgs e)
        {
            valueParamPanel.Visible = false;        //隱藏數值參數面板
            physicalParamPanel.Visible = false;     //隱藏物理參數面板

            ControllerUtility.SetHtmlUrl(comment, "Logo.html");
            ControllerUtility.InitialGridPictureBoxByProfile(ref mapPicBox, p);
            mapPicBox.Visible = true;

            LoadStatus();
            UpdateStatus();
        }


        private void LoadStatus()
        {
            if(p.IsMovableBedMode())
            {   //使用者選動床模組，則此處一定為變量流。PS: 後來取消此規格
                //p.flowType = RiverSimulationProfile.FlowType.VariableFlow;
            }

            switch (p.flowType)
            {
                case RiverSimulationProfile.FlowType.ConstantFlow:
                    constantFlowRdo.Checked = true;
                    break;
                case RiverSimulationProfile.FlowType.VariableFlow:
                    variableFlowRdo.Checked = true;
                    break;
                default:
                    constantFlowRdo.Checked = false;
                    variableFlowRdo.Checked = false;
                    break;
            }

            //1.1 數值參數 =========================================
            //1.1.1 時間
            totalSimulationTimeTxt.Text = p.totalSimulationTime.ToString();
            timeSpan2dTxt.Text = p.timeSpan2d.ToString();
            outputFrequencyTxt.Text = p.outputFrequency.ToString();
            steppingTimesInVertVslcTimeTxt.Text = p.steppingTimesInVertVslcTime.ToString();

            //1.1.2 收斂條件
            waterModelingConvergenceCriteria2dTxt.Text = p.waterModelingConvergenceCriteria2d.ToString();
            waterModelingConvergenceCriteria3dTxt.Text = p.waterModelingConvergenceCriteria3d.ToString();
            
            //1.1.3 輸出控制
            minWaterDeothTxt.Text = p.minWaterDeoth.ToString();
            viscosityFactorAdditionInMainstreamTxt.Text = p.viscosityFactorAdditionInMainstream.ToString();
            viscosityFactorAdditionInSideDirectionTxt.Text = p.viscosityFactorAdditionInSideDirection.ToString();
           
            //1.2 物理參數 =========================================
            switch (p.roughnessType)
            {
                case RiverSimulationProfile.RoughnessType.ManningN:
                    manningNRdo.Checked = true;
                    break;
                case RiverSimulationProfile.RoughnessType.Chezy:
                    chezyRdo.Checked = true;
                    break;
                default:
                    manningNRdo.Checked = false;
                    chezyRdo.Checked = false;
                    break;
            }

            //1.2.2 紊流黏滯係數
            switch (p.turbulenceViscosityType)
            {
                case RiverSimulationProfile.TurbulenceViscosityType.UserDefine:
                    userDefineRdo.Checked = true;
                    break;
                case RiverSimulationProfile.TurbulenceViscosityType.ZeroEquation:
                    zeroEquationRdo.Checked = true;
                    break;
                case RiverSimulationProfile.TurbulenceViscosityType.SingleEquation:
                    singleEquationRdo.Checked = true;
                    break;
                case RiverSimulationProfile.TurbulenceViscosityType.TwinEquation:
                    twinEquationRdo.Checked = true;
                    break;
                default:
                    userDefineRdo.Checked = false;
                    zeroEquationRdo.Checked = false;
                    singleEquationRdo.Checked = false;
                    twinEquationRdo.Checked = false;
                    break;
            }
            tvInMainstreamDirectionTxt.Text = p.tvInMainstreamDirection.ToString();
            tvInSideDirectionTxt.Text = p.tvInSideDirection.ToString();

            zeroEquationTypeCombo.SelectedIndex = (int)(p.zeroEquationType - 1);

            //1.2.3 其他
            gravityConstantTxt.Text = p.gravityConstant.ToString();
            waterDensityTxt.Text = p.waterDensity.ToString();

            //1.3 二次流效應 二維 only
            switch (p.curvatureRadiusType)
            {
                case RiverSimulationProfile.CurvatureRadiusType.AutoCurvatureRadius:
                    autoCurvatureRadiusRdo.Checked = true;
                    break;
                case RiverSimulationProfile.CurvatureRadiusType.InputCurvatureRadius:
                    inputCurvatureRadiusRdo.Checked = true;
                    break;
                default:
                    autoCurvatureRadiusRdo.Checked = false;
                    inputCurvatureRadiusRdo.Checked = false;
                    break;
            }

            //結構物設置
            tBarSetChk.Checked = p.tBarSet;
            tBarNumberTxt.Text = p.tBarNumber.ToString();

            bridgePierSetChk.Checked = p.bridgePierSet;
            bridgePierNumberTxt.Text = p.bridgePierNumber.ToString();

            groundsillWorkSetChk.Checked = p.groundsillWorkSet;
            groundsillWorkNumberTxt.Text = p.groundsillWorkNumber.ToString();

            sedimentationWeirSetChk.Checked = p.sedimentationWeirSet;
            sedimentationWeirNumberTxt.Text = p.sedimentationWeirNumber.ToString();

            //1.6 高含砂效應 供使用者輸入 6 個常數：α1、β1、c 1、α2、β2、c 2
            highSandEffectAlpha1Txt.Text = p.highSandEffectAlpha1.ToString();
            highSandEffectBeta1Txt.Text = p.highSandEffectBeta1.ToString();
            highSandEffectC1Txt.Text = p.highSandEffectC1.ToString();
            highSandEffectAlpha2Txt.Text = p.highSandEffectAlpha1.ToString();
            highSandEffectBeta2Txt.Text = p.highSandEffectBeta2.ToString();
            highSandEffectC2Txt.Text = p.highSandEffectC2.ToString();
        }

        private void UpdateStatus()
        {
            //20141124 取消此規格
            //constantFlowRdo.Enabled = !p.IsMovableBedMode();
            //if (p.IsMovableBedMode())
            //{   //使用者選動床模組，則此處一定為變量流
            //    p.flowType = RiverSimulationProfile.FlowType.VariableFlow;
            //}

            //模擬功能如果為定量流，則總模擬時間與時間間距相同，使用者不輸入。
            //20150224修改規格為僅定量流且水理模式時不輸入
            if(p.IsConstantFlowType() && p.IsWaterModelingMode())
            {
                totalSimulationTimeTxt.Enabled = false;
            }
            else
            {
                totalSimulationTimeTxt.Enabled = true;
            }

            structureSetGrp.Enabled = p.structureSetFunction;

            //timeSpan2dTxt.Enabled = !p.IsConstantFlowType();
            outputFrequencyTxt.Enabled = !p.IsConstantFlowType(); //模擬功能如果為定量流，則輸出頻率為1，使用者不輸入。

            manningNBtn.Enabled = (p.roughnessType == RiverSimulationProfile.RoughnessType.ManningN);
            chezyBtn.Enabled = (p.roughnessType == RiverSimulationProfile.RoughnessType.Chezy);
            roughnessHeightKsBtn.Enabled = p.Is3DMode();
            roughnessHeightKsHelpBtn.Enabled = p.Is3DMode();

            tvInMainstreamDirectionTxt.Enabled = (p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.UserDefine);
            tvInSideDirectionTxt.Enabled = (p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.UserDefine);
            zeroEquationTypeCombo.Enabled = (p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.ZeroEquation);
            twinEquationRdo.Enabled = p.Is3DMode();
            zeroEquationTypeCombo.Enabled = p.Is3DMode();

            if (p.Is2DMode())
            {   //二維只有 1 種選項：constant，其他則灰掉
                zeroEquationTypeCombo.SelectedIndex = 0;
            }

            secFlowEffectGrp.Enabled = p.Is2DMode() && p.secondFlowEffectFunction;    //二維 only。

            tBarNumberTxt.Enabled = p.tBarSet;
            bridgePierNumberTxt.Enabled = p.bridgePierSet;
            groundsillWorkNumberTxt.Enabled = p.groundsillWorkSet;
            sedimentationWeirNumberTxt.Enabled = p.sedimentationWeirSet;

            UpdateActiveFunctions();
        }

        private void UpdateActiveFunctions()
        {
            if (Program.programVersion.LiteVersion)
            {
                minWaterDepthPanel.Visible = false;
                turbulenceViscosityPanel.Visible = false;
            }
            outputControl3dGrp.Enabled = p.Is3DMode();
            steppingTimesInVertVslcTimeTxt.Enabled = p.Is3DMode();
            highSandContentEffectGrp.Enabled = p.waterHighSandContentEffectFunction;

        }

        private bool DoConvert()
        {
            if (p.flowType == RiverSimulationProfile.FlowType.None)
            {
                MessageBox.Show("請選取定/變量流設定！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!ConvertValueParam())
            {
                return false;
            }

            if(!ConvertPhysicalParam())
            {
                return false;
            }

            if (!ConvertStructureSetNumber())
            {
                return false;
            }
            return true;
        }

        private bool ConvertValueParam()
        {
            if (!ControllerUtility.CheckConvertDouble(ref p.totalSimulationTime, totalSimulationTimeTxt, "請輸入正確的總模擬時間！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.timeSpan2d, timeSpan2dTxt, "請輸入正確的二維時間間距！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertInt32(ref p.outputFrequency, outputFrequencyTxt, "請輸入正確的輸出頻率！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertInt32(ref p.steppingTimesInVertVslcTime, steppingTimesInVertVslcTimeTxt, "請輸入正確的垂直方向計算時間步進次數！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.waterModelingConvergenceCriteria2d, waterModelingConvergenceCriteria2dTxt, "請輸入正確的二維水理收斂標準！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }
            if (!ControllerUtility.CheckConvertDouble(ref p.waterModelingConvergenceCriteria3d, waterModelingConvergenceCriteria3dTxt, "請輸入正確的三維水理收斂標準！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.minWaterDeoth, minWaterDeothTxt, "請輸入正確的最小水深！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }
            if (!ControllerUtility.CheckConvertDouble(ref p.viscosityFactorAdditionInMainstream, viscosityFactorAdditionInMainstreamTxt, "請輸入正確的主流方向黏滯係數加成比例！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }
            if (!ControllerUtility.CheckConvertDouble(ref p.viscosityFactorAdditionInSideDirection, viscosityFactorAdditionInSideDirectionTxt, "請輸入正確的側方向黏滯係數加成比例！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }
            return true;
        }

        private bool ConvertPhysicalParam()
        {
            //zeroEquationTypeCombo.SelectedIndex = (int)p.zeroEquationType;

            ////1.2.3 其他
            if (p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.UserDefine)
            {
                if (!ControllerUtility.CheckConvertDouble(ref p.tvInMainstreamDirection, tvInMainstreamDirectionTxt, "請輸入正確的物理參數-主流方向紊流黏滯係數！", ControllerUtility.CheckType.NoCheck))
                {
                    return false;
                }
                if (!ControllerUtility.CheckConvertDouble(ref p.tvInSideDirection, tvInSideDirectionTxt, "請輸入正確的物理參數-側方向紊流黏滯係數！", ControllerUtility.CheckType.NoCheck))
                {
                    return false;
                }
            }
            else if(p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.None)
            {
                MessageBox.Show("請選擇物理參數-紊流黏滯係數！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (p.roughnessType == RiverSimulationProfile.RoughnessType.Chezy && !p.chezy.HasInputed())
            {
                MessageBox.Show("請輸入物理參數-Chezy！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (p.roughnessType == RiverSimulationProfile.RoughnessType.ManningN && !p.manningN.HasInputed())
            {
                MessageBox.Show("請輸入物理參數-Manning n！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (p.roughnessType == RiverSimulationProfile.RoughnessType.None)
            {
                MessageBox.Show("請選擇物理參數-糙度係數！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            //gravityConstantTxt.Text = p.gravityConstant.ToString();
            //waterDensityTxt.Text = p.waterDensity.ToString();
            //tvInMainstreamDirectionTxt.Enabled = (p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.UserDefine);
            //tvInSideDirectionTxt.Enabled = (p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.UserDefine);
            if(roughnessHeightKsBtn.Enabled && p.roughnessHeightKs.type == RiverSimulationProfile.TwoInOne.Type.None)
            {
                MessageBox.Show("請輸入物理參數-粗糙高度Ks！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.gravityConstant, gravityConstantTxt, "", ControllerUtility.CheckType.NoCheck))
            {
                return false;
            }

            if (!ControllerUtility.CheckConvertDouble(ref p.waterDensity, waterDensityTxt, "請輸入正確的水密度！", ControllerUtility.CheckType.NotNegative))
            {
                return false;
            }
            return true;
        }

        private bool ConvertStructureSetNumber()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int n = 0;
            if (p.tBarSet)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, tBarNumberTxt, "請輸入正確的丁壩數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.tBarNumber = n;

            if (p.bridgePierSet)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, bridgePierNumberTxt, "請輸入正確的橋墩數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.bridgePierNumber = n;

            if (p.groundsillWorkSet)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, groundsillWorkNumberTxt, "請輸入正確的固床工數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.groundsillWorkNumber = n;

            if (p.sedimentationWeirSet)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, sedimentationWeirNumberTxt, "請輸入正確的攔河堰數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.sedimentationWeirNumber = n;
            return true;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            sp.SlidePanel(null, SliderPanel.Direction.Back, this.ClientSize);
            ControllerUtility.SetHtmlUrl(comment, "Logo.html");

        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            Button orgBtn = sender as Button;
            if (orgBtn == valueParamBtn)
            {
                sp.SlidePanel(valueParamPanel, SliderPanel.Direction.ToRight, this.ClientSize);
            }
            else if (orgBtn == physicalParamBtn)
            {
                sp.SlidePanel(physicalParamPanel, SliderPanel.Direction.ToRight, this.ClientSize);
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

        private void groupBox_MouseHover(object sender, EventArgs e)
        {
            //GroupBox c = sender as GroupBox;
            //string url;


            //if (c == flowTypeGroup)
            //{
            //    url = "D1-1-0.html";
            //}
            //else if (c == groupBox2)
            //{
            //    url = "D1-1-1.html";
            //}
            //else if (c == secFlowEffectGrp)
            //{
            //    url = "D1-1-2.html";
            //}
            //else if (c == groupBox4)
            //{
            //    url = "D1-1-3.html";
            //}
            //else if (c == highSandContentEffectGrp)
            //{
            //    url = "D1-1-4.html";
            //}
            //else if (c == structureSetGrp)
            //{
            //    url = "D1-1-6.html";
            //}
            //else
            //{
            //    url = "D1-1.html";
            //}

            //ControllerUtility.SetHtmlUrl(comment, url);
        }

        private void flowTypeRdo_CheckedChanged(object sender, EventArgs e)
        {
            if (constantFlowRdo.Checked)
            {
                p.flowType = RiverSimulationProfile.FlowType.ConstantFlow;
            }
            else if (variableFlowRdo.Checked)
            {
                p.flowType = RiverSimulationProfile.FlowType.VariableFlow;
            }
            else
            {
                p.flowType = RiverSimulationProfile.FlowType.None;
            }
            UpdateStatus();
        }

        private void outputControl_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            if(sender as CheckBox == outputControlInitialBottomElevationChk)
            {
                p.outputControlInitialBottomElevation = chk;
            }
            else if(sender as CheckBox == outputControlLevelChk)
            {
                p.outputControlLevel = chk;
            }
            else if (sender as CheckBox == outputControlLevelChk)
            {
                p.outputControlLevel = chk;
            }
            else if (sender as CheckBox == outputControlDepthChk)
            {
                p.outputControlDepth = chk;
            }
            else if (sender as CheckBox == outputControlAverageDepthFlowRateChk)
            {
                p.outputControlAverageDepthFlowRate = chk;
            }
            else if (sender as CheckBox == outputControlFlowChk)
            {
                p.outputControlFlow = chk;
            }
            else if (sender as CheckBox == outputControlBottomShearingStressChk)
            {
                p.outputControlBottomShearingStress = chk;
            }
            else if (sender as CheckBox == outputControlVelocityInformation3DChk)
            {
                p.outputControlVelocityInformation3D = chk;
            }
        }

        private void manningBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(manningNBtn.Text, p.inputGrid.GetJ, p.inputGrid.GetI, manningNBtn.Text, "", "",
                TableInputForm.InputFormType.TwoInOneDouble, 90, 120, false, false, false, p.manningN);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.manningN = new RiverSimulationProfile.TwoInOne(form.GenericTwoInOneData());
            }
        }

        private void chezyBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(chezyBtn.Text, p.inputGrid.GetJ, p.inputGrid.GetI, chezyBtn.Text, "", "",
                TableInputForm.InputFormType.TwoInOneDouble, 90, 120, false, false, false, p.chezy);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.chezy = new RiverSimulationProfile.TwoInOne(form.GenericTwoInOneData());
            }
        }

        private void roughnessHeightKsBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode("粗糙高度Ks(mm)", p.inputGrid.GetJ, p.inputGrid.GetI, "粗糙高度Ks(mm)", "", "",
                TableInputForm.InputFormType.TwoInOneDouble, 90, 120, false, false, false, p.roughnessHeightKs);
            form.unitLbl.Text = "mm";       //20150325新增規格
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.roughnessHeightKs = new RiverSimulationProfile.TwoInOne(form.GenericTwoInOneData());
            }
        }

        private void roughnessHeightKsHelpBtn_Click(object sender, EventArgs e)
        {
            KsHelpForm form = new KsHelpForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void roughnessType_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            if (chk && sender as RadioButton == manningNRdo)
            {
                p.roughnessType = RiverSimulationProfile.RoughnessType.ManningN;
                ControllerUtility.SetHtmlUrl(comment, "D1-2-1-1.html");
            }
            else if (chk && sender as RadioButton == chezyRdo)
            {
                p.roughnessType = RiverSimulationProfile.RoughnessType.Chezy;
                ControllerUtility.SetHtmlUrl(comment, "D1-2-1-2.html");
            }
            else
            {
                //p.roughnessType = RiverSimulationProfile.RoughnessType.None;
            }
            UpdateStatus(); //操作此UI會有互動變化則需呼叫
        }

        private void turbulenceViscosityType_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            if (chk && sender as RadioButton == userDefineRdo)
            {
                p.turbulenceViscosityType = RiverSimulationProfile.TurbulenceViscosityType.UserDefine;
            }
            else if (chk && sender as RadioButton == zeroEquationRdo)
            {
                p.turbulenceViscosityType = RiverSimulationProfile.TurbulenceViscosityType.ZeroEquation;
                if(p.zeroEquationType == RiverSimulationProfile.ZeroEquationType.None)
                {
                    p.zeroEquationType = RiverSimulationProfile.ZeroEquationType.Constant;
                }
            }
            else if (chk && sender as RadioButton == twinEquationRdo)
            {
                p.turbulenceViscosityType = RiverSimulationProfile.TurbulenceViscosityType.TwinEquation;
            }
            else
            {
                //p.turbulenceViscosityType = RiverSimulationProfile.TurbulenceViscosityType.None;
            }
            UpdateStatus();     //操作此UI會有互動變化則需呼叫
        }

        private void zeroEquationTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.zeroEquationType = (RiverSimulationProfile.ZeroEquationType)(zeroEquationTypeCombo.SelectedIndex + 1);
        }

        private void curvatureRadiusRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            if (chk)
            {
                p.curvatureRadiusType = RiverSimulationProfile.CurvatureRadiusType.InputCurvatureRadius;
                curvatureRadiusBtn.Enabled = chk;
            }
        }

        private void autoCurvatureRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            if (chk)
            {
                p.curvatureRadiusType = RiverSimulationProfile.CurvatureRadiusType.AutoCurvatureRadius;
                curvatureRadiusBtn.Enabled = !chk;
            }
        }

        private void curvatureRadiusBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(curvatureRadiusBtn.Text, p.inputGrid.GetJ, p.inputGrid.GetI, "", "", "",
                TableInputForm.InputFormType.GenericDouble, 90, 120, true, false, false, p.curvatureRadius);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                p.curvatureRadius = (double[,])form.GenericDoubleData().Clone();
            }
        }

        private void tBarChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.tBarSet = chk;
            UpdateStatus(); //操作此UI會有互動變化則需呼叫
        }

        private void bridgePierChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.bridgePierSet = chk;
            UpdateStatus(); //操作此UI會有互動變化則需呼叫
        }

        private void groundsillWorkChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.groundsillWorkSet = chk;
            UpdateStatus(); //操作此UI會有互動變化則需呼叫
        }

        private void sedimentationWeirChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            p.sedimentationWeirSet = chk;
            UpdateStatus(); //操作此UI會有互動變化則需呼叫
        }

        private void structureSetBtn_Click(object sender, EventArgs e)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (!ConvertStructureSetNumber())
            {
                return;
            }

            if (p.tBarNumber + p.bridgePierNumber + p.groundsillWorkNumber + p.sedimentationWeirNumber == 0)
            {
                MessageBox.Show("請設置結構物數量！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            p.ResizeStructureSets(p.tBarNumber, p.bridgePierNumber, p.groundsillWorkNumber, p.sedimentationWeirNumber);
            StructureSetForm form = new StructureSetForm();
            form.SetFormMode(structureSetGrp.Text,
                (p.tBarSet) ? p.tBarNumber : 0, tBarSetChk.Text,
                (p.bridgePierSet) ? p.bridgePierNumber : 0, bridgePierSetChk.Text,
                (p.groundsillWorkSet) ? p.groundsillWorkNumber : 0, groundsillWorkSetChk.Text,
                (p.sedimentationWeirSet) ? p.sedimentationWeirNumber : 0, sedimentationWeirSetChk.Text);

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.separateArray = (double[])form.SeparateData().Clone();
                //ShowGridMap(PicBoxType.Sprate);
                //DrawPreview();
            }
        }

        private void valueTimePanel_MouseHover(object sender, EventArgs e)
        {
            ControllerUtility.SetHtmlUrl(comment, "D1-1.html");
        }

        private void timeTxt_Enter(object sender, EventArgs e)
        {
            ControllerUtility.SetHtmlUrl(comment, "D1-1.html");
        }

    }
}

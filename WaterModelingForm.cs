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


            if(Program.programVersion.LiteVersion)
            {
                minWaterDepthPanel.Visible = false;
                minWaterDepthText.Visible = false;
                fullPanel.Visible = false;
            }

            ControllerUtility.SetHtmlUrl(comment, "D1-1.html");


            
            LoadStatus();
            UpdateStatus();
        }

        private void LoadStatus()
        {
            if(p.IsMovableBedMode())
            {   //使用者選動床模組，則此處一定為變量流。
                p.flowType = RiverSimulationProfile.FlowType.VariableFlow;
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
            zeroEquationTypeCombo.SelectedIndex = (int)p.zeroEquationType;

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
            highSandEffectC1Txt.Text = p.highSandEffectC2.ToString();
        }

        private void UpdateStatus()
        {
            if (p.IsMovableBedMode())
            {
                constantFlowRdo.Enabled = false;
            }
            if (p.IsConstantFlowType())
            {   //模擬功能如果為定量流，則總模擬時間與時間間距相同，使用者不輸入。
                totalSimulationTimeTxt.Enabled = false;
                timeSpan2dTxt.Enabled = false;
                outputFrequencyTxt.Enabled = false; //模擬功能如果為定量流，則輸出頻率為1，使用者不輸入。
            }
            outputControl3dGrp.Enabled = p.Is3DMode();

            manningNBtn.Enabled = (p.roughnessType == RiverSimulationProfile.RoughnessType.ManningN);
            chezyBtn.Enabled = (p.roughnessType == RiverSimulationProfile.RoughnessType.Chezy);
            roughnessHeightKsBtn.Enabled = p.Is3DMode();
            roughnessHeightKsHelpBtn.Enabled = p.Is3DMode();


            tvInMainstreamDirectionTxt.Enabled = (p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.UserDefine);
            tvInSideDirectionTxt.Enabled = (p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.UserDefine);
            zeroEquationTypeCombo.Enabled = (p.turbulenceViscosityType == RiverSimulationProfile.TurbulenceViscosityType.ZeroEquation);
            twinEquationRdo.Enabled = p.Is3DMode();
            if (p.Is2DMode())
            {   //二維只有 1 種選項：constant，其他則灰掉
                zeroEquationTypeCombo.SelectedIndex = 0;
                zeroEquationTypeCombo.Enabled = false;
            }
            secFlowEffectGrp.Enabled = p.Is2DMode();    //二維 only。
            highSandContentEffectGrp.Enabled = p.IsWaterModelingMode();     //水理 only 


            UpdateActiveFunctions();
        }

        private void UpdateActiveFunctions()
        {

          
        }

        private void Back_Click(object sender, EventArgs e)
        {
            sp.SlidePanel(null, SliderPanel.Direction.Back, this.ClientSize);
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            Button orgBtn = sender as Button;
            if (orgBtn == valueParamBtn)
            {
                sp.SlidePanel(valueParamPanel, SliderPanel.Direction.ToRight, this.ClientSize);
            }
            //else if (orgBtn == setting3dBtn)
            //{
            //    sp.SlidePanel(setting3dPanel, SliderPanel.Direction.ToRight);
            //}
            //else if (orgBtn == roughnessBtn)
            //{
            //    sp.SlidePanel(roughnessPanel, SliderPanel.Direction.ToRight);
            //}
            else if (orgBtn == physicalParamBtn)
            {
                sp.SlidePanel(physicalParamPanel, SliderPanel.Direction.ToRight, this.ClientSize);
            }
            //else if (orgBtn == dryBedBtn)
            //{
            //    sp.SlidePanel(dryBedPanel, SliderPanel.Direction.ToLeft);
            //}
            //else if (orgBtn == immersionBtn)
            //{
            //    sp.SlidePanel(immersionPanel, SliderPanel.Direction.ToLeft);
            //}
        }

        private void inputManningBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }
        private void groupBox_MouseHover(object sender, EventArgs e)
        {
            GroupBox c = sender as GroupBox;
            string url = "file:///./" + Environment.CurrentDirectory.Replace('\\', '/');


            if (c == flowTypeGroup)
            {
                url += "/D1-1-0.html";
            }
            else if (c == groupBox2)
            {
                url += "/D1-1-1.html";
            }
            else if (c == secFlowEffectGrp)
            {
                url += "/D1-1-2.html";
            }
            else if (c == groupBox4)
            {
                url += "/D1-1-3.html";
            }
            else if (c == highSandContentEffectGrp)
            {
                url += "/D1-1-4.html";
            }
            //else if(c==groupBox6)
            //{
            //    url += "/D1-1-5.html";
            //}
            else if (c == structureSetGrp)
            {
                url += "/D1-1-6.html";
            }
            else
            {
                url += "/D1-1.html";
            }

            if (comment.Url.ToString() != url)
            {
                comment.Navigate(new Uri(url));
            }
        }
        private void manningRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            manningNBtn.Enabled = chk;
            if(chk)
            {
                string url = "file:///" + Environment.CurrentDirectory.Replace('\\', '/') + "/D1-2-1-1.html"; ;
                if (comment.Url.ToString() != url)
                {
                    comment.Navigate(new Uri(url));
                }
            }
        }

        private void chezyRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            chezyBtn.Enabled = chk;
            if (chk)
            {
                string url = "file:///" + Environment.CurrentDirectory.Replace('\\', '/') + "/D1-2-1-2.html"; ;
                if (comment.Url.ToString() != url)
                {
                    comment.Navigate(new Uri(url));
                }
            }
        }

        private void curvatureRadiusRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            curvatureRadiusBtn.Enabled = chk; 
        }


        private void autoCurvatureRdo_CheckedChanged(object sender, EventArgs e)
        {
            //curvatureRadiusBtn.Enabled = false;
        }

        private void curvatureRadiusBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(curvatureRadiusBtn.Text, true, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void manningBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(manningNBtn.Text, false, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void chezyBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(chezyBtn.Text, true, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        //private void PropStratBtn_Click(object sender, EventArgs e)
        //{
        //    TableInputForm form = new TableInputForm();
        //    form.SetFormMode(propStratBtn.Text, true, 26, 50);
        //    if (DialogResult.OK == form.ShowDialog())
        //    {

        //    }
        //}

        private void ok_Click(object sender, EventArgs e)
        {
            if (!DoConvert())
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //private void UserInputPanel_EnabledChanged(object sender, EventArgs e)
        //{
        //    bool enabled = (sender as Panel).Enabled;
        //    xParam.Enabled = enabled;
        //    yParam.Enabled = enabled;
        //}

        //private void userInputRdo_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool chk = (sender as RadioButton).Checked;
        //    userInputPanel.Enabled = chk;
        //}

        private void zeroEquationRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            zeroEquationTypeCombo.Enabled = chk;
        }

        private void twinEquationRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            twinEquationCombo.Enabled = chk;
        }

        private void WaterModelingForm_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void structureSetBtn_Click(object sender, EventArgs e)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (!ConvertStructureSetNum())
            {
                return;
            }

            if(p.tBarNum + p.bridgePierNum + p.groundsillWorkNum + p.sedimentationWeirNum == 0)
            {
                MessageBox.Show("請設置結構物數量！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            p.ResizeStructureSetPts(p.tBarNum, p.bridgePierNum, p.groundsillWorkNum, p.sedimentationWeirNum);
            GridSelectForm form = new GridSelectForm();
            form.SetFormMode(structureSetGrp.Text,
                (p.tBarCheck) ? p.tBarNum : 0, tBarSetChk.Text,
                (p.bridgePierCheck) ? p.bridgePierNum : 0, bridgePierSetChk.Text,
                (p.groundsillWorkCheck) ? p.groundsillWorkNum : 0, groundsillWorkSetChk.Text,
                (p.sedimentationWeirCheck) ? p.sedimentationWeirNum : 0, sedimentationWeirSetChk.Text);

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.separateArray = (double[])form.SeparateData().Clone();
                //ShowGridMap(PicBoxType.Sprate);
                //DrawPreview();
            }
        }

        private void ksHelpBtn_Click(object sender, EventArgs e)
        {
            KsHelpForm form = new KsHelpForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void convergenceCriteria3dTxt_TextChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            try
            {
                value = Convert.ToDouble((sender as TextBox).Text);
                (sender as TextBox).ForeColor = Color.Black;
            }
            catch
            {
                (sender as TextBox).ForeColor = Color.Red;
                return;
            }

            RiverSimulationProfile.profile.convergenceCriteria3d = value;
            //UpdateStatus();
        }

        private void convergenceCriteria2dTxt_TextChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            try
            {
                value = Convert.ToDouble((sender as TextBox).Text);
                (sender as TextBox).ForeColor = Color.Black;
            }
            catch
            {
                (sender as TextBox).ForeColor = Color.Red;
                return;
            }

            RiverSimulationProfile.profile.convergenceCriteria2d = value;
            //UpdateStatus();        
        }

        private bool ConvertStructureSetNum()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int n = 0;
            if (p.tBarCheck)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, tBarNumberTxt.Text, "請輸入正確的丁壩數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.tBarNum = n;

            if (p.bridgePierCheck)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, bridgePierNumberTxt.Text, "請輸入正確的橋墩數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.bridgePierNum = n;

            if (p.groundsillWorkCheck)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, groundsillWorkNumberTxt.Text, "請輸入正確的固床工數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.groundsillWorkNum = n;

            if (p.sedimentationWeirCheck)
            {
                if (!ControllerUtility.CheckConvertInt32(ref n, sedimentationWeirNumberTxt.Text, "請輸入正確的攔河堰數量！", ControllerUtility.CheckType.GreaterThanZero))
                {
                    return false;
                }
            }
            else
            {
                n = 0;
            }
            p.sedimentationWeirNum = n;
            return true;
        }

        private bool DoConvert()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int n = 0;
            if (!ControllerUtility.CheckConvertInt32(ref n, maxIterationsNumTxt.Text, "請輸入正確的水理最大疊代次數！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }
            p.maxIterationsNum = n;

            if(!ConvertStructureSetNum())
            {
                return false;
            }
            
            return true;
        }

        private void tBarChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            tBarNumberTxt.Enabled = chk;
            RiverSimulationProfile.profile.tBarCheck = chk;
        }

        private void bridgePierChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            bridgePierNumberTxt.Enabled = chk;
            RiverSimulationProfile.profile.bridgePierCheck = chk;
        }

        private void groundsillWorkChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            groundsillWorkNumberTxt.Enabled = chk;
            RiverSimulationProfile.profile.groundsillWorkCheck = chk;
        }

        private void sedimentationWeirChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            sedimentationWeirNumberTxt.Enabled = chk;
            RiverSimulationProfile.profile.sedimentationWeirCheck = chk;
        }
        ////////////////////////////////////////////////////////////////////////////////
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


    }
}

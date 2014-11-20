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
        private SliderPanel sp = new SliderPanel();

        private void groupBox_MouseHover(object sender, EventArgs e)
        {
            GroupBox c = sender as GroupBox;
            string url = "file:///./" + Environment.CurrentDirectory.Replace('\\', '/');
            

            if(c==flowTypeGroup)
            {
                url += "/D1-1-0.html";
            }
            else if(c==groupBox2)
            {
                url += "/D1-1-1.html";
            }
            else if(c==secFlowEffectGrp)
            {
                url += "/D1-1-2.html";
            }
            else if(c==groupBox4)
            {
                url += "/D1-1-3.html";
            }
            else if(c==highSandContentEffectGrp)
            {
                url += "/D1-1-4.html";
            }
            //else if(c==groupBox6)
            //{
            //    url += "/D1-1-5.html";
            //}
            else if(c==dryBedEffectGrp)
            {
                url += "/D1-1-6.html";
            }
            else if(c==immersedBoundaryGrp)
            {
                url += "/D1-1-7.html";
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

        private void WaterModelingForm_Load(object sender, EventArgs e)
        {
            if(Program.programVersion.LiteVersion)
            {
                minWaterDepthPanel.Visible = false;
                minWaterDepthText.Visible = false;
                fullPanel.Visible = false;
            }

            string url = "file:///./" + Environment.CurrentDirectory + "\\D1-1.html";
            comment.Navigate(new Uri(url));

            //this.Width = 1000;
            //this.Height = 780;
            valueParamPanel.Visible = false;
            setting3dPanel.Visible = false;
            roughnessPanel.Visible = false;
            physicalParamPanel.Visible = false;
            //dryBedPanel.Visible = false;
            immersionPanel.Visible = false;
            this.CenterToParent();

            //diffusionEffectGrp.Enabled = RiverSimulationProfile.profile.diffusionEffectFunction;
            secFlowEffectGrp.Enabled = RiverSimulationProfile.profile.secFlowEffectFunction;
            dryBedEffectGrp.Enabled = RiverSimulationProfile.profile.dryBedEffectFunction;
            immersedBoundaryGrp.Enabled = RiverSimulationProfile.profile.immersedBoundaryFunction;
            highSandContentEffectGrp.Enabled = RiverSimulationProfile.profile.highSandContentEffectFunction;
            threeDGrp.Enabled = RiverSimulationProfile.profile.GetModuleType1() == RiverSimulationProfile.ModuleType1.Type3D;

            UpdateStatus();
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

        private void manningRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            manningBtn.Enabled = chk;
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
        private void ksRadio_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            ksTxt.Enabled = chk;
            ksHelpBtn.Enabled = chk;
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
            form.SetFormMode(manningBtn.Text, false, 26, 50);
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
            zeroEquationCombo.Enabled = chk;
        }

        private void twinEquationRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            twinEquationCombo.Enabled = chk;
        }

        private void WaterModelingForm_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void dryBedBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(dryBedNumTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if(n < 2)
            {
                MessageBox.Show("請輸入正確的乾床數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            RiverSimulationProfile.profile.ResizeDryBedPts(n);
            GridSelectForm form = new GridSelectForm();
            form.SetFormMode("乾床圈選", n, "乾床", GridSelectForm.SelectType.DryBed);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void immersedBoundaryBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(immersedBoundaryNumTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if(n < 2)
            {
                MessageBox.Show("請輸入正確的數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            RiverSimulationProfile.profile.ResizeImmersedBoundary(n);
            GridSelectForm form = new GridSelectForm();
            form.SetFormMode("浸沒邊界選取", n, "邊界", GridSelectForm.SelectType.ImmersedBoundary);
            if (DialogResult.OK == form.ShowDialog())
            {

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

        private void UpdateStatus()
        {
            convergenceCriteria3dTxt.Text = RiverSimulationProfile.profile.convergenceCriteria3d.ToString();
            convergenceCriteria2dTxt.Text = RiverSimulationProfile.profile.convergenceCriteria2d.ToString();


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

        private bool DoConvert()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int n = 0;
            if (!ControllerUtility.CheckConvertInt32(ref n, maxIterationsNumTxt.Text, "請輸入正確的水理最大疊代次數！", ControllerUtility.CheckType.GreaterThanZero))
            {
                return false;
            }
            p.maxIterationsNum = n;
            return true;
        }
    }
}

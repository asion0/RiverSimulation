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
            //else if (orgBtn == fluidTypeBtn)
            //{
            //    sp.SlidePanel(physicalParamPanel, SliderPanel.Direction.ToRight, this.Size);
            //}
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
            if(Program.IsLiteVersion())
            {
                fullPanel.Visible = false;
            }

            this.Width = 1000;
            this.Height = 720;
            valueParamPanel.Visible = false;
            physicalParamPanel.Visible = false;
            seabedCompositionPanel.Visible = false;
            rockStablePanel.Visible = false;
            rockbedPanel.Visible = false;
            this.CenterToParent();

            this.bedrockGrp.Enabled = RiverSimulationProfile.profile.bedrockFunction;
            this.quayStableAnalysisGrp.Enabled = RiverSimulationProfile.profile.quayStableAnalysisFunction;
            this.highSandContentFlowGrp.Enabled = RiverSimulationProfile.profile.highSandContentFlowFunction;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            sp.SlidePanel(null, SliderPanel.Direction.Back, this.Size);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //private void normalSandyFlowPanel_EnabledChanged(object sender, EventArgs e)
        //{
        //    bool ebd = (sender as Panel).Enabled;
        //    selTranSandEquChk.Enabled = ebd;
        //    if (ebd)
        //        tranSandEquCombo.Enabled = selTranSandEquChk.Checked;
        //    else
        //        tranSandEquCombo.Enabled = ebd; 

        //}

        //private void highSandyFlowPanel_EnabledChanged(object sender, EventArgs e)
        //{
        //    bool ebd = (sender as Panel).Enabled;
        //    selExpandEquChk.Enabled = ebd;
        //    if (ebd)
        //        expandEquCombo.Enabled = selExpandEquChk.Checked;
        //    else
        //        expandEquCombo.Enabled = ebd;
        //}

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

        //private void bedrockRdo_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool chk = (sender as RadioButton).Checked;
        //    if (chk)
        //    {
        //        if (erosionMechanismsChk.Checked)
        //        {
        //            erosionMechanismsChk.Checked = false;
        //            erosionMechanismsChk.Checked = true;
        //        }
        //    }
        //    else
        //    {
        //        waterErosionChk.Enabled = false;
        //        criticalShearStressBtn.Enabled = false;
        //        sedimentErosionChk.Enabled = false;
        //        elasticityBtn.Enabled = false;
        //        tensileStrengthBtn.Enabled = false;
        //    }
        //    bedrockElevationBtn.Enabled = chk;
        //    erosionMechanismsChk.Enabled = chk;
        //}


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
            amTxt.Enabled = chk;
            ceTxt.Enabled = chk;
        }

        private void seabedCompositionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;

            if (chk)
            {
                if (cohesiveSedimentChk.Checked)
                {
                    cohesiveSedimentChk.Checked = false;
                    cohesiveSedimentChk.Checked = true;
                }
            }
            else
            {
                amTxt.Enabled = false;
                ceTxt.Enabled = false;
            }
            seabedLevelNumTxt.Enabled = chk;
            seabedThicknessBtn.Enabled = chk;
            sedimentCompositionRatioBtn.Enabled = chk;
            cohesiveSedimentChk.Enabled = chk;
        }

        private void alluviumRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;

            if (chk)
            {
                if (seabedCompositionChk.Checked)
                {
                    seabedCompositionChk.Checked = false;
                    seabedCompositionChk.Checked = true;
                }
            }
            else
            {
                seabedLevelNumTxt.Enabled = false;
                seabedThicknessBtn.Enabled = false;
                sedimentCompositionRatioBtn.Enabled = false;
                cohesiveSedimentChk.Enabled = false;
                amTxt.Enabled = false;
                ceTxt.Enabled = false;
            }
            seabedCompositionChk.Enabled = chk;
            noScourElevationBtn.Enabled = chk;

        }

        private void seabedThicknessBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(seabedLevelNumTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if (n < 1)
            {
                MessageBox.Show("請輸入正確的底床分層厚度", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentParticleSizeBtn.Text, true, 1, n);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void sedimentCompositionRatioBtn_Click(object sender, EventArgs e)
        {
            int n1 = -1;
            int n2 = -1;
            try
            {
                n1 = Convert.ToInt32(seabedLevelNumTxt.Text);
                n2 = Convert.ToInt32(sedimentParticlesNumTxt.Text);
            }
            catch
            {
//                n1 = -1;
            }

            if (n2 < 1)
            {
                MessageBox.Show("請輸入正確的泥砂顆粒數目", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (n1 < 1)
            {
                MessageBox.Show("請輸入正確的底床分層厚度", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(sedimentParticleSizeBtn.Text, true, n1, n2);
            if (DialogResult.OK == form.ShowDialog())
            {

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

        private void selTransSandMethodChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            selTransSandMethodCombo.Enabled = chk;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void alluviumChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            alluviumPanel.Enabled = chk;
        }

        private void alluviumPanel_EnabledChanged(object sender, EventArgs e)
        {
            bool eab = (sender as Panel).Enabled;
            seabedCompositionChk.Enabled = eab;
            noScourElevationBtn.Enabled = eab;
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
 


    }
}

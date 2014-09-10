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
        private SliderPanel sp = new SliderPanel();

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

        private void BoundaryConditionsForm_Load(object sender, EventArgs e)
        {
            //string url = "file:///./" + Environment.CurrentDirectory + "\\D1-1.html";
            //comment.Navigate(new Uri(url));

            this.Width = 1000;
            this.Height = 720;
            waterModelingPanel.Visible = false;
            moveableBedPanel.Visible = false;
            this.CenterToParent();

            waterSurfacePanel.Enabled = (RiverSimulationProfile.profile.GetModuleType1() == RiverSimulationProfile.ModuleType1.Type3D);
            bottomBedPanel.Enabled = (RiverSimulationProfile.profile.GetModuleType1() == RiverSimulationProfile.ModuleType1.Type3D);
            bottomBedPanel2.Enabled = (RiverSimulationProfile.profile.GetModuleType1() == RiverSimulationProfile.ModuleType1.Type3D);
            sideInOutFlowPanel.Enabled = RiverSimulationProfile.profile.sideInOutFlowFunction;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void boundaryThicknessChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            boundaryThicknessCombo.Enabled = chk;
        }

        private void upVerticalDistributionChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            upVerticalDistributionBtn.Enabled = chk;
            upVerticalDistributionNoTxt.Enabled = chk;
        }


        private void upFlowConditionsChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            condNoTxt.Enabled = chk;

            if(chk)
            {
                upSupercriticalFlowRdo.Enabled = true;
                upSubcriticalFlowRdo.Enabled = true;
                if(upSupercriticalFlowRdo.Checked)
                {
                    upSupercriticalFlowRdo.Checked = false;
                    upSupercriticalFlowRdo.Checked = true;
                }
                if (upSubcriticalFlowRdo.Checked)
                {
                    upSubcriticalFlowRdo.Checked = false;
                    upSubcriticalFlowRdo.Checked = true;
                }
            }
            else
            {
                upSupercriticalFlowRdo.Enabled = false;
                upSubcriticalFlowRdo.Enabled = false;
                upSuperFlowBtn.Enabled = false;
                upSuperWaterLevelBtn.Enabled = false;
                upSubFlowBtn.Enabled = false;
            }
            
        }

        private void upSupercriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            upSuperFlowBtn.Enabled = chk;
            upSuperWaterLevelBtn.Enabled = chk;
        }

        private void upSubcriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            upSubFlowBtn.Enabled = chk;
     }

        private void downSupercriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;

        }

        private void downSubcriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
           bool chk = (sender as RadioButton).Checked;
           downSuperWaterLevelBtn.Enabled = chk;
        }

        private void sideInFlowChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            inPositionBtn.Enabled = chk;
            inFlowBtn.Enabled = chk;
            sideInFlowNumTxt.Enabled = chk;
        }

        private void sideOutFlowChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            outPositionBtn.Enabled = chk;
            outFlowBtn.Enabled = chk;
            sideOutFlowNumTxt.Enabled = chk;
        }

        private void upVerticalDistributionBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(upVerticalDistributionNoTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if(n < 2)
            {
                MessageBox.Show("請輸入正確的分層數(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TableInputForm form = new TableInputForm(TableInputForm.Type.UpVerticalDistribution);
            form.SetFormMode(upVerticalDistributionChk.Text, true, 2, n);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        
        }

        private void superFlowBtn_Click(object sender, EventArgs e)
        {

        }

        private void upSuperWaterLevelBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(condNoTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if (n < 2)
            {
                MessageBox.Show("請輸入正確的邊界條件數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            TableInputForm form = new TableInputForm();
            form.SetFormMode(upSupercriticalFlowRdo.Text + " " + upSuperWaterLevelBtn.Text, false, n, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void upSuperFlowBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(condNoTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if (n < 2)
            {
                MessageBox.Show("請輸入正確的邊界條件數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            TableInputForm form = new TableInputForm();
            form.SetFormMode(upSupercriticalFlowRdo.Text + " " + upSuperFlowBtn.Text, false, n, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void upSubFlowBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(condNoTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if (n < 2)
            {
                MessageBox.Show("請輸入正確的邊界條件數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }



            TableInputForm form = new TableInputForm();
            form.SetFormMode(upSupercriticalFlowRdo.Text + " " + upSubFlowBtn.Text, false, n, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void downSuperWaterLevelBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(condNoTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if (n < 2)
            {
                MessageBox.Show("請輸入正確的邊界條件數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            TableInputForm form = new TableInputForm();
            form.SetFormMode(downSubcriticalFlowRdo.Text + " " + downSuperWaterLevelBtn.Text, false, n, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }

        }

        private void inFlowBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(sideInFlowNumTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if (n < 2)
            {
                MessageBox.Show("請輸入正確的側入流數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(sideInFlowChk.Text + " " + inFlowBtn.Text, false, n, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void outFlowBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(sideOutFlowNumTxt.Text);
            }
            catch
            {
                n = -1;
            }

            if (n < 2)
            {
                MessageBox.Show("請輸入正確的側出流數目(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TableInputForm form = new TableInputForm();
            form.SetFormMode(sideOutFlowChk.Text + " " + outFlowBtn.Text, false, n, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}

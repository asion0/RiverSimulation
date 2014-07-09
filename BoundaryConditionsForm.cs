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
                sp.SlidePanel(waterModelingPanel, SliderPanel.Direction.ToRight);
            }
            else if (orgBtn == moveableBedBtn)
            {
                sp.SlidePanel(moveableBedPanel, SliderPanel.Direction.ToRight);
            }

        }

        private void Back_Click(object sender, EventArgs e)
        {
            sp.SlidePanel(null, SliderPanel.Direction.ToLeft);
        }

        private void BoundaryConditionsForm_Load(object sender, EventArgs e)
        {
            //string url = "file:///./" + Environment.CurrentDirectory + "\\D1-1.html";
            //comment.Navigate(new Uri(url));

            this.Width = 912;
            this.Height = 720;
            waterModelingPanel.Visible = false;
            moveableBedPanel.Visible = false;
            this.CenterToParent();
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
                superCondNoTxt.Enabled = false;
                superFlowBtn.Enabled = false;
                upSuperWaterLevelBtn.Enabled = false;
                subCondNoTxt.Enabled = false;
                subFlowBtn.Enabled = false;
            }
            
        }

        private void upSupercriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            superCondNoTxt.Enabled = chk;
            superFlowBtn.Enabled = chk;
            upSuperWaterLevelBtn.Enabled = chk;
        }

        private void upSubcriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            subCondNoTxt.Enabled = chk;
            subFlowBtn.Enabled = chk;
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
        }

        private void sideOutFlowChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            outPositionBtn.Enabled = chk;
            outFlowBtn.Enabled = chk;
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
            TableInputForm form = new TableInputForm();
            form.SetFormMode(upVerticalDistributionChk.Text, true, 2, n);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        
        }

        
    }
}

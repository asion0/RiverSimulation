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

            //waterSurfacePanel.Enabled = p.Is3DMode();
            //bottomBedPanel.Enabled = p.Is3DMode();
            //bottomBedPanel2.Enabled = p.Is3DMode();
            //sideInOutFlowPanel.Enabled = RiverSimulationProfile.profile.sideInOutFlowFunction;

            ////upVertPanel.Enabled = p.Is3DMode();
            //upSand3DPanel.Enabled = p.Is3DMode();
            ////waterUpVert3dPanel.Enabled = p.Is3DMode();

  
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
                nonSidewallBoundarySlipRdo.Checked = true;
            }
            else
            {
                sidewallBoundarySlipRdo.Checked = true;
            }

            //4.1.4 水面 三維 only。(”即時互動處”不放圖示)
            mainstreamWindShearTxt.Text = p.mainstreamWindShear.ToString();              //4.1.4.1 主流方向風剪 單一數值 N/m2 0 實數 實數 8 格
            sideWindShearTxt.Text = p.sideWindShear.ToString();                    //4.1.4.2 側方向風剪 單一數值 N/m2 0 實數 實數 8 格
            coriolisForceTxt.Text = p.coriolisForce.ToString();                    //4.1.4.3 科氏力 單一數值 N/m2 0 實數 實數 8 格

            //4.1.5 底床 實數 三維 only。(”即時互動處”不放圖示)
            boundaryLayerThicknessCombo.SelectedIndex = p.boundaryLayerThickness - 1; //4.1.5.1 邊界層厚度 三選一 3 整數(>0) 整數 8 格 1、2、3，三維 only，下拉選單。
            seabedBoundarySlipCombo.SelectedIndex = (int)p.seabedBoundarySlip;           //4.1.5.2 底床邊界滑移 三選一 -- 0 整數(>0) 整數 8 格 a. 三維 only，下拉選單 b. 0：非滑移、1：滑移、2：壁函數

        }

        private void UpdateStatus()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            if (Program.programVersion.DemoVersion)
            {
                downSand3DPanel.Enabled = false;
            }
            nonSidewallBoundarySlipRdo.Checked = !p.sidewallBoundarySlip;
            sidewallBoundarySlipRdo.Checked = p.sidewallBoundarySlip;
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

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
 /*
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }


        private void boundaryThicknessChk_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void upSupercriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
        //    bool chk = (sender as RadioButton).Checked;
        //    upSuperFlowBtn.Enabled = chk;
        //    upSuperWaterLevelBtn.Enabled = chk;
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
        //    bool chk = (sender as CheckBox).Checked;
        //    inPositionBtn.Enabled = chk;
        //    inFlowBtn.Enabled = chk;
        //    sideInFlowNumTxt.Enabled = chk;
        }

        private void sideOutFlowChk_CheckedChanged(object sender, EventArgs e)
        {
        //    bool chk = (sender as CheckBox).Checked;
        //    outPositionBtn.Enabled = chk;
        //    outFlowBtn.Enabled = chk;
        //    sideOutFlowNumTxt.Enabled = chk;
        }

        private void upVerticalDistributionBtn_Click(object sender, EventArgs e)
        {
        //    int n = 0;
        //    try
        //    {
        //        n = Convert.ToInt32(upVerticalDistributionNoTxt.Text);
        //    }
        //    catch
        //    {
        //        n = -1;
        //    }

        //    if(n < 2)
        //    {
        //        MessageBox.Show("請輸入正確的分層數(大於2)", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }
        //    TableInputForm form = new TableInputForm(TableInputForm.Type.UpVerticalDistribution);
        //    form.SetFormMode(upVerticalDistributionChk.Text, true, 2, n);
        //    if (DialogResult.OK == form.ShowDialog())
        //    {

        //    }
        
        }

        private void superFlowBtn_Click(object sender, EventArgs e)
        {

        }

        //private void upSuperWaterLevelBtn_Click(object sender, EventArgs e)
        //{
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
        //    form.SetFormMode(upSupercriticalFlowRdo.Text + " " + upSuperWaterLevelBtn.Text, false, n, 50);
        //    if (DialogResult.OK == form.ShowDialog())
        //    {

        //    }
        //}

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

        private void noSidewallSlideRdo_CheckedChanged(object sender, EventArgs e)
        {
            RiverSimulationProfile.profile.sidewallBoundarySlip = !(nonSidewallBoundarySlipRdo.Checked);
        }

        private void sidewallSlideRdo_CheckedChanged(object sender, EventArgs e)
        {
            RiverSimulationProfile.profile.sidewallBoundarySlip = sidewallBoundarySlipRdo.Checked;

        }

 
        private void moveableBedPanel_Paint(object sender, PaintEventArgs e)
        {


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
        }

        private void upSubCriticalFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            subBoundaryConditionNumberTxt.Enabled = chk;
            subFlowQuantityBtn.Enabled = chk;
            p.upFlowCondition = RiverSimulationProfile.FlowConditionType.SubCriticalFlow;
        }

        private void superFlowQuantityBtn_Click(object sender, EventArgs e)
        {

        }

        private void superWaterLevelBtn_Click(object sender, EventArgs e)
        {

        }

        private void subFlowQuantityBtn_Click(object sender, EventArgs e)
        {

        }

        private void verticalVelocityDistributionChk_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void verticalVelocityDistributionBtn_Click(object sender, EventArgs e)
        {

        }

        private void downSuperCriticalFlowRdo_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void downSubCriticalFlowRdo_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void downSuperWaterLevelBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void nonSidewallBoundarySlipRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void sidewallBoundarySlipRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void boundaryLayerThicknessCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void seabedBoundarySlipCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }

}

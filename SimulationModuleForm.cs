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
    public partial class SimulationModuleForm : Form
    {
        public SimulationModuleForm()
        {
            InitializeComponent();
        }

        private RiverSimulationProfile p = RiverSimulationProfile.profile;
        public void SetForm(RiverSimulationProfile profile)
        {
            p = profile;
        }

        private void SimulationModuleForm_Load(object sender, EventArgs e)
        {
            LoadStatus();
            UpdateStatus();
        }

        private void LoadStatus()
        {
            switch(p.dimensionType)
            {
                case RiverSimulationProfile.DimensionType.Type2D:
                    dimension2dRdo.Checked = true;
                    break;
                case RiverSimulationProfile.DimensionType.Type3D:
                    dimension3dRdo.Checked = true;
                    break;
                default:
                    dimension2dRdo.Checked = false;
                    dimension3dRdo.Checked = false;
                    break;
            }

            switch (p.modelingType)
            {
                case RiverSimulationProfile.ModelingType.WaterModeling:
                    waterModelingRdo.Checked = true;
                    break;
                case RiverSimulationProfile.ModelingType.MovableBed:
                    movableBedRdo.Checked = true;
                    break;
                default:
                    waterModelingRdo.Checked = false;
                    movableBedRdo.Checked = false;
                    break;
            }

            closeDiffusionEffectFunctionChk.Checked = p.closeDiffusionEffectFunction;
            secondFlowEffectFunctionChk.Checked = p.secondFlowEffectFunction;
            structureSetFunctionChk.Checked = p.structureSetFunction;
            sideInOutFlowFunctionChk.Checked = p.sideInOutFlowFunction;
            waterHighSandContentEffectFunctionChk.Checked = p.waterHighSandContentEffectFunction;

            bedrockFunctionChk.Checked = p.bedrockFunction;
            quayStableAnalysisFunctionChk.Checked = p.quayStableAnalysisFunction;
            movableBedHighSandContentEffectFunctionChk.Checked = p.movableBedHighSandContentEffectFunction;
        }

        private void UpdateStatus()
        {
            secondFlowEffectFunctionChk.Enabled = p.Is2DMode();
            movableBedPanel.Enabled = p.IsMovableBedMode();
            if(p.IsMovableBedMode())
            {   //如果是動床模式，則動床高含砂效應與水理高含砂效應連動
                waterHighSandContentEffectFunctionChk.Checked = movableBedHighSandContentEffectFunctionChk.Checked;
            }
            waterPanel.Enabled = p.IsWaterModelingMode() || p.IsMovableBedMode();
            waterHighSandContentEffectFunctionChk.Enabled = p.IsWaterModelingMode();

            UpdateActiveFunctions();
        }

        private void UpdateActiveFunctions()
        {
            if (Program.programVersion.DemoVersion)
            {
                movableBedPanel.Enabled = false;
                //sideInOutFlowFunctionChk.Enabled = false;
                waterHighSandContentEffectFunctionChk.Enabled = false;
            }
        }

        private void dimensionRdo_CheckedChanged(object sender, EventArgs e)
        {
            if(dimension2dRdo.Checked)
            {
                p.dimensionType = RiverSimulationProfile.DimensionType.Type2D;
            }
            else if (dimension3dRdo.Checked)
            {
                p.dimensionType = RiverSimulationProfile.DimensionType.Type3D;
            }
            else
            {
                p.dimensionType = RiverSimulationProfile.DimensionType.None;
            }
            UpdateStatus();
        }

        private void modelingRdo_CheckedChanged(object sender, EventArgs e)
        {
            if (waterModelingRdo.Checked)
            {
                p.modelingType = RiverSimulationProfile.ModelingType.WaterModeling;
            }
            else if (movableBedRdo.Checked)
            {
                p.modelingType = RiverSimulationProfile.ModelingType.MovableBed;
            }
            else
            {
                p.modelingType = RiverSimulationProfile.ModelingType.None;
            }
            UpdateStatus();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            //本畫面無被動控件，不須呼叫DoConvert
            if(p.dimensionType == RiverSimulationProfile.DimensionType.None)
            {
                MessageBox.Show("請選擇維度！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(p.modelingType == RiverSimulationProfile.ModelingType.None)
            {
                MessageBox.Show("請選擇模組！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void closeDiffusionEffectFunctionChk_CheckedChanged(object sender, EventArgs e)
        {
            p.closeDiffusionEffectFunction = (sender as CheckBox).Checked;
        }

        private void secondFlowEffectFunctionChk_CheckedChanged(object sender, EventArgs e)
        {
            p.secondFlowEffectFunction = (sender as CheckBox).Checked;
        }

        private void structureSetFunctionChk_CheckedChanged(object sender, EventArgs e)
        {
            p.structureSetFunction = (sender as CheckBox).Checked;
        }

        private void sideInOutFlowFunctionChk_CheckedChanged(object sender, EventArgs e)
        {
            p.sideInOutFlowFunction = (sender as CheckBox).Checked;
        }

        private void waterHighSandContentEffectFunctionChk_CheckedChanged(object sender, EventArgs e)
        {
            p.waterHighSandContentEffectFunction = (sender as CheckBox).Checked;
        }

        private void bedrockFunctionChk_CheckedChanged(object sender, EventArgs e)
        {
            p.bedrockFunction = (sender as CheckBox).Checked;
        }

        private void quayStableAnalysisFunctionChk_CheckedChanged(object sender, EventArgs e)
        {
            p.quayStableAnalysisFunction = (sender as CheckBox).Checked;
        }

        private void movableBedHighSandContentEffectFunctionChk_CheckedChanged(object sender, EventArgs e)
        {
            p.movableBedHighSandContentEffectFunction = (sender as CheckBox).Checked;
            UpdateStatus();
        }
    }
}

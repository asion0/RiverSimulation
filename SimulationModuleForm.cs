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

        private void SimulationModuleForm_Load(object sender, EventArgs e)
        {
            if (Program.programVersion.DemoVersion)
            {
                immersedBoundaryChk.Enabled = false;
                sideInOutFlowChk.Enabled = false;
                highSandContentEffectChk.Enabled = false;
                movableBedPanel.Enabled = false;
            }

            this.CenterToParent();
            LoadStatus();
            UpdateStatus();
        }

        private void type2dRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.SetModuleType1((chk) ? RiverSimulationProfile.ModuleType1.Type2D : RiverSimulationProfile.ModuleType1.Type3D);
            if(type3dRdo.Checked == chk)
            {
                type3dRdo.Checked = !chk;
            }
            UpdateStatus();
        }

        private void type3dRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.SetModuleType1((chk) ? RiverSimulationProfile.ModuleType1.Type3D : RiverSimulationProfile.ModuleType1.Type2D);
            if (type2dRdo.Checked == chk)
            {
                type2dRdo.Checked = !chk;
            }
            UpdateStatus();
        }

        private void typeWaterModelingRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.SetModuleType2( (chk) ? RiverSimulationProfile.ModuleType2.WaterModeling : RiverSimulationProfile.ModuleType2.MovableBed);
            if (typeMovableBedRdo.Checked == chk)
            {
                typeMovableBedRdo.Checked = !chk;
            }
            UpdateStatus();
        }

        private void typeMovableBedRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.SetModuleType2((chk) ? RiverSimulationProfile.ModuleType2.MovableBed : RiverSimulationProfile.ModuleType2.WaterModeling);
            if (typeWaterModelingRdo.Checked == chk)
            {
                typeWaterModelingRdo.Checked = !chk;
            }

            UpdateStatus();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadStatus()
        {
            type2dRdo.Checked = (RiverSimulationProfile.profile.GetModuleType1() == RiverSimulationProfile.ModuleType1.Type2D) ? true : false;
            type3dRdo.Checked = (RiverSimulationProfile.profile.GetModuleType1() == RiverSimulationProfile.ModuleType1.Type3D) ? true : false;

            typeWaterModelingRdo.Checked = (RiverSimulationProfile.profile.GetModuleType2() == RiverSimulationProfile.ModuleType2.WaterModeling) ? true : false;
            typeMovableBedRdo.Checked = (RiverSimulationProfile.profile.GetModuleType2() == RiverSimulationProfile.ModuleType2.MovableBed) ? true : false;

            diffusionEffectChk.Checked = RiverSimulationProfile.profile.diffusionEffectFunction;
            secFlowEffectChk.Checked = RiverSimulationProfile.profile.secFlowEffectFunction;
            dryBedEffectChk.Checked = RiverSimulationProfile.profile.dryBedEffectFunction;
            immersedBoundaryChk.Checked = RiverSimulationProfile.profile.immersedBoundaryFunction;
            sideInOutFlowChk.Checked = RiverSimulationProfile.profile.sideInOutFlowFunction;
            highSandContentEffectChk.Checked = RiverSimulationProfile.profile.highSandContentEffectFunction;

            bedrockChk.Checked = RiverSimulationProfile.profile.bedrockFunction;
            quayStableAnalysisChk.Checked = RiverSimulationProfile.profile.quayStableAnalysisFunction;
            highSandContentFlowChk.Checked = RiverSimulationProfile.profile.highSandContentFlowFunction;
        }

        private void UpdateStatus()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;

            secFlowEffectChk.Enabled = (p.GetModuleType1() == RiverSimulationProfile.ModuleType1.Type2D);
            movableBedPanel.Enabled = (p.GetModuleType2() == RiverSimulationProfile.ModuleType2.MovableBed);
            highSandContentEffectChk.Enabled = (p.GetModuleType2() == RiverSimulationProfile.ModuleType2.WaterModeling);
        }

        private void diffusionEffectChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.diffusionEffectFunction = chk;
        }

        private void dryBedEffectChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.dryBedEffectFunction = chk;
        }

        private void immersedBoundaryChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.immersedBoundaryFunction = chk;
        }

        private void sideInOutFlowChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.sideInOutFlowFunction = chk;
        }

        private void highSandContentEffectChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.highSandContentEffectFunction = chk;
        }

        private void bedrockChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.bedrockFunction = chk;
        }

        private void quayStableAnalysisChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.quayStableAnalysisFunction = chk;
        }

        private void highSandContentFlowChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.highSandContentFlowFunction = chk;
            //20141119 新增規格，使水理"高含砂效應"與動床"高含砂效應"同步
            highSandContentEffectChk.Checked = RiverSimulationProfile.profile.highSandContentFlowFunction;

        }

        private void secFlowEffectChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            RiverSimulationProfile.profile.secFlowEffectFunction = chk;
        }
    }
}

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
            this.CenterToParent();
            UpdateStatus();
        }

        private void type2dRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            RiverSimulationProfile.profile.SetModuleType1((chk) ? RiverSimulationProfile.ModuleType1.Type2D : RiverSimulationProfile.ModuleType1.Type3D);
        }

        private void type3dRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            RiverSimulationProfile.profile.SetModuleType1((chk) ? RiverSimulationProfile.ModuleType1.Type3D : RiverSimulationProfile.ModuleType1.Type2D);
        }

        private void typeWaterModelingRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            RiverSimulationProfile.profile.SetModuleType2( (chk) ? RiverSimulationProfile.ModuleType2.WaterModeling : RiverSimulationProfile.ModuleType2.MovableBed);
        }

        private void typeMovableBedRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            RiverSimulationProfile.profile.SetModuleType2((chk) ? RiverSimulationProfile.ModuleType2.MovableBed : RiverSimulationProfile.ModuleType2.WaterModeling);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdateStatus()
        {
            type2dRdo.Checked = (RiverSimulationProfile.profile.GetModuleType1() == RiverSimulationProfile.ModuleType1.Type2D) ? true : false;
            type3dRdo.Checked = (RiverSimulationProfile.profile.GetModuleType1() == RiverSimulationProfile.ModuleType1.Type3D) ? true : false;

            typeWaterModelingRdo.Checked = (RiverSimulationProfile.profile.GetModuleType2() == RiverSimulationProfile.ModuleType2.WaterModeling) ? true : false;
            typeMovableBedRdo.Checked = (RiverSimulationProfile.profile.GetModuleType2() == RiverSimulationProfile.ModuleType2.MovableBed) ? true : false;
        }
    }
}

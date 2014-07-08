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
    public partial class RiverSimulationForm : Form
    {
        public RiverSimulationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutMnuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void waterModelingBtn_Click(object sender, EventArgs e)
        {
            WaterModelingForm form = new WaterModelingForm();


            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.waterModelingFinished = true;
            UpdateStatus();

        }

        private void simulationModuleBtn_Click(object sender, EventArgs e)
        {
            SimulationModuleForm form = new SimulationModuleForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.simulationModuleFinished = true;
            UpdateStatus();
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            ImportForm form = new ImportForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.importFinished = true;
            UpdateStatus();
        }

        private void movableBedBtn_Click(object sender, EventArgs e)
        {
            MovableBedForm form = new MovableBedForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.movableBedFinished = true;
            UpdateStatus();
        }

        private void initialConditionsBtn_Click(object sender, EventArgs e)
        {
            InitialConditionsForm form = new InitialConditionsForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.initialConditionsFinished = true;
            UpdateStatus();
        }

        private void boundaryConditionsBtn_Click(object sender, EventArgs e)
        {
            BoundaryConditionsForm form = new BoundaryConditionsForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.boundaryConditionsFinished = true;
            UpdateStatus();
        }
        private void UpdateStatus()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            Color FinishedButton = Color.LimeGreen;
            Color ReadyButton = Color.Goldenrod;
            Color DisableButton = SystemColors.Control;
            simulationModuleBtn.Enabled = p.IsSimulationModuleReady();


            importBtn.BackColor = (p.IsImportFinished()) ? FinishedButton : (p.IsImportReady()) ? ReadyButton : DisableButton;
            importBtn.Enabled = p.IsImportReady();

            simulationModuleBtn.BackColor = (p.IsSimulationModuleFinished()) ? FinishedButton : (p.IsSimulationModuleReady()) ? ReadyButton : DisableButton;
            waterModelingBtn.BackColor = (p.IsWaterModelingFinished()) ? FinishedButton : (p.IsWaterModelingReady()) ? ReadyButton : DisableButton;
            waterModelingBtn.Enabled = p.IsWaterModelingReady();

            movableBedBtn.BackColor = (p.IsMovableBedFinished()) ? FinishedButton : (p.IsMovableBedReady()) ? ReadyButton : DisableButton;
            movableBedBtn.Enabled = p.IsMovableBedReady();

            initialConditionsBtn.BackColor = (p.IsInitialConditionsFinished()) ? FinishedButton : (p.IsInitialConditionsReady()) ? ReadyButton : DisableButton;
            initialConditionsBtn.Enabled = p.IsInitialConditionsReady();

            boundaryConditionsBtn.BackColor = (p.IsBoundaryConditionsFinished()) ? FinishedButton : (p.IsBoundaryConditionsReady()) ? ReadyButton : DisableButton;
            boundaryConditionsBtn.Enabled = p.IsBoundaryConditionsReady();

            runSimulationBtn.BackColor = (p.IsRunSimulationFinished()) ? FinishedButton : (p.IsRunSimulationReady()) ? ReadyButton : DisableButton;
            runSimulationBtn.Enabled = p.IsRunSimulationReady();

            simulationResultBtn.BackColor = (p.IsSimulationResultFinished()) ? FinishedButton : (p.IsSimulationResultReady()) ? ReadyButton : DisableButton;
            simulationResultBtn.Enabled = p.IsSimulationResultReady();
        }

        private void runSimulationBtn_Click(object sender, EventArgs e)
        {
            RunSimulationForm form = new RunSimulationForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.runSimulationFinished = true;
            UpdateStatus();
        }

    }
}

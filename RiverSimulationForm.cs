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
    public partial class RiverSimulation : Form
    {
        public RiverSimulation()
        {
            InitializeComponent();
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void waterModeling_Click(object sender, EventArgs e)
        {
            WaterModelingForm form = new WaterModelingForm();
            
            
            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.waterModelingFinished = true;
        }

        private void simulationModule_Click(object sender, EventArgs e)
        {
            SimulationModuleForm form = new SimulationModuleForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.simulationModuleFinished = true;
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            ImportForm form = new ImportForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.importFinished = true;
        }

        private void movableBedBtn_Click(object sender, EventArgs e)
        {
            MovableBedForm form = new MovableBedForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.movableBedFinished = true;
        }

        private void initialConditionsBtn_Click(object sender, EventArgs e)
        {
            InitialConditionsForm form = new InitialConditionsForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.InitialConditionsFinished = true;
        }

        private void boundaryConditionsBtn_Click(object sender, EventArgs e)
        {
            BoundaryConditionsForm form = new BoundaryConditionsForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.BoundaryConditionsFinished = true;
        }

        private void RiverSimulation_Load(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            Color FinishedButton = Color.LimeGreen;
            Color ReadyButton = Color.LimeGreen;
            Color DisableButton = SystemColors.ActiveCaption;

            importBtn.BackColor = DisableButton;


        }
    }
}

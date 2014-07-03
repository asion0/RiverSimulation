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
        }

        private void simulationModule_Click(object sender, EventArgs e)
        {
            SimulationModuleForm form = new SimulationModuleForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            ImportForm form = new ImportForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void movableBedBtn_Click(object sender, EventArgs e)
        {
            MovableBedForm form = new MovableBedForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void initialConditionsBtn_Click(object sender, EventArgs e)
        {
            InitialConditionsForm form = new InitialConditionsForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }
    }
}

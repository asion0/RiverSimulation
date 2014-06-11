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
    }
}

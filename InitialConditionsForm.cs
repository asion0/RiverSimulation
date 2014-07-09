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
    public partial class InitialConditionsForm : Form
    {
        public InitialConditionsForm()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitialConditionsForm_Load(object sender, EventArgs e)
        {

        }

        private void uFlowSpeedBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(uFlowSpeedBtn.Text, false, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void vFlowSpeedBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(vFlowSpeedBtn.Text, false, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void initialWaterLevelBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(initialWaterLevelBtn.Text, false, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void assumingSectionChk_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void initialConcentrationBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(initialConcentrationBtn.Text, false, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }
    }
}

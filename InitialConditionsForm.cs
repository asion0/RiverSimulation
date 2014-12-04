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

        RiverSimulationProfile p = RiverSimulationProfile.profile;
        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitialConditionsForm_Load(object sender, EventArgs e)
        {
            ControllerUtility.SetHtmlUrl(comment, "Logo.html");

            LoadStatus();
            UpdateStatus();

        }

        private void LoadStatus()
        {
            switch (p.verticalVelocitySlice)
            {
                case RiverSimulationProfile.VerticalVelocitySliceType.Open:
                    verticalVelocitySliceOpenRdo.Checked = true;
                    break;
                case RiverSimulationProfile.VerticalVelocitySliceType.Close:
                    verticalVelocitySliceCloseRdo.Checked = true;
                    break;
                default:
                    verticalVelocitySliceOpenRdo.Checked = false;
                    verticalVelocitySliceCloseRdo.Checked = false;
                    break;
            }

            switch (p.verticalConcentrationSlice)
            {
                case RiverSimulationProfile.VerticalConcentrationSliceType.Open:
                    verticalConcentrationSliceOpenRdo.Checked = true;
                    break;
                case RiverSimulationProfile.VerticalConcentrationSliceType.Close:
                    verticalConcentrationSliceCloseRdo.Checked = true;
                    break;
                default:
                    verticalConcentrationSliceOpenRdo.Checked = false;
                    verticalConcentrationSliceCloseRdo.Checked = false;
                    break;
            }
        }

        private void UpdateStatus()
        {
            initialWater3DPanel.Enabled = p.Is3DMode();
            initialMovableBed3DPanel.Enabled = p.Is3DMode();
        }

        //===============================================================================
        private void depthAverageFlowSpeedUBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(depthAverageFlowSpeedUBtn.Text, false, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void depthAverageFlowSpeedVBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(depthAverageFlowSpeedVBtn.Text, false, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void waterLevelBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(waterLevelBtn.Text, false, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void depthAverageConcentrationBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(depthAverageConcentrationBtn.Text, false, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void verticalVelocitySliceRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void verticalConcentrationSliceRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}

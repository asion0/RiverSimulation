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
    public partial class MovableBedForm : Form
    {
        public MovableBedForm()
        {
            InitializeComponent();
        }

        private SliderPanel sp = new SliderPanel();

        private void SettingButton_Click(object sender, EventArgs e)
        {
            Button orgBtn = sender as Button;
            if (orgBtn == valueParamBtn)
            {
                sp.SlidePanel(valueParamPanel, SliderPanel.Direction.ToRight);
            }
            //else if (orgBtn == setting3dBtn)
            //{
            //    sp.SlidePanel(setting3dPanel, SliderPanel.Direction.ToRight);
            //}
            else if (orgBtn == fluidTypeBtn)
            {
                sp.SlidePanel(fluidTypePanel, SliderPanel.Direction.ToRight);
            }
            else if (orgBtn == seabedCompositionBtn)
            {
                sp.SlidePanel(seabedCompositionPanel, SliderPanel.Direction.ToRight);
            }
            else if (orgBtn == rockStableBtn)
            {
                sp.SlidePanel(rockStablePanel, SliderPanel.Direction.ToRight);
            }
            //else if (orgBtn == immersionBtn)
            //{
            //    sp.SlidePanel(immersionPanel, SliderPanel.Direction.ToRight);
            //}

        }

        private void MovableBedForm_Load(object sender, EventArgs e)
        {
            //string url = "file:///./" + Environment.CurrentDirectory + "\\D1-1.html";
            //comment.Navigate(new Uri(url));

            this.Width = 912;
            this.Height = 720;
            valueParamPanel.Visible = false;
            fluidTypePanel.Visible = false;
            seabedCompositionPanel.Visible = false;
            rockStablePanel.Visible = false;
            this.CenterToParent();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            sp.SlidePanel(null, SliderPanel.Direction.ToLeft);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void normalSandyFlowPanel_EnabledChanged(object sender, EventArgs e)
        {
            bool ebd = (sender as Panel).Enabled;
            selTranSandEquChk.Enabled = ebd;
            if (ebd)
                tranSandEquCombo.Enabled = selTranSandEquChk.Checked;
            else
                tranSandEquCombo.Enabled = ebd; 

        }

        private void highSandyFlowPanel_EnabledChanged(object sender, EventArgs e)
        {
            bool ebd = (sender as Panel).Enabled;
            selExpandEquChk.Enabled = ebd;
            if (ebd)
                expandEquCombo.Enabled = selExpandEquChk.Checked;
            else
                expandEquCombo.Enabled = ebd;
        }

        private void normalSandyFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            normalSandyFlowPanel.Enabled = chk;
        }

        private void highSandyFlowRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            highSandyFlowPanel.Enabled = chk;
        }

        private void selTranSandEquChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            tranSandEquCombo.Enabled = chk;
        }

        private void selExpandEquChk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            expandEquCombo.Enabled = chk;
        }
    }
}

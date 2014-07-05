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
    public partial class BoundaryConditionsForm : Form
    {
        public BoundaryConditionsForm()
        {
            InitializeComponent();
        }
        private SliderPanel sp = new SliderPanel();

        private void SettingButton_Click(object sender, EventArgs e)
        {
            Button orgBtn = sender as Button;
            if (orgBtn == waterModelingBtn)
            {
                sp.SlidePanel(waterModelingPanel, SliderPanel.Direction.ToRight);
            }
            else if (orgBtn == moveableBedBtn)
            {
                sp.SlidePanel(moveableBedPanel, SliderPanel.Direction.ToRight);
            }

        }

        private void Back_Click(object sender, EventArgs e)
        {
            sp.SlidePanel(null, SliderPanel.Direction.ToLeft);
        }

        private void BoundaryConditionsForm_Load(object sender, EventArgs e)
        {
            //string url = "file:///./" + Environment.CurrentDirectory + "\\D1-1.html";
            //comment.Navigate(new Uri(url));

            this.Width = 912;
            this.Height = 720;
            waterModelingPanel.Visible = false;
            moveableBedPanel.Visible = false;
            this.CenterToParent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

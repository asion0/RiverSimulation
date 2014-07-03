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
    public partial class WaterModelingForm : Form
    {
        public WaterModelingForm()
        {
            InitializeComponent();
        }
        private SliderPanel sp = new SliderPanel();

        private void groupBox_MouseHover(object sender, EventArgs e)
        {
            GroupBox c = sender as GroupBox;
            string url = "file:///./" + Environment.CurrentDirectory.Replace('\\', '/');
            

            if(c==flowTypeGroup)
            {
                url += "/D1-1-0.html";
            }
            else if(c==groupBox2)
            {
                url += "/D1-1-1.html";
            }
            else if(c==groupBox3)
            {
                url += "/D1-1-2.html";
            }
            else if(c==groupBox4)
            {
                url += "/D1-1-3.html";
            }
            else if(c==groupBox5)
            {
                url += "/D1-1-4.html";
            }
            else if(c==groupBox6)
            {
                url += "/D1-1-5.html";
            }
            else if(c==groupBox7)
            {
                url += "/D1-1-6.html";
            }
            else if(c==groupBox8)
            {
                url += "/D1-1-7.html";
            }
            else
            {
                url += "/D1-1.html";
            }

            if (comment.Url.ToString() != url)
            {
                comment.Navigate(new Uri(url));
            }
        }

        private void WaterModelingForm_Load(object sender, EventArgs e)
        {
            string url = "file:///./" + Environment.CurrentDirectory + "\\D1-1.html";
            comment.Navigate(new Uri(url));

            this.Width = 912;
            this.Height = 720;
            valueParamPanel.Visible = false;
            setting3dPanel.Visible = false;
            roughnessPanel.Visible = false;
            turbulencePanel.Visible = false;
            dryBedPanel.Visible = false;
            immersionPanel.Visible = false;
            this.CenterToParent();
        }


        private void Back_Click(object sender, EventArgs e)
        {
            sp.SlidePanel(null, SliderPanel.Direction.ToLeft);
        }

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
            //else if (orgBtn == roughnessBtn)
            //{
            //    sp.SlidePanel(roughnessPanel, SliderPanel.Direction.ToRight);
            //}
            else if (orgBtn == turbulenceBtn)
            {
                sp.SlidePanel(turbulencePanel, SliderPanel.Direction.ToRight);
            }
            //else if (orgBtn == dryBedBtn)
            //{
            //    sp.SlidePanel(dryBedPanel, SliderPanel.Direction.ToRight);
            //}
            //else if (orgBtn == immersionBtn)
            //{
            //    sp.SlidePanel(immersionPanel, SliderPanel.Direction.ToRight);
            //}
        }

        private void inputManningBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void manningRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            manningBtn.Enabled = chk;
        }

        private void chezyRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            chezyBtn.Enabled = chk;
        }

        private void curvatureRadiusRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            curvatureRadiusBtn.Enabled = chk; 
        }

        private void autoCurvatureRdo_CheckedChanged(object sender, EventArgs e)
        {
            //curvatureRadiusBtn.Enabled = false;
        }

        private void curvatureRadiusBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(curvatureRadiusBtn.Text, true, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void manningBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(manningBtn.Text, true, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void chezyBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(chezyBtn.Text, true, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void PropStratBtn_Click(object sender, EventArgs e)
        {
            TableInputForm form = new TableInputForm();
            form.SetFormMode(propStratBtn.Text, true, 26, 50);
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }
    }
}

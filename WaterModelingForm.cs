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


        private void groupBox_MouseHover(object sender, EventArgs e)
        {
            GroupBox c = sender as GroupBox;
            string url = "file:///./" + Environment.CurrentDirectory;
            

            if(c==groupBox1)
            {
                url += "\\D1-1-0.html";
            }
            else if(c==groupBox2)
            {
                url += "\\D1-1-1.html";
            }
            else if(c==groupBox3)
            {
                url += "\\D1-1-2.html";
            }
            else if(c==groupBox4)
            {
                url += "\\D1-1-3.html";
            }
            else if(c==groupBox5)
            {
                url += "\\D1-1-4.html";
            }
            else if(c==groupBox6)
            {
                url += "\\D1-1-5.html";
            }
            else if(c==groupBox7)
            {
                url += "\\D1-1-6.html";
            }
            else if(c==groupBox8)
            {
                url += "\\D1-1-7.html";
            }
            else
            {
                url += "\\D1-1.html";
            }
            comment.Navigate(new Uri(url));
 
        }

        private void WaterModelingForm_Load(object sender, EventArgs e)
        {
            string url = "file:///./" + Environment.CurrentDirectory + "\\D1-1.html";
            comment.Navigate(new Uri(url));

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }
    }
}

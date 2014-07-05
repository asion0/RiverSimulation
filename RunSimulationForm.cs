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
    public partial class RunSimulationForm : Form
    {
        public RunSimulationForm()
        {
            InitializeComponent();
        }

        private void RunSimulationForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 100;
            progressBar.Maximum = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value != 1000)
            {
                progressBar.Value++;
            }
            else
            {
                timer1.Stop();
            }
        }
    }
}

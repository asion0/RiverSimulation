using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace RiverSimulationApplication
{
    public partial class Delft3dMeshForm : Form
    {
        public Delft3dMeshForm()
        {
            InitializeComponent();
        }

        private void quickInBtn_Click(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\plugins\\quickin.cmd";
            ProcessStartInfo start = new ProcessStartInfo(path);
            start.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(start);
        }

        private void rgfGridBtn_Click(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\plugins\\rgfgrid.cmd";
            ProcessStartInfo start = new ProcessStartInfo(path);
            start.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(start);
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

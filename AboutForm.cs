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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            version.Text = "版本 " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

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
    public partial class ImportImageForm : Form
    {
        public ImportImageForm()
        {
            InitializeComponent();
        }

        public void SetImage(string s)
        {
            Bitmap bmp = new Bitmap(s);
            picBox.BackgroundImage = bmp;

        }

        public double e = 269958.74;
        public double n = 2733516.753;
        public double w = 1129.091;
        public double h = 648.143;
        private void ok_Click(object sender, EventArgs e)
        {
            this.e = n = w = h = 0.0;
            try
            { this.e = Convert.ToDouble(eTxt.Text); }
            catch { }
            try
            { this.n = Convert.ToDouble(nTxt.Text); }
            catch { }
            try
            { this.w = Convert.ToDouble(wTxt.Text); }
            catch { }
            try
            { this.h = Convert.ToDouble(hTxt.Text); }
            catch { }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ImportImageForm_Load(object sender, EventArgs e)
        {
            eTxt.Text = this.e.ToString();
            nTxt.Text = this.n.ToString();
            wTxt.Text = this.w.ToString();
            hTxt.Text = this.h.ToString();
        }
    }


}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RiverSimulationApplication
{
    public partial class ImportImageForm : Form
    {
        public ImportImageForm()
        {
            InitializeComponent();
        }

        private string jgwPath = "";
        Bitmap bmp = null;
        bool hasJgw = false;
        public void SetImage(string s)
        {
            bmp = new Bitmap(s);
            picBox.BackgroundImage = bmp;

            jgwPath = Path.GetDirectoryName(s) + "\\" + Path.GetFileNameWithoutExtension(s) + ".jgw";
            if (File.Exists(jgwPath))
            {
                string line, line2;
                System.IO.StreamReader file = new System.IO.StreamReader(jgwPath);
                line = file.ReadLine();
                line2 = file.ReadLine();
                if (line != null && line2 != null)
                {
                    w = (Convert.ToDouble(line) - Convert.ToDouble(line2)) * bmp.Width;
                }

                line = file.ReadLine();
                line2 = file.ReadLine();
                if (line != null && line2 != null)
                {
                    h = (Convert.ToDouble(line) - Convert.ToDouble(line2)) * bmp.Height;
                }
                line = file.ReadLine();
                if (line != null )
                {
                    e = Convert.ToDouble(line);
                }
                line = file.ReadLine();
                if (line != null)
                {
                    n = Convert.ToDouble(line);
                }
                file.Close();
                hasJgw = true;
            }

        }
        public bool HasJgw() { return hasJgw; }
        private bool SaveJgwFile()
        {
            try
            {
                if (File.Exists(jgwPath))
                {
                    File.Delete(jgwPath);
                }

                string[] lines = new string[7];
                lines[0] = String.Format("{0,20}", (w / bmp.Width).ToString("F6"));
                lines[1] = String.Format("{0,20}", "0.000000");
                lines[2] = String.Format("{0,20}", "-0.000000");
                lines[3] = String.Format("{0,20}", "-" + (h / bmp.Height).ToString("F6"));
                lines[4] = String.Format("{0,20}", e.ToString("F6"));
                lines[5] = String.Format("{0,20}", n.ToString("F6"));
                lines[6] = "";

                System.IO.File.WriteAllLines(jgwPath, lines);
            }
            catch(IOException err)
            {
                MessageBox.Show("無法更新jgw檔案！\r\n" + err.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        public double e = 0;
        public double n = 0;
        public double w = 0;
        public double h = 0;
        private void ok_Click(object sender, EventArgs e)
        {
            //this.e = n = w = h = 0.0;
            bool change = false;
            double d = 0;
            try
            { 
                d = Convert.ToDouble(eTxt.Text); 
                if(this.e != d)
                {
                    this.e = d;
                    change = true;
                }
                d = Convert.ToDouble(nTxt.Text);
                if (this.n != d)
                {
                    this.n = d;
                    change = true;
                }
                d = Convert.ToDouble(wTxt.Text);
                if (this.w != d)
                {
                    this.w = d;
                    change = true;
                }
                d = Convert.ToDouble(hTxt.Text);
                if (this.h != d)
                {
                    this.h = d;
                    change = true;
                }
                if(change)
                {
                    SaveJgwFile();
                }
            }
            catch 
            {
                MessageBox.Show("無法轉換數值！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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

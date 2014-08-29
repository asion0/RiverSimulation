using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace RiverSimulationApplication
{
    public partial class RunSimulationForm : Form
    {
        public RunSimulationForm()
        {
            InitializeComponent();
        }

        private BackgroundWorker bw = new BackgroundWorker();
        private void RunSimulationForm_Load(object sender, EventArgs e)
        {

        }

        private void BwProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tbResult.AppendText("\r\n" + e.UserState as string);
        }

        void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) // 如果非同步作業已被取消...
            {
                //UpdateProgressBar("非同步作業已被取消。");
            }
            else // 如果非同步作業順利完成....
            {
                //UpdateProgressBar("非同步作業順利完成。");
            }
        }

        void BwDoWork(object sender, DoWorkEventArgs e)
        {
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += new ProgressChangedEventHandler(BwProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwRunWorkerCompleted);

            Process simMain = new Process();
            simMain.StartInfo.FileName = Program.currentPath + "\\resed2d.exe";
            simMain.StartInfo.WorkingDirectory = Program.currentPath;
            //resed2d 2dinput.i 123 3D 3dinput.dat out
            simMain.StartInfo.Arguments = "2dinput.i 123 3D 3dinput.dat out";
            simMain.StartInfo.UseShellExecute = false;
            simMain.StartInfo.RedirectStandardOutput = true;
            simMain.StartInfo.CreateNoWindow = true;
            simMain.Start();

            StreamReader reader = simMain.StandardOutput;//截取輸出流
            string line = reader.ReadLine();//每次讀取一行
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                bw.ReportProgress(0, line.Clone());
            }

            simMain.WaitForExit();
        }

        private bool RunSimulationMain()
        {
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(BwDoWork);
            bw.RunWorkerAsync(0);
            return true;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value != 1000)
            {
                progressBar.Value++;
                double p = (progressBar.Value / 10.0);
                msg.Text = p.ToString() + "%";
            }
            else
            {
                timer1.Stop();
                EnterSimUI(false);
            }
        }

        void EnterSimUI(bool b)
        {
            if(b)
            {
                startBtn.Enabled = false;
                stopFlagChk.Enabled = false;

            }
            else
            {

                startBtn.Enabled = true;
                stopFlagChk.Enabled = true;
            }

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 100;
            progressBar.Maximum = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            RunSimulationMain();
            EnterSimUI(true);

        }
    }
}

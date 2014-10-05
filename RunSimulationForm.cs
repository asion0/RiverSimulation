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
using System.Windows.Forms.DataVisualization.Charting;

namespace RiverSimulationApplication
{
    public partial class RunSimulationForm : Form
    {
        public RunSimulationForm()
        {
            InitializeComponent();
        }

        private BackgroundWorker bw = new BackgroundWorker();
        private SimDebugForm simDebugForm = new SimDebugForm();
        private void RunSimulationForm_Load(object sender, EventArgs e)
        {
            simDebugForm.Show();

            chart1.ChartAreas.Add("Base");
            chart1.Series.Add("DepthD");
            chart1.Series.Add("FlowU");
            chart1.Series.Add("FlowV");

            ChartArea myArea = chart1.ChartAreas["Base"];
            Series mySeriesD = chart1.Series["DepthD"];
            Series mySeriesU = chart1.Series["FlowU"];
            Series mySeriesV = chart1.Series["FlowV"];

            mySeriesD.LegendText = "水深d誤差";
            mySeriesU.LegendText = "流速u誤差";
            mySeriesV.LegendText = "流速v誤差";


            myArea.AxisX.Name = "疊代次數";
            myArea.AxisY.Name = "error";

            myArea.AxisX.MajorGrid.LineColor = Color.Transparent; // X軸的刻度 縱線
            myArea.AxisY.MajorGrid.LineColor = Color.LightGray;  // Y軸的刻度 橫線
            myArea.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            myArea.AxisY.Minimum = 0.000001;
            myArea.AxisY.IsLogarithmic = true;
            myArea.AxisY.LogarithmBase = 10.0;

            myArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            myArea.AxisX.IntervalOffsetType = DateTimeIntervalType.Auto;
            myArea.AxisX.Minimum = 0.0;

            // 設定平均值的 Line
            if (RiverSimulationProfile.profile.convergenceCriteria2d != 0.0)
            {
                StripLine lineMean = new StripLine();

                lineMean.Text = "收斂值 : " + RiverSimulationProfile.profile.convergenceCriteria2d.ToString();
                lineMean.BorderColor = Color.Black; // 線條的顏色
                lineMean.BorderDashStyle = ChartDashStyle.Dash;
                lineMean.BorderWidth = 1;
                lineMean.IntervalOffsetType = DateTimeIntervalType.Auto;
                //lineMean.Interval = RiverSimulationProfile.profile.convergenceCriteria2d;
                lineMean.IntervalOffset = Math.Log10(RiverSimulationProfile.profile.convergenceCriteria2d);
                myArea.AxisY.StripLines.Add(lineMean);
            }


            mySeriesD.ChartType = SeriesChartType.Spline;        // 曲線圖
            mySeriesD.Color = Color.Blue;               // 在圖型上的顏色
            mySeriesD.BorderWidth = 1;                   // 線型的寬度
            mySeriesD.ShadowColor = Color.Transparent;      // 陰影的顏色
            mySeriesD.ShadowOffset = 2;                  // 陰影位置的角度
            mySeriesD.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            mySeriesD.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊

            mySeriesU.ChartType = SeriesChartType.Spline;        // 曲線圖
            mySeriesU.Color = Color.Red;               // 在圖型上的顏色
            mySeriesU.BorderWidth = 1;                   // 線型的寬度
            mySeriesU.ShadowColor = Color.Transparent;      // 陰影的顏色
            mySeriesU.ShadowOffset = 2;                  // 陰影位置的角度
            mySeriesU.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            mySeriesU.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊

            mySeriesV.ChartType = SeriesChartType.Spline;        // 曲線圖
            mySeriesV.Color = Color.Green;               // 在圖型上的顏色
            mySeriesV.BorderWidth = 1;                   // 線型的寬度
            mySeriesV.ShadowColor = Color.Transparent;      // 陰影的顏色
            mySeriesV.ShadowOffset = 2;                  // 陰影位置的角度
            mySeriesV.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            mySeriesV.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊

            this.progressBar.Maximum = 10000;
        }

        private void BwProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressItem pi = (ProgressItem)e.UserState;
            simDebugForm.AddResultMessage("\r\n" + pi.msg);

            if(pi.t == ProgressItem.Type.ShowConsole)
            {
                return;
            }

            this.progressBar.Value = (int)(pi.prog * 100);
            if (pi.t == ProgressItem.Type.ShowProgress)
            {
                return;
            }

            if(pi.x <= 0)
            {
                return;
            }

            ChartArea myArea = chart1.ChartAreas["Base"];
            Series mySeriesD = chart1.Series["DepthD"];
            Series mySeriesU = chart1.Series["FlowU"];
            Series mySeriesV = chart1.Series["FlowV"];
            double dd = 0.000001;
            if (pi.z < dd)
            {
                pi.z += dd;
            }
            if (pi.u < dd)
            {
                pi.u += dd;
            }
            if (pi.v < dd)
            {
                pi.v += dd;
            }
            
            try
            {
                //if (pi.z > 0.0 && pi.u > 0.0 && pi.v > 0.0)
                {
                    mySeriesD.Points.AddXY(pi.x, String.Format("{0:0.000000}", pi.z));
                    mySeriesU.Points.AddXY(pi.x, String.Format("{0:0.000000}", pi.u));
                    mySeriesV.Points.AddXY(pi.x, String.Format("{0:0.000000}", pi.v));
                }
                //myArea.AxisY.StripLines[0].IntervalOffset = RiverSimulationProfile.profile.convergenceCriteria2d;

                //StripLine ls = myArea.AxisY.StripLines[0];
                //ls.IntervalOffset = RiverSimulationProfile.profile.convergenceCriteria2d;
                //ls.Text = String.Format("Mean, {0:0.000000}",  RiverSimulationProfile.profile.convergenceCriteria2d);


                string ds = "$" + pi.x.ToString() + ", " + pi.z.ToString() + ", " + pi.u.ToString() + ", " + pi.v.ToString() + "*";
                simDebugForm.AddResultMessage("\r\n" + ds);
                
            }
            catch
            {
                //if (pi.z < 0.0 || pi.u < 0.0 || pi.v < 0.0)
                {
                    MessageBox.Show("Stop");
                }
            }


        }
        private void ResetChart()
        {
            ChartArea myArea = chart1.ChartAreas["Base"];
            Series mySeriesD = chart1.Series["DepthD"];
            Series mySeriesU = chart1.Series["FlowU"];
            Series mySeriesV = chart1.Series["FlowV"];

            mySeriesD.SetDefault(true);
            mySeriesU.SetDefault(true);
            mySeriesV.SetDefault(true);
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
        private class ProgressItem
        {
            public double prog;
            public int x;
            public double z;
            public double u;
            public double v;
            public string msg;
            public enum Type
            {
                ShowConsole,
                ShowProgress,
                ShowChart
            };
            public Type t;
            public ProgressItem()
            {

            }

            public ProgressItem(ProgressItem p)
            {
                prog = p.prog;
                x = p.x;
                z = p.z;
                u = p.u;
                v = p.v;
                msg = p.msg;
                t = p.t;
            }
        }

        private bool inRunning = false;
        private void ParsingResult(string s)
        {
            ProgressItem pi = new ProgressItem();
            pi.msg = s;
            if (s.Contains("NUMYY="))
            {
                inRunning = true;
                string s2 = s.Replace("NUMYY=", " ").Replace("&ZMAX=", " ").Replace("&U3XX=", " ").Replace("&VMAX=", " ").Replace("&PROG=", " ");
                string[] separators = { " ", "\t" };
                string[] words = s2.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                pi.t = ProgressItem.Type.ShowProgress;
                pi.prog = Convert.ToDouble(words[5]);
                pi.x = Convert.ToInt32(words[0]);
                pi.z = Convert.ToDouble(words[2]);
                pi.u = Convert.ToDouble(words[3]);
                pi.v = Convert.ToDouble(words[4]);
                bw.ReportProgress(0, new ProgressItem(pi));
            }
            else if (!inRunning && s.Contains("1NUMXX="))
            {
                string s2 = s.Replace("1NUMXX=", " ").Replace("|ZMAX=", " ").Replace("|U1XX=", " ").Replace("|VMAX=", " ").Replace("!PROG=", " ");
                string[] separators = { " ", "\t" };
                string[] words = s2.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                pi.t = ProgressItem.Type.ShowChart;
                pi.prog = Convert.ToDouble(words[5]);
                pi.x = Convert.ToInt32(words[0]);
                pi.z = Convert.ToDouble(words[2]);
                pi.u = Convert.ToDouble(words[3]);
                pi.v = Convert.ToDouble(words[4]);
                bw.ReportProgress(0, new ProgressItem(pi));
            }
            else
            {
                pi.t = ProgressItem.Type.ShowConsole;
                bw.ReportProgress(0, new ProgressItem(pi));
            }

        }

        void BwDoWork(object sender, DoWorkEventArgs e)
        {
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += new ProgressChangedEventHandler(BwProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwRunWorkerCompleted);

            Process simMain = new Process();
            simMain.StartInfo.FileName = Program.currentPath + "\\0925.exe";
            simMain.StartInfo.WorkingDirectory = Program.currentPath;
            //resed2d 2dinput.i 123 3D 3dinput.dat out

            simMain.StartInfo.Arguments = simDebugForm.inputFile + " 123 3D " + simDebugForm.dataFile + " out";
            simMain.StartInfo.UseShellExecute = false;
            simMain.StartInfo.RedirectStandardOutput = true;
            simMain.StartInfo.CreateNoWindow = true;
            simMain.Start();
            inRunning = false;

            StreamReader reader = simMain.StandardOutput;//截取輸出流
            string line = reader.ReadLine();//每次讀取一行
            while (!reader.EndOfStream)
            {
                ParsingResult(line);
                line = reader.ReadLine();
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
                EnterSimUI(Status.Ready);
            }
        }

        private enum Status
        {
            Ready,
            Running,
            Pause,
        }

        private Status status = Status.Ready;
        private void startBtn_Click(object sender, EventArgs e)
        {
            if (status == Status.Ready)
            {
                //timer1.Enabled = true;
                //timer1.Start();
                //timer1.Interval = 100;
                //progressBar.Maximum = 1000;
                //timer1.Tick += new EventHandler(timer1_Tick);
                ResetChart();
                RunSimulationMain();
                EnterSimUI(Status.Running);
            }
            else if (status == Status.Running)
            {
                //timer1.Stop();
                EnterSimUI(Status.Pause);
            }
            else if (status == Status.Pause)
            {
                //timer1.Start();
                EnterSimUI(Status.Running);
            }
        }

        void EnterSimUI(Status s)
        {
            if(s==Status.Ready)
            {
                startBtn.Text = "開始模擬";
                startBtn.Enabled = true;
                stopBtn.Enabled = false;
                stopFlagChk.Enabled = true;
            }
            else if (s == Status.Running)
            {
                startBtn.Text = "暫停模擬";
                startBtn.Enabled = true;
                stopBtn.Enabled = true;
                stopFlagChk.Enabled = false;
            }
            else if(s == Status.Pause)
            {
                startBtn.Text = "繼續模擬";
                startBtn.Enabled = true;
                stopBtn.Enabled = true;
                stopFlagChk.Enabled = false;
            }
            status = s;
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (status == Status.Running || status == Status.Pause)
            {
                //timer1.Stop();
                //timer1.Enabled = false;
                //timer1.Interval = 100;
                progressBar.Value = 0;
                EnterSimUI(Status.Ready);
            }
        }

        private void tbResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void RunSimulationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            simDebugForm.Hide();
        }
    }
}

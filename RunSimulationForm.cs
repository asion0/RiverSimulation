using System;
using System.ComponentModel;
using System.Drawing;
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
            if (Program.programVersion.DemoVersion)
            {
                simDebugForm.Visible = false;
            }

            bw.DoWork += new DoWorkEventHandler(BwDoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(BwProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwRunWorkerCompleted);
            bw.WorkerReportsProgress = true;
            //InitialChart();
            this.progTxt.BackColor = Color.Transparent;
            //this.progTxt.Parent = progressBar;

            this.progressBar.Maximum = 10000;
        }

        private void ResetChart()
        {
            chart1.Series.SuspendUpdates();
            if(chart1.ChartAreas.Count != 0)
            { 
                chart1.Series["DepthD"].Dispose();
                chart1.Series["FlowU"].Dispose();
                chart1.Series["FlowV"].Dispose();
                chart1.ChartAreas["Base"].Dispose();

                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
            }
                InitialChart();
            chart1.Series.ResumeUpdates();
        }

        private void InitialChart()
        {
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

            myArea.AxisX.MajorGrid.LineColor = Color.Transparent; // X軸的刻度 縱線
            myArea.AxisY.MajorGrid.LineColor = Color.LightGray;  // Y軸的刻度 橫線
            myArea.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            myArea.AxisY.IsLabelAutoFit = false;

            myArea.AxisY.IsLogarithmic = true;
            myArea.AxisY.LogarithmBase = 10.0;
            myArea.AxisY.Minimum = 1.0E-6;
            myArea.AxisY.Maximum = 1000.0;

            myArea.AxisY.Title = "error";
            myArea.AxisX.Title = "疊代次數";

            myArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            myArea.AxisX.IntervalOffsetType = DateTimeIntervalType.Auto;
            myArea.AxisX.Minimum = 0.0;
            myArea.AxisX.Maximum = 500.0;

            // 設定平均值的 Line
            if (RiverSimulationProfile.profile.waterModelingConvergenceCriteria2d != 0.0)
            {
                StripLine lineMean = new StripLine();

                lineMean.Text = "收斂值 : " + RiverSimulationProfile.profile.waterModelingConvergenceCriteria2d.ToString();
                lineMean.BorderColor = Color.Red; // 線條的顏色
                lineMean.BorderDashStyle = ChartDashStyle.Dash;
                lineMean.BorderWidth = 1;
                lineMean.IntervalOffsetType = DateTimeIntervalType.Auto;
                //lineMean.Interval = RiverSimulationProfile.profile.convergenceCriteria2d;
                lineMean.IntervalOffset = Math.Log10(RiverSimulationProfile.profile.waterModelingConvergenceCriteria2d);
                myArea.AxisY.StripLines.Add(lineMean);
            }

           // mySeriesD.ChartType = SeriesChartType.Spline;        // 曲線圖
            mySeriesD.ChartType = SeriesChartType.Line;        // 曲線圖
            mySeriesD.Color = Color.Blue;               // 在圖型上的顏色
            mySeriesD.BorderWidth = 1;                   // 線型的寬度
            mySeriesD.ShadowColor = Color.Transparent;      // 陰影的顏色
            mySeriesD.ShadowOffset = 2;                  // 陰影位置的角度
            mySeriesD.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            mySeriesD.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊

            mySeriesU.ChartType = SeriesChartType.Spline;        // 曲線圖
            mySeriesU.Color = Color.Orange;               // 在圖型上的顏色
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
        }

        private double xStart = 0.0;
        private double xScale = 500.0;
        private void BwProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressItem pi = (ProgressItem)e.UserState;
            simDebugForm.AddResultMessage("\r\n" + pi.msg);

            if(pi.t == ProgressItem.Type.ShowConsole)
            {
                
                return;
            }

            this.progressBar.Value = (int)(pi.prog * 100);
            this.progTxt.Text = pi.prog.ToString() + "%";
            if (this.progressBar.Value == this.progressBar.Maximum)
            {
                MessageBox.Show("模擬完成");
            }
            if (pi.t == ProgressItem.Type.ShowProgress)
            {
                return;
            }

            if(pi.t == ProgressItem.Type.ShowDivergence)
            {
                MessageBox.Show("模式發散");
                return;
            }

            if(pi.x <= 1)
            {
                return;
            }
            ChartArea myArea = chart1.ChartAreas["Base"];
            //if( pi.x > (myArea.AxisX.Maximum * 0.95))
            //{
            //    myArea.AxisX.Maximum += myArea.AxisX.Maximum;
            //}

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

            chart1.Series.SuspendUpdates();
            if (chart1.Series["DepthD"].Points.Count == 500)
            {
                chart1.Series["DepthD"].Points.Clear();
                chart1.Series["FlowU"].Points.Clear();
                chart1.Series["FlowV"].Points.Clear();
                xStart += xScale;
                myArea.AxisX.Minimum = xStart;
                myArea.AxisX.Maximum = xStart + xScale;
            }

            chart1.Series["DepthD"].Points.AddXY(pi.x, String.Format("{0:0.000000}", pi.z));
            chart1.Series["FlowU"].Points.AddXY(pi.x, String.Format("{0:0.000000}", pi.u));
            chart1.Series["FlowV"].Points.AddXY(pi.x, String.Format("{0:0.000000}", pi.v));
            chart1.Series.ResumeUpdates();
            //myArea.AxisY.StripLines[0].IntervalOffset = RiverSimulationProfile.profile.convergenceCriteria2d;

            //StripLine ls = myArea.AxisY.StripLines[0];
            //ls.IntervalOffset = RiverSimulationProfile.profile.convergenceCriteria2d;
            //ls.Text = String.Format("Mean, {0:0.000000}",  RiverSimulationProfile.profile.convergenceCriteria2d);

            //string ds = "$" + pi.x.ToString() + ", " + pi.z.ToString() + ", " + pi.u.ToString() + ", " + pi.v.ToString() + "*";
            //simDebugForm.AddResultMessage("\r\n" + ds);
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
            EnterSimUI(Status.Ready);
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
                ShowChart,
                ShowDivergence
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

                const double MaxValue = 1000.0;
                pi.t = ProgressItem.Type.ShowChart;
                pi.prog = Convert.ToDouble(words[5]);
                pi.x = Convert.ToInt32(words[0]);
                try
                { pi.z = Convert.ToDouble(words[2]); }
                catch
                {  pi.z = MaxValue; }

                try
                { pi.u = Convert.ToDouble(words[3]); }
                catch
                { pi.u = MaxValue; }

                try
                { pi.v = Convert.ToDouble(words[4]); }
                catch
                { pi.v = MaxValue; }

                bw.ReportProgress(0, new ProgressItem(pi));
            }
            else if(s.Contains("DIVERGENCE"))
            {
                pi.t = ProgressItem.Type.ShowDivergence;
                bw.ReportProgress(0, new ProgressItem(pi));
            }
            else
            {
                pi.t = ProgressItem.Type.ShowConsole;
                bw.ReportProgress(0, new ProgressItem(pi));
            }
        }

        private Process simProcess = new Process();
        void BwDoWork(object sender, DoWorkEventArgs e)
        {
            if (simDebugForm.runMode == SimDebugForm.RunMode.OutputFile)
            {
                simProcess.StartInfo.FileName = Environment.CurrentDirectory + "\\DumpOutput.exe";
                simProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                simProcess.StartInfo.Arguments = simDebugForm.inputFile + " " + simDebugForm.dataFile;
                simProcess.StartInfo.UseShellExecute = false;
                simProcess.StartInfo.RedirectStandardOutput = true;
                simProcess.StartInfo.CreateNoWindow = true;
                simProcess.Start();
            }
            else if (simDebugForm.runMode == SimDebugForm.RunMode.InputData)
            {
                simProcess.StartInfo.FileName = Environment.CurrentDirectory + "\\10062.exe";
                simProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                simProcess.StartInfo.Arguments = simDebugForm.inputFile + " 123 3D " + simDebugForm.dataFile + " out";
                simProcess.StartInfo.UseShellExecute = false;
                simProcess.StartInfo.RedirectStandardOutput = true;
                simProcess.StartInfo.CreateNoWindow = true;
                simProcess.Start();
            }
            else if (simDebugForm.runMode == SimDebugForm.RunMode.ExternalFile)
            {
                simProcess.StartInfo.FileName = Program.currentPath + "\\" + simDebugForm.inputFile;
                simProcess.StartInfo.WorkingDirectory = Program.currentPath;
                simProcess.StartInfo.Arguments = simDebugForm.dataFile;
                simProcess.StartInfo.UseShellExecute = false;
                simProcess.StartInfo.RedirectStandardOutput = true;
                simProcess.StartInfo.CreateNoWindow = true;
                simProcess.Start();
            }
            else
            {
                string strInputFile = "IamReadyNow.i";
                RiverSimulationProfile.profile.GenerateInputFile(Environment.CurrentDirectory + "\\" + strInputFile);
                simProcess.StartInfo.FileName = Environment.CurrentDirectory + "\\10062.exe";
                simProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                simProcess.StartInfo.Arguments = strInputFile + " 123 3D " + simDebugForm.dataFile + " out";
                simProcess.StartInfo.UseShellExecute = false;
                simProcess.StartInfo.RedirectStandardOutput = true;
                simProcess.StartInfo.CreateNoWindow = true;
                simProcess.Start();
            }
            inRunning = false;
            StreamReader reader = simProcess.StandardOutput;//截取輸出流
            string line = reader.ReadLine();//每次讀取一行
            while (!reader.EndOfStream)
            {
                ParsingResult(line);
                line = reader.ReadLine();
            }
            ParsingResult(line);
            simProcess.WaitForExit();
        }

        private bool RunSimulationMain()
        {
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync(0);
            return true;
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
                ResetChart();
                RunSimulationMain();
                EnterSimUI(Status.Running);
            }
            else if (status == Status.Running)
            {
                if (!simProcess.HasExited)
                {
                    Utility.Suspend(simProcess);
                }
                //timer1.Stop();
                EnterSimUI(Status.Pause);
            }
            else if (status == Status.Pause)
            {
                if (!simProcess.HasExited)
                {
                    Utility.Resume(simProcess);
                }
                EnterSimUI(Status.Running);
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (status == Status.Running || status == Status.Pause)
            {
                if (!simProcess.HasExited)
                {
                    simProcess.Kill();
                }
                progressBar.Value = 0;
                EnterSimUI(Status.Ready);
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

        private void RunSimulationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            simDebugForm.Hide();
        }
    }

    public static class ProcessExtension
    {

    }
}

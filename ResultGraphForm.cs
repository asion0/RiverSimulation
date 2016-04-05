using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Threading;
using Utilities.GnuplotCSharp;

namespace RiverSimulationApplication
{
    public partial class ResultGraphForm : ResultUI
    {
        public ResultGraphForm()
        {
            InitializeComponent();
        }
        //U-同參數單位 D-累距
        /* I|J|T|K|X|Y|Sel1|Sel2|Mode| Data  |
         * -+-+-+-+-+-+----+----+----+-------+
         * 1|M|-|-|D|U| I  | -  | 0  |2D IJ  |
         * -+-+-+-+-+-+----+----+----+-------+
         * M|1|-|-|D|U| J  | -  | 1  |2D IJ  |
         * -+-+-+-+-+-+----+----+----+-------+
         * 1|M|1|-|D|U| I  | -  | 2  |3D IJT |
         * -+-+-+-+-+-+----+----+----+-------+
         * M|1|1|-|D|U| J  | -  | 3  |3D IJT |
         * -+-+-+-+-+-+----+----+----+-------+
         * 1|1|M|-|T|U| J  | -  | 4  |3D IJT |<<<<<NOW
         * -+-+-+-+-+-+----+----+----+-------+
         * M|M|1|1|I|J| K  | T  | 5  |4D IJTK|
         * -+-+-+-+-+-+----+----+----+-------+
         * 1|M|1|M|K|J| I  | T  | 6  |4D IJTK|
         * -+-+-+-+-+-+----+----+----+-------+
         * M|1|1|M|K|J| J  | T  | 7  |4D IJTK|
         * -+-+-+-+-+-+----+----+----+-------+
         * 1|M|M|1|T|J| I  | K  | 8  |4D IJTK|
         * -+-+-+-+-+-+----+----+----+-------+
         * M|1|M|1|T|I| J  | K  | 9  |4D IJTK|
         * -+-+-+-+-+-+----+----+----+-------+
         * 1|1|M|M|T|K| T  | K  | A  |4D IJTK|
         * -+-+-+-+-+-+----+----+----+-------+
        */
        public enum GraphType
        {
            //XY Form
            Type0,
            Type1,
            Type2,
            Type3,
            Type4,
            Type5,
            Type6,
            Type7,
            Type8,
            Type01,
            Type234,
            Type5678,
            TypeUnknown,
        };

        public enum ResultGraphType
        {
            InitialBottomElevation, //初始底床高程(m)
            SingleIMultipleJSingleTime,
            MultipleISingleJSingleTime,
            SingleISingleJMultipleTime,

            GenericDouble,                          //初始一般用途，所有實數
            GenericDoubleGreaterThanZero,           //初始一般用途，大於零
            GenericDoubleGreaterThanOrEqualZero,    //初始一般用途，大於等於零
            TwoInOneDouble,                          //TwoInOne一般用途，所有實數
            TwoInOneDoubleGreaterThanZero,           //TwoInOne一般用途，大於零
            TwoInOneDoubleGreaterThanOrEqualZero,    //TwoInOne一般用途，大於等於零
            SeabedThicknessForm,            //底床分層厚度輸入
            SedimentCompositionRatioForm,   //泥砂組成比例輸入
            SeparateForm,                   //垂直向隔網分層比例輸入
            VerticalVelocityDistributionForm,   //垂直流速分布(3D)
            BottomElevationForm,            //編輯底床高程
            //FlowQuantity,                   //流量設定
            FlowConditionsSettingConstant,   //流況設定定量流
            FlowConditionsSettingVariable,  //流況設定變量流
            BoundaryTime,                   //邊界時間輸入
            VerticalDistribution,           //垂直濃度分布
            BottomBedParticleSizeRatio,     //底床粒徑比
        }
        protected ResultGraphType formType = ResultGraphType.InitialBottomElevation;

        RiverSimulationProfile p = RiverSimulationProfile.profile;
        private double max, min;

        public void SetFormMode(
            string title,
            int colStart, int colEnd,
            int rowStart, int rowEnd,
            string tableName = "",
            string colName = "",
            string rowName = "",
            ResultGraphType formType = ResultGraphType.InitialBottomElevation,
            int colWidth = 48,
            int rowHeadersWidth = 64,
            bool onlyTable = true,
            bool nocolNum = false,
            bool noRowNum = false,
            object initData = null,
            int xDim = 0,
            int yDim = 1,
            int sel1Dim = -1,
            int sel2Dim = -1,
            int sel1Index = -1,
            int sel2Index = -1,
            string sel1Title = "",
            string sel2Title = "",
            double[] timeList = null)
        {
            this.title = title;
            this.formType = formType;
            this.colStart = colStart;
            this.colEnd = colEnd;
            this.rowStart = rowStart;
            this.rowEnd = rowEnd;
            this.tableName = tableName;
            this.colName = colName;
            this.rowName = rowName;
            this.colWidth = colWidth;
            this.rowHeadersWidth = rowHeadersWidth;
            this.initData = initData;
            this.xDim = xDim;
            this.yDim = yDim;
            this.sel1Dim = sel1Dim;
            this.sel2Dim = sel2Dim;
            this.sel1Title = sel1Title;
            this.sel2Title = sel2Title;
            this.sel1Index = sel1Index;
            this.sel2Index = sel2Index;
            this.timeList = timeList;
        }

        private void ResultGraphForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            InitializeChartView();
        }

        private double CumulativeIDistance(int i, int j, PictureBoxCtrl.CoorPoint[,] inputCoor)
        {
            return (i > 0) ? Math.Abs(inputCoor[i, j].y - inputCoor[i - 1, j].y) : 0;
        }

        private double CumulativeJDistance(int i, int j, PictureBoxCtrl.CoorPoint[,] inputCoor)
        {   //inputCoor[95, 19] I, J
            return (j > 0) ? Math.Abs(inputCoor[i, j].x - inputCoor[i, j - 1].x) : 0;
        }

        private string DrawMode0XY(int i, int jS, int jE)
        {
            // Mode 0 status : 1-Single M-Multiple D-壘距 U-資料單位
            // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data   |
            // 1|M|-|-|D|U| I  | -  | 0  |3D IJ1  |
            string graphicsPath = Program.GetProjectFileWorkingPath() + "\\Graphics\\XYMode0";
            string pngFile = string.Format("{0}\\{1}_{2}_{3}.png", graphicsPath, i, jS, jE).Replace('\\', '/');
            if (File.Exists(pngFile))
                return pngFile;

            double distance = 0.0;
            if (jS != 0)
            {   //累距不從0開始
                for (int x = 0; x < jS; ++x)
                {
                    distance += CumulativeJDistance(i, x, p.inputGrid.inputCoor);
                }
            }
            //產生gunplot輸入檔內容
            StringBuilder sb = new StringBuilder();
            for (int x = jS; x < jE; ++x)
            {
                distance += CumulativeJDistance(i, x, p.inputGrid.inputCoor);
                sb.AppendFormat("{0,15} ", distance.ToString("F7"));
                sb.AppendFormat("{0,15} ", ((initData as double[, ,])[i, x, 0]).ToString("F7"));
                sb.AppendFormat("\n");
            }

            string plotfile = string.Format("{0}{1}{2}.txt", Program.GetProjectFileWorkingPath(), "\\g", i).Replace('\\', '/');
            Directory.CreateDirectory(graphicsPath);
            using (StreamWriter outfile = new StreamWriter(plotfile))
            {
                outfile.Write(sb.ToString());
                outfile.Close();
            }

            //Utility.DeleteFileOrFolder(pngFile);
            string setPngFile = string.Format("output \"{0}\"", pngFile);
            //*
            string[] setting = {
                                   "grid",
                                   "term png size 960,630",
                                   setPngFile,
                                   "title ''",
                                   "ylabel '(m)'",
                                   "xlabel '(m)'",
                                   "format x \"%.3f\"",
                                   "format y \"%.3f\"",
                                   "style data lines",
                                   "nokey" };
            GnuPlot.Set(setting);
            //plotfile = String.Format("\"{0}\"", plotfile);
            GnuPlot.Plot(plotfile, @"with lines");
            //GnuPlot.Set("term x11");
            //GnuPlot.WriteLine();
            GnuPlot.Set("output");
            return pngFile;
        }
        private string DrawMode1XY(int j, int iS, int iE)
        {
            // Mode 1 status : 1-Single M-Multiple D-壘距 U-資料單位
            // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data   |
            // M|1|-|-|D|U| J  | -  | 1  |3D IJ1  |
            string graphicsPath = Program.GetProjectFileWorkingPath() + "\\Graphics\\XYMode1";
            string pngFile = string.Format("{0}\\{1}_{2}_{3}.png", graphicsPath, j, iS, iE).Replace('\\', '/');
            if (File.Exists(pngFile))
                return pngFile;

            double distance = 0.0;
            if (iS != 0)
            {   //累距不從0開始
                for (int y = 0; y < iS; ++y)
                {
                    distance += CumulativeIDistance(y, j, p.inputGrid.inputCoor);
                }
            }
            //產生gunplot輸入檔內容
            StringBuilder sb = new StringBuilder();
            for (int y = iS; y < iE; ++y)
            {
                distance += CumulativeIDistance(y, j, p.inputGrid.inputCoor);
                sb.AppendFormat("{0,15} ", distance.ToString("F7"));
                sb.AppendFormat("{0,15} ", ((initData as double[, ,])[y, j, 0]).ToString("F7"));
                sb.AppendFormat("\n");
            }

            string plotfile = string.Format("{0}\\{1}_{2}_{3}.txt", graphicsPath, j, iS, iE).Replace('\\', '/');
            Directory.CreateDirectory(graphicsPath);
            using (StreamWriter outfile = new StreamWriter(plotfile))
            {
                outfile.Write(sb.ToString());
                outfile.Close();
            }

            //Utility.DeleteFileOrFolder(pngFile);
            string setPngFile = string.Format("output \"{0}\"", pngFile);
            //*
            string[] setting = {
                                   "grid",
                                   "term png size 960,630",
                                   setPngFile,
                                   "title ''",
                                   "ylabel '(m)'",
                                   "xlabel '(m)'",
                                   "format x \"%.3f\"",
                                   "format y \"%.3f\"",
                                   "style data lines",
                                   "nokey" };
            GnuPlot.Set(setting);
            //plotfile = String.Format("\"{0}\"", plotfile);
            GnuPlot.Plot(plotfile, @"with lines");
            //GnuPlot.Set("term x11");
            //GnuPlot.WriteLine();
            GnuPlot.Set("output");
            return pngFile;
        }

        public static int CumulativeDistance = -3;
        public static int DataContent = -2;
        private GraphType graphType = GraphType.TypeUnknown;
        private void InitializeChartView()
        {

            if (sel1Index != -1 && sel2Index == -1 && xDim == CumulativeDistance && yDim == DataContent && (rowEnd - rowStart == 1))
            {
                // Mode0 I固定 J範圍 選項一是I X顯示累距 Y顯示資料
                // Mode 0 status : 1-Single M-Multiple D-壘距 U-資料單位
                // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data   |
                // 1|M|-|-|D|U| I  | -  | 0  |3D IJ1  |                    graphType = GraphType.Type0;
                graphType = GraphType.Type0;
                sel1Lbl.Visible = true;
                sel1Lbl.Text = sel1Title;
                combo1.Visible = true;
                for (int i = 0; i < (initData as double[, ,]).GetLength(0); ++i)
                {
                    combo1.Items.Add((i + 1).ToString());
                    DrawMode0XY(i, colStart, colEnd);
                }
                combo1.SelectedIndex = rowStart;
                //string gfile = DrawMode0XY(rowStart, colStart, colEnd);
            }
            else if (sel1Index != -1 && sel2Index == -1 && xDim == CumulativeDistance && yDim == DataContent && (colEnd - colStart == 1))
            {
                // Mode1 I範圍 J固定 選項一是J X顯示累距 Y顯示資料
                // Mode 1 status : 1-Single M-Multiple D-壘距 U-資料單位
                // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data  |
                // M|1|-|-|D|U| J  | -  | 1  |2D IJ  |
                graphType = GraphType.Type1;
                sel1Lbl.Visible = true;
                sel1Lbl.Text = sel1Title;
                combo1.Visible = true;
                for (int j = 0; j < (initData as double[, ,]).GetLength(1); ++j)
                {
                    combo1.Items.Add((j + 1).ToString());
                    DrawMode1XY(j, rowStart, rowEnd);
                }
                combo1.SelectedIndex = rowStart;
                //string gfile = DrawMode0XY(rowStart, colStart, colEnd);
            }
            //else if (sel1Index != -1 && sel2Index == -1 && xDim == 1 && yDim == 3)       //3D data X顯示J Y顯示T
            //    dataGridView[x - colStart, y - rowStart].Value = (initData as double[, ,])[sel1Index, x, y].ToString();
            //else if (sel1Index != -1 && sel2Index == -1 && xDim == 0 && yDim == 3)       //3D data X顯示I Y顯示T
            //    dataGridView[x - colStart, y - rowStart].Value = (initData as double[, ,])[x, sel1Index, y].ToString();




        }

        private void InitialChart()
        {
            chart1.ChartAreas.Add("Base");
            chart1.Series.Add("Param1");

            ChartArea myArea = chart1.ChartAreas["Base"];
            Series mySeriesD = chart1.Series["Param1"];
            mySeriesD.LegendText = title;

            myArea.AxisX.MajorGrid.LineColor = Color.Transparent; // X軸的刻度 縱線
            myArea.AxisY.MajorGrid.LineColor = Color.LightGray;  // Y軸的刻度 橫線
            myArea.AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
            myArea.AxisY.IntervalOffsetType = DateTimeIntervalType.Auto;
            myArea.AxisY.IsLabelAutoFit = true;
            myArea.AxisY.IsLogarithmic = false;

            double gap = (max - min) / 8;
            if (gap == 0)
                gap = 1.0;
            myArea.AxisY.Minimum = min - gap;
            myArea.AxisY.Maximum = max + gap;

            myArea.AxisY.Title = title;

            if (formType == ResultGraphType.InitialBottomElevation || formType == ResultGraphType.SingleIMultipleJSingleTime
                || formType == ResultGraphType.MultipleISingleJSingleTime)
            {
                myArea.AxisX.Title = "累距(m)";
                myArea.AxisX.Minimum = 0.0;
            }
            else
            {
                myArea.AxisX.Title = "時間(s)";
                myArea.AxisX.Minimum = 0.0;
            }

            myArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            myArea.AxisX.IntervalOffsetType = DateTimeIntervalType.Auto;
            //myArea.AxisX.Minimum = 0.0;
            //myArea.AxisX.Maximum = 500.0;

            // 設定平均值的 Line
            //if (RiverSimulationProfile.profile.waterModelingConvergenceCriteria2d != 0.0)
            //{
            //    StripLine lineMean = new StripLine();

            //    lineMean.Text = "收斂值 : " + RiverSimulationProfile.profile.waterModelingConvergenceCriteria2d.ToString();
            //    lineMean.BorderColor = Color.Red; // 線條的顏色
            //    lineMean.BorderDashStyle = ChartDashStyle.Dash;
            //    lineMean.BorderWidth = 1;
            //    lineMean.IntervalOffsetType = DateTimeIntervalType.Auto;
            //    //lineMean.Interval = RiverSimulationProfile.profile.convergenceCriteria2d;
            //    lineMean.IntervalOffset = Math.Log10(RiverSimulationProfile.profile.waterModelingConvergenceCriteria2d);
            //    myArea.AxisY.StripLines.Add(lineMean);
            //}

            // mySeriesD.ChartType = SeriesChartType.Spline;        // 曲線圖
            mySeriesD.ChartType = SeriesChartType.Line;        // 曲線圖
            mySeriesD.Color = Color.Blue;               // 在圖型上的顏色
            mySeriesD.BorderWidth = 1;                   // 線型的寬度
            mySeriesD.ShadowColor = Color.Transparent;      // 陰影的顏色
            mySeriesD.ShadowOffset = 2;                  // 陰影位置的角度
            mySeriesD.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            mySeriesD.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊

            //mySeriesU.ChartType = SeriesChartType.Spline;        // 曲線圖
            //mySeriesU.Color = Color.Orange;               // 在圖型上的顏色
            //mySeriesU.BorderWidth = 1;                   // 線型的寬度
            //mySeriesU.ShadowColor = Color.Transparent;      // 陰影的顏色
            //mySeriesU.ShadowOffset = 2;                  // 陰影位置的角度
            //mySeriesU.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            //mySeriesU.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊

            //mySeriesV.ChartType = SeriesChartType.Spline;        // 曲線圖
            //mySeriesV.Color = Color.Green;               // 在圖型上的顏色
            //mySeriesV.BorderWidth = 1;                   // 線型的寬度
            //mySeriesV.ShadowColor = Color.Transparent;      // 陰影的顏色
            //mySeriesV.ShadowOffset = 2;                  // 陰影位置的角度
            //mySeriesV.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            //mySeriesV.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊
        }

        private void DrawChart()
        {
            /*
            double m = 0.0;
            chart1.Series.SuspendUpdates();
            switch (formType)
            {
                case ResultGraphType.InitialBottomElevation:
                    //編輯陣列I * J
                    if (dt == DrawType.DrawI)
                    {
                        for (int j = rowStart; j < rowEnd; ++j)
                        {
                            chart1.Series["Param1"].Points.AddXY(m.ToString("F3"), (initData as double[,])[comboSelectData, j]);
                            if (j < p.inputGrid.GetI - 1)
                            {
                                m += Math.Abs(p.inputGrid.inputCoor[j, comboSelectData].x - p.inputGrid.inputCoor[j + 1, comboSelectData].x);
                            }
                        }
                    }
                    else
                    {
                        for (int i = colStart; i < colEnd; ++i)
                        {
                            chart1.Series["Param1"].Points.AddXY(m.ToString("F3"), (initData as double[,])[i, comboSelectData]);
                            if (i < p.inputGrid.GetJ - 1)
                            {
                                m += Math.Abs(p.inputGrid.inputCoor[comboSelectData, i].y - p.inputGrid.inputCoor[comboSelectData, i + 1].y);
                            }
                        }
                    }
                    break;
                case ResultGraphType.SingleISingleJMultipleTime:
                    for (int t = 0; t < timeSel.Length; ++t)
                    {
                        int i = colStart;
                        int j = rowStart;
                        chart1.Series["Param1"].Points.AddXY(timeList[timeSel[t]], (initData as double[, ,])[timeSel[t], i, j]);
                    }
                    break;
                case ResultGraphType.SingleIMultipleJSingleTime:
                    for (int i = colStart; i < colEnd; ++i)
                    {
                        int t = timeSel[0];
                        chart1.Series["Param1"].Points.AddXY(m.ToString("F3"), (initData as double[, ,])[t, i, comboSelectData]);
                        if (i < p.inputGrid.GetJ - 1)
                        {
                            m += Math.Abs(p.inputGrid.inputCoor[comboSelectData, i].y - p.inputGrid.inputCoor[comboSelectData, i + 1].y);
                        }
                    }
                    break;
                case ResultGraphType.MultipleISingleJSingleTime:
                    for (int j = rowStart; j < rowEnd; ++j)
                    {
                        int t = timeSel[0];
                        chart1.Series["Param1"].Points.AddXY(m.ToString("F3"), (initData as double[, ,])[t, comboSelectData, j]);
                        if (j < p.inputGrid.GetI - 1)
                        {
                            m += Math.Abs(p.inputGrid.inputCoor[j, comboSelectData].x - p.inputGrid.inputCoor[j + 1, comboSelectData].x);
                        }
                    }
                    break;
            }
            chart1.Series.ResumeUpdates();
            */
        }

        private void CalcMinMax()
        {
            /*
            if (ResultGraphType.InitialBottomElevation == formType)
            {   
                if (colEnd - colStart == 1)
                {
                    GetIMaxMin(comboSelectData);
                }
                if (rowEnd - rowStart == 1)
                {
                    GetJMaxMin(comboSelectData);
                }
            }
            else if (formType == ResultGraphType.SingleISingleJMultipleTime)
            {
                GetTMaxMin(colStart, rowStart);
            }
            else if (formType == ResultGraphType.SingleIMultipleJSingleTime)
            {
                GetJMaxMin(comboSelectData);
            }
            else if (formType == ResultGraphType.MultipleISingleJSingleTime)
            {
                GetIMaxMin(comboSelectData);
            }
            */
        }

        //private int comboSelectData = -1;
        private void combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = (sender as ComboBox);
            int sel = Convert.ToInt32(c.Items[c.SelectedIndex]) - 1;
            String s = "";
            if (graphType == GraphType.Type0)
            {
                s = DrawMode0XY(sel, colStart, colEnd);
            }
            else if (graphType == GraphType.Type1)
            {
                s = DrawMode1XY(sel, rowStart, rowEnd);
            }
            pic1.Image = Image.FromFile(s);

            /*
            CalcMinMax();
            chart1.Series.SuspendUpdates();
            if (chart1.ChartAreas.Count != 0)
            {
                chart1.Series["Param1"].Dispose();
                chart1.ChartAreas["Base"].Dispose();

                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
            } 
            InitialChart();
            DrawChart();
            */
        }
    }
}

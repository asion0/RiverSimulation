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
using Utilities.GnuplotCSharp;

namespace RiverSimulationApplication
{
    public partial class ResultGraphForm : ResultUI
    {
        public ResultGraphForm()
        {
            InitializeComponent();
        }

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
        private double CumulativeJDistance(int i, int j, PictureBoxCtrl.CoorPoint[,] inputCoor)
        {
            /*
            double d0, d1, d = 0.0;
            d0 = (j == 0) ? 0 : Math.Sqrt(
                Math.Pow(inputCoor[i, j].x - inputCoor[i, j - 1].x, 2) +
                Math.Pow(inputCoor[i, j].y - inputCoor[i, j - 1].y, 2));
            d1 = (j == inputCoor.GetLength(1) - 1) ? 0 : Math.Sqrt(
                Math.Pow(inputCoor[i, j].x - inputCoor[i, j + 1].x, 2) +
                Math.Pow(inputCoor[i, j].y - inputCoor[i, j + 1].y, 2));
            d += d0 / 2 + d1 / 2;
            */
            return Math.Abs(inputCoor[j, i].y - inputCoor[j, i + 1].y);
        }

        private void InitializeChartView()
        {
            StringBuilder sb = new StringBuilder();

            //標題列，代表Sel1索引
            //sb.AppendFormat("{0,8} ", 0);
            //for (int x = colStart; x < colEnd; ++x)
            //{
            //    sb.AppendFormat("{0,8} ", x + 1);
            //}
            //sb.AppendFormat("{0,8} {1,8}\n", 1, 2);

            // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data  |
            // 1|M|-|-|D|U| I  | -  | 0  |2D IJ  |
            double distance = 0.0;
            if(colStart != 0)
            {   //累距不從0開始
                for (int x = 0; x < colStart; ++x)
                {
                    distance += CumulativeJDistance(rowStart, x, p.inputGrid.inputCoor);
                }
            }

            for (int x = colStart; x < colEnd; ++x)
            {
                distance += CumulativeJDistance(rowStart, x, p.inputGrid.inputCoor);
                sb.AppendFormat("{0,8} ", distance);
                for (int y = rowStart; y < rowEnd; ++y)
                {
                    sb.AppendFormat("{0,8} ", (initData as double[,,])[y, x, sel1Index]);
                }
                sb.AppendFormat("\n");
            }
            using (StreamWriter outfile = new StreamWriter(@"g:/_test.txt"))
            {
                outfile.Write(sb.ToString());
                outfile.Close();
            }
            //*
            string[] setting = {
                               "grid",
                               //"terminal png 0",
                               "term pngcairo font \"AR PL UKai TW\"",
                               "term png font \"g:/mingliu.ttc,12\" size 800,600",
                               "output \"G:/test.png\"",
                               "title '底床高程'",
                               "ylabel  '(微秒)'",
                               "xlabel  \"日期\"",
                               "format  y \"%.0f\"",
                               "style data lines",
                };
            GnuPlot.Set(setting);
            GnuPlot.Plot(@"G:/_test.txt", @"with lines");
            GnuPlot.Set("output");
            //*/
            /*
            string[] setting = {
                               "key tmargin",
                               "tics nomirror",
                               "set border 3",
                               "set style data lines",
                               "set terminal pngcairo notransparent font \"Helvetica, 12\"",
                               "terminal png size 800,600",
                               "output \"G:/test.png\"",
                               "style data lines",
                };
            GnuPlot.Set(setting);
            GnuPlot.Plot(@"G:/_test.txt", @"using 1 title '1'");
            */

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

        private int comboSelectData = -1;
        private void combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = (sender as ComboBox);
            comboSelectData = Convert.ToInt32(c.Items[c.SelectedIndex]) - 1;
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
        }
    }
}

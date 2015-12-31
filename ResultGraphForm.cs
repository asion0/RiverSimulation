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
using System.Collections.Generic;

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

        protected enum DrawType
        {
            None,
            DrawI,
            DrawJ,
            DrawIJ
        };
        DrawType dt = DrawType.None;
        DrawType CheckDT(int colStart, int colEnd, int rowStart, int rowEnd)
        {
            DrawType tempDt = DrawType.None;
            if (formType == ResultGraphType.InitialBottomElevation)
            {
                if (colEnd - colStart == 1)
                {
                    tempDt = DrawType.DrawI;
                    comboSelectData = colStart;
                    comboText.Text = "請選取I：";
                }
                if (rowEnd - rowStart == 1)
                {
                    tempDt = DrawType.DrawJ;
                    comboSelectData = rowStart;
                    comboText.Text = "請選取J：";
                }
            }
            else if(formType == ResultGraphType.MultipleISingleJSingleTime)
            {
                //if (colEnd - colStart == 1)
                {
                    tempDt = DrawType.DrawI;
                    comboSelectData = colStart;
                    comboText.Text = "請選取I：";
                }
            }
            else if (formType == ResultGraphType.SingleIMultipleJSingleTime)
            {
                //if (rowEnd - rowStart == 1)
                {
                    tempDt = DrawType.DrawJ;
                    comboSelectData = rowStart;
                    comboText.Text = "請選取J：";
                }
            }
            else if (formType == ResultGraphType.SingleISingleJMultipleTime)
            {
                //if (timeSel != null && timeSel.Length == 1)
                {
                    tempDt = DrawType.DrawIJ;
                    comboSelectData = -1;
                    comboText.Text = "";
                    combo1.Enabled = false;
                }
            }
            return tempDt;
        }

        RiverSimulationProfile p = RiverSimulationProfile.profile;
        private double max, min;
        public void SetFormMode(string title,
            int colStart,
            int colEnd,
            int rowStart,
            int rowEnd,
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
            object timeList = null,
            int[] timeSel = null)
        {
            this.title = title;
            this.formType = formType;
            this.colStart = colStart;
            this.colEnd = colEnd;
            this.rowStart = rowStart;
            this.rowEnd = rowEnd;
            this.colWidth = colWidth;
            this.rowHeadersWidth = rowHeadersWidth;
            this.initData = initData;
            this.timeList = (List<double>)timeList;
            this.timeSel = timeSel;

            dt = CheckDT(colStart, colEnd, rowStart, rowEnd);

            CalcMinMax();
        }

        private void GetIMaxMin(int j)
        {
            
            max = double.MinValue;
            min = double.MaxValue;
            if (formType == ResultGraphType.InitialBottomElevation)
            {
                for (int i = rowStart; i < rowEnd; ++i)
                {
                    if (min > (initData as double[,])[j, i])
                    {
                        min = (initData as double[,])[j, i];
                    }
                    if (max < (initData as double[,])[j, i])
                    {
                        max = (initData as double[,])[j, i];
                    }
                }
            }
            else
            {
                int t = timeSel[0];
                for (int i = rowStart; i < rowEnd; ++i)
                {
                    if (min > (initData as double[, ,])[t, j, i])
                    {
                        min = (initData as double[, ,])[t, j, i];
                    }
                    if (max < (initData as double[, ,])[t, j, i])
                    {
                        max = (initData as double[, ,])[t, j, i];
                    }
                }
            }
        }

        private void GetJMaxMin(int i)
        {
            max = double.MinValue;
            min = double.MaxValue;
            if (formType == ResultGraphType.InitialBottomElevation)
            {
                for (int j = colStart; j < colEnd; ++j)
                {
                    if (min > (initData as double[,])[j, i])
                    {
                        min = (initData as double[,])[j, i];
                    }
                    if (max < (initData as double[,])[j, i])
                    {
                        max = (initData as double[,])[j, i];
                    }
                }
            }
            else
            {
                int t = timeSel[0];
                for (int j = colStart; j < colEnd; ++j)
                {
                    if (min > (initData as double[, ,])[t, j, i])
                    {
                        min = (initData as double[, ,])[t, j, i];
                    }
                    if (max < (initData as double[, ,])[t, j, i])
                    {
                        max = (initData as double[, ,])[t, j, i];
                    }
                }
            }
        }

        private void GetTMaxMin(int j, int i)
        {
            max = double.MinValue;
            min = double.MaxValue;
            for (int t = 0; t < timeSel.Length; ++t)
            {
                if (min > (initData as double[, ,])[timeSel[t], j, i])
                {
                    min = (initData as double[, ,])[timeSel[t], j, i];
                }
                if (max < (initData as double[, ,])[timeSel[t], j, i])
                {
                    max = (initData as double[, ,])[timeSel[t], j, i];
                }
            }
     
        }

        private void ResultGraphForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            InitializeChartView();
        }

        private void InitializeChartView()
        {
            if (formType == ResultGraphType.InitialBottomElevation)
            {
               if (dt == DrawType.DrawI)
                {   //繪製河道縱面 J可選
                    for (int j = 0; j < (initData as double[,]).GetLength(0); ++j)
                    {
                        combo1.Items.Add((j + 1).ToString());
                    }
                    combo1.SelectedIndex = colStart;
                }
                if (dt == DrawType.DrawJ)
                {   //繪製河道剖面 I可選
                    for (int i = 0; i < (initData as double[,]).GetLength(1); ++i)
                    {
                        combo1.Items.Add((i + 1).ToString());
                    }
                    combo1.SelectedIndex = rowStart;
                }

            }
            else
            {
                if (dt == DrawType.DrawI)
                {   //繪製河道縱面 J可選
                    for (int j = 0; j < (initData as double[,,]).GetLength(1); ++j)
                    {
                        combo1.Items.Add((j + 1).ToString());
                    }
                    combo1.SelectedIndex = colStart;
                }
                else if (dt == DrawType.DrawJ)
                {   //繪製河道剖面 I可選
                    for (int i = 0; i < (initData as double[,,]).GetLength(2); ++i)
                    {
                        combo1.Items.Add((i + 1).ToString());
                    }
                    combo1.SelectedIndex = rowStart;
                }
                else
                {   //繪製一點I,J時間變化 無選取
                    InitialChart();
                    DrawChart();
                }
            }
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
        }

        private void CalcMinMax()
        {
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

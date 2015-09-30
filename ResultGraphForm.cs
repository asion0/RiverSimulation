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
            DrawJ
        };
        DrawType dt = DrawType.None;
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
            object initData = null)
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

            if(colEnd - colStart == 1)
            {
                dt = DrawType.DrawI;
                GetIMaxMin();
            }
            if (rowEnd - rowStart == 1)
            {
                dt = DrawType.DrawJ;
                GetJMaxMin();
            }
        }

        private void GetIMaxMin()
        {
            max = double.MinValue;
            min = double.MaxValue;
            for (int i = rowStart; i < rowEnd; ++i)
            {
                if(min > (initData as double[,])[colStart, i])
                {
                    min = (initData as double[,])[colStart, i];
                }
                if (max < (initData as double[,])[colStart, i])
                {
                    max = (initData as double[,])[colStart, i];
                }
            }
        }

        private void GetJMaxMin()
        {
            max = double.MinValue;
            min = double.MaxValue;
            for (int j = colStart; j < colEnd; ++j)
            {
                if (min > (initData as double[,])[j, rowStart])
                {
                    min = (initData as double[,])[j, rowStart];
                }
                if (max < (initData as double[,])[j, rowStart])
                {
                    max = (initData as double[,])[j, rowStart];
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
                InitialChart();
                //DataGridViewUtility.InitializeDataGridView2(chart1, colStart, colEnd, rowStart, rowEnd, colWidth, rowHeadersWidth,
                //    "", "", "", false, false, false, false);
                DrawChart();
            }
        }

        private void InitialChart()
        {
            chart1.ChartAreas.Add("Base");
            chart1.Series.Add("Param1");

            ChartArea myArea = chart1.ChartAreas["Base"];
            Series mySeriesD = chart1.Series["Param1"];

            mySeriesD.LegendText = "初始底床高程(m)";


            myArea.AxisX.MajorGrid.LineColor = Color.Transparent; // X軸的刻度 縱線
            myArea.AxisY.MajorGrid.LineColor = Color.LightGray;  // Y軸的刻度 橫線
            myArea.AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
            myArea.AxisY.IntervalOffsetType = DateTimeIntervalType.Auto;
            myArea.AxisY.IsLabelAutoFit = true;
            myArea.AxisY.IsLogarithmic = false;

            double gap = (max - min) / 8;
            myArea.AxisY.Minimum = min - gap;
            myArea.AxisY.Maximum = max + gap;

            myArea.AxisY.Title = "初始底床高程(m)";
            myArea.AxisX.Title = "累距(m)";

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
            switch (formType)
            {
                case ResultGraphType.InitialBottomElevation:
                    //編輯陣列I * J
                    chart1.Series.SuspendUpdates();
                    double m = 0.0;
                    if (dt == DrawType.DrawI)
                    {
                        for (int j = rowStart; j < rowEnd; ++j)
                        {
                            //dataGridView[i - colStart, j - rowStart].Value = (initData as double[,])[i, j].ToString();
                            double dd = (initData as double[,])[colStart, j];
                            chart1.Series["Param1"].Points.AddXY(m.ToString("F3"), (initData as double[,])[colStart, j]);
                            if (j < p.inputGrid.GetI - 1)
                            {
                                m += Math.Abs(p.inputGrid.inputCoor[j, colStart].x - p.inputGrid.inputCoor[j + 1, colStart].x);
                            }
                        }
                    }
                    else
                    {
                        for (int i = colStart; i < colEnd; ++i)
                        {
                            //dataGridView[i - colStart, j - rowStart].Value = (initData as double[,])[i, j].ToString();
                            double dd = (initData as double[,])[i, rowStart];
                            chart1.Series["Param1"].Points.AddXY(m.ToString("F3"), (initData as double[,])[i, rowStart]);
                            if (i < p.inputGrid.GetJ - 1)
                            {
                                m += Math.Abs(p.inputGrid.inputCoor[rowStart, i].y - p.inputGrid.inputCoor[rowStart, i + 1].y);
                            }
                        }
                    }
                    chart1.Series.ResumeUpdates();  
                    break;
            }
        }
    }
}

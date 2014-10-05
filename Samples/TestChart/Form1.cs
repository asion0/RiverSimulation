using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestChart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int xIndex = 0;
        private Random rand = new Random();  // 狀態產生數值資料
        private void Form1_Load(object sender, EventArgs e)
        {


            chart1.ChartAreas.Add("Base");
            chart1.Series.Add("Level1");
            chart1.Series.Add("Level2");
            chart1.Series.Add("Level3");

            ChartArea myArea = chart1.ChartAreas["Base"];
            Series mySeries = chart1.Series["Level1"];
            Series mySeries2 = chart1.Series["Level2"];
            Series mySeries3 = chart1.Series["Level3"];

            myArea.AxisX.MajorGrid.LineColor = Color.Transparent; // X軸的刻度 縱線
            myArea.AxisY.MajorGrid.LineColor = Color.LightBlue; // Y軸的刻度 橫線
            myArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            myArea.AxisX.IntervalOffsetType = DateTimeIntervalType.Auto;
            myArea.AxisX.Minimum = 0.0;

            // 設定平均值的 Line
            StripLine lineMean = new StripLine();
            lineMean.Text = "Mean";
            lineMean.BorderColor = Color.Blue; // 線條的顏色
            lineMean.BorderWidth = 2;
            myArea.AxisY.StripLines.Add(lineMean);

            // 設定一個標準差的 Line
            //StripLine lineSD = new StripLine();
            //lineSD.Text = "Standard Deviation";
            //lineSD.BorderColor = Color.LightPink; // 線條的顏色
            //lineSD.BorderWidth = 2;
            //myArea.AxisY.StripLines.Add(lineSD);

            //myArea.AxisY.Interval = 0.5;
            //myArea.AxisY.Maximum = 10.5;
            //myArea.AxisY.Minimum = 1;

            // 資料列表 (單純數值表示)
            //mySeries.Points.AddXY(1, 2.3);
            //mySeries.Points.AddXY(2, 5.10);
            //mySeries.Points.AddXY(3, 10.4);
            //mySeries.Points.AddXY(4, 5.1);
            //mySeries.Points.AddXY(3, 3.1);
            //mySeries.Points.AddXY(2, 1.1);

            // 直接指定 X 軸上的 Label
            //mySeries.Points.AddXY("2012/12/1", String.Format("{0:0.0}", 100 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY("2012/12/2", String.Format("{0:0.0}", 100 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY("2012/12/3", String.Format("{0:0.0}", 100 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY("2012/12/4", String.Format("{0:0.0}", 100 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY("2012/12/2", "10.0");
            //mySeries.Points[4].MarkerColor = Color.Red;       // 設定個別 標記符號
            //mySeries.Points[4].LabelToolTip = "Out of range";  // 設定個別的 ToolTips

            // ********** 資料列表 (X軸上的資料可以用許多型態；Y軸上的資料都是用 double 數值表示) **********
            // 將 X 軸上的 Label 轉成特定的型別 
            // 如果以字串型式來設定，則會一直條列下去 (如上面的例子)
            // 如果是用可辨識型別的話，系統會發現有重覆的 X 軸上的資料
            //int xIndex = 1;
            //mySeries.Points.AddXY(DateTime.Parse("2012/12/1"), String.Format("{0:0.0}", 50 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(DateTime.Parse("2012/12/2"), String.Format("{0:0.0}", 60 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(DateTime.Parse("2012/12/3"), String.Format("{0:0.0}", 70 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(DateTime.Parse("2012/12/4"), String.Format("{0:0.0}", 100 + 15 * rand.NextDouble() * -20));
            //mySeries.Points.AddXY(DateTime.Parse("2012/12/2"), "10.0"); // 這筆資料就是日期重覆

            mySeries.Points.AddXY(xIndex++, String.Format("{0:0.0}", 50 + 15 * rand.NextDouble()));
            mySeries2.Points.AddXY(xIndex++, String.Format("{0:0.0}", 50 + 15 * rand.NextDouble()));
            mySeries3.Points.AddXY(xIndex++, String.Format("{0:0.0}", 50 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(xIndex++, String.Format("{0:0.0}", 60 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(xIndex++, String.Format("{0:0.0}", 70 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(xIndex++, String.Format("{0:0.0}", 100 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(xIndex++, String.Format("{0:0.0}", "10.0"));

            // ********** 設定 lineMean 的位置 ********** 
            // 取出 名為 Level1 的資料數例 (Series) 的 平均值
            lineMean.IntervalOffset = chart1.DataManipulator.Statistics.Mean("Level1");

            // ********** 設定 lineSD 的位置 ********** 
//            lineSD.IntervalOffset = lineMean.IntervalOffset - Math.Sqrt(chart1.DataManipulator.Statistics.Variance("Level1", true));
            //lineSD.StripWidth = 2.0 * (lineMean.IntervalOffset - Math.Abs(lineSD.IntervalOffset)); // 這麼算是 OK 的
            // 如果想要畫面一個資料帶的話，直接設定 StripWidth 就行
//            lineSD.StripWidth = (lineMean.IntervalOffset + Math.Sqrt(chart1.DataManipulator.Statistics.Variance("Level1", true)))
//                + Math.Abs((lineMean.IntervalOffset + Math.Sqrt(chart1.DataManipulator.Statistics.Variance("Level1", true)))); // 這麼算也行
            //lineSD.StripWidth = 2.0 * chart1.DataManipulator.Statistics.Variance("Level1", true); // 標準差 = 變異數的平方根
            // 資料帶的顏色設定
//            lineSD.BackColor = Color.LightGray;

            // ********** 設定 Mean 及 SD 線的標示資料 ********** 
            lineMean.Text = String.Format("Mean, {0:0.00}", lineMean.IntervalOffset);
//            lineSD.Text = String.Format("SD, {0:0.00}, Width={1:0.00}", lineSD.IntervalOffset, lineSD.StripWidth);

            // 下面的例子會使得圖型變成一條縱直線條
            //mySeries.Points.AddXY(DateTime.Parse("2012/12/01"), String.Format("{0:0.0}", 100 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(DateTime.Parse("2012/12/01"), String.Format("{0:0.0}", 50 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(DateTime.Parse("2012/12/01"), String.Format("{0:0.0}", 20 + 15 * rand.NextDouble()));
            //mySeries.Points.AddXY(DateTime.Parse("2012/12/01"), String.Format("{0:0.0}", 0+ 15 * rand.NextDouble()));

            //mySeries.ChartType = SeriesChartType.Line;        // 要使用的圖型類別 (Line, Bar(橫式直方圖), Spline, Column (直方圖) ...)
            //mySeries.ChartType = SeriesChartType.Candlestick; // 畫出蠟燭圖 (股市圖會用到)
            mySeries.ChartType = SeriesChartType.Spline;        // 曲線圖
            mySeries.Color = Color.Red;               // 在圖型上的顏色
            mySeries.BorderWidth = 3;                   // 線型的寬度
            mySeries.ShadowColor = Color.DarkGray;      // 陰影的顏色
            mySeries.ShadowOffset = 2;                  // 陰影位置的角度
            //mySeries.MarkerColor = Color.Blue;           // 標記符號的顏色
            mySeries.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            //mySeries.MarkerSize = 15;                    // 標記符號的大小
            mySeries.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊
            //mySeries["LabelStyle"] = "Left";           // 指定所有的標籤出現在何處

            // 可以在特定的 Point (數值) 進行更進階的設定
            // 如：設定顏色、設定提示語
            //mySeries.Points[3].MarkerColor = Color.Red;        // 設定個別 標記符號
            //mySeries.Points[3].LabelToolTip = "Out of range";  // 設定個別的 ToolTips

            mySeries2.ChartType = SeriesChartType.Spline;        // 曲線圖
            mySeries2.Color = Color.Blue;               // 在圖型上的顏色
            mySeries2.BorderWidth = 3;                   // 線型的寬度
            mySeries2.ShadowColor = Color.DarkBlue;      // 陰影的顏色
            mySeries2.ShadowOffset = 2;                  // 陰影位置的角度
            mySeries2.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            mySeries2.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊

            mySeries3.ChartType = SeriesChartType.Spline;        // 曲線圖
            mySeries3.Color = Color.Green;               // 在圖型上的顏色
            mySeries3.BorderWidth = 3;                   // 線型的寬度
            mySeries3.ShadowColor = Color.DarkGreen;      // 陰影的顏色
            mySeries3.ShadowOffset = 2;                  // 陰影位置的角度
            mySeries3.MarkerStyle = MarkerStyle.None;  // 標記的樣式 (Circle, Diamond ...)
            mySeries3.IsValueShownAsLabel = false;         // 將 Y 值顯示在標記符號旁邊

        }

        private double lastD = 100.0;
//        private int lastXInterval = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            ChartArea myArea = chart1.ChartAreas["Base"];
            Series mySeries = chart1.Series["Level1"];
            Series mySeries2 = chart1.Series["Level2"];
            Series mySeries3 = chart1.Series["Level3"];
            double d1 = lastD + 60 * (0.5 - rand.NextDouble()) + 90;
            d1 %= 2500.0;
            double d2 = lastD + 50 * (0.5 - rand.NextDouble()) + 50;
            d2 %= 2500.0;
            double d3 = lastD + 40 * (0.5 - rand.NextDouble());
            d3 %= 2500.0;

            mySeries.Points.AddXY(xIndex++, String.Format("{0:0.0}", d1));
            mySeries2.Points.AddXY(xIndex++, String.Format("{0:0.0}", d2));
            mySeries3.Points.AddXY(xIndex++, String.Format("{0:0.0}", d3));


            StripLine lineMean = myArea.AxisY.StripLines[0];
            lineMean.IntervalOffset = chart1.DataManipulator.Statistics.Mean("Level1");
            lineMean.Text = String.Format("Mean, {0:0.00}", lineMean.IntervalOffset);
//           = chart1.DataManipulator.Statistics.Mean("Level1");
           // int interval = ((int)xIndex / 100) * 10;
           // if(lastXInterval < interval)
           // {
           //     lastXInterval = interval;
           //     myArea.AxisX.IntervalOffset = interval;
           //}

        }
    }
}

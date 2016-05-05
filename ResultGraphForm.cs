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
        //U-同參數單位 D-累距
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
            /*
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
            */
            Unknown,
            XyGraph,
            ContourGraph,
        }
        protected ResultGraphType formType = ResultGraphType.Unknown;

        RiverSimulationProfile p = RiverSimulationProfile.profile;
        public string key = "";
        public void SetFormMode(
            string title,
            int iStart, int iEnd,
            int jStart, int jEnd,
            int kStart, int kEnd,
            int tStart, int tEnd,
            string tableName = "",
            string colName = "",
            string rowName = "",
            ResultGraphType formType = ResultGraphType.XyGraph,
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
            int []timeSel = null,
            double[] timeList = null)
        {
            this.title = title;
            this.formType = formType;
            this.iStart = iStart;
            this.iEnd = iEnd;
            this.jStart = jStart;
            this.jEnd = jEnd;
            this.kStart = kStart;
            this.kEnd = kEnd;
            this.tStart = tStart;
            this.tEnd = tEnd;
            this.tableName = tableName;
            this.colName = colName;
            this.rowName = rowName;
            this.initData = initData;
            this.xDim = xDim;
            this.yDim = yDim;
            this.sel1Dim = sel1Dim;
            this.sel2Dim = sel2Dim;
            this.sel1Title = sel1Title;
            this.sel2Title = sel2Title;
            this.sel1Index = sel1Index;
            this.sel2Index = sel2Index;
            this.timeSel = timeSel;
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
            string graphicsPath = Program.GetProjectFileWorkingPath() + "\\Graphics\\XYMode0\\" + key;
            string pngFile = string.Format("{0}\\{1}_{2}_{3}.png", graphicsPath, i, jS, jE).Replace('\\', '/');
            if (File.Exists(pngFile))
                return pngFile;

            double distance = 0.0;
            if (jS != 0)
            {   //累距不從0開始
                for (int j = 0; j < jS; ++j)
                {
                    distance += CumulativeJDistance(i, j, p.inputGrid.inputCoor);
                }
            }
            //產生gunplot輸入檔內容
            StringBuilder sb = new StringBuilder();
            for (int j = jS; j < jE; ++j)
            {
                distance += CumulativeJDistance(i, j, p.inputGrid.inputCoor);
                sb.AppendFormat("{0,15} ", distance.ToString("F7"));
                sb.AppendFormat("{0,15} ", ((initData as double[, ,])[i, j, 0]).ToString("F7"));
                sb.AppendFormat("\n");
            }

            string plotfile = string.Format("{0}\\{1}_{2}_{3}.txt", graphicsPath, i, jS, jE).Replace('\\', '/');
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
            GnuPlot.Plot(plotfile, @"with lines");
            GnuPlot.Set("output");
            return pngFile;
        }

        private string DrawMode1XY(int j, int iS, int iE)
        {
            // Mode 1 status : 1-Single M-Multiple D-壘距 U-資料單位
            // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data   |
            // M|1|-|-|D|U| J  | -  | 1  |3D IJ1  |
            string graphicsPath = Program.GetProjectFileWorkingPath() + "\\Graphics\\XYMode1\\" + key;
            string pngFile = string.Format("{0}\\{1}_{2}_{3}.png", graphicsPath, j, iS, iE).Replace('\\', '/');
            if (File.Exists(pngFile))
                return pngFile;

            double distance = 0.0;
            if (iS != 0)
            {   //累距不從0開始
                for (int i = 0; i < iS; ++i)
                {
                    distance += CumulativeIDistance(i, j, p.inputGrid.inputCoor);
                }
            }
            //產生gunplot輸入檔內容
            StringBuilder sb = new StringBuilder();
            for (int i = iS; i < iE; ++i)
            {
                distance += CumulativeIDistance(i, j, p.inputGrid.inputCoor);
                sb.AppendFormat("{0,15} ", distance.ToString("F7"));
                sb.AppendFormat("{0,15} ", ((initData as double[, ,])[i, j, 0]).ToString("F7"));
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

        private string DrawMode2XY(int t, int i, int jS, int jE)
        {
            // Mode 0 status : 1-Single M-Multiple D-壘距 U-資料單位
            // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data   |
            // 1|M|-|-|D|U| I  | -  | 0  |3D IJ1  |
            // 1|M|1|-|D|U| I  | -  | 2  |3D IJT |
            string graphicsPath = Program.GetProjectFileWorkingPath() + "\\Graphics\\XYMode2\\" + key;
            string pngFile = string.Format("{0}\\{1}_{2}_{3}_{4}.png", graphicsPath, t, i, jS, jE).Replace('\\', '/');
            if (File.Exists(pngFile))
                return pngFile;

            double distance = 0.0;
            if (jS != 0)
            {   //累距不從0開始
                for (int j = 0; j < jS; ++j)
                {
                    distance += CumulativeJDistance(i, j, p.inputGrid.inputCoor);
                }
            }
            //產生gunplot輸入檔內容
            StringBuilder sb = new StringBuilder();
            for (int j= jS; j < jE; ++j)
            {
                distance += CumulativeJDistance(i, j, p.inputGrid.inputCoor);
                sb.AppendFormat("{0,15} ", distance.ToString("F7"));
                sb.AppendFormat("{0,15} ", ((initData as double[, ,])[i, j, t]).ToString("F7"));
                sb.AppendFormat("\n");
            }

            string plotfile = string.Format("{0}\\{1}_{2}_{3}_{4}.txt", graphicsPath, t, i, jS, jE).Replace('\\', '/');
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
            GnuPlot.Plot(plotfile, @"with lines");
            GnuPlot.Set("output");
            return pngFile;
        }

        private string DrawMode3XY(int t, int j, int iS, int iE)
        {
            // Mode 3 status : 1-Single M-Multiple D-壘距 U-資料單位
            // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data  |
            // M|1|1|-|D|U| J  | -  | 3  |3D IJT |
            // 1|M|1|-|D|U| I  | -  | 2  |3D IJT |
            string graphicsPath = Program.GetProjectFileWorkingPath() + "\\Graphics\\XYMode3\\" + key;
            string pngFile = string.Format("{0}\\{1}_{2}_{3}_{4}.png", graphicsPath, t, j, iS, iE).Replace('\\', '/');
            if (File.Exists(pngFile))
                return pngFile;

            double distance = 0.0;
            if (iS != 0)
            {   //累距不從0開始
                for (int i = 0; i < iS; ++i)
                {
                    distance += CumulativeIDistance(i, j, p.inputGrid.inputCoor);
                }
            }
            //產生gunplot輸入檔內容
            StringBuilder sb = new StringBuilder();
            for (int i= iS; i < iE; ++i)
            {
                distance += CumulativeIDistance(i, j, p.inputGrid.inputCoor);
                sb.AppendFormat("{0,15} ", distance.ToString("F7"));
                sb.AppendFormat("{0,15} ", ((initData as double[, ,])[i, j, t]).ToString("F7"));
                sb.AppendFormat("\n");
            }

            string plotfile = string.Format("{0}\\{1}_{2}_{3}_{4}.txt", graphicsPath, t, j, iS, iE).Replace('\\', '/');
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
            GnuPlot.Plot(plotfile, @"with lines");
            GnuPlot.Set("output");
            return pngFile;
        }

        private string DrawMode0Contour()
        {
            // Mode 1 status : 1-Single M-Multiple D-壘距 U-資料單位
            // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data   |
            // M|1|-|-|D|U| J  | -  | 1  |3D IJ1  |
            string graphicsPath = Program.GetProjectFileWorkingPath() + "\\Graphics\\ContourMode0\\" + key;
            string pngFile = string.Format("{0}\\C{1}_{2}.png", graphicsPath, iEnd, jEnd).Replace('\\', '/');
            if (File.Exists(pngFile))
                return pngFile;

            //產生gunplot輸入檔內容
            StringBuilder sb = new StringBuilder();
            for (int i = iStart; i < iEnd; ++i)
            {
                for (int j = jStart; j < jEnd; ++j)
                {
                    sb.AppendFormat("{0,15} ", p.inputGrid.inputCoor[i, j].x.ToString("F7"));
                    sb.AppendFormat("{0,15} ", p.inputGrid.inputCoor[i, j].y.ToString("F7"));
                    sb.AppendFormat("{0,15} ", ((initData as double[, ,])[i, j, 0]).ToString("F7"));
                    sb.AppendFormat("\n");
                }
                sb.AppendFormat("\n");
            }

            string plotfile = string.Format("{0}\\C{1}_{2}.txt", graphicsPath, iEnd, jEnd).Replace('\\', '/');
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
                                   "hidden3d",
                                   "contour base",
                                   "view map",
                                   "pm3d at b",
                                    "term png size 960,630",
                                   setPngFile,
                               };
            GnuPlot.Set(setting);
            //plotfile = String.Format("\"{0}\"", plotfile);
            GnuPlot.SPlot(plotfile, "with lines title \"\"");
            //GnuPlot.Set("term x11");
            //GnuPlot.WriteLine();
            GnuPlot.Set("output");
            return pngFile;
        }

        private string DrawMode1Contour()
        {
            // Mode 1 status : 1-Single M-Multiple D-壘距 U-資料單位
            // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data   |
            // M|1|-|-|D|U| J  | -  | 1  |3D IJ1  |
            string graphicsPath = string.Format("{0}\\Graphics\\ContourMode0_{1}", Program.GetProjectFileWorkingPath(), tableName.Replace('\\', '_').Replace('/', '_'));
            string pngFile = string.Format("{0}\\C{1}_{2}_{3}.png", graphicsPath, tStart, iEnd, jEnd).Replace('\\', '/');
            if (File.Exists(pngFile))
                return pngFile;

            //產生gunplot輸入檔內容
            StringBuilder sb = new StringBuilder();
            for (int i = iStart; i < iEnd; ++i)
            {
                for (int j = jStart; j < jEnd; ++j)
                {
                    sb.AppendFormat("{0,15} ", p.inputGrid.inputCoor[i, j].x.ToString("F7"));
                    sb.AppendFormat("{0,15} ", p.inputGrid.inputCoor[i, j].y.ToString("F7"));
                    sb.AppendFormat("{0,15} ", ((initData as double[, ,])[i, j, tStart]).ToString("F7"));
                    sb.AppendFormat("\n");
                }
                sb.AppendFormat("\n");
            }

            string plotfile = string.Format("{0}\\C{1}_{2}_{3}.txt", graphicsPath, tStart, iEnd, jEnd).Replace('\\', '/');
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
                                   "hidden3d",
                                   "contour base",
                                   "view map",
                                   "pm3d at b",
                                    "term png size 960,630",
                                   setPngFile,
                               };
            GnuPlot.Set(setting);
            //plotfile = String.Format("\"{0}\"", plotfile);
            GnuPlot.SPlot(plotfile, "with lines title \"\"");
            //GnuPlot.Set("term x11");
            //GnuPlot.WriteLine();
            GnuPlot.Set("output");
            return pngFile;
        }



        public static int Time = -4;
        public static int CumulativeDistance = -3;
        public static int DataContent = -2;
        private GraphType graphType = GraphType.TypeUnknown;
        private void InitializeChartView()
        {
            if (formType == ResultGraphType.XyGraph)
            {
                if (sel1Index != -1 && sel2Index == -1 && xDim == CumulativeDistance && yDim == DataContent && (iEnd - iStart == 1) && (tStart == -1))
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
                        DrawMode0XY(i, jStart, jEnd);
                    }
                    combo1.SelectedIndex = jStart;
                    //string gfile = DrawMode0XY(rowStart, colStart, colEnd);
                }
                else if (sel1Index != -1 && sel2Index == -1 && xDim == CumulativeDistance && yDim == DataContent && (jEnd - jStart == 1) && (tStart == -1))
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
                        DrawMode1XY(j, iStart, iEnd);
                    }
                    combo1.SelectedIndex = jStart;
                    //string gfile = DrawMode0XY(rowStart, colStart, colEnd);
                }
                else if (sel1Index != -1 && sel2Index == -1 && xDim == CumulativeDistance && yDim == DataContent && (iEnd - iStart == 1) && (timeList != null) && (tEnd - tStart == 1))
                {
                    // Mode1 I範圍 J固定 選項一是J X顯示累距 Y顯示資料
                    // Mode 1 status : 1-Single M-Multiple D-壘距 U-資料單位
                    // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data  |
                    // 1|M|1|-|D|U| I  | -  | 2  |3D IJT |
                    graphType = GraphType.Type2;
                    sel1Lbl.Visible = true;
                    sel1Lbl.Text = sel1Title;
                    combo1.Visible = true;
                    for (int i = 0; i < (initData as double[, ,]).GetLength(sel1Dim); ++i)
                    {
                        combo1.Items.Add((i + 1).ToString());
                        DrawMode2XY(tStart, i, jStart, jEnd);
                    }
                    combo1.SelectedIndex = iStart;
                    //string gfile = DrawMode0XY(rowStart, colStart, colEnd);
                }
                else if (sel1Index != -1 && sel2Index == -1 && xDim == CumulativeDistance && yDim == DataContent && (jEnd - jStart == 1) && (timeList != null) && (tEnd - tStart == 1))
                {
                    // Mode1 I範圍 J固定 選項一是J X顯示累距 Y顯示資料
                    // Mode 1 status : 1-Single M-Multiple D-壘距 U-資料單位
                    // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data  |
                    // 1|M|1|-|D|U| I  | -  | 2  |3D IJT |
                    // M|1|1|-|D|U| J  | -  | 3  |3D IJT |
                    graphType = GraphType.Type3;
                    sel1Lbl.Visible = true;
                    sel1Lbl.Text = sel1Title;
                    combo1.Visible = true;
                    for (int j = 0; j < (initData as double[, ,]).GetLength(sel1Dim); ++j)
                    {
                        combo1.Items.Add((j + 1).ToString());
                        DrawMode3XY(tStart, j, iStart, iEnd);
                    }
                    combo1.SelectedIndex = jStart;
                    //string gfile = DrawMode0XY(rowStart, colStart, colEnd);
                }
            }
            else if(formType == ResultGraphType.ContourGraph)
            {
                if (tStart == -1)
                {
                    // Mode 0 status : 1-Single M-Multiple D-壘距 U-資料單位
                    // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data   |
                    // 1|M|-|-|D|U| I  | -  | 0  |3D IJ1  |                    graphType = GraphType.Type0;
                    graphType = GraphType.Type0;
                    sel1Lbl.Visible = false;
                    sel1Lbl.Text = "";
                    combo1.Visible = false;
                    String s = DrawMode0Contour();
                    ShowImage(s);
                }
                else
                {
                    // Mode 0 status : 1-Single M-Multiple D-壘距 U-資料單位
                    // I|J|T|K|X|Y|Sel1|Sel2|Mode| Data   |
                    // 1|M|-|-|D|U| I  | -  | 0  |3D IJ1  |                    graphType = GraphType.Type0;
                    graphType = GraphType.Type0;
                    sel1Lbl.Visible = false;
                    sel1Lbl.Text = "";
                    combo1.Visible = false;
                    String s = DrawMode1Contour();
                    ShowImage(s);
                }

            }
        }

        void ShowImage(string s)
        {
            String s2 = s + ".png";
            int errCount = 100;
            while (true)
            {
                try
                {
                    if (File.Exists(s2))
                    {
                        File.Delete(s2);
                    }
                    File.Copy(s, s2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Thread.Sleep(10);
                    if (--errCount == 0)
                        break;
                    else
                        continue;
                }
                FileInfo fi = new FileInfo(s2);
                if (fi.Length == 0)
                {
                    Thread.Sleep(10);
                    if (--errCount == 0)
                        break;
                    else
                        continue;
                }
                break;
            }
            if(pic1.Image!= null)
                pic1.Image.Dispose();
            pic1.Image = Image.FromFile(s2);
        }
        /*
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

            if (formType == ResultGraphType.InitialBottomElevationXyGraph )
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
        */
        /*
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
            
        }
        */
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

        private int lastCombo1Select = -1;
        private void combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = (sender as ComboBox);
            int sel = Convert.ToInt32(c.Items[c.SelectedIndex]) - 1;
            if (lastCombo1Select == sel)
                return;

            lastCombo1Select = sel;
            String s = "";
            if (graphType == GraphType.Type0)
            {
                s = DrawMode0XY(sel, jStart, jEnd);
            }
            else if (graphType == GraphType.Type1)
            {
                s = DrawMode1XY(sel, iStart, iEnd);
            }
            else if (graphType == GraphType.Type2)
            {
                s = DrawMode2XY(tStart, sel, jStart, jEnd);
            }
            else if (graphType == GraphType.Type3)
            {
                s = DrawMode3XY(tStart, sel, iStart, iEnd);
            } 
            ShowImage(s);


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

        private void ResultGraphForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pic1.Image != null)
                pic1.Image.Dispose();
        }
    }
}

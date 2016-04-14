using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Utilities.GnuplotCSharp;


namespace RiverSimulationApplication
{
    public partial class SimulationResultForm : Form
    {
        public SimulationResultForm()
        {
            InitializeComponent();
        }

        RiverSimulationProfile p = null;
        private List<double> resedTimeList = null;
        private List<double> sedTimeList = null;

        /* I|J|T|K|X|Y|Sel1|Sel2|Mode| Data  |
         * -+-+-+-+-+-+----+----+----+-------+
         * M|M|-|-|I|J| -  | -  | 0  |2D IJ  |
         * -+-+-+-+-+-+----+----+----+-------+
         * M|M|1|-|I|J| T  | -  | 1  |3D IJT |
         * -+-+-+-+-+-+----+----+----+-------+
         * 1|M|M|-|T|J| I  | -  | 2  |3D IJT |
         * -+-+-+-+-+-+----+----+----+-------+
         * M|1|M|-|T|I| J  | -  | 3  |3D IJT |
         * -+-+-+-+-+-+----+----+----+-------+
         * 1|1|M|-|T|I| J  | -  | 4  |3D IJT |
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
        private enum TableType
        {
            Type0,
            Type1,
            Type2,
            Type3,
            Type4,
            Type5,
            Type6,
            Type7,
            Type8,
            Type9,
            TypeA,
            Type1234,
            Type56789A,
            TypeUnknown,
        };
        TableType tableType = TableType.TypeUnknown;
        ResultGraphForm.GraphType graphType = ResultGraphForm.GraphType.TypeUnknown;

        enum GraphFormMode
        {
            None,
            XY,
            Contour,
            Vector,
            ContouWithVector
        }
        private GraphFormMode graphFormMode = GraphFormMode.None;

        private void UpdateStatus()
        {
            if (drawType == Param1Type.ParamGraph1)
            {
                formGrp.Enabled = true;
                if(graphFormMode == GraphFormMode.None)
                {
                    paramGrp.Enabled = false;
                    posGrp.Enabled = false;
                    timeGrp.Enabled = false;
                    axisGrp.Enabled = false;
                }
                else if(graphFormMode == GraphFormMode.XY)
                {
                    paramGrp.Enabled = true;
                    posGrp.Enabled = true;
                    timeGrp.Enabled = (param1Cmb.SelectedIndex != 0);
                    axisGrp.Enabled = false;
                    param2Cmb.Enabled = false;
                }
                else if (graphFormMode == GraphFormMode.Contour)
                {

                    paramGrp.Enabled = true;
                    posGrp.Enabled = false;
                    timeGrp.Enabled = (param1Cmb.SelectedIndex != 0);
                    axisGrp.Enabled = false;
                    param2Cmb.Enabled = false;
                }
                else if (graphFormMode == GraphFormMode.Vector)
                {
                    paramGrp.Enabled = true;
                    posGrp.Enabled = true;
                    timeGrp.Enabled = true;
                    axisGrp.Enabled = false;
                    param2Cmb.Enabled = false;
                }
                else if (graphFormMode == GraphFormMode.ContouWithVector)
                {
                    paramGrp.Enabled = true;
                    posGrp.Enabled = true;
                    timeGrp.Enabled = true;
                    axisGrp.Enabled = false;
                    param2Cmb.Enabled = true;
                }
            }
            else if(drawType == Param1Type.ParamTable)
            { 
                paramGrp.Enabled = true;
                formGrp.Enabled = false;
                posGrp.Enabled = true;
                timeGrp.Enabled = true;
                axisGrp.Enabled = false;
                param2Cmb.Enabled = false;

                switch (tableType)
                {
                    case TableType.Type0:
                        timeGrp.Enabled = false;
                        poKPanel.Enabled = false;
                        break;
                    case TableType.Type1234:
                    case TableType.Type1:
                    case TableType.Type2:
                    case TableType.Type3:
                    case TableType.Type4:
                        timeGrp.Enabled = true;
                        timeBtn.Enabled = true;
                        poKPanel.Enabled = false;
                        break;
                    case TableType.Type56789A:
                    case TableType.Type5:
                    case TableType.Type6:
                    case TableType.Type7:
                    case TableType.Type8:
                    case TableType.Type9:
                    case TableType.TypeA:
                        timeGrp.Enabled = true;
                        poKPanel.Enabled = true;
                        break;
                    default:
                        timeGrp.Enabled = false;
                        timeBtn.Enabled = true;
                        poKPanel.Enabled = false;
                        break;
                }
            }
            else
            {
                paramGrp.Enabled = false;
                formGrp.Enabled = false;
                posGrp.Enabled = false;
                timeGrp.Enabled = false;
                axisGrp.Enabled = false;  
            }
        }

        private void SimulationResultForm_Load(object sender, EventArgs e)
        {
            p = RiverSimulationProfile.profile;
            posILbl.Text = String.Format("MAX:{0}", p.inputGrid.GetI);
            posJLbl.Text = String.Format("MAX:{0}", p.inputGrid.GetJ);
            if(p.Is3DMode())
            {
                posKLbl.Text = String.Format("MAX:{0}", p.verticalLevelNumber);
            }

            if (!ParsingTime("resed.O", ref resedTimeList))
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (p.IsMovableBedMode() && !ParsingTime("SEDoutput.dat", ref sedTimeList))
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            UpdateStatus();
        }

        private void graphRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            //20150811後處理流程示範說明,
            //2. 若選擇“圖形”：
            //  則“參數”先只能選擇一種，待“形式”若選擇“等值線疊向量圖”，
            //  再把第2 個“參數”的下拉選單打開供使用者選擇。
            drawType = Param1Type.ParamGraph1;
            InitialParam1(drawType);
            UpdateStatus();
        }
        private void tableRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            //20150811後處理流程示範說明,
            //1. 若選擇“表格”：
            //  A. 則“參數”只能選擇一種，第2 個“參數”的下拉選單灰階。
            //  B. 且“形式”及“座標軸”也灰階。
            drawType = Param1Type.ParamTable;
            InitialParam1(Param1Type.ParamTable);
            UpdateStatus();
        }

        private enum Param1Type {
            None,
            ParamTable,
            ParamGraph1,
        }
        Param1Type drawType = Param1Type.None;

        private string[] tableItemsParam1 = {
                "初始底床高程(m)",                    //0
                "水深平均流速-U(m/s)",                //1
                "水深平均流速-V(m/s)",                //2
                "水深平均流速-UV 合向量的絕對值(m/s)",  //3
                "底床剪應力(N/m2)",                   //4
                "水位(m)",                           //5
                "水深(m)",                           //6
                "流量-U(cms)",                       //7
                "流量-V(cms)",                       //8
                "底床高程(m)",                        //9
                "沖淤深度(m)",                        //10
                "水深平均濃度(ppm)",                  //11
                "粒徑分佈(%)",                        //12
                "流速-U(m/s)",                       //13
                "流速-V(m/s)",                       //14
                "流速-W(m/s)",                       //15
                "濃度(ppm)" };                       //16

        private string[] tableItemsParam2 = {
                "初始底床高程(m)",                      //0
                "水深平均流速-U(m/s)",                  //1
                "水深平均流速-V(m/s)",                  //2
                "水深平均流速-UV 合向量的絕對值(m/s)",    //3
                "底床剪應力(N/m2)",                     //4
                "水位(m)",                             //5
                "水深(m)",                             //6
                "流量-U(cms)",                         //7
                "流量-V(cms)",                         //8
                "底床高程(m)",                          //9
                "沖淤深度(m)",                          //10
                "水深平均濃度(ppm)",                    //11
                "粒徑分佈(%)",                          //12
                "流速-U(m/s)",                         //13
                "流速-V(m/s)",                         //14
                "流速-W(m/s)",                         //15
                "濃度(ppm)" };                         //16
        private string[] tableItemsParam3 = {
                "初始底床高程(m)",                    //0
                "水深平均流速-U(m/s)",                //1
                "水深平均流速-V(m/s)",                //2
                "水深平均流速-UV 合向量的絕對值(m/s)",  //3
                "底床剪應力(N/m2)",                   //4
                "水位(m)",                           //5
                "水深(m)",                           //6
                "流量-U(cms)",                       //7
                "流量-V(cms)",                       //8
                "底床高程(m)",                        //9
                "沖淤深度(m)",                        //10
                "水深平均濃度(ppm)",                  //11
                "粒徑分佈(%)",                        //12
                "流速-U(m/s)",                       //13
                "流速-V(m/s)",                       //14
                "流速-W(m/s)",                       //15
                "濃度(ppm)" };                       //16
        private string[] tableItemsParam4 = {       
                "水深平均流速-UV 合向量的絕對值(m/s)",   //0
                "流速-UW合向量(m/s)",                  //1
                "流速-VW合向量(m/s)" };                //2
                                                     
        private void InitialParam1(Param1Type p)
        {
            if (p == Param1Type.ParamTable)
            {
                param1Cmb.DataSource = tableItemsParam1;
            }
            else if (p == Param1Type.ParamGraph1)
            {
                switch(graphFormMode)
                {
                    case GraphFormMode.XY:
                        param1Cmb.DataSource = tableItemsParam2;
                        break;
                    case GraphFormMode.Contour:
                        param1Cmb.DataSource = tableItemsParam3;
                        break;
                    case GraphFormMode.Vector:
                        param1Cmb.DataSource = tableItemsParam4;
                        break;
                    case GraphFormMode.ContouWithVector:
                        param1Cmb.DataSource = tableItemsParam3;
                        param2Cmb.DataSource = tableItemsParam4;
                        break;
                }
                if (param1Cmb.DataSource != null)
                    param1Cmb.SelectedIndex = 0;
                if (param2Cmb.DataSource != null)
                    param2Cmb.SelectedIndex = 0;
            }
        }

        private void param1Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            if(cmb.DataSource == tableItemsParam1)
            {
                switch(cmb.SelectedIndex)
                {
                     case 0: //初始底床高程(m)
                        tableType = TableType.Type0;
                        break;
                    case 1: //水深平均流速-U(m/s)
                    case 2: //水深平均流速-V(m/s)
                    case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                    case 4: //水深平均流速-UV 合向量(m/s)
                    case 5: //底床剪應力(N/m2)
                    case 6: //水位(m)
                    case 7: //水深(m)
                    case 8: //流量-U(cms)
                    case 9: //流量-V(cms)
                    case 10: //底床高程(m)
                    case 11: //沖淤深度(m)
                    case 12: //水深平均濃度(ppm)
                    case 13: //粒徑分佈(%)
                        tableType = TableType.Type1234;
                        break;
                    case 14: //流速-U(m/s)
                    case 15: //流速-V(m/s)
                    case 16: //流速-W(m/s)
                    case 17: //流速-UW合向量(m/s
                    case 18: //流速-VW合向量(m/s)
                    case 19: //濃度(ppm) 
                        tableType = TableType.Type56789A;
                        break;
                    default:
                       break;
                }
                UpdateStatus();
            }
            else if(cmb.DataSource == tableItemsParam2)
            {
                switch (cmb.SelectedIndex)
                {
                    case 0: //初始底床高程(m)
                        graphType = ResultGraphForm.GraphType.Type01;
                        break;
                    case 1: //水深平均流速-U(m/s)
                    case 2: //水深平均流速-V(m/s)
                    case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                    case 4: //水深平均流速-UV 合向量(m/s)
                    case 5: //底床剪應力(N/m2)
                    case 6: //水位(m)
                    case 7: //水深(m)
                    case 8: //流量-U(cms)
                    case 9: //流量-V(cms)
                    case 10: //底床高程(m)
                    case 11: //沖淤深度(m)
                    case 12: //水深平均濃度(ppm)
                    case 13: //粒徑分佈(%)
                        graphType = ResultGraphForm.GraphType.Type234;
                        break;
                    case 14: //流速-U(m/s)
                    case 15: //流速-V(m/s)
                    case 16: //流速-W(m/s)
                    case 17: //流速-UW合向量(m/s
                    case 18: //流速-VW合向量(m/s)
                    case 19: //濃度(ppm) 
                        //graphType = GraphType.Type56789A;
                        break;
                    default:
                        break;
                    /*
                case 0: //初始底床高程(m)
                    //graphType = GraphType.Type0;
                    break;
                case 1: //水深平均流速-U(m/s)
                case 2: //水深平均流速-V(m/s)
                case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                case 4: //水深平均流速-UV 合向量(m/s)
                case 5: //底床剪應力(N/m2)
                case 6: //水位(m)
                case 7: //水深(m)
                case 8: //流量-U(cms)
                case 9: //流量-V(cms)
                case 10: //底床高程(m)
                case 11: //沖淤深度(m)
                case 12: //水深平均濃度(ppm)
                case 13: //粒徑分佈(%)
                    //graphType = GraphType.Type56789A;
                    break;
                case 14: //流速-U(m/s)
                case 15: //流速-V(m/s)
                case 16: //流速-W(m/s)
                case 17: //流速-UW合向量(m/s
                case 18: //流速-VW合向量(m/s)
                case 19: //濃度(ppm) 
                    //graphType = GraphType.Type56789A;
                    break;
                default:
                   break;
                     * */
                }
                UpdateStatus();
            }
            else
            {
                UpdateStatus();
            }
        }

        private void posKchk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            posKTxt.Enabled = !chk;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();            
        }

        private void generateResultBtn_Click(object sender, EventArgs e)
        {
            if(drawType == Param1Type.ParamTable)
            {
                GenerateTable();
            }
            else if(drawType == Param1Type.ParamGraph1)
            {
                GenerateGraph();
            }
            else
            {
                MessageBox.Show("請選取表格/圖形！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void GenerateTable()
        {
            switch (param1Cmb.SelectedIndex)
            {
                case 0: //初始底床高程(m)
                    GenerateInitialBottomElevationTable();
                    break;
                case 1: //水深平均流速-U(m/s)
                    GenerateTimeIJResultTable(" U-VELOCITY (M/S)", "水深平均流速-U(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedU);
                    break;
                case 2: //水深平均流速-V(m/s)
                    //GenerateDepthAverageFlowSpeedVTable();
                    GenerateTimeIJResultTable(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                  break;
                case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                   //GenerateDepthAverageFlowSpeedVTableAbs();
                    break;
                case 4: //底床剪應力(N/m2)   //⁰¹²³⁴⁵⁶⁷⁸⁹
                    GenerateTimeIJResultTable(" TOMD1-U", "底床剪應力(N/m²)", "resed.O", resedTimeList, ref tomd1);
                    break;
                case 5: //水位(m)
                    GenerateTimeIJResultTable(" ZS (M)", "水位(m)", "resed.O", resedTimeList, ref zs);
                    break;
                case 6: //水深(m)
                    GenerateTimeIJResultTable(" DEPTH (M)", "水深(m)", "resed.O", resedTimeList, ref depth);
                    break;
                case 7: //流量-U(cms)
                    GenerateTimeIJResultTable(" US-DISCHARGE (M3/S/M)", "流量-U(cms)", "resed.O", resedTimeList, ref usDischarge);
                    break;
                case 8: //流量-V(cms)
                    GenerateTimeIJResultTable(" VS-DISCHARGE (M3/S/M)", "流量-V(cms)", "resed.O", resedTimeList, ref vsDischarge);
                   break;
                case 9: //底床高程(m)
                   GenerateTimeIJResultTable(" ZS (M)", "底床高程(m)", "resed.O", sedTimeList, ref usDischarge);
                    break;
                case 10: //沖淤深度(m)
                    GenerateTimeIJResultTable(" DZBED (M)", "沖淤深度(m)", "SEDoutput.dat", sedTimeList, ref zbed);
                    break;
                case 11: //水深平均濃度(ppm)
                    GenerateTimeIJResultTable(" MUDCONC & CONC (-)", "水深平均濃度(ppm)", "SEDoutput.dat", sedTimeList, ref mudconc);
                    break;
                case 12: //粒徑分佈(%)
                    GenerateTimeIJResultTable(" BETA (-)", "粒徑分佈(%)", "SEDoutput.dat", sedTimeList, ref beta);
                    break;
                case 13: //流速-U(m/s)
                case 14: //流速-V(m/s)
                case 15: //流速-W(m/s)
                case 16: //濃度(ppm)
                    break;
            }
        }

        private void GenerateInitialBottomElevationTable()
        {
            int iStart = 0, iEnd = 0, jStart = 0, jEnd = 0;
            if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref iStart, ref iEnd))
            {
                MessageBox.Show("請輸入正確的I位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref jStart, ref jEnd))
            {
                MessageBox.Show("請輸入正確的J位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(!ParsingInitialBottomElevationResult())
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ResultTableForm form = new ResultTableForm();
            form.SetFormMode(
                "初始底床高程(m)",   //視窗標題
                jStart, jEnd,       //行數(左右有幾行)
                iStart, iEnd,       //列數(上下有幾列)
                "",                 //表格名稱
                "J=",                 //行標題(顯示於上方)
                "I=",                 //列標題(顯示於左方)
                ResultTableForm.ResultTableType.InitialBottomElevation, //表格形式
                48,                 //儲存格寬度
                64,                 //列標題寬度
                true,               //保留
                false,              //不須行數字
                false,              //不須列數字
                initialBottomElevation,  //資料
                1,                  //X維度
                0,                  //Y維度
                -1,                 //Sel1維度
                -1,                 //Sel2維度
                0,                  //Sel1索引
                -1,                 //Sel2索引
                "",                 //Sel1標籤
                "",                 //Sel2標籤
                null);              //Time陣列                

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
        }

        private void GenerateInitialBottomElevationGraph()
        {
            if (!ParsingInitialBottomElevationResult())
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ResultGraphForm form = new ResultGraphForm();
           
            switch (graphFormMode)
            {
                case GraphFormMode.XY:
                    PosInfo pi = new PosInfo();
                    ResultGraphForm.GraphType gt = GetGraphType(ref pi);
                    if (gt >= ResultGraphForm.GraphType.Type01)
                    {
                        MessageBox.Show("請輸入正確位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (gt == ResultGraphForm.GraphType.Type0)
                    {
                        form.SetFormMode(
                            "初始底床高程(m)",   //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            "",                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.XyGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            initialBottomElevation,  //資料
                            ResultGraphForm.CumulativeDistance,                  //X維度
                            ResultGraphForm.DataContent,                  //Y維度
                            0,                 //Sel1維度
                            -1,                //Sel2維度
                            pi.iS,             //Sel1索引
                            -1,                //Sel2索引
                            "I = ",             //Sel1標籤
                            "",                 //Sel2標籤
                            null);              //Time陣列                
                    }
                    else if (gt == ResultGraphForm.GraphType.Type1)
                    {
                        form.SetFormMode(
                            "初始底床高程(m)",   //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            "",                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.XyGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            initialBottomElevation,  //資料
                            ResultGraphForm.CumulativeDistance,                  //X維度
                            ResultGraphForm.DataContent,                  //Y維度
                            0,                 //Sel1維度
                            -1,                 //Sel2維度
                            pi.jS,               //Sel1索引
                            -1,                 //Sel2索引
                            "J = ",              //Sel1標籤
                            "",                 //Sel2標籤
                            null);              //Time陣列                
                    }
                    break;
                case GraphFormMode.Contour:
                    form.SetFormMode(
                        "初始底床高程(m)",   //視窗標題
                        0, initialBottomElevation.GetLength(0),       //行數(左右有幾行)
                        0, initialBottomElevation.GetLength(1),       //列數(上下有幾列)
                        -1, -1,       //k
                        -1, -1,       //t
                        "",                 //表格名稱
                        "",                 //行標題(顯示於上方)
                        "",                 //列標題(顯示於左方)
                        ResultGraphForm.ResultGraphType.ContourGraph, //表格形式
                        false,              //不須行數字
                        false,              //不須列數字
                        initialBottomElevation,  //資料
                        ResultGraphForm.CumulativeDistance,                  //X維度
                        ResultGraphForm.DataContent,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        -1,               //Sel1索引
                        -1,                 //Sel2索引
                        "",              //Sel1標籤
                        "",                 //Sel2標籤
                        null);              //Time陣列                
                    break;
            }

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
            //GnuPlot.Close();
            

        }
        private void GenerateTimeContourResultGraph(String key, String title, String outputfile, List<double> timeList, ref double[, ,] array)
        {


        }

        private void GenerateTimeIJResultGraph(String key, String title, String outputfile, List<double> timeList, ref double[, ,] array)
        {

            ResultGraphForm form = new ResultGraphForm();
            PosInfo pi = new PosInfo();              

            switch (graphFormMode)
            {
                case GraphFormMode.XY:
                    ResultGraphForm.GraphType gt = GetGraphType(ref pi);
                    if (gt >= ResultGraphForm.GraphType.Type01)
                    {
                        MessageBox.Show("請輸入正確位置/時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!ParsingTimeIJResult(resedTimeList, key, outputfile, ref array))
                    {
                        MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    /* I|J|T|K|X|Y|Sel1|Sel2|Mode| Data  |
                     * 1|M|1|-|D|U| I  | -  | 2  |3D IJT |
                     * -+-+-+-+-+-+----+----+----+-------+
                     * M|1|1|-|D|U| J  | -  | 3  |3D IJT |
                     * -+-+-+-+-+-+----+----+----+-------+
                     * 1|1|M|-|T|U| J  | -  | 4  |3D IJT |<<<<<NOW*/
                    if (gt == ResultGraphForm.GraphType.Type2)
                    {
                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            key,                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.XyGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            array,              //資料
                            ResultGraphForm.CumulativeDistance,                  //X維度
                            ResultGraphForm.DataContent,                  //Y維度
                            0,                 //Sel1維度
                            -1,                 //Sel2維度
                            pi.iS,               //Sel1索引
                            -1,                 //Sel2索引
                            "I = ",              //Sel1標籤
                            "",                 //Sel2標籤
                             timeSel,             //Time選取索引陣列                
                            timeList.ToArray());                //Time陣列                
                    }
                    else if (gt == ResultGraphForm.GraphType.Type3)
                    {
                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            key,                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.XyGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            array,              //資料
                            ResultGraphForm.CumulativeDistance,                  //X維度
                            ResultGraphForm.DataContent,                  //Y維度
                            1,                 //Sel1維度
                            -1,                 //Sel2維度
                            pi.jS,               //Sel1索引
                            -1,                 //Sel2索引
                            "J = ",              //Sel1標籤
                            "",                 //Sel2標籤
                            timeSel,             //Time選取索引陣列                
                            timeList.ToArray());              //Time陣列                
                    }
                    else if (gt == ResultGraphForm.GraphType.Type4)
                    {
                        /*
                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            "",                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.XyGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            array,              //資料
                            ResultGraphForm.Time,                         //X維度
                            ResultGraphForm.DataContent,                  //Y維度
                            1,                  //Sel1維度
                            -1,                 //Sel2維度
                            pi.jS,              //Sel1索引
                            -1,                 //Sel2索引
                            "J = ",             //Sel1標籤
                            "",                 //Sel2標籤
                            timeSel,             //Time選取索引陣列                
                            timeList.ToArray());              //Time陣列       
                         * */
                    }
                   break;
                case GraphFormMode.Contour:
                   if (timeSel == null || timeSel.Length != 1)
                    {
                        MessageBox.Show("請輸入正確時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }     
                    pi.tS = timeSel[0];
                    pi.tE = timeSel[timeSel.Length - 1] + 1;

                    if (!ParsingTimeIJResult(resedTimeList, key, outputfile, ref array))
                    {
                        MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                   form.SetFormMode(
                            title,              //視窗標題
                            0, array.GetLength(0),       //i
                            0, array.GetLength(1),       //j
                            -1, -1,              //k
                            pi.tS, pi.tE,       //t
                            key,                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.ContourGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            array,              //資料
                            ResultGraphForm.CumulativeDistance,                         //X維度
                            ResultGraphForm.DataContent,                  //Y維度
                            -1,                  //Sel1維度
                            -1,                 //Sel2維度
                            -1,              //Sel1索引
                            -1,                 //Sel2索引
                            "",             //Sel1標籤
                            "",                 //Sel2標籤
                            timeSel,             //Time選取索引陣列                
                            timeList.ToArray());              //Time陣列       
                   break;
            }

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
            
        }

        private void SaveToCsv(String file, double[,] data)
        {
            string outputFile = Program.GetProjectFileWorkingPath() + "\\" + file;
            FileMode fm = (File.Exists(outputFile)) ? FileMode.Open : FileMode.Create;
            using (FileStream fs = new FileStream(outputFile, fm, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                for (int i = 0; i < data.GetLength(1); ++i)
                {
                    for(int j =0; j < data.GetLength(0); ++j)
                    {
                        sw.Write(data[j, i]);
                        sw.Write(',');
                    }
                    sw.Write("\n");
                }
                sw.Close();
                fs.Close();
            }
        }

        private void GenerateTimeIJResultTable(String key, String title, String outputfile, List<double> timeList, ref double[, ,] array)
        {
            int iStart = 0, iEnd = 0, jStart = 0, jEnd = 0;
            if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref iStart, ref iEnd))
            {
                MessageBox.Show("請輸入正確的I位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref jStart, ref jEnd))
            {
                MessageBox.Show("請輸入正確的J位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            } 
            
            if (timeSel == null)
            {
                MessageBox.Show("請選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PosInfo pi = new PosInfo();
            TableType t = GetTableType(ref pi);
            if (t >= TableType.Type1234)
            {
                MessageBox.Show("請輸入正確位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!ParsingTimeIJResult(timeList, key, outputfile, ref array))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = 0;
            //double[,,] data = null;
            ResultTableForm form = new ResultTableForm();

            switch (t)
            {
                case TableType.Type1:
                    index = timeSel[0];
                    form.SetFormMode(
                        title + " T=" + resedTimeList[index].ToString(),    //視窗標題
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.iS, pi.iE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "I=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.InitialBottomElevation, //表格形式
                        48,                 //儲存格寬度
                        64,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        1,                  //X維度
                        0,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,          //Sel1索引
                        -1,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        null);              //Time陣列
                    break;
                case TableType.Type2:
                    index = pi.iS;
                    form.SetFormMode(
                        title + " I=" + (index + 1).ToString(),    //視窗標題
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.tS, pi.tE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "T=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.InitialBottomElevation, //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        1,                  //X維度
                        3,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,              //Sel1索引
                        -1,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        timeList.ToArray());              //Time陣列
                    break;
                case TableType.Type3:
                    index = pi.jS;
                    form.SetFormMode(
                        title + " J=" + (index + 1).ToString(),    //視窗標題
                        pi.iS, pi.iE,       //行數(左右有幾行)
                        pi.tS, pi.tE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "I=",                 //行標題(顯示於上方)
                        "T=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.InitialBottomElevation, //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        0,                  //X維度
                        3,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,              //Sel1索引
                        -1,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        timeList.ToArray());              //Time陣列
                    break;
                case TableType.Type4:
                    index = pi.jS;
                    form.SetFormMode(
                        title + " J=" + (index + 1).ToString(),    //視窗標題
                        pi.iS, pi.iE,       //行數(左右有幾行)
                        pi.tS, pi.tE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "I=",                 //行標題(顯示於上方)
                        "T=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.InitialBottomElevation, //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        0,                  //X維度
                        3,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,              //Sel1索引
                        -1,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        timeList.ToArray());              //Time陣列
                    break;
                default:
                    break;
            }
            /*
            else if (t == TableType.mIsJmT)
            {
                //timeIndex = FoundTimeSelInList(timeSel[0]);
                data = new double[pi.GetICount(), timeSel.Length];
                for (int ti = 0; ti < timeSel.Length; ++ti)
                    for (int i = pi.iS; i < pi.iE; ++i)
                        data[i, ti] = array[timeSel[ti], pi.jS, i];

                form.SetFormMode(
                    title + " J=" + (pi.jS + 1).ToString(),   //視窗標題
                    pi.iS, pi.iE - 1,       //行數(左右有幾行)
                    timeSel[0], timeSel[timeSel.Length - 1],       //列數(上下有幾列)
                    "",                 //表格名稱
                    "",                 //行標題(顯示於上方)
                    "",                 //列標題(顯示於左方)
                    ResultTableForm.ResultTableType.InitialBottomElevation, //表格形式
                    48,                 //儲存格寬度
                    64,                 //列標題寬度
                    true,               //保留
                    false,              //不須行數字
                    false,              //不須列數字
                    data//資料
                    );
            }
            else if (t == TableType.mIsJmT)
            {
                //timeIndex = FoundTimeSelInList(timeSel[0]);
                data = new double[pi.GetJCount(), timeSel.Length];
                for (int ti = 0; ti < timeSel.Length; ++ti)
                    for (int j = pi.jS; j < pi.jE; ++j)
                        data[j, ti] = array[timeSel[ti], j, pi.iS];

                form.SetFormMode(
                    title + " I=" + (pi.iS + 1).ToString(),   //視窗標題
                    pi.jS, pi.jE - 1,       //行數(左右有幾行)
                    timeSel[0], timeSel[timeSel.Length - 1],       //列數(上下有幾列)
                    "",                 //表格名稱
                    "",                 //行標題(顯示於上方)
                    "",                 //列標題(顯示於左方)
                    ResultTableForm.ResultTableType.InitialBottomElevation, //表格形式
                    48,                 //儲存格寬度
                    64,                 //列標題寬度
                    true,               //保留
                    false,              //不須行數字
                    false,              //不須列數字
                    data//資料
                    );
            }
 */
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
                   
        }

        private double[] GetLineDouble(string l, int size)
        {
            int count = l.Length / size;
            if(count == 0)
                return null;

            double[] r = new double[count];
            for (int i = 0; i < count; ++i)
            {
                r[i] = Convert.ToDouble(l.Substring(size * i, size).Trim());
            }
            return r;
        }

        private double[,,] initialBottomElevation = null;
        private bool ParsingInitialBottomElevationResult()
        {
            string outputFile = Program.GetProjectFileFullPath() + ".working\\resed.O";
            if (!File.Exists(outputFile))
            {
                return false;
            }
            // Read the file and display it line by line.
            StreamReader f = new System.IO.StreamReader(outputFile);
            string line;
            int count = 0;
            bool found = false;

            if (initialBottomElevation == null)
            {
                initialBottomElevation = new double[p.inputGrid.GetI, p.inputGrid.GetJ, 1];
            }

            while ((line = f.ReadLine()) != null)
            {
                if (!found && line.StartsWith(" INI-ZB"))
                {
                    found = true;
                    count = 0;
                    continue;
                }

                if (found)
                {

                    double[] ar = GetLineDouble(line, 10);
                    for(int i = 0; i < ar.Length; ++i)
                    {
                        initialBottomElevation[count, i, 0] = ar[i];
                    }

                    if (++count >= p.inputGrid.GetI)
                    {
                        break;
                    }
                }
            }
            return true;
        }

        private int FoundTimeSelInList(List<double> timeList, double d)
        {
            for(int i = 0; i < timeList.Count; ++i)
            {
                if (timeList[i] == d)
                    return i;
            }
            return -1;
        }

        private double[, ,] depthAverageFlowSpeedU = null;
        private double[, ,] depthAverageFlowSpeedV = null;
        private double[, ,] tomd1 = null;
        private double[, ,] zs = null;
        private double[, ,] depth = null;
        private double[, ,] usDischarge = null;
        private double[, ,] vsDischarge = null;
        private double[, ,] zbed = null;
        private double[, ,] mudconc = null;
        private double[, ,] beta = null;

        private bool ParsingTimeIJResult(/*TableType t, PosInfo pi, */List<double> timeList, String keyword, String outputfile, ref double[, ,] result)
        {
            string outputFile = Program.GetProjectFileFullPath() + ".working\\" + outputfile;
            if (!File.Exists(outputFile))
            {
                return false;
            }
            // Read the file and display it line by line.
            StreamReader f = new System.IO.StreamReader(outputFile);
            string line;
            int count = 0;
            bool foundTime = false;
            bool found = false;

            if (result == null)
            {
                result = new double[p.inputGrid.GetI, p.inputGrid.GetJ, timeList.Count];
            }

            int ti = 0;
            while ((line = f.ReadLine()) != null)
            {
                if (!foundTime && line.StartsWith("  TIME="))
                {
                    if (timeList[timeSel[ti]] == Convert.ToDouble(line.Substring(9, 16).Trim()))
                    {
                        foundTime = true;
                        continue;
                    }
                }

                if (foundTime && !found && line.StartsWith(keyword))
                {
                    found = true;
                    count = 0;
                    continue;
                }

                //int timeIndex = FoundTimeSelInList(timeSel[0]);
                int timeIndex = timeSel[ti];
                if (foundTime && found)
                {

                    double[] ar = GetLineDouble(line, 10);
                    for (int i = 0; i < ar.Length; ++i)
                    {
                        result[count, i, timeIndex] = ar[i];
                    }

                    if (++count >= p.inputGrid.GetI)
                    {
                        if (++ti == timeSel.Length)
                        {
                            break;
                        }
                        foundTime = false;
                        found = false;
                    }
                }
            }   //while ((line = f.ReadLine()) != null)
            return true;
        }

        private bool ParsingTime(String outputfile, ref List<double> timeList)
        {
            string outputFile = Program.GetProjectFileFullPath() + ".working\\" + outputfile;
            if (!File.Exists(outputFile))
            {
                MessageBox.Show("無法找到輸出檔" + outputfile, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            // Read the file and display it line by line.
            StreamReader f = null;
            try
            {
                f = new System.IO.StreamReader(outputFile);
            }
            catch
            {
                MessageBox.Show("無法開啟輸出檔" + outputfile, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            string line;
            int count = 0;

            if (timeList == null)
            {
                timeList = new List<double>();
            }
            else
            {
                timeList.Clear();
            }

            while ((line = f.ReadLine()) != null)
            {
                if (line.StartsWith("  TIME="))
                {
                    timeList.Add(Convert.ToDouble(line.Substring(9, 16).Trim()));
                    ++count;
                }
            }
            return true;
        }

        private bool GetPosRange(CheckBox chk, int max, TextBox txt, ref int start, ref int end)
        {
            bool useAll = chk.Checked;
            
            if (useAll)
            {
                start = 0;
                end = max;
            }
            else
            {
                string[] iPos = txt.Text.Split(':');
                if (iPos.Length < 2)
                {
                    try
                    {
                        start = Convert.ToInt32(txt.Text) - 1;
                        end = start + 1;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        start = Convert.ToInt32(iPos[0]) - 1;
                        end = Convert.ToInt32(iPos[1]);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            if (start < 0 || end > max)
            {
                return false;
            }
            return true;
        }

        private void GenerateGraph()
        {
            if (graphFormMode == GraphFormMode.XY)
            {
                switch (param1Cmb.SelectedIndex)
                {
                    case 0: //初始底床高程(m)
                        GenerateInitialBottomElevationGraph();
                        break;
                    case 1: //水深平均流速-U(m/s)
                            //GenerateDepthAverageFlowSpeedUGraph();
                        GenerateTimeIJResultGraph(" U-VELOCITY (M/S)", "水深平均流速-U(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedU);
                        break;
                    case 2: //水深平均流速-V(m/s)
                        GenerateTimeIJResultGraph(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                        break;
                    case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                            //GenerateTimeIJResultGraph(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                        break;
                    case 4: //水深平均流速-UV 合向量(m/s)
                            //GenerateTimeIJResultGraph(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                        break;
                    case 5: //底床剪應力(N/m2)
                        GenerateTimeIJResultGraph(" TOMD1-U", "底床剪應力(N/m²)", "resed.O", resedTimeList, ref tomd1);
                        break;
                    case 6: //水位(m)
                        GenerateTimeIJResultGraph(" ZS (M)", "水位(m)", "resed.O", resedTimeList, ref zs);
                        break;
                    case 7: //水深(m)
                        GenerateTimeIJResultGraph(" DEPTH (M)", "水深(m)", "resed.O", resedTimeList, ref depth);
                        break;
                    case 8://流量-U(cms)
                        GenerateTimeIJResultGraph(" ZS (M)", "流量-U(cms)", "resed.O", resedTimeList, ref usDischarge);
                        break;
                    case 9: //流量-V(cms)
                        GenerateTimeIJResultGraph(" VS-DISCHARGE (M3/S/M)", "流量-V(cms)", "resed.O", resedTimeList, ref vsDischarge);
                        break;
                    case 10: //底床高程(m)
                        GenerateTimeIJResultGraph(" US-DISCHARGE (M3/S/M)", "流量-U(cms)", "resed.O", sedTimeList, ref usDischarge);
                        break;
                    case 11: //沖淤深度(m)
                        GenerateTimeIJResultGraph(" DZBED (M)", "沖淤深度(m)", "SEDoutput.dat", sedTimeList, ref zbed);
                        break;
                    case 12: //水深平均濃度(ppm)
                        GenerateTimeIJResultGraph(" MUDCONC & CONC (-)", "水深平均濃度(ppm)", "SEDoutput.dat", sedTimeList, ref mudconc);
                        break;
                    case 13: //粒徑分佈(%)
                        GenerateTimeIJResultGraph(" BETA (-)", "粒徑分佈(%)", "SEDoutput.dat", sedTimeList, ref beta);
                        break;
                }
            }
            else if (graphFormMode == GraphFormMode.Contour)
            {
                switch (param1Cmb.SelectedIndex)
                {
                    case 0: //初始底床高程(m)
                        GenerateInitialBottomElevationGraph();
                        break;
                    case 1: //水深平均流速-U(m/s)                       
                        GenerateTimeIJResultGraph(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                        break;
                    case 2: //水深平均流速-V(m/s)
                        //GnerateTimeIJResultGraph(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                        break;
                    case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                        //GenerateTimeIJResultGraph(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                        break;
                    case 4: //底床剪應力(N/m2)
                        GenerateTimeIJResultGraph(" TOMD1-U", "底床剪應力(N/m²)", "resed.O", resedTimeList, ref tomd1);
                        break;
                    case 5: //水位(m)
                        GenerateTimeIJResultGraph(" ZS (M)", "水位(m)", "resed.O", resedTimeList, ref zs);
                        break;
                    case 6: //水深(m)
                        GenerateTimeIJResultGraph(" DEPTH (M)", "水深(m)", "resed.O", resedTimeList, ref depth);
                        break;
                    case 7://流量-U(cms)
                        GenerateTimeIJResultGraph(" ZS (M)", "流量-U(cms)", "resed.O", resedTimeList, ref usDischarge);
                        break;
                    case 8: //流量-V(cms)
                        GenerateTimeIJResultGraph(" VS-DISCHARGE (M3/S/M)", "流量-V(cms)", "resed.O", resedTimeList, ref vsDischarge);
                        break;
                    case 9: //底床高程(m)
                        GenerateTimeIJResultGraph(" US-DISCHARGE (M3/S/M)", "流量-U(cms)", "resed.O", sedTimeList, ref usDischarge);
                        break;
                    case 10: //沖淤深度(m)
                        GenerateTimeIJResultGraph(" DZBED (M)", "沖淤深度(m)", "SEDoutput.dat", sedTimeList, ref zbed);
                        break;
                    case 11: //水深平均濃度(ppm)
                        GenerateTimeIJResultGraph(" MUDCONC & CONC (-)", "水深平均濃度(ppm)", "SEDoutput.dat", sedTimeList, ref mudconc);
                        break;
                    case 12: //粒徑分佈(%)
                        GenerateTimeIJResultGraph(" BETA (-)", "粒徑分佈(%)", "SEDoutput.dat", sedTimeList, ref beta);
                        break;
                }
            }
        }

        ResultTimeSelForm.ResultTimeType GetTimeSelectionType()
        {
            if(drawType == Param1Type.ParamTable)
            {
                switch(tableType)
                {
                    case TableType.Type0:
                        break;
                    case TableType.Type1234:
                    case TableType.Type2:
                    case TableType.Type3:
                    case TableType.Type4:
                    case TableType.Type56789A:
                    case TableType.Type8:
                    case TableType.Type9:
                    case TableType.TypeA:
                        return ResultTimeSelForm.ResultTimeType.MultiSelect;
                    case TableType.Type1:
                    case TableType.Type5:
                    case TableType.Type6:
                    case TableType.Type7:
                        return ResultTimeSelForm.ResultTimeType.SingleSelect;
                    default:
                        break;
                }
            }
            else
            {

            }
            return ResultTimeSelForm.ResultTimeType.SingleSelect;
        }

        private int[] timeSel = null;
        private void timeBtn_Click(object sender, EventArgs e)
        {
            ResultTimeSelForm form = new ResultTimeSelForm();

            switch (param1Cmb.SelectedIndex)
            {
                case 1: //水深平均流速-U(m/s)
                case 2: //水深平均流速-V(m/s)
                case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                case 4: //底床剪應力(N/m2)   //⁰¹²³⁴⁵⁶⁷⁸⁹
                case 5: //水位(m)
                case 6: //水深(m)
                case 7: //流量-U(cms)
                case 8: //流量-V(cms)
                    form.SetFormMode("", GetTimeSelectionType(), resedTimeList);
                    break;
                case 9: //底床高程(m)
                case 10: //沖淤深度(m)
                case 11: //水深平均濃度(ppm)
                case 12: //粒徑分佈(%)
                    form.SetFormMode("", GetTimeSelectionType(), sedTimeList);
                    break;
                case 13:
                case 14:
                case 15:
                case 116:
                    form.SetFormMode("", GetTimeSelectionType(), resedTimeList);
                    break;
                default:
                    break;
            }


            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                timeSel = form.GetTimeSelect().ToArray();
            }
        }

        private void posIchk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            posITxt.Enabled = !chk;
            PosInfo pi = new PosInfo();
            TableType t = GetTableType(ref pi);
            timeGrp.Enabled = TimeGroupEnable();
        }

        private void posJchk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            posJTxt.Enabled = !chk;
            PosInfo pi = new PosInfo();
            TableType t = GetTableType(ref pi);
            timeGrp.Enabled = TimeGroupEnable();
        }

        private void posITxt_TextChanged(object sender, EventArgs e)
        {
            PosInfo pi = new PosInfo();
            TableType t = GetTableType(ref pi);
            timeGrp.Enabled = TimeGroupEnable();
        }

        private void posJTxt_TextChanged(object sender, EventArgs e)
        {
            PosInfo pi = new PosInfo();
            TableType t = GetTableType(ref pi);
            timeGrp.Enabled = TimeGroupEnable();
        }

        private bool TimeGroupEnable()
        {
            return true;
            //PosInfo pi = new PosInfo();
            //TableType t = GetTableSize(ref pi);
            //if (param1Cmb.SelectedIndex == 0)
            //    return false;

            //return t != TableType.Unknown;
        }

        private class PosInfo
        {
            public int iS = -1;
            public int iE = -1;
            public int jS = -1;
            public int jE = -1;
            public int tS = -1;
            public int tE = -1;
            public int kS = -1;
            public int kE = -1;

            public int GetICount() { return iE - iS; }
            public int GetJCount() { return jE - jS; }
            public int GetTCount() { return tE - tS; }
            public int GetKCount() { return kE - kS; }
        };

        private TableType GetTableType(ref PosInfo pi)
        {
            switch(tableType)
            {
                case TableType.Type1234:
                    if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref pi.iS, ref pi.iE) ||
                        !GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref pi.jS, ref pi.jE) ||
                        timeSel == null)
                    {   //I或J或T未輸入
                        return TableType.Type1234;
                    }
                    pi.tS = timeSel[0];
                    pi.tE = timeSel[timeSel.Length - 1] + 1;
                    if (pi.GetICount() > 1 && pi.GetJCount() > 1 && pi.GetTCount() == 1)
                    {   //T固定
                        return TableType.Type1;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() == 1 && pi.GetTCount() > 1)
                    {   //IJ固定
                        return TableType.Type4;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() > 1)
                    {   //I固定
                        return TableType.Type2;
                    }
                    else if (pi.GetICount() > 1 && pi.GetJCount() == 1)
                    {   //J固定
                        return TableType.Type3;
                    }
                    return TableType.Type1234;
                case TableType.Type56789A:
                    if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref pi.iS, ref pi.iE) ||
                        !GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref pi.jS, ref pi.jE) ||
                        !GetPosRange(posKchk, p.inputGrid.GetI, posKTxt, ref pi.kS, ref pi.kE) ||
                        timeSel == null)
                    {   //I或J或T或K未輸入
                        return TableType.Type56789A;
                    }
                    pi.tS = timeSel[0];
                    pi.tE = timeSel[timeSel.Length - 1] + 1;
                    if (pi.GetICount() > 1 && pi.GetJCount() > 1 && pi.GetTCount() == 1 && pi.GetKCount() == 1)
                    {   //KT固定
                        return TableType.Type5;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() > 1 && pi.GetTCount() == 1 && pi.GetKCount() > 1)
                    {   //IT固定
                        return TableType.Type6;
                    }
                    else if (pi.GetICount() > 1 && pi.GetJCount() == 1 && pi.GetTCount() == 1 && pi.GetKCount() > 1)
                    {   //JT固定
                        return TableType.Type7;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() > 1 && pi.GetTCount() > 1 && pi.GetKCount() == 1)
                    {   //IK固定
                        return TableType.Type8;
                    }
                    else if (pi.GetICount() > 1 && pi.GetJCount() == 1 && pi.GetTCount() > 1 && pi.GetKCount() == 1)
                    {   //JK固定
                        return TableType.Type9;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() == 1 && pi.GetTCount() > 1 && pi.GetKCount() > 1)
                    {   //IJ固定
                        return TableType.TypeA;
                    }
                    return TableType.Type56789A;
            }
            return TableType.TypeUnknown;
        }
        private ResultGraphForm.GraphType GetGraphType(ref PosInfo pi)
        {
            switch (graphType)
            {
                case ResultGraphForm.GraphType.Type01:
                    if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref pi.iS, ref pi.iE) ||
                        !GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref pi.jS, ref pi.jE))
                    {   //I或J未輸入
                        return ResultGraphForm.GraphType.Type01;
                    }

                    if (pi.GetICount() == 1 && pi.GetJCount() > 1)
                    {   //I固定
                        return ResultGraphForm.GraphType.Type0;
                    }
                    else if (pi.GetICount() > 1 && pi.GetJCount() == 1)
                    {   //J固定
                        return ResultGraphForm.GraphType.Type1;
                    }
                    return ResultGraphForm.GraphType.Type01;
                case ResultGraphForm.GraphType.Type234:
                    if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref pi.iS, ref pi.iE) ||
                        !GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref pi.jS, ref pi.jE) ||
                        timeSel == null)
                    {   //I或J或T未輸入
                        return ResultGraphForm.GraphType.Type234;
                    }
                    pi.tS = timeSel[0];
                    pi.tE = timeSel[timeSel.Length - 1] + 1;
                    if (pi.GetICount() == 1 && pi.GetJCount() > 1 && pi.GetTCount() == 1)
                    {   //IT固定
                        return ResultGraphForm.GraphType.Type2;
                    }
                    else if (pi.GetICount() > 1 && pi.GetJCount() == 1 && pi.GetTCount() == 1)
                    {   //JT固定
                        return ResultGraphForm.GraphType.Type3;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() == 1 && pi.GetTCount() > 1)
                    {   //IJ固定
                        return ResultGraphForm.GraphType.Type4;
                    }
                    return ResultGraphForm.GraphType.Type234;
            }
            return ResultGraphForm.GraphType.TypeUnknown;
        }
        /*
        enum TableType
        {
            Unknown,
            sImJnT,       //single I, multiple J, no Time
            sImJsT,       //single I, multiple J, single Time
            sImJmT,       //single I, multiple J, multiple Time

            mIsJnT,       //multiple I, single J, no Time
            mIsJsT,       //multiple I, single J, single Time
            mIsJmT,       //multiple I, single J, multiple Time
            
            sIsJmT,

            mImJnT,
            mImJsT,
            mImJmT,

            //IJ
        };
        */

        private void graphType1Rdo_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked && graphFormMode != GraphFormMode.XY)
            {
                graphFormMode = GraphFormMode.XY;
                InitialParam1(drawType);
                UpdateStatus();
            }
        }

        private void graphType2Rdo_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked && graphFormMode != GraphFormMode.Contour)
            {
                graphFormMode = GraphFormMode.Contour;
                InitialParam1(drawType);
                UpdateStatus();
            }
        }

        private void graphType3Rdo_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked && graphFormMode != GraphFormMode.Vector)
            {
                graphFormMode = GraphFormMode.Vector;
                InitialParam1(drawType);
                UpdateStatus();
            }
        }

        private void graphType4Rdo_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked && graphFormMode != GraphFormMode.ContouWithVector)
            {
                graphFormMode = GraphFormMode.ContouWithVector;
                InitialParam1(drawType);
                UpdateStatus();
            }
        }

        private void animChk_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void timeChk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (sender as CheckBox);
            timeBtn.Enabled = !c.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            //sb.AppendFormat("{0,8} {1, 8} {2, 8} {3, 8} {4, 8} {5, 8}\n", _i.ToString(), _j.ToString(), 
            //    maxX.ToString(), minX.ToString(), maxY.ToString(), minY.ToString());
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            for (int i = 0; i < p.inputGrid.GetI; ++i)
            {
                for (int j = 0; j < p.inputGrid.GetJ; ++j)
                {
                    if (i + 1 < p.inputGrid.GetI)
                    {
                        sb.AppendFormat("{0} {1} {2}\n", p.inputGrid.inputCoor[i, j].x.ToString(), p.inputGrid.inputCoor[i, j].y.ToString(), p.inputGrid.inputCoor[i, j].z.ToString());
                        sb.AppendFormat("{0} {1} {2}\n\n", p.inputGrid.inputCoor[i + 1, j].x.ToString(), p.inputGrid.inputCoor[i + 1, j].y.ToString(), p.inputGrid.inputCoor[i + 1, j].z.ToString());
                    }

                    if (j + 1 < p.inputGrid.GetJ)
                    {
                        sb.AppendFormat("{0} {1} {2}\n", p.inputGrid.inputCoor[i, j].x.ToString(), p.inputGrid.inputCoor[i, j].y.ToString(), p.inputGrid.inputCoor[i, j].z.ToString());
                        sb.AppendFormat("{0} {1} {2}\n\n", p.inputGrid.inputCoor[i, j + 1].x.ToString(), p.inputGrid.inputCoor[i, j + 1].y.ToString(), p.inputGrid.inputCoor[i, j + 1].z.ToString());
                    }
                    sb2.AppendFormat("{0} {1} {2}\n", p.inputGrid.inputCoor[i, j].x.ToString(), p.inputGrid.inputCoor[i, j].y.ToString(), p.inputGrid.inputCoor[i, j].z.ToString());
                    sb3.AppendFormat("{0} {1}\n", p.inputGrid.inputCoor[i, j].x.ToString(), p.inputGrid.inputCoor[i, j].y.ToString());
                }
                sb2.AppendFormat("\n");
                sb3.AppendFormat("\n");
            }

            using (StreamWriter outfile = new StreamWriter(@"_test.txt"))
            {
                outfile.Write(sb.ToString());
                outfile.Close();
            }
            using (StreamWriter outfile = new StreamWriter(@"_test2.txt"))
            {
                outfile.Write(sb2.ToString());
                outfile.Close();
            }
            using (StreamWriter outfile = new StreamWriter(@"_test3.txt"))
            {
                outfile.Write(sb3.ToString());
                outfile.Close();
            }


            double x1, x2, y1, y2;
            if ((p.inputGrid.GetMaxX - p.inputGrid.GetMinX) > (p.inputGrid.GetMaxY - p.inputGrid.GetMinY))
            {
                x1 = p.inputGrid.GetMinX;
                x2 = p.inputGrid.GetMaxX;
                y1 = p.inputGrid.GetMinY - ((p.inputGrid.GetMaxX - p.inputGrid.GetMinX) - (p.inputGrid.GetMaxY - p.inputGrid.GetMinY)) / 2;
                y2 = p.inputGrid.GetMaxY + ((p.inputGrid.GetMaxX - p.inputGrid.GetMinX) - (p.inputGrid.GetMaxY - p.inputGrid.GetMinY)) / 2;
            }
            else
            {
                y1 = p.inputGrid.GetMinY;
                y2 = p.inputGrid.GetMaxY;
                x1 = p.inputGrid.GetMinX - ((p.inputGrid.GetMaxY - p.inputGrid.GetMinY) - (p.inputGrid.GetMaxX - p.inputGrid.GetMinX)) / 2;
                x2 = p.inputGrid.GetMaxX + ((p.inputGrid.GetMaxY - p.inputGrid.GetMinY) - (p.inputGrid.GetMaxX - p.inputGrid.GetMinX)) / 2;
            }

            //string param = "";
            //GnuPlot.Set("title \"world.dat plotted with filledcurves\"");
            //GnuPlot.Set("size ratio 1");
            //GnuPlot.Set("format x \"\"");
            //GnuPlot.Set("format y \"\"");
            //GnuPlot.Set("grid layerdefault linewidth 0.5");
            //GnuPlot.Set("object  1 rect from graph 0, 0 to graph 1, 1 behind fc  rgb \"#afffff\" fillstyle solid 1.00 border -1");
            //param = "xrange[" + x1.ToString() + " : " + x2.ToString() + "]";
            //GnuPlot.Set(param);
            //param = "yrange[" + y1.ToString() + " : " + y2.ToString() + "]";
            //GnuPlot.Set(param);
            //GnuPlot.Set("lmargin  0");
            ////GnuPlot.Set("contour");
            ////GnuPlot.Plot(@"G:\_test.txt", "with filledcurve notitle fs solid 1.0 lc rgb 'dark-goldenrod'");
            //GnuPlot.SPlot(@"_test2.txt", "with filledcurve notitle fs solid 1.0 lc rgb 'dark-goldenrod'");


            /* 可行的contour
            set view map
            set xrange[270301.395 : 270903.805]
            set yrange[2732858.0 : 2733461.91]
            set size ratio 1
            set object 1 rect from graph 0, graph 0 to graph 1, graph 1 back
            set object 1 rect fc rgb "black" fillstyle solid 1.0
            splot 'G:\_test2.txt' using 1:2:3 with points pointtype 5 pointsize 1 palette linewidth 30
            */

            /* 可行的contour2
            set surface
            set contour surface
            set view 60, 30, 1, 1
            set clabel '%8.2f'
            set key right
            set title 'Graph Title'
            set xlabel 'vss'
            set ylabel 'sbb'
            set zlabel 'closs'
            #set term png
            #set output 'jf.png'
            splot '_test2.txt' using 2:1:3 notitle with pm3d
            */
            //string[] setting = { 
            //                   "surface",
            //                   "contour surface",
            //                   "ticslevel 0",   //the zero of Z-Axis moves to on the XY plane.
            //                   //"ratio 1",
            //                   "view 0, 0, 1, 1",       //default view is 60 rot_x, 30 rot_z, 1 scale, 1 scale_z
            //                   "cntrparam levels 10",
            //                   "clabel \'%8.2f\'",
            //                   "key right",
            //                   "title \'Graph Title\'",
            //                   "xlabel \'vss\'",
            //                   "ylabel \'sbb\'",
            //                   "zlabel \'closs\'",
            //                   }; 
            string[] setting = { 
                               "pm3d map",
                               };
            GnuPlot.Set(setting);
            //param = "xrange[" + x1.ToString() + " : " + x2.ToString() + "]";
            //GnuPlot.Set(param);
            //param = "yrange[" + y1.ToString() + " : " + y2.ToString() + "]";
            //GnuPlot.Set(param);
            //param = "zrange [0. : 500 ]";
            //GnuPlot.Set(param);

            GnuPlot.SPlot(@"_test2.txt", "with pm3d");
        }
        //TableType tableType = TableType.Unknown;
    }
}

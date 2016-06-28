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
            if (drawType == Param1Type.ParamTable)
            { //表格
                formGrp.Enabled = false;    //表格全部不需形式選項
                paramGrp.Enabled = true;    //表格全部需要參數選項
                param1Cmb.Enabled = true;   //表格全部不需第二參數
                param2Cmb.Enabled = false;  //表格全部不需第二參數
                posGrp.Enabled = true;      //表格全部需要位置選項
                poIPanel.Enabled = true;
                poJPanel.Enabled = true;
                poKPanel.Enabled = true;
                sedimentSizeGrp.Enabled = false;    //泥砂粒徑選項先關掉
                timeGrp.Enabled = true;     //時間選項先打開
                axisGrp.Enabled = false;    //座標軸選項先關掉

                switch (param1Cmb.SelectedIndex)
                {
                    case 0: //初始底床高程(m)
                        timeGrp.Enabled = false;    //初始底床不需時間選項
                        poKPanel.Enabled = false;
                        break;
                    case 1:     //水深平均流速-U(m/s)
                    case 2:     //水深平均流速-V(m/s)
                    case 3:     //水深平均流速-UV 合向量的絕對值(m/s)
                    case 4:     //底床剪應力(N/m2)
                    case 5:     //水位(m)
                    case 6:     //水深(m)
                    case 7:     //流量-U(cms)
                    case 8:     //流量-V(cms)
                    case 9:     //底床高程(m)
                    case 10:    //沖淤深度(m)
                    case 11:    //水深平均濃度(ppm)
                    case 12:    //粒徑分佈(%)
                        poKPanel.Enabled = false;
                        break;
                    case 13: //流速-U(m/s)
                    case 14: //流速-V(m/s)
                    case 15: //流速-W(m/s)
                        poKPanel.Enabled = false;
                        break;
                    case 16: //濃度(ppm) 
                        poKPanel.Enabled = false;
                        sedimentSizeGrp.Enabled = true;
                        break;
                }
            }
            else if (drawType == Param1Type.ParamGraph1)
            {   //圖形
                formGrp.Enabled = true;     //圖形全部需要形式選項
                paramGrp.Enabled = true;    //表格全部需要參數選項
                param1Cmb.Enabled = true;   //表格全部需第一參數
                param2Cmb.Enabled = false;  //先關閉參數二
                posGrp.Enabled = true;      //表格全部需要位置選項
                poIPanel.Enabled = true;
                poJPanel.Enabled = true;
                poKPanel.Enabled = true;
                sedimentSizeGrp.Enabled = false;    //泥砂粒徑選項先關掉
                timeGrp.Enabled = true;     //時間選項先打開
                axisGrp.Enabled = false;    //座標軸選項先關掉

                if (graphFormMode == GraphFormMode.None)
                {   //尚未選取，選項全部關閉
                    paramGrp.Enabled = false;
                    posGrp.Enabled = false;
                    sedimentSizeGrp.Enabled = false;
                    timeGrp.Enabled = false;
                    axisGrp.Enabled = false;
                }
                else if(graphFormMode == GraphFormMode.XY)
                {   //折線/散佈圖
                    paramGrp.Enabled = true;    //打開參數選項
                    posGrp.Enabled = true;      //需要位置選項
                    sedimentSizeGrp.Enabled = false;    //泥砂粒徑選項先關掉
                    timeGrp.Enabled = true;     //時間選項先打開
                    axisGrp.Enabled = false;    //座標軸選項先關掉
                    switch (param1Cmb.SelectedIndex)
                    {
                        case 0: //初始底床高程(m)
                            timeGrp.Enabled = false;    //初始底床不需時間選項
                            poKPanel.Enabled = false;
                            break;
                        case 1:     //水深平均流速-U(m/s)
                        case 2:     //水深平均流速-V(m/s)
                        case 3:     //水深平均流速-UV 合向量的絕對值(m/s)
                        case 4:     //底床剪應力(N/m2)
                        case 5:     //水位(m)
                        case 6:     //水深(m)
                        case 7:     //流量-U(cms)
                        case 8:     //流量-V(cms)
                        case 9:     //底床高程(m)
                        case 10:    //沖淤深度(m)
                        case 12:    //粒徑分佈(%)
                            poKPanel.Enabled = false;
                            break;
                        case 13:    //流速-U(m/s)
                        case 14:    //流速-V(m/s)
                        case 15:    //流速-W(m/s)
                            poKPanel.Enabled = true;
                            break;
                        case 11:    //水深平均濃度(ppm)
                        case 16:    //濃度(ppm) 
                            poKPanel.Enabled = true;
                            sedimentSizeGrp.Enabled = true;
                            break;
                        default:
                            break;
                    }
                }
                else if (graphFormMode == GraphFormMode.Contour)
                {   //等值線圖
                    paramGrp.Enabled = true;    //打開參數選項
                    posGrp.Enabled = true;      //不需要位置選項
                    poIPanel.Enabled = false;      //不需要位置I選項
                    poJPanel.Enabled = false;      //不需要位置J選項
                    sedimentSizeGrp.Enabled = false;    //泥砂粒徑選項先關掉
                    timeGrp.Enabled = true;     //時間選項先打開
                    axisGrp.Enabled = false;    //座標軸選項先關掉
                    switch (param1Cmb.SelectedIndex)
                    {
                        case 0: //初始底床高程(m)
                            timeGrp.Enabled = false;    //初始底床不需時間選項
                            poKPanel.Enabled = false;
                            break;
                        case 1:     //水深平均流速-U(m/s)
                        case 2:     //水深平均流速-V(m/s)
                        case 3:     //水深平均流速-UV 合向量的絕對值(m/s)
                        case 4:     //底床剪應力(N/m2)
                        case 5:     //水位(m)
                        case 6:     //水深(m)
                        case 7:     //流量-U(cms)
                        case 8:     //流量-V(cms)
                        case 9:     //底床高程(m)
                        case 10:    //沖淤深度(m)
                        case 12:    //粒徑分佈(%)
                            poKPanel.Enabled = false;
                            break;
                        case 13:    //流速-U(m/s)
                        case 14:    //流速-V(m/s)
                        case 15:    //流速-W(m/s)
                            poKPanel.Enabled = true;
                            break;
                        case 11:    //水深平均濃度(ppm)
                        case 16:    //濃度(ppm) 
                            poKPanel.Enabled = true;
                            sedimentSizeGrp.Enabled = true;
                            break;
                        default:
                            break;
                    }
                    //paramGrp.Enabled = true;
                    //posGrp.Enabled = false;
                    //timeGrp.Enabled = (param1Cmb.SelectedIndex != 0);
                    //axisGrp.Enabled = false;
                    //param2Cmb.Enabled = false;
                    //sedimentSizeGrp.Enabled = false;
                    //if (param1Cmb.SelectedIndex > 12)
                    //{
                    //    posGrp.Enabled = true;
                    //    pos_InJPanel.Enabled = false;
                    //    poKPanel.Enabled = true;
                    //}
                    //else
                    //{
                    //    pos_InJPanel.Enabled = true;
                    //    poKPanel.Enabled = false;

                    //}
                    //if (param1Cmb.SelectedIndex == 11 || param1Cmb.SelectedIndex == 16)
                    //{   //5："水深平均濃度"及"濃度"多了1 個維度M
                    //    sedimentSizeGrp.Enabled = true;
                    //}
                    //else
                    //{
                    //    sedimentSizeGrp.Enabled = false;
                    //}
                }
                else if (graphFormMode == GraphFormMode.Vector)
                {
                    paramGrp.Enabled = true;    //打開參數選項
                    posGrp.Enabled = true;      //需要位置選項
                    sedimentSizeGrp.Enabled = false;    //不須泥砂粒徑選項
                    timeGrp.Enabled = true;     //需要時間選項
                    axisGrp.Enabled = false;    //座標軸選項先關掉
                    switch (param1Cmb.SelectedIndex)
                    {
                        case 0: //水深平均流速-UV合向量(m/s)
                            posGrp.Enabled = false;      //不須選位置
                            poKPanel.Enabled = true;    //需要位置K選項
                            break;
                        case 1:     //流速-UW合向量(m/s)，只能畫縱面
                            posGrp.Enabled = true;      //需要選位置
                            poIPanel.Enabled = false;    //只能選J
                            poJPanel.Enabled = true;
                            poKPanel.Enabled = false;   //不須選K位置
                            break;
                        case 2:     //流速-VW合向量(m/s)，只能畫截面
                            posGrp.Enabled = true;      //需要選位置
                            poIPanel.Enabled = true;    //只能選I
                            poJPanel.Enabled = false;
                            poKPanel.Enabled = false;   //不須選K位置
                            break;
                        default:
                            break;
                    }
                }
                else if (graphFormMode == GraphFormMode.ContouWithVector)
                {
                    paramGrp.Enabled = true;
                    posGrp.Enabled = true;
                    timeGrp.Enabled = true;
                    axisGrp.Enabled = false;
                    param2Cmb.Enabled = true;
                    sedimentSizeGrp.Enabled = false;
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
            if(p.IsConstantFlowType())
            {   //“定量流”，則“模擬結果”中“時間”永遠灰階。
                timeGrp.Enabled = false;
                timeBtn.Enabled = false;
                animChk.Enabled = false;
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
            else
            {
                posKLbl.Text = 0.ToString();
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

            if(p.IsMovableBedMode())
            {
                sedimentSizeCmb.Items.Add("總濃度");
                for (int i = 0; i < p.sedimentParticlesNumber; ++i)
                {
                    sedimentSizeCmb.Items.Add(String.Format("粒徑{0}濃度", i + 1));
                }
                sedimentSizeCmb.SelectedIndex = 0;
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
                "水深平均流速-UV合向量(m/s)",   //0
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
                    case 1:     //水深平均流速-U(m/s)
                    case 2:     //水深平均流速-V(m/s)
                    case 3:     //水深平均流速-UV 合向量的絕對值(m/s)
                    case 4:     //底床剪應力(N/m2)
                    case 5:     //水位(m)
                    case 6:     //水深(m)
                    case 7:     //流量-U(cms)
                    case 8:     //流量-V(cms)
                    case 9:     //底床高程(m)
                    case 10:    //沖淤深度(m)
                    case 11:    //水深平均濃度(ppm)
                    case 12:    //粒徑分佈(%)
                        tableType = TableType.Type1234;
                        break;
                    case 13: //流速-U(m/s)
                    case 14: //流速-V(m/s)
                    case 15: //流速-W(m/s)
                    case 16: //濃度(ppm) 
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
                    case 4: //底床剪應力(N/m2)
                    case 5: //水位(m)
                    case 6: //水深(m)
                    case 7: //流量-U(cms)
                    case 8: //流量-V(cms)
                    case 9: //底床高程(m)
                    case 10: //沖淤深度(m)
                    case 11: //水深平均濃度(ppm)
                    case 12: //粒徑分佈(%)
                        graphType = ResultGraphForm.GraphType.Type234;
                        break;
                    case 13: //流速-U(m/s)
                    case 14: //流速-V(m/s)
                    case 15: //流速-W(m/s)
                    case 16: //濃度(ppm) 
                        graphType = ResultGraphForm.GraphType.Type5678;
                        break;
                    default:
                        break;
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

        private void GenerateTable(bool exportOnly = false)
        {
            switch (param1Cmb.SelectedIndex)
            {
                case 0: //初始底床高程(m)
                    GenerateInitialBottomElevationTable(exportOnly);
                    break;
                case 1: //水深平均流速-U(m/s)
                    GenerateTimeIJResultTable(" U-VELOCITY (M/S)", "水深平均流速-U(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedU, exportOnly);
                    break;
                case 2: //水深平均流速-V(m/s)
                    //GenerateDepthAverageFlowSpeedVTable();
                    GenerateTimeIJResultTable(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV, exportOnly);
                  break;
                case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                  GenerateTimeIJResultTable(" ABS-UV-VELOCITY (M/S)", "水深平均流速-UV 合向量的絕對值(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedAbsUV, exportOnly);
                    break;
                case 4: //底床剪應力(N/m2)   //⁰¹²³⁴⁵⁶⁷⁸⁹
                    GenerateTimeIJResultTable(" TOMD1-U", "底床剪應力(N/m²)", "resed.O", resedTimeList, ref tomd1, exportOnly);
                    break;
                case 5: //水位(m)
                    GenerateTimeIJResultTable(" ZS (M)", "水位(m)", "resed.O", resedTimeList, ref zs, exportOnly);
                    break;
                case 6: //水深(m)
                    GenerateTimeIJResultTable(" DEPTH (M)", "水深(m)", "resed.O", resedTimeList, ref depth, exportOnly);
                    break;
                case 7: //流量-U(cms)
                    GenerateTimeIJResultTable(" US-DISCHARGE (M3/S/M)", "流量-U(cms)", "resed.O", resedTimeList, ref usDischarge, exportOnly);
                    break;
                case 8: //流量-V(cms)
                    GenerateTimeIJResultTable(" VS-DISCHARGE (M3/S/M)", "流量-V(cms)", "resed.O", resedTimeList, ref vsDischarge, exportOnly);
                   break;
                case 9: //底床高程(m)
                   GenerateTimeIJResultTable(" ZBED (M)", "底床高程(m)", "resed.O", resedTimeList, ref zbed, exportOnly);
                    break;
                case 10: //沖淤深度(m)
                    GenerateTimeIJResultTable(" DZBED (M)", "沖淤深度(m)", "SEDoutput.dat", sedTimeList, ref zbed, exportOnly);
                    break;
                case 11: //水深平均濃度(ppm)
                    if (sedimentSizeCmb.SelectedIndex == 0)
                    {
                        GenerateTimeIJResultTable(" MUDCONC", "水深平均濃度(ppm)", "SEDoutput.dat", sedTimeList, ref mudconc, exportOnly);
                    }
                    else
                    {
                        GenerateTimeIJMResultTable(" CONC", "水深平均濃度(ppm)", "SEDoutput.dat", sedTimeList, ref conc, exportOnly);
                    }
                    break;
                case 12: //粒徑分佈(%)
                    GenerateTimeIJResultTable(" BETA (-)", "粒徑分佈(%)", "SEDoutput.dat", sedTimeList, ref beta, exportOnly);
                    break;
                case 13: //流速-U(m/s)
                    GenerateTimeIJKResultTable("流速-U(m/s)", "3Dvelocity-U.dat", resedTimeList, ref flowSpeedU, exportOnly);
                    break;
                case 14: //流速-V(m/s)
                    GenerateTimeIJKResultTable("流速-V(m/s)", "3Dvelocity-V.dat", resedTimeList, ref flowSpeedV, exportOnly);
                    break;
                case 15: //流速-W(m/s)
                    GenerateTimeIJKResultTable("流速-W(m/s)", "3Dvelocity-W.dat", resedTimeList, ref flowSpeedW, exportOnly);
                    break;
                case 16: //濃度(ppm)
                    GenerateTimeIJKMResultTable("濃度(ppm)", "3Dconcentration.dat", sedTimeList, ref concentration, exportOnly);
                    break;
            }
        }

        private void GenerateInitialBottomElevationTable(bool exportOnly = false)
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
            
            if(exportOnly)
            {
                exportFileDialog.ShowDialog();
                if (exportFileDialog.FileName == "")
                 {
                     MessageBox.Show("請選取匯出檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     return;
                 }
                ExportFile(exportFileDialog.FileName, 3, initialBottomElevation);
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
                ResultTableForm.ResultTableType.IJT_Table, //表格形式
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
            form.key = "INI-ZB";
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
                            "(m)",                 //表格名稱
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
                            "(m)",                 //表格名稱
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
                        "(m)",                 //表格名稱
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

        private void GenerateCoorUV_VelocityVectorGraph()
        {
            PosInfo pi = new PosInfo();
            ResultGraphForm.GraphType gt = GetGraphType(ref pi);
            ResultGraphForm form = new ResultGraphForm();
            form.key = "CoorU-VELOCITY (M/S) + CoorV-VELOCITY (M/S)";
            if (p.IsVariableFlowType() && (timeSel == null || timeSel.Length != 1))
            {
                MessageBox.Show("請輸入正確時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (p.IsVariableFlowType())
            {
                pi.tS = timeSel[0];
                pi.tE = timeSel[timeSel.Length - 1] + 1;
            }
            else
            {
                pi.tS = 0;
                pi.tE = 1;
            }
            if (!ParsingTimeIJResult(p.IsConstantFlowType() ? null : resedTimeList, " CoorU-VELOCITY (M/S) ", "resed.O", ref depthAverageFlowSpeedCoorU))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ParsingTimeIJResult(p.IsConstantFlowType() ? null : resedTimeList, " CoorV-VELOCITY (M/S) ", "resed.O", ref depthAverageFlowSpeedCoorV))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            form.SetFormUVVectorMode(
                        "水深平均流速-UV合向量(m/s)",   //視窗標題
                        0, depthAverageFlowSpeedCoorV.GetLength(0),       //i
                        0, depthAverageFlowSpeedCoorV.GetLength(1),       //j
                        -1, -1,              //k
                        pi.tS, pi.tE,       //t
                        form.key,                 //表格名稱
                        "",                 //行標題(顯示於上方)
                        "",                 //列標題(顯示於左方)
                        false,              //不須行數字
                        false,              //不須列數字
                        depthAverageFlowSpeedCoorU, depthAverageFlowSpeedCoorV,  //資料
                        ResultGraphForm.CumulativeDistance,                  //X維度
                        ResultGraphForm.DataContent,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        -1,               //Sel1索引
                        -1,                 //Sel2索引
                        "",              //Sel1標籤
                        "",                 //Sel2標籤
                        null,             //Time選取索引陣列                
                        null);              //Time陣列                 



            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
            //GnuPlot.Close();


        }
        private void GenerateCoorUW_VelocityVectorGraph()
        {
            PosInfo pi = new PosInfo();
            ResultGraphForm.GraphType gt = GetGraphType(ref pi);
            ResultGraphForm form = new ResultGraphForm();
            form.key = "3D Velocity-U + 3D Velocity-W";

            if (!GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref pi.jS, ref pi.jE) || (pi.jE - pi.jS) != 1)
            {
                MessageBox.Show("請輸入正確的J位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (p.IsVariableFlowType() && (timeSel == null || timeSel.Length != 1))
            {
                MessageBox.Show("請輸入正確時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (p.IsVariableFlowType())
            {
                pi.tS = timeSel[0];
                pi.tE = timeSel[timeSel.Length - 1] + 1;
            }
            else
            {
                pi.tS = 0;
                pi.tE = 1;
            }
            if (!ParsingTimeIJKResult(p.IsConstantFlowType() ? null : resedTimeList, "3Dvelocity-U.dat", ref flowSpeedU))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ParsingTimeIJKResult(p.IsConstantFlowType() ? null : resedTimeList, "3Dvelocity-W.dat", ref flowSpeedW))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ParsingTimeIJResult(p.IsConstantFlowType() ? null : resedTimeList, " ZS (M)", "resed.O", ref zs))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ParsingTimeIJResult(p.IsConstantFlowType() ? null : resedTimeList, " DEPTH (M)", "resed.O", ref depth))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            form.SetFormUWVectorMode(
                        "流速-UW(m/s)",   //視窗標題
                        0, p.inputGrid.GetI,      //i
                        pi.jS, pi.jE,       //j
                        -1, -1,              //k
                        pi.tS, pi.tE,       //t
                        form.key,                 //表格名稱
                        "",                 //行標題(顯示於上方)
                        "",                 //列標題(顯示於左方)
                        false,              //不須行數字
                        false,              //不須列數字
                        flowSpeedU, flowSpeedW,  //資料
                        zs, depth,  //資料
                        ResultGraphForm.CumulativeDistance,                  //X維度
                        ResultGraphForm.UW,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        -1,               //Sel1索引
                        -1,                 //Sel2索引
                        "",              //Sel1標籤
                        "",                 //Sel2標籤
                        null,             //Time選取索引陣列                
                        null);              //Time陣列                 

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
            //GnuPlot.Close();


        }

        private void GenerateCoorVW_VelocityVectorGraph()
        {
            PosInfo pi = new PosInfo();
            ResultGraphForm.GraphType gt = GetGraphType(ref pi);
            ResultGraphForm form = new ResultGraphForm();
            form.key = "3D Velocity-U + 3D Velocity-W";

            if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref pi.iS, ref pi.iE) || (pi.iE - pi.iS) != 1)
            {
                MessageBox.Show("請輸入正確的I位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (p.IsVariableFlowType() && (timeSel == null || timeSel.Length != 1))
            {
                MessageBox.Show("請輸入正確時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (p.IsVariableFlowType())
            {
                pi.tS = timeSel[0];
                pi.tE = timeSel[timeSel.Length - 1] + 1;
            }
            else
            {
                pi.tS = 0;
                pi.tE = 1;
            }
            if (!ParsingTimeIJKResult(p.IsConstantFlowType() ? null : resedTimeList, "3Dvelocity-V.dat", ref flowSpeedV))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ParsingTimeIJKResult(p.IsConstantFlowType() ? null : resedTimeList, "3Dvelocity-W.dat", ref flowSpeedW))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ParsingTimeIJResult(p.IsConstantFlowType() ? null : resedTimeList, " ZS (M)", "resed.O", ref zs))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ParsingTimeIJResult(p.IsConstantFlowType() ? null : resedTimeList, " DEPTH (M)", "resed.O", ref depth))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            form.SetFormVWVectorMode(
                        "流速-VW(m/s)",   //視窗標題
                        pi.iS, pi.iE,       //i
                        0, p.inputGrid.GetJ,      //J
                        -1, -1,              //k
                        pi.tS, pi.tE,       //t
                        form.key,                 //表格名稱
                        "",                 //行標題(顯示於上方)
                        "",                 //列標題(顯示於左方)
                        false,              //不須行數字
                        false,              //不須列數字
                        flowSpeedV, flowSpeedW,  //資料
                        zs, depth,  //資料
                        ResultGraphForm.CumulativeDistance,                  //X維度
                        ResultGraphForm.UW,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        -1,               //Sel1索引
                        -1,                 //Sel2索引
                        "",              //Sel1標籤
                        "",                 //Sel2標籤
                        null,             //Time選取索引陣列                
                        null);              //Time陣列                 

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

        private void GenerateTimeIJResultGraph(String ylabel, String key, String title, String outputfile, List<double> timeList, ref double[, ,] array)
        {

            ResultGraphForm form = new ResultGraphForm();
            form.key = key;
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
                    
                    if (!ParsingTimeIJResult(p.IsConstantFlowType() ? null : resedTimeList, key, outputfile, ref array))
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
                            ylabel,                 //表格名稱
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
                            (timeList==null) ? null : timeList.ToArray());                //Time陣列                
                    }
                    else if (gt == ResultGraphForm.GraphType.Type3)
                    {
                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            ylabel,                 //表格名稱
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
                            (timeList == null) ? null : timeList.ToArray());              //Time陣列                
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
                            ylabel,                 //表格名稱
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
                   if (p.IsVariableFlowType() && (timeSel == null || timeSel.Length != 1))
                    {
                        MessageBox.Show("請輸入正確時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                   if (p.IsVariableFlowType())
                   {
                       pi.tS = timeSel[0];
                       pi.tE = timeSel[timeSel.Length - 1] + 1;
                   }
                   else
                   {
                       pi.tS = 0;
                       pi.tE = 1;
                   }
                    
                    if (!ParsingTimeIJResult(p.IsConstantFlowType() ? null : resedTimeList, key, outputfile, ref array))
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
                            ylabel,                 //表格名稱
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
                            (timeList == null) ? null : timeList.ToArray());              //Time陣列       
                   break;
            }

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
            
        }

        private void GenerateTimeIJKResultGraph(String ylabel, String title, String outputfile, List<double> timeList, ref double[, , ,] array)
        {
            ResultGraphForm form = new ResultGraphForm();
            PosInfo pi = new PosInfo();

            int index = 0;
            int index2 = 0;
            switch (graphFormMode)
            {
                case GraphFormMode.XY:
                    ResultGraphForm.GraphType gt = GetGraphType(ref pi);
                    if (gt >= ResultGraphForm.GraphType.Type01)
                    {
                        MessageBox.Show("請輸入正確位置/時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!ParsingTimeIJKResult(p.IsConstantFlowType() ? null : resedTimeList, outputfile, ref array))
                    {
                        MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (gt == ResultGraphForm.GraphType.Type5)
                    {
                     //* -+-+-+-+-+-+----+----+----+-------+
                     //* 1|M|1|1|D|U| -  | -  | 5  |4D IJTK|
                     //* -+-+-+-+-+-+----+----+----+-------+
                        index = pi.iS;
                        index2 = (timeSel == null) ? 0 : timeSel[0];
                        form.key = title;

                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            ylabel,                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.XyGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            array,              //資料
                            ResultGraphForm.CumulativeDistance,     //X維度
                            ResultGraphForm.DataContent,            //Y維度
                            1,                 //Sel1維度
                            -1,                 //Sel2維度
                            -1,               //Sel1索引
                            -1,                 //Sel2索引
                            "",              //Sel1標籤
                            "",                 //Sel2標籤
                            timeSel,             //Time選取索引陣列                
                            (timeList == null) ? null : timeList.ToArray());                //Time陣列                
                    }
                    else if (gt == ResultGraphForm.GraphType.Type6)
                    {
                        //* -+-+-+-+-+-+----+----+----+-------+
                        //* M|1|1|1|D|U| -  | -  | 6  |4D IJTK|
                        //* -+-+-+-+-+-+----+----+----+-------+
                        form.key = title;
                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            ylabel,                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.XyGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            array,              //資料
                            ResultGraphForm.CumulativeDistance,     //X維度
                            ResultGraphForm.DataContent,            //Y維度
                            0,                 //Sel1維度
                            -1,                 //Sel2維度
                            -1,               //Sel1索引
                            -1,                 //Sel2索引
                            "",              //Sel1標籤
                            "",                 //Sel2標籤
                            timeSel,             //Time選取索引陣列                
                            (timeList == null) ? null : timeList.ToArray());                //Time陣列      
                    }
                    else if (gt == ResultGraphForm.GraphType.Type7)
                    {
                     //* -+-+-+-+-+-+----+----+----+-------+
                     //* 1|1|T|1|T|U| -  | -  | 7  |4D IJTK|
                     //* -+-+-+-+-+-+----+----+----+-------+
                     //* 1|1|1|M|U|?| -  | -  | 8  |4D IJTK|<<<<<NOW
                     //* -+-+-+-+-+-+----+----+----+-------+
                        /*
                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            ylabel,                 //表格名稱
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
                    if (p.IsVariableFlowType() && (timeSel == null || timeSel.Length != 1))
                    {
                        MessageBox.Show("請輸入正確時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (p.IsVariableFlowType())
                    {
                        pi.tS = timeSel[0];
                        pi.tE = timeSel[timeSel.Length - 1] + 1;
                    }
                    else
                    {
                        pi.tS = 0;
                        pi.tE = 1;
                    }

                    if (!GetPosRange(posKchk, p.verticalLevelNumber, posKTxt, ref pi.kS, ref pi.kE) || (pi.kE - pi.kS) != 1)
                    {
                        MessageBox.Show("請輸入正確的K位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!ParsingTimeIJKResult(p.IsConstantFlowType() ? null : timeList, outputfile, ref array))
                    {
                        MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    form.key = title;
                    form.SetFormMode(
                             title,              //視窗標題
                             0, array.GetLength(0),       //i
                             0, array.GetLength(1),       //j
                             pi.kS, pi.kE,                  //k
                             pi.tS, pi.tE,       //t
                             ylabel,                 //表格名稱
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
                             (timeList == null) ? null : timeList.ToArray());              //Time陣列       
                    break;
            }

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }

        }

        private void GenerateTimeIJKMResultGraph(String ylabel, String title, String outputfile, List<double> timeList, ref double[, , , ,] array)
        {
            ResultGraphForm form = new ResultGraphForm();
            PosInfo pi = new PosInfo();

            int index = 0;
            int index2 = 0;
            switch (graphFormMode)
            {
                case GraphFormMode.XY:
                    ResultGraphForm.GraphType gt = GetGraphType(ref pi);
                    pi.m = sedimentSizeCmb.SelectedIndex;
                    if (gt >= ResultGraphForm.GraphType.Type01)
                    {
                        MessageBox.Show("請輸入正確位置/時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!ParsingTimeIJKMResult(p.IsConstantFlowType() ? null : timeList, outputfile, ref array, pi.m))
                    {
                        MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (gt == ResultGraphForm.GraphType.Type5)
                    {
                     //* -+-+-+-+-+-+----+----+----+-------+
                     //* 1|M|1|1|D|U| -  | -  | 5  |4D IJTK|
                     //* -+-+-+-+-+-+----+----+----+-------+
                        index = pi.iS;
                        index2 = (timeSel == null) ? 0 : timeSel[0]; 
                        
                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            ylabel,                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.XyGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            array,              //資料
                            ResultGraphForm.CumulativeDistance,     //X維度
                            ResultGraphForm.DataContent,            //Y維度
                            1,                 //Sel1維度
                            -1,                 //Sel2維度
                            -1,               //Sel1索引
                            -1,                 //Sel2索引
                            "",              //Sel1標籤
                            "",                 //Sel2標籤
                            timeSel,             //Time選取索引陣列                
                            (timeList == null) ? null : timeList.ToArray(), //Time陣列   
                            pi.m);                             
                    }
                    else if (gt == ResultGraphForm.GraphType.Type6)
                    {
                     //* -+-+-+-+-+-+----+----+----+-------+
                     //* M|1|1|1|D|U| -  | -  | 6  |4D IJTK|
                     //* -+-+-+-+-+-+----+----+----+-------+
                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            ylabel,                 //表格名稱
                            "",                 //行標題(顯示於上方)
                            "",                 //列標題(顯示於左方)
                            ResultGraphForm.ResultGraphType.XyGraph, //表格形式
                            false,              //不須行數字
                            false,              //不須列數字
                            array,              //資料
                            ResultGraphForm.CumulativeDistance,     //X維度
                            ResultGraphForm.DataContent,            //Y維度
                            0,                 //Sel1維度
                            -1,                 //Sel2維度
                            -1,               //Sel1索引
                            -1,                 //Sel2索引
                            "",              //Sel1標籤
                            "",                 //Sel2標籤
                            timeSel,             //Time選取索引陣列                
                            (timeList == null) ? null : timeList.ToArray(), //Time陣列   
                            pi.m);                             
                    }
                    else if (gt == ResultGraphForm.GraphType.Type7)
                    {
                     //* -+-+-+-+-+-+----+----+----+-------+
                     //* 1|1|T|1|T|U| -  | -  | 7  |4D IJTK|
                     //* -+-+-+-+-+-+----+----+----+-------+
                     //* 1|1|1|M|U|?| -  | -  | 8  |4D IJTK|<<<<<NOW
                     //* -+-+-+-+-+-+----+----+----+-------+
                        /*
                        form.SetFormMode(
                            title,              //視窗標題
                            pi.iS, pi.iE,       //i
                            pi.jS, pi.jE,       //j
                            pi.kS, pi.kE,       //k
                            pi.tS, pi.tE,       //t
                            ylabel,                 //表格名稱
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
                            timeList.ToArray(), //Time陣列   
                            pi.m);                             
                         * */
                    }
                    break;
                case GraphFormMode.Contour:
                    if (p.IsVariableFlowType() && (timeSel == null || timeSel.Length != 1))
                    {
                        MessageBox.Show("請輸入正確時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (p.IsVariableFlowType())
                    {
                        pi.tS = timeSel[0];
                        pi.tE = timeSel[timeSel.Length - 1] + 1;
                    }
                    else
                    {
                        pi.tS = 0;
                        pi.tE = 1;
                    }

                    pi.m = sedimentSizeCmb.SelectedIndex;
                    if (!GetPosRange(posKchk, p.verticalLevelNumber, posKTxt, ref pi.kS, ref pi.kE))
                    {
                        MessageBox.Show("請輸入正確的K位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!ParsingTimeIJKMResult(p.IsConstantFlowType() ? null : timeList, outputfile, ref array, pi.m))
                    {
                        MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    form.SetFormMode(
                             title,              //視窗標題
                             0, array.GetLength(0),       //i
                             0, array.GetLength(1),       //j
                             pi.kS, pi.kE,              //k
                             pi.tS, pi.tE,       //t
                             ylabel,                 //表格名稱
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
                             (timeList == null) ? null : timeList.ToArray(),              //Time陣列       
                             pi.m);
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

        private void GenerateTimeIJResultTable(String key, String title, String outputfile, List<double> timeList, ref double[, ,] array, bool exportOnly = false)
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
            
            if (p.IsVariableFlowType() && timeSel == null)
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
            
            if (!ParsingTimeIJResult(p.IsConstantFlowType() ? null : timeList, key, outputfile, ref array))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = (timeSel == null) ? 0 : timeSel[0];
            if (exportOnly)
            {
                if (t == TableType.Type1)
                {
                    exportFileDialog.ShowDialog();
                    if (exportFileDialog.FileName == "")
                    {
                        MessageBox.Show("請選取匯出檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    ExportFile(exportFileDialog.FileName, 3, array, null, null, index);
                }
                else
                {
                    MessageBox.Show("只支援單一時間匯出檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }

            
            //double[,,] data = null;
            ResultTableForm form = new ResultTableForm();
            switch (t)
            {
                case TableType.Type1:
                    form.SetFormMode(
                        title + ((timeSel == null) ? " 定量流" : " T=" + timeList[index].ToString()),    //視窗標題
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.iS, pi.iE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "I=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJT_Table, //表格形式
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
                        ResultTableForm.ResultTableType.IJT_Table, //表格形式
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
                        (timeList == null) ? null : timeList.ToArray());              //Time陣列
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
                        ResultTableForm.ResultTableType.IJT_Table, //表格形式
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
                        (timeList == null) ? null : timeList.ToArray());              //Time陣列
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
                        ResultTableForm.ResultTableType.IJT_Table, //表格形式
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
                        (timeList == null) ? null : timeList.ToArray());              //Time陣列
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
                    ResultTableForm.ResultTableType.IJT_Table, //表格形式
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
                    ResultTableForm.ResultTableType.IJK_Table, //表格形式
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

        private void GenerateTimeIJMResultTable(String key, String title, String outputfile, List<double> timeList, ref double[, , , ,] array, bool exportOnly = false)
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

            if (p.IsVariableFlowType() && timeSel == null)
            {
                MessageBox.Show("請選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PosInfo pi = new PosInfo();
            TableType t = GetTableType(ref pi);
            pi.m = sedimentSizeCmb.SelectedIndex;
            if (t >= TableType.Type1234)
            {
                MessageBox.Show("請輸入正確位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!ParsingTimeIJMResult(p.IsConstantFlowType() ? null : timeList, key, outputfile, ref array, pi.m))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = (timeSel == null) ? 0 : timeSel[0];
            if (exportOnly)
            {
                if (t == TableType.Type1)
                {
                    exportFileDialog.ShowDialog();
                    if (exportFileDialog.FileName == "")
                    {
                        MessageBox.Show("請選取匯出檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    ExportFile(exportFileDialog.FileName, 3, array, null, null, index);
                }
                else
                {
                    MessageBox.Show("只支援單一時間匯出檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }


            //double[,,] data = null;
            ResultTableForm form = new ResultTableForm();
            switch (t)
            {
                case TableType.Type1:
                    form.SetFormMode(
                        String.Format("{0} {1} T={2} M={3}", title, (timeSel == null) ? " 定量流" : " T=", timeList[index], pi.m),
                        //title + ((timeSel == null) ? " 定量流" : " T=" + timeList[index].ToString()),    //視窗標題
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.iS, pi.iE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "I=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTM_Table, //表格形式
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
                        null,               //Time陣列
                        pi.m);              
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
                        ResultTableForm.ResultTableType.IJTM_Table, //表格形式
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
                        (timeList == null) ? null : timeList.ToArray(), //Time陣列
                        pi.m);              
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
                        ResultTableForm.ResultTableType.IJTM_Table, //表格形式
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
                        (timeList == null) ? null : timeList.ToArray(), //Time陣列
                        pi.m);     
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
                        ResultTableForm.ResultTableType.IJTM_Table, //表格形式
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
                        (timeList == null) ? null : timeList.ToArray(), //Time陣列
                        pi.m);     
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
                    ResultTableForm.ResultTableType.IJK_Table, //表格形式
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
                    ResultTableForm.ResultTableType.IJK_Table, //表格形式
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

        private void GenerateTimeIJKResultTable(String title, String outputfile, List<double> timeList, ref double[, , ,] array, bool exportOnly = false)
        {
            int iStart = 0, iEnd = 0, jStart = 0, jEnd = 0, kStart = 0, kEnd = 0;
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
            if (!GetPosRange(posKchk, p.verticalLevelNumber, posKTxt, ref kStart, ref kEnd))
            {
                MessageBox.Show("請輸入正確的K位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (p.IsVariableFlowType() && timeSel == null)
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

            if (!ParsingTimeIJKResult(p.IsConstantFlowType() ? null : timeList, outputfile, ref array))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = 0;
            int index2 = 0;
            if (exportOnly)
            {
                if (t == TableType.Type1)
                {
                    exportFileDialog.ShowDialog();
                    if (exportFileDialog.FileName == "")
                    {
                        MessageBox.Show("請選取匯出檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    ExportFile(exportFileDialog.FileName, 3, array, null, null, index);
                }
                else
                {
                    MessageBox.Show("只支援單一時間匯出檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }


            //double[,,] data = null;
            ResultTableForm form = new ResultTableForm();
            switch (t)
            {
                case TableType.Type5:
                    index = (timeSel == null) ? 0 : timeSel[0];
                    index2 = pi.kS;
                    form.SetFormMode(
                        String.Format("{0} T={1}, K={2}", title, timeList[index], index2 + 1),  //視窗標題
                        //title + " T=" + timeList[index].ToString(),    
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.iS, pi.iE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "I=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTK_Table,     //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        1,                  //X維度
                        0,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        null);               //Time陣列，若XY沒有要顯示T便不用傳
                    break;
                case TableType.Type6:
                    index = pi.iS;
                    index2 = (timeSel == null) ? 0 : timeSel[0];
                    form.SetFormMode(
                        String.Format("{0} I={1}, T={2}", title, index + 1, timeList[index2]),  //視窗標題
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.kS, pi.kE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "K=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTK_Table,     //表格形式
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
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        null);              //Time陣列，若XY沒有要顯示T便不用傳
                    break;
                case TableType.Type7:
                    index = pi.jS;
                    index2 = (timeSel == null) ? 0 : timeSel[0];
                    form.SetFormMode(
                        String.Format("{0} J={1}, T={2}", title, index + 1, timeList[index2]),  //視窗標題
                        pi.iS, pi.iE,       //行數(左右有幾行)
                        pi.kS, pi.kE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "I=",                 //行標題(顯示於上方)
                        "K=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTK_Table,     //表格形式
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
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        null);              //Time陣列，若XY沒有要顯示T便不用傳
                    break;
                case TableType.Type8:   //JK固定，顯示TI
                    index = pi.jS;
                    index2 = pi.kS;
                    form.SetFormMode(
                        String.Format("{0} J={1}, K={2}", title, index + 1, index2 + 1),  //視窗標題
                        pi.iS, pi.iE,       //行數(左右有幾行)
                        pi.tS, pi.tE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "I=",                 //行標題(顯示於上方)
                        "T=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTK_Table,     //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        0,                  //X維度
                        2,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        (timeList == null) ? null : timeList.ToArray());              //Time陣列，若XY沒有要顯示T便不用傳
                    break;
                case TableType.Type9:   //IK固定，顯示TJ
                    index = pi.iS;
                    index2 = pi.kS;
                    form.SetFormMode(
                        String.Format("{0} I={1}, K={2}", title, index + 1, index2 + 1),  //視窗標題
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.tS, pi.tE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "T=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTK_Table,     //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        1,                  //X維度
                        2,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        (timeList == null) ? null : timeList.ToArray());              //Time陣列，若XY沒有要顯示T便不用傳
                    break;
                case TableType.TypeA:   //IJ固定，顯示TK
                    index = pi.iS;
                    index2 = pi.jS;
                    form.SetFormMode(
                        String.Format("{0} I={1}, J={2}", title, index + 1, index2 + 1),  //視窗標題
                        pi.kS, pi.kE,       //行數(左右有幾行)
                        pi.tS, pi.tE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "K=",                 //行標題(顯示於上方)
                        "T=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTK_Table,     //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        3,                  //X維度
                        2,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        (timeList == null) ? null : timeList.ToArray());    //Time陣列，若XY沒有要顯示T便不用傳
                    break;
                default:
                    break;
            }
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }

        }

        private void GenerateTimeIJKMResultTable(String title, String outputfile, List<double> timeList, ref double[, , , , ] array, bool exportOnly = false)
        {
            int iStart = 0, iEnd = 0, jStart = 0, jEnd = 0, kStart = 0, kEnd = 0;
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
            if (!GetPosRange(posKchk, p.verticalLevelNumber, posKTxt, ref kStart, ref kEnd))
            {
                MessageBox.Show("請輸入正確的K位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (p.IsVariableFlowType() && timeSel == null)
            {
                MessageBox.Show("請選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PosInfo pi = new PosInfo();
            TableType t = GetTableType(ref pi);
            pi.m = sedimentSizeCmb.SelectedIndex;

            if (t >= TableType.Type1234)
            {
                MessageBox.Show("請輸入正確位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!ParsingTimeIJKMResult(p.IsConstantFlowType() ? null : timeList, outputfile, ref array, pi.m))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = 0;
            int index2 = 0;
            if (exportOnly)
            {
                if (t == TableType.Type1)
                {
                    exportFileDialog.ShowDialog();
                    if (exportFileDialog.FileName == "")
                    {
                        MessageBox.Show("請選取匯出檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    ExportFile(exportFileDialog.FileName, 3, array, null, null, index);
                }
                else
                {
                    MessageBox.Show("只支援單一時間匯出檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }


            //double[,,] data = null;
            ResultTableForm form = new ResultTableForm();
            switch (t)
            {
                case TableType.Type5:
                    index = (timeSel == null) ? 0 : timeSel[0];
                    index2 = pi.kS;
                    form.SetFormMode(
                        String.Format("{0} T={1}, K={2}", title, timeList[index], index2 + 1),  //視窗標題
                        //title + " T=" + timeList[index].ToString(),    
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.iS, pi.iE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "I=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTKM_Table,     //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        1,                  //X維度
                        0,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        null,               //Time陣列，若XY沒有要顯示T便不用傳
                        pi.m);              
                    break;
                case TableType.Type6:
                    index = pi.iS;
                    index2 = (timeSel == null) ? 0 : timeSel[0];
                    form.SetFormMode(
                        String.Format("{0} I={1}, T={2}", title, index + 1, timeList[index2]),  //視窗標題
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.kS, pi.kE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "K=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTKM_Table,     //表格形式
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
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        null,               //Time陣列，若XY沒有要顯示T便不用傳
                        pi.m);              
                    break;
                case TableType.Type7:
                    index = pi.jS;
                    index2 = (timeSel == null) ? 0 : timeSel[0];
                    form.SetFormMode(
                        String.Format("{0} J={1}, T={2}", title, index + 1, timeList[index2]),  //視窗標題
                        pi.iS, pi.iE,       //行數(左右有幾行)
                        pi.kS, pi.kE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "I=",                 //行標題(顯示於上方)
                        "K=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTKM_Table,     //表格形式
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
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        null,               //Time陣列，若XY沒有要顯示T便不用傳
                        pi.m);              
                    break;
                case TableType.Type8:   //JK固定，顯示TI
                    index = pi.jS;
                    index2 = pi.kS;
                    form.SetFormMode(
                        String.Format("{0} J={1}, K={2}", title, index + 1, index2 + 1),  //視窗標題
                        pi.iS, pi.iE,       //行數(左右有幾行)
                        pi.tS, pi.tE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "I=",                 //行標題(顯示於上方)
                        "T=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTKM_Table,     //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        0,                  //X維度
                        2,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        (timeList == null) ? null : timeList.ToArray(), //Time陣列，若XY沒有要顯示T便不用傳
                        pi.m);              
                    break;
                case TableType.Type9:   //IK固定，顯示TJ
                    index = pi.iS;
                    index2 = pi.kS;
                    form.SetFormMode(
                        String.Format("{0} I={1}, K={2}", title, index + 1, index2 + 1),  //視窗標題
                        pi.jS, pi.jE,       //行數(左右有幾行)
                        pi.tS, pi.tE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "J=",                 //行標題(顯示於上方)
                        "T=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTKM_Table,     //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        1,                  //X維度
                        2,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        (timeList == null) ? null : timeList.ToArray(), //Time陣列，若XY沒有要顯示T便不用傳
                        pi.m);              
                    break;
                case TableType.TypeA:   //IJ固定，顯示TK
                    index = pi.iS;
                    index2 = pi.jS;
                    form.SetFormMode(
                        String.Format("{0} I={1}, J={2}", title, index + 1, index2 + 1),  //視窗標題
                        pi.kS, pi.kE,       //行數(左右有幾行)
                        pi.tS, pi.tE,       //列數(上下有幾列)
                        "",                 //表格名稱
                        "K=",                 //行標題(顯示於上方)
                        "T=",                 //列標題(顯示於左方)
                        ResultTableForm.ResultTableType.IJTKM_Table,     //表格形式
                        48,                 //儲存格寬度
                        96,                 //列標題寬度
                        true,               //保留
                        false,              //不須行數字
                        false,              //不須列數字
                        array,              //資料
                        3,                  //X維度
                        2,                  //Y維度
                        -1,                 //Sel1維度
                        -1,                 //Sel2維度
                        index,                 //Sel1索引
                        index2,                 //Sel2索引
                        "",                 //Sel1標籤
                        "",                 //Sel2標籤
                        (timeList == null) ? null : timeList.ToArray(), //Time陣列，若XY沒有要顯示T便不用傳
                        pi.m);              
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
                    ResultTableForm.ResultTableType.IJK_Table, //表格形式
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
                    ResultTableForm.ResultTableType.IJK_Table, //表格形式
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

        private double[, ,] depthAverageFlowSpeedCoorU = null;
        private double[, ,] depthAverageFlowSpeedCoorV = null;
        private double[, ,] depthAverageFlowSpeedU = null;
        private double[, ,] depthAverageFlowSpeedV = null;
        private double[, ,] depthAverageFlowSpeedAbsUV = null;
        private double[, ,] tomd1 = null;
        private double[, ,] zs = null;
        private double[, ,] depth = null;
        private double[, ,] usDischarge = null;
        private double[, ,] vsDischarge = null;
        private double[, ,] zbed = null;
        private double[, ,] mudconc = null;
        private double[, ,] beta = null;
        private double[, , ,] flowSpeedU = null;    //I, J, T, K
        private double[, , ,] flowSpeedV = null;    //I, J, T, K
        private double[, , ,] flowSpeedW = null;    //I, J, T, K
        private double[, , , ,] concentration = null;    //I, J, T, K, M
        private double[, , , ,] conc = null;

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
            bool foundKey = false;
            if (result == null)
            {
                int tCount = (timeList == null) ? 1 : timeList.Count;
                result = new double[p.inputGrid.GetI, p.inputGrid.GetJ, tCount];
            }

            int ti = 0;
            while ((line = f.ReadLine()) != null)
            {
                if (!foundTime && line.StartsWith("  TIME="))
                {
                    double checkTime = (timeList == null) ? 0 : timeList[timeSel[ti]];
                    if (checkTime == Convert.ToDouble(line.Substring(9, 16).Trim()))
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

                //timeSel == null 定量流
                int timeIndex = (timeSel == null) ? 0 : timeSel[ti];
                if (foundTime && found)
                {
                    double[] ar = GetLineDouble(line, 10);
                    for (int i = 0; i < ar.Length; ++i)
                    {
                        result[count, i, timeIndex] = ar[i];
                    }
                    foundKey = true;
                    if (++count >= p.inputGrid.GetI)
                    {
                        if ((timeSel == null) || (++ti == timeSel.Length))
                        {
                            break;
                        }
                        foundTime = false;
                        found = false;
                    }
                }
            }   //while ((line = f.ReadLine()) != null)
            return foundKey;
        }

        private bool ParsingTimeIJKResult(List<double> timeList, String outputfile, ref double[, , ,] result)
        {
            string outputFile = Program.GetProjectFileFullPath() + ".working\\" + outputfile;
            if (!File.Exists(outputFile))
            {
                return false;
            }
            // Read the file and display it line by line.
            StreamReader f = new System.IO.StreamReader(outputFile);

            if (result == null)
            {
                int tCount = (timeList == null) ? 1 : timeList.Count;
                result = new double[p.inputGrid.GetI, p.inputGrid.GetJ, tCount, p.verticalLevelNumber];
            }

            string line;
            int count = 0;
            bool foundTime = false;
            bool found = false;
            bool foundKey = false; 
            int ti = 0;
            int ki = 0;
            int cc = 0;

            while ((line = f.ReadLine()) != null)
            {
                ++cc;
                if (!foundTime && line.StartsWith("  TIME="))
                {
                    double checkTime = (timeList == null) ? 0 : timeList[timeSel[ti]];
                    if (checkTime == Convert.ToDouble(line.Substring(9, 16).Trim()))
                    {
                        foundTime = true;
                        continue;
                    }
                }

                if (foundTime && !found && line.StartsWith(" K="))
                {
                    ki = Convert.ToInt32(line.Substring(3, line.Length - 3).Trim()) - 1;
                    found = true;
                    count = 0;
                    continue;
                }
                int timeIndex = (timeSel == null) ? 0 : timeSel[ti];
                if (foundTime && found)
                {
                    double[] ar = GetLineDouble(line, 10);
                    for (int i = 0; i < ar.Length; ++i)
                    {
                        result[count, i, timeIndex, ki] = ar[i];
                    }
                    foundKey = true;
                    if (++count >= p.inputGrid.GetI)
                    {
                        if ((timeSel == null) || (++ti == timeSel.Length))
                        {
                            --ti;
                            found = false;
                            if (ki == p.verticalLevelNumber - 1)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (ki == p.verticalLevelNumber - 1)
                            {
                                foundTime = false;
                            }
                            else
                            {
                                --ti;
                            }
                            found = false;
                        }
                    }
                }
            }   //while ((line = f.ReadLine()) != null)
            return foundKey;
        }

        private bool ParsingTimeIJKMResult(List<double> timeList, String outputfile, ref double[, , , ,] result, int m)
        {
            string outputFile = Program.GetProjectFileFullPath() + ".working\\" + outputfile;
            if (!File.Exists(outputFile))
            {
                return false;
            }
            // Read the file and display it line by line.
            StreamReader f = new System.IO.StreamReader(outputFile);

            if (result == null)
            {
                int tCount = (timeList == null) ? 1 : timeList.Count;
                result = new double[p.inputGrid.GetI, p.inputGrid.GetJ, tCount, p.verticalLevelNumber, p.sedimentParticlesNumber + 1];
            }

            string line;
            int count = 0;
            bool foundTime = false;
            bool foundM = false;
            bool foundK = false;
            bool foundData = false;
            int ti = 0;
            int mi = 0;
            int ki = 0;
            int cc = 0;

            while ((line = f.ReadLine()) != null)
            {
                ++cc;
                if (!foundTime && line.StartsWith("  TIME="))
                {   
                    double checkTime = (timeList == null) ? 0 : timeList[timeSel[ti]];
                    if (checkTime == Convert.ToDouble(line.Substring(9, 16).Trim()))
                    {   //找到所需讀取的時間
                        foundTime = true;
                        continue;
                    }
                }
                if (foundTime && !foundM && line.StartsWith(" M="))
                {   //找到所需讀取的時間
                    mi = Convert.ToInt32(line.Substring(3, line.Length - 3).Trim());
                    if(mi == m)
                    {   //找到所需讀取的M
                        foundM = true;
                        continue;
                    }
                }
 
                if (foundTime && foundM && !foundK && line.StartsWith(" K="))
                {
                    ki = Convert.ToInt32(line.Substring(3, line.Length - 3).Trim()) - 1;
                    foundK = true;
                    count = 0;
                    continue;
                }
                int timeIndex = (timeSel == null) ? 0 : timeSel[ti];
                if (foundTime && foundK)
                {
                    double[] ar = GetLineDouble(line, 10);
                    for (int i = 0; i < ar.Length; ++i)
                    {
                        result[count, i, timeIndex, ki, mi] = ar[i];
                    }
                    foundData = true;
                    if (++count >= p.inputGrid.GetI)
                    {   //讀完I X J個數值
                        if ((timeSel == null) || (++ti == timeSel.Length))
                        {   //讀到最後一個時間段
                            --ti;
                            foundK = false;
                            if (ki == p.verticalLevelNumber - 1)
                            {   //K讀完了還有下一個M
                                break;
                            }
                            else
                            {   //繼續讀取下一個K
                                continue;
                            }
                        }
                        else
                        {   //讀取下一個一個時間段
                            if (ki == p.verticalLevelNumber - 1)
                            {
                                foundTime = false;
                                foundM = false;
                            }
                            else
                            {
                                --ti;
                            }
                            foundK = false;
                        }
                    }
                }
            }   //while ((line = f.ReadLine()) != null)
            return foundData;
        }
        
        private bool ParsingTimeIJMResult(List<double> timeList, String key,  String outputfile, ref double[, , , ,] result, int m)
        {
            string outputFile = Program.GetProjectFileFullPath() + ".working\\" + outputfile;
            if (!File.Exists(outputFile))
            {
                return false;
            }
            // Read the file and display it line by line.
            StreamReader f = new System.IO.StreamReader(outputFile);

            if (result == null)
            {
                int tCount = (timeList == null) ? 1 : timeList.Count;
                result = new double[p.inputGrid.GetI, p.inputGrid.GetJ, tCount, 1, p.sedimentParticlesNumber + 1];
            }

            string line;
            int count = 0;
            bool foundTime = false;
            bool foundK = false;
            bool foundData = false;
            int ti = 0;
            int mi = 0;
            int cc = 0;

            while ((line = f.ReadLine()) != null)
            {
                ++cc;
                if (!foundTime && line.StartsWith("  TIME="))
                {
                    double checkTime = (timeList == null) ? 0 : timeList[timeSel[ti]];
                    if (checkTime == Convert.ToDouble(line.Substring(9, 16).Trim()))
                    {   //找到所需讀取的時間
                        foundTime = true;
                        continue;
                    }
                }

                if (foundTime && !foundK && line.StartsWith(key))
                {
                    String ss = line.Substring(35, line.Length - 35).Trim();
                    mi = Convert.ToInt32(line.Substring(35, line.Length - 35).Trim());
                    if (mi != m)
                    {
                        continue;
                    }
                    foundK = true;
                    count = 0;
                    continue;
                }

                int timeIndex = (timeSel == null) ? 0 : timeSel[ti];
                if (foundTime && foundK)
                {
                    double[] ar = GetLineDouble(line, 10);
                    for (int i = 0; i < ar.Length; ++i)
                    {
                        result[count, i, timeIndex, 0, mi] = ar[i];
                    }
                    foundData = true;
                    if (++count >= p.inputGrid.GetI)
                    {   //讀完I X J個數值
                        if ((timeSel == null) || (++ti == timeSel.Length))
                        {   //讀到最後一個時間段
                            break;
                        }
                        else
                        {   //讀取下一個一個時間段
                            foundTime = false;
                            foundK = false;
                        }
                    }
                }
            }   //while ((line = f.ReadLine()) != null)
            return foundData;
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

        private void GenerateGraph(bool exportOnly = false)
        {
            if (graphFormMode == GraphFormMode.XY)
            {
                switch (param1Cmb.SelectedIndex)
                {
                    case 0: //初始底床高程(m)
                        GenerateInitialBottomElevationGraph();
                        break;
                    case 1: //水深平均流速-U(m/s)
                        GenerateTimeIJResultGraph("(m/s)", " U-VELOCITY (M/S)", "水深平均流速-U(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedU);
                        break;
                    case 2: //水深平均流速-V(m/s)
                        GenerateTimeIJResultGraph("(m/s)", " V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                        break;
                    case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                        GenerateTimeIJResultGraph("(m/s)", " ABS-UV-VELOCITY (M/S)", "水深平均流速-UV 合向量的絕對值(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedAbsUV);
                        break;
                    case 4: //底床剪應力(N/m2)
                        GenerateTimeIJResultGraph("(N/m2)", " TOMD1-U", "底床剪應力(N/m²)", "resed.O", resedTimeList, ref tomd1);
                        break;
                    case 5: //水位(m)
                        GenerateTimeIJResultGraph("(m)", " ZS (M)", "水位(m)", "resed.O", resedTimeList, ref zs);
                        break;
                    case 6: //水深(m)
                        GenerateTimeIJResultGraph("(m)", " DEPTH (M)", "水深(m)", "resed.O", resedTimeList, ref depth);
                        break;
                    case 7://流量-U(cms)
                        GenerateTimeIJResultGraph("(cms)",  "US-DISCHARGE (M3/S/M)", "流量-U(cms)", "resed.O", resedTimeList, ref usDischarge);
                        break;
                    case 8: //流量-V(cms)
                        GenerateTimeIJResultGraph("(cms)", " VS-DISCHARGE (M3/S/M)", "流量-V(cms)", "resed.O", resedTimeList, ref vsDischarge);
                        break;
                    case 9: //底床高程(m)
                        GenerateTimeIJResultGraph("(m)", " ZBED (M)", "底床高程(m)", "resed.O", resedTimeList, ref zbed);
                        break;
                    case 10: //沖淤深度(m)
                        GenerateTimeIJResultGraph("(m)", " DZBED (M)", "沖淤深度(m)", "SEDoutput.dat", sedTimeList, ref zbed);
                        break;
                    case 11: //水深平均濃度(ppm)
                        GenerateTimeIJResultGraph("(ppm)", " MUDCONC", "水深平均濃度(ppm)", "SEDoutput.dat", sedTimeList, ref mudconc);
                        break;
                    case 12: //粒徑分佈(%)
                        GenerateTimeIJResultGraph("(%)", " BETA (-)", "粒徑分佈(%)", "SEDoutput.dat", sedTimeList, ref beta);
                        break;
                    case 13: //流速-U(m/s)
                        GenerateTimeIJKResultGraph("(m/s)", "流速-U(m/s)", "3Dvelocity-U.dat", resedTimeList, ref flowSpeedU);
                        break;
                    case 14: //流速-V(m/s)
                        GenerateTimeIJKResultGraph("(m/s)", "流速-V(m/s)", "3Dvelocity-V.dat", resedTimeList, ref flowSpeedV);
                        break;
                    case 15: //流速-W(m/s)
                        GenerateTimeIJKResultGraph("(m/s)", "流速-W(m/s)", "3Dvelocity-W.dat", resedTimeList, ref flowSpeedW);
                        break;
                    case 16: //濃度(ppm)
                        GenerateTimeIJKMResultGraph("(ppm)", "濃度(ppm)", "3Dconcentration.dat", sedTimeList, ref concentration);
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
                        GenerateTimeIJResultGraph("(m/s)", " U-VELOCITY (M/S)", "水深平均流速-U(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedU);
                        break;
                    case 2: //水深平均流速-V(m/s)
                        GenerateTimeIJResultGraph("(m/s)", " V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                        break;
                    case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                        GenerateTimeIJResultGraph("(m/s)", " ABS-UV-VELOCITY (M/S)", "水深平均流速-UV 合向量的絕對值(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedAbsUV);
                        break;
                    case 4: //底床剪應力(N/m2)
                        GenerateTimeIJResultGraph("(N/m2)", " TOMD1-U", "底床剪應力(N/m²)", "resed.O", resedTimeList, ref tomd1);
                        break;
                    case 5: //水位(m)
                        GenerateTimeIJResultGraph("(m)", " ZS (M)", "水位(m)", "resed.O", resedTimeList, ref zs);
                        break;
                    case 6: //水深(m)
                        GenerateTimeIJResultGraph("(m)", " DEPTH (M)", "水深(m)", "resed.O", resedTimeList, ref depth);
                        break;
                    case 7://流量-U(cms)
                        GenerateTimeIJResultGraph("(cms)", " US-DISCHARGE (M3/S/M)", "流量-U(cms)", "resed.O", resedTimeList, ref usDischarge);
                        break;
                    case 8: //流量-V(cms)
                        GenerateTimeIJResultGraph("(cms)", " VS-DISCHARGE (M3/S/M)", "流量-V(cms)", "resed.O", resedTimeList, ref vsDischarge);
                        break;
                    case 9: //底床高程(m)
                        GenerateTimeIJResultGraph("(m)", " ZBED (M)", "底床高程(m)", "resed.O", resedTimeList, ref zbed);
                        break;
                    case 10: //沖淤深度(m)
                        GenerateTimeIJResultGraph("(m)", " DZBED (M)", "沖淤深度(m)", "SEDoutput.dat", sedTimeList, ref zbed);
                        break;
                    case 11: //水深平均濃度(ppm)
                        GenerateTimeIJResultGraph("ppm", " MUDCONC", "水深平均濃度(ppm)", "SEDoutput.dat", sedTimeList, ref mudconc);
                        break;
                    case 12: //粒徑分佈(%)
                        GenerateTimeIJResultGraph("(%)", " BETA (-)", "粒徑分佈(%)", "SEDoutput.dat", sedTimeList, ref beta);
                        break;
                    case 13: //流速-U(m/s)
                        GenerateTimeIJKResultGraph("(m/s)", "3D Velocity-U(m/s)", "3Dvelocity-U.dat", resedTimeList, ref flowSpeedU);
                        break;
                    case 14: //流速-V(m/s)
                        GenerateTimeIJKResultGraph("(m/s)", "3D Velocity-V(m/s)", "3Dvelocity-V.dat", resedTimeList, ref flowSpeedV);
                        break;
                    case 15: //流速-W(m/s)
                        GenerateTimeIJKResultGraph("(m/s)", "3D Velocity-W(m/s)", "3Dvelocity-W.dat", resedTimeList, ref flowSpeedW);
                        break;
                    case 16: //濃度(ppm)
                        GenerateTimeIJKMResultGraph("(ppm)", "3D Cncentration(ppm)", "3Dconcentration.dat", sedTimeList, ref concentration);
                        break;
                }
            }
            else if (graphFormMode == GraphFormMode.Vector)
            {
                switch (param1Cmb.SelectedIndex)
                {
                    case 0: //水深平均流速-UV 合向量(m/s)
                        GenerateCoorUV_VelocityVectorGraph();
                        break;
                    case 1: //流速-UW合向量(m/s)              
                        GenerateCoorUW_VelocityVectorGraph();
                        break;
                    case 2: //流速-VW合向量(m/s)
                        GenerateCoorVW_VelocityVectorGraph();
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
            if (drawType == Param1Type.ParamTable)
            {
                switch (param1Cmb.SelectedIndex)
                {   //param1Cmb.DataSource = tableItemsParam1;
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
                    case 16:
                        form.SetFormMode("", GetTimeSelectionType(), sedTimeList);
                        break;
                    case 13:
                    case 14:
                    case 15:
                        form.SetFormMode("", GetTimeSelectionType(), resedTimeList);
                        break;
                    default:
                        break;
                }
            }
            else if(drawType == Param1Type.ParamGraph1)
            {
                if(graphFormMode == GraphFormMode.XY)   //折線圖
                {
                    switch (param1Cmb.SelectedIndex)
                    {   //param1Cmb.DataSource = tableItemsParam2;
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
                        case 16:
                            form.SetFormMode("", GetTimeSelectionType(), sedTimeList);
                            break;
                        case 13:
                        case 14:
                        case 15:
                            form.SetFormMode("", GetTimeSelectionType(), resedTimeList);
                            break;
                        default:
                            break;
                    }

                }
                else if(graphFormMode == GraphFormMode.Contour)   //等值線圖
                {
                    switch (param1Cmb.SelectedIndex)
                    {   //param1Cmb.DataSource = tableItemsParam3;
                        case 1: //水深平均流速-U(m/s)
                        case 2: //水深平均流速-V(m/s)
                        case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                        case 4: //底床剪應力(N/m2)   //⁰¹²³⁴⁵⁶⁷⁸⁹
                        case 5: //水位(m)
                        case 6: //水深(m)
                        case 7: //流量-U(cms)
                        case 8: //流量-V(cms)
                            form.SetFormMode("", ResultTimeSelForm.ResultTimeType.SingleSelect, resedTimeList);
                            break;
                        case 9: //底床高程(m)
                        case 10: //沖淤深度(m)
                        case 11: //水深平均濃度(ppm)
                        case 12: //粒徑分佈(%)
                        case 16:
                            form.SetFormMode("", ResultTimeSelForm.ResultTimeType.SingleSelect, sedTimeList);
                            break;
                        case 13:
                        case 14:
                        case 15:
                            form.SetFormMode("", ResultTimeSelForm.ResultTimeType.SingleSelect, resedTimeList);
                            break;
                        default:
                            break;
                    }
                }
                else if (graphFormMode == GraphFormMode.Vector)   //向量圖
                {
                    switch (param1Cmb.SelectedIndex)
                    {   //param1Cmb.DataSource = tableItemsParam4;
                        case 0: //水深平均流速-UV 合向量的絕對值(m/s)
                        case 1: //流速-UW合向量(m/s)
                        case 2: //流速-VW合向量(m/s)
                            form.SetFormMode("", ResultTimeSelForm.ResultTimeType.SingleSelect, resedTimeList);
                            break;
                        default:
                            break;
                    }
                }
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
            public int m = -1;

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
                        (p.IsVariableFlowType() && timeSel == null))
                    {   //I或J或T未輸入
                        return TableType.Type1234;
                    }

                    if (p.IsVariableFlowType())
                    {
                        pi.tS = timeSel[0];
                        pi.tE = timeSel[timeSel.Length - 1] + 1;
                    }
                    else
                    {
                        pi.tS = 0;
                        pi.tE = 1;
                    }

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
                        !GetPosRange(posKchk, p.verticalLevelNumber, posKTxt, ref pi.kS, ref pi.kE) ||
                        (p.IsVariableFlowType() && timeSel == null))
                    {   //I或J或T或K未輸入
                        return TableType.Type56789A;
                    }

                    if (p.IsVariableFlowType())
                    {
                        pi.tS = timeSel[0];
                        pi.tE = timeSel[timeSel.Length - 1] + 1;
                    }
                    else
                    {
                        pi.tS = 0;
                        pi.tE = 1;
                    }
                    if (pi.GetICount() > 1 && pi.GetJCount() > 1 && pi.GetTCount() == 1 && pi.GetKCount() == 1)
                    {   //KT固定，顯示IJ
                        return TableType.Type5;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() > 1 && pi.GetTCount() == 1 && pi.GetKCount() > 1)
                    {   //IT固定，顯示KJ
                        return TableType.Type6;
                    }
                    else if (pi.GetICount() > 1 && pi.GetJCount() == 1 && pi.GetTCount() == 1 && pi.GetKCount() > 1)
                    {   //JT固定，顯示KI
                        return TableType.Type7;
                    }
                    else if (pi.GetICount() > 1 && pi.GetJCount() == 1 && pi.GetTCount() > 1 && pi.GetKCount() == 1)
                    {   //JK固定，顯示TI
                        return TableType.Type8;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() > 1 && pi.GetTCount() > 1 && pi.GetKCount() == 1)
                    {   //IK固定，顯示TJ
                        return TableType.Type9;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() == 1 && pi.GetTCount() > 1 && pi.GetKCount() > 1)
                    {   //IJ固定，顯示TK
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
                        (p.IsVariableFlowType() && timeSel == null))
                    {   //I或J或T未輸入
                        return ResultGraphForm.GraphType.Type234;
                    }
                    if (p.IsVariableFlowType())
                    {
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
                    }
                    else
                    {
                        if (pi.GetICount() == 1 && pi.GetJCount() > 1)
                        {   //IT固定
                            return ResultGraphForm.GraphType.Type2;
                        }
                        else if (pi.GetICount() > 1 && pi.GetJCount() == 1)
                        {   //JT固定
                            return ResultGraphForm.GraphType.Type3;
                        }
                    }
                    return ResultGraphForm.GraphType.Type234;
                case ResultGraphForm.GraphType.Type5678:
                    if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref pi.iS, ref pi.iE) ||
                        !GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref pi.jS, ref pi.jE) ||
                        !GetPosRange(posKchk, p.verticalLevelNumber, posKTxt, ref pi.kS, ref pi.kE) ||
                        (p.IsVariableFlowType() && timeSel == null))
                    {   //I或J或T或K未輸入
                        return ResultGraphForm.GraphType.Type5678;
                    }
                    if (p.IsVariableFlowType())
                    {
                        pi.tS = timeSel[0];
                        pi.tE = timeSel[timeSel.Length - 1] + 1;
                    }
                    else
                    {
                        pi.tS = 0;
                        pi.tE = 1;
                    }
                    if (pi.GetICount() == 1 && pi.GetJCount() > 1 && pi.GetTCount() == 1 && pi.GetKCount() == 1)
                    {   //ITK固定，顯示J, 累距(m)
                        return ResultGraphForm.GraphType.Type5;
                    }
                    else if (pi.GetICount() > 1 && pi.GetJCount() == 1 && pi.GetTCount() == 1 && pi.GetKCount() == 1)
                    {   //JTK固定，顯示I, 累距(m)
                        return ResultGraphForm.GraphType.Type6;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() == 1 && pi.GetTCount() > 1 && pi.GetKCount() == 1)
                    {   //IJK固定，顯示IJK, 時間(sec)
                        return ResultGraphForm.GraphType.Type7;
                    }
                    else if (pi.GetICount() == 1 && pi.GetJCount() == 1 && pi.GetTCount() == 1 && pi.GetKCount() > 1)
                    {   //IJT固定，顯示高程(m), K
                        return ResultGraphForm.GraphType.Type8;
                    }
                    return ResultGraphForm.GraphType.Type5678;
            }
            return ResultGraphForm.GraphType.TypeUnknown;
        }

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

        private void exportFileBtn_Click(object sender, EventArgs e)
        {
            if (drawType == Param1Type.ParamTable)
            {
                GenerateTable(true);
            }
            else
            {
                MessageBox.Show("請選取表格！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        bool ExportFile(String file, int varCount, object array1, object array2 = null, object array3 = null, int index = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("title = Algebric Grid-Generation\n");
            if (varCount == 3)
            {
                sb.Append("variables = x , y , z\n");
            }
            else if (varCount == 4)
            {
                sb.Append("variables = x , y , U , V\n");
            }
            else if (varCount == 5)
            {
                sb.Append("variables = x , y , U , V , z\n");
            }
            sb.AppendFormat("zone i = {0} , j = {1} , f=point\n", p.inputGrid.GetI, p.inputGrid.GetJ);

            for(int j = p.inputGrid.GetJ - 1; j >= 0; --j)
            {
                for (int i = p.inputGrid.GetI - 1; i >= 0; --i)
                {
                    if (varCount == 3)
                    {
                        sb.AppendFormat("{0,8} {1,8} {2,8}\n",
                            p.inputGrid.inputCoor[i, j].x,
                            p.inputGrid.inputCoor[i, j].y,
                            (array1 as double[, ,])[i, j, index]);
                    }
                }
            }
            using (StreamWriter outfile = new StreamWriter(file))
            {
                outfile.Write(sb.ToString());
                outfile.Close();
            }
            return true;
        }
        //TableType tableType = TableType.Unknown;
    }
}

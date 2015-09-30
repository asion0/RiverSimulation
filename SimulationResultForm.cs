using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


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
        private enum GraphType
        {
            noSelect,
            type1,
            type2,
            type3,
            type4
        };
        GraphType graphType = GraphType.noSelect;

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
            if (!ParsingTime("SEDoutput.dat", ref sedTimeList))
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void graphRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            //20150811後處理流程示範說明,
            //2. 若選擇“圖形”：
            //  則“參數”先只能選擇一種，待“形式”若選擇“等值線疊向量圖”，
            //  再把第2 個“參數”的下拉選單打開供使用者選擇。
            param2Cmb.Enabled = !chk;
            formGrp.Enabled = chk;
            axisGrp.Enabled = chk;
            InitialParam1(Param1Type.ParamGraph1);
        }
        private void tableRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            //20150811後處理流程示範說明,
            //1. 若選擇“表格”：
            //  A. 則“參數”只能選擇一種，第2 個“參數”的下拉選單灰階。
            //  B. 且“形式”及“座標軸”也灰階。
            param2Cmb.Enabled = !chk;
            formGrp.Enabled = !chk;
            axisGrp.Enabled = !chk;
            InitialParam1(Param1Type.ParamTable);
        }

        private enum Param1Type {
            ParamTable,
            ParamGraph1,
        }

        private string[] tableItemsParam1 = {
                "初始底床高程(m)",
                "水深平均流速-U(m/s)",
                "水深平均流速-V(m/s)",
                "水深平均流速-UV 合向量的絕對值(m/s)",
                "底床剪應力(N/m2)",
                "水位(m)",
                "水深(m)",
                "流量-U(cms)",
                "流量-V(cms)",
                "底床高程(m)",
                "沖淤深度(m)",
                "水深平均濃度(ppm)",
                "粒徑分佈(%)",
                "流速-U(m/s)",
                "流速-V(m/s)",
                "流速-W(m/s)",
                "濃度(ppm)" };

        private string[] tableItemsParam2 = {
                "初始底床高程(m)",
                "水深平均流速-U(m/s)",
                "水深平均流速-V(m/s)",
                "水深平均流速-UV 合向量的絕對值(m/s)",
                "水深平均流速-UV 合向量(m/s)",
                "底床剪應力(N/m2)",
                "水位(m)",
                "水深(m)",
                "流量-U(cms)",
                "流量-V(cms)",
                "底床高程(m)",
                "沖淤深度(m)",
                "水深平均濃度(ppm)",
                "粒徑分佈(%)",
                "流速-U(m/s)",
                "流速-V(m/s)",
                "流速-W(m/s)",
                "流速-UW合向量(m/s)",
                "流速-VW合向量(m/s)",
                "濃度(ppm)" };

        private void InitialParam1(Param1Type p)
        {
            if (p == Param1Type.ParamTable)
            {
                param1Cmb.DataSource = tableItemsParam1;
            }
            else if (p == Param1Type.ParamGraph1)
            {
                param1Cmb.DataSource = tableItemsParam2;
            } 
            param1Cmb.SelectedIndex = 0;
        }

        private void param1Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            if(cmb.DataSource == tableItemsParam1)
            {
                switch(cmb.SelectedIndex)
                {
                    case 0: //初始底床高程(m)
                        //timeGrp.Enabled = false;
                        poKPanel.Enabled = false;
                        break;
                    case 1: //水深平均流速-U(m/s)
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = false;
                        poKPanel.Enabled = false;
                        break;
                    case 2: //水深平均流速-V(m/s)
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = false;
                        poKPanel.Enabled = false;
                        break;
                    case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 4: //底床剪應力(N/m2)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 5: //水位(m)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 6: //水深(m)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 7: //流量-U(cms)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 8: //流量-V(cms)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 9: //底床高程(m)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 10: //沖淤深度(m)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 11: //水深平均濃度(ppm)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 12: //粒徑分佈(%)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
            else if(cmb.DataSource == tableItemsParam2)
            {
                switch (cmb.SelectedIndex)
                {
                     case 0: //初始底床高程(m)
                        timeGrp.Enabled = false;
                        poKPanel.Enabled = false;
                       break;
                    case 1: //水深平均流速-U(m/s)
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        poKPanel.Enabled = false;
                        break;
                    case 2: //水深平均流速-V(m/s)
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = false;
                        poKPanel.Enabled = false;
                        break;
                    case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 4: //水深平均流速-UV 合向量(m/s)
                
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 5: //底床剪應力(N/m2)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 6: //水位(m)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 7: //水深(m)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 8: //流量-U(cms)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 9: //流量-V(cms)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 10: //底床高程(m)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 11: //沖淤深度(m)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 12: //水深平均濃度(ppm)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 13: //粒徑分佈(%)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 14: //流速-U(m/s)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 15: //流速-V(m/s)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 16: //流速-W(m/s)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 17: //流速-UW合向量(m/s
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 18: //流速-VW合向量(m/s)
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    case 19: //濃度(ppm) 
                        poKPanel.Enabled = false;
                        //timeGrp.Enabled = true;
                        timeChk.Enabled = true;
                        break;
                    default:
                       break;
                }
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
            if(tableRdo.Checked)
            {
                GenerateTable();
            }
            else if(graphRdo.Checked)
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
                    //GenerateDepthAverageFlowSpeedUTable();
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
                    GenerateTimeIJResultTable(" ZS (M)", "流量-U(cms)", "resed.O", resedTimeList, ref usDischarge);
                    break;
                case 8: //流量-V(cms)
                    GenerateTimeIJResultTable(" VS-DISCHARGE (M3/S/M)", "流量-V(cms)", "resed.O", resedTimeList, ref vsDischarge);
                   break;
                case 9: //底床高程(m)
                   GenerateTimeIJResultTable(" US-DISCHARGE (M3/S/M)", "流量-U(cms)", "resed.O", sedTimeList, ref usDischarge);
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
                default:
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
                "",                 //行標題(顯示於上方)
                "",                 //列標題(顯示於左方)
                ResultTableForm.ResultTableType.InitialBottomElevation, //表格形式
                48,                 //儲存格寬度
                64,                 //列標題寬度
                true,               //保留
                false,              //不須行數字
                false,              //不須列數字
                initialBottomElevation  //資料
                );

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
        }

        private void GenerateInitialBottomElevationGraph()
        {
            PosInfo pi = new PosInfo();
            TableType t = GetTableSize(ref pi);
            if (TableType.Unknown == t || TableType.IJ == t)
            {
                MessageBox.Show("請輸入正確位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //if (t == TableType.TimeI || t == TableType.TimeJ)
            //{
            //    MessageBox.Show("選取一維的位置請選取時間區段！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            if(!ParsingInitialBottomElevationResult())
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ResultGraphForm form = new ResultGraphForm();
            form.SetFormMode(
                "初始底床高程(m)",   //視窗標題
                pi.jS, pi.jE,       //行數(左右有幾行)
                pi.iS, pi.iE,       //列數(上下有幾列)
                "",                 //表格名稱
                "",                 //行標題(顯示於上方)
                "",                 //列標題(顯示於左方)
                ResultGraphForm.ResultGraphType.InitialBottomElevation, //表格形式
                48,                 //儲存格寬度
                64,                 //列標題寬度
                true,               //保留
                false,              //不須行數字
                false,              //不須列數字
                initialBottomElevation  //資料
                );

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
        }

        /*
        private void GenerateDepthAverageFlowSpeedUTable()
        {
            //int iStart = 0, iEnd = p.inputGrid.GetI, jStart = 0, jEnd = p.inputGrid.GetJ;
            if(timeSel == null)
            {
                MessageBox.Show("請選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PosInfo pi = new PosInfo();
            TableType t = GetTableSize(ref pi);
            if(TableType.Unknown == t)
            {
                MessageBox.Show("請輸入正確位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(timeSel.Length == 1 && (t == TableType.TimeI || t == TableType.TimeJ))
            {
                MessageBox.Show("選取一維的位置請選取時間區段！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (timeSel.Length > 1 && (t == TableType.IJ))
            {
                MessageBox.Show("選取二維的位置請選取單一時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(!ParsingTimeIJResult(t, pi, " U-VELOCITY (M/S)", ref depthAverageFlowSpeedU))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int timeIndex = 0;
            double[,] data = null;
            ResultTableForm form = new ResultTableForm();

            if (t == TableType.IJ)
            {
                timeIndex = FoundTimeSelInList(timeSel[0]);
                data = new double[p.inputGrid.GetJ, p.inputGrid.GetI];
                for (int j = 0; j < p.inputGrid.GetJ; ++j)
                    for (int i = 0; i < p.inputGrid.GetI; ++i)
                        data[j, i] = depthAverageFlowSpeedU[timeIndex, j, i];

                form.SetFormMode(
                    "水深平均流速-U(m/s) T=" + timeSel[0].ToString(),   //視窗標題
                    pi.jS, pi.jE,       //行數(左右有幾行)
                    pi.iS, pi.iE,       //列數(上下有幾列)
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
            else if(t == TableType.TimeI)
            {
                //timeIndex = FoundTimeSelInList(timeSel[0]);
                data = new double[pi.GetICount(), timeSel.Length];
                for (int ti = 0; ti < timeSel.Length; ++ti)
                    for (int i = pi.iS; i < pi.iE; ++i)
                        data[i, ti] = depthAverageFlowSpeedU[timeSel[ti], pi.jS, i];

                form.SetFormMode(
                    "水深平均流速-U(m/s) J=" + (pi.jS + 1).ToString(),   //視窗標題
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
            else if (t == TableType.TimeJ)
            {
                //timeIndex = FoundTimeSelInList(timeSel[0]);
                data = new double[pi.GetJCount(), timeSel.Length];
                for (int ti = 0; ti < timeSel.Length; ++ti)
                    for (int j = pi.jS; j < pi.jE; ++j)
                        data[j, ti] = depthAverageFlowSpeedU[timeSel[ti], j, pi.iS];

                form.SetFormMode(
                    "水深平均流速-U(m/s) I=" + (pi.iS + 1).ToString(),   //視窗標題
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

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
        }

        private void GenerateDepthAverageFlowSpeedVTable()
        {
            //int iStart = 0, iEnd = p.inputGrid.GetI, jStart = 0, jEnd = p.inputGrid.GetJ;
            if (timeSel == null || timeSel.Length != 1)
            {
                MessageBox.Show("請選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PosInfo pi = new PosInfo();
            TableType t = GetTableSize(ref pi);
            if (TableType.Unknown == t)
            {
                MessageBox.Show("請輸入正確位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (timeSel.Length == 1 && (t == TableType.TimeI || t == TableType.TimeJ))
            {
                MessageBox.Show("選取一維的位置請選取時間區段！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (timeSel.Length > 1 && (t == TableType.IJ))
            {
                MessageBox.Show("選取二維的位置請選取單一時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!ParsingTimeIJResult(t, pi, " V-VELOCITY (M/S)", ref depthAverageFlowSpeedV))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int timeIndex = 0;
            double[,] data = null;
            ResultTableForm form = new ResultTableForm();

            if (t == TableType.IJ)
            {
                timeIndex = FoundTimeSelInList(timeSel[0]);
                data = new double[p.inputGrid.GetJ, p.inputGrid.GetI];
                for (int j = 0; j < p.inputGrid.GetJ; ++j)
                    for (int i = 0; i < p.inputGrid.GetI; ++i)
                        data[j, i] = depthAverageFlowSpeedV[timeIndex, j, i];

                form.SetFormMode(
                    "水深平均流速-V(m/s) T=" + timeSel[0].ToString(),   //視窗標題
                    pi.jS, pi.jE,       //行數(左右有幾行)
                    pi.iS, pi.iE,       //列數(上下有幾列)
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
            else if (t == TableType.TimeI)
            {
                //timeIndex = FoundTimeSelInList(timeSel[0]);
                data = new double[pi.GetICount(), timeSel.Length];
                for (int ti = 0; ti < timeSel.Length; ++ti)
                    for (int i = pi.iS; i < pi.iE; ++i)
                        data[i, ti] = depthAverageFlowSpeedV[timeSel[ti], pi.jS, i];

                form.SetFormMode(
                    "水深平均流速-V(m/s) J=" + (pi.jS + 1).ToString(),   //視窗標題
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
            else if (t == TableType.TimeJ)
            {
                //timeIndex = FoundTimeSelInList(timeSel[0]);
                data = new double[pi.GetJCount(), timeSel.Length];
                for (int ti = 0; ti < timeSel.Length; ++ti)
                    for (int j = pi.jS; j <= pi.jE; ++j)
                        data[j, ti] = depthAverageFlowSpeedV[timeSel[ti], j, pi.iS];

                form.SetFormMode(
                    "水深平均流速-V(m/s) I" + (pi.iS + 1).ToString(),   //視窗標題
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

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.verticalVelocityDistributionArray = (double[,])form.VerticalVelocityDistributionData().Clone();
            }
        }
        */
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
            //int iStart = 0, iEnd = p.inputGrid.GetI, jStart = 0, jEnd = p.inputGrid.GetJ;
            if (timeSel == null)
            {
                MessageBox.Show("請選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PosInfo pi = new PosInfo();
            TableType t = GetTableSize(ref pi);
            if (TableType.Unknown == t)
            {
                MessageBox.Show("請輸入正確位置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (timeSel.Length == 1 && (t == TableType.TimeI || t == TableType.TimeJ))
            {
                MessageBox.Show("選取一維的位置請選取時間區段！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (timeSel.Length > 1 && (t == TableType.IJ))
            {
                MessageBox.Show("選取二維的位置請選取單一時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!ParsingTimeIJResult(t, pi, timeList, key, outputfile, ref array))
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int timeIndex = 0;
            double[,] data = null;
            ResultTableForm form = new ResultTableForm();

            if (t == TableType.IJ)
            {
                //timeIndex = FoundTimeSelInList(timeList, timeSel[0]);
                timeIndex = timeSel[0];
                data = new double[p.inputGrid.GetJ, p.inputGrid.GetI];
                for (int j = 0; j < p.inputGrid.GetJ; ++j)
                    for (int i = 0; i < p.inputGrid.GetI; ++i)
                        data[j, i] = array[timeIndex, j, i];

                SaveToCsv("Output.csv", data);
                form.SetFormMode(
                    title + " T=" + timeSel[0].ToString(),   //視窗標題
                    pi.jS, pi.jE,       //行數(左右有幾行)
                    pi.iS, pi.iE,       //列數(上下有幾列)
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
            else if (t == TableType.TimeI)
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
            else if (t == TableType.TimeJ)
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

        private double[,] initialBottomElevation = null;
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
                initialBottomElevation = new double[p.inputGrid.GetJ, p.inputGrid.GetI];
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
                        initialBottomElevation[i, count] = ar[i];
                    }

                    if (++count >= p.inputGrid.GetI)
                    {
                        break;
                    }
                }
            }
            return true;
        }

        /*
        private bool ParsingIDepthAverageFlowSpeedUResult(TableType t, PosInfo pi)
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
            bool foundTime = false;
            bool found = false;

            if (depthAverageFlowSpeedU == null)
            {
                depthAverageFlowSpeedU = new double[timeList.Count, p.inputGrid.GetJ, p.inputGrid.GetI];
            }

            int ti = 0;
            //bool exit = false;
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

                if (foundTime && !found && line.StartsWith(" U-VELOCITY (M/S)"))
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
                    for(int i = 0; i < ar.Length; ++i)
                    {
                        depthAverageFlowSpeedU[timeIndex, i, count] = ar[i];
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
        */
        /*
        private bool ParsingIDepthAverageFlowSpeedVResult(TableType t, PosInfo pi)
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
            bool foundTime = false;
            bool found = false;

            if (depthAverageFlowSpeedV == null)
            {
                depthAverageFlowSpeedV = new double[timeList.Count, p.inputGrid.GetJ, p.inputGrid.GetI];
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

                if (foundTime && !found && line.StartsWith(" V-VELOCITY (M/S)"))
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
                        depthAverageFlowSpeedV[timeIndex, i, count] = ar[i];
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
        */

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

        private bool ParsingTimeIJResult(TableType t, PosInfo pi, List<double> timeList, String keyword, String outputfile, ref double[, ,] result)
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
                result = new double[timeList.Count, p.inputGrid.GetJ, p.inputGrid.GetI];
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
                        result[timeIndex, i, count] = ar[i];
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
            switch (param1Cmb.SelectedIndex)
            {

                case 0: //初始底床高程(m)
                    GenerateInitialBottomElevationGraph();
                    break;
                case 1: //水深平均流速-U(m/s)
                    GenerateTimeIJResultTable(" U-VELOCITY (M/S)", "水深平均流速-U(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedU);
                    break;
                case 2: //水深平均流速-V(m/s)
                    GenerateTimeIJResultTable(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                    break;
                case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                    GenerateTimeIJResultTable(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                    break;
                case 4: //底床剪應力(N/m2)
                    GenerateTimeIJResultTable(" V-VELOCITY (M/S)", "水深平均流速-V(m/s)", "resed.O", resedTimeList, ref depthAverageFlowSpeedV);
                    break;
                case 5: //水位(m)

                    break;
                case 6: //水深(m)
                    break;
                case 7: //流量-U(cms)
                    break;
                case 8: //流量-V(cms)
                    break;
                case 9: //底床高程(m)
                    break;
                case 10: //沖淤深度(m)
                    break;
                case 11: //水深平均濃度(ppm)
                    break;
                case 12: //粒徑分佈(%)
                    break;
                default:
                    break;
            }
        }

        ResultTimeSelForm.ResultTimeType GetTimeSelectionType()
        {
            if(param1Cmb.SelectedIndex == 0)
            {
                return ResultTimeSelForm.ResultTimeType.SingleSelect;
            }

            int iS = -1, iE = -1;
            if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref iS, ref iE))
            {
                //return false;
            }
            int iCount = (iE - iS);

            int jS = -1, jE = -1;
            if (!GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref jS, ref jE))
            {
                //return false;
            }
            int jCount = (jE - jS);

            
            return (jCount == 1 || iCount == 1) ? ResultTimeSelForm.ResultTimeType.MultiSelect : ResultTimeSelForm.ResultTimeType.SingleSelect;
        }

        private int[] timeSel = null;
        private void timeBtn_Click(object sender, EventArgs e)
        {
            ResultTimeSelForm form = new ResultTimeSelForm();

            //form.SetFormMode("", GetTimeSelectionType(), timeList);
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
            TableType t = GetTableSize(ref pi);
            timeGrp.Enabled = TimeGroupEnable();
        }

        private void posJchk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            posJTxt.Enabled = !chk;
            PosInfo pi = new PosInfo();
            TableType t = GetTableSize(ref pi);
            timeGrp.Enabled = TimeGroupEnable();
        }

        private void posITxt_TextChanged(object sender, EventArgs e)
        {
            PosInfo pi = new PosInfo();
            TableType t = GetTableSize(ref pi);
            timeGrp.Enabled = TimeGroupEnable();
        }

        private void posJTxt_TextChanged(object sender, EventArgs e)
        {
            PosInfo pi = new PosInfo();
            TableType t = GetTableSize(ref pi);
            timeGrp.Enabled = TimeGroupEnable();
        }

        private bool TimeGroupEnable()
        {
            PosInfo pi = new PosInfo();
            TableType t = GetTableSize(ref pi);
            if (param1Cmb.SelectedIndex == 0)
                return false;

            return t != TableType.Unknown;
        }

        private class PosInfo
        {
            public int iS = -1;
            public int iE = -1;
            public int jS = -1;
            public int jE = -1;

            public int GetICount() { return iE - iS; }
            public int GetJCount() { return jE - jS; }
        };

        private TableType GetTableSize(ref PosInfo pi)
        {
            if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref pi.iS, ref pi.iE))
            {   //取不到I範圍，表格型態未知
                return TableType.Unknown;
            }
            if (!GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref pi.jS, ref pi.jE))
            {   //取不到J範圍，表格型態未知
                return TableType.Unknown;
            }


            if (pi.GetICount() == 1 && pi.GetJCount() > 1)
            {
                return TableType.TimeJ;
            }
            if (pi.GetICount() > 1 && pi.GetJCount() == 1)
            {
                return TableType.TimeI;
            }
            if (pi.GetICount() > 1 && pi.GetJCount() > 1)
            {
                return TableType.IJ;
            }
            return TableType.Unknown;
        }

        enum TableType
        {
            Unknown,
            TimeI,
            TimeJ,
            IJ
        };

        private void graphType1Rdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void graphType2Rdo_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void graphType3Rdo_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void graphType4Rdo_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void animChk_CheckedChanged(object sender, EventArgs e)
        {
        
        }
        //TableType tableType = TableType.Unknown;
    }
}

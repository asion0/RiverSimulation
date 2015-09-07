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
        private void SimulationResultForm_Load(object sender, EventArgs e)
        {
            p = RiverSimulationProfile.profile;
            posILbl.Text = String.Format("MAX:{0}", p.inputGrid.GetI);
            posJLbl.Text = String.Format("MAX:{0}", p.inputGrid.GetJ);
            if(p.Is3DMode())
            {
                posKLbl.Text = String.Format("MAX:{0}", p.verticalLevelNumber);
            }

            ParsingTime();
        }

        private void graphRdo_CheckedChanged(object sender, EventArgs e)
        {

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

        private void InitialParam1(Param1Type p)
        {
            if (p == Param1Type.ParamTable)
            {
                param1Cmb.DataSource = tableItemsParam1;
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
                    GenerateDepthAverageFlowSpeedUTable();
                    break;
                case 2: //水深平均流速-V(m/s)
                     GenerateDepthAverageFlowSpeedVTable();
                   break;
                case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                    break;
                case 4: //底床剪應力(N/m2)
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

            if(!ParsingIDepthAverageFlowSpeedUResult(t, pi))
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
                    for (int j = pi.jS; j <= pi.jE; ++j)
                        data[j, ti] = depthAverageFlowSpeedU[timeSel[ti], j, pi.iS];

                form.SetFormMode(
                    "水深平均流速-U(m/s) I=" + (pi.jS + 1).ToString(),   //視窗標題
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
            int iStart = 0, iEnd = p.inputGrid.GetI, jStart = 0, jEnd = p.inputGrid.GetJ;
            if (timeSel == null || timeSel.Length != 1)
            {
                MessageBox.Show("請選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!ParsingIDepthAverageFlowSpeedVResult())
            {
                MessageBox.Show("無法讀取輸出檔！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int timeIndex = FoundTimeSelInList(timeSel[0]);
            ResultTableForm form = new ResultTableForm();
            double[,] data = new double[p.inputGrid.GetJ, p.inputGrid.GetI];
            for (int j = 0; j < p.inputGrid.GetJ; ++j)
                for (int i = 0; i < p.inputGrid.GetI; ++i)
                    data[j, i] = depthAverageFlowSpeedV[timeIndex, j, i];
            form.SetFormMode(
                "水深平均流速-V(m/s) T=" + timeSel[0].ToString(),   //視窗標題
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
                data//資料
                );

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

        private double[, ,] depthAverageFlowSpeedU = null;
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

        private double[, ,] depthAverageFlowSpeedV = null;
        private bool ParsingIDepthAverageFlowSpeedVResult()
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

            while ((line = f.ReadLine()) != null)
            {
                if (!foundTime && line.StartsWith("  TIME="))
                {
                    if (timeSel[0] == Convert.ToDouble(line.Substring(9, 16).Trim()))
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

                int timeIndex = FoundTimeSelInList(timeSel[0]);
                if (foundTime && found)
                {

                    double[] ar = GetLineDouble(line, 10);
                    for (int i = 0; i < ar.Length; ++i)
                    {
                        depthAverageFlowSpeedV[timeIndex, i, count] = ar[i];
                    }

                    if (++count >= p.inputGrid.GetI)
                    {
                        break;
                    }
                }
            }
            return true;
        }

        private int FoundTimeSelInList(double d)
        {
            for(int i = 0; i < timeList.Count; ++i)
            {
                if (timeList[i] == d)
                    return i;
            }
            return -1;
        }

        private List<double> timeList = null;
        private bool ParsingTime()
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

            if (timeList == null)
            {
                timeList = new List<double>();
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

            form.SetFormMode("", GetTimeSelectionType(), timeList);
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
            if (param1Cmb.SelectedIndex == 0)
            {   
                return TableType.IJ;
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

        //bool CheckTimeSelectionEnable()
        //{
        //    int s = -1, e = -1;
        //    if (!GetPosRange(posIchk, p.inputGrid.GetI, posITxt, ref s, ref e))
        //    {
        //        return false;
        //    }

        //    if (!GetPosRange(posJchk, p.inputGrid.GetJ, posJTxt, ref s, ref e))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        enum TableType
        {
            Unknown,
            TimeI,
            TimeJ,
            IJ
        };
        //TableType tableType = TableType.Unknown;
    }
}

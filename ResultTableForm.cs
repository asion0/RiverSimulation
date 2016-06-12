using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiverSimulationApplication
{
    public partial class ResultTableForm : ResultUI
    {
        public ResultTableForm()
        {
            InitializeComponent();
        }

        public enum ResultTableType
        {
            IJT_Table, 
            IJTK_Table,
            IJTKM_Table,
            IJTM_Table,
        }
        protected ResultTableType formType = ResultTableType.IJT_Table;
        
        public void SetFormMode(
            string title,
            int colStart, int colEnd,
            int rowStart, int rowEnd,
            string tableName = "", 
            string colName = "", 
            string rowName = "",
            ResultTableType formType = ResultTableType.IJT_Table, 
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
            double[] timeList = null,
            int m = -1)
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
            this.m = m;
        }


        private void ResultTableForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            if (formType == ResultTableType.IJT_Table)
            {
                DataGridViewUtility.InitializeDataGridView2(
                    dataGridView, 
                    colStart, colEnd,   //行起始~結束
                    rowStart, rowEnd,   //列起始~結束
                    colWidth,           //行寬
                    rowHeadersWidth,    //列標題寬
                    title, //表格名稱
                    colName, //列名稱
                    rowName, //行名稱
                    false, //不顯示行號
                    false, //不顯示列號
                    false, //反轉行
                    false,  //反轉列
                    timeList);
                FillDataGridView();
            }
            else if (formType == ResultTableType.IJTK_Table)
            {
                DataGridViewUtility.InitializeDataGridView2(
                    dataGridView,
                    colStart, colEnd,   //行起始~結束
                    rowStart, rowEnd,   //列起始~結束
                    colWidth,           //行寬
                    rowHeadersWidth,    //列標題寬
                    title, //表格名稱
                    colName, //列名稱
                    rowName, //行名稱
                    false, //不顯示行號
                    false, //不顯示列號
                    false, //反轉行
                    false,  //反轉列
                    timeList);
                FillDataGridView();
            }
            else if (formType == ResultTableType.IJTKM_Table)
            {
                DataGridViewUtility.InitializeDataGridView2(
                    dataGridView,
                    colStart, colEnd,   //行起始~結束
                    rowStart, rowEnd,   //列起始~結束
                    colWidth,           //行寬
                    rowHeadersWidth,    //列標題寬
                    title, //表格名稱
                    colName, //列名稱
                    rowName, //行名稱
                    false, //不顯示行號
                    false, //不顯示列號
                    false, //反轉行
                    false,  //反轉列
                    timeList);
                FillDataGridView();
            }
            else if (formType == ResultTableType.IJTM_Table)
            {
                DataGridViewUtility.InitializeDataGridView2(
                    dataGridView,
                    colStart, colEnd,   //行起始~結束
                    rowStart, rowEnd,   //列起始~結束
                    colWidth,           //行寬
                    rowHeadersWidth,    //列標題寬
                    title, //表格名稱
                    colName, //列名稱
                    rowName, //行名稱
                    false, //不顯示行號
                    false, //不顯示列號
                    false, //反轉行
                    false,  //反轉列
                    timeList);
                FillDataGridView();
            }
        }

        private void FillDataGridView()
        {
            if (formType == ResultTableType.IJT_Table)
            {
                for (int x = colStart; x < colEnd; ++x)
                {
                    for (int y = rowStart; y < rowEnd; ++y)
                    {
                        if (sel1Index != -1 && sel2Index == -1 && xDim == 1 && yDim == 0)       //3D data X顯示J Y顯示I
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, ,])[y, x, sel1Index].ToString();
                        else if (sel1Index != -1 && sel2Index == -1 && xDim == 1 && yDim == 3)       //3D data X顯示J Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, ,])[sel1Index, x, y].ToString();
                        else if (sel1Index != -1 && sel2Index == -1 && xDim == 0 && yDim == 3)       //3D data X顯示I Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, ,])[x, sel1Index, y].ToString();
                    }
                }
            }
            else if (formType == ResultTableType.IJTK_Table)
            {
                for (int x = colStart; x < colEnd; ++x)
                {
                    for (int y = rowStart; y < rowEnd; ++y)
                    {
                        if (sel1Index != -1 && sel2Index != -1 && xDim == 1 && yDim == 0)       //4D data X顯示J Y顯示I
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , ,])[y, x, sel1Index, sel2Index].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 1 && yDim == 3)       //4D data X顯示J Y顯示K
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , ,])[sel1Index, x, sel2Index, y].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 0 && yDim == 3)       //4D data X顯示I Y顯示K
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , ,])[x, sel1Index, sel2Index, y].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 0 && yDim == 2)       //4D data X顯示J Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , ,])[x, sel1Index, y, sel2Index].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 1 && yDim == 2)       //4D data X顯示J Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , ,])[sel1Index, x, y, sel2Index].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 3 && yDim == 2)       //4D data X顯示K Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , ,])[sel1Index, sel2Index, y, x].ToString();
                    }
                }
            }
            else if (formType == ResultTableType.IJTKM_Table)
            {
                for (int x = colStart; x < colEnd; ++x)
                {
                    for (int y = rowStart; y < rowEnd; ++y)
                    {
                        if (sel1Index != -1 && sel2Index != -1 && xDim == 1 && yDim == 0)       //5D data X顯示J Y顯示I
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , , ,])[y, x, sel1Index, sel2Index, m].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 1 && yDim == 3)       //5D data X顯示J Y顯示K
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , , ,])[sel1Index, x, sel2Index, y, m].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 0 && yDim == 3)       //5D data X顯示I Y顯示K
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , , ,])[x, sel1Index, sel2Index, y, m].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 0 && yDim == 2)       //5D data X顯示J Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , , ,])[x, sel1Index, y, sel2Index, m].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 1 && yDim == 2)       //5D data X顯示J Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , , ,])[sel1Index, x, y, sel2Index, m].ToString();
                        else if (sel1Index != -1 && sel2Index != -1 && xDim == 3 && yDim == 2)       //5D data X顯示K Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , , ,])[sel1Index, sel2Index, y, x, m].ToString();
                    }
                }
            }
            else if (formType == ResultTableType.IJTM_Table)
            {
                for (int x = colStart; x < colEnd; ++x)
                {
                    for (int y = rowStart; y < rowEnd; ++y)
                    {
                        if (sel1Index != -1 && sel2Index == -1 && xDim == 1 && yDim == 0)       //5D data X顯示J Y顯示I
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , , ,])[y, x, sel1Index, 0, m].ToString();
                        else if (sel1Index != -1 && sel2Index == -1 && xDim == 1 && yDim == 3)       //5D data X顯示J Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , , ,])[sel1Index, x, y, 0, m].ToString();
                        else if (sel1Index != -1 && sel2Index == -1 && xDim == 0 && yDim == 3)       //5D data X顯示I Y顯示T
                            dataGridView[x - colStart, y - rowStart].Value = (initData as double[, , , ,])[x, sel1Index, y, 0, m].ToString();
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

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
        protected ResultTableType formType = ResultTableType.InitialBottomElevation;
        
        public void SetFormMode(
            string title,
            int colStart, int colEnd,
            int rowStart, int rowEnd,
            string tableName = "", 
            string colName = "", 
            string rowName = "",
            ResultTableType formType = ResultTableType.InitialBottomElevation, 
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

        private void ResultTableForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            if (formType == ResultTableType.InitialBottomElevation)
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

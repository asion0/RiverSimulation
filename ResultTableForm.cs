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
    public partial class ResultTableForm : Form
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

        private string title = "";
        private ResultTableType formType = ResultTableType.InitialBottomElevation;
        private int colStart;
        private int colEnd;
        private int rowStart;
        private int rowEnd;
        private int colWidth = 0;
        private int rowHeadersWidth = 0;
        private object initData = null;
        public void SetFormMode(string title,
            int colStart,
            int colEnd,
            int rowStart,
            int rowEnd,
            string tableName = "", 
            string colName = "", 
            string rowName = "",
            ResultTableType formType = ResultTableType.InitialBottomElevation, 
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
            /*
            hideSingle = onlyTable;
            this.colCount = colCount;
            this.rowCount = rowCount;
            this.title = title;
            this.tableName = tableName;
            this.nocolNum = nocolNum;
            this.noRowNum = noRowNum;
            this.colWidth = colWidth;
            this.rowHeadersWidth = rowHeadersWidth;
            this.inputFormType = inputFormType;
            CreateData(initData);
             * */
        }

        private void ResultTableForm_Load(object sender, EventArgs e)
        {

            this.Text = title;
            InitializeDataGridView();
            /*
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (inputFormType == InputFormType.SeparateForm || inputFormType == InputFormType.VerticalVelocityDistributionForm || inputFormType == TableInputForm.InputFormType.BoundaryTime)
            {
                averageBtn.Visible = true;
            }

            if (unitLbl.Text.Length == 0)
            {
                unitLbl.Visible = false;
            }
            */
        }

        private void InitializeDataGridView()
        {
            if (formType == ResultTableType.InitialBottomElevation)
            {
                DataGridViewUtility.InitializeDataGridView2(dataGridView, colStart, colEnd, rowStart, rowEnd, colWidth, rowHeadersWidth,
                    "", "", "", false, false, false, false);
                FillDataGridView();
            }
        }

        private void FillDataGridView()
        {
            switch (formType)
            {
                case ResultTableType.InitialBottomElevation:
                    //編輯陣列I * J
                    for (int i = colStart; i < colEnd; ++i)
                    {
                        for (int j = rowStart; j < rowEnd; ++j)
                        {
                            dataGridView[i - colStart, j - rowStart].Value = (initData as double[,])[i, j].ToString();
                        }
                    }
                    break;
            }
        }
    }
}

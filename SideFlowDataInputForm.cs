using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PictureBoxCtrl;
using System.Diagnostics;

namespace RiverSimulationApplication
{
    public partial class SideFlowDataInputForm : Form
    {
        public SideFlowDataInputForm()
        {
            InitializeComponent();
        }

        public enum InputFormType
        {
            Type1 = 1,
            Type2,
            Type3,
            Type4,
            Type5,
            Type6,
            Type7,
            Type8,

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
        private InputFormType _inputFormType = InputFormType.GenericDouble;

        bool hideSingle = false;        //隱藏 均一值 選項
        int colCount = 26;              //直行數目
        int rowCount = 50;              //橫列數目
        string title;                   //視窗標題
        RiverSimulationProfile.SideFlowObject inputData;

        string tableName;
        string colName;
        string rowName;
        bool nocolNum = false;
        bool noRowNum = false;
        int colWidth = 48;
        int rowHeadersWidth = 64;
        //public string[] columnNames = null;
        public RiverSimulationProfile p = null;
        public void SetFormMode(string title, int colCount, int rowCount, string tableName = "", string colName = "", string rowName = "", 
           int colWidth = 48, int rowHeadersWidth = 64,
           bool onlyTable = true, bool nocolNum = false, bool noRowNum = false, object initData = null)
        {
            hideSingle = onlyTable;
            this.colCount = colCount;
            this.rowCount = rowCount;
            this.title = title;
            this.tableName = tableName;
            this.colName = colName;
            this.rowName = rowName;
            this.nocolNum = nocolNum;
            this.noRowNum = noRowNum;
            this.colWidth = colWidth;
            this.rowHeadersWidth = rowHeadersWidth;
            this.inputFormType = inputFormType;
            this.inputData = initData as RiverSimulationProfile.SideFlowObject;
            this.p = RiverSimulationProfile.profile;
            tabControl.Enabled = true;
            CreateData(inputData.flowData);
        }

        private void TableInputForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            InitializeDataGridView();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (inputFormType == InputFormType.SeparateForm || inputFormType == InputFormType.VerticalVelocityDistributionForm || inputFormType == InputFormType.BoundaryTime)
            {
                averageBtn.Visible = true;
            }

            if (unitLbl.Text.Length == 0)
            {
                unitLbl.Visible = false;
            }
        }

        private void InitializeDataGridView()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            switch (o.type)
            {
                case RiverSimulationProfile.TwoInOne.Type.UseValue:
                    singleValueRdo.Checked = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    tableValueRdo.Checked = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.None:
                    Debug.Assert(false);
                    break;
            }


           if (inputFormType == InputFormType.Type1)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, "", "", false, false, false, false);
                dataGridView1.Columns[0].Name = "主流方向流量(cms)";
                dataGridView1.Columns[1].Name = "側方向流量(cms)";
            }
            else if (inputFormType == InputFormType.Type2)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    "時間 T (sec)", "主流方向流量(cms)", "", true, true, false, false);
                DataGridViewUtility.InitializeDataGridView(dataGridView2, colCount, rowCount, colWidth, rowHeadersWidth,
                    "時間 T (sec)", "側方向流量(cms)", "", true, true, false, false);
                for (int i = 0; i < p.boundaryTime.Length; ++i)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
                    dataGridView2.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
                }
                dataGridView1.Columns[0].Name = "主流方向流量(cms)";
                dataGridView2.Columns[0].Name = "側方向流量(cms)";
            }
            else if (inputFormType == InputFormType.Type3)
            {
               DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, 72, 140,
                   "", "", "主流方向流量(cms)", false, true, false, false);
               DataGridViewUtility.InitializeDataGridView(dataGridView2, colCount, rowCount, 72, 140,
                   "", "", "側方向流量(cms)", false, true, false, false);
               for (int i = 0; i < colCount; ++i)
               {
                   int start = inputData.sideFlowPoints.OrderBy(p => p.X).ToArray()[0].X;
                   dataGridView1.Columns[i].Name = (i + start + 1).ToString();
                   dataGridView2.Columns[i].Name = (i + start + 1).ToString();
               }
            }
            else if (inputFormType == InputFormType.Type4)
            {
               DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, 72, 140,
                   "時間 T (sec)", "", "", false, false, false, false);
               DataGridViewUtility.InitializeDataGridView(dataGridView2, colCount, rowCount, 72, 140,
                   "時間 T (sec)", "", "", false, false, false, false);
               for (int i = 0; i < p.boundaryTime.Length; ++i)
               {
                   dataGridView1.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
                   dataGridView2.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
               }
               //dataGridView1.Columns[0].Name = "主流方向流量(cms)";
               //dataGridView2.Columns[0].Name = "側方向流量(cms)";

               for (int i = 0; i < colCount; ++i)
               {
                   int start = inputData.sideFlowPoints.OrderBy(p => p.X).ToArray()[0].X;
                   dataGridView1.Columns[i].Name = (i + start + 1).ToString();
                   dataGridView2.Columns[i].Name = (i + start + 1).ToString();
               }
           }



            else if (inputFormType == InputFormType.VerticalVelocityDistributionForm)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum, false, true);
                dataGridView1.Columns[0].Name = "垂向分層位置";
                dataGridView1.Columns[1].Name = "比例係數(百分比)";  
            }
            else if (inputFormType == InputFormType.BoundaryTime)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum, false, false);

                timeStepLbl.Visible = true;
                timeGapTxt.Visible = true;
                settingBtn.Visible = true;
            }            
            else if (inputFormType == InputFormType.TwoInOneDouble ||
                    inputFormType == InputFormType.TwoInOneDoubleGreaterThanZero ||
                    inputFormType == InputFormType.TwoInOneDoubleGreaterThanOrEqualZero)
            {
                //RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                Debug.Assert(o != null);

                singleValueTxt.Text = o.ValueDouble()[0].ToString();
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum);
                selInputBtn.Visible = true;

                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        singleValueRdo.Checked = true;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        tableValueRdo.Checked = true;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.None:
                        singleValueRdo.Checked = false;
                        tableValueRdo.Checked = false;
                        singleValueTxt.Enabled = false;
                        dataGridView1.Enabled = false;
                        break;
                }
            }
            else if (inputFormType == InputFormType.GenericDouble ||
                    inputFormType == InputFormType.GenericDoubleGreaterThanZero ||
                    inputFormType == InputFormType.GenericDoubleGreaterThanOrEqualZero)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                      tableName, colName, rowName, nocolNum, noRowNum);
            }
            else if (inputFormType == InputFormType.FlowConditionsSettingConstant )
            {
                //RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                Debug.Assert(o != null);

                singleValueTxt.Text = o.ValueDouble()[0].ToString();
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    "", colName, rowName, nocolNum, true);
                dataGridView1.Rows[0].HeaderCell.Value = tableName;

                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        singleValueRdo.Checked = true;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        tableValueRdo.Checked = true;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.None:
                        singleValueRdo.Checked = false;
                        tableValueRdo.Checked = false;
                        singleValueTxt.Enabled = false;
                        dataGridView1.Enabled = false;
                        break;
                }
            } 
            else if(inputFormType == InputFormType.FlowConditionsSettingVariable)
            {
                //RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                Debug.Assert(o != null);

                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        singleValueRdo.Checked = true;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        tableValueRdo.Checked = true;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.None:
                        singleValueRdo.Checked = false;
                        tableValueRdo.Checked = false;
                        singleValueTxt.Enabled = false;
                        dataGridView1.Enabled = false;
                        break;
                }
                singleValueTxt.Visible = (inputFormType == InputFormType.FlowConditionsSettingConstant) ? true :false;
            }
            else if (inputFormType == InputFormType.SeabedThicknessForm)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum, false, true);
                dataGridView1.Rows[0].HeaderCell.Value = "作用層";
            }
            else if (inputFormType == InputFormType.SedimentCompositionRatioForm)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum, false, true);
                dataGridView1.Rows[0].HeaderCell.Value = "作用層";
            }
            else if (inputFormType == InputFormType.BottomBedParticleSizeRatio)
            {
                InitTableBottomBedParticleSizeRatio();
            }

            else
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum);
            }

            FillDataGridView();
        }

        private void InitTableBottomBedParticleSizeRatio()
        {
            DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
               tableName, colName, rowName, nocolNum, noRowNum);

            if (p.IsVariableFlowType())
            {   //變量流需顯示邊界時間欄位
                for (int jw = 0; jw < p.boundaryTimeNumber; ++jw)
                {
                    dataGridView1.Rows[jw].HeaderCell.Value = p.boundaryTime[jw].ToString();
                }
            }

        }


        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.PasteFromeExcel(dataGridView1);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.CopyToClipboard(dataGridView1);
        }

        private void valueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.FillSelectedValue(dataGridView1);
        }


        public InputFormType inputFormType
        {
            get { return _inputFormType; }
            set { _inputFormType = value; }
        }

        private object _data = null;
        public object data
        {
            get { return _data; }
        }

        public double[] SeabedThicknessData()
        {
            if (_data == null)
                return null;

            return _data as double[];
        }

        public double[] SeparateData()
        {
            if (_data == null)
                return null;

            return _data as double[];
        }

        public double[,] VerticalVelocityDistributionData()
        {
            if (_data == null)
                return null;

            return _data as double[,];
        }
        public double[] BoundaryTimeData()
        {
            if (_data == null)
                return null;

            return _data as double[];
        }

        public double[,] SedimentCompositionRatioData()
        {
            if (_data == null)
                return null;

            return _data as double[,];
        }

        public RiverSimulationProfile.TwoInOne GenericTwoInOneData()
        {
            if (_data == null)
                return null;

            return _data as RiverSimulationProfile.TwoInOne;
        }

        public double[,] GenericDoubleData()
        {
            if (_data == null)
                return null;

            return _data as double[,];
        }

        public RiverSimulationProfile.TwoInOne FlowQuantityData()
        {
            if (_data == null)
                return null;

            return _data as RiverSimulationProfile.TwoInOne;
        }

        private readonly string SeparateFormCellFormat = "F2";
        private void CreateData(Object d)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            RiverSimulationProfile.TwoInOne o = d as RiverSimulationProfile.TwoInOne;
            RiverSimulationProfile.TwoInOne _d = null;
            //第1, 3種：亞臨界流、均一值/逐點給、定量流
            if (inputData.criticalFlowType == RiverSimulationProfile.CriticalFlowType.SubCriticalFlow &&
                p.IsConstantFlowType())
            {
                if (d == null)
                {
                    _data = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.TwoDim, RiverSimulationProfile.TwoInOne.ArrayType.ThreeDim, RiverSimulationProfile.TwoInOne.Type.UseValue);
                    _d = _data as RiverSimulationProfile.TwoInOne;
                }
                else
                {
                    _data = new RiverSimulationProfile.TwoInOne(o);
                    _d = _data as RiverSimulationProfile.TwoInOne;
                }
                if (_d.type == RiverSimulationProfile.TwoInOne.Type.UseValue)
                {
                    colCount = 2;
                    rowCount = 1;
                    _inputFormType = InputFormType.Type1;
                    tabPage1.Parent = tabControl;
                    tabPage2.Parent = null;
                    tabPage3.Parent = null;
                    tabPage1.Text = "";

                    if (_d.ValueNull())
                    {
                        (_data as RiverSimulationProfile.TwoInOne).CreateValue2D(colCount, rowCount);
                    }
                }
                else
                {
                    colCount = inputData.sideFlowPoints.Count;
                    rowCount = 1;
                    _inputFormType = InputFormType.Type3;
                    tabPage1.Parent = tabControl;
                    tabPage2.Parent = tabControl;
                    tabPage3.Parent = null;
                    tabPage1.Text = "主流方向流量";
                    tabPage2.Text = "側方向流量";

                    if (_d.ArrayNull())
                    {
                        (_data as RiverSimulationProfile.TwoInOne).CreateArray3D(colCount, rowCount, 2);
                    }
                }
            }
            //第2, 4種：亞臨界流、均一值/逐點給、變量流
            else if (inputData.criticalFlowType == RiverSimulationProfile.CriticalFlowType.SubCriticalFlow &&
                p.IsVariableFlowType())
            {
                if (d == null)
                {
                    _data = new RiverSimulationProfile.TwoInOne(RiverSimulationProfile.TwoInOne.ValueType.ThreeDim, RiverSimulationProfile.TwoInOne.ArrayType.ThreeDim, RiverSimulationProfile.TwoInOne.Type.UseValue);
                    _d = _data as RiverSimulationProfile.TwoInOne;
                }
                else
                {
                    _data = new RiverSimulationProfile.TwoInOne(o);
                    _d = _data as RiverSimulationProfile.TwoInOne;
                }
                if (_d.type == RiverSimulationProfile.TwoInOne.Type.UseValue)
                {
                    colCount = 1;
                    rowCount = p.boundaryTime.Length;
                    _inputFormType = InputFormType.Type2;
                    tabPage1.Parent = tabControl;
                    tabPage2.Parent = tabControl;
                    tabPage3.Parent = null;
                    tabPage1.Text = "主流方向流量";
                    tabPage2.Text = "側方向流量";

                    if (_d.ValueNull())
                    {
                        (_data as RiverSimulationProfile.TwoInOne).CreateValue3D(colCount, rowCount, 2);
                    }
                }
                else
                {
                    colCount = inputData.sideFlowPoints.Count;
                    rowCount = p.boundaryTime.Length;
                    _inputFormType = InputFormType.Type4;
                    tabPage1.Parent = tabControl;
                    tabPage2.Parent = tabControl;
                    tabPage3.Parent = null;
                    tabPage1.Text = "主流方向流量";
                    tabPage2.Text = "側方向流量";

                    if (_d.ArrayNull())
                    {
                        (_data as RiverSimulationProfile.TwoInOne).CreateArray3D(colCount, rowCount, 2);
                    }
                }
            }
            /*    
            switch (_inputFormType)
            {
                case InputFormType.TwoInOneDouble:
                case InputFormType.TwoInOneDoubleGreaterThanZero:
                case InputFormType.TwoInOneDoubleGreaterThanOrEqualZero:
                    Debug.Assert(o != null);
                    _data = new RiverSimulationProfile.TwoInOne(d as RiverSimulationProfile.TwoInOne);
                    _d = _data as RiverSimulationProfile.TwoInOne;
                    Debug.Assert(_d != null);
                    if(o.ValueNull() || o.ArrayNull())
                    {
                        (_data as RiverSimulationProfile.TwoInOne).CreateDouble2D(colCount, rowCount);
                    }
                    if (_d.Array2D().GetLength(0) != colCount || _d.Array2D().GetLength(1) != rowCount)
                    {
                        Debug.Assert(false);
                    }
                    break;
                case InputFormType.GenericDouble:
                case InputFormType.GenericDoubleGreaterThanZero:
                case InputFormType.GenericDoubleGreaterThanOrEqualZero:
                case InputFormType.BottomBedParticleSizeRatio:
                    if (d == null)
                    {
                        _data = new double[colCount, rowCount];
                    }
                    else
                    {
                        _data = (double [,])(d as double[,]).Clone();
                    }
                    break;                
                case InputFormType.SeabedThicknessForm:
                    if (d == null)
                    {
                        _data = new double[rowCount];
                    }
                    else
                    {
                        _data = (double [])(d as double[]).Clone();
                    }
                    break;
                case InputFormType.SedimentCompositionRatioForm:
                    if (d == null)
                    {
                        _data = new double[colCount, rowCount];
                    }
                    else
                    {
                        _data = (double [,])(d as double[,]).Clone();
                    }
                   break;
                case InputFormType.SeparateForm:
                case InputFormType.BoundaryTime:
                    if (d == null)
                    {
                        _data = new double[rowCount];
                    }
                    else
                    {
                        _data = (double [])(d as double[]).Clone();
                    }
                    break;
                case InputFormType.VerticalVelocityDistributionForm:
                    if (d == null)
                    {
                        _data = new double[2, rowCount];
                    }
                    else
                    {
                        _data = (double [,])(d as double[,]).Clone();
                    }
                    break;
                case InputFormType.BottomElevationForm:
                    if (d == null)
                    {
                        _data = new CoorPoint[colCount, rowCount];
                    }
                    else
                    {
                        _data = (CoorPoint[,])(d as CoorPoint[,]).Clone();
                    }
                    break;
                case InputFormType.FlowConditionsSettingConstant:
                case InputFormType.FlowConditionsSettingVariable:
                    Debug.Assert(o != null);
                    _data = new RiverSimulationProfile.TwoInOne(d as RiverSimulationProfile.TwoInOne);
                    _d = _data as RiverSimulationProfile.TwoInOne;
                    if (o.ValueNull() || o.ArrayNull())
                    {   //rowCount : Q1 ~ Q5, colCount : J1 ~ J15
                        _d.CreateDouble2D(colCount, rowCount);
                       // (_data as RiverSimulationProfile.TwoInOne).dataValue = new double[colCount, rowCount];
                    }
                    break;
            }
             * */
        }

        private void FillDataGridView()
        {
            switch (_inputFormType)
            {
                case InputFormType.Type1:
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView1[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Value2D()[i, j].ToString();
                        }
                    }
                    break;
                case InputFormType.Type2:
                    for (int j = 0; j < rowCount; ++j)
                    {
                        dataGridView1[0, j].Value = (_data as RiverSimulationProfile.TwoInOne).Value3D()[0, j, 0].ToString();
                        dataGridView2[0, j].Value = (_data as RiverSimulationProfile.TwoInOne).Value3D()[0, j, 1].ToString();
                    }
                    break;
                case InputFormType.Type3:
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView1[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 0].ToString();
                            dataGridView2[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 1].ToString();
                        }
                    }
                    break;
                case InputFormType.Type4:
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView1[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 0].ToString();
                            dataGridView2[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 1].ToString();
                        }
                    }
                    break;




                case InputFormType.GenericDouble:
                case InputFormType.GenericDoubleGreaterThanZero:
                case InputFormType.GenericDoubleGreaterThanOrEqualZero:
                case InputFormType.BottomBedParticleSizeRatio:
                    //編輯陣列I * J
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView1[i, j].Value = (_data as double[,])[i, j].ToString();
                        }
                    }
                    break;
                case InputFormType.TwoInOneDouble:
                case InputFormType.TwoInOneDoubleGreaterThanZero:
                case InputFormType.TwoInOneDoubleGreaterThanOrEqualZero:
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView1[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array2D()[i, j].ToString();
                        }
                    }
                    break;
                case InputFormType.FlowConditionsSettingConstant:
                    for (int i = 0; i < dataGridView1.ColumnCount; ++i)
                    {
                        dataGridView1[i, 0].Value = (_data as RiverSimulationProfile.TwoInOne).Array2D()[i, 0].ToString();
                    }
                    break;
                case InputFormType.FlowConditionsSettingVariable:
                    for (int i = 1; i < dataGridView1.ColumnCount; ++i)
                    {
                        for (int j = 0; j < dataGridView1.RowCount; ++j)
                        {
                            dataGridView1[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array2D()[i - 1, j].ToString();
                        }
                    }

                    if (_inputFormType== InputFormType.FlowConditionsSettingVariable)
                    {   //變量流要填入邊界時間，且不可編輯
                        for (int i = 0; i < dataGridView1.RowCount; ++i)
                        {
                            dataGridView1[0, i].Value = p.boundaryTime[i].ToString();
                            dataGridView1[0, i].Style.BackColor = Color.LightGray;
                            dataGridView1[0, i].ReadOnly = true;
                        }
                    }
                    break;
                case InputFormType.SeabedThicknessForm: 
                //底床分層厚度輸入，直的一行，倒序
                    for (int i = 0; i < rowCount; ++i)
                    {
                        dataGridView1[0, rowCount - i - 1].Value = (_data as double[])[i].ToString();
                    }
                    break;
                case InputFormType.SedimentCompositionRatioForm:    
                //泥砂組成比例輸入，每列最後一欄(橫列最右一格)要累加成100，不能編輯。
                //垂直倒序
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView1[i, rowCount - j - 1].Value = (_data as double[,])[i, j].ToString();
                            if(i == colCount - 1)
                            {
                                dataGridView1[i, j].ReadOnly = true;
                            }
                        }
                    }
                    AutoFinishConvertSedimentCompositionRatioCell();
                    break;
                case InputFormType.SeparateForm:
                    //垂直向隔網分層比例輸入，直的一行，倒序
                    //垂直遞增不能相同或是減少
                    for (int i = 0; i < rowCount; ++i)
                    {
                        dataGridView1[0, rowCount - i - 1].Value = (_data as double[])[i].ToString();
                    }
                    dataGridView1[0, 0].ReadOnly = true;
                    dataGridView1[0, 0].Style.BackColor = Color.LightGray;
                    dataGridView1[0, 0].Style.ForeColor = Color.Red;
                    dataGridView1[0, rowCount - 1].ReadOnly = true;
                    dataGridView1[0, rowCount - 1].Style.BackColor = Color.LightGray;
                    dataGridView1[0, rowCount - 1].Style.ForeColor = Color.Red;
                    dataGridView1[0, 0].Selected = false;
                    dataGridView1[0, 1].Selected = true;
                    dataGridView1[0, 0].Value = 1.ToString(SeparateFormCellFormat);
                    dataGridView1[0, rowCount - 1].Value = 0.ToString(SeparateFormCellFormat);
                    break;
                case InputFormType.VerticalVelocityDistributionForm:
                    //垂直向隔網分層比例輸入，直的一行，倒序
                    //垂直遞增不能相同或是減少
                    for (int i = 0; i < rowCount; ++i)
                    {
                        dataGridView1[0, rowCount - i - 1].Value = (_data as double[,])[0, i].ToString();
                        dataGridView1[1, rowCount - i - 1].Value = (_data as double[,])[1, i].ToString();
                    }
                    dataGridView1[0, 0].ReadOnly = true;
                    dataGridView1[0, 0].Style.BackColor = Color.LightGray;
                    dataGridView1[0, 0].Style.ForeColor = Color.Red;
                    dataGridView1[0, rowCount - 1].ReadOnly = true;
                    dataGridView1[0, rowCount - 1].Style.BackColor = Color.LightGray;
                    dataGridView1[0, rowCount - 1].Style.ForeColor = Color.Red;
                    dataGridView1[0, 0].Selected = false;
                    dataGridView1[0, 1].Selected = true;
                    dataGridView1[0, 0].Value = 1.ToString(SeparateFormCellFormat);
                    dataGridView1[0, rowCount - 1].Value = 0.ToString(SeparateFormCellFormat);
                    break;
                case InputFormType.BoundaryTime:
                    //垂直向隔網分層比例輸入，直的一行，倒序
                    //垂直遞增不能相同或是減少
                    for (int i = 0; i < rowCount; ++i)
                    {
                        dataGridView1[0, i].Value = (_data as double[])[i].ToString();
                    }
                    //第一欄不能編輯，必須為0
                    dataGridView1[0, 0].ReadOnly = true;
                    dataGridView1[0, 0].Style.BackColor = Color.LightGray;
                    dataGridView1[0, 0].Style.ForeColor = Color.Red;
                    dataGridView1[0, 0].Value = 0.ToString();
                    //最後一欄不能編輯，必須為 1.1.1 總模擬時間
                    dataGridView1[0, rowCount - 1].ReadOnly = true;
                    dataGridView1[0, rowCount - 1].Style.BackColor = Color.LightGray;
                    dataGridView1[0, rowCount - 1].Style.ForeColor = Color.Red;
                    dataGridView1[0, rowCount - 1].Value = RiverSimulationProfile.profile.totalSimulationTime.ToString();

                    dataGridView1[0, 0].Selected = false;
                    dataGridView1[0, 1].Selected = true;
                    break;                
       
            }
            dataGridView1.PerformLayout();

        }

        private bool ConvertSeabedThicknessData()
        {
            try
            {
                DataGridView v = dataGridView1;
                for (int i = 0; i < rowCount; ++i)
                {
                    (_data as double[])[rowCount - i - 1] = Convert.ToDouble(v[0, i].Value);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
 
        private bool ConvertSeparateData()
        {
            try
            {
                DataGridView v = dataGridView1;
                for (int i = 0; i < rowCount; ++i)
                {
                    (_data as double[])[rowCount - i - 1] = Convert.ToDouble(v[0, i].Value);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertVerticalVelocityDistributionData()
        {
            try
            {
                DataGridView v = dataGridView1;
                for (int i = 0; i < rowCount; ++i)
                {
                    (_data as double[,])[0, rowCount - i - 1] = Convert.ToDouble(v[0, i].Value);
                    (_data as double[,])[1, rowCount - i - 1] = Convert.ToDouble(v[1, i].Value);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertBoundaryTimeData()
        {
            try
            {
                DataGridView v = dataGridView1;
                for (int i = 0; i < rowCount; ++i)
                {
                    (_data as double[])[i] = Convert.ToDouble(v[0, i].Value);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertSedimentCompositionRatioData()
        {
            if (!AutoFinishConvertSedimentCompositionRatioCell())
            {
                return false;
            }
            try
            {
                DataGridView v = dataGridView1;
                for (int i = 0; i < colCount; ++i)
                {
                    for (int j = 0; j < rowCount; ++j)
                    {
                        (_data as double[,])[i, rowCount - j - 1] = Convert.ToDouble(v[i, j].Value);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertTwoInOneDouble()
        {
            try
            {
                DataGridView v = dataGridView1;
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                for (int i = 0; i < colCount; ++i)
                {
                    for (int j = 0; j < rowCount; ++j)
                    {
                        o.Array2D()[i, j] = Convert.ToDouble(v[i, j].Value);
                    }
                }
                if (singleValueTxt.Enabled)
                {
                    o.ValueDouble()[0] = Convert.ToDouble(singleValueTxt.Text);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertType1()
        {
            try
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                DataGridView v = dataGridView1;
                o.Value2D()[0, 0] = Convert.ToDouble(v[0, 0].Value);
                o.Value2D()[1, 0] = Convert.ToDouble(v[1, 0].Value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertType3()
        {
            try
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                DataGridView v = dataGridView1;
                for (int i = 0; i < colCount; ++i)
                {
                    o.Array3D()[i, 0, 0] = Convert.ToDouble(dataGridView1[i, 0].Value);
                    o.Array3D()[i, 0, 1] = Convert.ToDouble(dataGridView2[i, 0].Value);
                    
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertType2()
        {
            try
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                for (int j = 0; j < rowCount; ++j)
                {
                    o.Value3D()[0, j, 0] = Convert.ToDouble(dataGridView1[0, j].Value);
                    o.Value3D()[0, j, 1] = Convert.ToDouble(dataGridView2[0, j].Value);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertType4()
        {
            try
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                for (int j = 0; j < rowCount; ++j)
                {
                    o.Array3D()[0, j, 0] = Convert.ToDouble(dataGridView1[0, j].Value);
                    o.Array3D()[0, j, 1] = Convert.ToDouble(dataGridView2[0, j].Value);

                }
            }
            catch
            {
                return false;
            }
            return true;
        }



        private bool ConvertGenericDouble()
        {
            try
            {
                DataGridView v = dataGridView1;
                for (int i = 0; i < colCount; ++i)
                {
                    for (int j = 0; j < rowCount; ++j)
                    {
                        (_data as double[,])[i, j] = Convert.ToDouble(v[i, j].Value);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertBottomBedParticleSizeRatio()
        {
            if (rowCount != 1 && !AutoFinishConvertBottomBedParticleSizeRatioCell())
            {
                return false;
            }

            try
            {
                DataGridView v = dataGridView1;
                for (int i = 0; i < colCount; ++i)
                {
                    for (int j = 0; j < rowCount; ++j)
                    {
                        (_data as double[,])[i, j] = Convert.ToDouble(v[i, j].Value);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertFlowConditionsSettingConstant()
        {
            try
            {
                DataGridView v = dataGridView1;
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                for (int i = 0; i < v.ColumnCount; ++i)
                {
                    o.Array2D()[i, 0] = Convert.ToDouble(v[i, 0].Value);
                }
                if (singleValueTxt.Enabled)
                {
                    o.ValueDouble()[0] = Convert.ToDouble(singleValueTxt.Text);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertFlowConditionsSettingVariable()
        {
            try
            {
                DataGridView v = dataGridView1;
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                for (int i = 1; i < v.ColumnCount; ++i)
                {
                    for (int j = 0; j < v.RowCount; ++j)
                    {
                        o.Array2D()[i - 1, j] = Convert.ToDouble(v[i, j].Value);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool CalSumOfOneRow(int index, ref double sum)
        {
            sum = 0.0;
            for(int i = 0; i < colCount - 1; ++i)
            {
                try
                {
                    sum += Convert.ToDouble(dataGridView1[i, index].Value);
                }
                catch
                {
                    return false;
                }
            }
            if (sum > 100.0)
            {
                return false;
            }

            return true;
        }

        private bool AutoFinishConvertSedimentCompositionRatioCell()
        {
            double sum = 0.0;
            bool allPass = true;
            for(int j = 0; j < rowCount; ++j)
            {
                if (!CalSumOfOneRow(j, ref sum))
                {
                    allPass = false;
                    continue;
                }
                dataGridView1[colCount - 1, j].Value = (100.0 - sum).ToString();
            }
            return allPass;
        }

        private bool AutoFinishConvertSeparateCell()
        {
            double last = 1.0;
            bool allPass = true;
            for (int i = 1; i < rowCount - 1; ++i)
            {
                try
                {
                    double v = Convert.ToDouble(dataGridView1[0, i].Value);
                    if (last <= v || v >= 1.0)
                    {
                        return false;
                    }
                    last = v;
                }
                catch
                {
                    return false;
                }
            }
            return allPass;
        }

        private bool AutoFinishConvertVerticalVelocityDistributionCell()
        {
            double last = 1.0;
            bool allPass = true;
            for (int i = 1; i < rowCount - 1; ++i)
            {
                try
                {
                    double v = Convert.ToDouble(dataGridView1[0, i].Value);
                    if (last <= v || v >= 1.0)
                    {
                        return false;
                    }
                    last = v;
                }
                catch
                {
                    return false;
                }
            }
            return allPass;
        }

        private bool AutoFinishConvertVerticalVelocityDistributionCell2()
        {
            double total = 0;
            try
            {
                double v, vp, vm, po;
                for (int i = 1; i < rowCount - 1; ++i)
                {
                    v = Convert.ToDouble(dataGridView1[0, i].Value);  //0.75
                    vp = Convert.ToDouble(dataGridView1[0, i + 1].Value); //0.5
                    vm = Convert.ToDouble(dataGridView1[0, i - 1].Value); //100
                    po = Convert.ToDouble(dataGridView1[1, i].Value);  //150
                    total += ((v - vp) / 2 + (vm - v) / 2) * po;
                }
                v = Convert.ToDouble(dataGridView1[0, 0].Value);  //1.00
                vp = Convert.ToDouble(dataGridView1[0, 1].Value); //0.75
                po = Convert.ToDouble(dataGridView1[1, 0].Value);  //200
                total += ((v - vp) / 2 ) * po;

                v = Convert.ToDouble(dataGridView1[0, rowCount - 1].Value);  //0.00
                vm = Convert.ToDouble(dataGridView1[0, rowCount - 2].Value); //0.25
                po = Convert.ToDouble(dataGridView1[1, rowCount - 1].Value);  //0
                total += ((vm - v) / 2) * po;            
            }
            catch
            {
                return false;
            }
            return (total == 100);
        }

        private bool AutoFinishConvertBottomBedParticleSizeRatioCell()
        {
            double sum = 0.0;
            bool allPass = true;
            for (int j = 0; j < rowCount; ++j)
            {
                if (!CalSumOfOneRow(j, ref sum))
                {
                    allPass = false;
                    continue;
                }
                dataGridView1[colCount - 1, j].Value = (100.0 - sum).ToString();
            }
            return allPass;
        }

        private bool AutoFinishConvertBoundaryTimeCell()
        {
            bool allPass = true;
            try
            {
                double last = 0;
                double max = RiverSimulationProfile.profile.totalSimulationTime;

                for (int i = 1; i < rowCount - 1; ++i)
                {
                    double v = Convert.ToDouble(dataGridView1[0, i].Value);
                    if (last >= v || v >= max)
                    {
                        return false;
                    }
                    last = v;
                }
            }
            catch
            {
                return false;
            }
            return allPass;
        }

        private void AutoFunction()
        {
            switch (_inputFormType)
            {
                case InputFormType.GenericDouble:
                case InputFormType.GenericDoubleGreaterThanZero:
                case InputFormType.GenericDoubleGreaterThanOrEqualZero:
                    //_data = null;
                    break;
                case InputFormType.SeabedThicknessForm:
                    break;
                case InputFormType.SedimentCompositionRatioForm:
                    AutoFinishConvertSedimentCompositionRatioCell();
                    break;
                case InputFormType.SeparateForm:
                    AutoFinishConvertSeparateCell();
                    break;
                case InputFormType.VerticalVelocityDistributionForm:
                    AutoFinishConvertVerticalVelocityDistributionCell();
                    break;
                case InputFormType.BoundaryTime:
                    AutoFinishConvertBoundaryTimeCell();
                    break;
                case InputFormType.BottomElevationForm:
                    break;
                case InputFormType.BottomBedParticleSizeRatio:
                    AutoFinishConvertBottomBedParticleSizeRatioCell();
                    break;

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AutoFunction();
        }

        private void averageBtn_Click(object sender, EventArgs e)
        {
            if (inputFormType == InputFormType.BoundaryTime)
            {
                double step = Convert.ToDouble(dataGridView1[0, rowCount - 1].Value) / (rowCount - 1);
                double v = step;
                for (int i = 1; i < rowCount - 1; ++i)
                {
                    dataGridView1[0, i].Value = v.ToString(SeparateFormCellFormat);
                    v += step;
                }
            }
            else
            {
                double step = 1.0 / (rowCount - 1);
                double v = 1.0 - step;
                for (int i = 1; i < rowCount - 1; ++i)
                {
                    dataGridView1[0, i].Value = v.ToString(SeparateFormCellFormat);
                    v -= step;
                }
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            switch (_inputFormType)
            {
                case InputFormType.Type1:
                    isSuccess = ConvertType1();
                    break;
                case InputFormType.Type2:
                    isSuccess = ConvertType2();
                    break;              
                case InputFormType.Type3:
                    isSuccess = ConvertType3();
                    break;
                case InputFormType.Type4:
                    isSuccess = ConvertType4();
                    break;                
                
                
                case InputFormType.GenericDouble:
                case InputFormType.GenericDoubleGreaterThanZero:
                case InputFormType.GenericDoubleGreaterThanOrEqualZero:
                    isSuccess = ConvertGenericDouble();
                    break;
                case InputFormType.BottomBedParticleSizeRatio:
                    isSuccess = ConvertBottomBedParticleSizeRatio();
                    break;
                case InputFormType.TwoInOneDouble:
                case InputFormType.TwoInOneDoubleGreaterThanZero:
                case InputFormType.TwoInOneDoubleGreaterThanOrEqualZero:
                    isSuccess = ConvertTwoInOneDouble();
                    break;
                case InputFormType.FlowConditionsSettingConstant:
                    isSuccess = ConvertFlowConditionsSettingConstant();
                    break;
                case InputFormType.FlowConditionsSettingVariable:
                    isSuccess = ConvertFlowConditionsSettingVariable();
                    break;                
                case InputFormType.SeabedThicknessForm:
                    isSuccess = ConvertSeabedThicknessData();
                    break;
                case InputFormType.SedimentCompositionRatioForm:
                    isSuccess = ConvertSedimentCompositionRatioData();
                    break;
                case InputFormType.SeparateForm:
                    if (AutoFinishConvertSeparateCell()) 
                    {
                        isSuccess = ConvertSeparateData();
                    }
                    break;
                case InputFormType.VerticalVelocityDistributionForm:
                    if (AutoFinishConvertVerticalVelocityDistributionCell() && AutoFinishConvertVerticalVelocityDistributionCell2())
                    {
                        isSuccess = ConvertVerticalVelocityDistributionData();
                    }
                    break;                
                case InputFormType.BoundaryTime:
                    if (AutoFinishConvertBoundaryTimeCell())
                    {
                        isSuccess = ConvertBoundaryTimeData();
                    }
                    break;                
            }

            if (!isSuccess)
            {
                //e.Cancel = true;
                MessageBox.Show("輸入資料格式錯誤！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            AutoFunction();
        }

        private void singleValueRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (chk)
            {
                if (o.type != RiverSimulationProfile.TwoInOne.Type.UseValue)
                {
                    o.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
                    CreateData(o);
                    InitializeDataGridView();
                }
            }
        }

        private void tableValueRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (chk)
            {
                if (o.type != RiverSimulationProfile.TwoInOne.Type.UseArray)
                {
                    o.type = RiverSimulationProfile.TwoInOne.Type.UseArray;
                    CreateData(o);
                    InitializeDataGridView();
                }
            }
        }

        private void selInputBtn_Click(object sender, EventArgs e)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            StructureSetForm form = new StructureSetForm();
            form.SetFormMode(selInputBtn.Text, -1, "", -1, "", -1, "", -1, "");

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                foreach (Point pt in form.selectedPl)
                {
                    dataGridView1[pt.Y, pt.X].Value = form.selectedValue;
                }
            }

        }

        private void settingBtn_Click(object sender, EventArgs e)
        {
            if (inputFormType != InputFormType.BoundaryTime)
            {
                return;
            }

            try
            {
                double step = Convert.ToDouble(timeGapTxt.Text);
                if(step <= 0)
                {
                    MessageBox.Show("無法設定時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                double v = step;
                for (int i = 1; i < rowCount - 1; ++i)
                {
                    if (v > RiverSimulationProfile.profile.totalSimulationTime)
                    {
                        dataGridView1[0, i].Value = RiverSimulationProfile.profile.totalSimulationTime.ToString();
                    }
                    else
                    {
                        dataGridView1[0, i].Value = v.ToString(SeparateFormCellFormat);
                    }
                    v += step;
                }
            }
            catch
            {
                MessageBox.Show("無法設定時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

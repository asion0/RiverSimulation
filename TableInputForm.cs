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
    public partial class TableInputForm : Form
    {
        public TableInputForm()
        {
            InitializeComponent();
        }

        bool hideSingle = false;        //隱藏 均一值 選項
        int colCount = 26;              //直行數目
        int rowCount = 50;              //橫列數目
        string title;                   //視窗標題

        public void SetFormMode(string title, bool onlyTable, int colCount, int rowCount)
        {
            hideSingle = onlyTable;
            this.colCount = colCount;
            this.rowCount = rowCount;
            this.title = title;
            CreateData(null);

        }

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
            InputFormType inputFormType = InputFormType.GenericDouble, int colWidth = 48, int rowHeadersWidth = 64,
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
            CreateData(initData);
        }

       private List<Point> bottomElevationPts = null;
        public void SetFormModeExtraData(object data1)
        {
             if(_inputFormType == InputFormType.BottomElevationForm)
             {
                 bottomElevationPts = new List<Point>((List<Point>)data1);
             }
           
        }

        private void InitializeDataGridView()
        {
            if (inputFormType == InputFormType.SeparateForm)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum, false, true);
            }
            else if (inputFormType == InputFormType.VerticalVelocityDistributionForm)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum, false, true);
                dataGridView.Columns[0].Name = "垂向分層位置";
                dataGridView.Columns[1].Name = "比例係數";  
            }
            else if (inputFormType == InputFormType.BoundaryTime)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum, false, false);
            }            
            else if (inputFormType == InputFormType.TwoInOneDouble ||
                    inputFormType == InputFormType.TwoInOneDoubleGreaterThanZero ||
                    inputFormType == InputFormType.TwoInOneDoubleGreaterThanOrEqualZero)
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                Debug.Assert(o != null);

                singleValueTxt.Text = o.ValueDouble()[0].ToString();
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum);

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
                        dataGridView.Enabled = false;
                        break;
                }
            }
            else if (inputFormType == InputFormType.GenericDouble ||
                    inputFormType == InputFormType.GenericDoubleGreaterThanZero ||
                    inputFormType == InputFormType.GenericDoubleGreaterThanOrEqualZero)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                      tableName, colName, rowName, nocolNum, noRowNum);
            }
            else if (inputFormType == InputFormType.FlowConditionsSettingConstant )
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                Debug.Assert(o != null);

                singleValueTxt.Text = o.ValueDouble()[0].ToString();
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                    "", colName, rowName, nocolNum, true);
                dataGridView.Rows[0].HeaderCell.Value = tableName;

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
                        dataGridView.Enabled = false;
                        break;
                }
            } 
            else if(inputFormType == InputFormType.FlowConditionsSettingVariable)
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
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
                        dataGridView.Enabled = false;
                        break;
                }
                singleValueTxt.Visible = (inputFormType == InputFormType.FlowConditionsSettingConstant) ? true :false;
            }
            else if (inputFormType == InputFormType.SeabedThicknessForm)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum, false, true);
                dataGridView.Rows[0].HeaderCell.Value = "作用層";
            }
            else if (inputFormType == InputFormType.SedimentCompositionRatioForm)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum, false, true);
                dataGridView.Rows[0].HeaderCell.Value = "作用層";
            }
            else
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum);
            }

            FillDataGridView();
         }

        private void TableInputForm_Load(object sender, EventArgs e)
        {
            if(hideSingle)
            {
                singleValueRdo.Visible = false;
                singleValueTxt.Visible = false;
                tableValueRdo.Visible = false;
                dataGridView.Height += dataGridView.Top;
                dataGridView.Top = 0;
                dataGridView.Enabled = true;
            }
         
            this.Text = title;
            InitializeDataGridView();

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (inputFormType == InputFormType.SeparateForm || inputFormType == InputFormType.VerticalVelocityDistributionForm)
            {
                averageBtn.Visible = true;
            }

            if(unitLbl.Text.Length == 0)
            {
                unitLbl.Visible = false;
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.PasteFromeExcel(dataGridView);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.CopyToClipboard(dataGridView);
        }

        private void valueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.FillSelectedValue(dataGridView);
        }

        public enum InputFormType
        {
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
        }

        private InputFormType _inputFormType = InputFormType.GenericDouble;
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
        private void CreateData(object d)
        {
            RiverSimulationProfile.TwoInOne o = d as RiverSimulationProfile.TwoInOne;
            RiverSimulationProfile.TwoInOne _d = null;
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
        }

        private void FillDataGridView()
        {
            switch (_inputFormType)
            {
                case InputFormType.GenericDouble:
                case InputFormType.GenericDoubleGreaterThanZero:
                case InputFormType.GenericDoubleGreaterThanOrEqualZero:
                    //編輯陣列I * J
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView[i, j].Value = (_data as double[,])[i, j].ToString();
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
                            dataGridView[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array2D()[i, j].ToString();
                        }
                    }
                    break;
                case InputFormType.FlowConditionsSettingConstant:
                    for (int i = 0; i < dataGridView.ColumnCount; ++i)
                    {
                        dataGridView[i, 0].Value = (_data as RiverSimulationProfile.TwoInOne).Array2D()[i, 0].ToString();
                    }
                    break;
                case InputFormType.FlowConditionsSettingVariable:
                    for (int i = 1; i < dataGridView.ColumnCount; ++i)
                    {
                        for (int j = 0; j < dataGridView.RowCount; ++j)
                        {
                            dataGridView[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array2D()[i - 1, j].ToString();
                        }
                    }

                    if (_inputFormType== InputFormType.FlowConditionsSettingVariable)
                    {   //變量流要填入邊界時間，且不可編輯
                        for (int i = 0; i < dataGridView.RowCount; ++i)
                        {
                            dataGridView[0, i].Value = p.boundaryTime[i].ToString();
                            dataGridView[0, i].Style.BackColor = Color.LightGray;
                            dataGridView[0, i].ReadOnly = true;
                        }
                    }
                    break;
                case InputFormType.SeabedThicknessForm: 
                //底床分層厚度輸入，直的一行，倒序
                    for (int i = 0; i < rowCount; ++i)
                    {
                        dataGridView[0, rowCount - i - 1].Value = (_data as double[])[i].ToString();
                    }
                    break;
                case InputFormType.SedimentCompositionRatioForm:    
                //泥砂組成比例輸入，每列最後一欄(橫列最右一格)要累加成100，不能編輯。
                //垂直倒序
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView[i, rowCount - j - 1].Value = (_data as double[,])[i, j].ToString();
                            if(i == colCount - 1)
                            {
                                dataGridView[i, j].ReadOnly = true;
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
                        dataGridView[0, rowCount - i - 1].Value = (_data as double[])[i].ToString();
                    }
                    dataGridView[0, 0].ReadOnly = true;
                    dataGridView[0, 0].Style.BackColor = Color.LightGray;
                    dataGridView[0, 0].Style.ForeColor = Color.Red;
                    dataGridView[0, rowCount - 1].ReadOnly = true;
                    dataGridView[0, rowCount - 1].Style.BackColor = Color.LightGray;
                    dataGridView[0, rowCount - 1].Style.ForeColor = Color.Red;
                    dataGridView[0, 0].Selected = false;
                    dataGridView[0, 1].Selected = true;
                    dataGridView[0, 0].Value = 1.ToString(SeparateFormCellFormat);
                    dataGridView[0, rowCount - 1].Value = 0.ToString(SeparateFormCellFormat);
                    break;
                case InputFormType.VerticalVelocityDistributionForm:
                    //垂直向隔網分層比例輸入，直的一行，倒序
                    //垂直遞增不能相同或是減少
                    for (int i = 0; i < rowCount; ++i)
                    {
                        dataGridView[0, rowCount - i - 1].Value = (_data as double[,])[0, i].ToString();
                        dataGridView[1, rowCount - i - 1].Value = (_data as double[,])[1, i].ToString();
                    }
                    dataGridView[0, 0].ReadOnly = true;
                    dataGridView[0, 0].Style.BackColor = Color.LightGray;
                    dataGridView[0, 0].Style.ForeColor = Color.Red;
                    dataGridView[0, rowCount - 1].ReadOnly = true;
                    dataGridView[0, rowCount - 1].Style.BackColor = Color.LightGray;
                    dataGridView[0, rowCount - 1].Style.ForeColor = Color.Red;
                    dataGridView[0, 0].Selected = false;
                    dataGridView[0, 1].Selected = true;
                    dataGridView[0, 0].Value = 1.ToString(SeparateFormCellFormat);
                    dataGridView[0, rowCount - 1].Value = 0.ToString(SeparateFormCellFormat);
                    break;
                case InputFormType.BoundaryTime:
                    //垂直向隔網分層比例輸入，直的一行，倒序
                    //垂直遞增不能相同或是減少
                    for (int i = 0; i < rowCount; ++i)
                    {
                        dataGridView[0, i].Value = (_data as double[])[i].ToString();
                    }
                    //第一欄不能編輯，必須為0
                    dataGridView[0, 0].ReadOnly = true;
                    dataGridView[0, 0].Style.BackColor = Color.LightGray;
                    dataGridView[0, 0].Style.ForeColor = Color.Red;
                    dataGridView[0, 0].Value = 0.ToString();
                    //最後一欄不能編輯，必須為 1.1.1 總模擬時間
                    dataGridView[0, rowCount - 1].ReadOnly = true;
                    dataGridView[0, rowCount - 1].Style.BackColor = Color.LightGray;
                    dataGridView[0, rowCount - 1].Style.ForeColor = Color.Red;
                    dataGridView[0, rowCount - 1].Value = RiverSimulationProfile.profile.totalSimulationTime.ToString();

                    dataGridView[0, 0].Selected = false;
                    dataGridView[0, 1].Selected = true;
                    break;                
                case InputFormType.BottomElevationForm:
                    //編輯底床高程，與輸入格網相同大小I * J
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            if (bottomElevationPts.Contains(new Point(j, i)))
                            {
                                dataGridView[i, j].Value = (_data as CoorPoint[,])[j, i].z.ToString();
                                dataGridView[i, j].ReadOnly = false;
                            }
                            else
                            {
                                dataGridView[i, j].ReadOnly = true;
                                dataGridView[i, j].Style.BackColor = Color.Gray;
                            }
                        }
                    }
                    break;            
            }
            dataGridView.PerformLayout();

        }

        private bool ConvertSeabedThicknessData()
        {
            try
            {
                DataGridView v = dataGridView;
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
                DataGridView v = dataGridView;
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
                DataGridView v = dataGridView;
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
                DataGridView v = dataGridView;
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
                DataGridView v = dataGridView;
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

        private bool ConvertBottomElevationData()
        {
            try
            {
                DataGridView v = dataGridView;
                for (int i = 0; i < colCount; ++i)
                {
                    for (int j = 0; j < rowCount; ++j)
                    {
                        (_data as CoorPoint[,])[j, i].z = Convert.ToDouble(v[i, j].Value);
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
                DataGridView v = dataGridView;
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

        private bool ConvertGenericDouble()
        {
            try
            {
                DataGridView v = dataGridView;
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
                DataGridView v = dataGridView;
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
                DataGridView v = dataGridView;
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
                    sum += Convert.ToDouble(dataGridView[i, index].Value);
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
                dataGridView[colCount - 1, j].Value = (100.0 - sum).ToString();
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
                    double v = Convert.ToDouble(dataGridView[0, i].Value);
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
                    double v = Convert.ToDouble(dataGridView[0, i].Value);
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

        private bool AutoFinishConvertBoundaryTimeCell()
        {
            bool allPass = true;
            try
            {
                double last = 0;
                double max = RiverSimulationProfile.profile.totalSimulationTime;

                for (int i = 1; i < rowCount - 1; ++i)
                {
                    double v = Convert.ToDouble(dataGridView[0, i].Value);
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
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AutoFunction();
        }

        private void averageBtn_Click(object sender, EventArgs e)
        {
            double step = 1.0 / (rowCount - 1);
            double v = 1.0 - step;
            for (int i = 1; i < rowCount - 1; ++i)
            {
                dataGridView[0, i].Value = v.ToString(SeparateFormCellFormat);
                v -= step;
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            switch (_inputFormType)
            {
                case InputFormType.GenericDouble:
                case InputFormType.GenericDoubleGreaterThanZero:
                case InputFormType.GenericDoubleGreaterThanOrEqualZero:
                    isSuccess = ConvertGenericDouble();
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
                    if (AutoFinishConvertVerticalVelocityDistributionCell())
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
                case InputFormType.BottomElevationForm:
                    isSuccess = ConvertBottomElevationData();
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

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            AutoFunction();
        }

        private void singleValueRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            if (!chk)
            {
                singleValueTxt.Enabled = false;
                return;
            }

            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (o != null)
            {
                o.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
            }

            if (inputFormType == InputFormType.FlowConditionsSettingVariable)
            {
                dataGridView.Enabled = true;
                dataGridView.Rows.Clear();

                DataGridViewUtility.InitializeDataGridView(dataGridView, 2, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, true, noRowNum);
                dataGridView.Columns[0].Name = "邊界時間";
                dataGridView.Columns[1].Name = colName;

                FillDataGridView();
            }
            else
            {
                singleValueTxt.Enabled = true;
                dataGridView.Enabled = false;
            } 
        }

        private void tableValueRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            if (!chk)
            {
                dataGridView.Enabled = false;
                return;
            }

            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (o != null)
            {
                o.type = RiverSimulationProfile.TwoInOne.Type.UseArray;
            }

            if (inputFormType == InputFormType.FlowConditionsSettingVariable)
            {
                dataGridView.Enabled = true;
                dataGridView.Rows.Clear();

                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount + 1, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum);
                dataGridView.Columns[0].Name = "邊界時間";
                for (int i = 1; i < dataGridView.ColumnCount; ++i )
                {
                    dataGridView.Columns[i].Name = colName + i.ToString();
                }
                FillDataGridView();
            }
            else
            {
                singleValueTxt.Enabled = false;
                dataGridView.Enabled = true;
            }
        }
    }
}

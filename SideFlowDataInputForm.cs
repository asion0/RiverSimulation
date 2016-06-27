using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
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
        }
        private InputFormType _inputFormType = InputFormType.Type1;
        public InputFormType inputFormType
        {
            get { return _inputFormType; }
            set { _inputFormType = value; }
        }

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
        }

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
                    tabPage1.Text = "主流方向流量(cms)";
                    tabPage2.Text = "側方向流量(cms)";

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
                    tabPage1.Text = "主流方向流量(cms)";
                    tabPage2.Text = "側方向流量(cms)";

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
                    tabPage1.Text = "主流方向流量(cms)";
                    tabPage2.Text = "側方向流量(cms)";

                    if (_d.ArrayNull())
                    {
                        (_data as RiverSimulationProfile.TwoInOne).CreateArray3D(colCount, rowCount, 2);
                    }
                }
            }
            //第5, 7種：超臨界流、均一值/逐點給、定量流
            else if (inputData.criticalFlowType == RiverSimulationProfile.CriticalFlowType.SuperCriticalFlow &&
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
                    colCount = 3;
                    rowCount = 1;
                    _inputFormType = InputFormType.Type5;
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
                    _inputFormType = InputFormType.Type7;
                    tabPage1.Parent = tabControl;
                    tabPage2.Parent = tabControl;
                    tabPage3.Parent = tabControl;
                    tabPage1.Text = "主流方向流量(cms)";
                    tabPage2.Text = "側方向流量(cms)";
                    tabPage3.Text = "水位(m)";

                    if (_d.ArrayNull())
                    {
                        (_data as RiverSimulationProfile.TwoInOne).CreateArray3D(colCount, rowCount, 3);
                    }
                }
            }
            //第6, 8種：超臨界流、均一值/逐點給、變量流
            else if (inputData.criticalFlowType == RiverSimulationProfile.CriticalFlowType.SuperCriticalFlow &&
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
                    _inputFormType = InputFormType.Type6;
                    tabPage1.Parent = tabControl;
                    tabPage2.Parent = tabControl;
                    tabPage3.Parent = tabControl;
                    tabPage1.Text = "主流方向流量(cms)";
                    tabPage2.Text = "側方向流量(cms)";
                    tabPage3.Text = "水位(m)";

                    if (_d.ValueNull())
                    {
                        (_data as RiverSimulationProfile.TwoInOne).CreateValue3D(colCount, rowCount, 3);
                    }
                }
                else
                {
                    colCount = inputData.sideFlowPoints.Count;
                    rowCount = p.boundaryTime.Length;
                    _inputFormType = InputFormType.Type8;
                    tabPage1.Parent = tabControl;
                    tabPage2.Parent = tabControl;
                    tabPage3.Parent = tabControl;
                    tabPage1.Text = "主流方向流量(cms)";
                    tabPage2.Text = "側方向流量(cms)";
                    tabPage3.Text = "水位(m)";

                    if (_d.ArrayNull())
                    {
                        (_data as RiverSimulationProfile.TwoInOne).CreateArray3D(colCount, rowCount, 3);
                    }
                }
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

                for (int i = 0; i < colCount; ++i)
                {
                    int start = inputData.sideFlowPoints.OrderBy(p => p.X).ToArray()[0].X;
                    dataGridView1.Columns[i].Name = (i + start + 1).ToString();
                    dataGridView2.Columns[i].Name = (i + start + 1).ToString();
                }
            }
            else if (inputFormType == InputFormType.Type5)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, "", "", true, true, false, false);
                dataGridView1.Columns[0].Name = "主流方向流量(cms)";
                dataGridView1.Columns[1].Name = "側方向流量(cms)";
                dataGridView1.Columns[2].Name = "水位(m)";
            }
            else if (inputFormType == InputFormType.Type7)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, 72, 140,
                    "", "", "主流方向流量(cms)", false, true, false, false);
                DataGridViewUtility.InitializeDataGridView(dataGridView2, colCount, rowCount, 72, 140,
                    "", "", "側方向流量(cms)", false, true, false, false);
                DataGridViewUtility.InitializeDataGridView(dataGridView3, colCount, rowCount, 72, 140,
                    "", "", "水位(m)", false, true, false, false);
                for (int i = 0; i < colCount; ++i)
                {
                    int start = inputData.sideFlowPoints.OrderBy(p => p.X).ToArray()[0].X;
                    dataGridView1.Columns[i].Name = (i + start + 1).ToString();
                    dataGridView2.Columns[i].Name = (i + start + 1).ToString();
                    dataGridView3.Columns[i].Name = (i + start + 1).ToString();
                }
            }
            else if (inputFormType == InputFormType.Type6)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, colWidth, rowHeadersWidth,
                    "時間 T (sec)", "主流方向流量(cms)", "", true, true, false, false);
                DataGridViewUtility.InitializeDataGridView(dataGridView2, colCount, rowCount, colWidth, rowHeadersWidth,
                    "時間 T (sec)", "側方向流量(cms)", "", true, true, false, false);
                DataGridViewUtility.InitializeDataGridView(dataGridView3, colCount, rowCount, colWidth, rowHeadersWidth,
                    "時間 T (sec)", "水位(m)", "", true, true, false, false);
                for (int i = 0; i < p.boundaryTime.Length; ++i)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
                    dataGridView2.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
                    dataGridView3.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
                }
                dataGridView1.Columns[0].Name = "主流方向流量(cms)";
                dataGridView2.Columns[0].Name = "側方向流量(cms)";
                dataGridView3.Columns[0].Name = "水位(m)";
            }
            else if (inputFormType == InputFormType.Type8)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView1, colCount, rowCount, 72, 140,
                    "時間 T (sec)", "", "", false, false, false, false);
                DataGridViewUtility.InitializeDataGridView(dataGridView2, colCount, rowCount, 72, 140,
                    "時間 T (sec)", "", "", false, false, false, false);
                DataGridViewUtility.InitializeDataGridView(dataGridView3, colCount, rowCount, 72, 140,
                    "時間 T (sec)", "", "", false, false, false, false);
                for (int i = 0; i < p.boundaryTime.Length; ++i)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
                    dataGridView2.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
                    dataGridView3.Rows[i].HeaderCell.Value = p.boundaryTime[i].ToString();
                }

                for (int i = 0; i < colCount; ++i)
                {
                    int start = inputData.sideFlowPoints.OrderBy(p => p.X).ToArray()[0].X;
                    dataGridView1.Columns[i].Name = (i + start + 1).ToString();
                    dataGridView2.Columns[i].Name = (i + start + 1).ToString();
                    dataGridView3.Columns[i].Name = (i + start + 1).ToString();
                }
            }

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            FillDataGridView();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                DataGridViewUtility.PasteFromeExcel(dataGridView1);
            }
            else if (tabControl.SelectedIndex == 1)
            {
                DataGridViewUtility.PasteFromeExcel(dataGridView2);
            }
            else if (tabControl.SelectedIndex == 2)
            {
                DataGridViewUtility.PasteFromeExcel(dataGridView3);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                DataGridViewUtility.CopyToClipboard(dataGridView1);
            }
            else if (tabControl.SelectedIndex == 1)
            {
                DataGridViewUtility.CopyToClipboard(dataGridView2);
            }
            else if (tabControl.SelectedIndex == 2)
            {
                DataGridViewUtility.CopyToClipboard(dataGridView3);
            }
        }

        private void valueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                DataGridViewUtility.FillSelectedValue(dataGridView1);
            }
            else if (tabControl.SelectedIndex == 1)
            {
                DataGridViewUtility.FillSelectedValue(dataGridView2);
            }
            else if (tabControl.SelectedIndex == 2)
            {
                DataGridViewUtility.FillSelectedValue(dataGridView3);
            }
        }

        private object _data = null;
        public object data
        {
            get { return _data; }
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
                case InputFormType.Type5:
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView1[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Value2D()[i, j].ToString();
                        }
                    }
                    break;
                case InputFormType.Type7:
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView1[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 0].ToString();
                            dataGridView2[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 1].ToString();
                            dataGridView3[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 2].ToString();
                        }
                    }
                    break;
                case InputFormType.Type6:
                    for (int j = 0; j < rowCount; ++j)
                    {
                        dataGridView1[0, j].Value = (_data as RiverSimulationProfile.TwoInOne).Value3D()[0, j, 0].ToString();
                        dataGridView2[0, j].Value = (_data as RiverSimulationProfile.TwoInOne).Value3D()[0, j, 1].ToString();
                        dataGridView3[0, j].Value = (_data as RiverSimulationProfile.TwoInOne).Value3D()[0, j, 2].ToString();
                    }
                    break;
                case InputFormType.Type8:
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView1[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 0].ToString();
                            dataGridView2[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 1].ToString();
                            dataGridView3[i, j].Value = (_data as RiverSimulationProfile.TwoInOne).Array3D()[i, j, 2].ToString();
                        }
                    }
                    break;
            }
            //dataGridView1.PerformLayout();

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
                for (int i = 0; i < colCount; ++i)
                {
                    for (int j = 0; j < rowCount; ++j)
                    {
                        o.Value3D()[i, j, 0] = Convert.ToDouble(dataGridView1[i, j].Value);
                        o.Value3D()[i, j, 1] = Convert.ToDouble(dataGridView2[i, j].Value);
                    }
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
                for (int i = 0; i < colCount; ++i)
                {
                    for (int j = 0; j < rowCount; ++j)
                    {
                        o.Array3D()[i, j, 0] = Convert.ToDouble(dataGridView1[i, j].Value);
                        o.Array3D()[i, j, 1] = Convert.ToDouble(dataGridView2[i, j].Value);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertType5()
        {
            try
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                o.Value2D()[0, 0] = Convert.ToDouble(dataGridView1[0, 0].Value);
                o.Value2D()[1, 0] = Convert.ToDouble(dataGridView1[1, 0].Value);
                o.Value2D()[2, 0] = Convert.ToDouble(dataGridView1[2, 0].Value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertType7()
        {
            try
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                for (int i = 0; i < colCount; ++i)
                {
                    o.Array3D()[i, 0, 0] = Convert.ToDouble(dataGridView1[i, 0].Value);
                    o.Array3D()[i, 0, 1] = Convert.ToDouble(dataGridView2[i, 0].Value);
                    o.Array3D()[i, 0, 2] = Convert.ToDouble(dataGridView3[i, 0].Value);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertType6()
        {
            try
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                for (int i = 0; i < colCount; ++i)
                {
                    for (int j = 0; j < rowCount; ++j)
                    {
                        o.Value3D()[i, j, 0] = Convert.ToDouble(dataGridView1[i, j].Value);
                        o.Value3D()[i, j, 1] = Convert.ToDouble(dataGridView2[i, j].Value);
                        o.Value3D()[i, j, 2] = Convert.ToDouble(dataGridView3[i, j].Value);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ConvertType8()
        {
            try
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                for (int i = 0; i < colCount; ++i)
                {
                    for (int j = 0; j < rowCount; ++j)
                    {
                        o.Array3D()[i, j, 0] = Convert.ToDouble(dataGridView1[i, j].Value);
                        o.Array3D()[i, j, 1] = Convert.ToDouble(dataGridView2[i, j].Value);
                        o.Array3D()[i, j, 2] = Convert.ToDouble(dataGridView3[i, j].Value);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }



        private void AutoFunction()
        {
            //switch (_inputFormType)
            //{
            //    case InputFormType.GenericDouble:
            //    case InputFormType.GenericDoubleGreaterThanZero:
            //    case InputFormType.GenericDoubleGreaterThanOrEqualZero:
            //        //_data = null;
            //        break;
            //    case InputFormType.SeabedThicknessForm:
            //        break;
            //    case InputFormType.SedimentCompositionRatioForm:
            //        AutoFinishConvertSedimentCompositionRatioCell();
            //        break;
            //    case InputFormType.SeparateForm:
            //        AutoFinishConvertSeparateCell();
            //        break;
            //    case InputFormType.VerticalVelocityDistributionForm:
            //        AutoFinishConvertVerticalVelocityDistributionCell();
            //        break;
            //    case InputFormType.BoundaryTime:
            //        AutoFinishConvertBoundaryTimeCell();
            //        break;
            //    case InputFormType.BottomElevationForm:
            //        break;
            //    case InputFormType.BottomBedParticleSizeRatio:
            //        AutoFinishConvertBottomBedParticleSizeRatioCell();
            //        break;

            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AutoFunction();
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
                case InputFormType.Type5:
                    isSuccess = ConvertType5();
                    break;
                case InputFormType.Type7:
                    isSuccess = ConvertType7();
                    break;
                case InputFormType.Type6:
                    isSuccess = ConvertType6();
                    break;
                case InputFormType.Type8:
                    isSuccess = ConvertType8();
                    break;
            }

            if (!isSuccess)
            {
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

        //private void selInputBtn_Click(object sender, EventArgs e)
        //{
        //    RiverSimulationProfile p = RiverSimulationProfile.profile;
        //    StructureSetForm form = new StructureSetForm();
        //    form.SetFormMode(selInputBtn.Text, -1, "", -1, "", -1, "", -1, "");

        //    DialogResult r = form.ShowDialog();
        //    if (DialogResult.OK == r)
        //    {
        //        foreach (Point pt in form.selectedPl)
        //        {
        //            dataGridView1[pt.Y, pt.X].Value = form.selectedValue;
        //        }
        //    }

        //}

    }
}

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

namespace RiverSimulationApplication
{
    public partial class TableInputForm : Form
    {
        public TableInputForm()
        {
            InitializeComponent();
        }

        public TableInputForm(Type t)
        {
            InitializeComponent();
            type = t;
        }

        Type type = Type.General;
        bool hideSingle = false;
        int colCount = 26;
        int rowCount = 50;
        string title;
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
        public void SetFormMode(string title, int colCount, int rowCount, string tableName = "", string colName = "", string rowName = "", 
            InputFormType inputFormType = InputFormType.Generic, int colWidth = 48, int rowHeadersWidth = 64,
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
            else if(inputFormType == InputFormType.Generic)
            {
                DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                    tableName, colName, rowName, nocolNum, noRowNum);
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

        public enum Type
        {
            General,
            UpVerticalDistribution,
        }

        private void TableInputForm_Load(object sender, EventArgs e)
        {
            if(hideSingle)
            {
                singleValueRdo.Visible = false;
                singleValueText.Visible = false;
                tableValueRdo.Visible = false;
                dataGridView.Height += dataGridView.Top;
                dataGridView.Top = 0;
                dataGridView.Enabled = true;
            }
            else
            {
                singleValueRdo.Checked = true;
            }            
            this.Text = title;
            InitializeDataGridView();

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if(inputFormType == InputFormType.SeparateForm)
            {
                averageBtn.Visible = true;

            }
        }

        private void singleValue_CheckedChanged(object sender, EventArgs e)
        {
            singleValueText.Enabled = true;
            dataGridView.Enabled = false;
        }

        private void tableValue_CheckedChanged(object sender, EventArgs e)
        {
            singleValueText.Enabled = false;
            dataGridView.Enabled = true;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataGridViewUtility.PasteFromeExcel(dataGridView);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.CopyToClipboard(dataGridView);
        }

        public enum InputFormType
        {
            Generic,                //初始一般用途
            SeabedThicknessForm,    //底床分層厚度輸入
            SedimentCompositionRatioForm,   //泥砂組成比例輸入
            SeparateForm,       //垂直向隔網分層比例輸入
            BottomElevationForm,    //編輯底床高程
        }

        private InputFormType _inputFormType = InputFormType.Generic;
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

        public double[,] SedimentCompositionRatioData()
        {
            if (_data == null)
                return null;

            return _data as double[,];
        }

        private void CreateData(object d)
        {
            switch(_inputFormType)
            {
                case InputFormType.Generic:
                    _data = new double[colCount, rowCount];
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
                    if (d == null)
                    {
                        _data = new double[rowCount];
                    }
                    else
                    {
                        _data = (double [])(d as double[]).Clone();
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
            }
        }

        private void FillDataGridView()
        {
            switch (_inputFormType)
            {
                case InputFormType.Generic:
                    break;
                case InputFormType.SeabedThicknessForm:
                    for (int i = 0; i < rowCount; ++i)
                    {
                        dataGridView[0, rowCount - i - 1].Value = (_data as double[])[i].ToString();
                    }
                    break;
                case InputFormType.SedimentCompositionRatioForm:
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
                    dataGridView[0, 0].Value = 1.ToString("F6");
                    dataGridView[0, rowCount - 1].Value = 0.ToString("F6");
                    break;
                case InputFormType.BottomElevationForm:
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

        private void TableInputForm_FormClosed(object sender, FormClosedEventArgs e)
        {

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

        private void AutoFunction()
        {
            switch (_inputFormType)
            {
                case InputFormType.Generic:
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
                case InputFormType.BottomElevationForm:
                    break;
            }
        }
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AutoFunction();
        }

        private void TableInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void averageBtn_Click(object sender, EventArgs e)
        {
            double step = 1.0 / (rowCount - 1);
            double v = 1.0 - step;
            for (int i = 1; i < rowCount - 1; ++i)
            {
                dataGridView[0, i].Value = v.ToString("F6");
                v -= step;
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            switch (_inputFormType)
            {
                case InputFormType.Generic:
                    isSuccess = true;
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
    }
}

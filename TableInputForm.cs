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
        }
        string tableName;
        string colName;
        string rowName;
        bool nocolNum = true;
        bool noRowNum = true;
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

        private void InitializeDataGridView()
        {
            DataGridViewUtility.InitializeDataGridView(dataGridView, colCount, rowCount, colWidth, rowHeadersWidth,
                tableName, colName, rowName, nocolNum, noRowNum);

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
            this.Text = title;
            InitializeDataGridView();
            if(!hideSingle)
            {
                singleValueRdo.Checked = true;
            }
            //RealtimeExampleForm forma = new RealtimeExampleForm();
            //forma.Show();
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
                    _data = null;
                    break;
                case InputFormType.SeabedThicknessForm:
                    if (d == null)
                    {
                        _data = new double[colCount];
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
            }
        }

        private void FillDataGridView()
        {
            switch (_inputFormType)
            {
                case InputFormType.Generic:
                    break;
                case InputFormType.SeabedThicknessForm:
                    for (int i = 0; i < colCount; ++i)
                    {
                        dataGridView[i, 0].Value = (_data as double[])[i].ToString();
                    }
                    break;
                case InputFormType.SedimentCompositionRatioForm:
                    for (int i = 0; i < colCount; ++i)
                    {
                        for (int j = 0; j < rowCount; ++j)
                        {
                            dataGridView[i,j].Value = (_data as double[,])[i, j].ToString();
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
                        dataGridView[0, i].Value = (_data as double[])[i].ToString();
                    }
                    dataGridView[0, 0].ReadOnly = true;
                    dataGridView[0, rowCount - 1].ReadOnly = true;
                    AutoFinishConvertSeparateCell();
                    break;  
            }
        }

        private bool ConvertSeabedThicknessData()
        {
            try
            {
                DataGridView v = dataGridView;
                for (int i = 0; i < colCount; ++i)
                {
                    (_data as double[])[i] = Convert.ToDouble(v[i, 0].Value);
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
            double last = 0.0;
            bool allPass = true;
            dataGridView[0, 0].Value = "0";
            for (int i = 1; i < rowCount - 1; ++i)
            {
                try
                {
                    double v = Convert.ToDouble(dataGridView[0, i].Value);
                    if (last > v || v > 1.0)
                    {
                        return false;
                    }
                    last = Convert.ToDouble(dataGridView[0, i].Value);
                }
                catch
                {
                    return false;
                }
            }
            dataGridView[0, rowCount - 1].Value = "1";
            return allPass;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
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
            }
        }

        private void TableInputForm_FormClosing(object sender, FormClosingEventArgs e)
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
                    if(AutoFinishConvertSeparateCell())
                    {
                        isSuccess = ConvertSeparateData();
                    }
                    break;
            }

            if (!isSuccess)
            {
                //e.Cancel = true;
                MessageBox.Show("輸入資料格式錯誤，將不被採用！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}

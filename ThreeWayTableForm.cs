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
    public partial class ThreeWayTableForm : Form
    {
        public ThreeWayTableForm()
        {
            InitializeComponent();
        }

        private RiverSimulationProfile p = null;
        public enum FormType
        {
            FlowQuantity,
        };

        public void SetFormMode(FormType t, string title, string colName, int colCount, int rowCount,  RiverSimulationProfile profile, object initData = null)
        {
            this.formType = t;
            this.p = profile;

            this.title = title;
            this.colCount = colCount;
            this.iTitle = colName;
            //this.rowCount = rowCount;

            if (formType == FormType.FlowQuantity)
            {
                if (p.IsVariableFlowType())
                {
                    this.iStart = 1;
                    this.jStart = 2;
                    this.rowCount = rowCount;
                    this.extraCol = 2;
                }
                else
                {
                    this.iStart = 0;
                    this.jStart = 2;
                    this.rowCount = 1;
                    this.extraCol = 1;
                }
            } 
            CreateData(initData);
        }

        public object GetData()
        {
            return _data;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            switch (formType)
            {
                case FormType.FlowQuantity:
                    isSuccess = ConvertFlowQuantityData();
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

        private string title;
        private int colCount = 0;
        private int rowCount = 0;
        private object _data = null;

        private void ThreeWayTableForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            InitializeDataGridView();

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private bool ConvertFlowQuantityData()
        {
            try
            {
                DataGridView v = dataGridView;
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        for (int jw = jStart; jw < jStart + rowCount; ++jw)
                        {
                            o.dataArray[0, jw - jStart] = Convert.ToDouble(v[iStart, jw].Value);
                        }
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        for (int jw = jStart; jw < jStart + rowCount; ++jw)
                        {
                            for (int iw = iStart; iw < iStart + colCount; ++iw)
                            {
                                o.dataArray[iw - iStart, jw - jStart] = Convert.ToDouble(v[iw, jw].Value);
                            }
                        }
                        break;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void CreateData(object d)
        {
            RiverSimulationProfile.TwoInOne o = d as RiverSimulationProfile.TwoInOne;
            if (o == null || o.dataArray == null)
            {   //rowCount : Q1 ~ Q5, colCount : J1 ~ J15
                _data = new RiverSimulationProfile.TwoInOne(colCount, rowCount);
            }
            else
            {
                _data = new RiverSimulationProfile.TwoInOne(d as RiverSimulationProfile.TwoInOne);
            }
        }


        private FormType formType = FormType.FlowQuantity;
        private int iStart = 1;
        private int jStart = 2;
        private string iTitle;
        private string timeTitle = "邊界時間";
        private string buttonText;
        private int extraCol = 2;
        private int extraRow = 4;

        private void InitializeDataGridView()
        {
            dataGridView.Rows.Clear();

            DataGridViewUtility.InitializeDataGridView(dataGridView, colCount + extraCol, rowCount + extraRow, 120, 60,
                "", "", "", false, false);

            dataGridView[0, 0].Value = p.IsVariableFlowType() ? "變量流" : "定量流";
            for (int jw = 0; jw < rowCount + extraRow; ++jw)
            {
                for (int iw = 0; iw < colCount + extraCol; ++iw)
                {
                    dataGridView[iw, jw].ReadOnly = true;
                }
            }
            InitTable();

            FillDataGridView();
        }

        private void InitTable()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (o == null)
            {
                return;
            }

            if (p.IsVariableFlowType())
            {   //變量流需顯示邊界時間欄位
                dataGridView[0, 1].Value = timeTitle;
                dataGridView[0, 1].Style.BackColor = Color.LightGray;
                for (int jw = jStart; jw < jStart + p.boundaryTimeNumber; ++jw)
                {
                    dataGridView[0, jw].Value = p.boundaryTime[jw - jStart].ToString();
                    dataGridView[0, jw].Style.BackColor = Color.LightGray;
                }
            }

            switch (o.type)
            {
                case RiverSimulationProfile.TwoInOne.Type.None:
                case RiverSimulationProfile.TwoInOne.Type.UseValue:
                    buttonText = "進階 - 非均勻入流";
                    for (int iw = iStart; iw < iStart + 1; ++iw)
                    {
                        dataGridView[iw, 1].Value = iTitle;
                        dataGridView[iw, 1].Style.BackColor = Color.LightGray;
                    }
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    buttonText = "均勻入流";
                    for (int iw = iStart; iw < iStart + colCount; ++iw)
                    {
                        dataGridView[iw, 1].Value = iTitle + (iw - iStart + 1).ToString();
                        dataGridView[iw, 1].Style.BackColor = Color.LightGray;
                    }
                    break;
            }

            DataGridViewButtonCell b = new DataGridViewButtonCell();
            b.Value = buttonText;
            b.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView[0, jStart + rowCount + 1] = b;
        }

        private void FillDataGridView()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            switch (o.type)
            {
                case RiverSimulationProfile.TwoInOne.Type.None:
                case RiverSimulationProfile.TwoInOne.Type.UseValue:
                    for (int jw = jStart; jw < jStart + rowCount; ++jw)
                    {
                        dataGridView[iStart, jw].Value = o.dataArray[0, jw - jStart].ToString();
                        dataGridView[iStart, jw].ReadOnly = false;
                        dataGridView[iStart, jw].Style.BackColor = Color.LemonChiffon;
                    }
                    dataGridView.CurrentCell = dataGridView[iStart, jStart];
                    //dataGridView[1, jStart]. = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    for (int jw = jStart; jw < jStart + rowCount; ++jw)
                    {
                        for (int iw = iStart; iw < iStart + colCount; ++iw)
                        {
                            dataGridView[iw, jw].Value = o.dataArray[iw - iStart, jw - jStart].ToString();
                            dataGridView[iw, jw].ReadOnly = false;
                            dataGridView[iw, jw].Style.BackColor = Color.LemonChiffon;
                        }
                    }
                    dataGridView.CurrentCell = dataGridView[iStart, jStart];
                    break;
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == jStart + rowCount + 1)
            {
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        o.type = RiverSimulationProfile.TwoInOne.Type.UseArray;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        o.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
                        break;
                }
                InitializeDataGridView();
            }
        }

    }
}

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

        public RiverSimulationProfile p = null;

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

        public void SetFormMode(string title, int colCount, int rowCount, object initData = null/*, string tableName = "", string colName = "", string rowName = "",
    InputFormType inputFormType = InputFormType.GenericDouble, int colWidth = 48, int rowHeadersWidth = 64,
   bool onlyTable = true, bool nocolNum = false, bool noRowNum = false, object initData = null*/)
        {
            //hideSingle = onlyTable;
            this.title = title;
            this.colCount = colCount;
            this.rowCount = rowCount;
            //this.tableName = tableName;
            //this.colName = colName;
            //this.rowName = rowName;
            //this.nocolNum = nocolNum;
            //this.noRowNum = noRowNum;
            //this.colWidth = colWidth;
            //this.rowHeadersWidth = rowHeadersWidth;
            //this.inputFormType = inputFormType;
            CreateData(initData);
        }

        private void ok_Click(object sender, EventArgs e)
        {

        }
        private void CreateData(object d)
        {
            RiverSimulationProfile.TwoInOne o = d as RiverSimulationProfile.TwoInOne;

            //if (o == null || o.dataArray == null || o.dataArray.GetLength(0) != colCount + 1 || o.dataArray.GetLength(1) != rowCount)
            if (o == null || o.dataArray == null)
            {   //rowCount : Q1 ~ Q5, colCount : J1 ~ J15
                _data = new RiverSimulationProfile.TwoInOne(colCount, rowCount);
            }
            else
            {
                _data = new RiverSimulationProfile.TwoInOne(d as RiverSimulationProfile.TwoInOne);
            }
        }

        private void InitializeDataGridView()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (o != null)
            {
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        //singleValueRdo.Checked = true;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        //tableValueRdo.Checked = true;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.None:
                        //singleValueRdo.Checked = false;
                        //tableValueRdo.Checked = false;
                        break;
                }
            }
            //singleValueTxt.Visible = (inputFormType == InputFormType.FlowConditionsSettingConstant) ? true : false;
            DataGridViewUtility.InitializeDataGridView(dataGridView, colCount + 2, rowCount + 4, 120, 60,
                "", "", "", false, false);

            dataGridView[0, 0].Value = p.IsVariableFlowType() ? "變量流" : "定量流";

            int jStart = 2;
            dataGridView[0, 1].Value = "邊界時間";
            dataGridView[0, 1].Style.BackColor = Color.LightGray;
            for (int jw = jStart; jw < jStart + p.boundaryTimeNumber; ++jw)
            {
                dataGridView[0, jw].Value = p.boundaryTime[jw - jStart].ToString();
                dataGridView[0, jw].Style.BackColor = Color.LightGray;
            }

            int iStart = 1;
            if (o.type == RiverSimulationProfile.TwoInOne.Type.None || o.type == RiverSimulationProfile.TwoInOne.Type.UseValue)
            {
                for (int iw = iStart; iw < iStart + 1; ++iw)
                {
                    dataGridView[iw, jStart].Value = "流量Q";
                    dataGridView[iw, jStart].Style.BackColor = Color.LightGray;
                }
            }
            else
            {
                for (int iw = iStart; iw < iStart + rowCount; ++iw)
                {
                    dataGridView[iw, 1].Value = "流量Q" + (iw - iStart + 1).ToString();
                    dataGridView[iw, 1].Style.BackColor = Color.LightGray;
                }
            }

            DataGridViewButtonCell b = new DataGridViewButtonCell();
            b.Value = "進階 - 非均勻入流";
            dataGridView[0, jStart + p.boundaryTimeNumber + 1] = b;
            //FillDataGridView();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0 && e.RowIndex == 13)
            {
                //button click
            }
        }
    }
}

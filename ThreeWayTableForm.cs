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
            BottomBedLoadFlux,
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
            else if (formType == FormType.BottomBedLoadFlux)
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
                this.colCount = (p.sedimentParticlesNumber > p.inputGrid.GetJ) ? p.sedimentParticlesNumber : p.inputGrid.GetJ;
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
                case FormType.BottomBedLoadFlux:
                    isSuccess = ConvertBottomBedLoadFluxData();
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
        private int tabIndex = 0;
        private Color colHeaderColor = Color.CornflowerBlue;        //橫向(上方)標題顏色
        private Color rowHeaderColor = Color.LightGray;            //直向(左方)標題顏色
        private Color tabActiveColor = Color.CornflowerBlue;         //被選取標籤顏色
        private Color tabItemColor = Color.DarkSlateBlue;            //未選取標籤顏色


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
                            o.dataArray2D()[0, jw - jStart] = Convert.ToDouble(v[iStart, jw].Value);
                        }
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        for (int jw = jStart; jw < jStart + rowCount; ++jw)
                        {
                            for (int iw = iStart; iw < iStart + colCount; ++iw)
                            {
                                o.dataArray2D()[iw - iStart, jw - jStart] = Convert.ToDouble(v[iw, jw].Value);
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
        private bool ConvertBottomBedLoadFluxData()
        {
            try
            {
                DataGridView v = dataGridView;
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        for (int tw = jStart; tw < jStart + p.boundaryTimeNumber; ++tw)
                        {
                            for (int kw = iStart; kw < iStart + p.sedimentParticlesNumber; ++kw)
                            {
                                o.dataArray3D()[kw - iStart, 0, tw - jStart] = Convert.ToDouble(v[kw, tw].Value);
                             }
                        }
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        for (int tw = jStart; tw < jStart + p.boundaryTimeNumber; ++tw)
                        {
                            for (int jw = iStart; jw < iStart + p.inputGrid.GetJ; ++jw)
                            {
                                o.dataArray3D()[tabIndex, jw - iStart, tw - jStart] = Convert.ToDouble(v[jw, tw].Value);
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
            if (formType == FormType.FlowQuantity)
            {
                if (o == null || o.dataArray == null)
                {   //rowCount : Q1 ~ Q5, colCount : J1 ~ J15
                    _data = new RiverSimulationProfile.TwoInOne(colCount, rowCount);
                }
                else
                {
                    _data = new RiverSimulationProfile.TwoInOne(d as RiverSimulationProfile.TwoInOne);
                }
            }
             else if (formType == FormType.BottomBedLoadFlux)
            {
                if (o == null)
                {   //rowCount : Q1 ~ Q5, colCount : J1 ~ J15
                   // _data = new RiverSimulationProfile.TwoInOne(p.boundaryTimeNumber, p.sedimentParticlesNumber, p.inputGrid.GetJ); //[T, K, J]
                    _data = new RiverSimulationProfile.TwoInOne(p.sedimentParticlesNumber, p.inputGrid.GetJ, p.boundaryTimeNumber); //[K, J, T]
                }
                else
                {
                    _data = new RiverSimulationProfile.TwoInOne(d as RiverSimulationProfile.TwoInOne);
                }
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
            {   //全畫面設為唯讀
                for (int iw = 0; iw < colCount + extraCol; ++iw)
                {
                    dataGridView[iw, jw].ReadOnly = true;
                }
            }
            if (formType == FormType.FlowQuantity)
            {
                InitTableFlowQuantity();
            }
            else if (formType == FormType.BottomBedLoadFlux)
            {
                InitTableBottomBedLoadFlux();
            }

            FillDataGridView();
        }

        private void InitTableFlowQuantity()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (o == null)
            {
                return;
            }

            if (p.IsVariableFlowType())
            {   //變量流需顯示邊界時間欄位
                dataGridView[0, 1].Value = timeTitle;
                dataGridView[0, 1].Style.BackColor = rowHeaderColor;
                for (int jw = jStart; jw < jStart + p.boundaryTimeNumber; ++jw)
                {
                    dataGridView[0, jw].Value = p.boundaryTime[jw - jStart].ToString();
                    dataGridView[0, jw].Style.BackColor = rowHeaderColor;
                }
            }
            switch (o.type)
            {
                case RiverSimulationProfile.TwoInOne.Type.None:
                case RiverSimulationProfile.TwoInOne.Type.UseValue:
                    buttonText = "進階 - 非均勻入流";
                    for (int iw = iStart; iw < iStart + 1; ++iw)
                    {   //填入橫排標題, 只有一欄
                        dataGridView[iw, 1].Value = iTitle;
                        dataGridView[iw, 1].Style.BackColor = colHeaderColor;
                    }
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    buttonText = "均勻入流";
                    for (int iw = iStart; iw < iStart + colCount; ++iw)
                    {   //填入橫排標題
                        dataGridView[iw, 1].Value = iTitle + (iw - iStart + 1).ToString();
                        dataGridView[iw, 1].Style.BackColor = colHeaderColor;
                    }
                    break;
            }

            DataGridViewButtonCell b = new DataGridViewButtonCell();
            b.Value = buttonText;
            b.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView[0, jStart + rowCount + 1] = b;
        }

        private void InitTableBottomBedLoadFlux()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (o == null)
            {
                return;
            }

            if (p.IsVariableFlowType())
            {   //變量流需顯示邊界時間欄位
                dataGridView[0, 1].Value = timeTitle;
                dataGridView[0, 1].Style.BackColor = rowHeaderColor;
                for (int jw = jStart; jw < jStart + p.boundaryTimeNumber; ++jw)
                {
                    dataGridView[0, jw].Value = p.boundaryTime[jw - jStart].ToString();
                    dataGridView[0, jw].Style.BackColor = rowHeaderColor;
                }
            }

            DataGridViewButtonCell b = null;
            switch (o.type)
            {
                case RiverSimulationProfile.TwoInOne.Type.None:
                case RiverSimulationProfile.TwoInOne.Type.UseValue:
                    buttonText = "進階 - 逐點輸入";
                    for (int iw = iStart; iw < iStart + p.sedimentParticlesNumber; ++iw)
                    {   //填入橫排標題，泥砂數目(K)
                        dataGridView[iw, 1].Value = iTitle + (iw - iStart + 1).ToString();
                        dataGridView[iw, 1].Style.BackColor = colHeaderColor;
                    }
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    buttonText = "均一值";
                    for (int iw = iStart; iw < iStart + p.inputGrid.GetJ; ++iw)
                    {   //填入橫排標題，J個
                        dataGridView[iw, 1].Value = (iw - iStart + 1).ToString();
                        dataGridView[iw, 1].Style.BackColor = colHeaderColor;
                    }
                    for (int iw = iStart; iw < iStart + p.sedimentParticlesNumber; ++iw)
                    {   //填入標籤標題，K個
                        dataGridView[iw, 0].Value = iTitle + (iw - iStart + 1).ToString();
                        dataGridView[iw, 0].Style.BackColor = (tabIndex == (iw - iStart)) ? tabActiveColor : tabItemColor;
                    }
                    b = new DataGridViewButtonCell();
                    b.Value = "套用到全部";
                    b.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView[1, jStart + rowCount + 1] = b;
                    break;
            }
            b = new DataGridViewButtonCell();
            b.Value = buttonText;
            b.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView[0, jStart + rowCount + 1] = b;
            
        }

        private void FillDataGridView()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (formType == FormType.FlowQuantity)
            {
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        for (int jw = jStart; jw < jStart + rowCount; ++jw)
                        {
                            dataGridView[iStart, jw].Value = o.dataArray2D()[0, jw - jStart].ToString();
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
                                dataGridView[iw, jw].Value = o.dataArray2D()[iw - iStart, jw - jStart].ToString();
                                dataGridView[iw, jw].ReadOnly = false;
                                dataGridView[iw, jw].Style.BackColor = Color.LemonChiffon;
                            }
                        }
                        dataGridView.CurrentCell = dataGridView[iStart, jStart];
                        break;
                }
            }
            else if(formType == FormType.BottomBedLoadFlux)
            {
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        for (int tw = jStart; tw < jStart + p.boundaryTimeNumber; ++tw)
                        {
                            for (int kw = iStart; kw < iStart + p.sedimentParticlesNumber; ++kw)
                            {
                                dataGridView[kw, tw].Value = o.dataArray3D()[kw - iStart, 0, tw - jStart].ToString();
                                dataGridView[kw, tw].ReadOnly = false;
                                dataGridView[kw, tw].Style.BackColor = Color.LemonChiffon;
                            }
                        }
                        dataGridView.CurrentCell = dataGridView[iStart, jStart];
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        for (int tw = jStart; tw < jStart + p.boundaryTimeNumber; ++tw)
                        {
                            for (int jw = iStart; jw < iStart + p.inputGrid.GetJ; ++jw)
                            {
                                dataGridView[jw, tw].Value = o.dataArray3D()[tabIndex, jw - iStart, tw - jStart].ToString();
                                dataGridView[jw, tw].ReadOnly = false;
                                dataGridView[jw, tw].Style.BackColor = Color.LemonChiffon;
                            }
                        }
                        dataGridView.CurrentCell = dataGridView[iStart, jStart];
                        break;
                }
            }
        }

        private void BottomBedLoadFluxApplyAll()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            for (int kw = 1; kw < p.sedimentParticlesNumber; ++kw)
            {
                for (int tw = jStart; tw < jStart + p.boundaryTimeNumber; ++tw)
                {
                    for (int jw = iStart; jw < iStart + p.inputGrid.GetJ; ++jw)
                    {
                        o.dataArray3D()[kw, jw - iStart, tw - jStart] = o.dataArray3D()[0, jw - iStart, tw - jStart];
                    }
                }
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (formType == FormType.FlowQuantity && e.ColumnIndex == 0 && e.RowIndex == jStart + rowCount + 1)
            {   //均一值 與 逐點輸入切換
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
            else if (formType == FormType.BottomBedLoadFlux && e.ColumnIndex == 0 && e.RowIndex == jStart + rowCount + 1)
            {   //均一值 與 逐點輸入切換
                if (!ConvertBottomBedLoadFluxData())
                {
                    MessageBox.Show("輸入資料格式錯誤，請先修正！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
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
            else if (formType == FormType.BottomBedLoadFlux && e.ColumnIndex == 1 && e.RowIndex == jStart + rowCount + 1)
            {   //套用到全部
                if (!ConvertBottomBedLoadFluxData())
                {
                    MessageBox.Show("輸入資料格式錯誤，請先修正！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                BottomBedLoadFluxApplyAll();
                InitializeDataGridView();
            }
            else if (formType == FormType.BottomBedLoadFlux && e.RowIndex == 0 && e.ColumnIndex >= iStart && e.ColumnIndex < iStart + p.sedimentParticlesNumber && o.type == RiverSimulationProfile.TwoInOne.Type.UseArray)
            {   //三維陣列標籤列
                int index = e.ColumnIndex - iStart;
                if(tabIndex != index)
                {
                    if(!ConvertBottomBedLoadFluxData())
                    {
                        MessageBox.Show("輸入資料格式錯誤，請先修正！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    tabIndex = index;
                    InitializeDataGridView();
                }
            }
        }

    }
}

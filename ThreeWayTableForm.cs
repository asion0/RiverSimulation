﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
            WaterLevel,
            BottomBedLoadFlux,
            DepthAverageConcentration,
        };

        public void SetFormMode(FormType t, string title, string colName, int colCount, int rowCount,  RiverSimulationProfile profile, object initData = null)
        {
            this.formType = t;
            this.p = profile;

            this.title = title;
            this.colCount = colCount;
            this.iTitle = colName;
            //this.rowCount = rowCount;

            if (formType == FormType.FlowQuantity || formType == FormType.WaterLevel)
            {
                if (p.IsVariableFlowType())
                {
                    this.iStart = 1;
                    this.jStart = 1;
                    this.rowCount = rowCount;
                    this.extraCol = (formType == FormType.FlowQuantity) ? 2 : 1;
                }
                else
                {
                    this.iStart = 0;
                    this.jStart = 1;
                    this.rowCount = 1;
                    this.extraCol = 1;
                }
                if (formType == FormType.FlowQuantity)
                {
                    type1Btn.Text = "均勻入流";
                    type2Btn.Text = "非均勻入流";
                }
                else
                {
                    type1Btn.Text = "均一值";
                    type2Btn.Text = "逐點給";
                }

            }
            else if (formType == FormType.BottomBedLoadFlux)
            {
                if (p.IsVariableFlowType())
                {
                    this.iStart = 1;
                    this.jStart = 1;
                    this.rowCount = rowCount;
                    this.extraCol = 2;
                    this.extraRow = 2;
                }
                else
                {
                    this.iStart = 0;
                    this.jStart = 1;
                    this.rowCount = 1;
                    this.extraCol = 2;
                    this.extraRow = 2;
                }
                this.colCount = (p.sedimentParticlesNumber > p.inputGrid.GetJ) ? p.sedimentParticlesNumber : p.inputGrid.GetJ;
            }
            else if (formType == FormType.DepthAverageConcentration)
            {
                this.iStart = 1;
                this.jStart = 2;
                this.rowCount = rowCount;
                this.extraCol = 2;
                this.extraRow = 4;

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
            if (DoConvert())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private bool DoConvert()
        {
            bool isSuccess = false;
            switch (formType)
            {
                case FormType.FlowQuantity:
                    isSuccess = ConvertFlowQuantityData();
                    break;
                case FormType.WaterLevel:
                    isSuccess = ConvertWaterLevelData();
                    break;                
                case FormType.BottomBedLoadFlux:
                    isSuccess = ConvertBottomBedLoadFluxData();
                    break;
                case FormType.DepthAverageConcentration:
                    isSuccess = ConvertDepthAverageConcentrationData();
                    break;            
            }

            if (!isSuccess)
            {
                MessageBox.Show("輸入資料格式錯誤！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return isSuccess;
        }

        private string title;
        private int colCount = 0;
        private int rowCount = 0;
        private object _data = null;
        private int tabIndex = 0;
        private Color colHeaderColor1 = Color.CornflowerBlue;        //橫向(上方)標題顏色
        private Color colHeaderColor2 = Color.LightSkyBlue;        //橫向(上方)標題顏色
        private Color rowHeaderColor = Color.LightGray;            //直向(左方)標題顏色
        private Color tabActiveColor = Color.CornflowerBlue;         //被選取標籤顏色
        private Color tabItemColor = Color.DarkSlateBlue;            //未選取標籤顏色
        private Color tabPageItemColor = Color.LemonChiffon;            //三維表單內文鵝黃色
        private Color tabPageItemColor2 = Color.Gold;            //三維表單內文鵝黃色
        
        private void ThreeWayTableForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            InitializeDataGridView();

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private bool ConvertWaterLevelData()
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
                            o.Value2D()[0, jw - jStart] = Convert.ToDouble(v[iStart, jw].Value);
                        }
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        for (int jw = jStart; jw < jStart + rowCount; ++jw)
                        {
                            for (int iw = iStart; iw < iStart + colCount; ++iw)
                            {
                                o.Array2D()[iw - iStart, jw - jStart] = Convert.ToDouble(v[iw, jw].Value);
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
                            o.Value2D()[0, jw - jStart] = Convert.ToDouble(v[iStart, jw].Value);
                        }
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        if (!AutoFinishConvertFlowQualityCell())
                        {
                            return false;
                        }
                        for (int jw = jStart; jw < jStart + rowCount; ++jw)
                        {
                            for (int iw = iStart; iw < iStart + colCount; ++iw)
                            {
                                o.Array2D()[iw - iStart, jw - jStart] = Convert.ToDouble(v[iw + 1, jw].Value);
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
                        for (int tw = jStart; tw < jStart + (p.IsConstantFlowType() ? 1 : p.boundaryTimeNumber); ++tw)
                        {
                            for (int kw = iStart; kw < iStart + p.sedimentParticlesNumber; ++kw)
                            {
                                o.Value3D()[kw - iStart, 0, tw - jStart] = Convert.ToDouble(v[kw, tw].Value);
                             }
                        }
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        for (int tw = jStart; tw < jStart + (p.IsConstantFlowType() ? 1 : p.boundaryTimeNumber); ++tw)
                        {
                            for (int jw = iStart; jw < iStart + p.inputGrid.GetJ; ++jw)
                            {
                                o.Array3D()[tabIndex, jw - iStart, tw - jStart] = Convert.ToDouble(v[jw + 1, tw + 1].Value);
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

        private bool ConvertDepthAverageConcentrationData()
        {
            try
            {
                DataGridView v = dataGridView;
                RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        for (int tw = jStart; tw < jStart + 1; ++tw)
                        {
                            for (int kw = iStart; kw < iStart + p.sedimentParticlesNumber; ++kw)
                            {
                                o.Value3D()[kw - iStart, 0, 0] = Convert.ToDouble(v[kw, tw].Value);
                            }
                        }
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        for (int jw = jStart; jw < jStart + p.inputGrid.GetI; ++jw)
                        {
                            for (int iw = iStart; iw < iStart + p.inputGrid.GetJ; ++iw)
                            {
                                o.Array3D()[tabIndex, iw - iStart, jw - jStart] = Convert.ToDouble(v[iw, jw].Value);
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
            RiverSimulationProfile.TwoInOne _d = null;
            Debug.Assert(o != null);

            if (formType == FormType.FlowQuantity || formType == FormType.WaterLevel)
            {
                _data = new RiverSimulationProfile.TwoInOne(d as RiverSimulationProfile.TwoInOne);
                _d = _data as RiverSimulationProfile.TwoInOne;
                Debug.Assert(_d != null);
                if (o.ValueNull() || o.ArrayNull() || 
                   (o.valueType == RiverSimulationProfile.TwoInOne.ValueType.TwoDim && (o.Value2D().GetLength(0) != colCount || o.Value2D().GetLength(1) != rowCount)) ||
                   (o.valueType == RiverSimulationProfile.TwoInOne.ValueType.ThreeDim && (o.Value3D().GetLength(0) != colCount || o.Value3D().GetLength(1) != rowCount)))
                {   //rowCount : Q1 ~ Q5, colCount : J1 ~ J15
                    //_data = new RiverSimulationProfile.TwoInOne(colCount, rowCount);
                   _d.Create2D(colCount, rowCount);
                   _d.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
                }
            }
            else if (formType == FormType.BottomBedLoadFlux)
            {   
                _data = new RiverSimulationProfile.TwoInOne(d as RiverSimulationProfile.TwoInOne);
                _d = _data as RiverSimulationProfile.TwoInOne;
                Debug.Assert(_d != null);
                if (o.ValueNull() || o.ArrayNull())
                {   //rowCount : Q1 ~ Q5, colCount : J1 ~ J15
                    // _data = new RiverSimulationProfile.TwoInOne(p.boundaryTimeNumber, p.sedimentParticlesNumber, p.inputGrid.GetJ); //[T, K, J]
                    //_data = new RiverSimulationProfile.TwoInOne(p.sedimentParticlesNumber, p.inputGrid.GetJ, p.boundaryTimeNumber); //[K, J, T]
                    //_d.Create3D(p.sedimentParticlesNumber, p.inputGrid.GetJ, p.boundaryTimeNumber);
                    _d.Create3D(p.sedimentParticlesNumber, p.inputGrid.GetJ, rowCount);
                    _d.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
               }
            }
            else if (formType == FormType.DepthAverageConcentration)
            {
                _data = new RiverSimulationProfile.TwoInOne(d as RiverSimulationProfile.TwoInOne);
                _d = _data as RiverSimulationProfile.TwoInOne;
                Debug.Assert(_d != null);
                if (o.ValueNull() || o.ArrayNull())
                {   //rowCount : Q1 ~ Q5, colCount : J1 ~ J15
                    _d.Create3D(p.sedimentParticlesNumber, p.inputGrid.GetJ, p.inputGrid.GetI);
                }
            }        
        }

        private FormType formType = FormType.FlowQuantity;
        private int iStart = 1;
        private int jStart = 2;
        private string iTitle;
        private string timeTitle = "邊界時間";
        //private string buttonText;
        //private string checkText;
        private int extraCol = 2;
        private int extraRow = 1;

        private void InitializeDataGridView()
        {
            dataGridView.Rows.Clear();

            DataGridViewUtility.InitializeDataGridView(dataGridView, colCount + extraCol, rowCount + extraRow, 120, 60,
                "", "", "", false, false);

            //if(formType == FormType.FlowQuantity)
            //{
            //    dataGridView[0, 0].Value = p.IsVariableFlowType() ? "變量流(輸入百分比)" : "定量流(輸入百分比)";
            //}
            //else
            //{
            //    dataGridView[0, 0].Value = p.IsVariableFlowType() ? "變量流" : "定量流";
            //}
            flowTypeLbl.Text = p.IsVariableFlowType() ? "變量流" : "定量流";

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
            else if (formType == FormType.WaterLevel)
            {
                InitTableWaterLevel();
            }
            else if (formType == FormType.BottomBedLoadFlux)
            {
                InitTableBottomBedLoadFlux();
            }
            else if (formType == FormType.DepthAverageConcentration)
            {
                InitTableDepthAverageConcentration();
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
                dataGridView[0, jStart - 1].Value = timeTitle;
                dataGridView[0, jStart - 1].Style.BackColor = rowHeaderColor;
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
                    for (int iw = iStart; iw < iStart + 1; ++iw)
                    {   //填入橫排標題, 只有一欄
                        dataGridView[iw, jStart - 1].Value = iTitle;
                        dataGridView[iw, jStart - 1].Style.BackColor = colHeaderColor1;
                    }
                    type1Btn.Enabled = false;
                    type2Btn.Enabled = true;

                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    dataGridView[iStart, jStart - 1].Value = iTitle;
                    dataGridView[iStart, jStart - 1].Style.BackColor = colHeaderColor1;

                    for (int iw = iStart; iw < iStart + colCount; ++iw)
                    {   //填入橫排標題
                        dataGridView[iw + 1, jStart - 1].Value = iTitle + (iw - iStart + 1).ToString() + "比例";
                        dataGridView[iw + 1, jStart - 1].Style.BackColor = colHeaderColor2;
                    }
                    type1Btn.Enabled = true;
                    type2Btn.Enabled = false;
                    break;
            }

            if(o.check)
            {
                type2Rdo.Checked = true;
            }
            else
            {
                type1Rdo.Checked = true;
            }
            //DataGridViewButtonCell b = new DataGridViewButtonCell();
            //b.Value = buttonText;
            //b.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView[0, jStart + rowCount + 1] = b;
            
            //if (formType == FormType.FlowQuantity)
            //{
            //    DataGridViewCheckBoxCell c = new DataGridViewCheckBoxCell();
            //    c.Value = o.check;
            //    c.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    dataGridView[0, jStart + rowCount] = c;
            //}
        }

        private void InitTableWaterLevel()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (o == null)
            {
                return;
            }

            if (p.IsVariableFlowType())
            {   //變量流需顯示邊界時間欄位
                dataGridView[0, jStart - 1].Value = timeTitle;
                dataGridView[0, jStart - 1].Style.BackColor = rowHeaderColor;
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
                    for (int iw = iStart; iw < iStart + 1; ++iw)
                    {   //填入橫排標題, 只有一欄
                        dataGridView[iw, jStart - 1].Value = iTitle;
                        dataGridView[iw, jStart - 1].Style.BackColor = colHeaderColor1;
                    }
                    type1Btn.Enabled = false;
                    type2Btn.Enabled = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    for (int iw = iStart; iw < iStart + colCount; ++iw)
                    {   //填入橫排標題
                        dataGridView[iw, jStart - 1].Value = iTitle + (iw - iStart + 1).ToString();
                        dataGridView[iw, jStart - 1].Style.BackColor = colHeaderColor1;
                    }
                    type1Btn.Enabled = true;
                    type2Btn.Enabled = false;
                    break;
            }

            if(o.check)
            {
                type2Rdo.Checked = true;
            }
            else
            {
                type1Rdo.Checked = true;
            }
        }

        private void InitTableBottomBedLoadFlux()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (o == null)
            {
                return;
            }

            int jShift = (o.type == RiverSimulationProfile.TwoInOne.Type.UseArray) ? 1 : 0;
            if (p.IsVariableFlowType())
            {   //變量流需顯示邊界時間欄位
                dataGridView[0, jStart - 1 + jShift].Value = timeTitle;
                dataGridView[0, jStart - 1 + jShift].Style.BackColor = rowHeaderColor;

                for (int jw = jStart; jw < jStart + p.boundaryTimeNumber; ++jw)
                {
                    dataGridView[0, jw + jShift].Value = p.boundaryTime[jw - jStart].ToString();
                    dataGridView[0, jw + jShift].Style.BackColor = rowHeaderColor;
                }
            }

            //DataGridViewButtonCell b = null;
            switch (o.type)
            {
                case RiverSimulationProfile.TwoInOne.Type.None:
                case RiverSimulationProfile.TwoInOne.Type.UseValue:
                    for (int iw = iStart; iw < iStart + p.sedimentParticlesNumber; ++iw)
                    {   //填入橫排標題，泥砂數目(K)
                        dataGridView[iw, jStart - 1].Value = iTitle + (iw - iStart + 1).ToString();
                        dataGridView[iw, jStart - 1].Style.BackColor = colHeaderColor1;
                    }
                    type1Btn.Enabled = false;
                    type2Btn.Enabled = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    dataGridView[iStart, jStart].Value = "粒徑" + (tabIndex + 1).ToString();
                    dataGridView[iStart, jStart].Style.BackColor = colHeaderColor2;
                    for (int iw = iStart; iw < iStart + p.inputGrid.GetJ; ++iw)
                    {   //填入橫排標題，J個
                        dataGridView[iw + 1, jStart].Value = (iw - iStart + 1).ToString() + "比例%";
                        dataGridView[iw + 1, jStart].Style.BackColor = colHeaderColor1;
                    }
                    for (int iw = iStart; iw < iStart + p.sedimentParticlesNumber; ++iw)
                    {   //填入標籤標題，K個
                        dataGridView[iw + 1, jStart - 1].Value = iTitle + (iw - iStart + 1).ToString();
                        dataGridView[iw + 1, jStart - 1].Style.BackColor = (tabIndex == (iw - iStart)) ? tabActiveColor : tabItemColor;
                    }
                    DataGridViewButtonCell b = new DataGridViewButtonCell();
                    b.Value = "比例套用到全部";
                    b.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView[iStart, jStart - 1] = b;

                    type1Btn.Enabled = true;
                    type2Btn.Enabled = false;
                    break;
            }

            if (o.check)
            {
                type2Rdo.Checked = true;
            }
            else
            {
                type1Rdo.Checked = true;
            }
            
        }

        private void InitTableDepthAverageConcentration()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (o == null)
            {
                return;
            }
            type1Rdo.Visible = false;
            type2Rdo.Visible = false;
            switch (o.type)
            {
                case RiverSimulationProfile.TwoInOne.Type.None:
                case RiverSimulationProfile.TwoInOne.Type.UseValue:
                    //buttonText = "進階 - 逐點輸入";
                    for (int iw = iStart; iw < iStart + p.sedimentParticlesNumber; ++iw)
                    {   //填入橫排標題，泥砂數目(K)
                        dataGridView[iw, jStart - 1].Value = iTitle + (iw - iStart + 1).ToString();
                        dataGridView[iw, jStart - 1].Style.BackColor = colHeaderColor1;
                    }
                    type1Btn.Enabled = false;
                    type2Btn.Enabled = true;
                    break;
                case RiverSimulationProfile.TwoInOne.Type.UseArray:
                    //顯示I左方垂直索引
                    dataGridView[0, 1].Value = "";
                    dataGridView[0, 1].Style.BackColor = rowHeaderColor;
                    for (int jw = jStart; jw < jStart + p.inputGrid.GetI; ++jw)
                    {
                        dataGridView[0, jw].Value = (jw - jStart).ToString();
                        dataGridView[0, jw].Style.BackColor = rowHeaderColor;
                    }
                    //buttonText = "均一值";
                    for (int iw = iStart; iw < iStart + p.inputGrid.GetJ; ++iw)
                    {   //填入橫排標題，I個
                        dataGridView[iw, 1].Value = (iw - iStart + 1).ToString();
                        dataGridView[iw, 1].Style.BackColor = colHeaderColor1;
                    }
                    for (int iw = iStart; iw < iStart + p.sedimentParticlesNumber; ++iw)
                    {   //填入標籤標題，K個
                        dataGridView[iw, 0].Value = iTitle + (iw - iStart + 1).ToString();
                        dataGridView[iw, 0].Style.BackColor = (tabIndex == (iw - iStart)) ? tabActiveColor : tabItemColor;
                    }
                    //b = new DataGridViewButtonCell();
                    //b.Value = "套用到全部";
                    //b.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //dataGridView[1, jStart + rowCount + 1] = b;
                    type1Btn.Enabled = true;
                    type2Btn.Enabled = false;
                    break;
            }
            //b = new DataGridViewButtonCell();
            //b.Value = buttonText;
            //b.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView[0, jStart + rowCount + 1] = b;
            
        }

        private void FillDataGridView()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (formType == FormType.FlowQuantity || formType == FormType.WaterLevel)
            {
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        for (int jw = jStart; jw < jStart + rowCount; ++jw)
                        {
                            dataGridView[iStart, jw].Value = o.Value2D()[0, jw - jStart].ToString();
                            dataGridView[iStart, jw].ReadOnly = false;
                            dataGridView[iStart, jw].Style.BackColor = tabPageItemColor;
                        }
                        dataGridView.CurrentCell = dataGridView[iStart, jStart];
                        //dataGridView[1, jStart]. = true;
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        if (formType == FormType.FlowQuantity)
                        {
                            for (int jw = jStart; jw < jStart + rowCount; ++jw)
                            {
                                dataGridView[iStart, jw].Value = o.Value2D()[0, jw - jStart].ToString();
                                dataGridView[iStart, jw].ReadOnly = true;
                                dataGridView[iStart, jw].Style.BackColor = tabPageItemColor2;
                            }
                        }

                        for (int jw = jStart; jw < jStart + rowCount; ++jw)
                        {
                            for (int iw = iStart; iw < iStart + colCount; ++iw)
                            {
                                if (formType == FormType.FlowQuantity)
                                {

                                    dataGridView[iw + 1, jw].Value = o.Array2D()[iw - iStart, jw - jStart].ToString();
                                    dataGridView[iw + 1, jw].ReadOnly = false;
                                    dataGridView[iw + 1, jw].Style.BackColor = tabPageItemColor;
                                }
                                else
                                {

                                    dataGridView[iw, jw].Value = o.Array2D()[iw - iStart, jw - jStart].ToString();
                                    dataGridView[iw, jw].ReadOnly = false;
                                    dataGridView[iw, jw].Style.BackColor = tabPageItemColor;
                                }
                            }
                        }
                        if (formType == FormType.FlowQuantity)
                        {
                            AutoFinishConvertFlowQualityCell();
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
                        for (int tw = jStart; tw < jStart + (p.IsConstantFlowType() ? 1 : p.boundaryTimeNumber); ++tw)
                        {
                            for (int kw = iStart; kw < iStart + p.sedimentParticlesNumber; ++kw)
                            {
                                dataGridView[kw, tw].Value = o.Value3D()[kw - iStart, 0, tw - jStart].ToString();
                                dataGridView[kw, tw].ReadOnly = false;
                                dataGridView[kw, tw].Style.BackColor = tabPageItemColor;
                            }
                        }
                        dataGridView.CurrentCell = dataGridView[iStart, jStart];
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        for (int tw = jStart; tw < jStart + (p.IsConstantFlowType() ? 1 : p.boundaryTimeNumber); ++tw)
                        {
                            for (int jw = iStart; jw < iStart + p.inputGrid.GetJ; ++jw)
                            {
                                dataGridView[jw + 1, tw + 1].Value = o.Array3D()[tabIndex, jw - iStart, tw - jStart].ToString();
                                dataGridView[jw + 1, tw + 1].ReadOnly = false;
                                dataGridView[jw + 1, tw + 1].Style.BackColor = tabPageItemColor;
                            }
                            dataGridView[iStart, tw + 1].Value = o.Value3D()[tabIndex, 0, tw - jStart].ToString();
                            dataGridView[iStart, tw + 1].ReadOnly = true;
                            dataGridView[iStart, tw + 1].Style.BackColor = tabPageItemColor2;
                        }
                        dataGridView.CurrentCell = dataGridView[iStart, jStart];
                        break;
                }
            }
            else if (formType == FormType.DepthAverageConcentration)
            {
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        for (int jw = jStart; jw < jStart + 1; ++jw)
                        {
                            for (int iw = iStart; iw < iStart + p.sedimentParticlesNumber; ++iw)
                            {
                                dataGridView[iw, jw].Value = o.Value3D()[iw - iStart, 0, 0].ToString();
                                dataGridView[iw, jw].ReadOnly = false;
                                dataGridView[iw, jw].Style.BackColor = tabPageItemColor;
                            }
                        }
                        dataGridView.CurrentCell = dataGridView[iStart, jStart];
                        break;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        for (int jw = jStart; jw < jStart + p.inputGrid.GetI; ++jw)
                        {
                            for (int iw = iStart; iw < iStart + p.inputGrid.GetJ; ++iw)
                            {
                                dataGridView[iw, jw].Value = o.Array3D()[tabIndex, iw - iStart, jw - jStart].ToString();
                                dataGridView[iw, jw].ReadOnly = false;
                                dataGridView[iw, jw].Style.BackColor = tabPageItemColor;
                            }
                        }
                        dataGridView.CurrentCell = dataGridView[iStart, jStart];
                        break;
                }
            }
            dataGridView.PerformLayout();
        }

        private void BottomBedLoadFluxApplyAll()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            for (int kw = 1; kw < p.sedimentParticlesNumber; ++kw)
            {
                for (int tw = jStart; tw < jStart + (p.IsConstantFlowType() ? 1 : p.boundaryTimeNumber); ++tw)
                {
                    for (int jw = iStart; jw < iStart + p.inputGrid.GetJ; ++jw)
                    {
                        o.Array3D()[kw, jw - iStart, tw - jStart] = o.Array3D()[0, jw - iStart, tw - jStart];
                    }
                }
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;

            if (formType == FormType.DepthAverageConcentration && e.ColumnIndex == 0 && e.RowIndex == jStart + rowCount + 1)
            {   //均一值 與 逐點輸入切換
                if (!ConvertDepthAverageConcentrationData())
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
            else if (formType == FormType.BottomBedLoadFlux && e.ColumnIndex == iStart && e.RowIndex == jStart - 1)
            {   //套用到全部
                if (!ConvertBottomBedLoadFluxData())
                {
                    MessageBox.Show("輸入資料格式錯誤，請先修正！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                BottomBedLoadFluxApplyAll();
                InitializeDataGridView();
            }
            else if (formType == FormType.BottomBedLoadFlux && e.RowIndex == 0 && e.ColumnIndex >= iStart + 1 && e.ColumnIndex < iStart + p.sedimentParticlesNumber + 1 && o.type == RiverSimulationProfile.TwoInOne.Type.UseArray)
            {   //三維陣列標籤列
                int index = e.ColumnIndex - iStart - 1;
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
            else if (formType == FormType.DepthAverageConcentration && e.RowIndex == 0 && e.ColumnIndex >= iStart && e.ColumnIndex < iStart + p.sedimentParticlesNumber && o.type == RiverSimulationProfile.TwoInOne.Type.UseArray)
            {   //三維陣列標籤列
                int index = e.ColumnIndex - iStart;
                if (tabIndex != index)
                {
                    if (!ConvertDepthAverageConcentrationData())
                    {
                        MessageBox.Show("輸入資料格式錯誤，請先修正！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    tabIndex = index;
                    InitializeDataGridView();
                }
            }
            //else if (formType == FormType.FlowQuantity && e.ColumnIndex == 0 && e.RowIndex == jStart + rowCount)
            //{   //進階模式勾選
            //    dataGridView[0, jStart + rowCount].Value = !(bool)dataGridView[0, jStart + rowCount].Value;
            //    o.check = (bool)dataGridView[0, jStart + rowCount].Value;
            //}
            //else if (formType == FormType.FlowQuantity && o.type == RiverSimulationProfile.TwoInOne.Type.UseArray &&
            //    e.ColumnIndex >= iStart && e.ColumnIndex < iStart + colCount - 1 && e.RowIndex >= jStart && e.RowIndex < jStart + rowCount)
            //{
            //    AutoFinishConvertFlowQualityCell();
            //}
        }

        private bool AutoFinishConvertFlowQualityCell()
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            double sum = 0.0;
            bool allPass = true;

            for (int jw = jStart; jw < jStart + rowCount; ++jw)
            {
                if (!CalFlowQualitySumOfOneRow(jw, ref sum))
                {
                    allPass = false;
                    continue;
                }
                dataGridView[iStart + colCount, jw].Value = (100.0 - sum).ToString();
            }

            return allPass;
        }

        private bool CalFlowQualitySumOfOneRow(int index, ref double sum)
        {
            sum = 0.0;
            for (int iw = iStart; iw < iStart + colCount - 1; ++iw)
            {
                try
                {
                    sum += Convert.ToDouble(dataGridView[iw + 1, index].Value);
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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.CopyToClipboard(dataGridView);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.PasteFromeExcel(dataGridView);
        }

        private void ValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.FillSelectedValue(dataGridView);
        }

        private void type1Btn_Click(object sender, EventArgs e)
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (formType == FormType.FlowQuantity)
            {   //流量 - 均一值 與 逐點輸入切換
                if (!ConvertFlowQuantityData())
                {
                    MessageBox.Show("輸入資料格式錯誤，請先修正！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        return;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        o.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
                        break;
                }
                InitializeDataGridView();
            }
            else if (formType == FormType.WaterLevel)
            {   //流量 - 均一值 與 逐點輸入切換
                if (!ConvertWaterLevelData())
                {
                    MessageBox.Show("輸入資料格式錯誤，請先修正！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        return;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        o.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
                        break;
                }
                InitializeDataGridView();
            }
            else if (formType == FormType.BottomBedLoadFlux)
            {   //底床載 - 均一值 與 逐點輸入切換
                if (!ConvertBottomBedLoadFluxData())
                {
                    MessageBox.Show("輸入資料格式錯誤，請先修正！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        return;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        o.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
                        break;
                }
                InitializeDataGridView();
            }
            else if (formType == FormType.DepthAverageConcentration)
            {   //均一值 與 逐點輸入切換
                if (!ConvertDepthAverageConcentrationData())
                {
                    MessageBox.Show("輸入資料格式錯誤，請先修正！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                switch (o.type)
                {
                    case RiverSimulationProfile.TwoInOne.Type.None:
                    case RiverSimulationProfile.TwoInOne.Type.UseValue:
                        return;
                    case RiverSimulationProfile.TwoInOne.Type.UseArray:
                        o.type = RiverSimulationProfile.TwoInOne.Type.UseValue;
                        break;
                }
                InitializeDataGridView();
            }
        }

        private void type2Btn_Click(object sender, EventArgs e)
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if (formType == FormType.FlowQuantity)
            {   //流量 - 均一值 與 逐點輸入切換
                if (!ConvertFlowQuantityData())
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
                        return;;
                }
                InitializeDataGridView();
            }
            else if (formType == FormType.WaterLevel)
            {   //流量 - 均一值 與 逐點輸入切換
                if (!ConvertWaterLevelData())
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
                        return; ;
                }
                InitializeDataGridView();
            }
            else if (formType == FormType.BottomBedLoadFlux)
            {   //底床載 - 均一值 與 逐點輸入切換
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
                        return;
                }
                InitializeDataGridView();
            }
            else if (formType == FormType.DepthAverageConcentration)
            {   //均一值 與 逐點輸入切換
                if (!ConvertDepthAverageConcentrationData())
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
                        return;
                }
                InitializeDataGridView();
            }
        }

        private void type1Rdo_CheckedChanged(object sender, EventArgs e)
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if ((sender as RadioButton).Checked && o.check == true)
            {
                o.check = false;
            }
        }

        private void type2Rdo_CheckedChanged(object sender, EventArgs e)
        {
            RiverSimulationProfile.TwoInOne o = _data as RiverSimulationProfile.TwoInOne;
            if ((sender as RadioButton).Checked && o.check == false)
            {
                o.check = true;
            }
        }
    }
}

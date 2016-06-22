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
    public partial class SideFlowSetTableForm : Form
    {
        public SideFlowSetTableForm()
        {
            InitializeComponent();
        }

        private bool CheclEditBtnEnabled(int type, int count)
        {
            List<Point> pts = (type == 0) ? sideOutObjects[count].sideFlowPoints : sideInObjects[count].sideFlowPoints;
            return (null != pts);
        }

        private void GetTypeAndCount(int index, ref int type, ref int count)
        {
            type = (sideOutObjects != null && index < sideOutObjects.Length) ? 0 : 1;
            count = (type == 0) ? index : index - ((sideOutObjects == null) ? 0 : sideOutObjects.Length);
        }

        private string objectName;
        private RiverSimulationProfile.SideFlowObject[] sideOutObjects = null;
        private RiverSimulationProfile.SideFlowObject[] sideInObjects = null;
        public void SetFormMode(string title, string name, 
            RiverSimulationProfile.SideFlowObject[] outObjs, RiverSimulationProfile.SideFlowObject[] inObjs)
        {
            this.Text = title;
            objectName = name;
            sideOutObjects = outObjs;
            sideInObjects = inObjs;
        }

        private RiverSimulationProfile p = RiverSimulationProfile.profile;

        private int[] groupColors = null;
        private Color[] colorTable = null;
        //private List<Point>[] pts = null;
        private int selIndex = -1;
        public void SetGroupColors(int[] gc)
        {
            groupColors = gc;
        }

        public void SetColorTable(Color[] ct)
        {
            colorTable = ct;
        }

        //public void SetGridData(List<Point>[] p, int i)
        //{
        //    pts = p;
        //    selIndex = i;
        //}

        private string[] structureName;
        //private int[] structureNum;
        //private RiverSimulationProfile.StructureType[] typeIndex = null;

        public void SetSelectionItems(ListBox lb, string[] structureName)
        {
            for(int i=0; i<lb.Items.Count; ++i)
            {
                selCombo.Items.Add(lb.Items[i].ToString());
            }
            this.structureName = structureName;
            //this.structureNum = structureNum;
            //this.typeIndex = typeIndex;
            selIndex = lb.SelectedIndex;

        }

        private void GridGroupTableForm_Load(object sender, EventArgs e)
        {
            InitialDataGrid();
            selCombo.SelectedIndex = selIndex;
            FillDataGrid();
        }

        private void InitialDataGrid()
        {
            int columnCount = p.inputGrid.GetJ;
            int rowCount = p.inputGrid.GetI;
            DataGridView g = dataGv;

            // Create an unbound DataGridView by declaring a column count.
            g.ColumnCount = columnCount;
            g.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            //columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            string[] row = new string[columnCount];
            // Set the column header names.
            int c = 1;
            for (int i = 0; i < columnCount; ++i)
            {
                g.Columns[i].Name = c.ToString();
                g.Columns[i].Width = 56;
                //row[i] = "1";
                c++;
            }
            g.RowHeadersWidth = 64;
            int n1 = g.Rows.Count;
            g.Rows.Clear();
            n1 = g.Rows.Count;

            for (int i = 0; i < rowCount; i++)
            {
                g.Rows.Add(row);
                g.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }

        }

        private Color selectedColor = Color.Blue;
        private Color alertColor = Color.Red;
        private void FillDataGrid(List<Point> newPl = null, bool alert = false, bool fillZ = false)
        {
            //RiverSimulationProfile p = RiverSimulationProfile.profile;
            var rg = p.inputGrid;

            //int type = 0, count = 0;
//            StructureSetUtility.CalcTypeCount(selIndex, ref type, ref count, typeIndex);
            int index = selCombo.SelectedIndex;
            int type = 0, count = 0;
            GetTypeAndCount(index, ref type, ref count);

            Point pt = new Point();
            for (int i = 0; i < rg.GetI; ++i)
            {
                for (int j = 0; j < rg.GetJ; ++j)
                {
                    pt.X = i;
                    pt.Y = j;
                    Point grpId = SideFlowtUtility.WhichGroup(sideOutObjects, sideInObjects, pt);
                    int nameId = grpId.X * (sideOutObjects == null ? 0 : sideOutObjects.Length) + grpId.Y;
                    Color cr;
                    if (grpId.X == -1 && grpId.Y == -1)
                    {   //空白處
                        cr = Color.White;
                        if (fillZ)
                        {
                            dataGv[j, i].Value = p.inputGrid.inputCoor[i, j].z.ToString();
                            dataGv[j, i].ReadOnly = true;
                        }
                        else
                        {
                            dataGv[j, i].Value = "";
                            dataGv[j, i].ReadOnly = true;
                        }
                    }
                    else if (grpId.X == type && grpId.Y == count)
                    {   //被選取的結構物
                        cr = (alert) ? alertColor : selectedColor;
                        if (fillZ)
                        {
                            dataGv[j, i].Value = structureName[nameId];
                            dataGv[j, i].ReadOnly = false;
                        }
                        else
                        {
                            dataGv[j, i].Value = structureName[nameId];
                            dataGv[j, i].ReadOnly = true;
                        }
                    }
                    else
                    {   //其他結構物
                        cr = colorTable[grpId.X % colorTable.Length];
                        if (fillZ)
                        {
                            dataGv[j, i].Value = structureName[nameId];
                            dataGv[j, i].ReadOnly = false;
                        }
                        else
                        {
                            dataGv[j, i].Value = structureName[nameId];
                            dataGv[j, i].ReadOnly = true;
                        }
                    }

                    dataGv[j, i].Style.BackColor = cr;
                }
            }
        }

        private void dataGv_SelectionChanged(object sender, EventArgs e)
        {
            if (selCombo.SelectedIndex == -1)
                return;
            var selectedCellCount = dataGv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount <= 0)
                return;

            int index = selCombo.SelectedIndex;
            int type = 0, count = 0;
            GetTypeAndCount(index, ref type, ref count);

            List<Point> pl = new List<Point>();
            for (int i = 0; i < selectedCellCount; ++i)
            {   //pl 所有被選取的點集合
                pl.Add(new Point(dataGv.SelectedCells[i].RowIndex, dataGv.SelectedCells[i].ColumnIndex));
            }
            addBtn.Enabled = SideFlowtUtility.IsAllInEmpty(p, pl, type, count);

            List<Point> pts = (type == 0) ? sideOutObjects[count].sideFlowPoints : sideInObjects[count].sideFlowPoints;
            removeBtn.Enabled = CheclRemoveBtnEnabled(pts, pl);

            editBtn.Enabled = CheclEditBtnEnabled(type, count);
        }

        private bool CheclRemoveBtnEnabled(List<Point> pts, List<Point> pl)
        {
            return (pts == null) ? false : SideFlowtUtility.IsAllInclude(pts, pl);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            var p = RiverSimulationProfile.profile;
            var selectedCellCount = dataGv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount <= 0)
                return;

            int index = selCombo.SelectedIndex;
            int type = 0, count = 0;
            GetTypeAndCount(index, ref type, ref count);

            List<Point> pl = new List<Point>();     //表格內被選取的格網點
            for (int i = 0; i < selectedCellCount; ++i)
            {
                pl.Add(new Point(dataGv.SelectedCells[i].RowIndex, dataGv.SelectedCells[i].ColumnIndex));
            }

            List<Point> pts = (type == 0) ? sideOutObjects[count].sideFlowPoints : sideInObjects[count].sideFlowPoints;
            List<Point> plSelected = (pts == null) ? null : new List<Point>(pts);
            if(null != plSelected)
            {   //正在編輯的結構物不為空則合併被選取的格網點到正在編輯的結構物中
                SideFlowtUtility.MergePoints(ref plSelected, pl);
                SideFlowtUtility.TrimToEdge(ref plSelected, p.inputGrid.GetI, p.inputGrid.GetJ);
            }
            else
            {   //正在編輯的結構物還沒有任何格網點
                if (!SideFlowtUtility.TrimToEdge(ref pl, p.inputGrid.GetI, p.inputGrid.GetJ))
                {
                    MessageBox.Show("請圈選邊界！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                plSelected = new List<Point>(pl);
            }

            if (!SideFlowtUtility.IsContinuous(plSelected))
            {   //檢查是否連續
                FillDataGrid(plSelected, true);
                MessageBox.Show("新增後不是連續區域，請重新選取！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FillDataGrid();
                return;
            }

            if (type == 0)
            {
                sideOutObjects[count].sideFlowPoints = plSelected;
            }
            else
            {
                sideInObjects[count].sideFlowPoints = plSelected;
            }

            FillDataGrid();
            dataGv.ClearSelection();
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            var selectedCellCount = dataGv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount <= 0)
                return;

            int index = selCombo.SelectedIndex;
            int type = 0, count = 0;
            GetTypeAndCount(index, ref type, ref count);

            List<Point> pl = new List<Point>();
            for (int i = 0; i < selectedCellCount; ++i)
            {
                pl.Add(new Point(dataGv.SelectedCells[i].RowIndex, dataGv.SelectedCells[i].ColumnIndex));
            }

            List<Point> pts = (type == 0) ? sideOutObjects[count].sideFlowPoints : sideInObjects[count].sideFlowPoints;
            List<Point> plSelected = (pts == null) ? null : new List<Point>(pts);
            SideFlowtUtility.RemovePoints(ref plSelected, pl);

            if (!SideFlowtUtility.IsContinuous(plSelected))
            {
                FillDataGrid(plSelected, true);
                MessageBox.Show("刪減後不是連續區域，請重新選取！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FillDataGrid();
                return;
            }

            if (type == 0)
            {
                sideOutObjects[count].sideFlowPoints = plSelected;
            }
            else
            {
                sideInObjects[count].sideFlowPoints = plSelected;
            }

            FillDataGrid();
            dataGv.ClearSelection();
        }

        private void dryBedCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = selCombo.SelectedIndex;
            int type = 0, count = 0;
            GetTypeAndCount(index, ref type, ref count);
            editBtn.Enabled = CheclEditBtnEnabled(type, count);

            if (type == 0)
            {
                if (sideOutObjects[count].criticalFlowType == RiverSimulationProfile.CriticalFlowType.SubCriticalFlow)
                    subTypeRdo.Checked = true;
                else
                    superTypeRdo.Checked = true;
            }
            else
            {
                if (sideInObjects[count].criticalFlowType == RiverSimulationProfile.CriticalFlowType.SubCriticalFlow)
                    subTypeRdo.Checked = true;
                else
                    superTypeRdo.Checked = true;
            }

            FillDataGrid();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            int index = selCombo.SelectedIndex;
            int type = 0, count = 0;
            GetTypeAndCount(index, ref type, ref count);

            String title = String.Format("輸入{0}流量/水位", selCombo.Items[index].ToString());
            SideFlowDataInputForm form = new SideFlowDataInputForm();
            form.SetFormMode(title, p.inputGrid.GetJ, p.inputGrid.GetI, "", "", "",
                120, 120, false, false, false, (type == 0) ? sideOutObjects[count] : sideInObjects[count]);
            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                if (type == 0)
                {
                    sideOutObjects[count].flowData = (RiverSimulationProfile.TwoInOne)form.data;
                }
                else
                {
                    sideInObjects[count].flowData = (RiverSimulationProfile.TwoInOne)form.data;
                }
            }
 
        }

        private void superTypeRdo_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                int index = selCombo.SelectedIndex;
                int type = 0, count = 0;
                GetTypeAndCount(index, ref type, ref count);
                if (type == 0)
                {
                    sideOutObjects[count].criticalFlowType = RiverSimulationProfile.CriticalFlowType.SuperCriticalFlow;
                }
                else
                {
                    sideInObjects[count].criticalFlowType = RiverSimulationProfile.CriticalFlowType.SuperCriticalFlow;
                }
            }
        }

        private void subTypeRdo_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                int index = selCombo.SelectedIndex;
                int type = 0, count = 0;
                GetTypeAndCount(index, ref type, ref count);
                if (type == 0)
                {
                    sideOutObjects[count].criticalFlowType = RiverSimulationProfile.CriticalFlowType.SubCriticalFlow;
                }
                else
                {
                    sideInObjects[count].criticalFlowType = RiverSimulationProfile.CriticalFlowType.SubCriticalFlow;
                }
            }
        }
    }
}

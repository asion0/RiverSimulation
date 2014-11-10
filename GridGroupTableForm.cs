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
    public partial class GridGroupTableForm : Form
    {
        public GridGroupTableForm()
        {
            InitializeComponent();
        }
        private string objectName;
        public void SetFormMode(string title, string name)
        {
            this.Text = title;
            objectName = name;
        }

        private int[] groupColors = null;
        private Color[] colorTable = null;
        private List<Point>[] pts = null;
        private int selIndex = -1;
        public void SetGroupColors(int[] gc)
        {
            groupColors = gc;
        }

        public void SetColorTable(Color[] ct)
        {
            colorTable = ct;
        }

        public void SetGridData(List<Point>[] p, int i)
        {
            pts = p;
            selIndex = i;
        }

        private void GridGroupTableForm_Load(object sender, EventArgs e)
        {
            InitialDataGrid();
            FillDataGrid();
        }

        private void InitialDataGrid()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
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
                    g.Columns[i].Width = 48;
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
        }

        private Color selectedColor = Color.Blue;
        private Color alertColor = Color.Red;
        private void FillDataGrid(List<Point> newPl = null, bool alert = false)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            var rg = p.inputGrid;

            Point pt = new Point();
            for (int i = 0; i < rg.GetI; ++i)
            {
                for (int j = 0; j < rg.GetJ; ++j)
                {
                    pt.X = i;
                    pt.Y = j;
                    int grpId = GroupGridUtility.WhichGroup(pts, pt, newPl, (newPl==null) ? -1 : selIndex);
                    Color cr;
                    if (grpId == -1)
                    {
                        cr = Color.White;
                        dataGv[j, i].Value = "";
                    }
                    else if (grpId >= 0 && grpId != selIndex)
                    {
                        cr = colorTable[(groupColors[grpId] > 0) ? groupColors[grpId] % colorTable.Length : 0];
                        dataGv[j, i].Value = objectName + (grpId + 1).ToString();
                    }
                    else
                    {
                        cr = (alert) ? alertColor : selectedColor;
                        dataGv[j, i].Value = objectName + (grpId + 1).ToString();
                    }
                    dataGv[j, i].Style.BackColor = cr;
                }
            }
        }

        private void dataGv_SelectionChanged(object sender, EventArgs e)
        {
            var selectedCellCount = dataGv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount <= 0)
                return;

            List<Point> pl = new List<Point>();
            for (int i = 0; i < selectedCellCount; ++i)
            {
                pl.Add(new Point(dataGv.SelectedCells[i].RowIndex, dataGv.SelectedCells[i].ColumnIndex));
            }
            addBtn.Enabled = GroupGridUtility.IsAllInEmpty(pts, pl);
            removeBtn.Enabled = (pts[selIndex] == null) ? false :GroupGridUtility.IsAllInclude(pts[selIndex], pl);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            var selectedCellCount = dataGv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount <= 0)
                return;

            List<Point> pl = new List<Point>();
            for (int i = 0; i < selectedCellCount; ++i)
            {
                pl.Add(new Point(dataGv.SelectedCells[i].RowIndex, dataGv.SelectedCells[i].ColumnIndex));
            }


            List<Point> plSelected = null;
            if(null != pts[selIndex])
            {
                plSelected = new List<Point>(pts[selIndex]);
                GroupGridUtility.MergePoints(ref plSelected, pl);
            }
            else
            {
                plSelected = new List<Point>(pl);
            }

            if (!GroupGridUtility.IsContinuous(plSelected))
            {
                FillDataGrid(plSelected, true);
                MessageBox.Show("新增後不是連續區域，請重新選取！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FillDataGrid();
                return;
            }
            pts[selIndex] = new List<Point>(plSelected);
            FillDataGrid();
            dataGv.ClearSelection();
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            var selectedCellCount = dataGv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount <= 0)
                return;

            List<Point> pl = new List<Point>();
            for (int i = 0; i < selectedCellCount; ++i)
            {
                pl.Add(new Point(dataGv.SelectedCells[i].RowIndex, dataGv.SelectedCells[i].ColumnIndex));
            }

            List<Point> plSelected = new List<Point>(pts[selIndex]);
            GroupGridUtility.RemovePoints(ref plSelected, pl);

            if (!GroupGridUtility.IsContinuous(plSelected))
            {
                FillDataGrid(plSelected, true);
                MessageBox.Show("刪減後不是連續區域，請重新選取！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FillDataGrid();
                return;
            }
            pts[selIndex] = new List<Point>(plSelected);
            FillDataGrid();
            dataGv.ClearSelection();
        }
    }
}

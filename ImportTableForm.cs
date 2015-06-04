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
    public partial class ImportTableForm : Form
    {
        public ImportTableForm()
        {
            InitializeComponent();
        }

        public RiverGrid gridData = null;

        bool hideGenerate = false;
        int colCount = 0;
        int rowCount = 0;
        public void SetFormMode(bool onlyTable, int colCount, int rowCount, RiverGrid data = null)
        {
            hideGenerate = onlyTable;
            this.colCount = colCount;
            this.rowCount = rowCount;

            if(data != null)
            {
                gridData = new RiverGrid(data);
            }
            //CreateData(null);
        }

        private void ImportTableForm_Load(object sender, EventArgs e)
        {
            if(hideGenerate)
            {
                yGridNum.Text = gridData.GetI.ToString();
                xGridNum.Text = gridData.GetJ.ToString();
                yGridNum.Enabled = false;
                xGridNum.Enabled = false;
                generateGridBtn.Enabled = false;

                tabControl.Enabled = true;

                DataGridViewUtility.InitializeDataGridView(dataGridViewX, colCount, rowCount, 96);
                DataGridViewUtility.InitializeDataGridView(dataGridViewY, colCount, rowCount, 96);
                DataGridViewUtility.InitializeDataGridView(dataGridViewZ, colCount, rowCount, 96);

                foreach (DataGridViewColumn column in dataGridViewX.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                foreach (DataGridViewColumn column in dataGridViewY.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                foreach (DataGridViewColumn column in dataGridViewZ.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                FillDataGridView();

                RiverSimulationProfile p = RiverSimulationProfile.profile;

                if (null != p.tBarSets)
                {
                    foreach (List<Point> pl in p.tBarSets)
                    {
                        if(pl == null)
                        { 
                            continue;
                        }
                        foreach (Point pt in pl)
                        {
                            dataGridViewZ[pt.Y, pt.X].ReadOnly = true;
                            dataGridViewZ[pt.Y, pt.X].Value = "99999999";
                            dataGridViewZ[pt.Y, pt.X].Style.BackColor = Color.LightGray;
                        }
                    }
                }

                if (null != p.bridgePierSets)
                {
                    foreach (List<Point> pl in p.bridgePierSets)
                    {
                        if (pl == null)
                        {
                            continue;
                        } 
                        foreach (Point pt in pl)
                        {
                            dataGridViewZ[pt.Y, pt.X].ReadOnly = true;
                            dataGridViewZ[pt.Y, pt.X].Value = "99999999";
                            dataGridViewZ[pt.Y, pt.X].Style.BackColor = Color.LightGray;
                        }
                    }
                }

                if (null != p.groundsillWorkSets)
                {
                    foreach (List<Point> pl in p.groundsillWorkSets)
                    {
                        if (pl == null)
                        {
                            continue;
                        } 
                        foreach (Point pt in pl)
                        {
                            dataGridViewZ[pt.Y, pt.X].ReadOnly = true;
                            dataGridViewZ[pt.Y, pt.X].Style.BackColor = Color.LightGray;
                        }
                    }
                }

                if (null != p.sedimentationWeirSets)
                {
                    foreach (List<Point> pl in p.sedimentationWeirSets)
                    {
                        foreach (Point pt in pl)
                        {
                            dataGridViewZ[pt.Y, pt.X].ReadOnly = true;
                            dataGridViewZ[pt.Y, pt.X].Style.BackColor = Color.LightGray;
                        }
                    }
                }
                //p.tBarSets, p.bridgePierSets, p.groundsillWorkSets, p.sedimentationWeirSets
            }
        }

        private void FillDataGridView()
        {
            for (int i = 0; i < rowCount; ++i)
            {
                for (int j = 0; j < colCount; ++j)
                {
                    dataGridViewX[j, i].Value = gridData.inputCoor[i, j].x.ToString();
                    dataGridViewY[j, i].Value = gridData.inputCoor[i, j].y.ToString();
                    dataGridViewZ[j, i].Value = gridData.inputCoor[i, j].z.ToString();
                }
            }
        }

        private bool DoConvert()
        {
            bool isSuccess = ConvertTableData();
            if (!isSuccess)
            {
                MessageBox.Show("輸入資料格式錯誤！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return isSuccess;
        }

        private bool ConvertTableData()
        {
            if(gridData == null)
            {
                try
                {
                    rowCount = Convert.ToInt32(yGridNum.Text); 
                    colCount = Convert.ToInt32(xGridNum.Text);
                }
                catch
                {
                    rowCount = 0;
                    colCount = 0;
                    return false;
                }
                gridData = new RiverGrid();
            }
             return gridData.ReadInputGridData(dataGridViewX, dataGridViewY, dataGridViewZ, rowCount, colCount);
        }

        private void generateGridBtn_Click(object sender, EventArgs e)
        {
            int xNum =0, yNum = 0;
            try
            {
                xNum = Convert.ToInt32(xGridNum.Text);
                yNum = Convert.ToInt32(yGridNum.Text);
            }
            catch
            {
            }

            if (xNum <= 0 && yNum <= 0)
            {
                MessageBox.Show("請輸入正確的數字", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            tabControl.Enabled = true;
            DataGridViewUtility.InitializeDataGridView(dataGridViewX, xNum, yNum, 96);
            DataGridViewUtility.InitializeDataGridView(dataGridViewY, xNum, yNum, 96);
            DataGridViewUtility.InitializeDataGridView(dataGridViewZ, xNum, yNum, 96);

            foreach (DataGridViewColumn column in dataGridViewX.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridViewY.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridViewZ.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private DataGridView GetCurrentDataGridView()
        {
            if(0 == tabControl.SelectedIndex)
            {
                return dataGridViewX;
            }
            else if (1 == tabControl.SelectedIndex)
            {
                return dataGridViewY;
            }
            else if (2 == tabControl.SelectedIndex)
            {
                return dataGridViewZ;
            }
            return null;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataGridViewUtility.PasteFromeExcel(GetCurrentDataGridView());
        }
        
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.CopyToClipboard(GetCurrentDataGridView());
        }

        private void valueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUtility.FillSelectedValue(GetCurrentDataGridView());
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (DoConvert())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void dataGridViewY_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

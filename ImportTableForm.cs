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


        RiverGrid gridData = null;

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

                DataGridViewUtility.InitializeDataGridView(dataGridViewX, colCount, rowCount, 96);
                DataGridViewUtility.InitializeDataGridView(dataGridViewY, colCount, rowCount, 96);
                DataGridViewUtility.InitializeDataGridView(dataGridViewZ, colCount, rowCount, 96);
                tabControl.Enabled = true;

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

        /*
        private void InitializeDataGridView(DataGridView g, int xNum, int yNum)
        {
            int columnCount = xNum;
            int rowCount = yNum;
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
        */

        private void GridNum_TextChanged(object sender, EventArgs e)
        {

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

            DataGridViewUtility.InitializeDataGridView(dataGridViewX, xNum, yNum);
            DataGridViewUtility.InitializeDataGridView(dataGridViewY, xNum, yNum);
            DataGridViewUtility.InitializeDataGridView(dataGridViewZ, xNum, yNum);
            tabControl.Enabled = true;
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
    }
}

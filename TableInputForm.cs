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

        bool hideSingle = false;
        int columnCount = 26;
        int rowCount = 50;
        string title;
        public void SetFormMode(string title, bool onlyTable, int col, int row)
        {
            hideSingle = onlyTable;
            columnCount = col;
            rowCount = row;
            this.title = title;
        }

        private void InitializeDataGridView()
        {
            // Create an unbound DataGridView by declaring a column count.
            dataGridView.ColumnCount = columnCount;
            dataGridView.ColumnHeadersVisible = true;
            
            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            //columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            string[] row = new string[columnCount];
           // Set the column header names.
            char c = 'A';
            for (int i = 0; i < columnCount; ++i)
            {
                dataGridView.Columns[i].Name = c.ToString();
                dataGridView.Columns[i].Width = 48;
                row[i] = "1";
                c++;
            }
            dataGridView.RowHeadersWidth = 64;
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView.Rows.Add(row);
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
          //  dataGridView.Rows[rowCount - 1].HeaderCell.Value = rowCount.ToString();
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
    }
}

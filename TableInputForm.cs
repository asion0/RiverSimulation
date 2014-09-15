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

            if (type == Type.UpVerticalDistribution)
            {
                dataGridView.Columns[0].Name = "位置";
                dataGridView.Columns[1].Name = "比例";
            }

            dataGridView.RowHeadersWidth = 64;
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView.Rows.Add(row);
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
          //  dataGridView.Rows[rowCount - 1].HeaderCell.Value = rowCount.ToString();
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

        private void TableInputForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("超過合理範圍：0.009~0.125", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }
}

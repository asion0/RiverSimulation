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
    public partial class ImportTableForm : Form
    {
        public ImportTableForm()
        {
            InitializeComponent();
        }

        private void ImportTableForm_Load(object sender, EventArgs e)
        {
            InitializeDataGridView(dataGridViewX);
            InitializeDataGridView(dataGridViewY);
            InitializeDataGridView(dataGridViewZ);
        }

        private void InitializeDataGridView(DataGridView g)
        {
            int columnCount = 26;
            int rowCount = 50;
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
            char c = 'A';
            for (int i = 0; i < columnCount; ++i)
            {
                g.Columns[i].Name = c.ToString();
                g.Columns[i].Width = 48;
                //row[i] = "1";
                c++;
            }
            g.RowHeadersWidth = 64;
            for (int i = 0; i < rowCount; i++)
            {
                g.Rows.Add(row);
                g.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

    }
}

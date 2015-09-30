using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DGVPaste
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			oData = new clsData();
		}

				private clsData oData { get; set; }
				private BindingSource oBS = new BindingSource();

		private void Form1_Load(object sender, EventArgs e)
		{

		}
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}



		private void btnGetData_Click(object sender, EventArgs e)
		{

			DataTable oTable = oData.GetData();
			oBS.DataSource = oTable;
			dgData.DataSource = oBS;

			//readonly the columns that cannot be edited
			dgData.Columns[0].ReadOnly = true;
			dgData.Columns[1].ReadOnly = true;
			dgData.Columns[2].ReadOnly = true;

		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CopyClipboard();
		}

		private void CopyClipboard()
		{
			DataObject d = dgData.GetClipboardContent();
			Clipboard.SetDataObject(d);
		}

		private void pasteCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PasteClipboard();
		}

		/// <summary>
		/// This will be moved to the util class so it can service any paste into a DGV
		/// </summary>
		private void PasteClipboard()
		{
			try
			{
				string s = Clipboard.GetText();
				string[] lines = s.Split('\n');
				int iFail = 0, iRow = dgData.CurrentCell.RowIndex;
				int iCol = dgData.CurrentCell.ColumnIndex;
				DataGridViewCell oCell;
				foreach (string line in lines)
				{
					if (iRow < dgData.RowCount && line.Length > 0)
					{
						string[] sCells = line.Split('\t');
						for (int i = 0; i < sCells.GetLength(0); ++i)
						{
							if (iCol + i < this.dgData.ColumnCount)
							{
								oCell = dgData[iCol + i, iRow];
								if (!oCell.ReadOnly)
								{
									if (oCell.Value.ToString() != sCells[i])
									{
										oCell.Value = Convert.ChangeType(sCells[i], oCell.ValueType);
										oCell.Style.BackColor = Color.Tomato;
									}
									else
										iFail++;//only traps a fail if the data has changed and you are pasting into a read only cell
								}
							}
							else
							{ break; }
						}
						iRow++;
					}
					else
					{ break; }
					if (iFail > 0)
						MessageBox.Show(string.Format("{0} updates failed due to read only column setting", iFail));
				}
			}
			catch (FormatException )
			{
				MessageBox.Show("The data you pasted is in the wrong format for the cell");
				return;
			}
		}

		private void dgData_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Control && e.KeyCode == Keys.Delete) || (e.Shift && e.KeyCode == Keys.Delete))
			{
				CopyClipboard();
			}
			if ((e.Control && e.KeyCode == Keys.Insert) || (e.Shift && e.KeyCode == Keys.Insert))
			{
				PasteClipboard();
			}

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			DataTable oTable = (DataTable)oBS.DataSource;
			DataView oDV = new DataView(oTable, "", "", DataViewRowState.ModifiedCurrent);
			foreach (DataRowView oRow in oDV)
			{
				//write each row to the database as all of them have changed
			}


        }
	}
}
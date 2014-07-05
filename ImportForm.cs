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
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {

        }

        private void inputGridBtn_Click(object sender, EventArgs e)
        {
            ImportTableForm form = new ImportTableForm();

            if (DialogResult.OK == form.ShowDialog())
            {

            }

        }

        private void inputFileRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            inputFileBtn.Enabled = chk;
            inputFilePath.Enabled = chk;
        }

        private void inputGridRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            inputGridBtn.Enabled = chk;
        }

        private void inputFileBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = inputFileDlg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                inputFilePath.Text = inputFileDlg.FileName;
            }
        }

        private void noBgRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void useGoogleBgRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void selectBgRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            selectBgBtn.Enabled = chk;

        }

        private void selectBgBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = selectBgDlg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                selectBgFilePath.Text = selectBgDlg.FileName;
            }
        }
    }
}

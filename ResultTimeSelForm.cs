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
    public partial class ResultTimeSelForm : Form
    {
        public ResultTimeSelForm()
        {
            InitializeComponent();
        }

        private object timeList = null;
        private string desc = "";
        private ResultTimeType type = ResultTimeType.SingleSelect;
        //private List<double> timeSel = null;
        private List<int> timeSelIdx = null;

        public enum ResultTimeType
        {
            SingleSelect, 
            MultiSelect,
        }

        public void SetFormMode(string desc, ResultTimeType type, object time)
        {
            this.desc = desc;
            this.timeList = time;
            this.type = type;
        }

        //public List<double> GetTimeSelect()
        public List<int> GetTimeSelect()
        {
            //return timeSel;
            return timeSelIdx;
        }

        private void ResultTimeSelForm_Load(object sender, EventArgs e)
        {
            timeLsb.SelectionMode = (type == ResultTimeType.SingleSelect) ? SelectionMode.One : SelectionMode.MultiExtended;
            if (type == ResultTimeType.SingleSelect)
            {
                selectAllChk.Enabled = false;
                clearBtn.Enabled = false;
            }

            //timeSel = new List<double>();
            timeSelIdx = new List<int>();
            if (timeLsb != null)
            {
                timeLsb.DataSource = timeList;
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            object o = timeLsb.SelectedItems;
            int start = -1, end = -1;
            timeSelIdx.Clear();
            for (int i = 0; i < timeLsb.Items.Count; ++i)
            {
                if(timeLsb.GetSelected(i))
                {
                    timeSelIdx.Add(i);
                    if(start == -1)
                        start = i;
                }
                else 
                {
                    if (end == -1 && start != -1)
                        end = i;
                }
            }
            if (end == -1 && start != -1)
                end = timeLsb.Items.Count;

            //if (timeSel.Count == 0)
            if (timeSelIdx.Count == 0)
            {
                //e.Cancel = true;
                MessageBox.Show("尚未選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (end - start != timeSelIdx.Count)
            {
                //e.Cancel = true;
                MessageBox.Show("請選取連續的時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = DialogResult.OK;
            //MessageBox.Show(timeSel.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        private void selectAllChk_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as CheckBox).Checked)
            {
                timeLsb.BeginUpdate();
                for (int i = 0; i < timeLsb.Items.Count; i++)
                {
                    timeLsb.SelectedIndex = i;
                }
                timeLsb.EndUpdate();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            timeLsb.ClearSelected();
        }
    }
}

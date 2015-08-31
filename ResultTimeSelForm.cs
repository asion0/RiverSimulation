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
        private List<double> timeSel = null;

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

        public List<double> GetTimeSelect()
        {
            return timeSel;
        }

        private void ResultTimeSelForm_Load(object sender, EventArgs e)
        {
            //descLbl.Text = desc;
            if(type == ResultTimeType.SingleSelect)
            {
                timeSel = new List<double>();
                if (timeList != null)
                {
                    timeCmb.DataSource = timeList;
                }
                if (timeLsb != null)
                {
                    timeLsb.DataSource = timeList;
                }
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            object o = timeLsb.SelectedItems;
            for (int i = 0; i < timeLsb.Items.Count; ++i)
            {
                if(timeLsb.GetSelected(i))
                {
                    timeSel.Add((timeList as List<double>)[i]);
                }
            }
            if (timeSel.Count == 0)
            {
                //e.Cancel = true;
                MessageBox.Show("尚未選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = DialogResult.OK;
            MessageBox.Show(timeSel.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
            
            //if (timeCmb.SelectedIndex >= 0)
            //    {
            //        timeSel.Add((timeList as List<double>)[timeCmb.SelectedIndex]);
            //    }
            //    else
            //    {
            //        //e.Cancel = true;
            //        MessageBox.Show("尚未選取時間！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }
            //DialogResult = DialogResult.OK;
        }
    }
}

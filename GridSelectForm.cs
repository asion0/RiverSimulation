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
    public partial class GridSelectForm : Form
    {
        public GridSelectForm()
        {
            InitializeComponent();
        }
        public void SetFormMode(string title, int numbers, string name)
        {
            objectNum = numbers;
            this.title = title;
            objectName = name;
        }

        private string title;
        private int objectNum;
        private string objectName;
        private void GridSelectForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            for (int i = 0; i < objectNum; ++i)
            {
                listBox.Items.Add(objectName + (i + 1).ToString());
            }

            ControllerUtility.InitialGridPictureBoxByProfile(ref mapPicBox, RiverSimulationProfile.profile);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (sender as ListBox).SelectedIndex;
        }
    }
}

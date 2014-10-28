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

        public enum SelectType
        {
            DryBed,
            ImmersedBoundary,

        };

        private SelectType st = SelectType.DryBed;
        public void SetFormMode(string title, int numbers, string name, SelectType t)
        {
            objectNum = numbers;
            this.title = title;
            objectName = name;
            st = t;
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

            listBox.SelectedIndex = 0;
            ControllerUtility.InitialGridPictureBoxByProfile(ref mapPicBox, RiverSimulationProfile.profile);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = (sender as ListBox).SelectedIndex;
            mapPicBox.SelectGroup = true;
            if (st == SelectType.DryBed)
            {
                mapPicBox.SetSelectedGrid(p.dryBedPts[index]);
            }
            else if (st == SelectType.ImmersedBoundary)
            {
                mapPicBox.SetSelectedGrid(p.immersedBoundaryPts[index]);
            }

        }

        private void mapPicBox_SelectedGroupChangedEvent(Point[] pts)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;
            if (st == SelectType.DryBed)
            {
                p.dryBedPts[index] = (Point[])pts.Clone();
            }
            else if (st == SelectType.ImmersedBoundary)
            {
                p.immersedBoundaryPts[index] = (Point[])pts.Clone();
            }
        }
    }
}

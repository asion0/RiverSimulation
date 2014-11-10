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

        private void SetPicBoxGrid(int index, bool alert)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            mapPicBox.SelectGroup = true;
            if (st == SelectType.DryBed)
            {
                mapPicBox.SetSelectedGrid(p.DryBedPts, index, alert);
            }
            else if (st == SelectType.ImmersedBoundary)
            {
                mapPicBox.SetSelectedGrid(p.ImmersedBoundaryPts, index, alert);
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPicBoxGrid((sender as ListBox).SelectedIndex, false);
        }
        /*
        private bool IsContinuous(List<Point> pts)
        {
            Point[] workQueue = new Point[pts.Count];
            workQueue[0] = pts[0];
            for (int i = 1; i < workQueue.Length; ++i )
            {
                workQueue[i].X = -1;
                workQueue[i].Y = -1;
            }
            int ptr = 1;
            for (int i = 0; i < workQueue.Length; ++i)
            {
                Point p0 = workQueue[i];
                if (-1 == p0.X)
                {
                    break;
                }

                Point p1 = new Point(p0.X, p0.Y - 1);
                Point p2 = new Point(p0.X, p0.Y + 1);
                Point p3 = new Point(p0.X - 1, p0.Y);
                Point p4 = new Point(p0.X + 1, p0.Y);

                foreach (Point p in pts)
                {
                    if (p1 == p)
                    {
                        if (!workQueue.Contains(p1))
                        {
                            workQueue[ptr++] = p1;
                        }
                    }
                    if (p2 == p)
                    {
                        if (!workQueue.Contains(p2))
                        {
                            workQueue[ptr++] = p2;
                        }
                    }
                    if (p3 == p)
                    {
                        if (!workQueue.Contains(p3))
                        {
                            workQueue[ptr++] = p3;
                        }
                    }
                    if (p4 == p)
                    {
                        if (!workQueue.Contains(p4))
                        {
                            workQueue[ptr++] = p4;
                        }
                    }
                }
            }
            return (ptr == pts.Count);
        }

        private bool IsOverlapping(List<Point> pts, int pass)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            for(int i = 0; i < p.DryBedPts.Length; ++i)
            {
                if (i == pass || p.DryBedPts[i] == null)
                    continue;
                foreach(Point pt in p.DryBedPts[i])
                {
                    foreach(Point pp in pts)
                    {
                        if (pt == pp)
                            return true;
                    }
                }
            }
            return false;
        }
        */

        private void UpdateSelectedGroup(List<Point> pts, bool alert = false)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;

            if (st == SelectType.DryBed)
            {
                p.DryBedPts[index] = (pts==null) ? null : new List<Point>(pts);
                SetPicBoxGrid(listBox.SelectedIndex, alert);
            }
            else if (st == SelectType.ImmersedBoundary)
            {
                p.ImmersedBoundaryPts[index] = (pts == null) ? null : new List<Point>(pts);
                SetPicBoxGrid(listBox.SelectedIndex, alert);
            }
        }

        private List<Point> GetSelectedGroup()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;

            if (st == SelectType.DryBed)
            {
                return p.DryBedPts[index];
            }
            else if (st == SelectType.ImmersedBoundary)
            {
                return p.ImmersedBoundaryPts[index];
            }
            return null;
        }

        private void mapPicBox_SelectedGroupChangedEvent(List<Point> pl)
        {
            int index = listBox.SelectedIndex;

            //檢查連續
            if (!GroupGridUtility.IsContinuous(pl))
            {
                UpdateSelectedGroup(pl, true);
                MessageBox.Show("請圈選連續區域！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UpdateSelectedGroup(null);
                return;
            }
            //檢查重疊
            var rg = (st == SelectType.DryBed) ? RiverSimulationProfile.profile.DryBedPts : RiverSimulationProfile.profile.ImmersedBoundaryPts;
            if (GroupGridUtility.IsOverlapping(rg, pl, index))
            {
                UpdateSelectedGroup(pl, true);
                if (DialogResult.Yes == MessageBox.Show("圈選到重覆區域，是否刪減重複範圍(選「否」將放棄此次圈選)", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    GroupGridUtility.RemoveOverlapping(ref pl, rg, index);
                    if (!GroupGridUtility.IsContinuous(pl))
                    {
                        UpdateSelectedGroup(pl, true);
                        MessageBox.Show("刪減後不是連續區域，請重新選取！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        UpdateSelectedGroup(null);
                        return;
                    }
                }
                else
                {
                    UpdateSelectedGroup(null);
                    return;
                }
            }

            UpdateSelectedGroup(pl);

        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            var p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;

            GridGroupTableForm form = new GridGroupTableForm();
            form.SetGroupColors(mapPicBox.GetGroupColors());
            form.SetColorTable(mapPicBox.GetColorTable());
            if (st == SelectType.DryBed)
            {
                form.SetFormMode("編輯" + objectName + (listBox.SelectedIndex + 1).ToString(), objectName);
                form.SetGridData(p.DryBedPts, index);
            }
            else if (st == SelectType.ImmersedBoundary)
            {
                form.SetFormMode("編輯" + objectName + (listBox.SelectedIndex + 1).ToString(), objectName);
                form.SetGridData(p.ImmersedBoundaryPts, index);
            }

            if (DialogResult.OK == form.ShowDialog())
            {

            }
            SetPicBoxGrid(listBox.SelectedIndex, false);
        }
    }
}

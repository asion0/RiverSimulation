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

        private void SetPicBoxGrid(int index)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            mapPicBox.SelectGroup = true;
            if (st == SelectType.DryBed)
            {
                mapPicBox.SetSelectedGrid(p.DryBedPts, index);
            }
            else if (st == SelectType.ImmersedBoundary)
            {
                mapPicBox.SetSelectedGrid(p.ImmersedBoundaryPts, index);
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPicBoxGrid((sender as ListBox).SelectedIndex);
            /*
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = (sender as ListBox).SelectedIndex;
            mapPicBox.SelectGroup = true;
            if (st == SelectType.DryBed)
            {
                mapPicBox.SetSelectedGrid(p.DryBedPts, index);
            }
            else if (st == SelectType.ImmersedBoundary)
            {
                mapPicBox.SetSelectedGrid(p.ImmersedBoundaryPts, index);
            }
             * */

        }

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

        private bool RemoveOverlapping(ref List<Point> pts, int pass)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            bool isRemove = false;
            //List<Point> ptsResult = new List<Point>(pts);
            for (int i = 0; i < p.DryBedPts.Length; ++i)
            {   //尋訪全部的乾床群組
                if (i == pass || p.DryBedPts[i] == null)
                    continue;
                foreach (Point pt in p.DryBedPts[i])
                {   //被尋訪的乾床群組內的每個點
                    foreach (Point pp in pts)
                    {   //尋訪此次選取的群組
                        if (pt == pp)
                        {
                            pts.Remove(pp);
                            isRemove = true;
                            break;
                        }
                    }
                }
            }
            return isRemove;
        }

        private void mapPicBox_SelectedGroupChangedEvent(List<Point> pts)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;
            //檢查連續
            if (!IsContinuous(pts))
            {
                MessageBox.Show("請圈選連續區域！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mapPicBox.Refresh();
                return;
            }
            //檢查重疊
            if (IsOverlapping(pts, index))
            {
                if (DialogResult.Yes == MessageBox.Show("圈選到重覆區域，是否刪減重複範圍(選「否」將放棄此次圈選)", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    bool b = RemoveOverlapping(ref pts, index);

                }
                else
                {
                    mapPicBox.Refresh();
                    return;
                }
            }

            if (st == SelectType.DryBed)
            {
                p.DryBedPts[index] = new List<Point>(pts);
                SetPicBoxGrid(listBox.SelectedIndex);
            }
            else if (st == SelectType.ImmersedBoundary)
            {
                p.ImmersedBoundaryPts[index] = new List<Point>(pts);
                SetPicBoxGrid(listBox.SelectedIndex);
            }
        }
    }
}

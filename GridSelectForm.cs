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
            StructureSet,
        };

        public enum StructureType
        {
            TBar,
            BridgePier,
            GroundSillWork,
            SedimentationWeir,
            StructureTypeSize,
        };
        const int StructureTypeNumber = (int)StructureType.StructureTypeSize;

        private SelectType st = SelectType.StructureSet;

        private string title;
        private string[] structureName = new string[StructureTypeNumber];
        private int[] structureNum = new int[StructureTypeNumber];
        private StructureType[] typeIndex = null;
        public void SetFormMode(string title, int num1, string name1, int num2, string name2, int num3, string name3, int num4, string name4, SelectType t = SelectType.StructureSet)
        {
            structureName[0] = name1;
            structureName[1] = name2;
            structureName[2] = name3;
            structureName[3] = name4;
            structureNum[0] = num1;
            structureNum[1] = num2;
            structureNum[2] = num3;
            structureNum[3] = num4;
            typeIndex = new StructureType[num1 + num2 + num3 + num4];
            this.title = title;
            this.st = t;
        }

        private void GridSelectForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            int typeCount = 0;
            for (int n = 0; n < StructureTypeNumber; ++n)
            {
                for (int i = 0; i < structureNum[n]; ++i)
                {
                    listBox.Items.Add(structureName[n] + (i + 1).ToString());
                    typeIndex[typeCount++] = (StructureType)n;
                }
            }
            listBox.SelectedIndex = 0;
            ControllerUtility.InitialGridPictureBoxByProfile(ref mapPicBox, RiverSimulationProfile.profile);
        }

        private void CalcTypeCount(int index, ref int type, ref int count)
        {
            if (index >= typeIndex.Length)
                return;
            
            StructureType lastType = StructureType.StructureTypeSize;
            int c = 0;

            for (int i = 0; i <= index; ++i)
            {
                if(typeIndex[i] != lastType)
                {
                    c = 0;
                    lastType = typeIndex[i];
                }
                else
                {
                    ++c;
                }

            }
            type = (lastType==StructureType.StructureTypeSize) ? -1 : (int)lastType;
            count = c;
        }

        private void SetPicBoxGrid(int index, bool alert)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            mapPicBox.SelectGroup = true;

            int type = -1, count = 0;
            CalcTypeCount(index, ref type, ref count);
            mapPicBox.SetSelectedGrid(p.TBarPts, p.BridgePierPts, p.GroundsillWorkPts, p.SedimentationWeirPts, type, count, alert);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPicBoxGrid((sender as ListBox).SelectedIndex, false);
        }

        private void UpdateSelectedGroup(List<Point> pts, bool alert = false)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;

            int type = -1, count = 0;
            CalcTypeCount(index, ref type, ref count);

            switch(type)
            {
                case 0:
                    p.TBarPts[count] = (pts == null) ? null : new List<Point>(pts);
                    break;
                case 1:
                    p.BridgePierPts[count] = (pts == null) ? null : new List<Point>(pts);
                    break;
                case 2:
                    p.GroundsillWorkPts[count] = (pts == null) ? null : new List<Point>(pts);
                    break;
                case 3:
                    p.SedimentationWeirPts[count] = (pts == null) ? null : new List<Point>(pts);
                    break;
                default:
                    break;
            }
            mapPicBox.SetSelectedGrid(p.TBarPts, p.BridgePierPts, p.GroundsillWorkPts, p.SedimentationWeirPts, type, count, alert);
        }

        //private List<Point> GetSelectedGroup()
        //{
        //    RiverSimulationProfile p = RiverSimulationProfile.profile;
        //    int index = listBox.SelectedIndex;

        //    if (st == SelectType.StructureSet)
        //    {
        //        return p.DryBedPts[index];
        //    }
        //    return null;
        //}
        private bool CheckOverlapping(List<Point> pl, List<Point>[] rg, int count)
        {
            if (rg == null)
            {   
                return true;
            }

            if (GroupGridUtility.IsOverlapping(rg, pl, count))
            {
                UpdateSelectedGroup(pl, true);
                if (DialogResult.Yes == MessageBox.Show("圈選到重覆區域，是否刪減重複範圍(選「否」將放棄此次圈選)", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    GroupGridUtility.RemoveOverlapping(ref pl, rg, count);
                    if (!GroupGridUtility.IsContinuous(pl))
                    {
                        UpdateSelectedGroup(pl, true);
                        MessageBox.Show("刪減後不是連續區域，請重新選取！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        UpdateSelectedGroup(null);
                        return false;
                    }
                }
                else
                {
                    UpdateSelectedGroup(null);
                    return false;
                }
            }
            return true;
        }

        private void mapPicBox_SelectedGroupChangedEvent(List<Point> pl)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;
            int type = -1, count = 0;
            CalcTypeCount(index, ref type, ref count);

            //檢查連續
            if (!GroupGridUtility.IsContinuous(pl))
            {
                UpdateSelectedGroup(pl, true);
                MessageBox.Show("請圈選連續區域！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UpdateSelectedGroup(null);
                return;
            }

            //檢查重疊
            for (int n = 0; n < StructureTypeNumber; ++n)
            {
                bool overlapping = false;
                switch(n)
                {
                    case 0:
                        overlapping = CheckOverlapping(pl, p.TBarPts, (n == type) ? count : -1);
                        break;
                    case 1:
                        overlapping = CheckOverlapping(pl, p.BridgePierPts, (n == type) ? count : -1);
                        break;
                    case 2:
                        overlapping = CheckOverlapping(pl, p.GroundsillWorkPts, (n == type) ? count : -1);
                        break;
                    case 3:
                        overlapping = CheckOverlapping(pl, p.SedimentationWeirPts, (n == type) ? count : -1);
                        break;
                    default:
                        break;
                }
                if(!overlapping)
                {
                    return;
                }
            }
            UpdateSelectedGroup(pl);

            //if (GroupGridUtility.IsOverlapping(rg, pl, index))
            //{
            //    UpdateSelectedGroup(pl, true);
            //    if (DialogResult.Yes == MessageBox.Show("圈選到重覆區域，是否刪減重複範圍(選「否」將放棄此次圈選)", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            //    {
            //        GroupGridUtility.RemoveOverlapping(ref pl, rg, index);
            //        if (!GroupGridUtility.IsContinuous(pl))
            //        {
            //            UpdateSelectedGroup(pl, true);
            //            MessageBox.Show("刪減後不是連續區域，請重新選取！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            UpdateSelectedGroup(null);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        UpdateSelectedGroup(null);
            //        return;
            //    }
            //}

            //UpdateSelectedGroup(pl);

        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            //var p = RiverSimulationProfile.profile;
            //int index = listBox.SelectedIndex;
            
            //GridGroupTableForm form = new GridGroupTableForm();
            //form.SetGroupColors(mapPicBox.GetGroupColors());
            //form.SetColorTable(mapPicBox.GetColorTable());
            //form.SetSelectionItems(listBox);
            //if (st == SelectType.StructureSet)
            //{
            //    form.SetFormMode("編輯" + structureName[(int)typeIndex[listBox.SelectedIndex]] + (listBox.SelectedIndex + 1).ToString(), structureName[(int)typeIndex[listBox.SelectedIndex]]);
            //    form.SetGridData(p.DryBedPts, index);
            //}

            //if (DialogResult.OK == form.ShowDialog())
            //{

            //}
            //SetPicBoxGrid(listBox.SelectedIndex, false);
        }
    }
}

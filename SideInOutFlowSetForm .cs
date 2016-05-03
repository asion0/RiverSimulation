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
    public partial class SideInOutFlowSetForm : Form
    {
        public SideInOutFlowSetForm()
        {
            InitializeComponent();
        }

        //public enum SelectType
        //{
        //    StructureSet,
        //};
        //private SelectType st = SelectType.StructureSet;

        const int SideFlowNumber = (int)RiverSimulationProfile.SideFlowType.SideFlowSize;
        private string title;
        private string[] sideFlowName = new string[SideFlowNumber];
        private int[] sideFlowNum = new int[SideFlowNumber];
        private bool onlySelectMode = false;
        private RiverSimulationProfile.SideFlowType[] typeIndex = null;
        public void SetFormMode(string title, int num1, string name1, int num2, string name2)
        {
            this.title = title;
            if (num1 == -1 && num2 == -1)
            {
                onlySelectMode = true;
                sideFlowName[0] = "圈選";
                sideFlowName[1] = "";

                sideFlowNum[0] = 1;
                sideFlowNum[1] = 0;
            }
            else
            {
                sideFlowName[0] = name1;
                sideFlowName[1] = name2;

                sideFlowNum[0] = num1;
                sideFlowNum[1] = num2;

            }
            typeIndex = new RiverSimulationProfile.SideFlowType[sideFlowNum[0] + sideFlowNum[1]];
        }

        private void StructureSetForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            int typeCount = 0;

            if (onlySelectMode)
            {
                listBox.Visible = false;
                editBtn.Visible = false;
                listBox.Items.Add(sideFlowName[0]);
                typeIndex[0] = RiverSimulationProfile.SideFlowType.SideFlowSize;
                listBox.SelectedIndex = 0;
                ControllerUtility.InitialGridPictureBoxByProfile(ref mapPicBox, RiverSimulationProfile.profile);

                return;
            }

            for (int n = 0; n < SideFlowNumber; ++n)
            {
                for (int i = 0; i < sideFlowNum[n]; ++i)
                {
                    listBox.Items.Add(sideFlowName[n] + (i + 1).ToString());
                    typeIndex[typeCount++] = (RiverSimulationProfile.SideFlowType)n;
                }
            }
            listBox.SelectedIndex = 0;
             ControllerUtility.InitialGridPictureBoxByProfile(ref mapPicBox, RiverSimulationProfile.profile);
        }

        private void SetPicBoxGrid(int index, bool alert)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            mapPicBox.SelectGroup = true;

            if(onlySelectMode)
            {
                return;
            }

            int type = -1, count = 0;
            SideFlowtUtility.CalcTypeCount(index, ref type, ref count, typeIndex);
            mapPicBox.SetSelectedGrid(p.sideOutFlowSets, p.sideInFlowSets, null, null, type, count, alert);
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
            SideFlowtUtility.CalcTypeCount(index, ref type, ref count, typeIndex);

            p.UpdateSideFlowSet(pts, type, count);
            mapPicBox.SetSelectedGrid(p.sideOutFlowSets, p.sideInFlowSets, null, null, type, count, alert);
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

            if (SideFlowtUtility.IsOverlapping(rg, pl, count))
            {
                UpdateSelectedGroup(pl, true);
                if (DialogResult.Yes == MessageBox.Show("圈選到重覆區域，是否刪減重複範圍(選「否」將放棄此次圈選)", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    SideFlowtUtility.RemoveOverlapping(ref pl, rg, count);
                    if (!SideFlowtUtility.IsContinuous(pl))
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

        public List<Point> selectedPl = null;
        public string selectedValue = "";
        private void mapPicBox_SelectedGroupChangedEvent(List<Point> pl)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;
            if (!SideFlowtUtility.TrimToEdge(ref pl, p.inputGrid.GetI, p.inputGrid.GetJ))
            {
                UpdateSelectedGroup(pl, true);
                MessageBox.Show("請圈選邊界！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UpdateSelectedGroup(null);
                return;
            }

            if(onlySelectMode)
            {
                InputForm dlg = new InputForm();
                dlg.Text = "填入數值";
                dlg.desc.Text = "請輸入數值";
                dlg.inputTxt.Text = "";
                if (DialogResult.OK != dlg.ShowDialog())
                {
                    return;
                }
                selectedPl = new List<Point>(pl);
                selectedValue = dlg.inputTxt.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }


            int type = -1, count = 0;
            SideFlowtUtility.CalcTypeCount(index, ref type, ref count, typeIndex);

            //檢查連續
            if (!SideFlowtUtility.IsContinuous(pl))
            {
                UpdateSelectedGroup(pl, true);
                MessageBox.Show("請圈選連續區域！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UpdateSelectedGroup(null);
                return;
            }

            //檢查重疊
            for (int n = 0; n < SideFlowNumber; ++n)
            {
                bool overlapping = false;
                switch(n)
                {
                    case 0:
                        overlapping = CheckOverlapping(pl, p.sideOutFlowSets, (n == type) ? count : -1);
                        break;
                    case 1:
                        overlapping = CheckOverlapping(pl, p.sideInFlowSets, (n == type) ? count : -1);
                        break;
                    default:
                        break;
                }
                if(!overlapping)
                {
                    return;
                }
            }

            //最後確認 [20141121]更新客製化需求 回報問題 新增規格
            if (DialogResult.OK == MessageBox.Show("請確認以此次圈選範圍取代原先資料。", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.None))
            {
                UpdateSelectedGroup(pl);
            }
            //StructureSetUtility.EditBottomElevation(p, "編輯" + structureName[type] + (1 + count).ToString() + "高程", type, count);
            //if (StructureSetUtility.IsOverlapping(rg, pl, index))
            //{
            //    UpdateSelectedGroup(pl, true);
            //    if (DialogResult.Yes == MessageBox.Show("圈選到重覆區域，是否刪減重複範圍(選「否」將放棄此次圈選)", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            //    {
            //        StructureSetUtility.RemoveOverlapping(ref pl, rg, index);
            //        if (!StructureSetUtility.IsContinuous(pl))
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
            var p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;

//            StructureSetTableForm form = new StructureSetTableForm();
//            form.SetGroupColors(mapPicBox.GetGroupColors());
//            form.SetColorTable(mapPicBox.GetColorTable());
//            form.SetSelectionItems(listBox, structureName, structureNum, typeIndex);

//            form.SetFormMode("編輯結構物", "");
////            form.SetGridData(p.tBarSets, index);


//            if (DialogResult.OK == form.ShowDialog())
//            {

//            }
//            SetPicBoxGrid(listBox.SelectedIndex, false);
        }
    }
}

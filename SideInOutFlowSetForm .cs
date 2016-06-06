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

        //const int SideFlowNumber = (int)RiverSimulationProfile.SideFlowType.SideFlowSize;
        private string title;
        //private string[] sideFlowName = new string[SideFlowNumber];
        //private int[] sideFlowNum = new int[SideFlowNumber];
        public RiverSimulationProfile.SideFlowObject[] sideOutObjects = null;
        public RiverSimulationProfile.SideFlowObject[] sideInObjects = null;

        //private bool onlySelectMode = false;
        //private RiverSimulationProfile.SideFlowType[] typeIndex = null;
        public void SetFormMode(string title, RiverSimulationProfile.SideFlowObject[] outObjs, string inName, RiverSimulationProfile.SideFlowObject[] inObjs, string outName)
        {
            this.title = title;
            //從RiverSimulationProfile複製一份資料來編輯
            if (outObjs != null && outObjs.Length > 0)
            {
                sideOutObjects = new RiverSimulationProfile.SideFlowObject[outObjs.Length];
                for(int i = 0; i < outObjs.Length; ++i)
                {
                    if (outObjs[i] != null)
                    {
                        sideOutObjects[i] = outObjs[i].Clone();
                    }
                    else
                    {
                        sideOutObjects[i] = new RiverSimulationProfile.SideFlowObject(RiverSimulationProfile.SideFlowType.SideOutFlow);
                    }
                }
            }

            if (inObjs != null && inObjs.Length > 0)
            {
                sideInObjects = new RiverSimulationProfile.SideFlowObject[inObjs.Length];
                for (int i = 0; i < inObjs.Length; ++i)
                {
                    if (outObjs[i] != null)
                    {
                        sideInObjects[i] = inObjs[i].Clone();
                    }
                    else
                    {
                        sideInObjects[i] = new RiverSimulationProfile.SideFlowObject(RiverSimulationProfile.SideFlowType.SideInFlow);
                    }
                }
            }

            //for (int i = 0; i < sideOutObjects.Length; ++i)
            //{
            //    if (sideOutObjects[i] == null)
            //    {
            //        sideOutObjects[i] = new RiverSimulationProfile.SideFlowObject(RiverSimulationProfile.SideFlowType.SideOutFlow);
            //    }
            //}
            //for (int i = 0; i < sideInObjects.Length; ++i)
            //{
            //    if (sideInObjects[i] == null)
            //    {
            //        sideInObjects[i] = new RiverSimulationProfile.SideFlowObject(RiverSimulationProfile.SideFlowType.SideInFlow);
            //    }
            //}
        }

        private void StructureSetForm_Load(object sender, EventArgs e)
        {
            this.Text = title;
            //int typeCount = 0;

            for (int i = 0; i < sideOutObjects.Length; ++i)
            {
                listBox.Items.Add("側出流" + (i + 1).ToString());
            }
            for (int i = 0; i < sideInObjects.Length; ++i)
            {
                listBox.Items.Add("側入流" + (i + 1).ToString());
            }
            listBox.SelectedIndex = 0;
            ControllerUtility.InitialGridPictureBoxByProfile(ref mapPicBox, RiverSimulationProfile.profile);
        }

        private void SetPicBoxGrid(int index, bool alert)
        {
            //RiverSimulationProfile p = RiverSimulationProfile.profile;
            mapPicBox.SelectGroup = true;
            int type = (index < sideOutObjects.Length) ? 0 : 1, count = (index < sideOutObjects.Length) ? index : index - sideOutObjects.Length;
            mapPicBox.SetSelectedGrid(SideFlowtUtility.GetSideFlowSets(sideOutObjects), SideFlowtUtility.GetSideFlowSets(sideInObjects), null, null, type, count, alert);

            if (index < sideOutObjects.Length)
            {
                if (sideOutObjects[index].criticalFlowType == RiverSimulationProfile.CriticalFlowType.SubCriticalFlow)
                    subTypeRdo.Checked = true;
                else
                    superTypeRdo.Checked = true;
            }
            else
            {
                if (sideInObjects[index - sideOutObjects.Length].criticalFlowType == RiverSimulationProfile.CriticalFlowType.SubCriticalFlow)
                    subTypeRdo.Checked = true;
                else
                    superTypeRdo.Checked = true;
            }

        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPicBoxGrid((sender as ListBox).SelectedIndex, false);
        }

        private void UpdateSelectedGroup(List<Point> pts, bool alert = false)
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;
            int type = (index < sideOutObjects.Length) ? 0 : 1, count = (index < sideOutObjects.Length) ? index : index - sideOutObjects.Length;
            if (index < sideOutObjects.Length)
            {
                sideOutObjects[index].sideFlowPoints = pts;
                //if (sideOutObjects[index].criticalFlowType == RiverSimulationProfile.CriticalFlowType.SubCriticalFlow)
                //    subTypeRdo.Checked = true;
                //else
                //    superTypeRdo.Checked = true;
            }
            else
            {
                sideInObjects[index - sideOutObjects.Length].sideFlowPoints = pts;
                //if (sideInObjects[index - sideOutObjects.Length].criticalFlowType == RiverSimulationProfile.CriticalFlowType.SubCriticalFlow)
                //    subTypeRdo.Checked = true;
                //else
                //    superTypeRdo.Checked = true;
            }

            mapPicBox.SetSelectedGrid(SideFlowtUtility.GetSideFlowSets(sideOutObjects), SideFlowtUtility.GetSideFlowSets(sideInObjects), null, null, type, count, alert);
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

            int type = (index < sideOutObjects.Length) ? 0 : 1, count = (index < sideOutObjects.Length) ? index : index - sideOutObjects.Length;

            //檢查連續
            if (!SideFlowtUtility.IsContinuous(pl))
            {
                UpdateSelectedGroup(pl, true);
                MessageBox.Show("請圈選連續區域！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UpdateSelectedGroup(null);
                return;
            }

            //檢查重疊

            if(!CheckOverlapping(pl, SideFlowtUtility.GetSideFlowSets(sideOutObjects), count))
            {
                return;
            }
            if(!CheckOverlapping(pl, SideFlowtUtility.GetSideFlowSets(sideInObjects), count))
            {
                return;
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
            //var p = RiverSimulationProfile.profile;
            int index = listBox.SelectedIndex;

            String[] sideObjNames = new String[listBox.Items.Count];
            for(int i = 0; i < listBox.Items.Count; ++i)
            {
                sideObjNames[i] = listBox.Items[i].ToString();
            }

            SideFlowSetTableForm form = new SideFlowSetTableForm();
            form.SetGroupColors(mapPicBox.GetGroupColors());
            form.SetColorTable(mapPicBox.GetColorTable());
            form.SetSelectionItems(listBox, sideObjNames);

            form.SetFormMode("編輯側出/入流", "", sideOutObjects, sideInObjects);
            if (DialogResult.OK == form.ShowDialog())
            {
                //if (index < sideOutObjects.Length)
                //{
                //     //p.boundaryUpVerticalDistribution.SetArrayObject(form.VerticalVelocityDistributionData().Clone());
                //    sideOutObjects[index].flowData = form.
                //}
                //else
                //{
                //    sideInObjects[index - sideOutObjects.Length].criticalFlowType = RiverSimulationProfile.CriticalFlowType.SuperCriticalFlow;
                //}
            }
//            SetPicBoxGrid(listBox.SelectedIndex, false);
        }

        private void superTypeRdo_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                int index = listBox.SelectedIndex;
                if (index < sideOutObjects.Length)
                {
                    sideOutObjects[index].criticalFlowType = RiverSimulationProfile.CriticalFlowType.SuperCriticalFlow;
                }
                else
                {
                    sideInObjects[index - sideOutObjects.Length].criticalFlowType = RiverSimulationProfile.CriticalFlowType.SuperCriticalFlow;
                }
            }
        }

        private void subTypeRdo_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                int index = listBox.SelectedIndex;
                if (index < sideOutObjects.Length)
                {
                    sideOutObjects[index].criticalFlowType = RiverSimulationProfile.CriticalFlowType.SubCriticalFlow;
                }
                else
                {
                    sideInObjects[index - sideOutObjects.Length].criticalFlowType = RiverSimulationProfile.CriticalFlowType.SubCriticalFlow;
                }
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            bool allInputFinished = true;
            foreach (RiverSimulationProfile.SideFlowObject obj in sideOutObjects)
            {
                if (obj == null || obj.sideFlowPoints == null || obj.sideFlowPoints.Count == 0)
                {
                    allInputFinished = false;
                }
            }
            foreach (RiverSimulationProfile.SideFlowObject obj in sideInObjects)
            {
                if (obj == null || obj.sideFlowPoints == null || obj.sideFlowPoints.Count == 0)
                {
                    allInputFinished = false;
                }
            }

            if (!allInputFinished)
            {
                MessageBox.Show("輸入未完成！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

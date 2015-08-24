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
    public partial class SimulationResultForm : Form
    {
        public SimulationResultForm()
        {
            InitializeComponent();
        }

        RiverSimulationProfile p = null;
        private void SimulationResultForm_Load(object sender, EventArgs e)
        {
            p = RiverSimulationProfile.profile;
            posILbl.Text = String.Format("MAX:{0}", p.inputGrid.GetI);
            posJLbl.Text = String.Format("MAX:{0}", p.inputGrid.GetJ);
            if(p.Is3DMode())
            {
                posKLbl.Text = String.Format("MAX:{0}", p.verticalLevelNumber);
            }

        }

        private void graphRdo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tableRdo_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as RadioButton).Checked;
            //20150811後處理流程示範說明,
            //1. 若選擇“表格”：
            //  A. 則“參數”只能選擇一種，第2 個“參數”的下拉選單灰階。
            //  B. 且“形式”及“座標軸”也灰階。
            param2Cmb.Enabled = !chk;
            formGrp.Enabled = !chk;
            axisGrp.Enabled = !chk;
            InitialParam1(Param1Type.ParamTable);
        }

        private enum Param1Type {
            ParamTable,
        }

        private string[] tableItemsParam1 = {
                "初始底床高程(m)",
                "水深平均流速-U(m/s)",
                "水深平均流速-V(m/s)",
                "水深平均流速-UV 合向量的絕對值(m/s)",
                "底床剪應力(N/m2)",
                "水位(m)",
                "水深(m)",
                "流量-U(cms)",
                "流量-V(cms)",
                "底床高程(m)",
                "沖淤深度(m)",
                "水深平均濃度(ppm)",
                "粒徑分佈(%)",
                "流速-U(m/s)",
                "流速-V(m/s)",
                "流速-W(m/s)",
                "濃度(ppm)" };

        private void InitialParam1(Param1Type p)
        {
            if (p == Param1Type.ParamTable)
            {
                param1Cmb.DataSource = tableItemsParam1;
            }
            param1Cmb.SelectedIndex = 0;
        }

        private void param1Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            if(cmb.DataSource == tableItemsParam1)
            {
                switch(cmb.SelectedIndex)
                {
                    case 0: //初始底床高程(m)
                        timeGrp.Enabled = false;
                        poKPanel.Enabled = false;
                        break;
                    case 1: //水深平均流速-U(m/s)
                        timeGrp.Enabled = true;
                        poKPanel.Enabled = false;
                        break;
                    case 2: //水深平均流速-V(m/s)
                        timeGrp.Enabled = true;
                        poKPanel.Enabled = false;
                        break;
                    case 3: //水深平均流速-UV 合向量的絕對值(m/s)
                        poKPanel.Enabled = false;
                        break;
                    case 4: //底床剪應力(N/m2)
                        poKPanel.Enabled = false;
                        break;
                    case 5: //水位(m)
                        poKPanel.Enabled = false;
                        break;
                    case 6: //水深(m)
                        poKPanel.Enabled = false;
                        break;
                    case 7: //流量-U(cms)
                        poKPanel.Enabled = false;
                        break;
                    case 8: //流量-V(cms)
                        poKPanel.Enabled = false;
                        break;
                    case 9: //底床高程(m)
                        poKPanel.Enabled = false;
                        break;
                    case 10: //沖淤深度(m)
                        poKPanel.Enabled = false;
                        break;
                    case 11: //水深平均濃度(ppm)
                        poKPanel.Enabled = false;
                        break;
                    case 12: //粒徑分佈(%)
                        poKPanel.Enabled = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void posIchk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            posITxt.Enabled = !chk;
        }

        private void posJchk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            posJTxt.Enabled = !chk;
        }

        private void posKchk_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = (sender as CheckBox).Checked;
            posKTxt.Enabled = !chk;
        }

    }
}

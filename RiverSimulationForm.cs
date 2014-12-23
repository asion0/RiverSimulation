using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RiverSimulationApplication
{
    public partial class RiverSimulationForm : Form
    {
        public RiverSimulationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            if (DialogResult.OK != form.ShowDialog())
            {
                this.Close();
            }

            UpdateStatus();
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void waterModelingBtn_Click(object sender, EventArgs e)
        {
            if(!RiverSimulationProfile.profile.IsWaterModelingReady())
            {
                MessageBox.Show("請先完成前置設定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            WaterModelingForm form = new WaterModelingForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.waterModelingFinished = true;
            UpdateStatus();

        }

        private void simulationModuleBtn_Click(object sender, EventArgs e)
        {
            //if(!RiverSimulationProfile.profile.IsSimulationModuleReady())
            //{
            //    MessageBox.Show("請先完成前置設定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            SimulationModuleForm form = new SimulationModuleForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.simulationModuleFinished = true;
            UpdateStatus();
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            if (!RiverSimulationProfile.profile.IsImportReady())
            {
                MessageBox.Show("請先完成前置設定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            } 
            
            ImportForm form = new ImportForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.importFinished = true;
            UpdateStatus();
        }

        private void movableBedBtn_Click(object sender, EventArgs e)
        {
            if(!RiverSimulationProfile.profile.IsMovableBedReady())
            {
                MessageBox.Show("請先完成前置設定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            MovableBedForm form = new MovableBedForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.movableBedFinished = true;
            UpdateStatus();
        }

        private void initialConditionsBtn_Click(object sender, EventArgs e)
        {
            if(!RiverSimulationProfile.profile.IsInitialConditionsReady())
            {
                MessageBox.Show("請先完成前置設定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            InitialConditionsForm form = new InitialConditionsForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.initialConditionsFinished = true;
            UpdateStatus();
        }

        private void boundaryConditionsBtn_Click(object sender, EventArgs e)
        {
            if(!RiverSimulationProfile.profile.IsBoundaryConditionsReady())
            {
                MessageBox.Show("請先完成前置設定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            BoundaryConditionsForm form = new BoundaryConditionsForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.boundaryConditionsFinished = true;
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            RiverSimulationProfile p = RiverSimulationProfile.profile;;
               
            //RiverSimulationProfile p = RiverSimulationProfile.profile;
            Color FinishedButton = Color.LimeGreen;
            Color ReadyButton = Color.Gold;
            Color DisableButton = Color.FromArgb(255, 174, 201);     //20141117 客製化
            
            sampleFinishedBtn.BackColor = FinishedButton;
            sampleReadyBtn.BackColor = ReadyButton;
            sampleDisableBtn.BackColor = DisableButton;

            //importBtn.BackColor = (p.IsImportFinished()) ? FinishedButton : (p.IsImportReady()) ? ReadyButton : DisableButton;
            //simulationModuleBtn.BackColor = (p.IsSimulationModuleFinished()) ? FinishedButton : (p.IsSimulationModuleReady()) ? ReadyButton : DisableButton;

            simulationModuleBtn.BackColor = (p.IsSimulationModuleFinished()) ? FinishedButton : (p.IsSimulationModuleReady()) ? ReadyButton : DisableButton;

            importBtn.BackColor = (p.IsImportFinished()) ? FinishedButton : (p.IsImportReady()) ? ReadyButton : DisableButton;

            waterModelingBtn.BackColor = (p.IsWaterModelingFinished()) ? FinishedButton : (p.IsWaterModelingReady()) ? ReadyButton : DisableButton;

            movableBedBtn.BackColor = (p.IsMovableBedFinished()) ? FinishedButton : (p.IsMovableBedReady()) ? ReadyButton : DisableButton;
           //movableBedBtn.Enabled = p.IsMovableBedReady();

            initialConditionsBtn.BackColor = (p.IsInitialConditionsFinished()) ? FinishedButton : (p.IsInitialConditionsReady()) ? ReadyButton : DisableButton;
            //initialConditionsBtn.Enabled = p.IsInitialConditionsReady();

            boundaryConditionsBtn.BackColor = (p.IsBoundaryConditionsFinished()) ? FinishedButton : (p.IsBoundaryConditionsReady()) ? ReadyButton : DisableButton;
            //boundaryConditionsBtn.Enabled = p.IsBoundaryConditionsReady();

            runSimulationBtn.BackColor = (p.IsRunSimulationFinished()) ? FinishedButton : (p.IsRunSimulationReady()) ? ReadyButton : DisableButton;
            //runSimulationBtn.Enabled = p.IsRunSimulationReady();

            simulationResultBtn.BackColor = (p.IsSimulationResultFinished()) ? FinishedButton : (p.IsSimulationResultReady()) ? ReadyButton : DisableButton;
            //simulationResultBtn.Enabled = p.IsSimulationResultReady();
        }

        private void runSimulationBtn_Click(object sender, EventArgs e)
        {
            if (!RiverSimulationProfile.profile.IsRunSimulationReady())
            {
                String errStr = "尚有參數尚未完成：\r\n" +
                                "水理參數 - 物理參數尚未設定！";

                MessageBox.Show(errStr, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            RunSimulationForm form = new RunSimulationForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }

            RiverSimulationProfile.profile.runSimulationFinished = true;
            UpdateStatus();
        }

        SolidBrush lineBrush = new SolidBrush(Color.Blue);
        Pen p = new Pen(Color.Black, 1);
        Pen pp = new Pen(Color.Red, 3);
        private void RiverSimulationForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            //int lineWidth = 3;
            Button up, dn, lf, rt;

            //line from importBtn to simulationModuleBtn
            up = simulationModuleBtn;
            dn = importBtn;
            g.DrawLine(p, up.Left + up.Width / 2, up.Bottom,
                          up.Left + up.Width / 2, dn.Top);

            //line under simulationModuleBtn
            up = importBtn;
            dn = waterModelingBtn;
            g.DrawLine(p, up.Left + up.Width / 2, up.Bottom,
                          up.Left + up.Width / 2, (dn.Top + up.Bottom) / 2);

            //lines upper waterModelingBtn and movableBedBtn
            up = importBtn;
            dn = waterModelingBtn;
            lf = waterModelingBtn;
            rt = movableBedBtn;
            Point[] pts =
            {
                 new Point(lf.Left + lf.Width / 2, lf.Top),
                 new Point(lf.Left + lf.Width / 2, (dn.Top + up.Bottom) / 2),
                 new Point(rt.Left + rt.Width / 2, (dn.Top + up.Bottom) / 2),
                 new Point(rt.Left + rt.Width / 2, rt.Top),
            };
            g.DrawLines(p, pts);

            //lines under waterModelingBtn and movableBedBtn
            up = waterModelingBtn;
            dn = initialConditionsBtn;
            lf = waterModelingBtn;
            rt = movableBedBtn;
            Point[] pts2 =
            {
                 new Point(lf.Left + lf.Width / 2, lf.Bottom),
                 new Point(lf.Left + lf.Width / 2, (dn.Top + 2 * up.Bottom) / 3),
                 new Point(rt.Left + rt.Width / 2, (dn.Top + 2 * up.Bottom) / 3),
                 new Point(rt.Left + rt.Width / 2, rt.Bottom),
            };
            g.DrawLines(p, pts2);

            //lines between waterModelingBtn and initialConditionsBtn
            up = waterModelingBtn;
            dn = initialConditionsBtn;
            lf = initialConditionsBtn;
            rt = boundaryConditionsBtn;
            g.DrawLine(p, (lf.Left + rt.Left + rt.Width) / 2, (dn.Top + 2 * up.Bottom) / 3,
                          (lf.Left + rt.Left + rt.Width) / 2, (up.Bottom + 2 * dn.Top) / 3);

            //lines upper initialConditionsBtn and boundaryConditionsBtn
            up = waterModelingBtn;
            dn = initialConditionsBtn;
            lf = initialConditionsBtn;
            rt = boundaryConditionsBtn; 
            Point[] pts3 =
            {
                 new Point(lf.Left + lf.Width / 2, initialConditionsBtn.Top),
                 new Point(lf.Left + lf.Width / 2, (up.Bottom + 2 * dn.Top) / 3),
                 new Point(rt.Left + rt.Width / 2, (up.Bottom + 2 * dn.Top) / 3),
                 new Point(rt.Left + rt.Width / 2, rt.Top),
            };
            g.DrawLines(p, pts3);

            //lines under initialConditionsBtn and boundaryConditionsBtn
            up = initialConditionsBtn;
            dn = runSimulationBtn;
            lf = initialConditionsBtn;
            rt = boundaryConditionsBtn;
            Point[] pts4 =
            {
                 new Point(lf.Left + lf.Width / 2, up.Bottom),
                 new Point(lf.Left + lf.Width / 2, (up.Bottom + dn.Top) / 2),
                 new Point(rt.Left + rt.Width / 2, (up.Bottom + dn.Top) / 2),
                 new Point(rt.Left + rt.Width / 2, rt.Bottom),
            };
            g.DrawLines(p, pts4);

            //line upper runSimulationBtn
            up = initialConditionsBtn;
            dn = runSimulationBtn;
            lf = initialConditionsBtn;
            rt = boundaryConditionsBtn;
            g.DrawLine(p, dn.Left + dn.Width / 2, (up.Bottom + dn.Top) / 2,
                          dn.Left + dn.Width / 2, dn.Top);

            ////line from dataVerifyBtn to runSimulationBtn
            //up = dataVerifyBtn;
            //dn = runSimulationBtn;
            //g.DrawLine(p, up.Left + up.Width / 2, up.Bottom,
            //              up.Left + up.Width / 2, dn.Top);

            //lines between runSimulationBtn and simulationResultBtn
            up = runSimulationBtn;
            dn = simulationResultBtn;
            g.DrawLine(p, up.Left + up.Width / 2, up.Bottom,
                          up.Left + up.Width / 2, dn.Top);

        }

        private void runSimulationResultBtn_Click(object sender, EventArgs e)
        {
            if (!RiverSimulationProfile.profile.IsRunSimulationReady())
            {
                MessageBox.Show("請先完成前置設定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //RunSimulationForm form = new RunSimulationForm();
            //if (DialogResult.OK == form.ShowDialog())
            //{

            //}
            //RiverSimulationProfile.profile.runSimulationFinished = true;
            //UpdateStatus();
        }

        private void aboutMnuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void exitMnuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void feedMnuItem_Click(object sender, EventArgs e)
        {
            FeedbackForm form = new FeedbackForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
        }

        private void descriptionMnuItem_Click(object sender, EventArgs e)
        {
            InputForm dlg = new InputForm();
            dlg.Text = "檔案敘述";
            dlg.desc.Text = "請輸入檔案敘述";
            dlg.inputTxt.Text = "";
            if (DialogResult.OK != dlg.ShowDialog())
            {
                return;
            }


        }

        private void userManualMenuItem_Click(object sender, EventArgs e)
        {
            Utility.ShellExecute(Environment.CurrentDirectory + "\\Manual.pdf");
        }

        private void RiverSimulationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (RiverSimulationProfile.profile != null)
            {
                string tempSave = Program.documentPath + Program.tempSaveName;
                RiverSimulationProfile.SerializeBinary(RiverSimulationProfile.profile, tempSave);
            }

        }


    }
}

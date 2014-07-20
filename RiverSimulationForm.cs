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
    public partial class RiverSimulationForm : Form
    {
        public RiverSimulationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            if(!RiverSimulationProfile.profile.IsSimulationModuleReady())
            {
                MessageBox.Show("請先完成前置設定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SimulationModuleForm form = new SimulationModuleForm();
            if (DialogResult.OK == form.ShowDialog())
            {

            }
            RiverSimulationProfile.profile.simulationModuleFinished = true;
            UpdateStatus();
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
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
            RiverSimulationProfile p = RiverSimulationProfile.profile;
            Color FinishedButton = Color.LimeGreen;
            Color ReadyButton = Color.Goldenrod;
            Color DisableButton = Color.Red;
            
            sampleFinishedBtn.BackColor = FinishedButton;
            sampleReadyBtn.BackColor = ReadyButton;
            sampleDisableBtn.BackColor = DisableButton;

            importBtn.BackColor = (p.IsImportFinished()) ? FinishedButton : (p.IsImportReady()) ? ReadyButton : DisableButton;
            //importBtn.Enabled = p.IsImportReady();

            simulationModuleBtn.BackColor = (p.IsSimulationModuleFinished()) ? FinishedButton : (p.IsSimulationModuleReady()) ? ReadyButton : DisableButton;
            //simulationModuleBtn.Enabled = p.IsSimulationModuleReady();

            waterModelingBtn.BackColor = (p.IsWaterModelingFinished()) ? FinishedButton : (p.IsWaterModelingReady()) ? ReadyButton : DisableButton;
            //waterModelingBtn.Enabled = p.IsWaterModelingReady();

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
                MessageBox.Show("請先完成前置設定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        Pen p = new Pen(Color.Blue, 3);
        private void RiverSimulationForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            //int lineWidth = 3;

            //line from importBtn to simulationModuleBtn
            g.DrawLine(p, importBtn.Left + importBtn.Width / 2, importBtn.Bottom,
                          importBtn.Left + importBtn.Width / 2, simulationModuleBtn.Top);

            //line under simulationModuleBtn
            g.DrawLine(p, simulationModuleBtn.Left + simulationModuleBtn.Width / 2, simulationModuleBtn.Bottom,
                          simulationModuleBtn.Left + simulationModuleBtn.Width / 2, (waterModelingBtn.Top + simulationModuleBtn.Bottom) / 2);

            //lines between waterModelingBtn and movableBedBtn
            Point[] pts =
            {
                 new Point(waterModelingBtn.Left + waterModelingBtn.Width / 2, waterModelingBtn.Top),
                 new Point(waterModelingBtn.Left + waterModelingBtn.Width / 2, (waterModelingBtn.Top + simulationModuleBtn.Bottom) / 2),
                 new Point(movableBedBtn.Left + movableBedBtn.Width / 2, (waterModelingBtn.Top + simulationModuleBtn.Bottom) / 2),
                 new Point(movableBedBtn.Left + movableBedBtn.Width / 2, movableBedBtn.Top),
            };
            g.DrawLines(p, pts);

            //lines between waterModelingBtn and movableBedBtn
            Point[] pts2 =
            {
                 new Point(waterModelingBtn.Left + waterModelingBtn.Width / 2, waterModelingBtn.Bottom),
                 new Point(waterModelingBtn.Left + waterModelingBtn.Width / 2, (initialConditionsBtn.Top + 2 * waterModelingBtn.Bottom) / 3),
                 new Point(movableBedBtn.Left + movableBedBtn.Width / 2, (initialConditionsBtn.Top + 2 * waterModelingBtn.Bottom) / 3),
                 new Point(movableBedBtn.Left + movableBedBtn.Width / 2, movableBedBtn.Bottom),
            };
            g.DrawLines(p, pts2);

            //lines between initialConditionsBtn and boundaryConditionsBtn
            Point[] pts3 =
            {
                 new Point(initialConditionsBtn.Left + initialConditionsBtn.Width / 2, initialConditionsBtn.Top),
                 new Point(initialConditionsBtn.Left + initialConditionsBtn.Width / 2, (waterModelingBtn.Bottom + 2 * initialConditionsBtn.Top) / 3),
                 new Point(boundaryConditionsBtn.Left + boundaryConditionsBtn.Width / 2, (waterModelingBtn.Bottom + 2 * initialConditionsBtn.Top) / 3),
                 new Point(boundaryConditionsBtn.Left + boundaryConditionsBtn.Width / 2, boundaryConditionsBtn.Top),
            };
            g.DrawLines(p, pts3);

            g.DrawLine(p, simulationModuleBtn.Left + simulationModuleBtn.Width / 2, (initialConditionsBtn.Top + 2 * waterModelingBtn.Bottom) / 3,
                          simulationModuleBtn.Left + simulationModuleBtn.Width / 2, (waterModelingBtn.Bottom + 2 * initialConditionsBtn.Top) / 3);

            //line under initialConditionsBtn
            Point[] pts4 =
            {
                 new Point(initialConditionsBtn.Left + initialConditionsBtn.Width / 2, initialConditionsBtn.Bottom),
                 new Point(initialConditionsBtn.Left + initialConditionsBtn.Width / 2, (initialConditionsBtn.Bottom + runSimulationBtn.Top) / 2),
                 new Point(boundaryConditionsBtn.Left + boundaryConditionsBtn.Width / 2, (initialConditionsBtn.Bottom + runSimulationBtn.Top) / 2),
                 new Point(boundaryConditionsBtn.Left + boundaryConditionsBtn.Width / 2, boundaryConditionsBtn.Bottom),
            };
            g.DrawLines(p, pts4);

            g.DrawLine(p, runSimulationBtn.Left + runSimulationBtn.Width / 2, (initialConditionsBtn.Bottom + runSimulationBtn.Top) / 2,
                          runSimulationBtn.Left + runSimulationBtn.Width / 2, runSimulationBtn.Top);

            //lines between runSimulationBtn and simulationResultBtn
            g.DrawLine(p, runSimulationBtn.Left + runSimulationBtn.Width / 2, runSimulationBtn.Bottom,
                          simulationResultBtn.Left + simulationResultBtn.Width / 2, simulationResultBtn.Top);
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



    }
}

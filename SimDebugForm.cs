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
    public partial class SimDebugForm : Form
    {
        public SimDebugForm()
        {
            InitializeComponent();
        }

        private void SimDebugForm_Load(object sender, EventArgs e)
        {
            case01Rdo.Checked = true;
        }
        public void AddResultMessage(string s)
        {
           tbResult.AppendText(s);
        }

        public string inputFile, dataFile;
        public enum RunMode
        {
            InputData,
            OutputFile,
            ExternalFile,
        };
        public RunMode runMode = RunMode.InputData;
        private void source_CheckedChanged(object sender, EventArgs e)
        {
            if(case01Rdo.Checked)
            {
                inputFile = "ul101_01.i";
                dataFile = "ul101.dat";
                runMode = RunMode.InputData;
            }
            else if(case02Rdo.Checked)
            {
                inputFile = "ul101_02.i";
                dataFile = "ul101.dat";
                runMode = RunMode.InputData;
            }
            else if (case03Rdo.Checked)
            {
                inputFile = "ul101_03.i";
                dataFile = "ul101_03.dat";
                runMode = RunMode.InputData;
            }
            else if (case04Rdo.Checked)
            {
                inputFile = "ul101_04.i";
                dataFile = "ul101.dat";
                runMode = RunMode.InputData;
            }
            else if (case05Rdo.Checked)
            {
                inputFile = "ul101_05.i";
                dataFile = "ul101.dat";
                runMode = RunMode.InputData;
            }
            else if(importTextRdo.Checked)
            {
                inputFile = fileCombo.Text;
                dataFile = importDurationTxt.Text;
                runMode = RunMode.OutputFile;
            }
            else if(externalCaseRdo.Checked)
            {
                inputFile = exeTxt.Text;
                dataFile = paramTxt.Text;
                runMode = RunMode.ExternalFile;
            }
        }

        private void fileCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(runMode == RunMode.OutputFile)
            {
                inputFile = fileCombo.Text;
            }
        }

        private void importDurationTxt_TextChanged(object sender, EventArgs e)
        {
            if (runMode == RunMode.OutputFile)
            {
                dataFile = importDurationTxt.Text;
            }
        }

        private void exeTxt_TextChanged(object sender, EventArgs e)
        {
            if (runMode == RunMode.ExternalFile)
            {
                inputFile = exeTxt.Text;
            }
        }

        private void paramTxt_TextChanged(object sender, EventArgs e)
        {
            if (runMode == RunMode.ExternalFile)
            {
                dataFile = paramTxt.Text;
            }
        }
    }
}

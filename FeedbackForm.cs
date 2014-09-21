﻿using System;
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
    public partial class FeedbackForm : Form
    {
        public FeedbackForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "mailto:hsieh0182@itri.org.tw?subject=水理動床模組問題回饋&body= ";
            proc.Start();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "mailto:yang.jc4169@gmail.com?subject=水理動床模組問題回饋&body= ";
            proc.Start();
        }

        private void FeedbackForm_Load(object sender, EventArgs e)
        {
            mrRdo.Checked = true;
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}

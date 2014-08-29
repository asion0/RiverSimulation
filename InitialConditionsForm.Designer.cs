namespace RiverSimulationApplication
{
    partial class InitialConditionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comment = new System.Windows.Forms.WebBrowser();
            this.ok = new System.Windows.Forms.Button();
            this.setting = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.initialConcentrationBtn = new System.Windows.Forms.Button();
            this.flowTypeGroup = new System.Windows.Forms.GroupBox();
            this.assumingSectionChk = new System.Windows.Forms.CheckBox();
            this.initialWaterLevelBtn = new System.Windows.Forms.Button();
            this.vFlowSpeedBtn = new System.Windows.Forms.Button();
            this.uFlowSpeedBtn = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowTypeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // comment
            // 
            this.comment.AllowWebBrowserDrop = false;
            this.comment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comment.IsWebBrowserContextMenuEnabled = false;
            this.comment.Location = new System.Drawing.Point(396, 13);
            this.comment.MinimumSize = new System.Drawing.Size(20, 20);
            this.comment.Name = "comment";
            this.comment.ScrollBarsEnabled = false;
            this.comment.Size = new System.Drawing.Size(488, 225);
            this.comment.TabIndex = 8;
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ok.Location = new System.Drawing.Point(788, 637);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(96, 32);
            this.ok.TabIndex = 7;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // setting
            // 
            this.setting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.setting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.setting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.setting.Location = new System.Drawing.Point(397, 264);
            this.setting.Name = "setting";
            this.setting.Size = new System.Drawing.Size(487, 360);
            this.setting.TabIndex = 6;
            this.setting.Text = "設定內容";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.groupBox2);
            this.mainPanel.Controls.Add(this.flowTypeGroup);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(391, 670);
            this.mainPanel.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.initialConcentrationBtn);
            this.groupBox2.Location = new System.Drawing.Point(12, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 71);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "動床模組";
            // 
            // initialConcentrationBtn
            // 
            this.initialConcentrationBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.initialConcentrationBtn.Location = new System.Drawing.Point(19, 22);
            this.initialConcentrationBtn.Name = "initialConcentrationBtn";
            this.initialConcentrationBtn.Size = new System.Drawing.Size(161, 26);
            this.initialConcentrationBtn.TabIndex = 2;
            this.initialConcentrationBtn.Text = "濃度";
            this.initialConcentrationBtn.UseVisualStyleBackColor = true;
            this.initialConcentrationBtn.Click += new System.EventHandler(this.initialConcentrationBtn_Click);
            // 
            // flowTypeGroup
            // 
            this.flowTypeGroup.Controls.Add(this.assumingSectionChk);
            this.flowTypeGroup.Controls.Add(this.initialWaterLevelBtn);
            this.flowTypeGroup.Controls.Add(this.vFlowSpeedBtn);
            this.flowTypeGroup.Controls.Add(this.uFlowSpeedBtn);
            this.flowTypeGroup.Location = new System.Drawing.Point(12, 11);
            this.flowTypeGroup.Name = "flowTypeGroup";
            this.flowTypeGroup.Size = new System.Drawing.Size(366, 156);
            this.flowTypeGroup.TabIndex = 0;
            this.flowTypeGroup.TabStop = false;
            this.flowTypeGroup.Text = "水理模組";
            // 
            // assumingSectionChk
            // 
            this.assumingSectionChk.AutoSize = true;
            this.assumingSectionChk.Location = new System.Drawing.Point(19, 127);
            this.assumingSectionChk.Name = "assumingSectionChk";
            this.assumingSectionChk.Size = new System.Drawing.Size(96, 16);
            this.assumingSectionChk.TabIndex = 3;
            this.assumingSectionChk.Text = "垂向流速剖面";
            this.assumingSectionChk.UseVisualStyleBackColor = true;
            this.assumingSectionChk.CheckedChanged += new System.EventHandler(this.assumingSectionChk_CheckedChanged);
            // 
            // initialWaterLevelBtn
            // 
            this.initialWaterLevelBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.initialWaterLevelBtn.Location = new System.Drawing.Point(19, 91);
            this.initialWaterLevelBtn.Name = "initialWaterLevelBtn";
            this.initialWaterLevelBtn.Size = new System.Drawing.Size(161, 26);
            this.initialWaterLevelBtn.TabIndex = 2;
            this.initialWaterLevelBtn.Text = "水位";
            this.initialWaterLevelBtn.UseVisualStyleBackColor = true;
            this.initialWaterLevelBtn.Click += new System.EventHandler(this.initialWaterLevelBtn_Click);
            // 
            // vFlowSpeedBtn
            // 
            this.vFlowSpeedBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.vFlowSpeedBtn.Location = new System.Drawing.Point(19, 56);
            this.vFlowSpeedBtn.Name = "vFlowSpeedBtn";
            this.vFlowSpeedBtn.Size = new System.Drawing.Size(161, 26);
            this.vFlowSpeedBtn.TabIndex = 2;
            this.vFlowSpeedBtn.Text = "水平流速-V";
            this.vFlowSpeedBtn.UseVisualStyleBackColor = true;
            this.vFlowSpeedBtn.Click += new System.EventHandler(this.vFlowSpeedBtn_Click);
            // 
            // uFlowSpeedBtn
            // 
            this.uFlowSpeedBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.uFlowSpeedBtn.Location = new System.Drawing.Point(19, 21);
            this.uFlowSpeedBtn.Name = "uFlowSpeedBtn";
            this.uFlowSpeedBtn.Size = new System.Drawing.Size(161, 26);
            this.uFlowSpeedBtn.TabIndex = 2;
            this.uFlowSpeedBtn.Text = "水平流速-U";
            this.uFlowSpeedBtn.UseVisualStyleBackColor = true;
            this.uFlowSpeedBtn.Click += new System.EventHandler(this.uFlowSpeedBtn_Click);
            // 
            // InitialConditionsForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 682);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.setting);
            this.Name = "InitialConditionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "初始條件";
            this.Load += new System.EventHandler(this.InitialConditionsForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.flowTypeGroup.ResumeLayout(false);
            this.flowTypeGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser comment;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Label setting;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button initialConcentrationBtn;
        private System.Windows.Forms.GroupBox flowTypeGroup;
        private System.Windows.Forms.CheckBox assumingSectionChk;
        private System.Windows.Forms.Button initialWaterLevelBtn;
        private System.Windows.Forms.Button vFlowSpeedBtn;
        private System.Windows.Forms.Button uFlowSpeedBtn;

    }
}